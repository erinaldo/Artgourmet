using System;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Artebit.Restaurante.Global.Util;

namespace Artebit.Restaurante.Caixa.Caixas.Janelas
{
    /// <summary>
    /// Interaction logic for UsuarioPermissao.xaml
    /// </summary>
    public partial class UsuarioPermissao
    {
        public string Permissao;

        public UsuarioPermissao(string permissao)
        {
            InitializeComponent();

            Permissao = permissao;

            int perm = Convert.ToInt32(Permissao);

            var lista =
                Contexto.Atual.GUSRFILMOD.Where(
                    r => r.codSistema == "C"
                         &&
                         r.GPERFIL.GPERMISSAO.Any(
                             z =>
                             z.idFuncionalidade == perm && z.GJANELA.nomeJanela == "Mesa" && z.GJANELA.codSistema == "C"))
                    .Select(p => p.GUSUARIO);

            cbUsuario.ItemsSource = lista;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string username = Convert.ToString(cbUsuario.SelectedValue);
            var usu = new GUSUARIO();
            var usuario = new UsuarioControl();
            usu.codusuario = username;
            usu.senha = Criptografia.GerarSHA1(Criptografia.GerarMD5(txtSenha.Password));


            usu = usuario.Verificar(usu);

            if (usu != null)
            {
                //pega a lista de sistemas
                string[] sistemas = Convert.ToString(ConfigurationManager.AppSettings["CODSISTEMA"]).Split(',');

                Memoria.Codusuario = usu.codusuario;

                IQueryable<GUSRFILMOD> lista =
                    Contexto.Atual.GUSRFILMOD.Where(
                        r => r.codUsuario == usu.codusuario && sistemas.Contains(r.codSistema));

                if (lista.Any())
                {
                    GPERFIL perfil = lista.First().GPERFIL;
                    var per = new PerfilControl();

                    bool acesso = per.Verificar(perfil, "Mesa", Convert.ToInt32(Permissao));

                    if (acesso)
                    {
                        DialogResult = true;
                        Close();
                    }
                    else
                    {
                        Alert("Usuário não possui permissão para acessar essa função.");
                    }
                }
                else
                {
                    Alert("Usuário não possui perfil no sistema.");
                }
            }
            else
            {
                Alert("Usuário inválido.");
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

            Close();
        }

        private void txtSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                btnOK_Click(btnOK, new RoutedEventArgs());
            }
        }
    }
}