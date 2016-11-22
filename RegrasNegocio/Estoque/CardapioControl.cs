using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.AcessoDados.Estoque;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Estoque
{
    public class CardapioControl : ControladorBase
    {
        private readonly CardapioDAL dal = new CardapioDAL();


        /// <summary>
        /// Função que gerencia qual o que será executado
        /// </summary>
        /// <param name="obj">objeto</param>
        /// <param name="funcao">nome da função a ser executada</param>
        /// <param name="compl">informações complementares, 0 -> sistema</param>
        /// <returns></returns>
        [Obsolete]
        public object ExecutaFuncao(ECARDAPIO obj, Funcoes funcao, List<string> compl)
        {
            try
            {
                // Dependendo do parametro "funcao" será executada uma função diferente
                switch (funcao)
                {
                    case Funcoes.Adicionar:
                        return Criar(obj);

                    case Funcoes.Atualizar:
                        return Atualizar(obj);

                    case Funcoes.Buscar:
                        return Buscar(obj);

                    case Funcoes.BuscarLista:
                        return BuscarLista();

                    case Funcoes.BuscarAtual:
                        return BuscarAtual();

                    case Funcoes.Cancelar:
                        return Cancelar(obj);

                    case Funcoes.Excluir:
                        return Excluir(obj);

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

        /// <summary>
        /// Função para criar
        /// </summary>
        /// <param name="obj">objeto</param>
        /// <returns>true ou false</returns>
        public Boolean Criar(ECARDAPIO obj)
        {
            return dal.Criar(obj);
        }

        /// <summary>
        /// Função para buscar
        /// </summary>
        /// <param name="obj">objeto</param>
        /// <returns>objeto usuário</returns>
        public ECARDAPIO Buscar(ECARDAPIO obj)
        {
            return dal.Buscar(obj);
        }

        /// <summary>
        /// Função para buscar uma lista
        /// </summary>
        /// <returns>Lista de objetos</returns>
        public IQueryable<ECARDAPIO> BuscarLista()
        {
            return dal.BuscarLista();
        }

        /// <summary>
        /// Função para atualizar
        /// </summary>
        /// <param name="obj">objeto</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(ECARDAPIO obj)
        {
            return dal.Atualizar(obj);
        }

        /// <summary>
        /// Função para mudar o status para inativo
        /// </summary>
        /// <param name="obj">obj</param>
        /// <returns>true ou false</returns>
        public bool Cancelar(ECARDAPIO obj)
        {
            return dal.Cancelar(obj);
        }


        /// <summary>
        /// Busca o cardapio atual
        /// </summary>
        public int BuscarAtualId()
        {
            return dal.BuscarAtualId();
        }

        /// <summary>
        /// Busca o cardapio atual
        /// </summary>
        public ECARDAPIO BuscarAtual()
        {
            return dal.BuscarAtual();
        }

        public bool Excluir(ECARDAPIO obj)
        {
            return dal.Excluir(obj);
        }
    }
}