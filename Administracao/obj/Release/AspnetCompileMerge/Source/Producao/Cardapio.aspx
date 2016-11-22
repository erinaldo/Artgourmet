<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cardapio.aspx.cs" Inherits="Artebit.Restaurante.Administracao.Producao.Cardapio" %>

<html>
<head runat="server">
    <title>..:: Cardápio ::..</title>
    
     <link rel="stylesheet" type="text/css" id="theme" href="" /> 

    <script type="text/javascript">
        function trocaTema() {
            var themeUrl = '../temas/css/xtheme-Beige.css';
            Ext.util.CSS.swapStyleSheet("theme", themeUrl);
        }

        function confirmaExclusao() {
            Ext.Msg.confirm('Alerta', 'Tem certeza que deseja excluir os itens selecionados?', function (btn) {
                //console.log(this, arguments);
                if (btn == 'yes') { Ext.net.DirectMethods.ExcluirVarios(); }
            });
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

            var key = Ext.EventObject.getKey();

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


        var selectionChanged1 = function (dv, nodes) {
            if (nodes.length > 0) {
                var id = nodes[0].id;
                storeCat1.filter("idItemPai", id, false, true, true);
                storeCat1.sort('posicao', 'ASC');

                DataViewGrupo2.clearSelections();
                storeCat2.filter("idItemPai", -1);
                storeCat2.sort('posicao', 'ASC');
            }
        };
        
        var selectionChanged2 = function (dv, nodes) {
            if (nodes.length > 0) {
                var id = nodes[0].id;
                storeCat1.filter("idItemPai", id, false, true, true);
                storeCat1.sort('posicao', 'ASC');

                DataViewGrupo1.clearSelections();
                storeCat2.filter("idItemPai", -1);
                storeCat2.sort('posicao', 'ASC');
            }
        };
        var selectionChanged3 = function (dv, nodes) {
            if (nodes.length > 0) {
                var id = nodes[0].id;
                storeCat2.filter("idItemPai", id, false, true, true);
                storeCat2.sort('posicao', 'ASC');
                //Ext.Msg.alert("Click", "The node with id='" + id + "' has been clicked");
            }
        };

        function novoGrupo1() {

            var idItem = parseInt(hdIdItensCardapio.getValue());

            var record = new storeGrupo1.recordType();
            record.data.idItemCard = idItem;
            record.data.idEmpresa = 0;
            record.data.idFilial = 0;
            record.data.idCardapio = 0;
            record.data.ativo = true;
            record.data.grupo = 1;
            record.data.posicao = calculaPosicao(DataViewGrupo1);
            record.data.idProduto = 0;
            record.data.nuPreco = 1;
            record.data.usaPreco = false;
            record.data.descricao = '';
            record.data.cor = '#0000FF';
            record.data.corFonte = '#000000';
            record.data.idItemPai = 0;

            storeGrupo1.add(record);

            hdIdItensCardapio.setValue(idItem + 1);

            PagingToolbar3.changePage(PagingToolbar3.getPageData().pages);
        }

        function novoGrupo2() {

            var idItem = parseInt(hdIdItensCardapio.getValue());

            var record = new storeGrupo2.recordType();
            record.data.idItemCard = idItem;
            record.data.idEmpresa = 0;
            record.data.idFilial = 0;
            record.data.idCardapio = 0;
            record.data.ativo = true;
            record.data.grupo = 2;
            record.data.posicao = calculaPosicao(DataViewGrupo2);
            record.data.idProduto = 0;
            record.data.nuPreco = 1;
            record.data.usaPreco = false;
            record.data.descricao = '';
            record.data.cor = '#0000FF';
            record.data.corFonte = '#000000';
            record.data.idItemPai = null;

            storeGrupo2.add(record);

            hdIdItensCardapio.setValue(idItem + 1);

            PagingToolbar4.changePage(PagingToolbar4.getPageData().pages);
        }

        function novoCat1() {
            var valida = false;
            var grupo = 1;
            var objeto = null;

            if (DataViewGrupo2.getSelectionCount() == 1) {
                grupo = 2;
                objeto = DataViewGrupo2.getSelectedRecords()[0];
                valida = true;
            }
            else if (DataViewGrupo1.getSelectionCount() == 1) {
                objeto = DataViewGrupo1.getSelectedRecords()[0];
                valida = true;
            }
            else {
                Ext.Msg.alert("Alerta", "Selecione UM item do grupo superior!");
            }

            if (valida) {
                var idItemPai = objeto.data.idItemCard;
                var idItem = parseInt(hdIdItensCardapio.getValue());

                var record = new storeCat1.recordType();
                record.data.idItemCard = idItem;
                record.data.idEmpresa = 0;
                record.data.idFilial = 0;
                record.data.idCardapio = 0;
                record.data.ativo = true;
                record.data.grupo = grupo;
                record.data.posicao = calculaPosicao(DataViewCat1);
                record.data.idProduto = 0;
                record.data.nuPreco = 1;
                record.data.usaPreco = false;
                record.data.descricao = '';
                record.data.cor = '#0000FF';
                record.data.corFonte = '#000000';
                record.data.idItemPai = idItemPai;

                storeCat1.add(record);

                hdIdItensCardapio.setValue(idItem + 1);
                PagingToolbar5.changePage(PagingToolbar5.getPageData().pages);
            }
        }

        function novoCat2() {

            if (DataViewCat1.getSelectionCount() == 1) {

                var idItemPai = DataViewCat1.getSelectedRecords()[0].data.idItemCard;
                var idItem = parseInt(hdIdItensCardapio.getValue());

                var record = new storeCat2.recordType();
                record.data.idEmpresa = 0;
                record.data.idFilial = 0;
                record.data.idCardapio = 0;
                record.data.idItemCard = idItem;
                record.data.ativo = true;
                record.data.grupo = 1;
                record.data.posicao = calculaPosicao(DataViewCat2);
                record.data.idProduto = 0;
                record.data.nuPreco = 1;
                record.data.usaPreco = false;
                record.data.descricao = '';
                record.data.cor = '#0000FF';
                record.data.corFonte = '#000000';
                record.data.idItemPai = idItemPai;

                storeCat2.add(record);

                hdIdItensCardapio.setValue(idItem + 1);
                PagingToolbar6.changePage(PagingToolbar6.getPageData().pages);
            }
            else {
                Ext.Msg.alert("Alerta", "Selecione UM item do grupo superior.");
            }
        }

        var dataviewGeral = null;

        function carregarForm(dataview) {
            dataviewGeral = dataview;

            if (dataviewGeral.getSelectionCount() == 0) {
                Ext.Msg.alert("Alerta", "Selecione pelo menos um item.");
            }
            else {
                var record = dataview.getSelectedRecords()[0];

                FormPanel2.getForm().loadRecord(record);

                cpCorItem.select(record.data.cor);

                cbProdutoItem.fireEvent('Select');

                JanelaItens.show();
            }
        }

        function alteraCor(dataview, cor) {
            if (dataview.getSelectionCount() == 0) {
                Ext.Msg.alert("Alerta", "Selecione pelo menos um item.");
            }
            else {
                var record = dataview.getSelectedIndexes();

                for (var i in record) {
                    if (!isNaN(i)) {
                        dataview.getStore().getAt(record[i]).set('cor', cor);
                    }
                }

            }
        }

        function alteraCorFonte(dataview, cor) {
            if (dataview.getSelectionCount() == 0) {
                Ext.Msg.alert("Alerta", "Selecione pelo menos um item.");
            }
            else {
                var record = dataview.getSelectedIndexes();

                for (var i in record) {
                    if (!isNaN(i)) {
                        dataview.getStore().getAt(record[i]).set('corFonte', cor);
                    }
                }

            }
        }

        function ativarItem(dataview) {
            if (dataview.getSelectionCount() == 0) {
                Ext.Msg.alert("Alerta", "Selecione pelo menos um item.");
            }
            else {
                var record = dataview.getSelectedIndexes();

                for (var i in record) {
                    if (!isNaN(i)) {
                        if (!dataview.getStore().getAt(record[i]).data.ativo) {
                            dataview.getStore().getAt(record[i]).set('cor','#0000FF');
                            dataview.getStore().getAt(record[i]).set('ativo',true);
                        }
                    }
                }
            }
        }

        function inativarItem(dataview) {
            if (dataview.getSelectionCount() == 0) {
                Ext.Msg.alert("Alerta", "Selecione pelo menos um item.");
            }
            else {
                var record = dataview.getSelectedIndexes();

                for (var i in record) {
                    if (!isNaN(i)) {
                        dataview.getStore().getAt(record[i]).set('cor','#C0C0C0');
                        dataview.getStore().getAt(record[i]).set('ativo', false);

                    }
                }
            }
        }

        function calculaPosicao(dataview) {
            return dataview.getStore().allData.getCount() + 1;
        }

        function alinhaEsquerda(dataview) {
            if (dataview.getSelectionCount() == 0) {
                Ext.Msg.alert("Alerta", "Selecione pelo menos um item.");
            }
            else {
                var recordOriginal = dataview.getStore().getAt(dataview.getSelectedIndexes()[0]);
                var recordNova = null;

                var posicaoDestino = 0;

                if ((recordOriginal.data.posicao - 1) <= 0)
                {
                    posicaoDestino = 1;
                }
                else
                {
                    posicaoDestino = recordOriginal.data.posicao -1;
                }


                var linhas = dataview.getStore().allData.getRange();

                if (posicaoDestino > 0) {
                    for (var i in linhas) {
                        if (!isNaN(i)) {
                            if (linhas[i] != null && linhas[i] != undefined) {
                                if (linhas[i].data.posicao == posicaoDestino) {
                                    recordNova = linhas[i];
                                    break;
                                }
                            }
                        }
                    }

                    //colocar as posicoes 
                    if (recordNova != null) {
                        recordNova.set('posicao', recordOriginal.data.posicao);
                    }

                    recordOriginal.set('posicao', posicaoDestino);

                    //reordenar store
                    dataview.getStore().sort('posicao','ASC');
                }
            }
        }

        function alinhaDireita(dataview) {
            if (dataview.getSelectionCount() == 0) {
                Ext.Msg.alert("Alerta", "Selecione pelo menos um item.");
            }
            else {
                var recordOriginal = dataview.getStore().getAt(dataview.getSelectedIndexes()[0]);
                var recordNova = null;

                var posicaoDestino = 0;

                posicaoDestino = recordOriginal.data.posicao + 1;

                var linhas = dataview.getStore().allData.getRange();

                if (posicaoDestino < dataview.getStore().allData.getCount()) {
                    for (var i in linhas) {
                        if (!isNaN(i)) {
                            if (linhas[i] != null && linhas[i] != undefined) {
                                if (linhas[i].data.posicao == posicaoDestino) {
                                    recordNova = linhas[i];
                                    break;
                                }
                            }
                        }
                    }

                    //colocar as posicoes 
                    if (recordNova != null) {
                        recordNova.set('posicao', recordOriginal.data.posicao);
                    }

                    recordOriginal.set('posicao', posicaoDestino);

                    //reordenar store
                    dataview.getStore().sort('posicao');
                }
            }
        }

        function TransformaDados(lista)
        {
            var arr = [];
            var len = lista.length;
            for (var i = 0; i < len; i++) {
                if(lista[i].data.nuPreco == "")
                {
                            
                    lista[i].data.nuPreco = 1;
                }
                     
                arr.push(lista[i].data);
            }

            return arr;
        }
    </script>
    <style type="text/css">
        .images-view .x-panel-body {
            background: white;
            font: 11px Arial, Helvetica, sans-serif;
        }

        .images-view .thumb {
            background: #dddddd;
            padding: 1px;
        }

            .images-view .thumb img {
                height: 60px;
                width: 80px;
            }

        .images-view .thumb-wrap {
            float: left;
            margin: 1px;
            margin-right: 0;
            padding: 1px;
            text-align: center;
            cursor: default;
        }

            .images-view .thumb-wrap span {
                display: block;
                overflow: hidden;
                text-align: center;
            }

        .images-view .x-view-over {
        }

        .images-view .x-view-selected {
            background: #eff5fb no-repeat right bottom;
            border: 1px solid red;
            padding: 0px;
        }

            .images-view .x-view-selected .thumb {
                background: transparent;
            }
    </style>
</head>

<body onload="trocaTema()">
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />

        <%-- Store da grid de lista de cardápio --%>
        <ext:Store ID="storeCardapioGrid" runat="server" RemotePaging="true" AutoLoad="true"
            OnRefreshData="CarregaGridPrincipal" OnLoad="LoadGridPrincipal">
            <Reader>
                <ext:JsonReader IDProperty="codigo">
                    <Fields>
                        <ext:RecordField Name="codigo" Type="Int" />
                        <ext:RecordField Name="nome" Type="String" />
                        <ext:RecordField Name="diaTodo" Type="String" />
                        <ext:RecordField Name="segunda" Type="String" />
                        <ext:RecordField Name="terca" Type="String" />
                        <ext:RecordField Name="quarta" Type="String" />
                        <ext:RecordField Name="quinta" Type="String" />
                        <ext:RecordField Name="sexta" Type="String" />
                        <ext:RecordField Name="sabado" Type="String" />
                        <ext:RecordField Name="domingo" Type="String" />
                        <ext:RecordField Name="status" Type="String" />
                        <ext:RecordField Name="tipo" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <ext:SortInfo runat="server" Field="nome" Direction="ASC" />
        </ext:Store>

        <%-- Store dos itens do cardápio (itens da janela) --%>
        <ext:Store ID="storeCardapio" runat="server" RemotePaging="true" AutoLoad="false"
            OnRefreshData="CarregaCardapio">
            <Reader>
                <ext:JsonReader IDProperty="codigo">
                    <Fields>
                        <ext:RecordField Name="codigo" Type="Int" />
                        <ext:RecordField Name="nome" Type="String" />
                        <ext:RecordField Name="diaTodo" Type="String" />
                        <ext:RecordField Name="segunda" Type="String" />
                        <ext:RecordField Name="terca" Type="String" />
                        <ext:RecordField Name="quarta" Type="String" />
                        <ext:RecordField Name="quinta" Type="String" />
                        <ext:RecordField Name="sexta" Type="String" />
                        <ext:RecordField Name="sabado" Type="String" />
                        <ext:RecordField Name="domingo" Type="String" />
                        <ext:RecordField Name="status" Type="String" />
                        <ext:RecordField Name="tipo" Type="String" />
                        <ext:RecordField Name="hdtipo" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <Listeners>
                <DataChanged Handler="  
                                        var record = this.getAt(0) || {};
                                        #{FormPanel1}.getForm().loadRecord(record); 
                                        if(this.getAt(0) != null)
                                        {
                                            JanelaPrincipal.setTitle('Cardápio: '+ record.get('codigo') + ' - ' +record.get('nome') );
                                        }
                                        Ext.net.DirectMethods.CarregarGrupos(record.get('codigo'));
                                        "
                    Delay="15" />
                <BeforeLoad Handler="#{FormPanel1}.body.mask('Carregando...', 'x-mask-loading');" />
                <Load Handler="#{FormPanel1}.body.unmask();" />
                <LoadException Handler="#{FormPanel1}.body.unmask();" />
            </Listeners>
        </ext:Store>

        <ext:Store ID="storeCat1" runat="server">
            <Reader>
                <ext:JsonReader IDProperty="idItemCard">
                    <Fields>
                        <ext:RecordField Name="idEmpresa" Type="Int" />
                        <ext:RecordField Name="idFilial" Type="Int" />
                        <ext:RecordField Name="idCardapio" Type="Int" />
                        <ext:RecordField Name="idItemCard" Type="Int" />
                        <ext:RecordField Name="ativo" Type="Boolean" />
                        <ext:RecordField Name="grupo" Type="Int" />
                        <ext:RecordField Name="posicao" Type="Int" />
                        <ext:RecordField Name="idProduto" Type="Int" />
                        <ext:RecordField Name="nuPreco" Type="Int" />
                        <ext:RecordField Name="descricao" Type="String" />
                        <ext:RecordField Name="cor" Type="String" />
                        <ext:RecordField Name="corFonte" Type="String" />
                        <ext:RecordField Name="usaPreco" Type="Boolean" />
                        <ext:RecordField Name="idItemPai" Type="Int" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <AutoLoadParams>
                <ext:Parameter Name="start" Value="0" Mode="Raw" />
                <ext:Parameter Name="limit" Value="12" Mode="Raw" />
            </AutoLoadParams>
        </ext:Store>

        <ext:Store ID="storeCat2" runat="server">
            <Reader>
                <ext:JsonReader IDProperty="idItemCard">
                    <Fields>
                        <ext:RecordField Name="idEmpresa" Type="Int" />
                        <ext:RecordField Name="idFilial" Type="Int" />
                        <ext:RecordField Name="idCardapio" Type="Int" />
                        <ext:RecordField Name="idItemCard" Type="Int" />
                        <ext:RecordField Name="ativo" Type="Boolean" />
                        <ext:RecordField Name="grupo" Type="Int" />
                        <ext:RecordField Name="posicao" Type="Int" />
                        <ext:RecordField Name="idProduto" Type="Int" />
                        <ext:RecordField Name="nuPreco" Type="Int" />
                        <ext:RecordField Name="descricao" Type="String" />
                        <ext:RecordField Name="cor" Type="String" />
                        <ext:RecordField Name="corFonte" Type="String" />
                        <ext:RecordField Name="usaPreco" Type="Boolean" />
                        <ext:RecordField Name="idItemPai" Type="Int" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <AutoLoadParams>
                <ext:Parameter Name="start" Value="0" Mode="Raw" />
                <ext:Parameter Name="limit" Value="9" Mode="Raw" />
            </AutoLoadParams>
        </ext:Store>

        <ext:Store ID="storeGrupo1" runat="server">
            <Reader>
                <ext:JsonReader IDProperty="idItemCard">
                    <Fields>
                        <ext:RecordField Name="idEmpresa" Type="Int" />
                        <ext:RecordField Name="idFilial" Type="Int" />
                        <ext:RecordField Name="idCardapio" Type="Int" />
                        <ext:RecordField Name="idItemCard" Type="Int" />
                        <ext:RecordField Name="ativo" Type="Boolean" />
                        <ext:RecordField Name="grupo" Type="Int" />
                        <ext:RecordField Name="posicao" Type="Int" />
                        <ext:RecordField Name="idProduto" Type="Int" />
                        <ext:RecordField Name="nuPreco" Type="Int" />
                        <ext:RecordField Name="descricao" Type="String" />
                        <ext:RecordField Name="cor" Type="String" />
                        <ext:RecordField Name="corFonte" Type="String" />
                        <ext:RecordField Name="usaPreco" Type="Boolean" />
                        <ext:RecordField Name="idItemPai" Type="Int" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <AutoLoadParams>
                <ext:Parameter Name="start" Value="0" Mode="Raw" />
                <ext:Parameter Name="limit" Value="12" Mode="Raw" />
            </AutoLoadParams>
            <Listeners>
                <Load Handler="storeCat1.filter('idItemPai',-1); storeCat1.sort('posicao','ASC'); storeCat2.filter('idItemPai',-1); storeCat2.sort('posicao','ASC');" />
            </Listeners>
        </ext:Store>

        <ext:Store ID="storeGrupo2" runat="server">
            <Reader>
                <ext:JsonReader IDProperty="idItemCard">
                    <Fields>
                        <ext:RecordField Name="idEmpresa" Type="Int" />
                        <ext:RecordField Name="idFilial" Type="Int" />
                        <ext:RecordField Name="idCardapio" Type="Int" />
                        <ext:RecordField Name="idItemCard" Type="Int" />
                        <ext:RecordField Name="ativo" Type="Boolean" />
                        <ext:RecordField Name="grupo" Type="Int" />
                        <ext:RecordField Name="posicao" Type="Int" />
                        <ext:RecordField Name="idProduto" Type="Int" />
                        <ext:RecordField Name="nuPreco" Type="Int" />
                        <ext:RecordField Name="descricao" Type="String" />
                        <ext:RecordField Name="cor" Type="String" />
                        <ext:RecordField Name="corFonte" Type="String" />
                        <ext:RecordField Name="usaPreco" Type="Boolean" />
                        <ext:RecordField Name="idItemPai" Type="Int" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <AutoLoadParams>
                <ext:Parameter Name="start" Value="0" Mode="Raw" />
                <ext:Parameter Name="limit" Value="12" Mode="Raw" />
            </AutoLoadParams>
            <Listeners>
                <Load Handler="storeCat1.filter('idItemPai',-1); storeCat2.filter('idItemPai',-1);" />
            </Listeners>
        </ext:Store>

        <%-- Store da combobox Produtos --%>
        <ext:Store ID="storeComboProdutos" runat="server" AutoLoad="true">
            <Reader>
                <ext:JsonReader IDProperty="codigo">
                    <Fields>
                        <ext:RecordField Name="codigo" Type="Int" />
                        <ext:RecordField Name="nome" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>


        <%------------------------------------------------- Grid Cardápio -------------------------------------------%>
        <ext:GridPanel
            ID="GridPrincipal"
            runat="server"
            Height="515"
            AutoWidth="true"
            StoreID="storeCardapioGrid"
            StripeRows="true"
            Frame="true"
            Collapsible="false"
            AnimCollapse="false"
            TrackMouseOver="true"
            AutoExpandColumn="nome"
            AnchorHorizontal="100%">
            <ColumnModel ID="ColumnModel2" runat="server">
                <Columns>
                    <ext:Column Hidden="true" DataIndex="codigo" />
                    <ext:Column Header="Nome" DataIndex="nome" Width="250" />
                    <ext:Column Header="Dia Todo" DataIndex="diaTodo" Width="80" />
                    <ext:Column Header="Segunda" DataIndex="segunda" Width="80" />
                    <ext:Column Header="Terça" DataIndex="terca" Width="80" />
                    <ext:Column Header="Quarta" DataIndex="quarta" Width="80" />
                    <ext:Column Header="Quinta" DataIndex="quinta" Width="80" />
                    <ext:Column Header="Sexta" DataIndex="sexta" Width="80" />
                    <ext:Column Header="Sabado" DataIndex="sabado" Width="80" />
                    <ext:Column Header="Domingo" DataIndex="domingo" Width="80" />
                    <ext:Column Header="Status" DataIndex="status" Width="80" />
                    <ext:Column Header="Tipo" DataIndex="tipo" Width="80" />
                </Columns>
            </ColumnModel>

            <SelectionModel>
                <ext:CheckboxSelectionModel ID="chkSelect" runat="server" CheckOnly="True" />
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
                        <ext:Button ID="btnEditarG" runat="server" Text="Editar" Icon="CommentEdit">
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
                                <Click Handler="confirmaExclusao();"></Click>
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="btnAtivarDesativarG" runat="server" Text="Ativar/Desativar" Icon="Wand">
                            <DirectEvents>
                                <Click OnEvent="GridAcao">
                                    <ExtraParams>
                                        <ext:Parameter Name="codigo" Value="GridPrincipal.getSelectionModel().getSelected().data['codigo']" Mode="Raw" />
                                        <ext:Parameter Name="command" Value="ativar" Mode="Value" />
                                    </ExtraParams>
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:ToolbarSeparator runat="server" />

                        <ext:ComboBox runat="server" Width="110" ID="comboFiltroPrincipal" AutoScroll="True">
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

                        <ext:ToolbarSeparator runat="server" />

                        <ext:PagingToolbar Flat="True" HideRefresh="true" runat="server" ID="PageGrid" PageSize="20" DisplayMsg="" StoreID="storeCardapioGrid">
                        </ext:PagingToolbar>
                    </Items>
                </ext:Toolbar>
            </TopBar>

            <Plugins>
                <ext:GridFilters runat="server" ID="GridFilters3" Local="true">
                    <Filters>
                        <ext:StringFilter DataIndex="nome" />
                    </Filters>
                </ext:GridFilters>
            </Plugins>

            <DirectEvents>
                <RowDblClick OnEvent="GridAcao">
                    <ExtraParams>
                        <ext:Parameter Name="codigo" Value="this.store.getAt(rowIndex).data['codigo']" Mode="Raw" />
                        <ext:Parameter Name="command" Value="editar" Mode="Value" />
                    </ExtraParams>
                </RowDblClick>
            </DirectEvents>

            <Listeners>
                <CellClick Handler="if(columnIndex != 0) {this.getSelectionModel().selectRow(rowIndex);}" />
                <AfterLayout Handler="carregaComboFiltro();"></AfterLayout>
            </Listeners>
        </ext:GridPanel>



        <%------------------------------------------------- Janela Cardápio -------------------------------------------%>
        <ext:Window
            ID="JanelaPrincipal"
            runat="server"
            Collapsible="false"
            Height="500"
            Icon="BuildingGo"
            Title="Cardápio"
            Resizable="False"
            Hidden="true"
            Modal="true"
            BodyStyle="background-color: #fff;"
            Padding="0"
            Width="730">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Border="false" Layout="Form" AutoWidth="true" Padding="0" Margins="0">
                    <Items>
                        <ext:Menu ID="DataViewContextMenu" runat="server" Width="100">
                            <Items>
                                <ext:MenuItem ID="MenuItem1" runat="server" Text="Pra Esquerda" Icon="ArrowLeft">
                                    <Listeners>
                                        <Click Handler="alinhaEsquerda(dataviewGeral);" />
                                    </Listeners>
                                </ext:MenuItem>
                                <ext:MenuItem ID="MenuItem2" runat="server" Text="Pra Direita" Icon="ArrowRight">
                                    <Listeners>
                                        <Click Handler="alinhaDireita(dataviewGeral);" />
                                    </Listeners>
                                </ext:MenuItem>
                            </Items>
                        </ext:Menu>
                        <ext:Hidden ID="hdIdItensCardapio" runat="server" Text="1" />
                        <ext:TabPanel EnableTabScroll="true" ID="TabPanel1" runat="server" Height="400" DeferredRender="false" Padding="0" Margins="0">
                            <Items>

                                <%--Painel de Identificação--%>
                                <ext:Panel ID="PanelIdent" runat="server" HideMode="Offsets" Title="Identificação">
                                    <Items>
                                        <ext:Portal ID="Portal1" runat="server" Layout="column" Height="230" Border="false">
                                            <Items>

                                                <%--Coluna Identificação--%>
                                                <ext:PortalColumn ID="PortalColumn1" runat="server" ColumnWidth="0.7" Border="false" DefaultAnchor="100%" Layout="anchor" StyleSpec="padding:5px">
                                                    <Items>

                                                        <ext:Portlet ID="Portlet1" runat="server" Padding="5" Title="Informações">
                                                            <Items>

                                                                <%--Campos ocultos--%>
                                                                <ext:TextField ID="hd_tipo" Hidden="true" DataIndex="hdtipo" runat="server" />

                                                                <ext:TextField FieldLabel="Identificador" Width="150" LabelWidth="80" Disabled="true"
                                                                    ID="cardapioID" ReadOnly="true" AnchorHorizontal="50%" runat="server"
                                                                    DataIndex="codigo" />

                                                                <ext:TextField ID="txtNome" DataIndex="nome" runat="server" Width="210" FieldLabel="Nome" LabelWidth="45" />

                                                                <ext:RadioGroup ID="rdTipo" FieldLabel="Tipo" LabelWidth="40" Width="210" runat="server" DataIndex="tipo">
                                                                    <Items>
                                                                        <ext:Radio ID="Radio1" BoxLabel="Simples"
                                                                            Name="tipo" runat="server" InputValue="S">
                                                                            <Listeners>
                                                                                <Check Handler="//if(checked){ if(PanelProdutos != null){#{PanelProdutos}.disable(); #{rgLocal}.disable();}}
                                                                                    " />
                                                                            </Listeners>
                                                                        </ext:Radio>
                                                                        <ext:Radio ID="Radio2" Cls="radioex" BoxLabel="Composto"
                                                                            Name="tipo" runat="server" InputValue="C">
                                                                            <Listeners>
                                                                                <Check Handler="//if(checked){if(PanelProdutos != null){#{PanelProdutos}.enable();#{rgLocal}.enable(); }}
                                                                                    " />
                                                                            </Listeners>
                                                                        </ext:Radio>
                                                                    </Items>
                                                                </ext:RadioGroup>

                                                                <ext:Checkbox ID="CheckAtivo" FieldLabel="Ativo" DataIndex="status" runat="server" />

                                                                <ext:Checkbox ID="CheckDiaTodo" FieldLabel="Dia todo" runat="server" DataIndex="diaTodo">
                                                                    <Listeners>
                                                                        <Check Handler="if(checked){#{txthorarioini}.disable();#{txthorariofim}.disable();#{Label2}.disable();#{Label1}.disable();}
                                                                                        else{#{txthorarioini}.enable();#{txthorariofim}.enable();#{Label2}.enable();#{Label1}.enable();}" />
                                                                    </Listeners>
                                                                </ext:Checkbox>

                                                                <ext:Label ID="Label2" runat="server" FieldLabel="Horário" Margins="0 0 30 0" />
                                                                <ext:Panel ID="Panel1" runat="server" Layout="Column" Width="260" Height="30" Border="false">
                                                                    <Items>
                                                                        <ext:Container ID="Container1" runat="server" ColumnWidth="0.3">
                                                                            <Items>
                                                                                <ext:TextField
                                                                                    ID="txthorarioini"
                                                                                    runat="server"
                                                                                    Width="50"
                                                                                    DataIndex="dataInicio">
                                                                                </ext:TextField>
                                                                            </Items>
                                                                        </ext:Container>

                                                                        <ext:Container ID="Container2" runat="server" ColumnWidth="0.1">
                                                                            <Items>
                                                                                <ext:Label ID="Label1" runat="server" Text="à" Margins="0 0 17 0" />
                                                                            </Items>
                                                                        </ext:Container>

                                                                        <ext:Container ID="Container3" runat="server" ColumnWidth="0.45">
                                                                            <Items>
                                                                                <ext:TextField
                                                                                    ID="txthorariofim"
                                                                                    runat="server"
                                                                                    Width="50"
                                                                                    DataIndex="dataFim" />

                                                                            </Items>
                                                                        </ext:Container>
                                                                    </Items>
                                                                </ext:Panel>

                                                            </Items>
                                                        </ext:Portlet>

                                                    </Items>
                                                </ext:PortalColumn>

                                                <%--Dias da semana--%>
                                                <ext:PortalColumn ID="PortalColumn2" runat="server" ColumnWidth="0.3" Border="false" DefaultAnchor="100%"
                                                    Layout="anchor" StyleSpec="padding:5px 5px 5px 5px">
                                                    <Items>
                                                        <ext:Portlet ID="Portlet2" runat="server" Padding="5" Title="Dias da semana">
                                                            <Items>
                                                                <ext:Checkbox ID="CheckSegunda" runat="server" FieldLabel="Segunda" DataIndex="segunda" LabelWidth="60" />
                                                                <ext:Checkbox ID="CheckTerca" runat="server" DataIndex="terca" FieldLabel="Terça" LabelWidth="60" />
                                                                <ext:Checkbox ID="CheckQuarta" runat="server" DataIndex="quarta" FieldLabel="Quarta" LabelWidth="60" />
                                                                <ext:Checkbox ID="CheckQuinta" runat="server" DataIndex="quinta" FieldLabel="Quinta" LabelWidth="60" />
                                                                <ext:Checkbox ID="CheckSexta" runat="server" DataIndex="sexta" FieldLabel="Sexta" LabelWidth="60" />
                                                                <ext:Checkbox ID="CheckSabado" runat="server" DataIndex="sabado" FieldLabel="Sábado" LabelWidth="60" />
                                                                <ext:Checkbox ID="CheckDomingo" runat="server" DataIndex="domingo" FieldLabel="Domingo" LabelWidth="60" />
                                                            </Items>
                                                        </ext:Portlet>
                                                    </Items>
                                                </ext:PortalColumn>

                                            </Items>
                                        </ext:Portal>
                                    </Items>
                                </ext:Panel>


                                <%--Painel de Itens--%>
                                <ext:Panel ID="PanelItens" runat="server" HideMode="Offsets" Height="310" Title="Itens">
                                    <Items>
                                        <ext:RowLayout ID="RowLayout1" runat="server">
                                            <Rows>
                                                <ext:LayoutRow RowHeight=".25">
                                                    <ext:Panel ID="Panel3" runat="server" Height="100" Cls="images-view">
                                                        <Items>
                                                            <ext:DataView ID="DataViewGrupo1" runat="server"
                                                                StoreID="storeGrupo1"
                                                                AutoHeight="true"
                                                                MultiSelect="true"
                                                                OverClass="x-view-over"
                                                                ContextMenuID="DataViewContextMenu"
                                                                ItemSelector="div.thumb-wrap">
                                                                <Template ID="Template1" runat="server">
                                                                    <Html>
                                                                        <tpl for=".">
								                                            <div class="thumb-wrap" id="{idItemCard}">
									                                            <div style="background-color:{cor}; color:{corFonte}; width:114px; height:21px; padding-top:7px;">{descricao}</div>
								                                            </div>
							                                            </tpl>
                                                                        <div class="x-clear"></div>
                                                                    </Html>
                                                                </Template>
                                                                <Listeners>
                                                                    <SelectionChange Fn="selectionChanged1" />
                                                                    <DblClick Handler="carregarForm(DataViewGrupo1);" />
                                                                    <ContextMenu Handler="dataviewGeral = this;" />
                                                                </Listeners>
                                                            </ext:DataView>
                                                        </Items>
                                                        <TopBar>
                                                            <ext:Toolbar ID="Toolbar2" runat="server">
                                                                <Items>
                                                                    <ext:ToolbarTextItem Text="Cozinha "></ext:ToolbarTextItem>
                                                                    <ext:ToolbarSeparator />
                                                                    <ext:Button ID="Button1" runat="server" ToolTip="Nova Item" Icon="Add">
                                                                        <Listeners>
                                                                            <Click Handler="novoGrupo1();" />
                                                                        </Listeners>
                                                                    </ext:Button>
                                                                    <ext:Button ID="Button2" runat="server" ToolTip="Editar" Icon="CommentEdit">
                                                                        <Listeners>
                                                                            <Click Handler="carregarForm(DataViewGrupo1)" />
                                                                        </Listeners>
                                                                    </ext:Button>
                                                                    <ext:Button ID="Button3" runat="server" ToolTip="Alterar Cor de Fundo" Icon="Paintcan">
                                                                        <Menu>
                                                                            <ext:ColorMenu ID="bt3ColorMenu" runat="server">
                                                                                <Listeners>
                                                                                    <Select Handler="alteraCor(DataViewGrupo1,color)" />
                                                                                </Listeners>
                                                                            </ext:ColorMenu>
                                                                        </Menu>
                                                                    </ext:Button>
                                                                     <ext:Button ID="Button25" runat="server" ToolTip="Altar Cor da Fonte" Icon="PaintBrushColor">
                                                                        <Menu>
                                                                            <ext:ColorMenu ID="ColorMenu4" runat="server">
                                                                                <Listeners>
                                                                                    <Select Handler="alteraCorFonte(DataViewGrupo1,color)" />
                                                                                </Listeners>
                                                                            </ext:ColorMenu>
                                                                        </Menu>
                                                                    </ext:Button>
                                                                    <ext:Button ID="Button4" runat="server" ToolTip="Ativar" Icon="Lightbulb">
                                                                        <Listeners>
                                                                            <Click Handler="ativarItem(DataViewGrupo1);" />
                                                                        </Listeners>
                                                                    </ext:Button>
                                                                    <ext:Button ID="Button14" runat="server" ToolTip="Inativar" Icon="LightbulbOff">
                                                                        <Listeners>
                                                                            <Click Handler="inativarItem(DataViewGrupo1);" />
                                                                        </Listeners>
                                                                    </ext:Button>
                                                                    <ext:PagingToolbar ID="PagingToolbar3" runat="server" StoreID="storeGrupo1" Flat="true" HideBorders="true" HideLabels="true" PageSize="12" HideRefresh="true" HideLabel="true" DisplayInfo="false" />
                                                                </Items>
                                                            </ext:Toolbar>
                                                        </TopBar>
                                                    </ext:Panel>
                                                </ext:LayoutRow>
                                                <ext:LayoutRow RowHeight=".25">
                                                    <ext:Panel ID="Panel5" runat="server" Height="100" Cls="images-view">
                                                        <Items>
                                                            <ext:DataView ID="DataViewGrupo2" runat="server"
                                                                StoreID="storeGrupo2"
                                                                AutoHeight="true"
                                                                MultiSelect="true"
                                                                OverClass="x-view-over"
                                                                ContextMenuID="DataViewContextMenu"
                                                                ItemSelector="div.thumb-wrap">
                                                                <Template ID="Template2" runat="server">
                                                                    <Html>
                                                                        <tpl for=".">
								                                            <div class="thumb-wrap" id="{idItemCard}">
									                                            <div style="background-color:{cor}; color:{corFonte}; width:114px; height:21px; padding-top:7px;">{descricao}</div>
								                                            </div>
							                                            </tpl>
                                                                        <div class="x-clear"></div>
                                                                    </Html>
                                                                </Template>
                                                                <Listeners>
                                                                    <SelectionChange Fn="selectionChanged2" />
                                                                    <DblClick Handler="carregarForm(DataViewGrupo2)" />
                                                                    <ContextMenu Handler="dataviewGeral = this;" />
                                                                </Listeners>
                                                            </ext:DataView>
                                                        </Items>
                                                        <TopBar>
                                                            <ext:Toolbar ID="Toolbar9" runat="server">
                                                                <Items>
                                                                    <ext:ToolbarTextItem Text="Bar "></ext:ToolbarTextItem>
                                                                    <ext:ToolbarSeparator />
                                                                    <ext:Button ID="Button15" runat="server" ToolTip="Nova Item" Icon="Add">
                                                                        <Listeners>
                                                                            <Click Handler="novoGrupo2();" />
                                                                        </Listeners>
                                                                    </ext:Button>
                                                                    <ext:Button ID="Button16" runat="server" ToolTip="Editar" Icon="CommentEdit">
                                                                        <Listeners>
                                                                            <Click Handler="carregarForm(DataViewGrupo2)" />
                                                                        </Listeners>
                                                                    </ext:Button>
                                                                    <ext:Button ID="Button18" runat="server" ToolTip="Alterar Cor de Fundo" Icon="Paintcan">
                                                                        <Menu>
                                                                            <ext:ColorMenu ID="ColorMenu1" runat="server">
                                                                                <Listeners>
                                                                                    <Select Handler="alteraCor(DataViewGrupo2,color)" />
                                                                                </Listeners>
                                                                            </ext:ColorMenu>
                                                                        </Menu>
                                                                    </ext:Button>
                                                                     <ext:Button ID="Button26" runat="server" ToolTip="Altar Cor da Fonte" Icon="PaintBrushColor">
                                                                        <Menu>
                                                                            <ext:ColorMenu ID="ColorMenu5" runat="server">
                                                                                <Listeners>
                                                                                    <Select Handler="alteraCorFonte(DataViewGrupo2,color)" />
                                                                                </Listeners>
                                                                            </ext:ColorMenu>
                                                                        </Menu>
                                                                    </ext:Button>
                                                                    <ext:Button ID="Button19" runat="server" ToolTip="Ativar" Icon="Lightbulb">
                                                                        <Listeners>
                                                                            <Click Handler="ativarItem(DataViewGrupo2);" />
                                                                        </Listeners>
                                                                    </ext:Button>
                                                                    <ext:Button ID="Button22" runat="server" ToolTip="Inativar" Icon="LightbulbOff">
                                                                        <Listeners>
                                                                            <Click Handler="inativarItem(DataViewGrupo2);" />
                                                                        </Listeners>
                                                                    </ext:Button>
                                                                    <ext:PagingToolbar ID="PagingToolbar4" DisplayInfo="false" runat="server" StoreID="storeGrupo2" Flat="true" HideBorders="true" HideLabels="true" PageSize="12" HideRefresh="true" />
                                                                </Items>
                                                            </ext:Toolbar>
                                                        </TopBar>
                                                    </ext:Panel>
                                                </ext:LayoutRow>
                                                <ext:LayoutRow RowHeight=".50">
                                                    <ext:Panel ID="Panel4" runat="server" Height="100" Title="" Header="false" Cls="images-view">
                                                        <Items>
                                                            <ext:ColumnLayout ID="ColumnLayout1" runat="server">
                                                                <Columns>
                                                                    <ext:LayoutColumn ColumnWidth="0.5">
                                                                        <ext:Panel runat="server" Height="100" Title="Categoria 1">
                                                                            <Items>
                                                                                <ext:DataView ID="DataViewCat1" runat="server"
                                                                                    StoreID="storeCat1"
                                                                                    AutoHeight="true"
                                                                                    MultiSelect="true"
                                                                                    OverClass="x-view-over"
                                                                                    ContextMenuID="DataViewContextMenu"
                                                                                    ItemSelector="div.thumb-wrap">
                                                                                    <Template ID="Template3" runat="server">
                                                                                        <Html>
                                                                                            <tpl for=".">
								                                                                <div class="thumb-wrap" id="{idItemCard}">
									                                                               <div style="background-color:{cor}; color:{corFonte}; width:114px; height:21px; padding-top:7px;">{descricao}</div>
								                                                                </div>
							                                                                </tpl>
                                                                                            <div class="x-clear"></div>
                                                                                        </Html>
                                                                                    </Template>
                                                                                    <Listeners>
                                                                                        <SelectionChange Fn="selectionChanged3" />
                                                                                        <DblClick Handler="carregarForm(DataViewCat1)" />
                                                                                        <ContextMenu Handler="dataviewGeral = this;" />
                                                                                    </Listeners>
                                                                                </ext:DataView>
                                                                            </Items>
                                                                            <TopBar>
                                                                                <ext:Toolbar ID="Toolbar7" runat="server">
                                                                                    <Items>
                                                                                        <ext:Button ID="Button5" runat="server" ToolTip="Nova Item" Icon="Add">
                                                                                            <Listeners>
                                                                                                <Click Handler="novoCat1();" />
                                                                                            </Listeners>
                                                                                        </ext:Button>
                                                                                        <ext:Button ID="Button6" runat="server" ToolTip="Editar" Icon="CommentEdit">
                                                                                            <Listeners>
                                                                                                <Click Handler="carregarForm(DataViewCat1)" />
                                                                                            </Listeners>
                                                                                        </ext:Button>
                                                                                        <ext:Button ID="Button9" runat="server" ToolTip="Alterar Cor de Fundo" Icon="Paintcan">
                                                                                            <Menu>
                                                                                                <ext:ColorMenu ID="ColorMenu2" runat="server">
                                                                                                    <Listeners>
                                                                                                        <Select Handler="alteraCor(DataViewCat1,color)" />
                                                                                                    </Listeners>
                                                                                                </ext:ColorMenu>
                                                                                            </Menu>
                                                                                        </ext:Button>
                                                                                         <ext:Button ID="Button27" runat="server" ToolTip="Altar Cor da Fonte" Icon="PaintBrushColor">
                                                                                            <Menu>
                                                                                                <ext:ColorMenu ID="ColorMenu6" runat="server">
                                                                                                    <Listeners>
                                                                                                        <Select Handler="alteraCorFonte(DataViewCat1,color)" />
                                                                                                    </Listeners>
                                                                                                </ext:ColorMenu>
                                                                                            </Menu>
                                                                                        </ext:Button>
                                                                                        <ext:Button ID="Button10" runat="server" ToolTip="Ativar" Icon="Lightbulb">
                                                                                            <Listeners>
                                                                                                <Click Handler="ativarItem(DataViewCat1);" />
                                                                                            </Listeners>
                                                                                        </ext:Button>
                                                                                        <ext:Button ID="Button23" runat="server" ToolTip="Inativar" Icon="LightbulbOff">
                                                                                            <Listeners>
                                                                                                <Click Handler="inativarItem(DataViewCat1);" />
                                                                                            </Listeners>
                                                                                        </ext:Button>
                                                                                        <ext:PagingToolbar ID="PagingToolbar5" DisplayInfo="false" runat="server" StoreID="storeCat1" Flat="true" HideBorders="true" HideLabels="true" PageSize="12" HideRefresh="true" />
                                                                                    </Items>
                                                                                </ext:Toolbar>
                                                                            </TopBar>
                                                                        </ext:Panel>
                                                                    </ext:LayoutColumn>
                                                                    <ext:LayoutColumn ColumnWidth="0.5">
                                                                        <ext:Panel runat="server" Height="100" Title="Categoria 2">
                                                                            <Items>
                                                                                <ext:DataView ID="DataViewCat2" runat="server"
                                                                                    StoreID="storeCat2"
                                                                                    AutoHeight="true"
                                                                                    MultiSelect="true"
                                                                                    OverClass="x-view-over"
                                                                                    ContextMenuID="DataViewContextMenu"
                                                                                    ItemSelector="div.thumb-wrap">
                                                                                    <Template ID="Template4" runat="server">
                                                                                        <Html>
                                                                                            <tpl for=".">
								                                                                <div class="thumb-wrap" id="{idItemCard}">
									                                                                <div style="background-color:{cor}; color:{corFonte}; width:114px; height:21px; padding-top:7px;">{descricao}</div>
								                                                                </div>
							                                                                </tpl>
                                                                                            <div class="x-clear"></div>
                                                                                        </Html>
                                                                                    </Template>
                                                                                    <Listeners>
                                                                                        <DblClick Handler="carregarForm(DataViewCat2)" />
                                                                                        <ContextMenu Handler="dataviewGeral = this;" />
                                                                                    </Listeners>

                                                                                </ext:DataView>
                                                                            </Items>
                                                                            <TopBar>
                                                                                <ext:Toolbar ID="Toolbar8" runat="server">
                                                                                    <Items>
                                                                                        <ext:Button ID="Button12" runat="server" ToolTip="Nova Item" Icon="Add">
                                                                                            <Listeners>
                                                                                                <Click Handler="novoCat2();" />
                                                                                            </Listeners>
                                                                                        </ext:Button>
                                                                                        <ext:Button ID="Button17" runat="server" ToolTip="Editar" Icon="ApplicationEdit">
                                                                                            <Listeners>
                                                                                                <Click Handler="carregarForm(DataViewCat2)" />
                                                                                            </Listeners>
                                                                                        </ext:Button>
                                                                                        <ext:Button ID="Button20" runat="server" ToolTip="Alterar Cor de Fundo" Icon="Paintcan">
                                                                                            <Menu>
                                                                                                <ext:ColorMenu ID="ColorMenu3" runat="server">
                                                                                                    <Listeners>
                                                                                                        <Select Handler="alteraCor(DataViewCat2,color)" />
                                                                                                    </Listeners>
                                                                                                </ext:ColorMenu>
                                                                                            </Menu>
                                                                                        </ext:Button>
                                                                                         <ext:Button ID="Button28" runat="server" ToolTip="Altar Cor da Fonte" Icon="PaintBrushColor">
                                                                                            <Menu>
                                                                                                <ext:ColorMenu ID="ColorMenu7" runat="server">
                                                                                                    <Listeners>
                                                                                                        <Select Handler="alteraCorFonte(DataViewCat2,color)" />
                                                                                                    </Listeners>
                                                                                                </ext:ColorMenu>
                                                                                            </Menu>
                                                                                        </ext:Button>
                                                                                        <ext:Button ID="Button21" runat="server" ToolTip="Ativar" Icon="Lightbulb">
                                                                                            <Listeners>
                                                                                                <Click Handler="ativarItem(DataViewCat2);" />
                                                                                            </Listeners>
                                                                                        </ext:Button>
                                                                                        <ext:Button ID="Button24" runat="server" ToolTip="Inativar" Icon="LightbulbOff">
                                                                                            <Listeners>
                                                                                                <Click Handler="inativarItem(DataViewCat2);" />
                                                                                            </Listeners>
                                                                                        </ext:Button>
                                                                                        <ext:PagingToolbar ID="PagingToolbar6" runat="server" DisplayInfo="false" StoreID="storeCat2" Flat="true" HideBorders="true" HideLabels="true" PageSize="9" HideRefresh="true" />
                                                                                    </Items>
                                                                                </ext:Toolbar>
                                                                            </TopBar>
                                                                        </ext:Panel>
                                                                    </ext:LayoutColumn>
                                                                </Columns>
                                                            </ext:ColumnLayout>
                                                        </Items>
                                                    </ext:Panel>
                                                </ext:LayoutRow>
                                            </Rows>

                                        </ext:RowLayout>
                                    </Items>
                                </ext:Panel>

                            </Items>
                        </ext:TabPanel>
                    </Items>
                </ext:FormPanel>
            </Items>
            <TopBar>
                <ext:Toolbar ID="Toolbar3" runat="server" Flat="true">
                    <Items>
                        <ext:Button ID="btnAdd" runat="server" Text="" Icon="Add">
                            <DirectEvents>
                                <Click OnEvent="AddClick" />
                            </DirectEvents>
                            <Listeners>
                                <Click Handler="#{FormPanel1}.body.mask('Carregando...', 'x-mask-loading');" />
                            </Listeners>
                        </ext:Button>

                        <ext:Button ID="Button7" runat="server" Text="" Icon="Cross">
                            <DirectEvents>
                                <Click OnEvent="ExcluirClick" />
                            </DirectEvents>
                            <Listeners>
                                <Click Handler="#{FormPanel1}.body.mask('Carregando...', 'x-mask-loading');" />
                            </Listeners>
                        </ext:Button>

                        <ext:ToolbarSeparator />

                        <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="1" Flat="true" StoreID="storeCardapio"
                            DisplayInfo="false">
                        </ext:PagingToolbar>

                        <ext:ToolbarSeparator />

                        <ext:ToolbarFill />

                    </Items>
                </ext:Toolbar>
            </TopBar>
            <Buttons>
                <ext:Button ID="btnOKPrincipal" runat="server" Text="OK" Icon="Add" Width="80px">
                    <Listeners>
                          <Click Handler="btnSalvarPrincipal.fireEvent('click'); JanelaItens.hide();" />
                    </Listeners>
                </ext:Button>
                <ext:Button ID="btnSalvarPrincipal" runat="server" Text="Salvar" Icon="Disk" Width="80px">
                    <DirectEvents>
                        <Click OnEvent="btnSalvarPrincipal_Click">
                            <EventMask ShowMask="True"></EventMask>
                            <ExtraParams>
                                <ext:Parameter Name="Grupo1" Value="Ext.encode(TransformaDados(storeGrupo1.getModifiedRecords()))" Mode="Raw" />
                                <ext:Parameter Name="Grupo2" Value="Ext.encode(TransformaDados(storeGrupo2.getModifiedRecords()))" Mode="Raw" />
                                <ext:Parameter Name="Categoria1" Value="Ext.encode(TransformaDados(storeCat1.getModifiedRecords()))" Mode="Raw" />
                                <ext:Parameter Name="Categoria2" Value="Ext.encode(TransformaDados(storeCat2.getModifiedRecords()))" Mode="Raw" />
                            </ExtraParams>
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="btnCancelarPrincipal" runat="server" Text="Cancelar" Icon="Cancel" Width="80px">
                    <Listeners>
                        <Click Handler="#{JanelaPrincipal}.hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>



        <%------------------------------------------------- Janela Adicionar Itens -------------------------------------------%>
        <ext:Window
            ID="JanelaItens"
            runat="server"
            Collapsible="false"
            Height="300"
            Icon="BuildingGo"
            Title="Itens do cardápio"
            Hidden="true"
            Modal="true"
            BodyStyle="background-color: #fff;"
            Padding="5"
            Width="500">
            <Items>
                <ext:FormPanel ID="FormPanel2" runat="server" Border="false" Layout="Form" AutoWidth="true">
                    <Items>
                        <ext:TextField ID="txtDescricaoItem" FieldLabel="Descrição" DataIndex="descricao" runat="server">
                        </ext:TextField>
                        <ext:ColorPalette ID="cpCorItem" FieldLabel="Cor de Fundo" runat="server">
                            <Listeners>
                                <Select Handler="hdCorItem.setValue(this.value)" />
                            </Listeners>
                        </ext:ColorPalette>
                        <ext:Hidden ID="hdCorItem" runat="server" DataIndex="cor" />
                        <ext:Checkbox ID="chkAtivoItem" FieldLabel="Ativo" runat="server" DataIndex="ativo">
                        </ext:Checkbox>
                        <ext:Checkbox ID="chkUsaPrecoItem" runat="server" FieldLabel="Associa Produto" DataIndex="usaPreco">
                            <Listeners>
                                <Change Handler="alert(this.checked);" />
                                <Check Handler="if(this.checked) {cbProdutoItem.setDisabled(false);} else {cbProdutoItem.setDisabled(true);}" />
                            </Listeners>
                        </ext:Checkbox>
                        <ext:ComboBox ID="cbProdutoItem"
                            runat="server"
                            DataIndex="idProduto"
                            Width="370"
                            FieldLabel="Produto"
                            StoreID="storeComboProdutos"
                            TypeAhead="false"
                            ForceSelection="false"
                            HideTrigger="true"
                            EnableKeyEvents="true"
                            ValueField="codigo"
                            Disabled="true"
                            DisplayField="nome">
                            <Listeners>
                                <AfterRender Handler="this.mun(this.el, 'keyup', this.onKeyUp, this);
                                                                          this.mon(this.el, 'keyup', onKeyUp, this);" />
                                <Select Handler="cbPrecoItem.removeByIndex(0);
                                                                     cbPrecoItem.removeByIndex(0);
                                                                     cbPrecoItem.removeByIndex(0);
                                                                     Ext.net.DirectMethods.CarregarPrecos(this.getValue());" />
                            </Listeners>
                        </ext:ComboBox>

                        <ext:ComboBox ID="cbPrecoItem" runat="server" FieldLabel="Preço" Width="100" Disabled="true" DataIndex="nuPreco"></ext:ComboBox>
                    </Items>
                </ext:FormPanel>
            </Items>

            <Buttons>
                <ext:Button ID="Button8" runat="server" Text="OK" Icon="Add" Width="80px">
                    <Listeners>
                        <Click Handler="FormPanel2.getForm().updateRecord(dataviewGeral.getSelectedRecords()[0]); JanelaItens.hide();" />
                    </Listeners>
                </ext:Button>
                <ext:Button ID="Button11" runat="server" Text="Salvar" Icon="Disk" Width="80px">
                    <Listeners>
                        <Click Handler="FormPanel2.getForm().updateRecord(dataviewGeral.getSelectedRecords()[0]);" />
                    </Listeners>
                </ext:Button>
                <ext:Button ID="Button13" runat="server" Text="Cancelar" Icon="Cancel" Width="80px">
                    <Listeners>
                        <Click Handler="#{JanelaItens}.hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>

    </form>
</body>
</html>
