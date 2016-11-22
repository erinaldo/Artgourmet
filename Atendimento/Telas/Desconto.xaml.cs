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
    public partial class Desconto
    {
        public string Resultado = "";
        private ACONTA _conta;
        public bool DescontoConta = false;
        public List<ACONTITEM> Items = null;


        public Desconto()
        {
            InitializeComponent();
        }

        public void CarregarInfo()
        {
            _conta = DataContext as ACONTA;
        }

        private void btOK_Click(object sender, RoutedEventArgs e)
        {
            if (txtDesconto.Text != "")
            {
                decimal desconto = Convert.ToDecimal(txtDesconto.Text);

                if (DescontoConta)
                {
                    _conta.vendedorDesconto = Memoria.Vendedor;
                    _conta.dataDesconto = DateTime.Now;
                    _conta.desconto = desconto;
                }
                else
                {
                    foreach (ACONTITEM it in Items)
                    {
                        it.vendedorDesconto = Memoria.Vendedor;
                        it.dataDesconto = DateTime.Now;

                        if (it.preco >= desconto)
                        {
                            it.desconto = desconto;
                        }
                        else if (it.preco < desconto)
                        {
                            it.desconto = it.preco;
                        }
                    }
                }

                Close();
            }
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

            txtDesconto.Focus();
        }

        private void entrar(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                if (txtDesconto.Text != "")
                {
                }
            }
        }
    }
}