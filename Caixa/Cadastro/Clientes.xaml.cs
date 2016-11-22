using System.ComponentModel;
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
    /// Interaction logic for Clientes.xaml
    /// </summary>
    public partial class Clientes : UserControl, IPagina
    {
        public Clientes()
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
            var fcontrol = new FornecedorControl();
            var forn = new GCLIFOR();
            forn.tpClifor = 1;

            IQueryable<GCLIFOR> lll = fcontrol.BuscarListaEspecifica(forn);
            var lista = from a in lll
                        select new
                                   {
                                       a.nomeFantasia,
                                       telefone = a.GCLIFOREND.FirstOrDefault(e => e.tipoEndereco == 1).telefone1,
                                       cpfcnpj = a.cpfcpnj,
                                       a.GCLIFOREND.FirstOrDefault(e => e.tipoEndereco == 1).email,
                                       a.GCLIFOREND.FirstOrDefault(e => e.tipoEndereco == 1).cidade,
                                       a.ativo
                                   };

            gridDados.ItemsSource = lista;
            gridDados.Rebind();
            gridDados.Focus();
        }

        private void Adicionar_Click(object sender, RoutedEventArgs e)
        {
            var frm = new FormClientes(null);
            WindowUtil.MostraModal();
            frm.ShowDialog();
            WindowUtil.FechaModal();

            Buscar();
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            if (gridDados.SelectedItem != null)
            {
                var cliente = new GCLIFOR();

                foreach (object it in gridDados.SelectedItems)
                {
                    var t = (string) TypeDescriptor.GetProperties(it)[1].GetValue(it);

                    cliente =
                        Contexto.Atual.GCLIFOR.Where(
                            r => r.GCLIFOREND.FirstOrDefault(s => s.tipoEndereco == 1).telefone1 == t).FirstOrDefault();


                    if (cliente == null)
                    {
                        t = (string) TypeDescriptor.GetProperties(it)[2].GetValue(it);
                        cliente = Contexto.Atual.GCLIFOR.Where(r => r.cpfcpnj == t).FirstOrDefault();
                    }
                }

                var frm = new FormClientes(cliente);
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
            if (e.Row.Item != null)
            {
                var cliente = new GCLIFOR();

                foreach (object it in gridDados.SelectedItems)
                {
                    var t = (string) TypeDescriptor.GetProperties(it)[1].GetValue(it);

                    cliente =
                        Contexto.Atual.GCLIFOR.Where(
                            r => r.GCLIFOREND.FirstOrDefault(s => s.tipoEndereco == 1).telefone1 == t).FirstOrDefault();


                    if (cliente == null)
                    {
                        t = (string) TypeDescriptor.GetProperties(it)[2].GetValue(it);
                        cliente = Contexto.Atual.GCLIFOR.Where(r => r.cpfcpnj == t).FirstOrDefault();
                    }
                }

                var frm = new FormClientes(cliente);
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