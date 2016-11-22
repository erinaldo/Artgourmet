using System.Data;
using System.Linq;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Estoque
{
    public class ObservacaoDAL
    {
        /// <summary>
        /// Função para buscar objeto completo
        /// </summary>
        /// <param name="obj">objeto</param>
        /// <returns>objeto</returns>
        public EOBSERVACAO Buscar(EOBSERVACAO obj)
        {
            // Busca
            EOBSERVACAO obj2 = Contexto.Atual.EOBSERVACAO.SingleOrDefault(a => a.idObs == obj.idObs
                                                                               &&
                                                                               (a.ativo == obj.ativo ||
                                                                                obj.ativo == null)
                                                                               &&
                                                                               (a.descricao == obj.descricao ||
                                                                                obj.descricao == null)
                );

            return obj2;
        }

        public IQueryable<EOBSERVACAO> BuscarLista()
        {
            IQueryable<EOBSERVACAO> lista = from a in Contexto.Atual.EOBSERVACAO
                                            where a.ativo == true
                                            select a;
            return lista;
        }

        public IQueryable<EPRODOBSBAIXA> BuscarProduto(EOBSERVACAO obj)
        {
            IQueryable<EPRODOBSBAIXA> lista = from a in Contexto.Atual.EPRODOBSBAIXA
                                              where a.idObs == obj.idObs && a.idEmpresa == Memoria.Empresa
                                              select a;
            return lista;
        }

        public bool Adicionar(EOBSERVACAO obj)
        {
            obj.idObs = Contexto.GerarId("EOBSERVACAO");


            // adiciona produto
            Contexto.Atual.AddToEOBSERVACAO(obj);

            // salva alterações
            Contexto.Atual.SaveChanges();

            return true;
        }

        public bool Atualizar(EOBSERVACAO obj)
        {
            Contexto.Atual.ObjectStateManager.ChangeObjectState(obj, EntityState.Modified);
            Contexto.Atual.SaveChanges();
            return true;
        }

        public bool Excluir(EOBSERVACAO obj)
        {
            Contexto.Atual.DeleteObject(obj);
            return true;
        }

        public IQueryable<EOBSERVACAO> BuscarAtual()
        {
            IQueryable<EOBSERVACAO> lista = from a in Contexto.Atual.EOBSERVACAO
                                            select a;
            return lista;
        }
    }
}