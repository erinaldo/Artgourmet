using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Artebit.Restaurante.Global.Util;

namespace Artebit.Restaurante.AtendimentoPDV.Telas
{
    /// <summary>
    /// Interaction logic for Transferir.xaml
    /// </summary>
    public partial class AltSenha
    {
        private readonly VendedorControl _vendedor = new VendedorControl();

        public string Resultado = "";
        public string Msg = "";

        public AltSenha()
        {
            InitializeComponent();

            txt_senha.Focus();
        }

        public void CarregarInfo()
        {
        }

        private void btOK_Click()
        {
            string senha = Criptografia.GerarSHA1(Criptografia.GerarMD5(txt_senha.Password));

            int ven =
                (from p in Contexto.Atual.GVENDEDOR select p).Count(r => r.codigo == senha);

            if (ven == 0)
            {
                GVENDEDOR usu =
                    (from p in Contexto.Atual.GVENDEDOR select p).FirstOrDefault(r => r.idVen == Memoria.Vendedor.Value);

                if (usu != null && Criptografia.GerarSHA1(Criptografia.GerarMD5(txt_senha.Password)) == usu.codigo)
                {
                    Msg = "A nova senha não pode ser igual a anterior.";
                }
                else
                {
                    if (usu != null)
                    {
                        usu.codigo = Criptografia.GerarSHA1(Criptografia.GerarMD5(txt_senha.Password));
                        usu.dataUpdSenha = DateTime.Now;
                        usu.dataAlteracao = DateTime.Now;
                        if (Memoria.Vendedor != null) usu.usuAlteracao = Memoria.Vendedor.Value.ToString(CultureInfo.InvariantCulture);

                        _vendedor.Atualizar(usu);

                        Resultado = usu.codigo;
                    }
                }
            }
            else
            {
                Msg = "Senha já existente.";
            }
            Close();
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            Resultado = "";
            Msg = "1";
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

            txt_senha.Focus();
        }

        private void entrar(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                if (txt_senha.Password != "")
                {
                    btOK_Click();
                }
            }
        }
    }
}