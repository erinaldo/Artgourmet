using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Estoque;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Telerik.Windows.Controls;

namespace Artebit.Restaurante.Caixa.Caixas.Janelas
{
    /// <summary>
    /// Interaction logic for Adicionar.xaml
    /// </summary>
    public partial class Adicionar : RadWindow
    {
        //int idProduto = 0;
        //int quantidade = 0;
        //decimal? precoUnit = 0;
        //decimal? precoTotal = 0;
        //decimal? desconto = 0;

        private static readonly ECARDAPIOITEM item = new ECARDAPIOITEM();
        private static readonly CardapioItemControl cardControl = new CardapioItemControl();
        private static readonly CardapioControl Control = new CardapioControl();

        private readonly int cardapioatual = Control.BuscarAtualId();
        private readonly IQueryable<ECARDAPIOITEM> itensCardapio = cardControl.BuscarListaEspecifica(item);
        private readonly List<ACONTITEM> pedidos = new List<ACONTITEM>();
        private int Cat1;
        private int Cat2;
        private int Cat3;
        private List<ACONTITEM> _adicionais = new List<ACONTITEM>();
        private int cont;
        private int contBar = 0;
        private int contCozinha = 0;
        private ACONTA conta;
        private ACONTITEM itemConta = new ACONTITEM();
        private int localPreco;
        private int? nuitem;
        private int pai;
        private int? produto;

        public Adicionar()
        {
            InitializeComponent();

            itensCardapio = itensCardapio.Where(i => i.idCardapio == cardapioatual);

            CarregaComidaBebida();
        }

        #region Função de carregamento de produtos

        // ====================================================
        // Função de carregamento de produtos
        private void CarregaComidaBebida()
        {
            //var lista = from a in itensCardapio
            //            join e in Contexto.Atual.EPRODUTO on a.idPrdCat1 equals e.idProduto
            //            where a.idPrdCat2 == null && a.idPrdCat3 == null
            //            select new
            //            {
            //                id = a.localPreco == 1 ? SqlFunctions.StringConvert((double)a.idPrdCat1).Trim() + "@" + SqlFunctions.StringConvert((double)a.localPreco).Trim() + "@" + SqlFunctions.StringConvert((double)a.nuPreco).Trim() + "@" + e.nome : SqlFunctions.StringConvert((double)a.idPrdCat1).Trim() + "@0@0@" + e.nome,
            //                nome = e.nome,
            //                grupo = a.grupo
            //            };

            //grid_bar.ItemsSource = lista.Where(r => r.grupo == 1).Distinct();// Bebidas
            //contBar = lista.Where(r => r.grupo == 1).Distinct().Count();

            //grid_cozinha.ItemsSource = lista.Where(r => r.grupo == 2).Distinct();// Comidas
            //contCozinha = lista.Where(r => r.grupo == 2).Distinct().Count();
        }

        private void CarregaCategoria2()
        {
            //var lista = from a in itensCardapio
            //            join e in Contexto.Atual.EPRODUTO on a.idPrdCat2 equals e.idProduto
            //            where a.idPrdCat3 == null
            //            select new
            //            {
            //                id = a.localPreco == 2 ? SqlFunctions.StringConvert((double)a.idPrdCat2).Trim() + "@" + SqlFunctions.StringConvert((double)a.localPreco).Trim() + "@" + SqlFunctions.StringConvert((double)a.nuPreco).Trim() + "@" + e.nome : SqlFunctions.StringConvert((double)a.idPrdCat2).Trim() + "@0@0@" + e.nome,
            //                nome = e.nome,
            //                idCat1 = a.idPrdCat1
            //            };

            //grid1.ItemsSource = lista.Where(r => r.idCat1 == Cat1).Distinct();

            //var uniform = grid1.FindChildByType<UniformGrid>();
            //uniform.Columns = lista.Where(r => r.idCat1 == Cat1).Distinct().Count();
        }


