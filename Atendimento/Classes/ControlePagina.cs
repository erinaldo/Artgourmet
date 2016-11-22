using System.Windows.Controls;
using System.Windows.Navigation;

namespace Artebit.Restaurante.AtendimentoPDV.Classes
{
    public static class ControlePagina
    {
        private static Frame _mainContentFrame;

        public static Frame Frame
        {
            set { _mainContentFrame = value; }
        }

        public static void NavigateTo(object target)
        {
            var pg = target as IPagina;
            pg.Carregar();

            _mainContentFrame.Navigate(target);
            _mainContentFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
        }
    }
}