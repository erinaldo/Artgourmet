using System;
using System.Data;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Estoque
{
    public class CardapioDAL
    {
        /// <summary>
        /// Função para buscar objeto completo
        /// </summary>
        /// <param name="obj">objeto</param>
        /// <returns>objeto</returns>
        public ECARDAPIO Buscar(ECARDAPIO obj)
        {
            if (Memoria.Empresa != 0 && Memoria.Empresa != 0)
            {
                // Busca
                ECARDAPIO obj2 = Contexto.Atual.ECARDAPIO.SingleOrDefault(a => a.idCardapio == obj.idCardapio
                                                                               &&
                                                                               (a.idEmpresa == Memoria.Empresa ||
                                                                                Memoria.Empresa == 0)
                                                                               &&
                                                                               (a.idFilial == Memoria.Filial ||
                                                                                Memoria.Filial == 0)
                    );

                return obj2;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Função para buscar uma lista de objetos
        /// </summary>
        /// <returns>lista de objetos</returns>
        public IQueryable<ECARDAPIO> BuscarLista()
        {
            // Busca a lista
            IQueryable<ECARDAPIO> lista = from a in Contexto.Atual.ECARDAPIO
                                          where (a.idEmpresa == Memoria.Empresa || Memoria.Empresa == 0)
                                                && (a.idFilial == Memoria.Filial || Memoria.Filial == 0)
                                          select a;

            return lista;
        }

        /// <summary>
        /// Função para criar novo
        /// </summary>
        /// <param name="obj">objeto</param>
        /// <returns>true ou false</returns>
        public bool Criar(ECARDAPIO obj)
        {
            if (Memoria.Empresa != 0 && Memoria.Filial != 0)
            {
                // atualiza a empresa e a filial
                obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);
                obj.idFilial = Convert.ToInt32(Memoria.Filial);


                if (obj.idCardapio == 0)
                {
                    obj.idCardapio = Contexto.GerarId("ECARDAPIO");
                }

                int cont = 1;
                foreach (ECARDAPIOITEM item in obj.ECARDAPIOITEM)
                {
                    item.idEmpresa = obj.idEmpresa;
                    item.idFilial = obj.idFilial;
                    item.idCardapio = obj.idCardapio;
                    item.idItemCard = cont;

                    cont++;
                }


                // adiciona
                Contexto.Atual.AddToECARDAPIO(obj);

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
        /// <param name="obj">objeto</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(ECARDAPIO obj)
        {
            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();
            return true;
        }

        /// <summary>
        /// Função que troca o status para inativo
        /// </summary>
        /// <param name="obj">objeto</param>
        /// <returns>true ou false</returns>
        public bool Cancelar(ECARDAPIO obj)
        {
            obj.ativo = false;
            return Atualizar(obj);
        }


        /// <summary>
        /// Busca cardapio atual
        /// </summary>
        /// <returns>id do cardapio atual</returns>
        public int BuscarAtualId()
        {
            bool? _segunda = null;
            bool? _terca = null;
            bool? _quarta = null;
            bool? _quinta = null;
            bool? _sexta = null;
            bool? _sabado = null;
            bool? _domingo = null;

            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    _segunda = true;
                    break;

                case DayOfWeek.Tuesday:
                    _terca = true;
                    break;

                case DayOfWeek.Wednesday:
                    _quarta = true;
                    break;

                case DayOfWeek.Thursday:
                    _quinta = true;
                    break;

                case DayOfWeek.Friday:
                    _sexta = true;
                    break;

                case DayOfWeek.Saturday:
                    _sabado = true;
                    break;

                case DayOfWeek.Sunday:
                    _domingo = true;
                    break;
            }

            DateTime diaatual = Convert.ToDateTime("01/01/2000 " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":00");

            ECARDAPIO cardapio = Contexto.Atual.ECARDAPIO.FirstOrDefault(c => c.ativo == true
                                                                              && (
                                                                                     (c.segunda == _segunda)
                                                                                     ||
                                                                                     (c.terca == _terca)
                                                                                     ||
                                                                                     (c.quarta == _quarta)
                                                                                     ||
                                                                                     (c.quinta == _quinta)
                                                                                     ||
                                                                                     (c.sexta == _sexta)
                                                                                     ||
                                                                                     (c.sabado == _sabado)
                                                                                     ||
                                                                                     (c.domingo == _domingo)
                                                                                 )
                                                                              && (
                                                                                     c.diaTodo == true
                                                                                     ||
                                                                                     (
                                                                                         c.horInicio <= diaatual &&
                                                                                         c.horFinal >= diaatual
                                                                                     )
                                                                                 )
                );

            return cardapio.idCardapio;
        }

        public ECARDAPIO BuscarAtual()
        {
            bool? _segunda = null;
            bool? _terca = null;
            bool? _quarta = null;
            bool? _quinta = null;
            bool? _sexta = null;
            bool? _sabado = null;
            bool? _domingo = null;

            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    _segunda = true;
                    break;

                case DayOfWeek.Tuesday:
                    _terca = true;
                    break;

                case DayOfWeek.Wednesday:
                    _quarta = true;
                    break;

                case DayOfWeek.Thursday:
                    _quinta = true;
                    break;

                case DayOfWeek.Friday:
                    _sexta = true;
                    break;

                case DayOfWeek.Saturday:
                    _sabado = true;
                    break;

                case DayOfWeek.Sunday:
                    _domingo = true;
                    break;
            }

            DateTime diaatual = Convert.ToDateTime("01/01/2000 " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":00");

            ECARDAPIO cardapio = Contexto.Atual.ECARDAPIO.FirstOrDefault(c => c.ativo == true
                                                                              && (
                                                                                     (c.segunda == _segunda)
                                                                                     ||
                                                                                     (c.terca == _terca)
                                                                                     ||
                                                                                     (c.quarta == _quarta)
                                                                                     ||
                                                                                     (c.quinta == _quinta)
                                                                                     ||
                                                                                     (c.sexta == _sexta)
                                                                                     ||
                                                                                     (c.sabado == _sabado)
                                                                                     ||
                                                                                     (c.domingo == _domingo)
                                                                                 )
                                                                              && (
                                                                                     c.diaTodo == true
                                                                                     ||
                                                                                     (
                                                                                         c.horInicio <= diaatual &&
                                                                                         c.horFinal >= diaatual
                                                                                     )
                                                                                 )
                );

            return cardapio;
        }

        public bool Excluir(ECARDAPIO obj)
        {
            Contexto.Atual.ECARDAPIO.DeleteObject(obj);
            Contexto.Atual.SaveChanges();
            return true;
        }
    }
}