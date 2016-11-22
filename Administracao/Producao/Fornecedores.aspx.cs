using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Ext.Net;

namespace Artebit.Restaurante.Administracao.Producao
{
    public partial class Fornecedores : System.Web.UI.Page
    {
        List<GCLIFOR> listaClifor = null;

        protected void Page_unload(object sender, EventArgs e)
        {
            Contexto.Atual.Dispose();
        }

        protected void Page_load(object sender, EventArgs e)
        {
            MemoriaWeb.ValidaSessao();

            Contexto.Atual = new Global.Modelo.Restaurante();
        }

        private void LoadListaClifor()
        {
            if (listaClifor == null)
            {
                FornecedorControl control = new FornecedorControl();
                listaClifor = ((IQueryable<GCLIFOR>)control.ExecutaFuncao(null, Funcoes.BuscarLista, null)).ToList();
            }
        }

        private void CarregaFornecedores()
        {
            LoadListaClifor();

            var dados = from a in listaClifor
                        select new
                                   {
                                       codigo = a.idClifor,
                                       nome = a.nomeFantasia,
                                       cpfcnpj = a.cpfcpnj,
                                       razao = a.razaoSocial,
                                       classificacao = a.GTPCLIFOR.descricao,
                                       categoria = a.tipoPessoa == "1" ? "Pessoa Jurídica" : "Pessoa Física",
                                       criacao = string.Format("{0:dd-MM-yyyy}",a.dataCriacao),
                                       insEstadual = a.insEstadual,
                                       insMunicipal = a.insMunicipal,
                                       ativo = a.ativo == true ? "Ativo" : "Inativo",
                                   };


            storeListaFornecedores.DataSource = dados;
            storeListaFornecedores.DataBind();

        }

        protected void CarregaGridPrincipal(object sender, StoreRefreshDataEventArgs e)
        {
            CarregaFornecedores();
        }

        protected void LoadStorePrincipal(object sender, EventArgs e)
        {
            CarregaFornecedores();
        }

        protected void FetchRecord(object sender, StoreRefreshDataEventArgs e)
        {
            LoadListaClifor();

            var dados2 = from a in listaClifor
                         select new
                         {
                             codigo = a.idClifor,
                             nome = a.nomeFantasia,
                             razao = a.razaoSocial,
                             cpfcnpj = a.cpfcpnj,
                             cnae = a.cnae,
                             insEstadual = a.insEstadual,
                             insMunicipal = a.insMunicipal,
                             insSuframa = a.insSuframa,
                             classificacao = a.tpClifor,
                             categoria = a.tipoPessoa,
                             ativo = a.ativo,

                             //endereco principal
                             endPrincipal = a.GCLIFOREND.Where(r => r.tipoEndereco == 1).FirstOrDefault(),

                             //endereco pagamento
                             endPagamento = a.GCLIFOREND.Where(r => r.tipoEndereco == 2).FirstOrDefault(),

                             //endereco entrega
                             endEntrega = a.GCLIFOREND.Where(r => r.tipoEndereco == 3).FirstOrDefault(),

                             novoRegistro = a.idEmpresa == 0 ? 2 : 1

                         };

            this.storeGeral.DataSource = dados2;
            this.storeGeral.DataBind();
        }

        private void NovoRegistro()
        {
            LoadListaClifor();

            GCLIFOR novo = new GCLIFOR();

            novo.idClifor = Contexto.GerarId("GCLIFOR");
            novo.idEmpresa = 0;
            novo.ativo = true;
            novo.tpClifor = 2;

            listaClifor.Add(novo);

            FetchRecord(storeGeral, new StoreRefreshDataEventArgs());
            PagingToolbar1.PageIndex = listaClifor.Count;
        }

        protected void AddClick(object sender, DirectEventArgs e)
        {
            NovoRegistro();
            FormPanel1.AddScript("#{FormPanel1}.body.unmask();");
        }

        protected void ExcluirClick(object sender, DirectEventArgs e)
        {
            Excluir(Convert.ToInt32(txtIdClifor.Value));
            FormPanel1.AddScript("#{FormPanel1}.body.unmask();");
        }

