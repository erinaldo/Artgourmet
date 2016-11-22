using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Estoque;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Ext.Net;
using Newtonsoft.Json.Linq;

namespace Artebit.Restaurante.Administracao.Producao
{
    public partial class Observacoes : System.Web.UI.Page
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

        //Verifica permissões dos botões de ação
        private void VerificarPermissoes()
        {
            var perfil = new GPERFIL();
            var per = new PerfilControl();
            perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
            perfil = (GPERFIL) per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
            var compl = new List<string> {"AD", "Observações", "0"};
            bool resultado = false;


            #region Ativar/Desativar Itens

            compl[2] = "19"; //Ativar/Desativar
            resultado = (bool) per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

            if (!resultado)
            {
                btnAtivarDesativarG.Disabled = true;
            }

            #endregion

            #region Adicionar

            compl[2] = "18";
            resultado = (bool) per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

            if (!resultado)
            {
                btnNovoG.Disabled = true;
            }

            #endregion

            #region Excluir

            compl[2] = "20";
            resultado = (bool) per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

            if (!resultado)
            {
                btnExcluirG.Disabled = true;
            }

            #endregion

            #region Editar

            compl[2] = "2";
            resultado = (bool) per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

            if (!resultado)
            {
                btnEditarG.Disabled = true;
            }

            #endregion
        }

        // Função para trazer os dados das 
        private void CarregaPrincipal()
        {
            ObservacaoControl control = new ObservacaoControl();
            IQueryable<EOBSERVACAO> lista =
                (IQueryable<EOBSERVACAO>) control.ExecutaFuncao(null, Funcoes.BuscarAtual, null);

            var dados = from a in lista
                        select new
                                   {
                                       codigo = a.idObs,
                                       descricao = a.descricao,
                                       ativo = a.ativo == true ? "Ativo" : "Inativo"
                                   };

            storePrincipal.DataSource = dados;
            storePrincipal.DataBind();
        }
        
