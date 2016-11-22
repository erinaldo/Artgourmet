using System;
using System.Windows;
using System.Windows.Controls;

namespace Artebit.Restaurante.AtendimentoPDV.Controles
{
    /// <summary>
    /// Interaction logic for Cabecalho1.xaml
    /// </summary>
    public partial class Cabecalho1 : UserControl
    {
        /// <summary>
        /// The dependency property that gets or sets the nsource of the image to render.
        /// </summary>
        public static DependencyProperty SourceProperty = DependencyProperty.Register(
            "Titulo", typeof (String), typeof (Cabecalho1));

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
    }
}