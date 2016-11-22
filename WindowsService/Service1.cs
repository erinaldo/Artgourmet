using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.ServiceProcess;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.ArtebitGourmetWS
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                using (var sw = new StreamWriter(@"C:\logServico.txt", true))
                {
                    sw.WriteLine("Iniciado " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                }

                iniciaMemoria();

                var p = new Processo();
                p.StartTimer();
            }
            catch (Exception ex)
            {
                using (var sw = new StreamWriter("C:\\logServico.txt", true))
                {
                    sw.WriteLine("Erro ao Executar " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    sw.WriteLine("Excecao: " + ex);
                }
            }
        }

        protected override void OnStop()
        {
            using (var sw = new StreamWriter(@"C:\logServico.txt", true))
            {
                sw.WriteLine("Parado " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
            }
        }

        private void iniciaMemoria()
        {
            Memoria.Empresa = Convert.ToInt32(ConfigurationManager.AppSettings["Empresa"]);
            Memoria.Filial = Convert.ToInt32(ConfigurationManager.AppSettings["Filial"]);
        }
    }
}