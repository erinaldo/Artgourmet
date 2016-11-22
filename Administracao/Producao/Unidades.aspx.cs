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

namespace Artebit.Restaurante.Administracao.Producao
{
    public partial class Unidades : System.Web.UI.Page
    {
        
        #region Carregamento da página

        protected void Page_Unload(object sender, EventArgs e)
        {
            Contexto.Atual.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            MemoriaWeb.ValidaSessao();

            Contexto.Atual = new Global.Modelo.Restaurante();

            if (!IsPostBack)
            {
                CarregaPrincipal();
                VerificarPermissoes();
            }
        }

        //Verifica as permissões do planoário para ter acesso aos botões superiores
        private void VerificarPermissoes()
        {
            var perfil = new GPERFIL();
            var per = new PerfilControl();
            perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
            perfil = (GPERFIL)per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
            var compl = new List<string> { "AD", "Unidades", "0" };
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

        private void CarregaPrincipal()
        {
            Global.RegrasNegocio.Estoque.UnidadesControl control = new Global.RegrasNegocio.Estoque.UnidadesControl();
            IQueryable<EUNIDADE> lista = (IQueryable<EUNIDADE>) control.ExecutaFuncao(null, Funcoes.BuscarLista, null);

            var dados = from a in lista
                        select new
                                   {
                                       codigo = a.codUnd,
                                       a.descricao,
                                       a.codUndBase,
                                       a.fatorConversao,
                                       ativo = a.ativo == true ? "Ativo" : "Inativo"

                                   };

            storePrincipal.DataSource = dados;
            storePrincipal.DataBind();
        }

        #endregion

        protected void StoreFormulario_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            Global.RegrasNegocio.Estoque.UnidadesControl control = new Global.RegrasNegocio.Estoque.UnidadesControl();
            IQueryable<EUNIDADE> lista = (IQueryable<EUNIDADE>)control.ExecutaFuncao(null, Funcoes.BuscarLista, null);

            var dados = from a in lista
                        select new
                        {
                            codigo = a.codUnd,
                            codigo2 = a.codUnd,
                            a.descricao,
                            a.codUndBase,
                            a.fatorConversao,
                            hdtipo = 2

                        };

            StoreFormulario.DataSource = dados;
            StoreFormulario.DataBind();
        
        
        }

