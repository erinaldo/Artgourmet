using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Artebit.Restaurante.Caixa.Caixas.Janelas;
using Artebit.Restaurante.Caixa.Classes;
using Artebit.Restaurante.Caixa.Fiscal;
using Artebit.Restaurante.Caixa.ModelView;
using Artebit.Restaurante.Caixa.Relatorio;
using Artebit.Restaurante.Global.AcessoDados.Atendimento;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.Modelo.Enum;
using Artebit.Restaurante.Global.RegrasNegocio.Caixa;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Artebit.Restaurante.Global.Util.WPF;
using Telerik.Windows;
using Telerik.Windows.Controls;

namespace Artebit.Restaurante.Caixa.Caixas
{
    /// <summary>
    /// Interaction logic for Mesas.xaml
    /// </summary>
    public partial class Delivery : IPagina
    {
        private readonly PreContaControl _preconta = new PreContaControl();

        private ACONTA _conta;

        #region Variaveis para validação de Permissão

        private bool _confAdicionarItens;
        private bool _confCancelarConta;
        private bool _confCancelarItens;
        private bool _confDesconto;
        private bool _confFecharConta;
        private bool _confPessoas;
        private bool _descontoConta;
        private bool _novaMesa;

        #endregion

        public Delivery()
        {
            InitializeComponent();
        }

        #region IPagina Members

        public void Carregar()
        {
            Memoria.TipoConta = TipoConta.Delivery;

            carregaPermissoes();

            txtNumeroBusca.Text = "";
            buscarConta("");

            Cabecalho.btInicio.Click += btInicio_Click;
            Cabecalho.btSair.Visibility = Visibility.Hidden;
        }

        #endregion

        private void carregaPermissoes()
        {
            var perfil = new GPERFIL();
            var per = new PerfilControl();

            if (Memoria.Perfil != null) perfil.idPerfil = Memoria.Perfil.Value;

            perfil = per.Buscar(perfil);

            const string janela = "Mesa";

            //_confTransferir = per.Verificar(perfil, janela, 7);
            //_confDesbloquear = per.Verificar(perfil, janela, 8);
            //_confGorjeta = per.Verificar(perfil, janela, 10);
            _confDesconto = per.Verificar(perfil, janela, 11);
            _confPessoas = per.Verificar(perfil, janela, 12);
            _confFecharConta = per.Verificar(perfil, janela, 9);
            _confAdicionarItens = per.Verificar(perfil, janela, 14);
            _confCancelarItens = per.Verificar(perfil, janela, 15);
            //_confBloquear = per.Verificar(perfil, janela, 16);
            _confCancelarConta = per.Verificar(perfil, janela, 17);
        }

        private decimal? calculaTotal(ACONTITEM p)
        {
            return _conta.ACONTITEM.Where(r => r.nuItemPai == p.nuItem).Aggregate<ACONTITEM, decimal?>(p.preco,
                                                                                                       (current, item)
                                                                                                       =>
                                                                                                       current +
                                                                                                       item.preco);
        }

        private void buscarConta(object sender, RoutedEventArgs e)
        {
            string valor = txtNumeroBusca.Text;
            buscarConta(valor);
        }

