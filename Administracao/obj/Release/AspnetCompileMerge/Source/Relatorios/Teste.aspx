<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Teste.aspx.cs" Inherits="Artebit.Restaurante.Administracao.Relatorios.Teste" %>

<%@ Register Assembly="Stimulsoft.Report.WebFx, Version=2012.2.1304.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a"
    Namespace="Stimulsoft.Report.WebFx" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:StiWebViewerFx ID="StiWebViewerFx1" ThemeName="Silver" 
            Localization="pt-BR" runat="server" Height="100%" Width="100%" />
    </div>
    </form>
</body>
</html>