        [DirectMethod]
        public void ExcluirVarios()
        {
            bool sucesso = true;
            foreach (var i in smGrid.SelectedRows)
            {
                int id = Convert.ToInt32(i.RecordID);
                if (!Excluir(id))
                {
                    sucesso = false;
                    break;
                }
            }

            if (sucesso)
            {
                Notification.Show(new NotificationConfig
                {
                    Title = "Informação",
                    Icon = Icon.Information,
                    Html = "Itens Excluídos com sucesso!"
                });
            }
        }  

        protected void AbrirJanela(object sender, DirectEventArgs e)
        {
            GPERFIL perfil = new GPERFIL();
            PerfilControl per = new PerfilControl();
            perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
            perfil = (GPERFIL)per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
            List<string> compl = new List<string>();
            compl.Add("AD");
            compl.Add("Fornecedores");
            compl.Add("0");

            compl[2] = "18";
            bool result = (bool)per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

            if (result)
            {
                TabPanel1.SetActiveTab(0);
                WindowForm.Show();

                NovoRegistro();
            }
            else
            {
                X.Msg.Alert("Alerta", "Você não tem permissão de executar essa ação contate o administrador").Show();
            }
        }

        protected void AbrirJanelaEdita(object sender, DirectEventArgs e)
        {
            int idFornecedor = Convert.ToInt32(e.ExtraParams["codigo"]);

            FetchRecord(storeGeral, new StoreRefreshDataEventArgs());

            GCLIFOR fornecedor = listaClifor.Where(r => r.idClifor == idFornecedor).FirstOrDefault();

            int index = listaClifor.IndexOf(fornecedor);

            PagingToolbar1.SetPageIndex(
                   Convert.ToInt32(index) + 1);

            TabPanel1.SetActiveTab(0);
            WindowForm.Show();
        }

