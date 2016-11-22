<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fornecedores.aspx.cs" Inherits="Artebit.Restaurante.Administracao.Producao.Fornecedores" %>

<html>
<head runat="server">
    <title>..:: Fornecedores ::..</title>
   
 <link rel="stylesheet" type="text/css" id="theme" href="" /> 

    <script type="text/javascript">
        function trocaTema() {
            var themeUrl = '../temas/css/xtheme-Beige.css';
            Ext.util.CSS.swapStyleSheet("theme", themeUrl);
        }

        var novoRegistro = false;

        // Função que decide qual botão de ação irá aparecer dependendo do status
        var prepareCommand = function (grid, command, record, row) {
            if (command.command == 'desativar' && (record.data.ativo == "Inativo")) {
                command.hidden = true;
                command.hideMode = 'visibility';
            }

            if (command.command == 'ativar' && record.data.ativo == "Ativo") {
                command.hidden = true;
                command.hideMode = 'visibility';
            }
        };

        var keyUpProductHandler = function (field, e) {
            
            var store = MultiSelect1.store;

            store.filter('nome', field.getValue(),true,false);
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
        
    </script>

     <script type="text/javascript">
         var doPostbackWithConfirmation = function () {

             Ext.Msg.confirm('Confirm', 'Tem certeza que deseja excluir os itens selecionados?', function (btn) {
                 //console.log(this, arguments);
                 if (btn == 'yes') { Ext.net.DirectMethods.ExcluirVarios(); }
             });

             return false;
         }
     </script>

    <style type="text/css">
        .formLabel
        {
            font-size: 13px;
            margin: 5px 0 0 0;
            padding: 0px;
        }
    </style>
</head>
<body onload="trocaTema()">
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />

    <%-- Store da grid de LISTA DE PRODUTOS --%>
    <ext:Store ID="storeListaFornecedores" runat="server" RemotePaging="true" AutoLoad="true" OnLoad="LoadStorePrincipal" OnRefreshData="CarregaGridPrincipal">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="String" />
                    <ext:RecordField Name="nome" Type="String" />
                    <ext:RecordField Name="cpfcnpj" Type="String" />
                    <ext:RecordField Name="razao" Type="String" />
                    <ext:RecordField Name="classificacao" Type="String" />
                    <ext:RecordField Name="categoria" Type="String" />
                    <ext:RecordField Name="criacao" Type="String" />
                    <ext:RecordField Name="insEstadual" Type="String" />
                    <ext:RecordField Name="insMunicipal" Type="String" />
                    <ext:RecordField Name="ativo" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <ext:SortInfo Field="nome" Direction="ASC" />
    </ext:Store>

    <%-- Store da grid de LISTA DE PRODUTOS --%>
    <ext:Store ID="storeGeral" runat="server" OnRefreshData="FetchRecord" RemotePaging="true" AutoLoad="false">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="String" />
                    <ext:RecordField Name="nome" Type="String" />
                    <ext:RecordField Name="cpfcnpj" Type="String" />
                    <ext:RecordField Name="razao" Type="String" />
                    <ext:RecordField Name="cnae" Type="String" />
                    <ext:RecordField Name="insEstadual" Type="String" />
                    <ext:RecordField Name="insMunicipal" Type="String" />
                    <ext:RecordField Name="insSuframa" Type="String" />
                    <ext:RecordField Name="classificacao" Type="String" />
                    <ext:RecordField Name="categoria" Type="String" />
                    <ext:RecordField Name="ativo" Type="Boolean" />
                    <ext:RecordField Name="novoRegistro" Type="String" />
                    <ext:RecordField Name="prCep" ServerMapping="endPrincipal.cep" Type="Auto" />
                    <ext:RecordField Name="prRua" ServerMapping="endPrincipal.rua" Type="Auto" />
                    <ext:RecordField Name="prNumero" ServerMapping="endPrincipal.numero" Type="Auto" />
                    <ext:RecordField Name="prBairro" ServerMapping="endPrincipal.bairro" Type="Auto" />
                    <ext:RecordField Name="prCidade" ServerMapping="endPrincipal.cidade" Type="Auto" />
                    <ext:RecordField Name="prComplemento" ServerMapping="endPrincipal.complemento" Type="Auto" />
                    <ext:RecordField Name="prUF" ServerMapping="endPrincipal.uf" Type="Auto" />
                    <ext:RecordField Name="prTelefone" ServerMapping="endPrincipal.telefone1" Type="Auto" />
                    <ext:RecordField Name="prCelular" ServerMapping="endPrincipal.telefone2" Type="Auto" />
                    <ext:RecordField Name="prEmail" ServerMapping="endPrincipal.email" Type="Auto" />
                    <ext:RecordField Name="paCep" ServerMapping="endPagamento.cep" Type="Auto" />
                    <ext:RecordField Name="paRua" ServerMapping="endPagamento.rua" Type="Auto" />
                    <ext:RecordField Name="paNumero" ServerMapping="endPagamento.numero" Type="Auto" />
                    <ext:RecordField Name="paBairro" ServerMapping="endPagamento.bairro" Type="Auto" />
                    <ext:RecordField Name="paCidade" ServerMapping="endPagamento.cidade" Type="Auto" />
                    <ext:RecordField Name="paComplemento" ServerMapping="endPagamento.complemento" Type="Auto" />
                    <ext:RecordField Name="paUF" ServerMapping="endPagamento.uf" Type="Auto" />
                    <ext:RecordField Name="paTelefone" ServerMapping="endPagamento.telefone1" Type="Auto" />
                    <ext:RecordField Name="paCelular" ServerMapping="endPagamento.telefone2" Type="Auto" />
                    <ext:RecordField Name="paEmail" ServerMapping="endPagamento.email" Type="Auto" />
                    <ext:RecordField Name="enCep" ServerMapping="endEntrega.cep" Type="Auto" />
                    <ext:RecordField Name="enRua" ServerMapping="endEntrega.rua" Type="Auto" />
                    <ext:RecordField Name="enNumero" ServerMapping="endEntrega.numero" Type="Auto" />
                    <ext:RecordField Name="enBairro" ServerMapping="endEntrega.bairro" Type="Auto" />
                    <ext:RecordField Name="enCidade" ServerMapping="endEntrega.cidade" Type="Auto" />
                    <ext:RecordField Name="enComplemento" ServerMapping="endEntrega.complemento" Type="Auto" />
                    <ext:RecordField Name="enUF" ServerMapping="endEntrega.uf" Type="Auto" />
                    <ext:RecordField Name="enTelefone" ServerMapping="endEntrega.telefone1" Type="Auto" />
                    <ext:RecordField Name="enCelular" ServerMapping="endEntrega.telefone2" Type="Auto" />
                    <ext:RecordField Name="enEmail" ServerMapping="endEntrega.email" Type="Auto" />
                </Fields>
            </ext:JsonReader>
        </Reader>
        <Listeners>
            <DataChanged Handler="  var record = this.getAt(0) || {};
                                    #{FormPanel1}.getForm().loadRecord(record); 
                                    if(this.getAt(0) != null)
                                    {
                                        WindowForm.setTitle('Cliente/Fornecedor: '+ record.get('codigo') + ' - ' +record.get('nome') );
                                    }
                                    storeInsumos.reload();
                                    "
                Delay="15" />
            <BeforeLoad Handler="#{FormPanel1}.body.mask('Carregando...', 'x-mask-loading');" />
            <Load Handler="#{FormPanel1}.body.unmask();" />
            <LoadException Handler="#{FormPanel1}.body.unmask();" />
        </Listeners>
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
    <ext:Store ID="storeInsumosFornecedor" runat="server" AutoLoad="false" OnRefreshData="storeInsumos_OnRefreshData">
        <Reader>
            <ext:JsonReader IDProperty="codigo">
                <Fields>
                    <ext:RecordField Name="codigo" Type="String" />
                    <ext:RecordField Name="nome" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>

    <%--Montar a GridView de fornecedores--%>
    <ext:GridPanel ID="GridPrincipal" runat="server" Height="510" AutoHeight="false"
        AutoWidth="true" StoreID="storeListaFornecedores" TrackMouseOver="true" Border="false"
        Frame="true" AutoExpandColumn="nome" StripeRows="true" EnableHdMenu="true" Selectable="true">
        <ColumnModel ID="ColumnModel1" runat="server">
            <Columns>
                <%--coluna oculta--%>
                <ext:Column Header="Codigo" DataIndex="codigo" runat="server" Width="60" />
                <ext:Column Header="Nome Fantasia" ColumnID="nome" DataIndex="nome" runat="server"
                    Width="250" />
                <ext:Column Header="Razão Social" DataIndex="razao" runat="server" Width="250" />
                <ext:Column Header="CPF/CNPJ" DataIndex="cpfcnpj" runat="server" />
                <ext:Column Header="Classificação" DataIndex="classificacao" runat="server" />
                <ext:Column Header="Categoria" DataIndex="categoria" runat="server" />
                <ext:Column Header="Data Criação" DataIndex="criacao" runat="server" />
                <ext:Column Header="Status" DataIndex="ativo" runat="server" />
                <ext:Column Header="Inscrição Estadual" DataIndex="insEstadual" runat="server" />
                <ext:Column Header="Inscrição Municipal" DataIndex="insMunicipal" runat="server" />
                <ext:ImageCommandColumn Width="200">
                    <Commands>
                        <ext:ImageCommand CommandName="desativar" Icon="Delete" Text="Desativar">
                            <ToolTip Text="Desativar" />
                        </ext:ImageCommand>
                        <ext:ImageCommand CommandName="ativar" Icon="ControlPlay" Text="Ativar">
                            <ToolTip Text="Ativar" />
                        </ext:ImageCommand>
                    </Commands>
                    <PrepareCommand Fn="prepareCommand" />
                </ext:ImageCommandColumn>
            </Columns>
        </ColumnModel>
        <TopBar>
            <ext:Toolbar runat="server">
                <Items>
                    <ext:Button ID="btnNovoG" runat="server" Text="Novo" Icon="Add">
                        <DirectEvents>
                            <Click OnEvent="AbrirJanela">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnEditarG" runat="server" Text="Editar" Icon="TableEdit" >
                        <DirectEvents>
                            <Click OnEvent="AbrirJanelaEdita">
                                <ExtraParams>
                                    <ext:Parameter Name="codigo" Value="GridPrincipal.getSelectionModel().getSelected().data['codigo']" Mode="Raw" />
                                </ExtraParams>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnExcluirG" runat="server" Text="Excluir" Icon="Cross">
                         <Listeners>
                                <Click Fn="doPostbackWithConfirmation" />
                         </Listeners>
                    </ext:Button>

                </Items>
            </ext:Toolbar>
        </TopBar>
        <DirectEvents>
            <Command OnEvent="GridComandos">
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
        <SelectionModel>
            <ext:CheckboxSelectionModel ID="smGrid" runat="server"  />
        </SelectionModel>
        <Plugins>
            <ext:GridFilters runat="server" ID="GridFilters3" Local="true">
                <Filters>
                    <ext:StringFilter DataIndex="codigo" />
                    <ext:StringFilter DataIndex="nome" />
                    <ext:StringFilter DataIndex="razao" />
                    <ext:StringFilter DataIndex="cpfcnpj" />
                    <ext:StringFilter DataIndex="categoria" />
                </Filters>
            </ext:GridFilters>
        </Plugins>
        <BottomBar>
            <ext:PagingToolbar ID="PagingToolbar2" runat="server" PageSize="20" Flat="true" StoreID="storeListaFornecedores"
                DisplayInfo="true">
            </ext:PagingToolbar>
        </BottomBar>
        <Listeners>
                <CellClick Handler="if(columnIndex != 0) {this.getSelectionModel().selectRow(rowIndex);}" />
        </Listeners>
    </ext:GridPanel>

   


    <%--Janela de adicionar e editar Fornecedores--%>
    <ext:Window ID="WindowForm" runat="server" Collapsible="false" Hidden="true"
        Modal="true" Height="470" Icon="BookAdd" Title="Cliente/Fornecedor: 1 - Fornecedor"
        Width="620" PaddingSummary="10px 0 0 0">
        <Items>
            <ext:FormPanel ID="FormPanel1" runat="server" Border="false" Layout="Form" AutoWidth="true">
                <Items>
                    <ext:Hidden runat="server" ID="hd_tipo" DataIndex="novoRegistro" />
                    <ext:TabPanel ID="TabPanel1" runat="server" AutoWidth="true" Border="false" DeferredRender="false" Padding="0">
                        <Items>
                            <ext:Panel ID="Tab1" runat="server" Title="Dados Básicos" AutoHeight="true" AutoWidth="true" HideMode="Offsets"
                                Padding="6">
                                <Items>
                                    <ext:Container ID="Container1" runat="server" Layout="Column" Height="45">
                                        <Items>
                                            <ext:Container ID="Container2" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".3">
                                                <Items>
                                                    <ext:TextField ID="txtIdClifor" DataIndex="codigo" runat="server" FieldLabel="Identificador"
                                                        AnchorHorizontal="50%" ReadOnly="true" Disabled="true">
                                                    </ext:TextField>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container3" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".3">
                                                <Items>
                                                    <ext:Checkbox runat="server" ID="checkAtivo" DataIndex="ativo" BoxLabel="Ativo" LabelAlign="Right"
                                                        AnchorHorizontal="95%">
                                                    </ext:Checkbox>
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Containerx" runat="server" Layout="Form" Height="95" LabelAlign="Top">
                                        <Items>
                                            <ext:TextField ID="txtNomeFantasia" DataIndex="nome" runat="server" FieldLabel="Nome Fantasia"
                                                AnchorHorizontal="100%">
                                            </ext:TextField>
                                            <ext:TextField ID="txtRazaoSocial" runat="server" DataIndex="razao" FieldLabel="Razão Social"
                                                AnchorHorizontal="100%">
                                            </ext:TextField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Containery" runat="server" Layout="Column" Height="45" LabelAlign="Top"
                                        AnchorHorizontal="100%">
                                        <Items>
                                            <ext:Container ID="Container4" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".5">
                                                <Items>
                                                    <ext:TextField ID="txtCGC" DataIndex="cpfcnpj" runat="server" FieldLabel="CNPJ" AnchorHorizontal="99%">
                                                    </ext:TextField>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container12" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".5">
                                                <Items>
                                                    <ext:TextField ID="txtCNAE" runat="server" DataIndex="cnae" FieldLabel="CNAE" AnchorHorizontal="100%">
                                                    </ext:TextField>
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container7" runat="server" Layout="Column" Height="45" LabelAlign="Top"
                                        AnchorHorizontal="100%">
                                        <Items>
                                            <ext:Container ID="Container8" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".33">
                                                <Items>
                                                    <ext:TextField ID="txtIEstadual" runat="server" DataIndex="insEstadual" FieldLabel="Insc. Estadual"
                                                        AnchorHorizontal="99%">
                                                    </ext:TextField>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container9" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".33">
                                                <Items>
                                                    <ext:TextField ID="txtIMunicipal" runat="server" DataIndex="insMunicipal" FieldLabel="Insc. Municipal"
                                                        AnchorHorizontal="99%">
                                                    </ext:TextField>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container5" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".33">
                                                <Items>
                                                    <ext:TextField ID="txtISuframa" runat="server" DataIndex="insSuframa" FieldLabel="Insc. Suframa"
                                                        AnchorHorizontal="100%">
                                                    </ext:TextField>
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container6" runat="server" Layout="Column" Height="105" LabelAlign="Top"
                                        AnchorHorizontal="100%">
                                        <Items>
                                            <ext:Container ID="Container10" runat="server" LabelAlign="Top" Layout="ColumnLayout"
                                                ColumnWidth=".5" AnchorHorizontal="90%">
                                                <Items>
                                                    <ext:FieldSet ID="FieldSet1" runat="server" Width="270" Title="Classificação" Padding="7"
                                                        Layout="Container" Collapsible="true">
                                                        <Items>
                                                            <ext:RadioGroup ID="rdClassificacao" runat="server" ColumnsNumber="1">
                                                                <Items>
                                                                    <ext:Radio ID="Radio9" runat="server" DataIndex="classificacao" InputValue="1" BoxLabel="Cliente"
                                                                        Cls="formLabel">
                                                                    </ext:Radio>
                                                                    <ext:Radio ID="Radio10" runat="server" DataIndex="classificacao" InputValue="3" BoxLabel="Fornecedor"
                                                                        Cls="formLabel">
                                                                    </ext:Radio>
                                                                    <ext:Radio ID="Radio3" runat="server" DataIndex="classificacao" InputValue="2" BoxLabel="Ambos"
                                                                        Cls="formLabel">
                                                                    </ext:Radio>
                                                                </Items>
                                                            </ext:RadioGroup>
                                                        </Items>
                                                    </ext:FieldSet>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container11" runat="server" LabelAlign="Top" Layout="ColumnLayout"
                                                ColumnWidth=".5" AnchorHorizontal="95%">
                                                <Items>
                                                    <ext:FieldSet ID="FieldSet2" Width="290" runat="server" Title="Categoria" Padding="7"
                                                        Layout="Container" Collapsible="true">
                                                        <Items>
                                                            <ext:RadioGroup ID="rdTipoPessoa" runat="server" ColumnsNumber="1">
                                                                <Items>
                                                                    <ext:Radio ID="Radio1" runat="server" DataIndex="categoria" InputValue="1" BoxLabel="Pessoa Jurídica"
                                                                        Cls="formLabel">
                                                                    </ext:Radio>
                                                                    <ext:Radio ID="Radio2" runat="server" DataIndex="categoria" InputValue="2" BoxLabel="Pessoa Física"
                                                                        Cls="formLabel">
                                                                    </ext:Radio>
                                                                </Items>
                                                            </ext:RadioGroup>
                                                        </Items>
                                                    </ext:FieldSet>
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:Panel>
                            <ext:Panel ID="Tab2" runat="server" Title="Endereço" AutoHeight="true"  Width="610" Padding="0" HideMode="Offsets">
                                <Items>
                                    <ext:TabPanel ID="TabPanel2" runat="server" AutoWidth="true" Border="false">
                                        <Items>
                                            <ext:Panel ID="Panel3" runat="server" Title="Principal" AutoHeight="true" Padding="6">
                                                <Items>
                                                    <ext:Container ID="Container13" runat="server" Layout="Column" Height="45">
                                                        <Items>
                                                            <ext:Container ID="Container14" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".3">
                                                                <Items>
                                                                    <ext:TextField ID="txtPrCEP" DataIndex="prCep" runat="server" FieldLabel="CEP" AnchorHorizontal="30%">
                                                                    </ext:TextField>
                                                                </Items>
                                                            </ext:Container>
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container ID="Container17" runat="server" Layout="Column" Height="45">
                                                        <Items>
                                                            <ext:Container ID="Container15" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".6">
                                                                <Items>
                                                                    <ext:TextField ID="txtPrRua" DataIndex="prRua" runat="server" FieldLabel="Rua" AnchorHorizontal="99%">
                                                                    </ext:TextField>
                                                                </Items>
                                                            </ext:Container>
                                                            <ext:Container ID="Container16" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".4">
                                                                <Items>
                                                                    <ext:TextField ID="txtPrNumero" DataIndex="prNumero" runat="server" FieldLabel="Número"
                                                                        AnchorHorizontal="100%">
                                                                    </ext:TextField>
                                                                </Items>
                                                            </ext:Container>
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container ID="Container18" runat="server" Layout="Column" Height="45">
                                                        <Items>
                                                            <ext:Container ID="Container19" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".6">
                                                                <Items>
                                                                    <ext:TextField ID="txtPrBairro" DataIndex="prBairro" runat="server" FieldLabel="Bairro"
                                                                        AnchorHorizontal="99%" />
                                                                </Items>
                                                            </ext:Container>
                                                            <ext:Container ID="Container20" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".4">
                                                                <Items>
                                                                    <ext:TextField ID="txtPrCidade" DataIndex="prCidade" runat="server" FieldLabel="Cidade"
                                                                        AnchorHorizontal="100%">
                                                                    </ext:TextField>
                                                                </Items>
                                                            </ext:Container>
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container ID="Container21" runat="server" Layout="Column" Height="45">
                                                        <Items>
                                                            <ext:Container ID="Container22" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".6">
                                                                <Items>
                                                                    <ext:TextField ID="txtPrComplemento" DataIndex="prComplemento" runat="server" FieldLabel="Complemento"
                                                                        AnchorHorizontal="99%">
                                                                    </ext:TextField>
                                                                </Items>
                                                            </ext:Container>
                                                            <ext:Container ID="Container23" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".4">
                                                                <Items>
                                                                    <ext:TextField ID="txtPrUF" DataIndex="prUF" runat="server" FieldLabel="UF" AnchorHorizontal="100%">
                                                                    </ext:TextField>
                                                                </Items>
                                                            </ext:Container>
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container ID="Container24" runat="server" Layout="Column" Height="150">
                                                        <Items>
                                                            <ext:Container ID="Container25" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth="1">
                                                                <Items>
                                                                    <ext:FieldSet ID="FieldSet3" runat="server" Title="Contato" Padding="7" Layout="Container"
                                                                        Collapsible="true">
                                                                        <Items>
                                                                            <ext:Container ID="Container26" runat="server" Layout="Column" Height="45">
                                                                                <Items>
                                                                                    <ext:Container ID="Container27" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".5">
                                                                                        <Items>
                                                                                            <ext:TextField ID="txtPrTelefone" DataIndex="prTelefone" runat="server" FieldLabel="Telefone"
                                                                                                AnchorHorizontal="99%">
                                                                                            </ext:TextField>
                                                                                        </Items>
                                                                                    </ext:Container>
                                                                                    <ext:Container ID="Container28" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".5">
                                                                                        <Items>
                                                                                            <ext:TextField ID="txtPrCelular" DataIndex="prCelular" runat="server" FieldLabel="Celular"
                                                                                                AnchorHorizontal="100%">
                                                                                            </ext:TextField>
                                                                                        </Items>
                                                                                    </ext:Container>
                                                                                </Items>
                                                                            </ext:Container>
                                                                            <ext:Container ID="Container29" runat="server" Layout="Form" Height="45">
                                                                                <Items>
                                                                                    <ext:TextField ID="txtPrEmail" DataIndex="prEmail" runat="server" FieldLabel="Email"
                                                                                        AnchorHorizontal="100%">
                                                                                    </ext:TextField>
                                                                                </Items>
                                                                            </ext:Container>
                                                                        </Items>
                                                                    </ext:FieldSet>
                                                                </Items>
                                                            </ext:Container>
                                                        </Items>
                                                    </ext:Container>
                                                </Items>
                                            </ext:Panel>
                                            <ext:Panel ID="Panel4" runat="server" Title="Pagamento" AutoHeight="true" Padding="6">
                                                <Items>
                                                    <ext:Container ID="Container30" runat="server" Layout="Column" Height="45">
                                                        <Items>
                                                            <ext:Container ID="Container31" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".3">
                                                                <Items>
                                                                    <ext:TextField ID="txtPaCEP" DataIndex="paCep" runat="server" FieldLabel="CEP" AnchorHorizontal="30%">
                                                                    </ext:TextField>
                                                                </Items>
                                                            </ext:Container>
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container ID="Container32" runat="server" Layout="Column" Height="45">
                                                        <Items>
                                                            <ext:Container ID="Container33" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".6">
                                                                <Items>
                                                                    <ext:TextField ID="txtPaRua" DataIndex="paRua" runat="server" FieldLabel="Rua" AnchorHorizontal="99%">
                                                                    </ext:TextField>
                                                                </Items>
                                                            </ext:Container>
                                                            <ext:Container ID="Container34" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".4">
                                                                <Items>
                                                                    <ext:TextField ID="txtPaNumero" DataIndex="paNumero" runat="server" FieldLabel="Número"
                                                                        AnchorHorizontal="100%">
                                                                    </ext:TextField>
                                                                </Items>
                                                            </ext:Container>
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container ID="Container35" runat="server" Layout="Column" Height="45">
                                                        <Items>
                                                            <ext:Container ID="Container36" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".6">
                                                                <Items>
                                                                    <ext:TextField ID="txtPaBairro" DataIndex="paBairro" runat="server" FieldLabel="Bairro"
                                                                        AnchorHorizontal="99%" />
                                                                </Items>
                                                            </ext:Container>
                                                            <ext:Container ID="Container37" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".4">
                                                                <Items>
                                                                    <ext:TextField ID="txtPaCidade" DataIndex="paCidade" runat="server" FieldLabel="Cidade"
                                                                        AnchorHorizontal="100%">
                                                                    </ext:TextField>
                                                                </Items>
                                                            </ext:Container>
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container ID="Container38" runat="server" Layout="Column" Height="45">
                                                        <Items>
                                                            <ext:Container ID="Container39" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".6">
                                                                <Items>
                                                                    <ext:TextField ID="txtPaComplemento" DataIndex="paComplemento" runat="server" FieldLabel="Complemento"
                                                                        AnchorHorizontal="99%">
                                                                    </ext:TextField>
                                                                </Items>
                                                            </ext:Container>
                                                            <ext:Container ID="Container40" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".4">
                                                                <Items>
                                                                    <ext:TextField ID="txtPaUF" DataIndex="paUF" runat="server" FieldLabel="UF" AnchorHorizontal="100%">
                                                                    </ext:TextField>
                                                                </Items>
                                                            </ext:Container>
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container ID="Container41" runat="server" Layout="Column" Height="150">
                                                        <Items>
                                                            <ext:Container ID="Container42" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth="1">
                                                                <Items>
                                                                    <ext:FieldSet ID="FieldSet4" runat="server" Title="Contato" Padding="7" Layout="Container"
                                                                        Collapsible="true">
                                                                        <Items>
                                                                            <ext:Container ID="Container43" runat="server" Layout="Column" Height="45">
                                                                                <Items>
                                                                                    <ext:Container ID="Container44" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".5">
                                                                                        <Items>
                                                                                            <ext:TextField ID="txtPaTelefone" DataIndex="paTelefone" runat="server" FieldLabel="Telefone"
                                                                                                AnchorHorizontal="99%">
                                                                                            </ext:TextField>
                                                                                        </Items>
                                                                                    </ext:Container>
                                                                                    <ext:Container ID="Container45" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".5">
                                                                                        <Items>
                                                                                            <ext:TextField ID="txtPaCelular" DataIndex="paCelular" runat="server" FieldLabel="Celular"
                                                                                                AnchorHorizontal="100%">
                                                                                            </ext:TextField>
                                                                                        </Items>
                                                                                    </ext:Container>
                                                                                </Items>
                                                                            </ext:Container>
                                                                            <ext:Container ID="Container46" runat="server" Layout="Form" Height="45">
                                                                                <Items>
                                                                                    <ext:TextField ID="txtPaEmail" DataIndex="paEmail" runat="server" FieldLabel="Email"
                                                                                        AnchorHorizontal="100%">
                                                                                    </ext:TextField>
                                                                                </Items>
                                                                            </ext:Container>
                                                                        </Items>
                                                                    </ext:FieldSet>
                                                                </Items>
                                                            </ext:Container>
                                                        </Items>
                                                    </ext:Container>
                                                </Items>
                                            </ext:Panel>
                                            <ext:Panel ID="Panel5" runat="server" Title="Entrega" AutoHeight="true" Padding="6">
                                                <Items>
                                                    <ext:Container ID="Container47" runat="server" Layout="Column" Height="45">
                                                        <Items>
                                                            <ext:Container ID="Container48" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".3">
                                                                <Items>
                                                                    <ext:TextField ID="txtEnCEP" DataIndex="enCep" runat="server" FieldLabel="CEP" AnchorHorizontal="30%">
                                                                    </ext:TextField>
                                                                </Items>
                                                            </ext:Container>
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container ID="Container49" runat="server" Layout="Column" Height="45">
                                                        <Items>
                                                            <ext:Container ID="Container50" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".6">
                                                                <Items>
                                                                    <ext:TextField ID="txtEnRua" DataIndex="enRua" runat="server" FieldLabel="Rua" AnchorHorizontal="99%">
                                                                        <Listeners>
                                                                            <Change Handler="HabilitaSalvar(false)" />
                                                                        </Listeners>
                                                                    </ext:TextField>
                                                                </Items>
                                                            </ext:Container>
                                                            <ext:Container ID="Container51" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".4">
                                                                <Items>
                                                                    <ext:TextField ID="txtEnNumero" DataIndex="enNumero" runat="server" FieldLabel="Número"
                                                                        AnchorHorizontal="100%">
                                                                    </ext:TextField>
                                                                </Items>
                                                            </ext:Container>
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container ID="Container52" runat="server" Layout="Column" Height="45">
                                                        <Items>
                                                            <ext:Container ID="Container53" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".6">
                                                                <Items>
                                                                    <ext:TextField ID="txtEnBairro" DataIndex="enBairro" runat="server" FieldLabel="Bairro"
                                                                        AnchorHorizontal="99%" />
                                                                </Items>
                                                            </ext:Container>
                                                            <ext:Container ID="Container54" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".4">
                                                                <Items>
                                                                    <ext:TextField ID="txtEnCidade" DataIndex="enCidade" runat="server" FieldLabel="Cidade"
                                                                        AnchorHorizontal="100%">
                                                                    </ext:TextField>
                                                                </Items>
                                                            </ext:Container>
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container ID="Container55" runat="server" Layout="Column" Height="45">
                                                        <Items>
                                                            <ext:Container ID="Container56" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".6">
                                                                <Items>
                                                                    <ext:TextField ID="txtEnComplemento" DataIndex="enComplemento" runat="server" FieldLabel="Complemento"
                                                                        AnchorHorizontal="99%">
                                                                    </ext:TextField>
                                                                </Items>
                                                            </ext:Container>
                                                            <ext:Container ID="Container57" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".4">
                                                                <Items>
                                                                    <ext:TextField ID="txtEnUF" DataIndex="enUF" runat="server" FieldLabel="UF" AnchorHorizontal="100%">
                                                                    </ext:TextField>
                                                                </Items>
                                                            </ext:Container>
                                                        </Items>
                                                    </ext:Container>
                                                    <ext:Container ID="Container58" runat="server" Layout="Column" Height="150">
                                                        <Items>
                                                            <ext:Container ID="Container59" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth="1">
                                                                <Items>
                                                                    <ext:FieldSet ID="FieldSet5" runat="server" Title="Contato" Padding="7" Layout="Container"
                                                                        Collapsible="true">
                                                                        <Items>
                                                                            <ext:Container ID="Container60" runat="server" Layout="Column" Height="45">
                                                                                <Items>
                                                                                    <ext:Container ID="Container61" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".5">
                                                                                        <Items>
                                                                                            <ext:TextField ID="txtEnTelefone" DataIndex="enTelefone" runat="server" FieldLabel="Telefone"
                                                                                                AnchorHorizontal="99%">
                                                                                            </ext:TextField>
                                                                                        </Items>
                                                                                    </ext:Container>
                                                                                    <ext:Container ID="Container62" runat="server" LabelAlign="Top" Layout="Form" ColumnWidth=".5">
                                                                                        <Items>
                                                                                            <ext:TextField ID="txtEnCelular" DataIndex="enCelular" runat="server" FieldLabel="Celular"
                                                                                                AnchorHorizontal="100%">
                                                                                            </ext:TextField>
                                                                                        </Items>
                                                                                    </ext:Container>
                                                                                </Items>
                                                                            </ext:Container>
                                                                            <ext:Container ID="Container63" runat="server" Layout="Form" Height="45">
                                                                                <Items>
                                                                                    <ext:TextField ID="txtEnEmail" DataIndex="enEmail" runat="server" FieldLabel="Email"
                                                                                        AnchorHorizontal="100%">                                                                                        
                                                                                    </ext:TextField>
                                                                                </Items>
                                                                            </ext:Container>
                                                                        </Items>
                                                                    </ext:FieldSet>
                                                                </Items>
                                                            </ext:Container>
                                                        </Items>
                                                    </ext:Container>
                                                </Items>
                                            </ext:Panel>
                                        </Items>
                                    </ext:TabPanel>
                                </Items>
                            </ext:Panel>
                            <ext:Panel ID="Tab3" runat="server" Title="Insumos Fornecidos" Height="347" Width="610" Layout="ColumnLayout"  HideMode="Offsets"
                                Padding="5">
                                <Items>
                                    <ext:MultiSelect ID="MultiSelect1" Legend="Insumos" AnchorHorizontal="50%" ColumnWidth=".45" runat="server" StoreID="storeInsumos" DisplayField="nome" ValueField="codigo"
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
                                    <ext:MultiSelect ID="MultiSelect2" Legend="Insumos do Fornecedor" AnchorHorizontal="50%" ColumnWidth=".45" AnchorVertical="100%" runat="server" DisplayField="nome" ValueField="codigo"
                                    DropGroup="grupo1" StoreID="storeInsumosFornecedor">
                                        <TopBar>
                                             <ext:Toolbar runat="server" ID="ToolBar1" Layout="FitLayout">
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
                            <ext:Panel ID="TabDadosRelacionamento" runat="server"  AutoDestroy="false" Title="Dados Relacionamento" Height="347" Layout="ColumnLayout"  HideMode="Offsets" Hidden="true"
                                Padding="5">
                                <Content>
                                </Content>
                            </ext:Panel>
                        </Items>
                    </ext:TabPanel>
                </Items>
            </ext:FormPanel>
        </Items>
        <TopBar>
            <ext:Toolbar runat="server" Flat="true">
                <Items>
                    <ext:Button ID="btnAdd" runat="server" Text="" Icon="Add">
                        <DirectEvents>
                            <Click OnEvent="AddClick" />
                        </DirectEvents>
                        <Listeners>
                            <Click Handler="#{FormPanel1}.body.mask('Carregando...', 'x-mask-loading');" />
                        </Listeners>
                    </ext:Button>
                    
                    <ext:Button ID="Button3" runat="server" Text="" Icon="Cross" >
                        <DirectEvents>
                            <Click OnEvent="ExcluirClick" />
                        </DirectEvents>
                        <Listeners>
                            <Click Handler="#{FormPanel1}.body.mask('Carregando...', 'x-mask-loading');" />
                        </Listeners>
                    </ext:Button>

                    <ext:ToolbarSeparator />
                    
                    <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="1" Flat="true" StoreID="storeGeral"
                        DisplayInfo="false">
                    </ext:PagingToolbar>
                    
                    <ext:ToolbarSeparator />
                    
                    <ext:ToolbarFill />
                    
                    <ext:Button ID="btOpcoesAvancadas" runat="server" Text="Opções Avançadas" Icon="ApplicationCascade">
                        <Menu>
                            <ext:Menu ID="Menu1" runat="server" DefaultType="Button" ShowSeparator="false">
                                <Items>
                                    <ext:MenuItem ID="MenuItem1" runat="server" Text="Dados de Relacionamento" >
                                        <Listeners>
                                            <Click Handler="TabPanel1.addTab(TabDadosRelacionamento)" />
                                        </Listeners>
                                    </ext:MenuItem>
                                </Items>
                            </ext:Menu>
                        </Menu>
                    </ext:Button>
                </Items>
            </ext:Toolbar>
        </TopBar>
        <Buttons>
            <ext:Button ID="btOK" runat="server" Text="OK" Icon="Accept" Width="80px">
                <DirectEvents>
                    <Click OnEvent="OKForm" >
                        <EventMask ShowMask="true" />
                        <ExtraParams>
                            <ext:Parameter Name="Insumos" Value="Ext.encode(#{storeInsumosFornecedor}.getRecordsValues())" Mode="Raw" />
                        </ExtraParams>
                    </Click>
                </DirectEvents>
            </ext:Button>
            
            <ext:Button ID="btnSalvar" runat="server" Text="Salvar" Icon="Disk" Width="80px">
                <DirectEvents>
                    <Click OnEvent="SalvarDados" >
                        <EventMask ShowMask="true" />
                        <ExtraParams>
                            <ext:Parameter Name="Insumos" Value="Ext.encode(#{storeInsumosFornecedor}.getRecordsValues())" Mode="Raw" />
                        </ExtraParams>
                    </Click>
                </DirectEvents>
            </ext:Button>

            <ext:Button ID="btCancelar" runat="server" Text="Cancelar" Icon="Cancel" Width="80px">
                <Listeners>
                    <Click Handler="#{WindowForm}.hide();" />
                </Listeners>
            </ext:Button>
        </Buttons>
        <Listeners>
            <Hide Handler="GridPrincipal.store.reload();" />
        </Listeners>
    </ext:Window>
    </form>
</body>
</html>
