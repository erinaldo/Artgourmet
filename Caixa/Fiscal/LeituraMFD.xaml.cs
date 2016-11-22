using System;
using System.Windows;
using Microsoft.Win32;

namespace Artebit.Restaurante.Caixa.Fiscal
{
    /// <summary>
    /// Interaction logic for LeituraMFD.xaml
    /// </summary>
    public partial class LeituraMFD
    {
        private readonly int _tipo;

        public LeituraMFD(int tipo)
        {
            InitializeComponent();

            _tipo = tipo;

            if (tipo == 2)
            {
                lbTitulo.Content = "Geração Arquivo MFD";
                //groupFaixa.IsEnabled = true;
                groupPeriodo.IsEnabled = true;
                rbtReducao.IsEnabled = true;
            }
        }

        private void btOK_Click(object sender, RoutedEventArgs e)
        {
//            ImpressoraFiscal.Impressora imp = new ImpressoraFiscal.Impressora();
            if (_tipo == 1)
            {
                //imp.GeraEspelhoMFD(txtArquivo.Text, data1.SelectedDate, data2.SelectedDate);
            }
            else
            {
                if (rbtData.IsChecked != null && rbtData.IsChecked.Value)
                {
                    //imp.GeraArquivoMFD(txtArquivo.Text, data1.SelectedDate.Value.ToString("dd/MM/yyyy"), data2.SelectedDate.Value.ToString("dd/MM/yyyy"), "D");
                }
                else
                {
                    string red1 = reducao1.Value.ToString();
                    string red2 = reducao2.Value.ToString();

                    while (red1.Length < 6)
                        red1 = "0" + red1;

                    while (red2.Length < 6)
                        red2 = "0" + red2;

                    //imp.GeraArquivoMFD(txtArquivo.Text,red1,red2, "C");
                }
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

        private void rbtReducao_Checked(object sender, RoutedEventArgs e)
        {
            groupPeriodo.IsEnabled = false;
            groupFaixa.IsEnabled = true;
        }

        private void rbtData_Checked(object sender, RoutedEventArgs e)
        {
            groupPeriodo.IsEnabled = true;
            groupFaixa.IsEnabled = false;
        }
    }
}