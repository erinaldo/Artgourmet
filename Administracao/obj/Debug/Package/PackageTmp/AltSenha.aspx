<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AltSenha.aspx.cs" Inherits="Artebit.Restaurante.Administracao.AltSenha" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>..:: Administrativo ::..</title>

    <link href="Css/Login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        
        <ext:Panel runat="server" ID="panel_AltSenha" Cls="panelLogin">
            <Items>
                <ext:TextField
                    ID="txt_senha" 
                    runat="server" 
                    InputType="Password" 
                    FieldLabel="Nova Senha" 
                    AllowBlank="false"
                    BlankText="Campo para digitar a nova senha do usuário."
                    AnchorHorizontal="100%"
                    />
                
                <ext:Button runat="server" ID="btOk" Text="Salvar" OnClick="btOKClick" />

            </Items>
        </ext:Panel>

    </form>
</body>
</html>
