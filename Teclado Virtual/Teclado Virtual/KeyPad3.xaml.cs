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
using WindowsInput;

namespace VKbrd
{
    /// <summary>
    /// Interaction logic for KeyPad3.xaml
    /// </summary>
    public partial class KeyPad3 : UserControl
    {
        public KeyPad3()
        {
            InitializeComponent();

        }


        private void BotaoPressionado(object sender, RoutedEventArgs e)
        {
            if (Keyboard.FocusedElement is TextBox)
            {
                Button b = (sender as Button);
                string valor = b.Content.ToString();

                if (b.Name == "back")
                {
                    InputSimulator.SimulateKeyPress(VirtualKeyCode.BACK);
                }
                else if (b.Name == "enter")
                {
                    InputSimulator.SimulateKeyPress(VirtualKeyCode.RETURN);
                }
                else
                {
                    InputSimulator.SimulateTextEntry(valor);

                }
            }
        }


        public EventHandler<EventArgs> Entrar;

    }
}
