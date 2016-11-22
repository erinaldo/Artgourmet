using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Ext.Net;

namespace Artebit.Restaurante.Administracao.PDV
{
    public partial class Aliquota : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MemoriaWeb.ValidaSessao();

            Contexto.AbrirContexto();

            if (!IsPostBack)
            {
                CarregaPrincipal();
                VerificarPermissoes();
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            Contexto.FecharContexto();
        }

        private void VerificarPermissoes()
        {
            var perfil = new GPERFIL();
            var per = new PerfilControl();
            perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
            perfil = (GPERFIL)per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
            var compl = new List<string> { "AD", "Aliquota", "0" };
            bool resultado = false;


            #region Ativar/Desativar Itens
            compl[2] = "19";//Ativar/Desativar
            resultado = (bool)per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

            if (!resultado)
            {
                btnAtivarDesativarG.Disabled = true;
            }
            #endregion

            #region Adicionar
            compl[2] = "18";
            resultado = (bool)per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

            if (!resultado)
            {
                btnNovoG.Disabled = true;
            }
            #endregion

            #region Excluir
            compl[2] = "20";
            resultado = (bool)per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

            if (!resultado)
            {
                btnExcluirG.Disabled = true;
            }
            #endregion

            #region Editar
            compl[2] = "2";
            resultado = (bool)per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

            if (!resultado)
            {
                btnEditarG.Disabled = true;
            }
            #endregion

        }

        private void CarregaPrincipal()
        {
            Global.RegrasNegocio.Atendimento.AliquotaControl ct = new Global.RegrasNegocio.Atendimento.AliquotaControl();
            IQueryable<AALIQUOTA> lista = (IQueryable<AALIQUOTA>) ct.ExecutaFuncao(null, Funcoes.BuscarLista, null);

            var dados = from a in lista
                        select new
                                   {
                                       codigo = a.idAliquota,
                                       //posicao = a.posicao,
                                       aliquota = a.aliquota,
                                       tipoImposto = a.tpImposto,
                                       alq = a.valor,
                                       //cst = a.cst,
                                       ativo = a.ativo == true ? "Ativo" : "Inativo"
                                   };
            
            StorePrincipal.DataSource = dados;
            StorePrincipal.DataBind();
        }

        // Função para carregar os dados no formulário
        protected void GridAcao(object sender, DirectEventArgs e)
        {
            int id = Convert.ToInt32(e.ExtraParams["codigo"]);
            string comando = Convert.ToString(e.ExtraParams["command"]);

            AALIQUOTA obj = new AALIQUOTA();
            Global.RegrasNegocio.Atendimento.AliquotaControl ctrl = new Global.RegrasNegocio.Atendimento.AliquotaControl();


            #region Editar
            if (comando == "editar" && btnEditarG.Disabled == false)
            {
                //-------------------------------------------------------------------------------------------
                //Preenchendo os campos com as informações do Aliquota
                FormPrincipal.AddScript(
                    "if(StoreFormulario.allData != null) { PagingToolbar1.changePage(StoreFormulario.allData.indexOfKey(" +
                    id.ToString() + ") + 1); }" + " else { PagingToolbar1.changePage(StoreFormulario.indexOfId(" + id.ToString() +
                    ") + 1);}");

                JanelaPrincipal.Show();
            }
            #endregion

            #region Excluir
            if (comando == "excluir")
            {
                bool res = (bool)ctrl.ExecutaFuncao(obj, Funcoes.Cancelar, null);

                if (res)
                    X.Msg.Alert("Alerta", "A Aliquota foi excluido").Show();

                CarregaPrincipal();
            }
            #endregion

            #region Ativar/Desativar
            if (comando == "ativar")
            {
                bool sucesso = true;

                foreach (var i in RowSelectionModel1.SelectedRows)
                {
                    id = Convert.ToInt32(i.RecordID);
                    AALIQUOTA ali = new AALIQUOTA();

                    ali.idAliquota = id;
                    ali.idEmpresa = Memoria.Empresa;
                    ali = (AALIQUOTA)ctrl.ExecutaFuncao(ali, Funcoes.Buscar, null);

                    ali.ativo = !ali.ativo;
                    bool res = (bool)ctrl.ExecutaFuncao(ali, Funcoes.Atualizar, null);

                    if (!res)
                    {
                        sucesso = false;
                        break;
                    }
                }

                if (sucesso)
                {
                    CarregaPrincipal();
                    Notification.Show(new NotificationConfig
                    {
                        Title = "Informação",
                        Icon = Icon.Information,
                        Html = "Itens Ativados/Desativados com sucesso!"
                    });
                }
            }
            #endregion

            #region Novo Item
            if (comando == "novo")
            {
                IDictionary<string, string> registro = new Dictionary<string, string>();
                registro.Add("codigo", Contexto.GerarId("AALIQUOTA").ToString());
                registro.Add("posicao", "0");
                registro.Add("aliquota", "");
                registro.Add("tpImposto", "");
                registro.Add("alq", "0");
                registro.Add("cst", "");
                registro.Add("ativo", "true");

                StoreFormulario.AddRecord(registro, true);
                StoreFormulario.CommitChanges();

                //Mostrando a Janela
                JanelaPrincipal.Show();
            }
            #endregion
        }