        // Função para carregar os dados no formulário
        protected void GridAcao(object sender, DirectEventArgs e)
        {
            int id = Convert.ToInt32(e.ExtraParams["codigo"]);
            string comando = Convert.ToString(e.ExtraParams["command"]);

            EOBSERVACAO obj = new EOBSERVACAO();
            ObservacaoControl ctrl = new ObservacaoControl();


            #region Editar
            if (comando == "editar" && btnEditarG.Disabled == false)
            {
                //-------------------------------------------------------------------------------------------
                //Preenchendo os campos com as informações do Observacao
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
                    X.Msg.Alert("Alerta", "A Observacao foi excluido").Show();

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
                    EOBSERVACAO ali = new EOBSERVACAO();

                    ali.idObs = id;
                    ali = (EOBSERVACAO)ctrl.ExecutaFuncao(ali, Funcoes.Buscar, null);

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
                registro.Add("codigo", Contexto.GerarId("EOBSERVACAO").ToString());
                registro.Add("descricao", "");
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
            EOBSERVACAO obj = new EOBSERVACAO();
            ObservacaoControl control = new ObservacaoControl();

            //----------------------------------------------------------------------------------------
            // hd_tipo.Value é um textbox oculto. Ele fica esático durante toda a execução do programa
            //----------------------------------------------------------------------------------------
            Funcoes acao;
            if (hd_tipo.Value.ToString() == "2") //editar
            {
                acao = Funcoes.Atualizar;
                obj.idObs = Convert.ToInt32(codigo.Text);
                obj = (EOBSERVACAO)control.ExecutaFuncao(obj, Funcoes.Buscar, null);
            }
            else
            {
                acao = Funcoes.Adicionar;
            }


            //----------------------------------------------------------------------------------------
            //Recebendo todos os dados do usuário
            //----------------------------------------------------------------------------------------
            obj.descricao = Convert.ToString(txtDescricao.Value);
            obj.ativo = CheckAtivo.Checked;

            //----------------------------------------------------------------------------------------
            //Inserindo os Grupos na observação
            //----------------------------------------------------------------------------------------
            
            string j = "";
            List<object> items = null;
            
            
            obj.EGRUPO.Clear();

            j = e.ExtraParams["Grupos"];
            items = JSON.Deserialize<List<object>>(j);
            foreach (object a in items)
            {
                EGRUPO grup = new EGRUPO();
                Global.RegrasNegocio.Estoque.GruposControl ct = new Global.RegrasNegocio.Estoque.GruposControl();

                grup.idGrupo = Convert.ToInt32(JContainer.FromObject(a)["codigo"].ToString());
                grup = (EGRUPO)ct.ExecutaFuncao(grup, Funcoes.Buscar, null);

               obj.EGRUPO.Add(grup);

            }


            //----------------------------------------------------------------------------------------
            //Inserindo os Produtos na observação
            //----------------------------------------------------------------------------------------

            string k = "";
            List<object> prod = null;


            obj.EPRODOBSBAIXA.Clear();

            k = e.ExtraParams["Produtos"];
            prod = JSON.Deserialize<List<object>>(k);
            foreach (object a in prod)
            {
                EPRODOBSBAIXA probaixa = new EPRODOBSBAIXA();
                Global.RegrasNegocio.Estoque.GruposControl ct = new Global.RegrasNegocio.Estoque.GruposControl();

                probaixa.idProduto = Convert.ToInt32(JContainer.FromObject(a)["codigo"].ToString());
                probaixa.Qtd = Convert.ToInt32(JContainer.FromObject(a)["qtde"].ToString());
                probaixa.Und = Convert.ToString(JContainer.FromObject(a)["unidade"].ToString());
                probaixa.idEmpresa = Memoria.Empresa;
                probaixa.idObs = Convert.ToInt32(codigo.Value);


                string baixa = (JContainer.FromObject(a)["tipo"].ToString());
                if (baixa == "Adicionar")
                    probaixa.TipoBaixa = "A";
                else
                {
                    if (baixa == "Remover")
                        probaixa.TipoBaixa = "R";
                    else
                        probaixa.TipoBaixa = null;
                }   


                obj.EPRODOBSBAIXA.Add(probaixa);

            }

            

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
        
        #region Ações para Grupos
        protected void InserirGrupo(object sender, EventArgs e)
        {
            //Criando ou recebendo Itens Adidionais(EPRODUTO)
            CheckboxSelectionModel sm = this.GridGruposEspecificos.SelectionModel.Primary as CheckboxSelectionModel;

            Global.RegrasNegocio.Estoque.GruposControl control = new Global.RegrasNegocio.Estoque.GruposControl();

            foreach (SelectedRow row in sm.SelectedRows)
            {
                EGRUPO obj = new EGRUPO();

                obj.idGrupo = Convert.ToInt32(row.RecordID);
                obj.idEmpresa = Memoria.Empresa;
                obj = (EGRUPO)control.ExecutaFuncao(obj, Funcoes.Buscar, null);

                IDictionary<string, string> registro = new Dictionary<string, string>();
                registro.Add("codigo", obj.idGrupo.ToString());
                registro.Add("nome", obj.codGrupo + " - " + obj.nome);

                StoreListaGrupos.AddRecord(registro, true);

                StoreListaGrupos.CommitChanges();
            }

            JanelaGrupo.Hide();
        }

        protected void RemoverGrupoAdicional(object sender, EventArgs e)
        {
            //Captando as linhas seleciondas
            CheckboxSelectionModel sm = this.gridAdicionais.SelectionModel.Primary as CheckboxSelectionModel;

            //Recebe o id do produto da linha selecionda e exclui o produto das session "itensAdicionais"
            //e "adicionais"
            foreach (SelectedRow row in sm.SelectedRows)
            {
                StoreListaGrupos.RemoveRecord(row.RecordID);
                StoreListaGrupos.CommitChanges();
            }
        }
        #endregion

        #region Ações para Produtos

        protected void CarregarUnidadesProdutos(object sender, RemoteValidationEventArgs e)
        {
            ComboBoxIUnd.Clear();
            StoreUndFT_RefreshData(StoreUndFT, new StoreRefreshDataEventArgs());
            e.Success = true;
        }

        protected void InserirProduto(object sender, DirectEventArgs e)
        {
            //Criando ou recebendo Itens Adidionais(EPRODUTO)

            EPRODOBSBAIXA obj = new EPRODOBSBAIXA();

            obj.Qtd = Convert.ToInt32(txtI_Quantidade.Value);
            obj.Und = Convert.ToString(ComboBoxIUnd.Value);
            obj.idObs = Convert.ToInt32(codigo.Value);
            obj.idProduto = Convert.ToInt32(ComboBoxProduto.Value);
            string nomeProduto = Convert.ToString(ComboBoxProduto.SelectedItem.Text);
            obj.idEmpresa = Memoria.Empresa;
            obj.TipoBaixa = Convert.ToString(RadioGroup1.CheckedItems[0].BoxLabel);

            bool adicionar = true;
            string j = e.ExtraParams["Produtos"];
            List<object> items = JSON.Deserialize<List<object>>(j);
            foreach (object a in items)
            {
                int idProduto = Convert.ToInt32(JContainer.FromObject(a)["codigo"].ToString());

                if (obj.idProduto == idProduto)
                {
                    adicionar = false;
                }
            }

            if (adicionar)
            {
                IDictionary<string, string> registro = new Dictionary<string, string>();
                registro.Add("codigo", obj.idProduto.ToString());
                registro.Add("nome", nomeProduto);
                registro.Add("qtde", obj.Qtd.ToString());
                registro.Add("unidade", obj.Und);
                registro.Add("tipo", obj.TipoBaixa);

                StoreListaProdutos.AddRecord(registro, true);

                StoreListaProdutos.CommitChanges();
                X.Msg.Alert("Alerta", "Produto adicionado.").Show();
            }
            else
            {
                X.Msg.Alert("Alerta", "O produto já foi inserido").Show();
            }

            txtI_Quantidade.Clear();
            RadioGroup1.Clear();
            ComboBoxIUnd.Clear();
            ComboBoxProduto.Clear();
            JanelaProduto.Hide();

        }

        protected void RemoverProdutoAdicional(object sender, EventArgs e)
        {
            //Captando as linhas seleciondas
            CheckboxSelectionModel sm = this.gridProdutos.SelectionModel.Primary as CheckboxSelectionModel;

            //Recebe o id do produto da linha selecionda e exclui o produto das session "itensAdicionais"
            //e "adicionais"
            foreach (SelectedRow row in sm.SelectedRows)
            {
                StoreListaProdutos.RemoveRecord(row.RecordID);
                StoreListaProdutos.CommitChanges();
            }
        }
        #endregion

        #region StoreRefreshData Events
        // Função que carrega os dados do formulário
        protected void StoreFormulario_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            ObservacaoControl control = new ObservacaoControl();
            IQueryable<EOBSERVACAO> lista =
                (IQueryable<EOBSERVACAO>)control.ExecutaFuncao(null, Funcoes.BuscarAtual, null);

            var dados = from a in lista
                        select new
                        {
                            codigo = a.idObs,
                            descricao = a.descricao,
                            ativo = a.ativo,
                            hdtipo = 2
                        };

            StoreFormulario.DataSource = dados;
            StoreFormulario.DataBind();
        }

        protected void StoreListaGrupos_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (codigo.Value != "")
            {
                ObservacaoControl ct = new ObservacaoControl();

                EOBSERVACAO obj = new EOBSERVACAO();
                obj.idObs = Convert.ToInt32(codigo.Value);

                obj = (EOBSERVACAO) ct.ExecutaFuncao(obj, Funcoes.Buscar, null);

                if (obj != null)
                {

                    var dados = from o in obj.EGRUPO
                                where o.idEmpresa == Memoria.Empresa
                                select new
                                {
                                    codigo = o.idGrupo,
                                    nome = o.codGrupo + " - " + o.nome
                                };


                    StoreListaGrupos.DataSource = dados;
                    StoreListaGrupos.DataBind();
                }
            }
        }

