using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio.Global;

namespace Artebit.Restaurante.AtendimentoPDV.Telas
{
    /// <summary>
    /// Interaction logic for Pessoas.xaml
    /// </summary>
    public partial class Pessoas
    {
        public string Resultado = "";
        private ACONTA _conta = new ACONTA();

        public Pessoas()
        {
            InitializeComponent();
        }

        public void CarregarInfo()
        {
            _conta = DataContext as ACONTA;
            if (_conta != null) txtQtdPessoas.Text = _conta.pessoas.ToString(CultureInfo.InvariantCulture);
        }

        private void btOK_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(txtQtdPessoas.Text) <= 0)
            {
                Alert("A mesa não pode estar ocupada por menos de 1 pessoa.");
            }
            else
            {
                _conta.pessoas = Convert.ToInt32(txtQtdPessoas.Text);

                var preconta = new PreContaControl();

                Memoria.LogAcao = "Quantidade de pessoas: " + txtQtdPessoas.Text;
                bool result = preconta.Atualizar(_conta, "0");

                if (result)
                {
                    Resultado = "1";
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
        }


        // Clique do botão UP
        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            int qtd = Convert.ToInt32(txtQtdPessoas.Text);
            qtd++;
            txtQtdPessoas.Text = qtd.ToString(CultureInfo.InvariantCulture);
        }

        // Clique do botão DOWN
        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            int qtd = Convert.ToInt32(txtQtdPessoas.Text);
            qtd--;

            if (qtd < 1)
            {
                qtd = 1;
            }

            txtQtdPessoas.Text = qtd.ToString(CultureInfo.InvariantCulture);
        }
    }
}