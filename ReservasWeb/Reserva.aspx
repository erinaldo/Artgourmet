<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reserva.aspx.cs" Inherits="ReservasWeb.Reserva2" %>

<%@ Import Namespace="Artebit.Restaurante.Global.Modelo" %>
<%@ Import Namespace="Artebit.Restaurante.Global.RegrasNegocio.Global" %>
<%@ Import Namespace="Artebit.Restaurante.Global.RegrasNegocio.Reserva" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.UX" Namespace="Ext.Net.UX" TagPrefix="ux" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

    <%--Código C#--%>
    <script runat="server">
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Memoria.Empresa = Contexto.Atual.GPARAMETROS.First().idEmpresa;
        Memoria.Filial = Contexto.Atual.GPARAMETROS.First().idFilial;
        Memoria.CodSistema = Convert.ToString(ConfigurationManager.AppSettings["CODSISTEMA"]);
        
    }

    // Função para window de reserva
    protected void formReserva_acao(object sender, DirectEventArgs e)
    {
        string msg = "";
        try
        {

            Reserva reserva = new Reserva();
            RRESERVA res = new RRESERVA();
            List<string> compl = new List<string>();


            string horario = txt_data.Text.Replace("00:00:00", txt_horario.Text);


            RParametros parametro = new RParametros();
            RPARAM param = new RPARAM();

            param = (RPARAM)parametro.ExecutaFuncao(param, Funcoes.Buscar, null);


            res.cliente = txt_cliente.Text;
            res.telefone = txt_telefone.Text;
            res.emailCliente = txt_email.Text;
            res.data = Convert.ToDateTime(txt_data.Text);
            res.horario = Convert.ToDateTime(horario);
            res.horarioLimite = Convert.ToDateTime(param.horFinReserva);
            //res.qtdLugares = Convert.ToInt32(cmb_qtd.Value);
            res.observacao = TxaObs.Text;
            res.status = 7;

            List<GMESA> mesas = (List<GMESA>)Session["mesas"];

            if (mesas != null)
            {
                foreach (GMESA m in mesas)
                {
                    RRESMESA r = new RRESMESA();
                    r.idEmpresa = Memoria.Empresa.Value;
                    r.idFilial = Memoria.Filial.Value;
                    r.idReserva = res.idReserva;
                    r.nuMesa = m.nuMesa;

                    res.RRESMESA.Add(r);
                }
            }

            msg = (string)reserva.ExecutaFuncao(res, Funcoes.ConfereDadosWeb, compl);

            //Verifica se o valor digitado é o mesmo que foi gerado
            //pela página do captcha

            if (txtCaptcha.Text != Session["CaptchaValue"].ToString())
            {
                msg = "Código digitado está incorreto";
            }

            if (msg == "")
            {
                bool conf = false;
                // Adicionar                  
                conf = (bool)reserva.ExecutaFuncao(res, Funcoes.Adicionar, compl);

                if (conf)
                {

                    string mensagem = param.corpoEmail2;

                    mensagem = mensagem.Replace("{idReserva}", Convert.ToString(res.idReserva));
                    mensagem = mensagem.Replace("{nome}", res.cliente);
                    mensagem = mensagem.Replace("{telefone}", res.telefone);
                    mensagem = mensagem.Replace("{horario}", res.horario.Value.ToString("HH:mm"));
                    mensagem = mensagem.Replace("{horarioLimite}", res.horarioLimite.Value.ToString("HH:mm"));
                    mensagem = mensagem.Replace("{data}", res.data.Value.ToString("dd/MM/yyyy"));
                    mensagem = mensagem.Replace("{status}", Convert.ToString(res.status));
                    mensagem = mensagem.Replace("{dataInclusao}", Convert.ToString(res.dataInclusao));
                    mensagem = mensagem.Replace("{emailCliente}", res.emailCliente);
                    mensagem = mensagem.Replace("{observacao}", res.observacao);
                    mensagem = mensagem.Replace("{idEmpresa}", Convert.ToString(res.idEmpresa));
                    mensagem = mensagem.Replace("{idFilial}", Convert.ToString(res.idFilial));
                    mensagem = mensagem.Replace("{dataAlteracao}", Convert.ToString(res.dataAlteracao));
                    mensagem = mensagem.Replace("{identificador}", res.identificador);

                    string assunto = param.assuntoEmail2;

                    assunto = assunto.Replace("{idReserva}", Convert.ToString(res.idReserva));
                    assunto = assunto.Replace("{nome}", res.cliente);
                    assunto = assunto.Replace("{telefone}", res.telefone);
                    assunto = assunto.Replace("{horario}", res.horario.Value.ToString("HH:mm"));
                    assunto = assunto.Replace("{horarioLimite}", res.horarioLimite.Value.ToString("HH:mm"));
                    assunto = assunto.Replace("{data}", res.data.Value.ToString("dd/MM/yyyy"));
                    assunto = assunto.Replace("{status}", Convert.ToString(res.status));
                    assunto = assunto.Replace("{dataInclusao}", Convert.ToString(res.dataInclusao));
                    assunto = assunto.Replace("{emailCliente}", res.emailCliente);
                    assunto = assunto.Replace("{observacao}", res.observacao);
                    assunto = assunto.Replace("{idEmpresa}", Convert.ToString(res.idEmpresa));
                    assunto = assunto.Replace("{idFilial}", Convert.ToString(res.idFilial));
                    assunto = assunto.Replace("{dataAlteracao}", Convert.ToString(res.dataAlteracao));
                    assunto = assunto.Replace("{identificador}", res.identificador);

                    Email.CriaEmail(mensagem, assunto, res.emailCliente);
                    
                    msg = "Reserva cadastrada com sucesso.";
                    txt_cliente.Text = "";
                    txt_telefone.Text = "";
                    txt_email.Text = "";
                    txt_data.Text = "";
                    txt_horario.Text = "";
                    //cmb_qtd.Value = "";
                    TxaObs.Text = "";
                    txtCaptcha.Text = "";
                }
                else
                {
                    msg = "Erro ao cadastrar reserva, por favor confira todos os campos.";
                }
            }
        }
        catch (Exception ex)
        {
            Excecao.TrataExcecao(ex);
            msg = "Erro ao executar tarefa.";
        }

        X.Msg.Alert("Alerta", msg).Show();

    }

    //Função para limpar campos do formulário
    protected void excluir(object sender, DirectEventArgs e)
    {
            txt_cliente.Text = "";
            txt_telefone.Text = "";
            txt_email.Text = "";
            txt_data.Text = "";
            txt_horario.Text = "";
            //cmb_qtd.Value = "";
            TxaObs.Text = "";
            txtCaptcha.Text = "";
        
        }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        
    <title>..:: Reservas Por Clientes ::..</title>

    <%--Código CSS--%>
    <style type="text/css">
        body
        {
            padding: 0;
            margin: 0;
            text-align: center;
            background: url("Img/imagemFundo.jpg")
        }
        .banner
        {
            background: url("Img/banner.png") no-repeat;
            margin: 16px auto;
            width: 1366px;
            height: 233px;
        }
        
        .div_form
        {
            background-color: #FFFFFF;
            height: 350px;
            margin: -33px -183px auto;
            left: 50%;
            padding: 20px;
            position: absolute;
            width: 355px;
            z-index: -1;
            
            /* arredondar bordas */
            -moz-border-radius: 0px 0px 5px 5px; /* Para Firefox */ 
            -webkit-border-radius: 0px 0px 5px 5px;  /* Para Safari e Chrome; */
            border-radius: 0px 0px 5px 5px; /* Para Opera 10.5+ */
        }
        
        .div_form div 
        {
            border: none;    
        }
        
        .div_botao
        {
            left: 404px;
            position: relative;
            top: 345px;
            width: 358px;
            height: 0px;
        }
        
        .div_botao div
        {
            border: none;             
        }
               
        .labelTitulo
        {
            border-bottom: 8px solid #3D5270;
            color: #FFFFFF;
            display: block;
            font-family: Arial;
            font-size: 25pt;
            margin: 113px 132px auto;
            padding: 0;
            position: absolute;
        }

        .x-italic
        {
            font-size: 17px;
            font-family: Arial,tahoma, Helvetica, sans-serif;
            color: #cc0000;
        }
        
    </style>
    
