using System;
using System.Windows;
using System.Windows.Input;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio.Global;

namespace Artebit.Restaurante.Caixa.Caixas.Janelas
{
    /// <summary>
    /// Interaction logic for Pessoas.xaml
    /// </summary>
    public partial class Pessoas
    {
        public string Resultado = "";
        private ACONTA _conta = new ACONTA();

        public Pessoas()
        {
            InitializeComponent();
            txtQtdPessoas.Focus();
        }

        public void CarregarInfo()
        {
            _conta = DataContext as ACONTA;
            if (_conta != null) txtQtdPessoas.Value = _conta.pessoas;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            _conta.pessoas = Convert.ToInt32(txtQtdPessoas.Value);

            var preconta = new PreContaControl();

            bool result = preconta.Atualizar(_conta);

            if (result)
            {
                Resultado = "1";
            }

            Alert("Pessoas Atualizadas com sucesso!");

            Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtQtdPessoas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                btnOK_Click(btnOK, new RoutedEventArgs());
            }
        }
    }
}