        protected void buscarConta(string numero)
        {
            DesabilitaBotoes();

            //pega o numero da mesa
            if (!string.IsNullOrEmpty(numero))
            {
                //busca a preconta  da mesa informada
                _conta = _preconta.BuscarPorId(Convert.ToInt32(numero));

                if (_conta != null)
                {

                    //passa os itens da conta para a grid,
                    // se o item for "adicional" o mesmo será exibido como adicional na grid
                    IEnumerable<ItemConta> itensGrid = from p in _conta.ACONTITEM
                                                       join pr in Memoria.Produtos on p.idProduto equals pr.idProduto
                                                       where p.idStatus != 5 && (p.nuItemPai == null)
                                                       orderby p.nuItem
                                                       select new ItemConta
                                                       {
                                                           nuItem = p.nuItem,
                                                           codigo = pr.codigo,
                                                           nome =
                                                               p.adicional
                                                                   ? "Adicional : " + pr.nome
                                                                   : p.opcao
                                                                         ? "Opção : " + pr.nome
                                                                         : pr.nome,
                                                           unidade =
                                                               pr.undVenda ?? pr.undControle,
                                                           quantidade = p.quantidade,
                                                           preco = calculaTotal(p),
                                                           desconto = p.desconto,
                                                           status = p.ASTATCONTITEM.descricao,
                                                           total =
                                                               (calculaTotal(p) * p.quantidade) - p.desconto,
                                                           vendedor = p.GVENDEDOR.nome
                                                       };

                    //pega os itens da conta, agrupando os por quantidade x valor
                    var itens = from p in _conta.ACONTITEM
                                join pr in Memoria.Produtos on p.idProduto equals pr.idProduto
                                where p.idStatus != 2 && p.idStatus != 5
                                //nao exibe itens cancelados ou transferidos
                                group new { p.quantidade, pr.nome, p.preco } by
                                    new { p.idProduto, p.preco, p.desconto, pr.undVenda, pr.codigo }
                                    into g
                                    select new
                                    {
                                        descricao = g.First().nome,
                                        qtdval =
                             String.Format("{0:0}", g.Sum(r => r.quantidade)) + " x " +
                             String.Format("{0:0.00}", g.First().preco)
                                    };

                    //pega o total da conta dos itens que não foram cancelados
                    _conta.Total =
                        _conta.ACONTITEM.Where(r => r.idStatus != 2 && r.idStatus != 5).Sum(
                            r => (r.quantidade * r.preco) - r.desconto).Value;

                    //pega o total da conta sem os descontos dados nos itens
                    _conta.TotalGeral =
                        _conta.ACONTITEM.Where(r => r.idStatus != 2 && r.idStatus != 5).Sum(r => (r.quantidade * r.preco));

                    //se o desconto for maior que o total da conta, o total vai ser 1 centavo
                    if (_conta.desconto >= _conta.Total)
                    {
                        _conta.Total = Convert.ToDecimal(0.01);
                    }
                    else
                    {
                        //se o desconto for menor, subtrai o desconto do total
                        _conta.Total = _conta.Total - Convert.ToDecimal(_conta.desconto);
                    }

                    //total de descontos
                    _conta.TotalDesconto =
                        _conta.ACONTITEM.Where(r => r.idStatus != 2 && r.idStatus != 5).Sum(r => r.desconto).Value;
                    _conta.TotalDesconto = _conta.TotalDesconto + Convert.ToDecimal(_conta.desconto);

                    _conta.TotalConta = _conta.Total + _conta.TotalServico;

                    _conta.TotalItens = _conta.ACONTITEM.Count(r => r.idStatus != 2 && r.idStatus != 5);


                    gridDados.ItemsSource = itensGrid;
                    gridResumo.ItemsSource = itens;

                    //quantidade de itens
                    lbResumo1.Text =
                        _conta.TotalItens.ToString(
                            CultureInfo.InvariantCulture);

                    //hora da criacao da conta
                    lbResumo2.Text = _conta.dataInclusao.ToString("HH:mm");

                    //horas de permanencia
                    lbResumo3.Text = string.Format("{0:hh\\:mm}",
                                                   DateTime.Now.Subtract(
                                                       _conta.dataInclusao));

                    ////mesas associadas
                    //lbMesasAssociada.Text = _conta.nuMesa.ToString();
                    //foreach (AASSOCIACAO mesa in _conta.AASSOCIACAO)
                    //{
                    //    lbMesasAssociada.Text += " - " +
                    //                             mesa.nuMesa.ToString();
                    //}

                    //quantidade de itens
                    lbTotal1.Text =
                        _conta.TotalItens.ToString(
                            CultureInfo.InvariantCulture);

                    //total de descontos
                    lbTotal2.Text = string.Format("{0:c}",
                                                  _conta.TotalDesconto);

                    //total geral da conta
                    lbTotal4.Text = string.Format("{0:c}", _conta.TotalConta);

                }
                else
                {
                    gridDados.ItemsSource = null;
                    gridResumo.ItemsSource = null;

                    //quantidade de itens
                    lbResumo1.Text = "0";

                    //hora da criacao da conta
                    lbResumo2.Text = "00:00";

                    //horas de permanencia
                    lbResumo3.Text = "00:00";

                    ////mesas associadas
                    //lbMesasAssociada.Text = "";

                    //quantidade de itens
                    lbTotal1.Text = "0";

                    //total de descontos
                    lbTotal2.Text = string.Format("{0:c}", 0);

                    //total geral da conta
                    lbTotal4.Text = string.Format("{0:c}", 0);
                }
            }
            else
            {
                _conta = null;

                gridDados.ItemsSource = null;
                gridResumo.ItemsSource = null;

                //quantidade de itens
                lbResumo1.Text = "0";

                //hora da criacao da conta
                lbResumo2.Text = "00:00";

                //horas de permanencia
                lbResumo3.Text = "00:00";

                ////mesas associadas
                //lbMesasAssociada.Text = "";

                //quantidade de itens
                lbTotal1.Text = "0";

                //total de descontos
                lbTotal2.Text = string.Format("{0:c}", 0);


                //total geral da conta
                lbTotal4.Text = string.Format("{0:c}", 0);
            }

            HabilitaBotoes();

            txtNumeroBusca.Focus();
        }

