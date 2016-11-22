using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio.Atendimento;
using Artebit.Restaurante.Global.RegrasNegocio.Estoque;
using Ext.Net;

namespace Administracao.Funcao.Paginas
{
    public partial class planoFidelidade : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
         protected void Page_load(object sender, EventArgs e)
        {
            MemoriaWeb.ValidaSessao();

            Contexto.AbrirContexto();
            
            if (!IsPostBack)
            {
                carregarProdutos();
                carregaPlanos();
            }
        }

        
       
        
        protected void carregarProdutos()
        {
            ProdutoControl ct = new ProdutoControl();
            IQueryable<EPRODUTO> lista = (IQueryable<EPRODUTO>)ct.ExecutaFuncao(null, Artebit.Restaurante.Global.RegrasNegocio.Funcoes.BuscarLista, null);

            var dados = from e in lista
                        select new
                                   {
                                       codigo = e.idProduto,
                                       nome = e.nome
                                   };

            storeProdutos.DataSource = dados;
            storeProdutos.DataBind();
        }

        
        protected void carregaPlanos()
        {
            AFIDELIDADE obj = new AFIDELIDADE();
            FidelidadeControl control = new FidelidadeControl();

            obj.ativo = true;
            obj.tipo = 1;
            obj.idFidelidade = 0;
            obj.dataCriacao = null;
            obj.dataAlteracao = null;
            obj.diaTodo = null;
            obj.moeda = null;
            obj.nome = null;
            obj.usrCriacao = null;
            obj.usrAlteracao = null;
            obj.valorPorReal = null;
            obj.horarioInicial = null;
            obj.horarioFinal = null;

            IQueryable<AFIDELIDADE> lista =
                (IQueryable<AFIDELIDADE>)control.ExecutaFuncao(obj, Artebit.Restaurante.Global.RegrasNegocio.Funcoes.BuscarLista, null);

            var dados = from b in lista
                        select new
                                   {
                                       codigo = b.idFidelidade,
                                       nome = b.nome,
                                       valor = ""
                                   };
            storePlanos.DataSource = dados;
            storePlanos.DataBind();
        }

        
        
        // Função para atualizar dados
        protected void Atualizar(object sender, DirectEventArgs e)
        {
            try
            {
                string json = e.ExtraParams["Values"];

                if (string.IsNullOrEmpty(json))
                {
                    return;
                }

                List<string> planos = new List<string>();

                XmlNode xml = JSON.DeserializeXmlNode("{records:{record:" + json + "}}");

                foreach (XmlNode row in xml.SelectNodes("records/record"))
                {
                    string codigo = row.SelectSingleNode("codigo").InnerXml;
                    string valor = row.SelectSingleNode("valor").InnerXml;

                    if (!String.IsNullOrEmpty(valor))
                    {
                        planos.Add(codigo + "_" + valor);
                    }
                }

                if (planos.Count > 0)
                {
                    string j = e.ExtraParams["Produtos"];
                    List<object> items = JSON.Deserialize<List<object>>(j);
                    foreach (object a in items)
                    {
                        int codigo = Convert.ToInt32(Newtonsoft.Json.Linq.JContainer.FromObject(a)["codigo"].ToString());

                        foreach (string pp in planos)
                        {
                            String[] dados = pp.Split('_');
                            int codplano = Convert.ToInt32(dados[0]);
                            decimal valor = Convert.ToDecimal(dados[1].Replace(".", ","));

                            APRDFIDELIDADE vinculo = new APRDFIDELIDADE();
                            vinculo.idEmpresa = Memoria.Empresa;
                            vinculo.idFilial = Memoria.Filial;
                            vinculo.idFidelidade = codplano;
                            vinculo.valorMoeda = valor;
                            vinculo.idProduto = codigo;

                            if (Contexto.Atual.APRDFIDELIDADE.Any(ab => ab.idEmpresa == vinculo.idEmpresa
                                                                        && ab.idFidelidade == vinculo.idFidelidade
                                                                        && ab.idFilial == vinculo.idFilial
                                                                        && ab.idProduto == vinculo.idProduto))
                            {
                                APRDFIDELIDADE deletado =
                                    Contexto.Atual.APRDFIDELIDADE.SingleOrDefault(
                                        ab => ab.idEmpresa == vinculo.idEmpresa
                                              && ab.idFidelidade == vinculo.idFidelidade
                                              && ab.idFilial == vinculo.idFilial
                                              && ab.idProduto == vinculo.idProduto);

                                Contexto.Atual.APRDFIDELIDADE.DeleteObject(deletado);
                            }

                            Contexto.Atual.AddToAPRDFIDELIDADE(vinculo);
                        }

                    }

                    Contexto.Atual.SaveChanges();

                    X.Msg.Alert("Sucess", "Produto(s) atualizado(s) com sucesso.").Show();
                    
                }
            }catch(Exception ex)
            {
                X.Msg.Alert("Erro","Erro ao atualizar produto(s).").Show();
            }

        }
    }
}