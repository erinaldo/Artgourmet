using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Ext.Net;
using Artebit.Restaurante.Global.RegrasNegocio.Estoque;
using Artebit.Restaurante.Global.RegrasNegocio.Atendimento;
using Newtonsoft.Json.Linq;
using System.Data;

namespace Artebit.Restaurante.Administracao.Producao
{
    public partial class Produtos : System.Web.UI.Page
    {

        #region Page Load/Unload e Carregamento Global
        protected void Page_Unload(object sender,EventArgs e)
        {
            Contexto.FecharContexto();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            MemoriaWeb.ValidaSessao();

            Contexto.AbrirContexto();
            
            if(!IsPostBack)
            {
                CarregarProdutos(true);

                VerificarPermissoes();

                StoreUndFT.DataSource = Contexto.Atual.EUNIDADE;
                StoreUndFT.DataBind();
            } 
        }

        private void VerificarPermissoes()
        {
            var per = new PerfilControl();
            var perfil = per.Buscar(Convert.ToInt32(Memoria.Perfil));

            var janela = "Produtos";

            #region Ativar/Desativar Itens
            bool resultado = per.Verificar(perfil, janela, 19);

            if (!resultado)
            {
                btnAtivarDesativarG.Disabled = true;
            }

            #endregion

            #region Adicionar
            resultado = per.Verificar(perfil, janela, 18);

            if (!resultado)
            {
                btnNovoG.Disabled = true;
            }

            #endregion

            #region Excluir
            resultado = per.Verificar(perfil, janela, 20);

            if (!resultado)
            {
                btnExcluirG.Disabled = true;
            }

            #endregion

            #region Editar
            resultado = per.Verificar(perfil, janela, 2);

            if (!resultado)
            {
                btnEditarG.Disabled = true;
            }

            #endregion

        }

        protected void CarregarProdutos(bool recarregaForm)
        {
            var controle = new ProdutoControl();

            var lista = controle.BuscarLista();

            var dados = (from a in lista
                         select new
                                    {
                                        a.idProduto,
                                        a.codigo,
                                        a.nome,
                                        a.nomeResumo,
                                        a.tipoItem,
                                        tipo = a.ETIPOITEM.nome,
                                        a.undCompra,
                                        a.undControle,
                                        a.undVenda,
                                        a.tipoTributacao,
                                        a.afetaEstoque,
                                        a.aliquota,
                                        a.ativo,
                                        a.codigoEAN1,
                                        a.codigoEAN2,
                                        a.CST,
                                        a.NCM,
                                        a.diasValidade,
                                        a.estocavel,
                                        a.estoqueMaximo,
                                        a.estoqueMinimo,
                                        a.grupo,
                                        a.idImagem,
                                        a.idImagemHD,
                                        a.modoPreparo,
                                        a.tempoPreparo,
                                        a.pesavel,
                                        a.pontoDePedido,
                                        a.custoAtual,
                                        a.custoMedio,
                                        a.dataUltimaCompra,
                                        a.idEmpresa,
                                        hdtipo = "1",
                                        ultimaCompra = a.dataUltimaCompra,
                                        ativoDesc = a.ativo == true ? "Ativo" : "Inativo",
                                        a.rendimento,
                                        nomecodigo = a.codigo + " - " + a.nome,
                                    }).ToList();

            StorePrincipal.DataSource = dados;
            StorePrincipal.DataBind();

            StoreListaEspecifica.DataSource = dados;
            StoreListaEspecifica.DataBind();

        }   
        #endregion

        #region Gravar Dados Formulário
        protected void btnOkFrm_Click(object sender, DirectEventArgs e)
        {
            SalvarDados(sender,e);
            JanelaPrincipal.Hide();
        }

        protected void btnSalvarFrm_Click(object sender, DirectEventArgs e)
        {
            SalvarDados(sender,e);
        }

        protected void SalvarDados(object sender, DirectEventArgs e)
        {
            var obj = new EPRODUTO();
            var control = new ProdutoControl();

            //----------------------------------------------------------------------------------------
            // hd_tipo.Value é um textbox oculto. Ele fica esático durante toda a execução do programa
            //----------------------------------------------------------------------------------------
            Funcoes acao;
            if (hd_tipo.Value.ToString() == "1") //editar
            {
                acao = Funcoes.Atualizar;
                obj = control.Buscar(Convert.ToInt32(produtoId.Value));
            }
            else
            {
                acao = Funcoes.Adicionar;
                obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);
                obj.idProduto = Convert.ToInt32(produtoId.Value);
            }


