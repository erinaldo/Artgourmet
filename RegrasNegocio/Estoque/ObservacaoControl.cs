using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.AcessoDados.Estoque;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Estoque
{
    public class ObservacaoControl : ControladorBase
    {
        private readonly ObservacaoDAL dal = new ObservacaoDAL();


        /// <summary>
        /// Função que gerencia qual o que será executado
        /// </summary>
        /// <param name="obj">objeto</param>
        /// <param name="funcao">nome da função a ser executada</param>
        /// <param name="compl">informações complementares, 0 -> sistema</param>
        /// <returns></returns>
        [Obsolete]
        public object ExecutaFuncao(EOBSERVACAO obj, Funcoes funcao, List<string> compl)
        {
            try
            {
                // Dependendo do parametro "funcao" será executada uma função diferente
                switch (funcao)
                {
                    case Funcoes.Buscar:
                        return Buscar(obj);

                    case Funcoes.BuscarLista:
                        return BuscarLista();

                    case Funcoes.Adicionar:
                        return Criar(obj);

                    case Funcoes.Atualizar:
                        return Atualizar(obj);

                    case Funcoes.Cancelar:
                        return Excluir(obj);

                    case Funcoes.BuscarAtual:
                        return BuscarAtual();

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

        public IQueryable<EOBSERVACAO> BuscarLista()
        {
            return dal.BuscarLista();
        }

        /// <summary>
        /// Função para buscar
        /// </summary>
        /// <param name="obj">objeto</param>
        /// <returns>objeto usuário</returns>
        public EOBSERVACAO Buscar(EOBSERVACAO obj)
        {
            return dal.Buscar(obj);
        }

        public bool Criar(EOBSERVACAO obj)
        {
            return dal.Adicionar(obj);
        }

        public bool Atualizar(EOBSERVACAO obj)
        {
            return dal.Atualizar(obj);
        }

        public bool Excluir(EOBSERVACAO obj)
        {
            return dal.Excluir(obj);
        }

        public IQueryable<EOBSERVACAO> BuscarAtual()
        {
            return dal.BuscarAtual();
        }

        public IQueryable<EPRODOBSBAIXA> BuscarProduto(EOBSERVACAO obj)
        {
            return dal.BuscarProduto(obj);
        }
    }
}