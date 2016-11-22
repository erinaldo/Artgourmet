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

namespace Artebit.Restaurante.Administracao.Sistema
{
    public partial class Perfil : System.Web.UI.Page
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
                carregaComboSistemas();
            }

        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            Contexto.FecharContexto();
        }

        //Verifica as permissões do perfário para ter acesso aos botões superiores
        private void VerificarPermissoes()
        {
            var perfil = new GPERFIL();
            var per = new Global.RegrasNegocio.Global.PerfilControl();
            perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
            perfil = (GPERFIL)per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
            var compl = new List<string> { "AD", "Perfis", "0" };
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


        // Função para carregar dados da gridpanel de perfil
        protected void CarregaPrincipal()
        {
            // Perfil
            Global.RegrasNegocio.Global.PerfilControl perfil = new Global.RegrasNegocio.Global.PerfilControl();
            GPERFIL per = new GPERFIL();

            IQueryable<GPERFIL> lista_perfil =
                (IQueryable<GPERFIL>) perfil.ExecutaFuncao(per, Funcoes.BuscarLista, null);
            var dados = from a in lista_perfil
                        select new
                                   {
                                       codigo = a.idPerfil,
                                       nome = a.descricao,
                                       ativo = a.ativo == true ? "Ativo" : "Inativo",
                                       sistema = a.GSISTEMA.descricao

                                   };
            StorePrincipal.DataSource = dados;
            StorePrincipal.DataBind();
        }


        // função para carregar dados da combo de sistemas
        protected void carregaComboSistemas()
        {

            Global.RegrasNegocio.Global.PerfilControl per = new Global.RegrasNegocio.Global.PerfilControl();
            IQueryable<GPERFIL> lista_sistemas =
                (IQueryable<GPERFIL>) per.ExecutaFuncao(null, Funcoes.BuscarLista, null);
            var dados = from a in lista_sistemas
                        select new
                                   {
                                       codSistema = a.GSISTEMA.codSistema,
                                       nome = a.GSISTEMA.descricao
                                   };
            store_sistemas.DataSource = dados;
            store_sistemas.DataBind();
        }

        #endregion


        //Edita, exclui, ativa e desativa o perfário
        protected void GridAcao(object sender, DirectEventArgs e)
        {
            string id = Convert.ToString(e.ExtraParams["codigo"]);
            string comando = Convert.ToString(e.ExtraParams["command"]);

            GPERFIL obj = new GPERFIL();
            Global.RegrasNegocio.Global.PerfilControl ctrl = new Global.RegrasNegocio.Global.PerfilControl();


            #region Editar

            if (comando == "editar" && btnEditarG.Disabled == false)
            {
                StoreFormulario_RefreshData(store_func,new StoreRefreshDataEventArgs());

                //-------------------------------------------------------------------------------------------
                //Preenchendo os campos com as informações do perfario
                FormPrincipal.AddScript(
                    "if(StoreFormulario.allData != null) { PagingToolbar1.changePage(StoreFormulario.allData.indexOfKey(" +
                    id + ") + 1); }" + " else { PagingToolbar1.changePage(StoreFormulario.indexOfId(" + id +
                    ") + 1);}");

            }

            #endregion

            #region Excluir

            if (comando == "excluir")
            {
                bool res = (bool)ctrl.ExecutaFuncao(obj, Funcoes.Cancelar, null);

                if (res)
                    X.Msg.Alert("Alerta", "O Perfil foi desativado").Show();

                CarregaPrincipal();
            }

            #endregion

            #region Ativar/Desativar

            if (comando == "ativar")
            {
                bool sucesso = true;

                foreach (var i in RowSelectionModel1.SelectedRows)
                {
                    int idperf = Convert.ToInt32(i.RecordID);
                    GPERFIL perf = new GPERFIL();

                    perf.idPerfil = idperf;
                    perf = (GPERFIL)ctrl.ExecutaFuncao(perf, Funcoes.Buscar, null);

                    perf.ativo = !perf.ativo;
                    bool res = (bool)ctrl.ExecutaFuncao(perf, Funcoes.Atualizar, null);

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
                registro.Add("codigo", Contexto.GerarId("GPERFIL").ToString());
                registro.Add("nome", "");
                registro.Add("sistema", "R");
                registro.Add("ativo", "true");
                registro.Add("hdtipo", "1");

                Session["permissao"] = "";

                StoreFormulario.AddRecord(registro, true);
                StoreFormulario.CommitChanges();

                //Mostrando a Janela
                JanelaPrincipal.Show();
                TabPanelForm.ActiveTabIndex = 0;
            }

            #endregion
        }

        [DirectMethod]
        public void atualizaSessao(string id)
        {
            Global.RegrasNegocio.Global.PerfilControl control = new Global.RegrasNegocio.Global.PerfilControl();
            GPERFIL per = new GPERFIL();

            per.idPerfil = Convert.ToInt32(Convert.ToInt32(id));

            per = (GPERFIL)control.ExecutaFuncao(per, Funcoes.Buscar, null);

            List<GPERMISSAO> perm = new List<GPERMISSAO>();

            Session["permissao"] = "";

            if (per != null)
            {

                foreach (GPERMISSAO pp in per.GPERMISSAO)
                {
                    perm.Add(pp);
                }

                Session["permissao"] = perm;
            }
        }


        #region refresh functions
        
        // Função que atualiza a tabela de janela
        protected void ComboBoxStore_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            string id = Convert.ToString(combo_sistemas.Value);

            JanelaControl control = new JanelaControl();

            IQueryable<GJANELA> lista = (IQueryable<GJANELA>) control.ExecutaFuncao(null, Funcoes.BuscarLista, null);

            var l = from b in lista.Where(a => a.codSistema == id)
                    select new
                               {
                                   id = b.idJanela,
                                   nome = b.descricao
                               };

            store_pagina.DataSource = l;
            store_pagina.DataBind();
        }

        protected void StoreFormulario_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            // Perfil
            Global.RegrasNegocio.Global.PerfilControl perfil = new Global.RegrasNegocio.Global.PerfilControl();
            GPERFIL per = new GPERFIL();

            IQueryable<GPERFIL> lista_perfil =
                (IQueryable<GPERFIL>) perfil.ExecutaFuncao(per, Funcoes.BuscarLista, null);
            var dados = from a in lista_perfil
                        select new
                                   {
                                       codigo = a.idPerfil,
                                       nome = a.descricao,
                                       ativo = a.ativo,
                                       sistema = a.GSISTEMA.codSistema,
                                       valsenha = a.valSenha,
                                       hdtipo = "2"
                                   };

            StoreFormulario.DataSource = dados;
            StoreFormulario.DataBind();
        }
        
        #endregion



        #region Funções para funcionalidades do perfil

        // Função de abrir a janela para inserir as funcionalidades
        protected void InserirFunc(object sender, DirectEventArgs e)
        {
            int id = Convert.ToInt32(e.ExtraParams["id"]);

            PermissaoControl controlador = new PermissaoControl();

            IQueryable<GPERMISSAO> lista =
                (IQueryable<GPERMISSAO>) controlador.ExecutaFuncao(null, Funcoes.BuscarLista, null);

            Session["janela"] = id;

            var dados = from b in lista.Where(a => a.idJanela == id)
                        select new
                                   {
                                       nome = b.GFUNCIONALIDADE.descricao,
                                       janela = b.idJanela,
                                       id = b.idPermissao
                                   };

            store_func.DataSource = dados;
            store_func.DataBind();

            RowSelectionModel sm = this.grid_func.SelectionModel.Primary as RowSelectionModel;
            sm.ClearSelections();

            List<GPERMISSAO> perm = new List<GPERMISSAO>();

            if (Session["permissao"] != "")
                perm = (List<GPERMISSAO>) Session["permissao"];

            foreach (var item in dados)
            {
                if (perm.Any(a => a.idPermissao == item.id))
                {
                    sm.SelectedRows.Add(new SelectedRow(Convert.ToString(item.id)));
                }
            }

            sm.UpdateSelection();

            Window2.Show();
        }


        // Função para salvar as funcionalidades
        protected void salvar_func(object sender, EventArgs e)
        {
            RowSelectionModel sm = this.grid_func.SelectionModel.Primary as RowSelectionModel;

            List<GPERMISSAO> perm = new List<GPERMISSAO>();

            if (Session["permissao"] == "")
            {
                Session["permissao"] = perm;
            }
            else
            {
                perm = (List<GPERMISSAO>) Session["permissao"];
            }

            perm.RemoveAll(a => a.idJanela == Convert.ToInt32(Session["janela"]));

            foreach (SelectedRow row in sm.SelectedRows)
            {
                GPERMISSAO obj = new GPERMISSAO();
                PermissaoControl control = new PermissaoControl();
                obj.idPermissao = Convert.ToInt32(row.RecordID);

                obj = (GPERMISSAO) control.ExecutaFuncao(obj, Funcoes.Buscar, null);

                perm.Add(obj);
            }

            Session["permissao"] = perm;

            Window2.Hide();
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
            string msg = "";

            try
            {
                Global.RegrasNegocio.Global.PerfilControl control = new Global.RegrasNegocio.Global.PerfilControl();
                GPERFIL per = new GPERFIL();

                // Se não for adicionar
                if (hd_tipo.Value.ToString() != "1")
                {
                    per.idPerfil = Convert.ToInt32(hd_id.Value);
                    per = (GPERFIL) control.ExecutaFuncao(per, Funcoes.Buscar, null);

                }

                per.descricao = txt_nome.Text;
                per.codSistema = Convert.ToString(combo_sistemas.Value);
                per.ativo = Convert.ToBoolean(cbx_ativo.Value);
                per.valSenha = Convert.ToInt32(txt_valsenha.Value);

                List<GPERMISSAO> perm = new List<GPERMISSAO>();

                if (Session["permissao"] != "")
                    perm = (List<GPERMISSAO>) Session["permissao"];
                else
                    Session["permissao"] = perm;

                per.GPERMISSAO.Clear();

                foreach (GPERMISSAO pp in perm)
                {
                    GPERMISSAO p = new GPERMISSAO();
                    p.idJanela = pp.idJanela;
                    p.idFuncionalidade = pp.idFuncionalidade;
                    p.idPermissao = pp.idPermissao;

                    PermissaoControl cc = new PermissaoControl();

                    p = (GPERMISSAO) cc.ExecutaFuncao(p, Funcoes.Buscar, null);

                    per.GPERMISSAO.Add(p);
                }


                if (msg == "")
                {
                    bool conf = false;

                    if (hd_tipo.Value.ToString() != "1")
                    {
                        if (hd_tipo.Value.ToString() == "2")
                        {
                            // Atualizar
                            conf = (bool) control.ExecutaFuncao(per, Funcoes.Atualizar, null);
                        }
                    }
                    else
                    {
                        // Adicionar                  
                        conf = (bool) control.ExecutaFuncao(per, Funcoes.Adicionar, null);
                    }

                    if (conf)
                    {
                        if (hd_tipo.Value.ToString() != "1")
                        {
                            if (hd_tipo.Value.ToString() == "2")
                            {
                                msg = "Perfil atualizado com sucesso.";
                            }
                        }
                        else
                        {
                            msg = "Perfil cadastrado com sucesso.";
                        }

                    }
                    else
                    {
                        if (hd_tipo.Value.ToString() != "1")
                        {
                            if (hd_tipo.Value.ToString() == "2")
                            {
                                msg = "Erro ao atualizar perfil, por favor confira todos os campos.";
                            }
                        }
                        else
                        {
                            msg = "Erro ao cadastrar perfil, por favor confira todos os campos.";
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Excecao.TratarExcecao(ex);
                msg = "Erro ao executar tarefa.";
            }


            X.Msg.Alert("Alerta", msg).Show();
            CarregaPrincipal();
        }

        #endregion

    }
}