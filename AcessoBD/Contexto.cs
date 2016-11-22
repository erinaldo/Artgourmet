using System;
using System.Linq;

namespace Artebit.Restaurante.Global.Modelo
{
    public static class Contexto
    {
        private static Restaurante contx;

        /// <summary>
        /// Retorna o contexto atual
        /// </summary>
        public static Restaurante Atual
        {
            get
            {
                if (contx == null)
                {
                    contx = new Restaurante();
                    return contx;
                }
                else
                {
                    return contx;
                }
            }

            set { contx = value; }
        }

        public static void AbrirContexto()
        {
            Atual = new Restaurante();
        }

        public static void FecharContexto()
        {
            Atual.Dispose();
        }

        /// <summary>
        /// Retorna o ID a ser utilizado e increnta um novo valor para o proximo registro
        /// </summary>
        /// <param name="tabela">GINCREMENTO</param>
        /// <returns>ID a ser utilizado na tabela</returns>
        public static int GerarId(String tabela)
        {
            using (var contexto = new Restaurante())
            {
                int? obj = contexto.SP_RESERVAID(tabela, Memoria.Empresa, Memoria.Filial).FirstOrDefault();

                if (obj != null)
                {
                    return obj.Value;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}