using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Ext.Net;
using Artebit.Restaurante.Global.RegrasNegocio.Estoque;
using Artebit.Restaurante.Global.RegrasNegocio.Atendimento;
using Newtonsoft.Json.Linq;

namespace Administracao.Funcao.Paginas
{
    public partial class observacao : System.Web.UI.Page    
    {

        protected void Page_load(object sender, EventArgs e)
        {
            MemoriaWeb.ValidaSessao();

            Contexto.AbrirContexto();

            if (!IsPostBack)
            {
                carregarProdutos();
                StoreGrupo_RefreshData(StoreGrupo, new StoreRefreshDataEventArgs());
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



        // Função para atualizar dados
        protected void Atualizar(object sender, DirectEventArgs e)
        {
            try
            {
                string j = e.ExtraParams["Produtos"];
                List<object> items = JSON.Deserialize<List<object>>(j);

                foreach (object a in items)
                {
                    int codigo = Convert.ToInt32(JContainer.FromObject(a)["codigo"].ToString());

                    EPRODUTO obj = new EPRODUTO();
                    ProdutoControl control = new ProdutoControl();
                    obj.idProduto = codigo;
                    obj.idEmpresa = Memoria.Empresa;

                    obj =
                        (EPRODUTO)
                        control.ExecutaFuncao(obj, Artebit.Restaurante.Global.RegrasNegocio.Funcoes.Buscar, null);

                    obj.grupo = Convert.ToInt32(ComboBoxGrupo.Value);

                    RowSelectionModel sm = GridObservacoes.SelectionModel.Primary as RowSelectionModel;

                    obj.EOBSERVACAO.Clear();
                    foreach (SelectedRow row in sm.SelectedRows)
                    {
                        EOBSERVACAO obs = new EOBSERVACAO();
                        ObservacaoControl ct = new ObservacaoControl();
                        obs.idObs = Convert.ToInt32(row.RecordID);  //row.RecordID é retorno do IDProperty="id" definido na store

                        obs = (EOBSERVACAO)ct.ExecutaFuncao(obs, Artebit.Restaurante.Global.RegrasNegocio.Funcoes.Buscar, null);

                        obj.EOBSERVACAO.Add(obs);
                    }

                }

                Contexto.Atual.SaveChanges();

                X.Msg.Alert("Sucess", "Produto(s) atualizado(s) com sucesso.").Show();
            }
            catch (Exception ex)
            {
                X.Msg.Alert("Erro", "Erro ao atualizar produto(s).").Show();
            }

        }




        #region refresh functions

        protected void StoreGrupo_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
                GruposControl ct = new GruposControl();
                IQueryable<EGRUPO> lista = (IQueryable<EGRUPO>)ct.ExecutaFuncao(null, Artebit.Restaurante.Global.RegrasNegocio.Funcoes.BuscarLista, null);

                var dados = from a in lista
                            select new
                            {
                                id = a.idGrupo,
                                nome = a.codGrupo + " - " + a.nome
                            };

                StoreGrupo.DataSource = dados;
                StoreGrupo.DataBind();
        }

        protected void StoreObservacoes_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (ComboBoxGrupo.Value != null)
            {
                EGRUPO grup = new EGRUPO();

                grup.idGrupo = Convert.ToInt32(ComboBoxGrupo.Value);
                grup.idEmpresa = Convert.ToInt32(Memoria.Empresa);

                GruposControl ct = new GruposControl();

                grup = (EGRUPO)ct.ExecutaFuncao(grup, Artebit.Restaurante.Global.RegrasNegocio.Funcoes.Buscar, null);

                List<EOBSERVACAO> lista = (List<EOBSERVACAO>)ct.ExecutaFuncao(grup, Artebit.Restaurante.Global.RegrasNegocio.Funcoes.BuscarAtual, null);

                if (lista != null)
                {
                    var dados = from a in lista
                                select new
                                {
                                    id = a.idObs,
                                    descricao = a.descricao
                                };

                    StoreObservacoes.DataSource = dados;
                    StoreObservacoes.DataBind();
                }
                else
                {
                    StoreObservacoes.DataSource = lista;
                    StoreObservacoes.DataBind();
                }
            }
            else
            {
                List<EOBSERVACAO> lista = new List<EOBSERVACAO>();
                StoreObservacoes.DataSource = lista;
                StoreObservacoes.DataBind();
            }
        }

        #endregion



    }
}