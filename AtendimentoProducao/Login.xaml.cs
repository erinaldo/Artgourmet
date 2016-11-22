using System;
using System.Collections.Generic;
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
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global;
using Artebit.Restaurante.Global.Util;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Caixa;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Artebit.Restaurante.Global.RegrasNegocio.Atendimento;
using Telerik.Windows.Controls;
using Artebit.Restaurante.Global.RegrasNegocio.Estoque;

namespace Artebit.Restaurante.AtendimentoProducao
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            StyleManager.ApplicationTheme = new Office_SilverTheme();
            InitializeComponent();
            Txt_usuario.Focus();

            

            //ImpressoraFiscal.Impressora imp = new ImpressoraFiscal.Impressora();
            //imp.Verifica();
            //imp.MovimentacaoECF("", DateTime.Now, DateTime.Now, new AECF());
            //string a = "";C:\Users\Naldo-S\Desktop\Artebit\Clientes\Devassa\Codigo Fonte\Restaurante\Caixa\ImpressoraFiscal\Suprimento.xaml
            //string b = "";
            //string c = "";
            //imp.RetornaDadosImpressora(ref a, ref b, ref c);

        }

        private void login()
        {
            string username = Txt_usuario.Text;
            GUSUARIO usu = new GUSUARIO();
            Usuario usuario = new Usuario();
            Criptografia cript = new Criptografia();
            List<string> compl = new List<string>();
            usu.codusuario = username;
            usu.senha = cript.GerarSHA1(cript.GerarMD5(txtbox_senha.Password));

            usu = (GUSUARIO)usuario.ExecutaFuncao(usu, Funcoes.Verificar, compl);


            Console.WriteLine(usu);

            if (usu != null)
            {
                Memoria.Codusuario = usu.codusuario;
                if (Memoria.RoundHouseKick(usu))
                {
                    Memoria.IdMonitor = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MONITOR"]);
                    Memoria.Vendedor = 1;
                    MainWindow win = new MainWindow();
                    win.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuário não possui perfil no sistema.");
                }
            }
            else
            {
                MessageBox.Show("Usuário inválido.");
            }



        }

        private void txtbox_senha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                login();
            }

        }

        private void Logar_Click(object sender, RoutedEventArgs e)
        {
            login();
        }


        private void sair_Click(object sender, RoutedEventArgs e)
        {
            Application curApp = Application.Current;
            curApp.Shutdown();
        }

    }

}
