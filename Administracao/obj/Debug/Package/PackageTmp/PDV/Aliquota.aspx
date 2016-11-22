<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Aliquota.aspx.cs" Inherits="Artebit.Restaurante.Administracao.PDV.Aliquota" %>

<html>
<head runat="server">
    <title>..:: Alíquota ::..</title>

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
    <%-- Store é o datasource, o resultado dos selects 
        
    <%-- Store da grid principal --%>
    <ext:Store ID="StorePrincipal" runat="server">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="Int" />
                    <ext:RecordField Name="posicao" Type="String" />
                    <ext:RecordField Name="aliquota" Type="String" />
                    <ext:RecordField Name="tipoImposto" Type="String" />
                    <ext:RecordField Name="alq" Type="String" />
                    <ext:RecordField Name="cst" Type="String" />
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
                    <ext:RecordField Name="aliquota" Type="String" />
                    <ext:RecordField Name="tipoImposto" Type="String" />
                    <ext:RecordField Name="alq" Type="String" />
                    <ext:RecordField Name="hdtipo" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <Listeners>
               <DataChanged Handler="  var record = this.getAt(0) || {};
                                        #{FormPrincipal}.getForm().loadRecord(record); 
                                        if(this.getAt(0) != null)
                                        {
                                            JanelaPrincipal.setTitle('Alíquota: '+ record.get('codigo') + ' - ' +record.get('aliquota') );
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
    Height="529" 
    AutoWidth="true"
    StoreID="StorePrincipal" 
    TrackMouseOver="true" 
    Border="false">
        <ColumnModel ID="ColumnModel1" runat="server">
            <Columns>
                <ext:Column Header="Código" DataIndex="codigo" Width="45" runat="server" /> 
                <ext:Column Header="Posição" DataIndex="posicao" runat="server" Width="70" />
                <ext:Column Header="Alíquota" DataIndex="aliquota" runat="server" Width="70" />
                <ext:Column Header="Tipo de Imposto" DataIndex="tipoImposto" runat="server" Width="100"/>
                <ext:Column Header="ALQ" DataIndex="alq"  runat="server" Width="50" />              
                <ext:Column Header="CST" DataIndex="cst"  runat="server" Width="50" />              
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
        Height="207" Icon="BookAdd" Title="Filial" Width="300">
        <Items>
        <ext:FormPanel ID="FormPrincipal" runat="server" Padding="7" >
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
                <ext:TextField ID="txtaliquota" FieldLabel="Nome Aliquota" DataIndex="aliquota" runat="server"/>
                
                <ext:ComboBox ID="ComboBoxTipoImposto" runat="server" Width="144" FieldLabel="Tipo de Imposto" DataIndex="tipoImposto">
                    <Items>
                        <ext:ListItem Value="ICMS" Text="ICMS"/>
                        <ext:ListItem Value="ISS" Text="ISS"/>
                    </Items>
                </ext:ComboBox>
                <ext:NumberField ID="txtalq" FieldLabel="ALQ" DataIndex="alq" runat="server"/>
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
