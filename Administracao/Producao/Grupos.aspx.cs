using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Estoque;
using Ext.Net;

namespace Artebit.Restaurante.Administracao.Producao
{
    public partial class Grupos : System.Web.UI.Page
    {
   
        protected void Page_unload(object sender,EventArgs e)
        {
            Contexto.FecharContexto();
        }

        protected void Page_load(object sender, EventArgs e)
        {   
            MemoriaWeb.ValidaSessao();

            Contexto.AbrirContexto();

            
            if (!IsPostBack)
            {
                CarregaPrincipal();
                CarregaObservacoes();
            }
            
        }

        //==============================================================================================
        // Função para carregar a store principal
        private Ext.Net.TreeNodeCollection CarregaPrincipal()
        {

            Ext.Net.TreeNodeCollection root = ColumnTree1.Root;

            root.Clear();

            Ext.Net.TreeNode root1 = new Ext.Net.TreeNode();
            root1.Text = "Grupos";
            root.Add(root1);

            Global.RegrasNegocio.Estoque.GruposControl control = new Global.RegrasNegocio.Estoque.GruposControl();
            IQueryable<EGRUPO> lista = (IQueryable<EGRUPO>) control.ExecutaFuncao(null, Funcoes.BuscarLista, null);


            foreach (var item in lista.Where(b => b.ativo == true && b.idGrupoPai == null))
            {
                ConfigItem it2 = new ConfigItem();
                it2.Name = "um";
                it2.Value = item.codGrupo + " - " + item.nome;
                it2.Mode = ParameterMode.Value;

                ConfigItem it = new ConfigItem();
                it.Name = "dois";
                it.Value = Convert.ToString(item.idGrupo);
                it.Mode = ParameterMode.Value;

                Ext.Net.TreeNode n = new Ext.Net.TreeNode();
                n.NodeID = Convert.ToString(item.idGrupo);
                n.Text = item.codGrupo + " - " + item.nome;
                n.CustomAttributes.Add(it2);
                n.CustomAttributes.Add(it);
                n.Icon = Icon.Folder;
                criaNodes(n, lista, item.idGrupo);
                root1.Nodes.Add(n);
            }
            
            return root;
        }

        //==============================================================================================
        // Função para carregar a store Observações
        protected void CarregaObservacoes()
        {
            ObservacaoControl ct = new ObservacaoControl();

            IQueryable<EOBSERVACAO> lista = (IQueryable<EOBSERVACAO>) ct.ExecutaFuncao(null, Funcoes.BuscarLista, null);

            var dados = from a in lista
                        select new
                                   {
                                       id = a.idObs,
                                       descricao = a.descricao
                                   };

            storeListaObservacoes.DataSource = dados;
            storeListaObservacoes.DataBind();

        }

        //==============================================================================================
        // Função para carregar a store principal
        private Ext.Net.TreeNode CarregaPrincipal(Ext.Net.TreeNode root)
        {
            root.Text = "Grupos";

            Global.RegrasNegocio.Estoque.GruposControl control = new Global.RegrasNegocio.Estoque.GruposControl();
            IQueryable<EGRUPO> lista = (IQueryable<EGRUPO>)control.ExecutaFuncao(null, Funcoes.BuscarLista, null);


            foreach (var item in lista.Where(b => b.ativo == true && b.idGrupoPai == null))
            {
                ConfigItem it2 = new ConfigItem();
                it2.Name = "um";
                it2.Value = item.codGrupo + " - " + item.nome;
                it2.Mode = ParameterMode.Value;

                ConfigItem it = new ConfigItem();
                it.Name = "dois";
                it.Value = Convert.ToString(item.idGrupo);
                it.Mode = ParameterMode.Value;

                Ext.Net.TreeNode n = new Ext.Net.TreeNode();
                n.NodeID = Convert.ToString(item.idGrupo);
                n.Text = item.codGrupo + " - " + item.nome;
                n.CustomAttributes.Add(it2);
                n.CustomAttributes.Add(it);
                n.Icon = Icon.Folder;
                criaNodes(n, lista, item.idGrupo);
                root.Nodes.Add(n);
            }

            return root;
        }
        
        //==============================================================================================
        public void criaNodes(Ext.Net.TreeNode nodee, IQueryable<EGRUPO> lista, int pai)
        {
            foreach (var item in lista.Where(b => b.idGrupoPai == pai && b.ativo == true))
            {
                ConfigItem it2 = new ConfigItem();
                it2.Name = "um";
                it2.Value = item.codGrupo + " - " + item.nome;
                it2.Mode = ParameterMode.Value;

                ConfigItem it = new ConfigItem();
                it.Name = "dois";
                it.Value = Convert.ToString(item.idGrupo);
                it.Mode = ParameterMode.Value;

                Ext.Net.TreeNode n = new Ext.Net.TreeNode();
                n.NodeID = Convert.ToString(item.idGrupo);
                n.Text = item.codGrupo + " - " + item.nome;
                n.CustomAttributes.Add(it2);
                n.CustomAttributes.Add(it);
                n.Icon = Icon.Folder;
                criaNodes(n, lista, item.idGrupo);

                nodee.Nodes.Add(n);
            }
        }

