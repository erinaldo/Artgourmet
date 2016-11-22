using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Estoque;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Ext.Net;
using System.Data;

namespace Artebit.Restaurante.Administracao.Producao
{
    public partial class Cardapio : System.Web.UI.Page
    {
        private List<ECARDAPIO> _listCardapio;
        private readonly List<ECARDAPIOITEM> _listItens = null;

        #region Page Load/Unload e Carregamento Global
        protected void Page_Load(object sender, EventArgs e)
        {
            MemoriaWeb.ValidaSessao();

            Contexto.AbrirContexto();

            if (!IsPostBack)
            {
                CarregaComboProdutos();

                VerificaPermissoes();
            }
        }

        // Função chamada quando a página acaba de carregar
        protected void Page_Unload(object sender, EventArgs e)
        {
            Contexto.FecharContexto();
        }

        private void VerificaPermissoes()
        {
            var per = new PerfilControl();
            var perfil = new GPERFIL {idPerfil = Convert.ToInt32(Memoria.Perfil)};

            perfil = per.Buscar(perfil);

            /*
             * Permissão: ATIVAR/DESATIVAR
             */
            bool result = per.Verificar(perfil, "Cardapio", 19);

            if (!result)
            {
                btnAtivarDesativarG.Disabled = true;
            }

            /*
             * Permissão: EDITAR ITEM
             */
            result = per.Verificar(perfil, "Cardapio", 2);

            if (!result)
            {
                btnEditarG.Disabled = true;
            }

            /*
            * Permissão: NOVO ITEM
            */
            result = per.Verificar(perfil, "Cardapio", 18);

            if (!result)
            {
                btnNovoG.Disabled = true;
            }

        }

        // Carrega lista de cardapios
        private void LoadListCardapio()
        {
            if (_listCardapio == null)
            {
                var ct = new CardapioControl();
                _listCardapio = ct.BuscarLista().ToList();
            }
        }

        // Função chamada quando a grid é carregada, no load da grid
        protected void LoadGridPrincipal(object sender, EventArgs e)
        {
            CarregaPrincipal();
        }

        // Função para colocar a lista de cardápios na store
        protected void CarregaPrincipal()
        {
            LoadListCardapio();

            var dados = from e in _listCardapio
                        select new
                        {
                            codigo = e.idCardapio,
                            e.nome,
                            diaTodo = e.diaTodo == true ? "Sim" : "Não",
                            segunda = e.segunda == true ? "Sim" : "Não",
                            terca = e.terca == true ? "Sim" : "Não",
                            quarta = e.quarta == true ? "Sim" : "Não",
                            quinta = e.quinta == true ? "Sim" : "Não",
                            sexta = e.sexta == true ? "Sim" : "Não",
                            sabado = e.sabado == true ? "Sim" : "Não",
                            domingo = e.domingo == true ? "Sim" : "Não",
                            status = e.ativo == true ? "Ativo" : "Inativo",
                            tipo = e.tipo.Trim() == "C" ? "Composto" : "Simples"
                        };

            storeCardapioGrid.DataSource = dados;
            storeCardapioGrid.DataBind();
        }

        protected void CarregaComboProdutos()
        {
            var ct = new ProdutoControl();
            IQueryable<EPRODUTO> lista = ct.BuscarLista();

            var dados = from a in lista
                        where a.tipoItem == 1
                        && a.ativo == true
                        orderby a.nome
                        select new
                        {
                            codigo = a.idProduto,
                            nome = a.codigo + " - " + a.nome,
                            nome2 = a.nome
                        };

            storeComboProdutos.DataSource = dados;
            storeComboProdutos.DataBind();
        }
        #endregion

        // Adiciona novo cardapio ao store
        protected void NovoCardapio()
        {
            LoadListCardapio();

            var novo = new ECARDAPIO
                           {
                               idCardapio = Contexto.GerarId("ECARDAPIO"),
                               idEmpresa = 0,
                               ativo = true,
                               nome = "",
                               diaTodo = true,
                               segunda = true,
                               terca = true,
                               quarta = true,
                               quinta = true,
                               sexta = true,
                               sabado = true,
                               domingo = true
                           };

            novo.ativo = true;
            novo.tipo = "C";
            novo.horInicio = null;
            novo.horFinal = null;

            _listCardapio.Add(novo);

            CarregaCardapio(storeCardapio, new StoreRefreshDataEventArgs());
            PagingToolbar1.PageIndex = _listCardapio.Count;
        }