        private void CarregaCategoria3()
        {
            //var lista = from a in itensCardapio
            //            join e in Contexto.Atual.EPRODUTO on a.idPrdCat3 equals e.idProduto
            //            select new
            //            {
            //                id = a.localPreco == 3 ? SqlFunctions.StringConvert((double)a.idPrdCat3).Trim() + "@" + SqlFunctions.StringConvert((double)a.localPreco).Trim() + "@" + SqlFunctions.StringConvert((double)a.nuPreco).Trim() + "@" + e.nome : SqlFunctions.StringConvert((double)a.idPrdCat3).Trim() + "@0@0@" + e.nome,
            //                nome = e.nome,
            //                idCat1 = a.idPrdCat1,
            //                idCat2 = a.idPrdCat2
            //            };

            //grid2.ItemsSource = lista.Where(r => r.idCat1 == Cat1 && r.idCat2 == Cat2).Distinct();

            //var uniform = grid2.FindChildByType<UniformGrid>();
            //uniform.Columns = lista.Where(r => r.idCat1 == Cat1 && r.idCat2 == Cat2).Distinct().Count();
        }

        private void CarregaAdicionais()
        {
            IQueryable<EPRODUTO> lista = (from p in Contexto.Atual.EPRODUTO
                                          where (localPreco == 1 && p.idProduto == Cat1) ||
                                                (localPreco == 2 && p.idProduto == Cat2) ||
                                                (localPreco == 3 && p.idProduto == Cat3)
                                          select p);

            var l = from a in lista.First().EPRODADD
                    select new
                               {
                                   a.EPRODUTO1.nome,
                                   id = a.idPrdAdd + "@0@" + a.nuPreco + "@" + a.EPRODUTO1.nome
                               };


            grid5.ItemsSource = l;
            var uniform = grid5.FindChildByType<UniformGrid>();
            uniform.Columns = l.Count();
        }

        private void CarregaObservacoes()
        {
            IQueryable<EPRODUTO> lista = (from p in Contexto.Atual.EPRODUTO
                                          where (p.idProduto == produto)
                                          select p);

            if (lista.Count() > 0)
            {
                grid6.ItemsSource = lista.First().EOBSERVACAO;
            }
            else
            {
                grid6.ItemsSource = null;
            }

            ACONTITEM c = pedidos.Single(r => r.idProduto == produto && r.nuItem == nuitem);

            foreach (object item in grid6.Items)
            {
                var tb = FindItemControl(grid6, "btObs", item) as ToggleButton;


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

        // Clique do botão para carregar adicionais
        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            var bb = (ToggleButton) sender;

            if (bb.IsChecked == true)
            {
                CarregaAdicionais();
            }
            else
            {
                CarregaCategoria3();
            }
        }

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

            Cat1 = Convert.ToInt32(valores[0]);

            if (valores[1] == "1")
            {
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

                CarregaAdicionais();
            }
            else
            {
                localPreco = 0;
            }

            grid1.ItemsSource = null;
            grid2.ItemsSource = null;

            CarregaCategoria2();
        }

        // Categoria
        private void Escolhe_Cat2(object sender, RoutedEventArgs e)
        {
            Escolhe_Obs(1);

            var bb = (Button) sender;
            String[] valores = Convert.ToString(bb.CommandParameter).Split('@');

            Cat2 = Convert.ToInt32(valores[0]);

            if (valores[1] == "2")
            {
                var i = new ACONTITEM();
                cont++;
                i.nuItem = cont;
                i.idProduto = Convert.ToInt32(valores[0]);
                i.idVen = Convert.ToInt32(Memoria.Vendedor);
                i.preco = BuscarPreco(valores);
                ;
                i.txtObs = null;
                i.adicional = false;
                i.opcao = false;
                i.nuItemPai = null;

                pedidos.Add(i);
                populaGrid();

                localPreco = Convert.ToInt32(valores[1]);
                pai = cont;

                CarregaAdicionais();
            }
            else
            {
                if (localPreco != 1)
                {
                    localPreco = 0;
                }
            }

            grid2.ItemsSource = null;

            CarregaCategoria3();
        }

