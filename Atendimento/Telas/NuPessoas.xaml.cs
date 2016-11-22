using System;
using System.Windows;
using System.Windows.Input;

namespace Artebit.Restaurante.AtendimentoPDV.Telas
{
    /// <summary>
    /// Interaction logic for Transferir.xaml
    /// </summary>
    public partial class NuPessoas
    {
        public int Resultado = 0;

        public NuPessoas()
        {
            InitializeComponent();

            txt_mesa.Focus();
        }

        public void CarregarInfo()
        {
        }


        private void btOK_Click()
        {
            if (txt_mesa.Text != "")
            {
                if (Convert.ToInt32(txt_mesa.Text) <= 0)
                {
                    Alert("A mesa não pode estar ocupada por menos de 1 pessoa.");
                }
                else
                {
                    Resultado = Convert.ToInt32(txt_mesa.Text);
                    Close();
                }
            }
            else
            {
                Alert("Informe a quantidade de pessoas.");
            }
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            Resultado = 0;
            Close();
        }


        // Função chamada quando o botão é criado, arruma o tamanho da letra

        private void entrar(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                if (txt_mesa.Text != "")
                {
                    btOK_Click();
                }
            }
        }
    }
}