using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Artebit.Restaurante.Caixa.Classes;
using Artebit.Restaurante.Caixa.Relatorio;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio.Caixa;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Artebit.Restaurante.Caixa.Caixas.Janelas
{
    /// <summary>
    /// Interaction logic for FecharConta.xaml
    /// </summary>
    public partial class FecharPeriodo
    {
        //AOPCAIXA op = null;
        private readonly List<ARECEBIMENTO> _recebimentos = new List<ARECEBIMENTO>();
        private readonly List<ARECEBIMENTO> _valoresRecebidos = new List<ARECEBIMENTO>();
        private readonly ACONTA _conta = new ACONTA();
        public APERIODOFISCAL PeriodoAtual;
        public string Resultado = "";
        public List<AFECHAMENTO> Afechamentos = new List<AFECHAMENTO>();

        public FecharPeriodo()
        {
            InitializeComponent();

            //usar seta para navegar na grid
            PreviewKeyDown += FecharConta_PreviewKeyDown;

            gridPagamentos.KeyboardCommandProvider = new CustomKeyboardCommandProvider(gridPagamentos);

            gridRecebimentos.KeyboardCommandProvider = new CustomKeyboardCommandProvider(gridRecebimentos);
        }

        public void CarregarInfo()
        {
            foreach (AFORMAPGTO item in Contexto.Atual.AFORMAPGTO.Where(r=> r.ativo == true).OrderBy(r=> r.ordem).ThenBy(r=> r.descricao))
            {
                Contexto.Atual.Detach(item);

                var recb = new ARECEBIMENTO
                               {
                                   idEmpresa = Memoria.Empresa,
                                   idFilial = Memoria.Filial,
                                   AFORMAPGTO = item,
                                   valorRecebido = 0,
                                   idConta = _conta.idConta,
                                   idRecebimento = 66
                               };

                _recebimentos.Add(recb);
            }

            gridPagamentos.ItemsSource = _recebimentos.AsEnumerable();

            gridPagamentos.ScrollIntoViewAsync(_recebimentos.FirstOrDefault(), gridPagamentos.Columns[1]
               , f =>
               {
                   var row = f as GridViewRow;
                   if (row != null)
                   {
                       ((GridViewCell)row.Cells[1]).BeginEdit();
                   }
               });
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            busyIndicator.IsBusy = true;

            var backgroundThread = new Thread(fecharPeriodoAtual);
            backgroundThread.Start();
        }

        private  void fecharPeriodoAtual()
        {
            foreach (ARECEBIMENTO receb in _valoresRecebidos)
            {
                receb.idRecebimento = 99;
            }

            List<ARECEBIMENTO> item = _valoresRecebidos;

            var periodo = new PeriodoFiscalControl();

           List<ACUPOMECF> listAcupom =
               Contexto.Atual.ACUPOMECF.Where(p => (p.dataEmitido >= PeriodoAtual.dataInicio && p.dataEmitido <= DateTime.Now) && p.cancelado == null && p.fiscal == true).ToList();

            var recebimentos = new List<ARECEBIMENTO>();

            foreach (ACUPOMECF ac in listAcupom)
            {
                recebimentos.AddRange(ac.ARECEBIMENTO.Where(w => w.idConta != null).ToList());
            }

            var itens = from h in recebimentos
                        where h.idFormaPGTO != null
                        group h by new { h.idFormaPGTO }
                            into g
                            select new
                            {
                                id = g.First().AFORMAPGTO.idFormaPGTO,
                                total = g.Sum(r => r.AFORMAPGTO.tipo == "Di" ? (r.valorRecebido - r.ACUPOMECF.troco) : r.valorRecebido)
                            };

            // itens = totais recebidos
            // item - valores digitados

            foreach (var forma in Contexto.Atual.AFORMAPGTO)
            {
                if (itens.Any(r => r.id == forma.idFormaPGTO) || item.Any(r => r.idFormaPGTO == forma.idFormaPGTO))
                {
                    var fechamento = new AFECHAMENTO
                                         {
                                             idEmpresa = Memoria.Empresa,
                                             idFilial = Memoria.Filial,
                                             idFechamento = Contexto.GerarId("AFECHAMENTO")
                                         };

                    //AFORMAPGTO formapagamento =
                    //    Contexto.Atual.AFORMAPGTO.SingleOrDefault(f => f.idEmpresa == Memoria.Empresa
                    //                                                   && f.idFormaPGTO == receb.AFORMAPGTO.idFormaPGTO);

                    //Contexto.Atual.Detach(formapagamento);

                    decimal? total = 0;

                    foreach (var k in itens)
                    {
                        if (k.id == forma.idFormaPGTO)
                        {
                            total = k.total;
                        }
                    }
                    ARECEBIMENTO receb = item.FirstOrDefault(r => r.idFormaPGTO == forma.idFormaPGTO);

                    if (PeriodoAtual != null) fechamento.idPeriodoFiscal = PeriodoAtual.idPeriodo;
                    fechamento.idTipoRecebimento = forma.idFormaPGTO;

                    fechamento.valorSistema = total ?? 0;
                    fechamento.valorFisico = receb != null ? receb.valorRecebido ?? 0 : 0;

                    //_afechamentos.Add(fechamento);

                    Contexto.Atual.AddToAFECHAMENTO(fechamento);
                }
            }

            Contexto.Atual.SaveChanges();

            var f = new FechaPeriodo();

            if (PeriodoAtual != null) 
                f.CarregaRelatorio(PeriodoAtual.dataInicio, DateTime.Now);

            int resultado = 0;

            try
            {
                resultado = f.Imprimir();
            }
            catch (Exception ex)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                                                      {
                                                          busyIndicator.IsBusy = false;
                                                          Alert(ex.Message);
                                                      }));
            }
            
            if (resultado == 1)
            {
                bool result = periodo.FecharAtual();

                Dispatcher.BeginInvoke(new Action(() =>
                                                      {
                                                          busyIndicator.IsBusy = false;
                                                          Alert(result
                                                                    ? "Período fechado com sucesso."
                                                                    : "Erro ao fechar período.");
                                                          if (result)
                                                              Close();
                                                      }));
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void gridPagamentos_CellEditEnded(object sender, GridViewCellEditEndedEventArgs e)
        {
            ARECEBIMENTO item = _recebimentos.FirstOrDefault(r => r.valorRecebido > 0);

            if (item != null)
            {
                var novo = new ARECEBIMENTO();

                if (_valoresRecebidos.Any(r => r.idFormaPGTO == item.AFORMAPGTO.idFormaPGTO) == false)
                {
                    novo.idEmpresa = Memoria.Empresa;
                    novo.idFilial = Memoria.Filial;

                    //AFORMAPGTO formapagamento =
                    //    Contexto.Atual.AFORMAPGTO.SingleOrDefault(f => f.idEmpresa == Memoria.Empresa
                    //                                                   && f.idFormaPGTO == item.AFORMAPGTO.idFormaPGTO);

                    //Contexto.Atual.Detach(formapagamento);

                    novo.idFormaPGTO = item.AFORMAPGTO.idFormaPGTO;// formapagamento.idFormaPGTO;
                    novo.DescricaoFormaPgto = item.AFORMAPGTO.descricao;
                    novo.valorRecebido = item.valorRecebido;
                    //novo.idConta = item.idConta;

                    novo.idRecebimento = 88;
                    _valoresRecebidos.Add(novo);
                }
                else
                {
                    novo = _valoresRecebidos.SingleOrDefault(r => r.idFormaPGTO == item.AFORMAPGTO.idFormaPGTO);
                    if (novo != null) novo.valorRecebido += item.valorRecebido;
                }

                gridRecebimentos.ItemsSource = _valoresRecebidos.AsEnumerable();
                gridRecebimentos.Rebind();

                foreach (ARECEBIMENTO it in _recebimentos)
                {
                    it.valorRecebido = 0;
                }

                gridPagamentos.ItemsSource = _recebimentos.AsEnumerable();

            }
        }

        private void gridRecebimentos_CellEditEnded(object sender, GridViewCellEditEndedEventArgs e)
        {
            
        }

        private void gridPagamentos_CellValidating(object sender, GridViewCellValidatingEventArgs e)
        {
            var obj = (e.EditingElement as TextBox);
            if (obj != null) obj.Text = obj.Text.Replace('.', ',');
        }

        private void gridPagamentos_SelectedCellsChanged(object sender, GridViewSelectedCellsChangedEventArgs e)
        {
        }

        private void btExcluir_Click(object sender, RoutedEventArgs e)
        {
            gridRecebimentos.CommitEdit();

            var bt = sender as RadButton;

            if (bt != null)
            {
                var item = bt.DataContext as ARECEBIMENTO;

                //Contexto.Atual.ObjectStateManager.ChangeObjectState(item, EntityState.Unchanged);
                _valoresRecebidos.Remove(item);

                _conta.ARECEBIMENTO.Remove(item);
            }

            //Contexto.Atual.Detach(item);

            gridRecebimentos.ItemsSource = _valoresRecebidos.AsEnumerable();
            gridRecebimentos.Rebind();

        }

        #region Navegação por teclado na grid

        private void FecharConta_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!gridPagamentos.Items.IsEditingItem)
                return;


            HandleKeyDown(e);
            HandleKeyUp(e);
        }

        private void HandleKeyUp(KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                RadGridViewCommands.MoveUp.Execute(null);
                RadGridViewCommands.SelectCurrentUnit.Execute(null);
                RadGridViewCommands.BeginEdit.Execute(null);
                FocusEditTextBox();

                e.Handled = true;
            }
        }

        private void HandleKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                RadGridViewCommands.MoveDown.Execute(null);
                RadGridViewCommands.SelectCurrentUnit.Execute(null);
                RadGridViewCommands.BeginEdit.Execute(null);
                FocusEditTextBox();

                e.Handled = true;
            }
        }

        private void FocusEditTextBox()
        {
            Dispatcher.BeginInvoke((Action) (() =>
                                                 {
                                                     var editBox =
                                                         gridPagamentos.CurrentCell.GetEditingElement() as TextBox;

                                                     if (editBox != null && editBox.IsLoaded)
                                                     {
                                                         editBox.Focus();
                                                     }
                                                     else
                                                     {
                                                         gridPagamentos.CurrentCell.GotFocus += CurrentCell_GotFocus;
                                                     }
                                                 }));
        }

        private void CurrentCell_GotFocus(object sender, RoutedEventArgs e)
        {
            if (gridPagamentos.CurrentCell.IsInEditMode)
            {
                var editBox = gridPagamentos.CurrentCell.GetEditingElement() as TextBox;
                if (editBox != null)
                {
                    editBox.SelectAll();
                    editBox.Focus();
                }
            }
        }

        #endregion

        private void gridRecebimentos_CellValidating(object sender, GridViewCellValidatingEventArgs e)
        {
            var obj = (e.EditingElement as TextBox);
            if (obj != null) obj.Text = obj.Text.Replace('.', ',');
        }
    }
}