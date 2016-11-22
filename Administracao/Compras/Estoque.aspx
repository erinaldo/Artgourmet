<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Estoque.aspx.cs" Inherits="Artebit.Restaurante.Administracao.Compras.Estoque" %>

<html>
<head id="Head1" runat="server">
    <title>..:: Movimentação ::..</title>
    
     <link rel="stylesheet" type="text/css" id="theme" href="" /> 

    <script type="text/javascript">
        function trocaTema() {
            var themeUrl = '../temas/css/xtheme-Beige.css';
            Ext.util.CSS.swapStyleSheet("theme", themeUrl);
        }

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
<body onload="trocaTema()">
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <%-- Store é o datasource, o resultado dos selects 
        
    <%-- Store da grid principal --%>
    <ext:Store ID="StorePrincipal" runat="server">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="Int" />
                    <ext:RecordField Name="tipo" Type="String" />
                    <ext:RecordField Name="status" Type="String" />
                    <ext:RecordField Name="dataEmissao" Type="String" />
                    <ext:RecordField Name="observacao" Type="String" />
                    <ext:RecordField Name="valorTotal" Type="String" />
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
                    <ext:RecordField Name="numeroMov" Type="String" />
                    <ext:RecordField Name="serie" Type="String" />
                    <ext:RecordField Name="tipo" Type="String" />
                    <ext:RecordField Name="nomeTipo" Type="String" />
                    <ext:RecordField Name="idStatus" Type="String" />
                    <ext:RecordField Name="status" Type="String" />
                    <ext:RecordField Name="dataEmissao" Type="String" />
                    <ext:RecordField Name="observacao" Type="String" />
                    <ext:RecordField Name="valorTotal" Type="String" />
                    <ext:RecordField Name="local" Type="int" />
                    <ext:RecordField Name="hd_tipo" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <Listeners>
               <DataChanged Handler="  var record = this.getAt(0) || {};
                                        #{FormPrincipal}.getForm().loadRecord(record); 
                                        if(this.getAt(0) != null)
                                        {
                                            JanelaPrincipal.setTitle('Solicitação: '+ record.get('codigo'));
                                        }
                   
                                        StoreStatus.reload();   
                                        StoreItemMov.reload();
                                        StoreProduto.reload();
                                        StoreTipo.reload();
                                        StoreFornecedor.reload();
                                        StoreCondPagamento.reload();
                                        if(StoreLocal.getCount() == 0)
                                        {                     
                                            StoreLocal.reload();
                                        }

                                        #{FormPrincipal}.body.unmask();
                                        "
                    Delay="15" />
               <BeforeLoad Handler="#{FormPrincipal}.body.mask('Carregando...', 'x-mask-loading');" />
               <LoadException Handler="#{FormPrincipal}.body.unmask();" />
               <Add Handler="PagingToolbar1.changePage(StoreFormulario.getTotalCount());" />

            </Listeners>
    </ext:Store>
        
        <%-- Store dos dados que aparecem na janela --%>
    <ext:Store ID="StoreFormularioOC" runat="server" AutoLoad="false" RemotePaging="true" OnRefreshData="StoreFormularioOC_RefreshData">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="String" />
                    <ext:RecordField Name="seq" Type="String" />
                    <ext:RecordField Name="idMov" Type="String" />
                    <ext:RecordField Name="idProduto" Type="String" />
                    <ext:RecordField Name="produto" Type="String" />
                    <ext:RecordField Name="quantidade" Type="String" />
                    <ext:RecordField Name="precoUnitario" Type="String" />
                    <ext:RecordField Name="quantidadeReceber" Type="String" />
                    <ext:RecordField Name="dataEntrega" Type="Date" />
                    <ext:RecordField Name="valorTotal" Type="String" />
                    <ext:RecordField Name="valorUnitario" Type="String" />
                    <ext:RecordField Name="codUnd" Type="String" />
                    <ext:RecordField Name="observacao" Type="String" />
                    <ext:RecordField Name="valorDesconto" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <Listeners>
               <DataChanged Handler="  var record = this.getAt(0) || {};
                                        #{FormItensOC}.getForm().loadRecord(record); 
                                        if(this.getAt(0) != null)
                                        {
                                            JanelaItensOC.setTitle('Itens OC: '+ record.get('codigo'));
                                        }
                   
                                        StoreProduto.reload();
                  
                                        #{FormItensOC}.body.unmask();
                                        "
                    Delay="15" />
               <BeforeLoad Handler="#{FormItensOC}.body.mask('Carregando...', 'x-mask-loading');" />
               <LoadException Handler="#{FormItensOC}.body.unmask();" />
               <Add Handler="PagingToolbar2.changePage(StoreFormularioOC.getTotalCount());" />

            </Listeners>
    </ext:Store>
        
        
    <%-- Store do combobox tipo --%>
    <ext:Store ID="StoreTipo" runat="server"  AutoLoad="false" OnRefreshData="StoreTipo_RefreshData">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="String" />
                    <ext:RecordField Name="nome" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
         <ext:SortInfo Field="nome" Direction="ASC" />
    </ext:Store>
        
    <%-- Store da combobox fornecedor--%>
    <ext:Store ID="StoreFornecedor" runat="server"  AutoLoad="false" OnRefreshData="StoreFornecedor_RefreshData">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="Int" />
                    <ext:RecordField Name="nome" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
         <ext:SortInfo Field="nome" Direction="ASC" />
    </ext:Store>
        
    <%-- Store da combobox Local--%>
    <ext:Store ID="StoreLocal" runat="server"  AutoLoad="true" OnRefreshData="StoreLocal_RefreshData">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="Int" />
                    <ext:RecordField Name="nome" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
         <ext:SortInfo Field="nome" Direction="ASC" />
    </ext:Store>
        
     <%-- Store da combobox status--%>
    <ext:Store ID="StoreStatus" runat="server"  AutoLoad="false" OnRefreshData="StoreStatus_RefreshData">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="Int" />
                    <ext:RecordField Name="nome" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
         <ext:SortInfo Field="nome" Direction="ASC" />
    </ext:Store>
        
    <%--Store do combobox produto--%>
    <ext:Store ID="StoreProduto" runat="server" GroupField="nomecodigo" OnRefreshData="StoreProdutos_RefreshData">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="int" />
                    <ext:RecordField Name="nomecodigo" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
        
    <%--Store do combobox Unidadee--%>
    <ext:Store ID="StoreUnidade" runat="server" GroupField="descricao">
        <Reader>
            <ext:JsonReader IDProperty="codUnd">
                <Fields>
                    <ext:RecordField Name="codUnd" Type="String" />
                    <ext:RecordField Name="descricao" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
    
    <%--Store da grid de Itens de movimentação--%>
    <ext:Store ID="StoreItemMov" runat="server"  AutoLoad="false" OnRefreshData="StoreItensMov_RefreshData">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="String" />
                    <ext:RecordField Name="idMov" Type="String" />
                    <ext:RecordField Name="idProduto" Type="String" />
                    <ext:RecordField Name="produto" Type="String" />
                    <ext:RecordField Name="quantidade" Type="String" />
                    <ext:RecordField Name="precoUnitario" Type="String" />
                    <ext:RecordField Name="quantidadeReceber" Type="String" />
                    <ext:RecordField Name="dataEntrega" Type="String" />
                    <ext:RecordField Name="valorUnitario" Type="String" />
                    <ext:RecordField Name="codUnd" Type="String" />
                    <ext:RecordField Name="observacao" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
         <ext:SortInfo Field="produto" Direction="ASC" />
    </ext:Store>
        
    <%--Store dos itens de movimentação da Ordem de Compra--%>
    <ext:Store ID="StoreItemMovOC" runat="server"  AutoLoad="false">
        <Reader>
            <ext:JsonReader IDProperty="dados">
                <Fields>
                    <ext:RecordField Name="codigo" Type="String" />
                    <ext:RecordField Name="idMov" Type="String" />
                    <ext:RecordField Name="dados" Type="String" />
                    <ext:RecordField Name="idProduto" Type="String" />
                    <ext:RecordField Name="produto" Type="String" />
                    <ext:RecordField Name="quantidade" Type="String" />
                    <ext:RecordField Name="precoUnitario" Type="String" />
                    <ext:RecordField Name="quantidadeReceber" Type="String" />
                    <ext:RecordField Name="dataEntrega" Type="String" />
                    <ext:RecordField Name="valorUnitario" Type="String" />
                    <ext:RecordField Name="codUnd" Type="String" />
                    <ext:RecordField Name="observacao" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
            <ext:SortInfo Field="produto" Direction="ASC" />
    </ext:Store>
    
    <%-- Store da combobox Local--%>
    <ext:Store ID="StoreCondPagamento" runat="server"  AutoLoad="true" OnRefreshData="StoreCondPagamento_RefreshData">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="Int" />
                    <ext:RecordField Name="nome" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
         <ext:SortInfo Field="nome" Direction="ASC" />
    </ext:Store>
        

    <%--Montar a GridView--%>
    <ext:GridPanel ID="GridPrincipal" runat="server" Height="529" AutoWidth="true" StoreID="StorePrincipal"
        TrackMouseOver="true" Border="false">
        <ColumnModel ID="ColumnModel1" runat="server">
            <Columns>
                <ext:Column Header="Código" DataIndex="codigo" Width="45" runat="server" />
                <ext:Column Header="Tipo" DataIndex="tipo" runat="server" Width="100" Hidden="True" />
                <ext:Column Header="Status" DataIndex="status" runat="server" Width="100" />
                <ext:Column Header="Data de Emissão" DataIndex="dataEmissao" runat="server" Width="100" Hidden="True" />
                <ext:Column Header="Observação" DataIndex="observacao" runat="server" Width="100" />
                <ext:Column Header="Valor Total" DataIndex="valorTotal" runat="server" Width="100" />
            </Columns>
        </ColumnModel>
        <%--Coluna de select--%>
        
        
        <SelectionModel>
            <ext:CheckboxSelectionModel ID="RowSelectionModel1" runat="server" CheckOnly="True">
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
                    <ext:Button ID="btnEditarG" runat="server" Text="Editar" Icon="CommentEdit">
                        <DirectEvents>
                            <Click OnEvent="GridAcao">
                                <ExtraParams>
                                    <ext:Parameter Name="codigo" Value="GridPrincipal.getSelectionModel().getSelected().data['codigo']"
                                        Mode="Raw" />
                                    <ext:Parameter Name="command" Value="editar" Mode="Value" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                        <Listeners>
                            <Click Handler="JanelaPrincipal.show();" />
                        </Listeners>
                    </ext:Button>
                    <%--Excluir--%>
                    <ext:Button ID="btnExcluirG" runat="server" Text="Cancelar" Icon="Cross">
                       <DirectEvents>
                            <Click OnEvent="GridAcao">
                                <ExtraParams>
                                    <ext:Parameter Name="codigo" Value="GridPrincipal.getSelectionModel().getSelected().data['codigo']"
                                        Mode="Raw" />
                                    <ext:Parameter Name="command" Value="cancelar" Mode="Value" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <%--Ativar/Desativar--%>
                    <ext:Button ID="btnAtivarDesativarG" runat="server" Text="Ativar/Desativar" Icon="Wand">
                        <DirectEvents>
                            <Click OnEvent="GridAcao">
                                <ExtraParams>
                                    <ext:Parameter Name="codigo" Value="GridPrincipal.getSelectionModel().getSelected().data['codigo']"
                                        Mode="Raw" />
                                    <ext:Parameter Name="command" Value="ativar" Mode="Value" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <%--Ordem de compra--%>
                    <ext:Button ID="Button2" runat="server" Text="OC" Icon="TransmitAdd">
                        <DirectEvents>
                            <Click OnEvent="GridAcao">
                                <ExtraParams>
                                    <ext:Parameter Name="codigo" Value="GridPrincipal.getSelectionModel().getSelected().data['codigo']"
                                        Mode="Raw" />
                                    <ext:Parameter Name="command" Value="OC" Mode="Value" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    
                    <ext:ToolbarSeparator ID="ToolbarSeparator1" runat="server" />
                    
                    <ext:ComboBox runat="server" Width="110" ID="comboFiltroPrincipal" AutoScroll="True">
                    </ext:ComboBox>
                    
                    <ext:TriggerField runat="server" ID="txtFiltroPrincipal" AnchorHorizontal="100%"
                        Width="200px" EnableKeyEvents="true">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" />
                            <ext:FieldTrigger Icon="Search" />
                        </Triggers>
                        <Listeners>
                            <KeyUp Fn="filtraGridPrincipal" />
                            <TriggerClick Handler=" if(index == 0) { limpaFiltroPrincipal(); } " />
                        </Listeners>
                    </ext:TriggerField>
                    
                    <ext:ToolbarSeparator ID="ToolbarSeparator2" runat="server" />
                    
                    <ext:PagingToolbar Flat="True" HideRefresh="true" runat="server" ID="PageGrid" PageSize="21"
                        StoreID="StorePrincipal" DisplayMsg="">
                    </ext:PagingToolbar>

                </Items>
            </ext:Toolbar>
        </TopBar>
        
        
        <BottomBar>
            <%-- Este pagingtoolbar é necessário pois o processo de paging inicial do store só ocorre quando o paging toolbar está no bottom bar da grid --%>
            <ext:PagingToolbar Flat="True" HideRefresh="true" Hidden="True" runat="server" ID="PageBottom"
                PageSize="21" StoreID="StorePrincipal">
            </ext:PagingToolbar>
        </BottomBar>
        
        
        <Plugins>
            <ext:GridFilters runat="server" ID="GridFilters3" Local="true">
                <Filters>
                    <ext:StringFilter DataIndex="codigo" />
                    <ext:StringFilter DataIndex="tipo" />
                    <ext:StringFilter DataIndex="status" />
                    <ext:StringFilter DataIndex="dataEmissao" />
                    <ext:StringFilter DataIndex="valorTotal" />
                </Filters>
            </ext:GridFilters>
        </Plugins>
        
        
        <DirectEvents>
            <Command OnEvent="GridAcao">
                <ExtraParams>
                    <ext:Parameter Name="codigo" Value="record.data.codigo" Mode="Raw" />
                    <ext:Parameter Name="command" Value="command" Mode="Raw" />
                </ExtraParams>
            </Command>
            <RowDblClick OnEvent="GridAcao">
                <ExtraParams>
                    <ext:Parameter Name="codigo" Value="this.store.getAt(rowIndex).data['codigo']" Mode="Raw" />
                    <ext:Parameter Name="command" Value="editar" Mode="Value" />
                </ExtraParams>
            </RowDblClick>
        </DirectEvents>
        
        
        <Listeners>
            <Command Handler="if(command == 'editar') {JanelaPrincipal.show();}" />
            <CellClick Handler="if(columnIndex != 0) {this.getSelectionModel().selectRow(rowIndex);}" />
            <AfterLayout Handler="carregaComboFiltro();"></AfterLayout>
        </Listeners>
        
        <LoadMask ShowMask="True"></LoadMask>
    
    </ext:GridPanel>
    
    <%--------------------------------- JANELA PRINCIPAL ---------------------------------%>
        
    <%--Janela de adicionar e editar--%>
    <ext:Window ID="JanelaPrincipal" runat="server" Collapsible="false" Hidden="true"
        Modal="true" Height="500" Icon="BookAdd" Title="Filial" Width="800">
        <Items>
            <ext:FormPanel ID="FormPrincipal" runat="server" Padding="7">
                <Items>
                    <ext:TabPanel EnableTabScroll="true" ID="TabPanelForm" runat="server" Height="470" DeferredRender="false"  >
                        <Items>
                           
                             <%--Painel de Identificação--%>
                            <ext:Panel ID="PanelIdent" runat="server" HideMode="Offsets" Title="Identificação">
                                <Items>
                                    <ext:Portal ID="Portal1" runat="server" Layout="column" Height="360" Border="false">
                                        <Items>
                                        
                                            <ext:PortalColumn ID="PortalColumn1" runat="server" ColumnWidth="1" DefaultAnchor="100%"
                                                Layout="anchor" StyleSpec="padding:10px">
                                                <Items>
                                                    
                                                    <%--Identificação--%>
                                                    <ext:Portlet ID="Portlet1" runat="server" Padding="5" Title="Dados" Icon="DateGo">
                                                        <Items>
                                                            <ext:Container ID="Container1" runat="server" Layout="Column" Height="50">
                                                                <Items>
                                                                     <%--Hidden do identificador--%>
                                                                    <ext:Hidden runat="server" ID="codigo" DataIndex="codigo">
                                                                        <Listeners>
                                                                            <Change Handler="#{txtIdentificador}.setText(this.getValue());" />
                                                                        </Listeners>
                                                                    </ext:Hidden>
                    
                                                                    <%-- Tipo de ação a ser executada, 1=adicionar, 2=atualizar --%>
                                                                    <ext:Hidden runat="server" ID="hd_tipo" DataIndex="hd_tipo" />
                                                                    
                                                                         <ext:Panel ID="Panel6" runat="server" Border="false" ColumnWidth=".15" Header="false" Layout="Form" LabelAlign="Top">
                                                                            <Items>
                                                                                
                                                                                <ext:Label FieldLabel="Identificador" ID="txtIdentificador" Width="10" runat="server" ColumnWidth=".2" />
                                                                            </Items>
                                                                        </ext:Panel>    
                                                                    
                                                                    <ext:Panel ID="Panel7" runat="server" Border="false" ColumnWidth=".2" Header="false" Layout="Form" LabelAlign="Top">
                                                                        <Items>
                                                                            
                                                                            <ext:Hidden runat="server" ID="hdMovTipo" DataIndex="tipo"/>
                                                                               
                                                                            <ext:Hidden runat="server" ID="hdNomeTipo" DataIndex="nomeTipo">
                                                                                <Listeners>
                                                                                    <Change Handler="#{lbltipo}.setText(this.getValue());" />
                                                                                </Listeners>
                                                                            </ext:Hidden>
                                                                            
                                                                            <ext:Label 
                                                                                ID="lbltipo" 
                                                                                runat="server" 
                                                                                Width="300" 
                                                                                FieldLabel="Tipo de Movimento" 
                                                                                LabelWidth="120"
                                                                                Text="Vendas"/>
                                                                        </Items>
                                                                    </ext:Panel>

                                                                  
                                                                    <ext:Panel ID="Panel2" runat="server"  Border="false" ColumnWidth=".13" Header="false" Layout="Form" LabelAlign="Top">
                                                                        <Items>
                                                                            
                                                                            <ext:Hidden runat="server" ID="hdidStatus" DataIndex="idStatus"/>
                                                                            
                                                                            <ext:Hidden runat="server" ID="hdstatus" DataIndex="status">
                                                                                <Listeners>
                                                                                    <Change Handler="#{lblstatus}.setText(this.getValue());" />
                                                                                </Listeners>
                                                                            </ext:Hidden>

                                                                                  <ext:Label 
                                                                                ID="lblstatus" 
                                                                                runat="server" 
                                                                                FieldLabel="Status" 
                                                                                Text="Pendente"
                                                                                DisplayField="nome">
                                                                                </ext:Label>
                                                                         </Items>
                                                                    </ext:Panel>
                                                                
                                                                    <ext:Panel ID="Panel1" runat="server" Border="false" ColumnWidth=".3" Header="false" Layout="Form" LabelAlign="Top">
                                                                        <Items>
                                                                                    <ext:ComboBox 
                                                                                ID="ComboBoxLocal" 
                                                                                runat="server" 
                                                                                DataIndex="local"
                                                                                StoreID="StoreLocal" 
                                                                                FieldLabel="Local" 
                                                                                EmptyText="Selecione"
                                                                                ValueField="codigo"
                                                                                DisplayField="nome">
                                                                                </ext:ComboBox>
                                                                         </Items>
                                                                    </ext:Panel>

                                                                    
                                                                    <ext:Panel ID="Panel4" runat="server" Width="500" Border="false" ColumnWidth=".15" Header="false" Layout="Form" LabelAlign="Top">
                                                                        <Items>
                                                                            
                                                                            <ext:Hidden runat="server" ID="hdNumeroMov" DataIndex="numeroMov">
                                                                                <Listeners>
                                                                                    <Change Handler="#{lblNumeroMov}.setText(this.getValue());" />
                                                                                </Listeners>
                                                                            </ext:Hidden>

                                                                            <ext:Label 
                                                                                ID="lblNumeroMov" 
                                                                                runat="server" 
                                                                                Width="300" 
                                                                                FieldLabel="Nº Movimento" 
                                                                                LabelWidth="120"/>
                                                                        </Items>
                                                                    </ext:Panel>
                                                                    
                                                                    <ext:Panel ID="Panel5" runat="server" Width="500" Border="false" ColumnWidth=".15" Header="false" Layout="Form" LabelAlign="Top">
                                                                        <Items>
                                                                            
                                                                            <ext:Hidden runat="server" ID="hdSerie" DataIndex="serie">
                                                                                <Listeners>
                                                                                    <Change Handler="#{lblserie}.setText(this.getValue());" />
                                                                                </Listeners>
                                                                            </ext:Hidden>


                                                                            <ext:Label 
                                                                                ID="lblserie" 
                                                                                runat="server" 
                                                                                Width="300" 
                                                                                FieldLabel="Série" 
                                                                                LabelWidth="120"/>
                                                                        </Items>
                                                                    </ext:Panel>

                                                                </Items>
                                                            </ext:Container>
                                                           
                                                        </Items>
                                                    </ext:Portlet>
                                                
                
                                                    <%--Itens de movimento--%>
                                                    <ext:Portlet ID="Portlet2" runat="server" Padding="5" Title="Itens de Movimento" Icon="ServerStart">
                                                        <Items>
                                                        <ext:GridPanel ID="GridItensMovimento" Width="760" runat="server" Height="200" StoreID="StoreItemMov" 
                                                            AutoExpandColumn="produto">
                                                                <ColumnModel>
                                                                    <Columns>
                                                                        <ext:Column Header="Código" DataIndex="codigo" Width="60px"/>
                                                                        <ext:Column Header="Produto" DataIndex="produto" Width="120px"/>
                                                                        <ext:Column Header="Qtd" DataIndex="quantidade" Width="70px"/>
                                                                        <ext:Column Header="Preço Und" DataIndex="valorUnitario" Width="60px"/>
                                                                        <ext:Column Header="Observação" DataIndex="observacao" />
                                                                        
                                                                    </Columns>
                                                                </ColumnModel>
                                                                <TopBar>
                                                                    <ext:Toolbar ID="Toolbar4" runat="server">
                                                                        <Items>
                                                                            <ext:Button ID="btnNovoItemFT" runat="server" Text="Inserir Item" Icon="Add">
                                                                                <DirectEvents>
                                                                                    <Click OnEvent="btnNovoItemFT_Click">
                                                                                        <ExtraParams>
                                                                                            <ext:Parameter Name="Itens" Value="Ext.encode(#{StoreItemMov}.getRecordsValues())" Mode="Raw" />
                                                                                        </ExtraParams>
                                                                                    </Click>
                                                                                </DirectEvents>
                                                                            </ext:Button>
                                                                            <ext:Button ID="btnRemoveItemFT" runat="server" Text="Remover Item" Icon="ControlRemoveBlue">
                                                                                <DirectEvents>
                                                                                    <Click OnEvent="btnRemoveItemFT_Click">
                                                                                         <EventMask ShowMask="true" />
                                                                                    </Click>
                                                                                </DirectEvents>
                                                                            </ext:Button>
                                                                        </Items>
                                                                    </ext:Toolbar>
                                                                </TopBar>
                                                                
                                                                <SelectionModel>
                                                                    <ext:CheckboxSelectionModel ID="CheckboxSelectionModel3" runat="server" RowSpan="2" />
                                                                </SelectionModel>
                                                                
                                                                <Plugins>
                                                                    <ext:GridFilters runat="server" ID="GridFilters2" Local="true">
                                                                        <Filters>
                                                                            <ext:StringFilter DataIndex="codigo" />
                                                                            <ext:StringFilter DataIndex="produto" />
                                                                            <ext:StringFilter DataIndex="valorTotal" />
                                                                        </Filters>
                                                                    </ext:GridFilters>
                                                                </Plugins>
                                                            </ext:GridPanel>     
                                                        </Items>
                                                    </ext:Portlet>
                                                
                                                </Items>
                                            </ext:PortalColumn>
                                    
                                        </Items>
                                    </ext:Portal>
                                </Items>
                            </ext:Panel>
                            
                            <ext:Panel ID="PanelRateio" HideMode="Offsets" runat="server" Title="Rateio" Cls="AddProduto">
                                <Items>
                                </Items>
                            </ext:Panel>
                            
                            <ext:Panel ID="PanelDadosAdd" HideMode="Offsets" runat="server" Title="Dados Adicionais" Cls="AddProduto">
                                <Items>
                                    
                                    <ext:Portal ID="Portal2" runat="server" Layout="column" Height="360" Border="false">
                                        <Items>
                                            <ext:Portlet ID="Portlet3" runat="server" Padding="5" Title="Observações" Icon="ServerStart">
                                                <Items>
                                                    <ext:TextArea ID="txtObservacao" FieldLabel="Observação" Width="750" Height="130" LabelAlign="Top" runat="server"/>
                                                </Items>
                                            </ext:Portlet>
                                        </Items>
                                    </ext:Portal>

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
                              <ext:Parameter Name="Itens" Value="Ext.encode(#{StoreItemMov}.getRecordsValues())" Mode="Raw" />

                        </ExtraParams>
                    </Click>
                </DirectEvents>
            </ext:Button>
            <ext:Button ID="btnSalvarFrm" runat="server" Text="Salvar" Icon="Disk">
                <DirectEvents>
                    <Click OnEvent="btnSalvarFrm_Click">
                        <EventMask ShowMask="true" Msg="Verificando..." MinDelay="500" />
                        <ExtraParams>
                              <ext:Parameter Name="Itens" Value="Ext.encode(#{StoreItemMov}.getRecordsValues())" Mode="Raw" />

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
            <Show Handler="if(StoreFormulario.getCount() == 0)
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
        
        

    <%--------------------------------- JANELA ITENS ---------------------------------%>
        
    <%--Janela de Adicionar Itens--%>
    <ext:Window 
        ID="JanelaItens" 
        runat="server" 
        Collapsible="false" 
        Hidden="true"
        Modal="true" 
        Height="300" 
        Icon="BookAdd" 
        BodyStyle="background-color: #fff;" 
        Title="Adidiconar Itens" 
        Width="500" 
        Padding="7">
        
        <Items>
            <ext:TextField ID="txtSequencia" FieldLabel="Sequência" Width="250" Disabled="True" runat="server"/>
            
            <ext:ComboBox 
                ID="ComboBoxProduto" 
                runat="server" 
                Width="450" 
                FieldLabel="Produto" 
                StoreID="StoreProduto" 
                Editable="true"
                TypeAhead="false" 
                ForceSelection="False"
                HideTrigger="true"
                EnableKeyEvents="true"
                ValueField="codigo"
                IsRemoteValidation="True"
                DisplayField="nomecodigo">
                <Listeners>
                        <AfterRender Handler="this.mun(this.el, 'keyup', this.onKeyUp, this);
                                                                        this.mon(this.el, 'keyup', onKeyUp, this);" />
                </Listeners>
                <RemoteValidation  OnValidation="CarregaUnidade" ValidationEvent="select" EventOwner="Field" />
            </ext:ComboBox>
            
            <ext:TextArea ID="txtObservacaoItem" FieldLabel="Observação" Width="450" Height="50" runat="server"/>
                
            <ext:ComboBox 
                ID="ComboBoxUnidade" 
                runat="server" 
                Width="350" 
                FieldLabel="Unidade" 
                StoreID="StoreUnidade" 
                Editable="true"
                TypeAhead="false" 
                ForceSelection="False"
                EnableKeyEvents="true"
                ValueField="codUnd"
                IsRemoteValidation="True"
                DisplayField="descricao">
                <RemoteValidation  OnValidation="CalculaValorItem" ValidationEvent="select" EventOwner="Field" />
            </ext:ComboBox>
            
            <ext:Container ID="Container3" runat="server" Layout="Column" Width="450" Height="65">
                <Items>
                    <ext:Panel ID="Panel3" runat="server" ColumnWidth="0.4" Border="false">
                        <Items>
                                <ext:NumberField ID="txtQTDE" FieldLabel="Quantidade" Width="50" LabelAlign="Top" IsRemoteValidation="True" runat="server">
                                    <RemoteValidation  OnValidation="CalculaValorItem" />

                                </ext:NumberField>
                        </Items>
                    </ext:Panel>
                            
                     <ext:Panel ID="Panel8" runat="server" ColumnWidth="0.3" Border="false">
                        <Items>
                            <ext:TextField ID="txtCustoUnitario" Disabled="True" FieldLabel="Custo Unitário" Width="100" runat="server" LabelAlign="Top"/>
                        </Items>
                    </ext:Panel>
                    
                    <ext:Panel ID="Panel9" runat="server" ColumnWidth="0.3" Border="false">
                        <Items>
                            <ext:TextField ID="txtCustoTotal" Disabled="True" FieldLabel="Custo Total" Width="100" runat="server" LabelAlign="Top"/>
                        </Items>
                    </ext:Panel>
                    
                </Items>
            </ext:Container>

        </Items>
        
        <Buttons>
            <ext:Button ID="Button1" runat="server" Text="OK" Icon="Accept">
                <DirectEvents>
                    <Click OnEvent="btnOkItn_Click">
                        <EventMask ShowMask="true" Msg="Verificando..." MinDelay="500" />
                        <ExtraParams>
                            <ext:Parameter Name="Itens" Value="Ext.encode(#{StoreItemMov}.getRecordsValues())" Mode="Raw" />
                        </ExtraParams>
                    </Click>
                </DirectEvents>
                <Listeners>
                    <Click Handler="#{JanelaItens}.hide();" />
                </Listeners>
            </ext:Button>
           
            <ext:Button ID="Button3" runat="server" Text="Cancelar" Icon="Cancel">
                <Listeners>
                    <Click Handler="#{JanelaItens}.hide();" />
                </Listeners>
            </ext:Button>
        </Buttons>
    </ext:Window>
        
        
    <%--------------------------------- JANELA ORDEM DE COMPRA ---------------------------------%>
        
    <%--Janela de gerar Ordem de Compra--%>
    <ext:Window 
        ID="JanelaOC" 
        runat="server" 
        Collapsible="false" 
        Hidden="true"
        Modal="true" 
        Height="500" 
        Icon="BookAdd" 
        BodyStyle="background-color: #fff;" 
        Title="Adidiconar Itens" 
        Width="800" 
        Padding="7">
        <Items>
            <ext:FormPanel ID="FormItemOC" runat="server" Padding="7">
                <Items>
                    <ext:TabPanel EnableTabScroll="true" ID="TabPanel1" runat="server" Height="470" DeferredRender="false"  >
                        <Items>
                           
                             <%--Painel de Identificação--%>
                            <ext:Panel ID="Panel16" runat="server" HideMode="Offsets" Title="Identificação">
                                <Items>
                                    <ext:Portal ID="Portal3" runat="server" Layout="column" Height="360" Border="false">
                                        <Items>
                                        
                                            <ext:PortalColumn ID="PortalColumn2" runat="server" ColumnWidth="1" DefaultAnchor="100%"
                                                Layout="anchor" StyleSpec="padding:10px">
                                                <Items>
                                                    
                                                    <%--Identificação--%>
                                                    <ext:Portlet ID="Portlet6" runat="server" Padding="5" Title="Dados" Icon="DateGo">
                                                        <Items>
                                                            <ext:Container ID="Container4" runat="server" Layout="Column" Height="50">
                                                                <Items>
                                                                     <%--Hidden do identificador--%>
                                                                    <ext:Hidden runat="server" ID="hdidentificadoOC" DataIndex="codigo">
                                                                        <Listeners>
                                                                            <Change Handler="#{txtIdentificadorOC}.setText(this.getValue());" />
                                                                        </Listeners>
                                                                    </ext:Hidden>
                    
                                                                    <%-- Tipo de ação a ser executada, 1=adicionar, 2=atualizar --%>
                                                                    <ext:Hidden runat="server" ID="Hidden2" DataIndex="hd_tipo" />
                                                                    
                                                                         <ext:Panel ID="Panel17" runat="server" Border="false" ColumnWidth=".15" Header="false" Layout="Form" LabelAlign="Top">
                                                                            <Items>
                                                                                
                                                                                <ext:Label FieldLabel="Identificador" ID="txtIdentificadorOC" Width="10" runat="server" ColumnWidth=".2" />
                                                                            </Items>
                                                                        </ext:Panel>    
                                                                    
                                                                    <ext:Panel ID="Panel18" runat="server" Border="false" ColumnWidth=".2" Header="false" Layout="Form" LabelAlign="Top">
                                                                        <Items>
                                                                            
                                                                            <ext:Hidden runat="server" ID="hdtipoOC" DataIndex="tipo"/>
                                                                               
                                                                            <ext:Hidden runat="server" ID="hdNomeTipoOC" DataIndex="nomeTipo">
                                                                                <Listeners>
                                                                                    <Change Handler="#{lbltipoOC}.setText(this.getValue());" />
                                                                                </Listeners>
                                                                            
                                                                            </ext:Hidden>
                                                                            <ext:Label 
                                                                                ID="lbltipoOC" 
                                                                                runat="server" 
                                                                                Width="300" 
                                                                                FieldLabel="Tipo de Movimento" 
                                                                                LabelWidth="120"
                                                                                Text="Vendas"/>
                                                                        </Items>
                                                                    </ext:Panel>

                                                                  
                                                                    <ext:Panel ID="Panel19" runat="server"  Border="false" ColumnWidth=".13" Header="false" Layout="Form" LabelAlign="Top">
                                                                        <Items>
                                                                            
                                                                            <ext:Hidden runat="server" ID="hdidStatusOC" DataIndex="idStatus"/>
                                                                            
                                                                            <ext:Hidden runat="server" ID="hdstatusOC" DataIndex="status">
                                                                                <Listeners>
                                                                                    <Change Handler="#{lblstatusOC}.setText(this.getValue());" />
                                                                                </Listeners>
                                                                            </ext:Hidden>

                                                                                  <ext:Label 
                                                                                ID="lblstatusOC" 
                                                                                runat="server" 
                                                                                FieldLabel="Status" 
                                                                                Text="Pendente"
                                                                                DisplayField="nome">
                                                                                </ext:Label>
                                                                         </Items>
                                                                    </ext:Panel>
                                                                
                                                                    <ext:Panel ID="Panel20" runat="server" Border="false" ColumnWidth=".3" Header="false" Layout="Form" LabelAlign="Top">
                                                                        <Items>
                                                                                    <ext:ComboBox 
                                                                                ID="ComboBoxLocalOC" 
                                                                                runat="server" 
                                                                                DataIndex="local"
                                                                                StoreID="StoreLocal" 
                                                                                FieldLabel="Local" 
                                                                                EmptyText="Selecione"
                                                                                ValueField="codigo"
                                                                                DisplayField="nome">
                                                                                </ext:ComboBox>
                                                                         </Items>
                                                                    </ext:Panel>

                                                                    
                                                                    <ext:Panel ID="Panel21" runat="server" Width="500" Border="false" ColumnWidth=".15" Header="false" Layout="Form" LabelAlign="Top">
                                                                        <Items>
                                                                            
                                                                            <ext:Hidden runat="server" ID="hdNumeroMovOC" DataIndex="numeroMov">
                                                                                <Listeners>
                                                                                    <Change Handler="#{lblNumeroMovOC}.setText(this.getValue());" />
                                                                                </Listeners>
                                                                            </ext:Hidden>

                                                                            <ext:Label 
                                                                                ID="lblNumeroMovOC" 
                                                                                runat="server" 
                                                                                Width="300" 
                                                                                FieldLabel="Nº Movimento" 
                                                                                LabelWidth="120"/>
                                                                        </Items>
                                                                    </ext:Panel>
                                                                    
                                                                    <ext:Panel ID="Panel22" runat="server" Width="500" Border="false" ColumnWidth=".15" Header="false" Layout="Form" LabelAlign="Top">
                                                                        <Items>
                                                                            
                                                                            <ext:Hidden runat="server" ID="hdSerieOC" DataIndex="serie">
                                                                                <Listeners>
                                                                                    <Change Handler="#{lblserieOC}.setText(this.getValue());" />
                                                                                </Listeners>
                                                                            </ext:Hidden>


                                                                            <ext:Label 
                                                                                ID="lblserieOC" 
                                                                                runat="server" 
                                                                                Width="300" 
                                                                                FieldLabel="Série" 
                                                                                LabelWidth="120"/>
                                                                        </Items>
                                                                    </ext:Panel>

                                                                </Items>
                                                            </ext:Container>
                                                           
                                                           
                                                        </Items>
                                                    </ext:Portlet>
                                                
                
                                                    <%--Itens de movimento--%>
                                                    <ext:Portlet ID="Portlet5" runat="server" Padding="0" Title="Itens de Movimento" Icon="ServerStart">
                                                        <Items>
                                                         <ext:Label ID="Label1" runat="server" Text="Marque os itens para a Ordem de Compra. Clique duas vezes em cima do item para editar." StyleSpec="font-size:12; color:red;"></ext:Label>   
                                                        <ext:GridPanel ID="GridItensMovimentoOC" Width="760" runat="server" Height="180" StoreID="StoreItemMovOC" 
                                                            AutoExpandColumn="produto">
                                                                <ColumnModel>
                                                                    <Columns>
                                                                        <ext:Column Header="ID MOV" DataIndex="idMov" Width="60px"/>
                                                                        <ext:Column Header="Produto" DataIndex="produto" Width="120px"/>
                                                                        <ext:Column Header="Qtd" DataIndex="quantidade" Width="70px"/>
                                                                        <ext:Column Header="Preço Und" DataIndex="valorUnitario" Width="60px"/>
                                                                        <ext:Column Header="Observação" DataIndex="observacao" />
                                                                        
                                                                    </Columns>
                                                                </ColumnModel>
                                       
                                                                
                                                                <SelectionModel>
                                                                    <ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" RowSpan="2" />
                                                                </SelectionModel>
                                                            
                                                            <DirectEvents>
                                                                <RowDblClick OnEvent="GridAcaoOC">
                                                                    <ExtraParams>
                                                                        <ext:Parameter Name="codigo" Value="this.store.getAt(rowIndex).data['dados']" Mode="Raw" />
                                                                        <ext:Parameter Name="command" Value="editar" Mode="Value" />
                                                                    </ExtraParams>
                                                                </RowDblClick>
                                                            </DirectEvents>
                                                                
                                                                <Plugins>
                                                                    <ext:GridFilters runat="server" ID="GridFilters1" Local="true">
                                                                        <Filters>
                                                                            <ext:StringFilter DataIndex="codigo" />
                                                                            <ext:StringFilter DataIndex="produto" />
                                                                            <ext:StringFilter DataIndex="valorTotal" />
                                                                        </Filters>
                                                                    </ext:GridFilters>
                                                                </Plugins>
                                                            </ext:GridPanel>     
                                                        </Items>
                                                    </ext:Portlet>

                                                
                                                </Items>
                                            </ext:PortalColumn>
                                    
                                        </Items>
                                    </ext:Portal>
                                </Items>
                            </ext:Panel>
                            
                            <ext:Panel ID="Panel31" HideMode="Offsets" runat="server" Title="Valores" Cls="AddProduto">
                                <Items>
                                    <ext:Portal ID="Portal7" runat="server" Layout="column" Height="360" Border="false">
                                        <Items>
                                        
                                            <ext:PortalColumn ID="PortalColumn3" runat="server" ColumnWidth="1" DefaultAnchor="100%"
                                                Layout="anchor" StyleSpec="padding:10px">
                                                <Items>
                                                    
                                                    <%--Identificação--%>
                                                    <ext:Portlet ID="Portlet4" runat="server" Padding="5" Title="Valores" Icon="DateGo">
                                                        <Items>
                                                                <ext:Container ID="Container6" runat="server" Layout="Column" Height="50">
                                                                <Items>
                                                             
                                                                    <ext:Panel ID="Panel25" runat="server" Width="500" Border="false" ColumnWidth=".5" Header="false" Layout="Form" LabelAlign="Top">
                                                                        <Items>
                                                                            <ext:ComboBox 
                                                                                ID="ComboBoxFornecedorOC" 
                                                                                runat="server" 
                                                                                DataIndex="fornecedor"
                                                                                StoreID="StoreFornecedor" 
                                                                                FieldLabel="Fornecedor" 
                                                                                EmptyText="Selecione"
                                                                                ValueField="codigo"
                                                                                Width="300"
                                                                                DisplayField="nome">
                                                                                </ext:ComboBox>
                                                                        </Items>
                                                                    </ext:Panel>
                                                                    
                                                                    <ext:Panel ID="Panel26" runat="server" Width="500" Border="false" ColumnWidth=".5" Header="false" Layout="Form" LabelAlign="Top">
                                                                        <Items>
                                                                            <ext:ComboBox 
                                                                                ID="ComboBoxCondPgto" 
                                                                                runat="server" 
                                                                                StoreID="StoreCondPagamento" 
                                                                                FieldLabel="Condição Pagamento" 
                                                                                EmptyText="Selecione"
                                                                                ValueField="codigo"
                                                                                Width="300"
                                                                                DisplayField="nome">
                                                                                </ext:ComboBox>

                                                                        </Items>
                                                                        </ext:Panel>
                                                                        
                                                                </Items>
                                                            </ext:Container>
                                                                    
                                                                <ext:Container ID="Container8" runat="server" Layout="Column" Height="50">
                                                                <Items>
                                                                    <ext:Panel ID="Panel27" runat="server" Width="500" Border="false" ColumnWidth=".2" Header="false" Layout="Form" LabelAlign="Top">
                                                                        <Items>
                                                                            <ext:NumberField ID="txtValorTotal" Disabled="True"  FieldLabel="Valor Total" Width="120" runat="server" LabelAlign="Top"/>
                                                                        </Items>
                                                                    </ext:Panel>
                                                             
                                                                    <ext:Panel ID="Panel28" runat="server" Width="500" Border="false" ColumnWidth=".2" Header="false" Layout="Form" LabelAlign="Top">
                                                                        <Items>
                                                                            <ext:NumberField ID="txtPerFrete" FieldLabel="% Frete" Width="120" runat="server" LabelAlign="Top" IsRemoteValidation="True">
                                                                                <RemoteValidation  OnValidation="perFrete" />
                                                                            </ext:NumberField>
                                                                        </Items>
                                                                    </ext:Panel>
                                                            
                                                                    <ext:Panel ID="Panel29" runat="server" Width="500" Border="false" ColumnWidth=".2" Header="false" Layout="Form" LabelAlign="Top">
                                                                            <Items>
                                                                                <ext:NumberField ID="txtValorFrete"  FieldLabel="Valor Frete" Width="120" runat="server" LabelAlign="Top" IsRemoteValidation="True">
                                                                                <RemoteValidation  OnValidation="ValorFrete" />
                                                                            </ext:NumberField>
                                                                            </Items>
                                                                    </ext:Panel>
                                                                    
                                                                    <ext:Panel ID="Panel32" runat="server" Width="500" Border="false" ColumnWidth=".2" Header="false" Layout="Form" LabelAlign="Top">
                                                                        <Items>
                                                                            <ext:NumberField ID="txtPerDesconto"  FieldLabel="% Desconto" Width="120" runat="server" LabelAlign="Top" IsRemoteValidation="True">
                                                                                <RemoteValidation  OnValidation="perDesconto" />
                                                                            </ext:NumberField>
                                                                        </Items>
                                                                    </ext:Panel>
                                                                    
                                                                    <ext:Panel ID="Panel30" runat="server" Width="500" Border="false" ColumnWidth=".2" Header="false" Layout="Form" LabelAlign="Top">
                                                                        <Items>
                                                                            <ext:NumberField ID="txtValorDesconto"  FieldLabel="Valor Desconto" Width="120" runat="server" LabelAlign="Top" IsRemoteValidation="True">
                                                                                <RemoteValidation  OnValidation="ValorDesconto" />
                                                                            </ext:NumberField>
                                                                        </Items>
                                                                    </ext:Panel>
                                                                    
                                                                </Items>
                                                            </ext:Container>
                                                        </Items>
                                                    </ext:Portlet>
                                                </Items>
                                            </ext:PortalColumn>
                                        </Items>
                                    </ext:Portal>
                                </Items>
                            </ext:Panel>

                            <ext:Panel ID="Panel23" HideMode="Offsets" runat="server" Title="Rateio" Cls="AddProduto">
                                <Items>
                                </Items>
                            </ext:Panel>
                            
                            <ext:Panel ID="Panel24" HideMode="Offsets" runat="server" Title="Dados Adicionais" Cls="AddProduto">
                                <Items>
                                    
                                    <ext:Portal ID="Portal4" runat="server" Layout="column" Height="360" Border="false">
                                        <Items>
                                            <ext:Portlet ID="Portlet8" runat="server" Padding="5" Title="Observações" Icon="ServerStart">
                                                <Items>
                                                    <ext:TextArea ID="txtObservacaoOC" FieldLabel="Observação" Width="750" Height="130" LabelAlign="Top" runat="server"/>
                                                </Items>
                                            </ext:Portlet>
                                        </Items>
                                    </ext:Portal>

                                </Items>
                            </ext:Panel>
                            

                            </Items>
                        </ext:TabPanel>
                    </Items>
                </ext:FormPanel>
            </Items>
        
         <Buttons>
            <ext:Button ID="Button8" runat="server" Text="OK" Icon="Accept">
                <DirectEvents>
                    <Click OnEvent="btnOkOC_Click">
                        <EventMask ShowMask="true" Msg="Verificando..." MinDelay="500" />
                        <ExtraParams>
                              <ext:Parameter Name="Itens" Value="Ext.encode(#{StoreItemMov}.getRecordsValues())" Mode="Raw" />

                        </ExtraParams>
                    </Click>
                </DirectEvents>
            </ext:Button>
            <ext:Button ID="Button10" runat="server" Text="Cancelar" Icon="Cancel">
                <Listeners>
                    <Click Handler="#{JanelaOC}.hide();" />
                </Listeners>
            </ext:Button>
        </Buttons>

        

    </ext:Window>


    <%--------------------------------- JANELA ITENS OC ---------------------------------%>
        
    <%--Janela de adicionar e editar--%>
    <ext:Window 
        ID="JanelaItensOC" 
        runat="server" 
        Collapsible="false" 
        Hidden="true"
        Modal="true" 
        Height="400" 
        Icon="BookAdd" 
        BodyStyle="background-color: #fff;" 
        Title="Adidiconar Itens" 
        Width="500" 
        Padding="7">
        <Items>
        <ext:FormPanel ID="FormItensOC" runat="server" Padding="7">
        <Items>
            <ext:TextField ID="txtSequenciaOC" DataIndex="seq" FieldLabel="Sequência" Width="250" Disabled="True" runat="server"/>
            
            <ext:ComboBox 
                ID="ComboBoxProdutoOC" 
                runat="server" 
                Width="350" 
                DataIndex="idProduto"
                FieldLabel="Produto" 
                StoreID="StoreProduto" 
                Editable="true"
                TypeAhead="false" 
                ForceSelection="False"
                HideTrigger="true"
                EnableKeyEvents="true"
                ValueField="codigo"
                IsRemoteValidation="True"
                DisplayField="nomecodigo">
                <Listeners>
                        <AfterRender Handler="this.mun(this.el, 'keyup', this.onKeyUp, this);
                                                                        this.mon(this.el, 'keyup', onKeyUp, this);" />
                </Listeners>
                <RemoteValidation  OnValidation="CarregaUnidadeOC" ValidationEvent="select" EventOwner="Field" />
            </ext:ComboBox>
            
            <ext:TextArea ID="txtObservacaoItemOC" DataIndex="observacao" FieldLabel="Observação" Width="350    " Height="50" runat="server"/>
                
            <ext:ComboBox 
                ID="ComboBoxUnidadeOC" 
                runat="server" 
                Width="350" 
                FieldLabel="Unidade" 
                StoreID="StoreUnidade" 
                Editable="true"
                TypeAhead="false" 
                ForceSelection="False"
                EnableKeyEvents="true"
                DataIndex="codUnd"
                ValueField="codUnd"
                IsRemoteValidation="True"
                DisplayField="descricao">
                <RemoteValidation  OnValidation="CalculaValorItemOC" ValidationEvent="select" EventOwner="Field" />
            </ext:ComboBox>
            
            <ext:Container ID="Container2" runat="server" Layout="Column" Width="450" Height="65">
                <Items>
                    <ext:Hidden runat="server" ID="hdCodigo" DataIndex="codigo"></ext:Hidden>
                    

                    <ext:Panel ID="Panel10" runat="server" ColumnWidth="0.4" Border="false">
                        <Items>
                                <ext:NumberField ID="txtQTDEOC" Disabled="True" FieldLabel="Quantidade" DataIndex="quantidade" Width="70" LabelAlign="Top" IsRemoteValidation="True" runat="server">
                                    <RemoteValidation  OnValidation="CalculaValorItemOC" />

                                </ext:NumberField>
                        </Items>
                    </ext:Panel>
                            
                     <ext:Panel ID="Panel11" runat="server" ColumnWidth="0.3" Border="false">
                        <Items>
                            <ext:TextField ID="txtCustoUnitarioOC" 
                                DataIndex="valorUnitario" 
                                Disabled="false" 
                                FieldLabel="Custo Unitário" 
                                Width="100" 
                                runat="server" 
                                IsRemoteValidation="True"
                                LabelAlign="Top">
                                
                                 <RemoteValidation  OnValidation="CalculaCustoTotalOC" />

                            </ext:TextField>
                        </Items>
                    </ext:Panel>
                    
                    <ext:Panel ID="Panel12" runat="server" ColumnWidth="0.3" Border="false">
                        <Items>
                            <ext:TextField ID="txtCustoTotalOC" Disabled="True" FieldLabel="Custo Total" Width="100" runat="server" DataIndex="valorTotal" LabelAlign="Top"/>
                        </Items>
                    </ext:Panel>
                    
                </Items>
            </ext:Container>

            <ext:Container ID="Container5" runat="server" Layout="Column" Width="450" Height="65">
                <Items>
                    <ext:Panel ID="Panel13" runat="server" ColumnWidth="0.4" Border="false">
                        <Items>
                                <ext:NumberField ID="txtQtdRecebeOC" FieldLabel="Quantidade a Receber" DataIndex="quantidadeReceber" Width="130" 
                                    LabelAlign="Top" IsRemoteValidation="True" runat="server" >
                                    <RemoteValidation  OnValidation="ValidaQtdReceber" />
                                </ext:NumberField>
                        </Items>
                    </ext:Panel>
                            
                     <ext:Panel ID="Panel14" runat="server" ColumnWidth="0.3" Border="false">
                        <Items>
                            <ext:NumberField ID="txtVlrDesconto" DataIndex="valorDesconto" Disabled="false" FieldLabel="Valor Desconto" Width="100" 
                                runat="server" IsRemoteValidation="True" LabelAlign="Top">
                                  <RemoteValidation  OnValidation="CalculaCustoTotalOC" />
                            </ext:NumberField>
                        </Items>
                    </ext:Panel>
                    
                    <ext:Panel ID="Panel15" runat="server" ColumnWidth="0.3" Border="false">
                        <Items>
                            <ext:DateField 
                                ID="txtDataEntrega" 
                                runat="server"
                                Vtype="daterange"
                                DataIndex="dataEntrega"
                                LabelAlign="Top"
                                FieldLabel="Data Entrega"
                                Format="dd/MM/yyyy"
                                Width="100">                          
                            </ext:DateField> 
                            <%--<ext:TextField ID="txtDataEntrega" FieldLabel="Data Entrega" Width="100" runat="server" DataIndex="dataEntrega" LabelAlign="Top"/>--%>
                        </Items>
                    </ext:Panel>
                    
                </Items>
            </ext:Container>
                    </Items>
            </ext:FormPanel>

        </Items>
        
        <TopBar>
            <ext:Toolbar ID="Toolbar3" runat="server" Flat="true">
                <Items>
                    
                    <ext:PagingToolbar ID="PagingToolbar2" runat="server" PageSize="1" Flat="true" StoreID="StoreFormularioOC"
                        DisplayInfo="false">
                    </ext:PagingToolbar>
                    
                    <ext:ToolbarSeparator />
                    
                    <ext:ToolbarFill />
                    
                </Items>
            </ext:Toolbar>
        </TopBar>
        
        <Buttons>
            <ext:Button ID="Button4" runat="server" Text="OK" Icon="Accept">
                <DirectEvents>
                    <Click OnEvent="btnOkItn_ClickOC">
                        <EventMask ShowMask="true" Msg="Verificando..." MinDelay="500" />
                        <ExtraParams>
                            <ext:Parameter Name="Itens" Value="Ext.encode(#{StoreItemMov}.getRecordsValues())" Mode="Raw" />
                        </ExtraParams>
                    </Click>
                </DirectEvents>
                <Listeners>
                    <Click Handler="#{JanelaItensOC}.hide();" />
                </Listeners>
            </ext:Button>
           
            <ext:Button ID="Button5" runat="server" Text="Cancelar" Icon="Cancel">
                <Listeners>
                    <Click Handler="#{JanelaItensOC}.hide();" />
                </Listeners>
            </ext:Button>

        </Buttons>
        
        <Listeners>
                <Show Handler="if(StoreFormulario.getCount() == 0)
                                {
                                    StoreFormulario.reload();
                                }" />
            </Listeners>

    </ext:Window>



        

    </form>
</body>
</html>
