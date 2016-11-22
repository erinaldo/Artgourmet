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
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.AtendimentoProducao
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Memoria.IdMonitor = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MONITOR"]);

            AMONITOR m =
                Contexto.Atual.AMONITOR.SingleOrDefault(
                    r =>
                    r.idMonitor == Memoria.IdMonitor && r.idEmpresa == Memoria.Empresa && r.idFilial == Memoria.Filial);
            if(m != null)
            {
                if(m.tipo == "B")
                {
                    principal.Content = new Bar();        
                }
                else
                {
                    principal.Content = new Cozinha();
                }
            }
        }
        private void AbrirPagina(object sender, ExecutedRoutedEventArgs e)
        {
            switch (e.Parameter.ToString())
            {
                case "Bar":
                    TituloSistema.Content = "Bar";
                    principal.Content = new Bar();
                    break;
                case "Cozinha":
                    TituloSistema.Content = "Cozinha";
                    principal.Content = new Cozinha();
                    break;
            }

        }

        private void BotoesAcao(object sender, ExecutedRoutedEventArgs e)
        {
            switch (e.Parameter.ToString())
            {
                case "Inicio":
                    TituloSistema.Content = "Inicio";
                    principal.Content = new MenuProducao();
                    break;
                case "Sair":
                    Memoria.Codusuario = "";
                    Memoria.Perfil = 0;

                    Login win = new Login();
                    win.Show();
                    this.Close();
                    break;
            } 
        }
    }
}
