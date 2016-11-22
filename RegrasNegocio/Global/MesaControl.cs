using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.AcessoDados.Global;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Global
{
    public class MesaControl : ControladorBase
    {
        private readonly MesaDAL dal = new MesaDAL();


        /// <summary>
        /// Função que gerencia qual o que será executado
        /// </summary>
        /// <param name="obj">objeto mesa</param>
        /// <param name="funcao">nome da função a ser executada</param>
        /// <param name="compl">informações complementares</param>
        /// <param name="obj2">objeto reserva</param>
        /// <returns></returns>
        [Obsolete]
        public object ExecutaFuncao(GMESA obj, Funcoes funcao, List<string> compl, RRESERVA obj2)
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

                    case Funcoes.BuscarListaPorReserva:
                        return BuscarListaPorReserva(obj2);

                    case Funcoes.BuscarListaEspecifica:
                        return BuscarListaEspecifica(obj);

                    case Funcoes.BuscaMatrizAtiva:
                        return BuscarMatrizAtiva();

                    case Funcoes.ValidaConta:
                        return ValidarConta(obj);

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

        public GMATRIZMESA BuscarMatrizAtiva()
        {
            return dal.BuscarMatrizAtiva();
        }

        /// <summary>
        /// Função para criar
        /// </summary>
        /// <param name="obj">objeto mesa</param>
        /// <returns>true ou false</returns>
        public Boolean Criar(GMESA obj)
        {
            return dal.Criar(obj);
        }

        /// <summary>
        /// Função para buscar
        /// </summary>
        /// <param name="obj">objeto mesa</param>
        /// <returns>objeto mesa</returns>
        public GMESA Buscar(GMESA obj)
        {
            return dal.Buscar(obj);
        }

        /// <summary>
        /// Função para buscar
        /// </summary>
        /// <param name="nuMesa"> Numero da Mesa </param>
        /// <returns>objeto mesa</returns>
        public GMESA Buscar(int nuMesa)
        {
            return dal.Buscar(nuMesa);
        }

        /// <summary>
        /// Função para buscar uma lista
        /// </summary>
        /// <returns>Lista de objetos</returns>
        public IQueryable<GMESA> BuscarLista()
        {
            return dal.BuscarLista();
        }

        /// <summary>
        /// Função para buscar uma lista por reserva
        /// </summary>
        /// <returns>Lista de objetos</returns>
        public IQueryable<GMESA> BuscarListaPorReserva(RRESERVA obj2)
        {
            return dal.BuscarListaPorReserva(obj2);
        }

        /// <summary>
        /// Função para buscar uma lista com parametros
        /// </summary>
        /// <param name="obj">objeto mesa</param>
        /// <returns>lista de objetos</returns>
        public IQueryable<GMESA> BuscarListaEspecifica(GMESA obj)
        {
            return dal.BuscarListaEspecifica(obj);
        }

        /// <summary>
        /// Função para atualizar
        /// </summary>
        /// <param name="obj">objeto mesa</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(GMESA obj)
        {
            return dal.Atualizar(obj);
        }

        public bool AtualizarStatus(GMESA obj)
        {
            return dal.AtualizarStatus(obj);
        }

        public bool ValidarConta(GMESA obj)
        {
            return dal.VerificarConta(obj);
        }
    }
}