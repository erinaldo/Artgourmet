using System;
using System.Collections.Generic;
using Artebit.Restaurante.Global.AcessoDados.Compras;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Compras
{
    public class ItemMovimentoControl
    {
        private readonly ItemMovimentoDAL dal = new ItemMovimentoDAL();

        [Obsolete]
        public object Executafuncao(CITEMMOV obj, Funcoes funcao, List<string> comp)
        {
            try
            {
                switch (funcao)
                {
                    case Funcoes.Buscar:
                        return Buscar(obj);

                    case Funcoes.ValidaQtdReceber:
                        return ValidarQtdReceber(obj);

                    default:
                        return null;
                }
            }

            catch (Exception ex)
            {
                Excecao.TratarExcecao(ex);
                return null;
            }
        }

        /// <summary>
        /// Função que valida a quantidade a receber do item do movimento
        /// </summary>
        /// <param name="obj">objeto CITEMMOV</param>
        /// <param name="compl">lista de dados opcionais</param>
        /// <returns>retorna TRUE, se o valor (no compl) for válido, caso contrário, retorna FALSE</returns>
        public Decimal ValidarQtdReceber(CITEMMOV obj)
        {
            return dal.ValidarQtd(obj);
        }

        public CITEMMOV Buscar(CITEMMOV obj)
        {
            return dal.Buscar(obj);
        }
    }
}