using System;
using System.Data;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Global
{
    public class FilialDAL
    {
        public IQueryable<GFILIAL> BuscarLista()
        {
            // Busca a lista de mesas
            IQueryable<GFILIAL> lista = from a in Contexto.Atual.GFILIAL
                                        where a.idEmpresa == Memoria.Empresa
                                              && a.idFilial != 0
                                        select a;

            return lista;
        }

        public bool Criar(GFILIAL obj)
        {
            obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);

            if (obj.idFilial == 0)
                obj.idFilial = Contexto.GerarId("GFILIAL");

            // Adicionar fornecedor
            Contexto.Atual.AddToGFILIAL(obj);

            IQueryable<GINCREMENTO> listaIC = from a in Contexto.Atual.GINCREMENTO
                                              where a.idEmpresa == Memoria.Empresa
                                                    && a.idFilial == Memoria.Filial
                                              select a;

            foreach (GINCREMENTO inc in listaIC)
            {
                var inc2 = new GINCREMENTO();
                inc2.idEmpresa = inc.idEmpresa;
                inc2.idFilial = obj.idFilial;
                inc2.tabela = inc.tabela;
                inc2.valor = 1;
                Contexto.Atual.AddToGINCREMENTO(inc2);
            }

            IQueryable<GUSRFILMOD> listaPermissao = from g in Contexto.Atual.GUSRFILMOD
                                                    where g.idEmpresa == Memoria.Empresa
                                                          && g.idFilial == Memoria.Filial
                                                          && g.codUsuario == Memoria.Codusuario
                                                    select g;

            foreach (GUSRFILMOD per in listaPermissao)
            {
                var permissao = new GUSRFILMOD();
                permissao.codSistema = per.codSistema;
                permissao.codUsuario = per.codUsuario;
                permissao.idEmpresa = per.idEmpresa;
                permissao.idFilial = obj.idFilial;
                permissao.idPerfil = per.idPerfil;
                permissao.supervisor = per.supervisor;

                Contexto.Atual.GUSRFILMOD.AddObject(permissao);
            }

            Contexto.Atual.SaveChanges();
            return true;
        }

        public IQueryable<GFILIAL> BuscarListaEspecifica(GFILIAL obj)
        {
            // Busca a lista de mesas
            IQueryable<GFILIAL> lista = from a in Contexto.Atual.GFILIAL
                                        where (a.idFilial == obj.idFilial || obj.idFilial == 0)
                                              && (a.idEmpresa == Memoria.Empresa || a.idEmpresa == obj.idEmpresa)
                                              && a.idFilial != 0
                                        select a;

            return lista;
        }

        public bool Atualizar(GFILIAL obj)
        {
            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();
            return true;
        }

        public GFILIAL Buscar(GFILIAL obj)
        {
            GFILIAL fornecedor =
                Contexto.Atual.GFILIAL.SingleOrDefault(r => r.idFilial == obj.idFilial && r.idEmpresa == Memoria.Empresa);
            return fornecedor;
        }
    }
}