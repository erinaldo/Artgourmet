using System;
using Artebit.Restaurante.Global.Modelo;

namespace Artebit.Restaurante.Global.AcessoDados.Atendimento
{
    public class LogDAL
    {
        public bool Criar()
        {
            if (Memoria.Empresa != 0)
            {
                var obj = new ALOG();

                obj.idLog = Contexto.GerarId("ALOG");

                obj.codUsuario = Memoria.Codusuario;
                obj.conta = Memoria.LogConta;
                obj.contaDestino = Memoria.LogContaDestino;
                obj.data = DateTime.Now;
                obj.idEmpresa = Convert.ToInt32(Memoria.Empresa);
                obj.idFilial = Convert.ToInt32(Memoria.Filial);
                obj.mesa = Memoria.LogMesa;
                obj.mesaDestino = Memoria.LogMesaDestino;
                obj.acao = Memoria.LogAcao;
                obj.vendedor = Memoria.Vendedor;

                if (obj.contaDestino != 0 && obj.mesaDestino != 0 && obj.conta != 0 && obj.mesa != 0)
                {
                    Contexto.Atual.AddToALOG(obj);
                }

                Memoria.LogAcao = null;
                Memoria.LogMesaDestino = null;
                Memoria.LogContaDestino = null;
                return true;
            }
            else
                return false;
        }
    }
}