<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Monitores.aspx.cs" Inherits="Administracao.PDV.Monitores" %>

<html>
<head id="Head1" runat="server">
    <title>..:: Monitores ::..</title>

    <script>

        function carregaComboFiltro() {
            for (var i = 0; i < GridPrincipal.getStore().fields.getCount(); i++) {
                var valor = GridPrincipal.getStore().fields.get(i).name;
                comboFiltroPrincipal.removeByValue(valor);
                comboFiltroPrincipal.addItem(valor, valor);
            }
            comboFiltroPrincipal.selectByIndex(1);
        }

        function filtraGridPrincipal() {
            var filtro = txtFiltroPrincipal.getValue();
            var campo = comboFiltroPrincipal.getSelectedItem().value;

            GridPrincipal.getStore().filter(campo, filtro, true, false);
        }

        function limpaFiltroPrincipal() {
            txtFiltroPrincipal.reset();
            GridPrincipal.getStore().clearFilter();
        }





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
        //            Ext.each(selected, function (r) {
        //                
        //            });
        
    </script>



</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
        
    <%-- Store da grid de monitores --%>
    <ext:Store ID="StorePrincipal" runat="server">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="Int" />
                    <ext:RecordField Name="descricao" Type="String" />
                    <ext:RecordField Name="ativo" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
    
    <%-- Store dos dados que aparecem na janela --%>
    <ext:Store ID="StoreFormulario" runat="server" AutoLoad="false" RemotePaging="true" OnRefreshData="StoreFormulario_RefreshData">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="Int" />
                    <ext:RecordField Name="descricao" Type="String" />
                    <ext:RecordField Name="ativo" Type="String" />
                    <ext:RecordField Name="hdtipo" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <Listeners>
               <DataChanged Handler="  var record = this.getAt(0) || {};
                                        #{FormPrincipal}.getForm().loadRecord(record); 
                                        if(this.getAt(0) != null)
                                        {
                                              JanelaPrincipal.setTitle('Monitores: '+ record.get('codigo') + ' - ' +record.get('descricao') );
                                        }

                                        storeProduto.reload();
                                        Ext.net.DirectMethods.AtualizaMesa(record.get('codigo'));

                                        #{FormPrincipal}.body.unmask();
                                        "
                    Delay="15" />
               <BeforeLoad Handler="#{FormPrincipal}.body.mask('Carregando...', 'x-mask-loading');" />
               <LoadException Handler="#{FormPrincipal}.body.unmask();" />
               <Add Handler="PagingToolbar1.changePage(StoreFormulario.getTotalCount());" />

            </Listeners>
    </ext:Store>
    
    <%--Store da grid de mesa--%>
    <ext:Store ID="storeMesa" runat="server" >
            <Reader>
                <ext:JsonReader IDProperty="nuMesa">
                    <Fields>
                        <ext:RecordField Name="nuMesa" Type="Int" />
                        <ext:RecordField Name="qtdLugares" Type="Int" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

    <%--Store da grid de produto--%>
    <ext:Store ID="storeProduto" runat="server"  AutoLoad="false" OnRefreshData="StoreProdutos_RefreshData">
                <Reader>
                    <ext:JsonReader IDProperty="codigo">
                        <Fields>
                            <ext:RecordField Name="codigo" Type="Int" />
                            <ext:RecordField Name="nome" Type="String" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
            </ext:Store>

    <%--Store da grid de produto--%>
    <ext:Store ID="storeProdutoSelecionado" runat="server" AutoLoad="false">
                <Reader>
                    <ext:JsonReader IDProperty="codigo">
                        <Fields>
                            <ext:RecordField Name="codigo" Type="Int" />
                            <ext:RecordField Name="nome" Type="String" />
                        </Fields>
                    </ext:JsonReader>
                </Reader>
     </ext:Store>

    <%--GridView de monitores--%>
    <ext:GridPanel 
    ID="GridPrincipal" 
    runat="server" 
    Height="529" 
    AutoWidth="true"
    StoreID="StorePrincipal" 
    TrackMouseOver="true" 
    Border="false">
        <ColumnModel ID="ColumnModel1" runat="server">
            <Columns>
                <ext:Column Header="Codigo" DataIndex="codigo"  />
                <ext:Column Header="ID" DataIndex="id" />
                <ext:Column Header="Descrição" DataIndex="descricao" Width="150" />
                <ext:Column Header="Status" DataIndex="ativo" />
                              
            </Columns>
        </ColumnModel>

        <SelectionModel>
            <ext:CheckboxSelectionModel ID="RowSelectionModel1" runat="server" CheckOnly="True"  >
            </ext:CheckboxSelectionModel>
        </SelectionModel>

        <TopBar>
            <ext:Toolbar ID="Toolbar1" runat="server">
                <Items>
                    <%--Novo--%>
                    <ext:Button ID="btnNovoG" runat="server" Text="Novo" Icon="Add">
                        <DirectEvents>
                            <Click OnEvent="GridAcao">
                                <ExtraParams>
                                    <ext:Parameter Name="codigo" Value="0" Mode="Value" />
                                    <ext:Parameter Name="command" Value="novo" Mode="Value" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                        <Listeners>
                            <Click Handler="JanelaPrincipal.show();" />
                        </Listeners>
                    </ext:Button>
                    <%--Editar--%>
                    <ext:Button ID="btnEditarG" runat="server" Text="Editar" Icon="CommentEdit" >
                        <DirectEvents>
                            <Click OnEvent="GridAcao">
                                <ExtraParams>
                                    <ext:Parameter Name="codigo" Value="GridPrincipal.getSelectionModel().getSelected().data['codigo']" Mode="Raw" />
                                    <ext:Parameter Name="command" Value="editar" Mode="Value" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                        <Listeners>
                            <Click Handler="JanelaPrincipal.show();" />
                        </Listeners>
                    </ext:Button>
                    <%--Excluir--%>
                    <ext:Button ID="btnExcluirG" runat="server" Text="Excluir" Icon="Cross" Disabled="true">
                        <Listeners>
                            <Click Handler="confirmaExclusao();"></Click>
                        </Listeners>
                    </ext:Button>
                    <%--Ativar/Desativar--%>
                    <ext:Button ID="btnAtivarDesativarG" runat="server" Text="Ativar/Desativar" Icon="Wand" >
                        <DirectEvents>
                            <Click OnEvent="GridAcao">
                                <ExtraParams>
                                    <ext:Parameter Name="codigo" Value="GridPrincipal.getSelectionModel().getSelected().data['codigo']" Mode="Raw" />
                                    <ext:Parameter Name="command" Value="ativar" Mode="Value" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:ToolbarSeparator ID="ToolbarSeparator1" runat="server"/>

                    <ext:ComboBox runat="server" Width="110" ID="comboFiltroPrincipal"  AutoScroll="True">
                    </ext:ComboBox>

                    <ext:TriggerField runat="server" ID="txtFiltroPrincipal" AnchorHorizontal="100%" Width="200px" EnableKeyEvents="true">
                            <Triggers>
                                <ext:FieldTrigger Icon="Clear" />
                                <ext:FieldTrigger Icon="Search" />
                            </Triggers>
                            <Listeners>
                                <KeyUp Fn="filtraGridPrincipal" />
                                <TriggerClick Handler=" if(index == 0) { limpaFiltroPrincipal(); } " />
                            </Listeners>
                        </ext:TriggerField>

                    <ext:ToolbarSeparator ID="ToolbarSeparator2" runat="server"/>

                    <ext:PagingToolbar Flat="True"  HideRefresh="true" runat="server" ID="PagingToolbar2" PageSize="21" StoreID="StorePrincipal" DisplayMsg="">
                    </ext:PagingToolbar>
                </Items>
            </ext:Toolbar>
        </TopBar>

        <BottomBar>
            <%-- Este pagingtoolbar é necessário pois o processo de paging inicial do store só ocorre quando o paging toolbar está no bottom bar da grid --%>
            <ext:PagingToolbar Flat="True"  HideRefresh="true" Hidden="True" runat="server" ID="PageBottom" PageSize="21" StoreID="StorePrincipal">
            </ext:PagingToolbar>
        </BottomBar>

        <Plugins>
                <ext:GridFilters runat="server" ID="GridFilters3" Local="true">
                    <Filters>
                        <ext:StringFilter DataIndex="codigo" />
                        <ext:StringFilter DataIndex="descricao" />
                    </Filters>
                </ext:GridFilters>
            </Plugins>
       
        <DirectEvents>
            <Command OnEvent="GridAcao">                                       
                <ExtraParams>
                    <ext:Parameter Name="codigo" Value="record.data.codigo" Mode="Raw" />
                    <ext:Parameter Name="command" Value="command" Mode="Raw"/>
                </ExtraParams>
            </Command> 
            <RowDblClick OnEvent="GridAcao">
                <ExtraParams>
                    <ext:Parameter Name="codigo" Value="this.store.getAt(rowIndex).data['codigo']" Mode="Raw" />
                    <ext:Parameter Name="command" Value="editar" Mode="Value"/>
                </ExtraParams>
            </RowDblClick>
        </DirectEvents>

        <Listeners>
                <CellClick Handler="if(columnIndex != 0) {this.getSelectionModel().selectRow(rowIndex);}" />
        </Listeners>

         <LoadMask ShowMask="True"></LoadMask>

    </ext:GridPanel>

    <%--Janela de adicionar e editar Monitores--%>
    <ext:Window 
    ID="JanelaPrincipal" 
    runat="server" 
    Collapsible="false" 
    Hidden="true" 
    Modal="true"
    Height="470"
    Icon="BookAdd" 
    Title="" 
    Width="620"
    PaddingSummary="10px 0 0 0">
        <Items>
            <ext:FormPanel ID="FormPrincipal" runat="server" Padding="7" >
                <Items>
                    <ext:TabPanel ID="tabFilial" runat="server" Height="500" DeferredRender="false"  >
                        <Items>
                            <%--Aba de Informações--%>
                            <ext:Portal ID="Portal1" runat="server" Layout="column" Height="400" Title="Informações" >
                                <Items>
                                    <ext:PortalColumn ID="PortalColumn1" runat="server" ColumnWidth="0.38" DefaultAnchor="100%"
                                         StyleSpec="padding:10px">
                                        <Items>
                                            <ext:Portlet ID="Portlet1" runat="server" Title="Identificação" Padding="8">
                                                <Items>
                                                    <%--Hidden do identificador--%>
                                                    <ext:Hidden runat="server" ID="codigo" DataIndex="codigo" >
                                                        <Listeners>
                                                            <Change Handler="#{txtIdentificador}.setText(this.getValue());" />
                                                        </Listeners>
                                                    </ext:Hidden>
                
                                                    <%-- Tipo de ação a ser executada, 1=adicionar, 2=atualizar --%>
                                                    <ext:Hidden runat="server" ID="hd_tipo" DataIndex="hdtipo" />
                                    
                                                    <%--Campo Identificador--%>
                                                    <ext:Label FieldLabel="Identificador" LabelWidth="75" ID="txtIdentificador" runat="server"/>
                                                    
                                                    <%--Campo Nome Filial--%>                                    
                                                    <ext:TextField ID="txtDescricao" DataIndex="descricao" LabelAlign="Top" FieldLabel="Descrição" Width="180" AllowBlank="true" runat="server" />

                                                    <%--Check button de status--%>
                                                    <ext:Checkbox ID="checkAtivo" runat="server" DataIndex="ativo" BoxLabel="Ativo" LabelAlign="Right">
                                                    </ext:Checkbox>
                                                </Items>
                                            </ext:Portlet>
                                       </Items>
                                    </ext:PortalColumn>

                                    <ext:PortalColumn ID="PortalColumn3" runat="server" ColumnWidth="0.72" DefaultAnchor="100%"
                                         StyleSpec="padding:10px">
                                        <Items>
                                            <ext:Portlet ID="Portlet2" runat="server" Title="Mesas">
                                                <Items>
                                                     <ext:GridPanel 
                                                    ID="GridMesa" 
                                                    runat="server" 
                                                    Width="340"
                                                    Height="290"
                                                    AutoScroll="true"
                                                    StoreID="storeMesa" 
                                                    TrackMouseOver="true" 
                                                    AutoExpandColumn="qtdLugares"
                                                    Border="false">
                                                        <ColumnModel ID="ColumnModel2" runat="server">
                                                            <Columns>
                                                
                                                                <ext:Column Header="Número da mesa" DataIndex="nuMesa" Align="Center" />
                                                                <ext:Column Header="Quantidade de lugares" DataIndex="qtdLugares" Align="Center" Width="150" />
                
                                                            </Columns>
                                                        </ColumnModel>
                                        
                                                        <SelectionModel>
                                                            <ext:CheckboxSelectionModel ID="check" runat="server" RowSpan="2" />
                                                        </SelectionModel>

                                                        <BottomBar>
                                                                <ext:PagingToolbar HideRefresh="true" runat="server" ID="PagingToolbar3" PageSize="20" StoreID="storeMesa">
                                                                </ext:PagingToolbar>
                                                        </BottomBar>

                                                    </ext:GridPanel>
                                                </Items>
                                            </ext:Portlet>
                                       </Items>
                                    </ext:PortalColumn>

                                </Items>
                             </ext:Portal>

                            <%--Aba de Produtos--%>
                            <ext:Panel ID="Tab3" runat="server" Title="Produtos" Height="347" Width="610" Layout="ColumnLayout"  HideMode="Offsets" Padding="5">
                                <Items>
                                    <ext:MultiSelect ID="MultiSelect1" Legend="Insumos" AnchorHorizontal="50%" ColumnWidth=".45" runat="server" StoreID="storeProduto" DisplayField="nome" ValueField="codigo"
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
                                    <ext:Panel ID="PanelMultiAcao" Border="false" runat="server" ColumnWidth=".1" AnchorVertical="100%" Layout="FormLayout" PaddingSummary="100px 0 0 0">
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
                                    <ext:MultiSelect ID="MultiSelect2" Legend="Produtos" AnchorHorizontal="50%" ColumnWidth=".45" AnchorVertical="100%" runat="server" DisplayField="nome" ValueField="codigo"
                                    DropGroup="grupo1" StoreID="storeProdutoSelecionado">
                                        <TopBar>
                                                <ext:Toolbar runat="server" ID="ToolBar2" Layout="FitLayout">
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
                                <Listeners>
                                    <Activate Handler="if(Ext.isEmpty(storeProduto.getAllRange())){ storeProduto.reload(); }" />
                                </Listeners>
                            </ext:Panel>
                        </Items>
                    </ext:TabPanel>
                </Items>
            </ext:FormPanel>
       </Items>
       
        <Buttons>
            <ext:Button ID="btnOkFrm" runat="server" Text="OK" Icon="Accept">
                <DirectEvents>
                    <Click OnEvent="btnOkFrm_Click">
                        <EventMask ShowMask="true" Msg="Verificando..." MinDelay="500" />
                        <ExtraParams>
                             <ext:Parameter Name="Mesas" Value="Ext.encode(#{storeMesa}.getRecordsValues())" Mode="Raw" />
                             <ext:Parameter Name="Produtos" Value="Ext.encode(#{storeProdutoSelecionado}.getRecordsValues())" Mode="Raw" />
                        </ExtraParams>
                    </Click>
                </DirectEvents>
            </ext:Button>
            <ext:Button ID="btnSalvarFrm" runat="server" Text="Salvar" Icon="Disk">
                <DirectEvents>
                    <Click OnEvent="btnSalvarFrm_Click">
                        <EventMask ShowMask="true" Msg="Verificando..." MinDelay="500" />
                        <ExtraParams>
                            <ext:Parameter Name="Mesas" Value="Ext.encode(#{storeMesa}.getRecordsValues())" Mode="Raw" />
                            <ext:Parameter Name="Produtos" Value="Ext.encode(#{storeProdutoSelecionado}.getRecordsValues())" Mode="Raw" />
                        </ExtraParams>
                    </Click>
                </DirectEvents>
                </ext:Button>
            <ext:Button ID="btnCancelar" runat="server" Text="Cancelar" Icon="Cancel">
                <Listeners>
                    <Click Handler="#{JanelaPrincipal}.hide();" />
                </Listeners>
            </ext:Button>
        </Buttons>

        <Listeners>
            <AfterLayout Handler="if(StoreFormulario.getCount() == 0)
                            {
                                StoreFormulario.reload();
                            }" />
        </Listeners>

        <TopBar>
            <ext:Toolbar ID="Toolbar6" runat="server" Flat="true">
                <Items>
                        <ext:Button ID="btnAdd" runat="server" Text="" Icon="Add">
                        <Listeners>
                            <Click Handler="#{FormPrincipal}.body.mask('Carregando...', 'x-mask-loading');" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="GridAcao">
                                <ExtraParams>
                                    <ext:Parameter Name="codigo" Value="0" Mode="Raw" />
                                    <ext:Parameter Name="command" Value="novo" Mode="Value" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    
                    <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="1" Flat="true" StoreID="StoreFormulario"
                        DisplayInfo="false">
                    </ext:PagingToolbar>
                    

                    <ext:ToolbarSeparator />
                    
                    <ext:ToolbarFill />
                    
                </Items>
            </ext:Toolbar>
        </TopBar>

    </ext:Window>

  

    </form>
</body>
</html>

