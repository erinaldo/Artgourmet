using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Artebit.Restaurante.AtendimentoPDV.Classes;
using Artebit.Restaurante.Global.AcessoDados.Atendimento;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.Modelo.Enum;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Estoque;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Artebit.Restaurante.Global.Util.WPF;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Label = System.Windows.Controls.Label;

namespace Artebit.Restaurante.AtendimentoPDV.Telas
{
    /// <summary>
    /// Interaction logic for Pedido.xaml
    /// </summary>
    public partial class Pedido : IPagina
    {
        private static readonly CardapioControl CardControl = new CardapioControl();

        private readonly List<int> _excluidos = new List<int>();
        private bool _ativaOpcao;
        public int Cont = 0;

        private List<ECARDAPIOITEM> _itensCardapio;
        private List<ECARDAPIOITEM> _itensCat1;
        private List<ECARDAPIOITEM> _itensCat2;
        private List<EOBSERVACAO> _itensObs;
        private int? _nuitem;
        private int? _pai;
        public List<ACONTITEM> Pedidos = new List<ACONTITEM>();
        private int _pgBar;
        private int _pgCat1;
        private int _pgCat2;
        private int _pgCozinha;
        private int _pgObs;

        public int QtdPessoas = 1;


        public Pedido()
        {
            InitializeComponent();

            _itensCardapio = CardControl.BuscarAtual().ECARDAPIOITEM.OrderBy(r => r.posicao).ToList();


            btAdd.Visibility = Visibility.Hidden;
        }

        #region Events Handlers Botoes Mudanca Pagina

        protected int PegarTotalPaginas(int totalItens, int tamanhoPagina)
        {
            int r = totalItens%tamanhoPagina;

            if (r != 0)
            {
                r = Convert.ToInt32(totalItens/tamanhoPagina) + 1;
            }
            else
            {
                r = totalItens/tamanhoPagina;
            }

            return r;
        }

        protected void MudarPagina(ItemsControl grid, IEnumerable<ECARDAPIOITEM> lista, int tamanhoPagina,
                                   ref int pagina, AvancaRecua direcao, RepeatButton btDireita, RepeatButton btEsquerda)
        {
            int totalItens = lista.Count();
            int totalPaginas = PegarTotalPaginas(totalItens, tamanhoPagina);

            switch (direcao)
            {
                case AvancaRecua.Avancar:
                    pagina++;
                    break;
                case AvancaRecua.Recuar:
                    pagina--;
                    break;
                case AvancaRecua.Carregar:
                    pagina = 0;
                    break;
            }

            grid.ItemsSource = lista.OrderBy(p => p.posicao).Skip(tamanhoPagina*pagina).Take(tamanhoPagina);


            btEsquerda.IsEnabled = pagina > 0;

            btDireita.IsEnabled = pagina < (totalPaginas - 1);
        }

        protected void MudarPagina(ItemsControl grid, IEnumerable<EOBSERVACAO> lista, int tamanhoPagina,
                                   ref int pagina, AvancaRecua direcao, RepeatButton btDireita, RepeatButton btEsquerda)
        {
            int totalItens = lista.Count();
            int totalPaginas = PegarTotalPaginas(totalItens, tamanhoPagina);

            switch (direcao)
            {
                case AvancaRecua.Avancar:
                    pagina++;
                    break;
                case AvancaRecua.Recuar:
                    pagina--;
                    break;
                case AvancaRecua.Carregar:
                    pagina = 0;
                    break;
            }

            grid.ItemsSource = lista.OrderBy(p => p.descricao).Skip(tamanhoPagina*pagina).Take(tamanhoPagina);


            btEsquerda.IsEnabled = pagina > 0;

            btDireita.IsEnabled = pagina < (totalPaginas - 1);
        }


        protected void lbtGrupo1_Click(object sender, RoutedEventArgs e)
        {
            MudarPagina(grid_cozinha,
                        _itensCardapio.Where(r => r.grupo == 1 && (r.idItemPai == 0 || r.idItemPai == null)), 12,
                        ref _pgCozinha, AvancaRecua.Recuar, rbtGrupo1, lbtGrupo1);
        }

