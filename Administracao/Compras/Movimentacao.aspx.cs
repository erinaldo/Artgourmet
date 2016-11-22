using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Compras;
using Artebit.Restaurante.Global.RegrasNegocio.Estoque;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Ext.Net;
using Newtonsoft.Json.Linq;

namespace Artebit.Restaurante.Administracao.Compras
{
    public partial class Movimentacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MemoriaWeb.ValidaSessao();

            Contexto.AbrirContexto();

            if (!IsPostBack)
            {
                VerificarPermissoes();
                CarregaPrincipal();
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
            var compl = new List<string> { "AD", "Movimentação", "0" };
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

        private void CarregaPrincipal()
        {
            MovimentoControl ct = new MovimentoControl();
            List<List<string>> comp = new List<List<string>>();
            List<string> compi = new List<string>();
            compi.Add("SC");
            comp.Add(compi);
            IQueryable<CMOVIMENTO> lista = (IQueryable<CMOVIMENTO>)ct.ExecutaFuncao(null, Funcoes.BuscarLista, comp);

            var dados = from a in lista
                        select new
                        {
                            codigo = a.idMov,
                            tipo = a.CTPMOV.descricao,
                            status = a.CSTATMOV.descricao,
                            dataEmissao = a.dataEmissao,
                            observacao = a.observacao,
                            valorTotal = a.valorTotal,
                        };

            StorePrincipal.DataSource = dados;
            StorePrincipal.DataBind();
        }

        protected void GridAcao(object sender, DirectEventArgs e)
        {
            int id = Convert.ToInt32(e.ExtraParams["codigo"]);
            string comando = Convert.ToString(e.ExtraParams["command"]);

            CMOVIMENTO obj = new CMOVIMENTO();
            MovimentoControl ctrl = new MovimentoControl();


            #region Editar
            if (comando == "editar" && btnEditarG.Disabled == false)
            {
                //-------------------------------------------------------------------------------------------
                //Preenchendo os campos com as informações do Movimento
                FormPrincipal.AddScript(
                    "if(StoreFormulario.allData != null) { PagingToolbar1.changePage(StoreFormulario.allData.indexOfKey(" +
                    id.ToString() + ") + 1); }" + " else { PagingToolbar1.changePage(StoreFormulario.indexOfId(" + id.ToString() +
                    ") + 1);}");

                JanelaPrincipal.Show();
            }
            #endregion

            #region Cancelar
            if (comando == "cancelar")
            {
                bool sucesso = true;
                
                foreach (var i in RowSelectionModel1.SelectedRows)
                {
                    id = Convert.ToInt32(i.RecordID);
                    CMOVIMENTO mov = new CMOVIMENTO();

                    mov.idMov = id;
                    mov.idEmpresa = Memoria.Empresa;
                    mov.idFilial = Memoria.Filial;
                    mov = (CMOVIMENTO)ctrl.ExecutaFuncao(mov, Funcoes.Buscar, null);

                    mov.codStatus = "C";

                    bool res = (bool)ctrl.ExecutaFuncao(mov, Funcoes.Atualizar, null);

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
                        Html = "Itens cancelados com sucesso!"
                    });
                }
            }
            #endregion

