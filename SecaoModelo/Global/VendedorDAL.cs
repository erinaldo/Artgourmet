using System;
using System.Data;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Global
{
    public class VendedorDAL
    {
        /// <summary>
        /// Função para buscar objeto vendedor completo
        /// </summary>
        /// <param name="obj">objeto vendedor</param>
        /// <returns>objeto vendedor</returns>
        public GVENDEDOR Buscar(GVENDEDOR obj)
        {
            if (Memoria.Empresa != 0 && Memoria.Empresa != 0)
            {
                // Busca
                GVENDEDOR vend = Contexto.Atual.GVENDEDOR.SingleOrDefault(a => a.idVen == obj.idVen
                                                                               &&
                                                                               (a.idEmpresa == Memoria.Empresa ||
                                                                                Memoria.Empresa == 0)
                                                                               &&
                                                                               (a.idFilial == Memoria.Filial ||
                                                                                Memoria.Filial == 0)
                    );

                return vend;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Função para criar um novo vendedor
        /// </summary>
        /// <param name="obj">objeto GVendedor</param>
        /// <returns>true ou false</returns>
        public bool Criar(GVENDEDOR obj)
        {
            if (Memoria.Empresa != 0 && Memoria.Filial != 0)
            {
                // Atualiza a empresa e filial
                obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);
                obj.idFilial = Convert.ToInt32(Memoria.Filial);
                obj.ativo = true;


                obj.idVen = Contexto.GerarId("GVENDEDOR");

                // Adicionar vendedor
                Contexto.Atual.AddToGVENDEDOR(obj);

                Contexto.Atual.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Função para atualizar
        /// </summary>
        /// <param name="obj">objeto vendedor</param>
        /// <returns>true ou false</returns>
        public bool Atualizar(GVENDEDOR obj)
        {
            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();
            return true;
        }


        /// <summary>
        /// Função para buscar uma lista de objetos
        /// </summary>
        /// <returns>lista de objetos</returns>
        public IQueryable<GVENDEDOR> BuscarLista()
        {
            // Busca a lista
            IQueryable<GVENDEDOR> lista = from a in Contexto.Atual.GVENDEDOR
                                          where (a.idEmpresa == Memoria.Empresa || Memoria.Empresa == 0)
                                                && (a.idFilial == Memoria.Filial || Memoria.Filial == 0)
                                          select a;

            return lista;
        }

        public bool Cancelar(GVENDEDOR obj)
        {
            obj.ativo = false;
            return Atualizar(obj);
        }

        public GVENDEDOR Verificar(GVENDEDOR obj)
        {
            GVENDEDOR vendedor = Contexto.Atual.GVENDEDOR.SingleOrDefault(a => a.idEmpresa == Memoria.Empresa
                                                                               && a.idFilial == Memoria.Filial
                                                                               && a.ativo == true
                                                                               && a.codigo == obj.codigo);
            if (vendedor != null)
            {
                return vendedor;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Função que valida a senha do vendedor, para saber se o mesmo precisa ou não alterar a senha
        /// </summary>
        /// <param name="obj">objeto vendedor</param>
        /// <returns>returna TRUE, para senha válida, ou FALSE, para senha expirada</returns>
        public bool ValidarSenha(GVENDEDOR obj)
        {
            if (obj.dataUpdSenha != null)
            {
                GPERFIL perfil =
                    Contexto.Atual.GPERFIL.FirstOrDefault(
                        r => r.idEmpresa == Memoria.Empresa && r.idPerfil == Memoria.Perfil);

                TimeSpan diff = Convert.ToDateTime(obj.dataUpdSenha) - DateTime.Now;

                if (perfil.valSenha != null && perfil.valSenha != 0)
                {
                    if (diff.Days >= perfil.valSenha)
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}