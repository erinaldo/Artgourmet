using System;
using System.Windows;
using System.Windows.Input;
using Artebit.Restaurante.Caixa.Classes;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio.Caixa;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Artebit.Restaurante.Global.Util;
using Artebit.Restaurante.Global.Util.WPF;
using Telerik.Windows.Controls;

namespace Artebit.Restaurante.Caixa
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : IPagina
    {
        public Login()
        {
            InitializeComponent();
        }

        #region IPagina Members

        public void Carregar()
        {
            Txt_usuario.Focus();
        }

        #endregion

        private void login()
        {
            string username = Txt_usuario.Text;

            var usu = new GUSUARIO();
            var usuario = new UsuarioControl();

            usu.codusuario = username;
            usu.senha = Criptografia.GerarSHA1(Criptografia.GerarMD5(txtbox_senha.Password));

            usu = usuario.Verificar(usu);

            if (usu != null)
            {
                Memoria.Codusuario = usu.codusuario;
                Memoria.Vendedor = 1;

                if (Memoria.RoundHouseKick(usu))
                {
                    bool? valsenha = usuario.ValidarSenha(usu);

                    if (valsenha == false)
                    {
                        var pss = new AltSenha();
                        WindowUtil.MostraModal();
                        pss.ShowDialog();
                        if (pss.Msg != "")
                        {
                            RadWindow.Alert(pss.Msg);
                            WindowUtil.FechaModal();
                        }
                        else
                        {
                            usu.senha = pss.Resultado;
                            WindowUtil.FechaModal();
                        }
                    }

                    var ctrl = new PeriodoFiscalControl();
                    APERIODOFISCAL obj = ctrl.BuscarAtual();

                    if (obj == null)
                    {
                        obj = new APERIODOFISCAL
                                  {idEmpresa = Memoria.Empresa, idFilial = Memoria.Filial, dataInicio = DateTime.Now};

                        bool result = ctrl.Criar(obj);

                        RadWindow.Alert(result
                                            ? "Por não haver nenhum período fiscal atual, um novo foi aberto."
                                            : "Erro ao abrir período fiscal.");
                    }

                    ControlePagina.NavigateTo(PaginaCore.PgCaixa_Mesas);

                    Close(false);
                }
                else
                {
                    RadWindow.Alert("Usuário não possui perfil no sistema.");
                }
            }
            else
            {
                RadWindow.Alert("Usuário inválido.");
            }
        }

        private void txtbox_Senha_KeyDown(object sender, KeyEventArgs e)
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

        public void Close()
        {
            Application curApp = Application.Current;
            curApp.Shutdown();
        }

        public void Close(bool fechaAplicativo)
        {
            if (fechaAplicativo)
            {
                Close();
            }
        }
    }
}