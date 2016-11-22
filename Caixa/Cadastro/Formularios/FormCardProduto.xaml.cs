using System.Collections.Generic;
using System.Windows;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.Modelo.ModelLight;
using Artebit.Restaurante.Global.RegrasNegocio.Estoque;
using Telerik.Windows.Controls;

namespace Artebit.Restaurante.Caixa.Cadastro
{
    /// <summary>
    /// Interaction logic for FormCardapioItem.xaml
    /// </summary>
    public partial class FormCardProduto : RadWindow
    {
        public List<EPRODUTO> Produtos = new List<EPRODUTO>();

        public FormCardProduto(ECARDAPIOITEM item)
        {
            InitializeComponent();

            DataContext = item;

            cbProdutos.ItemsSource = Memoria.Produtos;    
            
        }

        private void RadWindow_Closed(object sender, WindowClosedEventArgs e)
        {
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            var prd = new ProdutoControl();

            var filtro = new EPRODUTO();

            //filtro.nome = txtNomeProduto.Text;

            //gridDados.ItemsSource = prd.BuscarListaEspecifica(filtro);
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            Produtos.Clear();

            //foreach (EPRODUTO p in gridDados.SelectedItems)
            //{
            //    Produtos.Add(p);
            //}

            Close();
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}