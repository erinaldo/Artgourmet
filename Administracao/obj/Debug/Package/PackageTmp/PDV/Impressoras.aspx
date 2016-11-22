<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Impressoras.aspx.cs" Inherits="Artebit.Restaurante.Administracao.PDV.Impressoras" %>

<html>
<head runat="server">
    <title>..:: Impressoras ::..</title>
    
    <script type="text/javascript">
        function confirmaExclusao() {
            // Função para confirmação de exclusão
            Ext.Msg.confirm('Alerta', 'Tem certeza que deseja excluir os itens selecionados?', function (btn) {
                //console.log(this, arguments);
                if (btn == 'yes') { Ext.net.DirectMethods.ExcluirVarios(); }
            });
        }

        function carregaComboFiltro() {
            //carrega combo do filtro da grid
            for (var i = 0; i < GridPrincipal.getStore().fields.getCount(); i++) {
                var valor = GridPrincipal.getStore().fields.get(i).name;
                comboFiltroPrincipal.removeByValue(valor);
                comboFiltroPrincipal.addItem(valor, valor);
            }

            //seleciona o campo padrão para filtro
            comboFiltroPrincipal.selectByIndex(1);
        }

        function filtraGridPrincipal() {
            //executa filtro da gridPrincipal
            var filtro = txtFiltroPrincipal.getValue();
            var campo = comboFiltroPrincipal.getSelectedItem().value;

            GridPrincipal.getStore().filter(campo, filtro, true, false);
        }

        function limpaFiltroPrincipal() {
            txtFiltroPrincipal.reset();
            GridPrincipal.getStore().clearFilter();
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

            //            Ext.each(selected, function (r) {
            //
            //            });
        };



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



    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />

        <%-- Store da grid de lista de Impressoras Inicial --%>
        <ext:Store ID="StorePrincipal" runat="server">
            <Reader>
                <ext:JsonReader IDProperty="id">
                    <Fields>
                        <ext:RecordField Name="id" Type="Int" />
                        <ext:RecordField Name="descricao" Type="String" />
                        <ext:RecordField Name="status" Type="String" />
                        <ext:RecordField Name="ligadoA" Type="String" />
                        <ext:RecordField Name="modelo" Type="String" />
                        <ext:RecordField Name="ip" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        
        <%-- Store da grid de lista de Impressoras Inicial --%>
        <ext:Store ID="StoreFormulario" runat="server">
            <Reader>
                <ext:JsonReader IDProperty="id">
                    <Fields>
                        <ext:RecordField Name="id" Type="Int" />
                        <ext:RecordField Name="descricao" Type="String" />
                        <ext:RecordField Name="nome" Type="String" />
                        <ext:RecordField Name="ligadoA" Type="String" />
                        <ext:RecordField Name="modelo" Type="String" />
                        <ext:RecordField Name="ip" Type="String" />
                        <ext:RecordField Name="tipo" Type="Int" />
                        <ext:RecordField Name="ativo" Type="Boolean" />
                        <ext:RecordField Name="imprimeProdutos" Type="Boolean" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <Listeners>
                <DataChanged Handler="if(JanelaPrincipal.isVisible())
                                      {
                                           var record = this.getAt(0) || {};
                                           #{FormPrincipal}.getForm().loadRecord(record); 
                                           if(this.getAt(0) != null)
                                           {
                                               JanelaPrincipal.setTitle('Impressora: '+ record.get('id') + ' - ' +record.get('descricao') );
                                           }
                                           GridMesas.getSelectionModel().clearSelections();
                                           
                                           StoreMesas.reload();
                                           storeInsumos.reload();
                                       }
                                        " Delay="15" />
                <LoadException Handler="if(FormPrincipal != null) {#{FormPrincipal}.body.unmask();}" />
                <Add Handler="PagingToolbar2.changePage(StoreFormulario.getTotalCount());" />
            </Listeners>
        </ext:Store>



    <%-- Store da grid de lista de Mesas --%>
    <ext:Store ID="StoreMesas" runat="server" OnRefreshData="StoreMesas_RefreshData">
        <Reader>
            <ext:JsonReader IDProperty="idmesa">
                <Fields>
                    <ext:RecordField Name="idmesa" Type="Int"/>
                    <ext:RecordField Name="obs" Type="String" />
                    <ext:RecordField Name="lugares" Type="Int" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
        
    
    <%-- Store da grid de LISTA DE PRODUTOS --%>
    <ext:Store ID="storeInsumos" runat="server" AutoLoad="false" OnRefreshData="storeInsumos_OnRefreshData">
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
    <ext:Store ID="storeInsumosImpressora" runat="server" AutoLoad="false" OnRefreshData="storeInsumos_OnRefreshData">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="String" />
                    <ext:RecordField Name="nome" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
        

    

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
    AutoExpandColumn="descricao"
    AnchorHorizontal="100%">
       <ColumnModel runat="server">
            <Columns>
                <%--ocultos--%>
                <ext:Column Hidden="true" DataIndex="id" />

                <ext:Column Header="Descrição" DataIndex="descricao" Width="250" />
                <ext:Column Header="Ligado à" DataIndex="ligadoA" Width="150" />
                <ext:Column Header="Modelo" DataIndex="modelo" Width="150" />
                <ext:Column Header="IP" DataIndex="ip" Width="100" />
                <ext:Column Header="Status" DataIndex="status" Width="80" />
            </Columns>
        </ColumnModel>
        
        <SelectionModel>
            <ext:CheckBoxSelectionModel ID="RowSelectionModel1" runat="server" CheckOnly="True" />
        </SelectionModel>
        
        <TopBar>
            <ext:Toolbar ID="Toolbar1" runat="server">
                <Items>
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
                    <ext:Button ID="btnEditarG" runat="server" Text="Editar" Icon="CommentEdit" >
                        <DirectEvents>
                            <Click OnEvent="GridAcao">
                                <ExtraParams>
                                    <ext:Parameter Name="codigo" Value="GridPrincipal.getSelectionModel().getSelected().data['id']" Mode="Raw" />
                                    <ext:Parameter Name="command" Value="editar" Mode="Value" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                        <Listeners>
                            <Click Handler="JanelaPrincipal.show();" />
                        </Listeners>
                    </ext:Button>
                    <ext:Button ID="btnExcluirG" runat="server" Text="Excluir" Icon="Cross" Disabled="true">
                        <Listeners>
                            <Click Handler="confirmaExclusao();"></Click>
                        </Listeners>
                    </ext:Button>
                    <ext:Button ID="btnAtivarDesativarG" runat="server" Text="Ativar/Desativar" Icon="Wand" >
                        <DirectEvents>
                            <Click OnEvent="GridAcao">
                                <ExtraParams>
                                    <ext:Parameter Name="codigo" Value="GridPrincipal.getSelectionModel().getSelected().data['id']" Mode="Raw" />
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

                    <ext:PagingToolbar Flat="True"  HideRefresh="true" runat="server" ID="PagingToolbar1" PageSize="21" StoreID="StorePrincipal" DisplayMsg="">
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
                    <ext:StringFilter DataIndex="descricao" />
                    <ext:StringFilter DataIndex="ligadoA" />
                    <ext:StringFilter DataIndex="status" />
                    <ext:StringFilter DataIndex="modelo" />
                </Filters>
            </ext:GridFilters>
        </Plugins>        

        <Listeners>
                <Command Handler="if(command == 'editar') {JanelaPrincipal.show();}" />
                <CellClick Handler="if(columnIndex != 0) {this.getSelectionModel().selectRow(rowIndex);}" />
                <AfterLayout Handler="carregaComboFiltro();"></AfterLayout>
        </Listeners>
         <DirectEvents>
                <RowDblClick OnEvent="GridAcao">
                    <ExtraParams>
                        <ext:Parameter Name="codigo" Value="this.store.getAt(rowIndex).data['id']" Mode="Raw" />
                        <ext:Parameter Name="command" Value="editar" Mode="Value"/>
                    </ExtraParams>
                </RowDblClick>
            </DirectEvents>
    </ext:GridPanel>


    <ext:Window
        ID="JanelaPrincipal" 
        runat="server" 
        Collapsible="false" 
        Height="450" 
        Icon="BuildingGo"
        Title="Inserir Impressora" 
        Hidden="true"
        Modal="true"
        BodyStyle="background-color: #fff;" 
        Width="700">
        <Items>
              <ext:FormPanel ID="FormPrincipal" runat="server" Border="false" Layout="Form" AutoWidth="true">
              <Items>
                  <ext:TabPanel ID="TabPanel1" runat="server" AutoWidth="true" Border="false" DeferredRender="false" Padding="0">
                   <Items>
                       
                       <ext:Panel ID="Tab1" runat="server" Title="Dados Básicos" AutoHeight="true" AutoWidth="true" HideMode="Offsets"
                                Padding="6">
                        <Items>
                  <ext:Portal ID="Portal1" runat="server" Layout="column" Height="360" >
                      <Items>
                          <ext:PortalColumn ID="PortalColumn1" runat="server" ColumnWidth="0.43" DefaultAnchor="100%"
                                            Layout="anchor" StyleSpec="padding:5px 5px 5px 5px">
                              <Items>
                                  <ext:Portlet ID="Portlet1" runat="server" Padding="5" Title="Informações">
                                      <Items>
                                          <%--Campos ocultos--%>
                                          <ext:TextField ID="impressoraID" Hidden="true" runat="server" DataIndex="id"/>
                                          <ext:TextField ID="hd_tipo" Hidden="true" runat="server" DataIndex="tipo"/>


                                          <ext:TextField ID="txtDescricao" runat="server" DataIndex="descricao" Width="260" FieldLabel="Descrição" LabelWidth="55"/>
                                          <ext:TextField ID="txtLigadoA" runat="server" DataIndex="ligadoA" Width="260" FieldLabel="Ligado à" LabelWidth="55"/>
                                          <ext:TextField ID="txtModelo" runat="server" DataIndex="modelo" Width="260" FieldLabel="Modelo" LabelWidth="55"/>
                                          <ext:TextField ID="txtIP" runat="server" DataIndex="ip" Width="260" FieldLabel="IP" LabelWidth="55"    />
                                          <ext:TextField ID="txtNome" runat="server" DataIndex="nome" Width="260" FieldLabel="Nome" LabelWidth="55"    />
                                          
                                      </Items>
                                  </ext:Portlet>
                                  <ext:Portlet runat="server" Title="Tipo de Impressora" Layout="Form">
                                      <Items>
                                          <ext:Checkbox ID="chkImprimeProdutos" DataIndex="imprimeProdutos" FieldLabel="Produção" runat="server" ></ext:Checkbox>
                                      </Items>
                                  </ext:Portlet>
                              </Items>
                          </ext:PortalColumn>
                          <ext:PortalColumn ID="PortalColumn2" runat="server" ColumnWidth="0.57" DefaultAnchor="100%"
                                            Layout="anchor" StyleSpec="padding:5px 5px 5px 5px">
                              <Items>
                                  <ext:Portlet ID="Portlet3" runat="server" Padding="5" Title="Mesas"  Height="305">
                                      <Items>
                                          <ext:GridPanel 
                                              ID="GridMesas" 
                                              runat="server" 
                                              Height="270"
                                              AutoWidth="true"
                                              StoreID="StoreMesas" 
                                              StripeRows="true" 
                                              Frame="true" 
                                              Collapsible="false"
                                              AnimCollapse="false" 
                                              TrackMouseOver="true" 
                                              AnchorHorizontal="100%">
                                              <ColumnModel runat="server">
                                                  <Columns>
                                                
                                                      <ext:Column Header="Nº Mesa" DataIndex="idmesa" Width="60" />
                                                      <ext:Column Header="Observação" DataIndex="obs" Width="180" />
                                                      <ext:Column Header="Lugares" DataIndex="lugares" Width="60" />

                                                  </Columns>
                                              </ColumnModel>
                                              <SelectionModel>
                                                  <ext:CheckboxSelectionModel ID="check" runat="server" RowSpan="2" />
                                              </SelectionModel>
                                          </ext:GridPanel>
                                      </Items>
                                  </ext:Portlet>
                              </Items>
                          </ext:PortalColumn>
                      </Items>
                  </ext:Portal>
                       
                       </Items>
                      </ext:Panel>

                       
		<ext:Panel ID="Tab3" runat="server" Title="Produtos Impressors" Height="327" Width="610" Layout="ColumnLayout"  HideMode="Offsets"
                                Padding="5">
                                <Items>
                                    <ext:MultiSelect ID="MultiSelect1" Legend="Produtos" AnchorHorizontal="50%" ColumnWidth=".45" runat="server" StoreID="storeInsumos" DisplayField="nome" ValueField="codigo"
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
                                    <ext:MultiSelect ID="MultiSelect2" Legend="Produtos Impressos" AnchorHorizontal="50%" ColumnWidth=".45" AnchorVertical="100%" runat="server" DisplayField="nome" ValueField="codigo"
                                    DropGroup="grupo1" StoreID="storeInsumosImpressora">
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
                                    <Activate Handler="if(Ext.isEmpty(storeInsumos.getAllRange())){ storeInsumos.reload(); }" />
                                </Listeners>
                            </ext:Panel>

                        </Items>
                    </ext:TabPanel>

              </Items>
              </ext:FormPanel>
        </Items>
        <TopBar>
                <ext:Toolbar ID="Toolbar6" runat="server" Flat="true">
                    <Items>
                        <ext:Button ID="btnAdd" runat="server" Text="" Icon="Add">
                            <DirectEvents>
                                <Click OnEvent="GridAcao">
                                    <EventMask ShowMask="True"></EventMask>
                                    <ExtraParams>
                                        <ext:Parameter Name="codigo" Value="0" Mode="Value" />
                                        <ext:Parameter Name="command" Value="novo" Mode="Value" />
                                    </ExtraParams>
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    
                        <ext:Button ID="Button12" runat="server" Text="" Icon="Cross" Disabled="True" >
                            <Listeners>
                                <Click Handler="#{FormPrincipal}.body.mask('Excluindo...', 'x-mask-loading');" />
                            </Listeners>
                        </ext:Button>

                        <ext:ToolbarSeparator />
                    
                        <ext:PagingToolbar ID="PagingToolbar2" runat="server" PageSize="1" Flat="true" StoreID="StoreFormulario"
                            DisplayInfo="false">
                        </ext:PagingToolbar>
                    
                        <ext:ToolbarSeparator />
                    
                        <ext:ToolbarFill />
                    </Items>
                </ext:Toolbar>
            </TopBar>
        <Buttons>
                <ext:Button ID="btnOkFrm" runat="server" Text="OK" Icon="Accept">
                    <DirectEvents>
                        <Click OnEvent="btnOkFrm_Click">
                            <EventMask ShowMask="true" Msg="Verificando..." MinDelay="500" />
                            <ExtraParams>
                                <ext:Parameter Name="Insumos" Value="Ext.encode(#{storeInsumosImpressora}.getRecordsValues())" Mode="Raw" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="btnSalvarFrm" runat="server" Text="Salvar" Icon="Disk">
                    <DirectEvents>
                        <Click OnEvent="btnSalvarFrm_Click">
                            <EventMask ShowMask="true" Msg="Verificando..." MinDelay="500" />
                            <ExtraParams>
                                <ext:Parameter Name="Insumos" Value="Ext.encode(#{storeInsumosImpressora}.getRecordsValues())" Mode="Raw" />
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
    </ext:Window>

    </form>
</body>
</html>
