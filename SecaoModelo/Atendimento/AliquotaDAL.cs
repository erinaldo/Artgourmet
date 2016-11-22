using System;
using System.Data;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Atendimento
{
    public class AliquotaDAL
    {
        public bool Criar(AALIQUOTA obj)
        {
            // Atualiza a empresa e filial
            obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);
            obj.idFilial = Convert.ToInt32(Memoria.Filial);
            obj.ativo = true;

            obj.idAliquota = Contexto.GerarId("AALIQUOTA");

            // Adicionar fidelidade
            Contexto.Atual.AddToAALIQUOTA(obj);

            // Salva alterações
            Contexto.Atual.SaveChanges();

            return true;
        }

        public AALIQUOTA Buscar(AALIQUOTA obj)
        {
            AALIQUOTA ali = Contexto.Atual.AALIQUOTA.SingleOrDefault(a => a.idAliquota == obj.idAliquota
                                                                          &&
                                                                          (a.idEmpresa == a.idEmpresa ||
                                                                           a.idEmpresa == Memoria.Empresa));

            return ali;
        }

        public IQueryable<AALIQUOTA> BuscarLista()
        {
            IQueryable<AALIQUOTA> lista = from a in Contexto.Atual.AALIQUOTA
                                          where (a.idEmpresa == Memoria.Empresa
                                                 && a.idFilial == Memoria.Filial)
                                          select a;

            return lista;
        }

        public bool Atualizar(AALIQUOTA obj)
        {
            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();
            return true;
        }

        public bool Excluir(AALIQUOTA obj)
        {
            Contexto.Atual.AALIQUOTA.DeleteObject(obj);
            Contexto.Atual.SaveChanges();
            return true;
        }
    }
}