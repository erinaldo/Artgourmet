using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Estoque;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Ext.Net;

namespace Administracao.PDV
{
    public partial class Fidelidade : System.Web.UI.Page
    {

        #region Carregamento da página

        protected void Page_Load(object sender, EventArgs e)
        {
            MemoriaWeb.ValidaSessao();

            Contexto.AbrirContexto();

            if (!IsPostBack)
            {
                VerificarPermissoes();
                CarregaPrincipal();
            }

        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            Contexto.FecharContexto();
        }


        //Verifica as permissões do planoário para ter acesso aos botões superiores
        private void VerificarPermissoes()
        {
            var perfil = new GPERFIL();
            var per = new PerfilControl();
            perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
            perfil = (GPERFIL)per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
            var compl = new List<string> { "AD", "Fidelidade", "0" };
            bool resultado = false;


            #region Ativar/Desativar Itens

            compl[2] = "19"; //Ativar/Desativar
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

        // Função para trazer os dados de Fidelidade 
        private void CarregaPrincipal()
        {
            Artebit.Restaurante.Global.RegrasNegocio.Atendimento.FidelidadeControl control =
                new Artebit.Restaurante.Global.RegrasNegocio.Atendimento.FidelidadeControl();
            IQueryable<AFIDELIDADE> lista =
                (IQueryable<AFIDELIDADE>) control.ExecutaFuncao(null, Funcoes.BuscarLista, null);

            var dados = from a in lista
                        select new
                                   {
                                       codigo = a.idFidelidade,
                                       nome = a.nome,
                                       //tipo = a.ATIPOFIDELIDADE.descricao,
                                       moeda = a.moeda,
                                       ativo = a.ativo == true ? "Ativo" : "Inativo"
                                   };

            StorePrincipal.DataSource = dados;
            StorePrincipal.DataBind();
        }

        #endregion


        #region salvar dados

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
            AFIDELIDADE obj = new AFIDELIDADE();
            Artebit.Restaurante.Global.RegrasNegocio.Atendimento.FidelidadeControl control =
                new Artebit.Restaurante.Global.RegrasNegocio.Atendimento.FidelidadeControl();

            Funcoes acao = Funcoes.Adicionar;

            if (hd_tipo.Value.ToString() == "1") // O objeto foi editado.
            {
                obj.idFidelidade = Convert.ToInt32(hd_id.Value);
                obj = (AFIDELIDADE) control.ExecutaFuncao(obj, Funcoes.Buscar, null);
                acao = Funcoes.Atualizar;
            }

            //Recebendo os valores informados pelo usuário
            obj.nome = txtnome.Text;
            obj.moeda = txtmoeda.Text;
            obj.tipo = Convert.ToInt32(RadioGroup1.CheckedItems[0].InputValue);
            if (obj.tipo == 1)
                obj.valorPorReal = null;
            else
                obj.valorPorReal = Convert.ToDecimal(txtvalor.Value);

            obj.diaTodo = chkdiatodo.Checked;

            if (!chkdiatodo.Checked)
            {
                string dia = "01/01/2000 00:00:00";
                string horIni = txthorarioini.Text;
                string horFim = txthorariofim.Text;
                obj.horarioInicial = Convert.ToDateTime(dia.Replace("00:00:00", horIni + ":00"));
                dia = "01/01/2000 00:00:00";
                obj.horarioFinal = Convert.ToDateTime(dia.Replace("00:00:00", horFim + ":00"));
            }
            else
            {
                obj.horarioInicial = null;
                obj.horarioFinal = null;
            }

            obj.ativo = chkativo.Checked;


            bool result = (bool) control.ExecutaFuncao(obj, acao, null);

            if (result)
            {
                X.Msg.Alert("Alerta", "Dados gravados com sucesso").Show();
                CarregaPrincipal();

            }

            else
                X.Msg.Alert("Alerta", "Verifique os dados digitados e tente novamente").Show();
        }

        #endregion


        protected void StoreFormulario_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            Artebit.Restaurante.Global.RegrasNegocio.Atendimento.FidelidadeControl control =
                new Artebit.Restaurante.Global.RegrasNegocio.Atendimento.FidelidadeControl();
            IQueryable<AFIDELIDADE> lista =
                (IQueryable<AFIDELIDADE>)control.ExecutaFuncao(null, Funcoes.BuscarLista, null);

            IEnumerable<AFIDELIDADE> lista2 = lista.AsEnumerable();

