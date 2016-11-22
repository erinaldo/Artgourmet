<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lista.aspx.cs" Inherits="Artebit.Restaurante.Administracao.Relatorios.Lista" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" id="theme" href="" /> 
    <%-- Código JavaScript --%>
    <script type="text/javascript">
        function trocaTema() {
            var themeUrl = '../temas/css/xtheme-Beige.css';
            window.Ext.util.CSS.swapStyleSheet("theme", themeUrl);
        }
        var cont = 1;
    </script>
</head>
<body onload="trocaTema()">
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server">
        </ext:ResourceManager>
        
        <ext:Store ID="storeLista" runat="server" AutoLoad="false">
            <Reader>
                <ext:JsonReader IDProperty="relatorio">
                    <Fields>
                        <ext:RecordField Name="codigo" Type="String" />
                        <ext:RecordField Name="relatorio" Type="String" />
                        <ext:RecordField Name="descricao" Type="String" />
                        <ext:RecordField Name="nomeArquivo" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
    
        <ext:GridPanel ID="gridLista" runat="server" Height="300" StoreID="storeLista" AutoExpandColumn="relatorio">
            <ColumnModel runat="server" ID="gridListaCMModel"  >
                <Columns>
                    <ext:Column Header="Codigo" DataIndex="codigo" runat="server" Width="100" />
                    <ext:Column Header="Relatório" ColumnID="relatorio" DataIndex="relatorio" runat="server" />
                    <ext:Column Header="Descrição" DataIndex="descricao" runat="server" Width="450" />
                    <ext:ImageCommandColumn Width="200">
                        <Commands>
                            <ext:ImageCommand CommandName="abrir" Icon="ControlPlay" Text="Visualizar">
                                <ToolTip Text="Visualizar" />
                            </ext:ImageCommand>
                        </Commands>
                    </ext:ImageCommandColumn>
                </Columns>
            </ColumnModel>
            <Listeners>
                <Command Handler=" if(command == 'abrir') { window.open('VerRelatorio.aspx?id='+record.data.nomeArquivo,'mywindow' + cont,'width=700,height=400'); cont++; }"></Command>
                <RowDblClick Handler="window.open('VerRelatorio.aspx?id='+storeLista.getAt(rowIndex).data.nomeArquivo,'mywindow' + cont,'width=700,height=400'); cont++;" />
            </Listeners>
            <SelectionModel>
                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" >
                </ext:RowSelectionModel>
            </SelectionModel>
        </ext:GridPanel>
    
        <ext:Window runat="server" ID="winRelatorio" Width="800px" Height="400px" Maximizable="true" Hidden="true" Padding="0"
             Icon="Information" Modal="true" Title="Visualização de Relatório" >
        </ext:Window>

    </div>
    </form>
</body>
</html>
