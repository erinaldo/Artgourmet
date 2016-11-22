<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Funcoes.aspx.cs" Inherits="Administracao.Funcao.Funcoes" %>

<html>
<head runat="server">
    <title>..:: Funcões::..</title>
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

    <ext:Portal ID="Portal1" runat="server" Layout="column">
        <Items>
            <ext:PortalColumn ID="PortalColumn2" runat="server" Height="500" ColumnWidth="0.3" DefaultAnchor="100%"
                Layout="anchor" StyleSpec="padding:10px 10px 10px 10px">
                <Items>
                <%--Funcoes globais--%>
                    <ext:Portlet ID="portlet1" runat="server" Padding="5" Title="Funções Globais">
                        <Items>
                            <ext:TreePanel ID="TreePanel1" runat="server" Width="300" Height="440" Icon="BookOpen"
                                RootVisible="false" AutoScroll="true">
                                <TopBar>
                                    <ext:Toolbar ID="Toolbar1" runat="server">
                                        <Items>
                                            <ext:Button ID="Button1" runat="server" Text="Expandir">
                                                <Listeners>
                                                    <Click Handler="#{TreePanel1}.expandAll();" />
                                                </Listeners>
                                            </ext:Button>
                                            <ext:Button ID="Button2" runat="server" Text="Fechar">
                                                <Listeners>
                                                    <Click Handler="#{TreePanel1}.collapseAll();" />
                                                </Listeners>
                                            </ext:Button>
                                        </Items>
                                    </ext:Toolbar>
                                </TopBar>
                                <Root>
                                    <ext:TreeNode Text="Funções" Expanded="true">
                                        <Nodes>
                                            <ext:TreeNode Text="Produção">
                                                <Nodes>
                                                    <ext:TreeNode Text="Produtos">
                                                        <Nodes>
                                                            <ext:TreeNode Text="Plano de Fidelidade (Por Produto)" Icon="ApplicationLink">
                                                            <CustomAttributes>
                                                                <ext:ConfigItem
                                                                    Name="url"
                                                                    Value="Paginas/planoFidelidade.aspx"
                                                                    Mode="Value" />
                                                            </CustomAttributes>
                                                            </ext:TreeNode>
                                                            
                                                            <ext:TreeNode Text="Observações" Icon="ApplicationLink">
                                                            <CustomAttributes>
                                                                <ext:ConfigItem
                                                                    Name="url"
                                                                    Value="Paginas/observacao.aspx"
                                                                    Mode="Value" />
                                                            </CustomAttributes>
                                                            </ext:TreeNode>
                                                        </Nodes>
                                                    </ext:TreeNode>
                                                </Nodes>
                                            </ext:TreeNode>
                                        </Nodes>
                                    </ext:TreeNode>
                                </Root>
                                <BottomBar>
                                    <ext:StatusBar ID="StatusBar1" runat="server" AutoClear="1500" />
                                </BottomBar>
                                <Listeners>
                                    <Click Handler="Panel1.load({
                                                    mode : 'iframe',
                                                    url : node.attributes.url
                                                });#{StatusBar1}.setStatus({text: 'Opção selecionada: <b>' + node.text + '<br />', clear: true});" />
                                    <ExpandNode Handler="#{StatusBar1}.setStatus({text: 'Opção selecionada: <b>' + node.text + '<br />', clear: true});"
                                        Delay="30" />
                                    <CollapseNode Handler="#{StatusBar1}.setStatus({text: 'Opção selecionada: <b>' + node.text + '<br />', clear: true});" />
                                </Listeners>
                            </ext:TreePanel>
                        </Items>
                    </ext:Portlet>
                </Items>
            </ext:PortalColumn>
            <ext:PortalColumn ID="PortalColumn3" runat="server" ColumnWidth="0.7" Height="500" DefaultAnchor="100%"
                Layout="anchor" StyleSpec="padding:10px">
                <Items>
                    <ext:Portlet ID="portFunc" runat="server" Padding="5" Title="Ações">
                        <Items>
                        <%--Ações--%>
                            <ext:Panel ID="Panel1" runat="server" Height="440" Region="Center">
                                <AutoLoad Url="" Mode="IFrame" ShowMask="true" />
                            </ext:Panel>
                        </Items>
                    </ext:Portlet>
                </Items>
            </ext:PortalColumn>
        </Items>
    </ext:Portal>

    </form>
</body>
</html>