        protected void SalvarDados(object sender, DirectEventArgs e)
        {
            /*
             * valores para o campo hd_tipo:
             * 1 = editado
             * 2 = novoItem
             */

            //using (Contexto.Atual = new Restaurante())
            //{
            GCLIFOR obj = new GCLIFOR();
            FornecedorControl control = new FornecedorControl();

            Funcoes acao = Funcoes.Adicionar;
            
            obj.idClifor = Convert.ToInt32(txtIdClifor.Value);
            obj.idEmpresa = Memoria.Empresa;
            
            if (hd_tipo.Value.ToString() == "1") // O objeto foi editado.
            {   
                obj = (GCLIFOR)control.ExecutaFuncao(obj, Funcoes.Buscar, null);
                acao = Funcoes.Atualizar;
            }

            obj.idEmpresa = Memoria.Empresa;
            obj.nomeFantasia = Convert.ToString(txtNomeFantasia.Value);
            obj.razaoSocial = Convert.ToString(txtRazaoSocial.Value);
            obj.cpfcpnj = Convert.ToString(txtCGC.Value);
            obj.cnae = Convert.ToString(txtCNAE.Value);
            obj.insEstadual = Convert.ToString(txtIEstadual.Value);
            obj.insMunicipal = Convert.ToString(txtIMunicipal.Value);
            obj.insSuframa = Convert.ToString(txtISuframa.Value);
            obj.tpClifor = Convert.ToInt32(rdClassificacao.CheckedItems[0].InputValue);
            obj.tipoPessoa = Convert.ToInt32(rdTipoPessoa.CheckedItems[0].InputValue).ToString();
            obj.ativo = checkAtivo.Checked;

            //Endereço de Principal
            GCLIFOREND endPrincipal = obj.GCLIFOREND.Where(r => r.tipoEndereco == 1).FirstOrDefault();
            if (endPrincipal == null)
            {
                endPrincipal = new GCLIFOREND();
                endPrincipal.idEmpresa = obj.idEmpresa;
                endPrincipal.idClifor = obj.idClifor;
                endPrincipal.tipoEndereco = 1;
                endPrincipal.nuEndereco = 1;

                obj.GCLIFOREND.Add(endPrincipal);
            }

            endPrincipal.cep = Convert.ToString(txtPrCEP.Value);
            endPrincipal.rua = Convert.ToString(txtPrRua.Value);
            endPrincipal.numero = Convert.ToString(txtPrNumero.Value);
            endPrincipal.bairro = Convert.ToString(txtPrBairro.Value);
            endPrincipal.cidade = Convert.ToString(txtPrCidade.Value);
            endPrincipal.complemento = Convert.ToString(txtPrComplemento.Value);
            endPrincipal.uf = Convert.ToString(txtPrUF.Value);
            endPrincipal.telefone1 = Convert.ToString(txtPrTelefone.Value);
            endPrincipal.telefone2 = Convert.ToString(txtPrCelular.Value);
            endPrincipal.email = Convert.ToString(txtPrEmail.Value);

            //Endereço de Pagamento
            GCLIFOREND endPagamento = obj.GCLIFOREND.Where(r => r.tipoEndereco == 2).FirstOrDefault();
            if (endPagamento == null)
            {
                endPagamento = new GCLIFOREND();
                endPagamento.idEmpresa = obj.idEmpresa;
                endPagamento.idClifor = obj.idClifor;
                endPagamento.tipoEndereco = 2;
                endPagamento.nuEndereco = 2;

                obj.GCLIFOREND.Add(endPagamento);
            }

            endPagamento.cep = Convert.ToString(txtPaCEP.Value);
            endPagamento.rua = Convert.ToString(txtPaRua.Value);
            endPagamento.numero = Convert.ToString(txtPaNumero.Value);
            endPagamento.bairro = Convert.ToString(txtPaBairro.Value);
            endPagamento.cidade = Convert.ToString(txtPaCidade.Value);
            endPagamento.complemento = Convert.ToString(txtPaComplemento.Value);
            endPagamento.uf = Convert.ToString(txtPaUF.Value);
            endPagamento.telefone1 = Convert.ToString(txtPaTelefone.Value);
            endPagamento.telefone2 = Convert.ToString(txtPaCelular.Value);
            endPagamento.email = Convert.ToString(txtPaEmail.Value);

            //Endereço de Entrega
            GCLIFOREND endEntrega = obj.GCLIFOREND.Where(r => r.tipoEndereco == 3).FirstOrDefault();
            if (endEntrega == null)
            {
                endEntrega = new GCLIFOREND();
                endEntrega.idEmpresa = obj.idEmpresa;
                endEntrega.idClifor = obj.idClifor;
                endEntrega.tipoEndereco = 3;
                endEntrega.nuEndereco = 3;
                obj.GCLIFOREND.Add(endEntrega);

            }

            endEntrega.cep = Convert.ToString(txtEnCEP.Value);
            endEntrega.rua = Convert.ToString(txtEnRua.Value);
            endEntrega.numero = Convert.ToString(txtEnNumero.Value);
            endEntrega.bairro = Convert.ToString(txtEnBairro.Value);
            endEntrega.cidade = Convert.ToString(txtEnCidade.Value);
            endEntrega.complemento = Convert.ToString(txtEnComplemento.Value);
            endEntrega.uf = Convert.ToString(txtEnUF.Value);
            endEntrega.telefone1 = Convert.ToString(txtEnTelefone.Value);
            endEntrega.telefone2 = Convert.ToString(txtEnCelular.Value);
            endEntrega.email = Convert.ToString(txtEnEmail.Value);

            
            //Insumos fornecidos
            obj.EPRODUTO.Clear();
            
            string j = e.ExtraParams["Insumos"];
            List<object> items = JSON.Deserialize<List<object>>(j);
            foreach (object a in items)
            {
                int codigo = Convert.ToInt32(Newtonsoft.Json.Linq.JContainer.FromObject(a)["codigo"].ToString());
                
                obj.EPRODUTO.Add(Contexto.Atual.EPRODUTO.SingleOrDefault(r=> r.idProduto == codigo && r.idEmpresa == Memoria.Empresa));
            }
            
            bool result = (bool)control.ExecutaFuncao(obj, acao, null);


            if (result)
            {
                FormPanel1.AddScript("storeInsumosFornecedor.commitChanges(); storeInsumos.commitChanges(); storeGeral.reload();");
                //FetchRecord(storeGeral, new StoreRefreshDataEventArgs());
                FormPanel1.AddScript("#{FormPanel1}.body.unmask();");
                hd_tipo.Value = "1";
            }
            else
            {
                X.Msg.Alert("Alerta", "Verifique os dados digitados e tente novamente").Show();
                FormPanel1.AddScript("#{FormPanel1}.body.unmask();");
            }
        }

