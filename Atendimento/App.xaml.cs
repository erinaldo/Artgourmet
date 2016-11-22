using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Threading;
using Artebit.Restaurante.AtendimentoPDV.Telas;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.Util.WPF;
using Resolution;
using Telerik.Windows.Controls;

namespace Artebit.Restaurante.AtendimentoPDV
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            WindowUtil.MostraModal();
            var fo = new ErrosWindow(e.Exception);
            fo.ShowDialog();
            WindowUtil.FechaModal();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Screen srn = Screen.PrimaryScreen;
            Memoria.WidthScreen = srn.Bounds.Width;
            Memoria.HeightScreen = srn.Bounds.Height;

            if (srn.Bounds.Width < 1024)
            {
                //mudar resolução para 1024 por 768
                new CResolution(1024, 768);
            }

            StyleManager.ApplicationTheme = new MetroTheme();
            //MetroColors.PaletteInstance.MainColor = Colors.Black;

            // ReSharper disable PossibleNullReferenceException
            MetroColors.PaletteInstance.AccentColor = (Color) ColorConverter.ConvertFromString("#B68944");
            // ReSharper restore PossibleNullReferenceException

            //MetroColors.PaletteInstance.BasicColor = Colors.DarkGray;
            //MetroColors.PaletteInstance.StrongColor = Colors.Gray;
            //MetroColors.PaletteInstance.MarkerColor = Colors.LightGray;
            //MetroColors.PaletteInstance.ValidationColor = Colors.Red;

            TextElement.FontFamilyProperty.OverrideMetadata(
                typeof (TextElement),
                new FrameworkPropertyMetadata(
                    new FontFamily("Segoe UI")));

            TextBlock.FontFamilyProperty.OverrideMetadata(
                typeof (TextBlock),
                new FrameworkPropertyMetadata(
                    new FontFamily("Segoe UI")));

            var c = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentCulture = c;
            Thread.CurrentThread.CurrentUICulture = c;

            var splash = new Splash();
            splash.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (Memoria.WidthScreen < 1024)
            {
                //mudar resolução para 1024 por 768
                new CResolution(Memoria.WidthScreen, Memoria.HeightScreen);
            }
        }
    }
}