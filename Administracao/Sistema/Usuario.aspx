<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="Artebit.Restaurante.Administracao.Sistema.Usuario" %>


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
    
        
        .iconCancelar
        {
            background-image:url(../Img/iconCancelar.png)
        }
    
        .iconEditar
        {
            background-image:url(../Img/iconeEditar.jpg)
        }
    </style>



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <link rel="stylesheet" type="text/css" id="theme" href="" /> 

    <script type="text/javascript">
        function trocaTema() {
            var themeUrl = '../temas/css/xtheme-Beige.css';
            Ext.util.CSS.swapStyleSheet("theme", themeUrl);
        }
     </script>

</head>
<body onload="trocaTema()">
    <form id="form1" runat="server">
<ext:ResourceManager ID="ResourceManager1" runat="server" />

 <%-- Store é o datasource, o resultado dos selects --%>
        <%-- Store da lista de usuários --%>
        <ext:Store ID="StorePrincipal" runat="server">
            <Reader>
                <ext:JsonReader IDProperty="codigo">
                    <Fields>
                        <ext:RecordField Name="codigo" Type="String" />
                        <ext:RecordField Name="nome" Type="String" />
                        <ext:RecordField Name="ativo" Type="String" />
                        <ext:RecordField Name="dataInclusao" Type="Date" />
                        <ext:RecordField Name="dataAlteracao" Type="Date" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <SortInfo Field="nome" Direction="ASC" />
        </ext:Store>

        <%--Store de usuários(Campos do forumlário)--%>
        <ext:Store ID="StoreFormulario" runat="server" AutoLoad="false" RemotePaging="true" OnRefreshData="StoreFormulario_RefreshData">
            <Reader>
                <ext:JsonReader IDProperty="codigo">
                    <Fields>
                        <ext:RecordField Name="codigo" Type="String" />
                        <ext:RecordField Name="codigo2" Type="String" />
                        <ext:RecordField Name="nome" Type="String" />
                        <ext:RecordField Name="senha" Type="String" />
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
                                                JanelaPrincipal.setTitle('Usuário: '+ record.get('codigo') + ' - ' +record.get('nome') );
                                            }

                                            StoreSistema.reload();
                                            #{FormPrincipal}.body.unmask();
                                            "
                        Delay="15" />
                   <BeforeLoad Handler="#{FormPrincipal}.body.mask('Carregando...', 'x-mask-loading');" />
                   <LoadException Handler="#{FormPrincipal}.body.unmask();" />
                   <Add Handler="PagingToolbar1.changePage(StoreFormulario.getTotalCount());" />

            </Listeners>
        </ext:Store>

        
        <%-- Store da lista de sistema --%>
        <ext:Store ID="StoreSistema" runat="server" OnRefreshData="StoreSistema_RefreshData">
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="codigo" Type="String" />
                        <ext:RecordField Name="nome" Type="String" />
                        <ext:RecordField Name="idPerfil" Type="String" />
                        <ext:RecordField Name="descricao" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

        <ext:Store ID="StorePerfil" runat="server">
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="codigo" Type="String" />
                        <ext:RecordField Name="nome" Type="String" />
                        <ext:RecordField Name="idPerfil" Type="String" />
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
            AutoFocus="true">

            <ColumnModel ID="ColumnModel1" runat="server">
                <Columns>
                    <ext:Column DataIndex="codigo" Header="Login" Width="100" Align="Center" />
                    <ext:Column Header="Nome" Width="250" DataIndex="nome" />
                    <ext:Column Header="Ativo" DataIndex="ativo" Width="100" Align="Center" />
                    <ext:DateColumn Header="Data Criacao" DataIndex="dataInclusao" Width="100" Format="dd/MM/yyyy" Align="Center" />
                    <ext:DateColumn Header="Data Alteracao" DataIndex="dataAlteracao" Width="100" Format="dd/MM/yyyy" Align="Center" />
                </Columns>
            </ColumnModel>

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

            <Plugins>
                <ext:GridFilters runat="server" ID="GridFilters1" Local="true">
                    <Filters>
                        <ext:StringFilter DataIndex="codigo" />
                        <ext:StringFilter DataIndex="nome" />
                        <ext:StringFilter DataIndex="ativo" />
                        <ext:DateFilter DataIndex="dtInclusao" />
                        <ext:DateFilter DataIndex="dtAlteracao">
                            <DatePickerOptions runat="server" TodayText="Agora" />
                        </ext:DateFilter>
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




        <%-- window para adicionar usuário --%>
        <ext:Window 
            ID="JanelaPrincipal" 
            runat="server" 
            Closable="false"
            Resizable="false"
            Height="270" 
            Icon="Add" 
            Title="Novo usuário"
            Draggable="false"
            Width="450"
            Modal="true"
            Padding="7"
            Hidden="True"
            Layout="Form">
            <Items>
                <ext:FormPanel ID="FormPrincipal" runat="server">
                    <Items>
                        <ext:TabPanel ID="TabPanelForm" runat="server" Height="490">
                            <Items>
                                <%-- Tab de informação --%>
                               <ext:Panel runat="server" Title="Identificação" Padding="8" >
                                    <Items>
                            
                                        <%-- Tipo de ação a ser executada, 1=adicionar, 2=atualizar--%>
                                        <ext:Hidden runat="server" ID="hdtipo" />

                                        <%-- Login do usuario --%>
                                        <ext:TextField 
                                            ID="txt_codUsuario" 
                                            runat="server" 
                                            FieldLabel="Login" 
                                            AllowBlank="true"
                                            DataIndex="codigo2"
                                            BlankText="Digite o codigo do usuário."
                                            AnchorHorizontal="90%"
                                            />

                                        <%-- Nome do usuario --%>
                                        <ext:TextField 
                                            ID="txt_nome" 
                                            runat="server" 
                                            FieldLabel="Nome"
                                            DataIndex="nome" 
                                            AllowBlank="true"
                                            BlankText="Digite o nome do usuário."
                                            Text=""
                                            Width="400"
                                            AnchorHorizontal="90%"
                                            />
                
                                        <%-- Senha do usuario --%>
                                        <ext:TextField 
                                            ID="txt_senha" 
                                            runat="server" 
                                            FieldLabel="Senha" 
                                            AllowBlank="true"
                                            InputType="Password"
                                            BlankText="Digite a senha do usuário."
                                            Text=""
                                            AnchorHorizontal="60%"
                                            />

                                            <ext:Checkbox ID="cbx_ativo" DataIndex="ativo" runat="server" BoxLabel="Ativo" Checked="true" />
                                
                                        </Items>
                                    </ext:Panel>

                                <%-- Tab de informação --%>
                                <ext:Panel runat="server" Title="Perfil" Padding="8" >
                                    <Items>
                                    
                                        <ext:GridPanel
                                            ID="GridPanel1" 
                                            runat="server" 
                                            DisableSelection="true"
                                            StoreID="StoreSistema"
                                            Height="150"
                                            Width="600">

                                                <ColumnModel runat="server">
                                                    <Columns>
                                                        <ext:Column Header="ID" DataIndex="codigo" />
                                                        <ext:Column Header="Sistema" DataIndex="nome" Width="140" Align="Center" />
                                                        <ext:Column Header="Perfil" DataIndex="idPerfil" Width="150" Editable="True">
                                                            <Editor>
                                                                <ext:ComboBox 
                                                                ID="ComboPerfil" StoreID="StorePerfil"
                                                                DisplayField="idPerfil"  ValueField="idPerfil"
                                                                DataIndex="idPerfil"
                                                                runat="server" Editable="false">
                                                                <Triggers>
                                                                    <ext:FieldTrigger Icon="Clear" Qtip="Remove selected" />
                                                                </Triggers>
                                                                <Listeners>
                                                                   <TriggerClick Handler="this.clearValue();" />
                                                                </Listeners>
                                                                </ext:ComboBox>
                                                            </Editor>
                                                        </ext:Column>
                                                    </Columns>
                                                </ColumnModel>

                                                <DirectEvents>
                                                   <RowClick OnEvent="CarregarCombo">
                                                        <ExtraParams>
                                                            <ext:Parameter Name="codigo" Value="this.store.getAt(rowIndex).data['codigo']" Mode="Raw" />
                                                            <ext:Parameter Name="perfil" Value="this.store.getAt(rowIndex).data['idPerfil']" Mode="Raw" />
                                                        </ExtraParams>
                                                    </RowClick>
                                                </DirectEvents>
                                                <Plugins>
                                                    <ext:EditableGrid runat="server"/>
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
                                <ext:Parameter Name="Perfil" Value="Ext.encode(#{StoreSistema}.getRecordsValues())" Mode="Raw" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="btnSalvarFrm" runat="server" Text="Salvar" Icon="Disk">
                    <DirectEvents>
                        <Click OnEvent="btnSalvarFrm_Click">
                            <EventMask ShowMask="true" Msg="Verificando..." MinDelay="500" />
                            <ExtraParams>
                                <ext:Parameter Name="Perfil" Value="Ext.encode(#{StoreSistema}.getRecordsValues())" Mode="Raw" />
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