        #region Gravar Dados Formulário

        // Salva dados e fecha janela
        protected void btnOkFrm_Click(object sender, DirectEventArgs e)
        {
            SalvarDados(sender, e);
            JanelaPrincipal.Hide();
        }

        // Apenas salva dados
        protected void btnSalvarFrm_Click(object sender, DirectEventArgs e)
        {
            SalvarDados(sender, e);
        }

        // Função para salvar dados
        protected void SalvarDados(object sender, DirectEventArgs e)
        {
            AALIQUOTA obj = new AALIQUOTA();
            Global.RegrasNegocio.Atendimento.AliquotaControl control = new Global.RegrasNegocio.Atendimento.AliquotaControl();

            //----------------------------------------------------------------------------------------
            // hd_tipo.Value é um textbox oculto. Ele fica esático durante toda a execução do programa
            //----------------------------------------------------------------------------------------
            Funcoes acao;
            if (hd_tipo.Value.ToString() == "2") //editar
            {
                acao = Funcoes.Atualizar;
                obj.idAliquota = Convert.ToInt32(codigo.Text);
                obj.idEmpresa = Memoria.Empresa;
                obj.idFilial = Memoria.Filial;
                obj = (AALIQUOTA)control.ExecutaFuncao(obj, Funcoes.Buscar, null);
            }
            else
            {
                acao = Funcoes.Adicionar;
                obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);
            }


            //----------------------------------------------------------------------------------------
            //Recebendo todos os dados do usuário
            //----------------------------------------------------------------------------------------
            obj.idAliquota = Convert.ToInt32(codigo.Text);
            obj.aliquota = Convert.ToString(txtaliquota.Value);
            obj.tpImposto = Convert.ToString(ComboBoxTipoImposto.Value);
            obj.valor = Convert.ToDecimal(txtalq.Value);
            
            //------------------------------------------------------------------------------------------
            //Salvar o objeto.
            //------------------------------------------------------------------------------------------
            bool? result = (bool)control.ExecutaFuncao(obj, acao, null);

            if (result == true)
            {
                X.Msg.Alert("Alerta", "Dados gravados com sucesso").Show();
                CarregaPrincipal();
                StoreFormulario_RefreshData(StoreFormulario,new StoreRefreshDataEventArgs() );
            }
            else
                X.Msg.Alert("Alerta", "Verifique os dados digitados e tente novamente").Show();

        }

        #endregion

        #region StoreRefreshData Events

        // Função que carrega os dados do formulário
        protected void StoreFormulario_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            Global.RegrasNegocio.Atendimento.AliquotaControl ct = new Global.RegrasNegocio.Atendimento.AliquotaControl();
            IQueryable<AALIQUOTA> lista = (IQueryable<AALIQUOTA>)ct.ExecutaFuncao(null, Funcoes.BuscarLista, null);

            var dados = from a in lista
                        select new
                        {
                            codigo = a.idAliquota,
                            aliquota = a.aliquota,
                            tipoImposto = a.tpImposto,
                            alq = a.valor,
                            hdtipo = 2
                        };

            StoreFormulario.DataSource = dados;
            StoreFormulario.DataBind();
        }

        #endregion

    }
}