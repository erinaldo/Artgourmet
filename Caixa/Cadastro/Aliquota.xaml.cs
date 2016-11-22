using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Artebit.Restaurante.Caixa.Classes;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio.Atendimento;
using Artebit.Restaurante.Global.Util.WPF;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Artebit.Restaurante.Caixa.Cadastro
{
    /// <summary>
    /// Interaction logic for Aliquota.xaml
    /// </summary>
    public partial class Aliquota : UserControl, IPagina
    {
        public Aliquota()
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
            var control = new AliquotaControl();

            IQueryable<AALIQUOTA> lista = control.BuscarLista();

            gridDados.ItemsSource = lista;
            gridDados.Rebind();
            gridDados.Focus();
        }

        private void Adicionar_Click(object sender, RoutedEventArgs e)
        {
            var frm = new FormAliquota(null);
            WindowUtil.MostraModal();
            frm.ShowDialog();
            WindowUtil.FechaModal();

            Buscar();
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            if (gridDados.SelectedItem != null)
            {
                var obj = gridDados.SelectedItem as AALIQUOTA;

                var frm = new FormAliquota(obj);
                WindowUtil.MostraModal();
                frm.ShowDialog();
                WindowUtil.FechaModal();

                Buscar();
            }
            else
            {
                RadWindow.Alert("Nenhum item selecionado.");
            }
        }

        private void gridDados_RowActivated(object sender, RowEventArgs e)
        {
            if (gridDados.SelectedItem != null)
            {
                var obj = gridDados.SelectedItem as AALIQUOTA;

                var frm = new FormAliquota(obj);
                WindowUtil.MostraModal();
                frm.ShowDialog();
                WindowUtil.FechaModal();

                Buscar();
            }
            else
            {
                RadWindow.Alert("Nenhum item selecionado.");
            }
        }
    }
}