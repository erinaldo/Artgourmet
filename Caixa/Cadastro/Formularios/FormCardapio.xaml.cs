using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Estoque;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;

namespace Artebit.Restaurante.Caixa.Cadastro
{
    /// <summary>
    /// Interaction logic for CardapioUserControl.xaml
    /// </summary>
    public partial class FormCardapio : RadWindow
    {
        private readonly IQueryable<ENUPRECO> _precos;
        private readonly bool novo;
        private EntityCollection<ECARDAPIOITEM> _itensCardapio = new EntityCollection<ECARDAPIOITEM>();
        private QueryableCollectionView _pagedCat1;
        private QueryableCollectionView _pagedCat2;
        private QueryableCollectionView _pagedGrupo1;
        private QueryableCollectionView _pagedGrupo2;
        public ECARDAPIO cardapio = null;

        private ECARDAPIOITEM _itemAtivo;
        private int idItens;

        public FormCardapio(ECARDAPIO card)
        {
            InitializeComponent();

            cardapio = card;

            if (cardapio == null)
            {
                novo = true;
                cardapio = new ECARDAPIO();
                cardapio.ECARDAPIOITEM = _itensCardapio;

                cardapio.idEmpresa = Memoria.Empresa;
                cardapio.idFilial = Memoria.Filial;
                cardapio.idCardapio = Contexto.GerarId("ECARDAPIO");

            }

            _precos = from i in Contexto.Atual.ENUPRECO
                      select i;

            if (!novo)
            {
                carregarCardapio();
            }
        }

