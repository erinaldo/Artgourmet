using System.Windows.Controls;
using System.Windows.Navigation;

namespace Artebit.Restaurante.Caixa.Classes
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
            if (pg != null) pg.Carregar();

            _mainContentFrame.Navigate(target);
            _mainContentFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
        }

        public static void NavigateTo(object target, bool carregar)
        {
            if (carregar)
            {
                NavigateTo(target);
            }
            else
            {
                _mainContentFrame.Navigate(target);
                _mainContentFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            }
        }
    }
}