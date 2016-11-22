using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Global
{
    public class EmpresaDAL
    {
        /// <summary>
        /// Busca empresa atual da base(Deve ser utilizado somente em base local, que possui apenas uma Empresa)
        /// </summary>
        /// <returns>Objeto da Empresa Atual</returns>
        public GEMPRESA BuscarAtual()
        {
            return Contexto.Atual.GEMPRESA.First();
        }

        public IQueryable<GEMPRESA> BuscarLista()
        {
            IQueryable<GEMPRESA> lista = from a in Contexto.Atual.GUSRFILMOD
                                         join e in Contexto.Atual.GEMPRESA on a.idEmpresa equals e.idEmpresa
                                         where a.codSistema == Memoria.CodSistema
                                               && a.codUsuario == Memoria.Codusuario
                                               && a.idEmpresa != 0
                                         select e;

            return lista;
        }
    }
}