        private void HabilitaBotoes()
        {
            if (_conta != null)
            {
                if (_conta.idStatus == 1)
                {
                    btnAddItens.IsEnabled = _confAdicionarItens;
                    txtAddItens.Foreground = Brushes.Black;

                    btnDesconto.IsEnabled = true;
                    txtDesconto.Foreground = Brushes.Black;

                    btnCliente.IsEnabled = _confPessoas;
                    txtCliente.Foreground = Brushes.Black;

                }

                //btnBloquear.IsEnabled = _confBloquear;
                //txtBloquear.Foreground = Brushes.Black;

                //if (_conta.idStatus == 3)
                //{
                //    btnBloquear.IsEnabled = _confDesbloquear;
                //}

                btnCancelarConta.IsEnabled = true;
                txtCancelaConta.Foreground = Brushes.Black;

                btnCancelItens.IsEnabled = true;
                txtCancelItens.Foreground = Brushes.Black;

                btnFechaConta.IsEnabled = _confFecharConta;
                txtFechaConta.Foreground = Brushes.Black;

                Imprimir.IsEnabled = true;
                //JutarMesa.IsEnabled = true;
                //SepararMesa.IsEnabled = true;
            }
            else
            {
                if (txtNumeroBusca.Text != "")
                {
                    btnAddItens.IsEnabled = _confAdicionarItens;
                    txtAddItens.Foreground = Brushes.Black;
                }
            }
        }

        private void DesabilitaBotoes()
        {
            btnAddItens.IsEnabled = false;
            txtAddItens.Foreground = Brushes.SlateGray;

            btnDesconto.IsEnabled = false;
            txtDesconto.Foreground = Brushes.SlateGray;

            btnCliente.IsEnabled = false;
            txtCliente.Foreground = Brushes.SlateGray;

            //btnBloquear.IsEnabled = false;
            //txtBloquear.Foreground = Brushes.SlateGray;

            btnCancelarConta.IsEnabled = false;
            txtCancelaConta.Foreground = Brushes.SlateGray;

            btnFechaConta.IsEnabled = false;
            txtFechaConta.Foreground = Brushes.SlateGray;

            btnCancelItens.IsEnabled = false;
            txtCancelItens.Foreground = Brushes.SlateGray;

            Imprimir.IsEnabled = false;
            JutarMesa.IsEnabled = false;
        }

        private void btnFechaConta_Click(object sender, RoutedEventArgs e)
        {
            Contexto.Atual.Detach(_conta);

            Contexto.FecharContexto();
            Contexto.AbrirContexto();

            Contexto.Atual.Attach(_conta);

            //abre janela para fechar conta
            Memoria.LogAcao = "Fechar Conta";
            var fecha = new FecharConta
            {
                DataContext = _conta
            };

            fecha.CarregarInfo();

            WindowUtil.MostraModal();
            fecha.ShowDialog();
            WindowUtil.FechaModal();

            if (fecha.Resultado == "1")
            {
                Contexto.Atual = new Global.Modelo.Restaurante();

                buscarConta(btBuscar, new RoutedEventArgs());
                txtNumeroBusca.Focus();
            }
        }

        private void btnAddItens_Click(object sender, RoutedEventArgs e)
        {
            Memoria.Comanda = txtNumeroBusca.Text;

            ControlePagina.NavigateTo(PaginaCore.PgCaixa_Pedido2);
        }

