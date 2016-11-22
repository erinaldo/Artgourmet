using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Artebit.Restaurante.Caixa.Classes;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.Modelo.ModelLight;
using Telerik.Windows.Controls;

namespace Artebit.Restaurante.Caixa
{
    /// <summary>
    /// Interaction logic for Splash.xaml
    /// </summary>
    public partial class Splash
    {
        private readonly Storyboard _hideboard;
        private readonly Storyboard _showboard;

        private readonly HideDelegate _hideDelegate;
        private readonly ShowDelegate _showDelegate;
        private Thread _loadingThread;

        public Splash()
        {
            InitializeComponent();

            txtVersao.Content = "V " + Sistema.Versao;

            _showDelegate = showText;
            _hideDelegate = hideText;
            _showboard = Resources["showStoryBoard"] as Storyboard;
            _hideboard = Resources["HideStoryBoard"] as Storyboard;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _loadingThread = new Thread(load);
            _loadingThread.Start();
        }

        private void load()
        {
            try
            {
                #region Carregar Banco de Dados

                Dispatcher.Invoke(_showDelegate, "Carregando banco de dados...");
                Thread.Sleep(1000);

                Contexto.AbrirContexto();
                Contexto.Atual.DatabaseExists();

                Dispatcher.Invoke(_hideDelegate);
                Thread.Sleep(1000);

                #endregion

                #region Carregar Interface

                Dispatcher.Invoke(_showDelegate, "Carregando interface...");
                Thread.Sleep(1000);

                Memoria.Produtos = (from p in Contexto.Atual.EPRODUTO
                                    select new Produto
                                               {
                                                   codigo = p.codigo,
                                                   idProduto = p.idProduto,
                                                   nome = p.nome,
                                                   nomeResumo = p.nomeResumo,
                                                   ordemPDV = p.ordemPDV,
                                                   undControle = p.undControle,
                                                   corPDV = p.corPDV,
                                                   Preco1 =
                                                       p.ETABPRECO.FirstOrDefault() != null
                                                           ? p.ETABPRECO.FirstOrDefault().preco1
                                                           : 0,
                                                   Preco2 =
                                                       p.ETABPRECO.FirstOrDefault() != null
                                                           ? p.ETABPRECO.FirstOrDefault().preco2
                                                           : 0,
                                                   Preco3 =
                                                       p.ETABPRECO.FirstOrDefault() != null
                                                           ? p.ETABPRECO.FirstOrDefault().preco3
                                                           : 0,
                                                 tipoTributacao =  p.tipoTributacao,
                                                 valorTributo =  p.AALIQUOTA.valor ?? 0
                                               }).ToList();

                Dispatcher.Invoke(DispatcherPriority.Normal,
                                  (Action) PaginaCore.Iniciar);

                Dispatcher.Invoke(_hideDelegate);
                Thread.Sleep(1000);

                #endregion

                #region Acessar Impressora Fiscal

                Dispatcher.Invoke(_showDelegate, "Acessando Impressora Fiscal...");
                Thread.Sleep(1000);

                Dispatcher.Invoke(DispatcherPriority.Normal,
                                  (Action) Impressoras.Fiscal.ECF.ImpressoraFiscal.Criar);

                Impressoras.Fiscal.ECF.ImpressoraFiscal.Iniciar();

                Dispatcher.Invoke(_hideDelegate);
                Thread.Sleep(1000);

                #endregion

                #region Validar Impressora Fiscal

                Dispatcher.Invoke(_showDelegate, "Validando acesso Impressora Fiscal...");
                Thread.Sleep(1000);

                Dispatcher.Invoke(_hideDelegate);

                #endregion

                #region Iniciar Tela Principal

                Dispatcher.Invoke(DispatcherPriority.Normal,
                                  (Action) delegate
                                               {
                                                   PaginaCore.MainWindow = new MainWindow();
                                                   PaginaCore.MainWindow.Show();
                                                   Close();
                                               });

                #endregion
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(DispatcherPriority.Normal,
                                  (Action) (() =>
                                            RadWindow.Alert(ex.Message)));

                Dispatcher.Invoke(DispatcherPriority.Normal,
                                  (Action) (() => Application.Current.Shutdown()));
            }
        }

        private void showText(string txt)
        {
            txtLoading.Text = txt;
            BeginStoryboard(_showboard);
        }

        private void hideText()
        {
            BeginStoryboard(_hideboard);
        }

        #region Nested type: HideDelegate

        private delegate void HideDelegate();

        #endregion

        #region Nested type: ShowDelegate

        private delegate void ShowDelegate(string txt);

        #endregion
    }
}