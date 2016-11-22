using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.AcessoDados.Estoque;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Estoque
{
    public class ControleEstoqueControl
    {
        private readonly ControleEstoqueDAL dal = new ControleEstoqueDAL();

        [Obsolete]
        public object ExecutaFunção(ECTESTOQUE obj, Funcoes funcao, List<string> comp)
        {
            try
            {
                switch (funcao)
                {
                    case Funcoes.BuscarLista:
                        return BuscarLista(obj);

                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                Excecao.TratarExcecao(ex);
                return null;
            }
        }

        public IQueryable<ECTESTOQUE> BuscarLista(ECTESTOQUE obj)
        {
            return dal.BuscarLista(obj);
        }
    }
}