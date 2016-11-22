using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Estoque;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Artebit.Restaurante.Global.RegrasNegocio.Atendimento;
using Ext.Net;
using Newtonsoft.Json.Linq;

namespace Administracao.PDV
{
    public partial class Monitores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MemoriaWeb.ValidaSessao();

            Contexto.AbrirContexto();

            if (!IsPostBack)
            {
                CarregaPrincipal();
                VerificarPermissoes();
                CarregaMesa();
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
            var compl = new List<string> { "AD", "Monitores", "0" };
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

        // Função para trazer os dados de monitores 
        private void CarregaPrincipal()
        {
            MonitorControl control = new MonitorControl();
            IQueryable<AMONITOR> lista = (IQueryable<AMONITOR>)control.ExecutaFuncao(null, Funcoes.BuscarLista, null);

            var dados = from a in lista
                        select new
                        {
                            codigo = a.idMonitor,
                            descricao = a.descricao,
                            ativo = a.ativo == true ? "Ativo" : "Inativo",
                            hdtipo = 2
                        };

            StorePrincipal.DataSource = dados;
            StorePrincipal.DataBind();
        }

        // Função para trazer os dados de mesa 
        private void CarregaMesa()
        {
            MesaControl control = new MesaControl();
            IQueryable<GMESA> lista = (IQueryable<GMESA>)control.ExecutaFuncao(null, Funcoes.BuscarLista, null, null);

            var dados = from a in lista
                        select new
                        {
                            nuMesa = a.nuMesa,
                            qtdLugares = a.qtdLugares
                        };

            storeMesa.DataSource = dados;
            storeMesa.DataBind();

        }

        // Função para carregar os dados no formulário
        protected void GridAcao(object sender, DirectEventArgs e)
        {
            int id = Convert.ToInt32(e.ExtraParams["codigo"]);
            string comando = Convert.ToString(e.ExtraParams["command"]);

            AMONITOR obj = new AMONITOR();
            obj.idMonitor = id;
            MonitorControl ctrl = new MonitorControl();

            obj = (AMONITOR)ctrl.ExecutaFuncao(obj, Funcoes.Buscar, null);

            #region Editar
            if (comando == "editar" && btnEditarG.Disabled == false)
            {
                //-------------------------------------------------------------------------------------------
                //Preenchendo os campos com as informações do Monitor
                FormPrincipal.AddScript(
                    "if(StoreFormulario.allData != null) { PagingToolbar1.changePage(StoreFormulario.allData.indexOfKey(" +
                    id.ToString() + ") + 1); }" + " else { PagingToolbar1.changePage(StoreFormulario.indexOfId(" + id.ToString() +
                    ") + 1);}");

                //Recebendo as mesas selecionadas
                RowSelectionModel sm = this.GridMesa.SelectionModel.Primary as RowSelectionModel;
                sm.ClearSelections();
                foreach (AMONMESA gmesa in obj.AMONMESA)
                {
                    sm.SelectedRows.Add(new SelectedRow(Convert.ToString(gmesa.nuMesa)));
                }
                sm.UpdateSelection();

                JanelaPrincipal.Show();


            }
            #endregion

            #region Excluir
            if (comando == "excluir")
            {
                bool res = (bool)ctrl.ExecutaFuncao(obj, Funcoes.Cancelar, null);

                if (res)
                    X.Msg.Alert("Alerta", "A Monitor foi excluido").Show();

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
                    AMONITOR ali = new AMONITOR();

                    ali.idMonitor = id;
                    ali.idEmpresa = Memoria.Empresa;
                    ali = (AMONITOR)ctrl.ExecutaFuncao(ali, Funcoes.Buscar, null);

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
                registro.Add("codigo", Contexto.GerarId("AMONITOR").ToString());
                registro.Add("descricao", "");
                registro.Add("ativo", "true");

                StoreFormulario.AddRecord(registro, true);
                StoreFormulario.CommitChanges();

                RowSelectionModel sm = this.GridMesa.SelectionModel.Primary as RowSelectionModel;
                sm.ClearSelections();

                //Mostrando a Janela
                JanelaPrincipal.Show();
            }
            #endregion
        }

        [DirectMethod]
        public void AtualizaMesa(string id)
        {

            AMONITOR obj = new AMONITOR();
            obj.idMonitor = Convert.ToInt16(id);
            MonitorControl ct = new MonitorControl();
            obj = (AMONITOR) ct.ExecutaFuncao(obj, Funcoes.Buscar, null);

            //Recebendo as mesas selecionadas
            RowSelectionModel sm = this.GridMesa.SelectionModel.Primary as RowSelectionModel;
            sm.ClearSelections();
            foreach (AMONMESA gmesa in obj.AMONMESA)
            {
                sm.SelectedRows.Add(new SelectedRow(Convert.ToString(gmesa.nuMesa)));
            }
            sm.UpdateSelection();




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
            AMONITOR obj = new AMONITOR();
            MonitorControl control = new MonitorControl();

            //----------------------------------------------------------------------------------------
            // hd_tipo.Value é um textbox oculto. Ele fica esático durante toda a execução do programa
            //----------------------------------------------------------------------------------------
            Funcoes acao;
            if (hd_tipo.Value.ToString() == "2") //editar
            {
                acao = Funcoes.Atualizar;
                obj.idMonitor = Convert.ToInt32(codigo.Text);
                obj.idEmpresa = Memoria.Empresa;
                obj.idFilial = Memoria.Filial;
                obj = (AMONITOR)control.ExecutaFuncao(obj, Funcoes.Buscar, null);
            }
            else
            {
                acao = Funcoes.Adicionar;
                obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);
            }


            //----------------------------------------------------------------------------------------
            //Recebendo todos os dados do usuário
            //----------------------------------------------------------------------------------------
            obj.idMonitor = Convert.ToInt32(codigo.Text);
            obj.descricao = Convert.ToString(txtDescricao.Value);
            obj.ativo = checkAtivo.Checked;

            //----------------------------------------------------------------------------------------
            //Recebendo as mesas selecionadas
            //----------------------------------------------------------------------------------------

            obj.AMONMESA.Clear();

            CheckboxSelectionModel sm = this.GridMesa.SelectionModel.Primary as CheckboxSelectionModel;


            foreach (SelectedRow row in sm.SelectedRows)
            {
                AMONMESA mesa = new AMONMESA();

                mesa.nuMesa = Convert.ToInt32(row.RecordID);
                mesa.idEmpresa = Memoria.Empresa;
                mesa.idFilial = Memoria.Filial;
                mesa.idMonitor = Convert.ToInt32(codigo.Value);

                obj.AMONMESA.Add(mesa);

            }


            //----------------------------------------------------------------------------------------
            //Recebendo os produtos selecionados
            //----------------------------------------------------------------------------------------
            string j = "";
            List<object> items = null;

            obj.AMONPRD.Clear();

            j = e.ExtraParams["Produtos"];
            items = JSON.Deserialize<List<object>>(j);
            foreach (object a in items)
            {
                AMONPRD prd = new AMONPRD();
                prd.idProduto = Convert.ToInt32(JContainer.FromObject(a)["codigo"].ToString());
                prd.idEmpresa = Memoria.Empresa;
                prd.idFilial = Memoria.Filial;
                prd.idMonitor = Convert.ToInt32(codigo.Value);

                obj.AMONPRD.Add(prd);
            }


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

        #region StoreRefreshData Events

        // Função que carrega os dados do formulário
        protected void StoreFormulario_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            MonitorControl control = new MonitorControl();
            IQueryable<AMONITOR> lista = (IQueryable<AMONITOR>)control.ExecutaFuncao(null, Funcoes.BuscarLista, null);

            var dados = from a in lista
                        select new
                        {
                            codigo = a.idMonitor,
                            descricao = a.descricao,
                            ativo = a.ativo == true ? "Ativo" : "Inativo",
                            hdtipo = 2
                        };

            StoreFormulario.DataSource = dados;
            StoreFormulario.DataBind();
        }