        // Função chamada quando clicar no botão de novo em cardapio
        protected void AddClick(object sender, DirectEventArgs e)
        {
            NovoCardapio();
            FormPanel1.AddScript("#{FormPanel1}.body.unmask();");
        }

        // Função para excluir cardapio
        protected void ExcluirClick(object sender, DirectEventArgs e)
        {
            ExcluirItem(Convert.ToInt32(cardapioID.Value));

            CarregaCardapio(storeCardapio, new StoreRefreshDataEventArgs());

            if (_listCardapio.Count == 1)
            {
                JanelaPrincipal.Hide();
            }
            else
            {
                FormPanel1.AddScript("storeCardapio.commitChanges(); storeCardapio.reload();");

                FormPanel1.AddScript(
                    "if(storeCardapio.allData != null) { PagingToolbar1.changePage(storeCardapio.allData.indexOfKey(" +
                    cardapioID.Value + ")); }" +
                    " else { PagingToolbar1.changePage(storeCardapio.indexOfId(" + cardapioID.Value + "));}");
            }
        }

        // Função de ação de cardapio ============================================================
        protected void GridAcao(object sender, DirectEventArgs e)
        {
            int idItem = Convert.ToInt32(e.ExtraParams["codigo"]);
            string comando = Convert.ToString(e.ExtraParams["command"]);

            var ctrl = new CardapioControl();

            #region Editar
            if (comando == "editar" && btnEditarG.Disabled == false)
            {
                TabPanel1.ActiveTabIndex = 0;
                CarregaCardapio(storeCardapio, new StoreRefreshDataEventArgs());

                //Limpando a seleção da grid
                //RowSelectionModel sm = this.GridItens.SelectionModel.Primary as RowSelectionModel;
                //sm.ClearSelections();

                //-------------------------------------------------------------------------------------------
                //Preenchendo os campos com as informações do produto
                FormPanel1.AddScript(
                    "if(storeCardapio.allData != null) { PagingToolbar1.changePage(storeCardapio.allData.indexOfKey(" +
                    idItem.ToString(CultureInfo.InvariantCulture) + ") + 1); }" +
                    " else { PagingToolbar1.changePage(storeCardapio.indexOfId(" + idItem.ToString(CultureInfo.InvariantCulture) + ") + 1);}");

                JanelaPrincipal.Show();
            }
            #endregion
            
            #region Ativar/Desativar
            if (comando == "ativar")
            {
                bool sucesso = true;

                foreach (var i in chkSelect.SelectedRows)
                {
                    int id = Convert.ToInt32(i.RecordID);
                    //-------------------------------------------------------------------------------------------
                    //Carregando as informações
                    //-------------------------------------------------------------------------------------------
                    var obj = new ECARDAPIO {idCardapio = id, idEmpresa = Memoria.Empresa, idFilial = Memoria.Filial};
                    obj = ctrl.Buscar(obj);

                    obj.ativo = !obj.ativo;
                    bool res = ctrl.Atualizar(obj);

                    if (!res)
                    {
                        sucesso = false;
                        break;
                    }
                }

                if (sucesso)
                {
                    FormPanel1.AddScript("storeCardapioGrid.reload();");
                    Notification.Show(new NotificationConfig
                                          {
                                              Title = "Informação",
                                              Icon = Icon.Information,
                                              Html = "Itens Ativados/Desativados com sucesso!"
                                          });
                }
            }
            #endregion

            #region Excluir
            if (comando == "excluir")
            {
                bool sucesso = true;

                foreach (var i in chkSelect.SelectedRows)
                {
                    int id = Convert.ToInt32(i.RecordID);

                    cardapioID.Text = Convert.ToString(id);
                    //LoadListItens();

                    _listItens.RemoveAll(c => c.idCardapio == id);
                    //AtualizaSessao(_listItens);

                    var obj = new ECARDAPIO {idCardapio = id};
                    obj = ctrl.Buscar(obj);

                    bool res = ctrl.Excluir(obj);

                    if (!res)
                    {
                        sucesso = false;
                        break;
                    }
                }

                if (sucesso)
                {
                    FormPanel1.AddScript("storeCardapioGrid.reload();");
                    Notification.Show(new NotificationConfig
                                          {
                                              Title = "Informação",
                                              Icon = Icon.Information,
                                              Html = "Itens Excluídos com sucesso!"
                                          });
                }
            }
            #endregion

            #region Novo
            if(comando == "novo")
            {
                TabPanel1.ActiveTabIndex = 0;
                JanelaPrincipal.Show();

                NovoCardapio();
            }
            #endregion
        }

