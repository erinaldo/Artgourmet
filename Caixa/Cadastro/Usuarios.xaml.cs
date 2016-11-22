using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Artebit.Restaurante.Caixa.Cadastro.Formularios;
using Artebit.Restaurante.Caixa.Classes;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Artebit.Restaurante.Global.Util.WPF;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Artebit.Restaurante.Caixa.Cadastro
{
    /// <summary>
    /// Interaction logic for Usuarios.xaml
    /// </summary>
    public partial class Usuarios : UserControl, IPagina
    {
        public Usuarios()
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
            var control = new UsuarioControl();

            IQueryable<GUSUARIO> lista = control.BuscarLista();
            gridDados.ItemsSource = lista;
        }

        private void Adicionar_Click(object sender, RoutedEventArgs e)
        {
            var ff = new FormUsuario();

            WindowUtil.MostraModal();
            ff.ShowDialog();
            gridDados.Rebind();
            WindowUtil.FechaModal();
        }

        private void gridDados_RowActivated(object sender, RowEventArgs e)
        {
            var objeto = e.Row.Item as GUSUARIO;
            var ff = new FormUsuario(objeto);

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
                var ff = new FormUsuario(gridDados.SelectedItem as GUSUARIO);

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