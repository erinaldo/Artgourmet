using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.AcessoDados.Reserva;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Reserva
{
    public class ReservaControl : ControladorBase
    {
        private readonly ReservaDAL dal = new ReservaDAL();


        /// <summary>
        /// Função que gerencia qual o que será executado
        /// </summary>
        /// <param name="obj">objeto reserva</param>
        /// <param name="funcao">nome da função a ser executada</param>
        /// <param name="compl">informações complementares</param>
        /// <returns></returns>
        [Obsolete]
        public object ExecutaFuncao(RRESERVA obj, Funcoes funcao, List<string> compl)
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

                    case Funcoes.BuscarVazio:
                        return BuscarVazio(obj);

                    case Funcoes.BuscarLista:
                        return BuscarLista();

                    case Funcoes.Cancelar:
                        return Cancelar(obj);

                    case Funcoes.Atender:
                        return Atender(obj);

                    case Funcoes.ConfereDados:
                        return ConferirDados(obj);

                    case Funcoes.ConfereDadosWeb:
                        return ConferirDadosWeb(obj);

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
        /// <param name="obj">objeto reserva</param>
        /// <returns>true ou false</returns>
        public Boolean Criar(RRESERVA obj)
        {
            return dal.Criar(obj);
        }

        /// <summary>
        /// Função para buscar
        /// </summary>
        /// <param name="obj">objeto reserva</param>
        /// <returns>objeto reserva</returns>
        public RRESERVA Buscar(RRESERVA obj)
        {
            return dal.Buscar(obj);
        }

        /// <summary>
        /// Função para buscar vazio
        /// </summary>
        /// <param name="obj">objeto reserva</param>
        /// <returns>objeto reserva</returns>
        public RRESERVA BuscarVazio(RRESERVA obj)
        {
            return dal.BuscarVazio(obj);
        }

        /// <summary>
        /// Função para buscar uma lista
        /// </summary>
        /// <returns>Lista de objetos</returns>
        public IQueryable<RRESERVA> BuscarLista()
        {
            return dal.BuscarLista();
        }

        /// <summary>
        /// Função para atualizar
        /// </summary>
        /// <param name="obj">objeto reserva</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(RRESERVA obj)
        {
            return dal.Atualizar(obj);
        }

        /// <summary>
        /// Função para mudar o status para inativo
        /// </summary>
        /// <param name="obj">obj reserva</param>
        /// <returns>true ou false</returns>
        public bool Cancelar(RRESERVA obj)
        {
            return dal.Cancelar(obj);
        }

        /// <summary>
        /// Função para mudar o status para ativada
        /// </summary>
        /// <param name="obj">obj reserva</param>
        /// <returns>true ou false</returns>
        public bool Atender(RRESERVA obj)
        {
            return dal.Atender(obj);
        }

        /// <summary>
        /// Função que valida os campos
        /// </summary>
        /// <param name="obj">objeto reserva</param>
        /// <returns>retorna mensagem de erro</returns>
        public string ConferirDados(RRESERVA obj)
        {
            string msg = "";

            if (obj.cliente == "")
            {
                msg += "Nome do cliente; ";
            }

            if (obj.telefone == "")
            {
                msg += "Telefone do cliente, ";
            }

            if (obj.data == null)
            {
                msg += "Data da reserva; ";
            }

            if (obj.horario == null)
            {
                msg += "Horário desejado; ";
            }

            if (obj.horarioLimite == null)
            {
                msg += "Horário limite, ";
            }

            if (msg != "")
            {
                msg = "Preencha os seguintes campos: " + msg;
            }

            return msg;
        }

        /// <summary>
        /// Função que valida os campos de reserva web
        /// </summary>
        /// <param name="obj">objeto reserva</param>
        /// <returns>retorna mensagem de erro</returns>
        public string ConferirDadosWeb(RRESERVA obj)
        {
            string msg = "";

            if (obj.cliente == "")
            {
                msg += "Nome do cliente; ";
            }

            if (obj.telefone == "")
            {
                msg += "Telefone do cliente, ";
            }

            if (obj.data == null)
            {
                msg += "Data da reserva; ";
            }

            if (obj.horario == null)
            {
                msg += "Horário desejado; ";
            }

            if (msg != "")
            {
                msg = "Preencha os seguintes campos: " + msg;
            }

            return msg;
        }
    }
}