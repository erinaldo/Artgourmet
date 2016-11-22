using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
    public partial class Transferir
    {
        public string Resultado = "";
        private ACONTA _conta = new ACONTA();
        public List<ACONTITEM> Itens = null;
        public int Tipo = 0;

        public Transferir()
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
            if (txt_mesa.Text != "")
            {
                int mesaDestino = Convert.ToInt32(txt_mesa.Text);
                var preconta = new PreContaControl();

                if (_conta.AASSOCIACAO.All(r => r.nuMesa != mesaDestino))
                {
                    bool result;
                    if (Tipo == 0)
                    {
                        #region conta toda

                        result = preconta.Transferir(_conta, mesaDestino);

                        if (result)
                        {
                            Resultado = txt_mesa.Text;
                            Close();
                        }
                        else
                        {
                            Alert(Memoria.MsgGlobal);
                            Close();
                        }

                        #endregion
                    }
                    else
                    {
                        #region Itens individual

                        // Transferir itens individualmente
                        if (Tipo == 1)
                        {
                            result = preconta.Transferir(_conta, mesaDestino, Itens);

                            if (result)
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
                            Alert(Memoria.MsgGlobal);
                            Close();
                        }

                        #endregion
                    }
                }
                else
                {
                    Alert("Não é possível transferir para essa mesa, pois ela esta unida com a mesa atual.");
                }
            }
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
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

        #region Layout

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

        #endregion
    }
}