        #region Gravar Dados Formulário
        protected void btnSalvarPrincipal_Click(object sender, DirectEventArgs e)
        {
            var obj = new ECARDAPIO();
            var ct = new CardapioControl();
            var acao = Funcoes.Adicionar;

            if (hd_tipo.Value.ToString() == "1") // O objeto foi editado.
            {
                obj.idCardapio = Convert.ToInt32(cardapioID.Value);
                obj = ct.Buscar(obj);
                acao = Funcoes.Atualizar;
            }

            obj.idCardapio = Convert.ToInt32(cardapioID.Text);
            obj.ativo = CheckAtivo.Checked;
            obj.nome = txtNome.Text;
            obj.diaTodo = CheckDiaTodo.Checked;

            if (!CheckDiaTodo.Checked)
            {
                string dia = "01/01/2000 00:00:00";
                string horIni = txthorarioini.Text;
                string horFim = txthorariofim.Text;

                obj.horInicio = Convert.ToDateTime(horIni != "" ? dia.Replace("00:00:00", horIni + ":00") : dia);

                dia = "01/01/2000 00:00:00";

                obj.horFinal = Convert.ToDateTime(horFim != "" ? dia.Replace("00:00:00", horFim + ":00") : dia);
            }
            else
            {
                obj.horInicio = null;
                obj.horFinal = null;
            }

            obj.segunda = CheckSegunda.Checked;
            obj.terca = CheckTerca.Checked;
            obj.quarta = CheckQuarta.Checked;
            obj.quinta = CheckQuinta.Checked;
            obj.sexta = CheckSexta.Checked;
            obj.sabado = CheckSabado.Checked;
            obj.domingo = CheckDomingo.Checked;
            obj.ativo = CheckAtivo.Checked;
            obj.tipo = rdTipo.CheckedItems[0].InputValue;

            #region Trata Grupo 1
            string j = e.ExtraParams["Grupo1"];
            var itemsG1 = JSON.Deserialize<List<ECARDAPIOITEM>>(j);

            foreach (var item in itemsG1)
            {
                if (item.idProduto == 0)
                {
                    item.idProduto = null;
                }
                if (item.nuPreco <= 0 || item.nuPreco > 3)
                {
                    item.nuPreco = 1;
                }
                if (!item.cor.Contains('#'))
                {
                    item.cor = "#" + item.cor;
                }

                if (!item.corFonte.Contains('#'))
                {
                    item.corFonte = "#" + item.corFonte;
                }

                if (item.idEmpresa == 0)
                {
                    item.idEmpresa = Memoria.Empresa;
                    item.idFilial = Memoria.Filial;
                    item.idCardapio = obj.idCardapio;
                   
                    Contexto.Atual.AddToECARDAPIOITEM(item);
                    Contexto.Atual.ObjectStateManager.ChangeObjectState(item, EntityState.Added);
                }
                else
                {
                    Contexto.Atual.AttachTo("ECARDAPIOITEM", item);
                    Contexto.Atual.ObjectStateManager.ChangeObjectState(item, EntityState.Modified);
                }
            }
            #endregion

            #region Trata Grupo 2
            j = e.ExtraParams["Grupo2"];
            var itemsG2 = JSON.Deserialize<List<ECARDAPIOITEM>>(j);

            foreach (var item in itemsG2)
            {
                if (item.idProduto == 0)
                {
                    item.idProduto = null;
                }
                if (item.nuPreco <= 0 || item.nuPreco > 3)
                {
                    item.nuPreco = 1;
                }
                if (!item.cor.Contains('#'))
                {
                    item.cor = "#" + item.cor;
                }

                if (!item.corFonte.Contains('#'))
                {
                    item.corFonte = "#" + item.corFonte;
                }

                if (item.idEmpresa == 0)
                {
                    item.idEmpresa = Memoria.Empresa;
                    item.idFilial = Memoria.Filial;
                    item.idCardapio = 137;

                    Contexto.Atual.AddToECARDAPIOITEM(item);
                    Contexto.Atual.ObjectStateManager.ChangeObjectState(item, EntityState.Added);
                }
                else
                {
                    Contexto.Atual.AttachTo("ECARDAPIOITEM", item);
                    Contexto.Atual.ObjectStateManager.ChangeObjectState(item, EntityState.Modified);
                }
            }
            #endregion

            #region Trata Categoria 1
            j = e.ExtraParams["Categoria1"];
            var itemsC1 = JSON.Deserialize<List<ECARDAPIOITEM>>(j);

            foreach (var item in itemsC1)
            {
                if (item.idProduto == 0)
                {
                    item.idProduto = null;
                }
                if (item.nuPreco <= 0 || item.nuPreco > 3)
                {
                    item.nuPreco = 1;
                }
                if (!item.cor.Contains('#'))
                {
                    item.cor = "#" + item.cor;
                }

                if (!item.corFonte.Contains('#'))
                {
                    item.corFonte = "#" + item.corFonte;
                }

                if (item.idEmpresa == 0)
                {
                    item.idEmpresa = Memoria.Empresa;
                    item.idFilial = Memoria.Filial;
                    item.idCardapio = obj.idCardapio;

                    Contexto.Atual.AddToECARDAPIOITEM(item);
                    Contexto.Atual.ObjectStateManager.ChangeObjectState(item, EntityState.Added);
                }
                else
                {
                    Contexto.Atual.AttachTo("ECARDAPIOITEM", item);
                    Contexto.Atual.ObjectStateManager.ChangeObjectState(item, EntityState.Modified);
                }
            }
            #endregion

            #region Trata Categoria 2
            j = e.ExtraParams["Categoria2"];
            var itemsC2 = JSON.Deserialize<List<ECARDAPIOITEM>>(j);

            foreach (var item in itemsC2)
            {
                if (item.idProduto == 0)
                {
                    item.idProduto = null;
                }
                if (item.nuPreco <= 0 || item.nuPreco > 3)
                {
                    item.nuPreco = 1;
                }
                if (!item.cor.Contains('#'))
                {
                    item.cor = "#" + item.cor;
                }

                if (!item.corFonte.Contains('#'))
                {
                    item.corFonte = "#" + item.corFonte;
                }

                if (item.idEmpresa == 0)
                {
                    item.idEmpresa = Memoria.Empresa;
                    item.idFilial = Memoria.Filial;
                    item.idCardapio = obj.idCardapio;

                    Contexto.Atual.AddToECARDAPIOITEM(item);
                    Contexto.Atual.ObjectStateManager.ChangeObjectState(item, EntityState.Added);
                }
                else
                {
                    Contexto.Atual.AttachTo("ECARDAPIOITEM", item);
                    Contexto.Atual.ObjectStateManager.ChangeObjectState(item, EntityState.Modified);
                }
            }
            #endregion

            bool result = acao == Funcoes.Adicionar ? ct.Criar(obj) : ct.Atualizar(obj);

            if (result)
            {
                FormPanel1.AddScript("storeCardapioGrid.reload();");
                CarregarGrupos(obj.idCardapio);

                ExtNet.Msg.Alert("Alerta", "Os dados foram armazendos com sucesso.").Show();
            }
            else
                ExtNet.Msg.Alert("Alerta", "Verifique os dados digitados e tente novamente.").Show();

        }
        #endregion

