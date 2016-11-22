using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Artebit.Restaurante.Global.AcessoDados.Global;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Compras
{
    public class MovimentoDAL
    {
        List<GSISTEMA> lista = new List<GSISTEMA>();

        /// <summary>
        /// Função para buscar objeto movimento completo
        /// </summary>
        /// <param name="obj">objeto movimento</param>
        /// <param name="tpmov">tipo do movimento</param>
        /// <returns>objeto movimento</returns>
        public CMOVIMENTO Busca(CMOVIMENTO obj)
        {
            // Busca o usuário
            CMOVIMENTO movimento =
                Contexto.Atual.CMOVIMENTO.ToList().SingleOrDefault(
                    a =>
                    a.idEmpresa == Memoria.Empresa.Value && 
                    a.idFilial == Memoria.Filial.Value && 
                    a.idMov == obj.idMov);

            return movimento;
        }

        /// <summary>
        /// Função para buscar uma lista de objetos
        /// </summary>
        /// <returns>lista de objetos</returns>
        public IQueryable<CMOVIMENTO> BuscaLista(string tpmov)
        {
            // Busca a lista
            IQueryable<CMOVIMENTO> lista = (from a in Contexto.Atual.CMOVIMENTO
                                            select a).Where(r => r.codTipoMov == tpmov).Distinct();

            return lista;
        }

        /// <summary>
        /// Função para buscar uma lista de objetos
        /// </summary>
        /// <returns>lista de objetos</returns>
        public IQueryable<CMOVIMENTO> BuscaVazio()
        {
            // Busca a lista
            IQueryable<CMOVIMENTO> lista = from a in Contexto.Atual.CMOVIMENTO
                                           select a;

            return lista;
        }

        /// <summary>
        /// Função para buscar uma lista de objetos
        /// </summary>
        /// <returns>lista de objetos</returns>
        public IQueryable<CSTATMOV> BuscaStatus()
        {
            // Busca a lista
            IQueryable<CSTATMOV> lista = from a in Contexto.Atual.CSTATMOV
                                           select a;

            return lista;
        }

        /// <summary>
        /// Função para criar novo
        /// </summary>
        /// <param name="obj">objeto movimento</param>
        /// <returns>true ou false</returns>
        public bool Cria(CMOVIMENTO obj, List<List<string>> compl)
        {
            if (Memoria.Empresa != 0)
            {

                obj.idEmpresa = Memoria.Empresa.Value;
                obj.idFilial = Memoria.Filial.Value;
                obj.dataEmissao = DateTime.Now;
                obj.codUsuario = Memoria.Codusuario;

                if (obj.idMov == null || obj.idMov == 0)
                {
                    SistemaDAL sis = new SistemaDAL();
                    obj.idMov = sis.geraId("CMOVIMENTO");
                }

                // entrada no estoque, no caso de um movimento do tipo NF (Nota Fiscal)
                if (obj.codTipoMov == "NF")
                {
                    EntradaEstoque(obj);
                }

                if (compl != null)
                {
                    RelacionaMov(compl);
                }

                // adiciona usuário
                Contexto.Atual.AddToCMOVIMENTO(obj);

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
        /// Função que retorna o sequencial e a Série do movimento
        /// </summary>
        /// <param name="codTipoMov">tipo do movimento</param>
        /// <returns>retorna uma lista de string: 0 -> sequencial; 1 -> codTipoMov</returns>
        public List<string> RetornaSeqMov(string codTipoMov)
        {
            SistemaDAL sis = new SistemaDAL();
            List<string> SeqMov = new List<string>();

            string seqstr = Convert.ToString(sis.geraId(codTipoMov));

            for (int i = 0; seqstr.Length < 4; i++ )
            {
                seqstr = "0" + seqstr;
            }

            SeqMov.Add(seqstr);
            SeqMov.Add(codTipoMov);

            return SeqMov;
        }

        /// <summary>
        /// Função que da baixa no estoque
        /// </summary>
        /// <param name="obj">objeto movimento (do tipo NF - Nota Fiscal)</param>
        /// <returns>retorna true ou false</returns>
        public bool EntradaEstoque(CMOVIMENTO obj)
        {
            try
            {
                if (obj.codTipoMov == "NF")
                {
                    SistemaDAL sis = new SistemaDAL();

                    var lista = from p in obj.CITEMMOV
                                join e in Contexto.Atual.EPRODUTO on p.idProduto equals e.idProduto
                                select new
                                           {
                                               produto = p.idProduto,
                                               quantidade = p.quantidade,
                                               preco = p.precoUnitario,
                                               desconto = p.valorDesconto,
                                               idMov = p.idMov,
                                               sequencial = p.sequencialMov
                                           };

                    int cont = 1;
                    foreach (var item in lista)
                    {
                        IQueryable<EPRODUTO> prod =
                            (from a in Contexto.Atual.EPRODUTO select a).Where(r => r.idProduto == item.produto);

                        IQueryable<ECTESTOQUE> est =
                            (from a in Contexto.Atual.ECTESTOQUE select a).Where(
                                r =>
                                r.idEmpresa == Memoria.Empresa.Value && r.idFilial == Memoria.Filial.Value &&
                                r.idProduto == item.produto);

                        ECTESTOQUE ultEstoque = est.FirstOrDefault();
                        if (ultEstoque == null)
                        {
                            ultEstoque = new ECTESTOQUE();
                            ultEstoque.idEmpresa = Memoria.Empresa.Value;
                            ultEstoque.idFilial = Memoria.Filial.Value;
                            ultEstoque.undControle = prod.FirstOrDefault().undControle;
                            ultEstoque.qtdeAtual = 0;
                            ultEstoque.custoMedio = 0;
                            ultEstoque.vlrTotal = 0;
                        }

                        ECTESTOQUE novoEstoque = new ECTESTOQUE();
                        novoEstoque.idEmpresa = ultEstoque.idEmpresa;
                        novoEstoque.idFilial = ultEstoque.idFilial;
                        novoEstoque.idCtEstoque = sis.geraId("ECTESTOQUE");

                        novoEstoque.idMov = obj.idMov;
                        novoEstoque.sequencialMov = cont;
                        novoEstoque.idProduto = prod.FirstOrDefault().idProduto;
                        novoEstoque.dataMovimento = DateTime.Now; //pegar data do movimento
                        novoEstoque.undControle = ultEstoque.undControle;

                        novoEstoque.qtdeInicial = ultEstoque.qtdeAtual;
                        novoEstoque.qtdeEntrada = item.quantidade;
                        novoEstoque.vlrUnitEntrada = item.preco;

                        novoEstoque.qtdeSaida = 0;
                        novoEstoque.vlrUnitSaida = 0;

                        novoEstoque.qtdeAtual = novoEstoque.qtdeInicial + item.quantidade;
                        novoEstoque.vlrTotal = ultEstoque.vlrTotal + (item.preco*item.quantidade);

                        novoEstoque.custoMedio = novoEstoque.vlrTotal/novoEstoque.qtdeAtual;

                        EPRODUTO produto = prod.FirstOrDefault();

                        // Coloca o id do estoque e o id da filial do estoque no produto movimentado
                        produto.idLoc = novoEstoque.idCtEstoque;
                        produto.idFilialLoc = novoEstoque.idFilial;

                        Contexto.Atual.AddToECTESTOQUE(novoEstoque);
                        Contexto.Atual.AddToEPRODUTO(produto);

                        //Contexto.Atual.SaveChanges();

                        cont++;
                    }

                    return true;
                } else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                throw e;
                return false;
            }
        }
        
        /// <summary>
        /// Função que relaciona um movimento a outro
        /// </summary>
        /// <param name="objold">objeto movimento de origem</param>
        /// <param name="objnew">objeto movimento de destino</param>
        /// <returns>retorna true ou false</returns>
        public bool RelacionaMov(List<List<string>> obj)
        {
            try
            {
                SistemaDAL sis = new SistemaDAL();
                CMOVRELAC movrelac = new CMOVRELAC();

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
                CMOVIMENTO movimento = new CMOVIMENTO();

                foreach (List<string> items in obj)
                {
                    if (idmov != Convert.ToString(items[2]))
                    {

                        idmov = Convert.ToString(items[2]);

                        if (contIdmov != 0)
                        {
                            if (contIdmov == contItem)
                            {
                                movimento.codStatus = "R";
                            } else
                            {
                                movimento.codStatus = "X";
                            }
                        }

                        movimento = new CMOVIMENTO();

                        movimento.idMov = Convert.ToInt32(idmov);

                        movimento = Busca(movimento);

                        contIdmov = movimento.CITEMMOV.Count;

                        contItem = 0;

                        movrelac.idMovRelac = sis.geraId("CMOVRELAC");

                        movrelac.idEmpresaOri = Convert.ToInt32(items[0]);
                        movrelac.idFilialOri = Convert.ToInt32(items[1]);
                        movrelac.idMovOri = Convert.ToInt32(items[2]);

                        movrelac.idEmpresaDes = Convert.ToInt32(items[4]);
                        movrelac.idFilialDes = Convert.ToInt32(items[5]);
                        movrelac.idMovDes = Convert.ToInt32(items[6]);

                        Contexto.Atual.AddToCMOVRELAC(movrelac);
                    }

                    CITEMMOVRELAC itemmovrelac = new CITEMMOVRELAC();

                    itemmovrelac.idItemMovRelac = sis.geraId("CITEMMOVRELAC");

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
                            r => r.idEmpresa == Memoria.Empresa.Value && r.idFilial == Memoria.Filial.Value && r.idMov == itemmovrelac.idMovOri && r.sequencialMov == itemmovrelac.seqItemOri);

                    itemold.status = false;

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
                return false;
            }
        }

        /// <summary>
        /// Função para buscar
        /// </summary>
        /// <param name="obj">objeto movimento</param>
        /// <returns>true ou false</returns>
        public bool Atualiza(CMOVIMENTO obj)
        {
            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();
            return true;
        }

        /// <summary>
        /// Função para buscar tipo
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>lista de CTPMOV</returns>
        public IQueryable<CTPMOV> BuscaTipo()
        {
            IQueryable<CTPMOV> lista = (from a in Contexto.Atual.CTPMOV
                                        select a);
            return lista;
        }

        public IQueryable<CITEMMOV> BuscaItem(CMOVIMENTO obj)
        {
            IQueryable<CITEMMOV> lista = (from a in Contexto.Atual.CITEMMOV
                                          where a.idMov == obj.idMov
                                          select a);
            return lista;
        }

        /// <summary>
        /// Função que troca o status para inativo
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
