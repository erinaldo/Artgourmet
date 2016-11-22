using Artebit.Restaurante.Caixa.Cadastro;
using Artebit.Restaurante.Caixa.Caixas;
using Mesas = Artebit.Restaurante.Caixa.Caixas.Mesas;

namespace Artebit.Restaurante.Caixa.Classes
{
    public static class PaginaCore
    {
        private static Login _pgLogin;
        private static Inicial _pgInicial;

        private static Mesas _pgCaixaMesas;
        private static Balcao _pgCaixaBalcao;
        private static Delivery _pgCaixaDelivery;
        private static Cartao _pgCaixaCartao;
        private static Pedido2 _pgCaixaPedido2;

        //public static Caixas.Pedido PgCaixa_Pedido
        //{
        //    get { return PaginaCore._pgCaixa_Pedido; }
        //    set { PaginaCore._pgCaixa_Pedido = value; }
        //}

        private static PDV.Mesas _pgPdvMesas;

        private static Aliquota _pgCadastroAliquota;
        private static Cardapios _pgCadastroCardapios;
        private static Clientes _pgCadastroClientes;
        private static Fidelidade _pgCadastroFidelidade;
        private static GrupoMesa _pgCadastroGrupoMesas;
        private static Cadastro.Impressoras _pgCadastroImpressoras;
        private static Cadastro.Mesas _pgCadastroMesas;
        private static Monitores _pgCadastroMonitores;
        private static Produtos _pgCadastroProdutos;
        private static TipoRecebimento _pgCadastroTipoRecebimento;
        private static Usuarios _pgCadastroUsuarios;
        private static Vendedores _pgCadastroVendedores;
        private static Perfil _pgCadastroPerfil;
        private static Posts _pgCadastroAvisos;

        public static Pedido2 PgCaixa_Pedido2
        {
            get { return _pgCaixaPedido2; }
            set { _pgCaixaPedido2 = value; }
        }


        public static PDV.Mesas PgPDV_Mesas
        {
            get { return _pgPdvMesas; }
            set { _pgPdvMesas = value; }
        }

        public static Aliquota PgCadastro_Aliquota
        {
            get { return _pgCadastroAliquota; }
            set { _pgCadastroAliquota = value; }
        }

        public static Cardapios PgCadastro_Cardapios
        {
            get { return _pgCadastroCardapios; }
            set { _pgCadastroCardapios = value; }
        }

        public static Clientes PgCadastro_Clientes
        {
            get { return _pgCadastroClientes; }
            set { _pgCadastroClientes = value; }
        }

        public static Fidelidade PgCadastro_Fidelidade
        {
            get { return _pgCadastroFidelidade; }
            set { _pgCadastroFidelidade = value; }
        }

        public static GrupoMesa PgCadastro_GrupoMesas
        {
            get { return _pgCadastroGrupoMesas; }
            set { _pgCadastroGrupoMesas = value; }
        }

        public static Cadastro.Impressoras PgCadastro_Impressoras
        {
            get { return _pgCadastroImpressoras; }
            set { _pgCadastroImpressoras = value; }
        }

        public static Cadastro.Mesas PgCadastro_Mesas
        {
            get { return _pgCadastroMesas; }
            set { _pgCadastroMesas = value; }
        }

        public static Monitores PgCadastro_Monitores
        {
            get { return _pgCadastroMonitores; }
            set { _pgCadastroMonitores = value; }
        }

        public static Produtos PgCadastro_Produtos
        {
            get { return _pgCadastroProdutos; }
            set { _pgCadastroProdutos = value; }
        }

        public static TipoRecebimento PgCadastro_TipoRecebimento
        {
            get { return _pgCadastroTipoRecebimento; }
            set { _pgCadastroTipoRecebimento = value; }
        }

        public static Usuarios PgCadastro_Usuarios
        {
            get { return _pgCadastroUsuarios; }
            set { _pgCadastroUsuarios = value; }
        }

        public static Vendedores PgCadastro_Vendedores
        {
            get { return _pgCadastroVendedores; }
            set { _pgCadastroVendedores = value; }
        }


        public static Balcao PgCaixa_Balcao
        {
            get { return _pgCaixaBalcao; }
            set { _pgCaixaBalcao = value; }
        }

        public static Delivery PgCaixa_Delivery
        {
            get { return _pgCaixaDelivery; }
            set { _pgCaixaDelivery = value; }
        }

        public static Mesas PgCaixa_Mesas
        {
            get { return _pgCaixaMesas; }
            set { _pgCaixaMesas = value; }
        }

        public static Cartao PgCaixa_Cartao
        {
            get { return _pgCaixaCartao; }
            set { _pgCaixaCartao = value; }
        }

        public static Inicial PgInicial
        {
            get { return _pgInicial; }
            set { _pgInicial = value; }
        }

        public static Login PgLogin
        {
            get { return _pgLogin; }
            set { _pgLogin = value; }
        }

        public static MainWindow MainWindow { get; set; }

        public static Perfil PgCadastroPerfil
        {
            get { return _pgCadastroPerfil; }
            set { _pgCadastroPerfil = value; }
        }

        public static Posts PgCadastro_Avisos
        {
            get { return _pgCadastroAvisos; }
            set { _pgCadastroAvisos = value; }
        }

        public static void Iniciar()
        {
            _pgLogin = new Login();
            _pgInicial = new Inicial();

            _pgCaixaMesas = new Mesas();
            _pgCaixaDelivery = new Delivery();
            _pgCaixaBalcao = new Balcao();
            _pgCaixaCartao = new Cartao();
            //_pgCaixa_Pedido = new Caixas.Pedido();
            _pgCaixaPedido2 = new Pedido2();

            _pgPdvMesas = new PDV.Mesas();

            _pgCadastroAliquota = new Aliquota();
            _pgCadastroCardapios = new Cardapios();
            _pgCadastroClientes = new Clientes();
            _pgCadastroFidelidade = new Fidelidade();
            _pgCadastroGrupoMesas = new GrupoMesa();
            _pgCadastroImpressoras = new Cadastro.Impressoras();
            _pgCadastroMesas = new Cadastro.Mesas();
            _pgCadastroMonitores = new Monitores();
            _pgCadastroProdutos = new Produtos();
            _pgCadastroTipoRecebimento = new TipoRecebimento();
            _pgCadastroUsuarios = new Usuarios();
            _pgCadastroVendedores = new Vendedores();
            _pgCadastroPerfil = new Perfil();
            _pgCadastroAvisos = new Posts();
        }
    }
}