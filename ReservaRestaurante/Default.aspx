<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Artebit.Restaurante.Reserva.Default" %>

<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title>..:: Reserva ::..</title>
    
    <%--Código javaScript--%>
    <script type="text/javascript">
        // Função para sair do sistema
        var sair = function () {
            Ext.MessageBox.confirm("Confirme", "Deseja realmente sair do sistema?",
                function(btn) {
                    if (btn == "yes") {
                        location.href = "LogOut.aspx";
                    }
                }
            );
        };

        // Função para adicionar nova aba
        var addTab = function (tabPanel, id, url, titulo) {
            var tab = tabPanel.getComponent(id);

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
    </script>
    
    <%--Código CSS--%>
    <style type="text/css">
        .div_menu 
        {
            height: 89px;
            position: relative;
            background: url("Img/fundoSobMenu.png") repeat-x;
        }
        
        .div_menu_item
        {
            position: relative;
            height: 89px;
            background-color: transparent;
            text-align:center;
            float: left;
            margin-right: 1%;
            width: 7.5%;
            min-width:110px;
        }
        
        .div_menu div 
        {
            border: none;    
        }
        
        .div_menu_item :hover 
        {
            background-color:#3D5270;
            border-radius:5px 5px 0px 0px;
            -moz-border-radius:5px 5px 0px 0px;
            -webkit-border-radius:5px 5px 0px 0px;
        }
        
        .div_menu_item img 
        {
            margin-top: 10%;
            padding: 10% 10% 5%;
        }
        
        .div_sair
        {
            float: right!important;
            margin-right: 0%!important;
            width: 7.5%;
        }
        
        .div_itens
        {
           width: 7.5%; 
        }
        .labelTitulo
        {
            border-bottom: 8px solid #3D5270;
            color: #FFFFFF;
            display: block;
            font-family: Arial;
            font-size: 19pt;
            margin-left: 10px;
            margin-top: 0;
            padding: 0;
            position: absolute;
            width: 107px;
        }
        
        .labelMenu
        {
            color: #FFF;
            font-family:Calibri,Arial;
            font-size:15px;
            font-weight:bold;
            text-align:center;
            padding:3px;
            padding-top:0px;
        }
        
        .divTitulo 
        {
            width: 11%;
            margin-top: 1.6%;
            float: left;
            margin-right: 1%;
            width: 130px;
            margin-left: 2%;
        }
        
        .rodape
        {
            font-family:Calibri,Arial; 
            font-size:15px; 
            text-align:center; 
            width:100%; 
            background:url("Img/fundo1.jpg"); 
            color:White; 
            min-height:50px;
        }
        
        .rodape a
        {
            color:White; 
            text-decoration:none;
        }
    </style>

</head>
<body>
       
    <form id="form1" runat="server">
    <ext:ResourceManager runat="server" />

        <ext:ViewPort runat="server">
            <Items>

                <ext:AnchorLayout runat="server">
                    <Anchors>

                        <ext:Anchor Horizontal="100%">
                            <%-- painel do menu --%>
                            <ext:Panel runat="server" ID="panel_menu" BaseCls="div_menu">
                                <Items>
                                    
                                    <ext:Panel BaseCls="divTitulo" runat="server">
                                        <Items>
                                            <ext:Label Cls="labelTitulo" runat="server">
                                                Reservas
                                            </ext:Label>
                                        </Items>
                                    </ext:Panel>

                                    <ext:Panel BaseCls="div_menu_item div_itens" runat="server">
                                        <Items>
                                            <ext:ImageButton ID="ImageButton1" runat="server" 
                                                ImageUrl="Img/rest.png">
                                                <Listeners>
                                                    <Click Handler="addTab(#{TabPanel1}, 'reserva', 'Reserva.aspx', 'Reserva');" />
                                                </Listeners>
                                            </ext:ImageButton>
                                        </Items>
                                        <Content>
                                        <div class="labelMenu"> Reservas</div>
                                        </Content>
                                    </ext:Panel>
                                    
                                    <ext:Panel BaseCls="div_menu_item div_itens" runat="server">
                                        <Items>
                                            <ext:ImageButton ID="ImageButton2" runat="server" 
                                                ImageUrl="Img/filaEspera.png">
                                                <Listeners>
                                                    <Click Handler="addTab(#{TabPanel1}, 'filaespera', 'Espera.aspx', 'Fila de espera');" />
                                                </Listeners>
                                            </ext:ImageButton>
                                        </Items>
                                        <Content>
                                        <div class="labelMenu"> Fila de Espera</div>
                                        </Content>
                                    </ext:Panel>

                                    <ext:Panel BaseCls="div_menu_item div_itens" runat="server">
                                        <Items>
                                            <ext:ImageButton ID="ImageButton3" runat="server" 
                                                ImageUrl="Img/mesa.png">
                                                <Listeners>
                                                    <Click Handler="addTab(#{TabPanel1}, 'mesa', 'Mesa.aspx', 'Mesas');" />
                                                </Listeners>
                                            </ext:ImageButton>
                                        </Items>
                                        <Content>
                                            <div class="labelMenu"> Mesas</div>
                                        </Content>
                                    </ext:Panel>

                                    <ext:Panel BaseCls="div_menu_item div_sair" runat="server">
                                        <Items>
                                            <ext:ImageButton ID="ImageButton5" runat="server" 
                                                ImageUrl="Img/sair.png">
                                                <Listeners>
                                                    <Click Handler="sair();" />
                                                </Listeners>
                                            </ext:ImageButton>
                                        </Items>
                                        <Content>
                                            <div class="labelMenu"> Sair</div>
                                        </Content>
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

                        <ext:Anchor Horizontal="100%" Vertical="4%">
                            <ext:Panel ID="Panel2" runat="server" Layout="Fit" Border="false">
                                    <Content>
                                        <div class="rodape">
                                            <a href="http://www.artebit.com.br" target="_blank">
                                                Artebit Gourmet - Artebit Informática Ltda &copy; 2012  - Clique aqui para mais informações
                                            </a>
                                        </div>
                                    </Content>
                            </ext:Panel>
                         </ext:Anchor>
                    </Anchors>
                
                </ext:AnchorLayout>

            </Items>
        </ext:ViewPort>

    </form>

</body>
</html>
