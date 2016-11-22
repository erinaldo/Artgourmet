using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Estoque
{
    public class GrupoDAL
    {
        public IQueryable<EGRUPO> BuscarLista()
        {
            // Busca a lista
            IQueryable<EGRUPO> lista = from a in Contexto.Atual.EGRUPO
                                       where a.idEmpresa == Memoria.Empresa
                                       select a;
            return lista;
        }

        public bool Criar(EGRUPO obj)
        {
            obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);

            if (obj.idGrupo == 0)
            {
                obj.idGrupo = Contexto.GerarId("EGRUPO");
            }

            obj.dataCriacao = DateTime.Now;
            obj.dataAlteracao = DateTime.Now;

            if (obj.idGrupoPai == 0)
                obj.idGrupoPai = null;

            // Adicionar
            Contexto.Atual.AddToEGRUPO(obj);
            Contexto.Atual.SaveChanges();
            return true;
        }

        public IQueryable<EGRUPO> BuscarListaEspecifica(EGRUPO obj)
        {
            // Busca a lista de mesas
            IQueryable<EGRUPO> lista = from a in Contexto.Atual.EGRUPO
                                       where (a.idGrupo == obj.idGrupo || obj.idGrupo == 0)
                                             && (a.idGrupoPai == obj.idGrupoPai || obj.idGrupoPai == null)
                                       select a;

            return lista;
        }

        public bool Atualizar(EGRUPO obj)
        {
            obj.dataAlteracao = DateTime.Now;

            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();
            return true;
        }

        public EGRUPO Buscar(EGRUPO obj)
        {
            EGRUPO result =
                Contexto.Atual.EGRUPO.FirstOrDefault(
                    r => r.idGrupo == obj.idGrupo && (r.idEmpresa == Memoria.Empresa || Memoria.Empresa == 0));
            return result;
        }

        public EGRUPO Buscar(int idGrupo)
        {
            EGRUPO result =
                Contexto.Atual.EGRUPO.FirstOrDefault(
                    r => r.idGrupo == idGrupo && (r.idEmpresa == Memoria.Empresa || Memoria.Empresa == 0));
            return result;
        }

        public bool Excluir(EGRUPO obj)
        {
            bool resultado = VerificarFilho(obj);

            if (resultado)
            {
                //pode excluir
                Contexto.Atual.DeleteObject(obj);
                Contexto.Atual.SaveChanges();
            }
            else
            {
                //não pode excluir
                Memoria.MsgGlobal = "O Item não pode ser excluído, pois há produtos associados.";
                return false;
            }

            return true;
        }

        private bool VerificarFilho(EGRUPO obj)
        {
            bool resultado = (from a in Contexto.Atual.EPRODUTO
                              where ((a.grupo == obj.idGrupo)
                                     && (a.idEmpresa == Memoria.Empresa))
                              select a).Any();

            if (resultado)
            {
                return false;
            }
            else
            {
                bool valor = true;

                IQueryable<EGRUPO> lista = (from p in Contexto.Atual.EGRUPO
                                            where p.idGrupoPai == obj.idGrupo
                                            select p);

                foreach (EGRUPO egrupo in lista)
                {
                    valor = VerificarFilho(egrupo);

                    if (!valor)
                        break;
                }

                return valor;
            }
        }

        public List<EOBSERVACAO> BuscarAtual(EGRUPO obj)
        {
            var lista = new List<EOBSERVACAO>();

            if (obj != null)
            {
                lista.AddRange(obj.EOBSERVACAO);

                bool aux = true;

                if (obj.idGrupoPai != null)
                {
                    var pai = new EGRUPO {idGrupo = Convert.ToInt32(obj.idGrupoPai)};

                    pai = Buscar(pai.idGrupo);

                    while (aux)
                    {
                        lista.AddRange(pai.EOBSERVACAO);

                        if (pai.idGrupoPai != null)
                            {
                                int idpai = Convert.ToInt32(pai.idGrupoPai);
                                pai = new EGRUPO {idGrupo = idpai};
                            }
                            else
                            {
                                aux = false;
                            }
                    }
                }
            }

            return lista;
        }
    }
}