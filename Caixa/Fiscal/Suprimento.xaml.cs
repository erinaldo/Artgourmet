using System;
using System.Windows;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Caixa.Fiscal
{
    /// <summary>
    /// Interaction logic for Suprimento.xaml
    /// </summary>
    public partial class Suprimento
    {
        public Suprimento()
        {
            InitializeComponent();

            comboFormaPgto.ItemsSource = Contexto.Atual.AFORMAPGTO;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //var FormaPagamento = (comboFormaPgto.SelectedItem as AFORMAPGTO);
            decimal valor = Convert.ToDecimal(txtValor.Value);


            if (Impressoras.Fiscal.ECF.ImpressoraFiscal.OperacaoNaoFiscal.ExecutarSuprimento(valor))
            {
                Alert("Suprimento executado com sucesso.");
                Close();
            }
            else
            {
                Alert("Erro ao se conectar ao ECF");
            }
        }
    }
}