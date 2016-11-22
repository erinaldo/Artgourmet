<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Administracao.Default" %>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    
    <title>..:: Administrativo ::..</title>

        <%-- Código JavaScript --%>
        <Script type="text/javascript">

            // Função para trocar de filial
            function trocarFilial() {
                Ext.net.DirectMethods.TrocarFilial();
            }

            // Função para sair do sistema
            var sair = function () {
                Ext.MessageBox.confirm("Confirme", "Deseja realmente sair do sistema?",
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

                var tabPanel = Ext.getCmp('TabPanel1');

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
            function trocaImg(nome, tipo){
                
                // Imagem normal
                if (tipo == 1) {
                    document.images[nome].src = "Img/" + nome + ".png";
                } else {
                    
                    // Imagem Grande, quando passa o mouse
                    if(tipo == 2) {
                        document.images[nome].src = "Img/" + nome + "Hover.png";
                    }
                    else {
                        
                        // Imagem Opaca, desabilitada
                        if(tipo == 3) {
                            document.images[nome].src = "Img/" + nome + "Opaco.png";
                        }

                    }
                    
                }
            }
            
            
        </Script>

        <%-- Código Css --%>
        <style type="text/css">
        body { behavior:url(Css/csshover.htc) }
        
        .div_menu 
        {
            height: 89px;
            position: relative;
            left: 0;
            border: none;
            background: url("Img/fundoSobMenu.png") repeat-x;
        }
        
        .div_menu div 
        {
            border: none;    
        }
                      
        .labelTitulo
        {
            border-bottom: 8px solid #3D5270;
            color: #FFFFFF;
            display: block;
            font-family: Arial;
            font-size: 19pt;
            left: 2%;
            margin-left: 0;
            padding: 0;
            position: absolute;
            width: 157px;
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
        .window
        {
            font-family: Arial;
            font-size: 16px;
            height: 20px;
            padding: 35px;
        }
        .panelBotoes
        {
            height: 10px;
        }
        .iconWindow
        {
            background-image:url("Img/empresa.ico")
        }
        
        /* MENU */
        #menu
        {
            width: 75%; 
            margin-left: 15%; 
            padding: 0pt ! important; 
            background: none repeat scroll 0% 0% transparent; 
            position: absolute; 
            top: 24%;
        }         
        
        #menu ul {
            background-color:transparent;
            float: left;
            height: 88px;
            list-style: none outside none;
            margin-top: -20px;
            padding-top: 10px;
            width: 100%;
        }
        
        #menu ul li { display: inline-block; }
        
        #menu ul li a {
            padding: 2px 10px;
            float:left;
            /* visual do link */
            background-color:transparent;
            color: #333;
            text-decoration: none;
        }
        
        #menu ul li a:hover{
            background-color:#EFC687;
            height: 88px;
            margin-top: -10px;
            padding-top: 12px;
            
            /* arredondar bordas */
            -moz-border-radius: 5px 5px 0px 0px; /* Para Firefox */ 
            -webkit-border-radius: 5px 5px 0px 0px;  /* Para Safari e Chrome; */
            border-radius: 5px 5px 0px 0px; /* Para Opera 10.5+ */
        }
                
        .subMenu {
            background: none repeat scroll 0 0 #EFC687;
            display: none;
            height: auto;
            padding: 10px;
            position: absolute;
            top: 68px;
            width: 150px;
            z-index: 1;
            
            /* arredondar bordas */
            -moz-border-radius: 0px 5px 5px 5px; /* Para Firefox */ 
            -webkit-border-radius: 0px 5px 5px 5px;  /* Para Safari e Chrome; */
            border-radius: 0px 5px 5px 5px; /* Para Opera 10.5+ */
        }
            
        .subMenu li {
            background: none repeat scroll 0 0 transparent !important;
            color: white !important;
            float: none !important;
            font-family: Arial;
            font-size: 14px !important;
            letter-spacing: 0.1px;
            padding-bottom: 10px;
        }
        
        .subMenu li:hover
        {
            font-weight: bold;
            text-decoration: none;
        }
        
        #menu ul li:hover ol
        {
            display: block;
            cursor: pointer;
        }      
        
        .menuSair
        {
            right: -136px!important;
            position: absolute;
        }
        
        .olSistemas { left: 540px !important }
        
        .olProducao { left: 91px !important }
        
        .olVendas { left: 358px !important }
        
        .olCompras { left: 179px !important }           
        
        .olGlobal { left: 449px !important }           
            </style>

