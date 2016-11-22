using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Artebit.Restaurante.Global.Util;

namespace Artebit.Restaurante.Administracao
{
    public partial class AltSenha : System.Web.UI.Page
    {
        Artebit.Restaurante.Global.Util.Criptografia cript = new Artebit.Restaurante.Global.Util.Criptografia();
        UsuarioControl vendedor = new UsuarioControl();

        public string Resultado = "";
        public string msg = "";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void btOKClick(object sender, EventArgs e)
        {
            string senha = Criptografia.GerarSHA1(Criptografia.GerarMD5(txt_senha.Text));

            GUSUARIO usu =
                (from p in Contexto.Atual.GUSUARIO select p).Where(r => r.codusuario == Memoria.Codusuario).
                    FirstOrDefault();

            if (senha == usu.senha)
            {
                msg = "A nova senha nao pode ser igual a anterior.";
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
                    msg = "Erro ao atualizar o usu�rio.";
                }
                else
                {
                    Resultado = usu.senha;
                }
            }
        }
    }
}