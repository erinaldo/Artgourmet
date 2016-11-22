using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Global
{
    public class MesaDAL
    {
        /// <summary>
        /// Função para buscar objeto mesa completo
        /// </summary>
        /// <param name="obj">objeto mesa</param>
        /// <returns>objeto mesa</returns>
        public GMESA Buscar(GMESA obj)
        {
            // Busca o mesa
            GMESA mesa = Contexto.Atual.GMESA.SingleOrDefault(a => a.nuMesa == obj.nuMesa
                                                                   &&
                                                                   (a.idEmpresa == Memoria.Empresa ||
                                                                    Memoria.Empresa == 0)
                                                                   &&
                                                                   (a.idFilial == Memoria.Filial ||
                                                                    Memoria.Filial == 0));

            Contexto.Atual.Refresh(RefreshMode.StoreWins, mesa);

            return mesa;
        }

        /// <summary>
        /// Função para buscar objeto mesa completo
        /// </summary>
        /// <param name="obj">objeto mesa</param>
        /// <returns>objeto mesa</returns>
        public GMESA Buscar(int nuMesa)
        {
            // Busca o mesa
            GMESA mesa = Contexto.Atual.GMESA.SingleOrDefault(a => a.nuMesa == nuMesa
                                                                   &&
                                                                   (a.idEmpresa == Memoria.Empresa ||
                                                                    Memoria.Empresa == 0)
                                                                   &&
                                                                   (a.idFilial == Memoria.Filial ||
                                                                    Memoria.Filial == 0));

            Contexto.Atual.Refresh(RefreshMode.StoreWins, mesa);

            return mesa;
        }


        /// <summary>
        /// Função para buscar uma lista de objetos mesa
        /// </summary>
        /// <returns>lista de objetos</returns>
        public IQueryable<GMESA> BuscarLista()
        {
            // Busca o mesa
            IQueryable<GMESA> mesas = from a in Contexto.Atual.GMESA
                                      where (a.idEmpresa == Memoria.Empresa) && (a.idFilial == Memoria.Filial)
                                      orderby a.nuMesa
                                      select a;

            return mesas;
        }

        /// <summary>
        /// Função para buscar uma lista de objetos mesa com parametros
        /// </summary>
        /// <returns>lista de objetos</returns>
        public IQueryable<GMESA> BuscarListaEspecifica(GMESA obj)
        {
            // Busca o mesa
            IQueryable<GMESA> mesas = from a in Contexto.Atual.GMESA
                                      where (a.idEmpresa == Memoria.Empresa || Memoria.Empresa == 0)
                                            && (a.idFilial == Memoria.Filial || Memoria.Filial == 0)
                                            //&& (a.idStatus == obj.idStatus || obj.idStatus == null)
                                            && (a.qtdLugares == obj.qtdLugares || obj.qtdLugares == null)
                                            && (a.observacao == obj.observacao || obj.observacao == null)
                                            && (a.dataAlteracao == obj.dataAlteracao || obj.dataAlteracao == null)
                                            && (a.dataAlteracao == obj.dataAlteracao || obj.dataAlteracao == null)
                                      orderby a.nuMesa
                                      select a;

            return mesas;
        }

        /// <summary>
        /// Função para buscar uma lista de objetos mesa com parametros por reserva
        /// </summary>
        /// <returns>lista de objetos</returns>
        public IQueryable<GMESA> BuscarListaPorReserva(RRESERVA obj2)
        {
            // Busca o mesa
            IQueryable<GMESA> mesa = from p in Contexto.Atual.RRESMESA
                                     from h in Contexto.Atual.RRESERVA
                                     where p.idEmpresa == obj2.idEmpresa &&
                                           p.idFilial == obj2.idFilial
                                           && h.RRESMESA.Contains(p)
                                           && h.idReserva == obj2.idReserva
                                     orderby p.nuMesa
                                     select p.GMESA;

            return mesa;
        }

        /// <summary>
        /// Função para criar novo
        /// </summary>
        /// <param name="obj">objeto mesa</param>
        /// <returns>true ou false</returns>
        public bool Criar(GMESA obj)
        {
            if (Memoria.Empresa != 0 && Memoria.Empresa != 0)
            {
                // atualiza a empresa e a filial
                obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);
                obj.idFilial = Convert.ToInt32(Memoria.Filial);
                obj.ativo = true;

                foreach (GIMGMESA im in obj.GIMGMESA)
                {
                    if (im.idEmpresa == 0)
                        im.idEmpresa = Memoria.Empresa;

                    if (im.idFilial == 0)
                        im.idFilial = Memoria.Filial;

                    if (im.GIMAGEM.idImagem == 0)
                    {
                        im.GIMAGEM.idImagem = Contexto.GerarId("GIMAGEM");
                        im.idImagem = im.GIMAGEM.idImagem;
                    }
                }

                // atualiza data de inclusão e alteração
                obj.dataInclusao = DateTime.Now;
                obj.dataAlteracao = DateTime.Now;

                // adiciona mesa
                Contexto.Atual.AddToGMESA(obj);

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
        /// <param name="obj">objeto mesa</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(GMESA obj)
        {
            foreach (GIMGMESA im in obj.GIMGMESA)
            {
                if (im.idEmpresa == 0)
                    im.idEmpresa = Memoria.Empresa;

                if (im.idFilial == 0)
                    im.idFilial = Memoria.Filial;

                if (im.GIMAGEM.idImagem == 0)
                {
                    im.GIMAGEM.idImagem = Contexto.GerarId("GIMAGEM");
                    im.idImagem = im.GIMAGEM.idImagem;
                }

                if (im.nuMesa == 0)
                    im.nuMesa = obj.nuMesa;
            }

            // atualiza data de alteração
            obj.dataAlteracao = DateTime.Now;

            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();
            return true;
        }

        public bool AtualizarStatus(GMESA obj)
        {
            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();
            return true;
        }

        public GMATRIZMESA BuscarMatrizAtiva()
        {
            return Contexto.Atual.GMATRIZMESA.Where(r => r.ativo.Value).First();
        }

        /// <summary>
        /// Função que verifica se existe conta (aberta ou bloqueada) para a mesa
        /// </summary>
        /// <param name="obj">Objeto GMESA</param>
        /// <returns>TRUE, se existir conta aberta ou bloqueada, FALSE, se não existir conta aberta ou bloqueada</returns>
        public bool VerificarConta(GMESA obj)
        {
            int count =
                (from p in Contexto.Atual.ACONTA select p).Where(
                    r => r.nuMesa == obj.nuMesa && (r.idStatus == 1 || r.idStatus == 3)).Count();

            if (count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}