        protected void OKForm(object sender, DirectEventArgs e)
        {
            SalvarDados(btnSalvar, e);
            WindowForm.Hide();
        }

        protected void FecharJanela(object sender, EventArgs e)
        {
            WindowForm.Hide();
        }

        protected bool Excluir(int idClifor)
        {
            FornecedorControl fo = new FornecedorControl();
            
            GCLIFOR clifor = new GCLIFOR();
            clifor.idEmpresa = Memoria.Empresa;
            clifor.idClifor = idClifor;

            bool ret = (bool)fo.ExecutaFuncao(clifor, Funcoes.Cancelar, null);
            
            if (ret)
            {
                //FetchRecord(storeGeral, new StoreRefreshDataEventArgs());
                FormPanel1.AddScript("storeGeral.commitChanges(); storeGeral.reload();");
            }
            else
            {
                X.Msg.SetIcon(MessageBox.Icon.WARNING);
                X.Msg.Alert("Informação", Memoria.MsgGlobal).Show();
            }
            
            return ret;
        }

        
        protected void GridComandos(object sender, DirectEventArgs e)
        {
            int codigo = Convert.ToInt32(e.ExtraParams["codigo"]);
            string comando = Convert.ToString(e.ExtraParams["command"]);


            if (comando == "ativar")
            {
                FornecedorControl forn = new FornecedorControl();

                GCLIFOR fo = new GCLIFOR();
                fo.idClifor = codigo;

                fo = (GCLIFOR)forn.ExecutaFuncao(fo, Funcoes.Buscar, null);

                fo.ativo = true;

                forn.ExecutaFuncao(fo, Funcoes.Atualizar, null);
                FormPanel1.AddScript("storeGeral.commitChanges(); storeGeral.reload();");
            }

            if (comando == "desativar")
            {

                FornecedorControl forn = new FornecedorControl();

                GCLIFOR fo = new GCLIFOR();
                fo.idClifor = codigo;

                fo = (GCLIFOR)forn.ExecutaFuncao(fo, Funcoes.Buscar, null);

                fo.ativo = false;

                forn.ExecutaFuncao(fo, Funcoes.Atualizar, null);
                FormPanel1.AddScript("storeGeral.commitChanges(); storeGeral.reload();");
            }
        }

        public void GridAcao(object sender, DirectEventArgs e)
        {
            int idFornecedor = Convert.ToInt32(e.ExtraParams["codigo"]);

            FetchRecord(storeGeral, new StoreRefreshDataEventArgs());

            GCLIFOR fornecedor = listaClifor.Where(r => r.idClifor == idFornecedor).FirstOrDefault();

            int index = listaClifor.IndexOf(fornecedor);

            PagingToolbar1.SetPageIndex(
                   Convert.ToInt32(index)+1);

            TabPanel1.SetActiveTab(0);
            WindowForm.Show();
        }

        protected void storeInsumos_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        { 
            int id = Convert.ToInt32(txtIdClifor.Value);
            
            var lista = from p in Contexto.Atual.EPRODUTO
                        where !p.GCLIFOR.Where(r=> r.idEmpresa == Memoria.Empresa && r.idClifor == id).Any()
                        select new
                        {
                            codigo = p.idProduto,
                            nome = p.codigo + " - " + p.nome
                        };
            
            storeInsumos.DataSource = lista;
            storeInsumos.DataBind();


            var lista2 = from p in Contexto.Atual.EPRODUTO
                         where p.GCLIFOR.Where(r => r.idEmpresa == Memoria.Empresa && r.idClifor == id).Any()
                         select new
                         {
                             codigo = p.idProduto,
                             nome = p.codigo + " - " + p.nome
                         };
            
            
            storeInsumosFornecedor.DataSource = lista2;
            storeInsumosFornecedor.DataBind();
            
        }  

    }
}