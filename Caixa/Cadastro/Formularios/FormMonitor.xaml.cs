using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio;
using Artebit.Restaurante.Global.RegrasNegocio.Estoque;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Telerik.Windows.Controls;

namespace Artebit.Restaurante.Caixa.Cadastro
{
    /// <summary>
    /// Interaction logic for FormMonitor.xaml
    /// </summary>
    public partial class FormMonitor : RadWindow
    {
        private AMONITOR monitor = new AMONITOR();

        public FormMonitor()
        {
            InitializeComponent();
            CarregarSourcesMesa();
            CarregarSourcesProduto();
        }

        public FormMonitor(AMONITOR obj)
        {
            InitializeComponent();
            DataContext = obj;
            CarregarSourcesMesa();
            CarregarSourcesProduto();
            CarregaInfo();
        }

        public void CarregaInfo()
        {
            // carrega dados nos campos
            monitor = DataContext as AMONITOR;

            if (monitor != null)
            {
                txtboxDescricao.Value = monitor.descricao;
                checkAtivo.IsChecked = monitor.ativo;

                /*
                 * selecioa mesas da grid passando a lista de quais mesas devem estar selecionadas
                 * a lista de mesas selecionadas vem do objeto impressora
                 * */
                IEnumerable<GMESA> selecionados = from p in monitor.AMONMESA
                                                  select p.GMESA;
                gridMesas.Select(selecionados);
                IEnumerable<EPRODUTO> selecionadosp = from p in monitor.AMONPRD
                                                      select p.EPRODUTO;
                gridProduto.Select(selecionadosp);
            }
            else
            {
                monitor = new AMONITOR();
            }
        }

        private void CarregarSourcesMesa()
        {
            var mesacontrol = new MesaControl();
            var obj = new GMESA();
            IQueryable<GMESA> lista = mesacontrol.BuscarLista();
            gridMesas.ItemsSource = lista;
        }

        private void CarregarSourcesProduto()
        {
            var prodcontrol = new ProdutoControl();
            var obj = new EPRODUTO();
            IQueryable<EPRODUTO> lista = prodcontrol.BuscarLista();
            gridProduto.ItemsSource = lista;
        }

        private bool validaDados()
        {
            if (Convert.ToString(txtboxDescricao.Value) == "")
            {
                Alert("Informe a descricao.");
                return false;
            }

            return true;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            SalvarDados();
        }

        private void SalvarDados()
        {
            if (validaDados())
            {
                //Pegar dados do formulário
                monitor.descricao = Convert.ToString(txtboxDescricao.Value);
                monitor.ativo = checkAtivo.IsChecked;
                monitor.idEmpresa = Memoria.Empresa;
                monitor.idFilial = Memoria.Filial;

                //Pegar produtos selecionadas

                monitor.AMONPRD.Clear();
                foreach (EPRODUTO prd in gridProduto.SelectedItems)
                {
                    var amp = new AMONPRD();
                    monitor.AMONPRD.Add(amp);
                    amp.idEmpresa = Convert.ToInt32(Memoria.Empresa);
                    amp.idFilial = Convert.ToInt32(Memoria.Filial);
                    amp.idProduto = prd.idProduto;
                }

                //Pegar mesas selecionados

                monitor.AMONMESA.Clear();
                foreach (GMESA mes in gridMesas.SelectedItems)
                {
                    var amm = new AMONMESA();
                    monitor.AMONMESA.Add(amm);
                    amm.idEmpresa = Convert.ToInt32(Memoria.Empresa);
                    amm.idFilial = Convert.ToInt32(Memoria.Filial);
                    amm.nuMesa = mes.nuMesa;
                }


                //Verifica se é inclusão ou alterção
                Funcoes acao;
                if (DataContext != null)
                    acao = Funcoes.Atualizar;
                else
                    acao = Funcoes.Adicionar;

                //Atualizando banco
                var control = new MonitorControl();

                bool result = false;

                if (acao == Funcoes.Adicionar)
                {
                    result = control.Criar(monitor);
                }
                else
                {
                    result = control.Atualizar(monitor);
                }

                if (!result)
                    Alert("Verifique os dados digitados e tente novamente");
                else
                    Close();
            }
        }
    }
}