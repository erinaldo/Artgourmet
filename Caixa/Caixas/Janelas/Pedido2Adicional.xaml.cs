using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Artebit.Restaurante.Global.Modelo;
using Telerik.Windows.Controls;

namespace Artebit.Restaurante.Caixa
{
    /// <summary>
    /// Interaction logic for Pedido2Adicional.xaml
    /// </summary>
    public partial class Pedido2Adicional : RadWindow
    {
        private int Cat1;
        private int Cat2;
        private int Cat3;
        public int cont;
        public int Id;
        public List<ECARDAPIOITEM> itensCardapio = null;
        public int localPreco;
        public int pai;

        public List<ACONTITEM> pedidos = new List<ACONTITEM>();

        public Pedido2Adicional()
        {
            InitializeComponent();

            Fechar.Focus();
        }

        public void carrega()
        {
            var i = new ECARDAPIOITEM();

            //i = itensCardapio.FirstOrDefault(r => (localPreco == 1 && r.idPrdCat1 == id) ||
            //                                     (localPreco == 2 && r.idPrdCat2 == id) ||
            //                                     (localPreco == 3 && r.idPrdCat3 == id));

            //Cat1 = Convert.ToInt32(i.idPrdCat1);
            //Cat2 = Convert.ToInt32(i.idPrdCat2);
            //Cat3 = Convert.ToInt32(i.idPrdCat3);
            //localPreco = Convert.ToInt32(i.localPreco);

            CarregaCategoria3();
            CarregaAdicionais();
        }

        #region Funções de clique de botões

        // ========================================

        // Função chamada quando o botão é criado, arruma o tamanho da letra
        private void Button_Loaded(object sender, RoutedEventArgs e)
        {
            var bb = (Button) sender;
            bb.FontSize = GetFontSize(bb.Height, bb.Width - 10, bb.Content.ToString(), 20);
            bb.Foreground = Brushes.Black;
        }