        private void btnCancelItens_Click(object sender, RoutedEventArgs e)
        {
            RadWindow.Confirm(new DialogParameters
            {
                Content = "Tem certeza que deseja cancelar os itens?",
                Closed = (w, en) =>
                {
                    try
                    {
                        if (en.DialogResult == true)
                        {
                            if (gridDados.SelectedItems.Count > 0)
                            {
                                if (!_confCancelarItens)
                                {
                                    WindowUtil.MostraModal();

                                    var up = new UsuarioPermissao("15");
                                    up.ShowDialog();

                                    if (up.DialogResult == true)
                                    {
                                        WindowUtil.FechaModal();

                                        processaCancelamentoItens();
                                    }
                                    else
                                    {
                                        WindowUtil.FechaModal();
                                    }
                                }
                                else
                                {
                                    processaCancelamentoItens();
                                }
                            }
                            else
                            {

                                RadWindow.Alert("Nenhum item selecionado.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        RadWindow.Alert("Não foi possível cancelar a conta - " +
                                        ex.Message);
                    }

                }
            });
        }

        private void processaCancelamentoItens()
        {
            var excluidos = new List<int>();
            excluidos.Clear();

            //cancela itens selecionados na grid
            foreach (ItemConta it in gridDados.SelectedItems)
            {
                ItemConta it1 = it;

                ACONTITEM c = _conta.ACONTITEM.Single(r => r.nuItem == it1.nuItem);

                c.idStatus = 2; //Cancelado
                c.dataCancelamento = DateTime.Now;
                c.vendedorCancelamento = Memoria.Vendedor;

                if (c.adicional)
                {
                    ACONTITEM c2 = _conta.ACONTITEM.Single(r => r.nuItem == c.nuItemPai);

                    c2.preco += Convert.ToDecimal("0,01");
                }

                //Cancela adicionais do item pai
                foreach (ACONTITEM itadd in _conta.ACONTITEM.Where(r => r.nuItemPai == it1.nuItem))
                {
                    itadd.idStatus = 2;
                    itadd.dataCancelamento = DateTime.Now;
                    itadd.vendedorCancelamento = Memoria.Vendedor;
                }
            }

            int contCan = _conta.TotalItens;

            int cont = _conta.ACONTITEM.Count();

            if (contCan == cont)
            {
                btnCancelarConta_Click(btnCancelarConta, new RoutedEventArgs());
            }
            else
            {
                Memoria.LogAcao = "Cancelamento de itens";

                _preconta.Atualizar(_conta);

                //Gravando arquivos de log
                foreach (int t in excluidos)
                {
                    ACONTITEM c = _conta.ACONTITEM.SingleOrDefault(r => r.nuItem == t);

                    if (c != null)
                    {
                        var log = new LogDAL();

                        Memoria.LogConta = c.idConta;
                        Memoria.LogMesa = _conta.nuMesa;
                        Memoria.LogContaDestino = null;
                        Memoria.LogMesaDestino = null;
                        Memoria.LogAcao = "Item Excluído: " + c.idProduto;
                        log.Criar();
                    }
                }

                RadWindow.Alert("Itens cancelados com sucesso.");

                buscarConta(btBuscar, new RoutedEventArgs());
            }
        }

        private void btnDesconto_Click(object sender, RoutedEventArgs e)
        {
            //abre janela para aplica desconto
            if (gridDados.SelectedItems.Count > 0)
            {
                if (!_confDesconto)
                {
                    WindowUtil.MostraModal();

                    var up = new UsuarioPermissao("11");
                    up.ShowDialog();

                    if (up.DialogResult == true)
                    {
                        WindowUtil.FechaModal();

                        processaDesconto();
                    }
                    else
                    {
                        WindowUtil.FechaModal();
                    }
                }
                else
                {
                    processaDesconto();
                }
            }
            else
            {
                RadWindow.Alert("Nenhum item selecionado.");
            }
        }

        private void processaDesconto()
        {
            var lista = (from ItemConta it in gridDados.SelectedItems select _conta.ACONTITEM.Single(r => r.nuItem == it.nuItem)).ToList();

            _descontoConta = gridDados.SelectedItems.Count() == gridDados.Items.Count;

            var desc = new Desconto
            {
                DataContext = _conta,
                DescontoConta = _descontoConta,
                Items = lista
            };


            desc.CarregarInfo();

            WindowUtil.MostraModal();
            desc.ShowDialog();
            WindowUtil.FechaModal();

            Memoria.LogAcao = "Aplicar Desconto";
            _preconta.Atualizar(_conta);

            buscarConta(btBuscar, new RoutedEventArgs());
        }

        private void txtNumeroBusca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                buscarConta(btBuscar, new RoutedEventArgs());
            }
        }

