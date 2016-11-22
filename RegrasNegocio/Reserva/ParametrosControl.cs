﻿using System;
using System.Collections.Generic;
using Artebit.Restaurante.Global.AcessoDados.Reserva;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Reserva
{
    public class Parametros : ControladorBase
    {
        private readonly ParametrosDAL dal = new ParametrosDAL();

        /// <summary>
        /// Função que gerencia qual o que será executado
        /// </summary>
        /// <param name="obj">objeto reserva</param>
        /// <param name="funcao">nome da função a ser executada</param>
        /// <param name="compl">informações complementares</param>
        /// <returns></returns>
        [Obsolete]
        public object ExecutaFuncao(RPARAM obj, Funcoes funcao, List<string> compl)
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
                        return Buscar();

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

        /// <summary>
        /// Função para buscar
        /// </summary>
        /// <param name="obj">objeto parametro</param>
        /// <returns>objeto parametro</returns>
        public RPARAM Buscar()
        {
            return dal.Buscar();
        }

        /// <summary>
        /// Função para criar
        /// </summary>
        /// <param name="obj">objeto parametro</param>
        /// <returns>objeto parametro</returns>
        public bool Criar(RPARAM obj)
        {
            return dal.Criar(obj);
        }

        /// <summary>
        /// Função para atualizar
        /// </summary>
        /// <param name="obj">objeto parametro</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(RPARAM obj)
        {
            return dal.Atualizar(obj);
        }
    }
}