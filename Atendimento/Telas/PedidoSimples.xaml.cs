using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.Modelo.Enum;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Estoque;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Label = System.Windows.Controls.Label;

namespace Artebit.Restaurante.AtendimentoPDV.Telas
{
    /// <summary>
    /// Interaction logic for Pedido.xaml
    /// </summary>
    public partial class PedidoSimples
    {
/*
        private static ECARDAPIOITEM item = new ECARDAPIOITEM();
*/
        private static readonly CardapioItemControl cardControl = new CardapioItemControl();
        private static readonly CardapioControl Control = new CardapioControl();

        private readonly List<int> excluidos = new List<int>();
        private readonly List<ACONTITEM> pedidos = new List<ACONTITEM>();
        private int Cat1;
        private int Cat2 = 0;
        private int cont;
        private int contBar = 0;
        private int contCozinha = 0;

        private IQueryable<ECARDAPIOITEM> itensCardapio;
        private int localPreco;
        private int? nuitem;
        private int pai;
        private int? produto;
        public int qtdPessoas = 1;

        public PedidoSimples()
        {
            InitializeComponent();

            if (Memoria.Codusuario != null)
            {
                itensCardapio = Control.BuscarAtual().ECARDAPIOITEM.AsQueryable();

                CarregaComidaBebida();
            }
        }

        #region Função de carregamento de produtos

        // ====================================================
        // Função de carregamento de produtos
        private void CarregaComidaBebida()
        {
            //var lista = from a in itensCardapio.ToList()
            //            join e in Memoria.Produtos on a.idPrdCat1 equals e.idProduto
            //            where a.idPrdCat2 == null && a.idPrdCat3 == null
            //            orderby e.ordemPDV,e.nomeResumo
            //            select new
            //            {
            //                id = Convert.ToString(a.idPrdCat1) + "@" + Convert.ToString(a.localPreco) + "@" + Convert.ToString(a.nuPreco) + "@" + e.nome,
            //                nome = e.nomeResumo != "" ? e.nomeResumo : e.nome,
            //                grupo = a.grupo,
            //                cor = e.corPDV == "" || e.corPDV == null ? "#B68944" : "#" + e.corPDV,
            //                ordemPDV = e.ordemPDV ?? 999
            //            };

            // grid_cozinha.ItemsSource = lista.Where(r => r.grupo == 1).Distinct().OrderBy(r => r.ordemPDV).ThenBy(r => r.nome);// Comidas
            //contCozinha = lista.Where(r => r.grupo == 1).Distinct().Count();
            //grid_bar.ItemsSource = lista.Where(r => r.grupo == 2).Distinct().OrderBy(r => r.ordemPDV).ThenBy(r => r.nome);// Bebidas
            //contBar = lista.Where(r => r.grupo == 2).Distinct().Count();
        }

        private void CarregaObservacoes()
        {
            IQueryable<EPRODUTO> lista = (from p in Contexto.Atual.EPRODUTO
                                          where (p.idProduto == produto)
                                          select p);

            if (lista.Count() > 0)
            {
                grid3.ItemsSource = lista.First().EOBSERVACAO;
            }
            else
            {
                grid3.ItemsSource = null;
            }

            VerticalScroller3.ScrollToTop();


            ACONTITEM c = pedidos.Single(r => r.idProduto == produto && r.nuItem == nuitem);

            foreach (object item in grid3.Items)
            {
                var tb = FindItemControl(grid3, "btObs", item) as ToggleButton;


                int id = Convert.ToInt32(tb.CommandParameter);

                if (c.EOBSERVACAO.Any(o => o.idObs == id))
                {
                    tb.IsChecked = true;
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
                var t = (int) TypeDescriptor.GetProperties(it)[0].GetValue(it);

                ACONTITEM c = pedidos.Single(r => r.nuItem == t);

                produto = Convert.ToInt32(c.idProduto);
                nuitem = t;

                CarregaObservacoes();
            }
        }

        // Função de duplo clique no item da grid, exclui
        private void gridDados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            foreach (object it in gridDados.SelectedItems)
            {
                //pega o numero do item do objeto anonimo
                var t = (int) TypeDescriptor.GetProperties(it)[0].GetValue(it);

                ACONTITEM c = pedidos.SingleOrDefault(r => r.nuItem == t);

                if (c != null)
                {
                    if (c.nuItemPai != null && c.nuItemPai != 0)
                    {
                        Decimal preco = pedidos.SingleOrDefault(p => p.nuItem == c.nuItemPai).preco;
                        pedidos.SingleOrDefault(p => p.nuItem == c.nuItemPai).preco = preco + c.preco;
                    }

                    pedidos.Remove(c);

                    var removidos = new List<int>();

                    //Cancela adicionais do item pai
                    foreach (ACONTITEM itt in pedidos.Where(r => r.nuItemPai == t))
                    {
                        // Não pode ser removido aqui pois da erro
                        removidos.Add(itt.nuItem);
                    }

                    foreach (int ii in removidos)
                    {
                        ACONTITEM itt = pedidos.SingleOrDefault(p => p.nuItem == ii);
                        pedidos.Remove(itt);
                    }
                }
            }

            excluidos.Clear();
            populaGrid();
        }

