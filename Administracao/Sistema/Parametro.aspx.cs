using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Reserva;
using Ext.Net;

namespace Artebit.Restaurante.Administracao.Sistema
{
    public partial class Parametro : System.Web.UI.Page
    {


        #region Page Load/Unload e Carregamento Global

        protected void Page_Unload(object sender, EventArgs e)
        {
            Contexto.FecharContexto();
        }

        private void VerificarPermissoes()
        {
            var perfil = new GPERFIL();
            var per = new Global.RegrasNegocio.Global.PerfilControl();
            perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
            perfil = (GPERFIL)per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
            var compl = new List<string> { "AD", "Parâmetros", "0" };
            bool resultado = false;

            compl[2] = "2";
            resultado = (bool)per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

            if (!resultado)
            {
                btSalvar.Enabled = false;
            }else
            {
                btSalvar.Enabled = true;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            MemoriaWeb.ValidaSessao();

            Contexto.AbrirContexto();

            if (!IsPostBack)
            {
                VerificarPermissoes();
                CarregaDados();
            }
        }


        // Função que carrega dados
        protected void CarregaDados()
        {
            RPARAM param = new RPARAM();
            Parametros control = new Parametros();

            param = (RPARAM)control.ExecutaFuncao(param, Funcoes.Buscar, null);

            r_horariolimite.Text = param.horFinReserva;
            r_assuntoEmail1.Text = param.assuntoEmail1;
            r_assuntoEmail2.Text = param.assuntoEmail2;
            r_corpoEmail1.Text = param.corpoEmail1;
            r_corpoEmail2.Text = param.corpoEmail2;
        }

        #endregion


        // Função para salvar todos os parâmetros
        protected void salvar(object sender, DirectEventArgs e)
        {
            string msg = "";

            try
            {
                RPARAM param = new RPARAM();
                Parametros control = new Parametros();

                param = (RPARAM) control.ExecutaFuncao(param, Funcoes.Buscar, null);

                param.horFinReserva = r_horariolimite.Text;
                param.assuntoEmail1 = r_assuntoEmail1.Text;
                param.assuntoEmail2 = r_assuntoEmail2.Text;
                param.corpoEmail1 = r_corpoEmail1.Text;
                param.corpoEmail2 = r_corpoEmail2.Text;

                bool rconf = false;

                rconf = (bool) control.ExecutaFuncao(param, Funcoes.Atualizar, null);

                if (!rconf)
                {
                    msg = "Erro ao atualizar parâmetros de resevas.";
                }

                if (msg == "")
                {
                    msg = "Parâmetros atualizados com sucesso.";
                }
            }
            catch (Exception ex)
            {
                Excecao.TratarExcecao(ex);
                msg = "Erro ao executar tarefa.";
            }


            X.Msg.Alert("Alerta", msg).Show();
        }

    }
}