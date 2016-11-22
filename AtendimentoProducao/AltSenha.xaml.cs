using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Artebit.Restaurante.Global.Modelo;
using Telerik.Windows.Controls;
using Artebit.Restaurante.Global.Util.WPF;

namespace Artebit.Restaurante.AtendimentoProducao
{
    /// <summary>
    /// Interaction logic for Transferir.xaml
    /// </summary>
    public partial class AltSenha : RadWindow
    {
        Artebit.Restaurante.Global.Util.Criptografia cript = new Artebit.Restaurante.Global.Util.Criptografia();
        Usuario vendedor = new Usuario();

        public string Resultado = "";
        public string msg = "";

        public AltSenha()
        {
            InitializeComponent();

            txt_senha.Focus();
        }

        public void CarregarInfo()
        {
        }

        private void btOK_Click(object sender, RoutedEventArgs e)
        {
            string senha = cript.GerarSHA1(cript.GerarMD5(txt_senha.Password));

            GUSUARIO usu =
                (from p in Contexto.Atual.GUSUARIO select p).Where(r => r.codusuario == Memoria.Codusuario).
                    FirstOrDefault();

            if (senha == usu.senha)
            {
                msg = "A nova senha não pode ser igual a anterior.";
            }
            else
            {

                usu.senha = senha;
                usu.dataUpdSenha = DateTime.Now;
                usu.dataAlteracao = DateTime.Now;
                usu.usuAlteracao = Memoria.Codusuario;

                bool resultado = (bool) vendedor.ExecutaFuncao(usu, Funcoes.Atualizar, null);

                if (resultado == false)
                {
                    msg = "Erro ao atualizar o usuário.";
                } else
                {
                    Resultado = usu.senha;
                }
            }
            this.Close();
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            Resultado = "";
            msg = "Senha inválida!";
            this.Close();
        }


        private static double GetFontSize(double height, double width, string text, double fontSize)
        {

            double sampleFontSize = fontSize;

            double htRatio, wdRatio, ratio;

            Size textSize = GetSampleSize(text, sampleFontSize);

            double sampleHeight = textSize.Height;

            double sampleWidth = textSize.Width;

            htRatio = height / sampleHeight * 0.9;

            wdRatio = width / sampleWidth * 0.9;

            ratio = (htRatio < wdRatio) ? htRatio : wdRatio;

            //ratio = wdRatio;

            double final = (sampleFontSize * ratio);

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

            Typeface myTypeface = new Typeface("Segoe UI");

            FormattedText ft = new FormattedText(txt,

            CultureInfo.CurrentCulture,

            FlowDirection.LeftToRight,

            myTypeface, sampleFontSize, Brushes.Black);

            return new Size(ft.Width, ft.Height);

        }

        // Função chamada quando o botão é criado, arruma o tamanho da letra
        private void Button_Loaded(object sender, RoutedEventArgs e)
        {
            Button bb = (Button)sender;
            bb.FontSize = GetFontSize(bb.Height, bb.Width, bb.Content.ToString(), 20);

            txt_senha.Focus();
        }

        private void entrar(object sender, KeyEventArgs e)
        {

            if (e.Key.Equals(Key.Enter))
            {
                if (txt_senha.Password != "")
                {
                    btOK_Click(sender, new RoutedEventArgs());
                }
            }

        }

    }
}
