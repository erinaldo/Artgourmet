using System.Windows;
using Artebit.Restaurante.Caixa.Cadastro.Formularios;
using Artebit.Restaurante.Caixa.Classes;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.Util.WPF;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Artebit.Restaurante.Caixa.Cadastro
{
    /// <summary>
    /// Interaction logic for TipoRecebimento.xaml
    /// </summary>
    public partial class TipoRecebimento : IPagina
    {
        public TipoRecebimento()
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

            gridDados.ItemsSource = Contexto.Atual.AFORMAPGTO;
        }

        private void Adicionar_Click(object sender, RoutedEventArgs e)
        {
            WindowUtil.MostraModal();
            var form = new FormTipoRecebimento();
            form.ShowDialog();
            gridDados.Rebind();
            WindowUtil.FechaModal();
        }

        private void gridDados_RowActivated(object sender, RowEventArgs e)
        {
            var ff = new FormTipoRecebimento(e.Row.Item as AFORMAPGTO);

            WindowUtil.MostraModal();
            ff.ShowDialog();
            gridDados.Rebind();
            WindowUtil.FechaModal();
        }

        private void EditarItem()
        {
            if (gridDados.SelectedItem != null)
            {
                var ff = new FormTipoRecebimento(gridDados.SelectedItem as AFORMAPGTO);

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