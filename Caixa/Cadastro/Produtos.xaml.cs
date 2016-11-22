using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Artebit.Restaurante.Caixa.Classes;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio.Estoque;
using Artebit.Restaurante.Global.Util.WPF;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Artebit.Restaurante.Caixa.Cadastro
{
    /// <summary>
    /// Interaction logic for Mesas.xaml
    /// </summary>
    public partial class Produtos : UserControl, IPagina
    {
        public Produtos()
        {
            InitializeComponent();
        }

        #region IPagina Members

        public void Carregar()
        {
            Buscar();
        }

        #endregion

        private void Buscar()
        {
            var control = new ProdutoControl();
            IQueryable<EPRODUTO> lista = control.BuscarLista();
            gridDados.ItemsSource = lista;
        }


        private void Adicionar_Click(object sender, RoutedEventArgs e)
        {
            WindowUtil.MostraModal();
            var form = new FormProdutos();
            form.ShowDialog();
            gridDados.Rebind();
            WindowUtil.FechaModal();
        }

        private void gridDados_RowActivated(object sender, RowEventArgs e)
        {
            var ff = new FormProdutos(e.Row.Item as EPRODUTO);

            WindowUtil.MostraModal();
            ff.ShowDialog();
            gridDados.Rebind();
            WindowUtil.FechaModal();
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            EditarItem();
        }

        private void EditarItem()
        {
            if (gridDados.SelectedItem != null)
            {
                var ff = new FormProdutos(gridDados.SelectedItem as EPRODUTO);

                WindowUtil.MostraModal();
                ff.ShowDialog();
                gridDados.Rebind();
                WindowUtil.FechaModal();
            }
            else
            {
                RadWindow.Alert("Nenhum item selecionado");
            }
        }
    }
}