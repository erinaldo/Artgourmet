using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.AcessoDados.Atendimento;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio.Atendimento
{
    public class ImpressoraControl
    {
        private readonly ImpressoraDAL dal = new ImpressoraDAL();

        [Obsolete]
        public object ExecutaFuncao(AIMPRESSORA obj, Funcoes funcoes, List<string> comp)
        {
            try
            {
                switch (funcoes)
                {
                    case Funcoes.Adicionar:
                        return Criar(obj);
                    case Funcoes.Buscar:
                        return Buscar(obj);
                    case Funcoes.BuscarLista:
                        return BuscarLista();
                    case Funcoes.Atualizar:
                        return Atualizar(obj);
                    case Funcoes.Cancelar:
                        return Desativar(obj);
                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                Excecao.TratarExcecao(ex);
                throw;
            }
        }


        /// <summary>
        /// Criar Impressora
        /// </summary>
        /// <param name="obj">Objeto AIMPRESSORA</param>
        /// <returns>Objeto AIMPRESSORA</returns>
        public bool Criar(AIMPRESSORA obj)
        {
            return dal.Criar(obj);
        }


        /// <summary>
        /// Buscar informações da impressora
        /// </summary>
        /// <param name="obj">Objeto AIMPRESSORA</param>
        /// <returns>Objeto AIMPRESSORA</returns>
        public object Buscar(AIMPRESSORA obj)
        {
            return dal.Buscar(obj);
        }


        /// <summary>
        /// Atualizar dados da impressora
        /// </summary>
        /// <param name="obj">Objeto AIMPRESSORA</param>
        /// <returns>True ou false</returns>
        public bool Atualizar(AIMPRESSORA obj)
        {
            return dal.Atualizar(obj);
        }


        /// <summary>
        /// Desativar uma impressora
        /// </summary>
        /// <param name="obj">Objeto AIMPRESSORA</param>
        /// <returns>true ou false</returns>
        public bool Desativar(AIMPRESSORA obj)
        {
            return dal.Desativar(obj);
        }

        /// <summary>
        /// Buscar lista de impressoras
        /// </summary>
        /// <returns>Lista de impressoras</returns>
        public IQueryable<AIMPRESSORA> BuscarLista()
        {
            return dal.BuscarLista();
        }
    }
}