        // Opção e Guarnição
        private void Escolhe_Cat3(object sender, RoutedEventArgs e)
        {
            Escolhe_Obs(1);

            var bb = (Button) sender;
            String[] valores = Convert.ToString(bb.CommandParameter).Split('@');

            Cat3 = Convert.ToInt32(valores[0]);

            if (valores[1] == "3")
            {
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

                localPreco = Convert.ToInt32(valores[1]);
                pedidos.Add(i);

                pai = cont;

                CarregaAdicionais();
            }
            else
            {
                var i = new ACONTITEM();
                cont++;
                i.nuItem = cont;
                i.idProduto = Convert.ToInt32(valores[0]);
                i.idVen = Convert.ToInt32(Memoria.Vendedor);
                i.preco = Convert.ToDecimal("0,01");
                i.txtObs = null;
                i.adicional = false;
                i.opcao = true;
                i.nuItemPai = pai;

                Decimal preco = pedidos.SingleOrDefault(p => p.nuItem == i.nuItemPai).preco;
                pedidos.SingleOrDefault(p => p.nuItem == i.nuItemPai).preco = preco - i.preco;


                pedidos.Add(i);

                CarregaAdicionais();
            }

            populaGrid();
        }

        private void Escolhe_Add(object sender, RoutedEventArgs e)
        {
            Escolhe_Obs(1);

            var bb = (Button) sender;
            String[] valores = Convert.ToString(bb.CommandParameter).Split('@');
            Escolhe_Adicional(valores);

            populaGrid();
        }

