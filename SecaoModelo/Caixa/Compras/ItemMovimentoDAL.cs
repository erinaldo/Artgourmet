using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Compras
{
    public class ItemMovimentoDAL
    {
        public CITEMMOV Buscar(CITEMMOV obj)
        {
            CITEMMOV item = Contexto.Atual.CITEMMOV.SingleOrDefault(a => obj.idEmpresa == a.idEmpresa
                                                                    && obj.idFilial == a.idFilial
                                                                    && obj.idMov == a.idMov
                                                                    && obj.sequencialMov == a.sequencialMov);
            
            return item;

        }
    }
}