        //==============================================================================================
        // Função para abrir janela de edição e adição
        protected void AbrirJanela(object sender, DirectEventArgs e)
        {
            GPERFIL perfil = new GPERFIL();
            Global.RegrasNegocio.Global.PerfilControl per = new Global.RegrasNegocio.Global.PerfilControl();
            perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
            perfil = (GPERFIL) per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
            List<string> compl = new List<string>();
            compl.Add("AD");
            compl.Add("Grupos");
            compl.Add("0");

            compl[2] = "18";
            bool result2 = (bool) per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

            if (result2)
            {
                //Limpando os campos
                hd_id.Value = "";
                nome.Text = "";
                codigo.Text = "";
                pai.Value = null;

                //Campo oculto para controle de edição
                hd_tipo.Value = 0;

                //Zerando a grid observações
                RowSelectionModel sm = GridObservacoes.SelectionModel.Primary as RowSelectionModel;
                sm.ClearSelections();
                
                
                //Abre a janela
                WindowPrincipal.Show();
            }
            else
            {
                X.Msg.Alert("Alerta", "Você não tem permissão de executar essa ação, contate o administrador").
                    Show();
            }
        }

        [DirectMethod]
        public void AbrirFilho(int idpai)
        {
            string cod = "";

            EGRUPO eg = new EGRUPO();
            Global.RegrasNegocio.Estoque.GruposControl control = new Global.RegrasNegocio.Estoque.GruposControl();

            eg.idGrupo = Convert.ToInt32(idpai);

            eg = (EGRUPO) control.ExecutaFuncao(eg, Funcoes.Buscar, null);

            cod = eg.codGrupo;

            GPERFIL perfil = new GPERFIL();
            Global.RegrasNegocio.Global.PerfilControl per = new Global.RegrasNegocio.Global.PerfilControl();
            perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
            perfil = (GPERFIL) per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
            List<string> compl = new List<string>();
            compl.Add("AD");
            compl.Add("Grupos");
            compl.Add("0");

            compl[2] = "18";
            bool result2 = (bool) per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

            if (result2)
            {
                //Limpando os campos
                hd_id.Value = "";
                nome.Text = "";
                codigo.Text = "";
                codigo.FieldLabel += " - prefixo " + cod;
                pai.Value = idpai;

                //Campo oculto para controle de edição
                hd_tipo.Value = 0;

                //Zerando a grid observações
                RowSelectionModel sm = GridObservacoes.SelectionModel.Primary as RowSelectionModel;
                sm.ClearSelections();
                
                
                //Abre a janela
                WindowPrincipal.Show();
            }
            else
            {
                X.Msg.Alert("Alerta", "Você não tem permissão de executar essa ação, contate o administrador").
                    Show();
            }

        }

        [DirectMethod]
        public void Editar(int id)
        {

            //Buscando o fornecedor
            EGRUPO obj = new EGRUPO();

            Global.RegrasNegocio.Estoque.GruposControl control = new Global.RegrasNegocio.Estoque.GruposControl();
            obj.idGrupo = id;
            obj.idEmpresa = Memoria.Empresa;

            obj = (EGRUPO) control.ExecutaFuncao(obj, Funcoes.Buscar, null);

            string cod = obj.codGrupo;
            
            GPERFIL perfil = new GPERFIL();
            Global.RegrasNegocio.Global.PerfilControl per = new Global.RegrasNegocio.Global.PerfilControl();
            perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
            perfil = (GPERFIL) per.ExecutaFuncao(perfil, Funcoes.Buscar, null);
            List<string> compl = new List<string>();
            compl.Add("AD");
            compl.Add("Grupos");
            compl.Add("0");

            compl[2] = "2";
            bool result2 = (bool) per.ExecutaFuncao(perfil, Funcoes.Verificar, compl);

            if (result2)
            {
                hd_id.Value = obj.idGrupo;
                nome.Value = obj.nome;
                pai.Value = obj.idGrupoPai;
                codigo.Value = obj.codReduzido;
                ativo.Checked = Convert.ToBoolean(obj.ativo);

                //Campos ocultos
                hd_tipo.Value = 1;

                //Carrega as OBSERVAÇÕES  que foram inseridos na guia "Observações"

                RowSelectionModel sm = GridObservacoes.SelectionModel.Primary as RowSelectionModel;

                sm.ClearSelections();
                foreach (EOBSERVACAO item in obj.EOBSERVACAO)
                {
                    sm.SelectedRows.Add(new SelectedRow(Convert.ToString(item.idObs)));
                }
                sm.UpdateSelection();

                WindowPrincipal.Show();
            }
            else
            {
                X.Msg.Alert("Alerta", "Você não tem permissão de executar essa ação, contate o administrador").
                    Show();
            }

        }

