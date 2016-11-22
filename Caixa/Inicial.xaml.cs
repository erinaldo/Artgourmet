using System;
using System.Collections.Generic;
using System.Windows;
using Artebit.Restaurante.Caixa.Classes;
using Artebit.Restaurante.Caixa.Controles;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.RegrasNegocio.Global;

namespace Artebit.Restaurante.Caixa
{
    /// <summary>
    /// Interaction logic for TelaUserControl.xaml
    /// </summary>
    public partial class Inicial : IPagina
    {
        private bool _inicializado;
        private readonly PerfilControl _per = new PerfilControl();
        private GPERFIL _perfil = new GPERFIL();


        public Inicial()
        {
            InitializeComponent();

            Cabecalho.btSair.Visibility = Visibility.Hidden;
        }

        #region IPagina Members

        public void Carregar()
        {
            if (!_inicializado)
            {
                InicializarObjetos();
            }

            Cabecalho.btVoltar.Visibility = Visibility.Hidden;
            Cabecalho.btSair.Visibility = Visibility.Visible;

            _inicializado = true;
        }

        #endregion

        //Verificar as permissões do perfil
        private void InicializarObjetos()
        {
            var nomesBotoes = new List<ImageButton>
                                  {
                                     btCaixaMesas,
                                     btCaixaBalcao,
                                     btCaixaDelivery,
                                     btCaixaCartao,
                                     btPdvMesas,
                                     btCadAliquotas,
                                     btCadCardapio,
                                     btCadClientes,
                                     btCadFidelidade,
                                     btCadGrupoMesas,
                                     btCadImpressoras,
                                     btCadMesas,
                                     btCadMonitores,
                                     btCadProdutos,
                                     btCadUsuarios,
                                     btCadVendedores,
                                     btCadRecebimentos,
                                     btCadPerfil,
                                     btCadAvisos
                                  };

            //buscando o perfil
            _perfil.idPerfil = Convert.ToInt32(Memoria.Perfil);
            _perfil = _per.Buscar(_perfil);

            foreach (ImageButton t in nomesBotoes)
            {
                VerificarAcessoJanela(t);
            }
        }

        /// <summary>
        /// Verificar se o perfil tem acesso à janela
        /// </summary>
        /// <param name="bt">Botões da janela</param>
        private void VerificarAcessoJanela(ImageButton bt)
        {
            bool conf = _per.VerificarJanela(_perfil, bt.NomeJanela);

            bt.IsEnabled = conf;
        }

        private void Botoes_Click(object sender, RoutedEventArgs e)
        {
            var btAcao = sender as ImageButton;
            if (btAcao != null)
            {
                string parametro = Convert.ToString(btAcao.NomeJanela);

                switch (parametro)
                {
                    case "Mesa":
                        ControlePagina.NavigateTo(PaginaCore.PgCaixa_Mesas);
                        break;

                    case "Delivery":
                        ControlePagina.NavigateTo(PaginaCore.PgCaixa_Delivery);
                        break;

                    case "Balcao":
                        ControlePagina.NavigateTo(PaginaCore.PgCaixa_Balcao);
                        break;

                    case "Cartao":
                        ControlePagina.NavigateTo(PaginaCore.PgCaixa_Cartao);
                        break;

                    case "Cardapio":
                        ControlePagina.NavigateTo(PaginaCore.PgCadastro_Cardapios);
                        break;

                    case "Clientes":
                        ControlePagina.NavigateTo(PaginaCore.PgCadastro_Clientes);
                        break;

                    case "Vendedores":
                        ControlePagina.NavigateTo(PaginaCore.PgCadastro_Vendedores);
                        break;

                    case "Impressoras":
                        ControlePagina.NavigateTo(PaginaCore.PgCadastro_Impressoras);
                        break;

                    case "Monitores":
                        ControlePagina.NavigateTo(PaginaCore.PgCadastro_Monitores);
                        break;

                    case "Usuarios":
                        ControlePagina.NavigateTo(PaginaCore.PgCadastro_Usuarios);
                        break;

                    case "TiposRecebimentos":
                        ControlePagina.NavigateTo(PaginaCore.PgCadastro_TipoRecebimento);
                        break;

                    case "CadMesas":
                        ControlePagina.NavigateTo(PaginaCore.PgCadastro_Mesas);
                        break;

                    case "MesaPDV":
                        ControlePagina.NavigateTo(PaginaCore.PgPDV_Mesas);
                        break;

                    case "GrupoMesas":
                        ControlePagina.NavigateTo(PaginaCore.PgCadastro_GrupoMesas);
                        break;

                    case "Produtos":
                        ControlePagina.NavigateTo(PaginaCore.PgCadastro_Produtos);
                        break;

                    case "Fidelidade":
                        ControlePagina.NavigateTo(PaginaCore.PgCadastro_Fidelidade);
                        break;

                    case "Alicotas":
                        ControlePagina.NavigateTo(PaginaCore.PgCadastro_Aliquota);
                        break;

                    case "Perfil":
                        ControlePagina.NavigateTo(PaginaCore.PgCadastroPerfil);
                        break;

                    case "Avisos":
                        ControlePagina.NavigateTo(PaginaCore.PgCadastro_Avisos);
                        break;
                }
            }
        }
    }
}