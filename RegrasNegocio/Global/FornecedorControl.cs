using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.AcessoDados.Global;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Global
{
    public class FornecedorControl
    {
        private readonly FornecedorDAL dal = new FornecedorDAL();

        [Obsolete]
        public object ExecutaFuncao(GCLIFOR obj, Funcoes funcao, List<string> compl)
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

                    case Funcoes.BuscarAtual:
                        return BuscarAtual();

                    case Funcoes.Buscar:
                        return Buscar(obj);

                    case Funcoes.BuscarPorTelefone:
                        return BuscarPorTelefone(obj);

                    case Funcoes.Cancelar:
                        return Cancelar(obj);

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

        public bool Atualizar(GCLIFOR obj)
        {
            return dal.Atualizar(obj);
        }

        public IQueryable<GCLIFOR> BuscarListaEspecifica(GCLIFOR obj)
        {
            return dal.BuscarListaEspecifica(obj);
        }

        public IQueryable<GCLIFOR> BuscarLista()
        {
            return dal.BuscarLista();
        }

        public bool Criar(GCLIFOR obj)
        {
            return dal.Criar(obj);
        }

        public bool Cancelar(GCLIFOR obj)
        {
            return dal.Excluir(obj);
        }

        public IQueryable<GTPCLIFOR> BuscarAtual()
        {
            return dal.BuscarAtual();
        }

        public GCLIFOR Buscar(GCLIFOR obj)
        {
            return dal.Buscar(obj);
        }

        public GCLIFOR BuscarPorTelefone(GCLIFOR obj)
        {
            return dal.BuscarByTelefone(obj);
        }
    }
}