using System;
using System.Data;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Reserva
{
    public class ParametrosDAL
    {
        public RPARAM Buscar()
        {
            // Busca o reserva
            RPARAM parametro = Contexto.Atual.RPARAM.SingleOrDefault(a =>
                                                                     (a.idEmpresa == Memoria.Empresa ||
                                                                      Memoria.Empresa == 0)
                                                                     &&
                                                                     (a.idFilial == Memoria.Filial ||
                                                                      Memoria.Filial == 0));

            return parametro;
        }

        /// <summary>
        /// Função para criar novo
        /// </summary>
        /// <param name="obj">objeto parametro</param>
        /// <returns>true ou false</returns>
        public bool Criar(RPARAM obj)
        {
            if (Memoria.Empresa != 0 && Memoria.Empresa != 0)
            {
                // atualiza a empresa e a filial
                obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);
                obj.idFilial = Convert.ToInt32(Memoria.Filial);


                // adiciona parametro
                Contexto.Atual.AddToRPARAM(obj);

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
        /// Função para atualizar
        /// </summary>
        /// <param name="obj">objeto parametro</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(RPARAM obj)
        {
            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();
            return true;
        }
    }
}