            #region Ativar/Desativar
            if (comando == "ativar")
            {
                bool sucesso = true;

                foreach (var i in RowSelectionModel1.SelectedRows)
                {
                    id = Convert.ToInt32(i.RecordID);
                    CMOVIMENTO mov = new CMOVIMENTO();

                    mov.idMov = id;
                    mov.idEmpresa = Memoria.Empresa;
                    mov.idFilial = Memoria.Filial;
                    mov = (CMOVIMENTO)ctrl.ExecutaFuncao(mov, Funcoes.Buscar, null);

                    if (mov.codStatus == "I")
                        mov.codStatus = "P";
                    else if(mov.codStatus == "P")
                        mov.codStatus = "I";
                    
                    bool res = (bool)ctrl.ExecutaFuncao(mov, Funcoes.Atualizar, null);

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
                MovimentoControl ct = new MovimentoControl();
                List<List<string>> comp = new List<List<string>>();
                List<string> compi = new List<string>();
                compi.Add("SC");
                comp.Add(compi);

                List<string> seq = (List<string>) ct.ExecutaFuncao(null, Funcoes.RetornaSequencia, comp);


                IDictionary<string, string> registro = new Dictionary<string, string>();
                registro.Add("codigo", Contexto.GerarId("CMOVIMENTO").ToString());
                registro.Add("tipo", "");
                registro.Add("numeroMov", seq[0]);
                registro.Add("serie", seq[1]);

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
            CMOVIMENTO obj = new CMOVIMENTO();
            MovimentoControl control = new MovimentoControl();

            //----------------------------------------------------------------------------------------
            // hd_tipo.Value é um textbox oculto. Ele fica esático durante toda a execução do programa
            //----------------------------------------------------------------------------------------
            Funcoes acao;
            if (hd_tipo.Value.ToString() == "2") //editar
            {
                acao = Funcoes.Atualizar;
                obj.idMov = Convert.ToInt32(codigo.Text);
                obj.idEmpresa = Memoria.Empresa;
                obj.idFilial = Memoria.Filial;
                obj = (CMOVIMENTO)control.ExecutaFuncao(obj, Funcoes.Buscar, null);
            }
            else
            {
                acao = Funcoes.Adicionar;
                obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);
            }


            //----------------------------------------------------------------------------------------
            //Recebendo todos os dados do usuário
            //----------------------------------------------------------------------------------------
            obj.idMov = Convert.ToInt32(codigo.Text);
            obj.observacao = Convert.ToString(txtObservacao.Value);
            obj.idFilialLoc = Memoria.Filial;
            obj.codTipoMov = "SC";
            obj.codStatus = "P";
            obj.serie = Convert.ToString(hdSerie.Value);
            obj.numMov = Convert.ToString(hdNumeroMov.Value);

            

            if (ComboBoxLocal.Value != null)
                obj.idLoc = Convert.ToInt32(ComboBoxLocal.Value);
            else
                obj.idLoc = null;
            
            


            //----------------------------------------------------------------------------------------
            #region Inserindo os itens no CMOVIMENTO
            //----------------------------------------------------------------------------------------
            obj.CITEMMOV.Clear();

            string j = "";
            List<object> items = null;

            j = e.ExtraParams["Itens"];
            items = JSON.Deserialize<List<object>>(j);
            foreach (object a in items)
            {
                CITEMMOV item = new CITEMMOV();
                item.idEmpresa = Memoria.Empresa;
                item.sequencialMov = Convert.ToInt32(JContainer.FromObject(a)["codigo"].ToString());
                item.idProduto = Convert.ToInt32(JContainer.FromObject(a)["idProduto"].ToString());
                item.codUnd = Convert.ToString(JContainer.FromObject(a)["codUnd"].ToString());
                string qtde = (JContainer.FromObject(a)["quantidade"].ToString());
                if (qtde == "")
                    item.quantidade = 0;
                else
                    item.quantidade = Convert.ToDecimal(qtde);

                obj.CITEMMOV.Add(item);
            }
            #endregion


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

        #region Açoes dos botões da GridItens

        protected void btnNovoItemFT_Click(object sender, DirectEventArgs e)
        {
            //Zerando os campos
            ComboBoxProduto.Clear();
            txtObservacao.Clear();
            ComboBoxUnidade.Clear();
            txtQTDE.Clear();

            string j = e.ExtraParams["Itens"];
            List<object> items = JSON.Deserialize<List<object>>(j);

            int max = 1;

            foreach (object a in items)
            {
                int idSequencia = Convert.ToInt32(JContainer.FromObject(a)["codigo"].ToString());

                if (idSequencia > max)
                {
                    max = idSequencia;
                }
            }

            txtSequencia.Value = max;

            JanelaItens.Show();
        }

        protected void btnRemoveItemFT_Click(object sender, EventArgs e)
        {
            //Captando as linhas seleciondas
            CheckboxSelectionModel sm = this.GridItensMovimento.SelectionModel.Primary as CheckboxSelectionModel;

            //Recebe o id do produto da linha selecionda e exclui o produto das session "itensAdicionais"
            //e "adicionais"
            foreach (SelectedRow row in sm.SelectedRows)
            {
                StoreItemMov.RemoveRecord(row.RecordID);
                StoreItemMov.CommitChanges();
            }
        }

        #endregion

        #region Ação da janela Itens


         // Salva dados e fecha janela
        protected void btnOkItn_Click(object sender, DirectEventArgs e)
        {
            SalvarItens(sender, e);
            JanelaItens.Hide();
        }


        // Função para salvar dados
        protected void SalvarItens(object sender, DirectEventArgs e)
        {

            //Captando os valores informados pelo usuário
            CITEMMOV tipo = new CITEMMOV();

            tipo.sequencialMov = Convert.ToInt32(txtSequencia.Value);
            tipo.idProduto = Convert.ToInt32(ComboBoxProduto.Value);
            EPRODUTO prod = new EPRODUTO();
            prod.idProduto = Convert.ToInt32(tipo.idProduto);
            prod.idEmpresa = Memoria.Empresa;
            ProdutoControl p = new ProdutoControl();
            prod = (EPRODUTO) p.ExecutaFuncao(prod, Funcoes.Buscar, null);
            //tipo.observacao = txtObservacao.Value.ToString();
            tipo.quantidade = Convert.ToDecimal(txtQTDE.Value);
            tipo.codUnd = Convert.ToString(ComboBoxUnidade.Value);
            tipo.valorTotal = Convert.ToDecimal(txtCustoTotal.Text.Replace("R$ ", ""));


            IDictionary<string, string> registro = new Dictionary<string, string>();
            registro.Add("codigo", tipo.sequencialMov.ToString());
            registro.Add("idProduto", tipo.idProduto.ToString());
            registro.Add("produto", prod.nome);
            registro.Add("quantidade", tipo.quantidade.ToString());
            registro.Add("codUnd", tipo.codUnd);
            registro.Add("valorTotal", tipo.valorTotal.ToString());



            bool adicionar = true;
            string j = e.ExtraParams["Itens"];
            List<object> items = JSON.Deserialize<List<object>>(j);
            foreach (object a in items)
            {
                int idSequencia = Convert.ToInt32(JContainer.FromObject(a)["codigo"].ToString());
                int idMov = Convert.ToInt32(JContainer.FromObject(a)["idMov"].ToString());

                if (tipo.idMov == idMov && tipo.sequencialMov == idSequencia)
                {
                    adicionar = false;
                }
            }

            
            StoreItemMov.AddRecord(registro, true);
            StoreItemMov.CommitChanges();
            StoreItemMov.AddScript("StoreRecursos.save();");

            JanelaItens.Hide();

        }

        //Função para carregar as unidades de acordo com  o produto
        protected void CarregaUnidade(object sender, RemoteValidationEventArgs e)
        {
            //Recebendo os dados digitados pelo usuário
            if (ComboBoxProduto.Value != null)
            {
                EPRODUTO prod = new EPRODUTO();

                ProdutoControl ct = new ProdutoControl();
                prod.idProduto = Convert.ToInt32(ComboBoxProduto.SelectedItem.Value);
                prod.idEmpresa = Memoria.Empresa;

                prod = (EPRODUTO) ct.ExecutaFuncao(prod, Funcoes.Buscar, null);

                StoreUnidade.DataSource = Contexto.Atual.EUNIDADE.Where(r => r.codUndBase == prod.undControle);
                StoreUnidade.DataBind();

                ComboBoxUnidade.Clear();
                txtCustoTotal.Text = "";

                e.Success = true;
            }
        }

        //Calcula o valor final.
        protected void CalculaValorItem(object sender, RemoteValidationEventArgs e)
        {
            //Verifica se foi deixado um valor em branco.
            if(ComboBoxUnidade.Value == null || txtQTDE.Value == null)
            {
                string mensg;
                
                if(ComboBoxUnidade.Value == null)
                {
                    mensg = "Selecione uma unidade";
                }
                else if(txtQTDE.Value == null)
                {
                    mensg = "Informe um valor";
                }
                else
                {
                    mensg = "Selecione uma unidade e adicione uma quantidade";
                }

                X.Msg.Alert("Alerta", mensg).Show();
            }

            else
            {
                EPRODUTO prod = new EPRODUTO();
                prod.idProduto = Convert.ToInt32(ComboBoxProduto.Value);
                prod.idEmpresa = Memoria.Empresa;
                ProdutoControl ct = new ProdutoControl();
                prod = (EPRODUTO) ct.ExecutaFuncao(prod, Funcoes.Buscar, null);

                if(prod.custoMedio != null)
                {
                    decimal qtde = Convert.ToDecimal(txtQTDE.Value);
                    //decimal total = Convert.ToDecimal(prod.custoMedio)*qtde;
                    

                    string valorTotal = ((qtde * (
                                                             from t in Contexto.Atual.EUNIDADE
                                                             where t.codUnd == ComboBoxUnidade.Value && t.codUndBase == prod.undControle
                                                             select t.fatorConversao).FirstOrDefault())
                                                * ((prod.custoAtual != null || prod.custoAtual > 0 ? prod.custoAtual : prod.custoMedio) / prod.rendimento)).ToString();

                    txtCustoTotal.Text = Convert.ToString(string.Format("{0:C4}", Convert.ToDecimal(valorTotal)));
                }
                else
                {
                    X.Msg.Alert("Alerta", "O produto não contém um custo médio.").Show();
                }
                
                
                e.Success = true;
            }
        }

        #endregion

        #region StoreRefreshData Events

        // Função que carrega os dados do formulário
        protected void StoreFormulario_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            MovimentoControl ct = new MovimentoControl();
            List<List<string>> comp = new List<List<string>>();
            List<string> compi = new List<string>();
            compi.Add("SC");
            comp.Add(compi);
            IQueryable<CMOVIMENTO> lista = (IQueryable<CMOVIMENTO>)ct.ExecutaFuncao(null, Funcoes.BuscarLista, comp);

            var dados = from a in lista
                        select new
                        {
                            codigo = a.idMov,
                            tipo = a.codTipoMov,
                            status = a.CSTATMOV.descricao,
                            numeroMov = a.numMov,
                            serie = a.serie,
                            dataEmissao = a.dataEmissao,
                            observacao = a.observacao,
                            valorTotal = a.valorTotal,
                            fornecedor = a.idClifor,
                            a.percFrete,
                            a.valorFrete,
                            a.percDesconto,
                            a.valorDesconto,
                            local = a.idLoc,
                            hd_tipo = 2
                        };

            StoreFormulario.DataSource = dados;
            StoreFormulario.DataBind();
        }

