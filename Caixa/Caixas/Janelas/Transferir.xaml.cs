using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.Modelo.Enum;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Global;

namespace Artebit.Restaurante.Caixa.Caixas.Janelas
{
    /// <summary>
    /// Interaction logic for Transferir.xaml
    /// </summary>
    public partial class Transferir
    {
        public List<ACONTITEM> Itens = null;
        public string Resultado = "";
        public int Tipo = 0;
        private ACONTA _conta;

        public Transferir()
        {
            InitializeComponent();

            txtTitulo.Content = Memoria.TipoConta == TipoConta.Mesa ? "Mesa: " : "Comanda: ";

            txtNuMesa.Focus();
        }

        public void CarregarInfo()
        {
            _conta = DataContext as ACONTA;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            busyIndicator.IsBusy = true;
            string valor = txtNuMesa.Text;

            var backgroundThread = new Thread(() => efetuaTranferencia(valor));
            backgroundThread.Start();
        }

        protected void efetuaTranferencia(string numero)
        {
            if (Tipo == 0)
            {
                #region conta toda

                var preconta = new PreContaControl();

                bool result = preconta.Transferir(_conta, Convert.ToInt32(numero));

                if (result)
                {
                    Resultado = numero;
                    Dispatcher.BeginInvoke(new Action(Close));
                }
                else
                {
                    Dispatcher.BeginInvoke(new Action(() =>
                                                          {
                                                              busyIndicator.IsBusy = false;
                                                              Alert("Erro ao transferir conta. ");
                                                          }));
                }

                #endregion
            }
            else
            {
                #region Itens individual

                if (Memoria.TipoConta == TipoConta.Mesa)
                {
                    #region Tipo conta = M

                    // Transferir itens individualmente
                    if (Tipo == 1)
                    {
                        var cmesa = new MesaControl();
                        GMESA mesa = cmesa.Buscar(Convert.ToInt32(numero));

                        var cconta = new PreContaControl();

                        if (mesa.idStatus != (int) StatusMesa.Bloqueada)
                        {
                            Funcoes funcao;

                            // Se estiver livre, tem mudar o status e criar a conta
                            ACONTA conta2 = cconta.BuscarPorMesa(mesa.nuMesa);

                            if (mesa.idStatus != (int) StatusMesa.Ocupada || conta2 == null)
                            {
                                funcao = Funcoes.Adicionar;
                                conta2 = new ACONTA
                                             {
                                                 idConta = Contexto.GerarId("ACONTA"),
                                                 pessoas = _conta.pessoas,
                                                 nuMesa = mesa.nuMesa
                                             };

                                int cont = 0;

                                foreach (ACONTITEM pedido in Itens.OrderBy(r => r.nuItem))
                                {
                                    cont++;

                                    var novo = new ACONTITEM
                                                   {
                                                       adicional = pedido.adicional,
                                                       desconto = pedido.desconto,
                                                       dataInclusao = pedido.dataInclusao,
                                                       idEmpresa = pedido.idEmpresa,
                                                       idFilial = pedido.idFilial,
                                                       idProduto = pedido.idProduto,
                                                       idStatus = pedido.idStatus,
                                                       idVen = pedido.idVen,
                                                       impresso = pedido.impresso,
                                                       nuItem = cont
                                                   };

                                    novo.nuItemPai = novo.nuItem + (pedido.nuItemPai - pedido.nuItem);
                                    novo.opcao = pedido.opcao;
                                    novo.preco = pedido.preco;
                                    novo.produzido = pedido.produzido;
                                    novo.quantidade = pedido.quantidade;
                                    novo.txtObs = pedido.txtObs;

                                    #region Historio Trasnferencia

                                    var hist = new AHISTTRANS
                                                   {
                                                       idHistorico = Contexto.GerarId("AHISTTRANS"),
                                                       idEmpresa = Memoria.Empresa,
                                                       idFilial = Memoria.Filial,
                                                       codusuario = Memoria.Codusuario,
                                                       codvendedor = Memoria.Vendedor,
                                                       idProduto = pedido.idProduto,
                                                       contaOrigem = _conta.idConta,
                                                       contaDestino = conta2.idConta,
                                                       mesaOrigem = _conta.nuMesa,
                                                       mesaDestino = mesa.nuMesa,
                                                       data = DateTime.Now
                                                   };

                                    Contexto.Atual.AHISTTRANS.AddObject(hist);

                                    #endregion

                                    foreach (EOBSERVACAO eobservacao in pedido.EOBSERVACAO)
                                    {
                                        EOBSERVACAO novoOBS =
                                            Contexto.Atual.EOBSERVACAO.SingleOrDefault(a => a.idObs == eobservacao.idObs);

                                        novo.EOBSERVACAO.Add(novoOBS);
                                    }

                                    conta2.ACONTITEM.Add(novo);
                                }

                                Memoria.LogContaDestino = conta2.idConta;
                                Memoria.LogMesaDestino = conta2.nuMesa;
                            }
                            else // Se não estiver livre, basta acrescentar na conta
                            {
                                funcao = Funcoes.Atualizar;

                                int cont = conta2.ACONTITEM.OrderBy(a => a.nuItem).Last().nuItem;

                                foreach (ACONTITEM pedido in Itens.OrderBy(r => r.nuItem))
                                {
                                    cont++;

                                    var novo = new ACONTITEM
                                                   {
                                                       adicional = pedido.adicional,
                                                       desconto = pedido.desconto,
                                                       dataInclusao = pedido.dataInclusao,
                                                       idEmpresa = pedido.idEmpresa,
                                                       idFilial = pedido.idFilial,
                                                       idProduto = pedido.idProduto,
                                                       idStatus = pedido.idStatus,
                                                       idVen = pedido.idVen,
                                                       impresso = pedido.impresso,
                                                       nuItem = cont
                                                   };
                                    novo.nuItemPai = novo.nuItem + (pedido.nuItemPai - pedido.nuItem);
                                    novo.opcao = pedido.opcao;
                                    novo.preco = pedido.preco;
                                    novo.produzido = pedido.produzido;
                                    novo.quantidade = pedido.quantidade;
                                    novo.txtObs = pedido.txtObs;

                                    #region Historico de Transferencia

                                    var hist = new AHISTTRANS
                                                   {
                                                       idHistorico = Contexto.GerarId("AHISTTRANS"),
                                                       idEmpresa = Memoria.Empresa,
                                                       idFilial = Memoria.Filial,
                                                       codusuario = Memoria.Codusuario,
                                                       codvendedor = Memoria.Vendedor,
                                                       idProduto = pedido.idProduto,
                                                       contaOrigem = _conta.idConta,
                                                       contaDestino = conta2.idConta,
                                                       mesaOrigem = _conta.nuMesa,
                                                       mesaDestino = mesa.nuMesa,
                                                       data = DateTime.Now
                                                   };

                                    Contexto.Atual.AHISTTRANS.AddObject(hist);

                                    #endregion

                                    foreach (EOBSERVACAO eobservacao in pedido.EOBSERVACAO)
                                    {
                                        EOBSERVACAO novoOBS =
                                            Contexto.Atual.EOBSERVACAO.SingleOrDefault(a => a.idObs == eobservacao.idObs);

                                        novo.EOBSERVACAO.Add(novoOBS);
                                    }

                                    conta2.ACONTITEM.Add(novo);
                                }

                                Memoria.LogContaDestino = conta2.idConta;
                                Memoria.LogMesaDestino = conta2.nuMesa;
                            }

                            // Remove da lista de itens da conta anterior, sempre executado
                            foreach (ACONTITEM it in Itens)
                            {
                                it.idStatus = 5; //Transferido
                            }

                            //Carregando informações para o log
                            Memoria.LogConta = _conta.idConta;
                            Memoria.LogMesa = _conta.nuMesa;

                            // Adiciona os novos itens
                            bool conf = funcao == Funcoes.Adicionar
                                            ? cconta.Criar(conta2, "1")
                                            : cconta.Atualizar(conta2, "1");

                            if (conf)
                            {
                                // Caso tenha que mudar o status da mesa de livre para ocupada
                                if (funcao == Funcoes.Adicionar)
                                {
                                    mesa.idStatus = 1;

                                    // Atualiza status da mesa
                                    bool confmesa = cmesa.AtualizarStatus(mesa);

                                    if (confmesa)
                                    {
                                        Resultado = numero;
                                        Dispatcher.BeginInvoke(new Action(Close));
                                    }
                                    else
                                    {
                                        Dispatcher.BeginInvoke(new Action(() =>
                                                                              {
                                                                                  busyIndicator.IsBusy = false;
                                                                                  Alert("Erro ao atualizar mesa.");
                                                                              }));
                                    }
                                }
                                else
                                {
                                    Resultado = numero;
                                    Dispatcher.BeginInvoke(new Action(Close));
                                }
                            }
                            else
                            {
                                Dispatcher.BeginInvoke(new Action(() =>
                                                                      {
                                                                          busyIndicator.IsBusy = false;
                                                                          Alert("Erro ao adicionar pedido(s).");
                                                                      }));
                            }
                        }
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new Action(() =>
                                                              {
                                                                  busyIndicator.IsBusy = false;
                                                                  Alert("Não é possível transferir para mesa bloqueada.");
                                                              }));
                    }

                    #endregion
                }
                else
                {
                    #region Tipo conta = B

                    var conta2 = new ACONTA();
                    var cconta = new PreContaControl();

                    conta2.idConta = Convert.ToInt32(numero);

                    var funcao = Funcoes.Atualizar;

                    conta2 = cconta.Buscar(conta2);

                    int cont = 0;

                    if (conta2.ACONTITEM.Count > 0)
                        cont = conta2.ACONTITEM.OrderBy(a => a.nuItem).Last().nuItem;

                    foreach (ACONTITEM pedido in Itens.OrderBy(r => r.nuItem))
                    {
                        cont++;

                        var novo = new ACONTITEM
                                       {
                                           adicional = pedido.adicional,
                                           desconto = pedido.desconto,
                                           dataInclusao = pedido.dataInclusao,
                                           idEmpresa = pedido.idEmpresa,
                                           idFilial = pedido.idFilial,
                                           idProduto = pedido.idProduto,
                                           idStatus = pedido.idStatus,
                                           idVen = pedido.idVen,
                                           impresso = pedido.impresso,
                                           nuItem = cont
                                       };
                        novo.nuItemPai = novo.nuItem + (pedido.nuItemPai - pedido.nuItem);
                        novo.opcao = pedido.opcao;
                        novo.preco = pedido.preco;
                        novo.produzido = pedido.produzido;
                        novo.quantidade = pedido.quantidade;
                        novo.txtObs = pedido.txtObs;

                        #region Historico de Transferencia

                        var hist = new AHISTTRANS
                                       {
                                           idHistorico = Contexto.GerarId("AHISTTRANS"),
                                           idEmpresa = Memoria.Empresa,
                                           idFilial = Memoria.Filial,
                                           codusuario = Memoria.Codusuario,
                                           codvendedor = Memoria.Vendedor,
                                           idProduto = pedido.idProduto,
                                           contaOrigem = _conta.idConta,
                                           contaDestino = conta2.idConta,
                                           mesaOrigem = null,
                                           mesaDestino = null,
                                           data = DateTime.Now
                                       };

                        Contexto.Atual.AHISTTRANS.AddObject(hist);

                        #endregion

                        foreach (EOBSERVACAO eobservacao in pedido.EOBSERVACAO)
                        {
                            EOBSERVACAO novoOBS =
                                Contexto.Atual.EOBSERVACAO.SingleOrDefault(a => a.idObs == eobservacao.idObs);

                            novo.EOBSERVACAO.Add(novoOBS);
                        }

                        conta2.ACONTITEM.Add(novo);
                    }

                    // Remove da lista de itens da conta anterior, sempre executado
                    foreach (ACONTITEM it in Itens)
                    {
                        it.idStatus = 5; //Transferido
                    }

                    //Carregando informações para o log
                    Memoria.LogConta = _conta.idConta;
                    Memoria.LogMesa = _conta.nuMesa;
                    Memoria.LogContaDestino = conta2.idConta;
                    Memoria.LogMesaDestino = null;


                    bool conf = funcao == Funcoes.Adicionar ? cconta.Criar(conta2, "1") : cconta.Atualizar(conta2, "1");

                    if (conf)
                    {
                        Resultado = numero;
                        Dispatcher.BeginInvoke(new Action(Close));
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new Action(() =>
                                                              {
                                                                  busyIndicator.IsBusy = false;
                                                                  Alert(
                                                                      "Erro ao transferir. Confira o número da comanda.");
                                                              }));
                    }

                    #endregion
                }

                #endregion
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtNuMesa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                btnOK_Click(btnOK, new RoutedEventArgs());
            }
        }
    }
}