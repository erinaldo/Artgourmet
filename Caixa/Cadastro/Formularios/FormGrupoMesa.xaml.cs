using System;
using System.Linq;
using System.Windows;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Telerik.Windows.Controls;

namespace Artebit.Restaurante.Caixa.Cadastro
{
    /// <summary>
    /// Interaction logic for Editar_InserirMesa.xaml
    /// </summary>
    public partial class FormGrupoMesa : RadWindow
    {
        private RGRUPOMESA obj = new RGRUPOMESA();


        public FormGrupoMesa()
        {
            InitializeComponent();
            CarregaSources();
        }

        public FormGrupoMesa(RGRUPOMESA obj)
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
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void CarregarInfo()
        {
            //Carregando dados nos campos
            obj = DataContext as RGRUPOMESA;

            txtboxDescricao.Value = obj.descricao;
            txtboxqtdMax.Value = obj.quantMaxima;
            txtboxqtdMin.Value = obj.quantMinima;

            /*
             * selecioa mesas da grid passando a lista de quais mesas devem estar selecionadas
             * a lista de mesas selecionadas vem do objeto impressora
             * */
            gridMesas.Select(obj.GMESA);
        }

        private bool validaDados()
        {
            if (Convert.ToString(txtboxDescricao.Value) == "")
            {
                Alert("Informe a descricao.");
                return false;
            }

            if (Convert.ToString(txtboxqtdMax.Value) == "")
            {
                Alert("Informe a quantidade máxima.");
                return false;
            }

            if (Convert.ToString(txtboxqtdMin.Value) == "")
            {
                Alert("Informe a quantidade mínima.");
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
                obj.idEmpresa = Memoria.Empresa;
                obj.descricao = Convert.ToString(txtboxDescricao.Value);
                obj.quantMaxima = Convert.ToInt32(txtboxqtdMax.Value);
                obj.quantMinima = Convert.ToInt32(txtboxqtdMin.Value);

                #endregion

                #region Pega Mesas Selecionadas

                obj.GMESA.Clear();


                //Inserindo nas mesas selecionadas o código
                foreach (GMESA m in gridMesas.SelectedItems)
                {
                    obj.GMESA.Add(m);
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

                var controle = new GrupoMesaControl();


                bool result = false;

                if (acao == Funcoes.Adicionar)
                {
                    result = controle.Criar(obj);
                }
                else
                {
                    result = controle.Atualizar(obj);
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