        protected void StoreGruposEspecificos_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (codigo.Value != "")
            {
                Global.RegrasNegocio.Estoque.GruposControl ct = new Global.RegrasNegocio.Estoque.GruposControl();

                IQueryable<EGRUPO> lista = (IQueryable<EGRUPO>)ct.ExecutaFuncao(null, Funcoes.BuscarLista, null);

                var dados = from o in lista
                            select new
                            {
                                codigo = o.idGrupo,
                                nome = o.codGrupo + " - " + o.nome
                            };


                StoreGruposEspecificos.DataSource = dados;
                StoreGruposEspecificos.DataBind();
            }
        }

        protected void StoreListaProdutos_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            if (codigo.Value != "")
            {
                ObservacaoControl ct = new ObservacaoControl();

                EOBSERVACAO obj = new EOBSERVACAO();
                obj.idObs = Convert.ToInt32(codigo.Value);

                obj = (EOBSERVACAO)ct.ExecutaFuncao(obj, Funcoes.Buscar, null);

                if (obj != null)
                {
                    var dados = from o in obj.EPRODOBSBAIXA
                                where o.idEmpresa == Memoria.Empresa
                                select new
                                {
                                    codigo = o.idProduto,
                                    nome = o.EPRODUTO.nome,
                                    qtde = o.Qtd,
                                    unidade = o.Und,
                                    tipo = o.TipoBaixa == "A" ? "Adicionar" : "Remover"
                                };


                    StoreListaProdutos.DataSource = dados;
                    StoreListaProdutos.DataBind();
                }
            }
        }

        protected void StoreComboBoxProduto_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            ProdutoControl control = new ProdutoControl();
            IQueryable<EPRODUTO> lista =
                (IQueryable<EPRODUTO>)control.ExecutaFuncao(null, Funcoes.BuscarLista, null);

            var dados = from a in lista
                        select new
                        {
                            codigo = a.idProduto,
                            nome = a.nome
                        };

            StoreComboProdutos.DataSource = dados;
            StoreComboProdutos.DataBind();
        }

        protected void StoreUndFT_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            //Recebendo os dados digitados pelo usuário
            EPRODUTO prod = new EPRODUTO();
            ProdutoControl ct = new ProdutoControl();
            prod.idProduto = Convert.ToInt32(ComboBoxProduto.SelectedItem.Value);
            prod.idEmpresa = Memoria.Empresa;

            prod = (EPRODUTO)ct.ExecutaFuncao(prod, Funcoes.Buscar, null);

            StoreUndFT.DataSource = Contexto.Atual.EUNIDADE.Where(r => r.codUndBase == prod.undControle);
            StoreUndFT.DataBind();

        }

        #endregion