            //----------------------------------------------------------------------------------------
            #region Recebendo todos os dados do usuário
            //----------------------------------------------------------------------------------------
            obj.codigo = Convert.ToString(txtCodigo.Value);
            obj.nome = Convert.ToString(txtNome.Value);
            if (ComboUndCompra.SelectedItem != null)
                obj.undCompra = Convert.ToString(ComboUndCompra.SelectedItem.Value);

            obj.undVenda = Convert.ToString(ComboUndVenda.SelectedItem.Value);
            obj.undControle = Convert.ToString(ComboUndControle.SelectedItem.Value);
            obj.diasValidade = Convert.ToInt32(txtValidade.Value);
            obj.estoqueMaximo = Convert.ToDecimal(txtEstoqueMaximo.Value);
            obj.estoqueMinimo = Convert.ToDecimal(txtEstoqueMinimo.Value);
            obj.codigoEAN1 = Convert.ToString(txtEAN1.Value);
            obj.codigoEAN2 = Convert.ToString(txtEAN2.Value);
            obj.estocavel = Convert.ToBoolean(CheckEstocavel.Value);
            obj.CST = Convert.ToString(txtCST.Value);
            obj.NCM = Convert.ToString(txtNCM.Value);
            obj.pesavel = Convert.ToBoolean(CheckPesavel.Value);
            obj.pontoDePedido = Convert.ToDecimal(txtPontoPedido.Value);
            obj.custoMedio = Convert.ToDecimal(txtCustoMedio.Value);
            obj.modoPreparo = Convert.ToString(txtModoPreparo.Value);
            obj.tempoPreparo = Convert.ToInt32(txtTempoPreparo.Value);
            obj.afetaEstoque = Convert.ToBoolean(CheckAfetaEstoque.Value);
            obj.custoAtual = (string) hd_custoAtual.Value != "" ? Convert.ToDecimal(hd_custoAtual.Value.ToString().Replace(".",",")) : 0;
            obj.rendimento = Convert.ToString(txtRendimento.Value) != "" ? Convert.ToDecimal(txtRendimento.Value) : 1;

            if (Convert.ToInt32(ComboBoxGrupo.Value) != 0)
                obj.grupo = Convert.ToInt32(ComboBoxGrupo.Value);
            else
                obj.grupo = null;

            if (rdTributacao.CheckedItems.Count > 0)
            {
                obj.tipoTributacao = rdTributacao.CheckedItems[0].InputValue;
            }

            if (obj.tipoTributacao == "T")
                obj.aliquota = Convert.ToInt32(ComboBoxAliquota.Value);

            obj.tipoItem = Convert.ToInt32(rdTipoItem.CheckedItems[0].InputValue);
            #endregion

            #region Carregando a IMAGEM DA FOTO.

            if (imgProduto.HasFile)
            {
                byte[] imgBinaryData;
                if (obj.idImagem == null)
                {
                    imgBinaryData = RecuperarImagemBytes(imgProduto);
                    var img = new GIMAGEM
                                  {
                                      idEmpresa = Convert.ToInt32(Memoria.Empresa),
                                      idImagem = Contexto.GerarId("GIMAGEM"),
                                      dado = imgBinaryData,
                                      extensao = Path.GetExtension(imgProduto.FileName),
                                      mime = Path.GetExtension(imgProduto.FileName)
                                  };

                    obj.GIMAGEM1 = img;
                }
                else
                {
                    imgBinaryData = RecuperarImagemBytes(imgProduto);
                    if (obj.GIMAGEM1.dado != imgBinaryData)
                    {

                        var img = new GIMAGEM
                                      {
                                          idImagem = Convert.ToInt32(obj.idImagem),
                                          idEmpresa = Convert.ToInt32(Memoria.Empresa)
                                      };
                        var imgct = new ImagemControl();

                        img = imgct.Buscar(img);
                        bool exc = imgct.Cancelar(img);

                        if (exc)
                        {
                            var img2 = new GIMAGEM
                                           {
                                               idEmpresa = Convert.ToInt32(Memoria.Empresa),
                                               idImagem = Contexto.GerarId("GIMAGEM"),
                                               dado = imgBinaryData,
                                               extensao = Path.GetExtension(imgProduto.FileName),
                                               mime = Path.GetExtension(imgProduto.FileName)
                                           };

                            obj.GIMAGEM1 = img2;
                        }
                    }
                }
            }

