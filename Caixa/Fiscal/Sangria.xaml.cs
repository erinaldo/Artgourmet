using System;
using System.Windows;

namespace Artebit.Restaurante.Caixa.Fiscal
{
    /// <summary>
    /// Interaction logic for Sangria.xaml
    /// </summary>
    public partial class Sangria
    {
        public Sangria()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            decimal valor = Convert.ToDecimal(txtValor.Value);

            if (Impressoras.Fiscal.ECF.ImpressoraFiscal.OperacaoNaoFiscal.ExecutarSangria(valor))
            {
                Alert("Retiro parcial executado com sucesso.");
                Close();
            }
            else
            {
                Alert("Erro ao se conectar ao ECF");
            }
        }
    }
}