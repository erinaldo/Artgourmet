using System.Linq;
using System.Windows;
using System.Windows.Input;
using Artebit.Restaurante.AtendimentoPDV.Classes;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Artebit.Restaurante.Global.Util;
using Artebit.Restaurante.Global.Util.WPF;
using Telerik.Windows.Controls;

namespace Artebit.Restaurante.AtendimentoPDV.Telas
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : IPagina
    {
        public bool FinalizaApp = true;

        public Login()
        {
            InitializeComponent();

            carregarPosts();
        }

        #region Events Handlers

        private void entrar(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                Contexto.Atual = new Global.Modelo.Restaurante();
                login();
            }
        }

        private void sair_Click(object sender, RoutedEventArgs e)
        {
            Application curApp = Application.Current;
            curApp.Shutdown();
        }

        #endregion

        #region Private Methods

        private void login()
        {
            string username = txt_login.Password;

            var usu = new GVENDEDOR();
            var vendedor = new VendedorControl();

            if (username != "")
            {
                usu.codigo = Criptografia.GerarSHA1(Criptografia.GerarMD5(username));

                usu = vendedor.Verificar(usu);

                if (usu != null)
                {
                    Memoria.Codusuario = usu.codUsuario;
                    Memoria.Vendedor = usu.idVen;
                    Memoria.NomeVendedor = usu.nome;

                    var usu2 = new GUSUARIO();
                    var control = new UsuarioControl();

                    usu2.codusuario = Memoria.Codusuario;
                    usu2.ativo = true;

                    usu2 = control.Buscar(usu2);

                    if (Memoria.RoundHouseKick(usu2))
                    {
                        bool? valsenha = vendedor.ValidarSenha(usu);

                        if (valsenha == false)
                        {
                            var pss = new AltSenha();

                            WindowUtil.MostraModal();
                            pss.ShowDialog();
                            if (pss.Msg != "")
                            {
                                if (pss.Msg != "1")
                                {
                                    RadWindow.Alert(pss.Msg);
                                }

                                WindowUtil.FechaModal();
                                return;
                            }
                            else
                            {
                                usu.codigo = pss.Resultado;
                            }
                        }

                        ControlePagina.NavigateTo(PaginaCore.PgInicial);

                        FinalizaApp = false;
                    }
                    else
                    {
                        RadWindow.Alert("Usuário não possui perfil no sistema.");
                    }
                }
                else
                {
                    txt_login.Password = "";
                    RadWindow.Alert("Usuário inválido.");
                }
            }
            else
            {
                txt_login.Password = "";
                RadWindow.Alert("Usuário inválido.");
            }
        }

        private void carregarPosts()
        {
            Post1.Visibility = Visibility.Hidden;
            Post2.Visibility = Visibility.Hidden;
            Post3.Visibility = Visibility.Hidden;
            Post4.Visibility = Visibility.Hidden;

            var avisos = Contexto.Atual.AAVISOS.Where(r => r.ativo == true).ToList();

            if(avisos.Any())
            {
                Post1.Visibility = Visibility.Visible;
                Post1Text.Text = avisos[0].descricao;
            }

            if (avisos.Count > 1)
            {
                Post2.Visibility = Visibility.Visible;
                Post2Text.Text = avisos[1].descricao;
            }

            if (avisos.Count > 2)
            {
                Post3.Visibility = Visibility.Visible;
                Post3Text.Text = avisos[2].descricao;
            }

            if (avisos.Count > 3)
            {
                Post4.Visibility = Visibility.Visible;
                Post4Text.Text = avisos[3].descricao;
            }
        }

        #endregion

        #region IPagina Methodos

        public void Carregar()
        {
            txt_login.Password = "";

            carregarPosts();
        }

        #endregion
    }
}