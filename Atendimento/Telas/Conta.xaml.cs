using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Objects;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Artebit.Restaurante.AtendimentoPDV.Classes;
using Artebit.Restaurante.Global.AcessoDados.Atendimento;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.Modelo.Enum;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Artebit.Restaurante.Global.Util.WPF;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Label = System.Windows.Controls.Label;

namespace Artebit.Restaurante.AtendimentoPDV.Telas
{
    /// <summary>
    /// Interaction logic for Conta.xaml
    /// </summary>
    public partial class Conta : IPagina
    {
        private readonly MesaControl _cmesa = new MesaControl();

        private readonly List<int> _excluidos = new List<int>();
        private readonly PreContaControl _preconta = new PreContaControl();

        private bool _confCancelar;
        private bool _confDesbloquear;
        private bool _confDesconto;
        private bool _confFechar;
        private bool _confGorjeta;
        private bool _confJuntar;
        private bool _confPessoas;
        private bool _confTransferir;
        private ACONTA _conta = new ACONTA();
        private GMESA _mesa = new GMESA();

        public Conta()
        {
            InitializeComponent();
        }

        public bool descontoConta { get; private set; }

        private void CarregarConta()
        {
            _conta.nuMesa = Convert.ToInt32(Memoria.Mesa);

            //busca a preconta  da mesa informada
            _conta = _preconta.Buscar(_conta);

            // busca mesa
            _mesa.nuMesa = Convert.ToInt32(Memoria.Mesa);
            _mesa = _cmesa.Buscar(_mesa);

            if (_conta != null)
            {
                _conta.ACONTITEM.Load(MergeOption.OverwriteChanges);

                //passa os itens da conta para a grid,
                // se o item for "adicional" o mesmo será exibido como adicional na grid
                var lista =
                    from p in
                        _conta.ACONTITEM.Where(ll => ll.idStatus != 2 && ll.idStatus != 5 && (ll.nuItemPai == null))
                    join h in Memoria.Produtos on p.idProduto equals h.idProduto
                    orderby p.nuItem
                    select new
                               {
                                   p.nuItem,
                                   h.codigo,
                                   nome =
                        p.adicional
                            ? "Adicional : " + h.nome
                            : p.opcao ? "Opção : " + h.nome : h.nome,
                                   und = h.undVenda ?? h.undControle,
                                   qtd = p.quantidade,
                                   horario = p.dataInclusao,
                                   preco = calculaTotal(p),
                                   desconto = p.desconto ?? Convert.ToDecimal(0),
                                   status = p.ASTATCONTITEM.descricao,
                                   idstatus = p.idStatus,
                                   total = (calculaTotal(p)*p.quantidade) - p.desconto,
                                   vendedor = p.GVENDEDOR != null ? p.GVENDEDOR.nome : "Sem Vendedor",
                                   tab = p.adicional ? 0 : p.opcao ? 0 : 1,
                                   qtdFormat = string.Format("{0:0}", p.quantidade),
                                   horarioFormat = string.Format("{0:HH\\:mm}", p.dataInclusao),
                                   precoFormat = string.Format("{0:c}", calculaTotal(p)),
                                   descontoFormat = string.Format("{0:c}", p.desconto),
                                   totalFormat = string.Format("{0:c}", (calculaTotal(p)*p.quantidade) - p.desconto),
                               };


                gridDados.ItemsSource = lista;

                btServico.Content = _conta.servico ? "Sem gorjeta" : "Com gorjeta";

                // Arruma os botões
                if (_conta.idStatus != (int) StatusConta.Bloqueada && _mesa.idStatus != (int) StatusMesa.Bloqueada)
                {
                    btDesbloquear.IsEnabled = false;
                    btCancelar.IsEnabled = _confCancelar;
                    btFecharConta.IsEnabled = _confFechar;
                    btTransferir.IsEnabled = _confTransferir;
                    btServico.IsEnabled = _confGorjeta;
                    btPessoas.IsEnabled = _confPessoas;
                    btImprimir.IsEnabled = false;
                    btDesconto.IsEnabled = _confDesconto;
                    btJuntar.IsEnabled = _confJuntar;
                }
                else
                {
                    btDesbloquear.IsEnabled = _confDesbloquear;
                    btFecharConta.IsEnabled = false;
                    btCancelar.IsEnabled = false;
                    btTransferir.IsEnabled = false;
                    btServico.IsEnabled = false;
                    btPessoas.IsEnabled = false;
                    btDesconto.IsEnabled = false;
                    btJuntar.IsEnabled = false;
                    btImprimir.IsEnabled = false;
                }

                //pegar item do footer
                IList<GridViewFooterRow> a = gridDados.ChildrenOfType<GridViewFooterRow>().ToList();

                foreach (GridViewFooterRow r in a)
                {
                    var p = r.Template.FindName("nupedido", r) as Label;
                    var co = r.Template.FindName("cont", r) as Label;
                    var t = r.Template.FindName("totpreco", r) as Label;
                    var td = r.Template.FindName("totdesconto", r) as Label;
                    var st = r.Template.FindName("subtotal", r) as Label;
                    var bb = r.Template.FindName("MarcarTodos", r) as Button;

                    // Botão de Marcar/Desmarcar Todos
                    bb.Content = "Marcar";

                    // Número de pedidos
                    if (p != null)
                    {
                        p.Content = lista.Count(d => d.tab == 1 && d.idstatus != 2);
                        lbResumo1.Text = p.Content.ToString();
                    }

                    // Total de produtos
                    if (co != null)
                    {
                        co.Content = lista.Count(pr => pr.idstatus != 2);
                        lbResumo2.Text = co.Content.ToString();
                    }

                    // Total de preço sem desconto
                    if (t != null)
                    {
                        decimal? tcontent = lista.Where(pr => pr.idstatus != 2).Sum(b => b.preco);
                        t.Content = string.Format("{0:c}", tcontent);
                    }

                    // Total de desconto
                    if (td != null)
                    {
                        decimal? tdcontent = lista.Where(pr => pr.idstatus != 2).Sum(b => b.desconto);
                        td.Content = string.Format("{0:c}", tdcontent);
                        tdcontent += _conta.desconto;
                        lbResumo3.Text = string.Format("{0:c}", tdcontent);
                    }

                    // Total de preço com desconto
                    if (st != null)
                    {
                        decimal? stcontent = lista.Where(pr => pr.idstatus != 2).Sum(b => b.total);
                        st.Content = string.Format("{0:c}", stcontent);
                        lbResumo4.Text = string.Format("{0:c}", stcontent);
                    }
                }

                //pega o total da conta dos itens que não foram cancelados
                decimal total =
                    _conta.ACONTITEM.Where(r => r.idStatus != 2 && r.idStatus != 5).Sum(
                        r => (r.quantidade*r.preco) - r.desconto).Value;
                decimal totalGeral =
                    _conta.ACONTITEM.Where(r => r.idStatus != 2 && r.idStatus != 5).Sum(r => (r.quantidade*r.preco));

                //taxa de serviço
                decimal servico = Convert.ToDecimal("0,1");

                //total do servico
                decimal totalServico = (servico*totalGeral);

                //se a conta não estiver com serviço, o mesmo será 0
                if (!_conta.servico)
                {
                    totalServico = 0;
                }

                // Total de pessoas
                lbResumo8.Text = _conta.pessoas.ToString(CultureInfo.InvariantCulture);

                // Valor total por pessoa
                lbResumo9.Text = string.Format("{0:c}", (totalServico + total)/(_conta.pessoas == 0 ? 1 : _conta.pessoas));

                // Total do serviço
                lbResumo5.Text = string.Format("{0:c}", totalServico);

                // Total geral da conta
                lbResumo6.Text = _conta.desconto == null ? string.Format("{0:c}", (totalServico + total)) : string.Format("{0:c}", (totalServico + total - _conta.desconto));

                // Tempo do ultimo pedido
                if (lista.Any(pp => pp.idstatus != 2))
                {
                    DateTime? ultimo =
                        lista.Where(pr => pr.idstatus != 2).LastOrDefault(pr => pr.idstatus != 2).horario;
                    if (ultimo != null)
                        if (ultimo.HasValue)
                            lbResumo7.Text = string.Format("{0:hh\\:mm}", DateTime.Now.Subtract(ultimo.Value));
                }
            }
            else
            {
                btDesbloquear.IsEnabled = false;
                btFecharConta.IsEnabled = false;
                btCancelar.IsEnabled = false;
                btTransferir.IsEnabled = false;
                btServico.IsEnabled = false;
                btPessoas.IsEnabled = false;
                btDesconto.IsEnabled = false;
                btImprimir.IsEnabled = false;
                btJuntar.IsEnabled = false;

                gridDados.ItemsSource = null;
                gridDados.Rebind();

                //pegar item do footer
                IList<GridViewFooterRow> a = gridDados.ChildrenOfType<GridViewFooterRow>().ToList();
                foreach (GridViewFooterRow r in a)
                {
                    var p = r.Template.FindName("nupedido", r) as Label;
                    var co = r.Template.FindName("cont", r) as Label;
                    var t = r.Template.FindName("totpreco", r) as Label;
                    var td = r.Template.FindName("totdesconto", r) as Label;
                    var st = r.Template.FindName("subtotal", r) as Label;
                    var bb = r.Template.FindName("MarcarTodos", r) as Button;

                    // Botão de Marcar/Desmarcar Todos
                    bb.Content = "Marcar";

                    // Número de pedidos
                    if (p != null)
                    {
                        p.Content = "";
                        lbResumo1.Text = "";
                    }

                    // Total de produtos
                    if (co != null)
                    {
                        co.Content = "";
                        lbResumo2.Text = "";
                    }

                    // Total de preço sem desconto
                    if (t != null)
                    {
                        t.Content = "";
                    }

                    // Total de desconto
                    if (td != null)
                    {
                        td.Content = "";
                        lbResumo3.Text = "";
                    }

                    // Total de preço com desconto
                    if (st != null)
                    {
                        st.Content = "";
                        lbResumo4.Text = "";
                    }
                }

                // Total de pessoas
                lbResumo8.Text = "";

                // Valor total por pessoa
                lbResumo9.Text = "";

                // Total do serviço
                lbResumo5.Text = "";

                // Total geral da conta
                lbResumo6.Text = "";

                // Tempo do ultimo pedido
                lbResumo7.Text = "";
            }
        }

