using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Ext.Net;

namespace Artebit.Restaurante.Administracao.Sistema
{
    public partial class Filial : System.Web.UI.Page
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
                VerificarPermissoes();
            }
        }


        private void VerificarPermissoes()
        {
            var perfil = new GPERFIL();
            var per = new Global.RegrasNegocio.Global.PerfilControl();
            perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
            perfil = (GPERFIL)per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
            var compl = new List<string> { "AD", "Filial", "0" };
            bool resultado = false;

           #region Adicionar
            compl[2] = "18";
            resultado = (bool)per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

            if (!resultado)
            {
                btnNovoG.Disabled = true;
            }
            #endregion

            #region Editar
            compl[2] = "2";
            resultado = (bool)per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

            if (!resultado)
            {
                btnEditarG.Disabled = true;
            }
            #endregion

        }
        
        
        // Função para trazer os dados
        private void CarregaPrincipal()
        {
            Global.RegrasNegocio.Global.FilialControl control = new Global.RegrasNegocio.Global.FilialControl();
            IQueryable<GFILIAL> lista = (IQueryable<GFILIAL>)control.ExecutaFuncao(null, Funcoes.BuscarLista, null);

            var dados = from a in lista
                        select new
                        {
                            codigo = a.idFilial,
                            nome = a.nome,
                            cnpj = a.cnpj,
                            nomeEmpresarial = a.nomeEmpresarial,
                            razaoSocial = a.razaoSocial
                        };

            StorePrincipal.DataSource = dados;
            StorePrincipal.DataBind();
        }

        // Função para carregar os dados no formulário
        protected void GridAcao(object sender, DirectEventArgs e)
        {
            int id = Convert.ToInt32(e.ExtraParams["codigo"]);
            string comando = Convert.ToString(e.ExtraParams["command"]);

            GFILIAL obj = new GFILIAL();
            Global.RegrasNegocio.Global.FilialControl ctrl = new Global.RegrasNegocio.Global.FilialControl();


            #region Editar

            if (comando == "editar" && btnEditarG.Disabled == false)
            {
                //-------------------------------------------------------------------------------------------
                //Preenchendo os campos com as informações do Empresa
                FormPrincipal.AddScript(
                    "if(StoreFormulario.allData != null) { PagingToolbar1.changePage(StoreFormulario.allData.indexOfKey(" +
                    id.ToString() + ") + 1); }" + " else { PagingToolbar1.changePage(StoreFormulario.indexOfId(" +
                    id.ToString() +
                    ") + 1);}");

                JanelaPrincipal.Show();
            }

            #endregion

            #region Novo Item

            if (comando == "novo")
            {
                IDictionary<string, string> registro = new Dictionary<string, string>();
                registro.Add("codigo", Contexto.GerarId("GFILIAL").ToString());
                registro.Add("hdtipo", "1");
                registro.Add("nome", "");
                registro.Add("cnpj", "");
                registro.Add("insEstadual", "");
                registro.Add("insMunicipal", "");
                registro.Add("razaoSocial", "");
                registro.Add("nomeFantasia", "");
                registro.Add("nomeEmpresarial", "");
                registro.Add("logradouro", "");
                registro.Add("numero", "");
                registro.Add("complemento", "");
                registro.Add("cep", "");
                registro.Add("bairro", "");
                registro.Add("municipio", "");
                registro.Add("uf", "");

                StoreFormulario.AddRecord(registro, true);
                StoreFormulario.CommitChanges();

                ////Mostrando a Janela
                JanelaPrincipal.Show();
                TabPanelForm.ActiveTabIndex = 0;
            }

            #endregion

        }


        #region Gravar Dados Formulário

        // Salva dados e fecha janela
        protected void btnOkFrm_Click(object sender, DirectEventArgs e)
        {
            SalvarDados(sender, e);
            JanelaPrincipal.Hide();
        }

        // Apenas salva dados
        protected void btnSalvarFrm_Click(object sender, DirectEventArgs e)
        {
            SalvarDados(sender, e);
        }

        // Função para salvar dados
        protected void SalvarDados(object sender, DirectEventArgs e)
        {
            GFILIAL obj = new GFILIAL();
            Global.RegrasNegocio.Global.FilialControl control = new Global.RegrasNegocio.Global.FilialControl();

            //----------------------------------------------------------------------------------------
            // hd_tipo.Value é um textbox oculto. Ele fica esático durante toda a execução do programa
            //----------------------------------------------------------------------------------------
            Funcoes acao;
            if (hd_tipo.Value.ToString() == "2") //editar
            {
                acao = Funcoes.Atualizar;
                obj.idFilial = Convert.ToInt32(codigo.Text);
                obj.idEmpresa = Memoria.Empresa;
                obj = (GFILIAL)control.ExecutaFuncao(obj, Funcoes.Buscar, null);
            }
            else
            {
                acao = Funcoes.Adicionar;
                obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);
            }


            //----------------------------------------------------------------------------------------
            //Recebendo todos os dados do usuário
            //----------------------------------------------------------------------------------------
            obj.idFilial = Convert.ToInt32(codigo.Text);
            obj.nome = Convert.ToString(txtNome.Value);
            obj.nomeFantasia = Convert.ToString(txtNomeFantasia.Value);
            obj.nomeEmpresarial = Convert.ToString(txtNomeEmpresarial.Value);
            obj.cnpj = Convert.ToString(txtCNPJ.Value);
            obj.inscricaoEstadual = Convert.ToString(txtInsEstadual.Value);
            obj.inscricaoMunicipal = Convert.ToString(txtInsMunicipal.Value);
            obj.razaoSocial = Convert.ToString(txtRazaoSocial.Value);
            obj.logradouro = Convert.ToString(txtLogradouro.Value);
            obj.numero = Convert.ToString(txtNumero.Value);
            obj.complemento = Convert.ToString(txtComplemento.Value);
            obj.cep = Convert.ToString(txtCEP.Value);
            obj.municipio = Convert.ToString(txtMunicipio.Value);
            obj.uf = Convert.ToString(txtUf.Value);
            obj.bairro = Convert.ToString(txtBairro.Value);


            //------------------------------------------------------------------------------------------
            //Salvar o objeto.
            //------------------------------------------------------------------------------------------
            bool? result = (bool)control.ExecutaFuncao(obj, acao, null);

            if (result == true)
            {
                X.Msg.Alert("Alerta", "Dados gravados com sucesso").Show();
                CarregaPrincipal();
                StoreFormulario_RefreshData(StoreFormulario, new StoreRefreshDataEventArgs());
            }
            else
                X.Msg.Alert("Alerta", "Verifique os dados digitados e tente novamente").Show();

        }

        #endregion


        #region StoreRefreshData Events

        // Função que carrega os dados do formulário
        protected void StoreFormulario_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            Global.RegrasNegocio.Global.FilialControl control = new Global.RegrasNegocio.Global.FilialControl();
            IQueryable<GFILIAL> lista = (IQueryable<GFILIAL>)control.ExecutaFuncao(null, Funcoes.BuscarLista, null);

            var dados = from a in lista
                        select new
                        {
                            codigo = a.idFilial,
                            nome = a.nome,
                            cnpj = a.cnpj,
                            insEstadual = a.inscricaoEstadual,
                            insMunicipal = a.inscricaoMunicipal,
                            razaoSocial = a.razaoSocial,
                            nomeFantasia = a.nomeFantasia,
                            nomeEmpresarial = a.nomeEmpresarial,
                            logradouro = a.logradouro,
                            numero = a.numero,
                            complemento = a.complemento,
                            cep = a.cep,
                            bairro = a.bairro,
                            municipio = a.municipio,
                            uf = a.uf,
                            hdtipo = 2
                        };

            StoreFormulario.DataSource = dados;
            StoreFormulario.DataBind();
        }

        #endregion

    }
}