        private void CarregaCategoria3()
        {
            //var lista = from a in itensCardapio.ToList()
            //            join e in Memoria.Produtos on a.idPrdCat3 equals e.idProduto
            //            orderby e.ordemPDV, e.nomeResumo
            //            select new
            //            {
            //                id = Convert.ToString(a.idPrdCat3) + "@" + Convert.ToString(a.localPreco) + "@" + Convert.ToString(a.nuPreco) + "@" + e.nome,
            //                nome = e.nomeResumo != "" ? e.nomeResumo : e.nome,
            //                codigo = a.localPreco == 1 ?
            //                        a.EPRODUTO.codigo
            //                    :
            //                    a.localPreco == 2 ?
            //                            a.idPrdCat2 != null ?
            //                                a.EPRODUTO2.codigo
            //                            :
            //                                ""
            //                        :
            //                        a.localPreco == 3 ?
            //                                a.idPrdCat3 != null ?
            //                                    a.EPRODUTO1.codigo
            //                                :
            //                                    "" : "",
            //                codnome = a.localPreco == 1 ?
            //                        a.EPRODUTO.nomeResumo != "" ?
            //                            a.EPRODUTO.codigoPDV == "" ?
            //                                a.EPRODUTO.codigo + " - " + a.EPRODUTO.nomeResumo
            //                                    :
            //                                a.EPRODUTO.codigoPDV + " - " + a.EPRODUTO.nomeResumo
            //                            :
            //                            a.EPRODUTO.codigoPDV == "" ?
            //                                a.EPRODUTO.codigo + " - " + a.EPRODUTO.nome
            //                                    :
            //                                a.EPRODUTO.codigoPDV + " - " + a.EPRODUTO.nome
            //                    :
            //                    a.localPreco == 2 ?
            //                            a.idPrdCat2 != null ?
            //                                a.EPRODUTO2.nomeResumo != "" ?
            //                                    a.EPRODUTO2.codigoPDV == "" ?
            //                                        a.EPRODUTO2.codigo + " - " + a.EPRODUTO2.nomeResumo
            //                                            :
            //                                        a.EPRODUTO2.codigoPDV + " - " + a.EPRODUTO2.nomeResumo
            //                                    :
            //                                    a.EPRODUTO2.codigoPDV == "" ?
            //                                        a.EPRODUTO2.codigo + " - " + a.EPRODUTO2.nome
            //                                            :
            //                                        a.EPRODUTO2.codigoPDV + " - " + a.EPRODUTO2.nome
            //                            :
            //                                ""
            //                        :
            //                        a.localPreco == 3 ?
            //                                a.idPrdCat3 != null ?
            //                                    a.EPRODUTO1.nomeResumo != "" ?
            //                                        a.EPRODUTO1.codigoPDV == "" ?
            //                                            a.EPRODUTO1.codigo + " - " + a.EPRODUTO1.nomeResumo
            //                                                :
            //                                            a.EPRODUTO1.codigoPDV + " - " + a.EPRODUTO1.nomeResumo
            //                                        :
            //                                        a.EPRODUTO1.codigoPDV == "" ?
            //                                            a.EPRODUTO1.codigo + " - " + a.EPRODUTO1.nome
            //                                                :
            //                                            a.EPRODUTO1.codigoPDV + " - " + a.EPRODUTO1.nome
            //                                :
            //                                    "" : "",
            //                idCat1 = a.idPrdCat1,
            //                idCat2 = a.idPrdCat2,
            //                cor = e.corPDV == "" || e.corPDV == null ? "#B68944" : "#" + e.corPDV,
            //                ordemPDV = e.ordemPDV ?? 999
            //            };

            //grid_guarnicao.ItemsSource = lista.Where(r => r.idCat1 == Cat1 && r.idCat2 == Cat2).Distinct().OrderBy(r => r.ordemPDV).ThenBy(r => r.nome);
            //cmbGuarnicao.ItemsSource = lista.Where(r => r.idCat1 == Cat1 && r.idCat2 == Cat2).Distinct().OrderBy(r => r.ordemPDV).ThenBy(r => r.nome);
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
                                   nome = a.EPRODUTO1.nomeResumo != "" ? a.EPRODUTO1.nomeResumo : a.EPRODUTO1.nome,
                                   id = a.idPrdAdd + "@0@" + a.nuPreco + "@" + a.EPRODUTO1.nome,
                                   codnome =
                        a.EPRODUTO1.codigo + a.EPRODUTO1.nomeResumo != "" ? a.EPRODUTO1.nomeResumo : a.EPRODUTO1.nome,
                                   cor =
                        a.EPRODUTO1.corPDV == "" || a.EPRODUTO1.corPDV == null ? "#B68944" : "#" + a.EPRODUTO1.corPDV
                               };


            grid_adicional.ItemsSource = l;
        }

        // Fechar a Dialog
        private void fechaDialog(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void fechaDialogE(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Close();
            }
        }

        // Opção e Guarnição
        private void Escolhe_Cat3(object sender, RoutedEventArgs e)
        {
            var bb = (Button) sender;
            String[] valores = Convert.ToString(bb.CommandParameter).Split('@');

            // id = valores[0]
            // locapreco = valores[1]
            // nupreco = valores[2]
            // nome = valores[3]

            // se for produto de opção ou guarnição

            Cat3 = Convert.ToInt32(valores[0]);

            var i = new ACONTITEM();
            cont++;
            i.nuItem = cont;
            i.idProduto = Convert.ToInt32(valores[0]);
            i.idVen = Convert.ToInt32(Memoria.Vendedor);
            i.preco = Convert.ToDecimal("0,01");
            i.txtObs = null;
            i.adicional = false;
            i.quantidade = 1;
            i.opcao = true;
            i.nuItemPai = pai;

            Decimal preco = pedidos.SingleOrDefault(p => p.nuItem == i.nuItemPai).preco;
            pedidos.SingleOrDefault(p => p.nuItem == i.nuItemPai).preco = preco - i.preco;


            pedidos.Add(i);

            txtMsgGu.Text = "Guarnição incluida com sucesso!";

            Fechar.Focus();
        }

