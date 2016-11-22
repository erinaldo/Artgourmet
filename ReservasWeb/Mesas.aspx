<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mesas.aspx.cs" Inherits="ReservasWeb.Mesas" %>

<%@ Import Namespace="Artebit.Restaurante.Global.Modelo" %>
<%@ Import Namespace="Artebit.Restaurante.Global.RegrasNegocio.Global" %>
<%@ Import Namespace="Artebit.Restaurante.Global.RegrasNegocio.Reserva" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.UX" Namespace="Ext.Net.UX" TagPrefix="ux" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!X.IsAjaxRequest)
        {
            var queryy = Contexto.Atual.GITMATRIZMESA;
            var query = from p in Contexto.Atual.GITMATRIZMESA.AsEnumerable()
                        where p.rowspan != 0
                        group p by p.linha into grupo
                        select new
                        {
                            linhas = grupo,
                            items = from i in grupo
                                    where i.colspan != 0
                                    select new
                                    {
                                        i.colspan,
                                        i.rowspan,
                                        status = i.GMESA != null ? i.GMESA.GSTATMESA.descricao : null,
                                        nuMesa = i.GMESA != null ? i.GMESA.nuMesa : 0,
                                        qtdLugares = i.GMESA != null ? i.GMESA.RGRUPOMESA.quantMaxima : null,
                                        i.idItem,
                                        i.linha,
                                        i.coluna,
                                        i.tpObjeto,
                                        imagem = i.tpObjeto == 1 && i.GMESA.GIMGMESA.Count > 0 ? "CarregaImagem.aspx?id=" + Convert.ToString(i.GMESA.GIMGMESA.Where(r => r.idStatus == i.GMESA.idStatus).FirstOrDefault().idImagem) : "CarregaImagem.aspx?id=1",
                                        seleciona = i.tpObjeto == 1 && i.GMESA.idStatus == 2 ? "item-wrap" : "none",
                                        exibe = i.tpObjeto == 2 ? true : false,
                                        i.texto
                                    }
                        };

            this.Store1.DataSource = query;
            this.Store1.DataBind();
        }


    }

    protected void Add_Click(object sender, DirectEventArgs e)
    {
        string[] mesasLista = HiddenMesas.Text.Split(',');
        
        Mesa m = new Mesa();

        GMATRIZMESA g = (GMATRIZMESA)m.ExecutaFuncao(null, Funcoes.BuscaMatrizAtiva, null, null);

        
        List<GMESA> mesas = new List<GMESA>();

        foreach (string linha in mesasLista)
        {
            if (linha != "")
            {
                int id = Convert.ToInt32(linha);

                GMESA m2 = g.GITMATRIZMESA.Where(r => r.idItem == id).FirstOrDefault().GMESA;

                mesas.Add(m2);
            }
        }

        Session["mesas"] = mesas;

        X.WindowManager.AddScript("parent.Window1.hide();");
        

    }
</script>
<script type="text/javascript">

    var selectionChanged = function (dv, nodes) {
        var mess = "";

        if (nodes.length > 0) {
            for (var i = 0; i < nodes.length; i++) {
                mess += nodes[i].id + ",";
            }
        }

        document.getElementById("HiddenMesas").value = mess;
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .x-view-selected
        {
            background-color: #DDDDDD;
        }
        .item-wrap
        {
        }
        .item
        {
            border: solid 1px transparent;
        }
        .item-wrap:hover
        {
            background-color: #EEEEEE;
            border: 1px solid #dddddd;
        }
        .none
        {
        }
    </style>
    <link rel="stylesheet" type="text/css" href="tooltip.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Store ID="Store1" runat="server">
            <Reader>
                <ext:JsonReader IDProperty="idItem">
                    <Fields>
                        <ext:RecordField Name="linhas" Type="Int" />
                        <ext:RecordField Name="items" IsComplex="true" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        <script type="text/javascript" src="tooltip.js"></script>
        <ext:Panel ID="DashBoardPanel" runat="server" MultiSelect="true" Layout="AnchorLayout"
            AutoHeight="false" Width="800" Height="390" Border="false" AutoScroll="true">
            <Items>
                <ext:DataView ID="Dashboard" runat="server" StoreID="Store1" MultiSelect="true" ItemSelector="td.item-wrap"
                    AutoHeight="true" EmptyText="No items to display">
                    <Template ID="Template1" runat="server">
                        <Html>
                            <div id="items-ct">
                                <table width="100%">
                                    <tpl for=".">
                                        <tr>
								            <tpl for="items">
										        <td id={idItem} class="item {seleciona}" colspan={colspan} rowspan={rowspan} onmouseover="if(!{exibe}){ tooltip.show('<b>Mesa :</b> {nuMesa}<br/> <b>Lugares : </b> {qtdLugares} <br /> <b>Status : </b> {status}');}" onmouseout="tooltip.hide();">
                                                    <div style="text-align:center;">
                                                        <img src="{imagem}" style="border:none;"></img>
                                                        &nbsp;
                                                        {texto}
                                                    </div>
                                                </td>
								            </tpl>
                                        </tr>
                                    </tpl>
                                </table>
							</div>
                        </Html>
                    </Template>
                    <Listeners>
                        <SelectionChange Fn="selectionChanged" />
                    </Listeners>
                </ext:DataView>
            </Items>
        </ext:Panel>
        <ext:Toolbar runat="server">
            <Items>
                <ext:Button ID="Button1" runat="server" Text="Selecionar Mesas" Icon="Accept" Width="200">
                    <DirectEvents>
                        <Click OnEvent="Add_Click" />
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button2" runat="server" Text="Cancelar" Icon="Decline" Width="200">
                    <Listeners>
                            <Click Handler="parent.Window1.hide();" />
                    </Listeners>
                </ext:Button>
            </Items>
        </ext:Toolbar>

        <ext:Hidden ID="HiddenMesas" Text="" runat="server"></ext:Hidden>
    </div>
    </form>
</body>
</html>
