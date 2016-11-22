<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Artebit.Restaurante.Administracao.Login" %>

<html>
<head runat="server">
    <title>..:: Administrativo ::..</title>

    <link href="Css/Login.css" rel="stylesheet" type="text/css" />

 <script type="text/javascript">
     var isInIFrame = (window.location != window.parent.location) ? true : false;
 
     if(isInIFrame) {
         window.parent.location = window.location;
     }
 </script>

    <%-- Código CSS --%> 
    <style type="text/css">
    .classeWindow
    {
        margin-left: 100px;
        position: absolute;
    }
    .x-panel-body
    {
        background:none;
    }
    .x-panel-body-noheader, .x-panel-mc .x-panel-body
    {
        border:none;
    }
    .x-panel-bwrap
    {
        position: relative;
        z-index: 1;
        color: White;
    }
    .x-form-item-label
    {
        font-family: arial;
        font-size: 16px;
    }
    .x-btn button
    {
        font-size: 14px;
    }
    </style>


      <%--Código JavaScript--%>
    <script type="text/javascript" language="javascript">
        var enterKeyPressHandler = function(f, e) {
            if (e.getKey() == e.ENTER) {
                Ext.net.DirectMethods.LogIn();
                e.stopEvent();
            }
        };
        
           function MM_callJS(jsStr) { //v2.0
               return eval(jsStr);
           }
    //-->

 
    <!--

         function hidden()

         { document.body.style.overflow = 'hidden'; }

         function unhidden()

         { document.body.style.overflow = ''; } 
 
    // --> 
 
    </script>

</head>
<body onLoad="MM_callJS('hidden()')">
    <form id="form1" runat="server">

    <ext:ResourceManager ID="ResourceManager1" runat="server" />

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

    <div>

        <div id="divFundo">

            <div id="fundo2"></div>
             
             <div id="divLogo"></div>

             <div id="imagemSistema">
                <label class="labelSistema">
                    
                    SISTEMA ADMINISTRATIVO - VERSÃO 1.0

                </label>
             </div>
                    
            <div id="divLogin">
        
                <label class="labelLogin">
                    
                    LOGIN

                </label>


            <ext:Panel runat="server" ID="panel_menu" Cls="panelLogin">

                <Items>

                <ext:TextField 
                Width="450px"
                ID="nomeUsuario" 
                runat="server" 
                FieldLabel="Usuário" 
                AllowBlank="false"
                BlankText="Campo para digitar o nome de usuário."
                AnchorHorizontal="100%"
                />
                <ext:TextField
                ID="senhaUsuario" 
                runat="server" 
                InputType="Password" 
                FieldLabel="Senha" 
                AllowBlank="false"
                BlankText="Campo para digitar a senha do usuário."
                AnchorHorizontal="100%"
                >
                <Listeners>
                        <SpecialKey Fn="enterKeyPressHandler" />
                    </Listeners>
                </ext:TextField>

                <%-- Lista as empresas --%>
               <%-- <ext:ComboBox 
                    ID="combo_empresa"
                    runat="server"
                    FieldLabel="Empresa"
                    StoreID="store_empresa"
                    Width="301"
                    Editable="false"
                    ValueField="idEmp"
                    DisplayField="nomeFantasia"
                    TypeAhead="true" 
                    Mode="Local"
                    ForceSelection="true"
                    EmptyText="Escolha..."
                    SelectOnFocus="true"
                    AllowBlank="false">
                    </ext:ComboBox>--%>


                </Items>
                                   
                <Buttons>
                                
                <ext:Button ID="botaoLogar" Text="Entrar" runat="server">
                    <Listeners>
                        <Click Handler="
                            if (!#{nomeUsuario}.validate() || !#{senhaUsuario}.validate()) {
                                Ext.Msg.alert('Erro','Digite seu nome de usuário e senha!');
                                return false; 
                            }" />
                    </Listeners>
                    <DirectEvents>
                        <Click OnEvent="Logar">
                            <EventMask ShowMask="true" Msg="Verificando..." MinDelay="500" />
                        </Click>
                    </DirectEvents>
                </ext:Button>

                </Buttons>

            </ext:Panel>                    
            </div>

        </div>

    </div>
        
        <!-- Window de alteração de senha -->
        <ext:Window 
            runat="server" 
            Width="330" 
            Height="140"
            Title="Alteração de Senha"
            Padding="20"
            hidden="true"
            ID="pss">
            
            <Items>
                
                <ext:Label 
                    runat="server"
                    Text="Sua senha expirou, favor informar uma nova senha!"/>

                <ext:TextField
                    ID="txt_senha" 
                    runat="server" 
                    InputType="Password" 
                    FieldLabel="Nova Senha" 
                    AllowBlank="false"
                    BlankText="Campo para digitar a nova senha do usuário."
                    AnchorHorizontal="100%"
                    Width="270"
                    />
                

                <%--<ext:Button runat="server" ID="btOk" Text="Salvar" OnClick="btOKClick" />--%>

            </Items>
            <Buttons>
            <ext:Button ID="btnOk" runat="server" Text="OK" Icon="Accept">
                <DirectEvents>
                    <Click OnEvent="btOKClick">
                        <EventMask ShowMask="true" Msg="Verificando..." MinDelay="500" />
                    </Click>
                </DirectEvents>
                <Listeners>
                    <Click Handler="#{pss}.hide();" />
                </Listeners>
            </ext:Button>
            </Buttons>
        </ext:Window>

    </form>
</body>
</html>
