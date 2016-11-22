using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Artebit.Restaurante.AtendimentoPDV.Classes;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.Modelo.Enum;
using Artebit.Restaurante.Global.RegrasNegocio.Global;
using Artebit.Restaurante.Global.Util.WPF;
using Telerik.Windows.Controls;

namespace Artebit.Restaurante.AtendimentoPDV.Telas
{
    /// <summary>
    /// Interaction logic for Inicial.xaml
    /// </summary>
    public partial class Inicial : IPagina
    {
        private readonly IQueryable<GMESA> _listaMesa;

        public Inicial()
        {
            InitializeComponent();

            var mesa = new MesaControl();
            _listaMesa = mesa.BuscarLista();
        }

        #region Events Handlers

        private void Escolhe_mesa(object sender, RoutedEventArgs e)
        {
            if (txt_mesa.Text != "")
            {
                CarregarMesa(Convert.ToInt32(txt_mesa.Text));
            }
        }

        private void Escolhe_conta(object sender, RoutedEventArgs e)
        {
            if (txt_mesa.Text != "")
            {
                int nuMesa = Convert.ToInt32(txt_mesa.Text);
                if (_listaMesa.Any(m => m.nuMesa == nuMesa))
                {
                    Memoria.Mesa = txt_mesa.Text;

                    ControlePagina.NavigateTo(PaginaCore.PgConta);
                }
                else
                {
                    RadWindow.Alert("Digite uma mesa válida.");
                }
            }
        }

        private void entrar(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                if (txt_mesa.Text != "")
                {
                    int nuMesa = Convert.ToInt32(txt_mesa.Text);

                    CarregarMesa(nuMesa);
                }
            }
        }

        private void bt_Sair_Click(object sender, RoutedEventArgs e)
        {
            ControlePagina.NavigateTo(PaginaCore.PgLogin);
        }

        private void btMesa_Click(object sender, RoutedEventArgs e)
        {
            var bt = sender as Button;

            if (bt != null) CarregarMesa(Convert.ToInt32(bt.CommandParameter));
        }

        #endregion

        #region IPagina Methods

        public void Carregar()
        {
            txtNomeUsuario.Text = "Usuário: " + Memoria.NomeVendedor;
            txt_mesa.Text = "";
            txt_mesa.Focus();

            var dados = from a in _listaMesa
                        where a.ativo
                        select new
                                   {
                                       a.nuMesa,
                                       status = a.GSTATMESA.descricao,
                                       statusid = a.idStatus,
                                       icone =
                            a.idStatus == (int) StatusMesa.Reservada
                                ? "/AtendimentoPDV;component/Img/mesaS2.png" //reservada
                                : a.idStatus == (int) StatusMesa.Ocupada
                                      ? "/AtendimentoPDV;component/Img/mesaS4.png" //ocupada
                                      : a.idStatus == (int) StatusMesa.Livre
                                            ? "/AtendimentoPDV;component/Img/mesaS1.png" //livre
                                            : "/AtendimentoPDV;component/Img/mesaS3.png" //bloqueada
                                   };

            grid_mesa.ItemsSource = dados;
        }

        #endregion

        #region Private Methods

        private void CarregarMesa(int nuMesa)
        {
            Memoria.Mesa = nuMesa.ToString(CultureInfo.InvariantCulture);

            int mesa = (from a in _listaMesa
                         where a.ativo
                               && a.nuMesa == nuMesa
                         select a.idStatus).FirstOrDefault();

            if (mesa != 0)
            {
                if (mesa == (int) StatusMesa.Bloqueada)
                {
                    RadWindow.Alert("Mesa Bloqueada");
                }
                else if (mesa == (int) StatusMesa.Ocupada)
                {
                    //IR PARA PAGINA DE PEDIDOS
                    ControlePagina.NavigateTo(PaginaCore.PgPedido);
                }
                else
                {
                    if (mesa == (int) StatusMesa.Reservada)
                    {
                        RadWindow.Alert("Mesa reservada.");
                    }

                    var pss = new NuPessoas();

                    WindowUtil.MostraModal();
                    pss.ShowDialog();
                    WindowUtil.FechaModal();

                    if (pss.Resultado > 0)
                    {
                        PaginaCore.PgPedido.QtdPessoas = Convert.ToInt32(pss.Resultado);

                        //IR PARA PAGINA DE PEDIDOS
                        ControlePagina.NavigateTo(PaginaCore.PgPedido);
                    }
                }
            }
            else
            {
                RadWindow.Alert("Digite uma mesa válida.");
            }
        }

        #endregion
    }
}