</head>
<body>


    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
            

                            <%-- painel do banner --%>
                            <ext:Panel runat="server" ID="panel_menu" BaseCls="banner" >
                                <Items>                                    
                                    <ext:Label ID="Label1" Cls="labelTitulo" runat="server" Text="Reservas On-line" />                                       
                                </Items>
                            </ext:Panel>
                                    
                          
                       
                            <%-- Campos do formulário --%>
                            <ext:Panel runat="server" ID="panel1" BaseCls="div_form" >  
                                <Items>
                                    
                                    <ext:Label ID="Label2" runat="server" Text="Informe seus dados para reserva:" Cls="x-italic" />
                   
                                    <%-- Nome do cliente --%>
                                    <ext:TextField 
                                        ID="txt_cliente" 
                                        runat="server" 
                                        FieldLabel="Nome" 
                                        AllowBlank="false"
                                        BlankText="Digite o nome do cliente."
                                        Text=""
                                        Width="350"
                                        />
                
                                    <%-- Telefone do cliente --%>
                                    <ext:TextField 
                                        ID="txt_telefone" 
                                        runat="server" 
                                        FieldLabel="Telefone" 
                                        AllowBlank="false" 
                                        BlankText="Digite o telefone do cliente."
                                        Text=""
                                        Width="220"
                                        >
                                        <Plugins>
                                                <ux:InputTextMask Mask="(99) 9999-9999">
                                                </ux:InputTextMask>
                                        </Plugins>
                                    </ext:TextField>

                                    <%-- Email do cliente --%>
                                    <ext:TextField 
                                        ID="txt_email" 
                                        runat="server" 
                                        FieldLabel="Email" 
                                        AllowBlank="false"
                                        BlankText="Digite o email seu email."
                                        Text=""
                                        Width="350"
                                        />
                    
                                    <%-- Data da reserva --%>
                                    <ext:DateField 
                                        ID="txt_data" 
                                        runat="server"
                                        Vtype="daterange"
                                        FieldLabel="Data"
                                        Format="dd/MM/yyyy"
                                        Width="200">  
                                            <Plugins>
                                                <ux:InputTextMask Mask="99/99/9999">
                                                </ux:InputTextMask>
                                        </Plugins>                        
                                    </ext:DateField>


                    
                                    <%-- Horário da reserva --%>
                                    <ext:TextField 
                                        ID="txt_horario" 
                                        runat="server" 
                                        FieldLabel="Horario" 
                                        AllowBlank="false" 
                                        BlankText="Digite o horário da reserva."
                                        Text=""
                                        Width="150"
                                        >
                                        <Plugins>
                                                <ux:InputTextMask Mask="99:99">
                                                </ux:InputTextMask>
                                        </Plugins>
                                    </ext:TextField>



                                    <%-- Quantidade de lugares --%>
                                    <ext:Button ID="Button1" runat="server" Text="Selecionar" Icon="Application" Width="200" FieldLabel="Mesas">
                                        <Listeners>
                                            <Click Handler="#{Window1}.show(this);" />
                                        </Listeners>
                                    </ext:Button>

                                    <%-- Observação da reserva --%>
                                    <ext:TextArea 
                                        ID="TxaObs"
                                        runat="server" 
                                        FieldLabel="Observação" 
                                        Width="350" 
                                     />

                                    <ext:Image runat="server" FieldLabel="" ID="imgCaptcha" ImageUrl="Captcha.aspx" Width="100px" Height="40px" Style="margin: 10px"/>
                                    
                                    <%-- campo para digitar o "captcha" --%>
                                    <ext:TextField 
                                        ID="txtCaptcha" 
                                        runat="server" 
                                        FieldLabel="Digite o número acima" 
                                        AllowBlank="false"
                                        BlankText="código captcha"
                                        Text=""
                                        Width="230"
                                        />
                                </Items>
                                    
                                    <%-- Botões --%>
                                    <Buttons>

                                        <ext:Button ID="enviar" runat="server" Text="Enviar" CommandName="enviar" ondirectclick="formReserva_acao" />
                                        <ext:Button ID="cancelar" runat="server"  Text="cancelar" CommandName="cancelar" ondirectclick="excluir"/>
 
                                    </Buttons>
                          </ext:Panel>

        <%-- window das mesas --%>
        <ext:Window 
            ID="Window1" 
            runat="server" 
            Title="Mesas"  
            Icon="Application"
            Height="450px" 
            Width="850px"
            Padding="0"
            Collapsible="false" 
            Hidden="true"
            Modal="true"
            AutoScroll="false">
            <AutoLoad Url="Mesas.aspx" Mode="IFrame">
            </AutoLoad>
        </ext:Window>

    </form>
</body>
</html>