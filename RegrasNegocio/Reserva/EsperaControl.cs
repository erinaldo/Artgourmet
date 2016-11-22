using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.AcessoDados.Reserva;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Reserva
{
    public class EsperaControl : ControladorBase
    {
        private readonly EsperaDAL dal = new EsperaDAL();


        /// <summary>
        /// Função que gerencia qual o que será executado
        /// </summary>
        /// <param name="obj">objeto espera</param>
        /// <param name="funcao">nome da função a ser executada</param>
        /// <param name="compl">informações complementares</param>
        /// <returns></returns>
        [Obsolete]
        public object ExecutaFuncao(RFILAESPERA obj, Funcoes funcao, List<string> compl)
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

                    case Funcoes.BuscarListaEspecifica:
                        return BuscarListaEspecifica(obj);

                    case Funcoes.Cancelar:
                        return Cancelar(obj);

                    case Funcoes.Atender:
                        return Atender(obj);

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

        /// <summary>
        /// Função para criar
        /// </summary>
        /// <param name="obj">objeto espera</param>
        /// <returns>true ou false</returns>
        public Boolean Criar(RFILAESPERA obj)
        {
            return dal.Criar(obj);
        }

        /// <summary>
        /// Função para buscar
        /// </summary>
        /// <param name="obj">objeto espera</param>
        /// <returns>objeto espera</returns>
        public RFILAESPERA Buscar(RFILAESPERA obj)
        {
            return dal.Buscar(obj);
        }

        /// <summary>
        /// Função para buscar uma lista
        /// </summary>
        /// <returns>Lista de objetos</returns>
        public IQueryable<RFILAESPERA> BuscarLista()
        {
            return dal.BuscarLista();
        }

        /// <summary>
        /// Função para buscar uma lista com parametros
        /// </summary>
        /// <param name="obj">objeto fila</param>
        /// <returns>lista de objetos</returns>
        public IQueryable<RFILAESPERA> BuscarListaEspecifica(RFILAESPERA obj)
        {
            return dal.BuscarListaEspecifica(obj);
        }

        /// <summary>
        /// Função para atualizar
        /// </summary>
        /// <param name="obj">objeto espera</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(RFILAESPERA obj)
        {
            return dal.Atualizar(obj);
        }

        /// <summary>
        /// Função para mudar o status para ativada
        /// </summary>
        /// <param name="obj">obj fila</param>
        /// <param name="obj2">Lista de mesas</param>
        /// <returns>true ou false</returns>
        public bool Atender(RFILAESPERA obj)
        {
            return dal.Atender(obj);
        }

        /// <summary>
        /// Função para mudar o status para inativo
        /// </summary>
        /// <param name="obj">obj espera</param>
        /// <returns>true ou false</returns>
        public bool Cancelar(RFILAESPERA obj)
        {
            return dal.Cancelar(obj);
        }
    }
}