        //==============================================================================================
        [DirectMethod]
        public void ExcluirItem(int id)
        {
            //Buscando o fornecedor
            EGRUPO obj = new EGRUPO();
            
            Global.RegrasNegocio.Estoque.GruposControl control = new Global.RegrasNegocio.Estoque.GruposControl();
            obj.idGrupo = id;
            obj = (EGRUPO)control.ExecutaFuncao(obj, Funcoes.Buscar, null);

           
            bool result = (bool)control.ExecutaFuncao(obj, Funcoes.Cancelar, null); 
            
            if(result)
            {
                //excluido
                X.Msg.Alert("Excluido com sucesso", "Excluido com sucesso.");
                Response.Redirect(Request.Url.ToString(), true);
            }
            else
            {
                //erro ao excluir
                X.Msg.Alert("Alerta", Memoria.MsgGlobal).Show();
            }
        }

        //==============================================================================================
        // Função para salvar dados
        protected void SalvarDados(object sender, EventArgs e)
        {
            EGRUPO obj = new EGRUPO();
            Global.RegrasNegocio.Estoque.GruposControl control = new Global.RegrasNegocio.Estoque.GruposControl();

            Funcoes acao = Funcoes.Adicionar;

            if (hd_id.Value != "")
                obj.idGrupo = Convert.ToInt32(hd_id.Value);

            if (hd_tipo.Value.ToString() == "1") // O objeto foi editado.
            {
                obj = (EGRUPO) control.ExecutaFuncao(obj, Funcoes.Buscar, null);
                acao = Funcoes.Atualizar;
            }

            string prefixo = "";

            obj.nome = Convert.ToString(nome.Value);
            if (pai.Value != null && pai.Value != "")
            {
                obj.idGrupoPai = Convert.ToInt32(pai.Value);

                EGRUPO eg = new EGRUPO();
                Global.RegrasNegocio.Estoque.GruposControl control2 = new Global.RegrasNegocio.Estoque.GruposControl();

                eg.idGrupo = Convert.ToInt32(pai.Value);

                eg = (EGRUPO) control2.ExecutaFuncao(eg, Funcoes.Buscar, null);

                prefixo = eg.codGrupo + ".";
            }
            else
                obj.idGrupoPai = null;

            obj.codGrupo = prefixo + codigo.Text;
            obj.codReduzido = codigo.Text;
            obj.nome = nome.Text;

            if (ativo.Checked)
                obj.ativo = true;
            else
                obj.ativo = false;

            obj.idEmpresa = Memoria.Empresa;

            //Recebendo as OBSERVAÇÕES escolhidas pelo usuario e adicionando ao GRUPO.
            RowSelectionModel sm = GridObservacoes.SelectionModel.Primary as RowSelectionModel;

            obj.EOBSERVACAO.Clear();
            foreach (SelectedRow row in sm.SelectedRows)
            {
                EOBSERVACAO obs = new EOBSERVACAO();
                ObservacaoControl ct = new ObservacaoControl();
                obs.idObs = Convert.ToInt32(row.RecordID); //row.RecordID é retorno do IDProperty="id" definido na store

                obs = (EOBSERVACAO)ct.ExecutaFuncao(obs, Funcoes.Buscar, null);

                obj.EOBSERVACAO.Add(obs);
            }
            
            
            bool result = (bool) control.ExecutaFuncao(obj, acao, null);

            
            
            if (result)
            {
                X.Msg.Alert("Alerta", "Dados gravados com sucesso").Show();
                WindowPrincipal.Hide();
                Response.Redirect(Request.Url.ToString(), true);
            }
            else
                X.Msg.Alert("Alerta", "Verifique os dados digitados e tente novamente").Show();

            //CarregaPrincipal();
            //Ext.Net.TreeNode root2 = new Ext.Net.TreeNode();
            
            //ColumnTree1.SetRootNode(CarregaPrincipal(root2));
            //ColumnTree1.AddScript("location.reload();");

            CarregaPrincipal();
        }
        //==============================================================================================

        //==============================================================================================
        // Funçao para fechar janela
        protected void FecharJanela(object sender, EventArgs e)
        {
            WindowPrincipal.Hide();
        }
        //==============================================================================================

        //==============================================================================================
        // Função das ações chamadas pela grid
        protected void GridAcao(object sender, DirectEventArgs e)
        {
            
        }
        //==============================================================================================

        private Ext.Net.TreeNodeCollection BuildTree(Ext.Net.TreeNodeCollection nodes)
        {
            if (nodes == null)
            {
                nodes = new Ext.Net.TreeNodeCollection();
            }

            Ext.Net.TreeNode root = new Ext.Net.TreeNode();
            root.Text = "Root";
            nodes.Add(root);

            string prefix = DateTime.Now.Second + "_";
            for (int i = 0; i < 10; i++)
            {
                Ext.Net.TreeNode node = new Ext.Net.TreeNode();
                node.Text = prefix + i;
                root.Nodes.Add(node);
            }

            return nodes;
        }
        
        [DirectMethod]
        public string RefreshMenu()
        {
            Ext.Net.TreeNodeCollection nodes = this.BuildTree(null);

            return nodes.ToJson();
        }
        
       
    }
}