        protected void rbtGrupo1_Click(object sender, RoutedEventArgs e)
        {
            MudarPagina(grid_cozinha,
                        _itensCardapio.Where(r => r.grupo == 1 && (r.idItemPai == 0 || r.idItemPai == null)), 12,
                        ref _pgCozinha, AvancaRecua.Avancar, rbtGrupo1, lbtGrupo1);
        }

        protected void lbtGrupo2_Click(object sender, RoutedEventArgs e)
        {
            MudarPagina(grid_bar, _itensCardapio.Where(r => r.grupo == 2 && (r.idItemPai == 0 || r.idItemPai == null)),
                        12, ref _pgBar, AvancaRecua.Recuar, rbtGrupo2, lbtGrupo2);
        }

        protected void rbtGrupo2_Click(object sender, RoutedEventArgs e)
        {
            MudarPagina(grid_bar, _itensCardapio.Where(r => r.grupo == 2 && (r.idItemPai == 0 || r.idItemPai == null)),
                        12, ref _pgBar, AvancaRecua.Avancar, rbtGrupo2, lbtGrupo2);
        }

        protected void ubtCat1_Click(object sender, RoutedEventArgs e)
        {
            MudarPagina(grid1, _itensCat1, 12, ref _pgCat1, AvancaRecua.Recuar, dbtCat1, ubtCat1);
        }

        protected void dbtCat1_Click(object sender, RoutedEventArgs e)
        {
            MudarPagina(grid1, _itensCat1, 12, ref _pgCat1, AvancaRecua.Avancar, dbtCat1, ubtCat1);
        }

        protected void ubtCat2_Click(object sender, RoutedEventArgs e)
        {
            MudarPagina(grid2, _itensCat2, 9, ref _pgCat2, AvancaRecua.Recuar, dbtCat2, ubtCat2);
        }

        protected void dbtCat2_Click(object sender, RoutedEventArgs e)
        {
            MudarPagina(grid2, _itensCat2, 9, ref _pgCat2, AvancaRecua.Avancar, dbtCat2, ubtCat2);
        }

        protected void ubtObs_Click(object sender, RoutedEventArgs e)
        {
            MudarPagina(grid3, _itensObs, 6, ref _pgObs, AvancaRecua.Recuar, dbtObs, ubtObs);
            CarregaObservacoes();
        }

        protected void dbtObs_Click(object sender, RoutedEventArgs e)
        {
            MudarPagina(grid3, _itensObs, 6, ref _pgObs, AvancaRecua.Avancar, dbtObs, ubtObs);
            CarregaObservacoes();
        }

        protected enum AvancaRecua
        {
            Avancar,
            Recuar,
            Carregar
        }

        #endregion

        #region Função de carregamento de produtos

        private void CarregaComidaBebida()
        {
            MudarPagina(grid_cozinha,
                        _itensCardapio.Where(r => r.grupo == 1 && (r.idItemPai == 0 || r.idItemPai == null)), 12,
                        ref _pgCozinha, AvancaRecua.Carregar, rbtGrupo1, lbtGrupo1);

            MudarPagina(grid_bar, _itensCardapio.Where(r => r.grupo == 2 && (r.idItemPai == 0 || r.idItemPai == null)),
                        12, ref _pgBar, AvancaRecua.Carregar, rbtGrupo2, lbtGrupo2);
        }

        private void CarregarCat1(int idItemCard)
        {
            _itensCat1 = _itensCardapio.Where(r => r.idItemPai == idItemCard).ToList();

            MudarPagina(grid1, _itensCat1, 12, ref _pgCat1, AvancaRecua.Carregar, dbtCat1, ubtCat1);
        }

        private void CarregarCat2(int idItemCard)
        {
            _itensCat2 = _itensCardapio.Where(r => r.idItemPai == idItemCard).ToList();

            MudarPagina(grid2, _itensCat2, 9, ref _pgCat2, AvancaRecua.Carregar, dbtCat2, ubtCat2);
        }