            //Carregando a imagem  de receita

            if (imgProdutoHD.HasFile)
            {
                byte[] imgBinaryDataHD;
                if (obj.idImagemHD == null)
                {
                    imgBinaryDataHD = RecuperarImagemBytes(imgProdutoHD);
                    var img = new GIMAGEM
                                  {
                                      idEmpresa = Convert.ToInt32(Memoria.Empresa),
                                      idImagem = Contexto.GerarId("GIMAGEM"),
                                      dado = imgBinaryDataHD,
                                      extensao = Path.GetExtension(imgProdutoHD.FileName),
                                      mime = Path.GetExtension(imgProdutoHD.FileName)
                                  };

                    obj.GIMAGEM = img;
                }
                else
                {
                    imgBinaryDataHD = RecuperarImagemBytes(imgProdutoHD);
                    if (obj.GIMAGEM.dado != imgBinaryDataHD)
                    {

                        var img = new GIMAGEM
                                      {
                                          idImagem = Convert.ToInt32(obj.idImagemHD),
                                          idEmpresa = Convert.ToInt32(Memoria.Empresa)
                                      };
                        var imgct = new ImagemControl();

                        img = imgct.Buscar(img);
                        bool exc = imgct.Cancelar(img);

                        if (exc)
                        {
                            var img2 = new GIMAGEM
                                           {
                                               idEmpresa = Convert.ToInt32(Memoria.Empresa),
                                               idImagem = Contexto.GerarId("GIMAGEM"),
                                               dado = imgBinaryDataHD,
                                               extensao = Path.GetExtension(imgProdutoHD.FileName),
                                               mime = Path.GetExtension(imgProdutoHD.FileName)
                                           };

                            obj.GIMAGEM = img2;
                        }
                    }
                }
            }

            #endregion

            List<object> items;
                        
            //----------------------------------------------------------------------------------------
            #region Inserindo os produtos da FICHA TÉCNICA no objeto EPRODUTO          
            //----------------------------------------------------------------------------------------
            string j = e.ExtraParams["Recursos"];
            var itens = JSON.Deserialize<List<Global.Modelo.ModelLight.Recurso>>(j);
            
            if(itens.Count > 0)
            {
                obj.ERECURSO.Clear();
            }

            foreach (var a in itens)
            {
                var rec = new ERECURSO
                              {
                                  idEmpresa = Memoria.Empresa,
                                  idProdPai = obj.idProduto,
                                  idProdFilho = a.IdProduto,
                                  quantidade = a.Quantidade,
                                  codUnd = a.Unidade
                              };

                obj.ERECURSO.Add(rec);
            }
            #endregion

            //----------------------------------------------------------------------------------------
            #region Inserindo os FORNECEDORES no objeto EPRODUTO          
            //----------------------------------------------------------------------------------------
            if (e.ExtraParams["FornecedoresLoad"] == "true")
            {
                obj.GCLIFOR.Clear();

                j = e.ExtraParams["Fornecedores"];
                items = JSON.Deserialize<List<object>>(j);
                foreach (object a in items)
                {
                    var fo = new GCLIFOR {idClifor = Convert.ToInt32(JToken.FromObject(a)["codigo"].ToString())};
                    var cFornecedor = new FornecedorControl();
                    fo = cFornecedor.Buscar(fo);

                    obj.GCLIFOR.Add(fo);
                }
            }

            #endregion
            
            //----------------------------------------------------------------------------------------
            #region Inserindo os adicionais no objeto EPRODUTO          
            //----------------------------------------------------------------------------------------
            if (e.ExtraParams["AdicionaisLoad"] == "true")
            {
                obj.EPRODADD.Clear();

                j = e.ExtraParams["Adicionais"];
                items = JSON.Deserialize<List<object>>(j);
                foreach (object a in items)
                {
                    var pp = new EPRODADD
                                 {
                                     idProduto = obj.idProduto,
                                     idEmpresa = obj.idEmpresa,
                                     idPrdAdd = Convert.ToInt32(JToken.FromObject(a)["idProduto"].ToString()),
                                     nuPreco = Convert.ToInt32(JToken.FromObject(a)["nuPreco"].ToString())
                                 };

                    obj.EPRODADD.Add(pp);
                }
            }

