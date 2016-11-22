<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Artebit.Restaurante.Administracao.Default" %>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link rel="stylesheet" type="text/css" id="theme" href="" /> 

    <title>..:: Administrativo ::..</title>

    <%-- Código JavaScript --%>
    <script type="text/javascript">
        function trocaTema() {
            var themeUrl = 'temas/css/xtheme-Beige.css';
            window.Ext.util.CSS.swapStyleSheet("theme", themeUrl);
        }

        // Função para trocar de filial
        function trocarFilial() {
            window.Ext.net.DirectMethods.TrocarFilial();
        }

        // Função para sair do sistema
        var sair = function () {
            window.Ext.MessageBox.confirm("Confirme", "Deseja realmente sair do sistema?",
                function (btn) {
                    if (btn == "yes") {
                        location.href = "LogOut.aspx";
                    }
                }
            );
        };

        //addTab(#{TabPanel1}, 'sistema', 'Usuario.aspx', 'Sistema');

        // Função para adicionar nova aba
        function addTab(id, url, titulo) {

            var tabPanel = window.Ext.getCmp('TabPanel1');

            var tab = tabPanel.getItem(id);

            // Se achar o controle de abas
            if (!tab) {
                // Adiciona a nova aba
                tab = tabPanel.add({
                    id: id,
                    title: titulo,
                    closable: true,
                    layout: "fit",
                    autoLoad: {
                        showMask: true,
                        url: url,
                        mode: "iframe",
                        maskMsg: "Carregando... " //+ url + "..."
                    }
                });

                tab.on("activate", function () {
                    //var item = MenuPanel1.menu.items.get(id + "_item");

                    //if (item) {
                    //  MenuPanel1.setSelection(item);
                    //}
                }, this);
            }

            tabPanel.setActiveTab(tab);
        }



        // Função para troca de imagem
        function trocaImg(nome, tipo) {

            // Imagem normal
            if (tipo == 1) {
                document.images[nome].src = "Img/" + nome + ".png";
            } else {

                // Imagem Grande, quando passa o mouse
                if (tipo == 2) {
                    document.images[nome].src = "Img/" + nome + "Hover.png";
                }
                else {

                    // Imagem Opaca, desabilitada
                    if (tipo == 3) {
                        document.images[nome].src = "Img/" + nome + "Opaco.png";
                    }

                }

            }
        }


        if (typeof window.event != 'undefined')
            document.onkeydown = function () {
                if (event.srcElement.tagName.toUpperCase() != 'INPUT')
                    return (event.keyCode != 8);
            }
        else {
            document.onkeypress = function (e) {
                if (e.target.nodeName.toUpperCase() != 'INPUT')
                    return (e.keyCode != 8);
            }
        }

    </script>

    <%-- Código Css --%>
    <style type="text/css">
        body {
            behavior: url(Css/csshover.htc);
        }

        .div_menu {
            height: 75px;
            position: relative;
            left: 0;
            border: none;
            background: url("Img/header1.jpg") repeat-x;
        }

            .div_menu div {
                border: none;
            }

        .labelTitulo {
            color: #FFFFFF;
            display: block;
            font-family: Segoe UI, Verdana, sans-serif;
            font-size: 25px;
            left: 80px;
            margin-left: 0;
            padding: 0;
            position: absolute;
        }

        .imagemHeader {
            display: block;
            left: 0;
            margin-left: 0;
            padding: 0;
            top: 1px;
            position: absolute;
        }

        .labelMenu {
            color: #FFF;
            font-family: Calibri,Arial;
            font-size: 15px;
            font-weight: bold;
            text-align: center;
            padding: 3px;
            padding-top: 0;
        }

        .divTitulo {
            margin-top: 1.6%;
            float: left;
            margin-right: 1%;
            width: 130px;
            margin-left: 2%;
        }

        .rodape {
            font-family: Calibri,Arial;
            font-size: 15px;
            text-align: center;
            width: 100%;
            background: url("Img/fundo1.jpg");
            color: White;
            min-height: 50px;
        }

            .rodape a {
                color: White;
                text-decoration: none;
            }

        .window {
            font-family: Arial;
            font-size: 16px;
            height: 20px;
            padding: 35px;
        }

        .panelBotoes {
            height: 10px;
        }

        .iconWindow {
            background-image: url("Img/empresa.ico");
        }

        /* MENU */
        #menu {
            width: 75%;
            margin-left: 250px;
            margin-top: 0;
            padding: 0 !important;
            background: none repeat scroll 0 0 transparent;
            position: absolute;
            top: 0;
        }

            #menu ul {
                background-color: transparent;
                float: left;
                height: 75px;
                list-style: none outside none;
                margin-top: 0;
                padding-top: 0;
                width: 100%;
            }

                #menu ul li {
                    display: inline-block;
                }

                    #menu ul li a {
                        padding: 2px 10px;
                        float: left;
                        /* visual do link */
                        color: #333;
                        font-family: Segoe UI, Verdana, sans-serif;
                        font-size: 14px;
                        margin-right: 2px;
                        text-align: center;
                        text-decoration: none;
                    }

                        #menu ul li a:hover {
                            background: url(Img/fundoMenu2.jpg) repeat-x;
                        }

        .subMenu {
            background: none repeat scroll 0 0 #EFC687;
            display: none;
            height: auto;
            padding: 5px;
            position: absolute;
            top: 76px;
            width: 150px;
            z-index: 1;
            /* arredondar bordas */
            -moz-border-radius: 0 0 5px 5px; /* Para Firefox */
            -webkit-border-radius: 0 0 5px 5px; /* Para Safari e Chrome; */
            border-radius: 0 0 5px 5px; /* Para Opera 10.5+ */
        }

            .subMenu li {
                background: none repeat scroll 0 0 transparent !important;
                color: black !important;
                float: none !important;
                font-family: Segoe UI, Verdana, sans-serif;
                font-size: 15px !important;
                width: 145px;
                letter-spacing: 0.1px;
                padding-bottom: 10px;
            }

                .subMenu li:hover {
                    font-weight: bold;
                    text-decoration: none;
                }

        #menu ul li:hover ol {
            display: block;
            cursor: pointer;
        }

        .menuSair {
            right: 0;
            position: absolute;
        }

        .olSistemas {
            left: 529px !important;
        }

        .olProducao {
            left: 84px !important;
        }

        .olVendas {
            left: 352px !important;
        }

        .olCompras {
            left: 172px !important;
        }

        .olGlobal {
            left: 440px !important;
        }
    </style>

