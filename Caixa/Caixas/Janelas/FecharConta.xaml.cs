using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Artebit.Restaurante.Caixa.Classes;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio.Caixa;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Artebit.Restaurante.Global.Util;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Artebit.Restaurante.Caixa.Caixas.Janelas
{
    /// <summary>
    /// Interaction logic for FecharConta.xaml
    /// </summary>
    public partial class FecharConta
    {
        private readonly List<ARECEBIMENTO> _recebimentos = new List<ARECEBIMENTO>();
        private readonly List<ARECEBIMENTO> _valoresRecebidos = new List<ARECEBIMENTO>();

        public string Resultado = "";
        private decimal _diferenca;
        private decimal _totalConta;
        private decimal _totalRecebido;
        private decimal _troco;
        private ACONTA _conta = new ACONTA();
        private AOPCAIXA _op;
        private CupomFiscal _winCupom;
        private string _cpf;

        public FecharConta()
        {
            InitializeComponent();

            //usar seta para navegar na grid
            PreviewKeyDown += FecharConta_PreviewKeyDown;

            gridPagamentos.KeyboardCommandProvider = new CustomKeyboardCommandProvider(gridPagamentos);

            gridRecebimentos.KeyboardCommandProvider = new CustomKeyboardCommandProvider(gridRecebimentos);
        }

        public void CarregarInfo()
        {
            _conta = DataContext as ACONTA;

            if (_conta != null) _totalConta = _conta.TotalConta;

            lbValorTotal.Content = string.Format("{0:c}", _totalConta);
            lbValorRecebido.Content = string.Format("{0:c}", 0);
            lbValorDiferença.Content = string.Format("{0:c}", _totalConta);
            lbValorTroco.Content = string.Format("{0:c}", 0);

            foreach (AFORMAPGTO item in Contexto.Atual.AFORMAPGTO.Where(r => r.ativo != false).OrderBy(r => r.ordem).ThenBy(r => r.descricao))
            {
                Contexto.Atual.Detach(item);

                var recb = new ARECEBIMENTO
                               {
                                   idEmpresa = Memoria.Empresa,
                                   idFilial = Memoria.Filial,
                                   AFORMAPGTO = item,
                                   valorRecebido = 0
                               };
                if (_conta != null) recb.idConta = _conta.idConta;
                recb.idRecebimento = 66;

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

        private void criaECF()
        {
            var cupom = new ACUPOMECF
                            {
                                fiscal = true,
                                idEmpresa = Memoria.Empresa,
                                idFilial = Memoria.Filial,
                                CPF_CNPJ = _cpf,
                                dataEmitido = DateTime.Now,
                                idConta = _conta.idConta,
                                idECF = Memoria.IdECF,
                                tipoCupom = "RV",
                                total = _totalConta,
                                valorRecebido = _totalRecebido,
                                troco = _troco,
                                servico = _conta.TotalServico
                            };

            cupom.total = _totalConta - _conta.TotalServico;

            if (_conta.desconto == null)
            {
                _conta.desconto = 0;
            }

            if (_conta.desconto < _conta.Total)
            {
                cupom.desconto = _conta.desconto;
            }
            else
            {
                cupom.desconto = _conta.Total - Convert.ToDecimal(0.01);
            }
            
            cupom.subTotal = cupom.total - cupom.desconto;


            bool hasRepique = _valoresRecebidos.All(r => r.idFormaPGTO != 3);

            if(hasRepique)
            {
                cupom.troco = 0;
                cupom.repique = _troco;
            }

            foreach (ARECEBIMENTO receb in _valoresRecebidos)
            {
                var rec = receb;

                if(receb.idRecebimento == 0)
                    rec.idRecebimento = 99;
                else
                {
                    rec = new ARECEBIMENTO
                              {
                                  idEmpresa = Memoria.Empresa,
                                  idFormaPGTO = receb.idFormaPGTO,
                                  valorRecebido = receb.valorRecebido
                              };
                }

                cupom.ARECEBIMENTO.Add(rec);
            }

            var itens = from p in _conta.ACONTITEM
                        join pr in Memoria.Produtos on p.idProduto equals pr.idProduto
                        where p.idStatus != 5
                        group new
                                  {
                                      pr.codigo,
                                      pr.nome,
                                      p.desconto,
                                      p.preco,
                                      p.quantidade,
                                      pr.undVenda,
                                      pr.undControle,
                                      pr.tipoTributacao,
                                      pr.valorTributo,
                                      p.idStatus
                                  } by
                            new {p.idProduto, p.preco, p.desconto, pr.undVenda, pr.codigo, p.idStatus}
                        into g
                        select new
                                   {
                                       g.First().codigo,
                                       g.First().nome,
                                       desconto = g.Sum(r => r.desconto),
                                       g.First().preco,
                                       quantidade = g.Sum(r => r.quantidade),
                                       unidade = g.First().undVenda ?? g.First().undControle,
                                       cancelado = g.First().idStatus == 2,
                                       tributo = g.First().tipoTributacao,
                                       aliquota = g.First().valorTributo
                                   };

            cupom.icms = 0;

            int cont = 1;
            foreach (var it in itens)
            {
                var item = new AITEMCUPOM
                               {
                                   idEmpresa = Memoria.Empresa,
                                   idFilial = Memoria.Empresa,
                                   idItem = cont,
                                   codigoItem = it.codigo,
                                   descricao = it.nome
                               };

                if (item.desconto >= it.preco)
                {
                    item.desconto = it.preco - Convert.ToDecimal(0.01);
                }
                else
                {
                    item.desconto = it.desconto;
                }

                item.precoUnit = it.preco;
                item.quantidade = it.quantidade;
                item.unidade = it.unidade;
                item.cancelado = it.cancelado;
                item.sitTributaria = it.tributo;
                item.aliquotaPerc = it.aliquota != 0 ? it.aliquota : (decimal?) null;

                cupom.AITEMCUPOM.Add(item);

                if (it.aliquota != 0)
                    cupom.icms += ((it.preco*it.quantidade) - (item.desconto ?? 0))*(item.aliquotaPerc/100);


                cont++;
            }

            // Não existe mais serviço na nota fiscal
            //if(conta.servico == true && totalServico > 0)
            //{
            //    item = new AITEMCUPOM();

            //    EPRODUTO prd = Contexto.Atual.EPRODUTO.FirstOrDefault(r => r.ativo == true && r.idProduto == 740);

            //    item.idEmpresa = Memoria.Empresa.Value;
            //    item.idFilial = Memoria.Empresa.Value;
            //    item.idItem = cont;

            //    item.codigoItem = prd.codigo;
            //    item.descricao = prd.nome;
            //    item.desconto = 0;
            //    item.precoUnit = totalServico;
            //    item.quantidade = 1;
            //    item.unidade = "un";
            //    item.canceladoECF = false;
            //    item.sitTributaria = prd.tipoTributacao;
            //    item.aliquotaPerc = prd.AALIQUOTA != null ? prd.AALIQUOTA.alq : null;

            //    cupom.AITEMCUPOM.Add(item);
            //}

            _op.ACUPOMECF.Add(cupom);

            var operacao = new OperacaoCaixaControl();
            bool result = operacao.Criar(_op);


            if (result)
            {
                try
                {
                    //Emissão do Cumpom Fiscal
                    result = Impressoras.Fiscal.ECF.ImpressoraFiscal.RegistraPreVenda(_op, _winCupom);
                }
                catch (Exception ex)
                {
                    result = false;
                    Dispatcher.BeginInvoke(new Action(() =>
                                                          {
                                                              Alert(ex.Message);
                                                              _winCupom.Close();
                                                          }));
                }
                

                if (result)
                {
                    //operacao de caixa e cupom registrado com sucesso, no metodo abaixo deve se fechar a conta, liberando a mesa
                    var precon = new PreContaControl();
                    result = precon.FecharConta(_conta);

                    if (result)
                    {
                        Dispatcher.BeginInvoke(new Action(() =>
                                                              {
                                                                  Alert("Conta fechada com sucesso.");
                                                                  Resultado = "1";
                                                                  _winCupom.Close();
                                                                  Close();
                                                              }));
                    }
                    else
                    {
                        Dispatcher.BeginInvoke(new Action(() =>
                                                              {
                                                                  Alert(Memoria.MsgGlobal);
                                                                  _winCupom.Close();
                                                              }));
                    }
                }
                else
                {
                    Dispatcher.Invoke(new Action(() =>
                                                          {
                                                              Alert("Erro ao imprimir Cupom Fiscal \n "+Memoria.MsgGlobal);
                                                              _winCupom.Close();
                                                          }));

                    var cupomm = _op.ACUPOMECF.FirstOrDefault();
                    if (cupomm != null)
                    {
                        cupomm.cancelado = true;
                        cupomm.dataCancelado = DateTime.Now;
                    }

                    _op.dataFim = DateTime.Now;
                    _op.codErroECF = Memoria.CodErroECF;

                    Contexto.Atual.SaveChanges();

                    try
                    {
                        Impressoras.Fiscal.ECF.ImpressoraFiscal.CancelaUltimoCupom(false);
                    }
                    catch (Exception ex)
                    {
                        Dispatcher.Invoke(new Action(() =>
                        {
                            Alert(ex.Message);
                            _winCupom.Close();
                        }));
                    }
                    
                }
            }
            else
            {
                Dispatcher.Invoke(new Action(() =>
                                                      {
                                                          Alert(Memoria.MsgGlobal);
                                                          _winCupom.Close();
                                                      }));
            }

            Dispatcher.Invoke(new Action(() =>
                                             {
                                                 busyIndicator.IsBusy = false;

                                             }));
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string texto = Convert.ToString(txtCpf.Value);

            bool valida = true;

            if (texto != "")
            {
                if (chkCPF.IsChecked == true)
                {
                    valida = CPFCNPJ.VerificaCPF(texto);

                    if (!valida)
                        Alert("CPF inválido, redigite-o e tente novamente.");
                }

                if (chkCNPJ.IsChecked == true)
                {
                    valida = CPFCNPJ.VerificaCNPJ(texto);

                    if (!valida)
                        Alert("CNPJ inválido, redigite-o e tente novamente.");
                }
            }

            if (valida)
            {
                if (VerificaValorRecebido())
                {
                    _op = new AOPCAIXA
                              {
                                  dataInicio = DateTime.Now,
                                  idCaixa = Memoria.IdCaixa,
                                  idECF = Memoria.IdECF,
                                  idEmpresa = Memoria.Empresa,
                                  idFilial = Memoria.Filial
                              };

                    busyIndicator.IsBusy = true;

                    _winCupom = new CupomFiscal();

                    _winCupom.Show();

                    _cpf = Convert.ToString(txtCpf.Value); 

                    var backgroundThread = new Thread(criaECF);
                    backgroundThread.Start();
                    
                }
            }
        }

        private bool VerificaValorRecebido()
        {
            if (_totalRecebido < decimal.Round(_totalConta, 2))
            {
                Alert("Valor recebido é inferior ao total da conta.");
                return false;
            }
            else
            {
                return true;
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
                var novo = new ARECEBIMENTO
                               {
                                   idEmpresa = Memoria.Empresa,
                                   idFilial = Memoria.Filial,
                                   idFormaPGTO = item.AFORMAPGTO.idFormaPGTO,
                                   DescricaoFormaPgto = item.AFORMAPGTO.descricao,
                                   valorRecebido = item.valorRecebido,
                                   idRecebimento = 88
                               };

                //AFORMAPGTO formapagamento =
                //    Contexto.Atual.AFORMAPGTO.SingleOrDefault(f => f.idEmpresa == Memoria.Empresa
                //                                                   && f.idFormaPGTO == item.idFormaPGTO);

                //if (formapagamento != null)
                //{
                //    novo.idFormaPGTO = formapagamento.idFormaPGTO;
                //    //novo.AFORMAPGTO = formapagamento;
                //}
                //novo.idConta = item.idConta;

                _valoresRecebidos.Add(novo);

                gridRecebimentos.ItemsSource = _valoresRecebidos.AsEnumerable();
                gridRecebimentos.Rebind();

                foreach (ARECEBIMENTO it in _recebimentos)
                {
                    it.valorRecebido = 0;
                }

                gridPagamentos.ItemsSource = _recebimentos.AsEnumerable();

                CalcularTotais();
            }
        }

        private void gridRecebimentos_CellEditEnded(object sender, GridViewCellEditEndedEventArgs e)
        {
            CalcularTotais();
        }

        private void CalcularTotais()
        {
            _totalRecebido = _valoresRecebidos.Sum(r => r.valorRecebido).Value;
            _diferenca = _totalConta - _totalRecebido;
            _troco = _totalRecebido - _totalConta;

            lbValorRecebido.Content = string.Format("{0:c}", _totalRecebido);
            lbValorDiferença.Content = string.Format("{0:c}", _diferenca < 0 ? 0 : _diferenca);
            lbValorTroco.Content = string.Format("{0:c}", _troco < 0 ? 0 : _troco);
        }

        private void btPlanos_Click(object sender, RoutedEventArgs e)
        {
            var p = new PlanoFidelidade(_conta);

            p.ShowDialog();
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

                //Contexto.Atual.Detach(item);
            }

            gridRecebimentos.ItemsSource = _valoresRecebidos.AsEnumerable();
            gridRecebimentos.Rebind();


            CalcularTotais();
        }

        private void TrocaCPFCNPJ(object sender, RoutedEventArgs e)
        {
            if (chkCPF.IsChecked == true)
            {
                label4.Content = "CPF:";
                txtCpf.Mask = "###.###.###-##";
            }

            if (chkCNPJ.IsChecked == true)
            {
                label4.Content = "CNPJ:";
                txtCpf.Mask = "##.###.###/####-##";
            }
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