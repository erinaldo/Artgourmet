using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.AcessoDados.Atendimento;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Atendimento
{
    public class FidelidadeControl
    {
        private readonly FidelidadeDAL dal = new FidelidadeDAL();

        [Obsolete]
        public object ExecutaFuncao(AFIDELIDADE obj, Funcoes funcao, List<String> compl)
        {
            try
            {
                switch (funcao)
                {
                    case Funcoes.Adicionar:
                        return Criar(obj);

                    case Funcoes.Atualizar:
                        return Atualizar(obj);


                    case Funcoes.Buscar:
                        return Buscar(obj);

                    case Funcoes.BuscarListaEspecifica:
                        return BuscarListaEspecifica(obj);

                    case Funcoes.BuscarLista:
                        return BuscarLista();

                    case Funcoes.BuscarProduto:
                        return BuscarProduto(obj);

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

        public IQueryable<AFIDELIDADE> BuscarListaEspecifica(AFIDELIDADE obj)
        {
            return dal.BuscarListaEspecifica(obj);
        }

        public IQueryable<APRDFIDELIDADE> BuscarProduto(AFIDELIDADE obj)
        {
            return dal.BuscarProduto(obj);
        }

        public IQueryable<AFIDELIDADE> BuscarLista()
        {
            return dal.BuscarLista();
        }

        public AFIDELIDADE Buscar(AFIDELIDADE obj)
        {
            return dal.Buscar(obj);
        }

        public bool Atualizar(AFIDELIDADE obj)
        {
            return dal.Atualizar(obj);
        }

        public bool Criar(AFIDELIDADE obj)
        {
            return dal.Criar(obj);
        }
    }
}