</head>
<body onload="trocaTema()">
    <form id="form1" runat="server">

        <%-- Store da lista das empresas --%>
        <ext:Store ID="store_empresa" runat="server">
            <Reader>
                <ext:JsonReader IDProperty="idEmp">
                    <Fields>
                        <ext:RecordField Name="nomeFantasia" Type="String" />
                        <ext:RecordField Name="idEmp" Type="Int" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

        <%-- Store da lista de filiais --%>
        <ext:Store ID="store_filial" runat="server">
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="id" Type="String" />
                        <ext:RecordField Name="nome" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <SortInfo Field="nome" Direction="ASC" />
        </ext:Store>

        <ext:ResourceManager ID="ResourceManager1" runat="server" />

        <ext:Viewport ID="ViewPort1" runat="server">
            <Items>

                <ext:AnchorLayout ID="AnchorLayout1" runat="server">
                    <Anchors>

                        <ext:Anchor Horizontal="100%">
                            <%-- painel do menu --%>
                            <ext:Panel runat="server" ID="panel_menu" BaseCls="div_menu">
                                <Content>
                                    <nav id="menu">
                                        <ul id="Ul1" runat="server">
                                            <li id="Li1" runat="server" style="display: inline;">
                                                <a href="#">
                                                    <img id="iconFinanceiro" name="iconFinanceiro" runat="server" src="Img/icones/financeiro.png" />
                                                    <br />
                                                    Financeiro
                                                </a></li>

                                            <li id="Li5" runat="server" style="display: inline;">
                                                <a href="#">
                                                   <img id="iconProducao" name="iconProducao" runat="server" src="Img/icones/producao.png" />
                                                    <br />
                                                    Produção
                                                </a>
                                                <ol class="subMenu olProducao">
                                                    <li id="Li3" runat="server" onclick="addTab('produtos', 'Producao/Produtos.aspx', 'Produtos');">Produtos </li>
                                                    <br />
                                                    <li id="Li2" runat="server" onclick="addTab('cardapio', 'Producao/Cardapio.aspx', 'Cardápio');">Cardápio </li>
                                                    <br />
                                                    <li id="Li44" runat="server" onclick="addTab('unidades', 'Producao/Unidades.aspx', 'Unidades');">Unidades </li>
                                                    <br />
                                                    <li id="Li13" runat="server" onclick="addTab('grupos', 'Producao/Grupos.aspx', 'Grupos');">Grupos </li>
                                                    <br />
                                                    <li id="Li18" runat="server" onclick="addTab('observacoes', 'Producao/Observacoes.aspx', 'Observacoes');">Observações </li>
                                                </ol>
                                            </li>

                                            <li id="Li6" runat="server" style="display: inline;">
                                                <a href="#">
                                                    <img id="iconCompras" name="iconCompras" runat="server" src="Img/icones/suprimento.png" />
                                                    <br />
                                                    Suprimento
                                                </a>
                                                <ol class="subMenu olCompras">
                                                    <li id="Li20" runat="server" onclick="addTab('fornecedores', 'Producao/Fornecedores.aspx', 'Fornecedores');">Fornecedores </li>
                                                    <br />
                                                    <li id="Li26" runat="server" onclick="addTab('estoque', 'Compras/Estoque.aspx', 'Estoque');">Estoque </li>
                                                    <br />
                                                    <li id="Li27" runat="server" onclick="addTab('solicitacaoCompra', 'Compras/SolicitacaoCompra.aspx', 'Solicitação Compra');">Solicitação Compra </li>
                                                    <br />
                                                    <li id="Li28" runat="server" onclick="addTab('ordemCompra', 'Compras/OrdemCompra.aspx', 'Ordem de Compra');">Ordem de Compra </li>
                                                </ol>
                                            </li>

                                            <li id="Li7" runat="server" style="display: inline;" onclick="addTab('relatorios', 'Relatorios/Lista.aspx', 'Relatórios');">
                                                <a href="#">
                                                    <img id="iconRelatorios" name="iconRelatorios" runat="server" src="Img/icones/relatorio.png" />
                                                    <br />
                                                    Relatórios
                                                </a>
                                            </li>


                                            <li id="Li14" runat="server" style="display: inline;">
                                                <a href="#">
                                                    <img id="iconVendas" name="iconVendas" runat="server" src="Img/icones/pdv.png" />
                                                    <br />
                                                    Pdv
                                                </a>

                                                <ol class="subMenu olVendas">
                                                    <li id="Li15" runat="server" onclick="addTab('impressoras', 'PDV/Impressoras.aspx', 'Impressoras');">Impressoras </li>
                                                    <br />
                                                    <li id="Li16" runat="server" onclick="addTab('monitores', 'PDV/Monitores.aspx', 'Monitores');">Monitores </li>
                                                    <br />
                                                    <li id="Li17" runat="server" onclick="addTab('vendedores', 'PDV/Vendedores.aspx', 'Vendedores');">Vendedores </li>
                                                    <br />
                                                    <li id="Li19" runat="server" onclick="addTab('fidelidade', 'PDV/Fidelidade.aspx', 'Fidelidade');">Fidelidade </li>
                                                    <br />
                                                    <li id="Li24" runat="server" onclick="addTab('aliquota', 'PDV/Aliquota.aspx', 'Alíquota');">Alíquota </li>
                                                    <br />
                                                    <li id="Li25" runat="server" onclick="addTab('mesas', 'PDV/Mesas.aspx', 'Mesas');">Mesa </li>
                                                </ol>
                                            </li>

                                            <li id="LiGlobal" runat="server" style="display: inline;">
                                                <a href="#">
                                                     <img id="iconGlobal" name="iconGlobal" runat="server" src="Img/icones/global.png" />
                                                    <br />
                                                    Global
                                                </a>

                                                <ol class="subMenu olGlobal">
                                                    <li id="Li21" runat="server" onclick="addTab('funcoes', 'Funcao/funcoes.aspx', 'Funções Globais');">Funções Globais </li>
                                                    <br />
                                                    <li id="Li22" runat="server" onclick="trocarFilial()">Trocar filial  </li>
                                                </ol>
                                            </li>

                                            <li id="Li8" runat="server" style="display: inline;">
                                                <a href="#">
                                                    <img id="iconSistema" name="iconSistema" runat="server" src="Img/icones/config.png" />
                                                    <br />
                                                    Sistema
                                                </a>

                                                <ol class="subMenu olSistemas">
                                                    <li id="Li9" runat="server" onclick="addTab('usuario', 'Sistema/Usuario.aspx', 'Usuário');">Usuários </li>
                                                    <br />
                                                    <li id="Li10" runat="server" onclick="addTab('perfis', 'Sistema/Perfil.aspx', 'Perfis');">Perfis </li>
                                                    <br />
                                                    <li id="Li11" runat="server" onclick="addTab('parametros', 'Sistema/Parametro.aspx', 'Parâmetros');">Parâmetros </li>
                                                    <br />
                                                    <li id="Li4" runat="server" onclick="addTab('filial', 'Sistema/Filial.aspx', 'Filial');">Filial </li>
                                                    <br />
                                                    <li id="Li23" runat="server" onclick="addTab('empresa', 'Sistema/Empresa.aspx', 'Empresa');">Empresa </li>
                                                </ol>
                                            </li>

                                            <li id="Li12" class="menuSair" runat="server" onclick="sair();">
                                                <a href="#">
                                                    <img id="iconSair" name="iconVendas" runat="server" src="Img/icones/sair.png" />
                                                    <br />
                                                    Sair
                                                </a>
                                            </li>
                                        </ul>
                                    </nav>
                                </Content>

                                <Items>
                                    <ext:Panel BaseCls="divTitulo" runat="server">
                                        <Items>
                                            <ext:Image ID="Image1" runat="server" Cls="imagemHeader" ImageUrl="Img/logo1.png" Width="82" Height="74">
                                            </ext:Image>
                                            <ext:Label ID="Label1" Cls="labelTitulo" runat="server" Text="Administrativo" />
                                        </Items>
                                    </ext:Panel>
                                </Items>

                            </ext:Panel>

                        </ext:Anchor>


                        <ext:Anchor Horizontal="100%" Vertical="83%">
                            <%-- painel das abas --%>
                            <ext:TabPanel ID="TabPanel1" runat="server">
                                <Items>
                                    <ext:Panel ID="Panel1" runat="server" Title="Home" Layout="Fit">
                                        <AutoLoad Url="Home.aspx" Mode="IFrame" ShowMask="true" MaskMsg="Carregando..."></AutoLoad>
                                    </ext:Panel>
                                </Items>
                            </ext:TabPanel>

                        </ext:Anchor>

                        <%-- rodape --%>
                        <ext:Anchor Horizontal="100%" Vertical="4%">
                            <ext:Panel ID="Panel2" runat="server" Layout="Fit" Border="false">
                                <Content>
                                    <div class="rodape">
                                        <a href="http://www.artebit.com.br" target="_blank">Artebit Gourmet - Artebit Informática Ltda &copy; 2012  - Clique aqui para mais informações
                                        </a>
                                    </div>
                                </Content>
                            </ext:Panel>
                        </ext:Anchor>

                    </Anchors>

                </ext:AnchorLayout>

            </Items>
        </ext:Viewport>

        <ext:Window
            ID="Window1"
            runat="server"
            Title="Empresa"
            IconCls="iconWindow"
            Height="185px"
            Width="400px"
            Resizable="False"
            BodyStyle="background-color: #fff;"
            Padding="15"
            LabelAlign="Top"
            Hidden="true"
            Collapsible="false"
            Closable="false"
            Modal="true">
            <Items>

                <%-- Lista as empresas --%>
                <ext:ComboBox
                    ID="combo_empresa"
                    runat="server"
                    StoreID="store_empresa"
                    Width="340"
                    Editable="false"
                    ValueField="idEmp"
                    DisplayField="nomeFantasia"
                    FieldLabel="Empresa"
                    Mode="Local"
                    ForceSelection="true"
                    EmptyText="Escolha..."
                    SelectOnFocus="true"
                    IsRemoteValidation="true">
                    <RemoteValidation OnValidation="atualizaFilial" ValidationEvent="select" EventOwner="Field" />
                </ext:ComboBox>

                <%-- Lista as filial --%>
                <ext:ComboBox
                    ID="combo_filial"
                    runat="server"
                    StoreID="store_filial"
                    Width="340"
                    Editable="false"
                    ValueField="id"
                    DisplayField="nome"
                    FieldLabel="Filial"
                    Mode="Local"
                    ForceSelection="true"
                    EmptyText="Escolha..."
                    SelectOnFocus="true"
                    AllowBlank="false">
                </ext:ComboBox>

            </Items>

            <Buttons>
                <ext:Button ID="botaoOk" runat="server" Text="OK" Width="80px">
                    <DirectEvents>
                        <Click OnEvent="funcao_empresa" />
                    </DirectEvents>
                </ext:Button>
            </Buttons>
        </ext:Window>

    </form>
</body>
</html>
