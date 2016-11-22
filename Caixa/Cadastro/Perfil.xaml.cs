using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
    /// Interaction logic for Perfil.xaml
    /// </summary>
    public partial class Perfil : IPagina
    {
        public Perfil()
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
            var control = new PerfilControl();

            IQueryable<GPERFIL> lista = control.BuscarLista();
            gridDados.ItemsSource = lista;
        }

        private void Adicionar_Click(object sender, RoutedEventArgs e)
        {
            var ff = new FormPerfil();

            WindowUtil.MostraModal();
            ff.ShowDialog();
            gridDados.Rebind();
            WindowUtil.FechaModal();
        }

        private void gridDados_RowActivated(object sender, RowEventArgs e)
        {
            var objeto = e.Row.Item as GPERFIL;
            var ff = new FormPerfil(objeto);

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
                var ff = new FormPerfil(gridDados.SelectedItem as GPERFIL);

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
