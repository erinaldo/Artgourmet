using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;
using Artebit.Restaurante.Global.RegrasNegocio;
using Telerik.Windows.Controls;

namespace Artebit.Restaurante.Caixa
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            Excecao.TratarExcecao(e.Exception);
            var fo = new ErrosWindow(e.Exception);
            fo.ShowDialog();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
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
            //var p = new Caixas.Janelas.CupomFiscal();
            //p.Show();
            //var p = new Cadastro.FormCardapio(Contexto.Atual.ECARDAPIO.First());
            //p.Show();
        }
    }
}