        private void CarregaAdicionais()
        {
            //IQueryable<EPRODUTO> lista = (from p in Contexto.Atual.EPRODUTO
            //                                 where (localPreco == 1 && p.idProduto == Cat1) ||
            //                                        (localPreco == 2 && p.idProduto == Cat2) ||
            //                                        (localPreco == 3 && p.idProduto == Cat3)
            //                                 select p);

            //var l = from a in lista.First().EPRODADD
            //        select new
            //        {
            //            nome = a.EPRODUTO1.nomeResumo != "" ? a.EPRODUTO1.nomeResumo : a.EPRODUTO1.nome,
            //            id = a.idPrdAdd + "@0@" + a.nuPreco + "@" + a.EPRODUTO1.nome,
            //            cor = a.EPRODUTO1.corPDV == "" || a.EPRODUTO1.corPDV == null ? "#B68944" : "#" + a.EPRODUTO1.corPDV
            //        };


            //grid2.ItemsSource = l;
            //VerticalScroller2.ScrollToTop();
        }

        private void CarregaObservacoes(int idProduto)
        {
            _itensObs = (from p in Contexto.Atual.EOBSERVACAO
                        where p.EPRODUTO.Any(r => r.idProduto == idProduto)
                        select p).ToList();

            if (_itensObs.Any())
            {
                MudarPagina(grid3, _itensObs, 6, ref _pgObs, AvancaRecua.Carregar, dbtObs, ubtObs);
            }
            else
            {
                grid3.ItemsSource = null;
            }

            ACONTITEM c = Pedidos.Single(r => r.nuItem == _nuitem);

            foreach (object item in grid3.Items)
            {
                var tb = FindItemControl(grid3, "btObs", item) as ToggleButton;


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

        private void CarregaObservacoes()
        {
            ACONTITEM c = Pedidos.Single(r => r.nuItem == _nuitem);

            foreach (object item in grid3.Items)
            {
                var tb = FindItemControl(grid3, "btObs", item) as ToggleButton;


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

                    ACONTITEM c = Pedidos.Single(r => r.nuItem == t);

                    _nuitem = t;

                    CarregaObservacoes(c.idProduto);
                }
            }
        }

        private void gridDados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            foreach (object it in gridDados.SelectedItems)
            {
                //pega o numero do item do objeto anonimo
                var valor = TypeDescriptor.GetProperties(it)[0].GetValue(it);
                if (valor != null)
                {
                    var t = (int) valor;

                    ACONTITEM c = Pedidos.SingleOrDefault(r => r.nuItem == t);

                    if (c != null)
                    {
                        if (c.nuItemPai != null && c.nuItemPai != 0)
                        {
                            var itemm = Pedidos.SingleOrDefault(p => p.nuItem == c.nuItemPai);
                            if (itemm != null)
                            {
                                Decimal preco = itemm.preco;
                                itemm.preco = preco + c.preco;
                            }
                        }

                        Pedidos.Remove(c);

                        var removidos = Pedidos.Where(r => r.nuItemPai == t).Select(itt => itt.nuItem).ToList();

                        //Cancela adicionais do item pai

                        foreach (int ii in removidos)
                        {
                            ACONTITEM itt = Pedidos.SingleOrDefault(p => p.nuItem == ii);
                            Pedidos.Remove(itt);
                        }
                    }
                }
            }

            _excluidos.Clear();
            populaGrid();
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            var bb = (ToggleButton) sender;

            if (bb.IsChecked == true)
            {
                CarregaAdicionais();
            }
            else
            {
                //CarregaCat2();
            }
        }

        private void Quantidade_Click(object sender, RoutedEventArgs e)
        {
            if (!gridDados.SelectedItems.Any())
            {
                RadWindow.Alert("Selecione ao menos um item");
            }
            else
            {
                var q = new Quantidade();

                foreach (object it in gridDados.SelectedItems)
                {
                    //pega o numero do item do objeto anonimo
                    var valor = TypeDescriptor.GetProperties(it)[0].GetValue(it);
                    if (valor != null)
                    {
                        var t = (int) valor;

                        ACONTITEM c = Pedidos.Single(r => r.nuItem == t);

                        q.Items.Add(c);
                    }
                    q.PaginaPai = this;
                }

                WindowUtil.MostraModal();
                q.ShowDialog();
                WindowUtil.FechaModal();

                populaGrid();
            }
        }

