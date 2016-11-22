using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.AcessoDados.Global;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Global
{
    public class FilialControl
    {
        private readonly FilialDAL dal = new FilialDAL();

        [Obsolete]
        public object ExecutaFuncao(GFILIAL obj, Funcoes funcao, List<string> compl)
        {
            try
            {
                switch (funcao)
                {
                    case Funcoes.BuscarLista:
                        return BuscarLista();

                    case Funcoes.BuscarListaEspecifica:
                        return BuscarListaEspecifica(obj);

                    case Funcoes.Adicionar:
                        return Criar(obj);

                    case Funcoes.Atualizar:
                        return Atualizar(obj);

                    case Funcoes.Buscar:
                        return Buscar(obj);

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

        public IQueryable<GFILIAL> BuscarLista()
        {
            return dal.BuscarLista();
        }

        public IQueryable<GFILIAL> BuscarListaEspecifica(GFILIAL obj)
        {
            return dal.BuscarListaEspecifica(obj);
        }

        public bool Criar(GFILIAL obj)
        {
            return dal.Criar(obj);
        }

        public object Atualizar(GFILIAL obj)
        {
            return dal.Atualizar(obj);
        }

        public GFILIAL Buscar(GFILIAL obj)
        {
            return dal.Buscar(obj);
        }
    }
}