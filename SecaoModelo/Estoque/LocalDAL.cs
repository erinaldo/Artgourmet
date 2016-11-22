using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Estoque
{
    public class LocalDAL
    {
        public IQueryable<ELOC> BuscarLista()
        {
            IQueryable<ELOC> lista = from a in Contexto.Atual.ELOC
                                     where a.idEmpresa == Memoria.Empresa || Memoria.Empresa == 0
                                     select a;
            return lista;
        }

        public IQueryable<ELOC> BuscarListaEspecifica(ELOC obj)
        {
            IQueryable<ELOC> lista = from a in Contexto.Atual.ELOC
                                     where (a.idEmpresa == Memoria.Empresa || Memoria.Empresa == 0)
                                           && a.idFilial == obj.idFilial
                                     select a;

            return lista;
        }
    }
}