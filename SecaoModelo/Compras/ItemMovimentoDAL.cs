using System;
using System.Linq;
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

        /// <summary>
        /// Função que valida a quantidade que pode ser enviada de um produto de um movimento para outro
        /// </summary>
        /// <param name="itemmov">objeto do item do movimento</param>
        /// <returns>retorna o valor da quantidade restante</returns>
        public decimal ValidarQtd(CITEMMOV itemmov)
        {
            // retorna o relacionamento do item em questão
            IQueryable<CITEMMOVRELAC> itemmovrelac =
                (from p in Contexto.Atual.CITEMMOVRELAC select p).Where(
                    r =>
                    r.idEmpresaOri == itemmov.idEmpresa && r.idFilialOri == itemmov.idFilial &&
                    r.idMovOri == itemmov.idFilial && r.seqItemOri == itemmov.sequencialMov);

            decimal somaqtd = 0;
            // retorna os dados do item relacionado ao item em questão
            foreach (CITEMMOVRELAC item in itemmovrelac)
            {
                CITEMMOV itemmovDes =
                    (from p in Contexto.Atual.CITEMMOV select p).FirstOrDefault(
                        r =>
                        r.idEmpresa == item.idEmpresaDes && r.idFilial == item.idFilialDes &&
                        r.idMov == item.idMovDes && r.sequencialMov == item.seqItemDes);

                somaqtd = somaqtd + Convert.ToDecimal(itemmovDes.quantidade);
            }

            // calcula a quantidade que pode ser utilizada para gerar um novo movimento
            decimal qtdrestante;
            qtdrestante = Convert.ToDecimal(itemmov.quantidade) - somaqtd;

            return qtdrestante;
        }

        /// <summary>
        /// Função que verifica se o usuário está digitando uma quantidade a receber válida, verificando a quantidade que já foi enviada para o próximo movimento
        /// </summary>
        /// <param name="itemmov">objeto item do movimento</param>
        /// <param name="qtdReceber">valor da quantidade a receber digitada pelo usuário</param>
        /// <returns>retorna TRUE, se o valor for válido, ou FALSE caso contrario</returns>
        public bool ValidarQtdReceber(CITEMMOV itemmov, decimal qtdReceber)
        {
            // retorna o relacionamento do item em questão
            IQueryable<CITEMMOVRELAC> itemmovrelac =
                (from p in Contexto.Atual.CITEMMOVRELAC select p).Where(
                    r =>
                    r.idEmpresaOri == itemmov.idEmpresa && r.idFilialOri == itemmov.idFilial &&
                    r.idMovOri == itemmov.idFilial && r.seqItemOri == itemmov.sequencialMov);

            decimal somaqtd = 0;
            // retorna os dados do item relacionado ao item em questão
            foreach (CITEMMOVRELAC item in itemmovrelac)
            {
                CITEMMOV itemmovDes =
                    (from p in Contexto.Atual.CITEMMOV select p).FirstOrDefault(
                        r =>
                        r.idEmpresa == item.idEmpresaDes && r.idFilial == item.idFilialDes &&
                        r.idMov == item.idMovDes && r.sequencialMov == item.seqItemDes);

                somaqtd = somaqtd + Convert.ToDecimal(itemmovDes.quantidade);
            }

            // calcula a quantidade que pode ser utilizada para gerar um novo movimento
            decimal qtdrestante;
            qtdrestante = Convert.ToDecimal(itemmov.quantidade) - somaqtd;

            // o usuário pode digitar até a quantidade que resto do produto
            if (qtdReceber <= qtdrestante)
            {
                return true;
            }

            return false;
        }
    }
}