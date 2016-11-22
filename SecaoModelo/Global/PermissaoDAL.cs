using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Global
{
    public class PermissaoDAL
    {
        /// <summary>
        /// Função para buscar objeto completo
        /// </summary>
        /// <param name="obj">objeto</param>
        /// <returns>objeto</returns>
        public GPERMISSAO Buscar(GPERMISSAO obj)
        {
            GPERMISSAO obj2 = Contexto.Atual.GPERMISSAO.SingleOrDefault(a => a.idPermissao == obj.idPermissao);

            return obj2;
        }

        /// <summary>
        /// Retorna lista de objeto
        /// </summary>
        /// <returns></returns>
        public IQueryable<GPERMISSAO> BuscarLista()
        {
            IQueryable<GPERMISSAO> lista = from a in Contexto.Atual.GPERMISSAO
                                           select a;
            return lista;
        }
    }
}