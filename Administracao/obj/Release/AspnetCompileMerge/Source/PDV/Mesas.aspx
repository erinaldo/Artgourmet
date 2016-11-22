<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mesas.aspx.cs" Inherits="Artebit.Restaurante.Administracao.PDV.Mesas" %>


<html>
<head runat="server">
    <title> ..::Mesas::.. </title>

     
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
        
    <%-- Store da grid principal --%>
    <ext:Store ID="StorePrincipal" runat="server">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="Int" />
                    <ext:RecordField Name="qtdLugares" Type="String" />
                    <ext:RecordField Name="idStatus" Type="String" />
                    <ext:RecordField Name="idImpressora" Type="String" />
                    <ext:RecordField Name="observacao" Type="String" />
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
                    <ext:RecordField Name="qtdLugares" Type="String" />
                    <ext:RecordField Name="idStatus" Type="String" />
                    <ext:RecordField Name="idImpressora" Type="String" />
                    <ext:RecordField Name="observacao" Type="String" />
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
                                            JanelaPrincipal.setTitle('Mesa: '+ record.get('codigo'));
                                        }
                                        
                                        StoreImpressoras.reload();
                                        
                                        #{FormPrincipal}.body.unmask();
                                        "
                    Delay="15" />
               <BeforeLoad Handler="#{FormPrincipal}.body.mask('Carregando...', 'x-mask-loading');" />
               <LoadException Handler="#{FormPrincipal}.body.unmask();" />
               <Add Handler="PagingToolbar1.changePage(StoreFormulario.getTotalCount());" />

            </Listeners>
    </ext:Store>
    
    <%--Montar a combobox impresssoras--%>
    <ext:Store ID="StoreImpressoras" runat="server" AutoLoad="false" OnRefreshData="StoreImpressora_RefreshData">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="Int" />
                    <ext:RecordField Name="descricao" Type="String" />
                    <ext:RecordField Name="ligado" Type="String" />
                    <ext:RecordField Name="modelo" Type="String" />

                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>
   
     <%--Montar a GridView--%>
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
                <ext:Column Header="Código" DataIndex="codigo" Width="45" runat="server" /> 
                <ext:Column Header="Qnte Lugares" DataIndex="qtdLugares" runat="server" Width="90" />
                <ext:Column Header="Status" DataIndex="idStatus" runat="server" Width="70" />
                <ext:Column Header="Impressora" DataIndex="idImpressora" runat="server" Width="100"/>
                <ext:Column Header="Observação" DataIndex="observacao"  runat="server" Width="90" />              
                <ext:Column Header="Status" DataIndex="ativo"  runat="server" Width="70" />              
            </Columns>
        </ColumnModel>

        <%--Coluna de select--%>
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
                        <ext:StringFilter DataIndex="codigo" />
                        <ext:StringFilter DataIndex="nome" />
                        <ext:StringFilter DataIndex="cnpj" />
                        <ext:StringFilter DataIndex="nomeFantasia" />
                        <ext:StringFilter DataIndex="nomeEmpresarial" />
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

            <LoadMask ShowMask="True"></LoadMask>

    </ext:GridPanel>
    
   


    <%--Janela de adicionar e editar--%>
    <ext:Window ID="JanelaPrincipal" runat="server" Collapsible="false" Hidden="true" Modal="true"
        Height="300" Icon="BookAdd" Title="Filial" Width="500">
        <Items>
        <ext:FormPanel ID="FormPrincipal" runat="server" Padding="7" >
           <Items>
                <ext:TabPanel EnableTabScroll="true" ID="TabPanel1" runat="server" Height="200" DeferredRender="false">
                    <Items>
                        
                        <%--Panel de identificação--%>
                        <ext:Panel ID="Panel1" runat="server" HideMode="Offsets" Height="300" Padding="7" Title="Identificação">
                               <Items>
                                    <%--Hidden do identificador--%>
                                    <ext:Hidden runat="server" ID="codigo" DataIndex="codigo" >
                                        <Listeners>
                                            <Change Handler="#{txtIdentificador}.setText(this.getValue());" />
                                        </Listeners>
                                    </ext:Hidden>
                
                                    <%-- Tipo de ação a ser executada, 1=adicionar, 2=atualizar --%>
                                    <ext:Hidden runat="server" ID="hd_tipo" DataIndex="hdtipo" />
                
                                    <ext:Label FieldLabel="Identificador" ID="txtIdentificador" runat="server"/>
                
                                    <ext:NumberField ID="txtQtdeLugares" FieldLabel="Qtde de Lugares" Width="184" DataIndex="qtdLugares" runat="server"/>
                
                                    <ext:ComboBox ID="ComboBoxIdStatus" runat="server" FieldLabel="Status" DataIndex="idStatus">
                                        <Items>
                                            <ext:ListItem Value="1" Text="Ocupada"/>
                                            <ext:ListItem Value="2" Text="Livre"/>
                                            <ext:ListItem Value="3" Text="Reservada"/>
                                            <ext:ListItem Value="4" Text="Bloqueada"/>
                                        </Items>
                                    </ext:ComboBox>
                
                                    <ext:TextField ID="txtobs" FieldLabel="Observação" DataIndex="observacao" runat="server"/>
                                    <ext:Checkbox ID="Checkbox1" runat="server" FieldLabel="Ativo" DataIndex="ativo"/>
                               </Items>
                           </ext:Panel>
                        
                        <%--Panel de Impressoras--%>
                        <ext:Panel ID="Panel2" runat="server" HideMode="Offsets" Height="300" Title="Impressoras">
                            <Items>
                                <ext:GridPanel ID="gridImpressoras" runat="server" Height="150" Width="460" StoreID="StoreImpressoras" AutoExpandColumn="descricao" >
                                    <ColumnModel>
                                        <Columns>
                                            <ext:Column Header="Código" Width="50" DataIndex="codigo" />
                                            <ext:Column Header="Descrição" Width="760" DataIndex="descricao" />
                                            <ext:Column Header="Ligado à" Width="100" DataIndex="ligado" />
                                            <ext:Column Header="Modelo" Width="100" DataIndex="modelo" />
                                        </Columns>
                                    </ColumnModel>

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
