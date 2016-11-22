using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Caixa.Caixas.Janelas
{
    /// <summary>
    /// Interaction logic for Desconto.xaml
    /// </summary>
    public partial class Desconto
    {
        public string Resultado = "";
        private ACONTA _conta = new ACONTA();

        public bool DescontoConta;
        public List<ACONTITEM> Items = null;

        public Desconto()
        {
            InitializeComponent();

            txtDesconto.Focus();
        }

        public void CarregarInfo()
        {
            _conta = DataContext as ACONTA;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            decimal desconto = 0;
            if (Convert.ToString(txtDesconto.Value) != "")
                desconto = Convert.ToDecimal(txtDesconto.Value);

            if (DescontoConta)
            {
                _conta.desconto = Convert.ToDecimal(txtDesconto.Value);
            }
            else
            {
                foreach (ACONTITEM it in Items)
                {
                    it.desconto = it.preco > desconto ? Convert.ToDecimal(txtDesconto.Value) : Convert.ToDecimal(it.preco - Convert.ToDecimal(0.01));
                }
            }

            Alert("Desconto aplicado com sucesso!");

            Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtDesconto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                btnOK_Click(btnOK, new RoutedEventArgs());
            }
        }
    }
}