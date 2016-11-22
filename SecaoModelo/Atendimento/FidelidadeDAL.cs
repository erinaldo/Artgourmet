using System;
using System.Data;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Atendimento
{
    public class FidelidadeDAL
    {
        public bool Criar(AFIDELIDADE obj)
        {
            // Atualiza a empresa e filial
            obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);
            obj.idFilial = Convert.ToInt32(Memoria.Filial);

            obj.usrCriacao = Memoria.Codusuario;
            obj.usrAlteracao = Memoria.Codusuario;

            obj.dataCriacao = DateTime.Now;
            obj.dataAlteracao = DateTime.Now;

            obj.idFidelidade = Contexto.GerarId("AFIDELIDADE");

            // Adicionar fidelidade
            Contexto.Atual.AddToAFIDELIDADE(obj);

            // Salva alterações
            Contexto.Atual.SaveChanges();

            return true;
        }


        public IQueryable<AFIDELIDADE> BuscarLista()
        {
            // Busca a lista
            IQueryable<AFIDELIDADE> lista = from a in Contexto.Atual.AFIDELIDADE
                                            where (a.idEmpresa == Memoria.Empresa || Memoria.Empresa == 0)
                                                  && (a.idFilial == Memoria.Filial || Memoria.Filial == 0)
                                            select a;

            return lista;
        }


        public AFIDELIDADE Buscar(AFIDELIDADE obj)
        {
            AFIDELIDADE mon = Contexto.Atual.AFIDELIDADE.SingleOrDefault(a => a.idFidelidade == obj.idFidelidade
                                                                              && (a.idEmpresa == Memoria.Empresa ||
                                                                                  a.idEmpresa == 0)
                                                                              && (a.idFilial == Memoria.Filial ||
                                                                                  a.idFilial == 0));
            return mon;
        }


        public bool Atualizar(AFIDELIDADE obj)
        {
            obj.usrAlteracao = Memoria.Codusuario;
            obj.dataAlteracao = DateTime.Now;

            //Atualiza o fidelidade
            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();
            return true;
        }


        public IQueryable<APRDFIDELIDADE> BuscarProduto(AFIDELIDADE obj)
        {
            IQueryable<APRDFIDELIDADE> lista =
                Contexto.Atual.APRDFIDELIDADE.Where(a => a.idFidelidade == obj.idFidelidade
                                                         && (a.idFilial == Memoria.Filial || Memoria.Filial == 0)
                                                         && (a.idEmpresa == Memoria.Empresa || Memoria.Empresa == 0)
                    );

            return lista;
        }


        public IQueryable<AFIDELIDADE> BuscarListaEspecifica(AFIDELIDADE obj)
        {
            // Busca a lista
            IQueryable<AFIDELIDADE> lista = from a in Contexto.Atual.AFIDELIDADE
                                            where (a.idEmpresa == Memoria.Empresa || Memoria.Empresa == 0)
                                                  && (a.idFilial == Memoria.Filial || Memoria.Filial == 0)
                                                  && (a.ativo == true)
                                                  && (a.tipo == obj.tipo)
                                            select a;

            return lista;
        }
    }
}