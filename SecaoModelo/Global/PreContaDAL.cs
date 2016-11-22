using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Objects;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Artebit.Restaurante.Global.AcessoDados.Atendimento;
using Artebit.Restaurante.Global.Modelo;
using Artebit.Restaurante.Global.Modelo.Enum;
using Artebit.Restaurante.Global.Modelo.Extensions;

namespace Artebit.Restaurante.Global.AcessoDados.Global
{
    public class PreContaDAL
    {
        private int _contaDes;
        private int _mesaDes;

        /// <summary>
        /// Função para buscar objeto conta completo
        /// </summary>
        /// <param  name="obj">objeto conta</param>
        /// <returns>objeto conta</returns>
        public ACONTA Buscar(ACONTA obj)
        {
            // Busca a conta
            IQueryable<ACONTA> resultado;

            if (Memoria.TipoConta != TipoConta.Delivery)
            {
                string tpConta = Memoria.TipoConta.GetStringValue();

                resultado = Contexto.Atual.ACONTA.Where(a =>
                                                        (a.idConta == obj.idConta || obj.idConta == 0)
                                                        &&
                                                        (a.nuCartao == obj.nuCartao || obj.nuCartao == null || obj.nuCartao == 0)
                                                        &&
                                                        (a.tpConta == tpConta)
                                                        &&
                                                        (a.idEmpresa == Memoria.Empresa ||
                                                         Memoria.Empresa == 0)
                                                        &&
                                                        (a.idFilial == Memoria.Filial)
                                                        &&
                                                        (a.idStatus == 1 || a.idStatus == 3)
                                                        &&
                                                        (a.AASSOCIACAO.Any(r => r.nuMesa == obj.nuMesa) ||
                                                         obj.nuMesa == 0 || obj.nuMesa == null || a.nuMesa == obj.nuMesa)
                    );
            }
            else
            {
                string tpConta = Memoria.TipoConta.GetStringValue();

                resultado = Contexto.Atual.ACONTA.Where(a => (a.idCliFor == obj.idCliFor || obj.idCliFor == 0)
                                                             &&
                                                             (a.tpConta == tpConta)
                                                             &&
                                                             (a.idEmpresa == Memoria.Empresa ||
                                                              Memoria.Empresa == 0)
                                                             &&
                                                             (a.idFilial == Memoria.Filial)
                                                             &&
                                                             (a.idStatus == 1 || a.idStatus == 3)
                    );
            }

            if (resultado.Any())
            {
                Contexto.Atual.Refresh(RefreshMode.StoreWins, resultado.First());
                return resultado.First();
            }
            return null;
        }

        public ACONTA BuscarByMesa(ACONTA obj)
        {
            string tpConta = Memoria.TipoConta.GetStringValue();

            // Busca a conta
            ACONTA obj2 = Contexto.Atual.ACONTA.FirstOrDefault(a => (((a.nuMesa == obj.nuMesa || obj.nuMesa == 0)
                                                                      &&
                                                                      (a.tpConta == tpConta)
                                                                      &&
                                                                      (a.idEmpresa == Memoria.Empresa)
                                                                      &&
                                                                      (a.idFilial == Memoria.Filial)
                                                                      ||
                                                                      (a.AASSOCIACAO.Any(r => r.nuMesa == obj.nuMesa))
                                                                     )
                                                                     &&
                                                                     (a.idStatus == 1 || a.idStatus == 3)
                                                                    //Busca somente contas Abertas ou Bloqueadas
                                                                    ));

            Contexto.Atual.Refresh(RefreshMode.StoreWins, obj2);

            return obj2;
        }

        public ACONTA BuscarPorMesa(int nuMesa)
        {
            //string tpConta = Memoria.TipoConta.GetStringValue();

            // Busca a conta
            ACONTA obj2 = (from p in Contexto.Atual.ACONTA
                          from ass in p.AASSOCIACAO.DefaultIfEmpty() //Contexto.Atual.AASSOCIACAO on p.idConta equals ass.idConta
                          where ((p.nuMesa == nuMesa && p.idEmpresa == Memoria.Empresa && p.idFilial == Memoria.Filial)
                                 ||
                                 (ass.nuMesa == nuMesa && ass.idEmpresa == Memoria.Empresa &&
                                  ass.idFilial == Memoria.Filial))
                                && (p.idStatus == 1 || p.idStatus == 3)
                          select p).FirstOrDefault();

            return obj2;
        }

        public ACONTA BuscarPorCartao(int nuCartao)
        {
            //string tpConta = Memoria.TipoConta.GetStringValue();

            // Busca a conta
            ACONTA obj2 = (from p in Contexto.Atual.ACONTA
                           from ass in p.AASSOCIACAO.DefaultIfEmpty() //Contexto.Atual.AASSOCIACAO on p.idConta equals ass.idConta
                           where ((p.nuCartao == nuCartao && p.idEmpresa == Memoria.Empresa && p.idFilial == Memoria.Filial)
                                  ||
                                  (ass.nuCartao == nuCartao && ass.idEmpresa == Memoria.Empresa &&
                                   ass.idFilial == Memoria.Filial))
                                 && (p.idStatus == 1 || p.idStatus == 3)
                           select p).FirstOrDefault();

            return obj2;
        }

        public ACONTA BuscarPorId(int idConta)
        {
            //string tpConta = Memoria.TipoConta.GetStringValue();

            // Busca a conta
            ACONTA obj2 = (from p in Contexto.Atual.ACONTA
                           from ass in p.AASSOCIACAO.DefaultIfEmpty() //Contexto.Atual.AASSOCIACAO on p.idConta equals ass.idConta
                           where ((p.idConta == idConta && p.idEmpresa == Memoria.Empresa && p.idFilial == Memoria.Filial)
                                 && (p.idStatus == 1 || p.idStatus == 3))
                           select p).FirstOrDefault();

            return obj2;
        }

        /// <summary>
        /// Função para buscar uma lista de objetos
        /// </summary>
        /// <returns>lista de objetos</returns>
        public IQueryable<ACONTA> BuscarLista()
        {
            string tpConta = Memoria.TipoConta.GetStringValue();

            // Busca a lista
            IQueryable<ACONTA> lista = from a in Contexto.Atual.ACONTA
                                       where (a.idEmpresa == Memoria.Empresa || Memoria.Empresa == 0)
                                             &&
                                             (a.tpConta == tpConta)
                                             &&
                                             (a.idFilial == Memoria.Filial)
                                       select a;

            return lista;
        }

        /// <summary>
        /// Função para criar novo
        /// </summary>
        /// <param name="obj">objeto aconta</param>
        /// <param name="trans"> </param>
        /// <returns>true ou false</returns>
        public bool Criar(ACONTA obj, string trans)
        {
            obj.tpConta = Memoria.TipoConta.GetStringValue();

            foreach (ALOG al in obj.ALOG)
            {
                if (al.contaDestino == 0)
                    al.contaDestino = null;

                if (al.mesaDestino == 0)
                    al.mesaDestino = null;
            }

            if (Memoria.Vendedor == null)
            {
                obj.usuarioAbertura = Memoria.Codusuario;
            }
            else
            {
                obj.vendedorAbertura = Memoria.Vendedor;
            }

            if (Memoria.Empresa != 0)
            {
                Memoria.LogCodUsuario = Memoria.Codusuario;
                Memoria.LogData = DateTime.Now;
                var log = new LogDAL();

                // atualiza a empresa e a filial
                obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);
                obj.idFilial = Convert.ToInt32(Memoria.Filial);


                if (obj.idConta.ToString(CultureInfo.InvariantCulture) == "" || obj.idConta == 0)
                    obj.idConta = Contexto.GerarId("ACONTA");

                obj.servico = obj.tpConta == "M";
                obj.idStatus = 1; // Status 1 : Aberta

                // atualiza data de inclusão e alteração
                obj.dataInclusao = DateTime.Now;

                IOrderedEnumerable<ACONTITEM> lis = obj.ACONTITEM.ToList().OrderBy(r => r.nuItem);

                foreach (ACONTITEM pedido in lis)
                {
                    pedido.idConta = obj.idConta;
                    pedido.idStatus = 1;
                    pedido.idEmpresa = Memoria.Empresa;
                    pedido.idFilial = Memoria.Filial;
                    pedido.dataInclusao = DateTime.Now;

                    if (pedido.impresso != true && pedido.impresso != null)
                        pedido.impresso = false;
                }

                foreach (AASSOCIACAO mesas in obj.AASSOCIACAO)
                {
                    mesas.idConta = obj.idConta;
                }

                if (obj.pessoas == 0)
                {
                    obj.pessoas = 1;
                }

                if (trans == "0")
                {
                    if (lis.Any(r => r.impresso == false || r.impresso == null))
                    {
                        var lista = new GFILAIMPRESSAO
                                        {
                                            idEmpresa = obj.idEmpresa,
                                            idFilial = obj.idFilial,
                                            impresso = false,
                                            nuMesa = obj.nuMesa,
                                            tipoImpressao = 2
                                        };
                        GVENDEDOR vend =
                            Contexto.Atual.GVENDEDOR.FirstOrDefault(
                                r =>
                                r.idVen == Memoria.Vendedor && r.idEmpresa == Memoria.Empresa &&
                                r.idFilial == Memoria.Filial);
                        if (vend != null)
                            lista.nomeVendedor = vend.nome;
                        lista.idDocumento = Contexto.GerarId("GFILAIMPRESSAO");

                        Contexto.Atual.AddToGFILAIMPRESSAO(lista);
                    }
                }

                // adiciona conta
                Contexto.Atual.AddToACONTA(obj);

                // salva alterações
                Contexto.Atual.SaveChanges();

                //Criando o log
                if (Memoria.LogMesaDestino == null) //Mesa destino é zerada depois q grava o log, 
                {
                    //se não for nula é pq houve uma tranferencia para uma mesa vazia

                    //Memoria.LogMesa = obj.nuMesa;
                    Memoria.LogConta = obj.idConta;
                    Memoria.LogContaDestino = null;
                    Memoria.LogMesaDestino = null;
                    Memoria.LogAcao = "Cria nova conta e mesa";
                }
                else
                {
                    Memoria.LogAcao = "Tranferencia de iten(s), abertura de mesa e criação de conta";
                }

                log.Criar();

                foreach (ACONTITEM pedido in lis)
                {
                    var lo = new LogDAL();

                    Memoria.LogConta = obj.idConta;
                    Memoria.LogMesa = obj.nuMesa;
                    Memoria.LogAcao = "Novo item adicionado: " + pedido.idProduto;
                    lo.Criar();
                }

                Contexto.Atual.SaveChanges();

                return true;
            }
            return false;
        }