        private void Escolhe_Cat3_cmb(object sender, RoutedEventArgs e)
        {
            var bb = (Button) sender;
            String[] valores = Convert.ToString(cmbGuarnicao.SelectedValue).Split('@');

            // id = valores[0]
            // locapreco = valores[1]
            // nupreco = valores[2]
            // nome = valores[3]

            // se for produto de opção ou guarnição

            Cat3 = Convert.ToInt32(valores[0]);

            var i = new ACONTITEM();
            cont++;
            i.nuItem = cont;
            i.idProduto = Convert.ToInt32(valores[0]);
            i.idVen = Convert.ToInt32(Memoria.Vendedor);
            i.preco = Convert.ToDecimal("0,01");
            i.txtObs = null;
            i.adicional = false;
            i.quantidade = 1;
            i.opcao = true;
            i.nuItemPai = pai;

            Decimal preco = pedidos.SingleOrDefault(p => p.nuItem == i.nuItemPai).preco;
            pedidos.SingleOrDefault(p => p.nuItem == i.nuItemPai).preco = preco - i.preco;


            pedidos.Add(i);

            txtMsgGu.Text = "Guarnição incluida com sucesso!";

            Fechar.Focus();
        }

        // Adicional
        private void Escolhe_Adicional(object sender, RoutedEventArgs e)
        {
            var bb = (Button) sender;
            String[] valores = Convert.ToString(bb.CommandParameter).Split('@');

            // id = valores[0]
            // locapreco = valores[1]
            // nupreco = valores[2]
            // nome = valores[3]


            var i = new ACONTITEM();
            cont++;
            i.nuItem = cont;
            i.idProduto = Convert.ToInt32(valores[0]);
            i.idVen = Convert.ToInt32(Memoria.Vendedor);
            i.preco = BuscarPreco(valores);
            i.txtObs = null;
            i.adicional = true;
            i.quantidade = 1;
            i.opcao = false;
            i.nuItemPai = pai;

            pedidos.Add(i);

            txtMsgAd.Text = "Adicional incluida com sucesso!";

            Fechar.Focus();
        }

        // Função que busca o preço
        private decimal BuscarPreco(string[] valores)
        {
            //ECARDAPIOITEM item1 = new ECARDAPIOITEM();

            int idProduto = 0;
            switch (Convert.ToInt32(valores[1]))
            {
                case 1:
                    //item1.idPrdCat1 = Cat1;
                    idProduto = Cat1;
                    break;

                case 2:
                    //item1.idPrdCat2 = Cat2;
                    idProduto = Cat2;
                    break;

                case 3:
                    //item1.idPrdCat3 = Cat3;
                    idProduto = Cat3;
                    break;

                default:
                    break;
            }

            if (idProduto == 0)
            {
                idProduto = Convert.ToInt32(valores[0]);
            }

            //item1.localPreco = Convert.ToInt32(valores[1]);
            //item1.nuPreco = Convert.ToInt32(valores[2]);
            //item1.idEmpresa = Convert.ToInt32(Memoria.Empresa);
            //item1.idFilial = Convert.ToInt32(Memoria.Filial);

            switch (Convert.ToInt32(valores[2]))
            {
                case 1:
                    return Memoria.Produtos.FirstOrDefault(r => r.idProduto == idProduto).Preco1;
                case 2:
                    return Memoria.Produtos.FirstOrDefault(r => r.idProduto == idProduto).Preco2;
                case 3:
                    return Memoria.Produtos.FirstOrDefault(r => r.idProduto == idProduto).Preco3;
                default:
                    return 0;
            }

            //return (decimal)cardControl.ExecutaFuncao(item1, Funcoes.BuscarPreco, null);
        }

        // ========================================

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

            return 10;
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

        #endregion
    }
}