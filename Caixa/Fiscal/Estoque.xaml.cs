using System;
using System.Windows;
using Microsoft.Win32;

namespace Artebit.Restaurante.Caixa.Fiscal
{
    /// <summary>
    /// Interaction logic for Estoque.xaml
    /// </summary>
    public partial class Estoque
    {
        public Estoque()
        {
            InitializeComponent();
        }

        private void btOK_Click(object sender, RoutedEventArgs e)
        {
            if (rbtTotal.IsChecked != null && rbtTotal.IsChecked.Value)
            {
                Impressoras.Fiscal.ECF.ImpressoraFiscal.GeraTabelaEstoque(txtArquivo.Text, 1, "", "");
            }
            else
            {
                Impressoras.Fiscal.ECF.ImpressoraFiscal.GeraTabelaEstoque(txtArquivo.Text, 2, txtCodigo.Text,
                                                                          txtDescricao.Text);
            }
        }

        private void btProcurar_Click(object sender, RoutedEventArgs e)
        {
            var fileBox = new OpenFileDialog
                              {
                                  InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer),
                                  Multiselect = false
                              };

            fileBox.ShowDialog();

            txtArquivo.Text = fileBox.FileName;
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}