            #endregion

            //------------------------------------------------------------------------------------------
            #region Recebendo as OBSERVAÇÕES escolhidas pelo usuario e adicionando ao PRODUTO
            //------------------------------------------------------------------------------------------
            if (e.ExtraParams["ObservacoesLoad"] == "true")
            {
                var sm = GridObservacoes.SelectionModel.Primary as RowSelectionModel;

                obj.EOBSERVACAO.Clear();
                foreach (SelectedRow row in sm.SelectedRows)
                {
                    var obs = new EOBSERVACAO {idObs = Convert.ToInt32(row.RecordID)};

                    var ct = new ObservacaoControl();
                    //row.RecordID é retorno do IDProperty="id" definido na store

                    obs = ct.Buscar(obs);

                    obj.EOBSERVACAO.Add(obs);
                }
            }

            #endregion
            
            //------------------------------------------------------------------------------------------
            #region Recebendo o(s) preço(s) cadastrado pelo usuário
            //------------------------------------------------------------------------------------------
            j = e.ExtraParams["Precos"];
            var itensPrecos = JSON.Deserialize<List<ETABPRECO>>(j);

            foreach (var a in itensPrecos)
            {
                if(a.idTabPreco == -1)
                {
                    obj.ETABPRECO.Add(a);
                }
                else
                {
                    Contexto.Atual.AttachTo("ETABPRECO",a);
                    Contexto.Atual.ObjectStateManager.ChangeObjectState(a,EntityState.Modified);
                    a.dataAlteracao = DateTime.Now;
                }
            }
            #endregion

            //------------------------------------------------------------------------------------------
            #region Inserindo os planos de fidelidade
            //------------------------------------------------------------------------------------------
            if (e.ExtraParams["FidelidadeLoad"] == "true")
            {
                string planos = e.ExtraParams["Planos"];
                XmlNode xml = JSON.DeserializeXmlNode("{records:{record:" + planos + "}}");

                foreach (XmlNode row in xml.SelectNodes("records/record"))
                {
                    string codigo = row.SelectSingleNode("codigo").InnerXml;
                    string valor = row.SelectSingleNode("valor").InnerXml;

                    var vinculo = new APRDFIDELIDADE
                                      {
                                          idEmpresa = Memoria.Empresa,
                                          idFilial = Memoria.Filial,
                                          idFidelidade = Convert.ToInt32(codigo),
                                          idProduto = Convert.ToInt32(produtoId.Value)
                                      };

                    if (!String.IsNullOrEmpty(valor))
                        vinculo.valorMoeda = Convert.ToDecimal(valor.Replace(".", ","));


                    APRDFIDELIDADE objBanco =
                        Contexto.Atual.APRDFIDELIDADE.FirstOrDefault(ab => ab.idEmpresa == vinculo.idEmpresa
                                                                           && ab.idFidelidade == vinculo.idFidelidade
                                                                           && ab.idFilial == vinculo.idFilial
                                                                           && ab.idProduto == vinculo.idProduto);
                    if (objBanco != null)
                        Contexto.Atual.APRDFIDELIDADE.DeleteObject(objBanco);

                    Contexto.Atual.AddToAPRDFIDELIDADE(vinculo);
                }
            }

            #endregion

            //------------------------------------------------------------------------------------------
            //Salvar o objeto.
            //------------------------------------------------------------------------------------------
            bool result = acao == Funcoes.Atualizar ? control.Atualizar(obj) : control.Criar(obj);

            if (result)
            {
                hd_tipo.Value = "1";
                //FormPrincipal.AddScript("PagingToolbar1.changePage(StorePrincipal.allData.indexOfKey(" + obj.idProduto + ") + 1);");

                ExtNet.Msg.Alert("Alerta", "Dados gravados com sucesso").Show();
              
                imgFotoProduto.ImageUrl = "~/Sistema/CarregaImagem.aspx?id=" + obj.idImagem.ToString();
                imgFotoProdutoHD.ImageUrl = "~/Sistema/CarregaImagem.aspx?id=" + obj.idImagemHD.ToString();

                imgProduto.Clear();
                imgProdutoHD.Clear();

                StorePrecos.CommitChanges();
                StoreRecursos.CommitChanges();
            }

