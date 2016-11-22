using System;
using System.Data;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Reserva
{
    public class EsperaDAL
    {
        /// <summary>
        /// Função para buscar objeto espera completo
        /// </summary>
        /// <param name="obj">objeto espera</param>
        /// <returns>objeto espera</returns>
        public RFILAESPERA Buscar(RFILAESPERA obj)
        {
            // Busca o espera
            RFILAESPERA espera = Contexto.Atual.RFILAESPERA.SingleOrDefault(a => a.idFila == obj.idFila
                                                                                 &&
                                                                                 (a.idEmpresa == Memoria.Empresa ||
                                                                                  Memoria.Empresa == 0)
                                                                                 &&
                                                                                 (a.idFilial == Memoria.Filial ||
                                                                                  Memoria.Filial == 0));

            return espera;
        }

        /// <summary>
        /// Função para buscar uma lista de objetos espera
        /// </summary>
        /// <returns>lista de objetos</returns>
        public IQueryable<RFILAESPERA> BuscarLista()
        {
            // Busca o espera
            IQueryable<RFILAESPERA> esperas = from a in Contexto.Atual.RFILAESPERA
                                              where (a.idEmpresa == Memoria.Empresa || Memoria.Empresa == 0)
                                                    && (a.idFilial == Memoria.Filial || Memoria.Filial == 0)
                                              select a;

            return esperas;
        }

        /// <summary>
        /// Função para buscar uma lista de objetos lista com parametros
        /// </summary>
        /// <returns>lista de objetos</returns>
        public IQueryable<RFILAESPERA> BuscarListaEspecifica(RFILAESPERA obj)
        {
            // Busca o mesa
            IQueryable<RFILAESPERA> mesas = from a in Contexto.Atual.RFILAESPERA
                                            where (a.idEmpresa == Memoria.Empresa || Memoria.Empresa == 0)
                                                  && (a.idFilial == Memoria.Filial || Memoria.Filial == 0)
                                                  && (a.idStatus == obj.idStatus || obj.idStatus == null)
                                                  && (a.qtdLugares == obj.qtdLugares || obj.qtdLugares == null)
                                                  && (a.dataAlteracao == obj.dataAlteracao || obj.dataAlteracao == null)
                                                  && (a.dataAlteracao == obj.dataAlteracao || obj.dataAlteracao == null)
                                            select a;

            return mesas;
        }

        /// <summary>
        /// Função para criar novo
        /// </summary>
        /// <param name="obj">objeto espera</param>
        /// <returns>true ou false</returns>
        public bool Criar(RFILAESPERA obj)
        {
            if (Memoria.Empresa != 0 && Memoria.Empresa != 0)
            {
                // atualiza a empresa e a filial
                obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);
                obj.idFilial = Convert.ToInt32(Memoria.Filial);

                // atualiza data de inclusão e alteração
                obj.dataInclusao = DateTime.Now;
                obj.dataAlteracao = DateTime.Now;

                obj.idFila = Contexto.GerarId("RREserva");


                // adiciona espera
                Contexto.Atual.AddToRFILAESPERA(obj);

                // salva alterações
                Contexto.Atual.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Função para buscar
        /// </summary>
        /// <param name="obj">objeto espera</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(RFILAESPERA obj)
        {
            // atualiza data de alteração
            obj.dataAlteracao = DateTime.Now;

            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();
            return true;
        }

        /// <summary>
        /// Função que troca o status para inativo
        /// </summary>
        /// <param name="obj">objeto espera</param>
        /// <returns>true ou false</returns>
        public bool Cancelar(RFILAESPERA obj)
        {
            obj.idStatus = 9;
            return Atualizar(obj);
        }

        /// <summary>
        /// Função que troca o status para atendido
        /// </summary>
        /// <returns>true ou false</returns>
        public bool Atender(RFILAESPERA obj)
        {
            obj.idStatus = 11;

            return Atualizar(obj);
        }
    }
}