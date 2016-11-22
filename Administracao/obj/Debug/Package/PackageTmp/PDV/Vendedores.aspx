<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vendedores.aspx.cs" Inherits="Administracao.PDV.Vendedores" %>

<%@ Import Namespace="Artebit.Restaurante.Global.Modelo" %>
<%@ Import Namespace="Artebit.Restaurante.Global.RegrasNegocio" %>
<%@ Import Namespace="Artebit.Restaurante.Global.RegrasNegocio.Estoque" %>
<%@ Import Namespace="Artebit.Restaurante.Global.RegrasNegocio.Global" %>
<%@ Import Namespace="Ext.Net" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>..:: Vendedores ::..</title>

    <%--Código JavaScript para a grid Vendedores--%>
   
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

    </script>

    
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <%-- Store é o datasource, o resultado dos selects --%>

    <%--Store da combobox Codigo do usuário--%>
    <ext:Store ID="StoreCodUsuario" runat="server">
        <Reader>
            <ext:JsonReader>
                <Fields>
                    <ext:RecordField Name="cod" Type="String" />
                    <ext:RecordField Name="nome" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>

    <%--Store da Grid Vendedores--%>
    <ext:Store ID="StorePrincipal" runat="server">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="Int" />
                    <ext:RecordField Name="nome" Type="String" />
                    <ext:RecordField Name="codusu" Type="String" />
                    <ext:RecordField Name="senha" Type="String" />
                    <ext:RecordField Name="comissao" Type="String" />
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
                    <ext:RecordField Name="nome" Type="String" />
                    <ext:RecordField Name="codusu" Type="String" />
                    <ext:RecordField Name="senha" Type="String" />
                    <ext:RecordField Name="comissao" Type="String" />
                    <ext:RecordField Name="ativo" Type="Boolean" />
                    <ext:RecordField Name="hdtipo" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <Listeners>
               <DataChanged Handler="  var record = this.getAt(0) || {};
                                        #{FormPrincipal}.getForm().loadRecord(record); 
                                        if(this.getAt(0) != null)
                                        {
                                            JanelaPrincipal.setTitle('Vendedor: '+ record.get('codigo') + ' - ' +record.get('nome') );
                                        }

                                        #{FormPrincipal}.body.unmask();
                                        "
                    Delay="15" />
               <BeforeLoad Handler="#{FormPrincipal}.body.mask('Carregando...', 'x-mask-loading');" />
               <LoadException Handler="#{FormPrincipal}.body.unmask();" />
               <Add Handler="PagingToolbar1.changePage(StoreFormulario.getTotalCount());" />

            </Listeners>
    </ext:Store>
    

    <%-------------------------------------------------Grid Vendedores-------------------------------------------%>
    <ext:GridPanel 
    ID="GridPrincipal" 
    runat="server" 
    Height="529" 
    AutoWidth="true"
    StoreID="StorePrincipal" 
    StripeRows="true" 
    Frame="true" 
    Collapsible="false"
    AnimCollapse="false" 
    TrackMouseOver="true" 
    AnchorHorizontal="100%">
        <ColumnModel ID="ColumnModel2" runat="server">
            <Columns>
                <%--ocultos--%>
                <ext:Column Header="Codigo" DataIndex="codigo" />
                <ext:Column Header="Nome" DataIndex="nome" Width="250" />
                <ext:Column Header="Comissão" DataIndex="comissao" Width="100" />
                <ext:Column Header="Status" DataIndex="ativo" Width="80" />
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


    <%-------------------------------------------Janela Inserir Vendedor-----------------------------------------%>

    <ext:Window
        ID="JanelaPrincipal" 
        runat="server" 
        Collapsible="false" 
        Height="320" 
        Icon="BuildingGo"
        Title="Inserir Vendedor" 
        Hidden="true"
        Modal="true"
        BodyStyle="background-color: #fff;" 
        Padding="8"
        Width="400">
        <Items>
        <ext:FormPanel ID="FormPrincipal" runat="server" Border="false">
            <Items>
                <ext:Portal ID="Portal1" runat="server" Layout="column" Height="240" Border="false">
                    <Items>
                        <ext:PortalColumn ID="PortalColumn1" runat="server" ColumnWidth="1" Border="false" DefaultAnchor="100%"
                            Layout="anchor" StyleSpec="padding:5px">
                            <Items>
                                <ext:Portlet ID="Portlet1" runat="server" Padding="5" Title="Informações">
                                    <Items>
                                        <%--Hidden do identificador--%>
                                        <ext:Hidden runat="server" ID="codigo" DataIndex="codigo" >
                                            <Listeners>
                                                <Change Handler="#{txtIdentificador}.setText(this.getValue());" />
                                            </Listeners>
                                        </ext:Hidden>
                
                                        <%-- Tipo de ação a ser executada, 1=adicionar, 2=atualizar --%>
                                        <ext:Hidden runat="server" ID="hd_tipo" DataIndex="hdtipo" />
                


                                        <ext:Label FieldLabel="Identificador" LabelWidth="75" ID="txtIdentificador" runat="server"/>

                                        <ext:TextField ID="txtNome" DataIndex="nome" runat="server" Width="260" FieldLabel="Nome" LabelWidth="75"/>
                                    
                                        <ext:ComboBox 
                                            ID="ComboCodUsuario" 
                                            runat="server" 
                                            DataIndex="codusu"
                                            Width="260" 
                                            StoreID="StoreCodUsuario" 
                                            FieldLabel="Cod Usuario" 
                                            EmptyText="Selecione"
                                            ValueField="cod"
                                            LabelWidth="75"
                                            Editable="true"
                                            TypeAhead="true" 
                                            Mode="Local"
                                            ForceSelection="true"
                                            TriggerAction="All"
                                            SelectOnFocus="true"
                                            DisplayField="nome"></ext:ComboBox>

                                        <ext:TextField InputType="Password" ID="txtCodigo" runat="server" Width="260" FieldLabel="Código PDV" LabelWidth="75"/>
                                        <ext:NumberField MaxLength="3" ID="txtComissao" DataIndex="comissao" runat="server" Width="260" FieldLabel="Perc. Comissão" LabelWidth="75"    />
                                        <ext:Checkbox ID="CheckAtivo" LabelWidth="75" runat="server" FieldLabel="Ativo" DataIndex="ativo"/>
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
        <Listeners>
            <AfterLayout Handler=" if(StoreFormulario.getCount() == 0)
                                  {
                                    StoreFormulario.reload();
                                  }" />
        </Listeners>
    </ext:Window>


    </form>
</body>
</html>
