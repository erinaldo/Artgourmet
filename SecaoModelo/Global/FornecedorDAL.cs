using System;
using System.Data;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Global
{
    public class FornecedorDAL
    {
        public IQueryable<GCLIFOR> BuscarLista()
        {
            // Busca a lista de mesas
            IQueryable<GCLIFOR> lista = from a in Contexto.Atual.GCLIFOR
                                        where a.idEmpresa == Memoria.Empresa
                                        select a;

            return lista;
        }

        public bool Criar(GCLIFOR obj)
        {
            obj.dataCriacao = DateTime.Now;
            obj.usuarioCriacao = Memoria.Codusuario;

            if (obj.idEmpresa == 0)
            {
                obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);
            }

            if (obj.idClifor == 0)
            {
                obj.idClifor = Contexto.GerarId("GCLIFOR");
            }

            foreach (GCLIFOREND ende in obj.GCLIFOREND)
            {
                ende.idClifor = obj.idClifor;
                ende.idEmpresa = obj.idEmpresa;
            }

            // Adicionar fornecedor
            Contexto.Atual.AddToGCLIFOR(obj);
            Contexto.Atual.SaveChanges();
            return true;
        }

        public IQueryable<GCLIFOR> BuscarListaEspecifica(GCLIFOR obj)
        {
            // Busca a lista de mesas
            IQueryable<GCLIFOR> lista = from a in Contexto.Atual.GCLIFOR
                                        where a.tpClifor == obj.tpClifor
                                              && (a.nomeFantasia.Contains(obj.nomeFantasia) || obj.nomeFantasia == null)
                                        select a;

            return lista;
        }

        public IQueryable<GTPCLIFOR> BuscarAtual()
        {
            IQueryable<GTPCLIFOR> lista = from a in Contexto.Atual.GTPCLIFOR
                                          select a;
            return lista;
        }

        public bool Atualizar(GCLIFOR obj)
        {
            obj.dataAlteracao = DateTime.Now;
            obj.usuarioAlteracao = Memoria.Codusuario;

            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();
            return true;
        }

        public GCLIFOR Buscar(GCLIFOR obj)
        {
            GCLIFOR fornecedor =
                Contexto.Atual.GCLIFOR.SingleOrDefault(r => obj.idClifor == r.idClifor && r.idEmpresa == Memoria.Empresa);
            return fornecedor;
        }

        public bool Excluir(GCLIFOR obj)
        {
            obj = Contexto.Atual.GCLIFOR.SingleOrDefault(r => r.idClifor == obj.idClifor && r.idEmpresa == obj.idEmpresa);

            if (obj != null)
            {
                if (obj.CMOVIMENTO.Count > 0)
                {
                    Memoria.MsgGlobal =
                        "Não é possível excluir fornecedor, pois o mesmo possui Movimentos a ele associados.";
                    return false;
                }
                else
                {
                    if (obj.EPRODUTO.Count > 0)
                    {
                        Memoria.MsgGlobal =
                            "Não é possível excluir fornecedor, pois o mesmo possui Produtos a ele associados.";
                        return false;
                    }
                    else
                    {
                        Contexto.Atual.DeleteObject(obj);
                        Contexto.Atual.SaveChanges();

                        return true;
                    }
                }
            }
            else
            {
                return true;
            }
        }

        public GCLIFOR BuscarByTelefone(GCLIFOR obj)
        {
            string telefone = obj.GCLIFOREND.FirstOrDefault(s => s.tipoEndereco == 1).telefone1;

            if (telefone != null || telefone != "")
            {
                GCLIFOREND end =
                    Contexto.Atual.GCLIFOREND.SingleOrDefault(r => r.tipoEndereco == 1 && r.telefone1 == telefone);

                GCLIFOR fornecedor = Contexto.Atual.GCLIFOR.SingleOrDefault(r => r.idClifor == end.idClifor
                                                                                 && r.idEmpresa == Memoria.Empresa);
                return fornecedor;
            }
            else
            {
                return null;
            }
        }
    }
}