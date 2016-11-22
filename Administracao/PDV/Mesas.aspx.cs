using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Atendimento;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Ext.Net;

namespace Artebit.Restaurante.Administracao.PDV
{
    public partial class Mesas : System.Web.UI.Page
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
            var compl = new List<string> { "AD", "Mesa", "0" };
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
            MesaControl ct = new MesaControl();
            IQueryable<GMESA> lista = (IQueryable<GMESA>)ct.ExecutaFuncao(null, Funcoes.BuscarLista, null, null);

            var dados = from a in lista
                        select new
                        {
                            codigo = a.nuMesa,
                            qtdLugares = a.qtdLugares,
                            idStatus = a.GSTATMESA.descricao,
                            idImpressora = a.AIMPRESSORA.descricao,
                            observacao = a.observacao,
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

            GMESA obj = new GMESA();
            MesaControl ctrl = new MesaControl();


            #region Editar
            if (comando == "editar" && btnEditarG.Disabled == false)
            {
                //-------------------------------------------------------------------------------------------
                //Preenchendo os campos com as informações do Mesa
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
                bool res = (bool)ctrl.ExecutaFuncao(obj, Funcoes.Cancelar, null, null);

                if (res)
                    X.Msg.Alert("Alerta", "A Mesa foi excluido").Show();

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
                    GMESA ali = new GMESA();

                    ali.nuMesa = id;
                    ali = (GMESA)ctrl.ExecutaFuncao(ali, Funcoes.Buscar, null, null);

                    ali.ativo = !ali.ativo;
                    bool res = (bool)ctrl.ExecutaFuncao(ali, Funcoes.Atualizar, null, null);

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
                registro.Add("codigo", Contexto.GerarId("GMESA").ToString());
                registro.Add("qtdLugares", "0");
                registro.Add("idStatus", "");
                registro.Add("idImpressora", "");
                registro.Add("observacao", "");
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
            GMESA obj = new GMESA();
            MesaControl control = new MesaControl();

            //----------------------------------------------------------------------------------------
            // hd_tipo.Value é um textbox oculto. Ele fica esático durante toda a execução do programa
            //----------------------------------------------------------------------------------------
            Funcoes acao;
            if (hd_tipo.Value.ToString() == "2") //editar
            {
                acao = Funcoes.Atualizar;
                obj.nuMesa = Convert.ToInt32(codigo.Text);
                obj = (GMESA)control.ExecutaFuncao(obj, Funcoes.Buscar, null, null);
            }
            else
            {
                acao = Funcoes.Adicionar;
                obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);
            }


            //----------------------------------------------------------------------------------------
            //Recebendo todos os dados do usuário
            //----------------------------------------------------------------------------------------
            obj.nuMesa = Convert.ToInt32(codigo.Text);
            obj.qtdLugares = Convert.ToInt32(txtQtdeLugares.Value);
            obj.idStatus = Convert.ToInt32(ComboBoxIdStatus.Value);
            obj.observacao = Convert.ToString(txtobs.Value);

            //----------------------------------------------------------------------------------------
            //Recebendo as impressoras selecionadas
            //----------------------------------------------------------------------------------------

            RowSelectionModel sm = gridImpressoras.SelectionModel.Primary as RowSelectionModel;

            obj.AIMPRESSMESA.Clear();
            foreach (SelectedRow row in sm.SelectedRows)
            {
                AIMPRESSMESA imp = new AIMPRESSMESA();
                imp.idImpressora = Convert.ToInt32(row.RecordID);
                imp.idEmpresa = Memoria.Empresa;
                imp.idFilial = Memoria.Filial;
                imp.nuMesa = Convert.ToInt32(codigo.Value);

                obj.AIMPRESSMESA.Add(imp);
            }



            //------------------------------------------------------------------------------------------
            //Salvar o objeto.
            //------------------------------------------------------------------------------------------
            bool? result = (bool)control.ExecutaFuncao(obj, acao, null, null);

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


        #region StoreRefreshData Events

        // Função que carrega os dados do formulário
        protected void StoreFormulario_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
           MesaControl ct = new MesaControl();
           IQueryable<GMESA> lista = (IQueryable<GMESA>)ct.ExecutaFuncao(null, Funcoes.BuscarLista, null, null);

            var dados = from a in lista
                        select new
                        {
                            codigo = a.nuMesa,
                            qtdLugares = a.qtdLugares,
                            idStatus = a.idStatus,
                            idImpressora = a.idImpressora,
                            observacao = a.observacao,
                            ativo = a.ativo,
                            hdtipo = 2
                        };

            StoreFormulario.DataSource = dados;
            StoreFormulario.DataBind();


           

        }

        protected void StoreImpressora_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            ImpressoraControl ct = new ImpressoraControl();
            IQueryable<AIMPRESSORA> lista = (IQueryable<AIMPRESSORA>) ct.ExecutaFuncao(null, Funcoes.BuscarLista, null);

            var dados = from a in lista
                        select new
                                   {
                                       codigo = a.idImpressora,
                                       descricao = a.descricao,
                                       ligado = a.ligadoa,
                                       modelo = a.modelo
                                   };
            StoreImpressoras.DataSource = dados;
            StoreImpressoras.DataBind();

            int id = Convert.ToInt32(codigo.Value);

            IQueryable<AIMPRESSMESA> imp =
                Contexto.Atual.AIMPRESSMESA.Where(
                    r =>
                    r.idEmpresa == Memoria.Empresa && r.idFilial == Memoria.Filial && r.nuMesa == id);


            CheckboxSelectionModel sm = gridImpressoras.SelectionModel.Primary as CheckboxSelectionModel;

            sm.ClearSelections();
            int cont = 0;
            foreach (AIMPRESSMESA a in imp)
            {
                cont++;
                sm.SelectedRows.Add(new SelectedRow(Convert.ToString(a.idImpressora)));
            }
            if (cont > 0)
                sm.UpdateSelection();
            else
                sm.ClearSelections();

        }




        #endregion


    }
}