using System.ComponentModel;
using System.Linq;
using System.Windows;
using Artebit.Restaurante.Caixa.Cadastro;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Artebit.Restaurante.Global.Util.WPF;

namespace Artebit.Restaurante.Caixa.Caixas.Janelas
{
    /// <summary>
    /// Interaction logic for Cliente.xaml
    /// </summary>
    public partial class Cliente
    {
        public GCLIFOR Client = new GCLIFOR();

        public Cliente()
        {
            InitializeComponent();
            CarregarInfo();
            Buscar();
        }

        public void CarregarInfo()
        {
        }

        private void Buscar()
        {
            var fcontrol = new FornecedorControl();
            var forn = new GCLIFOR {tpClifor = 1};

            IQueryable<GCLIFOR> lll = fcontrol.BuscarListaEspecifica(forn);

            var lista = from a in lll
                        select new
                                   {
                                       a.nomeFantasia,
                                       telefone = a.GCLIFOREND.FirstOrDefault(e => e.tipoEndereco == 1).telefone1,
                                       cpfcnpj = a.cpfcpnj
                                   };


            gridDados.ItemsSource = lista;
            gridDados.Rebind();
            gridDados.Focus();
        }

        private void btnNovo_Click(object sender, RoutedEventArgs e)
        {
            var frm = new FormClientes(null);
            WindowUtil.MostraModal();
            frm.ShowDialog();
            WindowUtil.FechaModal();

            Client = frm.cliente;

            gridDados.Rebind();
        }

        private void btnPesquisar_Click(object sender, RoutedEventArgs e)
        {
            var lll =
                Contexto.Atual.GCLIFOR.Where(
                    r =>
                    r.tpClifor == 1 &&
                    (r.cpfcpnj == txtCpfCnpj.Text || txtCpfCnpj.Text == "") &&
                    (r.GCLIFOREND.FirstOrDefault(s => s.tipoEndereco == 1).telefone1 == txtTelefone.Text ||
                     txtTelefone.Text == ""));

            var lista = from a in lll
                        select new
                                   {
                                       a.nomeFantasia,
                                       telefone = a.GCLIFOREND.FirstOrDefault(f => f.tipoEndereco == 1).telefone1,
                                       cpfcnpj = a.cpfcpnj
                                   };


            gridDados.ItemsSource = lista;
            gridDados.Rebind();
            gridDados.Focus();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (gridDados.SelectedItems.Count > 0)
            {
                foreach (object it in gridDados.SelectedItems)
                {
                    var t = (string) TypeDescriptor.GetProperties(it)[1].GetValue(it);

                    Client =
                        Contexto.Atual.GCLIFOR.FirstOrDefault(r => r.GCLIFOREND.FirstOrDefault(s => s.tipoEndereco == 1).telefone1 == t);
                }
            }
            Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Client.telefone))
            {
                Client.telefone = "";
            }

            Close();
        }
    }
}