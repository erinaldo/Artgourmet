<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="observacao.aspx.cs" Inherits="Administracao.Funcao.Paginas.observacao" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="Artebit.Restaurante.Global.RegrasNegocio.Atendimento" %>

<html>
<head id="Head1" runat="server">
    <title>..:: Plano de Fidelidade ::..</title>

    <%--Código JavaScript --%>
    
    <script type="text/javascript">
        var onKeyUp = function () {
            var v = this.getRawValue();

            key = Ext.EventObject.getKey();

            if (key === Ext.EventObject.ENTER ||
                    key === Ext.EventObject.ESC) {

                        this.collapse();
                        return;
                    }

            if (key === Ext.EventObject.UP ||
                    key === Ext.EventObject.DOWN) {

                        return;
                    }

            if (v.length > 0) {
                this.getStore().filter(this.displayField, new RegExp(v + "+", "i"));
                this.onLoad();
            } else {
                this.collapse();
            }
        };
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
    
    
    <%--Store da grid lista de observações--%>
        <ext:Store ID="StoreObservacoes" runat="server"  AutoLoad="false" OnRefreshData="StoreObservacoes_RefreshData">
            <Reader>
                <ext:JsonReader IDProperty="id">
                    <Fields>
                        <ext:RecordField Name="id" Type="Int" />
                        <ext:RecordField Name="descricao" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
    
    <%--Store da combobox Grupo --%>
        <ext:Store ID="StoreGrupo" runat="server"  AutoLoad="false" OnRefreshData="StoreGrupo_RefreshData">
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="id" Type="Int" />
                        <ext:RecordField Name="nome" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <SortInfo Field="nome" Direction="ASC" />
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
                                
                                <ext:ComboBox 
                                ID="ComboBoxGrupo" 
                                runat="server" 
                                Width="280" 
                                StoreID="StoreGrupo" 
                                FieldLabel="Grupo" 
                                ValueField="id"
                                DisplayField="nome"
                                TypeAhead="false" 
                                ForceSelection="False"
                                EnableKeyEvents="true">
                                <Listeners>
                                     <AfterRender Handler="this.mun(this.el, 'keyup', this.onKeyUp, this);
                                                           this.mon(this.el, 'keyup', onKeyUp, this);" />
                                     <%--<Change Handler="StoreObservacoes.reload();" />--%>
                                </Listeners>
                                </ext:ComboBox>
                                
                                
                                <ext:GridPanel ID="GridObservacoes" runat="server" Height="360" Width="280" 
                                StoreID="StoreObservacoes" AutoExpandColumn="descricao" AutoRender="false">
                                    <ColumnModel>
                                        <Columns>
                                            <ext:Column Header="Descrição" Width="760" DataIndex="descricao" />
                                        </Columns>
                                    </ColumnModel>
                                    <SelectionModel>
                                        <ext:CheckboxSelectionModel ID="check" runat="server" RowSpan="2" />
                                    </SelectionModel>
                                    <BottomBar>
                                    <ext:Toolbar ID="Toolbar2" runat="server">
                                        <Items>
                                            <ext:Button ID="Button1" runat="server" Text="Atualizar Dados"  Icon="VcardEdit">
                                                <DirectEvents>
                                                    <Click onEvent="Atualizar" >
                                                        <ext:EventMask ShowMask="true" />
                                                        <ExtraParams>
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
