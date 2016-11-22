using System;
using System.Collections.Generic;
using Ext.Net;

namespace Artebit.Restaurante.Administracao.Relatorios
{
    public partial class Lista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            winRelatorio.AutoLoad.MaskMsg = "Carregando...";
            winRelatorio.AutoLoad.ShowMask = true;
            winRelatorio.AutoLoad.Mode = LoadMode.IFrame;

            IDictionary<string, string> registro = new Dictionary<string, string>();



            registro.Add("codigo", "R0001");
            registro.Add("relatorio", "Descontos");
            registro.Add("descricao", "Relatório com descontos efetuados em determinado período de tempo.");
            registro.Add("nomeArquivo", "descontos");
            storeLista.AddRecord(registro, true);

            registro = new Dictionary<string, string>
                           {
                               {"codigo", "R0002"},
                               {"relatorio", "Cancelamentos"},
                               {"descricao", "Relatório com cancelamentos efetuados em determinado período de tempo."},
                               {"nomeArquivo", "cancelamento"}
                           };
            storeLista.AddRecord(registro, true);

            registro = new Dictionary<string, string>
                           {
                               {"codigo", "R0003"},
                               {"relatorio", "Transferências"},
                               {"descricao", "Relatório com transferências efetuadas em determinado período de tempo."},
                               {"nomeArquivo", "transferencia"}
                           };
            storeLista.AddRecord(registro, true);

            registro = new Dictionary<string, string>
                           {
                               {"codigo", "R0004"},
                               {"relatorio", "Detalhes de Faturamento"},
                               {"descricao", "Relatório detalhes de faturamento em determinado período de tempo."},
                               {"nomeArquivo", "faturamento"}
                           };
            storeLista.AddRecord(registro, true);

            registro = new Dictionary<string, string>
                           {
                               {"codigo", "R0005"},
                               {"relatorio", "Detalhes de Recebimento"},
                               {"descricao", "Relatório detalhes de recebimentos em determinado período de tempo."},
                               {"nomeArquivo", "recebimentos"}
                           };
            storeLista.AddRecord(registro, true);

            registro = new Dictionary<string, string>
                           {
                               {"codigo", "R0006"},
                               {"relatorio", "Resumo de Faturamento"},
                               {"descricao", "Relatório resumo de faturamento em determinado período de tempo."},
                               {"nomeArquivo", "ResumoFaturamento"}
                           };
            storeLista.AddRecord(registro, true);

            registro = new Dictionary<string, string>
                           {
                               {"codigo", "R0007"},
                               {"relatorio", "Resumo de Vendas"},
                               {"descricao", "Relatório resumo de vendas do dia atual."},
                               {"nomeArquivo", "ResumoVendas"}
                           };
            storeLista.AddRecord(registro, true);

            registro = new Dictionary<string, string>
                           {
                               {"codigo", "R0008"},
                               {"relatorio", "Relação de Produtos"},
                               {"descricao", "Relação de Produtos"},
                               {"nomeArquivo", "RelacaoDeProdutos"}
                           };
            storeLista.AddRecord(registro, true);

            registro = new Dictionary<string, string>
                           {
                               {"codigo", "R0009"},
                               {"relatorio", "Cardapio"},
                               {"descricao", "Cardápio"},
                               {"nomeArquivo", "Cardapio"}
                           };
            storeLista.AddRecord(registro, true);

            registro = new Dictionary<string, string>
                           {
                               {"codigo", "R0010"},
                               {"relatorio", "Curva ABC Por Data"},
                               {"descricao", "Relatório Curva ABC por Data"},
                               {"nomeArquivo", "CurvaAbcPorData"}
                           };
            storeLista.AddRecord(registro, true);

            registro = new Dictionary<string, string>
                           {
                               {"codigo", "R0011"},
                               {"relatorio", "Curva ABC Por Dia Semana"},
                               {"descricao", "Relatório Curva ABC por Dia da Semana"},
                               {"nomeArquivo", "CurvaAbcPorDiaSemana"}
                           };
            storeLista.AddRecord(registro, true);

            registro = new Dictionary<string, string>
                           {
                               {"codigo", "R0012"},
                               {"relatorio", "Gerencial Vendas"},
                               {"descricao", "Relatório Gerecial de Vendas"},
                               {"nomeArquivo", "GerencialVendas"}
                           };
            storeLista.AddRecord(registro, true);
        }


        protected void ver_Relatorio(object sender, DirectEventArgs e)
        {

            string relatorio = e.ExtraParams["id"];
            string url = ResolveUrl("~/Relatorios/VerRelatorio.aspx?id=" + relatorio);
            Response.Redirect(url);


        }

    }
}