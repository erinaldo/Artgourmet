<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Artebit.Restaurante.Administracao.Home" %>


<html>
<head runat="server">
    <title>..:: Administrativo ::..</title>
</head>
<body>
    <form id="form1" runat="server" >
        <ext:ResourceManager runat="server" />

        <ext:ViewPort runat="server">
            <Items>

                <ext:AnchorLayout runat="server">
                    <Anchors>

                        <ext:Anchor>
                            <ext:panel runat="server">
                                
                                <Items>
                                    <ext:Image runat="server" ImageUrl="Img/fundoHome.jpg" Height="100%" Width="100%"></ext:Image>
                                </Items>

                            </ext:panel>
                        </ext:Anchor>                                             

                    </Anchors>
                </ext:AnchorLayout> 
                
             </Items> 
       </ext:ViewPort> 

    </form>
</body>
</html>
