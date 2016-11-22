using System;
using System.Data.Objects;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Timers;
using Artebit.Restaurante.Global.AcessoDados.Global;
using Artebit.Restaurante.Global.Modelo;
using Timer = System.Timers.Timer;

namespace Artebit.Restaurante.ArtebitGourmetWS
{
    /// <summary>
    /// Demonstrate the use of a timer.
    /// </summary>
    public class Processo
    {
        private readonly Thread _thread;
        private readonly Thread _thread2;
        private int _interval = 1000;
        private int _interval2 = 1000;
        private string _name = "";
        private bool _ocupado;
        private bool _ocupado2;
        private Timer _timer;
        private Timer _timer2;

        /// <summary>
        /// Constructor
        /// </summary>
        public Processo()
        {
            _thread = new Thread(WorkThreadFunction);
            _thread2 = new Thread(WorkThreadFunction2);
        }


        public int Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }

        public int Interval2
        {
            get { return _interval2; }
            set { _interval2 = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public void WorkThreadFunction()
        {
            try
            {
                _timer = new Timer();
                _timer.Elapsed += timer_Elapsed;
                _timer.Enabled = true;
                _timer.Interval = Interval;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        public void WorkThreadFunction2()
        {
            try
            {
                _timer2 = new Timer();
                _timer2.Elapsed += timer_Elapsed2;
                _timer2.Enabled = true;
                _timer2.Interval = Interval2;
            }
            catch (Exception ex)
            {
                // log errors
                Console.WriteLine(ex.ToString());
            }
        }


        /// <summary>
        /// Start the timer.
        /// </summary>
        public void StartTimer()
        {
            _thread.Start();
            _thread2.Start();
        }

        /// <summary>
        /// Stop the timer.
        /// </summary>
        public void StopTimer()
        {
            _thread.Abort();
            _thread2.Abort();
        }

        /*
         * Respond to the _Timer elapsed event.
         */

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Console.WriteLine("Tempo passado, data: {0}", DateTime.Now);
            if (!_ocupado)
                GerenciaFila(2);
        }

        private void timer_Elapsed2(object sender, ElapsedEventArgs e)
        {
            //Console.WriteLine("Tempo passado, data: {0}", DateTime.Now);
            if (!_ocupado2)
                GerenciaFila2(1);
        }

        private void GerenciaFila(int tipo)
        {
            _ocupado = true;
            try
            {
                //Console.WriteLine("Fila {0}", tipo);
                var prec = new PreContaDAL();

                using (Contexto.Atual = new Global.Modelo.Restaurante())
                {
                    IQueryable<GFILAIMPRESSAO> lista = from p in Contexto.Atual.GFILAIMPRESSAO
                                                       where (p.impresso == false || p.impresso == null)
                                                             && p.tipoImpressao == tipo
                                                       select p;


                    foreach (GFILAIMPRESSAO objeto in lista)
                    {
                        var conta = new ACONTA {nuMesa = objeto.nuMesa};


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

                            if (conta != null)
                            {
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

                                    if (ret == 1)
                                        objeto.impresso = true;
                                }
                            }
                        }
                        else
                        {
                            objeto.impresso = true;
                        }
                    }

                    Contexto.Atual.SaveChanges();
                }

                using (var sw = new StreamWriter("C:\\logServico" + Convert.ToString(tipo) + ".txt", true))
                {
                    int id = Thread.CurrentThread.ManagedThreadId;
                    sw.WriteLine("Threat: " + Convert.ToString(id) + " - Tipo Executado " + Convert.ToString(tipo) +
                                 " - " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                }
            }
            catch (Exception ex)
            {
                using (var sw = new StreamWriter("C:\\logServico-Tipo1.txt", true))
                {
                    sw.WriteLine("Erro ao Executar " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    sw.WriteLine("Excecao: " + ex);
                }
            }
            _ocupado = false;
        }

        private void GerenciaFila2(int tipo)
        {
            _ocupado2 = true;
            try
            {
                var prec = new PreContaDAL();

                using (var contexto = new Global.Modelo.Restaurante())
                {
                    IQueryable<GFILAIMPRESSAO> lista = from p in contexto.GFILAIMPRESSAO
                                                       where (p.impresso == false || p.impresso == null)
                                                             && p.tipoImpressao == tipo
                                                       select p;

                    foreach (GFILAIMPRESSAO objeto in lista)
                    {
                        var conta = new ACONTA {nuMesa = objeto.nuMesa};

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

                            if (conta != null)
                            {
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

                                    if (ret == 1)
                                        objeto.impresso = true;
                                }
                            }

                            objeto.impresso = true;
                        }
                        else
                        {
                            objeto.impresso = true;
                        }
                    }

                    contexto.SaveChanges();
                }

                using (var sw = new StreamWriter("C:\\logServico" + Convert.ToString(tipo) + ".txt", true))
                {
                    int id = Thread.CurrentThread.ManagedThreadId;
                    sw.WriteLine("Threat: " + Convert.ToString(id) + " - Tipo Executado " + Convert.ToString(tipo) +
                                 " - " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                }
            }
            catch (Exception ex)
            {
                using (var sw = new StreamWriter("C:\\logServico-Tipo2.txt", true))
                {
                    sw.WriteLine("Erro ao Executar " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    sw.WriteLine("Excecao: " + ex);
                }
            }
            _ocupado2 = false;
        }
    }
}