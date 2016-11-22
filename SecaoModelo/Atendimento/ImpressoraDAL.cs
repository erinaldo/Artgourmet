using System;
using System.Data;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Atendimento
{
    public class ImpressoraDAL
    {
        /// <summary>
        /// Cria uma nova impressora
        /// </summary>
        /// <param name="obj">Objeto AIMPRESSORA</param>
        /// <returns>objeto AIMPRESSORA</returns>
        public bool Criar(AIMPRESSORA obj)
        {
            if (Memoria.Empresa != 0 && Memoria.Filial != 0)
            {
                // Adicionando os dados da empresa e filial
                obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);
                obj.idFilial = Convert.ToInt32(Memoria.Filial);
                obj.ativo = true;

                obj.idImpressora = Contexto.GerarId("AIMPRESSORA");

                //Alteara o valor da idimpressora de 0 para o novo id gerado pelo "geraId"
                //foreach (GMESA m in obj.GMESA)
                //{
                //    m.idImpressora = obj.idImpressora;
                //}

                //Adicionando usuario
                Contexto.Atual.AddToAIMPRESSORA(obj);

                //Salvando alterações
                Contexto.Atual.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Bucar impressora
        /// </summary>
        /// <param name="obj">objeto AIMPRESSORA</param>
        /// <returns>objeto AIMPRESSORA</returns>
        public object Buscar(AIMPRESSORA obj)
        {
            AIMPRESSORA impressora = Contexto.Atual.AIMPRESSORA.SingleOrDefault(a => a.idImpressora == obj.idImpressora
                                                                                     &&
                                                                                     (a.idEmpresa == Memoria.Empresa ||
                                                                                      Memoria.Empresa == 0)
                                                                                     &&
                                                                                     (a.idFilial == Memoria.Filial ||
                                                                                      Memoria.Filial == 0)
                );
            return impressora;
        }


        /// <summary>
        /// Atualizar impressora
        /// </summary>
        /// <param name="obj">objeto AIMPRESSORA</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(AIMPRESSORA obj)
        {
            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();
            return true;
        }


        /// <summary>
        /// Desativar impressora
        /// </summary>
        /// <param name="obj">objeto AIMPRESSORA</param>
        /// <returns>true ou false</returns>
        public bool Desativar(AIMPRESSORA obj)
        {
            obj.ativo = false;
            return Atualizar(obj);
        }

        /// <summary>
        /// Buscar lista de impressoras
        /// </summary>
        /// <returns>Lista de impressoras</returns>
        public IQueryable<AIMPRESSORA> BuscarLista()
        {
            // Busca a lista
            IQueryable<AIMPRESSORA> lista = from a in Contexto.Atual.AIMPRESSORA
                                            where (a.idEmpresa == Memoria.Empresa || Memoria.Empresa == 0)
                                                  && (a.idFilial == Memoria.Filial || Memoria.Filial == 0)
                                            select a;

            return lista;
        }
    }
}