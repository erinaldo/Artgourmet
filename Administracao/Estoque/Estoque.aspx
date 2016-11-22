﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Estoque.aspx.cs" Inherits="Artebit.Restaurante.Administracao.Estoque.Estoque" %>


<html">
<head runat="server">
    <title>..:: Estoque ::..</title>
    
     <link rel="stylesheet" type="text/css" id="theme" href="" /> 

    <script type="text/javascript">
        function trocaTema() {
            var themeUrl = '../temas/css/xtheme-Beige.css';
            Ext.util.CSS.swapStyleSheet("theme", themeUrl);
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

</head>
<body onload="trocaTema()">
    <form id="form1" runat="server">
    
     <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <%-- Store é o datasource, o resultado dos selects --%>
        
   
    </form>
</body>
</html>
