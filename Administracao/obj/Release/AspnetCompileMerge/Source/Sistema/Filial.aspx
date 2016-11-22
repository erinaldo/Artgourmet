<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Filial.aspx.cs" Inherits="Artebit.Restaurante.Administracao.Sistema.Filial" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>..:: Filial::.. </title>
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
                    <ext:RecordField Name="nome" Type="String" />
                    <ext:RecordField Name="cnpj" Type="String" />
                    <ext:RecordField Name="nomeEmpresarial" Type="String" />
                    <ext:RecordField Name="razaoSocial" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>

    <%-- Store da dos dados da Filial(que aparecem na janela) --%>
    <ext:Store ID="StoreFormulario" runat="server" AutoLoad="false" RemotePaging="true" OnRefreshData="StoreFormulario_RefreshData">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="Int" />
                    <ext:RecordField Name="nome" Type="String" />
                    <ext:RecordField Name="cnpj" Type="String" />
                    <ext:RecordField Name="nomeEmpresarial" Type="String" />
                    <ext:RecordField Name="razaoSocial" Type="String" />
                    <ext:RecordField Name="insEstadual" Type="Int" />
                    <ext:RecordField Name="insMunicipal" Type="String" />
                    <ext:RecordField Name="nomeFantasia" Type="String" />
                    <ext:RecordField Name="logradouro" Type="String" />
                    <ext:RecordField Name="numero" Type="String" />
                    <ext:RecordField Name="complemento" Type="String" />
                    <ext:RecordField Name="cep" Type="String" />
                    <ext:RecordField Name="bairro" Type="Int" />
                    <ext:RecordField Name="municipio" Type="String" />
                    <ext:RecordField Name="uf" Type="String" />
                    <ext:RecordField Name="bairro" Type="Int" />
                    <ext:RecordField Name="hdtipo" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <Listeners>
               <DataChanged Handler="  var record = this.getAt(0) || {};
                                        #{FormPrincipal}.getForm().loadRecord(record); 
                                        if(this.getAt(0) != null)
                                        {
                                            JanelaPrincipal.setTitle('Filial: '+ record.get('codigo') + ' - ' +record.get('nome') );
                                        }

                                        #{FormPrincipal}.body.unmask();
                                        "
                    Delay="15" />
               <BeforeLoad Handler="#{FormPrincipal}.body.mask('Carregando...', 'x-mask-loading');" />
               <LoadException Handler="#{FormPrincipal}.body.unmask();" />
               <Add Handler="PagingToolbar1.changePage(StoreFormulario.getTotalCount());" />

            </Listeners>
    </ext:Store>




    <%--Montar a GridView de Filial--%>
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
                <ext:Column Header="Nome" DataIndex="nome" runat="server" Width="300" />
                <ext:Column Header="CNPJ" DataIndex="cnpj" runat="server" Width="150" />
                <ext:Column Header="Nome Empresarial" DataIndex="nomeEmpresarial" runat="server" Width="200"/>
                <ext:Column Header="Razão social" DataIndex="razaoSocial"  runat="server" Width="200" />              
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

                        <ext:Button ID="btnNovoG" runat="server" Text="Novo" Icon="Add">
                            <DirectEvents>
                                <Click OnEvent="GridAcao">
                                  <ExtraParams>
                                        <ext:Parameter Name="codigo" Value="0" Mode="Raw" />
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
        Height="320" Icon="BookAdd" Title="Filial" Width="500">
        <Items>
        <ext:FormPanel ID="FormPrincipal" runat="server" >
            <Items>
                <ext:TabPanel ID="TabPanelForm" runat="server" DeferredRender="false">
                    <Items>
                        <%--Panel identificação--%>
                        <ext:Panel ID="Panel1" runat="server" HideMode="Offsets" Title="Identificação" Height="290" Layout="Column" Padding="7">
                            <Items>

                                <%--Hidden do identificador--%>
                                <ext:Hidden runat="server" ID="codigo" DataIndex="codigo" >
                                    <Listeners>
                                        <Change Handler="#{txtIdentificador}.setText(this.getValue());" />
                                    </Listeners>
                                </ext:Hidden>
                
                                <%-- Tipo de ação a ser executada, 1=adicionar, 2=atualizar --%>
                                <ext:Hidden runat="server" ID="hd_tipo" DataIndex="hdtipo" />

                                <ext:Container ID="Container1" runat="server" ColumnWidth="0.5">
                                    <Items>
                                        <%--Campo Identificador --%>
                                        <ext:Label ID="txtIdentificador" FieldLabel="Identificador" Height="26" LabelAlign="Top" Text="87" runat="server" Cls="ident" />

                                        <%--Campo Nome Fantasia --%>                                    
                                        <ext:TextField ID="txtNome" DataIndex="nome" FieldLabel="Nome" Width="200"  runat="server" LabelAlign="Top" /> 

                                        <%--Campo Nome Fantasia--%>
                                        <ext:TextField ID="txtNomeFantasia" DataIndex="nomeFantasia" FieldLabel="Nome Fantasia" Width="200"  runat="server" LabelAlign="Top" /> 

                                        <%--Campo Razão Social--%>
                                        <ext:TextField ID="txtRazaoSocial" DataIndex="razaoSocial" FieldLabel="Razão social" Width="200"  runat="server" LabelAlign="Top" />

                                    </Items>
                                </ext:Container>

                                <ext:Container ID="Container2" runat="server" ColumnWidth="0.5">
                                    <Items>
                                        <%--Campo CNPJ--%>
                                        <ext:NumberField ID="txtCNPJ" DataIndex="cnpj" MaxLength="14" FieldLabel="CNPJ" Width="100"  runat="server" LabelAlign="Top" />

                                        <%--Campo Nome Filialrial--%>                                    
                                        <ext:TextField ID="txtNomeEmpresarial" DataIndex="nomeEmpresarial" FieldLabel="Nome Empresarial" Width="200"  runat="server" LabelAlign="Top"/>

                                        <%--Campo Inscrição Municipal--%>
                                        <ext:NumberField ID="txtInsMunicipal" MaxLength="14" FieldLabel="Inscrição municipal" Width="110"  runat="server" 
                                                        LabelAlign="Top" DataIndex="insMunicipal" />

                                        <%--Campo Inscrição Estadual--%>
                                        <ext:NumberField ID="txtInsEstadual" MaxLength="14" FieldLabel="Inscrição estadual" Width="110"  runat="server"
                                                        LabelAlign="Top" DataIndex="insEstadual" />

                                    </Items>
                                </ext:Container>
                            
                        
                            </Items>
                        </ext:Panel>

                        <%--Panel Endereço--%>
                        <ext:Panel ID="Panel2" runat="server" HideMode="Offsets" Title="Endereço" Height="290" Padding="7">
                            <Items>
                                <%--Campo Logradouro --%>
                                <ext:TextField ID="txtLogradouro" DataIndex="logradouro" FieldLabel="Logradouro" LabelAlign="Top" Width="425" runat="server" Cls="ident" />
                                   
                                                    
                                <ext:Panel ID="Panel3" runat="server" Border="false" Layout="Column" Height="230">
                                    <Items>
                                        <ext:Container ID="Container3" runat="server" ColumnWidth="0.4">
                                            <Items>

                                                <%--Campo Complemento --%>
                                                <ext:TextField ID="txtComplemento" DataIndex="complemento" FieldLabel="Complemento" LabelAlign="Top" Width="170" runat="server" Cls="ident" />

                                                <%--Campo bairro--%>
                                                <ext:TextField ID="txtBairro" DataIndex="bairro" FieldLabel="Bairro" LabelAlign="Top" Width="170" runat="server" Cls="ident" />
                                            </Items>
                                        </ext:Container>

                                        <ext:Container ID="Container4" runat="server" ColumnWidth="0.33">
                                            <Items>
                                                 <%--Campo Numero --%>
                                                <ext:NumberField ID="txtNumero" DataIndex="numero" FieldLabel="Número" LabelAlign="Top" Width="50" MaxLength="6" runat="server" Cls="ident" />

                                                <%--Campo Municipio--%>
                                                <ext:TextField ID="txtMunicipio" DataIndex="municipio" FieldLabel="Município" LabelAlign="Top" Width="140" runat="server" Cls="ident" />
                                            </Items>
                                        </ext:Container>

                                        <ext:Container ID="Container5" runat="server" ColumnWidth="0.27">
                                            <Items>
                                                 <%--Campo CEP --%>
                                                <ext:NumberField ID="txtCEP" FieldLabel="CEP" DataIndex="cep" LabelAlign="Top" Width="80" MaxLength="8" runat="server" Cls="ident" />

                                                <%--Campo bairro--%>
                                                <ext:TextField ID="txtUf" FieldLabel="UF" DataIndex="uf" LabelAlign="Top" Width="30" MaxLength="2" runat="server" Cls="ident" />
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Panel>
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


    </form>
</body>
</html>
