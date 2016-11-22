using System;
using System.Windows;

namespace Artebit.Restaurante.Caixa.Fiscal
{
    /// <summary>
    /// Interaction logic for MemoriaFiscal.xaml
    /// </summary>
    public partial class MemoriaFiscal
    {
        private readonly int tipo;

        public MemoriaFiscal(int Tipo)
        {
            InitializeComponent();
            tipo = Tipo;
        }

        private void btOK_Click(object sender, RoutedEventArgs e)
        {
            int tipoDado = 0;
            string flag = "c";

            if (tipo == 2)
            {
                flag = "s";
            }

            if (rbtData.IsChecked.Value)
            {
                tipoDado = 1;
            }
            else
            {
                tipoDado = 2;
            }

            int? red1 = null;
            int? red2 = null;

            if (reducao1.Value != null && Convert.ToString(reducao1.Value) != "")
            {
                red1 = Convert.ToInt32(reducao1.Value);
            }

            if (reducao2.Value != null && Convert.ToString(reducao2.Value) != "")
            {
                red2 = Convert.ToInt32(reducao2.Value);
            }

            //   imp.LeituraMemoriaFiscal(tipoDado, flag, data1.SelectedDate, data2.SelectedDate, red1, red2, chkArquivo.IsChecked.Value, "");
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}