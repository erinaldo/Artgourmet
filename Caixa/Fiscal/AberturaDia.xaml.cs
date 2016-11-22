using System;
using System.Windows;
using Artebit.Restaurante.Global.Modelo;
using Telerik.Windows.Controls;

namespace Artebit.Restaurante.Caixa.Fiscal
{
    /// <summary>
    /// Interaction logic for AberturaDia.xaml
    /// </summary>
    public partial class AberturaDia
    {
        public AberturaDia()
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
            decimal valor = Convert.ToDecimal(txtValor.Value);

            if (Impressoras.Fiscal.ECF.ImpressoraFiscal.OperacaoNaoFiscal.ExecutarSuprimento(valor))
            {
                Alert("Período aberto com sucesso.");
                Close();
            }
            else
            {
                Alert("Erro ao se conectar ao ECF");
            }
        }
    }
}