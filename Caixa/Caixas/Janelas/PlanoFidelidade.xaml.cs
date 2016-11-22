using System;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Windows;
using Artebit.Restaurante.Global.Modelo;
using Telerik.Windows.Controls;

namespace Artebit.Restaurante.Caixa.Caixas.Janelas
{
    /// <summary>
    /// Interaction logic for PlanoFidelidade.xaml
    /// </summary>
    public partial class PlanoFidelidade
    {
        private readonly EntityCollection<ARECFIDELIDADE> _recebimentos;
        private readonly ACONTA _conta = new ACONTA();

        public string Resultado = "";

        public PlanoFidelidade(ACONTA conta)
        {
            InitializeComponent();

            _conta = conta;
            comboPlanos.ItemsSource = Contexto.Atual.AFIDELIDADE;

            _recebimentos = conta.ARECFIDELIDADE;
            gridPrincipal.ItemsSource = _recebimentos;
        }

        private void radGridView1_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (VerificaValorRecebido())
            {
                Close();
            }
        }

        private bool VerificaValorRecebido()
        {
            decimal? totalRecebido = _recebimentos.Sum(r => r.valor);
            decimal total =
                _conta.ACONTITEM.Where(r => r.idStatus != 2).Sum(r => (r.quantidade*r.preco) - r.desconto).Value;

            if (totalRecebido < total)
            {
                Alert("Valor recebido é inferior ao total da conta.");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            var arec = new ARECFIDELIDADE
                           {
                               idEmpresa = _conta.idEmpresa,
                               idFilial = _conta.idFilial,
                               AFIDELIDADE = comboPlanos.SelectedItem as AFIDELIDADE,
                               valor = Convert.ToDecimal(txtValor.Value),
                               cpf = txtCpf.Value.ToString()
                           };
            //arec.idConta = conta.idConta;


            ARECFIDELIDADE receb1 =
                _recebimentos.SingleOrDefault(r => r.cpf == arec.cpf && r.AFIDELIDADE == arec.AFIDELIDADE);

            if (receb1 == null)
            {
                _recebimentos.Add(arec);
            }
            else
            {
                receb1.valor += arec.valor;
            }

            gridPrincipal.ItemsSource = _recebimentos;
            gridPrincipal.Rebind();

            txtValor.Value = null;
            txtCpf.Value = null;
        }

        private void btExcluir_Click(object sender, RoutedEventArgs e)
        {
            var lista = (from ARECFIDELIDADE item in gridPrincipal.SelectedItems from it in _recebimentos select it).ToList();

            foreach (ARECFIDELIDADE it in lista.Distinct())
            {
                _recebimentos.Remove(it);
            }
        }
    }
}