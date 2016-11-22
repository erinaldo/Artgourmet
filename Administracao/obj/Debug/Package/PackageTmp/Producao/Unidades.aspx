<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Unidades.aspx.cs" Inherits="Artebit.Restaurante.Administracao.Producao.Unidades" %>

<html>
<head id="Head1" runat="server">
    <title>..:: Unidades ::..</title>
   
    
    <script>

       function carregaComboFiltro() {
           for (var i = 0; i < GridPrincipal.getStore().fields.getCount() ; i++) {
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

    </script>
    

</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
        
    <%-- Store da grid --%>
    <ext:Store ID="storePrincipal" runat="server">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="String" />
                    <ext:RecordField Name="descricao" Type="String" />
                    <ext:RecordField Name="codUndBase" Type="String" />
                    <ext:RecordField Name="fatorConversao" Type="Float" />
                    <ext:RecordField Name="ativo" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <ext:SortInfo Field="codigo" Direction="ASC" />
    </ext:Store>
        
    <%--Store dos dados que aparecem na janela--%>
    <ext:Store ID="StoreFormulario" runat="server" AutoLoad="false" RemotePaging="true" OnRefreshData="StoreFormulario_RefreshData">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="String" />
                    <ext:RecordField Name="codigo2" Type="String" />
                    <ext:RecordField Name="descricao" Type="String" />
                    <ext:RecordField Name="codUndBase" Type="String" />
                    <ext:RecordField Name="fatorConversao" Type="Float" />
                    <ext:RecordField Name="hdtipo" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <Listeners>
               <DataChanged Handler="  var record = this.getAt(0) || {};
                                        #{FormPrincipal}.getForm().loadRecord(record); 
                                        if(this.getAt(0) != null)
                                        {
                                            JanelaPrincipal.setTitle('Vendedor: '+ record.get('codigo') + ' - ' +record.get('descricao') );
                                        }

                                        #{FormPrincipal}.body.unmask();
                                        "
                    Delay="15" />
               <BeforeLoad Handler="#{FormPrincipal}.body.mask('Carregando...', 'x-mask-loading');" />
               <LoadException Handler="#{FormPrincipal}.body.unmask();" />
               <Add Handler="PagingToolbar1.changePage(StoreFormulario.getTotalCount());" />

        </Listeners>
    </ext:Store>


    <%--Montar a GridView--%>
    <ext:GridPanel 
    ID="GridPrincipal" 
    runat="server" 
    Height="515"
    AutoWidth="true"
    StoreID="storePrincipal" 
    TrackMouseOver="true"
    AutoExpandColumn="descricao"
    StripeRows="True"
    Frame="True"
    EnableHdMenu="True"
    Selectable="True"
    Border="false">
        <ColumnModel ID="ColumnModel1" runat="server">
            <Columns>
                <%--coluna oculta--%>
                <ext:Column Header="Unidade" DataIndex="codigo" Hidden="false" runat="server" />
                <ext:Column Header="Descrição" DataIndex="descricao" runat="server" />
                <ext:Column Header="Unidade Base" DataIndex="codUndBase" runat="server" />
                <ext:Column Header="Fator Conversão" DataIndex="fatorConversao" runat="server" />
                <ext:Column Header="Status" DataIndex="ativo" runat="server" />
            </Columns>
        </ColumnModel>
        
        <SelectionModel>
            <ext:CheckBoxSelectionModel ID="RowSelectionModel1" runat="server" CheckOnly="True" />
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
                    <ext:StringFilter DataIndex="nome" />
                    <ext:StringFilter DataIndex="codusu" />
                    <ext:StringFilter DataIndex="comissao" />
                    <ext:StringFilter DataIndex="ativo" />
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
            <Command Handler="if(command == 'editar') {JanelaPrincipal.show();}" />
            <CellClick Handler="if(columnIndex != 0) {this.getSelectionModel().selectRow(rowIndex);}" />
            <AfterLayout Handler="carregaComboFiltro();"></AfterLayout>
        </Listeners>
    </ext:GridPanel>


    <%--Janela de adicionar e editar Fornecedores--%>
    <ext:Window ID="JanelaPrincipal" runat="server" Collapsible="false" Hidden="true" Modal="true"
        Height="270" Icon="BookAdd" Title="" Width="500">
        <Items>
            <ext:FormPanel ID="FormPrincipal" runat="server" Border="false">
                <Items>
                    <ext:Portal ID="Portal1" runat="server" Layout="column" Height="320" >
                        <Items>
                            <ext:PortalColumn ID="PortalColumn1" runat="server" ColumnWidth="0.33" DefaultAnchor="100%"
                                Layout="anchor" StyleSpec="padding:10px 10px 0 10px">
                                <Items>
                            
                                    <%--------------------------------------------Informações---------------------------------------------%>
                                    <ext:Portlet ID="Portlet1" runat="server" Padding="5" Title="Informações" Icon="Information" Collapsible="false">
                                        <Items>

                                            <%-- Tipo de ação a ser executada, 1=adicionar, 2=atualizar --%>
                                            <ext:Hidden runat="server" ID="hd_tipo" DataIndex="hdtipo" />

                                            <ext:TextField ID="txtCodUnd" FieldLabel="Código Unidade" EmptyText="" DataIndex="codigo2"
                                                Width="250" AllowBlank="true" runat="server" />

                                            <ext:TextField ID="txtDescricao" FieldLabel="Descrição" EmptyText="" DataIndex="descricao"
                                                Width="400" AllowBlank="true" runat="server" />

                                            <ext:ComboBox 
                                            ID="dropUnidades" 
                                            FieldLabel="Unidade Base" 
                                            Width="300" 
                                            DataIndex="codUndBase"
                                            EmptyText="Selecione"
                                            StoreID="StorePrincipal" 
                                            ValueField="codigo" 
                                            DisplayField="descricao" 
                                            AllowBlank="true"
                                            runat="server">
                                            </ext:ComboBox>
                               
                                            <ext:NumberField ID="txtFator" FieldLabel="Fator Conversão:" Width="220" AllowBlank="true" AllowDecimals="true"
                                                 AllowNegative="false" DecimalSeparator="." DecimalPrecision="4" DataIndex="fatorConversao"
                                                runat="server" />

                                        </Items>
                                    </ext:Portlet>
                                </Items>
                            </ext:PortalColumn>
                        </Items>
                    </ext:Portal>
                </Items>
            </ext:FormPanel>
        </Items>
        
        <Buttons>
            <ext:Button ID="btnOkFrm" runat="server" Text="OK" Icon="Accept">
                <DirectEvents>
                    <Click OnEvent="btnOkFrm_Click">
                        <EventMask ShowMask="true" Msg="Verificando..." MinDelay="500" />
                    </Click>
                </DirectEvents>
            </ext:Button>
            <ext:Button ID="btnSalvarFrm" runat="server" Text="Salvar" Icon="Disk">
                <DirectEvents>
                    <Click OnEvent="btnSalvarFrm_Click">
                        <EventMask ShowMask="true" Msg="Verificando..." MinDelay="500" />
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
