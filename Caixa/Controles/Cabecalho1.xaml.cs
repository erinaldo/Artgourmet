using System;
using System.Windows;
using System.Windows.Controls;
using Artebit.Restaurante.Caixa.Classes;
using Artebit.Restaurante.Caixa.Fiscal;
using Artebit.Restaurante.Global.Util.WPF;

namespace Artebit.Restaurante.Caixa.Controles
{
    /// <summary>
    /// Interaction logic for Cabecalho1.xaml
    /// </summary>
    public partial class Cabecalho1
    {
        /// <summary>
        /// The dependency property that gets or sets the nsource of the image to render.
        /// </summary>
        public static DependencyProperty SourceProperty = DependencyProperty.Register(
            "Titulo", typeof (String), typeof (Cabecalho1));

        /// <summary>
        /// The dependency property that gets or sets the nsource of the image to render.
        /// </summary>
        public static DependencyProperty SourceProperty2 = DependencyProperty.Register(
            "BotaoInicioEvento", typeof (Boolean), typeof (Cabecalho1));

        public Cabecalho1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the nsource of the image to render.
        /// </summary>
        public string Titulo
        {
            get { return (string) GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the nsource of the image to render.
        /// </summary>
        public bool BotaoInicioEvento
        {
            get { return (bool) GetValue(SourceProperty2); }
            set { SetValue(SourceProperty2, value); }
        }

        private void btInicio_Click(object sender, RoutedEventArgs e)
        {
            if (BotaoInicioEvento)
            {
                ControlePagina.NavigateTo(PaginaCore.PgInicial, false);
            }
        }

        private void btMenuFiscal_Click(object sender, RoutedEventArgs e)
        {
            var m = new MenuFiscal();
            WindowUtil.MostraModal();
            m.ShowDialog();
            WindowUtil.FechaModal();
        }

        private void btMapaMesas_Click(object sender, RoutedEventArgs e)
        {
            ControlePagina.NavigateTo(PaginaCore.PgPDV_Mesas);
        }

        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            ControlePagina.NavigateTo(PaginaCore.PgLogin);
        }

        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            ControlePagina.NavigateTo(PaginaCore.PgInicial);
        }
    }
}