        #endregion

        #region Quadros Horizontais

        // ================================================================
        // Lógica para funcionamento dos quadros horizontais

        private RepeatButton LeftButton;
        private RepeatButton RightButton;
        private ScrollBar _horizontalScrollBar;
        private RepeatButton _leftButton;
        private RepeatButton _rightButton;

        private void HorizontalScrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;

            _horizontalScrollBar = scrollViewer.Template.FindName("PART_HorizontalScrollBar", scrollViewer) as ScrollBar;
            _leftButton =
                _horizontalScrollBar.Template.FindName("PART_LeftButton", _horizontalScrollBar) as RepeatButton;
            _rightButton =
                _horizontalScrollBar.Template.FindName("PART_RightButton", _horizontalScrollBar) as RepeatButton;

            atualizarHorizontalVariavel(scrollViewer);

            UpdateHorizontalScrollBarButtons(sender);
        }

        private void HorizontalScrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;

            atualizarHorizontalVariavel(scrollViewer);

            UpdateHorizontalScrollBarButtons(sender);
        }

        private void HorizontalScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;

            atualizarHorizontalVariavel(scrollViewer);

            UpdateHorizontalScrollBarButtons(sender);
        }

        private void atualizarHorizontalVariavel(ScrollViewer scroll)
        {
            _horizontalScrollBar = scroll.Template.FindName("PART_HorizontalScrollBar", scroll) as ScrollBar;
            _leftButton =
                _horizontalScrollBar.Template.FindName("PART_LeftButton", _horizontalScrollBar) as RepeatButton;
            _rightButton =
                _horizontalScrollBar.Template.FindName("PART_RightButton", _horizontalScrollBar) as RepeatButton;

            String nome = scroll.Name;
            nome = nome.Replace("HorizontalScroller", "");
            LeftButton = FindName("LeftButton" + nome) as RepeatButton;
            RightButton = FindName("RightButton" + nome) as RepeatButton;
        }

        private void UpdateHorizontalScrollBarButtons(object sender)
        {
            var VerticalScroller = (ScrollViewer) sender;

            // at startup, _horizontalScrollBar can be null
            if (_horizontalScrollBar == null)
                return;

            double desiredPanelWidth = 0;
            String nome = VerticalScroller.Name;
            nome = nome.Replace("HorizontalScroller", "");
            if (nome == "1")
            {
                desiredPanelWidth += grid_cozinha.ActualWidth;
            }
            else
            {
                if (nome == "2")
                {
                    desiredPanelWidth += grid_bar.ActualWidth;
                }
            }

            double availablePanelWidth = VerticalScroller.ActualWidth;

            bool leftButtonVisibility;
            bool rightButtonVisibility;

            if (availablePanelWidth < desiredPanelWidth)
            {
                bool isAtTheFarLeft = false;
                bool isAtTheFarRight = false;

                if (_horizontalScrollBar != null)
                {
                    if (_horizontalScrollBar.Value == _horizontalScrollBar.Maximum)
                        isAtTheFarRight = true;
                    if (_horizontalScrollBar.Value == _horizontalScrollBar.Minimum)
                        isAtTheFarLeft = true;
                }

                if (isAtTheFarLeft)
                    leftButtonVisibility = false;
                else
                    leftButtonVisibility = true;

                if (isAtTheFarRight)
                    rightButtonVisibility = false;
                else
                    rightButtonVisibility = true;
            }
            else
            {
                // scroll bars are not needed

                leftButtonVisibility = false;
                rightButtonVisibility = false;
            }

            LeftButton.IsEnabled = leftButtonVisibility;
            RightButton.IsEnabled = rightButtonVisibility;
        }

        #region Botões de bar e cozinha

        // ===========================================================================
        // Botões para passagem horizontal, comidas e bebidas
        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            var bb = (RepeatButton) sender;

            String nome = bb.Name;
            nome = nome.Replace("LeftButton", "");
            var scroll = FindName("HorizontalScroller" + nome) as ScrollViewer;

            scroll.PageLeft();
        }

        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            var bb = (RepeatButton) sender;

            String nome = bb.Name;
            nome = nome.Replace("RightButton", "");
            var scroll = FindName("HorizontalScroller" + nome) as ScrollViewer;

            scroll.PageRight();
        }

        // ===========================================================================

        #endregion

        #endregion

        #region Quadros Verticais

        // ================================================================
        // Lógica para funcionamento dos quadros verticais

        private RepeatButton DownButton;
        private RepeatButton UpButton;
        private RepeatButton _downButton;
        private RepeatButton _upButton;
        private ScrollBar _verticalScrollBar;

        // Função executada quando a scrollbar é carregada
        private void VerticalScroller_Loaded(object sender, RoutedEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;

            atualizarVerticalVariavel(scrollViewer);

            UpdateVerticalScrollBarButtons(sender);
        }

        // Quando mudar o scrollbar
        private void VerticalScroller_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;

            atualizarVerticalVariavel(scrollViewer);

            UpdateVerticalScrollBarButtons(sender);
        }

        private void VerticalScroller_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;

            atualizarVerticalVariavel(scrollViewer);

            UpdateVerticalScrollBarButtons(sender);
        }

        private void atualizarVerticalVariavel(ScrollViewer scroll)
        {
            // Atualiza as varíaveis com o controle da tela
            _verticalScrollBar = scroll.Template.FindName("PART_VerticalScrollBar", scroll) as ScrollBar;
            _upButton = _verticalScrollBar.Template.FindName("PART_UpButton", _verticalScrollBar) as RepeatButton;
            _downButton = _verticalScrollBar.Template.FindName("PART_DownButton", _verticalScrollBar) as RepeatButton;

            String nome = scroll.Name;
            nome = nome.Replace("VerticalScroller", "");
            UpButton = FindName("UpButton" + nome) as RepeatButton;
            DownButton = FindName("DownButton" + nome) as RepeatButton;
        }

        // Atualiza os botões da scrollbar, quando deve aparecer cada um
        private void UpdateVerticalScrollBarButtons(object sender)
        {
            var VerticalScroller = (ScrollViewer) sender;

            if (_verticalScrollBar == null)
                return;

            // Busca o tamanho atual da scrollbar
            double desiredPanelHeight = 0;
            String nome = VerticalScroller.Name;
            nome = nome.Replace("VerticalScroller", "");
            var ic = VerticalScroller.FindName("grid" + nome) as ItemsControl;
            desiredPanelHeight += ic.ActualHeight;


            // busca o tamanho da scroll acrescentando os botões 
            double availablePanelHeight = VerticalScroller.ActualHeight;


            // Dependendo da posição da scroll vai esconder a posição do botão de subir ou de descer
            bool upButtonVisibility;
            bool downButtonVisibility;

            // Se o tamanho atual da scroll for menor que o tamanho disponivel 
            if (availablePanelHeight < desiredPanelHeight)
            {
                // By comparing availablePanelHeight and desiredPanelHeight,
                // we now know that scroll bar buttons are needed due to space
                // constraints.  However, we still might choose to hide a scroll bar
                // button based on the current scrollbar position.  If the position
                // is at the top, hide the Up button, and if the position is at the
                // bottom, hide the Down button.

                bool isAtTheTop = false;
                bool isAtTheBottom = false;

                if (_verticalScrollBar != null)
                {
                    if (_verticalScrollBar.Value == _verticalScrollBar.Maximum)
                        isAtTheBottom = true;
                    if (_verticalScrollBar.Value == _verticalScrollBar.Minimum)
                        isAtTheTop = true;
                }

                if (isAtTheTop)
                    upButtonVisibility = false;
                else
                    upButtonVisibility = true;

                if (isAtTheBottom)
                    downButtonVisibility = false;
                else
                    downButtonVisibility = true;
            }
            else
            {
                // scroll bars are not needed
                upButtonVisibility = false;
                downButtonVisibility = false;
            }

            // Coloca a visibility nos botões
            UpButton.IsEnabled = upButtonVisibility;
            DownButton.IsEnabled = downButtonVisibility;
        }

        #region Botões verticais

        // Clique do botão UP
        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            var bb = (RepeatButton) sender;

            String nome = bb.Name;
            nome = nome.Replace("UpButton", "");
            var scroll = FindName("VerticalScroller" + nome) as ScrollViewer;

            scroll.PageUp();
        }

        // Clique do botão DOWN
        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            var bb = (RepeatButton) sender;

            String nome = bb.Name;
            nome = nome.Replace("DownButton", "");
            var scroll = FindName("VerticalScroller" + nome) as ScrollViewer;

            scroll.PageDown();
        }

        #endregion

        #endregion

        #region Funções de clique de botões de produtos

        // ========================================
        // Função de clique dos produtos

        // Bar e Cozinha
        private void Escolhe_Cat1(object sender, RoutedEventArgs e)
        {
            Escolhe_Obs(1);

            var bb = (Button) sender;
            String[] valores = Convert.ToString(bb.CommandParameter).Split('@');

            // id = valores[0]
            // locapreco = valores[1]
            // nupreco = valores[2]
            // nome = valores[3]

            Cat1 = Convert.ToInt32(valores[0]);

            if (valores[1] == "1")
            {
                busyIndicator.IsBusy = true;

                var i = new ACONTITEM();
                cont++;
                i.nuItem = cont;
                i.idProduto = Convert.ToInt32(valores[0]);
                i.idVen = Convert.ToInt32(Memoria.Vendedor);
                i.preco = BuscarPreco(valores);
                i.txtObs = null;
                i.adicional = false;
                i.opcao = false;
                i.nuItemPai = null;

                pedidos.Add(i);
                populaGrid();

                pai = cont;
                localPreco = Convert.ToInt32(valores[1]);
            }
            else
            {
                localPreco = 0;
            }

            busyIndicator.IsBusy = false;
        }

        // Observação
        private void Escolhe_Obs(int v)
        {
            if (produto != null && produto != 0 && nuitem != null && nuitem != 0)
            {
                ACONTITEM c = pedidos.SingleOrDefault(p => p.idProduto == produto && p.nuItem == nuitem);

                if (c != null)
                {
                    c.EOBSERVACAO.Clear();

                    foreach (object item in grid3.Items)
                    {
                        var tb = FindItemControl(grid3, "btObs", item) as ToggleButton;

                        if (tb.IsChecked == true)
                        {
                            string descricao = tb.Content.ToString();
                            int idObs = Convert.ToInt32(tb.CommandParameter);

                            var cobs = new ObservacaoControl();
                            var obs = new EOBSERVACAO();
                            obs.idObs = idObs;
                            obs.descricao = descricao;
                            obs = cobs.Buscar(obs);

                            c.EOBSERVACAO.Add(obs);
                        }
                    }

                    c.txtObs = txt_obs.Text;
                }
            }

            grid3.ItemsSource = null;
            txt_obs.Text = "";
            produto = null;
            nuitem = null;

            if (v != 0)
            {
                gridDados.UnselectAll();
            }
        }


        private object FindItemControl(ItemsControl itemsControl, string controlName, object item)
        {
            var container = itemsControl.ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;
            container.ApplyTemplate();
            return container.ContentTemplate.FindName(controlName, container);
        }

        // Função que busca o preço
        private decimal BuscarPreco(string[] valores)
        {
            var item1 = new ECARDAPIOITEM();

            if (Convert.ToInt32(valores[1]) > 0)
            {
                //item1.idPrdCat1 = Cat1;

                if (Convert.ToInt32(valores[1]) > 1)
                {
                    //item1.idPrdCat2 = Cat2;
                }
            }

            //item1.localPreco = Convert.ToInt32(valores[1])
            item1.nuPreco = Convert.ToInt32(valores[2]);
            item1.idEmpresa = Convert.ToInt32(Memoria.Empresa);
            item1.idFilial = Convert.ToInt32(Memoria.Filial);

            return cardControl.BuscarPrecoItem(item1);
        }

        // Função para popular grid
        private void populaGrid()
        {
            var l = from i in pedidos
                    join e in Memoria.Produtos on i.idProduto equals e.idProduto
                    select new
                               {
                                   i.nuItem,
                                   i.preco,
                                   nome =
                        i.adicional ? "..Adicional : " + e.nome : i.opcao ? "....Opção : " + e.nome : e.nome,
                                   qtd = 1,
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

                ch.IsChecked = false;

                if (p != null)
                {
                    p.Content = l.Where(d => d.tab == 1).Count();
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

        private static double GetFontSize(double height, double width, string text, double fontSize)
        {
            double sampleFontSize = fontSize;

            double htRatio, wdRatio, ratio;

            Size textSize = GetSampleSize(text, sampleFontSize);

            double sampleHeight = textSize.Height;

            double sampleWidth = textSize.Width;

            htRatio = height/sampleHeight*0.9;

            wdRatio = width/sampleWidth*0.9;

            ratio = (htRatio < wdRatio) ? htRatio : wdRatio;

            //ratio = wdRatio;

            double final = (sampleFontSize*ratio);

            if (final > 20)
            {
                final = 20;
            }

            return 12;
        }

        private static Size GetSampleSize(string p, double fontSize)
        {
            double sampleFontSize = fontSize;

            String txt = p;

            var myTypeface = new Typeface("Segoe UI");

            var ft = new FormattedText(txt,
                                       CultureInfo.CurrentCulture,
                                       FlowDirection.LeftToRight,
                                       myTypeface, sampleFontSize, Brushes.Black);

            return new Size(ft.Width, ft.Height);
        }

        // Função chamada quando o botão é criado, arruma o tamanho da letra
        private void Button_Loaded(object sender, RoutedEventArgs e)
        {
            var bb = (Button) sender;
            bb.FontSize = GetFontSize(bb.Height, bb.Width - 10, bb.Content.ToString(), 20);
            bb.Foreground = Brushes.Black;
        }

        // Função chamada quando o toggle é criado, arruma o tamanho da letra
        private void toggle_Loaded(object sender, RoutedEventArgs e)
        {
            var bb = (ToggleButton) sender;
            bb.FontSize = GetFontSize(bb.Height, bb.Width, bb.Content.ToString(), 20);
        }

        // Volta para a página inicial
        private void Voltar(object sender, RoutedEventArgs e)
        {
            Memoria.Botao = "Inicial";
            Memoria.Mesa = "";
        }

        // Exclui um item da lista
        private void Exluir_Click(object sender, RoutedEventArgs e)
        {
            foreach (int t in excluidos)
            {
                ACONTITEM c = pedidos.SingleOrDefault(r => r.nuItem == t);

                if (c != null)
                {
                    if (c.nuItemPai != null && c.nuItemPai != 0)
                    {
                        Decimal preco = pedidos.SingleOrDefault(p => p.nuItem == c.nuItemPai).preco;
                        pedidos.SingleOrDefault(p => p.nuItem == c.nuItemPai).preco = preco + c.preco;
                    }

                    pedidos.Remove(c);

                    var removidos = new List<int>();

                    //Cancela adicionais do item pai
                    foreach (ACONTITEM itt in pedidos.Where(r => r.nuItemPai == t))
                    {
                        // Não pode ser removido aqui pois da erro
                        removidos.Add(itt.nuItem);
                    }

                    foreach (int ii in removidos)
                    {
                        ACONTITEM itt = pedidos.SingleOrDefault(p => p.nuItem == ii);
                        pedidos.Remove(itt);
                    }
                }
            }

            excluidos.Clear();
            populaGrid();
        }

        // Finaliza o pedido
        private void Finalizar_Click(object sender, RoutedEventArgs e)
        {
            Contexto.FecharContexto();
            Contexto.AbrirContexto();

            var mesa = new GMESA();
            var cmesa = new MesaControl();
            mesa.nuMesa = Convert.ToInt32(Memoria.Mesa);

            var conta = new ACONTA();
            var cconta = new PreContaControl();

            Funcoes funcao;

            mesa = cmesa.Buscar(mesa);

            conta.nuMesa = mesa.nuMesa;

            // Se estiver livre, tem mudar o status e criar a conta
            if (mesa.idStatus != (int) StatusMesa.Ocupada && mesa.idStatus != (int) StatusMesa.Bloqueada)
            {
                funcao = Funcoes.Adicionar;
                conta.pessoas = qtdPessoas;

                foreach (ACONTITEM pedido in pedidos)
                {
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

                int cont = conta.ACONTITEM.Count();

                foreach (ACONTITEM pedido in pedidos.OrderBy(s => s.nuItem))
                {
                    cont++;

                    pedido.idStatus = 1;
                    int antigopai = pedido.nuItem;
                    pedido.nuItem = cont;

                    // Atualizando os filhos com o novo numero do pai
                    foreach (ACONTITEM itt in pedidos.Where(r => r.nuItemPai == antigopai).OrderBy(s => s.nuItem))
                    {
                        itt.nuItemPai = cont;
                    }

                    pedido.quantidade = 1;
                    pedido.desconto = 0;
                    pedido.dataInclusao = DateTime.Now;
                    conta.ACONTITEM.Add(pedido);
                }
            }

            bool conf = false;

            if (funcao == Funcoes.Adicionar)
            {
                conf = cconta.Criar(conta, "0");
            }
            else
            {
                conf = cconta.Atualizar(conta, "0");
            }

            if (conf)
            {
                if (funcao == Funcoes.Adicionar)
                {
                    mesa.idStatus = (int) StatusMesa.Ocupada;

                    bool confmesa = cmesa.Atualizar(mesa);

                    if (confmesa)
                    {
                        Memoria.Botao = "Inicial";
                        Memoria.Mesa = "";
                        btVoltar.Command.Execute(null);
                    }
                    else
                    {
                        RadWindow.Alert("Erro ao enviar pedido(s).");
                    }
                }
                else
                {
                    Memoria.Botao = "Inicial";
                    Memoria.Mesa = "";
                    btVoltar.Command.Execute(null);
                }
            }
            else
            {
                RadWindow.Alert("Erro ao enviar pedido(s).");
            }
        }

        // Função de clique do checkbox da grid de pedidos
        private void chk_Click(object sender, RoutedEventArgs e)
        {
            var chk = (CheckBox) sender;
            int id = Convert.ToInt32(chk.CommandParameter);

            if (chk.IsChecked == true)
            {
                excluidos.Add(id);
            }
            else
            {
                excluidos.Remove(id);
            }
        }

        // Função para marquar e desmarcar todos
        private void chk_total_Click(object sender, RoutedEventArgs e)
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
                        excluidos.Add(id);
                    }
                    else
                    {
                        i.IsChecked = false;
                        int id = Convert.ToInt32(i.CommandParameter);
                        excluidos.Remove(id);
                    }
                }
                object t = r.FindName("chk");
            }
        }

        #endregion

        private void txt_obs_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (gridDados.SelectedItems.Count == 0)
            {
                RadWindow.Alert("Selecione um Item. ");
            }
        }
    }
}