        #endregion

        #region Funções de clique de botões de produtos

        private void Escolhe_Grupo(object sender, RoutedEventArgs e)
        {
            Escolhe_Obs(1);

            var bb = (Button) sender;
            var item = bb.DataContext as ECARDAPIOITEM;

            if (item != null && (item.usaPreco == true && item.idProduto != null && item.idProduto != 0))
            {
                var i = new ACONTITEM();
                Cont++;
                i.nuItem = Cont;
                if (item.idProduto != null) i.idProduto = item.idProduto.Value;
                i.idVen = Convert.ToInt32(Memoria.Vendedor);
                i.preco = BuscarPreco(item);
                i.txtObs = null;
                i.quantidade = 1;
                i.adicional = false;
                i.opcao = false;
                i.nuItemPai = null;

                Pedidos.Add(i);
                populaGrid();

                //btAdd.Visibility = Visibility.Visible;

                _ativaOpcao = true;

                _pai = i.nuItem;
            }
            else
            {
                _pai = null;
                _ativaOpcao = false;

                btAdd.Visibility = Visibility.Hidden;
            }

            grid1.ItemsSource = null;
            ubtCat1.IsEnabled = false;
            dbtCat1.IsEnabled = false;

            grid2.ItemsSource = null;
            ubtCat2.IsEnabled = false;
            dbtCat2.IsEnabled = false;

            btAdd.IsChecked = false;

            if (item != null) CarregarCat1(item.idItemCard);
        }

        private void Escolhe_Cat1(object sender, RoutedEventArgs e)
        {
            Escolhe_Obs(1);

            var bb = (Button) sender;
            var item = bb.DataContext as ECARDAPIOITEM;

            ECARDAPIOITEM itemPai = _itensCardapio.FirstOrDefault(r => item != null && r.idItemCard == item.idItemPai);

            if (itemPai != null && (itemPai.usaPreco == true && itemPai.EPRODUTO != null))
            {
                _ativaOpcao = true;
            }
            else
            {
                _ativaOpcao = false;
                _pai = null;
            }

            if (item != null && (item.usaPreco == true && item.EPRODUTO != null))
            {
                var i = new ACONTITEM();
                Cont++;
                i.nuItem = Cont;
                if (item.idProduto != null) i.idProduto = item.idProduto.Value;
                i.idVen = Convert.ToInt32(Memoria.Vendedor);
                i.preco = BuscarPreco(item);
                i.txtObs = null;
                i.quantidade = 1;
                i.adicional = false;
                i.opcao = _ativaOpcao;
                i.nuItemPai = _pai;

                if (_ativaOpcao)
                {
                    i.preco = 0.01M;
                    var itemm = Pedidos.SingleOrDefault(p => p.nuItem == i.nuItemPai);
                    if (itemm != null)
                    {
                        Decimal preco = itemm.preco;
                        itemm.preco = preco - i.preco;
                    }
                }

                Pedidos.Add(i);
                populaGrid();

                //btAdd.Visibility = Visibility.Visible;

                _ativaOpcao = true;

                _pai = i.nuItem;
            }
            else
            {
                btAdd.Visibility = Visibility.Hidden;

                _ativaOpcao = false;

                _pai = null;
            }

            grid2.ItemsSource = null;

            btAdd.IsChecked = false;

            if (item != null) CarregarCat2(item.idItemCard);
        }

        // Categoria
        private void Escolhe_Cat2(object sender, RoutedEventArgs e)
        {
            Escolhe_Obs(1);

            var bb = (Button) sender;
            var item = bb.DataContext as ECARDAPIOITEM;

            if (item != null && (item.usaPreco == true && item.EPRODUTO != null))
            {
                var i = new ACONTITEM();
                Cont++;
                i.nuItem = Cont;
                if (item.idProduto != null) i.idProduto = item.idProduto.Value;
                i.idVen = Convert.ToInt32(Memoria.Vendedor);
                i.preco = BuscarPreco(item);
                i.txtObs = null;
                i.quantidade = 1;
                i.adicional = false;
                i.opcao = _ativaOpcao;
                i.nuItemPai = _pai;

                if (_ativaOpcao)
                {
                    i.preco = 0.01M;
                    var itemm = Pedidos.SingleOrDefault(p => p.nuItem == i.nuItemPai);
                    if (itemm != null)
                    {
                        Decimal preco = itemm.preco;
                        itemm.preco = preco - i.preco;
                    }
                }

                Pedidos.Add(i);
                populaGrid();

                //btAdd.Visibility = Visibility.Visible;
            }
            else
            {
                btAdd.Visibility = Visibility.Hidden;
            }
        }