        // Função que carrega o combobox tipo
        protected void StoreTipo_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            MovimentoControl ct = new MovimentoControl();
            IQueryable<CTPMOV> lista = (IQueryable<CTPMOV>)ct.ExecutaFuncao(null, Funcoes.BuscarTipo, null);

            var dados = from a in lista
                        select new
                                   {
                                       codigo = a.codTipoMov,
                                       nome = a.descricao,
                                   };

            StoreTipo.DataSource = dados;
            StoreTipo.DataBind();
        }

        // Função que carrega o combobox tipo
        protected void StoreFornecedor_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            FornecedorControl ct = new FornecedorControl();
            IQueryable<GCLIFOR> lista = (IQueryable<GCLIFOR>)ct.ExecutaFuncao(null, Funcoes.BuscarLista, null);

            var dados = from a in lista
                        select new
                        {
                            codigo = a.idClifor,
                            nome = a.nomeFantasia,
                        };

            StoreFornecedor.DataSource = dados;
            StoreFornecedor.DataBind();
        }

        // Função que carrega o combo local
        protected void StoreLocal_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            int idFilial = Convert.ToInt32(Memoria.Filial);

            ELOC obj = new ELOC();
            obj.idFilial = idFilial;
            LocalControl ct = new LocalControl();
            IQueryable<ELOC> lista = (IQueryable<ELOC>)ct.ExecutaFuncao(obj, Funcoes.BuscarListaEspecifica, null);

            var dados = from a in lista

                        select new
                        {
                            codigo = a.idLoc,
                            nome = a.nome,
                        };

            StoreLocal.DataSource = dados;
            StoreLocal.DataBind();
        }

        // Função que carrega o combo status
        protected void StoreStatus_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            MovimentoControl ct = new MovimentoControl();
            IQueryable<CSTATMOV> lista = (IQueryable<CSTATMOV>)ct.ExecutaFuncao(null, Funcoes.BuscarStatus, null);

            var dados = from a in lista

                        select new
                        {
                            codigo = a.CodStatus,
                            nome = a.descricao,
                        };

            StoreStatus.DataSource = dados;
            StoreStatus.DataBind();
        }

        // Função que carrega a grid de itens de movimento
        protected void StoreItensMov_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if(codigo.Value != "")
            {
                CMOVIMENTO obj = new CMOVIMENTO();
                obj.idMov = Convert.ToInt32(codigo.Value);
                MovimentoControl ct = new MovimentoControl();

                IQueryable<CITEMMOV> lista = (IQueryable<CITEMMOV>) ct.ExecutaFuncao(obj, Funcoes.BuscarItem, null);

                var dados = from a in lista join p in Contexto.Atual.EPRODUTO on a.idProduto equals p.idProduto 
                
                            select new
                                       {
                                           codigo = a.sequencialMov,
                                           idMov = a.idMov,
                                           idProduto = a.idProduto,
                                           produto = p.nome,
                                           a.quantidade,
                                           a.precoUnitario,
                                           a.quantidadeReceber,
                                           a.dataEntrega,
                                           a.valorTotal,
                                           a.codUnd,

                                           
                                       };

                StoreItemMov.DataSource = dados;
                StoreItemMov.DataBind();

            }
        }

        // Função que carrega os itens do combobox produtos
        protected void StoreProdutos_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            ProdutoControl controle = new ProdutoControl();

            IQueryable<EPRODUTO> lista =
                (IQueryable<EPRODUTO>)controle.ExecutaFuncao(null, Funcoes.BuscarLista, null);

            var dados = from a in lista
                        select new
                        {
                            codigo = a.idProduto,
                            nomecodigo = a.codigo + " - " + a.nome,
                        };

            StoreProduto.DataSource = dados;
            StoreProduto.DataBind();
        
        }


        #endregion
    }
}