            else
                ExtNet.Msg.Alert("Alerta", "Verifique os dados digitados e tente novamente").Show();

        }
        #endregion
    
        #region Ações da Grid Principal
        protected void GridAcao(object sender, DirectEventArgs e)
        {
            int id = Convert.ToInt32(e.ExtraParams["codigo"]);
            string comando = Convert.ToString(e.ExtraParams["command"]);

            var obj = new EPRODUTO();
            var ctrl = new ProdutoControl();

            #region Editar
            if (comando == "editar" && btnEditarG.Disabled == false)
            {
                //-------------------------------------------------------------------------------------------
                //Preenchendo os campos com as informações do produto
                FormPrincipal.AddScript(
                    "if(StorePrincipal.allData != null) { PagingToolbar1.changePage(StorePrincipal.allData.indexOfKey(" +
                    id.ToString(CultureInfo.InvariantCulture) + ") + 1); }" + " else { PagingToolbar1.changePage(StorePrincipal.indexOfId(" + id.ToString(CultureInfo.InvariantCulture) +
                    ") + 1);}");

                JanelaPrincipal.Show();
            }
            #endregion

            #region Excluir
            if (comando == "excluir")
            {
                obj = ctrl.Buscar(id);

                bool res = ctrl.Desativar(obj);

                if (res)
                    ExtNet.Msg.Alert("Alerta", "O produto foi desativado").Show();

                CarregarProdutos(true);
            }
            #endregion

            #region Ativar/Desativar
            if (comando == "ativar")
            {
                bool sucesso = true;

                foreach (var i in RowSelectionModel1.SelectedRows)
                {
                    id = Convert.ToInt32(i.RecordID);

                    EPRODUTO prd = ctrl.Buscar(id);

                    prd.ativo = !prd.ativo;

                    bool res = ctrl.Atualizar(obj);
                    
                    if (!res)
                    {
                        sucesso = false;
                        break;
                    }
                }

                if (sucesso)
                {
                    CarregarProdutos(true);
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
                registro.Add("idProduto", Contexto.GerarId("EPRODUTO").ToString(CultureInfo.InvariantCulture));
                registro.Add("tipoItem", "1");
                registro.Add("nomeTributacao", "N");
                registro.Add("afetaEstoque", "false");
                registro.Add("ativo", "true");
                registro.Add("pesavel", "false");
                registro.Add("estocavel", "false");
                registro.Add("diasvalidade", "");
                registro.Add("nuPreco", "1");
                registro.Add("hdtipo", "2");

                StorePrincipal.AddRecord(registro, true);
                StorePrincipal.CommitChanges();

                //Mostrando a Janela
                JanelaPrincipal.Show();
                TabPanelForm.ActiveTabIndex = 0;
            }
            #endregion
        }
        #endregion

        #region Ações para Produtos Adicionais
        protected void InserirProdutoAdicional(object sender, EventArgs e)
        {
            //Criando ou recebendo Itens Adidionais(EPRODUTO)
            var sm = GridProdutosAdicionais.SelectionModel.Primary as CheckboxSelectionModel;

            var control = new ProdutoControl();

            if (sm != null)
                foreach (SelectedRow row in sm.SelectedRows)
                {
                    var obj = control.Buscar(Convert.ToInt32(row.RecordID));

                    IDictionary<string, string> registro = new Dictionary<string, string>();
                    registro.Add("idEmpresa", obj.idEmpresa.ToString(CultureInfo.InvariantCulture));
                    registro.Add("idProduto", obj.idProduto.ToString(CultureInfo.InvariantCulture));
                    registro.Add("codigo", obj.codigo);
                    registro.Add("nome", obj.nome);
                    registro.Add("tipo", obj.ETIPOITEM.nome);
                    registro.Add("diasvalidade", obj.diasValidade.ToString());
                    registro.Add("nuPreco", "1");

                    StoreListaAdicionais.AddRecord(registro, true);

                    StoreListaAdicionais.CommitChanges();
                }

            JanelaProdutosAdicionais.Hide();
        }
    
        protected void RemoverProdutoAdicional(object sender, EventArgs e)
        {
            //Captando as linhas seleciondas
            var sm = gridAdicionais.SelectionModel.Primary as CheckboxSelectionModel;

            //Recebe o id do produto da linha selecionda e exclui o produto das session "itensAdicionais"
            //e "adicionais"
            if (sm != null)
                foreach (SelectedRow row in sm.SelectedRows)
                {
                    StoreListaAdicionais.RemoveRecord(row.RecordID);
                    StoreListaAdicionais.CommitChanges();
                }
        }

        #endregion

        #region Ações para Tabela de Preços
        protected void btnRemovePreco_Click(object sender, DirectEventArgs e)
        {
            var sm = GridPrecos.SelectionModel.Primary as CheckboxSelectionModel;

            if (sm != null)
                foreach (SelectedRow row in sm.SelectedRows)
                {
                    StorePrecos.RemoveRecord(row.RecordID);
                }
        }

        protected void InserirPrecoProduto (object sender, EventArgs e)
        {
            //Zerando os dados
            tipo_preco.Value = "2";
            txtP_IdTabPreco.Value = "-1";
            txtP_Descricao.Value = "";
            txtP_Preco1.Value = "";
            txtP_Preco2.Value = "";
            txtP_Preco3.Value = "";
            ComboBoxFilialPreco.Value = Convert.ToString(Memoria.Filial);
            chkP_Ativo.Checked = true;
            
            JanelaPreco.Show();
        }    

        protected void CalcularPUnitario(object sender, RemoteValidationEventArgs e)
        {
            //Recebendo os dados digitados pelo usuário
            var ct = new ProdutoControl();

            if (ComboBoxProduto.SelectedItem != null && txtI_Quantidade.Value != null &&
                ComboBoxIUnd.SelectedItem != null)
            {
                EPRODUTO prod = ct.Buscar(Convert.ToInt32(ComboBoxProduto.SelectedItem.Value));

                if (prod != null)
                {
                    decimal valorUnitario = Convert.ToDecimal(prod.custoAtual);
                    if (valorUnitario == 0)
                        valorUnitario = Convert.ToDecimal(prod.custoMedio);

                    string unidade = ComboBoxIUnd.SelectedItem.Value;

                    decimal? fator = (from p in Contexto.Atual.EUNIDADE
                                      where p.codUnd == unidade && p.codUndBase == prod.undControle
                                      select p.fatorConversao).FirstOrDefault();

                    if (!fator.HasValue)
                    {
                        fator = 1;
                    }

                    if (prod.rendimento != null)
                    {
                        decimal custo = (prod.rendimento.Value * fator.Value) * valorUnitario;

                        txtIPrecoUnitario.Text = string.Format("{0:n4}", custo);

                        decimal quantidade = Convert.ToDecimal(txtI_Quantidade.Value.ToString().Replace(".",","));

                        txtITotal.Text = string.Format("{0:n4}", quantidade * custo);
                    }

                    e.Success = true;
                }
            }
        }
        #endregion

        #region Ações para Ficha Técnica
        protected void btnNovoItemFT_Click(object sender, EventArgs e)
        {
            tipo_produto.Value = 2;
            ComboBoxProduto.Value = "";
            txtI_Quantidade.Value = "";
            ComboBoxIUnd.Value = "";

            txtITotal.Text = "R$ 0,00";
            JanelaFichaTecnica.Show();
        }

        protected void btnRemoveItemFT_Click(object sender, DirectEventArgs e)
        {
            var sm = GridFichaTecnica.SelectionModel.Primary as RowSelectionModel;

            if (sm != null)
                foreach (SelectedRow row in sm.SelectedRows)
                {
                    StoreRecursos.RemoveRecord(row.RecordID);
                }
        }

        #endregion

        #region Ações para Tratamento de Imagens
        protected byte[] RecuperarImagemBytes(FileUploadField upp)
        {
            Stream imgStream = upp.PostedFile.InputStream;
            int imgLen = upp.PostedFile.ContentLength;
            var imgBinaryData = new byte[imgLen];
            imgStream.Read(imgBinaryData, 0, imgLen);
            return imgBinaryData;
        }
        #endregion

        #region StoreRefreshData Events
        protected void StorePlanos_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            int idprd = Convert.ToInt32(produtoId.Value);

            var control = new FidelidadeControl();

            var lista = control.BuscarLista();

            var dados = from d in lista
                        select new
                        {
                            codigo = d.idFidelidade,
                            d.nome,
                            valor =
                            d.APRDFIDELIDADE.FirstOrDefault(
                                 r => r.idProduto == idprd && r.idEmpresa == Memoria.Empresa).valorMoeda
                         };

            StorePlanos.DataSource = dados;
            StorePlanos.DataBind();

            PanelFidelidade.BodyElement.Unmask();

        }

