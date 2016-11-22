<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerRelatorio.aspx.cs" Inherits="Artebit.Restaurante.Administracao.Relatorios.VerRelatorio" %>

<%@ Register Assembly="Stimulsoft.Report.WebFx, Version=2012.2.1304.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a"
    Namespace="Stimulsoft.Report.WebFx" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
         <cc1:StiWebViewerFx ID="reportViewer" ThemeName="Silver" 
            runat="server" Height="100%" Width="100%"
             ShowExportToBmp="false" ShowExportToCsv="false"
                 ShowExportToDbf="false" ShowExportToDif="false" 
            ShowExportToExcelXml="false" ShowExportToMetafile="false" ShowExportToMht="false"
                 ShowExportToOpenDocumentCalc="false" 
            ShowExportToOpenDocumentWriter="false" ShowExportToPcx="false" 
            ShowExportToGif="false" ShowExportToSvg="false"
                 ShowExportToSvgz="false" ShowExportToSylk="false" 
            ShowExportToText="false" ShowExportToTiff="false" ShowExportToXml="false" 
            ShowExportToXps="false" Localization="pt-BR" LocalizationDirectory="Localization" />
    </form>
</body>
</html>
