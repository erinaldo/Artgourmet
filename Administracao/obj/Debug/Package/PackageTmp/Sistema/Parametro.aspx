<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Parametro.aspx.cs" Inherits="Artebit.Restaurante.Administracao.Sistema.Parametro" %>

<html>
<head runat="server">
    <title></title>
</head>


<style>

    fieldset
    {
        margin: 30px;
        padding: 20px;
    }
    
    fieldset div
    {
        border: none!important;
    }
    
    .div_email
    {
        width:350px;
        margin-right: 50px;
        float: left;
    }

    .div_obs
    {
        margin-right: 0px;
        width: 430px !important;
        color:Red;
    }
    
    iframe
    {
        width: 244px;
        height: 150px;
    }

</style>


<body>
    <form id="form1" runat="server">
    <div>

    <ext:ResourceManager ID="ResourceManager1" runat="server" />


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

        <center>
            <div style="position: relative; top: 20px;">
                <ext:Button ID="btSalvar" runat="server" Text="Salvar/Atualizar Parâmetros (todos)" Icon="CupEdit">
                    <DirectEvents>
                        <Click OnEvent="salvar">
                            <EventMask ShowMask="true" Msg="Verificando..." MinDelay="500" />
                        </Click>
                    </DirectEvents>
                </ext:Button>
            </div>
        </center>



        <fieldset>
            <legend>:: Reserva  ::</legend>

                <ext:Panel runat="server">

                    <Items>

                        <ext:TextField 
                            ID="r_horariolimite" 
                            runat="server" 
                            FieldLabel="Horário limite" 
                            AllowBlank="false"
                            BlankText="Digite o horário limite da reserva"
                            Text=""
                            Width="150"
                            >

                            </ext:TextField>

                        <ext:Panel runat="server" Cls="div_email">
                            <Items>

                                <ext:HtmlEditor 
                                    runat="server"
                                    ID="r_assuntoEmail1"
                                    FieldLabel="Assunto de email de confirmação"
                                    AllowBlank="true"
                                    BlankText="Digite o assunto de email de confirmação"
                                    Text=""
                                    Width="350" >
                                </ext:HtmlEditor>

                                <ext:HtmlEditor 
                                    runat="server"
                                    ID="r_corpoEmail1"
                                    FieldLabel="Corpo de email de confirmação"
                                    AllowBlank="true"
                                    BlankText="Digite o corpo de email de confirmação"
                                    Text=""
                                    Width="350" >
                                </ext:HtmlEditor>

                            </Items>
                        </ext:Panel>

                        <ext:Panel runat="server" Cls="div_email">
                            <Items>

                                <ext:HtmlEditor 
                                    runat="server"
                                    ID="r_assuntoEmail2"
                                    FieldLabel="Assunto de email de análise"
                                    AllowBlank="true"
                                    BlankText="Digite o assunto de email de análise"
                                    Text=""
                                    Width="350" >
                                </ext:HtmlEditor>

                                <ext:HtmlEditor 
                                    ID="r_corpoEmail2"
                                    FieldLabel="Corpo de email de análise"
                                    AllowBlank="true"
                                    BlankText="Digite o corpo de email de análise"
                                    Width="350"
                                    runat="server"
                                    >
                                </ext:HtmlEditor>
                                </Items>
                        </ext:Panel>


                        <ext:Panel runat="server" Cls="div_email div_obs">
                            <Items>
                                <ext:DisplayField ID="DisplayField1" runat="server">
                                    <Content>
                                    Na criação ou edição do modelo do email, para utilizar dados cadastrados, como nome do cliente, utilize "{}" antes do atributo.
                                    <br /><br />
                                    Ex.: "{nome}, Sua reserva foi confirmada para o dia {data} às {horario}"
                                        
                                    <br /><br />{nome} => Nome do cliente
                                    <br />{telefone} => Telefone do cliente
                                    <br />{horario} => Horário da reserva
                                    <br />{horarioLimite} => Horário limite da reserva
                                    <br />{data} => Data da reserva
                                    <br />{emailCliente} => Email do cliente
                                    <br />{observacao} => Observação do cliente
                                    <br />{identificador} => Identificador do cliente

                                    </Content>
                                </ext:DisplayField>

                            </Items>
                        </ext:Panel>

                    </Items>
                </ext:Panel>

        </fieldset>

    </div>


    </form>
</body>
</html>
