<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Artebit.Restaurante.Reserva.Login"  %>

<%@ Import Namespace="Artebit.Restaurante.Global.Modelo" %>
<%@ Import Namespace="Artebit.Restaurante.Global.Util" %>
<%@ Import Namespace="Artebit.Restaurante.Global.RegrasNegocio.Reserva" %>
<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>..:: Reservas ::..</title>

    <link href="Css/Login.css" rel="stylesheet" type="text/css" />

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

   <%-- Código C# --%>
    <script runat="server">
        
        /// <summary>
        /// Função para fazer autenticação do usuário no sistema
        /// </summary>
        /// <param name="sender">objetos nomeUsuario,senhaUsuario</param>
        /// <param name="e"></param>
        protected void Logar(object sender, DirectEventArgs e)
        {
            using (Contexto.Atual = new Restaurante())
            {
                Criptografia cript = new Criptografia();
                
                string usuario_nome = this.nomeUsuario.Text;
                string usuario_senha = cript.GerarSHA1(cript.GerarMD5(this.senhaUsuario.Text));

                GUSUARIO usuario = new GUSUARIO();
                GPERFIL perfil = new GPERFIL();
                Artebit.Restaurante.Global.RegrasNegocio.Global.Usuario usu =
                    new Artebit.Restaurante.Global.RegrasNegocio.Global.Usuario();

                usuario.codusuario = usuario_nome;
                usuario.senha = usuario_senha;

                string[] sistemas = Convert.ToString(ConfigurationManager.AppSettings["CODSISTEMA"]).Split(',');
                Memoria.CodSistema = sistemas[0];

                usuario = (GUSUARIO) usu.ExecutaFuncao(usuario, Funcoes.Verificar, null);

                if (usuario != null && perfil != null)
                {

                    RPARAM pa = new RPARAM();
                    RParametros con = new RParametros();

                    pa = (RPARAM) con.ExecutaFuncao(pa, Funcoes.Buscar, null);

                    Memoria.Empresa = pa.idEmpresa;
                    Memoria.Filial = pa.idFilial;
                    
                    FormsAuthentication.SetAuthCookie(pa.idEmpresa + "ª" + usuario_nome, true);

                    MemoriaWeb.CriaSessao(usuario);

                    bool? valsenha = (bool)usu.ExecutaFuncao(usuario, Funcoes.ValidaSenha, null);

                    if (valsenha == false)
                    {
                        //WindowUtil.MostraModal();
                        pss.Show();
                        pss.Hidden = false;
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }                    
                }
                else
                {
                    X.Msg.Alert("Erro",
                                " O usuário não existe ou não tem permissão de acesso no sistema, consute o administrador.")
                        .Show();
                }
            }
        }
        
        [DirectMethod]
        public void LogIn()
        {
            Logar(botaoLogar, new DirectEventArgs(new Ext.Net.ParameterCollection()));
        }
        
    </script>

    <%--Código JavaScript--%>
    <script type="text/javascript" language="javascript">
        var enterKeyPressHandler = function(f, e) {
            if (e.getKey() == e.ENTER) {
                Ext.net.DirectMethods.LogIn();
                e.stopEvent();
            }
        }
        
           function MM_callJS(jsStr) { //v2.0
               return eval(jsStr)
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

    <div>

        <div id="divFundo">

            <div id="fundo2"></div>
             
             <div id="divLogo"></div>

             <div id="imagemSistema">
                <label class="labelSistema">
                    
                    SISTEMA RESERVAS - VERSÃO 1.0

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
                
                <ext:Label ID="Label1" 
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
        </ext:Window>    </form>
</body>
</html>