//#region antigo

//        protected void AbrirJanela(object sender, EventArgs e)
//        {
//            GPERFIL perfil = new GPERFIL();
//            Perfil per = new Perfil();
//            perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
//            perfil = (GPERFIL)per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
//            List<string> compl = new List<string>();
//            compl.Add("AD");
//            compl.Add("Observações");
//            compl.Add("0");

//            compl[2] = "18";
//            bool result = (bool)per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

//            if (result)
//            {

//                //Limpando os campos
//                txtDescricao.Value = "";
//                Radio1.Checked = false;
//                Radio2.Checked = false;


//                //Campo oculto para controle de edição
//                hd_tipo.Value = 0;
//                hd_id.Value = "";


//                //Zerando os valores da gridGrupo
//                RowSelectionModel sm = GridGrupos.SelectionModel.Primary as RowSelectionModel;
//                sm.ClearSelections();

//                //Abre a janela
//                WindowPrincipal.Show();
//            }
//            else
//                X.Msg.Alert("Alerta", "Você não tem permissão de executar essa ação, contate o administrador").Show();
//        }


//        protected void Salvar(object sender, EventArgs e)
//        {
//            SalvarDados();
//            WindowPrincipal.Hide();
//        }

//        protected void SalvarOK(object sender, EventArgs e)
//        {
//            SalvarDados();
//        }


//        protected void SalvarDados()
//        {
//            EOBSERVACAO obj = new EOBSERVACAO();
//            Observacao control = new Observacao();

//            Funcoes acao = Funcoes.Adicionar;

//            if (hd_tipo.Value.ToString() == "1") // O objeto foi editado.
//            {
//                obj.idObs = Convert.ToInt32(hd_id.Value);
//                obj = (EOBSERVACAO)control.ExecutaFuncao(obj, Funcoes.Buscar, null);
//                acao = Funcoes.Atualizar;
//            }

//            //Recebendo os valores 
//            obj.descricao = Convert.ToString(txtDescricao.Value);
//            if (Radio1.Checked == true)
//            {
//                obj.ativo = true;
//            }
//            else
//            {
//                obj.ativo = false;
//            }


//            //Recebendo os valores da gridGrupo
//            RowSelectionModel sm = GridGrupos.SelectionModel.Primary as RowSelectionModel;

//            obj.EGRUPO.Clear();
//            foreach (SelectedRow row in sm.SelectedRows)
//            {
//                EGRUPO grup = new EGRUPO();
//                Global.RegrasNegocio.Estoque.Grupos ct = new Global.RegrasNegocio.Estoque.Grupos();
//                grup.idGrupo = Convert.ToInt32(row.RecordID);
//                //row.RecordID é retorno do IDProperty="id" definido na store

//                grup = (EGRUPO)ct.ExecutaFuncao(grup, Funcoes.Buscar, null);

//                obj.EGRUPO.Add(grup);
//            }


//            bool result = (bool)control.ExecutaFuncao(obj, acao, null);
//            CarregaPrincipal();

