using System.Linq;
using System.Windows;

namespace Artebit.Restaurante.Global.Util.WPF
{
    public class WindowUtil
    {
        public static void MostraModal()
        {
            Window w = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.IsActive);

            if (w != null)
            {
                w.Opacity = 0.5;
            }
        }

        public static void FechaModal()
        {
            Window w = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.IsActive);

            if (w != null)
            {
                w.Opacity = 1;
            }
        }

        public static Window RetornaWindowMestre()
        {
            return Application.Current.Windows.Cast<Window>().FirstOrDefault();
        }
    }
}