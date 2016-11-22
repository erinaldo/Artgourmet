using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Artebit.Restaurante.Global.Util;

namespace Artebit.Restaurante.Caixa
{
    /// <summary>
    /// Interaction logic for Transferir.xaml
    /// </summary>
    public partial class AltSenha
    {
        private readonly UsuarioControl _vendedor = new UsuarioControl();

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

        private void btOK_Click(object sender, RoutedEventArgs e)
        {
            string senha = Criptografia.GerarSHA1(Criptografia.GerarMD5(txt_senha.Password));

            GUSUARIO usu =
                (from p in Contexto.Atual.GUSUARIO select p).FirstOrDefault(r => r.codusuario == Memoria.Codusuario);

            if (usu != null && senha == usu.senha)
            {
                Msg = "A nova senha não pode ser igual a anterior.";
            }
            else
            {
                if (usu != null)
                {
                    usu.senha = senha;
                    usu.dataUpdSenha = DateTime.Now;
                    usu.dataAlteracao = DateTime.Now;
                    usu.usuAlteracao = Convert.ToString(Memoria.Codusuario);
                    usu.usuAlteracao = Memoria.Codusuario;

                    bool resultado = _vendedor.Atualizar(usu);

                    if (resultado == false)
                    {
                        Msg = "Erro ao atualizar o usuário.";
                    }
                    else
                    {
                        Resultado = usu.senha;
                    }
                }
            }
            Close();
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            Resultado = "";
            Msg = "Senha Inválida";
            Close();
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