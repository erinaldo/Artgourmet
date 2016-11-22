using System;
using System.Linq;
using System.Windows;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Atendimento;
using Artebit.Restaurante.Global.RegrasNegocio.Estoque;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Telerik.Windows.Controls;

namespace Artebit.Restaurante.Caixa.Cadastro
{
    /// <summary>
    /// Interaction logic for Editar_InserirMesa.xaml
    /// </summary>
    public partial class FormImpressoras : RadWindow
    {
        private AIMPRESSORA impressora = new AIMPRESSORA();


        public FormImpressoras()
        {
            InitializeComponent();
            CarregaSources();
        }

        public FormImpressoras(AIMPRESSORA obj)
        {
            InitializeComponent();
            DataContext = obj;
            CarregaSources();
            CarregarInfo();
        }

        public void CarregaSources()
        {
            var mesaCTRL = new MesaControl();
            IQueryable<GMESA> mesas = mesaCTRL.BuscarLista();
            gridMesas.ItemsSource = mesas;

            var prdCTRL = new ProdutoControl();
            gridProdutos.ItemsSource = prdCTRL.BuscarLista();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void CarregarInfo()
        {
            //Carregando dados nos campos
            impressora = DataContext as AIMPRESSORA;

            txtboxDescricao.Value = impressora.descricao;
            txtboxligadoa.Value = impressora.ligadoa;
            txtboxmodelo.Value = impressora.modelo;
            txtIpImpressora.Value = impressora.IP;
            checkAtivo.IsChecked = impressora.ativo;
            checkProdutos.IsChecked = impressora.imprimeProdutos;

            /*
             * selecioa mesas da grid passando a lista de quais mesas devem estar selecionadas
             * a lista de mesas selecionadas vem do objeto impressora
             * */
            gridMesas.Select(from p in impressora.AIMPRESSMESA select p.GMESA);

            gridProdutos.Select(from p in impressora.AIMPRESSORAPRD select p.EPRODUTO);
        }

        private bool validaDados()
        {
            if (Convert.ToString(txtboxDescricao.Value) == "")
            {
                Alert("Informe a descricao.");
                return false;
            }

            if (Convert.ToString(txtIpImpressora.Value) == "")
            {
                Alert("Informe o ip da Impressora");
                return false;
            }

            return true;
        }

        private void SalvarDados()
        {
            if (validaDados())
            {
                #region Pega dados da aba Identificação

                //Recebendo dados modificados do usuário
                impressora.idEmpresa = Memoria.Empresa;
                impressora.idFilial = Memoria.Filial;
                impressora.descricao = Convert.ToString(txtboxDescricao.Value);
                impressora.ligadoa = Convert.ToString(txtboxligadoa.Value);
                impressora.modelo = Convert.ToString(txtboxmodelo.Value);
                impressora.ativo = Convert.ToBoolean(checkAtivo.IsChecked);
                impressora.IP = Convert.ToString(txtIpImpressora.Value);
                impressora.imprimeProdutos = Convert.ToBoolean(checkProdutos.IsChecked);

                #endregion

                #region Pega Mesas Selecionadas

                impressora.AIMPRESSMESA.Clear();


                //Inserindo nas mesas selecionadas o código da impressora
                foreach (GMESA m in gridMesas.SelectedItems)
                {
                    var imp = new AIMPRESSMESA();
                    imp.idEmpresa = impressora.idEmpresa;
                    imp.idFilial = impressora.idFilial;
                    imp.nuMesa = m.nuMesa;
                    imp.idImpressora = impressora.idImpressora;

                    impressora.AIMPRESSMESA.Add(imp);
                }

                #endregion

                #region Pega os Produtos Selecionados

                impressora.AIMPRESSORAPRD.Clear();


                //Inserindo nos produtos selecionadas o código da impressora
                foreach (EPRODUTO m in gridProdutos.SelectedItems)
                {
                    var imp = new AIMPRESSORAPRD();
                    imp.idEmpresa = impressora.idEmpresa;
                    imp.idFilial = impressora.idFilial;
                    imp.idImpressora = impressora.idImpressora;
                    imp.idProduto = m.idProduto;

                    impressora.AIMPRESSORAPRD.Add(imp);
                }

                #endregion

                //Verificando se é uma alteração ou inclusão
                Funcoes acao;

                if (DataContext == null)
                {
                    acao = Funcoes.Adicionar;
                }
                else
                {
                    acao = Funcoes.Atualizar;
                }

                //Atualizando no banco
                var controle = new ImpressoraControl();


                bool result = false;

                if (acao == Funcoes.Adicionar)
                {
                    result = controle.Criar(impressora);
                }
                else
                {
                    result = controle.Atualizar(impressora);
                }

                if (!result)
                    Alert("Verifique os dados digitados e tente novamente.");
                else
                    Close();
            }
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            SalvarDados();
        }
    }
}