using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Atendimento;
using Artebit.Restaurante.Global.RegrasNegocio.Estoque;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Ext.Net;

namespace Artebit.Restaurante.Administracao.PDV
{
    public partial class Impressoras : System.Web.UI.Page
    {
        #region Page Load/Unload e Carregamento Global
        protected void Page_Load(object sender, EventArgs e)
        {
            Contexto.AbrirContexto();

            if (!IsPostBack)
            {
                VerificarPermissoes();
                CarregarImpressoras();
                CarregaMesas();
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            Contexto.FecharContexto();    
        }

        //Verifica as permissões do planoário para ter acesso aos botões superiores
        private void VerificarPermissoes()
        {
            var perfil = new GPERFIL();
            var per = new PerfilControl();
            perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
            perfil = (GPERFIL)per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
            var compl = new List<string> { "AD", "Impressora", "0" };
            bool resultado = false;


            #region Ativar/Desativar Itens

            compl[2] = "19"; //Ativar/Desativar
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

        protected void CarregarImpressoras()
        {
            ImpressoraControl control = new ImpressoraControl();

            IQueryable<AIMPRESSORA> lista =
                (IQueryable<AIMPRESSORA>)control.ExecutaFuncao(null, Funcoes.BuscarLista, null);

            var dados = from a in lista
                        select new
                        {
                            id = a.idImpressora,
                            descricao = a.descricao,
                            nome = a.nome,
                            status = a.ativo == true ? "Ativo" : "Inativo",
                            ligadoA = a.ligadoa,
                            modelo = a.modelo,
                            ip = a.IP,
                            a.imprimeProdutos,
                            a.ativo,
                            tipo = 1
                        };

            StorePrincipal.DataSource = dados;
            StorePrincipal.DataBind();

            StoreFormulario.DataSource = dados;
            StoreFormulario.DataBind();
        }

        protected void CarregaMesas()
        {
            MesaControl ctrl = new MesaControl();
            IQueryable<GMESA> lista = (IQueryable<GMESA>)ctrl.ExecutaFuncao(null, Funcoes.BuscarLista, null, null);

            var dados = from m in lista
                        select new
                        {
                            idmesa = m.nuMesa,
                            obs = m.observacao,
                            lugares = m.qtdLugares
                        };

            StoreMesas.DataSource = dados;
            StoreMesas.DataBind();
        }
        #endregion

        #region Gravar Dados Formulário
        protected void btnOkFrm_Click(object sender, DirectEventArgs e)
        {
            SalvarImpressora(e);
            JanelaPrincipal.Hide();
        }
        
        protected void btnSalvarFrm_Click(object sender, DirectEventArgs e)
        {
            SalvarImpressora(e);
        }

        protected void SalvarImpressora(DirectEventArgs e)
        {
            AIMPRESSORA obj = new AIMPRESSORA();
            ImpressoraControl ct = new ImpressoraControl();
            Funcoes acao = Funcoes.Adicionar;

            if (hd_tipo.Value.ToString() == "1") //editar
            {
                obj.idImpressora = Convert.ToInt32(impressoraID.Value);
                acao = Funcoes.Atualizar;
                obj = (AIMPRESSORA) ct.ExecutaFuncao(obj, Funcoes.Buscar, null);
            }

            //Recebendo os dados digitados pelo usuário
            obj.descricao = Convert.ToString(txtDescricao.Value);
            obj.IP = Convert.ToString(txtIP.Value);
            obj.ligadoa = Convert.ToString(txtLigadoA.Value);
            obj.modelo = Convert.ToString(txtModelo.Value);
            obj.imprimeProdutos = chkImprimeProdutos.Checked;
            obj.nome = Convert.ToString(txtNome.Value);

            //Recebendo as mesas selecionadas
            CheckboxSelectionModel sm = this.GridMesas.SelectionModel.Primary as CheckboxSelectionModel;

            obj.AIMPRESSMESA.Clear();
            foreach (SelectedRow row in sm.SelectedRows)
            {
                AIMPRESSMESA imp = new AIMPRESSMESA();
                imp.idEmpresa = Memoria.Empresa;
                imp.idFilial = Memoria.Filial;
                imp.nuMesa = Convert.ToInt32(row.RecordID);
                imp.idImpressora = obj.idImpressora;
                
                obj.AIMPRESSMESA.Add(imp);
            }

            obj.AIMPRESSORAPRD.Clear();

            string j = e.ExtraParams["Insumos"];
            List<object> items = JSON.Deserialize<List<object>>(j);
            foreach (object a in items)
            {
                int codigo = Convert.ToInt32(Newtonsoft.Json.Linq.JContainer.FromObject(a)["codigo"].ToString());

                AIMPRESSORAPRD prd = new AIMPRESSORAPRD();

                prd.idProduto = codigo;

                obj.AIMPRESSORAPRD.Add(prd);
            }

            bool result = (bool) ct.ExecutaFuncao(obj, acao, null);
            
            CarregarImpressoras();

            if (result)
            {
                FormPrincipal.AddScript("storeInsumosImpressora.commitChanges(); storeInsumos.commitChanges(); FormPrincipal.reload();");
                //FetchRecord(storeGeral, new StoreRefreshDataEventArgs());
                FormPrincipal.AddScript("#{FormPrincipal}.body.unmask();");
                X.Msg.Alert("Alerta", "Dados gravados com sucesso").Show();
            }
            else
                X.Msg.Alert("Alerta", "Verifique os dados digitados e tente novamente").Show();
        }
        #endregion

        #region Ações da Grid Principal
        protected void GridAcao(object sender, DirectEventArgs e)
        {
            int id = Convert.ToInt32(e.ExtraParams["codigo"]);
            string comando = Convert.ToString(e.ExtraParams["command"]);

            AIMPRESSORA obj = new AIMPRESSORA();
            ImpressoraControl ctrl = new ImpressoraControl();


            #region Editar
            if (comando == "editar" && btnEditarG.Disabled == false)
            {
                JanelaPrincipal.AddScript(
                    "if(StoreFormulario.allData != null) { PagingToolbar2.changePage(StoreFormulario.allData.indexOfKey(" +
                    id.ToString() + ") + 1); }" + " else { PagingToolbar2.changePage(StoreFormulario.indexOfId(" + id.ToString() +
                    ") + 1);}");

                TabPanel1.SetActiveTab(0);
                JanelaPrincipal.Show();

            }
            #endregion

            #region Excluir
            if (comando == "excluir")
            {
                bool res = (bool)ctrl.ExecutaFuncao(obj, Funcoes.Cancelar, null);

                if (res)
                    X.Msg.Alert("Alerta", "O produto foi desativado").Show();

            }
            #endregion

            #region Ativar/Desativar
            if (comando == "ativar")
            {
                bool sucesso = true;

                foreach (var i in RowSelectionModel1.SelectedRows)
                {
                    id = Convert.ToInt32(i.RecordID);

                    obj = new AIMPRESSORA();
                    obj.idEmpresa = Memoria.Empresa;
                    obj.idFilial = Memoria.Filial;
                    obj.idImpressora = id;
                    obj = (AIMPRESSORA)ctrl.ExecutaFuncao(obj, Funcoes.Buscar, null);

                    obj.ativo = !obj.ativo;

                    bool res = (bool)ctrl.ExecutaFuncao(obj, Funcoes.Atualizar, null);

                    if (!res)
                    {
                        sucesso = false;
                        break;
                    }
                }

                if (sucesso)
                {
                    CarregarImpressoras();
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
                registro.Add("id", Contexto.GerarId("AIMPRESSORA").ToString());
                registro.Add("descricao", "");
                registro.Add("ligadoA", "");
                registro.Add("modelo", "");
                registro.Add("ip", "");
                registro.Add("ativo", "true");
                registro.Add("imprimeProdutos", "false");
                registro.Add("tipo", "0");

                StoreFormulario.AddRecord(registro, true);
                StoreFormulario.CommitChanges();

                //Mostrando a Janela
                TabPanel1.SetActiveTab(0);
                JanelaPrincipal.Show();
            }
            #endregion
        }
        #endregion

        #region StoreRefresh Events
        protected void StoreMesas_RefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            CarregaMesas();
            int id = Convert.ToInt32(impressoraID.Value);

            IQueryable<AIMPRESSMESA> imp =
                Contexto.Atual.AIMPRESSMESA.Where(
                    r =>
                    r.idEmpresa == Memoria.Empresa && r.idFilial == Memoria.Filial && r.idImpressora == id);


            CheckboxSelectionModel sm = GridMesas.SelectionModel.Primary as CheckboxSelectionModel;
            sm.ClearSelections();

            int cont = 0;
            foreach (AIMPRESSMESA a in imp)
            {
                cont++;
                sm.SelectedRows.Add(new SelectedRow(Convert.ToString(a.nuMesa)));
            }
            if(cont > 0)
                sm.UpdateSelection();
            else
                sm.ClearSelections();

        }

        protected void storeInsumos_OnRefreshData(object sender, StoreRefreshDataEventArgs e)
        {
            int id = Convert.ToInt32(impressoraID.Value);

            var lista = from p in Contexto.Atual.EPRODUTO
                        where !p.AIMPRESSORAPRD.Where(r => r.idEmpresa == Memoria.Empresa && r.idImpressora == id).Any()
                        select new
                        {
                            codigo = p.idProduto,
                            nome = p.codigo + " - " + p.nome
                        };

            storeInsumos.DataSource = lista;
            storeInsumos.DataBind();


            var lista2 = from p in Contexto.Atual.EPRODUTO
                         where p.AIMPRESSORAPRD.Where(r => r.idEmpresa == Memoria.Empresa && r.idImpressora == id).Any()
                         select new
                         {
                             codigo = p.idProduto,
                             nome = p.codigo + " - " + p.nome
                         };


            storeInsumosImpressora.DataSource = lista2;
            storeInsumosImpressora.DataBind();

        }  

        #endregion
    }
}