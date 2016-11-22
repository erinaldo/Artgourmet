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
    /// Interaction logic for Impressora.xaml
    /// </summary>
    public partial class Impressoras : UserControl, IPagina
    {
        public Impressoras()
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
            var impressora = new AIMPRESSORA();
            var control = new ImpressoraControl();

            IQueryable<AIMPRESSORA> lista = control.BuscarLista();
            gridDados.ItemsSource = lista;
        }

        private void Adicionar_Click(object sender, RoutedEventArgs e)
        {
            WindowUtil.MostraModal();
            var form = new FormImpressoras();
            form.ShowDialog();
            gridDados.Rebind();
            WindowUtil.FechaModal();
        }

        private void gridDados_RowActivated(object sender, RowEventArgs e)
        {
            var ff = new FormImpressoras(e.Row.Item as AIMPRESSORA);

            WindowUtil.MostraModal();
            ff.ShowDialog();
            gridDados.Rebind();
            WindowUtil.FechaModal();
        }

        private void EditarItem()
        {
            if (gridDados.SelectedItem != null)
            {
                var ff = new FormImpressoras(gridDados.SelectedItem as AIMPRESSORA);

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