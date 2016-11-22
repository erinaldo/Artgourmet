<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="planoFidelidade.aspx.cs" Inherits="Administracao.Funcao.Paginas.planoFidelidade" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="Artebit.Restaurante.Global.RegrasNegocio.Atendimento" %>

<html>
<head runat="server">
    <title>..:: Plano de Fidelidade ::..</title>

    <%--Código C#--%>
    <script runat="server">
       

    </script>


</head>



    <%--Código JavaScript para a grid fornecedores--%>
    <script type="text/javascript">

        var novoRegistro = false;

        var keyUpProductHandler = function (field, e) {

            var store = MultiSelect1.store;

            store.filter('nome', field.getValue(), true, false);
        }

        var keyUpProductHandler2 = function (field, e) {

            var store = MultiSelect2.store;

            store.filter('nome', field.getValue(), true, false);
        }

        var clearProductFilter = function () {
            filtroMulti1.reset();
            MultiSelect1.store.clearFilter();

        }

        var clearProductFilter2 = function () {
            filtroMulti2.reset();
            MultiSelect2.store.clearFilter();

        }

        var transferir = function (source, dest, tudo) {
            if (tudo) {
                source.selectAll();
            }

            var selected = source.view.getSelectedRecords(),
                storeDest = dest.store,
                recordType = storeDest.recordType;


            //storeDest.removeAll();
            Ext.each(selected, function (r) {
                source.store.remove(r);
                storeDest.add(r);
            });
        };
        
    </script>



