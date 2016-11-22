using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Artebit.Restaurante.Caixa.Classes;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.Modelo.Enum;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Estoque;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Artebit.Restaurante.Global.Util.WPF;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Label = System.Windows.Controls.Label;

namespace Artebit.Restaurante.Caixa.Caixas
{
    /// <summary>
    /// Interaction logic for Pedido2.xaml
    /// </summary>
    public partial class Pedido2 : IPagina
    {
        private static readonly ECARDAPIOITEM Item = new ECARDAPIOITEM();
        private static readonly CardapioItemControl CardControl = new CardapioItemControl();
        private static readonly CardapioControl CardapioCtrl = new CardapioControl();

        private int _cont;
        private List<int> _excluidos = new List<int>();
        private List<ECARDAPIOITEM> _itensCardapio;
        private int? _nuitem;
        private int _pai;
        private List<ACONTITEM> _pedidos = new List<ACONTITEM>();
        private int? _produto;

        public int QtdPessoas = 1;
        public string TipoCard;
        public bool Carregada { get; set; }

        public Pedido2()
        {
            InitializeComponent();

            Carregada = false;
        }

        private void RadioButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (chkCozinha.IsChecked == true)
            {
                VerticalScroller_cozinha.Visibility = Visibility.Visible;
                VerticalScroller_bar.Visibility = Visibility.Hidden;
            }
            else
            {
                VerticalScroller_cozinha.Visibility = Visibility.Hidden;
                VerticalScroller_bar.Visibility = Visibility.Visible;
            }
        }

        private void txt_OBS_GotFocus(object sender, MouseButtonEventArgs e)
        {
            if (gridDados.SelectedItems.Count == 0)
            {
                RadWindow.Alert("Selecione um Item. ");
            }
        }

        #region Função de carregamento de produtos

        // ====================================================
        // Função de carregamento de produtos
        private void CarregaComidaBebida()
        {
            List<ECARDAPIOITEM> lista = _itensCardapio.Where(r => r.usaPreco == true).ToList();

            grid_pedido_cozinha.ItemsSource =
                lista.Where(r => r.grupo == 1).Distinct().OrderBy(r => r.posicao).ThenBy(r => r.descricao); // Comidas

            grid_pedido_bar.ItemsSource =
                lista.Where(r => r.grupo == 2).Distinct().OrderBy(r => r.posicao).ThenBy(r => r.descricao); // Bebidas

            cmbProduto.ItemsSource = lista.Distinct().OrderBy(r => r.DescricaoPrd).ThenBy(r => r.descricao);
        }

        private void CarregaObservacoes()
        {
            IQueryable<EPRODUTO> lista = (from p in Contexto.Atual.EPRODUTO
                                          where (p.idProduto == _produto)
                                          select p);

            grid3.ItemsSource = lista.Any() ? lista.First().EOBSERVACAO : null;

            VerticalScroller3.ScrollToTop();

            ACONTITEM c = _pedidos.Single(r => r.idProduto == _produto && r.nuItem == _nuitem);

#pragma warning disable 168
            foreach (var i in grid3.Items)
#pragma warning restore 168
            {
                var tb = FindItemControl(grid3, "btObs", Item) as ToggleButton;

                if (tb != null)
                {
                    int id = Convert.ToInt32(tb.CommandParameter);

                    if (c.EOBSERVACAO.Any(o => o.idObs == id))
                    {
                        tb.IsChecked = true;
                    }
                }
            }

            txt_obs.Text = c.txtObs;
        }

        // Função quando é escolhido algum item da grid
        private void gridDados_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            Escolhe_Obs(0);

