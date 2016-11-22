using Artebit.Restaurante.AtendimentoPDV.Classes;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.Modelo.Enum;

namespace Artebit.Restaurante.AtendimentoPDV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public bool FinalizaApp = true;

        public MainWindow()
        {
            InitializeComponent();

            ControlePagina.Frame = principal;

            Memoria.TipoConta = TipoConta.Mesa;
        }
    }
}