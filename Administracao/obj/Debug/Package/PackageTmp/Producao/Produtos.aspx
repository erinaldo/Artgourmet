<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Produtos.aspx.cs" Inherits="Artebit.Restaurante.Administracao.Producao.Produtos" %>

<html>
<head id="Head1" runat="server">
    <title>..:: Produção::..</title>
    
    <meta http-equiv="cache-control" content="no-cache"/>
    <meta http-equiv="pragma" content="no-cache"/>
    <META HTTP-EQUIV="EXPIRES" CONTENT="0"> 
    
    <link href="../Css/Producao/AddProduto.css" rel="stylesheet" type="text/css" />
    

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
             comboFiltroPrincipal.selectByIndex(2);
         }

         function filtraGridPrincipal() {
             //executa filtro da gridPrincipal
             var filtro = txtFiltroPrincipal.getValue();
             var campo = comboFiltroPrincipal.getSelectedItem().value;

             GridPrincipal.getStore().filter(campo, filtro,true,false);
         }

         function limpaFiltroPrincipal() {
             txtFiltroPrincipal.reset();
             GridPrincipal.getStore().clearFilter();
         }

         function calculaCustos() {
             //Executa cálculo
             
             var custoPrd = 0;
             
             //pegar custo do produto
             var custo = txtCustoMedio.getValue();
             
             //se custo for custo da ficha técnica , calcular custo total do produto
             if(custo == "") {
                 for (var i = 0; i < StoreRecursos.getCount(); i++) {
                     custoPrd = custoPrd + parseFloat(StoreRecursos.getAt(i).data.valorTotal);
                 }

                 custoPrd = custoPrd / txtRendimento.getValue();
             }
             else {
                 custoPrd = custo;
             }

             var p1 = 0;
             var p2 = 0;
             var p3 = 0;
             
             //pegar precos
             for (var i = 0; i < StorePrecos.getCount(); i++) {
                 if(StorePrecos.getAt(0).data.ativo == "Ativo") {
                     p1 = parseFloat(StorePrecos.getAt(i).data.preco1);
                     p2 = parseFloat(StorePrecos.getAt(i).data.preco2);
                     p3 = parseFloat(StorePrecos.getAt(i).data.preco3);
                 }
             }
             
             //calcular cmv
             // CMV = (C / P) * 100; C = Custo, P = Preco
             var cmv1 = 0;
             var cmv2 = 0;
             var cmv3 = 0;

             if (p1 > 0)
                 cmv1 = (custoPrd / p1) * 100;
             
             if(p2 > 0)
                 cmv2 = (custoPrd / p2) * 100;
             
             if(p3 > 0)
                 cmv3 = (custoPrd / p3) * 100;
             
             //passa valores para os labels
             LabelVFProduto.setText("R$ " + custoPrd.toFixed(2)); //Custo Produto
             
             LabelVL1.setText("R$ " + p1.toFixed(2)); //Preço 1
             LabelML1.setText(cmv1.toFixed(2) + "%"); //CMV 1

             if (cmv1 > 25)
                 LabelML1.addClass('labelAlerta');
             else
                 LabelML1.removeClass('labelAlerta');

             LabelVL2.setText("R$ " + p2.toFixed(2)); //Preço 2
             LabelML2.setText(cmv2.toFixed(2) + "%"); //CMV 2

             if (cmv2 > 25)
                 LabelML2.addClass('labelAlerta');
             else
                 LabelML2.removeClass('labelAlerta');
             
             LabelVL3.setText("R$ " + p3.toFixed(2)); //Preço 3
             LabelML3.setText(cmv3.toFixed(2) + "%"); //CMV 3

             if (cmv3 > 25)
                 LabelML3.addClass('labelAlerta');
             else
                 LabelML3.removeClass('labelAlerta');


             hd_custoAtual.setValue(custoPrd);
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

    <style type="text/css">
        .panelLeft 
        {
            width: 410px;
            float: left;
        }
    
       .panelRight 
       {
            float: right;
            font-size: 13px;
            margin-right: 82px;
            margin-top: 26px;
        }
                
        .result
        {
            text-align: right !important;
        }
            
       .item-wrap
        {
        }
       
        /* Tabela */
        
        .tab td
        {
            font-size: 10px;
            padding: 0;
            text-align: left;
        }
            
        #ext-gen136
        {
            
        }
        
        .labelAlerta
        {
            margin: 0 0 0 50px; 
            color:red;
        }
        
        #Panel6 .x-form-label-left
        {
            margin-bottom: -4px!important;
        }
        
        .radio{
            margin:0 0 0 17.4px;
        }
        
        .radio1{
            margin:0 0 0 17.4px;
        }
        
        @media screen and (-webkit-min-device-pixel-ratio:0) {
            .radio 
                {
                margin: 7px 0 0 2px;
                }
            .radio1 
                {
                margin: -2px 0 0 18px;
                }
        }
        
        .ident{float:left; border:none;margin-right:50px;}
        .divcod{float:left;}
    </style>
   
    <script type="text/javascript">
        var tipo = "Novo";


        var keyUpProductHandler = function (field, e) {

            var store = msFornecedor1.store;

            store.filter('nome', field.getValue(), true, false);
        }

        var keyUpProductHandler2 = function (field, e) {

            var store = msFornecedor2.store;

            store.filter('nome', field.getValue(), true, false);
        }

        var clearFilter = function (filtroTxt, MultiSelect) {
            filtroTxt.reset();
            MultiSelect.store.clearFilter();

            //filtroMulti1.reset();
            //MultiSelect1.store.clearFilter();

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

</head>
<body>
        <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />        

        <%-- Store da grid de lista de produtos Inicial --%>
        <ext:Store ID="StorePrincipal" runat="server">
            <Reader>
                <ext:JsonReader IDProperty="idProduto">
                    <Fields>
                        <ext:RecordField Name="idProduto" Type="Int" />
                        <ext:RecordField Name="codigo" Type="String" />
                        <ext:RecordField Name="nome" Type="String" />
                        <ext:RecordField Name="tipo" Type="String" />
                        <ext:RecordField Name="nomeEmpresa" Type="String" />
                        <ext:RecordField Name="idEmpresa" Type="Int" />
                        <ext:RecordField Name="undCompra" Type="String" />
                        <ext:RecordField Name="undVenda" Type="String" />
                        <ext:RecordField Name="undControle" Type="String" />
                        <ext:RecordField Name="diasvalidade" Type="Int" />
                        <ext:RecordField Name="estoqueMinimo" Type="Int" />
                        <ext:RecordField Name="estoqueMaximo" Type="Int" />
                        <ext:RecordField Name="ultimaCompra" Type="Date" />
                        <ext:RecordField Name="ativoDesc" Type="String" />
                        <ext:RecordField Name="custoMedio" Type="String" />
                        <ext:RecordField Name="custoAtual" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

        <%-- Store da grid de lista de produtos Inicial --%>
        <ext:Store ID="StoreFormulario" runat="server" RemotePaging="true" AutoLoad="true" OnRefreshData="StoreFormulario_RefreshData">
            <Reader>
                <ext:JsonReader IDProperty="idProduto">
                    <Fields>
                        <ext:RecordField Name="idProduto" Type="Int" />
                        <ext:RecordField Name="codigo" Type="String" />
                        <ext:RecordField Name="nome" Type="String" />
                        <ext:RecordField Name="nomeResumo" Type="String" />
                        <ext:RecordField Name="tipoItem" Type="Int" />
                        <ext:RecordField Name="undCompra" Type="String" />
                        <ext:RecordField Name="undControle" Type="String" />
                        <ext:RecordField Name="undVenda" Type="String" />
                        <ext:RecordField Name="tipoTributacao" Type="String" />
                        <ext:RecordField Name="afetaEstoque" Type="Boolean" />
                        <ext:RecordField Name="aliquota" Type="String" />
                        <ext:RecordField Name="ativo" Type="Boolean" />
                        <ext:RecordField Name="codigoEAN1" Type="String" />
                        <ext:RecordField Name="codigoEAN2" Type="String" />
                        <ext:RecordField Name="CST" Type="String" />
                        <ext:RecordField Name="NCM" Type="String" />
                        <ext:RecordField Name="diasValidade" Type="Int" />
                        <ext:RecordField Name="estocavel" Type="Boolean" />
                        <ext:RecordField Name="estoqueMaximo" Type="String" />
                        <ext:RecordField Name="estoqueMinimo" Type="String" />
                        <ext:RecordField Name="grupo" Type="String" />
                        <ext:RecordField Name="idImagem" Type="String" />
                        <ext:RecordField Name="idImagemHD" Type="String" />
                        <ext:RecordField Name="modoPreparo" Type="String" />
                        <ext:RecordField Name="tempoPreparo" Type="Int" />
                        <ext:RecordField Name="pesavel" Type="Boolean" />
                        <ext:RecordField Name="pontoDePedido" Type="String" />
                        <ext:RecordField Name="custoMedio" Type="String" />
                        <ext:RecordField Name="dataUltimaCompra" Type="Date" />
                        <ext:RecordField Name="hdtipo" Type="String" />
                        <ext:RecordField Name="rendimento" Type="String"/>
                        <ext:RecordField Name="codigoPDV" Type="String"/>
                        <ext:RecordField Name="corPDV" Type="String"/>
                        <ext:RecordField Name="ordemPDV" Type="Int"/>
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <Listeners>
                <DataChanged Handler=" if(JanelaPrincipal.isVisible())
                {
                                        
                                        var record = this.getAt(0) || {};
                                        #{FormPrincipal}.getForm().loadRecord(record); 
                                        if(this.getAt(0) != null)
                                        {
                                            JanelaPrincipal.setTitle('Produto: '+ record.get('codigo') + ' - ' +record.get('nome') );
                                        }

                                        
                                        
                                        StoreRecursos.reload();
                                        StorePrecos.reload();
                                        StoreFornecedores.reload();
                                        StoreObservacoes.reload();
                                        StoreListaAdicionais.reload();
                                        StoreInfoEstoque.reload();
                                        StorePlanos.reload();
                                       
                                        if(StoreGrupo.getCount() == 0)
                                        {
                                            StoreGrupo.reload();
                                        }
                                        if(StoreUndMedida.getCount() == 0)
                                        {
                                            StoreUndMedida.reload();
                                        }
                                        if(StoreAliquota.getCount() == 0)
                                        {
                                            StoreAliquota.reload();
                                        }
                                        if(StoreFilial.getCount() == 0)
                                        {
                                            StoreFilial.reload();
                                        }

                                        txtRendimento.setValue(record.get('rendimento'));
                                        ComboUndControle.setInitValue(record.get('undControle'));
                                        ComboUndControle2.setInitValue(record.get('undControle'));
                                        ComboBoxGrupo.setInitValue(record.get('grupo'));
                                        ComboBoxAliquota.setInitValue(record.get('aliquota'));
                                        ComboUndCompra.setInitValue(record.get('undCompra'));
                                        ComboUndVenda.setInitValue(record.get('undVenda'));
                                        
                                        if(record.get('corPDV') != '')
                                        {
                                            cpCoresPDV.select(record.get('corPDV'));
                                        }
                                        else
                                        {
                                            cpCoresPDV.select('FFFFFF');
                                        }

                                        #{FormPrincipal}.body.unmask();
                               }
                                        
                                        "
                    Delay="15" />
               <BeforeLoad Handler="if(document.getElementById('StoreListaAdicionais') != null) { StoreListaAdicionais.commitChanges(); }
                                    if(document.getElementById('FormPrincipal') != null) {#{FormPrincipal}.body.mask('Carregando...', 'x-mask-loading');}
                                    " />
               <LoadException Handler="if(FormPrincipal != null) {#{FormPrincipal}.body.unmask();}" />
               <Add Handler="PagingToolbar1.changePage(StoreFormulario.getTotalCount());" />

            </Listeners>
            <SortInfo Field="idProduto" Direction="ASC" />
        </ext:Store>

        <%--Store da combobox Grupo --%>
        <ext:Store ID="StoreGrupo" runat="server"  AutoLoad="false" OnRefreshData="StoreGrupo_RefreshData">
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="id" Type="Int" />
                        <ext:RecordField Name="nome" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <SortInfo Field="nome" Direction="ASC" />
        </ext:Store>

        <%--Store da textbox Unidade de Medida--%>
        <ext:Store ID="StoreUndMedida" runat="server" GroupField="codUnd"  AutoLoad="false" OnRefreshData="StoreUndMedida_RefreshData">
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="codUnd" Type="String" />
                        <ext:RecordField Name="descricao" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <ext:SortInfo Field="nome" Direction="ASC" />
        </ext:Store>

        <%--Store da combobox Aliquota --%>
        <ext:Store ID="StoreAliquota" runat="server"  AutoLoad="false" OnRefreshData="StoreAliquota_RefreshData">
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="id" Type="Int" />
                        <ext:RecordField Name="nome" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

        <%-- Store da grid de lista de fichaTecnica --%>
        <ext:Store ID="StoreRecursos" runat="server" GroupField="nome"  AutoLoad="false" OnRefreshData="StoreRecursos_RefreshData">
            <Reader>
                <ext:JsonReader IDProperty="idProduto">
                    <Fields>
                        <ext:RecordField Name="idEmpresa" Type="Int" />
                        <ext:RecordField Name="idProduto" Type="Int" />
                        <ext:RecordField Name="codigo" Type="String" />
                        <ext:RecordField Name="nome" Type="String" />
                        <ext:RecordField Name="quantidade" Type="String" />
                        <ext:RecordField Name="unidade" Type="String" />
                        <ext:RecordField Name="valorUnitario" Type="String" />
                        <ext:RecordField Name="valorTotal" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <Listeners>
                <CommitDone Handler="calculaCustos();"></CommitDone>
            </Listeners>
        </ext:Store>

        <%--Store da grid lista de precos--%>
        <ext:Store ID="StorePrecos" runat="server"  AutoLoad="false" OnRefreshData="StorePrecos_RefreshData">
            <Reader>
                <ext:JsonReader IDProperty="idPreco">
                    <Fields>
                        <ext:RecordField Name="idPreco" Type="Int" />
                        <ext:RecordField Name="idProduto" Type="Int" />
                        <ext:RecordField Name="filial" Type="String" />
                        <ext:RecordField Name="descricao" Type="String" />
                        <ext:RecordField Name="preco1" Type="String" />
                        <ext:RecordField Name="preco2" Type="String" />
                        <ext:RecordField Name="preco3" Type="String" />
                        <ext:RecordField Name="ativo" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <Listeners>
                <Load Handler="calculaCustos();"></Load>
                <CommitDone Handler="calculaCustos();"></CommitDone>
            </Listeners>
        </ext:Store>

        <%-- Store da grid de lista específica de produtos(os que serão inseridos e podem ser filtrados) --%>
        <ext:Store ID="StoreListaEspecifica" runat="server" GroupField="nome">
            <Reader>
                <ext:JsonReader IDProperty="idProduto">
                    <Fields>
                        <ext:RecordField Name="idEmpresa" Type="Int" />
                        <ext:RecordField Name="idProduto" Type="Int" />
                        <ext:RecordField Name="codigo" Type="String" />
                        <ext:RecordField Name="nome" Type="String" />
                        <ext:RecordField Name="nomecodigo" Type="String" />
                        <ext:RecordField Name="tipo" Type="String" />
                        <ext:RecordField Name="diasvalidade" Type="Int" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

        <%-- Store da grid de lista de produtos adicionados --%>
        <ext:Store ID="StoreListaAdicionais" runat="server" GroupField="nome"  AutoLoad="false" OnRefreshData="StoreListaAdicionais_RefreshData">
            <Reader>
                <ext:JsonReader IDProperty="idProduto">
                    <Fields>
                        <ext:RecordField Name="idEmpresa" Type="Int" />
                        <ext:RecordField Name="idProduto" Type="Int" />
                        <ext:RecordField Name="codigo" Type="String" />
                        <ext:RecordField Name="nome" Type="String" />
                        <ext:RecordField Name="tipo" Type="String" />
                        <ext:RecordField Name="diasvalidade" Type="Int" />
                        <ext:RecordField Name="nuPreco" Type="Int" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

        <%--Store da grid lista de observações--%>
        <ext:Store ID="StoreObservacoes" runat="server"  AutoLoad="false" OnRefreshData="StoreObservacoes_RefreshData">
            <Reader>
                <ext:JsonReader IDProperty="id">
                    <Fields>
                        <ext:RecordField Name="id" Type="Int" />
                        <ext:RecordField Name="descricao" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

        <%-- Store da grid de lista de Informãções do estoque --%>
        <ext:Store ID="StoreInfoEstoque" runat="server" GroupField="filial"  AutoLoad="false" OnRefreshData="StoreInfoEstoque_RefreshData">
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="filial" Type="String" />
                        <ext:RecordField Name="local" Type="String" />
                        <ext:RecordField Name="qtdeAtual" Type="Float" />
                        <ext:RecordField Name="custoMedio" Type="Float" />
                        <ext:RecordField Name="valorTotal" Type="Float" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

        <%-- Store da grid de lista de Fornecedores --%>
        <ext:Store ID="StoreFornecedores" runat="server"  AutoLoad="false" OnRefreshData="StoreFornecedores_RefreshData">
            <Reader>
                <ext:JsonReader IDProperty="codigo">
                    <Fields>
                        <ext:RecordField Name="codigo" Type="Int" />
                        <ext:RecordField Name="nome" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

        <%-- Store da grid de lista de Fornecedores Especifica --%>
        <ext:Store ID="StoreFornecedoresEspecifica" runat="server"  AutoLoad="false">
            <Reader>
                <ext:JsonReader IDProperty="codigo">
                    <Fields>
                        <ext:RecordField Name="codigo" Type="Int" />
                        <ext:RecordField Name="nome" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

        <%--Store da combobox Filial--%>
        <ext:Store ID="StoreFilial" runat="server"  AutoLoad="false" OnRefreshData="StoreFilial_RefreshData">
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="idFilial" Type="Int" />
                        <ext:RecordField Name="nomeFilial" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

        <%--Store da combobox Preço --%>
        <ext:Store ID="StoreComboPreco" runat="server"  AutoLoad="false" >
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="idPreco" Type="Int" />
                        <ext:RecordField Name="descricaoPreco" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

        <%--Store da combobox Unidade da Produto da ficha técnica --%>
        <ext:Store ID="StoreUndFT" runat="server"  AutoLoad="false" OnRefreshData="StoreUndFT_RefreshData">
            <Reader>
                <ext:JsonReader IDProperty="codUnd">
                    <Fields>
                        <ext:RecordField Name="codUnd" Type="String" />
                        <ext:RecordField Name="descricao" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

        <%-- Store da grid de LISTA DE Planos --%>
        <ext:Store ID="StorePlanos" runat="server" AutoLoad="false" OnRefreshData="StorePlanos_RefreshData">
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

        <%--------------------------------------------------------Painel dos protudos-------------------------------------------------------%>
        <ext:GridPanel 
            ID="GridPrincipal" 
            runat="server" 
            Height="515"
            AutoWidth="true"    
            StoreID="StorePrincipal"
            StripeRows="true"
            Frame="true"
            Collapsible="false"
            AnimCollapse="false"
            TrackMouseOver="true"
            AnchorHorizontal="100%" EnableHdMenu="true" Selectable="true"
            AutoExpandColumn="nome">
            <ColumnModel runat="server">
                <Columns>
                    <%--ocultos--%>
                    <ext:Column Hidden="true" Header="ID Produto" DataIndex="idProduto" />
                    <ext:Column Hidden="true" DataIndex="idEmpresa" />

                    <%--Mostrar Campos--%>
                    <ext:Column Header="Código" DataIndex="codigo" Width="80" />
                    <ext:Column Header="Nome" DataIndex="nome" />
                    <ext:Column Header="Tipo" DataIndex="tipo" Width="90" />
                    <ext:Column Header="Und Compra" DataIndex="undCompra" />
                    <ext:Column Header="Und Venda" DataIndex="undVenda" />
                    <ext:Column Header="Und Controle" DataIndex="undControle"/>
                    <ext:Column Header="Validade(dias)" DataIndex="diasvalidade" Width="80"/>
                    <ext:Column Header="Estoque Mínimo" DataIndex="estoqueMinimo" Width="90"/>
                    <ext:Column Header="Estoque Máximo" DataIndex="estoqueMaximo" Width="90"/>
                    <ext:Column Header="Status" DataIndex="ativoDesc" Width="45" />                  

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
                                        <ext:Parameter Name="codigo" Value="GridPrincipal.getSelectionModel().getSelected().data['idProduto']" Mode="Raw" />
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
                                        <ext:Parameter Name="codigo" Value="GridPrincipal.getSelectionModel().getSelected().data['idProduto']" Mode="Raw" />
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
                        <ext:StringFilter DataIndex="ativo" />
                        <ext:StringFilter DataIndex="tipo" />
                    </Filters>
                </ext:GridFilters>
            </Plugins>
            <DirectEvents>
                <Command OnEvent="GridAcao">                                       
                    <ExtraParams>
                        <ext:Parameter Name="codigo" Value="record.data.idProduto" Mode="Raw" />
                        <ext:Parameter Name="command" Value="command" Mode="Raw"/>
                    </ExtraParams>
                </Command> 
                <RowDblClick OnEvent="GridAcao">
                    <ExtraParams>
                        <ext:Parameter Name="codigo" Value="this.store.getAt(rowIndex).data['idProduto']" Mode="Raw" />
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

        <%--------------------------------------------------------Janela de adicionar/editar produtos-------------------------------------%>
        <ext:Window ID="JanelaPrincipal"
        runat="server" 
        Height="500" 
        Icon="BookAdd"
        Title="Adicionar Produto" 
        Hidden="true"
        Modal="true"
        Width="800">
            <Items>
                <ext:FormPanel ID="FormPrincipal" runat="server" Border="false" Layout="Form" AutoWidth="true">
                <Items>
                <ext:TabPanel EnableTabScroll="true" ID="TabPanelForm" runat="server" Height="470" DeferredRender="false"  >
                    <Items>

                        <%--Painel de Identificação--%>
                        <ext:Panel ID="PanelIdent" runat="server" HideMode="Offsets" Title="Identificação">
                            <Items>
                                <ext:Portal ID="Portal1" runat="server" Layout="column" Height="360" Border="false">
                                    <Items>
                                        <ext:PortalColumn ID="PortalColumn1" runat="server" ColumnWidth="0.6" DefaultAnchor="100%"
                                            Layout="anchor" StyleSpec="padding:10px 10px 0 10px">
                                            <Items>
                                                <ext:Portlet ID="Portlet1" runat="server" Padding="5" Title="Dados" Icon="DateGo">
                                                    <Items>
                                                        <%-- ID da Espera --%>
                                                        <ext:Hidden runat="server" ID="produtoId" DataIndex="idProduto" >
                                                            <Listeners>
                                                                <Change Handler="#{txtIdentificador}.setText(this.getValue());" />
                                                            </Listeners>
                                                        </ext:Hidden>

                                                        <%--ID da Empresa--%>
                                                        <ext:Hidden runat="server" ID="empresaId" />
                
                                                        <%-- Tipo de ação a ser executada, 1=adicionar, 2=atualizar --%>
                                                        <ext:Hidden runat="server" ID="hd_tipo" DataIndex="hdtipo" />

                                                        <ext:Container ID="Container1" runat="server" Layout="Column" Height="65">
                                                            <Items>
                                                                <ext:Panel ID="Panel2" runat="server" Width="90" Border="false" ColumnWidth=".2" Header="false" Layout="Form" LabelAlign="Top">
                                                                    <Items>
                                                                        <ext:Label ID="txtIdentificador" FieldLabel="Identificador" LabelAlign="Top" Text="87" runat="server" Cls="ident" />
                                                                    </Items>
                                                                </ext:Panel>
                                                                <ext:Panel ID="Panel3" runat="server" Border="false" Header="false" Width="180" ColumnWidth=".4" Layout="Form" LabelAlign="Top">
                                                                    <Items>
                                                                        <ext:TextField ID="txtCodigo" MaxLength="9" runat="server" Width="160"  FieldLabel="Código" DataIndex="codigo" 
                                                                                LabelAlign="Top"  AllowBlank="true"/>
                                                                    </Items>
                                                                </ext:Panel>
                                                                <ext:Panel ID="Panel6" runat="server" Border="false" Header="false" ColumnWidth=".4" Layout="FitLayout" Width="100">
                                                                    <Items>
                                                                        <ext:RadioGroup ID="rdTipoItem" runat="server" ColumnsNumber="1" AnchorHorizontal="100%" FieldLabel="Tipo" LabelAlign="Left" LabelPad="0" LabelWidth="50" DataIndex="tipoItem">
                                                                            <Items>
                                                                                <ext:Radio ID="Radio5" Name="TipoProduto" runat="server" BoxLabel="Produto" InputValue="1" />
                                                                                <ext:Radio ID="Radio6" Name="TipoProduto" runat="server" BoxLabel="Insumo" InputValue="2" />
                                                                                <ext:Radio ID="Radio7" Name="TipoProduto" runat="server" BoxLabel="Sub-Insumo" InputValue="3" />
                                                                            </Items>
                                                                        </ext:RadioGroup>
                                                                    </Items>
                                                                </ext:Panel>
                                                            </Items>
                                                        </ext:Container>

                                                        

                                                        <ext:TextField ID="txtNome" LabelAlign="Top" runat="server" Width="433" EmptyText="Digite o nome do produto" FieldLabel="Nome" AllowBlank="true" DataIndex="nome" />
                                                        
                                                         <ext:Container ID="CompositeField7" runat="server" Layout="ColumnLayout">
                                                            <Items>
                                                                <ext:Panel ID="Panel4" runat="server" Width="277" Border="false" Height="43" Header="false" ColumnWidth=".65" Layout="Form" LabelAlign="Top">
                                                                    <Items>
                                                                        <ext:ComboBox 
                                                                        ID="ComboBoxGrupo" 
                                                                        runat="server" 
                                                                        Width="268" 
                                                                        StoreID="StoreGrupo" 
                                                                        FieldLabel="Grupo" 
                                                                        EmptyText="Selecione"
                                                                        ValueField="id"
                                                                        DisplayField="nome"
                                                                        DataIndex="grupo"
                                                                        Editable="True"
                                                                        TypeAhead="false"
                                                                        ForceSelection="False"
                                                                        HideTrigger="true"
                                                                        EnableKeyEvents="true">
                                                                            <Listeners>
                                                                                <AfterRender Handler="this.mun(this.el, 'keyup', this.onKeyUp, this);
                                                                                                      this.mon(this.el, 'keyup', onKeyUp, this);" />
                                                                            </Listeners>
                                                                        <Triggers>
                                                                            <ext:FieldTrigger Icon="Clear" Qtip="Remove selected" />
                                                                        </Triggers>
                                                                        <Listeners>
                                                                            <TriggerClick Handler="this.clearValue();" />
                                                                            <Change Handler="StoreObservacoes.reload();" />
                                                                        </Listeners>
                                                                        </ext:ComboBox>
                                                                    </Items>
                                                                </ext:Panel>

                                                                <ext:Panel ID="Panel5" runat="server" Border="false" Width="151" Header="false" Height="43" ColumnWidth=".3">
                                                                    <Items>
                                                                        <ext:ComboBox 
                                                                        ID="ComboUndControle" 
                                                                        runat="server" 
                                                                        Width="150" 
                                                                        StoreID="StoreUndMedida" 
                                                                        LabelAlign="Top"
                                                                        AllowBlank="true"
                                                                        FieldLabel="Unidade Controle" 
                                                                        EmptyText="Selecione"
                                                                        ValueField="codUnd"
                                                                        DataIndex="undControle"
                                                                        DisplayField="descricao">
                                                                        <Listeners>
                                                                            <Select Handler="if(ComboUndControle2.getValue() != this.getValue()) {ComboUndControle2.setValue(this.getValue());}"></Select>
                                                                        </Listeners>
                                                                        </ext:ComboBox>
                                                                    </Items>
                                                                </ext:Panel>
                                                            </Items>
                                                        </ext:Container>

                                                        <ext:NumberField ID="txtValidade" runat="server" Width="150" FieldLabel="Validade(dias)" MaxLength="9" DataIndex="diasValidade" Margins="20px 0 0 0"  />

                                                        
                                                        <ext:Container ID="CompositeField3" runat="server" Height="45">
                                                            <Items>
                                                                <ext:RadioGroup ID="rdTributacao" runat="server" AnchorHorizontal="100%" FieldLabel="Tributação" LabelAlign="Left" LabelPad="0" LabelWidth="50" DataIndex="tipoTributacao" LabelStyle="color:red;">
                                                                    <Items>
                                                                         <ext:Radio ID="Radio2" Name="Tributacao" runat="server" BoxLabel="Tributado" InputValue="T">
                                                                            <Listeners>
                                                                                <Check Handler="if(checked){#{ComboBoxAliquota}.enable();}" />
                                                                            </Listeners>
                                                                         </ext:Radio>
                                                                         <ext:Radio ID="Radio3" Name="Tributacao" runat="server" BoxLabel="Isento" InputValue="I">
                                                                             <Listeners>
                                                                                <Check Handler="if(checked){#{ComboBoxAliquota}.disable();}" />
                                                                            </Listeners>
                                                                         </ext:Radio>
                                                                          <ext:Radio ID="Radio1" Name="Tributacao" runat="server" BoxLabel="Sub-Tributado" InputValue="S">
                                                                            <Listeners>
                                                                                <Check Handler="if(checked){#{ComboBoxAliquota}.disable();}" />
                                                                            </Listeners>
                                                                         </ext:Radio>
                                                                         <ext:Radio ID="Radio4" Name="Tributacao" runat="server" BoxLabel="Não Tributado" InputValue="N">
                                                                             <Listeners>
                                                                                <Check Handler="if(checked){#{ComboBoxAliquota}.disable();}" />
                                                                            </Listeners>
                                                                         </ext:Radio>
                                                                     </Items>
                                                                 </ext:RadioGroup>
                                                            </Items>
                                                        </ext:Container>
                                                        
                                                        <ext:ComboBox 
                                                        ID="ComboBoxAliquota" 
                                                        runat="server" 
                                                        Width="350" 
                                                        StoreID="StoreAliquota" 
                                                        FieldLabel="Alíquota" 
                                                        EmptyText="Selecione"
                                                        ValueField="id"
                                                        DataIndex="aliquota"
                                                        DisplayField="nome">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" Qtip="Remove selected" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <TriggerClick Handler="this.clearValue();" />
                                                        </Listeners>
                                                        </ext:ComboBox>
                                
                                                    </Items>
                                                </ext:Portlet>

                                                
                                            </Items>
                                        </ext:PortalColumn>
                                        <ext:PortalColumn ID="PortalColumn2" runat="server" ColumnWidth="0.4" DefaultAnchor="100%"
                                            Layout="anchor" StyleSpec="padding:10px 10px 10px 10px;">
                                            <Items>
                                                <ext:Portlet ID="Portlet4" runat="server" Padding="5" Title="Miniatura" Icon="Image">
                                                    <Items>
                                                        <ext:Hidden ID="Hidden2" runat="server" DataIndex="idImagem">
                                                              <Listeners>
                                                                      <Change Handler="#{imgFotoProduto}.setImageUrl('../Sistema/CarregaImagem.aspx?id='+this.getValue());" />
                                                              </Listeners>
                                                        </ext:Hidden>

                                                        <ext:Image ID="imgFotoProduto" StyleSpec="padding: 0 0 0 50px" runat="server" Width="220" Height="120" Margins="5" ImageUrl="">
                                                        </ext:Image>

                                                        <ext:FileUploadField ID="imgProduto" Width="270" runat="server" Icon="Attach" ButtonText="Procurar">
                                                        </ext:FileUploadField>
                                                        
                                                    </Items>
                                                </ext:Portlet>
                                                <ext:Portlet ID="Portlet3" runat="server" Padding="5" Width="100" Title="Informações" Icon="Information">
                                                    <Content>
                                                        <table cellspacing="0" align="center" class="tab" style="font-size: 12px; margin: 0 0 0 20px;">
                                                            <tr>
                                                                <td>Custo</td>
                                                                <td class="result">
                                                                    <ext:Label ID="LabelVFProduto" StyleSpec="margin: 0 0 0 50px;" Width="500" runat="server"/>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Preço 1</td>
                                                                <td class="result"><ext:Label ID="LabelVL1" Width="200" StyleSpec="margin: 0 0 0 50px;" runat="server" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td>CMV 1</td>
                                                                <td class="result"><ext:Label ID="LabelML1" Width="200" StyleSpec="margin: 0 0 0 50px;" runat="server" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td>Preço 2</td>
                                                                <td class="result"><ext:Label ID="LabelVL2" Width="200" StyleSpec="margin: 0 0 0 50px;" runat="server" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td>CMV 2</td>
                                                                <td class="result"><ext:Label ID="LabelML2" Width="200" StyleSpec="margin: 0 0 0 50px;" runat="server" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td>Preço 3</td>
                                                                <td class="result"><ext:Label ID="LabelVL3" Width="200" StyleSpec="margin: 0 0 0 50px;" runat="server"/></td>
                                                            </tr>
                                                            <tr>
                                                                <td>CMV 3</td>
                                                                <td class="result"><ext:Label ID="LabelML3" Width="200" StyleSpec="margin: 0 0 0 50px;" runat="server" /></td>
                                                            </tr>
                                                       </table> 
                                                    </Content>
                                                    </ext:Portlet>
                                            </Items>
                                        </ext:PortalColumn>
                                       
                                    </Items>
                                </ext:Portal>

                            </Items>
                        </ext:Panel>


                        <%--Painel de Ficha Técnica--%>
                        <ext:Panel ID="PanelFicha" HideMode="Offsets" runat="server" Title="Ficha Técnica" Cls="AddProduto" Height="510" >
                            <Items>
                             
                            <ext:GridPanel ID="GridFichaTecnica" Width="760" runat="server" Height="370" StoreID="StoreRecursos" AutoExpandColumn="nome">
                                    <ColumnModel>
                                        <Columns>
                                            <ext:Column Header="Código" DataIndex="codigo" Width="60px"/>
                                            <ext:Column Header="Nome" DataIndex="nome"/>
                                            <ext:Column Header="Qtd" DataIndex="quantidade" Width="70px"/>
                                            <ext:Column Header="Unidade" DataIndex="unidade" Width="60px"/>
                                            <ext:Column Header="Valor Unitário" DataIndex="valorUnitario" >
                                                <Renderer Fn="Ext.util.Format.numberRenderer('0000.0000')"></Renderer>
                                            </ext:Column>
                                            <ext:Column Header="Valor Total" DataIndex="valorTotal" >
                                                <Renderer Fn="Ext.util.Format.numberRenderer('0000.0000')"></Renderer>
                                            </ext:Column>
                                            <ext:ImageCommandColumn Width="60px">
                                                <Commands>
                                                <%--Botões--%>
                                                    <ext:ImageCommand CommandName="editar" Icon="TableEdit" Text="Editar">
                                                        <ToolTip Text="editar" />
                                                    </ext:ImageCommand>
                                                </Commands>
                                            </ext:ImageCommandColumn>
                                        </Columns>
                                    </ColumnModel>
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar4" runat="server">
                                            <Items>
                                                <ext:Button ID="btnNovoItemFT" runat="server" Text="Inserir Produto" Icon="Add">
                                                    <DirectEvents>
                                                        <Click OnEvent="btnNovoItemFT_Click" />
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Button ID="btnRemoveItemFT" runat="server" Text="Remover Produto" Icon="ControlRemoveBlue">
                                                    <DirectEvents>
                                                        <Click OnEvent="btnRemoveItemFT_Click">
                                                             <EventMask ShowMask="true" />
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:ToolbarFill runat="server"/>
                                                <ext:NumberField ID="txtRendimento" FieldLabel="Rendimento" Width="200" runat="server" MaxLength="9" DataIndex="rendimento" />                                                    
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                    <DirectEvents>
                                        <Command OnEvent="EditarProdFichaTecn">                                       
                                            <ExtraParams>
                                                <ext:Parameter Name="idproduto" Value="record.data.idProduto" Mode="Raw" />
                                                <ext:Parameter Name="codUnd" Value="record.data.unidade" Mode="Raw" />
                                                <ext:Parameter Name="quantidade" Value="record.data.quantidade" Mode="Raw" />
                                                <ext:Parameter Name="valorUnit" Value="record.data.valorUnitario" Mode="Raw" />
                                                <ext:Parameter Name="valorTotal" Value="record.data.valorTotal" Mode="Raw" />
                                            </ExtraParams>
                                        </Command> 
                                        <RowDblClick OnEvent="EditarProdFichaTecn">
                                            <ExtraParams>
                                                <ext:Parameter Name="idproduto" Value="this.store.getAt(rowIndex).data['idProduto']" Mode="Raw" />
                                                <ext:Parameter Name="codUnd" Value="this.store.getAt(rowIndex).data['unidade']" Mode="Raw" />
                                                <ext:Parameter Name="quantidade" Value="this.store.getAt(rowIndex).data['quantidade']" Mode="Raw" />
                                                <ext:Parameter Name="valorUnit" Value="this.store.getAt(rowIndex).data['valorUnitario']" Mode="Raw" />
                                                <ext:Parameter Name="valorTotal" Value="this.store.getAt(rowIndex).data['valorTotal']" Mode="Raw" />
                                            </ExtraParams>
                                        </RowDblClick>
                                    </DirectEvents>
                                    <SelectionModel>
                                        <ext:CheckboxSelectionModel ID="CheckboxSelectionModel3" runat="server" RowSpan="2" />
                                    </SelectionModel>
                                    <Plugins>
                                        <ext:GridFilters runat="server" ID="GridFilters2" Local="true">
                                            <Filters>
                                                <ext:StringFilter DataIndex="codigo" />
                                                <ext:StringFilter DataIndex="nome" />
                                                <ext:StringFilter DataIndex="tipo" />
                                            </Filters>
                                        </ext:GridFilters>
                                    </Plugins>
                                </ext:GridPanel>        
                             </Items>
                        </ext:Panel>


                        <%--Painel de Receita--%>
                        <ext:Panel ID="Panel1"  runat="server" HideMode="Offsets" Title="Receita">
                            <Items>
                                <ext:Portal ID="Portal3" runat="server" Layout="column" Height="380" Border="false">
                                    <Items>
                                        <ext:PortalColumn ID="PortalColumn5" runat="server" ColumnWidth="0.5" DefaultAnchor="100%"
                                                          Layout="anchor" StyleSpec="padding:10px 10px 0px 10px">
                                            <Items>
                                                <ext:Portlet ID="Portlet6" Height="370" runat="server" Padding="5" Title="Receita" Icon="DateGo">
                                                    <Items>
                                                        <ext:NumberField ID="txtTempoPreparo" FieldLabel="Tempo de Finalização (mim)" 
                                                                         DataIndex="tempoPreparo" LabelWidth="150" 
                                                                         MaxLength="3" Width="200" runat="server"/>
                                                        <ext:TextArea LabelAlign="Top" ID="txtModoPreparo" FieldLabel="Modo de Preparo" 
                                                                      Width="356" Height="330" runat="server" DataIndex="modoPreparo" />
                                                    </Items>
                                                </ext:Portlet>
                                            </Items>
                                        </ext:PortalColumn>
                                        <ext:PortalColumn ID="PortalColumn7" runat="server" ColumnWidth="0.5" DefaultAnchor="100%"
                                                          Layout="anchor" StyleSpec="padding:10px">
                                            <Items>
                                                <ext:Portlet ID="Portlet9" runat="server" Padding="5" Title="Imagem">
                                                    <Items>
                                                        <ext:Hidden ID="Hidden3" runat="server" DataIndex="idImagemHD">
                                                            <Listeners>
                                                                <Change Handler="#{imgFotoProdutoHD}.setImageUrl('../Sistema/CarregaImagem.aspx?id='+this.getValue());" />
                                                            </Listeners>
                                                        </ext:Hidden>

                                                        <ext:Image ID="imgFotoProdutoHD" StyleSpec="padding: 0 0 0 35px" Height= "309" Width="330" runat="server" Margins="5">
                                                        </ext:Image>

                                                        <ext:FileUploadField ID="imgProdutoHD" Width="350" runat="server" Icon="Attach" ButtonText="Procurar">
                                                        </ext:FileUploadField>
                                                    </Items>
                                                </ext:Portlet>
                                            </Items>
                                        </ext:PortalColumn>
                                    </Items>
                                </ext:Portal>

                            </Items>
                        </ext:Panel>
                        

                        <%--Painel de Detalhes--%>
                        <ext:Panel ID="PanelDetalhes" HideMode="Offsets" runat="server" Title="Detalhes" Cls="AddProduto">
                            <Items>
                                <ext:Portal ID="Portal5" runat="server" Height="360" Border="false" Width="350"  >
                                    <Items>
                                        <ext:PortalColumn ID="PortalColumn8" runat="server" Width="350"  >
                                            <Items>
                                                <ext:Portlet ID="Portlet7" runat="server" Padding="5" Title="Informações PDV" Icon="Information">
                                                    <Items>
                                                        <ext:NumberField ID="txtOrdem" LabelAlign="Top" runat="server" Width="50" FieldLabel="Ordem Cardápio" MaxLength="4" DataIndex="ordemPDV" MaxLengthText="4"/>
                                                        <ext:TextField ID="txtResumo" LabelAlign="Top" runat="server" Width="220" FieldLabel="Nome Cardápio" MaxLength="15" DataIndex="nomeResumo" MaxLengthText="15"/>
                                                        <ext:TextField ID="txtCodigoMobile" LabelAlign="Top" runat="server" Width="100" FieldLabel="Código Mobile" MaxLength="4" DataIndex="codigoPDV" MaxLengthText="4"/>
                                                        <ext:ColorPalette ID="cpCoresPDV" FieldLabel="Cor Item PDV" runat="server">
                                                            <Content>
                                                                <div id="divCores" style="width:30px; height:100%; top:0px; left: 150px; background-color: red; position: relative;">
                                                                </div>
                                                            </Content>
                                                            <Listeners>
                                                                <Select Handler="divCores.style.backgroundColor = this.value;"></Select>
                                                            </Listeners>
                                                        </ext:ColorPalette>
                                                    </Items>
                                                </ext:Portlet>
                                            </Items>
                                        </ext:PortalColumn>
                                    </Items>
                                </ext:Portal>
                            </Items>
                        </ext:Panel>
                        

                        <%--Painel de Preços--%>
                        <ext:Panel ID="PanelPreco" HideMode="Offsets" runat="server" Title="Preços" Cls="AddProduto">
                            <Items>
                                <ext:GridPanel 
                                ID="GridPrecos" 
                                runat="server" 
                                Height="370" 
                                Width="760"
                                StoreID="StorePrecos"
                                AutoExpandColumn="descricao"
                                >
                                    <ColumnModel>
                                        <Columns>
                                            <ext:Column Header="Filial" DataIndex="filial" />
                                            <ext:Column Header="Descrição" DataIndex="descricao" />
                                            <ext:Column Header="Preço 1" DataIndex="preco1">
                                                <Renderer Fn="Ext.util.Format.numberRenderer('R$ 0.00')" />
                                            </ext:Column>
                                            <ext:Column Header="Preço 2" DataIndex="preco2">
                                                 <Renderer Fn="Ext.util.Format.numberRenderer('R$ 0.00')" />
                                            </ext:Column>
                                            <ext:Column Header="Preço 3" DataIndex="preco3">
                                                 <Renderer Fn="Ext.util.Format.numberRenderer('R$ 0.00')" />
                                            </ext:Column>
                                             <ext:Column Header="Status" DataIndex="ativo" />
                                            <ext:ImageCommandColumn Width="120">
                                                <Commands>
                                                    <ext:ImageCommand CommandName="preco" Icon="TableEdit" Text="Editar">
                                                       <ToolTip Text="Preco" />
                                                    </ext:ImageCommand>
                                                </Commands>
                                            </ext:ImageCommandColumn>
                                        </Columns>
                                    </ColumnModel>
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar3" runat="server">
                                            <Items>
                                                <ext:Button ID="bntPrecoProduto" runat="server" Text="Inserir Preço" Icon="Add">
                                                    <DirectEvents>
                                                        <Click OnEvent="InserirPrecoProduto" />
                                                    </DirectEvents>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                    <DirectEvents>
                                        <Command OnEvent="EditarPreco">                                       
                                            <ExtraParams>
                                                <ext:Parameter Name="idPreco" Value="record.data.idPreco" Mode="Raw" />
                                                <ext:Parameter Name="descricao" Value="record.data.descricao" Mode="Raw" />
                                                <ext:Parameter Name="preco1" Value="record.data.preco1" Mode="Raw" />
                                                <ext:Parameter Name="preco2" Value="record.data.preco2" Mode="Raw" />
                                                <ext:Parameter Name="preco3" Value="record.data.preco3" Mode="Raw" />
                                                <ext:Parameter Name="filial" Value="record.data.filial" Mode="Raw" />
                                                <ext:Parameter Name="ativo" Value="record.data.ativo" Mode="Raw" />
                                            </ExtraParams>
                                        </Command> 
                                        <RowDblClick OnEvent="EditarPreco">
                                            <ExtraParams>
                                                <ext:Parameter Name="idPreco" Value="this.store.getAt(rowIndex).data['idPreco']" Mode="Raw" />
                                                <ext:Parameter Name="descricao" Value="this.store.getAt(rowIndex).data['descricao']" Mode="Raw" />
                                                <ext:Parameter Name="preco1" Value="this.store.getAt(rowIndex).data['preco1']" Mode="Raw" />
                                                <ext:Parameter Name="preco2" Value="this.store.getAt(rowIndex).data['preco2']" Mode="Raw" />
                                                <ext:Parameter Name="preco3" Value="this.store.getAt(rowIndex).data['preco3']" Mode="Raw" />
                                                <ext:Parameter Name="filial" Value="this.store.getAt(rowIndex).data['filial']" Mode="Raw" />
                                                <ext:Parameter Name="ativo" Value="this.store.getAt(rowIndex).data['ativo']" Mode="Raw" />
                                            </ExtraParams>
                                        </RowDblClick>
                                    </DirectEvents>
                                    <SelectionModel>
                                        <ext:CheckboxSelectionModel runat="server"></ext:CheckboxSelectionModel>
                                    </SelectionModel>

                                </ext:GridPanel>
                            </Items>    
                        </ext:Panel>


                        <%--Painel de Fornecedores--%>
                        <ext:Panel ID="PanelFornecedores" HideMode="Offsets" runat="server" Title="Fornecedores" Cls="AddProduto" Layout="ColumnLayout">
                            <Items>
                                <ext:MultiSelect ID="msFornecedor1" Legend="Fornecedores" AnchorHorizontal="50%" ColumnWidth=".45" runat="server" StoreID="StoreFornecedores" DisplayField="nome" ValueField="codigo"
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
                                                            <TriggerClick Handler=" if(index == 0) { clearFilter(filtroMulti1,msFornecedor1); } else { Ext.Msg.alert('','buscar'); }" />
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
                                                    <Click Handler="transferir(msFornecedor1,msFornecedor2,false);" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button ID="btMove2" runat="server" Icon="ForwardGreen" Flat="true" AnchorHorizontal="100%" >
                                                <Listeners>
                                                    <Click Handler="transferir(msFornecedor1,msFornecedor2,true);" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button ID="btMove3" runat="server" Icon="ReverseBlue" Flat="true" AnchorHorizontal="100%">
                                                <Listeners>
                                                    <Click Handler="transferir(msFornecedor2,msFornecedor1,false);" />
                                                </Listeners>
                                            </ext:Button>

                                            <ext:Button ID="btMove4" runat="server" Icon="RewindBlue" Flat="true" AnchorHorizontal="100%">
                                                <Listeners>
                                                    <Click Handler="transferir(msFornecedor2,msFornecedor1,true);" />
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Panel>
                                    <ext:MultiSelect ID="msFornecedor2" Legend="Fornecedores do Produto" AnchorHorizontal="50%" ColumnWidth=".45" AnchorVertical="100%" runat="server" DisplayField="nome" ValueField="codigo"
                                    DropGroup="grupo1" StoreID="StoreFornecedoresEspecifica">
                                        <TopBar>
                                             <ext:Toolbar runat="server" ID="ToolBar7" Layout="FitLayout">
                                                <Items>
                                                    <ext:TriggerField runat="server" ID="filtroMulti2" AnchorHorizontal="100%" EnableKeyEvents="true">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" />
                                                            <ext:FieldTrigger Icon="Search" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <KeyUp Fn="keyUpProductHandler2" />
                                                            <TriggerClick Handler=" if(index == 0) { clearFilter(filtroMulti2,msFornecedor2); } else { Ext.Msg.alert('','buscar'); }" />
                                                        </Listeners>
                                                    </ext:TriggerField>
                                                </Items>
                                            </ext:Toolbar>
                                        </TopBar>
                                    </ext:MultiSelect>

                            </Items>
                        </ext:Panel>


                        <%--Painel de Observação--%>
                        <ext:Panel ID="PanelObs" HideMode="Offsets" runat="server" Title="Observações" Cls="AddProduto">
                            <Items>
                                <ext:GridPanel ID="GridObservacoes" runat="server" Height="370" Width="760" 
                                StoreID="StoreObservacoes" AutoExpandColumn="descricao" AutoRender="false">
                                    <ColumnModel>
                                        <Columns>
                                            <ext:Column Header="Descrição" Width="760" DataIndex="descricao" />
                                        </Columns>
                                    </ColumnModel>
                                    <SelectionModel>
                                        <ext:CheckboxSelectionModel ID="check" runat="server" RowSpan="2" />
                                    </SelectionModel>
                                </ext:GridPanel>
                            </Items>
                        </ext:Panel>


                        <%--Painel de Adicionais--%>
                        <ext:Panel ID="PanelAdicionais" HideMode="Offsets" runat="server" Title="Adicionais" Cls="AddProduto">
                            <Items>
                             <ext:GridPanel ID="gridAdicionais" Width="760" runat="server" Height="370" StoreID="StoreListaAdicionais" AutoExpandColumn="nome">
                                    <ColumnModel>
                                        <Columns>
                                            <ext:Column Header="Código" DataIndex="codigo"/>
                                            <ext:Column Header="Nome" DataIndex="nome"/>
                                            <ext:Column Header="Tipo" DataIndex="tipo"/>
                                            <ext:Column Header="Preço" DataIndex="nuPreco" Editable="True">
                                                <Editor>
                                                     <ext:ComboBox ID="ComboPrecos" runat="server" Editable="false">
                                                         <Items>
                                                                <ext:ListItem Text="Preço 1" Value="1" />
                                                                <ext:ListItem Text="Preço 2" Value="2" />
                                                                <ext:ListItem Text="Preço 3" Value="3" />
                                                         </Items>
                                                    </ext:ComboBox>
                                                </Editor>
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar2" runat="server">
                                            <Items>
                                                <ext:Button ID="btnNovoAdicional" runat="server" Text="Inserir Produto" Icon="Add">
                                                   <Listeners>
                                                        <Click Handler="JanelaProdutosAdicionais.show();"></Click>
                                                   </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="btnRemoverAdicional" runat="server" Text="Remover Produto" Icon="ControlRemoveBlue">
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
                                        <ext:GridFilters runat="server" ID="GridFilters1" Local="true">
                                            <Filters>
                                                <ext:StringFilter DataIndex="codigo" />
                                                <ext:StringFilter DataIndex="nome" />
                                                <ext:StringFilter DataIndex="tipo" />
                                            </Filters>
                                        </ext:GridFilters>
                                        <ext:EditableGrid ID="EditableGrid2" runat="server" />
                                    </Plugins>
                                </ext:GridPanel>
                            </Items>
                        </ext:Panel>



                        <%-- ============================================================================ --%>
                        <%-- Opções Avançadas --%>

                        <%--Painel de Código de Barras--%>
                        <ext:Panel ID="PanelCodBarras" HideMode="Offsets" runat="server" Title="Código de Barras" Hidden="true">
                            <Items>
                                <ext:Portal ID="Portal2" runat="server" Layout="column" Border="false" Height="360">
                                    <Items>
                                        <ext:PortalColumn ID="PortalColumn3" runat="server" ColumnWidth="1" DefaultAnchor="100%"
                                            Layout="anchor" StyleSpec="padding:10px 10px 10px 10px">
                                            <Items>
                                                <ext:Portlet ID="Portlet2" runat="server" Padding="5" Title="Código de Barras" Icon="ComputerGo">
                                                    <Items>
                                                        <ext:TextField ID="txtEAN1" runat="server" DataIndex="codigoEAN1" Width="350" FieldLabel="Código EAN1"/>
                                                        <ext:TextField ID="txtEAN2" runat="server" DataIndex="codigoEAN2" Width="350" FieldLabel="Código EAN2"/>
                                                        <ext:TextField ID="txtCST" runat="server" DataIndex="CST" Width="350" FieldLabel="Código CST"/>
                                                        <ext:TextField ID="txtNCM" runat="server" DataIndex="NCM" Width="350" FieldLabel="Código NCM"/>
                                                    </Items>
                                                </ext:Portlet>
                                            </Items>
                                        </ext:PortalColumn>
                                    </Items>
                                </ext:Portal>
                            </Items>
                        </ext:Panel>


                        <%--Painel de Dados Fiscais--%>
                        <ext:Panel ID="PanelDadosFiscais" HideMode="Offsets" runat="server" Title="Dados Fiscais" Cls="AddProduto" Hidden="true">
                            <Items>
                            </Items>
                        </ext:Panel>


                        <%--Painel de Estoque--%>
                        <ext:Panel ID="PanelEstoque" HideMode="Offsets" runat="server" Title="Informações de estoque">
                            <Items>
                                   
                                <ext:Portal ID="Portal4" runat="server" Height="370" Layout="column">
                                    <Items>
                                        <ext:PortalColumn ID="PortalColumn4" runat="server" ColumnWidth="0.4" DefaultAnchor="100%"
                                            Layout="anchor" StyleSpec="padding:10px 10px 10px 10px">
                                            <Items>
                                                <ext:Portlet ID="Portlet5" runat="server" Padding="5" Title="Unidades de Medida" Icon="VcardEdit">
                                                    <Items>
                                                        <ext:ComboBox 
                                                        ID="ComboUndControle2" 
                                                        runat="server" 
                                                        Width="200" 
                                                        StoreID="StoreUndMedida" 
                                                        AllowBlank="true"
                                                        FieldLabel="Unidade Controle" 
                                                        EmptyText="Selecione"
                                                        ValueField="codUnd"
                                                        DisplayField="descricao"
                                                        IsRemoteValidation="true">
                                                        </ext:ComboBox>
                                                                                        
                                                        <ext:ComboBox 
                                                        ID="ComboUndCompra" 
                                                        runat="server" 
                                                        Width="200" 
                                                        StoreID="StoreUndMedida" 
                                                        FieldLabel="Unidade Compra" 
                                                        EmptyText="Selecione"
                                                        AllowBlank="true"
                                                        ValueField="codUnd"
                                                        DataIndex="undCompra"
                                                        DisplayField="descricao"></ext:ComboBox>

                                                        <ext:ComboBox 
                                                        ID="ComboUndVenda" 
                                                        runat="server" 
                                                        Width="200" 
                                                        StoreID="StoreUndMedida" 
                                                        AllowBlank="true"
                                                        FieldLabel="Unidade Venda" 
                                                        EmptyText="Selecione"
                                                        ValueField="codUnd"
                                                        DataIndex="undVenda"
                                                        DisplayField="descricao"></ext:ComboBox>
                                                    </Items>
                                                </ext:Portlet>
                                            </Items>
                                        </ext:PortalColumn>
                                        <ext:PortalColumn ID="PortalColumn6" runat="server" ColumnWidth="0.6" DefaultAnchor="100%"
                                            Layout="anchor" StyleSpec="padding:10px 10px 10px 10px">
                                            <Items>
                                                <ext:Portlet ID="Portlet8" runat="server" Padding="5" Title="Estoque" Icon="GroupEdit">
                                                    <Items>
                                                       <ext:Container ID="CompositeField9" runat="server" Layout="ColumnLayout">
                                                            <Items>
                                                                <ext:Panel ID="Panel8" Width="250" runat="server" Border="false" ColumnWidth=".5" Header="false">
                                                                    <Items>
                                                                        <ext:NumberField ID="txtEstoqueMinimo" FieldLabel="Estoque Mínimo" Width="200" runat="server" MaxLength="9" DataIndex="estoqueMinimo"/>
                                                                        <ext:NumberField ID="txtEstoqueMaximo" FieldLabel="Estoque Máximo" Width="200" runat="server" MaxLength="9" DataIndex="estoqueMaximo"/>
                                                                        <ext:NumberField ID="txtPontoPedido" FieldLabel="Ponto de Pedido" Width="200" runat="server" MaxLength="9" DataIndex="pontoDePedido"/>
                                                                        <ext:NumberField ID="txtCustoMedio" FieldLabel="Custo Médio" Width="200" runat="server" MaxLength="9" DataIndex="custoMedio" />
                                                                        <ext:Hidden runat="server" ID="hd_custoAtual" DataIndex="custoAtual"></ext:Hidden>
                                                                        </Items>
                                                                </ext:Panel>
                                                                <ext:Panel ID="Panel7" runat="server" Border="false" ColumnWidth=".5" Header="false">
                                                                    <Items>
                                                                        <ext:Checkbox ID="CheckAfetaEstoque" FieldLabel="Afeta Estoque" runat="server" DataIndex="afetaEstoque"/>
                                                                        <ext:Checkbox ID="CheckEstocavel" FieldLabel="Estocável" runat="server" DataIndex="estocavel"/>
                                                                        <ext:Checkbox ID="CheckPesavel" FieldLabel="Pesável" runat="server" DataIndex="pesavel"/>
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


                        <%--Painel de Saldos e Custos--%>
                        <ext:Panel ID="PanelSaldosCustos" HideMode="Offsets" runat="server" Title="Saldos e Custos" Cls="AddProduto" Hidden="true">
                            <Items>

                            </Items>
                        </ext:Panel>


                        <%--Painel de Movimentação do Produto--%>
                        <ext:Panel ID="PanelMovimentacao" HideMode="Offsets" runat="server" Title="Movimentação do Produto" Cls="AddProduto" Hidden="true">
                            <Items>
                                <ext:GridPanel ID="grid" Width="760" runat="server" Height="370" StoreID="StoreInfoEstoque">
                                    <ColumnModel>
                                        <Columns>
                                            <ext:Column Header="Filial" DataIndex="filial" Width="150" />
                                            <ext:Column Header="Local" DataIndex="local" Width="150" />
                                            <ext:Column Header="QTDE Atual" DataIndex="qtdeAtual" Width="100" />
                                            <ext:Column Header="Custo Médio" DataIndex="custoMedio" Width="150">
                                                 <Renderer Format="UsMoney" />
                                            </ext:Column>
                                            <ext:Column Header="Valor Total" DataIndex="valorTotal" Width="150">
                                                 <Renderer Format="UsMoney" />
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>
                                </ext:GridPanel>
                            </Items>
                        </ext:Panel>

                         <%--Painel de Fidelidade--%>
                        <ext:Panel ID="PanelFidelidade" HideMode="Offsets" runat="server" Title="Planos de Fidelidade" 
                           Cls="AddProduto" Hidden="true">
                            <Items>
                                <ext:GridPanel 
                                    ID="GridPanelFidelidade" 
                                    runat="server" 
                                    Border="false" 
                                    Height="380" 
                                    StoreID="StorePlanos"
                                    >
                                    <Plugins>
                                        <ext:EditableGrid ID="EditableGrid1" runat="server" />
                                    </Plugins>
                                    <ColumnModel>
                                        <Columns>
                                            <ext:Column Header="ID" DataIndex="codigo" Width="30" />
                                            <ext:Column Header="Nome" DataIndex="nome" Width="140" />
                                            <ext:Column 
                                                Header="Pontos por Real" 
                                                DataIndex="valor" 
                                                Width="120" 
                                                Sortable="true">
                                                <Editor>
                                                    <ext:NumberField ID="TextField1" runat="server" AllowBlank="true" />
                                                </Editor>
                                            </ext:Column>
                                        </Columns>
                                  </ColumnModel>
                                </ext:GridPanel>
                            </Items>
                        </ext:Panel>

                        <%-- ============================================================================ --%>
                    </Items>
                </ext:TabPanel>
                </Items>
                </ext:FormPanel>
            </Items>
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
                                        <ext:Parameter Name="codigo" Value="0" Mode="Value" />
                                        <ext:Parameter Name="command" Value="novo" Mode="Value" />
                                    </ExtraParams>
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    
                        <ext:Button ID="Button12" runat="server" Text="" Icon="Cross" >
                            <Listeners>
                                <Click Handler="#{FormPrincipal}.body.mask('Excluindo...', 'x-mask-loading');" />
                            </Listeners>
                        </ext:Button>

                        <ext:ToolbarSeparator />
                    
                        <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="1" Flat="true" StoreID="StoreFormulario"
                            DisplayInfo="false">
                        </ext:PagingToolbar>
                    
                        <ext:ToolbarSeparator />
                    
                        <ext:ToolbarFill />
                    
                        <ext:Button ID="btOpcoesAvancadas" runat="server" Text="Opções Avançadas" Icon="ApplicationCascade">
                            <Menu>
                                <ext:Menu ID="Menu1" runat="server" DefaultType="Button" ShowSeparator="false">
                                    <Items>
                                        <ext:MenuItem ID="MenuItem1" runat="server" Text="Detalhes" >
                                            <Listeners>
                                                <Click Handler="TabPanelForm.addTab(PanelDetalhes)" />
                                            </Listeners>
                                        </ext:MenuItem>
                                        <ext:MenuItem ID="MenuItem2" runat="server" Text="Código de Barras" >
                                            <Listeners>
                                                <Click Handler="TabPanelForm.addTab(PanelCodBarras)" />
                                            </Listeners>
                                        </ext:MenuItem>
                                        <ext:MenuItem ID="MenuItem3" runat="server" Text="Dados Fiscais" >
                                            <Listeners>
                                                <Click Handler="TabPanelForm.addTab(PanelDadosFiscais)" />
                                            </Listeners>
                                        </ext:MenuItem>
                                        <ext:MenuItem ID="MenuItem4" runat="server" Text="Informações de estoque" >
                                            <Listeners>
                                                <Click Handler="TabPanelForm.addTab(PanelEstoque)" />
                                            </Listeners>
                                        </ext:MenuItem>
                                         <ext:MenuItem ID="MenuItem5" runat="server" Text="Saldos e Custos" >
                                            <Listeners>
                                                <Click Handler="TabPanelForm.addTab(PanelSaldosCustos)" />
                                            </Listeners>
                                        </ext:MenuItem>
                                         <ext:MenuItem ID="MenuItem6" runat="server" Text="Movimentação do Produto" >
                                            <Listeners>
                                                <Click Handler="TabPanelForm.addTab(PanelMovimentacao)" />
                                            </Listeners>
                                        </ext:MenuItem>
                                        <ext:MenuItem ID="MenuItem7" runat="server" Text="Planos de Fidelidade" >
                                            <Listeners>
                                                <Click Handler="TabPanelForm.addTab(PanelFidelidade)" />
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
                <ext:Button ID="btnOkFrm" runat="server" Text="OK" Icon="Accept">
                    <DirectEvents>
                        <Click OnEvent="btnOkFrm_Click">
                            <EventMask ShowMask="true" Msg="Verificando..." MinDelay="500" />
                            <ExtraParams>
                                <ext:Parameter Name="Recursos" Value="Ext.encode(#{StoreRecursos}.getRecordsValues())" Mode="Raw" />
                                <ext:Parameter Name="Fornecedores" Value="Ext.encode(#{StoreFornecedoresEspecifica}.getRecordsValues())" Mode="Raw" />
                                <ext:Parameter Name="Adicionais" Value="Ext.encode(#{StoreListaAdicionais}.getRecordsValues())" Mode="Raw" />
                                <ext:Parameter Name="Precos" Value="Ext.encode(#{StorePrecos}.getRecordsValues())" Mode="Raw" />
                                <ext:Parameter Name="Planos" Value="Ext.encode(#{GridPanelFidelidade}.getRowsValues())" Mode="Raw" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="btnSalvarFrm" runat="server" Text="Salvar" Icon="Disk">
                    <DirectEvents>
                        <Click OnEvent="btnSalvarFrm_Click">
                            <EventMask ShowMask="true" Msg="Verificando..." MinDelay="500" />
                             <ExtraParams>
                                <ext:Parameter Name="Recursos" Value="Ext.encode(#{StoreRecursos}.getRecordsValues())" Mode="Raw" />
                                <ext:Parameter Name="Fornecedores" Value="Ext.encode(#{StoreFornecedoresEspecifica}.getRecordsValues())" Mode="Raw" />
                                <ext:Parameter Name="Adicionais" Value="Ext.encode(#{StoreListaAdicionais}.getRecordsValues())" Mode="Raw" />
                                <ext:Parameter Name="Precos" Value="Ext.encode(#{StorePrecos}.getRecordsValues())" Mode="Raw" />
                                <ext:Parameter Name="Planos" Value="Ext.encode(#{GridPanelFidelidade}.getRowsValues())" Mode="Raw" />
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

        <%------------------------------------------------------Janela inserir produto-----------------------------------------------------%>
        <ext:Window
        ID="JanelaProdutosAdicionais" 
        runat="server" 
        Collapsible="false" 
        Height="400" 
        Icon="BuildingGo"
        Title="Produto" 
        Hidden="true"
        Modal="true"
        Padding="8"
        BodyStyle="background-color: #fff;" 
        Width="600">
            <Items>
            <ext:Hidden ID="tipo_produto" runat="server"></ext:Hidden>

                <ext:GridPanel ID="GridProdutosAdicionais" StoreID="StoreListaEspecifica" runat="server" Height="300" Padding="8">
                    <ColumnModel>
                        <Columns>
                            <ext:Column Header="Código" DataIndex="codigo"/>
                            <ext:Column Header="Nome" DataIndex="nome" Width="200"/>
                            <ext:Column Header="Tipo" DataIndex="tipo"/>
                            <ext:Column Header="Validade(dias)" DataIndex="diasvalidade"/>
                        </Columns>
                    </ColumnModel>
                    <DirectEvents>
                        <Command OnEvent="InserirProdutoAdicional" />
                    </DirectEvents>
                    <SelectionModel>
                        <ext:CheckboxSelectionModel ID="SmProdutosAdicionais" runat="server" RowSpan="2" />
                    </SelectionModel>
                    <Plugins>
                    <ext:GridFilters runat="server" ID="gfGridPrdAdd" Local="true">
                        <Filters>
                            <ext:StringFilter DataIndex="codigo" />
                            <ext:StringFilter DataIndex="nome" />
                            <ext:StringFilter DataIndex="tipo" />
                        </Filters>
                    </ext:GridFilters>
            </Plugins>
                </ext:GridPanel>
            </Items>
            <Buttons>
                <ext:Button ID="btnOkAdicional" runat="server" Text="OK" Icon="Add" Width="80px">
                    <DirectEvents>
                        <Click OnEvent="InserirProdutoAdicional" />
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="btnCancelarAdicional" runat="server" Text="Cancelar" Icon="Cancel" Width="80px">
                    <Listeners>
                        <Click Handler="#{JanelaProdutosAdicionais}.hide();" />
                    </Listeners>
                </ext:Button>
             </Buttons>
             <Listeners>
                <Show Handler="if(StoreFormulario.getCount() == 0) { StoreFormulario.reload(); } SmProdutosAdicionais.clearSelections();" />
             </Listeners>
        </ext:Window>
                
        <%-----------------------------------------------------Janela Preços---------------------------------------------------------------%>
        <ext:Window 
        ID="JanelaPreco" 
        runat="server" 
        Collapsible="false" 
        Height="230" 
        Icon="BuildingGo"
        Title="Inserir Preço" 
        Hidden="true"
        Modal="true"
        Padding="8"
        BodyStyle="background-color: #fff;" 
        Width="340">
            <Items>
                
                 <ext:ComboBox 
                    ID="ComboBoxFilialPreco" 
                    runat="server" 
                    Width="300" 
                    StoreID="StoreFilial" 
                    AllowBlank="true"
                    FieldLabel="Filial" 
                    EmptyText="Selecione"
                    ValueField="idFilial"
                    DisplayField="nomeFilial">
                    </ext:ComboBox>

                <%--Campo Oculto--%>
                <ext:TextField Hidden="true" ID="txtP_IdTabPreco" runat="server"/>
                
                <ext:TextField ID="txtP_Descricao" runat="server" Width="300" FieldLabel="Descrição" AllowBlank="true" />
                <ext:NumberField ID="txtP_Preco1" runat="server" Width="180" FieldLabel="Preço 1" AllowBlank="true" MaxLength="9" />
                <ext:NumberField ID="txtP_Preco2" runat="server" Width="180" FieldLabel="Preço 2" AllowBlank="true" MaxLength="9" />
                <ext:NumberField ID="txtP_Preco3" runat="server" Width="180" FieldLabel="Preço 3" AllowBlank="true" MaxLength="9" />
                <ext:Checkbox ID="chkP_Ativo" runat="server" FieldLabel="Ativo" />

            </Items>
            <Buttons>
                <ext:Button ID="btnOK4" runat="server" Text="OK" Icon="Add" Width="80px">
                    <DirectEvents>
                        <Click OnEvent="SalvarPrecoProduto"/>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Cancel4" runat="server" Text="Cancelar" Icon="Cancel" Width="80px">
                    <Listeners>
                        <Click Handler="#{JanelaPreco}.hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
        
        <%------------------------------------------------------Janela FichaTecnica -------------------------------------------------------%>
        <ext:Window 
            ID="JanelaFichaTecnica"
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
                    StoreID="StoreListaEspecifica" 
                    Editable="true"
                    TypeAhead="false" 
                    ForceSelection="False"
                    HideTrigger="true"
                    EnableKeyEvents="true"
                    ValueField="idProduto"
                    DisplayField="nomecodigo"
                    IsRemoteValidation="true">
                    <Listeners>
                          <AfterRender Handler="this.mun(this.el, 'keyup', this.onKeyUp, this);
                                                                          this.mon(this.el, 'keyup', onKeyUp, this);" />
                    </Listeners>
                    <RemoteValidation  OnValidation="CarregarUnidadesProdutos" ValidationEvent="select" EventOwner="Field" />
                </ext:ComboBox>

                <ext:NumberField ID="txtI_Quantidade" runat="server" Width="180" FieldLabel="Quantidade" AllowBlank="true" IsRemoteValidation="true" MaxLength="9" AllowDecimals="True">
                    <RemoteValidation  OnValidation="CalcularPUnitario" />
                </ext:NumberField>

                <ext:ComboBox 
                    ID="ComboBoxIUnd" 
                    runat="server" 
                    Width="200" 
                    StoreID="StoreUndFT" 
                    AllowBlank="true"
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
                    IsRemoteValidation="true">
                    <RemoteValidation  OnValidation="CalcularPUnitario"  ValidationEvent="select" EventOwner="Field" />
                </ext:ComboBox>

                <ext:Label ID="txtIPrecoUnitario" runat="server" Width="350" FieldLabel="Custo Unitário" />

                <ext:Label ID="txtITotal" runat="server" Width="350" FieldLabel="TOTAL"/>

            </Items>
            <Buttons>
                <ext:Button ID="Button4" runat="server" Text="OK" Icon="Add" Width="80px">
                    <DirectEvents>
                        <Click OnEvent="SalvarItemReceita">
                            <ExtraParams>
                                <ext:Parameter Name="Recursos" Value="Ext.encode(#{StoreRecursos}.getRecordsValues())" Mode="Raw" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button5" runat="server" Text="Cancelar" Icon="Cancel" Width="80px">
                    <Listeners>
                        <Click Handler="#{JanelaFichaTecnica}.hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
            <Listeners>
                <Show Handler="if(StoreFormulario.getCount() == 0) {StoreFormulario.reload();}" />
            </Listeners>
        </ext:Window>
     
    </form>

</body>
</html>