        protected void StoreGrupo_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if ((string) produtoId.Value != "")
            {

                var ct = new GruposControl();
                var lista = ct.BuscarLista();

                var dados = from a in lista
                            select new
                            {
                                id = a.idGrupo,
                                nome = a.codGrupo + " - " + a.nome
                            };

                StoreGrupo.DataSource = dados;
                StoreGrupo.DataBind();
            }
        }

        protected void StoreUndMedida_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if ((string) produtoId.Value != "")
            {
                var control = new ProdutoControl();

                IQueryable<EUNIDADE> lista = control.BuscarUndMedida();

                var dados = from a in lista
                            select new
                            {
                                a.codUnd, a.descricao
                            };

                StoreUndMedida.DataSource = dados;
                StoreUndMedida.DataBind();
            }
        }

        protected void StoreAliquota_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if ((string) produtoId.Value != "")
            {
                IQueryable<AALIQUOTA> lista = from aliquota in Contexto.Atual.AALIQUOTA
                                              select aliquota;

                var dados = from a in lista
                            select new
                            {
                                id = a.idAliquota,
                                nome = a.aliquota
                            };

                StoreAliquota.DataSource = dados;
                StoreAliquota.DataBind();
            }
        }

        protected void StoreRecursos_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if ((string) produtoId.Value != "")
            {
                int idProduto = Convert.ToInt32(produtoId.Value);

                var dados = from p in Contexto.Atual.ERECURSO
                            join h in Contexto.Atual.EPRODUTO on p.idProdFilho equals h.idProduto
                            where p.idProdPai == idProduto && p.idEmpresa == Memoria.Empresa
                            && p.idEmpresa == h.idEmpresa
                            select new Global.Modelo.ModelLight.Recurso
                                       {
                                IdProduto = p.idProdFilho,
                                IdEmpresa = p.idEmpresa,
                                Codigo = h.codigo,
                                Nome = h.nome,
                                Quantidade = p.quantidade ?? 0,
                                Unidade = p.codUnd,
                                ValorUnitario = 
                                                  (1 * (from t in Contexto.Atual.EUNIDADE
                                                           where t.codUnd == p.codUnd && t.codUndBase == h.undControle
                                                           select t.fatorConversao).FirstOrDefault()) * ((h.custoAtual != null || h.custoAtual > 0  ? h.custoAtual : h.custoMedio) / h.rendimento) ?? 0
                            };

                StoreRecursos.DataSource = dados;
                StoreRecursos.DataBind();

            }

            PanelFicha.BodyElement.Unmask();
        }

        protected void StorePrecos_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if ((string) produtoId.Value != "")
            {
                int idProduto = Convert.ToInt32(produtoId.Value);

                var dPrecos = from a in Contexto.Atual.ETABPRECO
                              where a.idProduto == idProduto && a.idEmpresa == Memoria.Empresa
                              select new
                              {
                                  a.idEmpresa,
                                  a.idTabPreco,
                                  a.idProduto,
                                  a.idFilial,
                                  a.descricao,
                                  a.preco1,
                                  a.preco2,
                                  a.preco3,
                                  a.ativo,
                                  ativoDesc = a.ativo ? "Ativo" : "Inativo",
                              };

                StorePrecos.DataSource = dPrecos;
                StorePrecos.DataBind();

            }

            PanelPreco.BodyElement.Unmask();
        }

        protected void StoreFornecedores_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if ((string) produtoId.Value != "")
            {
                int idProduto = Convert.ToInt32(produtoId.Value);

                var dados = from a in Contexto.Atual.GCLIFOR
                            where !a.EPRODUTO.Any(r => r.idEmpresa == Memoria.Empresa && r.idProduto == idProduto)
                            select new
                            {
                                codigo = a.idClifor,
                                nome = a.nomeFantasia,
                            };

                StoreFornecedores.DataSource = dados;
                StoreFornecedores.DataBind();

                var dados2 = from a in Contexto.Atual.GCLIFOR
                            where a.EPRODUTO.Any(r => r.idEmpresa == Memoria.Empresa && r.idProduto == idProduto)
                            select new
                            {
                                codigo = a.idClifor,
                                nome = a.nomeFantasia,
                            };

                StoreFornecedoresEspecifica.DataSource = dados2;
                StoreFornecedoresEspecifica.DataBind();

            }

            PanelFornecedores.BodyElement.Unmask();
        }

        protected void StoreObservacoes_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (ComboBoxGrupo.Value != null)
            {
                var ct = new GruposControl();

                var grup = ct.Buscar(Convert.ToInt32(ComboBoxGrupo.Value));

                var lista = ct.BuscarAtual(grup);

                if (lista != null)
                {
                    var dados = from a in lista
                                select new
                                           {
                                               id = a.idObs, a.descricao
                                           };

                    StoreObservacoes.DataSource = dados;
                    StoreObservacoes.DataBind();

                    int codigo = Convert.ToInt32(produtoId.Value);

                    var sm = GridObservacoes.SelectionModel.Primary as RowSelectionModel;

                    if (sm != null)
                    {
                        sm.ClearSelections();

                        foreach (EOBSERVACAO item in Contexto.Atual.EOBSERVACAO.Where(r => r.EPRODUTO.Any(p => p.idProduto == codigo && p.idEmpresa == Memoria.Empresa)))
                        {

                            sm.SelectedRows.Add(new SelectedRow(Convert.ToString(item.idObs)));
                        }

                        sm.UpdateSelection();
                    }
                }
            }
            else
            {
                var lista = new List<EOBSERVACAO>();
                StoreObservacoes.DataSource = lista;
                StoreObservacoes.DataBind();
            }

            PanelObs.BodyElement.Unmask();
        }

        protected void StoreListaAdicionais_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if ((string) produtoId.Value != "")
            {
                int idProduto = Convert.ToInt32(produtoId.Value);

                var dados = from o in Contexto.Atual.EPRODADD
                            where o.idEmpresa == Memoria.Empresa && o.idProduto == idProduto
                            select new
                            {
                                o.idEmpresa,
                                idProduto = o.idPrdAdd, 
                                o.EPRODUTO1.codigo, 
                                o.EPRODUTO1.nome,
                                tipo = o.EPRODUTO1.ETIPOITEM.nome,
                                diasvalidade = o.EPRODUTO1.diasValidade, 
                                o.nuPreco
                            };


                StoreListaAdicionais.DataSource = dados;
                StoreListaAdicionais.DataBind();
            }

            PanelAdicionais.BodyElement.Unmask();
        }

        protected void StoreInfoEstoque_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            var ct = new ControleEstoqueControl();
            var obj = new ECTESTOQUE
                          {idEmpresa = Convert.ToInt32(Memoria.Empresa), idProduto = Convert.ToInt32(produtoId.Value)};

            IQueryable<ECTESTOQUE> lista = ct.BuscarLista(obj);

            var dados = from a in lista
                        join g in Contexto.Atual.GFILIAL on a.idFilialLoc equals g.idFilial
                        select new
                        {
                            filial = g.nome,
                            local = a.ELOC.nome,
                            a.qtdeAtual,
                            a.custoMedio,
                            valorTotal = a.vlrTotal
                        };

            StoreInfoEstoque.DataSource = dados;
            StoreInfoEstoque.DataBind();

            PanelMovimentacao.BodyElement.Unmask();
        }

        protected void StoreListaEspecifica_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            var controle = new ProdutoControl();

            IQueryable<EPRODUTO> lista = controle.BuscarLista();

            var dados = from a in lista
                        select new
                        {
                            a.idProduto,
                            a.codigo,
                            a.nome,
                            tipo = a.ETIPOITEM.nome,
                            undCompra = a.EUNIDADE.descricao,
                            undVenda = a.EUNIDADE2.descricao,
                            undControle = a.EUNIDADE1.descricao,
                            diasvalidade = a.diasValidade,
                            a.estoqueMinimo,
                            a.estoqueMaximo,
                            ultimaCompra = a.dataUltimaCompra,
                            ativo = a.ativo == true ? "Ativo" : "Inativo"

                        };
            
            StoreListaEspecifica.DataSource = dados;
            StoreListaEspecifica.DataBind();
        }

        protected void StoreFilial_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            var dados = from g in Contexto.Atual.GFILIAL
                        where g.idEmpresa == Memoria.Empresa
                        select new
                        {
                            g.idFilial,
                            nomeFilial = g.nome,

                        };
            StoreFilial.DataSource = dados;
            StoreFilial.DataBind();
        }

        #endregion

    }
}