using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artebit.Restaurante.Global.Util;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Artebit.Restaurante.Global.Util.WPF;
using Ext.Net;
using Artebit.Restaurante.Global.RegrasNegocio;

namespace Artebit.Restaurante.Administracao
{
    public partial class Login : System.Web.UI.Page
    {
        UsuarioControl vendedor = new UsuarioControl();

        public string Resultado = "";
        public string msg = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
        }

        protected void Logar(object sender, DirectEventArgs e)
        {
            using (Contexto.Atual = new Global.Modelo.Restaurante())
            {

                string usuario_nome = this.nomeUsuario.Text;
                string usuario_senha = Criptografia.GerarSHA1(Criptografia.GerarMD5(this.senhaUsuario.Text));

                GUSUARIO usuario = new GUSUARIO();
                UsuarioControl usu = new UsuarioControl();

                usuario.codusuario = usuario_nome;
                usuario.senha = usuario_senha;

                string[] sistemas =
                    Convert.ToString(ConfigurationManager.AppSettings["CODSISTEMA"]).Split(',');
                Memoria.CodSistema = sistemas[0];

                usuario = (GUSUARIO)usu.ExecutaFuncao(usuario, Funcoes.Verificar, null);
                Memoria.Filial = 0;

                if (usuario != null)
                {
                    //Memoria.Empresa = Convert.ToInt32(combo_empresa.SelectedItem.Value);
                    Memoria.Empresa = 0;
                    FormsAuthentication.SetAuthCookie(Memoria.Empresa.ToString() + "ª" + usuario_nome, true);

                    MemoriaWeb.CriaSessao(usuario);

                    bool? valsenha = (bool)usu.ExecutaFuncao(usuario, Funcoes.ValidaSenha, null);

                    if (valsenha == false)
                    {
                        //WindowUtil.MostraModal();
                        pss.Show();
                        pss.Hidden = false;
                        //if (msg != "")
                        //{
                        //    X.Msg.Alert("Alerta", msg);
                        //    pss.Hide();
                        //    return;
                        //}
                        //else
                        //{
                        //    usuario.senha = Resultado;
                        //    pss.Hide();
                        //    //WindowUtil.FechaModal();
                        //}
                    } else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
                else
                {
                    X.Msg.Alert("Erro",
                                " O usuário não existe ou não tem permissão de acesso no sistema, consute o administrador.")
                        .Show();
                }
            }


        }


        [DirectMethod]
        public void LogIn()
        {
            Logar(botaoLogar, new DirectEventArgs(new Ext.Net.ParameterCollection()));
        }


        // Função para carregar dados da empresa na combo
        protected void carregaDropEmpresa()
        {
            using (Contexto.Atual = new Global.Modelo.Restaurante())
            {
                // Empresa
                EmpresaControl empresa = new EmpresaControl();
                GEMPRESA emp = new GEMPRESA();
                List<string> compl = new List<string>();

                IQueryable<GEMPRESA> lista_empresa =
                    (IQueryable<GEMPRESA>)empresa.ExecutaFuncao(emp, Funcoes.BuscarLista, compl);
                var dados = from a in lista_empresa
                            select new
                            {
                                idEmp = a.idEmpresa,
                                nomeFantasia = a.nomeFantasia
                            };

                store_empresa.DataSource = dados;
                //combo_empresa.SelectedItem.Value = "1";
                store_empresa.DataBind();
            }
        }

        public void btOKClick(object sender, EventArgs e)
        {
            using (Contexto.Atual = new Global.Modelo.Restaurante())
            {
                string senha = Criptografia.GerarSHA1(Criptografia.GerarMD5(txt_senha.Text));

                GUSUARIO usu =
                    (from p in Contexto.Atual.GUSUARIO select p).Where(r => r.codusuario == Memoria.Codusuario).
                        FirstOrDefault();

                if (senha == usu.senha)
                {
                    msg = "A nova senha não pode ser igual a anterior.";
                    X.Msg.Alert("Alerta", msg).Show();
                }
                else
                {

                    usu.senha = senha;
                    usu.dataUpdSenha = DateTime.Now;
                    usu.dataAlteracao = DateTime.Now;
                    usu.usuAlteracao = Convert.ToString(Memoria.Codusuario);
                    usu.usuAlteracao = Memoria.Codusuario;

                    bool resultado = (bool) vendedor.ExecutaFuncao(usu, Funcoes.Atualizar, null);

                    if (resultado == false)
                    {
                        msg = "Erro ao atualizar o usuário.";
                        X.Msg.Alert("Alerta", "Erro ao atualizar o usuário.").Show();
                    }
                    else
                    {
                        X.Msg.Alert("Alerta", "Senha atualizada.").Show();
                        pss.Hide();
                        Response.Redirect("Default.aspx");
                    }
                }
            }
        }
    }
}