        private void txtNumeroBusca_Loaded(object sender, RoutedEventArgs e)
        {
            var t = (TextBox)sender;

            t.Focus();
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.None)
            {
                switch (e.Key)
                {
                    case Key.F2:
                        if (btnAddItens.IsEnabled)
                            btnAddItens_Click(btnAddItens, new RoutedEventArgs());
                        break;

                    case Key.F4:
                        if (btnDesconto.IsEnabled)
                            btnDesconto_Click(btnDesconto, new RoutedEventArgs());

                        break;

                    case Key.F5:
                        if (btnCliente.IsEnabled)
                            btnCliente_Click(btnCliente, new RoutedEventArgs());

                        break;

                        //case Key.F6:
                        //    if (btnBloquear.IsEnabled)
                        //        btnBloquear_Click(btnBloquear, new RoutedEventArgs());

                        //break;

                    case Key.F7:
                        if (btnCancelItens.IsEnabled)
                            btnCancelItens_Click(btnCancelItens, new RoutedEventArgs());

                        break;

                    case Key.F8:
                        if (btnCancelarConta.IsEnabled)
                            btnCancelarConta_Click(btnCancelarConta, new RoutedEventArgs());
                        break;

                    case Key.System:
                        if (btnFechaConta.IsEnabled)
                            btnFechaConta_Click(btnFechaConta, new RoutedEventArgs());
                        break;

                    case Key.F10:
                        if (btnFechaConta.IsEnabled)
                            btnFechaConta_Click(btnFechaConta, new RoutedEventArgs());
                        break;

                    case Key.F11:
                        MenuOpcoes.IsOpen = !MenuOpcoes.IsOpen;
                        break;

                    case Key.D0:
                        FocarBuscaMesa();
                        break;

                    case Key.D1:
                        FocarBuscaMesa();
                        break;

                    case Key.D2:
                        FocarBuscaMesa();
                        break;

                    case Key.D3:
                        FocarBuscaMesa();
                        break;

                    case Key.D4:
                        FocarBuscaMesa();
                        break;

                    case Key.D5:
                        FocarBuscaMesa();
                        break;

                    case Key.D6:
                        FocarBuscaMesa();
                        break;

                    case Key.D7:
                        FocarBuscaMesa();
                        break;

                    case Key.D8:
                        FocarBuscaMesa();
                        break;

                    case Key.D9:
                        FocarBuscaMesa();
                        break;

                    case Key.NumPad0:
                        FocarBuscaMesa();
                        break;

                    case Key.NumPad1:
                        FocarBuscaMesa();
                        break;

                    case Key.NumPad2:
                        FocarBuscaMesa();
                        break;

                    case Key.NumPad3:
                        FocarBuscaMesa();
                        break;

                    case Key.NumPad4:
                        FocarBuscaMesa();
                        break;

                    case Key.NumPad5:
                        FocarBuscaMesa();
                        break;

                    case Key.NumPad6:
                        FocarBuscaMesa();
                        break;

                    case Key.NumPad7:
                        FocarBuscaMesa();
                        break;

                    case Key.NumPad8:
                        FocarBuscaMesa();
                        break;

                    case Key.NumPad9:
                        FocarBuscaMesa();
                        break;

                    case Key.Return:
                        if (txtNumeroBusca.IsFocused)
                        {
                            _novaMesa = true;
                        }
                        break;

                    case Key.Escape:
                        txtNumeroBusca.Text = "";
                        buscarConta(btBuscar, new RoutedEventArgs());
                        break;
                }
            }
        }

        private void FocarBuscaMesa()
        {
            txtNumeroBusca.Focus();

            if (_novaMesa)
            {
                txtNumeroBusca.Text = "";

                _novaMesa = false;
            }
        }

