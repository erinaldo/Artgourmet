using System.Data;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Caixa
{
    public class FormaPagtoDAL
    {
        public bool Criar(AFORMAPGTO obj)
        {
            obj.idEmpresa = Memoria.Empresa;

            int filial = Memoria.Filial;
            Memoria.Filial = 0;

            obj.idFormaPGTO = Contexto.GerarId("AFORMAPGTO");
            Memoria.Filial = filial;

            Contexto.Atual.AddToAFORMAPGTO(obj);
            Contexto.Atual.SaveChanges();

            return true;
        }

        public bool Atualizar(AFORMAPGTO obj)
        {
            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();
            return true;
        }
    }
}