            foreach (object it in gridDados.SelectedItems)
            {
                //pega o numero do item do objeto anonimo
                var valor = TypeDescriptor.GetProperties(it)[0].GetValue(it);
                if (valor != null)
                {
                    var t = (int) valor;

                    ACONTITEM c = _pedidos.Single(r => r.nuItem == t);

                    _produto = Convert.ToInt32(c.idProduto);
                    _nuitem = t;
                }

                CarregaObservacoes();
            }
        }

        // Função de duplo clique no item da grid, exclui
        private void gridDados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            foreach (object it in gridDados.SelectedItems)
            {
                //pega o numero do item do objeto anonimo
                var valor = TypeDescriptor.GetProperties(it)[0].GetValue(it);
                if (valor != null)
                {
                    var t = (int) valor;

                    ACONTITEM c = _pedidos.SingleOrDefault(r => r.nuItem == t);

                    if (c != null)
                    {
                        if (c.nuItemPai != null && c.nuItemPai != 0)
                        {
                            var ped = _pedidos.SingleOrDefault(p => p.nuItem == c.nuItemPai);

                            if (ped != null)
                            {
                                Decimal preco = ped.preco;
                                ped.preco = preco + c.preco;
                            }
                        }

                        _pedidos.Remove(c);

                        var removidos = _pedidos.Where(r => r.nuItemPai == t).Select(itt => itt.nuItem).ToList();

                        //Cancela adicionais do item pai
                        foreach (ACONTITEM itt in removidos.Select(ii => _pedidos.SingleOrDefault(p => p.nuItem == ii)))
                        {
                            _pedidos.Remove(itt);
                        }
                    }
                }
            }
            _excluidos.Clear();
            populaGrid();
        }

        #endregion

        #region Funções de clique de botões de produtos

        // ========================================
        // Função de clique dos produtos

        // Bar e Cozinha
        private void Escolhe_Cat1(ECARDAPIOITEM itemCard, int qtd = 1)
        {
            Escolhe_Obs(1);

            var i = new ACONTITEM();
            _cont++;
            i.nuItem = _cont;
            i.idProduto = itemCard.idProduto ?? 0;
            i.idVen = Memoria.Vendedor ?? 0;
            i.preco = BuscarPreco(itemCard);
            i.txtObs = null;
            i.adicional = false;
            i.opcao = false;
            i.nuItemPai = null;
            i.quantidade = qtd;

            _pedidos.Add(i);

            _pai = _cont;

            txtQtd.Value = 1;

            populaGrid();
        }

        private void Escolhe_Cat1(object sender, RoutedEventArgs e)
        {
            var bb = (Button) sender;

            var itemCard = bb.DataContext as ECARDAPIOITEM;
            
            Escolhe_Cat1(itemCard, Convert.ToInt32(txtQtd.Value));

            if (TipoCard.Trim() == "C" && chkAdicional.IsChecked == true)
            {
                var p = new Pedido2Adicional();
                
                WindowUtil.MostraModal();
                
                p.itensCardapio = _itensCardapio;
                p.pedidos = _pedidos;
                p.Id = Convert.ToInt32(itemCard);
                p.cont = _cont;
                p.pai = _pai;
                p.carrega();
                p.ShowDialog();

                _pedidos = p.pedidos;
                _cont = p.cont;
                
                populaGrid();
                
                WindowUtil.FechaModal();
            }
            else
            {
                populaGrid();
            }
        }

        private void escolhe_Cat1_Cmb(object sender, RoutedEventArgs e)
        {
            var itemCard = cmbProduto.SelectedItem as ECARDAPIOITEM;

            Escolhe_Cat1(itemCard, Convert.ToInt32(txtQtd.Value));

            if (TipoCard.Trim() == "C" && chkAdicional.IsChecked == true)
            {
                var p = new Pedido2Adicional();
                WindowUtil.MostraModal();
                p.itensCardapio = _itensCardapio;
                p.pedidos = _pedidos;
                p.cont = _cont;
                p.pai = _pai;
                p.carrega();

                p.ShowDialog();

                _pedidos = p.pedidos;
                _cont = p.cont;
                populaGrid();
                WindowUtil.FechaModal();
            }
            else
            {
                populaGrid();
            }

            txtQtd.Value = 1;
        }

        // Observação
        private void Escolhe_Obs(int v)
        {
            if (_produto != null && _produto != 0 && _nuitem != null && _nuitem != 0)
            {
                ACONTITEM c = _pedidos.SingleOrDefault(p => p.idProduto == _produto && p.nuItem == _nuitem);

                if (c != null)
                {
                    c.EOBSERVACAO.Clear();

                    foreach (object item in grid3.Items)
                    {
                        var tb = FindItemControl(grid3, "btObs", item) as ToggleButton;

                        if (tb != null && tb.IsChecked == true)
                        {
                            string descricao = tb.Content.ToString();
                            int idObs = Convert.ToInt32(tb.CommandParameter);

                            var cobs = new ObservacaoControl();
                            var obs = new EOBSERVACAO {idObs = idObs, descricao = descricao};
                            obs = cobs.Buscar(obs);

                            c.EOBSERVACAO.Add(obs);
                        }
                    }

                    c.txtObs = txt_obs.Text;
                }
            }

            grid3.ItemsSource = null;
            txt_obs.Text = "";
            _produto = null;
            _nuitem = null;

            if (v != 0)
            {
                gridDados.UnselectAll();
            }
        }


        private object FindItemControl(ItemsControl itemsControl, string controlName, object item)
        {
            var container = itemsControl.ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;
            if (container != null)
            {
                container.ApplyTemplate();
                return container.ContentTemplate.FindName(controlName, container);
            }
            else
            {
                return null;
            }
        }

        // Função que busca o preço
        private decimal BuscarPreco(ECARDAPIOITEM itemCard)
        {
            switch (itemCard.nuPreco)
            {
                case 1:
                    return itemCard.ProdutoLight.Preco1;
                case 2:
                    return itemCard.ProdutoLight.Preco2;
                case 3:
                    return itemCard.ProdutoLight.Preco3;
                default:
                    return 0;
            }
        }

        // Função para popular grid
        private void populaGrid()
        {
            var l = from i in _pedidos
                    join e in Memoria.Produtos on i.idProduto equals e.idProduto
                    select new
                               {
                                   i.nuItem,
                                   i.preco,
                                   nome =
                        i.adicional ? "..Adicional : " + e.nome : i.opcao ? "....Opção : " + e.nome : e.nome,
                                   qtd = i.quantidade,
                                   und = e.undControle,
                                   tab = i.adicional ? 0 : i.opcao ? 0 : 1
                               };

            gridDados.ItemsSource = l;
            gridDados.UnselectAll();

            //pegar item do footer
            IList<GridViewFooterRow> a = gridDados.ChildrenOfType<GridViewFooterRow>().ToList();

            foreach (GridViewFooterRow r in a)
            {
                var p = r.Template.FindName("nupedido", r) as Label;
                var c = r.Template.FindName("cont", r) as Label;
                var t = r.Template.FindName("totpreco", r) as Label;
                var ch = r.Template.FindName("chk_total", r) as CheckBox;

                if (ch != null) ch.IsChecked = false;

                if (p != null)
                {
                    p.Content = l.Count(d => d.tab == 1);
                }
                if (c != null)
                {
                    c.Content = l.Count();
                }
                if (t != null)
                {
                    t.Content = l.Sum(b => b.preco);
                }
            }
        }

        #endregion

        #region Resto

        // Função chamada quando o botão é criado, arruma o tamanho da letra
        private void Button_Loaded(object sender, RoutedEventArgs e)
        {
            var bb = (Button) sender;
            bb.FontSize = 10;
        }

        // Função chamada quando o toggle é criado, arruma o tamanho da letra
        private void toggle_Loaded(object sender, RoutedEventArgs e)
        {
            var bb = (ToggleButton) sender;
            bb.FontSize = 10;
        }

        // Volta para a página inicial
        private void Voltar(object sender, RoutedEventArgs e)
        {
           NavigationCommands.BrowseBack.Execute(null,null);
        }

        // Exclui um item da lista
        private void Exluir_Click(object sender, RoutedEventArgs e)
        {
            foreach (int t in _excluidos)
            {
                ACONTITEM c = _pedidos.SingleOrDefault(r => r.nuItem == t);

                if (c != null)
                {
                    if (c.nuItemPai != null && c.nuItemPai != 0)
                    {
                        var ped = _pedidos.SingleOrDefault(p => p.nuItem == c.nuItemPai);
                        if (ped != null)
                        {
                            Decimal preco = ped.preco;
                            ped.preco = preco + c.preco;
                        }
                    }

                    _pedidos.Remove(c);

                    var removidos = _pedidos.Where(r => r.nuItemPai == t).Select(itt => itt.nuItem).ToList();

                    //Cancela adicionais do item pai
                    foreach (int ii in removidos)
                    {
                        ACONTITEM itt = _pedidos.SingleOrDefault(p => p.nuItem == ii);
                        _pedidos.Remove(itt);
                    }
                }
            }

            _excluidos.Clear();
            populaGrid();
        }

        // Finaliza o pedido
        private void Finalizar_Click(object sender, RoutedEventArgs e)
        {
            Escolhe_Obs(3);

            if (Finalizar.IsEnabled)
            {
                var mesa = new GMESA();
                var cmesa = new MesaControl();

                var conta = new ACONTA();
                var cconta = new PreContaControl();

                bool teste = false;

                // Ajustar o buscar conta, pode buscar a partir da conta ou mesa ou cliente
                if (Memoria.TipoConta == TipoConta.Mesa)
                {
                    // Mesa
                    mesa.nuMesa = Convert.ToInt32(Memoria.Mesa);
                    mesa = cmesa.Buscar(mesa);
                    conta.nuMesa = mesa.nuMesa;

                    teste = mesa.idStatus != 1 && mesa.idStatus != 4;
                }
                else
                {
                    if (Memoria.TipoConta == TipoConta.Balcao)
                    {
                        // Balcão - comanda/conta
                        conta.idConta = Convert.ToInt32(Memoria.Comanda);
                        conta.idEmpresa = Memoria.Empresa;
                        conta.idFilial = Memoria.Filial;
                    }
                    else
                    {
                        if (Memoria.TipoConta == TipoConta.Cartao)
                        {
                            conta = cconta.BuscarPorCartao(Convert.ToInt32(Memoria.Comanda));

                            if (conta == null)
                            {
                                teste = true;

                                conta = new ACONTA
                                            {
                                                nuCartao = Convert.ToInt32(Memoria.Comanda),
                                                idEmpresa = Memoria.Empresa,
                                                idFilial = Memoria.Filial
                                            };
                            }
                        }
                        else
                        {
                            // Delivery - cliente
                            var cli = new GCLIFOR();

                            var end = new GCLIFOREND
                                          {
                                              telefone1 = Memoria.Cliente,
                                              tipoEndereco = 1
                                          };
                            cli.GCLIFOREND.Add(end);

                            var forn = new FornecedorControl();
                            cli = forn.BuscarPorTelefone(cli);

                            conta.idCliFor = cli.idClifor;

                            //busca a preconta  da mesa informada
                            conta = cconta.Buscar(conta);

                            if (conta == null)
                            {
                                teste = true;
                                conta = new ACONTA {idCliFor = cli.idClifor};
                            }
                        }
                    }
                }

                Funcoes funcao;

                // Se estiver livre, tem mudar o status e criar a conta
                if (teste)
                {
                    funcao = Funcoes.Adicionar;
                    conta.pessoas = 1;

                    foreach (ACONTITEM pedido in _pedidos)
                    {
                        if (pedido.quantidade == 0)
                            pedido.quantidade = 1;
                        pedido.desconto = 0;
                        conta.ACONTITEM.Add(pedido);
                    }

                    // Se não estiver livre, basta acrescentar na conta
                }
                else
                {
                    funcao = Funcoes.Atualizar;

                    conta = cconta.Buscar(conta);

                    int cont = 0;

                    if (conta.ACONTITEM.Count > 0)
                        cont = conta.ACONTITEM.OrderBy(r => r.nuItem).Last().nuItem;

                    foreach (ACONTITEM pedido in _pedidos.Where(r => r.nuItemPai == null).OrderBy(s => s.nuItem))
                    {
                        cont++;

                        pedido.idStatus = 1;
                        int antigopai = pedido.nuItem;
                        pedido.nuItem = cont;
                        pedido.desconto = 0;
                        pedido.dataInclusao = DateTime.Now;
                        conta.ACONTITEM.Add(pedido);

                        // Atualizando os filhos com o novo numero do pai
                        foreach (ACONTITEM itt in _pedidos.Where(r => r.nuItemPai == antigopai).OrderBy(s => s.nuItem))
                        {
                            cont++;

                            var ac = new ACONTITEM
                                         {
                                             adicional = itt.adicional,
                                             idConta = itt.idConta,
                                             idEmpresa = itt.idEmpresa,
                                             idFilial = itt.idFilial,
                                             idProduto = itt.idProduto,
                                             idVen = itt.idVen,
                                             impresso = itt.impresso,
                                             opcao = itt.opcao,
                                             preco = itt.preco,
                                             produzido = itt.produzido,
                                             txtObs = itt.txtObs,
                                             idStatus = 1,
                                             nuItem = cont,
                                             nuItemPai = pedido.nuItem,
                                             quantidade = itt.quantidade,
                                             desconto = 0,
                                             dataInclusao = DateTime.Now
                                         };


                            conta.ACONTITEM.Add(ac);
                        }
                    }
                }

                bool conf = funcao == Funcoes.Adicionar ? cconta.Criar(conta) : cconta.Atualizar(conta);

                if (conf)
                {
                    if (funcao == Funcoes.Adicionar)
                    {
                        bool confmesa = true;

                        if (Memoria.TipoConta == TipoConta.Mesa)
                        {
                            mesa.idStatus = 1;
                            confmesa = cmesa.Atualizar(mesa);
                        }

                        if (confmesa)
                        {
                            _pedidos.Clear();

                            switch (Memoria.TipoConta)
                            {
                                case TipoConta.Mesa:
                                    PaginaCore.PgCaixa_Mesas.Carregar(mesa.nuMesa);
                                    ControlePagina.NavigateTo(PaginaCore.PgCaixa_Mesas, false);
                                    break;
                                
                                case TipoConta.Balcao:
                                    PaginaCore.PgCaixa_Balcao.Carregar(conta.idConta);
                                    ControlePagina.NavigateTo(PaginaCore.PgCaixa_Balcao,false);
                                    break;

                                case TipoConta.Delivery:
                                    PaginaCore.PgCaixa_Delivery.Carregar(conta.idCliFor);
                                    ControlePagina.NavigateTo(PaginaCore.PgCaixa_Delivery);
                                    break;

                                case TipoConta.Cartao:
                                    PaginaCore.PgCaixa_Cartao.Carregar(conta.nuCartao);
                                    ControlePagina.NavigateTo(PaginaCore.PgCaixa_Cartao);
                                    break;
                            }
                        }
                        else
                        {
                            RadWindow.Alert("Erro ao enviar pedido(s).");
                            Finalizar.IsEnabled = false;
                        }
                    }
                    else
                    {
                        switch (Memoria.TipoConta)
                        {
                            case TipoConta.Mesa:
                                PaginaCore.PgCaixa_Mesas.Carregar(mesa.nuMesa);
                                ControlePagina.NavigateTo(PaginaCore.PgCaixa_Mesas, false);
                                break;

                            case TipoConta.Balcao:
                                PaginaCore.PgCaixa_Balcao.Carregar(conta.idConta);
                                ControlePagina.NavigateTo(PaginaCore.PgCaixa_Balcao, false);
                                break;

                            case TipoConta.Delivery:
                                PaginaCore.PgCaixa_Delivery.Carregar(conta.idCliFor);
                                ControlePagina.NavigateTo(PaginaCore.PgCaixa_Delivery);
                                break;

                            case TipoConta.Cartao:
                                PaginaCore.PgCaixa_Cartao.Carregar(conta.nuCartao);
                                ControlePagina.NavigateTo(PaginaCore.PgCaixa_Cartao);
                                break;

                        }
                    }

                    _pedidos.Clear();
                    populaGrid();
                    Finalizar.IsEnabled = true;
                }
                else
                {
                    RadWindow.Alert("Erro ao enviar pedido(s).");
                    Finalizar.IsEnabled = false;
                }
            }
        }

        // Função de clique do checkbox da grid de pedidos
        private void chk_Click(object sender, RoutedEventArgs e)
        {
            var chk = (CheckBox) sender;
            int id = Convert.ToInt32(chk.CommandParameter);

            if (chk.IsChecked == true)
            {
                _excluidos.Add(id);
            }
            else
            {
                _excluidos.Remove(id);
            }
        }

        // Função para marquar e desmarcar todos
        private void chk_Total_Click(object sender, RoutedEventArgs e)
        {
            var chk = (CheckBox) sender;

            //pegar itens da linha da grid
            IList<GridViewRow> rows = gridDados.ChildrenOfType<GridViewRow>().ToList();
            foreach (GridViewRow r in rows)
            {
                var i = r.FindChildByType<CheckBox>();
                if (i != null)
                {
                    if (chk.IsChecked == true)
                    {
                        i.IsChecked = true;
                        int id = Convert.ToInt32(i.CommandParameter);
                        _excluidos.Add(id);
                    }
                    else
                    {
                        i.IsChecked = false;
                        int id = Convert.ToInt32(i.CommandParameter);
                        _excluidos.Remove(id);
                    }
                }
            }
        }

        #endregion

        #region IPagina Members

        void IPagina.Carregar()
        {
            ECARDAPIO obj = CardapioCtrl.BuscarAtual();

            TipoCard = obj.tipo;

            _itensCardapio = CardControl.BuscarItensCardapio(obj.idCardapio);

            if (TipoCard.Trim() == "C")
            {
                chkAdicional.Visibility = Visibility.Visible;
            }

            CarregaComidaBebida();

            string valor = "";
            switch (Memoria.TipoConta)
            {
                case TipoConta.Mesa:
                    valor ="Mesa "+ Memoria.Mesa;
                    break;

                case TipoConta.Balcao:
                    valor = "Comanda " +Memoria.Comanda;
                    break;

                case TipoConta.Delivery:
                    valor = "Conta " + Memoria.Conta;
                    break;

                case TipoConta.Cartao:
                    valor = "Cartão "+Memoria.Comanda;
                    break;
            }

            Cabecalho.Titulo = String.Format("Caixa / {0} - Novo Pedido", valor);
        }
        #endregion

        internal void Limpar()
        {
            _pedidos = new List<ACONTITEM>();
            _cont = 0;

            _excluidos = new List<int>();

            gridDados.ItemsSource = null;
            gridDados.UnselectAll();
        }
    }
}