        [DirectMethod]
        public void ExcluirVarios()
        {
            var parametros = new ParameterCollection
                                 {
                                                             new Parameter("codigo", "0"),
                                                             new Parameter("command", "excluir"),
                                                         };

            GridAcao(btnExcluirG, new DirectEventArgs(parametros));
        }

        public void ExcluirItem(int id)
        {
            //Removendo os itens do cardápio
            cardapioID.Text = Convert.ToString(id);
            //LoadListItens();

            _listItens.RemoveAll(c => c.idCardapio == id);
            //AtualizaSessao(_listItens);

            //Removendo o cardápio
            var cardapio = new ECARDAPIO();
            var ct = new CardapioControl();

            cardapio.idCardapio = id;
            cardapio = ct.Buscar(cardapio);

            bool resultado = ct.Excluir(cardapio);

            if (resultado)
            {
                CarregaCardapio(storeCardapio, new StoreRefreshDataEventArgs());
                ExtNet.Msg.Alert("Alerta", "Os registros foram excluidos").Show();
            }
            else
                ExtNet.Msg.Alert("Alerta", " Não foi possivel exluir, tente mais tarde").Show();
        }

        [DirectMethod]
        public void CarregarPrecos(int idProduto)
        {
            ETABPRECO tab =
                Contexto.Atual.ETABPRECO.FirstOrDefault(
                    r =>
                    r.idProduto == idProduto && r.idEmpresa == Memoria.Empresa &&
                    (r.idFilial == Memoria.Filial || r.idFilial == 0) && r.ativo);

            if (tab != null)
            {
                cbPrecoItem.Clear();
                cbPrecoItem.InsertItem(0, Convert.ToString(tab.preco1), "1");
                cbPrecoItem.InsertItem(1, Convert.ToString(tab.preco2), "2");
                cbPrecoItem.InsertItem(2, Convert.ToString(tab.preco3), "3");

                cbPrecoItem.SelectedIndex = 0;

                cbPrecoItem.Disabled = false;
            }
            else
            {
                cbPrecoItem.Clear();
                cbPrecoItem.Disabled = true;
            }
        }

