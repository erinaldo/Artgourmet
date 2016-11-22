using System;
using System.ComponentModel;
using System.Data.Objects;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using Artebit.Restaurante.Global.AcessoDados.Global;
using Artebit.Restaurante.Global.Modelo;
using Timer = System.Threading.Timer;

namespace ArtebitGourmet___Host
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly NotifyIcon MyNotifyIcon;

        private bool _ocupado;
        private bool _ocupado2;

        public MainWindow()
        {
            InitializeComponent();

            MyNotifyIcon = new NotifyIcon();
            MyNotifyIcon.Icon = new Icon(
                @"C:\artebitGoumet.ico");
            MyNotifyIcon.MouseDoubleClick +=
                MyNotifyIcon_MouseDoubleClick;

            var bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;

            bw.RunWorkerAsync();
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            IniciaHost();
        }

        public void IniciaHost()
        {
            var autoEvent = new AutoResetEvent(false);

            // Create the delegate that invokes methods for the timer.
            TimerCallback timerDelegate =
                Teste1;

            // Create a timer that signals the delegate to invoke 
            // CheckStatus after one second, and every 1/4 second 
            // thereafter.
            Console.WriteLine("{0} Creating timer.\n",
                              DateTime.Now.ToString("h:mm:ss.fff"));

            var stateTimer =
                new Timer(timerDelegate, autoEvent, 0, 1000);

            var auto2 = new AutoResetEvent(false);

            var stateTimer2 = new Timer(Teste2, auto2, 0, 1000);


            // When autoEvent signals, change the period to every 
            // 1/2 second.
            autoEvent.WaitOne(-1, false);
            auto2.WaitOne(-1, false);
        }

        public void Teste1(object state)
        {
            Console.WriteLine("Executando - {0}", DateTime.Now);
            if (!_ocupado)
                GerenciaFila(2);
        }

        public void Teste2(object state)
        {
            Console.WriteLine("Executando - {0}", DateTime.Now);
            if (!_ocupado2)
                GerenciaFila2(1);
        }

        private void GerenciaFila(int tipo)
        {
            _ocupado = true;
            try
            {
                Console.WriteLine("Fila {0}", tipo);
                var prec = new PreContaDAL();

                using (Contexto.Atual = new Restaurante())
                {
                    IQueryable<GFILAIMPRESSAO> lista = from p in Contexto.Atual.GFILAIMPRESSAO
                                                       where p.impresso == false
                                                             && p.tipoImpressao == tipo
                                                       select p;

                    if (lista.Count() == 0)
                        Console.WriteLine("Nenhum Item na Fila - {0}", tipo);
                    else
                        Console.WriteLine("Imprimindo - {0} - Quantidade:{1} - Tempo : {2}", tipo, lista.Count(),
                                          DateTime.Now);


                    foreach (GFILAIMPRESSAO objeto in lista)
                    {
                        var conta = new ACONTA();
                        conta.nuMesa = objeto.nuMesa;


                        //pega a conta da mesa
                        Memoria.Empresa = 1;
                        Memoria.Filial = 1;
                        Memoria.NomeVendedor = objeto.nomeVendedor;

                        IQueryable<ACONTA> resultado =
                            Contexto.Atual.ACONTA.Where(a => (a.idConta == conta.idConta || conta.idConta == 0)
                                                             &&
                                                             (a.idEmpresa == Memoria.Empresa ||
                                                              Memoria.Empresa == 0)
                                                             &&
                                                             (a.idFilial == Memoria.Filial)
                                                             &&
                                                             (a.idStatus == 1 || a.idStatus == 3)
                                                             &&
                                                             (a.AASSOCIACAO.Any(r => r.nuMesa == conta.nuMesa) ||
                                                              conta.nuMesa == 0 || a.nuMesa == conta.nuMesa)
                                );

                        if (resultado.Any())
                        {
                            Contexto.Atual.Refresh(RefreshMode.StoreWins, resultado.First());
                            conta = resultado.First();
                        }

                        if (conta != null)
                        {
                            Console.WriteLine("Imprimindo Fila {0}", tipo);
                            if (objeto.tipoImpressao == 2)
                            {
                                int ret = prec.EnviaItemsProducao(conta, objeto.idDocumento);

                                if (ret == 1)
                                    objeto.impresso = true;
                            }
                            else
                            {
                                //imprime conta
                                int ret = prec.ImprimirPreConta(conta, objeto.idDocumento);
                                objeto.impresso = true;
                            }
                        }
                    }

                    Contexto.Atual.SaveChanges();
                }

                //using (StreamWriter sw = new StreamWriter("C:\\logServico" + Convert.ToString(tipo) + ".txt", true))
                //{
                //    int id = Thread.CurrentThread.ManagedThreadId;
                //    sw.WriteLine("Threat: " + Convert.ToString(id) + " - Tipo Executado " + Convert.ToString(tipo) + " - " + DateTime.Now.ToString());
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro no tipo{0}", tipo);
                Console.WriteLine(ex.ToString());
                //using (StreamWriter sw = new StreamWriter("C:\\logServico-Tipo1.txt", true))
                //{
                //    sw.WriteLine("Erro ao Executar " + DateTime.Now.ToString());
                //    sw.WriteLine("Excecao: " + ex.ToString());
                //}
            }
            _ocupado = false;
        }

        private void GerenciaFila2(int tipo)
        {
            _ocupado2 = true;
            try
            {
                Console.WriteLine("Fila {0}", tipo);

                var prec = new PreContaDAL();

                using (var contexto = new Restaurante())
                {
                    IQueryable<GFILAIMPRESSAO> lista = from p in contexto.GFILAIMPRESSAO
                                                       where p.impresso == false
                                                             && p.tipoImpressao == tipo
                                                       select p;

                    if (lista.Count() == 0)
                        Console.WriteLine("Nenhum Item na Fila - {0}", tipo);
                    else
                        Console.WriteLine("Imprimindo - {0} - Quantidade:{1} - Tempo : {2}", tipo, lista.Count(),
                                          DateTime.Now);

                    foreach (GFILAIMPRESSAO objeto in lista)
                    {
                        var conta = new ACONTA();
                        conta.nuMesa = objeto.nuMesa;


                        //pega a conta da mesa
                        Memoria.Empresa = 1;
                        Memoria.Filial = 1;
                        Memoria.NomeVendedor = objeto.nomeVendedor;

                        IQueryable<ACONTA> resultado =
                            contexto.ACONTA.Where(a => (a.idConta == conta.idConta || conta.idConta == 0)
                                                       &&
                                                       (a.idEmpresa == Memoria.Empresa ||
                                                        Memoria.Empresa == 0)
                                                       &&
                                                       (a.idFilial == Memoria.Filial)
                                                       &&
                                                       (a.idStatus == 1 || a.idStatus == 3)
                                                       &&
                                                       (a.AASSOCIACAO.Any(r => r.nuMesa == conta.nuMesa) ||
                                                        conta.nuMesa == 0 || a.nuMesa == conta.nuMesa)
                                );

                        if (resultado.Any())
                        {
                            contexto.Refresh(RefreshMode.StoreWins, resultado.First());
                            conta = resultado.First();
                        }

                        if (conta != null)
                        {
                            Console.WriteLine("Imprimindo Fila {0}", tipo);
                            if (objeto.tipoImpressao == 2)
                            {
                                int ret = prec.EnviaItemsProducao(conta, objeto.idDocumento);

                                if (ret == 1)
                                    objeto.impresso = true;
                            }
                            else
                            {
                                //imprime conta
                                int ret = prec.ImprimirPreConta(conta, objeto.idDocumento);
                                objeto.impresso = true;
                            }
                        }
                    }

                    contexto.SaveChanges();
                }

                //using (StreamWriter sw = new StreamWriter("C:\\logServico" + Convert.ToString(tipo) + ".txt", true))
                //{
                //    int id = Thread.CurrentThread.ManagedThreadId;
                //    sw.WriteLine("Threat: " + Convert.ToString(id) + " - Tipo Executado " + Convert.ToString(tipo) + " - " + DateTime.Now.ToString());
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro no tipo{0}", tipo);
                Console.WriteLine(ex.ToString());
                //using (StreamWriter sw = new StreamWriter("C:\\logServico-Tipo2.txt", true))
                //{
                //    sw.WriteLine("Erro ao Executar " + DateTime.Now.ToString());
                //    sw.WriteLine("Excecao: " + ex.ToString());
                //}
            }
            _ocupado2 = false;
        }

        private void MyNotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WindowState = WindowState.Normal;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                ShowInTaskbar = false;
                MyNotifyIcon.BalloonTipTitle = "Minimize Sucessful";
                MyNotifyIcon.BalloonTipText = "Minimized the app ";
                MyNotifyIcon.ShowBalloonTip(400);
                MyNotifyIcon.Visible = true;
            }
            else if (WindowState == WindowState.Normal)
            {
                MyNotifyIcon.Visible = false;
                ShowInTaskbar = true;
            }

            IniciaHost();
        }
    }
}