using System;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Ext.Net;

namespace Artebit.Restaurante.Administracao.Sistema
{
    public partial class Empresa : System.Web.UI.Page
    {

        protected void Page_Unload(object sender, EventArgs e)
        {
            Contexto.FecharContexto();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            MemoriaWeb.ValidaSessao();

            Contexto.AbrirContexto();

            if (!IsPostBack)
            {
                CarregaPrincipal();
            }
        }

        // Função para trazer os dados
        private void CarregaPrincipal()
        {
            Global.RegrasNegocio.Global.EmpresaControl control = new Global.RegrasNegocio.Global.EmpresaControl();
            IQueryable<GEMPRESA> lista = (IQueryable<GEMPRESA>) control.ExecutaFuncao(null, Funcoes.BuscarLista, null);

            var dados = from a in lista
                        select new
                                   {
                                       codigo = a.idEmpresa,
                                       nome = a.nomeFantasia,
                                       cnpj = a.cnpj,
                                       nomeEmpresarial = a.nomeEmpresarial,
                                       razaoSocial = a.RazaoSocial
                                   };

            StorePrincipal.DataSource = dados;
            StorePrincipal.DataBind();
        }

        // Função que salva os dados no banco
        protected void SalvarDados(object sender, EventArgs e)
        {
            GEMPRESA obj = new GEMPRESA();
            Global.RegrasNegocio.Global.EmpresaControl control = new Global.RegrasNegocio.Global.EmpresaControl();


            obj.idEmpresa = Memoria.Empresa;
            obj.nomeFantasia = Convert.ToString(txtNome.Value);
            obj.cnpj = Convert.ToString(txtCNPJ.Value);
            obj.InscricaoEstadual = Convert.ToString(txtInsEstadual.Value);
            obj.InscricaoMunicipal = Convert.ToString(txtInsMunicipal.Value);
            obj.RazaoSocial = Convert.ToString(txtRazaoSocial.Value);


            bool result = (bool) control.ExecutaFuncao(obj, Funcoes.Atualizar, null);
            CarregaPrincipal();

            if (result)
            {
                X.Msg.Alert("Alerta", "Dados gravados com sucesso").Show();
                JanelaPrincipal.Hide();
            }

            else
                X.Msg.Alert("Alerta", "Verifique os dados digitados e tente novamente").Show();
        }   
        
        // Função para carregar os dados no formulário
        protected void GridAcao(object sender, DirectEventArgs e)
        {
            int id = Convert.ToInt32(e.ExtraParams["codigo"]);
            string comando = Convert.ToString(e.ExtraParams["command"]);


            #region Editar/Visualizar

            if (comando == "editar" && btnEditarG.Disabled == false)
            {
                //-------------------------------------------------------------------------------------------
                //Preenchendo os campos com as informações do Empresa
                FormPrincipal.AddScript(
                    "if(StoreFormulario.allData != null) { PagingToolbar1.changePage(StoreFormulario.allData.indexOfKey(" +
                    id.ToString() + ") + 1); }" + " else { PagingToolbar1.changePage(StoreFormulario.indexOfId(" +
                    id.ToString() +
                    ") + 1);}");
            }

            #endregion

        }


        #region StoreRefreshData Events

        // Função que carrega os dados do formulário
        protected void StoreFormulario_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            Global.RegrasNegocio.Global.EmpresaControl control = new Global.RegrasNegocio.Global.EmpresaControl();
            IQueryable<GEMPRESA> lista = (IQueryable<GEMPRESA>)control.ExecutaFuncao(null, Funcoes.BuscarLista, null);

            var dados = from a in lista
                        select new
                        {
                            codigo = a.idEmpresa,
                            nome = a.nomeFantasia,
                            cnpj = a.cnpj,
                            insEstadual = a.InscricaoEstadual,
                            insMunicipal = a.InscricaoMunicipal,
                            razaoSocial = a.RazaoSocial,
                            nomeEmpresarial = a.nomeEmpresarial,
                            logradouro = a.Logradouro,
                            numero = a.numero,
                            complemento = a.complemento,
                            cep = a.cep,
                            bairro = a.bairro,
                            municipio = a.municipio,
                            uf = a.uf
                        };

            StoreFormulario.DataSource = dados;
            StoreFormulario.DataBind();
        }

        #endregion

    }
}