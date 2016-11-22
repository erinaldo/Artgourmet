using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.AcessoDados.Estoque;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Estoque
{
    public class CardapioItemControl : ControladorBase
    {
        private readonly CardapioItemDAL dal = new CardapioItemDAL();


        /// <summary>
        /// Função que gerencia qual o que será executado
        /// </summary>
        /// <param name="obj">objeto</param>
        /// <param name="funcao">nome da função a ser executada</param>
        /// <param name="compl">informações complementares, 0 -> sistema</param>
        /// <returns></returns>
        [Obsolete]
        public object ExecutaFuncao(ECARDAPIOITEM obj, Funcoes funcao, List<string> compl)
        {
            try
            {
                // Dependendo do parametro "funcao" será executada uma função diferente
                switch (funcao)
                {
                    case Funcoes.BuscarListaEspecifica:
                        return BuscarListaEspecifica(obj);

                    case Funcoes.BuscarPreco:
                        return BuscarPrecoItem(obj);

                    case Funcoes.BuscarItem:
                        return BuscarIdProduto(obj);

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
        /// Função para buscar uma lista
        /// </summary>
        /// <returns>Lista de objetos</returns>
        public IQueryable<ECARDAPIOITEM> BuscarListaEspecifica(ECARDAPIOITEM obj)
        {
            return dal.BuscarListaEspecifica(obj);
        }

        public List<ECARDAPIOITEM> BuscarItensCardapio(int idCardapio)
        {
            return dal.BuscarItensCardapio(idCardapio);
        }

        public decimal BuscarPrecoItem(ECARDAPIOITEM obj)
        {
            return dal.BuscarPrecoItem(obj);
        }

        public int BuscarIdProduto(ECARDAPIOITEM obj)
        {
            return dal.BuscarIdProdutoItem(obj);
        }
    }
}