        // Adicional
/*
        private void Escolhe_Adicional(String[] valores)
        {
            // id = valores[0]
            // locapreco = valores[1]
            // nupreco = valores[2]
            // nome = valores[3]


            var i = new ACONTITEM();
            Cont++;
            i.nuItem = Cont;
            i.idProduto = Convert.ToInt32(valores[0]);
            i.idVen = Convert.ToInt32(Memoria.Vendedor);
            //i.preco = BuscarPreco(valores);
            i.txtObs = null;
            i.adicional = true;
            i.quantidade = 1;
            i.opcao = false;
            i.nuItemPai = _pai;

            Pedidos.Add(i);
        }
*/

        // Observação
        private void Escolhe_Obs(int v)
        {
            if (_nuitem != null && _nuitem != 0)
            {
                ACONTITEM c = Pedidos.SingleOrDefault(p => p.nuItem == _nuitem);

                if (c != null)
                {
                    c.txtObs = txt_obs.Text;
                }
            }

            if (v != 3)
            {
                grid3.ItemsSource = null;
                txt_obs.Text = "";
                _nuitem = null;

                if (v != 0)
                {
                    gridDados.UnselectAll();
                }
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

            return null;
        }

        // Função que busca o preço
        private decimal BuscarPreco(ECARDAPIOITEM item)
        {
            switch (item.nuPreco)
            {
                case 1:
                    var tbPreco = item.EPRODUTO.ETABPRECO.FirstOrDefault();
                    if (tbPreco != null)
                        return tbPreco.preco1;
                    else
                        return 0;
                case 2:
                    var tbPreco2 = item.EPRODUTO.ETABPRECO.FirstOrDefault();
                    if (tbPreco2 != null)
                        return tbPreco2.preco2;
                    else
                        return 0;
                case 3:
                    var tbPreco3 = item.EPRODUTO.ETABPRECO.FirstOrDefault();
                    if (tbPreco3 != null)
                        return tbPreco3.preco3;
                    else
                        return 0;
                default:
                    return 0;
            }
        }

        // Função para popular grid
        public void populaGrid()
        {
            var l = from i in Pedidos
                    join e in Memoria.Produtos on i.idProduto equals e.idProduto
                    select new
                               {
                                   i.nuItem,
                                   i.preco,
                                   precoView = string.Format("{0:c}", i.preco*i.quantidade),
                                   nome =
                        i.adicional ? "..Adicional : " + e.nome : i.opcao ? "....Opção : " + e.nome : e.nome,
                                   qtd = i.quantidade,
                                   und = e.undControle,
                                   tab = i.adicional ? 0 : i.opcao ? 0 : 1
                               };

            gridDados.ItemsSource = l;
            gridDados.UnselectAll();

            GridViewScrollViewer scrollViewer = gridDados.ChildrenOfType<GridViewScrollViewer>().First();
            scrollViewer.ScrollToBottom();

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
                    t.Content = l.Sum(b => b.preco*b.qtd);
                }
            }
        }

        #endregion

        #region Ações Gerais