        protected void StoreProdutos_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (codigo.Value != "")
            {
                int id = Convert.ToInt32(codigo.Value);

                //todos os produtos do monitor
                var lista = from p in Contexto.Atual.EPRODUTO
                            where
                                p.AMONPRD.Where(
                                    r =>
                                    r.idEmpresa == Memoria.Empresa && r.idMonitor == id &&
                                    r.idFilial == Memoria.Filial).Any()
                            select new
                            {
                                codigo = p.idProduto,
                                nome = p.codigo + " - " + p.nome
                            };

                storeProdutoSelecionado.DataSource = lista;
                storeProdutoSelecionado.DataBind();


                //todos os produtos do monitor
                var lista2 = from p in Contexto.Atual.EPRODUTO
                             where
                                 !p.AMONPRD.Where(
                                     r =>
                                     r.idEmpresa == Memoria.Empresa && r.idMonitor == id &&
                                     r.idFilial == Memoria.Filial).Any()
                             select new
                             {
                                 codigo = p.idProduto,
                                 nome = p.codigo + " - " + p.nome
                             };


                storeProduto.DataSource = lista2;
                storeProduto.DataBind();


            }
        }

        #endregion



        #region antigo

        //protected void AbrirJanela(object sender, DirectEventArgs e)
        //{

        //    GPERFIL perfil = new GPERFIL();
        //    Perfil per = new Perfil();
        //    perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
        //    perfil = (GPERFIL)per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
        //    List<string> compl = new List<string>();
        //    compl.Add("AD");
        //    compl.Add("Monitores");
        //    compl.Add("0");

        //    compl[2] = "18";
        //    bool result2 = (bool)per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

        //    if (result2)
        //    {
        //        //Limpando os campos
        //        txtDescricao.Value = "";
        //        checkAtivo.Checked = true;

        //        RowSelectionModel sm = this.GridMesa.SelectionModel.Primary as RowSelectionModel;
        //        sm.ClearSelections();

        //        //RowSelectionModel sp = this.GridProduto.SelectionModel.Primary as RowSelectionModel;
        //        //sp.ClearSelections();



        //        tabFilial.SetActiveTab(0);


        //        //Campo oculto para controle de edição
        //        hd_tipo.Value = 0;
        //        codigo.Value = "-1";

        //        CarregaProduto();

        //        //Abre a janela
        //        JanelaPrincipal.Show();
        //    }
        //    else
        //    {
        //        X.Msg.Alert("Alerta", "Você não tem permissão de executar essa ação, contate o administrador").
        //            Show();
        //    }
        //}

        ////protected void SalvarDados(object sender, DirectEventArgs e)
        ////{
        ////    AMONITOR obj = new AMONITOR();
        ////    Monitor control = new Monitor();

        ////    Funcoes acao = Funcoes.Adicionar;

        ////    if (hd_tipo.Value.ToString() == "1") // O objeto foi editado.
        ////    {
        ////        obj.idMonitor = Convert.ToInt32(codigo.Value);
        ////        obj = (AMONITOR)control.ExecutaFuncao(obj, Funcoes.Buscar, null);
        ////        acao = Funcoes.Atualizar;
        ////    }

        ////    obj.descricao = Convert.ToString(txtDescricao.Value);
        ////    obj.ativo = checkAtivo.Checked;

        ////    //Recebendo as mesas selecionadas
        ////    RowSelectionModel sm = this.GridMesa.SelectionModel.Primary as RowSelectionModel;

        ////    obj.AMONMESA.Clear();
        ////    foreach (SelectedRow row in sm.SelectedRows)
        ////    {
        ////        AMONMESA mesa = new AMONMESA();
        ////        mesa.nuMesa = Convert.ToInt32(row.RecordID);
        ////        mesa.idEmpresa = Convert.ToInt32(Memoria.Empresa);
        ////        mesa.idFilial = Convert.ToInt32(Memoria.Filial);

        ////        obj.AMONMESA.Add(mesa);
        ////    }

        ////    //Recebendo os produtos selecionadas
        ////    //RowSelectionModel sp = this.GridProduto.SelectionModel.Primary as RowSelectionModel;

        ////    obj.AMONPRD.Clear();

        ////    string j = e.ExtraParams["Insumos"];
        ////    List<object> items = JSON.Deserialize<List<object>>(j);
        ////    foreach (object a in items)
        ////    {
        ////        int codigo = Convert.ToInt32(Newtonsoft.Json.Linq.JContainer.FromObject(a)["codigo"].ToString());

        ////        AMONPRD produto = new AMONPRD();
        ////        produto.idProduto = codigo;
        ////        produto.idEmpresa = Convert.ToInt32(Memoria.Empresa);
        ////        produto.idFilial = Convert.ToInt32(Memoria.Filial);

        ////        obj.AMONPRD.Add(produto);
        ////    }

        ////    bool result = false;
        ////    if (obj.descricao != "" && obj.AMONMESA.Count != 0 && obj.AMONPRD.Count != 0)
        ////    {
        ////        result = (bool)control.ExecutaFuncao(obj, acao, null);
        ////        CarregaPrincipal();
        ////    }

        ////    if (result)
        ////    {
        ////        X.Msg.Alert("Alerta", "Dados gravados com sucesso").Show();
        ////        JanelaPrincipal.Hide();
        ////    }

        ////    else
        ////        X.Msg.Alert("Alerta", "Verifique os dados digitados e tente novamente").Show();

        ////}


        //protected void FecharJanela(object sender, EventArgs e)
        //{
        //    JanelaPrincipal.Hide();
        //}

        ////protected void GridAcao(object sender, DirectEventArgs e)
        ////{
        ////    //Recebendo os parâmetros da grid
        ////    int id = Convert.ToInt32(e.ExtraParams["id"]);
        ////    string comando = Convert.ToString(e.ExtraParams["command"]);

        ////    //Buscando o monitor
        ////    AMONITOR obj = new AMONITOR();
        ////    Monitor control = new Monitor();
        ////    obj.idMonitor = id;
        ////    obj = (AMONITOR)control.ExecutaFuncao(obj, Funcoes.Buscar, null);

        ////    if (comando == "editar")
        ////    {
        ////        GPERFIL perfil = new GPERFIL();
        ////        Perfil per = new Perfil();
        ////        perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
        ////        perfil = (GPERFIL)per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
        ////        List<string> compl = new List<string>();
        ////        compl.Add("AD");
        ////        compl.Add("Monitores");
        ////        compl.Add("0");

        ////        compl[2] = "2";
        ////        bool result2 = (bool)per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

        ////        if (result2)
        ////        {
        ////            //Carregando o campo de descrição
        ////            txtDescricao.Value = obj.descricao;

        ////            //Carrega o status de ativo/desativo
        ////            checkAtivo.Checked = Convert.ToBoolean(obj.ativo);

        ////            //Carrega a grid mesas
        ////            RowSelectionModel sm = this.GridMesa.SelectionModel.Primary as RowSelectionModel;
        ////            sm.ClearSelections();
        ////            foreach (AMONMESA gmesa in obj.AMONMESA)
        ////            {
        ////                sm.SelectedRows.Add(new SelectedRow(Convert.ToString(gmesa.nuMesa)));
        ////            }
        ////            sm.UpdateSelection();

        ////            //Campos ocultos
        ////            hd_tipo.Value = 1;
        ////            codigo.Value = obj.idMonitor;

        ////            tabFilial.SetActiveTab(0);
        ////            CarregaProduto();

        ////            JanelaPrincipal.Show();


        ////        }
        ////        else
        ////        {
        ////            X.Msg.Alert("Alerta", "Você não tem permissão de executar essa ação, contate o administrador").
        ////                Show();
        ////        }
        ////    }
        ////    if (comando == "desativar")
        ////    {
        ////        GPERFIL perfil = new GPERFIL();
        ////        Perfil per = new Perfil();
        ////        perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
        ////        perfil = (GPERFIL)per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
        ////        List<string> compl = new List<string>();
        ////        compl.Add("AD");
        ////        compl.Add("Monitores");
        ////        compl.Add("0");

        ////        compl[2] = "20";
        ////        bool result2 = (bool)per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

        ////        if (result2)
        ////        {
        ////            obj.ativo = false;
        ////            bool result = (bool)control.ExecutaFuncao(obj, Funcoes.Atualizar, null);
        ////            CarregaPrincipal();

        ////            if (result)
        ////                X.Msg.Alert("Alerta", "O monitor foi desativado").Show();
        ////        }
        ////        else
        ////        {
        ////            X.Msg.Alert("Alerta", "Você não tem permissão de executar essa ação, contate o administrador").
        ////                Show();
        ////        }

        ////    }
        ////    if (comando == "ativar")
        ////    {
        ////        GPERFIL perfil = new GPERFIL();
        ////        Perfil per = new Perfil();
        ////        perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
        ////        perfil = (GPERFIL)per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
        ////        List<string> compl = new List<string>();
        ////        compl.Add("AD");
        ////        compl.Add("Monitores");
        ////        compl.Add("0");

        ////        compl[2] = "19";
        ////        bool result2 = (bool)per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

        ////        if (result2)
        ////        {
        ////            obj.ativo = true;
        ////            bool result = (bool)control.ExecutaFuncao(obj, Funcoes.Atualizar, null);
        ////            CarregaPrincipal();

        ////            if (result)
        ////                X.Msg.Alert("Alerta", "O monitor foi ativado").Show();
        ////        }
        ////        else
        ////        {
        ////            X.Msg.Alert("Alerta", "Você não tem permissão de executar essa ação, contate o administrador").
        ////                Show();
        ////        }

        ////    }

        ////}

        //[DirectMethod]
        //public void ExcluirVarios()
        //{
        //    //bool sucesso = true;
        //    //foreach (var i in smGrid.SelectedRows)
        //    //{
        //    //    int id = Convert.ToInt32(i.RecordID);
        //    //    if (!Excluir(id))
        //    //    {
        //    //        sucesso = false;
        //    //        break;
        //    //    }
        //    //}

        //    //if (sucesso)
        //    //{
        //    //    Notification.Show(new NotificationConfig
        //    //    {
        //    //        Title = "Informação",
        //    //        Icon = Icon.Information,
        //    //        Html = "Itens Excluídos com sucesso!"
        //    //    });
        //    //}
        //}

        //protected bool Excluir(int id)
        //{
        //    Monitor mon = new Monitor();

        //    AMONITOR monitor = new AMONITOR();
        //    monitor.idEmpresa = Memoria.Empresa;
        //    monitor.idMonitor = id;

        //    bool ret = (bool)mon.ExecutaFuncao(monitor, Funcoes.Cancelar, null);

        //    if (ret)
        //    {
        //        //FetchRecord(storeGeral, new StoreRefreshDataEventArgs());
        //        tabFilial.AddScript("StorePrincipal.commitChanges();");
        //    }
        //    else
        //    {
        //        X.Msg.SetIcon(MessageBox.Icon.WARNING);
        //        X.Msg.Alert("Informação", Memoria.MsgGlobal).Show();
        //    }

        //    CarregaPrincipal();
        //    return ret;
        //}

        #endregion

    }
}