        private void btnCancelarConta_Click(object sender, RoutedEventArgs e)
        {
            RadWindow.Confirm(new DialogParameters
            {
                Content = "Tem certeza que deseja cancelar a conta?",
                Closed = (w, en) =>
                {
                    try
                    {
                        if (en.DialogResult == true)
                        {
                            if (!_confCancelarConta)
                            {
                                WindowUtil.MostraModal();

                                var up = new UsuarioPermissao("17");
                                up.ShowDialog();

                                if (up.DialogResult == true)
                                {
                                    WindowUtil.FechaModal();

                                    bool result = _preconta.Cancelar(_conta);

                                    if (result)
                                    {
                                        RadWindow.Alert("Conta Cancelada com sucesso.");
                                        buscarConta(btBuscar, new RoutedEventArgs());
                                    }
                                    else
                                    {
                                        RadWindow.Alert("Erro ao cancelar conta.");
                                    }
                                }
                                else
                                {
                                    WindowUtil.FechaModal();
                                }
                            }
                            else
                            {
                                bool result = _preconta.Cancelar(_conta);

                                if (result)
                                {
                                    RadWindow.Alert("Conta Cancelada com sucesso.");
                                    buscarConta(btBuscar, new RoutedEventArgs());
                                }
                                else
                                {
                                    RadWindow.Alert("Erro ao cancelar conta.");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        RadWindow.Alert("Não foi possível cancelar a conta - " + ex.Message);
                    }

                }
            });
        }

        private void ImprimirPreConta()
        {
            try
            {
                var impressao = new PreConta();

                impressao.ImprimeRelatorio(_conta);

                Application.Current.Dispatcher.Invoke(new Action(() =>
                                                                PaginaCore.MainWindow.busyIndicator.IsBusy = false));
            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                                                               RadWindow.Alert(ex.Message)));

                Application.Current.Dispatcher.Invoke(new Action(() =>
                                                               PaginaCore.MainWindow.busyIndicator.IsBusy = false));
            }
        }

        private void MenuOpcoes_ItemClick(object sender, RadRoutedEventArgs e)
        {
            var r = sender as RadMenuItem;

            if (r != null)
                switch (r.CommandParameter.ToString())
                {
                    case "CaixaMesa":
                        ControlePagina.NavigateTo(PaginaCore.PgCaixa_Mesas);
                        break;

                    case "CaixaBalcao":
                        ControlePagina.NavigateTo(PaginaCore.PgCaixa_Balcao);
                        break;

                    case "CaixaDelivery":
                        ControlePagina.NavigateTo(PaginaCore.PgCaixa_Delivery);
                        break;

                    case "AbrirDia":
                        RadWindow.Confirm(new DialogParameters
                        {
                            Content = "Tem certeza que deseja gerar a Leitura X?",
                            Closed = (w, en) =>
                            {
                                try
                                {
                                    if (en.DialogResult == true)
                                    {
                                        PaginaCore.MainWindow.busyIndicator.IsBusy = true;

                                        var busy = PaginaCore.MainWindow.busyIndicator;

                                        var processo = new Thread(() =>
                                                                  Impressoras.Fiscal.ECF.ImpressoraFiscal.ImprimirLeituraX(busy));

                                        processo.Start();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    RadWindow.Alert("Não foi possível gerar a leitura X - " + ex.Message);
                                }

                            }
                        });
                        break;

                    case "Suprimento":
                        new Suprimento().ShowDialog();
                        break;

                    case "Sangria":
                        new Sangria().ShowDialog();
                        break;

                    case "FecharDia":
                        RadWindow.Confirm(new DialogParameters
                        {
                            Content = "Tem certeza que deseja gerar a Redução Z?",
                            Closed = (w, en) =>
                            {
                                try
                                {
                                    if (en.DialogResult == true)
                                    {
                                        PaginaCore.MainWindow.busyIndicator.IsBusy = true;

                                        var busy = PaginaCore.MainWindow.busyIndicator;

                                        var processo = new Thread(() =>
                                                                  Impressoras.Fiscal.ECF.ImpressoraFiscal.FechamentoDia(busy));

                                        processo.Start();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    RadWindow.Alert("Não foi possível gerar a redução Z - " + ex.Message);
                                }

                            }
                        });
                        break;

                    case "CancelUltCupom":
                        Impressoras.Fiscal.ECF.ImpressoraFiscal.CancelaUltimoCupom();
                        break;

                    case "ImprimirPreconta":

                        PaginaCore.MainWindow.busyIndicator.IsBusy = true;

                        var processo2 = new Thread(ImprimirPreConta);

                        processo2.Start();
                        break;

                    case "JuntarMesa":
                        var tt = new JuntarMesa
                        {
                            DataContext = _conta
                        };
                        tt.CarregarInfo();

                        //exibe modal
                        WindowUtil.MostraModal();
                        tt.ShowDialog();
                        WindowUtil.FechaModal();
                        buscarConta(btBuscar, new RoutedEventArgs());
                        break;

                    case "SepararMesa":
                        var sm = new SepararMesa
                        {
                            DataContext = _conta
                        };
                        sm.CarregarInfo();

                        //exibe modal
                        WindowUtil.MostraModal();
                        sm.ShowDialog();
                        WindowUtil.FechaModal();
                        buscarConta(btBuscar, new RoutedEventArgs());
                        break;

                    //case "Relatorio.FechamentoPeriodo":
                    //    btnAddItens.Command.Execute("Relatorio.FechamentoPeriodo");
                    //    break;

                    case "Relatorio.ResumoVendas":

                        PaginaCore.MainWindow.busyIndicator.IsBusy = true;

                        var busy2 = PaginaCore.MainWindow.busyIndicator;

                        var processo3 = new Thread(() => ResumoVendas.ImprimeRelatorio(busy2));

                        processo3.Start();

                        break;

                    case "AbrirPeriodo":
                        {
                            var ctrl = new PeriodoFiscalControl();
                            APERIODOFISCAL obj = ctrl.BuscarAtual();
                            if (obj != null)
                            {
                                RadWindow.Alert("Não é possível abrir um novo período, pois já existe período em aberto. Período Aberto em : " +
                                    obj.dataInicio.ToString("dd/MM/yyyy HH:mm:ss"));
                            }
                            else
                            {
                                obj = new APERIODOFISCAL
                                {
                                    idEmpresa = Memoria.Empresa,
                                    idFilial = Memoria.Filial,
                                    dataInicio = DateTime.Now
                                };

                                bool result = ctrl.Criar(obj);

                                RadWindow.Alert(result ? "Período aberto com sucesso." : "Erro ao abrir período.");
                            }
                        }

                        break;

                    case "FecharPeriodo":
                        {
                            var ctrl = new PeriodoFiscalControl();
                            APERIODOFISCAL obj = ctrl.BuscarAtual();
                            if (obj == null)
                            {
                                RadWindow.Alert("Não há Período em aberto.");
                            }
                            else
                            {
                                var fp = new FecharPeriodo();

                                WindowUtil.MostraModal();
                                fp.CarregarInfo();
                                fp.PeriodoAtual = obj;
                                fp.ShowDialog();
                                WindowUtil.FechaModal();
                            }
                        }
                        break;
                }
        }

        private void chk_Total_Click(object sender, RoutedEventArgs e)
        {
            var ch = (CheckBox)sender;

            if (ch.IsChecked != null && ch.IsChecked.Value)
            {
                gridDados.SelectAll();
            }
            else
            {
                gridDados.UnselectAll();
            }
        }

        public void Carregar(int? idClifor)
        {
            txtNumeroBusca.Text = Convert.ToString(idClifor);
            carregaPermissoes();
            buscarConta(btBuscar, new RoutedEventArgs());
        }

        private void btInicio_Click(object sender, RoutedEventArgs e)
        {
            txtNumeroBusca.Text = "";
            buscarConta(btBuscar, new RoutedEventArgs());
        }

        private void txtNumeroBusca_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int result;

            if (!int.TryParse(e.Text, out result))
            {
                e.Handled = true;
            }
        }

        private void btnCliente_Click(object sender, RoutedEventArgs e)
        {
            //abre janela para alteração da quantidade de pessoas
            var pss = new Cliente { DataContext = _conta };

            pss.CarregarInfo();

            WindowUtil.MostraModal();
            pss.ShowDialog();
            WindowUtil.FechaModal();

            if (pss.Client.idClifor != 0)
            {
                _conta.idCliFor = pss.Client.idClifor;
                lbResumo4.Text = pss.Client.nomeFantasia;
                Contexto.Atual.SaveChanges();
            }
        }
    }
}