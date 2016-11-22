using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artebit.Restaurante.Global.Util;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Artebit.Restaurante.Global.RegrasNegocio.Reserva;
using Ext.Net;
using Newtonsoft.Json.Linq;

namespace Artebit.Restaurante.Administracao.Sistema
{
    public partial class Usuario : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            MemoriaWeb.ValidaSessao();

            Contexto.AbrirContexto();

            if (!IsPostBack)
            {
                Session["perfil"] = "";
                VerificarPermissoes();
                CarregaPrincipal();
            }

            //if (Session["perfil"] == null)
            //{
            //    Session["perfil"] = "";
            //}


            if (!X.IsAjaxRequest)
            {

            }


        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            Contexto.FecharContexto();
        }

        //Verifica as permissões do usuário para ter acesso aos botões superiores
        private void VerificarPermissoes()
        {
            var perfil = new GPERFIL();
            var per = new Global.RegrasNegocio.Global.PerfilControl();
            perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
            perfil = (GPERFIL) per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
            var compl = new List<string> {"AD", "Usuários", "0"};
            bool resultado = false;


            #region Ativar/Desativar Itens

            compl[2] = "19"; //Ativar/Desativar
            resultado = (bool) per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

            if (!resultado)
            {
                btnAtivarDesativarG.Disabled = true;
            }

            #endregion

            #region Adicionar

            compl[2] = "18";
            resultado = (bool) per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

            if (!resultado)
            {
                btnNovoG.Disabled = true;
            }

            #endregion

            #region Excluir

            compl[2] = "20";
            resultado = (bool) per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

            if (!resultado)
            {
                btnExcluirG.Disabled = true;
            }

            #endregion

            #region Editar

            compl[2] = "2";
            resultado = (bool) per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

            if (!resultado)
            {
                btnEditarG.Disabled = true;
            }

            #endregion

        }

        //Edita, exclui, ativa e desativa o usuário
        protected void GridAcao(object sender, DirectEventArgs e)
        {
            string id = Convert.ToString(e.ExtraParams["codigo"]);
            string comando = Convert.ToString(e.ExtraParams["command"]);

            GUSUARIO obj = new GUSUARIO();
            Global.RegrasNegocio.Global.UsuarioControl ctrl = new Global.RegrasNegocio.Global.UsuarioControl();


            #region Editar

            if (comando == "editar" && btnEditarG.Disabled == false)
            {
                //-------------------------------------------------------------------------------------------
                //Preenchendo os campos com as informações do Usuario
                FormPrincipal.AddScript(
                    "if(StoreFormulario.allData != null) { PagingToolbar1.changePage(StoreFormulario.allData.indexOfKey('" +
                    id + "') + 1); }" + " else { PagingToolbar1.changePage(StoreFormulario.indexOfId('" + id +
                    "') + 1);}");
                txt_codUsuario.Disabled = true;

                JanelaPrincipal.Show();

            }

            #endregion

            #region Excluir

            if (comando == "excluir")
            {
                bool res = (bool) ctrl.ExecutaFuncao(obj, Funcoes.Cancelar, null);

                if (res)
                    X.Msg.Alert("Alerta", "O Usuario foi desativado").Show();

                CarregaPrincipal();
            }

            #endregion

            #region Ativar/Desativar

            if (comando == "ativar")
            {
                bool sucesso = true;

                foreach (var i in RowSelectionModel1.SelectedRows)
                {
                    string idusu = Convert.ToString(i.RecordID);
                    GUSUARIO usu = new GUSUARIO();

                    usu.codusuario = idusu;
                    usu = (GUSUARIO) ctrl.ExecutaFuncao(usu, Funcoes.Buscar, null);

                    usu.ativo = !usu.ativo;
                    bool res = (bool) ctrl.ExecutaFuncao(usu, Funcoes.Atualizar, null);

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
                registro.Add("nome", "");
                registro.Add("senha", "");
                registro.Add("ativo", "true");

                StoreFormulario.AddRecord(registro, true);
                StoreFormulario.CommitChanges();

                //Mostrando a Janela
                txt_codUsuario.Disabled = false;
                JanelaPrincipal.Show();
                TabPanelForm.ActiveTabIndex = 0;
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
            GUSUARIO obj = new GUSUARIO();
            Global.RegrasNegocio.Global.UsuarioControl control = new Global.RegrasNegocio.Global.UsuarioControl();

            //----------------------------------------------------------------------------------------
            // hd_tipo.Value é um textbox oculto. Ele fica esático durante toda a execução do programa
            //----------------------------------------------------------------------------------------
            Funcoes acao;
            if (hdtipo.Value.ToString() == "2") //editar
            {
                acao = Funcoes.Atualizar;
                obj.codusuario = Convert.ToString(txt_codUsuario.Value);
                obj = (GUSUARIO) control.ExecutaFuncao(obj, Funcoes.Buscar, null);
            }
            else
            {
                acao = Funcoes.Adicionar;
            }


            //----------------------------------------------------------------------------------------
            //Recebendo todos os dados do usuário
            //----------------------------------------------------------------------------------------
            obj.codusuario = Convert.ToString(txt_codUsuario.Value);
            obj.nome = Convert.ToString(txt_nome.Value);
            if (txt_senha.Value != "" && txt_senha.Value != null)
            {
                obj.senha = Criptografia.GerarSHA1(Criptografia.GerarMD5(Convert.ToString(txt_senha.Value)));
            }
            obj.ativo = Convert.ToBoolean(cbx_ativo.Value);


            //------------------------------------------------------------------------------------------
            //Recebendo os perfis
            //-------------------------------------------------------------------------------------------

            string j = "";
            List<object> items = null;

            j = e.ExtraParams["Perfil"];
            items = JSON.Deserialize<List<object>>(j);
            int cont = 0;
            foreach (object p in items)
            {
                //              "codigo": "A",
                //"nome": "Atendimento",
                //"idPerfil": "14 - admin",
                //"descricao": "admin",
                //"id": -16

                GPERFIL per = new GPERFIL();

                string idPerfil = Convert.ToString(JContainer.FromObject(p)["idPerfil"].ToString());

                idPerfil = idPerfil.Replace(" - ", "-");

                if (idPerfil == "")
                {
                    idPerfil = "0-0";
                }

                string[] dados = idPerfil.Split('-');

                per.idPerfil = Convert.ToInt32(dados[0]);
                Global.RegrasNegocio.Global.PerfilControl ct = new Global.RegrasNegocio.Global.PerfilControl();
                per = (GPERFIL) ct.ExecutaFuncao(per, Funcoes.Buscar, null);

                GUSRFILMOD perfil = new GUSRFILMOD();

                string codsistema = Convert.ToString(JContainer.FromObject(p)["codigo"].ToString());

                perfil.codSistema = codsistema;
                perfil.codUsuario = Convert.ToString(txt_codUsuario.Value);
                perfil.idEmpresa = Memoria.Empresa;
                perfil.idFilial = Memoria.Filial;
                perfil.supervisor = false;
                perfil.idPerfil = Convert.ToInt32(dados[0]);

                GUSRFILMOD m =
                    obj.GUSRFILMOD.SingleOrDefault(
                        a =>
                        a.codSistema == perfil.codSistema
                        && a.codUsuario == perfil.codUsuario
                        && a.idEmpresa == perfil.idEmpresa
                        && a.idFilial == perfil.idFilial);

                if (m != null)
                {
                    obj.GUSRFILMOD.Remove(m);
                }

                if (dados[0] != "0")
                    obj.GUSRFILMOD.Add(perfil);

                cont++;
            }



            //------------------------------------------------------------------------------------------
            //Salvar o objeto.
            //------------------------------------------------------------------------------------------
            bool? result = (bool) control.ExecutaFuncao(obj, acao, null);




            if (result == true)
            {
                StoreSistema.CommitChanges();
                X.Msg.Alert("Alerta", "Dados gravados com sucesso").Show();
                CarregaPrincipal();
                StoreFormulario_RefreshData(StoreFormulario, new StoreRefreshDataEventArgs());
            }
            else
                X.Msg.Alert("Alerta", "Verifique os dados digitados e tente novamente").Show();

        }

        #endregion

        #region Carrega Grid

        protected void CarregaPrincipal()
        {
            Global.RegrasNegocio.Global.UsuarioControl usuario = new Global.RegrasNegocio.Global.UsuarioControl();
            GUSUARIO usu = new GUSUARIO();
            List<string> compl = new List<string>();

            IQueryable<GUSUARIO> lista_usuario =
                (IQueryable<GUSUARIO>) usuario.ExecutaFuncao(usu, Funcoes.BuscarLista, compl);
            var dados = from a in lista_usuario
                        select new
                                   {
                                       codigo = a.codusuario,
                                       nome = a.nome,
                                       ativo = a.ativo == true ? "Ativo" : "Inativo",
                                       dataInclusao = a.dataInclusao,
                                       dataAlteracao = a.dataAlteracao

                                   };
            StorePrincipal.DataSource = dados;
            StorePrincipal.DataBind();
        }

        #endregion


        //Carrega o combo de perfis quando o usuário clica na combobox
        protected void CarregarCombo(object sender, DirectEventArgs e)
        {
            string id = Convert.ToString(e.ExtraParams["codigo"]);
            string perfil = Convert.ToString(e.ExtraParams["idPerfil"]);

            GPERFIL per = new GPERFIL();
            Global.RegrasNegocio.Global.PerfilControl ct = new Global.RegrasNegocio.Global.PerfilControl();

            per.codSistema = id;
            IQueryable<GPERFIL> lista = (IQueryable<GPERFIL>) ct.ExecutaFuncao(per, Funcoes.BuscarListaEspecifica, null);

            var dados = from a in lista
                        select new
                                   {
                                       codigo = a.codSistema,
                                       nome = a.descricao,
                                       idPerfil =
                            SqlFunctions.StringConvert((double) a.idPerfil).Trim() + " - " + a.descricao
                                   };

            StorePerfil.DataSource = dados;
            StorePerfil.DataBind();

            ComboPerfil.Value = perfil;
        }


        // StoreRefreshData Events
        protected void StoreFormulario_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            Global.RegrasNegocio.Global.UsuarioControl usuario = new Global.RegrasNegocio.Global.UsuarioControl();
            GUSUARIO usu = new GUSUARIO();
            List<string> compl = new List<string>();

            IQueryable<GUSUARIO> lista_usuario =
                (IQueryable<GUSUARIO>) usuario.ExecutaFuncao(usu, Funcoes.BuscarLista, compl);
            var dados = from a in lista_usuario
                        select new
                                   {
                                       codigo = a.codusuario,
                                       codigo2 = a.codusuario,
                                       nome = a.nome,
                                       senha = a.senha,
                                       ativo = a.ativo,
                                       hdtipo = 2
                                   };

            StoreFormulario.DataSource = dados;
            StoreFormulario.DataBind();
        }

        protected void StoreSistema_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            Global.RegrasNegocio.Global.UsuarioControl control = new Global.RegrasNegocio.Global.UsuarioControl();

            //Buscar todos os sistemas que o usuário logado tem acesso
            GUSUARIO usulog = new GUSUARIO();
            usulog.codusuario = Memoria.Codusuario;
            string usuario = Convert.ToString(txt_codUsuario.Value);
            IQueryable<GSISTEMA> listaSistemas =
                (IQueryable<GSISTEMA>) control.ExecutaFuncao(usulog, Funcoes.BuscaSistemaPerfil, null);

            var l = from a in listaSistemas
                    select new
                               {
                                   codigo = a.codSistema,
                                   nome = a.descricao,
                                   descricao = a.GUSRFILMOD.FirstOrDefault(b => b.codSistema == a.codSistema
                                                                                && b.codUsuario == usuario
                                                                                && b.idEmpresa == Memoria.Empresa
                                                                                && b.idFilial == Memoria.Filial).GPERFIL
                        .descricao,
                                   id = a.GUSRFILMOD.FirstOrDefault(b => b.codSistema == a.codSistema
                                                                         && b.codUsuario == usuario
                                                                         && b.idEmpresa == Memoria.Empresa
                                                                         && b.idFilial == Memoria.Filial).GPERFIL.
                        idPerfil
                               };

            var l2 = from c in l
                     select new
                                {
                                    codigo = c.codigo,
                                    nome = c.nome,
                                    descricao = c.descricao,
                                    idPerfil = SqlFunctions.StringConvert((double) c.id).Trim() + " - " + c.descricao
                                };

            StoreSistema.DataSource = l2;
            StoreSistema.DataBind();

        }

    }
}