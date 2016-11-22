using System;
using System.Data;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Caixa
{
    public class PeriodoFiscalDAL
    {
        public bool Criar(APERIODOFISCAL obj)
        {
            obj.idEmpresa = Memoria.Empresa;
            obj.idFilial = Memoria.Filial;

            obj.idPeriodo = Contexto.GerarId("APERIODOFISCAL");
            obj.status = false;

            Contexto.Atual.AddToAPERIODOFISCAL(obj);
            Contexto.Atual.SaveChanges();

            return true;
        }

        public bool Atualizar(APERIODOFISCAL obj)
        {
            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();
            return true;
        }

        public APERIODOFISCAL PegarPeriodoAtivo()
        {
            return Contexto.Atual.APERIODOFISCAL.FirstOrDefault(r => r.status == false);
        }

        public bool FecharPeriodoAtual()
        {
            APERIODOFISCAL obj = PegarPeriodoAtivo();

            obj.dataFim = DateTime.Now;
            obj.status = true;

            Contexto.Atual.SaveChanges();

            return true;
        }
    }
}