        private decimal? calculaTotal(ACONTITEM p)
        {
            return _conta.ACONTITEM.Where(r => r.nuItemPai == p.nuItem).Aggregate<ACONTITEM, decimal?>(p.preco, (current, item) => current + item.preco);
        }

        private string ArrumaTitulo()
        {
            string titulo = "Atendimento \\ Mesa " + (_conta != null ? _conta.nuMesa.ToString() : Memoria.Mesa);

            if (_conta != null)
            {
                titulo = _conta.AASSOCIACAO.Aggregate(titulo, (current, mesaAss) => current + (" - " + mesaAss.nuMesa.ToString()));
            }

            return titulo;
        }

        #region Handlers de layout

        private static double GetFontSize(double height, double width, string text, double fontSize)
        {
            double sampleFontSize = fontSize;

            Size textSize = GetSampleSize(text, sampleFontSize);

            double sampleHeight = textSize.Height;

            double sampleWidth = textSize.Width;

            double htRatio = height/sampleHeight*0.9;

            double wdRatio = width/sampleWidth*0.9;

            double ratio = (htRatio < wdRatio) ? htRatio : wdRatio;

            //ratio = wdRatio;

            double final = (sampleFontSize*ratio);

            if (final > 20)
            {
                final = 20;
            }

            return final;
        }