        private void carregarCardapio()
        {
            txtNome.Text = cardapio.nome;
            txtHor1.Value = cardapio.horInicio;
            txtHor2.Value = cardapio.horFinal;

            ckbAtivo.IsChecked = cardapio.ativo;
            ckbDiaTodo.IsChecked = cardapio.diaTodo;
            ckbSegunda.IsChecked = cardapio.segunda;
            ckbTerca.IsChecked = cardapio.terca;
            ckbQuarta.IsChecked = cardapio.quarta;
            ckbQuinta.IsChecked = cardapio.quinta;
            ckbSexta.IsChecked = cardapio.sexta;
            ckbSabado.IsChecked = cardapio.sabado;
            ckbDomingo.IsChecked = cardapio.domingo;
            _itensCardapio = cardapio.ECARDAPIOITEM;

            carregarItensCardapio();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void carregarItensCardapio()
        {
            _itensCardapio = cardapio.ECARDAPIOITEM;

            //GRUPO 1
            _pagedGrupo1 =
                new QueryableCollectionView(
                    cardapio.ECARDAPIOITEM.Where(r => r.grupo == 1 && (r.idItemPai == 0 || r.idItemPai == null)).OrderBy
                        (r => r.posicao).ToList());
            pager.Source = _pagedGrupo1;
            grid_cozinha.ItemsSource = _pagedGrupo1;

            //GRUPO 2
            _pagedGrupo2 =
                new QueryableCollectionView(
                    cardapio.ECARDAPIOITEM.Where(r => r.grupo == 2 && (r.idItemPai == 0 || r.idItemPai == null)).OrderBy
                        (r => r.posicao).ToList());
            pager2.Source = _pagedGrupo2;
            grid_bar.ItemsSource = _pagedGrupo2;

            //CAT 1
            _pagedCat1 =
                new QueryableCollectionView(
                    cardapio.ECARDAPIOITEM.Where(r => r.idItemPai != 0 && r.idItemPai != null).OrderBy(r => r.posicao));

            pagerCat1.Source = _pagedCat1;
            grid_Cat1.ItemsSource = _pagedCat1;

            _pagedCat1.FilterDescriptors.Clear();

            var cfd1 = new CompositeFilterDescriptor();
            var f1 = new FilterDescriptor("idItemPai", FilterOperator.IsEqualTo, -1);
            cfd1.FilterDescriptors.Add(f1);
            _pagedCat1.FilterDescriptors.Add(cfd1);


            //CAT 2
            _pagedCat2 =
                new QueryableCollectionView(
                    cardapio.ECARDAPIOITEM.Where(r => r.idItemPai != 0 && r.idItemPai != null).OrderBy(r => r.posicao));

            pagerCat2.Source = _pagedCat2;
            grid_Cat2.ItemsSource = _pagedCat2;

            _pagedCat2.FilterDescriptors.Clear();

            var cfd2 = new CompositeFilterDescriptor();
            var f2 = new FilterDescriptor("idItemPai", FilterOperator.IsEqualTo, -1);
            cfd2.FilterDescriptors.Add(f2);
            _pagedCat2.FilterDescriptors.Add(cfd2);


            idItens = _itensCardapio.Max(r => r.idItemCard);
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            idItens++;
            var item = new ECARDAPIOITEM
                           {
                               idItemPai = null,
                               cor = "#FF00FF",
                               corFonte = "#000000",
                               posicao = 999,
                               idItemCard = idItens
                           };

            _pagedGrupo1.AddNew(item);

            pager.MoveToLastPage();

            //var janela = new FormCardProduto();

            //WindowUtil.MostraModal();
            //janela.ShowDialog();

            //foreach (EPRODUTO p in janela.Produtos)
            //{
            //    var ec = new ECARDAPIOITEM
            //                 {
            //                     idCardapio = cardapio == null ? 0 : cardapio.idCardapio, ativo = true
            //                 };

            //    if (Memoria.Empresa != null) ec.idEmpresa = Memoria.Empresa.Value;
            //    if (Memoria.Filial != null) ec.idFilial = Memoria.Filial.Value;
            //    //ec.grupo = pegaIdGrupo();
            //    //ec.idPrdCat1 = p.idProduto;
            //    //ec.localPreco = 0;
            //    ec.nuPreco = 0;
            //    ec.ENUPRECO = _precos.Single(r => r.nuPreco == 0);
            //    ec.EPRODUTO = p;

            //    _itensCardapio.Add(ec);
            //}

            //WindowUtil.FechaModal();

            //carregaItensCardapio();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            Funcoes funcao;

            if (novo)
            {
                funcao = Funcoes.Adicionar;
            }
            else
            {
                //EDITAR CARDAPIO
                funcao = Funcoes.Atualizar;
            }


            cardapio.nome = txtNome.Text;
            cardapio.horInicio =
                Convert.ToDateTime("01/01/2000 " + Convert.ToDateTime(txtHor1.Value.ToString()).ToString("HH:mm:ss"));
            cardapio.horFinal =
                Convert.ToDateTime("01/01/2000 " + Convert.ToDateTime(txtHor2.Value.ToString()).ToString("HH:mm:ss"));
            cardapio.ativo = ckbAtivo.IsChecked;
            cardapio.diaTodo = ckbDiaTodo.IsChecked;
            cardapio.segunda = ckbSegunda.IsChecked;
            cardapio.terca = ckbTerca.IsChecked;
            cardapio.quarta = ckbQuarta.IsChecked;
            cardapio.quinta = ckbQuinta.IsChecked;
            cardapio.sexta = ckbSexta.IsChecked;
            cardapio.sabado = ckbSabado.IsChecked;
            cardapio.domingo = ckbDomingo.IsChecked;

            int id = 1;
            if (cardapio.ECARDAPIOITEM.Count > 0)
            {
                IOrderedEnumerable<ECARDAPIOITEM> lista =
                    cardapio.ECARDAPIOITEM.Where(r => r.idItemCard != 0).OrderBy(r => r.idItemCard);
                if (lista.Count() > 0)
                {
                    id = lista.Last().idItemCard;
                }
            }

            foreach (ECARDAPIOITEM cont in _itensCardapio.Where(r => r.idItemCard == 0))
            {
                id++;
                cont.idEmpresa = Memoria.Empresa;
                cont.idFilial = Memoria.Filial;
                cont.idItemCard = id;
            }

            var controlador = new CardapioControl();

            bool result = false;

            if (funcao == Funcoes.Adicionar)
            {
                result = controlador.Criar(cardapio);
            }
            else
            {
                result = controlador.Atualizar(cardapio);
            }

            if (result)
            {
                Alert("Dados gravados com sucesso");
                Close();
            }
            else
            {
                Alert("Erro ao gravar os dados");
            }
        }

        private void btGrupo_Click(object sender, RoutedEventArgs e)
        {
            var bt = sender as ToggleButton;

            if (bt == null) return;
            var item = bt.DataContext as ECARDAPIOITEM;

            _itemAtivo = item;

            _pagedCat1.FilterDescriptors.Clear();

            var cfd = new CompositeFilterDescriptor();

            if (item != null)
            {
                var f1 = new FilterDescriptor("idItemPai", FilterOperator.IsEqualTo, item.idItemCard);
                cfd.FilterDescriptors.Add(f1);
            }

            _pagedCat1.FilterDescriptors.Add(cfd);


            _pagedCat2.FilterDescriptors.Clear();

            var cfd2 = new CompositeFilterDescriptor();
            var f2 = new FilterDescriptor("idItemPai", FilterOperator.IsEqualTo, -1);
            cfd2.FilterDescriptors.Add(f2);
            _pagedCat2.FilterDescriptors.Add(cfd2);            
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

        #region ToolBar Grupo 1
        private void btAdd1_Click(object sender, RoutedEventArgs e)
        {
            idItens++;
            var item = new ECARDAPIOITEM
            {
                idItemCard = idItens,
                idEmpresa = 1,
                idFilial = 1,
                idItemPai = null,
                cor = "#FF00FF",
                corFonte = "#000000",
                posicao = 999,
                ativo = true,
                descricao = "",
                grupo = 1,
                idCardapio = cardapio.idCardapio,
                idProduto = null,
                usaPreco = false
            };

            _itensCardapio.Add(item);

            _pagedGrupo1 =
                new QueryableCollectionView(
                    _itensCardapio.Where(r => r.grupo == 1 && (r.idItemPai == 0 || r.idItemPai == null)).OrderBy(
                        r => r.posicao).ToList());
            pager.Source = _pagedGrupo1;
            grid_cozinha.ItemsSource = _pagedGrupo1;

            pager.MoveToLastPage();
        }

        private void colocPickBG1_SelectedColorChanged(object sender, EventArgs e)
        {
            var corpk = (RadColorPicker) sender;

            IEnumerable<ToggleButton> itens =
                (from object item in grid_cozinha.Items select FindItemControl(grid_cozinha, "btItem", item)).OfType
                    <ToggleButton>().Where(tb => tb.IsChecked == true);

            foreach (ToggleButton tb in itens)
            {
                tb.IsChecked = false;

                var it = tb.DataContext as ECARDAPIOITEM;

                if (it != null) it.cor = corpk.SelectedColor.ToString();
            }
        }

        private void colocPickFG1_SelectedColorChanged(object sender, EventArgs e)
        {
            var corpk = (RadColorPicker) sender;

            IEnumerable<ToggleButton> itens =
                (from object item in grid_cozinha.Items select FindItemControl(grid_cozinha, "btItem", item)).OfType
                    <ToggleButton>().Where(tb => tb.IsChecked == true);

            foreach (ToggleButton tb in itens)
            {
                tb.IsChecked = false;

                var it = tb.DataContext as ECARDAPIOITEM;

                if (it != null) it.corFonte = corpk.SelectedColor.ToString();
            }
        }

        private void btG1Ativar_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<ToggleButton> itens =
                (from object item in grid_cozinha.Items select FindItemControl(grid_cozinha, "btItem", item)).OfType
                    <ToggleButton>().Where(tb => tb.IsChecked == true);

            foreach (ToggleButton tb in itens)
            {
                tb.IsChecked = false;

                var it = tb.DataContext as ECARDAPIOITEM;

                if (it == null) continue;

                if (it.ativo == true) continue;

                it.cor = "#0000FF";
                it.ativo = true;
            }
        }

        private void btG1Inativar_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<ToggleButton> itens =
                (from object item in grid_cozinha.Items select FindItemControl(grid_cozinha, "btItem", item)).OfType
                    <ToggleButton>().Where(tb => tb.IsChecked == true);

            foreach (ToggleButton tb in itens)
            {
                tb.IsChecked = false;

                var it = tb.DataContext as ECARDAPIOITEM;

                if (it == null) continue;

                if (it.ativo == false) continue;

                it.cor = "#EEEEEE";
                it.ativo = false;
            }
        }

