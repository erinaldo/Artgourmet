<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fidelidade.aspx.cs" Inherits="Administracao.PDV.Fidelidade" %>

<html>
<head id="Head1" runat="server">
    <title>..:: Fidelidade ::..</title>
    
    

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



</head>


<body onload="trocaTema()">
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
        
    <%-- Store da grid de Fidelidade --%>
    <ext:Store ID="StorePrincipal" runat="server">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="Int" />
                    <ext:RecordField Name="nome" Type="String" />
                    <ext:RecordField Name="tipo" Type="String" />
                    <ext:RecordField Name="moeda" Type="String" />
                    <ext:RecordField Name="ativo" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>

    <ext:Store ID="StoreFormulario" runat="server" AutoLoad="false" RemotePaging="true" OnRefreshData="StoreFormulario_RefreshData">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="Int" />
                    <ext:RecordField Name="nome" Type="String" />
                    <ext:RecordField Name="tipo" Type="String" />
                    <ext:RecordField Name="moeda" Type="String" />
                    <ext:RecordField Name="ativo" Type="String" />
                    <ext:RecordField Name="diatodo" Type="String" />
                    <ext:RecordField Name="horarioini" Type="String" />
                    <ext:RecordField Name="horariofim" Type="String" />
                    <ext:RecordField Name="hdtipo" Type="String" />
                    <ext:RecordField Name="pontos" Type="Float" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <Listeners>
                   <DataChanged Handler="  var record = this.getAt(0) || {};
                                            #{FormPrincipal}.getForm().loadRecord(record); 
                                            if(this.getAt(0) != null)
                                            {
                                                JanelaPrincipal.setTitle('Plano: '+ record.get('codigo') + ' - ' +record.get('nome') );
                                            }
                                            
                                            hd_id.setValue(record.get('codigo'));

                                            storeProduto.reload();
                                            #{FormPrincipal}.body.unmask();
                                            "
                        Delay="15" />
                   <BeforeLoad Handler="#{FormPrincipal}.body.mask('Carregando...', 'x-mask-loading');" />
                   <LoadException Handler="#{FormPrincipal}.body.unmask();" />
                   <Add Handler="PagingToolbar1.changePage(StoreFormulario.getTotalCount());" />
        </Listeners>
    </ext:Store>

    <%--Store da grid de produto--%>
    <ext:Store ID="storeProduto" runat="server" OnRefreshData="StoreProduto_RefreshData" >
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




    <%--GridView de Fidelidades--%>
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
                <%--coluna oculta--%>
                <ext:Column Header="Codigo" DataIndex="codigo" />
                <ext:Column Header="Plano" DataIndex="nome" Width="150" />
                <ext:Column Header="Tipo" DataIndex="tipo" Width="80" />
                <ext:Column Header="Moeda" DataIndex="moeda" Width="80" />
                <ext:Column Header="Status" DataIndex="ativo" />

            </Columns>
        </ColumnModel>
         
        <Plugins>
            <ext:GridFilters runat="server" ID="GridFilters1" Local="true">
                <Filters>
                    <ext:StringFilter DataIndex="codigo" />
                    <ext:StringFilter DataIndex="nome" />
                    <ext:StringFilter DataIndex="ativo" />
                    <ext:StringFilter DataIndex="moeda" />
                    <ext:StringFilter DataIndex="tipo" />
                </Filters>
            </ext:GridFilters>
        </Plugins>

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





    <%--Janela de adicionar e editar Fidelidade--%>
    <ext:Window 
    ID="JanelaPrincipal" 
    runat="server" 
    Collapsible="false" 
    Hidden="true" 
    Modal="true"
    Height="350"
    Icon="BookAdd" 
    Title="Fidelidade" 
    Width="450">
        <Items>

            <ext:FormPanel ID="FormPrincipal" runat="server">
                <Items>
                    <ext:TabPanel ID="TabPanelForm" runat="server" Height="340" DeferredRender="false"  >
                        <Items>

                                <%--Aba de Informações--%>
                                <ext:Panel ID="panelIdent" runat="server" Padding="7" HideMode="Offsets" Title="Identificação" Layout="Column">
                                        <Items>
                                            <%--Campos ocultos--%>
                                            <ext:TextField Hidden="true" DataIndex="hdtipo" ID="hd_tipo" runat="server" />
                                            <ext:TextField Hidden="true" DataIndex="codigo" ID="hd_id" runat="server" />
                                    
                                            <ext:Container ID="Container0" runat="server" ColumnWidth="0.5">
                                                <Items>
                                                    <ext:TextField ID="txtmoeda" FieldLabel="Moeda" DataIndex="moeda" Width="150" runat="server" LabelAlign="Top" />
                                                    <ext:Checkbox ID="chkdiatodo" runat="server" FieldLabel="Dia todo" DataIndex="diatodo" LabelWidth="50" Checked="false">
                                                        <Listeners>
                                                            <Check Handler="if(checked){#{txthorarioini}.disable();#{txthorariofim}.disable();}
                                                                            else{#{txthorarioini}.enable();#{txthorariofim}.enable();}" />
                                                        </Listeners>
                                                    </ext:Checkbox>
                                                    
                                                    <ext:Label runat="server" FieldLabel="Horário" Width="10"/>
                                                    <ext:Panel runat="server" LabelAlign="Top" Layout="Column" Height="100" Border="false">
                                                        <Items>
                                                            <ext:Container runat="server" ColumnWidth="0.45">
                                                                <Items>
                                                                    <ext:TextField 
                                                                    ID="txthorarioini" 
                                                                    runat="server" 
                                                                    DataIndex="horarioini"
                                                                    Width="40"
                                                                    LabelAlign="Top"
                                                                    >
                                                                    </ext:TextField>
                                                                </Items>
                                                            </ext:Container>
                                                            
                                                            <ext:Container ID="Container2" runat="server"  ColumnWidth="0.1">
                                                                <Items>
                                                                    <ext:Label runat="server" Text="à" Margins="0 0 30 0" />
                                                                </Items>
                                                            </ext:Container>
                                                            
                                                            <ext:Container ID="Container3" runat="server" ColumnWidth="0.45">
                                                                <Items>
                                                                     <ext:TextField 
                                                                        ID="txthorariofim" 
                                                                        runat="server" 
                                                                        DataIndex="horariofim"
                                                                        Width="40"
                                                                        />
                                                    
                                                                </Items>
                                                            </ext:Container>
                                                        </Items>

                                                    </ext:Panel>

                                                    
                                                    
                                                </Items>
                                            </ext:Container>

                                            <ext:Container ID="Container1" runat="server" ColumnWidth="0.5">
                                                <Items>
                                                    <ext:TextField ID="txtnome" FieldLabel="Descrição" Width="200" DataIndex="nome" runat="server" Height="30" LabelAlign="Top" />
                                                    <ext:RadioGroup ID="RadioGroup1" runat="server" DataIndex="tipo" Height="30">
                                                        <Items>
                                                            <ext:Radio ID="Radio9" runat="server" DataIndex="tipo" InputValue="1" BoxLabel="Produto"
                                                                Cls="formLabel">
                                                                <Listeners>
                                                                    <Check Handler="if(checked){#{txtvalor}.disable(); #{panelproduto}.enable();}" />
                                                                </Listeners>
                                                            </ext:Radio>
                                                            <ext:Radio ID="Radio10" Checked="true" runat="server" DataIndex="tipo" InputValue="2" BoxLabel="Consumo"
                                                                Cls="formLabel">
                                                                <Listeners>
                                                                    <Check Handler="if(checked){#{txtvalor}.enable();#{panelproduto}.disable();}" />
                                                                </Listeners>
                                                            </ext:Radio>
                                                        </Items>
                                                    </ext:RadioGroup>
                                                    <ext:NumberField ID="txtvalor" FieldLabel="Pontos Por Real" DataIndex="pontos" Height="35" Width="200" LabelAlign="Top" runat="server" />
                                                   
                                                    <ext:Checkbox ID="chkativo" LabelWidth="40" Height="30" DataIndex="ativo" runat="server" FieldLabel="Ativo" Checked="true" ></ext:Checkbox>
                                                </Items>
                                            </ext:Container>

                                       </Items>
                                   </ext:Panel>

                                <%--Aba de Produtos--%>
                                <ext:Panel runat="server" Padding="7" ID="panelproduto" Height="330" HideMode="Offsets" Title="Produtos">
                                    <Items>
                                        <ext:GridPanel 
                                        ID="GridProduto" 
                                        runat="server" 
                                        Width="420"
                                        Height="280"
                                        AutoScroll="true"
                                        StoreID="storeProduto" 
                                        TrackMouseOver="true" 
                                        Border="false">
                                            <ColumnModel ID="ColumnModel3" runat="server">
                                                <Columns>
                                                
                                                    <ext:Column Header="Código" DataIndex="codigo" Align="Center" />
                                                    <ext:Column Header="Produto" DataIndex="nome" Width="211" />
                                                    <ext:Column Header="Valor por Real" DataIndex="valor" Width="100" />
                
                                                </Columns>
                                            </ColumnModel>
                                        
                                            <BottomBar>
                                                    <ext:PagingToolbar HideRefresh="true" runat="server" ID="PagingToolbar2" PageSize="20" StoreID="storeProduto">
                                                    </ext:PagingToolbar>
                                            </BottomBar>

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
                <Show Handler="StoreFormulario.reload();" />
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

