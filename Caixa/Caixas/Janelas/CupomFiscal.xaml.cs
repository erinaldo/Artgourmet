using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Caixa.Caixas.Janelas
{
    /// <summary>
    /// Interaction logic for CupomFiscal.xaml
    /// </summary>
    public partial class CupomFiscal : INotificaTela
    {
        public CupomFiscal()
        {
            InitializeComponent();
        }

        public void AtualizaValor(string valor)
        {
            txtTexto.Text += valor;
        }
    }
}
