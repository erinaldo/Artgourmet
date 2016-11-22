using Artebit.Restaurante.AtendimentoPDV.Telas;

namespace Artebit.Restaurante.AtendimentoPDV.Classes
{
    public static class PaginaCore
    {
        private static Login _pgLogin;
        private static Pedido _pgPedido;

        private static PedidoSimples _pgPedidoSimples;

        private static Inicial _pgInicial;

        private static Conta _pgConta;

        public static Pedido PgPedido
        {
            get { return _pgPedido; }
            set { _pgPedido = value; }
        }

        public static PedidoSimples PgPedidoSimples
        {
            get { return _pgPedidoSimples; }
            set { _pgPedidoSimples = value; }
        }

        public static Inicial PgInicial
        {
            get { return _pgInicial; }
            set { _pgInicial = value; }
        }

        public static Conta PgConta
        {
            get { return _pgConta; }
            set { _pgConta = value; }
        }

        public static Login PgLogin
        {
            get { return _pgLogin; }
            set { _pgLogin = value; }
        }

        public static void Iniciar()
        {
            _pgLogin = new Login();
            _pgConta = new Conta();
            _pgInicial = new Inicial();
            _pgPedido = new Pedido();
            _pgPedidoSimples = new PedidoSimples();
        }
    }
}