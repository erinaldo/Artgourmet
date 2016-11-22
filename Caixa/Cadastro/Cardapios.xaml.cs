using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Artebit.Restaurante.Caixa.Classes;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio.Estoque;
using Artebit.Restaurante.Global.Util.WPF;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Artebit.Restaurante.Caixa.Cadastro
{
    /// <summary>
    /// Interaction logic for Cardapios.xaml
    /// </summary>
    public partial class Cardapios : IPagina
    {
        public Cardapios()
        {
            InitializeComponent();
        }

        #region IPagina Members

        public void Carregar()
        {
            Buscar();
            gridDados.Focus();
        }

        #endregion

        private void Buscar()
        {
            var control = new CardapioControl();

            IQueryable<ECARDAPIO> lista = control.BuscarLista();
            gridDados.ItemsSource = lista;
        }

        private void Adicionar_Click(object sender, RoutedEventArgs e)
        {
            WindowUtil.MostraModal();
            var form = new FormCardapio(null);
            form.ShowDialog();
            gridDados.Rebind();
            WindowUtil.FechaModal();
        }

        private void gridDados_RowActivated(object sender, RowEventArgs e)
        {
            var ff = new FormCardapio(e.Row.Item as ECARDAPIO);

            WindowUtil.MostraModal();
            ff.ShowDialog();
            gridDados.Rebind();
            WindowUtil.FechaModal();
        }

        private void EditarItem()
        {
            if (gridDados.SelectedItem != null)
            {
                var ff = new FormCardapio(gridDados.SelectedItem as ECARDAPIO);

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

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.None)
            {
                switch (e.Key)
                {
                    case Key.F1:
                        Adicionar_Click(Adicionar, new RoutedEventArgs());
                        break;

                    case Key.F2:
                        EditarItem();
                        break;
                }
            }
        }
    }
}