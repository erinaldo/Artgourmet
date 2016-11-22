using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Reserva
{
    public class ReservaDAL
    {
        private static Modelo.Restaurante contx;

        /// <summary>
        /// Função para buscar objeto reserva completo
        /// </summary>
        /// <param name="obj">objeto reserva</param>
        /// <returns>objeto reserva</returns>
        public RRESERVA Buscar(RRESERVA obj)
        {
            // Busca o reserva
            RRESERVA reserva = Contexto.Atual.RRESERVA.SingleOrDefault(a => a.idReserva == obj.idReserva
                                                                            &&
                                                                            (a.idEmpresa == Memoria.Empresa ||
                                                                             Memoria.Empresa == 0)
                                                                            &&
                                                                            (a.idFilial == Memoria.Filial ||
                                                                             Memoria.Filial == 0));

            return reserva;
        }

        /// <summary>
        /// Função para buscar objeto reserva sem as ligações
        /// </summary>
        /// <param name="obj">objeto reserva</param>
        /// <returns>objeto reserva</returns>
        public RRESERVA BuscarVazio(RRESERVA obj)
        {
            // Busca
            RRESERVA reserva = Contexto.Atual.RRESERVA.SingleOrDefault(a => a.idReserva == obj.idReserva
                                                                            &&
                                                                            (a.idEmpresa == Memoria.Empresa ||
                                                                             Memoria.Empresa == 0)
                                                                            &&
                                                                            (a.idFilial == Memoria.Filial ||
                                                                             Memoria.Filial == 0));


            if (reserva != null)
            {
                // Limpa os relacionamentos
                for (int x = 0; x < reserva.RRESMESA.Count; x++)
                {
                    reserva.RRESMESA.Remove((reserva.RRESMESA.ElementAt(x)));
                    x--;
                }
            }

            return reserva;
        }

        /// <summary>
        /// Função para buscar uma lista de objetos reserva
        /// </summary>
        /// <returns>lista de objetos</returns>
        public IQueryable<RRESERVA> BuscarLista()
        {
            // Busca o reserva
            IQueryable<RRESERVA> reservas = from a in Contexto.Atual.RRESERVA
                                            where (a.idEmpresa == Memoria.Empresa || Memoria.Empresa == 0)
                                                  && (a.idFilial == Memoria.Filial || Memoria.Filial == 0)
                                            select a;


            return reservas;
        }

        /// <summary>
        /// Função para criar novo
        /// </summary>
        /// <param name="obj">objeto reserva</param>
        /// <returns>true ou false</returns>
        public bool Criar(RRESERVA obj)
        {
            if (Memoria.Empresa != 0 && Memoria.Filial != 0)
            {
                obj.idReserva = Contexto.GerarId("RRESERVA");

                // atualiza a empresa e a filial
                obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);
                obj.idFilial = Convert.ToInt32(Memoria.Filial);

                // atualiza data de inclusão e alteração
                obj.dataInclusao = DateTime.Now;
                obj.dataAlteracao = DateTime.Now;

                foreach (RRESMESA rre in obj.RRESMESA)
                {
                    rre.idReserva = obj.idReserva;
                }

                obj.identificador = GerarIdentificador(obj);

                // adiciona reserva
                Contexto.Atual.AddToRRESERVA(obj);

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
        /// <param name="obj">objeto reserva</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(RRESERVA obj)
        {
            // atualiza data de alteração
            obj.dataAlteracao = DateTime.Now;

            contx = new Modelo.Restaurante();

            var reserva_antiga = new RRESERVA();
            reserva_antiga = contx.RRESERVA.SingleOrDefault(a => a.idReserva == obj.idReserva
                                                                 &&
                                                                 (a.idEmpresa == Memoria.Empresa ||
                                                                  Memoria.Empresa == 0)
                                                                 &&
                                                                 (a.idFilial == Memoria.Filial ||
                                                                  Memoria.Filial == 0));


            IEnumerable<RRESMESA> mesas_antigas = from h in reserva_antiga.RRESMESA
                                                  where !obj.RRESMESA.Contains(h)
                                                  select h;

            foreach (RRESMESA mesaAntiga in mesas_antigas)
            {
                //mesaAntiga.GMESA.idStatus = 2;
                contx.SaveChanges();
            }

            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();

            return true;
        }

        /// <summary>
        /// Função que troca o status para inativo
        /// </summary>
        /// <param name="obj">objeto reserva</param>
        /// <returns>true ou false</returns>
        public bool Cancelar(RRESERVA obj)
        {
            obj = BuscarVazio(obj);
            obj.status = 6;
            return Atualizar(obj);
        }

        /// <summary>
        /// Função que troca o status para atendido
        /// </summary>
        /// <param name="obj">objeto reserva</param>
        /// <returns>true ou false</returns>
        public bool Atender(RRESERVA obj)
        {
            obj.status = 8;
            return Atualizar(obj);
        }

        public string GerarIdentificador(RRESERVA obj)
        {
            string identificador;

            identificador = Convert.ToString(obj.idEmpresa) + Convert.ToString(obj.idFilial);

            string id = "";


            if (obj.idReserva < 10)
            {
                id = "000" + Convert.ToString(obj.idReserva);
            }

            else
            {
                if (obj.idReserva < 100)
                {
                    id = "00" + Convert.ToString(obj.idReserva);
                }

                else
                {
                    if (obj.idReserva < 1000)
                    {
                        id = "0" + Convert.ToString(obj.idReserva);
                    }

                    else
                    {
                        id = Convert.ToString(obj.idReserva);
                    }
                }
            }

            return identificador += id;
        }
    }
}