<body>
    <ext:ResourceManager ID="ResourceManager1" runat="server" />

     <%--Store da combobox Filial--%>
    <ext:Store ID="StoreFilial" runat="server" >
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="idFilial" Type="Int" />
                        <ext:RecordField Name="nomeFilial" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

    <%-- Store da grid de LISTA DE PRODUTOS --%>
    <ext:Store ID="storeProdutos" runat="server">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="String" />
                    <ext:RecordField Name="nome" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>

    <%-- Store da grid de LISTA DE PRODUTOS --%>
    <ext:Store ID="storeProdutos2" runat="server">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="String" />
                    <ext:RecordField Name="nome" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>

     <%-- Store da grid de LISTA DE Planos --%>
    <ext:Store ID="storePlanos" runat="server">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="String" />
                    <ext:RecordField Name="nome" Type="String" />
                    <ext:RecordField Name="valor" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>

    <form id="form1" runat="server">
        <ext:Portal ID="Portal1" runat="server" Layout="column" Border="false" Height="440">
            <Items>
                 <%--Coluna explorer--%>
                <ext:PortalColumn ID="PortalColumn2" runat="server" ColumnWidth="0.65" DefaultAnchor="100%"
                    Layout="anchor" StyleSpec="padding:5px">
                    <Items>
                        <ext:Portlet ID="Portlet3" runat="server" Padding="5" Border="false" Height="420" Layout="Column">
                            <Items>
                                <%--Lista de produtos--%>
                                 <ext:MultiSelect ID="MultiSelect1" Legend="Produtos" AnchorHorizontal="50%" ColumnWidth=".47" runat="server" AnchorVertical="10%"
                                                    StoreID="storeProdutos" DisplayField="nome" ValueField="codigo"
                                     DragGroup="grupo1">
                                        <TopBar>
                                            <ext:Toolbar runat="server" ID="ToolBarMulti1" Layout="FitLayout">
                                                <Items>
                                                    <ext:TriggerField runat="server" ID="filtroMulti1" AnchorHorizontal="100%" EnableKeyEvents="true">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" />
                                                            <ext:FieldTrigger Icon="Search" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <KeyUp Fn="keyUpProductHandler" />
                                                            <TriggerClick Handler=" if(index == 0) { clearProductFilter(); } else { Ext.Msg.alert('','buscar'); }" />
                                                        </Listeners>
                                                    </ext:TriggerField>
                                                </Items>
                                            </ext:Toolbar>
                                        </TopBar>
                                    </ext:MultiSelect>

                                <%--Botões de transferência--%>
                                <ext:Panel ID="PanelMultiAcao" Border="false" runat="server" ColumnWidth=".06" AnchorVertical="100%" Layout="FormLayout" PaddingSummary="100px 0 0 0">
                                    <Items>
                                        <ext:Button ID="btMove1" runat="server" Icon="PlayGreen" Flat="true" AnchorHorizontal="100%" >
                                            <Listeners>
                                                <Click Handler="transferir(MultiSelect1,MultiSelect2,false);" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="btMove2" runat="server" Icon="ForwardGreen" Flat="true" AnchorHorizontal="100%" >
                                            <Listeners>
                                                <Click Handler="transferir(MultiSelect1,MultiSelect2,true);" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="btMove3" runat="server" Icon="ReverseBlue" Flat="true" AnchorHorizontal="100%">
                                            <Listeners>
                                                <Click Handler="transferir(MultiSelect2,MultiSelect1,false);" />
                                            </Listeners>
                                        </ext:Button>

                                        <ext:Button ID="btMove4" runat="server" Icon="RewindBlue" Flat="true" AnchorHorizontal="100%">
                                            <Listeners>
                                                <Click Handler="transferir(MultiSelect2,MultiSelect1,true);" />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:Panel>

                                <%--Lista dos produtos escolhidos--%>
                                <ext:MultiSelect ID="MultiSelect2" Legend="Produtos escolhidos" AnchorHorizontal="50%" ColumnWidth=".47" AnchorVertical="100%" 
                                runat="server" DisplayField="nome" ValueField="codigo"
                                DropGroup="grupo1" StoreID="storeProdutos2">
                                    <TopBar>
                                            <ext:Toolbar runat="server" ID="ToolBar1" Layout="FitLayout">
                                            <Items>
                                                <ext:TriggerField runat="server" ID="filtroMulti2" AnchorHorizontal="100%" EnableKeyEvents="true">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <KeyUp Fn="keyUpProductHandler2" />
                                                        <TriggerClick Handler=" if(index == 0) { clearProductFilter2(); } else { Ext.Msg.alert('','buscar'); }" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                </ext:MultiSelect>
                            </Items>
                        </ext:Portlet>
                    </Items>
                </ext:PortalColumn>
                
                <%--Coluna de Ação--%>
                <ext:PortalColumn ID="PortalColumn3" runat="server" ColumnWidth="0.35" DefaultAnchor="100%"
                    Layout="anchor" StyleSpec="padding:5px">
                    <Items>
                        <ext:Portlet ID="Portlet4" runat="server" Padding="5" Height="420">
                            <Items>
                                <ext:GridPanel 
                                    ID="GridPanel1" 
                                    runat="server" 
                                    Border="true" 
                                    Height="380" 
                                    StoreID="storePlanos"
                                    >

                                    <Plugins>
                                        <ext:EditableGrid ID="EditableGrid1" runat="server" />
                                    </Plugins>
                                    <ColumnModel>
                                        <Columns>
                                            <ext:Column Header="ID" DataIndex="codigo" Width="30" />
                                            <ext:Column Header="Nome" DataIndex="nome" Width="140" />
                                            <ext:Column 
                                                Header="Pontos por Real" 
                                                DataIndex="valor" 
                                                Width="120" 
                                                Sortable="true">
                                                <Editor>
                                                    <ext:NumberField ID="TextField1" runat="server" AllowBlank="true" />
                                                </Editor>
                                            </ext:Column>
                                        </Columns>
                                  </ColumnModel>
                                  <BottomBar>
                                    <ext:Toolbar ID="Toolbar2" runat="server">
                                        <Items>
                                            <ext:Button ID="Button1" runat="server" Text="Atualizar Dados"  Icon="VcardEdit">
                                                <DirectEvents>
                                                    <Click onEvent="Atualizar" >
                                                        <ext:EventMask ShowMask="true" />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="Values" Value="Ext.encode(#{GridPanel1}.getRowsValues())" Mode="Raw" />
                                                            <ext:Parameter Name="Produtos" Value="Ext.encode(#{storeProdutos2}.getRecordsValues())" Mode="Raw" />
                                                        </ExtraParams>
                                                    </Click>
                                                </DirectEvents>
                                            </ext:Button>
                                        </Items>
                                  </ext:Toolbar>
                                </BottomBar>
                                </ext:GridPanel>
                            </Items>
                        </ext:Portlet>
                    </Items>
                </ext:PortalColumn>
            </Items>
        </ext:Portal>

      
        </form>
</body>
</html>