        [DirectMethod]
        public void CarregarGrupos(int idCardapio)
        {
            var lista = from p in Contexto.Atual.ECARDAPIOITEM
                        where p.idCardapio == idCardapio && p.idEmpresa == Memoria.Empresa && p.idFilial == Memoria.Filial
                        select p;

            storeGrupo1.DataSource = lista.Where(r => r.grupo == 1 && (r.idItemPai == null || r.idItemPai == 0)).OrderBy(r => r.posicao);
            storeGrupo1.DataBind();

            storeGrupo2.DataSource = lista.Where(r => r.grupo == 2 && (r.idItemPai == null || r.idItemPai == 0)).OrderBy(r => r.posicao);
            storeGrupo2.DataBind();

            storeCat1.DataSource = lista.Where(r => r.idItemPai != null && r.idItemPai != 0).OrderBy(r=> r.posicao);
            storeCat1.DataBind();
            storeCat1.Filter("idItemPai", "-1", true, true);

            storeCat2.DataSource = lista.Where(r => r.idItemPai != null && r.idItemPai != 0).OrderBy(r => r.posicao);
            storeCat2.DataBind();
            storeCat2.Filter("idItemPai", "-1", true, true);

            if (lista.Any())
            {
                var item = lista.OrderByDescending(r => r.idItemCard).FirstOrDefault();
                if (item != null)
                    hdIdItensCardapio.SetValue(item.idItemCard + 1);
            }
            else
            {
                hdIdItensCardapio.SetValue(1);
            }
        }

        #region StoreRefresh Events
        protected void CarregaCardapio(object sender, StoreRefreshDataEventArgs ev)
        {
            LoadListCardapio();

            var dados = from e in _listCardapio
                        select new
                        {
                            codigo = e.idCardapio,
                            e.nome,
                            e.diaTodo,
                            e.segunda,
                            e.terca,
                            e.quarta,
                            e.quinta,
                            e.sexta,
                            e.sabado,
                            e.domingo,
                            status = e.ativo,
                            tipo = e.tipo.Trim(),
                            hdtipo = e.idEmpresa == 0 ? 2 : 1
                        };

            storeCardapio.DataSource = dados;
            storeCardapio.DataBind();
        }

        // Função chamada quando a store pe recarregada , exemplo: ao mudar de página ou fazer filtro
        protected void CarregaGridPrincipal(object sender, StoreRefreshDataEventArgs e)
        {
            CarregaPrincipal();
        }
        #endregion
    }
}