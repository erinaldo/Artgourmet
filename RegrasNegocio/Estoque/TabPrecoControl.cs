using System;
using System.Collections.Generic;
using Artebit.Restaurante.Global.AcessoDados.Estoque;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Estoque
{
    public class TabPrecoControl
    {
        private readonly TabPrecoDAL dal = new TabPrecoDAL();

        [Obsolete]
        public object ExecutaFuncao(ETABPRECO obj, Funcoes funcoes, List<string> compl)
        {
            try
            {
                switch (funcoes)
                {
                    case Funcoes.BuscarLista:
                        return BuscarLista(obj);

                    case Funcoes.Buscar:
                        return Buscar(obj);

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

        public List<ETABPRECO> BuscarLista(ETABPRECO obj)
        {
            return dal.BuscarLista(obj);
        }

        public ETABPRECO Buscar(ETABPRECO obj)
        {
            return dal.Buscar(obj);
        }
    }
}