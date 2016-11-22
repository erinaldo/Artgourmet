using System;
using System.Data;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Producao
{
    /// <summary>
    /// Metodos para manipulação da tabela AMONITOR
    /// </summary>
    public class MonitorDAL
    {
        /// <summary>
        /// Metodo de inserção de dados na tabela AMONITOR
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Criar(AMONITOR obj)
        {
            if (Memoria.Empresa != 0 && Memoria.Filial != 0) //verifica existencia
            {
                // Atualiza a empresa e filial
                obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);
                obj.idFilial = Convert.ToInt32(Memoria.Filial);


                obj.idMonitor = Contexto.GerarId("AMONITOR");

                //altera o valor 0 para o novo ID gerado pelo "Gera ID"
                foreach (AMONPRD amp in obj.AMONPRD)
                {
                    amp.idMonitor = obj.idMonitor;
                }

                //altera o valor 0 para o novo ID gerado pelo "Gera ID"
                foreach (AMONMESA amm in obj.AMONMESA)
                {
                    amm.idMonitor = obj.idMonitor;
                }

                // Adicionar Monitor
                Contexto.Atual.AddToAMONITOR(obj);

                // Salva alterações
                Contexto.Atual.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///  Metodo que faz busca no banco de dados e retorna uma lista com todos os Monitores cadastrados
        /// </summary>
        /// <returns></returns>
        public IQueryable<AMONITOR> BuscarLista()
        {
            if (Memoria.Empresa != 0 || Memoria.Filial != 0)
            {
                // Busca a lista
                IQueryable<AMONITOR> lista = from a in Contexto.Atual.AMONITOR
                                             where (a.idEmpresa == Memoria.Empresa || Memoria.Empresa == 0)
                                                   && (a.idFilial == Memoria.Filial || Memoria.Filial == 0)
                                             select a;

                return lista;
            }
            else
            {
                return null;
            }
        }

        /* Busca de monitor */

        /// <summary>
        /// Metodo que retorna um registro da base de dados
        /// </summary>
        /// <param name="obj">AMONITOR</param>
        /// <returns></returns>
        public AMONITOR Buscar(AMONITOR obj)
        {
            if (Memoria.Empresa != 0 || Memoria.Filial != 0)
            {
                AMONITOR mon = Contexto.Atual.AMONITOR.SingleOrDefault(a => a.idMonitor == obj.idMonitor
                                                                            && (a.idEmpresa == Memoria.Empresa ||
                                                                                a.idEmpresa == 0)
                                                                            && (a.idFilial == Memoria.Filial ||
                                                                                a.idFilial == 0));
                return mon;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Método que atualiza dados cadastrados na base de dados
        /// </summary>
        /// <param name="obj">AMONITOR</param>
        /// <returns></returns>
        public bool Atualizar(AMONITOR obj)
        {
            if (Memoria.Empresa != 0 || Memoria.Filial != 0)
            {
                //Atualiza o Monitor
                Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
                Contexto.Atual.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Cancelar(AMONITOR obj)
        {
            if (Memoria.Empresa != 0 || Memoria.Filial != 0)
            {
                //Atualiza o Monitor
                obj.ativo = false;
                return Atualizar(obj);
            }
            else
            {
                return false;
            }
        }

        public bool Excluir(AMONITOR obj)
        {
            obj =
                Contexto.Atual.AMONITOR.SingleOrDefault(
                    r => r.idMonitor == obj.idMonitor && r.idEmpresa == obj.idEmpresa);

            if (obj != null)
            {
                if (obj.AMONMESA.Count > 0)
                {
                    Memoria.MsgGlobal =
                        "Não é possível excluir monitor, pois o mesmo possui Mesa(s) a ele associado(s).";
                    return false;
                }
                else
                {
                    if (obj.AMONPRD.Count > 0)
                    {
                        Memoria.MsgGlobal =
                            "Não é possível excluir monitor, pois o mesmo possui Produto(s) a ele associado(s).";
                        return false;
                    }
                    else
                    {
                        Contexto.Atual.DeleteObject(obj);
                        Contexto.Atual.SaveChanges();

                        return true;
                    }
                }
            }
            else
            {
                return true;
            }
        }
    }
}