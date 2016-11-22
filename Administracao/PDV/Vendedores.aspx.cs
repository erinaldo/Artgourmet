using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artebit.Restaurante.Global;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Estoque;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Artebit.Restaurante.Global.Util;
using Ext.Net;

namespace Administracao.PDV
{
    public partial class Vendedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            MemoriaWeb.ValidaSessao();
            Contexto.AbrirContexto();
            
            
            if (!IsPostBack)
            {
                VerificarPermissoes();
                CarregaPrincipal();
                CarregaComboCodUsuario();
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            Contexto.FecharContexto();
        }

        private void VerificarPermissoes()
        {
            var perfil = new GPERFIL();
            var per = new PerfilControl();
            perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
            perfil = (GPERFIL)per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
            var compl = new List<string> { "AD", "Vendedores", "0" };
            bool resultado = false;


            #region Ativar/Desativar Itens
            compl[2] = "19";//Ativar/Desativar
            resultado = (bool)per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

            if (!resultado)
            {
                btnAtivarDesativarG.Disabled = true;
            }
            #endregion

            #region Adicionar
            compl[2] = "18";
            resultado = (bool)per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

            if (!resultado)
            {
                btnNovoG.Disabled = true;
            }
            #endregion

            #region Excluir
            compl[2] = "20";
            resultado = (bool)per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

            if (!resultado)
            {
                btnExcluirG.Disabled = true;
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

        protected void CarregaPrincipal()
        {

            VendedorControl ct = new VendedorControl();

            IQueryable<GVENDEDOR> lista = (IQueryable<GVENDEDOR>) ct.ExecutaFuncao(null, Funcoes.BuscarLista, null);

            var dados = from a in lista
                        select new
                                   {
                                       codigo = a.idVen,
                                       nome = a.nome,
                                       codusu = a.codUsuario,
                                       senha = a.codigo,
                                       comissao = a.comissao,
                                       ativo = a.ativo == true ? "Ativo" : "Inativo"
                                   };

            StorePrincipal.DataSource = dados;
            StorePrincipal.DataBind();

        }

        protected void CarregaComboCodUsuario()
        {

            UsuarioControl usu = new UsuarioControl();
            IQueryable<GUSUARIO> lista = (IQueryable<GUSUARIO>) usu.ExecutaFuncao(null, Funcoes.BuscarLista, null);

            var dados = from a in lista
                        select new
                                   {
                                       cod = a.codusuario,
                                       nome = a.nome
                                   };

            StoreCodUsuario.DataSource = dados;
            StoreCodUsuario.DataBind();
        }


        // Função para carregar os dados no formulário
        protected void GridAcao(object sender, DirectEventArgs e)
        {
            int id = Convert.ToInt32(e.ExtraParams["codigo"]);
            string comando = Convert.ToString(e.ExtraParams["command"]);

            GVENDEDOR obj = new GVENDEDOR();
            VendedorControl ctrl = new VendedorControl();


            #region Editar
            if (comando == "editar" && btnEditarG.Disabled == false)
            {
                //-------------------------------------------------------------------------------------------
                //Preenchendo os campos com as informações do Vendedor
                FormPrincipal.AddScript(
                    "if(StoreFormulario.allData != null) { PagingToolbar1.changePage(StoreFormulario.allData.indexOfKey(" +
                    id.ToString() + ") + 1); }" + " else { PagingToolbar1.changePage(StoreFormulario.indexOfId(" + id.ToString() +
                    ") + 1);}");

                JanelaPrincipal.Show();
            }
            #endregion

            #region Excluir
            if (comando == "excluir")
            {
                bool res = (bool)ctrl.ExecutaFuncao(obj, Funcoes.Cancelar, null);

                if (res)
                    X.Msg.Alert("Alerta", "A Vendedor foi excluido").Show();

                CarregaPrincipal();
            }
            #endregion

            #region Ativar/Desativar
            if (comando == "ativar")
            {
                bool sucesso = true;

                foreach (var i in RowSelectionModel1.SelectedRows)
                {
                    id = Convert.ToInt32(i.RecordID);
                    GVENDEDOR ali = new GVENDEDOR();

                    ali.idVen = id;
                    ali = (GVENDEDOR)ctrl.ExecutaFuncao(ali, Funcoes.Buscar, null);

                    ali.ativo = !ali.ativo;
                    bool res = (bool)ctrl.ExecutaFuncao(ali, Funcoes.Atualizar, null);

                    if (!res)
                    {
                        sucesso = false;
                        break;
                    }
                }

                if (sucesso)
                {
                    CarregaPrincipal();
                    Notification.Show(new NotificationConfig
                    {
                        Title = "Informação",
                        Icon = Icon.Information,
                        Html = "Itens Ativados/Desativados com sucesso!"
                    });
                }
            }
            #endregion

            #region Novo Item
            if (comando == "novo")
            {
                IDictionary<string, string> registro = new Dictionary<string, string>();
                registro.Add("codigo", Contexto.GerarId("GVENDEDOR").ToString());
                registro.Add("nome", "");
                registro.Add("codusu", "");
                registro.Add("senha", "");
                registro.Add("comissao", "");
                registro.Add("ativo", "true");

                StoreFormulario.AddRecord(registro, true);
                StoreFormulario.CommitChanges();

                //Mostrando a Janela
                JanelaPrincipal.Show();
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
            GVENDEDOR obj = new GVENDEDOR();
            VendedorControl control = new VendedorControl();

            //----------------------------------------------------------------------------------------
            // hd_tipo.Value é um textbox oculto. Ele fica esático durante toda a execução do programa
            //----------------------------------------------------------------------------------------
            Funcoes acao;
            if (hd_tipo.Value.ToString() == "2") //editar
            {
                acao = Funcoes.Atualizar;
                obj.idVen = Convert.ToInt32(codigo.Text);
                obj.idEmpresa = Memoria.Empresa;
                obj.idFilial = Memoria.Filial;
                obj = (GVENDEDOR)control.ExecutaFuncao(obj, Funcoes.Buscar, null);
            }
            else
            {
                acao = Funcoes.Adicionar;
                obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);
            }


            //----------------------------------------------------------------------------------------
            //Recebendo todos os dados do usuário
            //----------------------------------------------------------------------------------------
            obj.idVen = Convert.ToInt32(codigo.Text);
            obj.nome= Convert.ToString(txtNome.Value);
            obj.codUsuario= Convert.ToString(ComboCodUsuario.Value);
            if (txtCodigo.Value != "" && txtCodigo.Value != null)
            {

                obj.codigo = Criptografia.GerarSHA1(Criptografia.GerarMD5(Convert.ToString(txtCodigo.Value)));
            }
            obj.comissao = Convert.ToDouble(txtComissao.Value);
            obj.ativo = CheckAtivo.Checked;

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
            VendedorControl ct = new VendedorControl();

            IQueryable<GVENDEDOR> lista = (IQueryable<GVENDEDOR>)ct.ExecutaFuncao(null, Funcoes.BuscarLista, null);

            var dados = from a in lista
                        select new
                        {
                            codigo = a.idVen,
                            nome = a.nome,
                            codusu = a.codUsuario,
                            senha = a.codigo,
                            comissao = a.comissao,
                            ativo = a.ativo,
                            hdtipo = 2
                        };

            StoreFormulario.DataSource = dados;
            StoreFormulario.DataBind();
        }

        #endregion
        
        
        
        
//        #region antigo

//        protected void Nova_Vendedor(object sender, EventArgs e)
//        {
//            //zerando os campos
//            txtNome.Value = "";
//            ComboCodUsuario.Value = "";
//            txtCodigo.Value = "";
//            txtComissao.Value = "";

//            hd_tipo.Value = 0;

//            JanelaPrincipal.Show();
//        }

//        protected void GridProdutosAcao(object sender, DirectEventArgs e)
//        {
//            using (Contexto.Atual = new Restaurante())
//            {
//                //Recebendo os parâmetros id da impressora e o comando acionado
//                int idVendedor = Convert.ToInt32(e.ExtraParams["id"]);
//                string comando = Convert.ToString(e.ExtraParams["command"]);

//                //Controle e objeto
//                GVENDEDOR obj = new GVENDEDOR();
//                Vendedor ctrl = new Vendedor();

//                //Buscando a impressora
//                obj.idVen = Convert.ToInt32(idVendedor);
//                obj = (GVENDEDOR) ctrl.ExecutaFuncao(obj, Funcoes.Buscar, null);

//                switch (comando)
//                {
//                    case "editar":
//                        {
//                            //Preenchendo o formulário
//                            txtNome.Value = obj.nome;
//                            ComboCodUsuario.Value = obj.codUsuario;
//                            txtCodigo.Value = obj.codigo;
//                            txtComissao.Value = obj.comissao;

//                            //campo oculto
//                            hd_tipo.Value = 1;
//                            codigo.Value = obj.idVen;


//                            //mostrando a janela
//                            JanelaPrincipal.Show();

//                            break;
//                        }
//                    case "ativar":
//                        {
//                            obj.ativo = true;
//                            bool res = (bool) ctrl.ExecutaFuncao(obj, Funcoes.Atualizar, null);
//                            CarregaPrincipal();

//                            if (res)
//                                X.Msg.Alert("Alerta", "O vendedor foi ativado").Show();
//                            break;
//                        }
//                    case "desativar":
//                        {
//                            obj.ativo = false;
//                            bool res = (bool) ctrl.ExecutaFuncao(obj, Funcoes.Atualizar, null);
//                            CarregaPrincipal();

//                            if (res)
//                                X.Msg.Alert("Alerta", "O vendedor foi desativado").Show();
//                            break;
//                        }
//                }
//            }
//        }

//        protected bool ValidaDados()
//        {
//            if (hd_tipo.Value.ToString() == "1")
//            {
//                GVENDEDOR obj = new GVENDEDOR();
//                Vendedor ct = new Vendedor();
//                obj.idVen = Convert.ToInt32(codigo.Value);
//                obj = (GVENDEDOR) ct.ExecutaFuncao(obj, Funcoes.Buscar, null);

//                if (txtCodigo.Text != obj.codigo)
//                {
//                    if (
//                        Contexto.Atual.GVENDEDOR.Any(
//                            r =>
//                            r.idEmpresa == Memoria.Empresa && r.idFilial == Memoria.Filial &&
//                            r.codigo == txtCodigo.Value))
//                    {
//                        X.Msg.Alert("Alerta", "Código digitado inválido. Insira outro código").Show();
//                        return false;
//                    }
//                    else
//                    {
//                        return true;
//                    }
//                }
//            }
//            else
//            {
//                if (
//                    Contexto.Atual.GVENDEDOR.Any(
//                        r =>
//                        r.idEmpresa == Memoria.Empresa && r.idFilial == Memoria.Filial &&
//                        r.codigo == txtCodigo.Value))
//                {
//                    X.Msg.Alert("Alerta", "Código digitado inválido. Insira outro código").Show();
//                    return false;
//                }
//            }
//            return true;
//        }


//        protected void SalvarVendedor(object sender, EventArgs e)
//        {
//            using (Contexto.Atual = new Restaurante())
//            {

//                if (ValidaDados())
//                {

//                    GVENDEDOR obj = new GVENDEDOR();
//                    Vendedor ct = new Vendedor();
//                    Funcoes acao = Funcoes.Adicionar;

//                    if (hd_tipo.Value.ToString() == "1") //editar
//                    {
//                        obj.idVen = Convert.ToInt32(codigo.Value);
//                        acao = Funcoes.Atualizar;
//                        obj = (GVENDEDOR) ct.ExecutaFuncao(obj, Funcoes.Buscar, null);
//                    }

//                    //Recebendo os dados digitados pelo usuário
//                    obj.codUsuario = Convert.ToString(ComboCodUsuario.Value);
//                    obj.codigo = Convert.ToString(txtCodigo.Value);
//                    obj.comissao = Convert.ToDouble(txtComissao.Value);
//                    obj.nome = Convert.ToString(txtNome.Value);


//                    bool result = (bool) ct.ExecutaFuncao(obj, acao, null);
//                    CarregaPrincipal();

//                    if (result)
//                    {
//                        JanelaPrincipal.Hide();
//                        X.Msg.Alert("Alerta", "Dados gravados com sucesso").Show();
//                    }

//                    else
//                        X.Msg.Alert("Alerta", "Verifique os dados digitados e tente novamente").Show();
//                }
//            }
//        }

//#endregion


    }
}