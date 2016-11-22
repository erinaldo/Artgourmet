using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.AcessoDados.Estoque;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Estoque
{
    public class GruposControl
    {
        private readonly GrupoDAL dal = new GrupoDAL();

        [Obsolete]
        public object ExecutaFuncao(EGRUPO obj, Funcoes funcao, List<string> compl)
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

                    case Funcoes.Cancelar:
                        return Excluir(obj);

                    case Funcoes.BuscarAtual:
                        return BuscarAtual(obj);

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

        public object Atualizar(EGRUPO obj)
        {
            return dal.Atualizar(obj);
        }

        public object BuscarListaEspecifica(EGRUPO obj)
        {
            return dal.BuscarListaEspecifica(obj);
        }

        public IQueryable<EGRUPO> BuscarLista()
        {
            return dal.BuscarLista();
        }

        public bool Criar(EGRUPO obj)
        {
            return dal.Criar(obj);
        }

        public EGRUPO Buscar(EGRUPO obj)
        {
            return dal.Buscar(obj);
        }

        public EGRUPO Buscar(int idGrupo)
        {
            return dal.Buscar(idGrupo);
        }

        public bool Excluir(EGRUPO obj)
        {
            return dal.Excluir(obj);
        }

        public List<EOBSERVACAO> BuscarAtual(EGRUPO obj)
        {
            return dal.BuscarAtual(obj);
        }
    }
}