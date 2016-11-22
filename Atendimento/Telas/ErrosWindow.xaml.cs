using System;
using System.Windows;
using Telerik.Windows.Controls;

namespace Artebit.Restaurante.AtendimentoPDV.Telas
{
    /// <summary>
    /// Interaction logic for ErrosWindow.xaml
    /// </summary>
    public partial class ErrosWindow : RadWindow
    {
        public ErrosWindow(Exception ex)
        {
            InitializeComponent();
        }

        private void btOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}