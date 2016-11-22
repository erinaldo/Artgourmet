using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Estoque
{
    public class LocalControl
    {
        private readonly LocalDAL dal = new LocalDAL();

        [Obsolete]
        public object ExecutaFuncao(ELOC obj, Funcoes funcao, List<string> compl)
        {
            try
            {
                switch (funcao)
                {
                    case Funcoes.BuscarLista:
                        return BuscarLista();

                    case Funcoes.BuscarListaEspecifica:
                        return BuscarListaEspecifica(obj);

                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                Excecao.TratarExcecao(ex);
                return false;
            }
        }

        public IQueryable<ELOC> BuscarListaEspecifica(ELOC obj)
        {
            return dal.BuscarListaEspecifica(obj);
        }

        public IQueryable<ELOC> BuscarLista()
        {
            return dal.BuscarLista();
        }
    }
}