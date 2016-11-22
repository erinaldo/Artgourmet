using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Ext.Net;
using System.Web.Security;

namespace Artebit.Restaurante.Administracao
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MemoriaWeb.ValidaSessao();

            Contexto.AbrirContexto();

            if (Memoria.Empresa == 0)
            {
                // Chama função para carregar dados na combo
                carregaDropEmpresa();

                // Chama a Window
                Window1.Show();
            }
        }


        protected void Page_Unload(object sender, EventArgs e)
        {
            Contexto.Atual.Dispose();
        }

        // Função para carregar dados da empresa na combo
        protected void carregaDropEmpresa()
        {
            // Empresa
            var empresa = new EmpresaControl();
            var emp = new GEMPRESA();
            var compl = new List<string>();

            var lista_empresa =
                (IQueryable<GEMPRESA>) empresa.ExecutaFuncao(emp, Funcoes.BuscarLista, compl);
            var dados = from a in lista_empresa
                        select new
                                   {
                                       idEmp = a.idEmpresa,
                                       nomeFantasia = a.nomeFantasia
                                   };

            store_empresa.DataSource = dados;
            store_empresa.DataBind();

            if (Memoria.Empresa != 0)
                combo_empresa.Value = Convert.ToString(Memoria.Empresa);

        }


        // Função para salvar nova empresa
        protected void funcao_empresa(object sender, DirectEventArgs e)
        {
            if (this.combo_empresa.Value == "" || this.combo_empresa.Value == null)
            {
                X.Msg.Alert("Alerta", "Escolha a empresa").Show();
            }
            else
            {

                if (this.combo_filial.Value == "" || this.combo_filial.Value == null)
                {
                    X.Msg.Alert("Alerta", "Escolha a Filial").Show();
                }
                else
                {
                    string[] cookie =
                        Convert.ToString(HttpContext.Current.Request.ServerVariables["AUTH_USER"]).Split('ª');

                    FormsAuthentication.SetAuthCookie(combo_empresa.Value + "ª" + cookie[1], true);

                    Memoria.Empresa = Convert.ToInt32(this.combo_empresa.Value);
                    HttpContext.Current.Session["Artebit.Empresa"] = Memoria.Empresa;

                    Memoria.Filial = Convert.ToInt32(this.combo_filial.Value);
                    HttpContext.Current.Session["Artebit.Filial"] = Memoria.Filial;

                    var perfil = new GPERFIL();
                    var control = new PerfilControl();

                    perfil = (GPERFIL) control.ExecutaFuncao(null, Funcoes.BuscarItem, null);

                    Memoria.Perfil = perfil.idPerfil;
                    HttpContext.Current.Session["Artebit.Perfil"] = Memoria.Perfil;


                    //Fecha Window
                    Window1.Hide();
                }
            }
        }


        // Função para trocar de filial, chamada no menu
        [DirectMethod]
        public void TrocarFilial()
        {
            carregaDropEmpresa();

            trocaFilial();

            Window1.Show();
        }


        // Função para carregar as filias de acordo com a empresa escolhida
        protected void trocaFilial()
        {
            var empresa = new FilialControl();
            var emp = new GFILIAL();
            var compl = new List<string>();

            if (Memoria.Empresa != 0)
                emp.idEmpresa = Convert.ToInt32(Memoria.Empresa);
            else
                emp.idEmpresa = Convert.ToInt32(this.combo_empresa.Value);


            var lista =
                (IQueryable<GFILIAL>) empresa.ExecutaFuncao(emp, Funcoes.BuscarListaEspecifica, compl);
            var dados = from a in lista
                        select new
                                   {
                                       id = a.idFilial,
                                       nome = a.nome
                                   };

            store_filial.DataSource = dados;
            store_filial.DataBind();
            combo_filial.Value = Convert.ToString(dados.FirstOrDefault().id);

            if (Memoria.Filial != 0)
                combo_filial.Value = Convert.ToString(Memoria.Filial);
        }

        // Fecha sistema e vai para login
        protected void funcao_cancelar(object sender, DirectEventArgs e)
        {
            Response.Redirect("Login.aspx");
            HttpContext.Current.Request.ServerVariables["AUTH_USER"] = "";
        }


        //  Função chamada quando a empresa é escolhida
        protected void atualizaFilial(object sender, RemoteValidationEventArgs e)
        {
            trocaFilial();

            e.Success = true;
        }

            
    }
}
