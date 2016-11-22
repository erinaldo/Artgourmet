using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.AcessoDados.Compras;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Compras
{
    public class MovimentoControl : ControladorBase
    {
        private readonly MovimentoDAL dal = new MovimentoDAL();


        /// <summary>
        /// Função que gerencia qual o que será executado
        /// </summary>
        /// <param name="obj">objeto usuario</param>
        /// <param name="funcao">nome da função a ser executada</param>
        /// <param name="compl">informações complementares, 0 -> tipo de movimento : para a função Buscar()</param>
        /// <param name="compl">informações complementares, 0 -> idEmpres; 1 -> idFilial; 2 -> idMov : para a função Criar()</param>
        /// <returns></returns>
        [Obsolete]
        public object ExecutaFuncao(CMOVIMENTO obj, Funcoes funcao, List<List<string>> compl)
        {
            try
            {
                // Dependendo do parametro "funcao" será executada uma função diferente
                switch (funcao)
                {
                    case Funcoes.Adicionar:
                        if (compl != null)
                        {
                            return Criar(obj, compl);
                        }
                        else
                        {
                            return Criar(obj, null);
                        }

                    case Funcoes.Atualizar:
                        return Atualizar(obj);

                    case Funcoes.Buscar:
                        return Buscar(obj);

                    case Funcoes.BuscarLista:
                        return BuscarLista(compl[0]);

                    case Funcoes.BuscarItem:
                        return BuscarItem(obj);

                    case Funcoes.BuscarTipo:
                        return BuscarTipo();

                    case Funcoes.BuscarVazio:
                        return BuscarVazio();

                    case Funcoes.BuscarStatus:
                        return BuscarStatus();

                    case Funcoes.RetornaSequencia:
                        return RetornarSequencial(compl[0][0]);

                        //case Funcoes.Cancelar:
                        //    return cancela(obj);

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

        public List<string> RetornarSequencial(string codTipoMov)
        {
            return dal.PegarSeqMov(codTipoMov);
        }

        /// <summary>
        /// Função para criar
        /// </summary>
        /// <param name="obj">objeto movimento</param>
        /// <returns>true ou false</returns>
        public Boolean Criar(CMOVIMENTO obj, List<List<string>> compl)
        {
            return dal.Criar(obj, compl);
        }

        /// <summary>
        /// Função para buscar
        /// </summary>
        /// <param name="obj">objeto usuario</param>
        /// <returns>objeto usuário</returns>
        public CMOVIMENTO Buscar(CMOVIMENTO obj)
        {
            return dal.Buscar(obj);
        }

        /// <summary>
        /// Função para buscar uma lista
        /// </summary>
        /// <returns>Lista de objetos</returns>
        public IQueryable<CMOVIMENTO> BuscarLista(List<string> tpmov)
        {
            return dal.BuscarLista(tpmov);
        }

        /// <summary>
        /// Função para atualizar
        /// </summary>
        /// <param name="obj">objeto usuario</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(CMOVIMENTO obj)
        {
            return dal.Atualizar(obj);
        }

        /// <summary>
        /// Função para buscar Item
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>true ou false</returns>
        public IQueryable<CITEMMOV> BuscarItem(CMOVIMENTO obj)
        {
            return dal.BuscarItem(obj);
        }

        /// <summary>
        /// Função para buscar tipo
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>true ou false</returns>
        public IQueryable<CTPMOV> BuscarTipo()
        {
            return dal.BuscarTipo();
        }

        /// <summary>
        /// Função para buscar combobox
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>true ou false</returns>
        public IQueryable<CMOVIMENTO> BuscarVazio()
        {
            return dal.BuscarVazio();
        }

        /// <summary>
        /// Função para buscar status
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>lista de CSTATMOV</returns>
        public IQueryable<CSTATMOV> BuscarStatus()
        {
            return dal.BuscarStatus();
        }


        /// <summary>
        /// Função para mudar o status para cancelado
        /// </summary>
        /// <param name="obj">obj movimento</param>
        /// <returns>true ou false</returns>
        //private bool cancela(CMOVIMENTO obj)
        //{
        //    return dal.Cancelar(obj);
        //}
    }
}