//            if (result)
//            {
//                X.Msg.Alert("Alerta", "Dados gravados com sucesso").Show();
//            }

//            else
//                X.Msg.Alert("Alerta", "Verifique os dados digitados e tente novamente").Show();
//        }




//        protected void FecharJanela(object sender, EventArgs e)
//        {
//            WindowPrincipal.Hide();
//        }




//        //protected void GridAcao(object sender, DirectEventArgs e)
//        //{
//        //    //Recebendo os parâmetros da grid
//        //    int id = Convert.ToInt32(e.ExtraParams["codigo"]);
//        //    string comando = Convert.ToString(e.ExtraParams["command"]);

//        //    //Buscando o fornecedor
//        //    EOBSERVACAO obj = new EOBSERVACAO();
//        //    Observacao control = new Observacao();
//        //    obj.idObs = id;
//        //    obj = (EOBSERVACAO)control.ExecutaFuncao(obj, Funcoes.Buscar, null);

//        //    if (comando == "editar")
//        //    {
//        //        GPERFIL perfil = new GPERFIL();
//        //        Perfil per = new Perfil();
//        //        perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
//        //        perfil = (GPERFIL)per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
//        //        List<string> compl = new List<string>();
//        //        compl.Add("AD");
//        //        compl.Add("Observações");
//        //        compl.Add("0");

//        //        compl[2] = "2";
//        //        bool result = (bool)per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

//        //        if (result)
//        //        {
//        //            //Carregando os campos do formulário
//        //            txtDescricao.Value = obj.descricao;
//        //            switch (obj.ativo)
//        //            {
//        //                case true:
//        //                    Radio1.Checked = true;
//        //                    break;
//        //                case false:
//        //                    Radio2.Checked = true;
//        //                    break;
//        //            }

//        //            //Carregando dados na grid

//        //            RowSelectionModel sm = GridGrupos.SelectionModel.Primary as RowSelectionModel;

//        //            sm.ClearSelections();

//        //            foreach (EGRUPO item in obj.EGRUPO)
//        //            {
//        //                sm.SelectedRows.Add(new SelectedRow(Convert.ToString(item.idGrupo)));
//        //            }

//        //            sm.UpdateSelection();


//        //            //Campos ocultos
//        //            hd_tipo.Value = 1;
//        //            hd_id.Value = obj.idObs;

//        //            WindowPrincipal.Show();
//        //        }
//        //        else
//        //            X.Msg.Alert("Alerta", "Você não tem permissão de executar essa ação, contate o administrador").Show();
//        //    }



//        //    if (comando == "excluir")
//        //    {
//        //        GPERFIL perfil = new GPERFIL();
//        //        Perfil per = new Perfil();
//        //        perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
//        //        perfil = (GPERFIL)per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
//        //        List<string> compl = new List<string>();
//        //        compl.Add("AD");
//        //        compl.Add("Observações");
//        //        compl.Add("0");

//        //        compl[2] = "20";
//        //        bool result2 = (bool)per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

//        //        if (result2)
//        //        {
//        //            obj.ativo = false;
//        //            bool result = (bool)control.ExecutaFuncao(obj, Funcoes.Atualizar, null);
//        //            CarregaPrincipal();

//        //            if (result)
//        //                X.Msg.Alert("Alerta", "A observação foi desativada.").Show();

//        //        }
//        //        else
//        //            X.Msg.Alert("Alerta", "Você não tem permissão de executar essa ação, contate o administrador").Show();
//        //    }
//        //    if (comando == "ativar")
//        //    {
//        //        GPERFIL perfil = new GPERFIL();
//        //        Perfil per = new Perfil();
//        //        perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
//        //        perfil = (GPERFIL)per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
//        //        List<string> compl = new List<string>();
//        //        compl.Add("AD");
//        //        compl.Add("Observações");
//        //        compl.Add("0");

//        //        compl[2] = "19";
//        //        bool result2 = (bool)per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

//        //        if (result2)
//        //        {
//        //            obj.ativo = true;
//        //            bool result = (bool)control.ExecutaFuncao(obj, Funcoes.Atualizar, null);
//        //            CarregaPrincipal();

//        //            if (result)
//        //                X.Msg.Alert("Alerta", "A observação foi ativada.").Show();

//        //        }
//        //        else
//        //            X.Msg.Alert("Alerta", "Você não tem permissão de executar essa ação, contate o administrador").Show();
//        //    }
//        //}

//#endregion

    }
}