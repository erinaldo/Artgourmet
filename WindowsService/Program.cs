using System.Globalization;
using System.ServiceProcess;
using System.Threading;

namespace Artebit.Restaurante.ArtebitGourmetWS
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            var c = new CultureInfo("pt-BR");

            Thread.CurrentThread.CurrentCulture = c;
            Thread.CurrentThread.CurrentUICulture = c;

            var servicesToRun = new ServiceBase[]
                                    {
                                        new Service1()
                                    };
            ServiceBase.Run(servicesToRun);
        }
    }
}