using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Artebit.Restaurante.Caixa.Classes;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio.Global;

namespace Artebit.Restaurante.Caixa.PDV
{
    /// <summary>
    /// Interaction logic for Mesas.xaml
    /// </summary>
    public partial class Mesas : IPagina
    {
        public Mesas()
        {
            InitializeComponent();
        }

        #region IPagina Members

        public void Carregar()
        {
            CarregarMesas();
        }

        #endregion

        private void CarregarMesas()
        {
            var control = new MesaControl();

            //Adicionando as mesas e números.
            IQueryable<GMESA> lista = control.BuscarLista();
            var dados = from a in lista
                        select new
                                   {
                                       a.nuMesa,
                                       status = a.GSTATMESA.descricao,
                                       statusid = a.idStatus,
                                       qtdLugares = a.RGRUPOMESA.descricao,
                                       a.observacao,
                                       icone =
                            a.idStatus == 3
                                ? "/Artebit.Restaurante.Caixa;component/Img/mesaS2.png" //reservada
                                : a.idStatus == 1
                                      ? "/Artebit.Restaurante.Caixa;component/Img/mesaS4.png" //ocupada
                                      : a.idStatus == 2
                                            ? "/Artebit.Restaurante.Caixa;component/Img/mesaS1.png" //livre
                                            : "/Artebit.Restaurante.Caixa;component/Img/mesaS3.png" //bloqueada
                                   };
            grid_mesa.ItemsSource = dados;

            //Adicionando informações ao quadro de resumo
            int total = dados.Count();
            int reservada = dados.Count(l => l.statusid == 3);
            int ocupada = dados.Count(l => l.statusid == 1);
            int livre = dados.Count(l => l.statusid == 2);
            int bloqueada = dados.Count(l => l.statusid == 4);

            tlbTotal.Text = Convert.ToString(total);
            tlbReservada.Text = Convert.ToString(reservada);
            tlbOcupadas.Text = Convert.ToString(ocupada);
            tlbLivres.Text = Convert.ToString(livre);
            tlBloqueadas.Text = Convert.ToString(bloqueada);
        }

        private void btMesa_Click(object sender, RoutedEventArgs e)
        {
            var btMesa = sender as Button;
            if (btMesa != null)
            {
                Memoria.Mesa = Convert.ToString(btMesa.CommandParameter);

                PaginaCore.PgCaixa_Mesas.Carregar(Convert.ToInt32(btMesa.CommandParameter));
            }

            ControlePagina.NavigateTo(PaginaCore.PgCaixa_Mesas, false);
        }
    }
}