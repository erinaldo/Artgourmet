using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.AtendimentoPDV.Telas
{
    /// <summary>
    /// Interaction logic for Desconto.xaml
    /// </summary>
    public partial class Quantidade
    {
        public List<ACONTITEM> Items = null;
        public Pedido PaginaPai = null;

        public string Resultado = "";
        public bool DescontoConta = false;


        public Quantidade()
        {
            InitializeComponent();
            Items = new List<ACONTITEM>();
        }

        public void CarregarInfo()
        {
        }

        private void btOK_Click(object sender, RoutedEventArgs e)
        {
            decimal qtd = 0;
            if (txtQuantidade.Text != "")
                qtd = Convert.ToDecimal(txtQuantidade.Text);

            foreach (ACONTITEM it in Items)
            {
                for (int i = 1; i < qtd; i++)
                {
                    var nItem = new ACONTITEM();
                    PaginaPai.Cont ++;

                    nItem.nuItem = PaginaPai.Cont;
                    nItem.idProduto = it.idProduto;
                    nItem.idVen = it.idVen;
                    nItem.preco = it.preco;
                    nItem.txtObs = it.txtObs;
                    nItem.adicional = it.adicional;
                    nItem.quantidade = it.quantidade;
                    nItem.opcao = it.opcao;
                    nItem.nuItemPai = it.nuItemPai;

                    foreach (EOBSERVACAO o in it.EOBSERVACAO)
                    {
                        nItem.EOBSERVACAO.Add(o);
                    }
                    //nItem.EOBSERVACAO = it.EOBSERVACAO;

                    PaginaPai.Pedidos.Add(nItem);
                }
            }
            //PaginaPai.populaGrid();


            Close();
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private static double GetFontSize(double height, double width, string text, double fontSize)
        {
            double sampleFontSize = fontSize;

            Size textSize = GetSampleSize(text, sampleFontSize);

            double sampleHeight = textSize.Height;

            double sampleWidth = textSize.Width;

            double htRatio = height/sampleHeight*0.9;

            double wdRatio = width/sampleWidth*0.9;

            double ratio = (htRatio < wdRatio) ? htRatio : wdRatio;

            //ratio = wdRatio;

            double final = (sampleFontSize*ratio);

            if (final > 20)
            {
                final = 20;
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

        // Função chamada quando o botão é criado, arruma o tamanho da letra
        private void Button_Loaded(object sender, RoutedEventArgs e)
        {
            var bb = (Button) sender;
            bb.FontSize = GetFontSize(bb.Height, bb.Width, bb.Content.ToString(), 20);

            txtQuantidade.Focus();
        }

        private void entrar(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                if (txtQuantidade.Text != "")
                {
                }
            }
        }
    }
}