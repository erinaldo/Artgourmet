using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Artebit.Restaurante.Global.Modelo;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls;
using System.ComponentModel;

namespace Artebit.Restaurante.AtendimentoProducao
{
    /// <summary>
    /// Interaction logic for Bar.xaml
    /// </summary>
    public partial class Cozinha : UserControl
    {
        List<ItemModel> itens;

        public Cozinha()
        {
            InitializeComponent();

            CarregaItens();
            atualizaStatus();
            gridPrincipal.ItemsSource = itens;

            criaTempo();
        }

        private void CarregaItens()
        {
            itens = (from p in Contexto.Atual.ACONTITEM
                    where p.idStatus != 3 && p.idStatus != 2//cancelado
                    && p.ACONTA.idStatus == 1//aberta
                    && p.EPRODUTO.AMONPRD.Any(r => r.idMonitor == Memoria.IdMonitor)
                    orderby p.horaInclusao
                    select new ItemModel()
                    {
                        IdConta = p.idConta,
                        Produto = p.EPRODUTO.nome,
                        NuItem = p.nuItem,
                        Vendedor = p.GVENDEDOR.nome,
                        IdStatus = p.idStatus.Value,
                        Quantidade = p.quantidade,
                        Mesa = p.ACONTA.nuMesa,
                        Item = p,
                        TempoPreparo = p.EPRODUTO.tempoPreparo,
                    }).ToList();
        }

        private void gridPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            if (gridPrincipal.ChildrenOfType<GridViewCell>().Count > 0)
            {
                gridPrincipal.ChildrenOfType<GridViewCell>().First().Focus();
                gridPrincipal.ChildrenOfType<GridViewCell>().First().ParentRow.IsSelected = true;
            }
        }

        private void gridPrincipal_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void gridPrincipal_RowActivated(object sender, RowEventArgs e)
        {
            ItemModel a = e.Row.Item as ItemModel;

            if (a.IdStatus != 3)
            {

                itens = itens.OrderByDescending(r => r.IdStatus).ThenBy(r => r.DataAlteracao).ToList();
                a.IdStatus = 3;
                a.Produzido = true;
                a.DataAlteracao = DateTime.Now;

                SalvaInfo(a);

                if (itens.Count(r => r.IdStatus == 3) > 3)
                {
                    itens.RemoveAt(0);
                }

                atualizaStatus();
                gridPrincipal.ItemsSource = itens.OrderByDescending(r => r.IdStatus).ThenBy(r => r.DataAlteracao).ToList();

                e.Handled = false;
            }
            else
            {
                e.Handled = false;
            }            
        }

        private void SalvaInfo(ItemModel objeto)
        {
            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += delegate(object s, DoWorkEventArgs args)
            {
                if (objeto.Itens != null)
                {
                    foreach (ACONTITEM a in objeto.Itens)
                    {
                        a.idStatus = 3;
                        a.produzido = true;
                    }
                }
                else
                {
                    objeto.Item.idStatus = 3;
                    objeto.Item.produzido = true;
                }
            };

            worker.RunWorkerAsync();
        }

        private void criaTempo()
        {
            // Create a Timer with a Normal Priority
            DispatcherTimer _timer = new DispatcherTimer();

            // Set the Interval to 2 seconds
            _timer.Interval = TimeSpan.FromMilliseconds(15000);

            // Set the callback to just show the time ticking away
            // NOTE: We are using a control so this has to run on 
            // the UI thread
            _timer.Tick += new EventHandler(delegate(object s, EventArgs a)
            {
                Contexto.Atual.SaveChanges();

                atualizaStatus();

                //CarregaItens();
                List<ItemModel> it = (from p in Contexto.Atual.ACONTITEM
                                      where p.idStatus != 3 && p.idStatus != 2//cancelado
                                      && p.ACONTA.idStatus == 1//aberta
                                      && p.EPRODUTO.AMONPRD.Any(r => r.idMonitor == Memoria.IdMonitor)
                                      orderby p.horaInclusao
                                      select new ItemModel()
                                      {
                                          IdConta = p.idConta,
                                          Produto = p.EPRODUTO.nome,
                                          NuItem = p.nuItem,
                                          Vendedor = p.GVENDEDOR.nome,
                                          IdStatus = p.idStatus.Value,
                                          Quantidade = p.quantidade,
                                          Mesa = p.ACONTA.nuMesa,
                                          Item = p,
                                          TempoPreparo = p.EPRODUTO.tempoPreparo,
                                      }).ToList();

                foreach (ItemModel bar in it)
                {
                    if (!itens.Any(r => r.IdConta == bar.IdConta && r.NuItem == bar.NuItem && r.Quantidade == bar.Quantidade))
                    {
                        itens.Add(bar);
                    }
                }

                gridPrincipal.ItemsSource = itens.OrderByDescending(r => r.IdStatus).ThenBy(r => r.DataAlteracao);

                setaFocoGrid();
            });

            // Start the timer
            _timer.Start();
        }

        protected void setaFocoGrid()
        {
            var targetColumn = gridPrincipal.Columns[0];

            if (gridPrincipal.Items.Count > 0)
            {
                int cont = itens.Count(r => r.IdStatus == 3);

                if (cont == gridPrincipal.Items.Count)
                {
                    cont = cont - 1;
                }

                gridPrincipal.SelectedItem = gridPrincipal.Items[cont];

                gridPrincipal.ScrollIntoViewAsync(gridPrincipal.SelectedItem, targetColumn, (f) =>
                {
                    GridViewRow row = f as GridViewRow;
                    if (row != null)
                    {
                        row.Cells.Cast<GridViewCell>().First(c => c.Column == targetColumn).Focus();
                    }
                });
            }
        }

        protected void atualizaStatus()
        {
            foreach(ItemModel i in itens)
            {
                i.AtualizaStatus();
            }
        }
    }
}
