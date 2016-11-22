using System;

namespace Artebit.Restaurante.Global.Modelo
{
    public static class Sistema
    {
        private static readonly Version _versao = new Version(1, 1, 0);

        public static Version Versao
        {
            get { return _versao; }
        }
    }
}