        private void btG1Edit_Click(object sender, RoutedEventArgs e)
        {

            IEnumerable<ToggleButton> itens =
                (from object item in grid_cozinha.Items select FindItemControl(grid_cozinha, "btItem", item)).OfType
                    <ToggleButton>().Where(tb => tb.IsChecked == true);

            var obj = itens.FirstOrDefault(r => r.IsChecked == true);

            if (obj != null)
            {
                //WindowUtil.MostraModal();
                var janela = new FormCardProduto(obj.DataContext as ECARDAPIOITEM);
                janela.ShowDialog();
                //WindowUtil.FechaModal();
            }
            else
            {
                Alert("Selecione ao menos um item.");
            }
        }
        #endregion


        #region ToolBar Grupo 2
        private void btAdd2_Click(object sender, RoutedEventArgs e)
        {
            idItens++;

            var item = new ECARDAPIOITEM
            {
                idItemCard = idItens,
                idEmpresa = 1,
                idFilial = 1,
                idItemPai = null,
                cor = "#FF00FF",
                corFonte = "#000000",
                posicao = 999,
                ativo = true,
                descricao = "",
                grupo = 2,
                idCardapio = cardapio.idCardapio,
                idProduto = null,
                usaPreco = false
            };

            _itensCardapio.Add(item);

            _pagedGrupo2 =
                new QueryableCollectionView(
                    _itensCardapio.Where(r => r.grupo == 2 && (r.idItemPai == 0 || r.idItemPai == null)).OrderBy(
                        r => r.posicao).ToList());
            pager2.Source = _pagedGrupo2;
            grid_bar.ItemsSource = _pagedGrupo2;

            pager2.MoveToLastPage();
        }

