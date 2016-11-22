using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.AcessoDados.Estoque;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Estoque
{
    public class UnidadesControl
    {
        private readonly UnidadeDAL dal = new UnidadeDAL();

        [Obsolete]
        public object ExecutaFuncao(EUNIDADE obj, Funcoes funcao, List<string> compl)
        {
            try
            {
                switch (funcao)
                {
                    case Funcoes.BuscarLista:
                        return BuscarLista();

                    case Funcoes.Adicionar:
                        return Criar(obj);

                    case Funcoes.BuscarListaEspecifica:
                        return BuscarListaEspecifica(obj);

                    case Funcoes.Atualizar:
                        return Atualizar(obj);

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

        public object Atualizar(EUNIDADE obj)
        {
            return dal.Atualizar(obj);
        }

        public object BuscarListaEspecifica(EUNIDADE obj)
        {
            return dal.BuscarListaEspecifica(obj);
        }

        public IQueryable<EUNIDADE> BuscarLista()
        {
            return dal.BuscarLista();
        }

        public bool Criar(EUNIDADE obj)
        {
            return dal.Criar(obj);
        }

        public EUNIDADE Buscar(EUNIDADE obj)
        {
            return dal.Buscar(obj);
        }
    }
}