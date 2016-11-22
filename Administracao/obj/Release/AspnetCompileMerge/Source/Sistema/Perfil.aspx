<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="Artebit.Restaurante.Administracao.Sistema.Perfil" %>


<html>
<head runat="server">

    <title>..:: Perfis ::..</title>
    
    <link rel="stylesheet" type="text/css" id="theme" href="" /> 

    <script type="text/javascript">
        function trocaTema() {
            var themeUrl = '../temas/css/xtheme-Beige.css';
            Ext.util.CSS.swapStyleSheet("theme", themeUrl);
        }
     </script>

    
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

    
    <script type="text/javascript">
        var BeforeShow = function (e) {
            switch (e.field) {
                case "codigo":
                    this.getColumnModel().getCellEditor(e.column, e.row).field.allQuery = e.record.get('codigo');
                    break;
            }
        }
    </script>



    <%--Código CSS--%>
    <style type="text/css">
        .iconEditar
        {
            background-image:url(Img/iconeEditar.jpg)
        }
        
        .iconPerfil
        {
            background-image:url(Img/iconPerfil.png)
        }
        
        .iconInativar
        {
            background-image:url(Img/iconInativar.png)
        }
    </style>

</head>
<body onload="trocaTema()">
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />

        <%-- Store é o datasource, o resultado dos selects --%>
        <%-- Store que lista os perfis --%>
        <ext:Store ID="StorePrincipal" runat="server">
            <Reader>
                <ext:JsonReader IDProperty="codigo">
                    <Fields>
                        <ext:RecordField Name="codigo" Type="String" />
                        <ext:RecordField Name="nome" Type="String" />
                        <ext:RecordField Name="ativo" Type="String" />
                        <ext:RecordField Name="sistema" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <SortInfo Field="nome" Direction="ASC" />
        </ext:Store>

        <%-- Store que carrega os dados na edição --%>
        <ext:Store ID="StoreFormulario" runat="server" AutoLoad="false" RemotePaging="true" OnRefreshData="StoreFormulario_RefreshData">
            <Reader>
                <ext:JsonReader IDProperty="codigo">
                    <Fields>
                        <ext:RecordField Name="codigo" Type="String" />
                        <ext:RecordField Name="nome" Type="String" />
                        <ext:RecordField Name="ativo" Type="String" />
                        <ext:RecordField Name="sistema" Type="String" />
                        <ext:RecordField Name="valsenha" Type="Int" />
                        <ext:RecordField Name="hdtipo" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <Listeners>
                   <DataChanged Handler="  var record = this.getAt(0) || {};
                                            #{FormPrincipal}.getForm().loadRecord(record); 
                                            if(this.getAt(0) != null)
                                            {
                                                JanelaPrincipal.setTitle('Perfil: '+ record.get('codigo') + ' - ' +record.get('nome') );
                                            }

                                            store_pagina.load();
                                            Ext.net.DirectMethods.atualizaSessao(record.get('codigo'));
                                            #{FormPrincipal}.body.unmask();
                                            "
                        Delay="15" />
                   <BeforeLoad Handler="#{FormPrincipal}.body.mask('Carregando...', 'x-mask-loading');" />
                   <LoadException Handler="#{FormPrincipal}.body.unmask();" />
                   <Add Handler="PagingToolbar1.changePage(StoreFormulario.getTotalCount());" />
            </Listeners>
        </ext:Store>



        <%-- Store que lista os sistemas --%>
        <ext:Store ID="store_sistemas" runat="server">
            <Reader>
                <ext:JsonReader IDProperty="codSistema">
                    <Fields>
                        <ext:RecordField Name="codSistema" Type="String" />
                        <ext:RecordField Name="nome" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>


         <%-- Store que lista as página/telas --%>
        <ext:Store ID="store_pagina" runat="server"
            OnRefreshData="ComboBoxStore_RefreshData"
            AutoLoad="false">
            <Proxy>
                <ext:PageProxy />
            </Proxy>
            <Reader>
                <ext:JsonReader IDProperty="id">
                    <Fields>
                        <ext:RecordField Name="id" Type="Int" />
                        <ext:RecordField Name="nome" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>


        <%-- Store que lista as funcionalidades --%>
        <ext:Store ID="store_func" runat="server">
            <Reader>
                <ext:JsonReader IDProperty="id">
                    <Fields>
                        <ext:RecordField Name="id" Type="int" />
                        <ext:RecordField Name="janela" Type="int" />
                        <ext:RecordField Name="nome" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>





         <ext:GridPanel 
            ID="GridPrincipal" 
            runat="server" 
            Frame="true"
            StoreID="StorePrincipal"
            TrackMouseOver="true"
            AnchorHorizontal="100%"
            Height="529"
            AutoWidth="true"
            AutoFocus="true"         
            >
            <ColumnModel ID="cooo" runat="server">
                <Columns>
                    <ext:Column DataIndex="codigo" Header="Código" Width="100" Align="Center" />
                    <ext:Column Header="Nome" Width="200" DataIndex="nome" />
                    <ext:Column Header="Ativo" DataIndex="ativo" Width="100" Align="Center" />
                    <ext:Column Header="Sistema" DataIndex="sistema" Width="100" Align="Center" /> 
                </Columns>
            </ColumnModel>

            <Plugins>
                <ext:GridFilters runat="server" ID="GridFilters1" Local="true">
                    <Filters>
                        <ext:StringFilter DataIndex="codigo" />
                        <ext:StringFilter DataIndex="nome" />
                        <ext:StringFilter DataIndex="ativo" />
                        <ext:StringFilter DataIndex="sistema" />
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
                                        <ext:Parameter Name="codigo" Value="GridPrincipal.getSelectionModel().getSelected().data['codigo']" Mode="Raw" />
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
                                <Click ></Click>
                            </Listeners>
                        </ext:Button>
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
                <Command Handler="if(command == 'editar') {JanelaPrincipal.show();}" />
                <RowDblClick Handler="JanelaPrincipal.show();" />
                <CellClick Handler="if(columnIndex != 0) {this.getSelectionModel().selectRow(rowIndex);}" />
                <AfterLayout Handler="carregaComboFiltro();"></AfterLayout>
            </Listeners>

            <LoadMask ShowMask="True"></LoadMask>


        </ext:GridPanel>






        <%-- window para adicionar novo perfil --%>
        <ext:Window 
            ID="JanelaPrincipal" 
            runat="server" 
            Closable="false"
            Resizable="false"
            Height="300" 
            Icon="Add" 
            Title="Novo Perfil"
            Draggable="false"
            Width="550"
            Modal="true"
            Padding="7"
            Hidden="True"
            Layout="Form">
            <Items>

                <ext:FormPanel ID="FormPrincipal" runat="server">
                    <Items>
                        <ext:TabPanel ID="TabPanelForm" runat="server" Height="510">
                            <Items>

                                <%-- Tab de informação --%>
                                <ext:Panel ID="Panel1" runat="server" Title="Identificação" Padding="8" >
                                    <Items>

                                        <%-- ID do perfil --%>
                                        <ext:Hidden runat="server" DataIndex="codigo" ID="hd_id" />
                
                                        <%-- Tipo de ação a ser executada, 1=adicionar, 2=atualizar--%>
                                        <ext:Hidden runat="server" DataIndex="hdtipo" ID="hd_tipo" />

                                        <%-- Nome do control --%>
                                        <ext:TextField 
                                            ID="txt_nome" 
                                            DataIndex="nome"
                                            runat="server" 
                                            FieldLabel="Nome" 
                                            AllowBlank="false"
                                            BlankText="Digite o nome do perfil"
                                            Text=""
                                            Width="400"
                                            AnchorHorizontal="90%"
                                            />
                
                                        <%-- Lista as sistemas --%>
                                        <ext:ComboBox 
                                            ID="combo_sistemas"
                                            runat="server"
                                            DataIndex="sistema"
                                            StoreID="store_sistemas"
                                            Width="250"
                                            ValueField="codSistema"
                                            DisplayField="nome"
                                            EmptyText="Escolha..."
                                            FieldLabel="Sistema"
                                            AllowBlank="false"
                                            TypeAhead="true">
                                            <Listeners>
                                                <Change Handler="#{store_pagina}.load();" />
                                            </Listeners>
                                            </ext:ComboBox>
                   
                                        
                                        <ext:NumberField 
                                            ID="txt_valsenha" 
                                            DataIndex="valsenha"
                                            runat="server" 
                                            FieldLabel="Expiração da senha (dias)" 
                                            AllowBlank="false"
                                            BlankText="Digite o número de dias"
                                            Text=""
                                            Width="130"
                                            AnchorHorizontal="90%"
                                            />

                                        <%-- Ativo/Inativo --%>
                                        <ext:Checkbox ID="cbx_ativo" runat="server" Margins="0 10 0 0" DataIndex="ativo" BoxLabel="Ativo" Checked="true" />

                                    </Items>
                                </ext:Panel>

                                <ext:Panel ID="Panel2" runat="server" Title="Permissões" Padding="8" >
                                    <Items>

                                        <ext:GridPanel
                                                ID="GridPanel1" 
                                                runat="server" 
                                                DisableSelection="true"
                                                StoreID="store_pagina"
                                                TrackMouseOver="true"
                                                Height="150">

                                                    <ColumnModel ID="ColumnModel1" runat="server">
                                                        <Columns>
                                                            <ext:Column Header="Página / Tela" Width="300" DataIndex="nome" />
                                                            <ext:ImageCommandColumn>
                                                                <Commands>
                                                                <%--Botões--%>
                                                                    <ext:ImageCommand CommandName="perm" Icon="TableEdit" Text="Permissões">
                                                                        <ToolTip Text="Permissões" />
                                                                    </ext:ImageCommand>
                                                                </Commands>
                                                            </ext:ImageCommandColumn>
                                                        </Columns>
                                                    </ColumnModel>
                                                   <DirectEvents>
                                                        <Command OnEvent="InserirFunc">                                       
                                                            <ExtraParams>
                                                                <ext:Parameter Name="id" Value="record.data.id" Mode="Raw" />
                                                            </ExtraParams>
                                                        </Command> 
                                                    </DirectEvents>

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





        <%-- window para adicionar nova funcionalidade --%>
        <ext:Window 
            ID="Window2" 
            runat="server" 
            Closable="false"
            Resizable="false"
            Height="250" 
            Icon="Add" 
            Title="Funcionalidades"
            Draggable="false"
            Width="550"
            Modal="true"
            Padding="7"
            Hidden="True"
            Layout="Form">
            <Items>

                <ext:Panel ID="Panel4" runat="server" >
                    <Items>

                        <ext:GridPanel
                                ID="grid_func" 
                                runat="server" 
                                StoreID="store_func"
                                Height="150">

                                    <ColumnModel ID="ColumnModel2" runat="server">
                                        <Columns>
                                            <ext:Column DataIndex="janela" Hidden="true" />
                                            <ext:Column Header="Funcionalidade" Width="300" DataIndex="nome" />
                                        </Columns>
                                    </ColumnModel>
                                    <SelectionModel>
                                        <ext:CheckboxSelectionModel ID="CheckboxSelectionModel2" runat="server" RowSpan="2" />
                                    </SelectionModel>

                            </ext:GridPanel>

                    </Items>
                </ext:Panel>

            </Items>

            <Buttons>
                <ext:Button ID="Button1" runat="server" Text="Adicionar" Icon="Add">
                    <DirectEvents>
                        <Click OnEvent="salvar_func">
                            <EventMask ShowMask="true" Msg="Verificando..." MinDelay="500" />
                        </Click>
                    </DirectEvents>
                </ext:Button>

                <ext:Button ID="Button3" runat="server" Text="Cancelar" Icon="Decline">
                    <Listeners>
                        <Click Handler="#{Window2}.hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>




    </form>
</body>
</html>
