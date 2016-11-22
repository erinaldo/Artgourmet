using Artebit.Restaurante.Caixa.Classes;
using System.Windows;

namespace Artebit.Restaurante.Caixa.Fiscal
{
    /// <summary>
    /// Interaction logic for MenuFiscal.xaml
    /// </summary>
    public partial class MenuFiscal
    {
        public MenuFiscal()
        {
            InitializeComponent();
        }

        private void btLX_Click(object sender, RoutedEventArgs e)
        {
            Impressoras.Fiscal.ECF.ImpressoraFiscal.ImprimirLeituraX(PaginaCore.MainWindow.busyIndicator);
        }

        private void btLMFC_Click(object sender, RoutedEventArgs e)
        {
            var mm = new MemoriaFiscal(1);
            mm.ShowDialog();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var mm = new MemoriaFiscal(2);
            mm.ShowDialog();
        }

        private void btespMFD_Click(object sender, RoutedEventArgs e)
        {
            var l = new LeituraMFD(1);
            l.ShowDialog();
        }

        private void btArqMFD_Click(object sender, RoutedEventArgs e)
        {
            var l = new LeituraMFD(2);
            l.ShowDialog();
        }

        private void btEstoque_Click(object sender, RoutedEventArgs e)
        {
            var es = new Estoque();
            es.ShowDialog();
        }

        private void btTabProdutos_Click(object sender, RoutedEventArgs e)
        {
            //Impressoras.Fiscal.ECF.ImpressoraFiscal.GeraTabelaProduto
            //imp.GeraTabelaProduto("C:\\TabelaProdutos.txt");
        }
    }
}