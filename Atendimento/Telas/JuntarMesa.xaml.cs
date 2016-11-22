using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio.Global;

namespace Artebit.Restaurante.AtendimentoPDV.Telas
{
    /// <summary>
    /// Interaction logic for Transferir.xaml
    /// </summary>
    public partial class JuntarMesa
    {
        public string Resultado = "";
        private ACONTA _conta = new ACONTA();

        public JuntarMesa()
        {
            InitializeComponent();

            txt_mesa.Focus();
        }

        public void CarregarInfo()
        {
            _conta = DataContext as ACONTA;
        }

        private void btOK_Click()
        {
            var preconta = new PreContaControl();

            if (txt_mesa.Text != "")
            {
                int nuMesa = Convert.ToInt32(txt_mesa.Text);

                if (preconta.JuntarMesa(_conta, nuMesa))
                {
                    Resultado = txt_mesa.Text;
                    Close();
                }
                else
                {
                    Alert(Memoria.MsgGlobal);
                    Close();
                }
            }
            else
            {
                Alert("Selecione uma mesa.");
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

            txt_mesa.Focus();
        }

        private void entrar(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                if (txt_mesa.Text != "")
                {
                    btOK_Click();
                }
            }
        }

        private void btSeparar_Click(object sender, RoutedEventArgs e)
        {
            var preconta = new PreContaControl();

            if (txt_mesa.Text != "")
            {
                int nuMesa = Convert.ToInt32(txt_mesa.Text);

                if (preconta.SepararMesa(_conta, nuMesa))
                {
                    Resultado = txt_mesa.Text;
                    Close();
                }
                else
                {
                    Alert(Memoria.MsgGlobal);
                    Close();
                }
            }
            else
            {
                Alert("Selecione uma mesa.");
            }
        }
    }
}