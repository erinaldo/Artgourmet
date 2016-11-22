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
    public partial class OrdemCompra : System.Web.UI.Page
    {

        private List<List<string>> listitem = new List<List<string>>();

        protected void Page_Load(object sender, EventArgs e)
        {
            MemoriaWeb.ValidaSessao();

            Contexto.AbrirContexto();

            if (!IsPostBack)
            {
                VerificarPermissoes();
                CarregaPrincipal();
                StoreFornecedor_RefreshData(StoreFornecedor, new StoreRefreshDataEventArgs());
                StoreLocal_RefreshData(StoreLocal, new StoreRefreshDataEventArgs());
                StoreCondPagamento_RefreshData(StoreCondPagamento, new StoreRefreshDataEventArgs());
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
            compi.Add("OC");
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


        #region Parte de Ordem de Compra

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
                    id.ToString() + ") + 1); }" + " else { PagingToolbar1.changePage(StoreFormulario.indexOfId(" +
                    id.ToString() +
                    ") + 1);}");

                List<CITEMMOV> listItens = new List<CITEMMOV>();
                CMOVIMENTO mov = new CMOVIMENTO();

                mov.idMov = id;
                mov.idEmpresa = Memoria.Empresa;
                mov.idFilial = Memoria.Filial;
                mov = (CMOVIMENTO) ctrl.ExecutaFuncao(mov, Funcoes.Buscar, null);

                foreach (CITEMMOV it in mov.CITEMMOV)
                {
                    CITEMMOV itens = new CITEMMOV();
                    itens = it;
                    listItens.Add(itens);
                }

                Session["ItensOC"] = listItens;
                Session["TotalOC"] = mov.valorTotal;

                JanelaOC.Show();

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
                    else if (mov.codStatus == "P")
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

            #region NF
            if (comando == "NF")
            {

                List<CITEMMOV> listItens = new List<CITEMMOV>();

                foreach (var i in RowSelectionModel1.SelectedRows)
                {
                    id = Convert.ToInt32(i.RecordID);
                    CMOVIMENTO mov = new CMOVIMENTO();

                    mov.idMov = id;
                    mov.idEmpresa = Memoria.Empresa;
                    mov.idFilial = Memoria.Filial;
                    mov = (CMOVIMENTO) ctrl.ExecutaFuncao(mov, Funcoes.Buscar, null);
                

                    if (mov.codStatus != "C" && mov.codStatus != "I")
                    {
                        foreach (CITEMMOV it in mov.CITEMMOV)
                        {
                            CITEMMOV itens = new CITEMMOV();
                            itens = it;
                            listItens.Add(itens);
                        }
                    }

                }

                Session["ItensNF"] = listItens;
                Session["TotalNF"] = 0;

                //StoreItensOC_RefreshData(StoreFormularioNF, new StoreRefreshDataEventArgs());

                //List<List<string>> comp = new List<List<string>>();
                //List<string> compi = new List<string>();
                //compi.Add("NF");
                //comp.Add(compi);
                //List<string> seq = (List<string>)ctrl.ExecutaFuncao(null, Funcoes.RetornaSequencia, comp);

                //txtIdentificadorNF.Text = Convert.ToString(Contexto.GeraId("CMOVIMENTO"));
                //hdidentificadoNF.Value = txtIdentificadorNF.Text;

                //lbltipoNF.Text = "Nota Fiscal";
                //lblstatusNF.Text = "Pendente";

                //lblNumeroMovNF.Text = seq[0];
                //hdNumeroMovNF.Value = seq[0];

                //lblserieNF.Text = seq[1];
                //hdSerieNF.Value = seq[1];

                //ComboBoxLocalNF.Clear();
                //ComboBoxFornecedorNF.Clear();
                //ComboBoxCondPgtoNF.Clear();
                //txtValorTotalNF.Clear();
                //txtPerFreteNF.Clear();
                //txtValorFreteNF.Clear();
                //txtPerDescontoNF.Clear();
                //txtValorDescontoNF.Clear();



                //JanelaNF.Show();

            }

            #endregion

            #region Novo Item
            if (comando == "novo")
            {
                MovimentoControl ct = new MovimentoControl();
                List<List<string>> comp = new List<List<string>>();
                List<string> compi = new List<string>();
                compi.Add("OC");
                comp.Add(compi);

                List<string> seq = (List<string>)ct.ExecutaFuncao(null, Funcoes.RetornaSequencia, comp);


                IDictionary<string, string> registro = new Dictionary<string, string>();
                registro.Add("codigo", Contexto.GerarId("CMOVIMENTO").ToString());
                registro.Add("tipo", "OC");
                registro.Add("nomeTipo", "Ordem de Compra");
                registro.Add("status", "Pendente");
                registro.Add("idStatus", "P");
                registro.Add("numeroMov", seq[0]);
                registro.Add("serie", seq[1]);

                StoreFormulario.AddRecord(registro, true);
                StoreFormulario.CommitChanges();

                List<CITEMMOV> lista = new List<CITEMMOV>();

                Session["ItensOC"] = lista;
                Session["TotalOC"] = 0;

                //Mostrando a Janela
                JanelaOC.Show();
            }
            #endregion
        }

        protected void GridAcaoOC(object sender, DirectEventArgs e)
        {
            string id = Convert.ToString(e.ExtraParams["codigo"]);
            string comando = Convert.ToString(e.ExtraParams["command"]);


            #region Editar
            if (comando == "editar")
            {

                //-------------------------------------------------------------------------------------------
                //Preenchendo os campos com as informações do Movimento
                FormItensOC.AddScript(
                    "if(StoreItensOC.allData != null) {  PagingToolbar2.changePage(StoreItensOC.allData.indexOfKey(" +
                    id.ToString() + ") + 1); }" + " else { PagingToolbar2.changePage(StoreItensOC.indexOfId(" + id.ToString() +
                    ") + 1);}");


                JanelaItensOC.Show();
            }
            #endregion
        }


        #region Ação da janela ItensOC

        // Salva dados e fecha janela
        protected void btnOkItn_ClickOC(object sender, DirectEventArgs e)
        {
            SalvarItensOC(sender, e);
            JanelaItensOC.Hide();
        }

        // Função para salvar dados
        protected void SalvarItensOC(object sender, DirectEventArgs e)
        {
            List<CITEMMOV> listItens = (List<CITEMMOV>)Session["ItensOC"];
            string[] dados = hdCodigo.Value.ToString().Split('-');

            int idmov = Convert.ToInt32(dados[0]);
            int seq = Convert.ToInt32(dados[1]);

            //Captando os valores informados pelo usuário
            CITEMMOV tipo = listItens.SingleOrDefault(a => a.idMov == idmov && a.sequencialMov == seq);

            bool novo = false;

            if(tipo == null)
            {
                tipo = new CITEMMOV();
                tipo.idEmpresa = Memoria.Empresa;
                tipo.idFilial = Memoria.Filial;
                tipo.sequencialMov = seq;
                novo = true;
            }

            //tipo.sequencialMov = Convert.ToInt32(txtSequencia.Value);
            tipo.idProduto = Convert.ToInt32(ComboBoxProdutoOC.Value);
            EPRODUTO prod = new EPRODUTO();
            prod.idProduto = Convert.ToInt32(tipo.idProduto);
            prod.idEmpresa = Memoria.Empresa;
            ProdutoControl p = new ProdutoControl();
            prod = (EPRODUTO)p.ExecutaFuncao(prod, Funcoes.Buscar, null);
            tipo.observacao = txtObservacaoItemOC.Text;
            tipo.dataEntrega = Convert.ToDateTime(txtDataEntrega.Text);
            tipo.codUnd = Convert.ToString(ComboBoxUnidadeOC.Value);
            if (txtQTDEOC.Value != null)
                tipo.quantidade = Convert.ToDecimal(txtQTDEOC.Value.ToString().Replace(".", ","));
            else
                tipo.quantidade = null;


            if (txtCustoUnitarioOC.Value != null)
                tipo.precoUnitario = Convert.ToDecimal(txtCustoUnitarioOC.Text.ToString().Replace(".", ","));
            else
                tipo.precoUnitario = null;


            if (txtCustoTotalOC.Value != null)
                tipo.valorTotal = Convert.ToDecimal(txtCustoTotalOC.Value.ToString().Replace(".", ","));
            else
                tipo.valorTotal = null;


            if (txtVlrDesconto.Value != null)
                tipo.valorDesconto = Convert.ToDecimal(txtVlrDesconto.Value.ToString().Replace(".", ","));
            else
                tipo.valorDesconto = null;


            if (txtQtdRecebeOC.Value != null)
                tipo.quantidadeReceber = Convert.ToDecimal(txtQtdRecebeOC.Value.ToString().Replace(".", ","));
            else
                tipo.quantidadeReceber = null;


            if(novo == true)
            {
                listItens.Add(tipo);
            }

            Session["ItensOC"] = listItens;

            StoreItensOC_RefreshData(StoreItensOC, new StoreRefreshDataEventArgs());
        }

        //Função para carregar as unidades de acordo com  o produto
        protected void CarregaUnidadeOC(object sender, RemoteValidationEventArgs e)
        {
            //Recebendo os dados digitados pelo usuário
            if (ComboBoxProdutoOC.Value != null)
            {
                EPRODUTO prod = new EPRODUTO();

                ProdutoControl ct = new ProdutoControl();
                prod.idProduto = Convert.ToInt32(ComboBoxProdutoOC.SelectedItem.Value);
                prod.idEmpresa = Memoria.Empresa;

                prod = (EPRODUTO)ct.ExecutaFuncao(prod, Funcoes.Buscar, null);

                StoreUnidade.DataSource = Contexto.Atual.EUNIDADE.Where(r => r.codUndBase == prod.undControle);
                StoreUnidade.DataBind();

                ComboBoxUnidadeOC.Clear();
                txtCustoTotalOC.Text = "";

                e.Success = true;
            }
        }

        //Calcula o valor final.
        protected void CalculaValorItemOC(object sender, RemoteValidationEventArgs e)
        {
            //Verifica se foi deixado um valor em branco.
            if (ComboBoxUnidadeOC.Value == null || txtQTDEOC.Value == null)
            {
                string mensg;

                if (ComboBoxUnidadeOC.Value == null)
                {
                    mensg = "Selecione uma unidade";
                }
                else if (txtQTDEOC.Value == null)
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
                if (txtCustoUnitarioOC.Value == "" && txtCustoUnitarioOC.Value == null)
                {
                    EPRODUTO prod = new EPRODUTO();
                    prod.idProduto = Convert.ToInt32(ComboBoxProdutoOC.Value);
                    prod.idEmpresa = Memoria.Empresa;
                    ProdutoControl ct = new ProdutoControl();
                    prod = (EPRODUTO)ct.ExecutaFuncao(prod, Funcoes.Buscar, null);

                    if (prod.custoMedio != null)
                    {
                        decimal qtde = Convert.ToDecimal(txtQTDEOC.Value);
                        //decimal total = Convert.ToDecimal(prod.custoMedio)*qtde;


                        string valorTotal = ((qtde * (
                                                       from t in Contexto.Atual.EUNIDADE
                                                       where
                                                           t.codUnd == ComboBoxUnidadeOC.Value &&
                                                           t.codUndBase == prod.undControle
                                                       select t.fatorConversao).FirstOrDefault())
                                             *
                                             ((prod.custoAtual != null || prod.custoAtual > 0
                                                   ? prod.custoAtual
                                                   : prod.custoMedio) / prod.rendimento)).ToString();

                        txtCustoUnitarioOC.Text =
                            Convert.ToString(Convert.ToDecimal(valorTotal) / qtde);
                        txtCustoTotalOC.Text = Convert.ToString(string.Format("{0:C4}", Convert.ToDecimal(valorTotal)));
                    }
                    else
                    {
                        X.Msg.Alert("Alerta", "O produto não contém um custo médio.").Show();
                    }

                }
                else
                {
                    decimal qtde = Convert.ToDecimal(txtQTDEOC.Value);

                    string valorTotal = txtCustoUnitarioOC.Value.ToString();

                    txtCustoUnitarioOC.Text =
                            Convert.ToString(Convert.ToDecimal(valorTotal));
                    txtCustoTotalOC.Text = Convert.ToString(string.Format("{0:C4}", Convert.ToDecimal(valorTotal) * qtde));
                }

                e.Success = true;
            }
        }

        #endregion


        #region Ação da Janela Ordem de Compras

        // Salva dados e fecha janela
        protected void btnOkOC_Click(object sender, DirectEventArgs e)
        {
            SalvarDados(sender, e);
            JanelaItensOC.Hide();
        }

        // Apenas salva dados
        protected void btnSalvarOC_Click(object sender, DirectEventArgs e)
        {
            SalvarDados(sender, e);
        }

        protected void SalvarDados(object sender, DirectEventArgs e)
        {
            //Receber os dados
            CMOVIMENTO obj = new CMOVIMENTO();
            MovimentoControl control = new MovimentoControl();

            //----------------------------------------------------------------------------------------
            // hd_tipo.Value é um textbox oculto. Ele fica esático durante toda a execução do programa
            //----------------------------------------------------------------------------------------
            Funcoes acao;
            if (hd_tipo.Value.ToString() == "2") //editar
            {
                acao = Funcoes.Atualizar;
                obj.idMov = Convert.ToInt32(hdidentificadoOC.Value);
                obj.idEmpresa = Memoria.Empresa;
                obj.idFilial = Memoria.Filial;
                obj = (CMOVIMENTO)control.ExecutaFuncao(obj, Funcoes.Buscar, null);
            }
            else
            {
                acao = Funcoes.Adicionar;
                obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);
            }

            obj.idMov = Convert.ToInt32(hdidentificadoOC.Value);
            obj.codTipoMov = "OC";
            if (acao == Funcoes.Adicionar)
            {
                obj.codStatus = "P";
            }
            obj.idFilial = Memoria.Filial;
            obj.idFilialLoc = Memoria.Filial;
            obj.numMov = Convert.ToString(hdNumeroMovOC.Value);
            obj.serie = Convert.ToString(hdSerieOC.Value);

            if (ComboBoxFornecedorOC.Value != null)
                obj.idClifor = Convert.ToInt32(ComboBoxFornecedorOC.Value);
            else
                obj.idClifor = null;

            if (ComboBoxLocalOC.Value != null)
                obj.idLoc = Convert.ToInt32(ComboBoxLocalOC.Value);
            else
                obj.idLoc = null;

            if (ComboBoxCondPgto.Value != null)
                obj.idCondPgto = Convert.ToInt16(ComboBoxCondPgto.Value);
            else
                obj.idCondPgto = null;


            if (txtPerFrete.Value == null || txtPerFrete.Value == "")
            {
                txtPerFrete.Value = 0;
            }

            if (txtPerDesconto.Value == null || txtPerDesconto.Value == "")
            {
                txtPerDesconto.Value = 0;
            }

            if (txtValorFrete.Value == null || txtValorFrete.Value == "")
            {
                txtValorFrete.Value = 0;
            }

            if (txtValorDesconto.Value == null || txtValorDesconto.Value == "")
            {
                txtValorDesconto.Value = 0;
            }

            Decimal valorTotal = CalculaValorTotal();
            atualizaValorTotal(valorTotal);

            obj.observacao = Convert.ToString(txtObservacaoOC.Value);
            obj.valorTotal = Convert.ToDecimal(txtValorTotal.Value);
            obj.percFrete = Convert.ToDecimal(txtPerFrete.Value);
            obj.valorFrete = Convert.ToDecimal(txtValorFrete.Value);
            obj.percDesconto = Convert.ToDecimal(txtPerFrete.Value);
            obj.valorDesconto = Convert.ToDecimal(txtValorDesconto.Value);


            //----------------------------------------------------------------------------------------
            #region Inserindo os itens no CMOVIMENTO
            //----------------------------------------------------------------------------------------

            int cont = 1;

            List<CITEMMOV> list2item = obj.CITEMMOV.ToList();

            foreach (CITEMMOV citemmov in list2item)
            {
                //if (citemmov.status != false && citemmov.CITEMMOVRELAC.Count() == 0)
                //{
                //    obj.CITEMMOV.Remove(citemmov);
                //}
                //else
                //{
                //    if (cont < citemmov.sequencialMov)
                //    {
                //        cont = citemmov.sequencialMov;
                //    }
                //}
            }

            List<CITEMMOV> listItens = (List<CITEMMOV>)Session["ItensOC"];

            foreach (CITEMMOV item in listItens)
            {
                CITEMMOV it = new CITEMMOV();
                it.idMov = obj.idMov;
                it.sequencialMov = cont;
                it.codUnd = item.codUnd;
                it.dataEntrega = item.dataEntrega;
                it.idClifor = Convert.ToInt32(ComboBoxFornecedorOC.Value);
                it.idEmpresa = Memoria.Empresa;
                it.idFilial = Memoria.Filial;
                it.idProduto = item.idProduto;
                //it.status = true;
                it.observacao = item.observacao;
                it.precoUnitario = item.precoUnitario;
                it.quantidade = item.quantidade;
                it.quantidadeReceber = item.quantidadeReceber;
                it.valorDesconto = item.valorDesconto;
                it.valorTotal = item.valorTotal;
                it.dataEntrega = item.dataEntrega;

                int seq = item.sequencialMov;

                if (!obj.CITEMMOV.Any(b => b.idProduto == item.idProduto && b.sequencialMov == seq) && 
                    obj.CITEMMOV.FirstOrDefault(b => b.idProduto == item.idProduto && b.sequencialMov == seq).CITEMMOVRELAC.Count() == 0)
                {
                    obj.CITEMMOV.Add(it);
                    cont++;
                }else
                {
                    CITEMMOV it2 = new CITEMMOV();

                    it2 = obj.CITEMMOV.SingleOrDefault(b => b.idProduto == item.idProduto && b.sequencialMov == seq);

                    it2.codUnd = item.codUnd;
                    it2.dataEntrega = item.dataEntrega;
                    it2.idClifor = Convert.ToInt32(ComboBoxFornecedorOC.Value);
                    it2.idProduto = item.idProduto;
                    it2.status = true;
                    it2.observacao = item.observacao;
                    it2.precoUnitario = item.precoUnitario;
                    it2.quantidade = item.quantidade;
                    it2.quantidadeReceber = item.quantidadeReceber;
                    it2.valorDesconto = item.valorDesconto;
                    it2.valorTotal = item.valorTotal;
                    it2.dataEntrega = item.dataEntrega;
                }
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

        //Calcula o custo total do item
        protected void CalculaCustoTotalOC(object sender, RemoteValidationEventArgs e)
        {
            if (txtVlrDesconto.Value == null)
                txtVlrDesconto.Value = 0;

            if (txtCustoUnitarioOC.Value != null && txtQtdRecebeOC.Value != null)
            {

                decimal custoTotal = Convert.ToDecimal(txtCustoUnitarioOC.Value.ToString().Replace(".", ",")) *
                                     Convert.ToDecimal(txtQtdRecebeOC.Value.ToString().Replace(".", ",")) -
                                     Convert.ToDecimal(txtVlrDesconto.Value.ToString().Replace(".", ","));

                if (custoTotal <= 0)
                    custoTotal = 0;

                txtCustoTotalOC.Value = custoTotal;

                e.Success = true;
            }
            else
            {
                txtCustoTotalOC.Value = "";
            }
        }

        //Valida a quantiade a receber
        public void ValidaQtdReceber(object sender, RemoteValidationEventArgs e)
        {
            string[] dados = hdCodigo.Value.ToString().Split('-');
            decimal qtdReceber = Convert.ToDecimal(txtQtdRecebeOC.Value);

            ItemMovimentoControl control = new ItemMovimentoControl();

            CITEMMOV itemmov = new CITEMMOV();

            itemmov.idEmpresa = Memoria.Empresa;
            itemmov.idFilial = Memoria.Filial;
            itemmov.idMov = Convert.ToInt32(dados[0]);
            itemmov.sequencialMov = Convert.ToInt32(dados[1]);

            itemmov = (CITEMMOV)control.Executafuncao(itemmov, Funcoes.Buscar, null);

            decimal? result = (decimal)control.Executafuncao(itemmov, Funcoes.ValidaQtdReceber, null);

            if (qtdReceber > result)
            {
                X.Msg.Alert("Alerta", "A quantidade a receber deve ser menor ou igual a " + String.Format("{0:F}", result) + ".").Show();
            }
            else
            {
                e.Success = true;
                CalculaCustoTotalOC(txtQtdRecebeOC, e);
            }
        }

        #region Valores

        //Calcula o valor total da Ordem de compras(Guia Valores)
        public decimal CalculaValorTotal()
        {

            List<CITEMMOV> listItens = (List<CITEMMOV>)Session["ItensOC"];

            decimal valorTotal = 0;

            foreach (CITEMMOV item in listItens)
            {
                valorTotal = valorTotal + Convert.ToDecimal(item.valorTotal);
            }

            return valorTotal;
        }

        protected void atualizaValorTotal(decimal valorTotal)
        {
            decimal valorFrete = 0, valorDesconto = 0;

            if (txtValorFrete.Value != null && txtValorFrete.Value != "")
                valorFrete = Convert.ToDecimal(txtValorFrete.Value);

            if (txtValorDesconto.Value != null && txtValorDesconto.Value != "")
                valorDesconto = Convert.ToDecimal(txtValorDesconto.Value);

            //Acrescentando o % frete
            valorTotal = (valorTotal + valorFrete - valorDesconto);
            txtValorTotal.Value = valorTotal;
        }


        public void perFrete(object sender, RemoteValidationEventArgs e)
        {
            decimal valorTotal = CalculaValorTotal();

            txtValorFrete.Value = (Convert.ToDecimal(txtPerFrete.Value) * valorTotal) / 100;

            atualizaValorTotal(valorTotal);
        }

        public void perDesconto(object sender, RemoteValidationEventArgs e)
        {
            decimal valorTotal = CalculaValorTotal();

            txtValorDesconto.Value = (Convert.ToDecimal(txtPerDesconto.Value) * valorTotal) / 100;

            atualizaValorTotal(valorTotal);
        }

        public void ValorFrete(object sender, RemoteValidationEventArgs e)
        {
            decimal valorTotal = CalculaValorTotal();

            txtPerFrete.Value = (Convert.ToDecimal(txtValorFrete.Value) * 100) / valorTotal;

            atualizaValorTotal(valorTotal);
        }

        public void ValorDesconto(object sender, RemoteValidationEventArgs e)
        {
            decimal valorTotal = CalculaValorTotal();

            txtPerDesconto.Value = (Convert.ToDecimal(txtValorDesconto.Value) * 100) / valorTotal;

            atualizaValorTotal(valorTotal);
        }

        #endregion


        #endregion


        #region Açoes dos botões da GridItens

        protected void btnNovoItemFT_Click(object sender, DirectEventArgs e)
        {

            int idmov = Convert.ToInt32(hdidentificadoOC.Value);

            int cont = 1;

            List<CITEMMOV> listItens = (List<CITEMMOV>)Session["ItensOC"];

            foreach (CITEMMOV citemmov in listItens)
            {
                if (cont < citemmov.sequencialMov)
                {
                    cont = citemmov.sequencialMov + 1;
                }
            }

            string codigo = idmov + "-" + cont; 


            IDictionary<string, string> registro = new Dictionary<string, string>();
            registro.Add("codigo", codigo);
            registro.Add("seq", cont.ToString());
            registro.Add("quantidade", "1");

             //<ext:RecordField Name="codigo" Type="String" />
             //       <ext:RecordField Name="seq" Type="String" />
             //       <ext:RecordField Name="idMov" Type="String" />
             //       <ext:RecordField Name="idProduto" Type="String" />
             //       <ext:RecordField Name="produto" Type="String" />
             //       <ext:RecordField Name="quantidade" Type="String" />
             //       <ext:RecordField Name="precoUnitario" Type="String" />
             //       <ext:RecordField Name="quantidadeReceber" Type="String" />
             //       <ext:RecordField Name="dataEntrega" Type="Date" />
             //       <ext:RecordField Name="valorTotal" Type="String" />
             //       <ext:RecordField Name="valorUnitario" Type="String" />
             //       <ext:RecordField Name="codUnd" Type="String" />
             //       <ext:RecordField Name="observacao" Type="String" />
             //       <ext:RecordField Name="valorDesconto" Type="String" />

            StoreItensOC.AddRecord(registro, true);
            StoreItensOC.CommitChanges();

            List<CITEMMOV> lista = new List<CITEMMOV>();

            Session["ItensOC"] = lista;
            Session["TotalOC"] = 0;

            txtQTDEOC.Disabled = false;

            //Mostrando a Janela
            JanelaItensOC.Show();
        }

        protected void btnRemoveItemFT_Click(object sender, EventArgs e)
        {
            //Captando as linhas seleciondas
            CheckboxSelectionModel sm = this.GridItensMovimentoOC.SelectionModel.Primary as CheckboxSelectionModel;

            //Recebe o id do produto da linha selecionda e exclui o produto das session "ItensOC"
            //e "adicionais"
            foreach (SelectedRow row in sm.SelectedRows)
            {
                StoreItemMovOC.RemoveRecord(row.RecordID);
                StoreItemMovOC.CommitChanges();
            }
        }

        #endregion

        #endregion






        #region StoreRefreshData Events

        // Função que carrega os dados do formulário
        protected void StoreFormulario_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            MovimentoControl ct = new MovimentoControl();
            List<List<string>> comp = new List<List<string>>();
            List<string> compi = new List<string>();
            compi.Add("OC");
            comp.Add(compi);
            IQueryable<CMOVIMENTO> lista = (IQueryable<CMOVIMENTO>)ct.ExecutaFuncao(null, Funcoes.BuscarLista, comp);

            var dados = from a in lista
                        select new
                        {
                            codigo = a.idMov,
                            tipo = a.codTipoMov,
                            nomeTipo = a.CTPMOV.descricao,
                            codStatus = a.codStatus,
                            status = a.CSTATMOV.descricao,
                            numeroMov = a.numMov,
                            serie = a.serie,
                            dataEmissao = a.dataEmissao,
                            observacao = a.observacao,
                            valorTotal = a.valorTotal,
                            fornecedor = a.idClifor,
                            condpagto = a.idCondPgto,
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

        // Função que carrega os dados do formulário
        protected void StoreItensOC_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {

            List<CITEMMOV> listItens = (List<CITEMMOV>)Session["ItensOC"];
            ItemMovimentoControl ct = new ItemMovimentoControl();

            if (listItens != null || listItens.Count > 0)
            {

                foreach (CITEMMOV it in listItens)
                {
                    if (it.quantidadeReceber == null)
                        it.quantidadeReceber = (decimal) ct.Executafuncao(it, Funcoes.ValidaQtdReceber, null);
                }

                var dados = from a in listItens
                            join p in Contexto.Atual.EPRODUTO on a.idProduto equals p.idProduto
                            select new
                                {
                                    codigo = a.idMov + "-" + a.sequencialMov,
                                    seq = a.sequencialMov,
                                    idMov = a.idMov,
                                    // sequencia do campo dados: idEmpresa, idFilial, idMov, sequencial
                                    dados = a.idEmpresa + "-" + a.idFilial + "-" + a.idMov + "-" + a.sequencialMov,
                                    idProduto = a.idProduto,
                                    produto = p.nome,
                                    a.quantidade,
                                    valorUnitario = a.precoUnitario,
                                    a.quantidadeReceber,
                                    a.dataEntrega,
                                    a.valorDesconto,
                                    a.valorTotal,
                                    a.codUnd,
                                    a.observacao

                                };

                StoreItemMovOC.DataSource = dados;
                StoreItemMovOC.DataBind();

            }
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

        // Função que carrega os itens do combobox produtos
        protected void StoreCondPagamento_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            CondPagtoControl controle = new CondPagtoControl();

            IQueryable<CCONDPAGTO> lista =
                (IQueryable<CCONDPAGTO>)controle.ExecutaFuncao(null, Funcoes.BuscarLista, null);

            var dados = from a in lista
                        select new
                        {
                            codigo = a.idCondPagto,
                            nome = a.descricao
                        };

            StoreCondPagamento.DataSource = dados;
            StoreCondPagamento.DataBind();

        }


        #endregion

    }
}