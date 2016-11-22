using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Artebit.Restaurante.Caixa.Classes;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Artebit.Restaurante.Global.Util.WPF;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Artebit.Restaurante.Caixa.Cadastro
{
    /// <summary>
    /// Interaction logic for VendedoresUserControl.xaml
    /// </summary>
    public partial class Vendedores : UserControl, IPagina
    {
        public Vendedores()
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
            var vendedor = new GVENDEDOR();
            var control = new VendedorControl();

            IQueryable<GVENDEDOR> lista = control.BuscarLista();
            gridDados.ItemsSource = lista;
        }

        private void Adicionar_Click(object sender, RoutedEventArgs e)
        {
            WindowUtil.MostraModal();
            var form = new FormVendedor(null);
            form.ShowDialog();
            gridDados.Rebind();
            WindowUtil.FechaModal();
        }

        private void gridDados_RowActivated(object sender, RowEventArgs e)
        {
            var ff = new FormVendedor(e.Row.Item as GVENDEDOR);

            WindowUtil.MostraModal();
            ff.ShowDialog();
            gridDados.Rebind();
            WindowUtil.FechaModal();
        }

        private void EditarItem()
        {
            if (gridDados.SelectedItem != null)
            {
                var ff = new FormVendedor(gridDados.SelectedItem as GVENDEDOR);

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

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            EditarItem();
        }
    }
}