        private void colocPickBG2_SelectedColorChanged(object sender, EventArgs e)
        {
            var corpk = (RadColorPicker)sender;

            IEnumerable<ToggleButton> itens =
                (from object item in grid_bar.Items select FindItemControl(grid_bar, "btItem", item)).OfType
                    <ToggleButton>().Where(tb => tb.IsChecked == true);

            foreach (ToggleButton tb in itens)
            {
                tb.IsChecked = false;

                var it = tb.DataContext as ECARDAPIOITEM;

                if (it != null) it.cor = corpk.SelectedColor.ToString();
            }
        }

        private void colocPickFG2_SelectedColorChanged(object sender, EventArgs e)
        {
            var corpk = (RadColorPicker)sender;

            IEnumerable<ToggleButton> itens =
                (from object item in grid_bar.Items select FindItemControl(grid_bar, "btItem", item)).OfType
                    <ToggleButton>().Where(tb => tb.IsChecked == true);

            foreach (ToggleButton tb in itens)
            {
                tb.IsChecked = false;

                var it = tb.DataContext as ECARDAPIOITEM;

                if (it != null) it.corFonte = corpk.SelectedColor.ToString();
            }
        }

        private void btG2Ativar_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<ToggleButton> itens =
                (from object item in grid_bar.Items select FindItemControl(grid_bar, "btItem", item)).OfType
                    <ToggleButton>().Where(tb => tb.IsChecked == true);

            foreach (ToggleButton tb in itens)
            {
                tb.IsChecked = false;

                var it = tb.DataContext as ECARDAPIOITEM;

                if (it == null) continue;

                if (it.ativo == true) continue;

                it.cor = "#0000FF";
                it.ativo = true;
            }
        }

        private void btG2Inativar_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<ToggleButton> itens =
                (from object item in grid_bar.Items select FindItemControl(grid_bar, "btItem", item)).OfType
                    <ToggleButton>().Where(tb => tb.IsChecked == true);