        // Adicional
        private void Escolhe_Adicional(String[] valores)
        {
            var i = new ACONTITEM();
            cont++;
            i.nuItem = cont;
            i.idProduto = Convert.ToInt32(valores[0]);
            i.idVen = Convert.ToInt32(Memoria.Vendedor);
            i.preco = BuscarPreco(valores);
            i.txtObs = null;
            i.adicional = true;
            i.opcao = false;
            i.nuItemPai = pai;

            pedidos.Add(i);
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

                    foreach (object item in grid6.Items)
                    {
                        var tb = FindItemControl(grid6, "btObs", item) as ToggleButton;

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

            grid6.ItemsSource = null;
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
            //ECARDAPIOITEM item1 = new ECARDAPIOITEM();

            //if (Convert.ToInt32(valores[1]) > 0)
            //{
            //    item1.idPrdCat1 = Cat1;

            //    if (Convert.ToInt32(valores[1]) > 1)
            //    {
            //        item1.idPrdCat2 = Cat2;
            //    }
            //}

            //item1.localPreco = Convert.ToInt32(valores[1]);
            //item1.nuPreco = Convert.ToInt32(valores[2]);
            //item1.idEmpresa = Convert.ToInt32(Memoria.Empresa);
            //item1.idFilial = Convert.ToInt32(Memoria.Filial);

            //return cardControl.BuscarPrecoItem(item1);

            return 0;
        }

        // Função para popular grid
        private void populaGrid()
        {
            var l = from i in pedidos
                    join e in Contexto.Atual.EPRODUTO on i.idProduto equals e.idProduto
                    select new
                               {
                                   i.nuItem,
                                   i.preco,
                                   nome =
                        i.adicional ? "..Adicional : " + e.nome : i.opcao ? "....Opção : " + e.nome : e.nome,
                                   qtd = 1,
                                   und = e.undControle
                               };

            gridDados.ItemsSource = l;
            gridDados.UnselectAll();
        }

        #endregion

        public void CarregarInfo()
        {
            conta = DataContext as ACONTA;

            var item = new ECARDAPIOITEM();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Finalizar();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Função chamada quando o botão é criado, arruma o tamanho da letra
        private void Button_Loaded(object sender, RoutedEventArgs e)
        {
            var bb = (Button) sender;
            bb.FontSize = GetFontSize(bb.Height, bb.Width, bb.Content.ToString(), 20);
        }

        // Função chamada quando o toggle é criado, arruma o tamanho da letra
        private void toggle_Loaded(object sender, RoutedEventArgs e)
        {
            var bb = (ToggleButton) sender;
            bb.FontSize = GetFontSize(bb.Height, bb.Width, bb.Content.ToString(), 20);
        }

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

            if (final > 15)
            {
                final = 15;
            }

            return final;
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

        // Arruma o número de colunas da caixa de cozinha
        private void cozinha_Loaded(object sender, RoutedEventArgs e)
        {
            var ee = sender as UniformGrid;

            ee.Columns = contCozinha;
        }

        // Arruma o número de colunas da caixa de bar
        private void bar_Loaded(object sender, RoutedEventArgs e)
        {
            var ee = sender as UniformGrid;

            ee.Columns = contBar;
        }

        // Finaliza o pedido
        private void Finalizar()
        {
            var mesa = new GMESA();
            var cmesa = new MesaControl();
            mesa.nuMesa = Convert.ToInt32(Memoria.Mesa);

            var conta = new ACONTA();
            var cconta = new PreContaControl();

            Funcoes funcao;

            mesa = cmesa.Buscar(mesa);

            conta.nuMesa = mesa.nuMesa;

            ACONTA contaA = cconta.Buscar(conta);

            // Se estiver livre, tem que mudar o status e criar a conta
            if (mesa.idStatus == 2 || mesa.idStatus == 3 || mesa.idStatus == 4 || contaA == null)
            {
                funcao = Funcoes.Adicionar;
                conta.pessoas = 1;

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

                conta = contaA;

                int cont = conta.ACONTITEM.Count();

                foreach (ACONTITEM pedido in pedidos)
                {
                    cont++;

                    pedido.idStatus = 1;
                    int antigopai = pedido.nuItem;
                    pedido.nuItem = cont;

                    // Atualizando os filhos com o novo numero do pai
                    foreach (ACONTITEM itt in pedidos.Where(r => r.nuItemPai == antigopai))
                    {
                        itt.nuItemPai = cont;
                    }

                    pedido.quantidade = 1;
                    pedido.desconto = 0;
                    conta.ACONTITEM.Add(pedido);
                }
            }


            bool conf = false;

            if (funcao == Funcoes.Adicionar)
            {
                conf = cconta.Criar(conta);
            }
            else
            {
                conf = cconta.Atualizar(conta);
            }

            if (conf)
            {
                if (funcao == Funcoes.Adicionar)
                {
                    mesa.idStatus = 1;

                    bool confmesa = cmesa.Atualizar(mesa);

                    if (confmesa)
                    {
                        RadWindow.Alert("Pedido(s) enviado(s).");
                        Close();
                    }
                    else
                    {
                        RadWindow.Alert("Erro ao enviar pedido(s).");
                    }
                }
                else
                {
                    RadWindow.Alert("Pedido(s) enviado(s).");
                    Close();
                }
            }
            else
            {
                RadWindow.Alert("Erro ao enviar pedido(s).");
            }
        }

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
            switch (nome)
            {
                case "1":
                    desiredPanelWidth += grid_cozinha.ActualWidth;
                    break;
                case "2":
                    desiredPanelWidth += grid_bar.ActualWidth;
                    break;
                case "3":
                    desiredPanelWidth += grid1.ActualWidth;
                    break;
                case "4":
                    desiredPanelWidth += grid2.ActualWidth;
                    break;
                case "5":
                    desiredPanelWidth += grid5.ActualWidth;
                    break;
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
    }
}