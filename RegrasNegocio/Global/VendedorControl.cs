using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.AcessoDados.Global;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Global
{
    public class VendedorControl : ControladorBase
    {
        private readonly VendedorDAL dal = new VendedorDAL();

        /// <summary>
        /// Função que gerencia qual e o que será executado
        /// </summary>
        /// <param name="obj">objeto vendedor</param>
        /// <param name="funcao">nome da função a ser executada</param>
        /// <param name="compl">campos complementares</param>
        /// <returns>obj</returns>
        [Obsolete]
        public object ExecutaFuncao(GVENDEDOR obj, Funcoes funcao, List<String> compl)
        {
            try
            {
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

                    case Funcoes.Cancelar:
                        return Cancelar(obj);

                    case Funcoes.Verificar:
                        return Verificar(obj);

                    case Funcoes.ValidaSenha:
                        return ValidarSenha(obj);

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
        /// função para criar
        /// </summary>
        /// <param name="obj">objeto GVendedor</param>
        /// <returns>objeto vendedor</returns>
        public bool Criar(GVENDEDOR obj)
        {
            return dal.Criar(obj);
        }

        /// <summary>
        /// função para atualizar
        /// </summary>
        /// <param name="obj">objeto vendedor</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(GVENDEDOR obj)
        {
            return dal.Atualizar(obj);
        }


        /// <summary>
        /// Função para buscar
        /// </summary>
        /// <param name="obj">objeto vendedor</param>
        /// <returns>objeto vendedor</returns>
        public GVENDEDOR Buscar(GVENDEDOR obj)
        {
            return dal.Buscar(obj);
        }


        /// <summary>
        /// Função para buscar uma lista
        /// </summary>
        /// <returns>Lista de objetos</returns>
        public IQueryable<GVENDEDOR> BuscarLista()
        {
            return dal.BuscarLista();
        }

        /// <summary>
        /// Função para desativar vendedor
        /// </summary>
        /// <param name="obj">objeto vendedor</param>
        /// <returns>objeto vendedor</returns>
        public bool Cancelar(GVENDEDOR obj)
        {
            return dal.Cancelar(obj);
        }

        public GVENDEDOR Verificar(GVENDEDOR obj)
        {
            return dal.Verificar(obj);
        }

        public bool ValidarSenha(GVENDEDOR obj)
        {
            return dal.ValidarSenha(obj);
        }
    }
}