</head>
<body>
    <form id="form1" runat="server">

        <%-- Store da lista das empresas --%>
        <ext:Store ID="store_empresa" runat="server">
            <Reader>
                 <ext:JsonReader IDProperty="idEmp">
                    <Fields>
                        <ext:RecordField Name="nomeFantasia" Type="String"/>
                        <ext:RecordField Name="idEmp" Type="Int" />
                    </Fields>
                 </ext:JsonReader>
            </Reader>    
        </ext:Store>

        <%-- Store da lista de filiais --%>
        <ext:Store ID="store_filial" runat="server">
            <Reader>
                <ext:JsonReader >
                    <Fields>
                        <ext:RecordField Name="id" Type="String" />
                        <ext:RecordField Name="nome" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <SortInfo Field="nome" Direction="ASC" />
        </ext:Store>

    <ext:ResourceManager ID="ResourceManager1" runat="server" />

        <ext:ViewPort ID="ViewPort1" runat="server">
            <Items>

                <ext:AnchorLayout ID="AnchorLayout1" runat="server">
                    <Anchors>

                        <ext:Anchor Horizontal="100%">
                            <%-- painel do menu --%>
                            <ext:Panel runat="server" ID="panel_menu" BaseCls="div_menu" >
                            <Content>
                            <nav id="menu">
                                <ul id="Ul1" runat="server">
                                    <li id="Li1" runat="server" style="display: inline;">
                                        <a href="#">
                                            <img id="iconFinanceiro" name="iconFinanceiro" runat="server" src="Img/iconFinanceiro.png" 
                                                    onmouseover="trocaImg('iconFinanceiro',2)"
                                                    onmouseout="trocaImg('iconFinanceiro',1)" />
                                        </a></li>

                                    <li id="Li5" runat="server" style="display: inline;">
                                        <a href="#">
                                            <img id="iconProducao" name="iconProducao" runat="server" src="Img/iconProducao.png" 
                                                    onmouseover="trocaImg('iconProducao',2)"
                                                    onmouseout="trocaImg('iconProducao',1)" />
                                        </a>
                                            <ol class="subMenu olProducao">
                                                <li id="Li3" runat="server" onclick="addTab('produtos', 'Producao/Produtos.aspx', 'Produtos');"> Produtos </li>     
                                                <br />
                                                <li id="Li2" runat="server" onclick="addTab('cardapio', 'Producao/Cardapio.aspx', 'Cardápio');"> Cardápio </li>     
                                                <br />
                                                <li id="Li44" runat="server" onclick="addTab('unidades', 'Producao/Unidades.aspx', 'Unidades');"> Unidades </li>    
                                                <br />
                                                <li id="Li13" runat="server" onclick="addTab('grupos', 'Producao/Grupos.aspx', 'Grupos');"> Grupos </li>     
                                                <br />
                                                <li id="Li18" runat="server" onclick="addTab('observacoes', 'Producao/Observacoes.aspx', 'Observacoes');"> Observações </li>     
                                           </ol>
                                    </li>
                                    
                                    <li id="Li6" runat="server" style="display: inline;" >
                                        <a href="#">
                                            <img id="iconCompras" name="iconCompras" runat="server" src="Img/iconCompras.png" 
                                                    onmouseover="trocaImg('iconCompras',2)"
                                                    onmouseout="trocaImg('iconCompras',1)" />
                                        </a>
                                        <ol class="subMenu olCompras">
                                            <li id="Li20" runat="server" onclick="addTab('fornecedores', 'Producao/Fornecedores.aspx', 'Fornecedores');"> Fornecedores </li>
                                             <br/>
                                            <li id="Li26" runat="server" onclick="addTab('estoque', 'Compras/Estoque.aspx', 'Estoque');"> Estoque </li>    
                                            <br/>
                                            <li id="Li27" runat="server" onclick="addTab('solicitacaoCompra', 'Compras/SolicitacaoCompra.aspx', 'Solicitação Compra');"> Solicitação Compra </li>    
                                            <br/>
                                            <li id="Li28" runat="server" onclick="addTab('ordemCompra', 'Compras/OrdemCompra.aspx', 'Ordem de Compra');"> Ordem de Compra </li>    
                                        </ol>
                                    </li>
                                    
                                    <li id="Li7" runat="server" style="display: inline;" onclick="addTab('relatorios', 'Relatorios/Lista.aspx', 'Relatórios');">
                                        <a href="#">
                                            <img id="iconRelatorios" name="iconRelatorios" runat="server" src="Img/iconRelatorios.png" 
                                                    onmouseover="trocaImg('iconRelatorios',2)"
                                                    onmouseout="trocaImg('iconRelatorios',1)" />
                                        </a>
                                    </li>
                                    
                                   
                                    <li id="Li14" runat="server" style="display: inline;">
                                        <a href="#">
                                            <img id="iconVendas" name="iconVendas" runat="server" src="Img/iconVendas.png" 
                                                    onmouseover="trocaImg('iconVendas',2)"
                                                    onmouseout="trocaImg('iconVendas',1)" />
                                        </a>

                                         <ol class="subMenu olVendas">
                                            <li id="Li15" runat="server" onclick="addTab('impressoras', 'PDV/Impressoras.aspx', 'Impressoras');"> Impressoras </li>
                                            <br/>
                                            <li id="Li16" runat="server" onclick="addTab('monitores', 'PDV/Monitores.aspx', 'Monitores');"> Monitores </li>
                                            <br/>
                                            <li id="Li17" runat="server" onclick="addTab('vendedores', 'PDV/Vendedores.aspx', 'Vendedores');"> Vendedores </li>
                                            <br/>
                                            <li id="Li19" runat="server" onclick="addTab('fidelidade', 'PDV/Fidelidade.aspx', 'Fidelidade');"> Fidelidade </li>
                                            <br/>
                                            <li id="Li24" runat="server" onclick="addTab('aliquota', 'PDV/Aliquota.aspx', 'Alíquota');"> Alíquota </li>
                                            <br/>
                                            <li id="Li25" runat="server" onclick="addTab('mesas', 'PDV/Mesas.aspx', 'Mesas');"> Mesa </li>
                                          </ol>
                                    </li>

                                    <li id="LiGlobal" runat="server" style="display: inline;">
                                        <a href="#">
                                            <img id="iconGlobal" name="iconGlobal" runat="server" src="Img/iconGlobal.png" 
                                                    onmouseover="trocaImg('iconGlobal',2)"
                                                    onmouseout="trocaImg('iconGlobal',1)" />
                                        </a>

                                        <ol class="subMenu olGlobal">
                                            <li id="Li21" runat="server" onclick="addTab('funcoes', 'Funcao/funcoes.aspx', 'Funções Globais');" > Funções Globais </li>
                                            <br/>
                                            <li id="Li22" runat="server" onclick="trocarFilial()" > Trocar filial  </li>
                                        </ol>
                                    </li>

                                     <li id="Li8" runat="server" style="display: inline;">
                                        <a href="#">
                                            <img id="iconSistema" name="iconSistema" runat="server" src="Img/iconSistema.png" 
                                                    onmouseover="trocaImg('iconSistema',2)"
                                                    onmouseout="trocaImg('iconSistema',1)" />
                                        </a>

                                        <ol class="subMenu olSistemas">
                                            <li id="Li9" runat="server" onclick="addTab('usuario', 'Sistema/Usuario.aspx', 'Usuário');"> Usuários </li>
                                            <br/>
                                            <li id="Li10" runat="server" onclick="addTab('perfis', 'Sistema/Perfil.aspx', 'Perfis');"> Perfis </li>
                                            <br/>
                                            <li id="Li11" runat="server" onclick="addTab('parametros', 'Sistema/Parametro.aspx', 'Parâmetros');"> Parâmetros </li>
                                            <br/>     
                                            <li id="Li4" runat="server" onclick="addTab('filial', 'Sistema/Filial.aspx', 'Filial');"> Filial </li>
                                            <br/>     
                                            <li id="Li23" runat="server" onclick="addTab('empresa', 'Sistema/Empresa.aspx', 'Empresa');"> Empresa </li>
                                       </ol>
                                    </li>

                                    <li id="Li12" class="menuSair" runat="server" onclick="sair();"><a href="#"><img id="Img6" runat="server" src="Img/sair.png"/></a></li>
                                </ul>
                            </nav>
                            </Content>

                            <Items>
                                <ext:Panel BaseCls="divTitulo">
                                    <Items>
                                        <ext:Label ID="Label1" Cls="labelTitulo" runat="server">
                                            Administrativo
                                        </ext:Label>
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

        <ext:Window 
            ID="Window1" 
            runat="server" 
            Title="Empresa"  
            IconCls="iconWindow"
            Height="185px" 
            Width="350px"
            BodyStyle="background-color: #fff;" 
            Padding="5"
            Hidden="true"
            Collapsible="false"
            Closable="false" 
            Modal="true">
            <Items>

                        <ext:label ID="lblEmpresa" Text="Escolha a empresa:" />

                        <%-- Lista as empresas --%>
                        <ext:ComboBox 
                            ID="combo_empresa"
                            runat="server"
                            StoreID="store_empresa"
                            Width="250"
                            Editable="false"
                            ValueField="idEmp"
                            DisplayField="nomeFantasia"
                            TypeAhead="true" 
                            Mode="Local"
                            ForceSelection="true"
                            EmptyText="Escolha..."
                            SelectOnFocus="true"
                            IsRemoteValidation="true">
                                <RemoteValidation  OnValidation="atualizaFilial" ValidationEvent="select" EventOwner="Field" />
                            </ext:ComboBox>

                            <ext:label ID="lblFilial" Text="Escolha a filial:" />

                            <%-- Lista as filial --%>
                            <ext:ComboBox 
                                ID="combo_filial"
                                runat="server"
                                StoreID="store_filial"
                                Width="250"
                                Editable="false"
                                ValueField="id"
                                DisplayField="nome"
                                TypeAhead="true" 
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