        // Volta para a página inicial
        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            ControlePagina.NavigateTo(PaginaCore.PgInicial);
        }

        // Exclui um item da lista
        private void Exluir_Click(object sender, RoutedEventArgs e)
        {
            foreach (int t in _excluidos)
            {
                ACONTITEM c = Pedidos.SingleOrDefault(r => r.nuItem == t);

                if (c != null)
                {
                    if (c.nuItemPai != null && c.nuItemPai != 0)
                    {
                        ACONTITEM item = Pedidos.SingleOrDefault(p => p.nuItem == c.nuItemPai);
                        if (item != null)
                        {
                            Decimal preco = item.preco;
                            var pedido = Pedidos.SingleOrDefault(p => p.nuItem == c.nuItemPai);
                            if (pedido != null)
                                pedido.preco = preco + c.preco;
                        }
                    }

                    Pedidos.Remove(c);
                    //Contexto.Atual.Detach(c);

                    var removidos = Pedidos.Where(r => r.nuItemPai == t).Select(itt => itt.nuItem).ToList();

                    //Cancela adicionais do item pai

                    //foreach (var obj in c.EOBSERVACAO)
                    //{
                    //    Contexto.Atual.Detach(obj);
                    //}

                    foreach (int ii in removidos)
                    {
                        ACONTITEM itt = Pedidos.SingleOrDefault(p => p.nuItem == ii);
                        Pedidos.Remove(itt);
                        //Contexto.Atual.Detach(itt);
                    }
                }
            }

            _excluidos.Clear();
            populaGrid();
        }

        // Finaliza o pedido
        private void Finalizar_Click(object sender, RoutedEventArgs e)
        {
            if (Finalizar.IsEnabled)
            {
                Escolhe_Obs(3);

                var mesa = new GMESA();
                var cmesa = new MesaControl();
                mesa.nuMesa = Convert.ToInt32(Memoria.Mesa);

                var cconta = new PreContaControl();
                ACONTA conta = cconta.BuscarPorMesa(mesa.nuMesa);

                Funcoes funcao;

                // Se estiver livre, tem mudar o status e criar a conta
                if (conta == null)
                {
                    funcao = Funcoes.Adicionar;

                    conta = new ACONTA {nuMesa = mesa.nuMesa, pessoas = QtdPessoas};

                    foreach (ACONTITEM pedido in Pedidos)
                    {
                        List<EOBSERVACAO> listaObs = pedido.EOBSERVACAO.ToList();

                        pedido.EOBSERVACAO.Clear();

                        foreach (EOBSERVACAO obs in listaObs)
                        {
                            pedido.EOBSERVACAO.Add(Contexto.Atual.EOBSERVACAO.FirstOrDefault(r => r.idObs == obs.idObs));
                        }

                        if (pedido.quantidade == 0)
                            pedido.quantidade = 1;


                        pedido.desconto = 0;
                        conta.ACONTITEM.Add(pedido);
                    }
                }
                else
                {
                    funcao = Funcoes.Atualizar;

                    int cont = 1;

                    if (conta.ACONTITEM.Count > 0)
                    {
                        cont = conta.ACONTITEM.OrderBy(r => r.nuItem).Last().nuItem;
                    }

                    foreach (ACONTITEM pedido in Pedidos.Where(r => r.nuItemPai == null).OrderBy(s => s.nuItem))
                    {
                        cont++;

                        List<EOBSERVACAO> listaObs = pedido.EOBSERVACAO.ToList();

                        pedido.EOBSERVACAO.Clear();

                        foreach (EOBSERVACAO obs in listaObs)
                        {
                            pedido.EOBSERVACAO.Add(Contexto.Atual.EOBSERVACAO.FirstOrDefault(r => r.idObs == obs.idObs));
                        }

                        pedido.idStatus = 1;
                        int antigopai = pedido.nuItem;
                        pedido.nuItem = cont;

                        if (pedido.quantidade == 0)
                            pedido.quantidade = 1;

                        pedido.desconto = 0;
                        pedido.dataInclusao = DateTime.Now;
                        conta.ACONTITEM.Add(pedido);

                        //Gravando o Log de adição
                        var lo = new LogDAL();

                        Memoria.LogConta = conta.idConta;
                        Memoria.LogMesa = conta.nuMesa;
                        Memoria.LogAcao = "Novo item adicionado: " + pedido.idProduto;
                        lo.Criar();

                        // Atualizando os filhos com o novo numero do pai
                        foreach (ACONTITEM itt in Pedidos.Where(r => r.nuItemPai == antigopai).OrderBy(s => s.nuItem))
                        {
                            cont++;

                            var ac = new ACONTITEM
                                         {
                                             adicional = itt.adicional,
                                             idConta = itt.idConta,
                                             idEmpresa = pedido.idEmpresa,
                                             idFilial = pedido.idFilial,
                                             idProduto = itt.idProduto,
                                             idVen = itt.idVen,
                                             impresso = itt.impresso,
                                             opcao = itt.opcao,
                                             preco = itt.preco,
                                             produzido = itt.produzido,
                                             txtObs = itt.txtObs
                                         };

                            foreach (EOBSERVACAO z in itt.EOBSERVACAO)
                            {
                                Contexto.Atual.AttachTo("EOBSERVACAO", z);
                                ac.EOBSERVACAO.Add(z);
                                //ac.EOBSERVACAO.Add(Contexto.Atual.EOBSERVACAO.FirstOrDefault(r => r.idObs == z.idObs));
                            }

                            ac.idStatus = 1;
                            ac.nuItem = cont;
                            ac.nuItemPai = pedido.nuItem;

                            if (ac.quantidade == 0)
                                ac.quantidade = 1;

                            ac.desconto = 0;
                            ac.dataInclusao = DateTime.Now;
                            conta.ACONTITEM.Add(ac);

                            //Granvando Log
                            lo = new LogDAL();

                            Memoria.LogConta = conta.idConta;
                            Memoria.LogMesa = conta.nuMesa;
                            Memoria.LogAcao = "Novo item adicionado: " + pedido.idProduto;
                            lo.Criar();
                        }
                    }
                }

                Memoria.LogAcao = "Adicionar itens com sucesso";


                bool conf = funcao == Funcoes.Adicionar ? cconta.Criar(conta) : cconta.Atualizar(conta);

                if (conf)
                {
                    mesa = conta.GMESA;
                    mesa.idStatus = (int) StatusMesa.Ocupada;

                    bool confmesa = cmesa.AtualizarStatus(mesa);

                    if (confmesa)
                    {
                        Memoria.Mesa = "";
                        ControlePagina.NavigateTo(PaginaCore.PgInicial);

                        Finalizar.IsEnabled = false;
                    }
                    else
                    {
                        RadWindow.Alert("Erro ao enviar pedido(s).");
                    }
                }
                else
                {
                    RadWindow.Alert("Erro ao enviar pedido(s).");
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

        public void Carregar()
        {
            _itensCardapio = CardControl.BuscarAtual().ECARDAPIOITEM.OrderBy(r => r.posicao).ToList();

            Cabecalho.Titulo = "Atendimento \\ Mesa " + Memoria.Mesa;

            Finalizar.IsEnabled = true;

            Pedidos.Clear();
            Cont = 0;
            _excluidos.Clear();


            grid1.ItemsSource = null;
            ubtCat1.IsEnabled = false;
            dbtCat1.IsEnabled = false;

            grid2.ItemsSource = null;
            ubtCat2.IsEnabled = false;
            dbtCat2.IsEnabled = false;
            grid3.ItemsSource = null;

            _pgCozinha = 0;
            _pgBar = 0;
            _pgCat1 = 0;
            _pgCat2 = 0;

            CarregaComidaBebida();

            btAdd.Visibility = Visibility.Hidden;

            gridDados.ItemsSource = Pedidos;
            gridDados.Rebind();

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
                    p.Content = "";
                }
                if (c != null)
                {
                    c.Content = "";
                }
                if (t != null)
                {
                    t.Content = "";
                }
            }
        }

        #endregion

        private void btObs_Click(object sender, RoutedEventArgs e)
        {
            var bt = (ToggleButton) sender;

            var obs = bt.DataContext as EOBSERVACAO;

            ACONTITEM c = Pedidos.SingleOrDefault(p => p.nuItem == _nuitem);

            if (bt.IsChecked == true)
            {
                if (c != null && c.EOBSERVACAO.All(r => r != obs))
                {
                    c.EOBSERVACAO.Add(obs);
                }
            }
            else
            {
                if (c != null && c.EOBSERVACAO.Any(r => r == obs))
                {
                    c.EOBSERVACAO.Remove(obs);
                }
            }
        }

        private void txt_OBS_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (gridDados.SelectedItems.Count == 0)
            {
                RadWindow.Alert("Selecione um Item. ");
            }
        }
    }
}