        private static Size GetSampleSize(string p, double fontSize)
        {
            double sampleFontSize = fontSize;

            String txt = p;

            var myTypeface = new Typeface("Segoe UI");

            var ft = new FormattedText(txt,
                                       CultureInfo.CurrentCulture,
                                       FlowDirection.LeftToRight,
                                       myTypeface, sampleFontSize, Brushes.Black);

            return new Size(ft.Width, ft.Height);
        }

        // Função chamada quando o botão é criado, arruma o tamanho da letra
        private void Button_Loaded(object sender, RoutedEventArgs e)
        {
            var bb = (Button) sender;
            bb.FontSize = GetFontSize(bb.Height, bb.Width - 10, bb.Content.ToString(), 20);
        }

        #endregion

        #region Events Handlers

        // Volta para a página inicial
        private void btVoltar_Click(object sender, RoutedEventArgs e)
        {
            Memoria.Mesa = "";
            ControlePagina.NavigateTo(PaginaCore.PgInicial);
        }

        // Função para marquar e desmarcar todos
        private void chk_Total_Click(object sender, RoutedEventArgs e)
        {
            var bb = (Button) sender;
            if (Convert.ToString(bb.Content) == "Marcar")
            {
                gridDados.SelectAll();
                bb.Content = "Desmarcar";
            }
            else
            {
                gridDados.UnselectAll();
                bb.Content = "Marcar";
            }
            Button_Loaded(bb, new RoutedEventArgs());
        }