        /// <summary>
        /// Função para criar novo
        /// </summary>
        /// <param name="obj">objeto aconta</param>
        /// <returns>true ou false</returns>
        public bool Criar(ACONTA obj)
        {
            foreach (ALOG al in obj.ALOG)
            {
                if (al.contaDestino == 0)
                    al.contaDestino = null;

                if (al.mesaDestino == 0)
                    al.mesaDestino = null;
            }

            if (Memoria.Vendedor == null)
            {
                obj.usuarioAbertura = Memoria.Codusuario;
            }
            else
            {
                obj.vendedorAbertura = Memoria.Vendedor;
            }

            if (Memoria.Empresa != 0)
            {
                Memoria.LogCodUsuario = Memoria.Codusuario;
                Memoria.LogData = DateTime.Now;
                var log = new LogDAL();

                // atualiza a empresa e a filial
                obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);
                obj.idFilial = Convert.ToInt32(Memoria.Filial);

                if (obj.idConta.ToString(CultureInfo.InvariantCulture) == "" || obj.idConta == 0)
                    obj.idConta = Contexto.GerarId("ACONTA");

                obj.tpConta = Memoria.TipoConta.GetStringValue();

                obj.servico = obj.tpConta == "M";
                obj.idStatus = 1; // Status 1 : Aberta

                // atualiza data de inclusão e alteração
                obj.dataInclusao = DateTime.Now;

                IOrderedEnumerable<ACONTITEM> lis = obj.ACONTITEM.ToList().OrderBy(r => r.nuItem);

                foreach (ACONTITEM pedido in lis)
                {
                    pedido.idConta = obj.idConta;
                    pedido.idStatus = 1;
                    pedido.idEmpresa = Memoria.Empresa;
                    pedido.idFilial = Memoria.Filial;
                    pedido.dataInclusao = DateTime.Now;

                    if (pedido.impresso != true && pedido.impresso != null)
                        pedido.impresso = false;

                    if (pedido.idVen == 0)
                        pedido.idVen = 1;
                }

                foreach (AASSOCIACAO mesas in obj.AASSOCIACAO)
                {
                    mesas.idConta = obj.idConta;
                }

                if (obj.pessoas == 0)
                {
                    obj.pessoas = 1;
                }

                if (lis.Any(r => r.impresso == false || r.impresso == null))
                {
                    var lista = new GFILAIMPRESSAO
                                    {
                                        idEmpresa = obj.idEmpresa,
                                        idFilial = obj.idFilial,
                                        impresso = false,
                                        nuMesa = obj.nuMesa,
                                        tipoImpressao = 2
                                    };
                    GVENDEDOR vend =
                        Contexto.Atual.GVENDEDOR.FirstOrDefault(
                            r =>
                            r.idVen == Memoria.Vendedor && r.idEmpresa == Memoria.Empresa &&
                            r.idFilial == Memoria.Filial);

                    if (vend != null)
                        lista.nomeVendedor = vend.nome;

                    lista.idDocumento = Contexto.GerarId("GFILAIMPRESSAO");

                    Contexto.Atual.AddToGFILAIMPRESSAO(lista);
                }

                // adiciona conta
                Contexto.Atual.AddToACONTA(obj);

                // salva alterações
                Contexto.Atual.SaveChanges();

                //Criando o log
                if (Memoria.LogMesaDestino == null) //Mesa destino é zerada depois q grava o log, 
                {
                    //se não for nula é pq houve uma tranferencia para uma mesa vazia

                    //Memoria.LogMesa = obj.nuMesa;
                    Memoria.LogConta = obj.idConta;
                    Memoria.LogContaDestino = null;
                    Memoria.LogMesaDestino = null;
                    Memoria.LogAcao = "Cria nova conta e mesa";
                }
                else
                {
                    Memoria.LogAcao = "Tranferencia de iten(s), abertura de mesa e criação de conta";
                }

                log.Criar();

                foreach (ACONTITEM pedido in lis)
                {
                    var lo = new LogDAL();

                    Memoria.LogConta = obj.idConta;
                    Memoria.LogMesa = obj.nuMesa;
                    Memoria.LogAcao = "Novo item adicionado: " + pedido.idProduto;
                    lo.Criar();
                }

                Contexto.Atual.SaveChanges();

                return true;
            }
            return false;
        }

        /// <summary>
        /// Função para criar novo
        /// </summary>
        /// <param name="obj">objeto aconta</param>
        /// <param name="transf"> </param>
        /// <returns>true ou false</returns>
        public bool Criar(ACONTA obj, bool transf)
        {
            if (transf)
            {
                if (Memoria.Empresa != 0)
                {
                    var log = new LogDAL();

                    Memoria.LogCodUsuario = Memoria.Codusuario;
                    Memoria.LogData = DateTime.Now;

                    // atualiza a empresa e a filial
                    obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);
                    obj.idFilial = Convert.ToInt32(Memoria.Filial);

                    if (obj.pessoas == 0)
                    {
                        obj.pessoas = 1;
                    }

                    // adiciona conta
                    Contexto.Atual.AddToACONTA(obj);

                    // salva alterações
                    Contexto.Atual.SaveChanges();

                    //Criando o log
                    if (Memoria.LogMesaDestino == null) //Mesa destino é zerada depois q grava o log, 
                    {
                        //se não for nula é pq houve uma tranferencia para uma mesa vazia

                        //Memoria.LogMesa = obj.nuMesa;
                        Memoria.LogConta = obj.idConta;
                        Memoria.LogContaDestino = null;
                        Memoria.LogMesaDestino = null;
                        Memoria.LogAcao = "Cria nova conta e mesa";
                    }
                    else
                    {
                        Memoria.LogAcao = "Tranferencia de iten(s), abertura de mesa e criação de conta";
                    }

                    log.Criar();

                    foreach (ACONTITEM pedido in obj.ACONTITEM)
                    {
                        var lo = new LogDAL();

                        Memoria.LogConta = obj.idConta;
                        Memoria.LogMesa = obj.nuMesa;
                        Memoria.LogAcao = "Novo item adicionado: " + pedido.idProduto;
                        lo.Criar();
                    }

                    Contexto.Atual.SaveChanges();

                    return true;
                }
                return false;
            }
            return Criar(obj);
        }

        /// <summary>
        /// Função para Atualizar
        /// </summary>
        /// <param name="obj">objeto aconta</param>
        /// <param name="trans"> </param>
        /// <returns>true ou false</returns>
        public bool Atualizar(ACONTA obj, string trans)
        {
            if (trans == "0")
            {
                if (obj.ACONTITEM.Count(r => r.EntityState == EntityState.Added) > 0)
                {
                    foreach (ACONTITEM item in obj.ACONTITEM.Where(r => r.EntityState == EntityState.Added))
                    {
                        if (item.impresso != true && item.impresso != null)
                            item.impresso = false;
                    }
                }

                if (obj.ACONTITEM.Count(r => r.EntityState == EntityState.Added) > 0 &&
                    obj.ACONTITEM.Any(r => r.impresso == false || r.impresso == null))
                {
                    var lista = new GFILAIMPRESSAO
                                    {
                                        idEmpresa = obj.idEmpresa,
                                        idFilial = obj.idFilial,
                                        impresso = false,
                                        nuMesa = obj.nuMesa,
                                        tipoImpressao = 2
                                    };
                    GVENDEDOR vend =
                        Contexto.Atual.GVENDEDOR.FirstOrDefault(
                            r =>
                            r.idVen == Memoria.Vendedor && r.idEmpresa == Memoria.Empresa &&
                            r.idFilial == Memoria.Filial);
                    if (vend != null)
                        lista.nomeVendedor = vend.nome;

                    lista.idDocumento = Contexto.GerarId("GFILAIMPRESSAO");
                    Contexto.Atual.AddToGFILAIMPRESSAO(lista);
                }
            }

            foreach (ALOG al in obj.ALOG)
            {
                if (al.contaDestino == 0)
                    al.contaDestino = null;

                if (al.mesaDestino == 0)
                    al.mesaDestino = null;
            }


            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();

            if (trans == "1")
            {
                Memoria.LogAcao = "Transferência de item";
                _mesaDes = Convert.ToInt16(Memoria.LogMesaDestino);
                _contaDes = Convert.ToInt16(Memoria.LogContaDestino);
            }

            else
            {
                Memoria.LogMesa = obj.nuMesa;
                Memoria.LogConta = obj.idConta;
                Memoria.LogContaDestino = null;
                Memoria.LogMesaDestino = null;
            }


            Memoria.LogCodUsuario = Memoria.Codusuario;
            Memoria.LogData = DateTime.Now;

            var log = new LogDAL();

            log.Criar();

            if (trans == "1")
            {
                List<ACONTITEM> lis = obj.ACONTITEM.Where(a => a.idStatus == 5).ToList();
                foreach (ACONTITEM pedido in lis)
                {
                    Memoria.LogContaDestino = _contaDes;
                    Memoria.LogMesaDestino = _mesaDes;
                    Memoria.LogAcao = "Item trasferido: " + pedido.idProduto;
                    log.Criar();
                }
            }

            Contexto.Atual.SaveChanges();

            return true;
        }

        /// <summary>
        /// Função para Atualizar
        /// </summary>
        /// <param name="obj">objeto aconta</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(ACONTA obj)
        {
            if (obj.ACONTITEM.Count(r => r.EntityState == EntityState.Added) > 0)
            {
                foreach (ACONTITEM item in obj.ACONTITEM.Where(r => r.EntityState == EntityState.Added))
                {
                    if (item.impresso != true && item.impresso != null)
                        item.impresso = false;
                }
            }

            if (obj.ACONTITEM.Count(r => r.EntityState == EntityState.Added) > 0 &&
                obj.ACONTITEM.Any(r => r.impresso == false || r.impresso == null))
            {
                if (obj.nuMesa != null)
                {
                    var lista = new GFILAIMPRESSAO
                                    {
                                        idEmpresa = obj.idEmpresa,
                                        idFilial = obj.idFilial,
                                        impresso = false,
                                        nuMesa = obj.nuMesa,
                                        tipoImpressao = 2
                                    };
                    GVENDEDOR vend =
                        Contexto.Atual.GVENDEDOR.FirstOrDefault(
                            r =>
                            r.idVen == Memoria.Vendedor && r.idEmpresa == Memoria.Empresa &&
                            r.idFilial == Memoria.Filial);
                    if (vend != null)
                        lista.nomeVendedor = vend.nome;

                    lista.idDocumento = Contexto.GerarId("GFILAIMPRESSAO");
                    Contexto.Atual.AddToGFILAIMPRESSAO(lista);
                }
            }

            foreach (ALOG al in obj.ALOG)
            {
                if (al.contaDestino == 0)
                    al.contaDestino = null;

                if (al.mesaDestino == 0)
                    al.mesaDestino = null;
            }


            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();


            Memoria.LogMesa = obj.nuMesa;
            Memoria.LogConta = obj.idConta;
            Memoria.LogContaDestino = null;
            Memoria.LogMesaDestino = null;


            Memoria.LogCodUsuario = Memoria.Codusuario;
            Memoria.LogData = DateTime.Now;

            var log = new LogDAL();

            log.Criar();

            Contexto.Atual.SaveChanges();

            return true;
        }

        /// <summary>
        /// Função para Atualizar
        /// </summary>
        /// <param name="obj">objeto aconta</param>
        /// <param name="transf"> </param>
        /// <returns>true ou false</returns>
        public bool Atualizar(ACONTA obj, bool transf)
        {
            if (transf)
            {
                Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
                Contexto.Atual.SaveChanges();

                Memoria.LogAcao = "Transferência de item";
                _mesaDes = Convert.ToInt16(Memoria.LogMesaDestino);
                _contaDes = Convert.ToInt16(Memoria.LogContaDestino);

                Memoria.LogCodUsuario = Memoria.Codusuario;
                Memoria.LogData = DateTime.Now;

                var log = new LogDAL();

                log.Criar();

                List<ACONTITEM> lis = obj.ACONTITEM.Where(a => a.idStatus == 5).ToList();
                foreach (ACONTITEM pedido in lis)
                {
                    Memoria.LogContaDestino = _contaDes;
                    Memoria.LogMesaDestino = _mesaDes;
                    Memoria.LogAcao = "Item trasferido: " + pedido.idProduto;
                    log.Criar();
                }

                Contexto.Atual.SaveChanges();

                return true;
            }
            return Atualizar(obj);
        }

        public bool Transferir(ACONTA obj, string contanova)
        {
            foreach (ALOG al in obj.ALOG)
            {
                if (al.contaDestino == 0)
                    al.contaDestino = null;

                if (al.mesaDestino == 0)
                    al.mesaDestino = null;
            }


            if (Memoria.TipoConta == TipoConta.Mesa)
            {
                #region Tipo conta = M

                var ms = new MesaDAL();
                var novaMesa = new GMESA {idEmpresa = Memoria.Empresa, idFilial = Memoria.Filial};

                if (obj.nuMesa != null) novaMesa.nuMesa = obj.nuMesa.Value;

                //busca dados da nova mesa, para a qual a conta será transferida
                novaMesa = ms.Buscar(novaMesa);


                var mesaAntiga = new GMESA {idEmpresa = Memoria.Empresa, idFilial = Memoria.Filial};
                //mesaAntiga.nuMesa = obj.nuMesaAnterior.Value;

                //busca a mesa antiga da conta
                mesaAntiga = ms.Buscar(mesaAntiga);

                foreach (AASSOCIACAO a in obj.AASSOCIACAO)
                {
                    a.GMESA.idStatus = 2;
                    Contexto.Atual.ObjectStateManager.ChangeObjectState(a.GMESA, EntityState.Modified);
                }

                //obj.AASSOCIACAO.Clear();

                //se a nova mesa estiver livre
                if (novaMesa.idStatus == (int) StatusMesa.Livre) //status livre
                {
                    //coloca a nova mesa como ocupada
                    //novaMesa.status = 1; //Ocupada

                    //coloca a mesa antiga como livre
                    //mesaAntiga.idStatus = 2; //Livre


                    //Gravar histórico de tranferências
                    foreach (ACONTITEM cont in obj.ACONTITEM)
                    {
                        var hist = new AHISTTRANS
                                       {
                                           idHistorico = Contexto.GerarId("AHISTTRANS"),
                                           idEmpresa = Memoria.Empresa,
                                           idFilial = Memoria.Filial,
                                           codusuario = Memoria.Codusuario,
                                           codvendedor = Memoria.Vendedor,
                                           idProduto = cont.idProduto,
                                           contaOrigem = cont.idConta,
                                           contaDestino = cont.idConta,
                                           mesaOrigem = mesaAntiga.nuMesa,
                                           mesaDestino = novaMesa.nuMesa,
                                           data = DateTime.Now
                                       };

                        Contexto.Atual.AHISTTRANS.AddObject(hist);
                    }

                    Contexto.Atual.ObjectStateManager.ChangeObjectState(mesaAntiga, EntityState.Modified);
                    Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
                    Contexto.Atual.ObjectStateManager.ChangeObjectState(novaMesa, EntityState.Modified);

                    Memoria.LogMesa = mesaAntiga.nuMesa;
                    Memoria.LogAcao =
                        "Transferência da conta toda, nova mesa ocupada e mesa antiga livre, e todas mesas associadas livres";
                    Memoria.LogCodUsuario = Memoria.Codusuario;
                    Memoria.LogData = DateTime.Now;
                    Memoria.LogConta = obj.idConta;
                    Memoria.LogContaDestino = obj.idConta;
                    Memoria.LogMesaDestino = novaMesa.nuMesa;

                    var log = new LogDAL();

                    log.Criar();

                    Contexto.Atual.SaveChanges();

                    return true;
                }

                if (novaMesa.idStatus == (int) StatusMesa.Ocupada) //status ocupada
                {
                    //se a nova mesa estiver ocupada, pega a conta da mesa
                    ACONTA act = BuscarByMesa(obj);

                    //se a conta estiver aberta, executa trasnferencia dos itens
                    if (act.idStatus == 1) //status conta: aberta
                    {
                        int itens = act.ACONTITEM.OrderBy(a => a.nuItem).Last().nuItem;

                        foreach (ACONTITEM item in obj.ACONTITEM.Where(r => r.idStatus != 5))
                            //não pega itens transferidos
                        {
                            var novoItem = new ACONTITEM
                                               {
                                                   idEmpresa = item.idEmpresa,
                                                   idFilial = item.idFilial,
                                                   idConta = act.idConta,
                                                   idProduto = item.idProduto,
                                                   adicional = item.adicional,
                                                   desconto = item.desconto,
                                                   idStatus = item.idStatus,
                                                   idVen = item.idVen,
                                                   nuItem = itens + 1
                                               };
                            novoItem.nuItemPai = novoItem.nuItem + (item.nuItemPai - item.nuItem);
                            novoItem.preco = item.preco;
                            novoItem.opcao = item.opcao;
                            novoItem.produzido = item.produzido;
                            novoItem.impresso = item.impresso;
                            novoItem.quantidade = item.quantidade;
                            novoItem.txtObs = item.txtObs;
                            novoItem.dataInclusao = item.dataInclusao;

                            act.ACONTITEM.Add(novoItem);

                            #region Gravando um histórico da tranferencia

                            var hist = new AHISTTRANS
                                           {
                                               idHistorico = Contexto.GerarId("AHISTTRANS"),
                                               idEmpresa = Memoria.Empresa,
                                               idFilial = Memoria.Filial,
                                               codusuario = Memoria.Codusuario,
                                               codvendedor = Memoria.Vendedor,
                                               idProduto = item.idProduto,
                                               contaOrigem = obj.idConta,
                                               contaDestino = act.idConta,
                                               mesaOrigem = mesaAntiga.nuMesa,
                                               mesaDestino = novaMesa.nuMesa,
                                               data = DateTime.Now
                                           };


                            Contexto.Atual.AHISTTRANS.AddObject(hist);

                            #endregion

                            itens++;
                        }

                        //obj.nuMesa = obj.nuMesaAnterior;
                        //obj.nuMesaAnterior = null;
                        obj.idStatus = 4; //Transferido

                        //act.nuMesaAnterior = obj.nuMesa;

                        //mesaAntiga.idStatus = 2; //Livre

                        Contexto.Atual.ObjectStateManager.ChangeObjectState(mesaAntiga, EntityState.Modified);
                        Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
                        Contexto.Atual.ObjectStateManager.ChangeObjectState(act, EntityState.Modified);

                        Memoria.LogMesa = mesaAntiga.nuMesa;
                        Memoria.LogAcao =
                            "Transferência da conta toda para uma mesa já ocupada, nova mesa ocupada e mesa antiga livre, e todas mesas associadas livres";
                        Memoria.LogCodUsuario = Memoria.Codusuario;
                        Memoria.LogData = DateTime.Now;
                        Memoria.LogConta = obj.idConta;
                        Memoria.LogContaDestino = act.idConta;
                        Memoria.LogMesaDestino = novaMesa.nuMesa;

                        var log = new LogDAL();

                        log.Criar();

                        Contexto.Atual.SaveChanges();

                        return true;
                    }
                    Memoria.MsgGlobal =
                        "Não é possível transferir para a mesa informada, pois a mesma esta com a pre-conta " +
                        act.ASTATCONTA.descricao;
                    return false;
                }
                Memoria.MsgGlobal =
                    "Não é possível transferir para a mesa informada pois a mesma esta com o status ";
                //novaMesa.GSTATMESA.descricao;
                return false;

                #endregion
            }
            else
            {
                #region Tipo conta = B

                var act = new ACONTA
                              {
                                  idEmpresa = Memoria.Empresa,
                                  idFilial = Memoria.Filial,
                                  idConta = Convert.ToInt32(contanova)
                              };

                act = Buscar(act);

                //se a conta estiver aberta, executa trasnferencia dos itens
                if (act.idStatus == 1) //status conta: aberta
                {
                    int itens = 0;
                    if (act.ACONTITEM.OrderBy(a => a.nuItem).Any())
                        itens = act.ACONTITEM.OrderBy(a => a.nuItem).Last().nuItem;

                    foreach (ACONTITEM item in obj.ACONTITEM.Where(r => r.idStatus != 5))
                        //não pega itens transferidos
                    {
                        var novoItem = new ACONTITEM
                                           {
                                               idEmpresa = item.idEmpresa,
                                               idFilial = item.idFilial,
                                               idConta = act.idConta,
                                               idProduto = item.idProduto,
                                               adicional = item.adicional,
                                               desconto = item.desconto,
                                               idStatus = item.idStatus,
                                               idVen = item.idVen,
                                               nuItem = itens + 1
                                           };
                        novoItem.nuItemPai = novoItem.nuItem + (item.nuItemPai - item.nuItem);
                        novoItem.preco = item.preco;
                        novoItem.opcao = item.opcao;
                        novoItem.produzido = item.produzido;
                        novoItem.impresso = item.impresso;
                        novoItem.quantidade = item.quantidade;
                        novoItem.txtObs = item.txtObs;
                        novoItem.dataInclusao = item.dataInclusao;

                        act.ACONTITEM.Add(novoItem);

                        #region Gravando um histórico da tranferencia

                        var hist = new AHISTTRANS
                                       {
                                           idHistorico = Contexto.GerarId("AHISTTRANS"),
                                           idEmpresa = Memoria.Empresa,
                                           idFilial = Memoria.Filial,
                                           codusuario = Memoria.Codusuario,
                                           codvendedor = Memoria.Vendedor,
                                           idProduto = item.idProduto,
                                           contaOrigem = obj.idConta,
                                           contaDestino = act.idConta,
                                           mesaOrigem = null,
                                           mesaDestino = null,
                                           data = DateTime.Now
                                       };


                        Contexto.Atual.AHISTTRANS.AddObject(hist);

                        #endregion

                        itens++;
                    }

                    obj.idStatus = 4; //Transferido

                    Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
                    Contexto.Atual.ObjectStateManager.ChangeObjectState(act, EntityState.Modified);

                    Memoria.LogMesa = null;
                    Memoria.LogAcao =
                        "Transferência da comanda toda para outra comanda";
                    Memoria.LogCodUsuario = Memoria.Codusuario;
                    Memoria.LogData = DateTime.Now;
                    Memoria.LogConta = obj.idConta;
                    Memoria.LogContaDestino = act.idConta;
                    Memoria.LogMesaDestino = null;

                    var log = new LogDAL();

                    log.Criar();

                    Contexto.Atual.SaveChanges();

                    return true;
                }
                Memoria.MsgGlobal =
                    "Não é possível transferir para a comanda informada, pois a mesma esta com a pre-conta " +
                    act.ASTATCONTA.descricao;
                return false;

                #endregion
            }
        }

        //TRANSFERENCIA TOTAL
        public bool Transferir(ACONTA contaAtual, int destino)
        {
            if (Memoria.TipoConta == TipoConta.Mesa)
            {
                #region Tipo conta = M

                ACONTA nConta = BuscarPorMesa(destino);

                //se a nova mesa estiver livre
                if (nConta == null) //status livre
                {
                    var mdal = new MesaDAL();
                    if (contaAtual.nuMesa != null)
                    {
                        GMESA mesaAntiga = mdal.Buscar(contaAtual.nuMesa.Value);

                        GMESA mesaNova = mdal.Buscar(destino);


                        foreach (AASSOCIACAO assoc in contaAtual.AASSOCIACAO)
                        {
                            assoc.GMESA.idStatus = (int) StatusMesa.Livre;
                        }

                        //coloca a mesa antiga como livre
                        mesaAntiga.idStatus = 2; //Livre

                        //coloca a nova mesa como ocupada
                        mesaNova.idStatus = 1; //Ocupada

                        contaAtual.AASSOCIACAO.Clear();

                        contaAtual.nuMesa = destino;

                        #region Gravar histórico de tranferências

                        foreach (ACONTITEM cont in contaAtual.ACONTITEM)
                        {
                            var hist = new AHISTTRANS
                                           {
                                               idHistorico = Contexto.GerarId("AHISTTRANS"),
                                               idEmpresa = Memoria.Empresa,
                                               idFilial = Memoria.Filial,
                                               codusuario = Memoria.Codusuario,
                                               codvendedor = Memoria.Vendedor,
                                               idProduto = cont.idProduto,
                                               contaOrigem = cont.idConta,
                                               contaDestino = cont.idConta,
                                               mesaOrigem = mesaAntiga.nuMesa,
                                               mesaDestino = mesaNova.nuMesa,
                                               data = DateTime.Now
                                           };

                            Contexto.Atual.AHISTTRANS.AddObject(hist);
                        }

                        #endregion

                        Contexto.Atual.ObjectStateManager.ChangeObjectState(mesaAntiga, EntityState.Modified);
                        Contexto.Atual.ObjectStateManager.ChangeObjectState(contaAtual, EntityState.Modified);
                        Contexto.Atual.ObjectStateManager.ChangeObjectState(mesaNova, EntityState.Modified);

                        #region Log de Operacao

                        Memoria.LogMesa = mesaAntiga.nuMesa;
                        Memoria.LogAcao =
                            "Transferência da conta toda, nova mesa ocupada e mesa antiga livre, e todas mesas associadas livres";
                        Memoria.LogCodUsuario = Memoria.Codusuario;
                        Memoria.LogData = DateTime.Now;
                        Memoria.LogConta = contaAtual.idConta;
                        Memoria.LogContaDestino = contaAtual.idConta;
                        Memoria.LogMesaDestino = mesaNova.nuMesa;
                    }

                    var log = new LogDAL();

                    log.Criar();

                    #endregion

                    Contexto.Atual.SaveChanges();

                    return true;
                }

                if (nConta.GMESA.idStatus == (int) StatusMesa.Ocupada) //status ocupada
                {
                    //se a conta estiver aberta, executa trasnferencia dos itens
                    if (nConta.idStatus == 1) //status conta: aberta
                    {
                        int itens = nConta.ACONTITEM.OrderBy(a => a.nuItem).Last().nuItem;

                        foreach (ACONTITEM item in contaAtual.ACONTITEM.Where(r => r.idStatus != 5))
                            //não pega itens transferidos
                        {
                            var novoItem = new ACONTITEM
                                               {
                                                   idEmpresa = item.idEmpresa,
                                                   idFilial = item.idFilial,
                                                   idConta = nConta.idConta,
                                                   idProduto = item.idProduto,
                                                   adicional = item.adicional,
                                                   desconto = item.desconto,
                                                   idStatus = item.idStatus,
                                                   idVen = item.idVen,
                                                   nuItem = itens + 1
                                               };
                            novoItem.nuItemPai = novoItem.nuItem + (item.nuItemPai - item.nuItem);
                            novoItem.preco = item.preco;
                            novoItem.opcao = item.opcao;
                            novoItem.produzido = item.produzido;
                            novoItem.impresso = item.impresso;
                            novoItem.quantidade = item.quantidade;
                            novoItem.txtObs = item.txtObs;
                            novoItem.dataInclusao = item.dataInclusao;

                            nConta.ACONTITEM.Add(novoItem);

                            #region Gravando um histórico da tranferencia

                            var hist = new AHISTTRANS
                                           {
                                               idHistorico = Contexto.GerarId("AHISTTRANS"),
                                               idEmpresa = Memoria.Empresa,
                                               idFilial = Memoria.Filial,
                                               codusuario = Memoria.Codusuario,
                                               codvendedor = Memoria.Vendedor,
                                               idProduto = item.idProduto,
                                               contaOrigem = contaAtual.idConta,
                                               contaDestino = nConta.idConta,
                                               mesaOrigem = contaAtual.nuMesa,
                                               mesaDestino = nConta.nuMesa,
                                               data = DateTime.Now
                                           };


                            Contexto.Atual.AHISTTRANS.AddObject(hist);

                            #endregion

                            itens++;
                        }

                        contaAtual.idStatus = 4; //Transferido
                        contaAtual.GMESA.idStatus = 2; //Livre

                        foreach (AASSOCIACAO assoc in contaAtual.AASSOCIACAO)
                        {
                            assoc.GMESA.idStatus = (int) StatusMesa.Livre;
                        }

                        contaAtual.AASSOCIACAO.Clear();


                        Contexto.Atual.ObjectStateManager.ChangeObjectState(contaAtual, EntityState.Modified);
                        Contexto.Atual.ObjectStateManager.ChangeObjectState(nConta, EntityState.Modified);

                        #region Log de Operacao

                        Memoria.LogMesa = contaAtual.nuMesa;
                        Memoria.LogAcao =
                            "Transferência da conta toda para uma mesa já ocupada, nova mesa ocupada e mesa antiga livre, e todas mesas associadas livres";
                        Memoria.LogCodUsuario = Memoria.Codusuario;
                        Memoria.LogData = DateTime.Now;
                        Memoria.LogConta = contaAtual.idConta;
                        Memoria.LogContaDestino = nConta.idConta;
                        Memoria.LogMesaDestino = nConta.nuMesa;

                        var log = new LogDAL();

                        log.Criar();

                        #endregion

                        Contexto.Atual.SaveChanges();

                        return true;
                    }
                    Memoria.MsgGlobal =
                        "Não é possível transferir para a mesa informada, pois a mesma esta com a pre-conta " +
                        nConta.ASTATCONTA.descricao;
                    return false;
                }
                Memoria.MsgGlobal =
                    "Não é possível transferir para a mesa informada pois a mesma esta com o status " +
                    nConta.GMESA.GSTATMESA.descricao;
                return false;

                #endregion
            }
            else
            {
                #region Tipo conta = B

                var act = new ACONTA
                              {
                                  idEmpresa = Memoria.Empresa,
                                  idFilial = Memoria.Filial,
                                  idConta = Convert.ToInt32(destino)
                              };

                act = Buscar(act);

                //se a conta estiver aberta, executa trasnferencia dos itens
                if (act.idStatus == 1) //status conta: aberta
                {
                    int itens = 0;
                    if (act.ACONTITEM.OrderBy(a => a.nuItem).Any())
                        itens = act.ACONTITEM.OrderBy(a => a.nuItem).Last().nuItem;

                    foreach (ACONTITEM item in contaAtual.ACONTITEM.Where(r => r.idStatus != 5))
                        //não pega itens transferidos
                    {
                        var novoItem = new ACONTITEM
                                           {
                                               idEmpresa = item.idEmpresa,
                                               idFilial = item.idFilial,
                                               idConta = act.idConta,
                                               idProduto = item.idProduto,
                                               adicional = item.adicional,
                                               desconto = item.desconto,
                                               idStatus = item.idStatus,
                                               idVen = item.idVen,
                                               nuItem = itens + 1
                                           };
                        novoItem.nuItemPai = novoItem.nuItem + (item.nuItemPai - item.nuItem);
                        novoItem.preco = item.preco;
                        novoItem.opcao = item.opcao;
                        novoItem.produzido = item.produzido;
                        novoItem.impresso = item.impresso;
                        novoItem.quantidade = item.quantidade;
                        novoItem.txtObs = item.txtObs;
                        novoItem.dataInclusao = item.dataInclusao;

                        act.ACONTITEM.Add(novoItem);

                        #region Gravando um histórico da tranferencia

                        var hist = new AHISTTRANS
                                       {
                                           idHistorico = Contexto.GerarId("AHISTTRANS"),
                                           idEmpresa = Memoria.Empresa,
                                           idFilial = Memoria.Filial,
                                           codusuario = Memoria.Codusuario,
                                           codvendedor = Memoria.Vendedor,
                                           idProduto = item.idProduto,
                                           contaOrigem = contaAtual.idConta,
                                           mesaOrigem = null,
                                           mesaDestino = null,
                                           data = DateTime.Now
                                       };

                        //hist.contaDestino = nConta.idConta;


                        Contexto.Atual.AHISTTRANS.AddObject(hist);

                        #endregion

                        itens++;
                    }

                    contaAtual.idStatus = 4; //Transferido

                    Contexto.Atual.ObjectStateManager.ChangeObjectState(contaAtual, EntityState.Modified);
                    Contexto.Atual.ObjectStateManager.ChangeObjectState(act, EntityState.Modified);

                    #region Log de Operacao

                    Memoria.LogMesa = null;
                    Memoria.LogAcao =
                        "Transferência da comanda toda para outra comanda";
                    Memoria.LogCodUsuario = Memoria.Codusuario;
                    Memoria.LogData = DateTime.Now;
                    Memoria.LogConta = contaAtual.idConta;
                    Memoria.LogContaDestino = act.idConta;
                    Memoria.LogMesaDestino = null;

                    var log = new LogDAL();

                    log.Criar();

                    #endregion

                    Contexto.Atual.SaveChanges();

                    return true;
                }
                Memoria.MsgGlobal =
                    "Não é possível transferir para a comanda informada, pois a mesma esta com a pre-conta " +
                    act.ASTATCONTA.descricao;
                return false;

                #endregion
            }
        }

        //TRANSFERENCIA PARCIAL
        public bool Transferir(ACONTA contaAtual, int destino, List<ACONTITEM> itens)
        {
            foreach (ALOG al in contaAtual.ALOG)
            {
                if (al.contaDestino == 0)
                    al.contaDestino = null;

                if (al.mesaDestino == 0)
                    al.mesaDestino = null;
            }

            if (Memoria.TipoConta == TipoConta.Mesa)
            {
                /* Logica dessa transferencia:
                 * 
                 * destino: numero da mesa de destino dos itens
                 * 
                 * se a mesa destino estiver LIVRE, deve se criar uma nova conta e inserir os itens da transferencia
                 * se a mesa destino estiver OCUPADA, deve se adicionar os itens da trasnferencia na conta da mesa
                 * 
                 * para ambos os casos os itens da conta atual devem ser marcados como transferidos: STATUS = 5
                 * 
                 * 
                 */

                #region Tipo conta = M

                var ms = new MesaDAL();

                //Pega a conta da mesa de destino
                ACONTA nConta = BuscarPorMesa(destino);

                //se a nova mesa estiver livre
                if (nConta == null)
                {
                    #region CRIAR A NOVA CONTA

                    nConta = new ACONTA
                                 {
                                     idConta = Contexto.GerarId("ACONTA"),
                                     nuMesa = destino,
                                     idEmpresa = Memoria.Empresa,
                                     idFilial = Memoria.Filial,
                                     dataInclusao = DateTime.Now,
                                     idStatus = (int) StatusConta.Aberta,
                                     pessoas = 1,
                                     servico = true,
                                     tpConta = TipoConta.Mesa.GetStringValue()
                                 };

                    if (Memoria.Codusuario != "")
                        nConta.usuarioAbertura = Memoria.Codusuario;

                    nConta.vendedorAbertura = Memoria.Vendedor;

                    #endregion

                    var novaMesa = new GMESA {idEmpresa = Memoria.Empresa, idFilial = Memoria.Filial, nuMesa = destino};

                    //busca dados da nova mesa, para a qual a conta será transferida
                    novaMesa = ms.Buscar(novaMesa);

                    //coloca a nova mesa como ocupada
                    novaMesa.idStatus = 1; //Ocupada

                    int cont = 0;

                    #region Cria os novos itens e adiciona a nova conta

                    foreach (ACONTITEM it in itens.OrderBy(r => r.nuItem))
                    {
                        cont++;

                        var n = new ACONTITEM
                                    {
                                        adicional = it.adicional,
                                        dataDesconto = it.dataDesconto,
                                        dataInclusao = it.dataInclusao,
                                        desconto = it.desconto,
                                        idConta = nConta.idConta,
                                        idEmpresa = nConta.idEmpresa,
                                        idFilial = nConta.idFilial,
                                        idProduto = it.idProduto,
                                        idStatus = it.idStatus,
                                        idVen = it.idVen,
                                        impresso = it.impresso,
                                        nuItem = cont
                                    };
                        n.nuItemPai = n.nuItem + (it.nuItemPai - it.nuItem);
                        n.opcao = it.opcao;
                        n.preco = it.preco;
                        n.produzido = it.produzido;
                        n.quantidade = it.quantidade;
                        n.txtObs = it.txtObs;
                        n.usuarioDesconto = it.usuarioDesconto;
                        n.vendedorDesconto = it.vendedorDesconto;

                        #region Historio Trasnferencia

                        var hist = new AHISTTRANS
                                       {
                                           idHistorico = Contexto.GerarId("AHISTTRANS"),
                                           idEmpresa = Memoria.Empresa,
                                           idFilial = Memoria.Filial,
                                           codusuario = Memoria.Codusuario,
                                           codvendedor = Memoria.Vendedor,
                                           idProduto = it.idProduto,
                                           contaOrigem = contaAtual.idConta,
                                           contaDestino = nConta.idConta,
                                           mesaOrigem = contaAtual.nuMesa,
                                           mesaDestino = nConta.nuMesa,
                                           data = DateTime.Now
                                       };

                        Contexto.Atual.AHISTTRANS.AddObject(hist);

                        #endregion

                        foreach (EOBSERVACAO eobservacao in it.EOBSERVACAO)
                        {
                            EOBSERVACAO novoOBS =
                                Contexto.Atual.EOBSERVACAO.SingleOrDefault(a => a.idObs == eobservacao.idObs);

                            n.EOBSERVACAO.Add(novoOBS);
                        }

                        nConta.ACONTITEM.Add(n);

                        //Marca item origem com transferido
                        it.idStatus = 5;
                    }

                    #endregion

                    Memoria.LogConta = contaAtual.idConta;
                    Memoria.LogMesa = contaAtual.nuMesa;
                    Memoria.LogContaDestino = nConta.idConta;
                    Memoria.LogMesaDestino = nConta.nuMesa;

                    //cria a nova conta com os itens transferidos
                    return Criar(nConta, true);
                }
                else if (nConta.GMESA.idStatus == (int) StatusMesa.Ocupada) //status ocupada
                {
                    //se a conta estiver aberta, executa trasnferencia dos itens
                    if (nConta.idStatus == 1) //status conta: aberta
                    {
                        int cont = nConta.ACONTITEM.OrderBy(a => a.nuItem).Last().nuItem;

                        #region Cria os novos itens e adiciona a nova conta

                        foreach (ACONTITEM it in itens.OrderBy(r => r.nuItem))
                        {
                            cont++;

                            var n = new ACONTITEM
                                        {
                                            adicional = it.adicional,
                                            dataDesconto = it.dataDesconto,
                                            dataInclusao = it.dataInclusao,
                                            desconto = it.desconto,
                                            idConta = nConta.idConta,
                                            idEmpresa = nConta.idEmpresa,
                                            idFilial = nConta.idFilial,
                                            idProduto = it.idProduto,
                                            idStatus = it.idStatus,
                                            idVen = it.idVen,
                                            impresso = it.impresso,
                                            nuItem = cont
                                        };
                            n.nuItemPai = n.nuItem + (it.nuItemPai - it.nuItem);
                            n.opcao = it.opcao;
                            n.preco = it.preco;
                            n.produzido = it.produzido;
                            n.quantidade = it.quantidade;
                            n.txtObs = it.txtObs;
                            n.usuarioDesconto = it.usuarioDesconto;
                            n.vendedorDesconto = it.vendedorDesconto;

                            #region Historio Trasnferencia

                            var hist = new AHISTTRANS
                                           {
                                               idHistorico = Contexto.GerarId("AHISTTRANS"),
                                               idEmpresa = Memoria.Empresa,
                                               idFilial = Memoria.Filial,
                                               codusuario = Memoria.Codusuario,
                                               codvendedor = Memoria.Vendedor,
                                               idProduto = it.idProduto,
                                               contaOrigem = contaAtual.idConta,
                                               contaDestino = nConta.idConta,
                                               mesaOrigem = contaAtual.nuMesa,
                                               mesaDestino = nConta.nuMesa,
                                               data = DateTime.Now
                                           };

                            Contexto.Atual.AHISTTRANS.AddObject(hist);

                            #endregion

                            foreach (EOBSERVACAO eobservacao in it.EOBSERVACAO)
                            {
                                EOBSERVACAO novoOBS =
                                    Contexto.Atual.EOBSERVACAO.SingleOrDefault(a => a.idObs == eobservacao.idObs);

                                n.EOBSERVACAO.Add(novoOBS);
                            }

                            nConta.ACONTITEM.Add(n);

                            //Marca item origem com transferido
                            it.idStatus = 5;
                        }

                        #endregion

                        Memoria.LogConta = contaAtual.idConta;
                        Memoria.LogMesa = contaAtual.nuMesa;
                        Memoria.LogContaDestino = nConta.idConta;
                        Memoria.LogMesaDestino = nConta.nuMesa;

                        //cria a nova conta com os itens transferidos
                        return Atualizar(nConta, true);
                    }
                    else
                    {
                        Memoria.MsgGlobal =
                            "Não é possível transferir para a mesa informada, pois a mesma esta com a pre-conta " +
                            nConta.ASTATCONTA.descricao;
                        return false;
                    }
                }
                else
                {
                    Memoria.MsgGlobal =
                        "Não é possível transferir para a mesa informada pois a mesma esta com o status " +
                        nConta.GMESA.GSTATMESA.descricao;
                    return false;
                }

                #endregion
            }
            else
            {
                #region Tipo conta = B

                var act = new ACONTA
                              {
                                  idEmpresa = Memoria.Empresa,
                                  idFilial = Memoria.Filial,
                                  idConta = Convert.ToInt32(destino)
                              };

                act = Buscar(act);

                //se a conta estiver aberta, executa trasnferencia dos itens
                if (act.idStatus == 1) //status conta: aberta
                {
                    int cont = 0;
                    if (act.ACONTITEM.OrderBy(a => a.nuItem).Any())
                        cont = act.ACONTITEM.OrderBy(a => a.nuItem).Last().nuItem;

                    foreach (ACONTITEM item in contaAtual.ACONTITEM.Where(r => r.idStatus != 5))
                        //não pega itens transferidos
                    {
                        var novoItem = new ACONTITEM
                                           {
                                               idEmpresa = item.idEmpresa,
                                               idFilial = item.idFilial,
                                               idConta = act.idConta,
                                               idProduto = item.idProduto,
                                               adicional = item.adicional,
                                               desconto = item.desconto,
                                               idStatus = item.idStatus,
                                               idVen = item.idVen,
                                               nuItem = cont + 1
                                           };
                        novoItem.nuItemPai = novoItem.nuItem + (item.nuItemPai - item.nuItem);
                        novoItem.preco = item.preco;
                        novoItem.opcao = item.opcao;
                        novoItem.produzido = item.produzido;
                        novoItem.impresso = item.impresso;
                        novoItem.quantidade = item.quantidade;
                        novoItem.txtObs = item.txtObs;
                        novoItem.dataInclusao = item.dataInclusao;

                        act.ACONTITEM.Add(novoItem);

                        #region Gravando um histórico da tranferencia

                        var hist = new AHISTTRANS
                                       {
                                           idHistorico = Contexto.GerarId("AHISTTRANS"),
                                           idEmpresa = Memoria.Empresa,
                                           idFilial = Memoria.Filial,
                                           codusuario = Memoria.Codusuario,
                                           codvendedor = Memoria.Vendedor,
                                           idProduto = item.idProduto,
                                           contaOrigem = contaAtual.idConta,
                                           contaDestino = act.idConta,
                                           mesaOrigem = null,
                                           mesaDestino = null,
                                           data = DateTime.Now
                                       };


                        Contexto.Atual.AHISTTRANS.AddObject(hist);

                        #endregion

                        cont++;
                    }

                    contaAtual.idStatus = 4; //Transferido

                    Contexto.Atual.ObjectStateManager.ChangeObjectState(contaAtual, EntityState.Modified);
                    Contexto.Atual.ObjectStateManager.ChangeObjectState(act, EntityState.Modified);

                    Memoria.LogMesa = null;
                    Memoria.LogAcao =
                        "Transferência da comanda toda para outra comanda";
                    Memoria.LogCodUsuario = Memoria.Codusuario;
                    Memoria.LogData = DateTime.Now;
                    Memoria.LogConta = contaAtual.idConta;
                    Memoria.LogContaDestino = act.idConta;
                    Memoria.LogMesaDestino = null;

                    var log = new LogDAL();

                    log.Criar();

                    Contexto.Atual.SaveChanges();

                    return true;
                }
                else
                {
                    Memoria.MsgGlobal =
                        "Não é possível transferir para a comanda informada, pois a mesma esta com a pre-conta " +
                        act.ASTATCONTA.descricao;
                    return false;
                }

                #endregion
            }
        }

        public bool FecharConta(ACONTA co)
        {
            foreach (ALOG al in co.ALOG)
            {
                if (al.contaDestino == 0)
                    al.contaDestino = null;

                if (al.mesaDestino == 0)
                    al.mesaDestino = null;
            }


            //criar movimento de venda
            var venda = new CMOVIMENTO
                            {
                                idEmpresa = co.idEmpresa,
                                idFilial = co.idFilial,
                                idMov = Contexto.GerarId("CMOVIMENTO"),
                                codTipoMov = "V"
                            };

            //pegar itens agrupados por id
            var itensAgrupados = from p in co.ACONTITEM
                                 group p by p.idProduto
                                 into g
                                 select new
                                            {
                                                produto = g.First().EPRODUTO,
                                                quantidade = g.Sum(r => r.quantidade),
                                                g.First().preco,
                                                desconto = g.Sum(r => r.desconto),
                                            };

            int cont = 1;
            //pegar tabela de ficha técnica de cada produto
            foreach (var itbaixa in itensAgrupados)
            {
                var itMov = new CITEMMOV
                                {
                                    CMOVIMENTO = venda,
                                    idEmpresa = venda.idEmpresa,
                                    idFilial = venda.idFilial,
                                    idMov = venda.idMov,
                                    idProduto = itbaixa.produto.idProduto,
                                    sequencialMov = cont
                                };

                venda.CITEMMOV.Add(itMov);


                //pega os insumos que devem ser baixados para o produto atual
                foreach (EPRDLISTA prds in itbaixa.produto.EPRDLISTA)
                {
                    //decimal quantidade = itbaixa.quantidade * prds.Qtde;

                    /* para cada item dessa foreach:
                     *  1- converter a quantidade da unidade de venda em unidade de controle
                     *  2- executar baixa da quantidade calculada na tabela de ECTESTOQUE
                     * * */

                    EUNIDADE unidade = Contexto.Atual.EUNIDADE.SingleOrDefault(r => r.codUnd == prds.EPRODUTO1.undVenda);

                    if (unidade != null)
                    {
                        //decimal fatorDecimal = Convert.ToDecimal(unidade.fatorConversao);
                        //decimal qtbBaixa = quantidade / fatorDecimal;
                    }


                    EPRDLISTA prds1 = prds;

                    IQueryable<ECTESTOQUE> estoques = from p in Contexto.Atual.ECTESTOQUE
                                                      where p.idEmpresa == venda.idEmpresa
                                                            && p.idFilial == venda.idFilial
                                                            && p.idProduto == prds1.EPRODUTO1.idProduto
                                                      orderby p.idCtEstoque ascending
                                                      select p;

                    ECTESTOQUE ultEstoque = estoques.FirstOrDefault() ?? new ECTESTOQUE
                                                                             {
                                                                                 idEmpresa = Memoria.Empresa,
                                                                                 idFilial = Memoria.Filial,
                                                                                 undControle =
                                                                                     prds.EPRODUTO1.undControle,
                                                                                 qtdeAtual = 0,
                                                                                 custoMedio = 0,
                                                                                 vlrTotal = 0
                                                                             };

                    var novoEstoque = new ECTESTOQUE
                                          {
                                              idEmpresa = ultEstoque.idEmpresa,
                                              idFilial = ultEstoque.idFilial,
                                              idCtEstoque = Contexto.GerarId("ECTESTOQUE"),
                                              idMov = venda.idMov,
                                              sequencialMov = cont,
                                              idProduto = prds.EPRODUTO1.idProduto,
                                              dataMovimento = DateTime.Now,
                                              undControle = ultEstoque.undControle,
                                              idLocDest = ultEstoque.idLoc,
                                              idFilialLocDest = ultEstoque.idFilialLoc,
                                              qtdeInicial = ultEstoque.qtdeAtual,
                                              vlrInicial = ultEstoque.vlrTotal,
                                              qtdeEntrada = 0,
                                              vlrUnitEntrada = 0,
                                              qtdeSaida = itbaixa.quantidade,
                                              vlrUnitSaida = ultEstoque.custoMedio
                                          };

                    novoEstoque.qtdeAtual = novoEstoque.qtdeInicial - novoEstoque.qtdeSaida;
                    novoEstoque.vlrTotal = ultEstoque.vlrTotal + (novoEstoque.qtdeEntrada*novoEstoque.vlrUnitEntrada) -
                                           (novoEstoque.qtdeSaida*novoEstoque.vlrUnitSaida);

                    novoEstoque.custoMedio = novoEstoque.vlrTotal/novoEstoque.qtdeAtual;


                    Contexto.Atual.AddToECTESTOQUE(novoEstoque);
                }
                cont++;
            }

            Contexto.Atual.AddToCMOVIMENTO(venda);

            //mudar status da conta para fechada
            co.idStatus = 2;

            if (Memoria.TipoConta == TipoConta.Mesa)
            {
                //mudar status da mesa livre
                co.GMESA.idStatus = 2;

                foreach (AASSOCIACAO a in co.AASSOCIACAO)
                {
                    a.GMESA.idStatus = 2;
                    Contexto.Atual.ObjectStateManager.ChangeObjectState(a.GMESA, EntityState.Modified);
                }
            }

            Contexto.Atual.ObjectStateManager.ChangeObjectState(co, EntityState.Modified);
            Contexto.Atual.SaveChanges();

            if (Memoria.TipoConta == TipoConta.Mesa)
            {
                //Memoria.LogMesa = co.nuMesa;
                Memoria.LogAcao = "Fecha conta e mesa fica livre";
                Memoria.LogCodUsuario = Memoria.Codusuario;
                Memoria.LogData = DateTime.Now;
                Memoria.LogConta = co.idConta;
                Memoria.LogContaDestino = null;
                Memoria.LogMesaDestino = null;
            }
            else
            {
                Memoria.LogMesa = null;
                Memoria.LogAcao = "Fecha conta";
                Memoria.LogCodUsuario = Memoria.Codusuario;
                Memoria.LogData = DateTime.Now;
                Memoria.LogConta = co.idConta;
                Memoria.LogContaDestino = null;
                Memoria.LogMesaDestino = null;
            }

            var log = new LogDAL();

            log.Criar();

            Contexto.Atual.SaveChanges();

            return true;
        }

        public bool Cancelar(ACONTA obj)
        {
            foreach (ALOG al in obj.ALOG)
            {
                if (al.contaDestino == 0)
                    al.contaDestino = null;

                if (al.mesaDestino == 0)
                    al.mesaDestino = null;
            }

            obj.idStatus = 5; //Cancelada

            if (Memoria.TipoConta == TipoConta.Mesa)
            {
                obj.GMESA.idStatus = (int) StatusMesa.Livre; //Livre

                foreach (AASSOCIACAO a in obj.AASSOCIACAO)
                {
                    a.GMESA.idStatus = (int) StatusMesa.Livre;
                    Contexto.Atual.ObjectStateManager.ChangeObjectState(a.GMESA, EntityState.Modified);
                }
            }

            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();

            if (Memoria.TipoConta == TipoConta.Mesa)
            {
                //Memoria.LogMesa = obj.nuMesa;
                Memoria.LogAcao = "Cancela conta e mesa fica livre";
                Memoria.LogCodUsuario = Memoria.Codusuario;
                Memoria.LogData = DateTime.Now;
                Memoria.LogConta = obj.idConta;
                Memoria.LogContaDestino = null;
                Memoria.LogMesaDestino = null;
            }
            else
            {
                Memoria.LogMesa = null;
                Memoria.LogAcao = "Cancela conta";
                Memoria.LogCodUsuario = Memoria.Codusuario;
                Memoria.LogData = DateTime.Now;
                Memoria.LogConta = obj.idConta;
                Memoria.LogContaDestino = null;
                Memoria.LogMesaDestino = null;
            }

            var log = new LogDAL();

            log.Criar();

            Contexto.Atual.SaveChanges();

            return true;
        }

        public string VerificaVendedor(ACONTA obj)
        {
            string str = obj.vendedorAbertura != null ? obj.GVENDEDOR.nome : obj.GUSUARIO.nome;

            return str;
        }

        public bool ValidarMesaAssociada(ACONTA obj, string nuMesa)
        {
            int mesa = Convert.ToInt32(nuMesa);

            return
                Contexto.Atual.ACONTA.Any(a => a.AASSOCIACAO.Any(r => r.nuMesa == mesa && r.idConta == obj.idConta));
        }

        public bool JuntarMesa(ACONTA conta, int nuMesa)
        {
            /* LOGICA DA JUNCAO DE MESAS
             * 
             * Se a mesa destino estiver livre, deve-se criar um registro na AASSOCIACAO e marcar a mesa destino como ocupada
             * 
             * Se a mesa destino estiver ocupada, deve-ser criar um registro na ASSOCIACAO, fazer a transferencia total da conta da mesa destino
             * para a conta da mesa atual, e marcar a mesa destino como ocupada
             * 
             */

            if (conta.nuMesa != nuMesa) //verifica se mesa destino é igual a mesa atual
            {
                if (conta.AASSOCIACAO.All(r => r.nuMesa != nuMesa))
                    // verifica se a mesa destino já esta unida com a mesa atual
                {
                    ACONTA nConta = BuscarPorMesa(nuMesa); // pega a conta da mesa destino

                    if (nConta != null) //se a mesa destino estiver ocupada
                    {
                        if (nConta.GMESA.idStatus != (int) StatusMesa.Bloqueada)
                            // se a mesa destino nao estiver bloqueada
                        {
                            List<AASSOCIACAO> mesasDestino = nConta.AASSOCIACAO.ToList();

                            foreach (AASSOCIACAO massoc in mesasDestino)
                            {
                                var nassoc = new AASSOCIACAO
                                                 {
                                                     idEmpresa = Memoria.Empresa,
                                                     idFilial = Memoria.Filial,
                                                     idConta = conta.idConta
                                                 };

                                if (conta.AASSOCIACAO.Count > 0)
                                    nassoc.nuAssociacao =
                                        conta.AASSOCIACAO.OrderByDescending(r => r.nuAssociacao).First().nuAssociacao +
                                        1;
                                else
                                    nassoc.nuAssociacao = 1;

                                nassoc.nuMesa = massoc.nuMesa;

                                Contexto.Atual.AddToAASSOCIACAO(nassoc); //cria associacao de mesas
                            }

                            if (conta.nuMesa != null && Transferir(nConta, conta.nuMesa.Value))
                                // transfere os itens da mesa destino para a nova mesa
                            {
                                var assoc = new AASSOCIACAO
                                                {
                                                    idEmpresa = Memoria.Empresa,
                                                    idFilial = Memoria.Filial,
                                                    idConta = conta.idConta
                                                };

                                if (conta.AASSOCIACAO.Count > 0)
                                    assoc.nuAssociacao =
                                        conta.AASSOCIACAO.OrderByDescending(r => r.nuAssociacao).First().nuAssociacao +
                                        1;
                                else
                                    assoc.nuAssociacao = 1;

                                assoc.nuMesa = nConta.nuMesa;

                                Contexto.Atual.AddToAASSOCIACAO(assoc); //cria associacao de mesas

                                nConta.GMESA.idStatus = (int) StatusMesa.Ocupada;

                                Contexto.Atual.SaveChanges();

                                foreach (AASSOCIACAO s in conta.AASSOCIACAO)
                                {
                                    s.GMESA.idStatus = (int) StatusMesa.Ocupada;
                                }

                                conta.pessoas += nConta.pessoas;

                                Contexto.Atual.SaveChanges();

                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            Memoria.MsgGlobal = "A mesa informada está bloqueada.";
                            return false;
                        }
                    }
                    else
                    {
                        var assoc = new AASSOCIACAO
                                        {
                                            idEmpresa = Memoria.Empresa,
                                            idFilial = Memoria.Filial,
                                            idConta = conta.idConta
                                        };

                        if (conta.AASSOCIACAO.Count > 0)
                            assoc.nuAssociacao =
                                conta.AASSOCIACAO.OrderByDescending(r => r.nuAssociacao).First().nuAssociacao + 1;
                        else
                            assoc.nuAssociacao = 1;

                        assoc.nuMesa = nuMesa;

                        Contexto.Atual.AddToAASSOCIACAO(assoc); //cria associacao de mesas

                        var c = new MesaDAL();
                        GMESA mesa = c.Buscar(nuMesa);
                        mesa.idStatus = (int) StatusMesa.Ocupada;

                        Contexto.Atual.SaveChanges();

                        return true;
                    }
                }
                else
                {
                    Memoria.MsgGlobal = "Essa mesa já esta unida com a mesa atual.";
                    return false;
                }
            }
            else
            {
                Memoria.MsgGlobal = "A mesa informada não pode ser igual a mesa atual.";
                return false;
            }
        }

        public bool SepararMesa(ACONTA conta, int nuMesa)
        {
            AASSOCIACAO assoc = conta.AASSOCIACAO.FirstOrDefault(r => r.nuMesa == nuMesa);

            if (assoc != null)
            {
                assoc.GMESA.idStatus = (int) StatusMesa.Livre;

                conta.AASSOCIACAO.Remove(assoc);

                Contexto.Atual.SaveChanges();

                return true;
            }
            else
            {
                if (conta.nuMesa == nuMesa)
                {
                    if (conta.AASSOCIACAO.Count > 0)
                    {
                        //pega a mesa atual e coloca como livre
                        GMESA mesa = conta.GMESA;
                        mesa.idStatus = (int) StatusMesa.Livre;

                        //pega a primeira mesa associada e faz dela a mesa principal
                        AASSOCIACAO passoc = conta.AASSOCIACAO.OrderBy(r => r.nuAssociacao).First();

                        conta.nuMesa = passoc.nuMesa;

                        conta.AASSOCIACAO.Remove(passoc);

                        Contexto.Atual.SaveChanges();

                        return true;
                    }
                    else
                    {
                        Memoria.MsgGlobal = "A mesa informada não esta unida com nenhuma outra mesa";
                        return false;
                    }
                }
                else
                {
                    Memoria.MsgGlobal = "A mesa informada não esta unida com nenhuma outra mesa";
                    return false;
                }
            }
        }

        #region Métodos de Relacionados a Impressão

        public int ImprimirPreConta(ACONTA co, int idDocumento)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                // Reset the printer bws (NV images are not cleared)
                bw.Write(AsciiControlChars.Escape);
                bw.Write('@');

                bw.Write(AsciiControlChars.Newline);

                if (co.GMESA.AIMPRESSMESA.Count > 0)
                {
                    #region Calculo dos totais

                    //pega o total da conta dos itens que não foram cancelados
                    decimal total =
                        co.ACONTITEM.Where(r => r.idStatus != 2 && r.idStatus != 5).Sum(
                            r => (r.quantidade*r.preco) - r.desconto).Value;

                    decimal totalGeral1 =
                        co.ACONTITEM.Where(r => r.idStatus != 2 && r.idStatus != 5).Sum(r => (r.quantidade*r.preco));

                    Decimal totalDesc;

                    if (total < co.desconto)
                    {
                        totalDesc = Convert.ToDecimal(0.01);
                    }
                    else
                    {
                        totalDesc = total - Convert.ToDecimal(co.desconto);
                    }

                    //taxa de serviço
                    decimal servico = Convert.ToDecimal(0.1);

                    //total do servico
                    decimal totalServico = servico*totalGeral1;

                    //se não tiver serviço, o mesmo será igual a 0
                    if (!co.servico)
                    {
                        totalServico = 0;
                    }

                    //total de descontos
                    //decimal totalDesconto =
                    //    co.ACONTITEM.Where(r => r.idStatus != 2 && r.idStatus != 5).Sum(r => r.desconto).Value;

                    //quantidade de itens
                    decimal quantidadeItens =
                        co.ACONTITEM.Count(r => (r.idStatus != 2 && r.idStatus != 5) && r.opcao == false);

                    //permanencia
                    string permanencia = string.Format("{0:hh\\:mm\\:ss}", DateTime.Now.Subtract(co.dataInclusao));

                    //totalgeral = total + servico
                    decimal totalGeral = (totalServico + totalDesc);

                    //total por pessoa
                    string totalPessoa = string.Format("{0:c}", totalGeral/(co.pessoas == 0 ? 1 : co.pessoas));

                    #endregion

                    foreach (
                        AIMPRESSMESA imp in
                            co.GMESA.AIMPRESSMESA.Where(
                                r => r.AIMPRESSORA.imprimeProdutos == false && r.AIMPRESSORA.ativo == true))
                    {
                        #region Cabeçalho

                        bw.Write(AsciiControlChars.Escape); //Centraliza
                        bw.Write((char) 97);
                        bw.Write((char) 49);

                        bw.Write("CUPOM NAO FISCAL \n");

                        bw.Write(AsciiControlChars.Escape);
                        bw.Write((char) 97);
                        bw.Write((char) 48);

                        string vendedor = "Vendedor: " + Convert.ToString(Memoria.NomeVendedor);
                        bw.Write(Encoding.GetEncoding(0).GetBytes(vendedor));
                        bw.Write(AsciiControlChars.Newline);

                        string preconta = "PreConta Nro: " + co.idConta.ToString(CultureInfo.InvariantCulture);
                        bw.Write(preconta);
                        bw.Write(AsciiControlChars.Newline);

                        //bw.Write(AsciiControlChars.Escape);
                        bw.Write(Encoding.GetEncoding(0).GetBytes(new string('=', 40)));
                        bw.Write(AsciiControlChars.Newline);

                        string data = "DATA: " + DateTime.Now.ToString("dd/MM/yyyy") + "   HORA: " +
                                      DateTime.Now.ToString("HH:mm");
                        bw.Write(data);
                        bw.Write(AsciiControlChars.Newline);

                        bw.Write(AsciiControlChars.Escape); //centraliza
                        bw.Write((char) 97);
                        bw.Write((char) 49);

                        bw.Write(AsciiControlChars.Escape);
                        bw.Write((char) 0x57);
                        bw.Write((char) 0x01);

                        string mesaCartao = "MESA/CARTAO: " + co.nuMesa;
                        bw.Write(Encoding.GetEncoding(0).GetBytes(mesaCartao));

                        bw.Write(AsciiControlChars.Newline);
                        bw.Write(AsciiControlChars.Escape);
                        bw.Write((char) 0x57);
                        bw.Write((char) 0x00);

                        bw.Write(AsciiControlChars.Escape);
                        bw.Write((char) 97);
                        bw.Write((char) 48);

                        #endregion

                        #region Header da tabela de produtos

                        //bw.Write(AsciiControlChars.Escape);
                        bw.Write(Encoding.GetEncoding(0).GetBytes(new string('-', 40)));
                        bw.Write(AsciiControlChars.Newline);

                        const string tituloTabela = " QTDE     PRODUTO              P.UNIT   VALOR";
                        //bw.Write(AsciiControlChars.Escape);
                        bw.Write(Encoding.GetEncoding(0).GetBytes(tituloTabela));
                        bw.Write(AsciiControlChars.Newline);

                        //bw.Write(AsciiControlChars.Escape);
                        bw.Write(Encoding.GetEncoding(0).GetBytes(new string('-', 40)));
                        bw.Write(AsciiControlChars.Newline);

                        #endregion

                        var itens = from p in co.ACONTITEM
                                    where (p.idStatus != 2 && p.idStatus != 5) && p.opcao == false
                                    group p by
                                        new {p.idProduto, p.preco, p.desconto, p.EPRODUTO.undVenda, p.EPRODUTO.codigo}
                                    into g
                                    select new
                                               {
                                                   g.First().EPRODUTO.codigo,
                                                   g.First().EPRODUTO.nome,
                                                   desconto = g.Sum(r => r.desconto),
                                                   g.First().preco,
                                                   quantidade = g.Sum(r => r.quantidade),
                                                   unidade = g.First().EPRODUTO.undVenda,
                                                   nuItens = (from j in g
                                                              select j.nuItem).ToList()
                                               };

                        #region Produtos

                        string msg;
                        foreach (var ac in itens)
                        {
                            //formata a quantidade
                            msg = string.Format("{0:0}", ac.quantidade);
                            string linha = " " + msg + new string(' ', 9 - msg.Length);

                            //formata o nome do produto
                            string nomeProduto = ac.nome.ToUpper();
                            if (nomeProduto.Length > 21)
                            {
                                nomeProduto = nomeProduto.Substring(0, 21);
                            }
                            linha += nomeProduto + new string(' ', 22 - nomeProduto.Length);

                            //formata o preco total do item
                            var ac2 = ac;
                            decimal? totalOpcionais = co.ACONTITEM.Where(r => r.nuItemPai != null && r.opcao).Where(
                                r => r.nuItemPai == ac2.nuItens.First()).Sum(r => r.quantidade*r.preco);

                            var ac1 = ac;
                            decimal? totalOpcionaisGeral = co.ACONTITEM.Where(r => r.nuItemPai != null && r.opcao).Where
                                (
                                    r => r.nuItemPai != null && ac1.nuItens.Contains(r.nuItemPai.Value)).Sum(
                                        r => r.quantidade*r.preco);

                            //formata o preco unitario
                            msg = string.Format("{0:0.00}", ac.preco + totalOpcionais);
                            linha += msg + new string(' ', 9 - msg.Length);

                            decimal? totalItem = ((ac.preco*ac.quantidade) - ac.desconto) + totalOpcionaisGeral;
                            msg = " " + string.Format("{0:0.00}", totalItem);
                            linha += msg + new string(' ', 9 - msg.Length);

                            //    bw.Write(AsciiControlChars.Escape);
                            //manda linha para a impressora
                            bw.Write(Encoding.GetEncoding(0).GetBytes(linha));
                            bw.Write(AsciiControlChars.Newline);

                            if (ac.desconto != 0)
                            {
                                linha = new string(' ', 9) + "DESCONTO: " + string.Format("{0:0.00}", ac.desconto);
                                bw.Write(Encoding.GetEncoding(0).GetBytes(linha));
                                bw.Write(AsciiControlChars.Newline);
                            }
                        }

                        #endregion

                        //bw.Write(AsciiControlChars.Escape);
                        bw.Write(Encoding.GetEncoding(0).GetBytes(new string('-', 40)));
                        bw.Write(AsciiControlChars.Newline);

                        #region Totais

                        //bw.Write(AsciiControlChars.Escape);
                        string total1 = new string(' ', 10) + "TOTAL PRODUTOS : " + string.Format("{0:c}", total);
                        bw.Write(Encoding.GetEncoding(0).GetBytes(total1));
                        bw.Write(AsciiControlChars.Newline);

                        //bw.Write(AsciiControlChars.Escape);
                        string total3 = new string(' ', 10) + "DESCONTO: " + string.Format("{0:c}", co.desconto);
                        bw.Write(Encoding.GetEncoding(0).GetBytes(total3));
                        bw.Write(AsciiControlChars.Newline);

                        string total4 = new string(' ', 10) + "SERVICO: " + string.Format("{0:c}", totalServico);
                        bw.Write(Encoding.GetEncoding(0).GetBytes(total4));
                        bw.Write(AsciiControlChars.Newline);

                        bw.Write(AsciiControlChars.Escape);
                        bw.Write((char) 87);
                        bw.Write((char) 01);
                        string total2 = new string(' ', 5) + "TOTAL: " + string.Format("{0:c}", totalGeral);
                        bw.Write(Encoding.GetEncoding(0).GetBytes(total2));
                        bw.Write(AsciiControlChars.Newline);

                        bw.Write(AsciiControlChars.Escape);
                        bw.Write((char) 87);
                        bw.Write((char) 48);

                        bw.Write(AsciiControlChars.Newline);

                        #endregion

                        #region Rodapé de totais

                        msg = "QUANTIDADE DE ITENS: " + quantidadeItens.ToString(CultureInfo.InvariantCulture);

                        bw.Write(Encoding.GetEncoding(0).GetBytes(msg));
                        bw.Write(AsciiControlChars.Newline);

                        msg = "TOTAL DE PESSOAS: " + co.pessoas.ToString(CultureInfo.InvariantCulture);
                        bw.Write(Encoding.GetEncoding(0).GetBytes(msg));
                        bw.Write(AsciiControlChars.Newline);

                        msg = "TOTAL POR PESSOA: " + totalPessoa;
                        bw.Write(Encoding.GetEncoding(0).GetBytes(msg));
                        bw.Write(AsciiControlChars.Newline);

                        msg = "PERMANENCIA: " + permanencia;
                        bw.Write(Encoding.GetEncoding(0).GetBytes(msg));
                        bw.Write(AsciiControlChars.Newline);

                        #endregion

                        bw.Write(Encoding.GetEncoding(0).GetBytes(new string(' ', 40)));
                        bw.Write(AsciiControlChars.Newline);

                        bw.Write(Encoding.GetEncoding(0).GetBytes("CPF/CNPJ:____________________________"));
                        bw.Write(AsciiControlChars.Newline);

                        //Rodapé da impressão
                        bw.Write(AsciiControlChars.Newline);

                        bw.Write(Encoding.GetEncoding(0).GetBytes(new string('-', 40)));
                        bw.Write(AsciiControlChars.Newline);

                        bw.Write(AsciiControlChars.Escape); //Centraliza
                        bw.Write((char) 97);
                        bw.Write((char) 49);

                        bw.Write(Encoding.GetEncoding(0).GetBytes("ARTEBIT GOURMET " + Sistema.Versao));

                        bw.Write(AsciiControlChars.Newline);

                        bw.Write(Encoding.GetEncoding(0).GetBytes("WWW.ARTEBIT.COM.BR"));

                        bw.Write(AsciiControlChars.Newline);

                        bw.Write(AsciiControlChars.Escape);
                        bw.Write((char) 97);
                        bw.Write((char) 48);

                        bw.Write(AsciiControlChars.Escape); //Corte Parcial
                        bw.Write((char) 109);

                        bw.Flush();

                        PrintPreConta(imp.AIMPRESSORA.nome, ms.ToArray());
                    }
                }
            }

            return 1;
        }

        public int EnviaItemsProducao(ACONTA co, int idDocumento)
        {
            int retorno = 0;
            bool existeItemASerImpresso = false;

            IQueryable<AIMPRESSORA> impressoras = from p in Contexto.Atual.AIMPRESSMESA
                                                  where
                                                      p.AIMPRESSORA.imprimeProdutos == true && p.nuMesa == co.nuMesa &&
                                                      p.AIMPRESSORA.ativo == true
                                                  select p.AIMPRESSORA;

            foreach (AIMPRESSORA imp in impressoras)
            {
                List<ACONTITEM> itemsImpressao =
                    co.ACONTITEM.Where(r => (r.impresso == false || r.impresso == null) && r.idStatus == 1).Where(
                        aco => imp.AIMPRESSORAPRD.Any(r => r.idProduto == aco.idProduto)).ToList();

                if (itemsImpressao.Count > 0)
                {
                    using (var ms = new MemoryStream())
                    using (var bw = new BinaryWriter(ms))
                    {
                        // Reset the printer bws (NV images are not cleared)
                        bw.Write(AsciiControlChars.Escape);
                        bw.Write('@');

                        bw.Write(AsciiControlChars.Newline);

                        existeItemASerImpresso = true;

                        #region Cabeçalho

                        bw.Write(AsciiControlChars.Escape); //Centraliza
                        bw.Write((char) 97);
                        bw.Write((char) 49);

                        bw.Write(Encoding.GetEncoding(0).GetBytes("PRODUCAO"));

                        bw.Write(AsciiControlChars.Newline);

                        bw.Write(AsciiControlChars.Escape);
                        bw.Write((char) 97);
                        bw.Write((char) 48);

                        ACONTITEM item = itemsImpressao.FirstOrDefault();
                        if (item != null)
                        {
                            string vendedor = "Vendedor: " + item.GVENDEDOR.nome;
                            bw.Write(Encoding.GetEncoding(0).GetBytes(vendedor));
                        }
                        bw.Write(AsciiControlChars.Newline);

                        string preconta = "PreConta Nro: " + co.idConta.ToString(CultureInfo.InvariantCulture);
                        bw.Write(Encoding.GetEncoding(0).GetBytes(preconta));
                        bw.Write(AsciiControlChars.Newline);

                        bw.Write(Encoding.GetEncoding(0).GetBytes(new string('=', 40)));
                        bw.Write(AsciiControlChars.Newline);

                        string data = "DATA: " + DateTime.Now.ToString("dd/MM/yyyy") + "   HORA: " +
                                      DateTime.Now.ToString("HH:mm");

                        bw.Write(Encoding.GetEncoding(0).GetBytes(data));
                        bw.Write(AsciiControlChars.Newline);

                        bw.Write(AsciiControlChars.Escape); //centraliza
                        bw.Write((char) 97);
                        bw.Write((char) 49);

                        bw.Write(AsciiControlChars.Escape);
                        bw.Write((char) 0x57);
                        bw.Write((char) 0x01);

                        string mesaCartao = "MESA/CARTAO: " + co.nuMesa;
                        bw.Write(Encoding.GetEncoding(0).GetBytes(mesaCartao));

                        bw.Write(AsciiControlChars.Escape);
                        bw.Write((char) 0x57);
                        bw.Write((char) 0x00);

                        bw.Write(AsciiControlChars.Newline);

                        bw.Write(AsciiControlChars.Escape);
                        bw.Write((char) 97);
                        bw.Write((char) 48);

                        #endregion

                        #region Header da tabela de produtos

                        bw.Write(Encoding.GetEncoding(0).GetBytes(new string('-', 40)));
                        bw.Write(AsciiControlChars.Newline);

                        const string tituloTabela = " QTDE     PRODUTO                            ";
                        bw.Write(Encoding.GetEncoding(0).GetBytes(tituloTabela));
                        bw.Write(AsciiControlChars.Newline);

                        bw.Write(Encoding.GetEncoding(0).GetBytes(new string('-', 40)));
                        bw.Write(AsciiControlChars.Newline);

                        #endregion

                        var itens = from p in itemsImpressao
                                    select new
                                               {
                                                   p.EPRODUTO.codigo,
                                                   p.EPRODUTO.nome,
                                                   p.desconto,
                                                   p.preco,
                                                   p.quantidade,
                                                   unidade = p.EPRODUTO.undVenda,
                                                   obs = p.EOBSERVACAO,
                                                   txtobs = p.txtObs,
                                                   idPai = p.nuItemPai,
                                                   p.opcao,
                                                   p.adicional,
                                                   p.nuItem
                                               };

                        #region Produtos

                        foreach (var ac in itens.ToList().Where(r => r.opcao == false))
                        {
                            //formata a quantidade
                            string msg = string.Format("{0:0}", ac.quantidade);
                            string linha = " " + msg + new string(' ', 8 - msg.Length);

                            //formata o nome do produto
                            string nomePrd = ac.nome.ToUpper();
                            if (nomePrd.Length > 41)
                            {
                                nomePrd = nomePrd.Substring(0, 41);
                            }
                            linha += nomePrd;

                            if (linha.Length > 43)
                            {
                                linha = linha.Substring(0, 43);
                            }

                            bw.Write(Encoding.GetEncoding(0).GetBytes(linha));
                            bw.Write(AsciiControlChars.Newline);

                            if (ac.obs.Count > 0 || ac.txtobs != null)
                            {
                                foreach (EOBSERVACAO obs in ac.obs)
                                {
                                    linha = "           " + obs.descricao.ToUpper();
                                    bw.Write(Encoding.GetEncoding(0).GetBytes(linha));
                                    bw.Write(AsciiControlChars.Newline);
                                }

                                if (Convert.ToString(ac.txtobs) != "" && ac.txtobs != null)
                                {
                                    linha = "          " + ac.txtobs.ToUpper();

                                    bw.Write(Encoding.GetEncoding(0).GetBytes(linha));
                                    bw.Write(AsciiControlChars.Newline);
                                }
                            }

                            foreach (var opcao in itens.ToList().Where(r => r.opcao && r.idPai == ac.nuItem))
                            {
                                //formata a quantidade
                                //msg = string.Format("{0:0}", opcao.quantidade);
                                linha = "     >"; // + msg + new string(' ', 3 - msg.Length);

                                //formata o nome do produto
                                string nomeProduto = "Com " + opcao.nome.ToUpper();
                                if (nomeProduto.Length > 27)
                                {
                                    nomeProduto = nomeProduto.Substring(0, 28);
                                }

                                linha += nomeProduto + new string(' ', 28 - nomeProduto.Length);

                                bw.Write(Encoding.GetEncoding(0).GetBytes(linha));
                                bw.Write(AsciiControlChars.Newline);

                                if (opcao.obs.Count > 0 || opcao.txtobs != null)
                                {
                                    foreach (EOBSERVACAO obs in opcao.obs)
                                    {
                                        linha = "           " + obs.descricao.ToUpper();
                                        bw.Write(Encoding.GetEncoding(0).GetBytes(linha));
                                        bw.Write(AsciiControlChars.Newline);
                                    }

                                    if (Convert.ToString(ac.txtobs) != "")
                                    {
                                        linha = "          " + opcao.txtobs.ToUpper();
                                        bw.Write(Encoding.GetEncoding(0).GetBytes(linha));
                                        bw.Write(AsciiControlChars.Newline);
                                    }
                                }
                            }

                            bw.Write(AsciiControlChars.Newline);
                        }

                        #endregion

                        bw.Write(Encoding.GetEncoding(0).GetBytes(new string('-', 40)));
                        bw.Write(AsciiControlChars.Newline);

                        bw.Write(AsciiControlChars.Escape); //Centraliza
                        bw.Write((char) 97);
                        bw.Write((char) 49);

                        bw.Write(Encoding.GetEncoding(0).GetBytes("ARTEBIT GOURMET " + Sistema.Versao));

                        bw.Write(AsciiControlChars.Newline);

                        bw.Write(Encoding.GetEncoding(0).GetBytes("WWW.ARTEBIT.COM.BR"));

                        bw.Write(AsciiControlChars.Newline);

                        bw.Write(AsciiControlChars.Escape);
                        bw.Write((char) 97);
                        bw.Write((char) 48);

                        retorno = 1;

                        foreach (ACONTITEM aco in itemsImpressao)
                        {
                            aco.impresso = true;
                        }

                        bw.Write(AsciiControlChars.Escape); //Corte Parcial
                        bw.Write((char) 109);

                        bw.Flush();

                        PrintProducao(imp.nome, ms.ToArray());
                    }
                }
            }

            if (retorno == 0 && !existeItemASerImpresso)
            {
                retorno = 1;
            }

            return retorno;
        }

        private void PrintPreConta(string printerName, byte[] document)
        {
            var documentInfo = new NativeMethods.DOC_INFO_1
                                   {
                                       pDataType = "RAW",
                                       pDocName =
                                           "Impressao Artebit Gourmet - Pre Conta " +
                                           DateTime.Now.ToString(CultureInfo.InvariantCulture)
                                   };

            IntPtr printerHandle;

            if (NativeMethods.OpenPrinter(printerName.Normalize(), out printerHandle, IntPtr.Zero))
            {
                if (NativeMethods.StartDocPrinter(printerHandle, 1, documentInfo))
                {
                    byte[] managedData = document;
                    IntPtr unmanagedData = Marshal.AllocCoTaskMem(managedData.Length);
                    Marshal.Copy(managedData, 0, unmanagedData, managedData.Length);

                    if (NativeMethods.StartPagePrinter(printerHandle))
                    {
                        int bytesWritten;
                        NativeMethods.WritePrinter(
                            printerHandle,
                            unmanagedData,
                            managedData.Length,
                            out bytesWritten);
                        NativeMethods.EndPagePrinter(printerHandle);
                    }
                    else
                    {
                        using (var sw = new StreamWriter("C:\\logServico-Tipo1.txt", true))
                        {
                            sw.WriteLine("Erro ao Executar " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                            sw.WriteLine("Excecao: Nome Impressora: {0} , Exception: {1}", printerName,
                                         new Win32Exception());
                        }
                    }

                    Marshal.FreeCoTaskMem(unmanagedData);

                    NativeMethods.EndDocPrinter(printerHandle);
                }
                else
                {
                    using (var sw = new StreamWriter("C:\\logServico-Tipo1.txt", true))
                    {
                        sw.WriteLine("Erro ao Executar " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                        sw.WriteLine("Excecao: Nome Impressora: {0} , Exception: {1}", printerName, new Win32Exception());
                    }
                }

                NativeMethods.ClosePrinter(printerHandle);
            }
            else
            {
                using (var sw = new StreamWriter("C:\\logServico-Tipo1.txt", true))
                {
                    sw.WriteLine("Erro ao Executar " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    sw.WriteLine("Excecao: Nome Impressora: {0} , Exception: {1}", printerName, new Win32Exception());
                }
            }
        }

        private void PrintProducao(string printerName, byte[] document)
        {
            var documentInfo = new NativeMethods.DOC_INFO_1
                                   {
                                       pDataType = "RAW",
                                       pDocName =
                                           "Impressao Artebit Gourmet - Producao " +
                                           DateTime.Now.ToString(CultureInfo.InvariantCulture)
                                   };

            IntPtr printerHandle;

            if (NativeMethods.OpenPrinter(printerName.Normalize(), out printerHandle, IntPtr.Zero))
            {
                if (NativeMethods.StartDocPrinter(printerHandle, 1, documentInfo))
                {
                    byte[] managedData = document;
                    IntPtr unmanagedData = Marshal.AllocCoTaskMem(managedData.Length);
                    Marshal.Copy(managedData, 0, unmanagedData, managedData.Length);

                    if (NativeMethods.StartPagePrinter(printerHandle))
                    {
                        int bytesWritten;
                        NativeMethods.WritePrinter(
                            printerHandle,
                            unmanagedData,
                            managedData.Length,
                            out bytesWritten);
                        NativeMethods.EndPagePrinter(printerHandle);
                    }
                    else
                    {
                        using (var sw = new StreamWriter("C:\\logServico-Tipo2.txt", true))
                        {
                            sw.WriteLine("Erro ao Executar " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                            sw.WriteLine("Excecao: Nome Impressora: {0} , Exception: {1}", printerName,
                                         new Win32Exception());
                        }
                    }

                    Marshal.FreeCoTaskMem(unmanagedData);

                    NativeMethods.EndDocPrinter(printerHandle);
                }
                else
                {
                    using (var sw = new StreamWriter("C:\\logServico-Tipo2.txt", true))
                    {
                        sw.WriteLine("Erro ao Executar " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                        sw.WriteLine("Excecao: Nome Impressora: {0} , Exception: {1}", printerName, new Win32Exception());
                    }
                }

                NativeMethods.ClosePrinter(printerHandle);
            }
            else
            {
                using (var sw = new StreamWriter("C:\\logServico-Tipo2.txt", true))
                {
                    sw.WriteLine("Erro ao Executar " + DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    sw.WriteLine("Excecao: Nome Impressora: {0} , Exception: {1}", printerName, new Win32Exception());
                }
            }
        }

        public bool ImprimirPreContaFila(ACONTA obj)
        {
            var lista = new GFILAIMPRESSAO
                            {
                                idEmpresa = obj.idEmpresa,
                                idFilial = obj.idFilial,
                                impresso = false,
                                nuMesa = obj.nuMesa,
                                tipoImpressao = 1
                            };
            GVENDEDOR vend =
                Contexto.Atual.GVENDEDOR.FirstOrDefault(
                    r => r.idVen == Memoria.Vendedor && r.idEmpresa == Memoria.Empresa && r.idFilial == Memoria.Filial);
            if (vend != null)
                lista.nomeVendedor = vend.nome;
            lista.idDocumento = Contexto.GerarId("GFILAIMPRESSAO");

            Contexto.Atual.AddToGFILAIMPRESSAO(lista);
            Contexto.Atual.SaveChanges();
            return true;
        }

        #endregion

        #region Relatorio PreConta

        // Função que retorna a preconta em uma string
        public string RetornaRelatorio(ACONTA conta)
        {
            #region Calculo dos totais

            //pega o total da conta dos itens que não foram cancelados
            decimal total =
                conta.ACONTITEM.Where(r => r.idStatus != 2 && r.idStatus != 5).Sum(
                    r => (r.quantidade*r.preco) - r.desconto).Value;
            decimal totalGeral1 =
                conta.ACONTITEM.Where(r => r.idStatus != 2 && r.idStatus != 5).Sum(r => (r.quantidade*r.preco));

            Decimal totalDesc;

            if (total < conta.desconto)
            {
                totalDesc = Convert.ToDecimal(0.01);
            }
            else
            {
                totalDesc = total - Convert.ToDecimal(conta.desconto);
            }

            //taxa de serviço
            decimal servico = Convert.ToDecimal(0.1);

            //total do servico
            decimal totalServico = servico*totalGeral1;

            //se não tiver serviço, o mesmo será igual a 0
            if (!conta.servico)
            {
                totalServico = 0;
            }

            //total de descontos
            //decimal totalDesconto = conta.ACONTITEM.Where(r => r.idStatus != 2 && r.idStatus != 5).Sum(r => r.desconto).Value;

            //quantidade de itens
            decimal quantidadeItens =
                conta.ACONTITEM.Count(r => (r.idStatus != 2 && r.idStatus != 5) && r.opcao == false);

            //permanencia
            string permanencia = string.Format("{0:hh\\:mm\\:ss}", DateTime.Now.Subtract(conta.dataInclusao));

            //totalgeral = total + servico
            decimal totalGeral = (totalServico + totalDesc);

            //total por pessoa
            string totalPessoa = string.Format("{0:c}", totalGeral/(conta.pessoas == 0 ? 1 : conta.pessoas));

            #endregion

            var sb = new StringBuilder();

            sb.Append(new string(' ', 10) + "CUPOM NÃO FISCAL*"); //negrito | centralizado
            sb.Append("========================================*");

            sb.AppendFormat("VENDEDOR: " + VerificaVendedor(conta) + "*");
            sb.AppendFormat("PRECONTA NRO: " + conta.idConta.ToString(CultureInfo.InvariantCulture) + "*");
            sb.AppendFormat("Data: {0:dd/MM/yyyy}    Hora: {0:HH:mm}*", DateTime.Now);
            sb.AppendFormat("Hora: {0:HH:mm}*", DateTime.Now);
            sb.AppendFormat(new string(' ', 7) + "MESA/CARTÃO: " + conta.nuMesa + "*");

            #region Header da tabela de produtos

            sb.Append(new string('-', 100) + "*");
            const string tituloTabela = " QTDE   PRODUTO               P.UNIT   VALOR";
            sb.Append(tituloTabela + "*");
            sb.Append(new string('-', 100) + "*");

            #endregion

            var itens = from p in conta.ACONTITEM
                        where (p.idStatus != 2 && p.idStatus != 5) && p.opcao == false
                        group p by new {p.idProduto, p.preco, p.desconto, p.EPRODUTO.undVenda, p.EPRODUTO.codigo}
                        into g
                        select new
                                   {
                                       g.First().EPRODUTO.codigo,
                                       g.First().EPRODUTO.nome,
                                       desconto = g.Sum(r => r.desconto),
                                       g.First().preco,
                                       quantidade = g.Sum(r => r.quantidade),
                                       unidade = g.First().EPRODUTO.undVenda,
                                       nuItens = (from j in g
                                                  select j.nuItem).ToList()
                                   };

            #region Produtos

            string msg;

            foreach (var ac in itens)
            {
                //formata a quantidade
                msg = string.Format("{0:0}", ac.quantidade);
                string linha = " " + msg + new string(' ', 7 - msg.Length);

                //formata o nome do produto
                string nomeProduto = ac.nome.ToUpper();
                if (nomeProduto.Length > 21)
                {
                    nomeProduto = nomeProduto.Substring(0, 21);
                }
                linha += nomeProduto + new string(' ', 22 - nomeProduto.Length);

                //formata o preco total do item
                decimal? totalOpcionais =
                    conta.ACONTITEM.Where(r => r.nuItemPai != null && r.opcao).Where(
                        r => r.nuItemPai == ac.nuItens.First()).Sum(r => r.quantidade*r.preco);

                decimal? totalOpcionaisGeral =
                    conta.ACONTITEM.Where(r => r.nuItemPai != null && r.opcao).Where(
                        r => r.nuItemPai != null && ac.nuItens.Contains(r.nuItemPai.Value)).Sum(
                            r => r.quantidade*r.preco);

                //formata o preco unitario
                msg = string.Format("{0:0.00}", ac.preco + totalOpcionais);
                linha += msg + new string(' ', 9 - msg.Length);

                decimal? totalItem = ((ac.preco*ac.quantidade) - ac.desconto) + totalOpcionaisGeral;
                msg = " " + string.Format("{0:0.00}", totalItem);
                linha += msg + new string(' ', 7 - msg.Length);

                //manda linha para a impressora
                sb.Append(linha + "*");

                if (ac.desconto != 0)
                {
                    linha = new string(' ', 8) + "DESCONTO: " + string.Format("{0:0.00}", ac.desconto);
                    sb.Append(linha + "*");
                }
            }

            #endregion

            sb.Append(new string('-', 100) + "*");

            #region Totais

            string total1 = new string(' ', 10) + "TOTAL PRODUTOS : " + string.Format("{0:c}", total);
            sb.Append(total1 + "*");
            //retorno = MP1032.FormataTX(total1 + "*", 2, 0, 0, 0, 0);

            string total3 = new string(' ', 10) + "DESCONTO: " + string.Format("{0:c}", conta.desconto);
            sb.Append(total3 + "*");
            //retorno = MP1032.FormataTX(total3 + "*", 2, 0, 0, 0, 0);

            string total4 = new string(' ', 10) + "SERVIÇO: " + string.Format("{0:c}", totalServico);
            sb.Append(total4 + "*");
            //retorno = MP1032.FormataTX(total4 + "*", 2, 0, 0, 0, 0);

            string total2 = new string(' ', 7) + "TOTAL: " + string.Format("{0:c}", totalGeral);
            sb.Append(total2 + "*");
            //retorno = MP1032.FormataTX(total2 + "*", 2, 0, 0, 1, 0);
            sb.Append(new string(' ', 100) + "*");
            //retorno = MP1032.BematechTX(new string(' ', 40) + "*");

            #endregion

            #region Rodapé de totais

            msg = "QUANTIDADE DE ITENS: " + quantidadeItens.ToString(CultureInfo.InvariantCulture);
            sb.Append(msg + "*");
            //retorno = MP1032.BematechTX(msg + "*");

            msg = "TOTAL DE PESSOAS: " + conta.pessoas.ToString(CultureInfo.InvariantCulture);
            sb.Append(msg + "*");
            //retorno = MP1032.BematechTX(msg + "*");

            msg = "TOTAL POR PESSOA: " + totalPessoa;
            sb.Append(msg + "*");
            //retorno = MP1032.BematechTX(msg + "*");

            msg = "PERMANÊNCIA: " + permanencia;
            sb.Append(msg + "*");
            //retorno = MP1032.BematechTX(msg + "*");

            #endregion

            //Rodapé da impressão
            sb.Append(new string(' ', 100) + "*");
            //retorno = MP1032.BematechTX(new string(' ', 40) + "*");
            sb.Append(new string('-', 100) + "*");
            //retorno = MP1032.BematechTX(new string('-', 40) + "*");
            sb.Append(new string(' ', 10) + "ARTEBIT GOURMET 1.0" + "*");
            //retorno = MP1032.BematechTX(new string(' ', 10) + "ARTEBIT GOURMET 1.0" + "*");
            sb.Append(new string(' ', 10) + "WWW.ARTEBIT.COM.BR" + "*");
            //retorno = MP1032.BematechTX(new string(' ', 10) + "WWW.ARTEBIT.COM.BR" + "*");

            return sb.ToString();
        }

        public string RetornaRelProducao(ACONTA co)
        {
            var sb = new StringBuilder();

            //int stat = MP2032.Le_Status();

            IQueryable<AIMPRESSORA> impressoras = from p in Contexto.Atual.AIMPRESSMESA
                                                  //where p.AIMPRESSORA.imprimeProdutos == true && p.nuMesa == co.nuMesa
                                                  select p.AIMPRESSORA;

            foreach (AIMPRESSORA imp in impressoras)
            {
                List<ACONTITEM> itemsImpressao =
                    co.ACONTITEM.Where(r => (r.impresso == false || r.impresso == null)).Where(
                        aco => imp.AIMPRESSORAPRD.Any(r => r.idProduto == aco.idProduto)).ToList();

                if (itemsImpressao.Count > 0)
                {
                    #region Cabeçalho

                    sb.Append(new string(' ', 10) + "PRODUCAO" + "*");
                    sb.Append(new string(' ', 40) + "*");

                    ACONTITEM item = itemsImpressao.FirstOrDefault();
                    if (item != null)
                    {
                        string vendedor = "Vendedor: " + item.GVENDEDOR.nome;
                        sb.Append(vendedor + "*");
                    }


                    string preconta = "PreConta Nro: " + co.idConta.ToString(CultureInfo.InvariantCulture);
                    sb.Append(preconta + "*");

                    sb.Append(new string('=', 40) + "*");

                    string data = "DATA: " + DateTime.Now.ToString("dd/MM/yyyy") + "   HORA: " +
                                  DateTime.Now.ToString("HH:mm");
                    sb.Append(data + "*");

                    //string mesaCartao = new string(' ', 5) + "MESA/CARTAO: " + co.nuMesa;
                    //sb.Append(mesaCartao + "*");

                    #endregion

                    #region Header da tabela de produtos

                    sb.Append(new string('-', 40) + "*");

                    const string tituloTabela = " QTDE     PRODUTO                            ";
                    sb.Append(tituloTabela + "*");

                    sb.Append(new string('-', 40) + "*");

                    #endregion

                    var itens = from p in itemsImpressao
                                select new
                                           {
                                               p.EPRODUTO.codigo,
                                               p.EPRODUTO.nome,
                                               p.desconto,
                                               p.preco,
                                               p.quantidade,
                                               unidade = p.EPRODUTO.undVenda,
                                               obs = p.EOBSERVACAO,
                                               txtobs = p.txtObs,
                                               idPai = p.nuItemPai,
                                               p.opcao,
                                               p.adicional,
                                               p.nuItem
                                           };

                    #region Produtos

                    foreach (var ac in itens.Where(r => r.opcao == false))
                    {
                        //formata a quantidade
                        string msg = string.Format("{0:0}", ac.quantidade);
                        string linha = " " + msg + new string(' ', 8 - msg.Length);

                        //formata o nome do produto
                        string nomePrd = ac.nome.ToUpper();
                        if (nomePrd.Length > 41)
                        {
                            nomePrd = nomePrd.Substring(0, 41);
                        }
                        linha += nomePrd;

                        if (linha.Length > 43)
                        {
                            linha = linha.Substring(0, 43);
                        }

                        sb.Append(linha + "*");

                        if (ac.obs.Count > 0 || ac.txtobs != null)
                        {
                            foreach (EOBSERVACAO obs in ac.obs)
                            {
                                linha = "           " + obs.descricao.ToUpper();
                                sb.Append(linha + "*");
                            }

                            if (Convert.ToString(ac.txtobs) != "")
                            {
                                linha = "          " + ac.txtobs.ToUpper();
                                sb.Append(linha + "*");
                            }
                        }

                        foreach (var opcao in itens.Where(r => r.opcao && r.idPai == ac.nuItem))
                        {
                            //formata a quantidade
                            //msg = string.Format("{0:0}", opcao.quantidade);
                            linha = "     >"; // + msg + new string(' ', 3 - msg.Length);

                            //formata o nome do produto
                            string nomeProduto = "Com " + opcao.nome.ToUpper();
                            if (nomeProduto.Length > 27)
                            {
                                nomeProduto = nomeProduto.Substring(0, 28);
                            }

                            linha += nomeProduto + new string(' ', 28 - nomeProduto.Length);

                            sb.Append(linha + "*");

                            if (opcao.obs.Count > 0 || opcao.txtobs != null)
                            {
                                foreach (EOBSERVACAO obs in opcao.obs)
                                {
                                    linha = "           " + obs.descricao.ToUpper();
                                    sb.Append(linha + "*");
                                }

                                if (Convert.ToString(ac.txtobs) != "")
                                {
                                    linha = "          " + opcao.txtobs.ToUpper();
                                    sb.Append(linha + "*");
                                }
                            }
                        }


                        sb.Append(new string(' ', 10) + "*");
                    }

                    #endregion

                    sb.Append(new string('-', 40) + "*");
                    sb.Append(new string(' ', 10) + "Artebit Gourmet 1.0" + "*");
                    sb.Append(new string(' ', 10) + "www.artebit.com.br" + "*");

                    //int ret = retorno;

                    ////stat = MP2032.Le_Status();

                    ////aciona a guilhotina de forma total
                    //retorno = MP2032_2.AcionaGuilhotina(0);

                    ////fecha comunicação com a impressora
                    //retorno = MP2032_2.FechaPorta();

                    foreach (ACONTITEM aco in itemsImpressao)
                    {
                        aco.impresso = true;
                    }
                }
            }

            return sb.ToString();
        }

        #endregion
    }
}