using System;
using System.Collections.Generic;
using System.Linq;
using Artebit.Restaurante.Global.AcessoDados.Producao;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.RegrasNegocio
{
    public class MonitorControl
    {
        private readonly MonitorDAL dal = new MonitorDAL();

        [Obsolete]
        public object ExecutaFuncao(AMONITOR obj, Funcoes funcoes, List<string> compl)
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
                        return Cancelar(obj);

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
        /// Criar Monitor
        /// </summary>
        /// <param name="obj">Objeto AMONITOR</param>
        /// <returns>Objeto AMONITOR</returns>
        public bool Criar(AMONITOR obj)
        {
            return dal.Criar(obj);
        }


        /// <summary>
        /// Buscar informações da Monitor
        /// </summary>
        /// <param name="obj">Objeto AMONITOR</param>
        /// <returns>Objeto AMONITOR</returns>
        public object Buscar(AMONITOR obj)
        {
            return dal.Buscar(obj);
        }


        /// <summary>
        /// Atualizar dados da Monitor
        /// </summary>
        /// <param name="obj">Objeto AMONITOR</param>
        /// <returns>True ou false</returns>
        public bool Atualizar(AMONITOR obj)
        {
            return dal.Atualizar(obj);
        }

        /// <summary>
        /// Buscar lista de Monitors
        /// </summary>
        /// <returns>Lista de Monitors</returns>
        public IQueryable<AMONITOR> BuscarLista()
        {
            return dal.BuscarLista();
        }

        /// <summary>
        /// Exclui monitor
        /// </summary>
        /// <param name="obj">Objeto AMONITOR</param>
        /// <returns></returns>
        public bool Cancelar(AMONITOR obj)
        {
            return dal.Excluir(obj);
        }
    }
}