            var dados = from a in lista2
                        select new
                        {
                            codigo = a.idFidelidade,
                            nome = a.nome,
                            tipo = a.tipo,
                            diatodo = a.diaTodo,
                            horarioini = string.Format("{0:HH\\:mm}", a.horarioInicial),
                            horariofim = string.Format("{0:HH\\:mm}", a.horarioFinal),
                            pontos = a.valorPorReal,
                            moeda = a.moeda,
                            ativo = a.ativo,
                            hdtipo = "1"
                        };

            StoreFormulario.DataSource = dados;
            StoreFormulario.DataBind();
        }

        protected void StoreProduto_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {

            int id = Convert.ToInt32(hd_id.Value);

            AFIDELIDADE obj = new AFIDELIDADE();
            Artebit.Restaurante.Global.RegrasNegocio.Atendimento.FidelidadeControl control = 
                new Artebit.Restaurante.Global.RegrasNegocio.Atendimento.FidelidadeControl();

            obj.idFidelidade = id;
            obj = (AFIDELIDADE)control.ExecutaFuncao(obj, Funcoes.Buscar, null);

            if (obj != null)
            {

                IQueryable<APRDFIDELIDADE> list =
                    (IQueryable<APRDFIDELIDADE>) control.ExecutaFuncao(obj, Funcoes.BuscarProduto, null);

                var lista = from a in list
                            select new
                                       {
                                           nome = a.EPRODUTO.nome,
                                           codigo = a.EPRODUTO.codigo,
                                           valor = a.valorMoeda
                                       };

                storeProduto.DataSource = lista;
                storeProduto.DataBind();
            }else
            {
                GridProduto.ClearContent();
            }
        }


        //Edita, exclui, ativa e desativa o planoário
        protected void GridAcao(object sender, DirectEventArgs e)
        {
            string id = Convert.ToString(e.ExtraParams["codigo"]);
            string comando = Convert.ToString(e.ExtraParams["command"]);

            AFIDELIDADE obj = new AFIDELIDADE();
            Artebit.Restaurante.Global.RegrasNegocio.Atendimento.FidelidadeControl ctrl = new Artebit.Restaurante.Global.RegrasNegocio.Atendimento.FidelidadeControl();


            #region Editar

            if (comando == "editar" && btnEditarG.Disabled == false)
            {
                //-------------------------------------------------------------------------------------------
                //Preenchendo os campos com as informações do planoario
                FormPrincipal.AddScript(
                    "if(StoreFormulario.allData != null) { PagingToolbar1.changePage(StoreFormulario.allData.indexOfKey(" +
                    id + ") + 1); }" + " else { PagingToolbar1.changePage(StoreFormulario.indexOfId(" + id +
                    ") + 1);}");

                JanelaPrincipal.Show();

            }

            #endregion

            #region Excluir

            if (comando == "excluir")
            {
                bool res = (bool)ctrl.ExecutaFuncao(obj, Funcoes.Cancelar, null);

                if (res)
                    X.Msg.Alert("Alerta", "O Plano de Fidelidade foi desativado").Show();

                CarregaPrincipal();
            }

            #endregion

            #region Ativar/Desativar

            if (comando == "ativar")
            {
                bool sucesso = true;

                foreach (var i in RowSelectionModel1.SelectedRows)
                {
                    int idPlano = Convert.ToInt32(i.RecordID);
                    AFIDELIDADE plano = new AFIDELIDADE();

                    plano.idFidelidade = idPlano;
                    plano = (AFIDELIDADE)ctrl.ExecutaFuncao(plano, Funcoes.Buscar, null);

                    plano.ativo = !plano.ativo;
                    bool res = (bool)ctrl.ExecutaFuncao(plano, Funcoes.Atualizar, null);

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
                registro.Add("codigo", Contexto.GerarId("AFIDELIDADE").ToString());
                registro.Add("nome", "");
                registro.Add("tipo", "C");
                registro.Add("ativo", "true");
                registro.Add("diatodo", "true");
                registro.Add("horarioini", "");
                registro.Add("horariofim", "");
                registro.Add("pontos", "");
                registro.Add("moeda", "");
                registro.Add("hdtipo", "2");

                StoreFormulario.AddRecord(registro, true);
                StoreFormulario.CommitChanges();

                GridProduto.ClearContent();

                //Mostrando a Janela
                JanelaPrincipal.Show();
                TabPanelForm.ActiveTabIndex = 0;
            }

            #endregion
        }




    }
}