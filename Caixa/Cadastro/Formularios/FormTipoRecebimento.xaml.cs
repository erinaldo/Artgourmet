using System;
using System.Windows;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Caixa;

namespace Artebit.Restaurante.Caixa.Cadastro.Formularios
{
    /// <summary>
    /// Interaction logic for FormTipoRecebimento.xaml
    /// </summary>
    public partial class FormTipoRecebimento
    {
        private readonly AFORMAPGTO _forma = new AFORMAPGTO();

        public FormTipoRecebimento()
        {
            InitializeComponent();
            cbTipoPagamento.ItemsSource = Contexto.Atual.ATIPOPGTO;
        }

        public FormTipoRecebimento(AFORMAPGTO obj)
        {
            InitializeComponent();

            cbTipoPagamento.ItemsSource = Contexto.Atual.ATIPOPGTO;
            DataContext = obj;
            _forma = obj;

            CarregarInfo();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void CarregarInfo()
        {
            //Carregando dados nos campos
            var obj = DataContext as AFORMAPGTO;

            if (obj != null)
            {
                txtboxDescricao.Value = obj.descricao;
                checkAtivo.IsChecked = obj.ativo;
                txtOrdem.Text = Convert.ToString(obj.ordem);

                cbTipoPagamento.SelectedItem = obj.ATIPOPGTO;
            }
        }

        private bool validaDados()
        {
            if (Convert.ToString(txtboxDescricao.Value) == "")
            {
                Alert("Informe a descricao.");
                return false;
            }

            if(txtOrdem.Text != "")
            {
                int valor;
                if (!int.TryParse(txtOrdem.Text,out valor))
                {
                    Alert("Informe uma ordem em formato numérico.");
                    return false;
                }
            }

            return true;
        }

        private void SalvarDados()
        {
            if (validaDados())
            {
                int? ordem = null;

                if (!string.IsNullOrEmpty(txtOrdem.Text))
                    ordem = Convert.ToInt32(txtOrdem.Text);

                _forma.descricao = Convert.ToString(txtboxDescricao.Value);
                _forma.tipo = Convert.ToString(cbTipoPagamento.SelectedValue);
                _forma.ativo = checkAtivo.IsChecked;
                _forma.ordem = ordem;
                _forma.ATIPOPGTO = cbTipoPagamento.SelectedItem as ATIPOPGTO;

                //Verificando se é uma alteração ou inclusão

                Funcoes acao = DataContext == null ? Funcoes.Adicionar : Funcoes.Atualizar;

                //Atualizando no banco
                var controle = new FormaPagtoControl();

                bool result = acao == Funcoes.Adicionar ? controle.Criar(_forma) : controle.Atualizar(_forma);

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