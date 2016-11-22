<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Grupos.aspx.cs" Inherits="Artebit.Restaurante.Administracao.Producao.Grupos" %>

<html>
<head id="Head1" runat="server">
    <title>..:: Grupos ::..</title>

    

    <%--Código JavaScript para a grid --%>
    <script type="text/javascript">

        function acao(id, comando) {
            if (comando == 1) {
                Ext.net.DirectMethods.AbrirFilho(id);
            }else {
                if (comando == 2) {
                    Ext.net.DirectMethods.Editar(id);
                } else {
                    if (comando == 3) {
                        Ext.net.DirectMethods.ExcluirItem(id);
                        //alert(id);
                    }
                }
            }
            
        }
        
    </script>

    <%--Código CSS--%>
    <style type="text/css">
    .botao{
        background-image:url("Img/botaoAddGrupos.png");
        width: 100px;
        height: 20px;
        cursor: pointer;
       }
       
       .botaoeditar{
        background-image:url("Img/botaoEditarGrupo.png");
        width: 80px;
        height: 20px;
        cursor: pointer;
       }
       
       .botaoexcluir{
        background-image:url("Img/botaoExcluirGrupos.png");
        width: 80px;
        height: 20px;
        cursor: pointer;
       }
    </style>

    <script type="text/javascript">
        var refreshTree = function (tree) {
            Ext.net.DirectMethods.RefreshMenu({
                success: function (result) {
                    var nodes = eval(result);
                    if (nodes.length > 0) {
                        tree.initChildren(nodes);
                    }
                    else {
                        tree.getRootNode().removeChildren();
                    }
                }
            });
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    
    <%--Store da grid lista de observações--%>
        <ext:Store ID="storeListaObservacoes" runat="server">
            <Reader>
                <ext:JsonReader IDProperty="id">
                    <Fields>
                        <ext:RecordField Name="id" Type="Int" />
                        <ext:RecordField Name="descricao" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

    <ext:Panel runat="server">
        <TopBar>
            <ext:Toolbar ID="Toolbar1" runat="server">
                <Items>
                    <ext:Button ID="Button1" runat="server" Text="Adicionar" Icon="Add">
                        <DirectEvents>
                            <Click OnEvent="AbrirJanela">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                </Items>
            </ext:Toolbar>
        </TopBar>
        <Tools>            
                <ext:Tool Type="Refresh" Qtip="Refresh" Handler="refreshTree(#{TreePanel1});" />
        </Tools>
        <Items>

            <ext:TreeGrid ID="ColumnTree1"  ClientIDMode="Static"
                runat="server"
                Height="500"
                RootVisible="false"
                AutoScroll="true"
                Animate="true"
                Selectable="true">
                <Columns>
                    <ext:TreeGridColumn  Header="Grupos" Width="330" DataIndex="um" />
                    <ext:TreeGridColumn  Header="Ação" Width="260" DataIndex="dois" >
                        <XTemplate ID="XTemplate1" runat="server">
                            <Html>
                                 <img src="../Img/botaoAddGrupos.png" onclick="acao({dois},1)" class="botao"/>
                                 <img src="../Img/botaoEditarGrupo.png" onclick="acao({dois},2)" class="botaoeditar"/>
                                 <img src="../Img/botaoExcluirGrupos.png" onclick="acao({dois},3)" class="botaoexcluir"/>
                            </Html>
                        </XTemplate>
                    </ext:TreeGridColumn>
                </Columns>
        </ext:TreeGrid>


        </Items>
    </ext:Panel>

    <ext:TextField ID="hd_lista" Hidden="true" runat="server"></ext:TextField>

    <ext:Window ID="WindowPrincipal" runat="server" Collapsible="false" Hidden="true" Modal="true"
        Height="300" Icon="BookAdd" Title="Grupo" Width="400">
        <Items>
           <ext:TabPanel ID="TabPanel1" runat="server" Height="300" DeferredRender="False">
                <Items>
                    <ext:Panel ID="Panel1" runat="server" Padding="7" Title="Informações" Icon="Information">
                        <Items>
                            <ext:TextField Hidden="true" ID="hd_tipo" runat="server" />
                            <ext:TextField Hidden="true" ID="hd_id" runat="server"/>
                            <ext:TextField Hidden="true" ID="pai" runat="server"/>
                            
                            <ext:TextField 
                                ID="codigo" 
                                FieldLabel="Código" 
                                Width="150" 
                                AllowBlank="false" 
                                runat="server">
                            </ext:TextField> 
                            
                            <ext:TextField 
                                ID="nome" 
                                FieldLabel="Descrição" 
                                Width="200" 
                                AllowBlank="false" 
                                runat="server">
                            </ext:TextField>        

                            <ext:Checkbox
                                ID="ativo" 
                                FieldLabel="Ativo" 
                                Checked="true"
                                runat="server">
                            </ext:Checkbox>        
                       </Items>
                    </ext:Panel>
                    <ext:Panel ID="Panel2" runat="server" Title="Observações" Padding="7" Icon="Information">
                        <Items>
                            <ext:Panel ID="PanelObs" HideMode="Offsets" runat="server" Title="Observações" Height="180" AutoScroll="True" Cls="AddProduto" Width="370">
                            <Items>
                                <ext:GridPanel ID="GridObservacoes" runat="server" Width="350" StoreID="storeListaObservacoes"> 
                                    <ColumnModel>
                                        <Columns>
                                            <ext:Column Header="Descrição" Width="323" DataIndex="descricao" />
                                        </Columns>
                                    </ColumnModel>
                                    <SelectionModel>
                                        <ext:CheckboxSelectionModel ID="check" runat="server" RowSpan="2" CheckOnly="True" />
                                    </SelectionModel>
                                     <Listeners>
                                        <CellClick Handler="if(columnIndex != 0) {this.getSelectionModel().selectRow(rowIndex);}" />
                                    </Listeners>
                                </ext:GridPanel>
                            </Items>
                        </ext:Panel>

                        </Items>
                    </ext:Panel>
                </Items>
            </ext:TabPanel> 
        </Items>
        <Buttons>
            <ext:Button ID="OK3" runat="server" Text="OK" Icon="Add" Width="80px">
                    <DirectEvents>
                        <Click OnEvent="SalvarDados"/>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button4" runat="server" Text="Cancelar" Icon="Cancel" Width="80px">
                    <Listeners>
                        <Click Handler="#{WindowPrincipal}.hide();" />
                    </Listeners>
                </ext:Button>
        </Buttons>
    </ext:Window>



    </form>
</body>
</html>
