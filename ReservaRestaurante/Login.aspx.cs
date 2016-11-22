using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Ext.Net;
using Artebit.Restaurante.Global.RegrasNegocio;

namespace Artebit.Restaurante.Reserva
{
    public partial class Login : System.Web.UI.Page
    {
        Artebit.Restaurante.Global.Util.Criptografia cript = new Artebit.Restaurante.Global.Util.Criptografia();
        Usuario vendedor = new Usuario();

        public string Resultado = "";
        public string msg = "";

        protected void Page_Load(object sender, EventArgs e)
        {
             
        }

        public void btOKClick(object sender, EventArgs e)
        {
            using (Contexto.Atual = new Global.Modelo.Restaurante())
            {
                string senha = cript.GerarSHA1(cript.GerarMD5(txt_senha.Text));

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

                    bool resultado = (bool)vendedor.ExecutaFuncao(usu, Funcoes.Atualizar, null);

                    if (resultado == false)
                    {
                        msg = "Erro ao atualizar o usuário.";
                        X.Msg.Alert("Alerta", "Erro ao atualizar o usuário.").Show();
                    }
                    else
                    {
                        //X.Msg.Alert("Alerta", "Senha atualizada.");
                        pss.Hide();
                        Response.Redirect("Default.aspx");
                    }
                }
            }
        }
    }
}