        private void gridDados_Loaded(object sender, RoutedEventArgs e)
        {
            CarregarConta();

            Cabecalho.Titulo = ArrumaTitulo();
        }

        #endregion

        #region Botões de ação

        private void btPessoas_Click(object sender, RoutedEventArgs e)
        {
            //abre janela para alteração da quantidade de pessoas
            var pss = new Pessoas {DataContext = _conta};

            pss.CarregarInfo();

            WindowUtil.MostraModal();
            pss.ShowDialog();
            WindowUtil.FechaModal();

            CarregarConta();
        }

        private void btImprimir_Click(object sender, RoutedEventArgs e)
        {
            int? suc = _preconta.Imprimir(_conta);

            if (suc != 1)
            {
                RadWindow.Alert("Erro ao imprimir.");
            }
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            _excluidos.Clear();

            if (gridDados.SelectedItems.Count > 0)
            {
                //PEGA A QUANTIDADE DE TODOS OS ITEMS QUE PODEM SER CANCELADOS
                int cont = gridDados.Items.Count;

                //SE TODOS OS ITENS TIVERM SIDOS SELECIONADOS, A CONTA SERA CANCELADA
                if (gridDados.SelectedItems.Count == cont)
                {
                    #region CANCELAMENTO DA CONTA

                    if (MessageBox.Show(
                        "Cancelando todos os items da conta, a própria conta é cancelada. Deseja continuar?",
                        "Confirmação", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        _preconta.Cancelar(_conta);

                        Memoria.LogAcao = "Cancelamento da conta";

                        Memoria.Mesa = "";
                        ControlePagina.NavigateTo(PaginaCore.PgInicial);
                    }

                    #endregion
                }
                else
                {
                    foreach (object it in gridDados.SelectedItems)
                    {
                        //pega o numero do item do objeto anonimo
                        var valor = TypeDescriptor.GetProperties(it)[0].GetValue(it);
                        if (valor != null)
                        {
                            var t = (int) valor;

                            _excluidos.Add(t);
                        }
                    }

                    foreach (int t in _excluidos)
                    {
                        ACONTITEM c = _conta.ACONTITEM.SingleOrDefault(r => r.nuItem == t);

                        if (c != null)
                        {
                            if (c.nuItemPai != null && c.nuItemPai != 0)
                            {
                                var item = _conta.ACONTITEM.SingleOrDefault(p => p.nuItem == c.nuItemPai);
                                if (item != null)
                                {
                                    Decimal preco = item.preco;
                                    item.preco = preco + c.preco;
                                }
                            }

                            c.idStatus = 2;
                            c.dataCancelamento = DateTime.Now;
                            c.vendedorCancelamento = Memoria.Vendedor;

                            //Cancela adicionais do item pai
                            foreach (ACONTITEM itt in _conta.ACONTITEM.Where(r => r.nuItemPai == t))
                            {
                                itt.idStatus = 2;
                                itt.dataCancelamento = DateTime.Now;
                                itt.vendedorCancelamento = Memoria.Vendedor;
                            }
                        }
                    }

                    Memoria.LogAcao = "Cancelamento de itens";

                    bool conf = _preconta.Atualizar(_conta);

                    if (conf)
                    {
                        #region Gravando arquivos de log

                        foreach (int t in _excluidos)
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

                        #endregion

                        Contexto.Atual.SaveChanges();

                        _excluidos.Clear();
                        CarregarConta();
                    }
                    else
                    {
                        RadWindow.Alert("Erro ao cancelar itens.");
                    }
                }
            }
            else
            {
                RadWindow.Alert("Marque pelo menos um item.");
            }
        }

        private void btServico_Click(object sender, RoutedEventArgs e)
        {
            if (_conta.servico)
            {
                _conta.servico = false;
                btServico.Content = "Com gorjeta";
            }
            else
            {
                _conta.servico = true;
                btServico.Content = "Sem gorjeta";
            }

            Memoria.LogAcao = btServico.Content.ToString();
            bool conf = _preconta.Atualizar(_conta, "0");

            if (conf)
            {
                CarregarConta();
            }
            else
            {
                RadWindow.Alert("Erro ao atualizar conta.");
            }
        }

        private void btFecharConta_Click(object sender, RoutedEventArgs e)
        {
            _conta.idStatus = 3;
            _conta.GMESA.idStatus = 4;

            Memoria.LogAcao = "Bloquear Conta";

            bool conf = _preconta.Atualizar(_conta);

            foreach (AASSOCIACAO am in _conta.AASSOCIACAO)
            {
                var m = new GMESA();
                if (am.nuMesa != null) m.nuMesa = am.nuMesa.Value;
                m = _cmesa.Buscar(m);
                m.idStatus = 4;

                conf = _cmesa.Atualizar(m);
            }

            btImprimir_Click(btImprimir, new RoutedEventArgs());

            if (conf)
            {
                CarregarConta();
            }
            else
            {
                RadWindow.Alert("Erro ao atualizar conta.");
            }
        }

        private void btDesbloquear_Click(object sender, RoutedEventArgs e)
        {
            _conta.idStatus = 1;
            _conta.GMESA.idStatus = 1;

            Memoria.LogAcao = "Desbloqueio de conta";

            bool conf = _preconta.Atualizar(_conta);

            foreach (AASSOCIACAO am in _conta.AASSOCIACAO)
            {
                var m = new GMESA();
                if (am.nuMesa != null) m.nuMesa = am.nuMesa.Value;
                m = _cmesa.Buscar(m);
                m.idStatus = 1;

                conf = _cmesa.Atualizar(m);
            }

            if (conf)
            {
                CarregarConta();
            }
            else
            {
                RadWindow.Alert("Erro ao atualizar conta.");
            }
        }

        private void btnTranferir_Click(object sender, RoutedEventArgs e)
        {
            //abre janela para transferencia de mesa
            var tt = new Transferir();

            if ((gridDados.SelectedItems.Count() == gridDados.Items.Count) || gridDados.SelectedItems.Count == 0)
            {
                tt.Tipo = 0;

                List<ACONTITEM> lista = _conta.ACONTITEM.ToList();

                tt.Itens = lista;
            }
            else
            {
                tt.Tipo = 1;

                var lista = new List<ACONTITEM>();

                int cont = 0;

                foreach (object it in gridDados.SelectedItems)
                {
                    //pega o numero do item do objeto anonimo
                    var valor = TypeDescriptor.GetProperties(it)[0].GetValue(it);
                    if (valor != null)
                    {
                        var t = (int) valor;

                        ACONTITEM c = _conta.ACONTITEM.Single(r => r.nuItem == t);

                        if (lista.All(b => b.nuItem != c.nuItem))
                        {
                            lista.Add(c);
                            cont++;
                        }

                        //Pegando o itens filho
                        List<ACONTITEM> listaf = _conta.ACONTITEM.Where(r => r.nuItemPai == c.nuItem).ToList();

                        lista.AddRange(listaf);
                    }

                    //foreach (var acontitem in gridDados.Items)
                    //{
                    //    int t2 = (int)TypeDescriptor.GetProperties(acontitem)[0].GetValue(acontitem);

                    //    ACONTITEM c2 = conta.ACONTITEM.SingleOrDefault(r => r.nuItem == t2);

                    //    if(c2.nuItemPai == c.nuItem)
                    //    {
                    //        if(!lista.Any(b => b.nuItem == c2.nuItem))
                    //        {
                    //            lista.Add(c2);
                    //            cont++;
                    //        }
                    //    }

                    //}
                }

                int contItem = _conta.ACONTITEM.Count(acontitem => acontitem.idStatus == 1 || acontitem.idStatus == 3 || acontitem.idStatus == 4);

                if (cont == contItem)
                {
                    tt.Tipo = 0;
                }

                tt.Itens = lista;
            }

            tt.DataContext = _conta;
            tt.CarregarInfo();


            //exibe modal
            WindowUtil.MostraModal();
            tt.ShowDialog();
            WindowUtil.FechaModal();

            if (tt.Resultado != "")
            {
                Memoria.Mesa = "";
                ControlePagina.NavigateTo(PaginaCore.PgInicial);
            }
        }

        private void btDesconto_Click(object sender, RoutedEventArgs e)
        {
            //abre janela para aplica desconto

            if (gridDados.SelectedItems.Count > 0)
            {
                var lista = new List<ACONTITEM>();

                foreach (object it in gridDados.SelectedItems)
                {
                    //pega o numero do item do objeto anonimo
                    var valor = TypeDescriptor.GetProperties(it)[0].GetValue(it);
                    if (valor != null)
                    {
                        var t = (int) valor;

                        ACONTITEM c = _conta.ACONTITEM.Single(r => r.nuItem == t);

                        lista.Add(c);
                    }
                }

                descontoConta = gridDados.SelectedItems.Count() == gridDados.Items.Count;

                var desc = new Desconto {DataContext = _conta, DescontoConta = descontoConta, Items = lista};

                desc.CarregarInfo();

                WindowUtil.MostraModal();
                desc.ShowDialog();
                WindowUtil.FechaModal();


                Memoria.LogAcao = "Aplicar Desconto";
                _preconta.Atualizar(_conta, "0");
                CarregarConta();
            }
            else
            {
                RadWindow.Alert("Nenhum item selecionado.");
            }
        }

        private void btJuntar_Click(object sender, RoutedEventArgs e)
        {
            //abre janela para transferencia de mesa
            var tt = new JuntarMesa {DataContext = _conta};

            tt.CarregarInfo();

            //exibe modal
            WindowUtil.MostraModal();
            tt.ShowDialog();
            WindowUtil.FechaModal();

            if (tt.Resultado != "")
            {
                Memoria.Mesa = "";
                ControlePagina.NavigateTo(PaginaCore.PgInicial);
            }
        }

        #endregion

        #region IPagina Methods

        public void Carregar()
        {
            var per = new PerfilControl();
            var perfil = new GPERFIL {idPerfil = Convert.ToInt32(Memoria.Perfil)};

            perfil = per.Buscar(perfil);

            string janela = "Conta";

            _confCancelar = per.Verificar(perfil, janela, 6);
            _confTransferir = per.Verificar(perfil, janela, 7);
            _confDesbloquear = per.Verificar(perfil, janela, 8);
            _confFechar = per.Verificar(perfil, janela, 9);
            _confGorjeta = per.Verificar(perfil, janela, 10);
            _confDesconto = per.Verificar(perfil, janela, 11);
            _confPessoas = per.Verificar(perfil, janela, 12);
            _confJuntar = per.Verificar(perfil, janela, 13);

            _conta = new ACONTA();
            _mesa = new GMESA();
        }

        #endregion
    }
}