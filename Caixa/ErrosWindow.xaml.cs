using System;
using System.Windows;
using Artebit.Restaurante.Global.RegrasNegocio;

namespace Artebit.Restaurante.Caixa
{
    /// <summary>
    /// Interaction logic for ErrosWindow.xaml
    /// </summary>
    public partial class ErrosWindow
    {
        public ErrosWindow(Exception ex)
        {
            InitializeComponent();

            textBox1.Text = ex.ToString();

            Excecao.TratarExcecao(ex);
        }

        private void btOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}