        //Edita, exclui, ativa e desativa o planoário
        protected void GridAcao(object sender, DirectEventArgs e)
        {
            string id = Convert.ToString(e.ExtraParams["codigo"]);
            string comando = Convert.ToString(e.ExtraParams["command"]);

            EUNIDADE obj = new EUNIDADE();
            Global.RegrasNegocio.Estoque.UnidadesControl ctrl = new Global.RegrasNegocio.Estoque.UnidadesControl();


            #region Editar

            if (comando == "editar" && btnEditarG.Disabled == false)
            {
                //-------------------------------------------------------------------------------------------
                //Preenchendo os campos com as informações do planoario
                FormPrincipal.AddScript(
                    "if(StoreFormulario.allData != null) { PagingToolbar1.changePage(StoreFormulario.allData.indexOfKey('" +
                    id + "') + 1); }" + " else { PagingToolbar1.changePage(StoreFormulario.indexOfId('" + id +
                    "') + 1);}");

                txtCodUnd.Disabled = true;
                JanelaPrincipal.Show();

            }

            #endregion

            #region Excluir

            if (comando == "excluir")
            {
                bool res = (bool)ctrl.ExecutaFuncao(obj, Funcoes.Cancelar, null);

                if (res)
                    X.Msg.Alert("Alerta", "A unidade foi desativado").Show();

                CarregaPrincipal();
            }

            #endregion

            #region Ativar/Desativar

            if (comando == "ativar")
            {
                bool sucesso = true;

                foreach (var i in RowSelectionModel1.SelectedRows)
                {
                    string codUnidade = Convert.ToString(i.RecordID);
                    EUNIDADE und = new EUNIDADE();

                    und.codUnd = codUnidade;
                    und = (EUNIDADE)ctrl.ExecutaFuncao(und, Funcoes.Buscar, null);

                    und.ativo = !und.ativo;
                    bool res = (bool)ctrl.ExecutaFuncao(und, Funcoes.Atualizar, null);

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
                registro.Add("descricao", "");
                registro.Add("codUndBase", "");
                registro.Add("ativo", "true");
                registro.Add("fatorConversao", "");

                StoreFormulario.AddRecord(registro, true);
                StoreFormulario.CommitChanges();


                //Mostrando a Janela
                txtCodUnd.Disabled = false;
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
            EUNIDADE obj = new EUNIDADE();
            Global.RegrasNegocio.Estoque.UnidadesControl control = new Global.RegrasNegocio.Estoque.UnidadesControl();

            //----------------------------------------------------------------------------------------
            // hd_tipo.Value é um textbox oculto. Ele fica esático durante toda a execução do programa
            //----------------------------------------------------------------------------------------
            obj.codUnd = Convert.ToString(txtCodUnd.Value);
            Funcoes acao;
            if (hd_tipo.Value.ToString() == "2") //editar
            {
                acao = Funcoes.Atualizar;
                obj = (EUNIDADE)control.ExecutaFuncao(obj, Funcoes.Buscar, null);
            }
            else
            {
                acao = Funcoes.Adicionar;
            }


            //----------------------------------------------------------------------------------------
            //Recebendo todos os dados do usuário
            //----------------------------------------------------------------------------------------
            
            obj.descricao = Convert.ToString(txtDescricao.Value);
            obj.codUndBase = Convert.ToString(dropUnidades.SelectedItem.Value);
            obj.fatorConversao = Convert.ToDecimal(txtFator.Value);
            obj.ativo = true;

            //------------------------------------------------------------------------------------------
            //Salvar o objeto.
            //------------------------------------------------------------------------------------------
            bool? result = (bool)control.ExecutaFuncao(obj, acao, null);

            if (result == true)
            {
                X.Msg.Alert("Alerta", "Dados gravados com sucesso").Show();
                CarregaPrincipal();
                StoreFormulario_RefreshData(StoreFormulario, new StoreRefreshDataEventArgs());
            }
            else
                X.Msg.Alert("Alerta", "Verifique os dados digitados e tente novamente").Show();

        }

        #endregion


        #region antigo

   //protected void AbrirJanela(object sender, EventArgs e)
        //{
        //    GPERFIL perfil = new GPERFIL();
        //    Perfil per = new Perfil();
        //    perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
        //    perfil = (GPERFIL) per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
        //    List<string> compl = new List<string>();
        //    compl.Add("AD");
        //    compl.Add("Unidades");
        //    compl.Add("0");

        //    compl[2] = "18";
        //    bool result2 = (bool) per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

        //    if (result2)
        //    {

        //        //Limpando os campos
        //        txtCodUnd.ReadOnly = false;
        //        txtCodUnd.Value = "";
        //        txtDescricao.Value = "";
        //        dropUnidades.Value = null;
        //        txtFator.Value = "";

        //        //Campo oculto para controle de edição
        //        hd_tipo.Value = 0;

        //        //Abre a janela
        //        JanelaPrincipal.Show();
        //    }
        //    else
        //    {
        //        X.Msg.Alert("Alerta", "Você não tem permissão de executar essa ação, contate o administrador").
        //            Show();
        //    }
        //}

        //protected void SalvarDados(object sender, EventArgs e)
        //{

        //    EUNIDADE obj = new EUNIDADE();
        //    Global.RegrasNegocio.Estoque.Unidades control =
        //        new Global.RegrasNegocio.Estoque.Unidades();

        //    Funcoes acao = Funcoes.Adicionar;

        //    obj.codigo = Convert.ToString(txtCodUnd.Value);

        //    if (hd_tipo.Value.ToString() == "1") // O objeto foi editado.
        //    {
        //        obj = (EUNIDADE) control.ExecutaFuncao(obj, Funcoes.Buscar, null);
        //        acao = Funcoes.Atualizar;
        //    }


        //    obj.descricao = Convert.ToString(txtDescricao.Value);
        //    obj.codUndBase = Convert.ToString(dropUnidades.SelectedItem.Value);
        //    obj.fatorConversao = Convert.ToDecimal(txtFator.Value);
        //    obj.ativo = true;


        //    bool result = (bool) control.ExecutaFuncao(obj, acao, null);
        //    CarregaPrincipal();

        //    if (result)
        //    {
        //        X.Msg.Alert("Alerta", "Dados gravados com sucesso").Show();
        //        JanelaPrincipal.Hide();
        //    }

        //    else
        //        X.Msg.Alert("Alerta", "Verifique os dados digitados e tente novamente").Show();
        //}



        //protected void FecharJanela(object sender, EventArgs e)
        //{
        //    JanelaPrincipal.Hide();
        //}

        //protected void GridAcao(object sender, DirectEventArgs e)
        //{
        //    string codigo = Convert.ToString(e.ExtraParams["codigo"]);
        //    string comando = Convert.ToString(e.ExtraParams["command"]);

        //    //Buscando o fornecedor
        //    EUNIDADE obj = new EUNIDADE();

        //    Global.RegrasNegocio.Estoque.Unidades control = new Global.RegrasNegocio.Estoque.Unidades();
        //    obj.codigo = codigo;

        //    obj = (EUNIDADE) control.ExecutaFuncao(obj, Funcoes.Buscar, null);

        //    if (comando == "editar")
        //    {
        //        GPERFIL perfil = new GPERFIL();
        //        Perfil per = new Perfil();
        //        perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
        //        perfil = (GPERFIL) per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
        //        List<string> compl = new List<string> {"AD", "Unidades", "0"};

        //        compl[2] = "2";
        //        bool result2 = (bool) per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

        //        if (result2)
        //        {
        //            txtCodUnd.Enabled = false;
        //            txtCodUnd.ReadOnly = true;
        //            txtCodUnd.Value = obj.codigo;
        //            txtDescricao.Value = obj.descricao;
        //            dropUnidades.Value = obj.codUndBase;
        //            txtFator.Value = obj.fatorConversao;

        //            //Campos ocultos
        //            hd_tipo.Value = 1;

        //            JanelaPrincipal.Show();
        //        }
        //        else
        //        {
        //            X.Msg.Alert("Alerta", "Você não tem permissão de executar essa ação, contate o administrador").
        //                Show();
        //        }
        //    }
        //    if (comando == "desativar")
        //    {
        //        GPERFIL perfil = new GPERFIL();
        //        Perfil per = new Perfil();
        //        perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
        //        perfil = (GPERFIL) per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
        //        List<string> compl = new List<string> {"AD", "Unidades", "0"};

        //        compl[2] = "20";
        //        bool result2 = (bool) per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

        //        if (result2)
        //        {

        //            obj.ativo = false;
        //            bool result = (bool) control.ExecutaFuncao(obj, Funcoes.Atualizar, null);

        //            //if (result)
        //            //X.Msg.Alert("Alerta", "desativado com sucesso").Show();
        //        }
        //        else
        //        {
        //            X.Msg.Alert("Alerta", "Você não tem permissão de executar essa ação, contate o administrador").
        //                Show();
        //        }

        //    }
        //    if (comando == "ativar")
        //    {
        //        GPERFIL perfil = new GPERFIL();
        //        Perfil per = new Perfil();
        //        perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
        //        perfil = (GPERFIL) per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
        //        List<string> compl = new List<string> {"AD", "Unidades", "0"};

        //        compl[2] = "19";
        //        bool result2 = (bool) per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

        //        if (result2)
        //        {
        //            obj.ativo = true;
        //            bool result = (bool) control.ExecutaFuncao(obj, Funcoes.Atualizar, null);
        //            //CarregaFornecedores();

        //            //if (result)
        //            //X.Msg.Alert("Alerta", "ativado com sucesso").Show();
        //        }
        //        else
        //        {
        //            X.Msg.Alert("Alerta", "Você não tem permissão de executar essa ação, contate o administrador").
        //                Show();
        //        }


        //    }

        //    CarregaPrincipal();

        //}
        #endregion

     
    }
}