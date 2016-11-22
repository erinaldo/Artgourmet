<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Observacoes.aspx.cs" Inherits="Artebit.Restaurante.Administracao.Producao.Observacoes" %>


<html>
<head runat="server">
    <title>..:: Observações ::..</title>

    <link rel="stylesheet" type="text/css" id="theme" href="" /> 

    <script type="text/javascript">
        function trocaTema() {
            var themeUrl = '../temas/css/xtheme-Beige.css';
            Ext.util.CSS.swapStyleSheet("theme", themeUrl);
        }

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
<body onload="trocaTema()">
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <%-- Store é o datasource, o resultado dos selects 
        
    <%-- Store da grid de Observações --%>
    <ext:Store ID="storePrincipal" runat="server">
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

    <%-- Store da grid de Grupos --%>
    <ext:Store ID="StoreFormulario" runat="server" AutoLoad="false" RemotePaging="true" OnRefreshData="StoreFormulario_RefreshData">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="Int" />
                    <ext:RecordField Name="descricao" Type="String" />
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
                                            JanelaPrincipal.setTitle('Observação: '+ record.get('codigo') + ' - ' +record.get('descricao') );   
                                        }


                                        StoreListaGrupos.reload();
                                        StoreGruposEspecificos.reload();
                                        StoreListaProdutos.reload();
                                        StoreComboProdutos.reload();
                                        #{FormPrincipal}.body.unmask();
                                        "
                    Delay="15" />
               <BeforeLoad Handler="#{FormPrincipal}.body.mask('Carregando...', 'x-mask-loading');" />
               <LoadException Handler="#{FormPrincipal}.body.unmask();" />
               <Add Handler="PagingToolbar1.changePage(StoreFormulario.getTotalCount());" />

        </Listeners>
    </ext:Store>
    
    <%-- Store da grid de lista de grupos(todos)--%>
    <ext:Store ID="StoreListaGrupos" runat="server" GroupField="nome"  AutoLoad="false" OnRefreshData="StoreListaGrupos_RefreshData">
            <Reader>
                <ext:JsonReader IDProperty="codigo">
                    <Fields>
                        <ext:RecordField Name="codigo" Type="String" />
                        <ext:RecordField Name="nome" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

    <%-- Store da grid de lista de grupos(apenas os esolhidos pelo usuário) --%>
    <ext:Store ID="StoreGruposEspecificos" runat="server" OnRefreshData="StoreGruposEspecificos_RefreshData">
            <Reader>
                <ext:JsonReader IDProperty="codigo">
                    <Fields>
                        <ext:RecordField Name="codigo" Type="String" />
                        <ext:RecordField Name="nome" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        
         <%-- Store da grid de lista de grupos(todos)--%>
    <ext:Store ID="StoreListaProdutos" runat="server" GroupField="nome"  AutoLoad="false" OnRefreshData="StoreListaProdutos_RefreshData">
            <Reader>
                <ext:JsonReader IDProperty="codigo">
                    <Fields>
                        <ext:RecordField Name="codigo" Type="String" />
                        <ext:RecordField Name="nome" Type="String" />
                        <ext:RecordField Name="qtde" Type="String" />
                        <ext:RecordField Name="unidade" Type="String" />
                        <ext:RecordField Name="tipo" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

        
    <%-- Store da grid de lista específica de produtos(os que serão inseridos e podem ser filtrados) --%>
    <ext:Store ID="StoreComboProdutos" runat="server" GroupField="nome" OnRefreshData="StoreComboBoxProduto_RefreshData">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="String" />
                    <ext:RecordField Name="nome" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
        
        
    <%--Store da combobox Unidade da Produto da ficha técnica --%>
    <ext:Store ID="StoreUndFT" runat="server"  AutoLoad="false" OnRefreshData="StoreUndFT_RefreshData">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codUnd" Type="String" />
                    <ext:RecordField Name="descricao" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
        

    <%--Montar a GridView de observações--%>
    <ext:GridPanel 
    ID="GridPrincipal" 
    runat="server" 
    Height="529" 
    AutoWidth="true"
    StoreID="storePrincipal" 
    StripeRows="true" 
    Frame="true" 
    Collapsible="false"
    AnimCollapse="false" 
    TrackMouseOver="true" 
    AnchorHorizontal="100%">
        <ColumnModel ID="ColumnModel1" runat="server">
            <Columns>
                <%--coluna oculta--%>
                <ext:Column Header="Codigo" DataIndex="codigo" runat="server" />
                
                <ext:Column Header="Descrição" DataIndex="descricao" runat="server" />
                <ext:Column Header="Ativo" DataIndex="ativo" runat="server" Width="150" />
                
                
            </Columns>
        </ColumnModel>
        
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

                        <ext:PagingToolbar Flat="True"  HideRefresh="true" runat="server" ID="PageGrid" PageSize="21" StoreID="StorePrincipal" DisplayMsg="">
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
                        <ext:StringFilter DataIndex="Descrição" />
                        <ext:StringFilter DataIndex="Ativo" />
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
       
        <SelectionModel>
                <ext:CheckBoxSelectionModel ID="RowSelectionModel1" runat="server" CheckOnly="True"/>
        </SelectionModel>
        
        <Listeners>
                <Command Handler="if(command == 'editar') {JanelaPrincipal.show();}" />
                <CellClick Handler="if(columnIndex != 0) {this.getSelectionModel().selectRow(rowIndex);}" />
                <AfterLayout Handler="carregaComboFiltro();"></AfterLayout>
            </Listeners>

    </ext:GridPanel>
        
 


    <%--Janela de adicionar e editar Observações--%>
    <ext:Window 
    ID="JanelaPrincipal" 
    runat="server" 
    Collapsible="false" 
    Hidden="true" 
    Modal="true"
    Height="300" 
    Icon="BookAdd" 
    Title="Observações" 
    Width="500">
        <Items>
            <ext:FormPanel ID="FormPrincipal" runat="server" Padding="7">
                <Items>
                    <ext:TabPanel EnableTabScroll="true" ID="TabPanel1" runat="server" Height="200" DeferredRender="false"  >
                            <Items>

                            <%--Painel de Identificação--%>
                            <ext:Panel ID="PanelIdent" runat="server" HideMode="Offsets" Padding="7" Title="Identificação">
                                <Items>
                                    <%--Hidden do identificador--%>
                                    <ext:Hidden runat="server" ID="codigo" DataIndex="codigo" >
                                        <Listeners>
                                            <Change Handler="#{txtIdentificador}.setText(this.getValue());" />
                                        </Listeners>
                                    </ext:Hidden>
                
                                    <%-- Tipo de ação a ser executada, 1=adicionar, 2=atualizar --%>
                                    <ext:Hidden runat="server" ID="hd_tipo" DataIndex="hdtipo" />
                                    
                                     <%--Campo Identificado--%>
                                     <ext:Label FieldLabel="Identificador" ID="txtIdentificador" runat="server"/>

                                    <%--Campo Descrição --%>                                    
                                    <ext:TextField ID="txtDescricao" FieldLabel="Descrição" DataIndex="descricao" Width="400" AllowBlank="false" runat="server" />

                                    <%--Campo Ativo--%>
                                    <ext:Checkbox ID="CheckAtivo" DataIndex="ativo" FieldLabel="Ativo" runat="server">
                                    </ext:Checkbox>

                                </Items>
                            </ext:Panel>

                            <%--Painel de grupos--%>
                            <ext:Panel ID="Panel1" runat="server" HideMode="Offsets" Padding="7" Title="Grupos">
                                <Items>
                                    <ext:GridPanel ID="gridAdicionais" runat="server" Height="150" Width="460" StoreID="StoreListaGrupos" AutoExpandColumn="nome" >
                                        <ColumnModel>
                                            <Columns>
                                                <ext:Column Header="Código" DataIndex="codigo" />
                                                <ext:Column Header="Descrição" Width="200" DataIndex="nome" />
                                            </Columns>
                                        </ColumnModel>

                                        <TopBar>
                                            <ext:Toolbar ID="Toolbar2" runat="server">
                                                <Items>
                                                    <ext:Button ID="btnNovoAdicional" runat="server" Text="Inserir Grupo" Icon="Add">
                                                        <Listeners>
                                                            <Click Handler="JanelaGrupo.show();"></Click>
                                                        </Listeners>
                                                    </ext:Button>
                                                    <ext:Button ID="btnRemoverAdicional" runat="server" Text="Remover Grupo" Icon="ControlRemoveBlue">
                                                        <DirectEvents>
                                                            <Click OnEvent="RemoverGrupoAdicional"></Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                </Items>
                                            </ext:Toolbar>
                                        </TopBar>

                                        <SelectionModel>
                                            <ext:CheckboxSelectionModel ID="check" runat="server" RowSpan="2" />
                                        </SelectionModel>

                                        <Plugins>
                                            <ext:GridFilters runat="server" ID="GridFilters1" Local="true">
                                                <Filters>
                                                    <ext:StringFilter DataIndex="codigo" />
                                                    <ext:StringFilter DataIndex="nome" />
                                                </Filters>
                                            </ext:GridFilters>
                                            <ext:EditableGrid ID="EditableGrid2" runat="server" />
                                        </Plugins>

                                    </ext:GridPanel>
                                </Items>
                            </ext:Panel>

                            <%--Painel de Produtos--%>
                            <ext:Panel ID="Panel2" runat="server" HideMode="Offsets" Padding="7" Title="Produtos">
                                <Items>
                                    <ext:GridPanel ID="gridProdutos" runat="server" Height="150" Width="460" StoreID="StoreListaProdutos" AutoExpandColumn="nome" >
                                        <ColumnModel>
                                            <Columns>
                                                <ext:Column Header="Código" Width="45" DataIndex="codigo" />
                                                <ext:Column Header="Produto" DataIndex="nome" />
                                                <ext:Column Header="Quantidade" Width="65" DataIndex="qtde" />
                                                <ext:Column Header="Unidade" Width="60" DataIndex="unidade" />
                                                <ext:Column Header="Tipo" Width="60" DataIndex="tipo" />
                                            </Columns>
                                        </ColumnModel>

                                        <TopBar>
                                            <ext:Toolbar ID="Toolbar3" runat="server">
                                                <Items>
                                                    <ext:Button ID="Button1" runat="server" Text="Inserir Produto" Icon="Add">
                                                        <Listeners>
                                                            <Click Handler="JanelaProduto.show();"></Click>
                                                        </Listeners>
                                                    </ext:Button>
                                                    <ext:Button ID="Button2" runat="server" Text="Remover Produto" Icon="ControlRemoveBlue">
                                                        <DirectEvents>
                                                            <Click OnEvent="RemoverProdutoAdicional"></Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                </Items>
                                            </ext:Toolbar>
                                        </TopBar>

                                        <SelectionModel>
                                            <ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" RowSpan="2" />
                                        </SelectionModel>

                                        <Plugins>
                                            <ext:GridFilters runat="server" ID="GridFilters2" Local="true">
                                                <Filters>
                                                    <ext:StringFilter DataIndex="codigo" />
                                                    <ext:StringFilter DataIndex="nome" />
                                                </Filters>
                                            </ext:GridFilters>
                                            <ext:EditableGrid ID="EditableGrid1" runat="server" />
                                        </Plugins>

                                    </ext:GridPanel>
                                </Items>
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
                            <ext:Parameter Name="Grupos" Value="Ext.encode(#{StoreListaGrupos}.getRecordsValues())" Mode="Raw" />
                            <ext:Parameter Name="Produtos" Value="Ext.encode(#{StoreListaProdutos}.getRecordsValues())" Mode="Raw" />
                        </ExtraParams>
                    </Click>
                </DirectEvents>
            </ext:Button>
            <ext:Button ID="btnSalvarFrm" runat="server" Text="Salvar" Icon="Disk">
                <DirectEvents>
                    <Click OnEvent="btnSalvarFrm_Click">
                        <EventMask ShowMask="true" Msg="Verificando..." MinDelay="500" />
                        <ExtraParams>
                            <ext:Parameter Name="Grupos" Value="Ext.encode(#{StoreListaGrupos}.getRecordsValues())" Mode="Raw" />
                            <ext:Parameter Name="Produtos" Value="Ext.encode(#{StoreListaProdutos}.getRecordsValues())" Mode="Raw" />
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

     <%-- Janela inserir Grupo --%>
    <ext:Window
        ID="JanelaGrupo" 
        runat="server" 
        Collapsible="false" 
        Height="400" 
        Icon="BuildingGo"
        Title="Grupo" 
        Hidden="true"
        Modal="true"
        Padding="8"
        BodyStyle="background-color: #fff;" 
        Width="600">
            <Items>
            <ext:Hidden ID="tipo_produto" runat="server"></ext:Hidden>

                <ext:GridPanel ID="GridGruposEspecificos" StoreID="StoreGruposEspecificos" runat="server" Height="300" AutoExpandColumn="nome" Padding="8">
                    <ColumnModel>
                        <Columns>
                            <ext:Column Header="Código" DataIndex="codigo"/>
                            <ext:Column Header="Nome" DataIndex="nome" Width="200"/>
                        </Columns>
                    </ColumnModel>
                    <DirectEvents>
                        <Command OnEvent="InserirGrupo" />
                    </DirectEvents>
                    <SelectionModel>
                        <ext:CheckboxSelectionModel ID="SmGrupos" runat="server" RowSpan="2" />
                    </SelectionModel>
                    <Plugins>
                        <ext:GridFilters runat="server" ID="gfGridPrdAdd" Local="true">
                            <Filters>
                                <ext:StringFilter DataIndex="codigo" />
                                <ext:StringFilter DataIndex="nome" />
                            </Filters>
                        </ext:GridFilters>  
                    </Plugins>
                </ext:GridPanel>
            </Items>
            <Buttons>
                <ext:Button ID="btnOkAdicional" runat="server" Text="OK" Icon="Add" Width="80px">
                    <DirectEvents>
                        <Click OnEvent="InserirGrupo" />
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="btnCancelarAdicional" runat="server" Text="Cancelar" Icon="Cancel" Width="80px">
                    <Listeners>
                        <Click Handler="#{JanelaGrupo}.hide();" />
                    </Listeners>
                </ext:Button>
             </Buttons>
             <Listeners>
                <Show Handler="if(StoreFormulario.getCount() == 0) { StoreFormulario.reload(); } SmGrupos.clearSelections();" />
             </Listeners>
        </ext:Window>
        
        
    <%-- Janela inserir Produto --%>
        <ext:Window 
            ID="JanelaProduto"
            runat="server" 
            Collapsible="false" 
            Height="230" 
            Icon="BuildingGo"
            Title="Produto Ficha Técnica" 
            Hidden="true"
            Modal="true"
            Padding="8"
            BodyStyle="background-color: #fff;" 
            Width="400">
            <Items>
                <ext:ComboBox 
                    ID="ComboBoxProduto" 
                    runat="server" 
                    Width="350" 
                    FieldLabel="Produto" 
                    StoreID="StoreComboProdutos" 
                    Editable="true"
                    TypeAhead="false" 
                    ForceSelection="False"
                    EnableKeyEvents="true"
                    ValueField="codigo"
                    DisplayField="nome"
                    IsRemoteValidation="True">
                        <RemoteValidation  OnValidation="CarregarUnidadesProdutos" ValidationEvent="select" EventOwner="Field" />
                </ext:ComboBox>
                

                <ext:NumberField 
                    ID="txtI_Quantidade" 
                    runat="server" 
                    Width="200" 
                    FieldLabel="Quantidade" 
                    MaxLength="9" 
                    AllowDecimals="True"/>
                

                <ext:ComboBox 
                    ID="ComboBoxIUnd" 
                    runat="server" 
                    Width="200" 
                    StoreID="StoreUndFT" 
                    FieldLabel="Unidade" 
                    EmptyText="Selecione"
                    ValueField="codUnd"
                    DisplayField="descricao"
                    Editable="false"
                    TypeAhead="true" 
                    Mode="Local"
                    ForceSelection="true"
                    TriggerAction="All"
                    SelectOnFocus="true"
                    IsRemoteValidation="true"/>
                

                <ext:RadioGroup ID="RadioGroup1" runat="server" FieldLabel="Tipo">
                    <Items>
                        <ext:Radio ID="Radio1" runat="server" BoxLabel="Adicionar" InputValue="A" />
                        <ext:Radio ID="Radio2" runat="server" BoxLabel="Remover" InputValue="B" Checked="True"/>
                    </Items>
                </ext:RadioGroup> 


            </Items>
             <Buttons>
                <ext:Button ID="Button3" runat="server" Text="OK" Icon="Add" Width="80px">
                    <DirectEvents>
                        <Click OnEvent="InserirProduto" >
                            <ExtraParams>
                                <ext:Parameter Name="Produtos" Value="Ext.encode(#{StoreListaProdutos}.getRecordsValues())" Mode="Raw" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button4" runat="server" Text="Cancelar" Icon="Cancel" Width="80px">
                    <Listeners>
                        <Click Handler="#{JanelaProduto}.hide();" />
                    </Listeners>
                </ext:Button>
             </Buttons>
             <Listeners>
                <Show Handler="if(StoreFormulario.getCount() == 0) { StoreFormulario.reload(); } SmGrupos.clearSelections();" />
             </Listeners>
        </ext:Window>



    </form>
</body>
</html>
