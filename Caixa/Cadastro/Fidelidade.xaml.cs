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
    /// Interaction logic for Fidelidade.xaml
    /// </summary>
    public partial class Fidelidade : UserControl, IPagina
    {
        public Fidelidade()
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
            var control = new FidelidadeControl();

            IQueryable<AFIDELIDADE> lista = control.BuscarLista();

            gridDados.ItemsSource = lista;
            gridDados.Rebind();
            gridDados.Focus();
        }

        private void Adicionar_Click(object sender, RoutedEventArgs e)
        {
            var frm = new FormFidelidade(null);
            WindowUtil.MostraModal();
            frm.ShowDialog();
            WindowUtil.FechaModal();

            Buscar();
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            if (gridDados.SelectedItem != null)
            {
                var obj = gridDados.SelectedItem as AFIDELIDADE;

                var frm = new FormFidelidade(obj);
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
                var obj = gridDados.SelectedItem as AFIDELIDADE;

                var frm = new FormFidelidade(obj);
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