            foreach (ToggleButton tb in itens)
            {
                tb.IsChecked = false;

                var it = tb.DataContext as ECARDAPIOITEM;

                if (it == null) continue;

                if (it.ativo == false) continue;

                it.cor = "#EEEEEE";
                it.ativo = false;
            }
        }

        private void btG2Edit_Click(object sender, RoutedEventArgs e)
        {

            IEnumerable<ToggleButton> itens =
                (from object item in grid_bar.Items select FindItemControl(grid_bar, "btItem", item)).OfType
                    <ToggleButton>().Where(tb => tb.IsChecked == true);

            var obj = itens.FirstOrDefault(r => r.IsChecked == true);

            if (obj != null)
            {
                //WindowUtil.MostraModal();
                var janela = new FormCardProduto(obj.DataContext as ECARDAPIOITEM);
                janela.ShowDialog();
                //WindowUtil.FechaModal();
            }
            else
            {
                Alert("Selecione ao menos um item.");
            }
        }
        #endregion

        #region ToolBar Cat 1
        private void btAdd3_Click(object sender, RoutedEventArgs e)
        {
            idItens++;

            var item = new ECARDAPIOITEM
            {
                idItemCard = idItens,
                idEmpresa = Memoria.Empresa,
                idFilial = Memoria.Filial,
                idItemPai = _itemAtivo.idItemCard,
                cor = "#FF00FF",
                corFonte = "#000000",
                posicao = 999,
                ativo = true,
                descricao = "",
                grupo = _itemAtivo.grupo,
                idCardapio = cardapio.idCardapio,
                idProduto = null,
                usaPreco = false
            };

            _itensCardapio.Add(item);

            
            _pagedCat1 =
                new QueryableCollectionView(
                    _itensCardapio.Where(r => r.idItemPai == _itemAtivo.idItemCard).OrderBy(
                        r => r.posicao).ToList());
            pagerCat1.Source = _pagedCat1;
            grid_Cat1.ItemsSource = _pagedCat1;

            pagerCat1.MoveToLastPage();
        }

        private void colocPickBC1_SelectedColorChanged(object sender, EventArgs e)
        {
            var corpk = (RadColorPicker)sender;

            IEnumerable<ToggleButton> itens =
                (from object item in grid_Cat1.Items select FindItemControl(grid_Cat1, "btItem", item)).OfType
                    <ToggleButton>().Where(tb => tb.IsChecked == true);

            foreach (ToggleButton tb in itens)
            {
                tb.IsChecked = false;

                var it = tb.DataContext as ECARDAPIOITEM;

                if (it != null) it.cor = corpk.SelectedColor.ToString();
            }
        }

        private void colocPickFC1_SelectedColorChanged(object sender, EventArgs e)
        {
            var corpk = (RadColorPicker)sender;

            IEnumerable<ToggleButton> itens =
                (from object item in grid_Cat1.Items select FindItemControl(grid_Cat1, "btItem", item)).OfType
                    <ToggleButton>().Where(tb => tb.IsChecked == true);

            foreach (ToggleButton tb in itens)
            {
                tb.IsChecked = false;

                var it = tb.DataContext as ECARDAPIOITEM;

                if (it != null) it.corFonte = corpk.SelectedColor.ToString();
            }
        }

        private void btC1Ativar_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<ToggleButton> itens =
                (from object item in grid_Cat1.Items select FindItemControl(grid_Cat1, "btItem", item)).OfType
                    <ToggleButton>().Where(tb => tb.IsChecked == true);

            foreach (ToggleButton tb in itens)
            {
                tb.IsChecked = false;

                var it = tb.DataContext as ECARDAPIOITEM;

                if (it == null) continue;

                if (it.ativo == true) continue;

                it.cor = "#0000FF";
                it.ativo = true;
            }
        }

        private void btC1Inativar_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<ToggleButton> itens =
                (from object item in grid_Cat1.Items select FindItemControl(grid_Cat1, "btItem", item)).OfType
                    <ToggleButton>().Where(tb => tb.IsChecked == true);

            foreach (ToggleButton tb in itens)
            {
                tb.IsChecked = false;

                var it = tb.DataContext as ECARDAPIOITEM;

                if (it == null) continue;

                if (it.ativo == false) continue;

                it.cor = "#EEEEEE";
                it.ativo = false;
            }
        }

        private void btC1Edit_Click(object sender, RoutedEventArgs e)
        {

            IEnumerable<ToggleButton> itens =
                (from object item in grid_Cat1.Items select FindItemControl(grid_Cat1, "btItem", item)).OfType
                    <ToggleButton>().Where(tb => tb.IsChecked == true);

            var obj = itens.FirstOrDefault(r => r.IsChecked == true);

            if (obj != null)
            {
                //WindowUtil.MostraModal();
                var janela = new FormCardProduto(obj.DataContext as ECARDAPIOITEM);
                janela.ShowDialog();
                //WindowUtil.FechaModal();
            }
            else
            {
                Alert("Selecione ao menos um item.");
            }
        }
        #endregion

        #region ToolBar Cat 2
        private void btAdd4_Click(object sender, RoutedEventArgs e)
        {
            idItens++;

            var item = new ECARDAPIOITEM
            {
                idItemCard = idItens,
                idEmpresa = Memoria.Empresa,
                idFilial = Memoria.Filial,
                idItemPai = _itemAtivo.idItemCard,
                cor = "#FF00FF",
                corFonte = "#000000",
                posicao = 999,
                ativo = true,
                descricao = "",
                grupo = _itemAtivo.grupo,
                idCardapio = cardapio.idCardapio,
                idProduto = null,
                usaPreco = false
            };

            _itensCardapio.Add(item);


            _pagedCat2 =
                new QueryableCollectionView(
                    _itensCardapio.Where(r => r.idItemPai == _itemAtivo.idItemCard).OrderBy(
                        r => r.posicao).ToList());
            pagerCat2.Source = _pagedCat2;
            grid_Cat2.ItemsSource = _pagedCat2;

            pagerCat2.MoveToLastPage();
        }

        private void colocPickBC2_SelectedColorChanged(object sender, EventArgs e)
        {
            var corpk = (RadColorPicker)sender;

            IEnumerable<ToggleButton> itens =
                (from object item in grid_Cat2.Items select FindItemControl(grid_Cat2, "btItem", item)).OfType
                    <ToggleButton>().Where(tb => tb.IsChecked == true);

            foreach (ToggleButton tb in itens)
            {
                tb.IsChecked = false;

                var it = tb.DataContext as ECARDAPIOITEM;

                if (it != null) it.cor = corpk.SelectedColor.ToString();
            }
        }

        private void colocPickFC2_SelectedColorChanged(object sender, EventArgs e)
        {
            var corpk = (RadColorPicker)sender;

            IEnumerable<ToggleButton> itens =
                (from object item in grid_Cat2.Items select FindItemControl(grid_Cat2, "btItem", item)).OfType
                    <ToggleButton>().Where(tb => tb.IsChecked == true);

            foreach (ToggleButton tb in itens)
            {
                tb.IsChecked = false;

                var it = tb.DataContext as ECARDAPIOITEM;

                if (it != null) it.corFonte = corpk.SelectedColor.ToString();
            }
        }

        private void btC2Ativar_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<ToggleButton> itens =
                (from object item in grid_Cat2.Items select FindItemControl(grid_Cat2, "btItem", item)).OfType
                    <ToggleButton>().Where(tb => tb.IsChecked == true);

            foreach (ToggleButton tb in itens)
            {
                tb.IsChecked = false;

                var it = tb.DataContext as ECARDAPIOITEM;

                if (it == null) continue;

                if (it.ativo == true) continue;

                it.cor = "#0000FF";
                it.ativo = true;
            }
        }

        private void btC2Inativar_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<ToggleButton> itens =
                (from object item in grid_Cat2.Items select FindItemControl(grid_Cat2, "btItem", item)).OfType
                    <ToggleButton>().Where(tb => tb.IsChecked == true);

            foreach (ToggleButton tb in itens)
            {
                tb.IsChecked = false;

                var it = tb.DataContext as ECARDAPIOITEM;

                if (it == null) continue;

                if (it.ativo == false) continue;

                it.cor = "#EEEEEE";
                it.ativo = false;
            }
        }

        private void btC2Edit_Click(object sender, RoutedEventArgs e)
        {

            IEnumerable<ToggleButton> itens =
                (from object item in grid_Cat2.Items select FindItemControl(grid_Cat2, "btItem", item)).OfType
                    <ToggleButton>().Where(tb => tb.IsChecked == true);

            var obj = itens.FirstOrDefault(r => r.IsChecked == true);

            if (obj != null)
            {
                //WindowUtil.MostraModal();
                var janela = new FormCardProduto(obj.DataContext as ECARDAPIOITEM);
                janela.ShowDialog();
                //WindowUtil.FechaModal();
            }
            else
            {
                Alert("Selecione ao menos um item.");
            }
        }
        #endregion

        private void btCat1_Click(object sender, RoutedEventArgs e)
        {
            var bt = sender as ToggleButton;

            if (bt == null) return;
            var item = bt.DataContext as ECARDAPIOITEM;

            _pagedCat2.FilterDescriptors.Clear();

            var cfd = new CompositeFilterDescriptor();

            if (item != null)
            {
                var f1 = new FilterDescriptor("idItemPai", FilterOperator.IsEqualTo, item.idItemCard);
                cfd.FilterDescriptors.Add(f1);
            }

            _pagedCat2.FilterDescriptors.Add(cfd);
        }
    }
}