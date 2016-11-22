using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Compras
{
    public class MovimentoDAL
    {
        private List<GSISTEMA> lista = new List<GSISTEMA>();

        /// <summary>
        /// Fun��o para buscar objeto movimento completo
        /// </summary>
        /// <param name="obj">objeto movimento</param>
        /// <param name="tpmov">tipo do movimento</param>
        /// <returns>objeto movimento</returns>
        public CMOVIMENTO Buscar(CMOVIMENTO obj)
        {
            // Busca o usu�rio
            CMOVIMENTO movimento =
                Contexto.Atual.CMOVIMENTO.ToList().SingleOrDefault(
                    a =>
                    a.idEmpresa == Memoria.Empresa &&
                    a.idFilial == Memoria.Filial &&
                    a.idMov == obj.idMov);

            return movimento;
        }

        /// <summary>
        /// Fun��o para buscar uma lista de objetos
        /// </summary>
        /// <returns>lista de objetos</returns>
        public IQueryable<CMOVIMENTO> BuscarLista(List<string> tpmov)
        {
            // Busca a lista
            IQueryable<CMOVIMENTO> lista = (from a in Contexto.Atual.CMOVIMENTO
                                            select a).Where(r => tpmov.Contains(r.codTipoMov)).Distinct();

            return lista;
        }

        /// <summary>
        /// Fun��o para buscar uma lista de objetos
        /// </summary>
        /// <returns>lista de objetos</returns>
        public IQueryable<CMOVIMENTO> BuscarVazio()
        {
            // Busca a lista
            IQueryable<CMOVIMENTO> lista = from a in Contexto.Atual.CMOVIMENTO
                                           select a;

            return lista;
        }

        /// <summary>
        /// Fun��o para buscar uma lista de objetos
        /// </summary>
        /// <returns>lista de objetos</returns>
        public IQueryable<CSTATMOV> BuscarStatus()
        {
            // Busca a lista
            IQueryable<CSTATMOV> lista = from a in Contexto.Atual.CSTATMOV
                                         select a;

            return lista;
        }

        /// <summary>
        /// Fun��o para criar novo
        /// </summary>
        /// <param name="obj">objeto movimento</param>
        /// <returns>true ou false</returns>
        public bool Criar(CMOVIMENTO obj, List<List<string>> compl)
        {
            if (Memoria.Empresa != 0)
            {
                obj.idEmpresa = Memoria.Empresa;
                obj.idFilial = Memoria.Filial;
                obj.dataEmissao = DateTime.Now;
                obj.codUsuario = Memoria.Codusuario;

                if (obj.idMov == 0)
                {
                    obj.idMov = Contexto.GerarId("CMOVIMENTO");
                }

                if (compl != null)
                {
                    RelacionarMovimentos(compl);
                }

                // adiciona usu�rio
                Contexto.Atual.AddToCMOVIMENTO(obj);

                // entrada no estoque, no caso de um movimento do tipo NF (Nota Fiscal)
                if (obj.codTipoMov == "NF")
                {
                    BaixarEstoque(obj);
                }

                // salva altera��es
                Contexto.Atual.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Funcao que retorna o sequencial e a Serie do movimento
        /// </summary>
        /// <param name="codTipoMov">tipo do movimento</param>
        /// <returns>retorna uma lista de string: 0 -> sequencial; 1 -> codTipoMov</returns>
        public List<string> PegarSeqMov(string codTipoMov)
        {
            var SeqMov = new List<string>();

            string seqstr = Convert.ToString(Contexto.GerarId(codTipoMov));

            for (int i = 0; seqstr.Length < 4; i++)
            {
                seqstr = "0" + seqstr;
            }

            SeqMov.Add(seqstr);
            SeqMov.Add(codTipoMov);

            return SeqMov;
        }

        /// <summary>
        /// Funcao que da baixa no estoque
        /// </summary>
        /// <param name="obj">objeto movimento (do tipo NF - Nota Fiscal)</param>
        /// <returns>retorna true ou false</returns>
        public bool BaixarEstoque(CMOVIMENTO obj)
        {
            try
            {
                ECTESTOQUE ultEstoque =
                    (from p in Contexto.Atual.ECTESTOQUE select p).Where(
                        r => r.idEmpresa == Memoria.Empresa
                             && r.idFilial == Memoria.Filial).OrderByDescending(r => r.idCtEstoque).FirstOrDefault();

                IQueryable<CITEMMOV> lista =
                    (from p in Contexto.Atual.CITEMMOV select p).Where(
                        r =>
                        r.idEmpresa == Memoria.Empresa && r.idFilial == Memoria.Filial &&
                        r.idMov == obj.idMov);

                int cont = 1;
                foreach (CITEMMOV item in lista)
                {
                    var novoEstoque = new ECTESTOQUE();
                    novoEstoque.idEmpresa = Memoria.Empresa;
                    novoEstoque.idFilial = Memoria.Filial;
                    novoEstoque.idCtEstoque = Contexto.GerarId("ECTESTOQUE");

                    novoEstoque.idMov = obj.idMov;
                    novoEstoque.sequencialMov = cont;
                    novoEstoque.idProduto = item.idProduto;
                    novoEstoque.dataMovimento = DateTime.Now; //pegar data do movimento
                    novoEstoque.undControle = ultEstoque.undControle;

                    novoEstoque.idLocDest = ultEstoque.idLoc;
                    novoEstoque.idFilialLocDest = ultEstoque.idFilialLoc;

                    novoEstoque.qtdeInicial = ultEstoque.qtdeAtual;
                    novoEstoque.vlrInicial = ultEstoque.vlrTotal;
                    novoEstoque.qtdeEntrada = item.quantidade;
                    novoEstoque.vlrUnitEntrada = item.precoUnitario;

                    novoEstoque.qtdeSaida = 0;
                    novoEstoque.vlrUnitSaida = 0;

                    novoEstoque.qtdeAtual = novoEstoque.qtdeInicial + item.quantidade;
                    novoEstoque.vlrTotal = ultEstoque.vlrTotal + (item.precoUnitario*item.quantidade);

                    novoEstoque.custoMedio = novoEstoque.vlrTotal/novoEstoque.qtdeAtual;

                    EPRODUTO produto =
                        (from p in Contexto.Atual.EPRODUTO select p).Where(
                            r => r.idEmpresa == Memoria.Empresa && r.idProduto == item.idProduto).OrderBy(
                                r => r.idProduto).FirstOrDefault();

                    // Coloca o id do estoque e o id da filial do estoque no produto movimentado
                    produto.idLoc = novoEstoque.idCtEstoque;
                    produto.idFilialLoc = novoEstoque.idFilial;

                    Contexto.Atual.AddToECTESTOQUE(novoEstoque);

                    //Contexto.Atual.SaveChanges();

                    cont++;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Funcao que relaciona um movimento a outro
        /// </summary>
        /// <param name="objold">objeto movimento de origem</param>
        /// <param name="objnew">objeto movimento de destino</param>
        /// <returns>retorna true ou false</returns>
        public bool RelacionarMovimentos(List<List<string>> obj)
        {
            try
            {
                var itemmov = new ItemMovimentoDAL();
                var movrelac = new CMOVRELAC();

                /*
                 * values[0] = idEmpresaOri
                 * values[1] = idFilialOri
                 * values[2] = idMovOri
                 * values[3] = sequencialMovOri
                 * 
                 * values[4] = idEmpresaDes
                 * values[5] = idFilialDes
                 * values[6] = idMovDes
                 * values[7] = sequencialMovDes
                 */

                string idmov = "0";
                int contIdmov = 0;
                int contItem = 0;
                var movimento = new CMOVIMENTO();

                foreach (var items in obj)
                {
                    if (idmov != Convert.ToString(items[2]))
                    {
                        idmov = Convert.ToString(items[2]);

                        if (contIdmov != 0)
                        {
                            if (contIdmov == contItem)
                            {
                                movimento.codStatus = "R";
                            }
                            else
                            {
                                movimento.codStatus = "X";
                            }
                        }

                        movimento = new CMOVIMENTO();

                        movimento.idMov = Convert.ToInt32(idmov);

                        movimento = Buscar(movimento);

                        contIdmov = movimento.CITEMMOV.Count;

                        contItem = 0;

                        movrelac.idMovRelac = Contexto.GerarId("CMOVRELAC");

                        movrelac.idEmpresaOri = Convert.ToInt32(items[0]);
                        movrelac.idFilialOri = Convert.ToInt32(items[1]);
                        movrelac.idMovOri = Convert.ToInt32(items[2]);

                        movrelac.idEmpresaDes = Convert.ToInt32(items[4]);
                        movrelac.idFilialDes = Convert.ToInt32(items[5]);
                        movrelac.idMovDes = Convert.ToInt32(items[6]);

                        Contexto.Atual.AddToCMOVRELAC(movrelac);
                    }

                    var itemmovrelac = new CITEMMOVRELAC();

                    itemmovrelac.idItemMovRelac = Contexto.GerarId("CITEMMOVRELAC");


                    itemmovrelac.idEmpresaOri = Convert.ToInt32(items[0]);
                    itemmovrelac.idFilialOri = Convert.ToInt32(items[1]);
                    itemmovrelac.idMovOri = Convert.ToInt32(items[2]);
                    itemmovrelac.seqItemOri = Convert.ToInt32(items[3]);

                    itemmovrelac.idEmpresaDes = Convert.ToInt32(items[4]);
                    itemmovrelac.idFilialDes = Convert.ToInt32(items[5]);
                    itemmovrelac.idMovDes = Convert.ToInt32(items[6]);
                    itemmovrelac.seqItemDes = Convert.ToInt32(items[7]);

                    CITEMMOV itemold =
                        (from p in Contexto.Atual.CITEMMOV select p).SingleOrDefault(
                            r =>
                            r.idEmpresa == Memoria.Empresa && r.idFilial == Memoria.Filial &&
                            r.idMov == itemmovrelac.idMovOri && r.sequencialMov == itemmovrelac.seqItemOri);

                    decimal qtdRes = itemmov.ValidarQtd(itemold);

                    if (itemold.quantidade == qtdRes)
                    {
                        //itemold.status = false;
                    }

                    Contexto.Atual.AddToCITEMMOVRELAC(itemmovrelac);

                    contItem++;
                }


                if (contIdmov != 0)
                {
                    if (contIdmov == contItem)
                    {
                        movimento.codStatus = "R";
                    }
                    else
                    {
                        movimento.codStatus = "X";
                    }
                }


                //Contexto.Atual.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Funcao para buscar
        /// </summary>
        /// <param name="obj">objeto movimento</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(CMOVIMENTO obj)
        {
            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();
            return true;
        }

        /// <summary>
        /// Fun��o para buscar tipo
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>lista de CTPMOV</returns>
        public IQueryable<CTPMOV> BuscarTipo()
        {
            IQueryable<CTPMOV> lista = (from a in Contexto.Atual.CTPMOV
                                        select a);
            return lista;
        }

        public IQueryable<CITEMMOV> BuscarItem(CMOVIMENTO obj)
        {
            IQueryable<CITEMMOV> lista = (from a in Contexto.Atual.CITEMMOV
                                          where a.idMov == obj.idMov
                                          select a);
            return lista;
        }

        /// <summary>
        /// Fun��o que troca o status para inativo
        /// </summary>
        /// <param name="obj">objeto movimento</param>
        /// <returns>true ou false</returns>
        //public bool Cancelar(CMOVIMENTO obj)
        //{
        //    obj.codStatus = "C";
        //    return Atualiza(obj);
        //}
    }
}