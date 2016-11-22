<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Artebit.Restaurante.Reserva.Home" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Artebit.Restaurante.Global.Modelo" %>
<%@ Import Namespace="Artebit.Restaurante.Global.RegrasNegocio.Global" %>
<%@ Import Namespace="Artebit.Restaurante.Global.RegrasNegocio.Reserva" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.UX" Namespace="Ext.Net.UX" TagPrefix="ux" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>..:: Reservas ::..</title>


<script runat="server">
    
    protected void Page_Load(object sender, EventArgs e)
    {
        MemoriaWeb.ValidaSessao();
        
        carregaRgridMesa();
    }

    
    
    protected void Atualiza(object sender, DirectEventArgs e)
    {
        //this.lbMesas1.Text = DateTime.Now.ToString("HH:mm:ss");
    }
    
    
    
    // Função para carregar dados da gridpanel de Mesa
    protected void carregaRgridMesa()
    {
        using (Contexto.Atual = new Restaurante())
        {
            // Mesa
            Mesa mesa = new Mesa();
            GMESA mes = new GMESA();
            RRESERVA res = new RRESERVA();
            List<string> compl = new List<string>();

            IQueryable<GMESA> lista_mesa = (IQueryable<GMESA>) mesa.ExecutaFuncao(mes, Funcoes.BuscarLista, compl, res);
            var dados = from a in lista_mesa
                        select new
                                   {
                                       nuMesa = a.nuMesa,
                                       status = a.GSTATMESA.descricao,
                                       statusid = a.idStatus,
                                       qtdLugares = a.RGRUPOMESA.descricao,
                                       observacao = a.observacao,
                                       icone =
                            a.idStatus == 3
                                ? "img/mesaS2.png" //reservada
                                : a.idStatus == 1
                                      ? "img/mesaS4.png" //ocupada
                                      : a.idStatus == 2
                                            ? "img/mesaS1.png" //livre
                                            : "img/mesaS3.png" //bloqueada

                                   };

            lbQtdMesa1.Text = dados.Count().ToString(); //total de mesas

            lbQtdMesa2.Text = dados.Where(r => r.statusid == 1).Count().ToString(); //total mesas ocupadas
            lbQtdMesa3.Text = dados.Where(r => r.statusid == 2).Count().ToString(); //total mesas livres
            lbQtdMesa4.Text = dados.Where(r => r.statusid == 3).Count().ToString(); //total mesas reservadas
            lbQtdMesa5.Text = dados.Where(r => r.statusid == 4).Count().ToString(); //total mesas bloqueadas

            store_mesa.DataSource = dados;
            store_mesa.DataBind();
        }
    }



    [DirectMethod]
    public void nova_reserva(int nuMesa)
    {
        using (Contexto.Atual = new Restaurante())
        {
            int idmesa = nuMesa;

            Mesa m = new Mesa();
            GMESA g = new GMESA();
            g.nuMesa = idmesa;


            g = (GMESA)m.ExecutaFuncao(g, Funcoes.Buscar, null, null);

            if (g.idStatus == 2)//livre
            {
                RParametros parametro = new RParametros();
                RPARAM param = new RPARAM();

                param = (RPARAM)parametro.ExecutaFuncao(param, Funcoes.Buscar, null);


                txt_cliente.Text = "";
                txt_telefone.Text = "";
                txt_data.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txt_horario.Text = "";
                txt_horariolimite.Text = param.horFinReserva;
                hd_id.Value = "";
                hd_tipo.Value = "1";

                Window1.Title = "Nova reserva";
                Window1.Icon = Icon.ApplicationFormAdd;
                btn_reserva.Text = "Cadastrar";
                btn_reserva.Icon = Icon.ApplicationAdd;
                Window1.Show();

                carregaRgridmesawin();

                RowSelectionModel sm = this.grid_mesa.SelectionModel.Primary as RowSelectionModel;
                sm.ClearSelections();
                sm.SelectedRows.Add(new SelectedRow(Convert.ToString(idmesa)));
                sm.UpdateSelection();
            }
        }
    }



    // Função para carregar dados da gridpanel de Mesa
    protected void carregaRgridmesawin()
    {
        using (Contexto.Atual = new Restaurante())
        {
            // Mesa
            Mesa mesa = new Mesa();
            GMESA mes = new GMESA();
            List<string> compl = new List<string>();
            mes.idStatus = 2;

            IQueryable<GMESA> lista_mesa =
                (IQueryable<GMESA>)mesa.ExecutaFuncao(mes, Funcoes.BuscarListaEspecifica, compl, null);
            var dados = from a in lista_mesa
                        select new
                        {
                            nuMesa = a.nuMesa,
                            status = a.GSTATMESA.descricao,
                            qtdLugares = a.RGRUPOMESA.descricao,
                            observacao = a.observacao,
                        };
            store_mesa_win.DataSource = dados;
            store_mesa_win.DataBind();
        }
    }




    // Função para window de reserva
    protected void gridreserva_acao(object sender, DirectEventArgs e)
    {
        using (Contexto.Atual = new Restaurante())
        {
            string msg = "";
            try
            {

                Reserva reserva = new Reserva();
                RRESERVA res = new RRESERVA();
                List<string> compl = new List<string>();

                RowSelectionModel sm = this.grid_mesa.SelectionModel.Primary as RowSelectionModel;

                foreach (SelectedRow row in sm.SelectedRows)
                {
                    GMESA msa = new GMESA();
                    Mesa mesa = new Mesa();

                    int id = Convert.ToInt32(row.RecordID);
                    msa.nuMesa = id;

                    msa = (GMESA) mesa.ExecutaFuncao(msa, Funcoes.Buscar, compl, null);

                    // Atualizar ou Adicionar
                    msa.idStatus = 3;
                    RRESMESA mesaReserva = new RRESMESA();
                    mesaReserva.idEmpresa = msa.idEmpresa;
                    mesaReserva.idFilial = msa.idFilial;
                    mesaReserva.nuMesa = msa.nuMesa;

                    //mesaReserva.idReserva = 10;

                    res.RRESMESA.Add(mesaReserva);
                }

                string horario = txt_data.Text.Replace("00:00:00", txt_horario.Text);
                string horariolimite = txt_data.Text.Replace("00:00:00", txt_horariolimite.Text);

                res.cliente = txt_cliente.Text;
                res.telefone = txt_telefone.Text;
                res.data = Convert.ToDateTime(txt_data.Text);
                res.horario = Convert.ToDateTime(horario);
                res.horarioLimite = Convert.ToDateTime(horariolimite);
                res.status = 5;

                if (res.horario <= res.horarioLimite)
                {
                    msg = (string) reserva.ExecutaFuncao(res, Funcoes.ConfereDados, compl);
                }
                else
                {
                    msg = "Horário deve ser menor que horário limite.";
                }

                if (msg == "")
                {
                    bool conf = false;
                    // Adicionar                  
                    conf = (bool) reserva.ExecutaFuncao(res, Funcoes.Adicionar, compl);

                    if (conf)
                    {
                        msg = "Reserva cadastrada com sucesso.";
                        Window1.Hide();
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
            //grid_mesa1.Reload();}
        }
    }



</script>


 <script type="text/javascript">
        var selectionChanged = function (dv, nodes) {
            if (nodes.length > 0) {
                var id = nodes[0].id;
                Ext.net.DirectMethods.nova_reserva(id);
                //Ext.Msg.alert("Click", "The node with id='" + id + "' has been clicked");
            }
        }
 </script>

    <style type="text/css">
        div.item-wrap
        {
            float: left;
            border: 1px solid transparent;
            margin: 10px 10px 10px 10px;
            width: 100px;
            cursor: pointer;
            height: 70px;
            text-align: center;
        }
        
        div.item-wrap img
        {
            margin: 5px 5px 5px 5px;
        }
        
        div.item-wrap h6
        {
            font-size: 14px;
            color: #3A4B5B;
            font-family: tahoma,arial,san-serif;
        }
        
        .items-view .x-view-over
        {
            border: solid 1px silver;
        }
        
        .x-view-over
        {
            background-color: #EEEEEE;
            border: solid 1px silver;
        }
        
        #items-ct
        {
            padding: 0px 30px 24px 30px;
        }
        
        #items-ct h2
        {
            border-bottom: 2px solid #3A4B5B;
            cursor: pointer;
        }
        
        #items-ct h2 div
        {
            background: transparent url(resources/images/group-expand-sprite.gif) no-repeat 3px -47px;
            padding: 4px 4px 4px 17px;
            font-family: tahoma,arial,san-serif;
            font-size: 12px;
            color: #3A4B5B;
        }
        
        #items-ct .collapsed h2 div
        {
            background-position: 3px 3px;
        }
        #items-ct dl
        {
            margin-left: 2px;
        }
        #items-ct .collapsed dl
        {
            display: none;
        }
        
        .tabelaDados table
        {
            width: 100%;
            font-family: Calibri,Arial;
        }
        
        .tabelaDados td
        {
            padding: 4px;
        }
        
        .tdTitle
        {
            background: url(img/fundo1.jpg);
            color: White;
            text-align: center;
        }
        
        .tdGeral1
        {
            text-align: center;
        }
        
        .tdGeral2
        {
            text-align: left;
            padding: 5px;
        }
    </style>


</head>
<body>
    <form id="Form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <%-- Store é o datasource, o resultado dos selects --%>
    <%-- Store da lista de mesas --%>
    <ext:Store ID="store_mesa" runat="server">
        <Reader>
            <ext:JsonReader IDProperty="nuMesa">
                <Fields>
                    <ext:RecordField Name="nuMesa" Type="Int" />
                    <ext:RecordField Name="status" Type="String" />
                    <ext:RecordField Name="qtdLugares" Type="String" />
                    <ext:RecordField Name="observacao" Type="String" />
                    <ext:RecordField Name="icone" Type="String" />
                </Fields>
            </ext:JsonReader>
        </Reader>
    </ext:Store>


    <%-- Store da lista de mesas da window --%>
        <ext:Store ID="store_mesa_win" runat="server">
            <Reader>
                <ext:JsonReader IDProperty="nuMesa">
                    <Fields>
                        <ext:RecordField Name="nuMesa" Type="Int" />
                        <ext:RecordField Name="idStatus" Type="Int" />
                        <ext:RecordField Name="status" Type="String" />
                        <ext:RecordField Name="qtdLugares" Type="String" />
                        <ext:RecordField Name="observacao" Type="String" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <SortInfo Field="nuMesa" Direction="ASC" />
        </ext:Store>



    <ext:Viewport ID="Viewport1" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="Panel2" runat="server" Region="East" Collapsible="true" Split="true"
                MinWidth="260" Width="260" Layout="AnchorLayout">
                <Items>
                    <ext:Panel ID="Panel3" runat="server"  Border="false" Padding="6">
                        <Items>
                            <ext:TableLayout ID="tabela1" runat="server" Columns="2" AnchorHorizontal="100%"
                                Cls="tabelaDados">
                                <Cells>
                                    <ext:Cell ColSpan="2" CellCls="tdTitle">
                                         <ext:Label runat="server" ID="lbData" Text="" AnchorHorizontal="100%">
                                        </ext:Label>
                                    </ext:Cell>
                                    <ext:Cell>
                                        <ext:Label runat="server" ID="lbMesa1" Text="Total Mesas:" AnchorHorizontal="100%">
                                        </ext:Label>
                                    </ext:Cell>
                                    <ext:Cell>
                                        <ext:Label runat="server" ID="lbQtdMesa1" Text="5" AnchorHorizontal="100%">
                                        </ext:Label>
                                    </ext:Cell>
                                     <ext:Cell>
                                        <ext:Label runat="server" ID="lbMesa2" Text="Mesas Ocupadas:" AnchorHorizontal="100%">
                                        </ext:Label>
                                    </ext:Cell>
                                    <ext:Cell>
                                        <ext:Label runat="server" ID="lbQtdMesa2" Text="5" AnchorHorizontal="100%">
                                        </ext:Label>
                                    </ext:Cell>
                                    <ext:Cell>
                                        <ext:Label runat="server" ID="lbMesa3" Text="Mesas Livres:" AnchorHorizontal="100%">
                                        </ext:Label>
                                    </ext:Cell>
                                    <ext:Cell>
                                        <ext:Label runat="server" ID="lbQtdMesa3" Text="5" AnchorHorizontal="100%">
                                        </ext:Label>
                                    </ext:Cell>
                                    <ext:Cell>
                                        <ext:Label runat="server" ID="lbMesa4" Text="Mesas Reservadas:" AnchorHorizontal="100%">
                                        </ext:Label>
                                    </ext:Cell>
                                    <ext:Cell>
                                        <ext:Label runat="server" ID="lbQtdMesa4" Text="5" AnchorHorizontal="100%">
                                        </ext:Label>
                                    </ext:Cell>
                                    <ext:Cell>
                                        <ext:Label runat="server" ID="lbMesa5" Text="Mesas Bloqueadas:" AnchorHorizontal="100%">
                                        </ext:Label>
                                    </ext:Cell>
                                    <ext:Cell>
                                        <ext:Label runat="server" ID="lbQtdMesa5" Text="5" AnchorHorizontal="100%">
                                        </ext:Label>
                                    </ext:Cell>
                                </Cells>
                            </ext:TableLayout>
                        </Items>
                    </ext:Panel>
                    <ext:Panel ID="Panel4" runat="server" Border="false" Padding="6">
                        <Items>
                            <ext:TableLayout ID="TableLayout1" runat="server" Columns="2" AnchorHorizontal="100%"
                                Cls="tabelaDados">
                                <Cells>
                                    <ext:Cell ColSpan="2" CellCls="tdTitle">
                                        <ext:Label runat="server" ID="Label14" Text="Legenda" AnchorHorizontal="100%">
                                        </ext:Label>
                                    </ext:Cell>
                                    <ext:Cell CellCls="tdGeral1">
                                        <ext:Image ID="Image1" runat="server" Align="Middle" ImageUrl="~/img/mesaS1.png"
                                            Width="40" Height="24">
                                        </ext:Image>
                                    </ext:Cell>
                                    <ext:Cell CellCls="tdGeral2">
                                        <ext:Label runat="server" ID="Label15" Text="Livre">
                                        </ext:Label>
                                    </ext:Cell>
                                    <ext:Cell CellCls="tdGeral1">
                                        <ext:Image ID="Image2" runat="server" Align="Middle" ImageUrl="~/img/mesaS2.png"
                                            Width="40" Height="24">
                                        </ext:Image>
                                    </ext:Cell>
                                    <ext:Cell CellCls="tdGeral2">
                                        <ext:Label runat="server" ID="Label13" Text="Reservada">
                                        </ext:Label>
                                    </ext:Cell>
                                    <ext:Cell CellCls="tdGeral1">
                                        <ext:Image ID="Image3" runat="server" Align="Middle" ImageUrl="~/img/mesaS3.png"
                                            Width="40" Height="24">
                                        </ext:Image>
                                    </ext:Cell>
                                    <ext:Cell CellCls="tdGeral2">
                                        <ext:Label runat="server" ID="Label16" Text="Bloqueada">
                                        </ext:Label>
                                    </ext:Cell>
                                    <ext:Cell CellCls="tdGeral1">
                                        <ext:Image ID="Image4" runat="server" Align="Middle" ImageUrl="~/img/mesaS4.png"
                                            Width="40" Height="24">
                                        </ext:Image>
                                    </ext:Cell>
                                    <ext:Cell CellCls="tdGeral2">
                                        <ext:Label runat="server" ID="Label17" Text="Ocupada">
                                        </ext:Label>
                                    </ext:Cell>
                                </Cells>
                            </ext:TableLayout>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:Panel ID="Panel1" runat="server" Region="Center" Title="Mapa de Mesas" Border="false"
                Padding="6" AutoScroll="true" >
                <Items>
                    <ext:DataView ID="Dashboard"
                        runat="server" 
                        StripeRows="true"
                        Frame="false"
                        Collapsible="false"
                        AnimCollapse="false"
                        TrackMouseOver="true" 
                        StoreID="store_mesa" 
                        OverClass="x-view-over" 
                        ItemSelector="div.item-wrap" 
                        SingleSelect="true"
                        AnchorVertical="100%" >
                        <Template ID="Template1" runat="server">
                            <Html>
                                <div id="items-ct">
								<tpl for=".">
												<div id="{nuMesa}" class="item-wrap">
													<img src="{icone}"/>
													<div>
														<H6>{nuMesa}</H6>                                                    
													</div>
												</div>

								</tpl>
                                <div style="clear:left"></div>
							</div>
                            </Html>
                        </Template>
                        <Listeners>
                            <SelectionChange Fn="selectionChanged" />
                        </Listeners>
                    </ext:DataView>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Viewport>




    <%-- window para adicionar nova reserva --%>
        <ext:Window 
            ID="Window1" 
            runat="server" 
            Closable="false"
            Resizable="false"
            Height="350" 
            Icon="Add" 
            Title="Nova reserva"
            Draggable="false"
            Width="550"
            Modal="true"
            Padding="5"
            Hidden="True"
            Layout="Form">
            <Items>
                <%-- ID da reserva --%>
                <ext:Hidden runat="server" ID="hd_id" />
                
                <%-- Tipo de ação a ser executada, 1=adicionar, 2=atualizar, 3=atender --%>
                <ext:Hidden runat="server" ID="hd_tipo" />

                <%-- Nome do cliente --%>
                <ext:TextField 
                    ID="txt_cliente" 
                    runat="server" 
                    FieldLabel="Nome cliente" 
                    AllowBlank="false"
                    BlankText="Digite o nome do cliente."
                    Text=""
                    AnchorHorizontal="90%"
                    />
                
                <%-- Telefone do cliente --%>
                <ext:TextField 
                    ID="txt_telefone" 
                    runat="server" 
                    FieldLabel="Telefone" 
                    AllowBlank="false" 
                    BlankText="Digite o telefone do cliente."
                    Text=""
                    AnchorHorizontal="50%"
                    >
                    <Plugins>
                         <ux:InputTextMask Mask="(99) 9999-9999">
                         </ux:InputTextMask>
                    </Plugins>
                </ext:TextField>
                    
                <%-- Data --%>
                <ext:DateField 
                    ID="txt_data" 
                    runat="server"
                    Vtype="daterange"
                    FieldLabel="Data"
                    Format="dd/MM/yyyy"
                    AnchorHorizontal="40%">                          
                </ext:DateField>
                    
                <%-- Horário --%>
                <ext:TextField 
                    ID="txt_horario" 
                    runat="server" 
                    FieldLabel="Horario" 
                    AllowBlank="false" 
                    BlankText="Digite o horário da reserva."
                    Text=""
                    AnchorHorizontal="30%"
                    >
                    <Plugins>
                         <ux:InputTextMask Mask="99:99">
                         </ux:InputTextMask>
                    </Plugins>
                </ext:TextField>
                    
                <%-- Horário limite --%>
                <ext:TextField 
                    ID="txt_horariolimite" 
                    runat="server" 
                    FieldLabel="Horário limite" 
                    AllowBlank="false" 
                    BlankText="Digite o horário limite da reserva."
                    Text=""
                    AnchorHorizontal="30%"
                    >
                    <Plugins>
                         <ux:InputTextMask Mask="99:99">
                         </ux:InputTextMask>
                    </Plugins>
                </ext:TextField>
                
                <ext:Button ID="btn_winmesa" runat="server" Text="Mesas" Icon="FolderTable">
                    <Listeners>
                        <Click Handler="#{Window2}.show();" />
                    </Listeners>
                </ext:Button>

            </Items>
            <Buttons>
                <ext:Button ID="btn_reserva" runat="server" Text="Adicionar" Icon="Add">
                    <DirectEvents>
                        <Click OnEvent="gridreserva_acao">
                            <EventMask ShowMask="true" Msg="Verificando..." MinDelay="500" />
                        </Click>
                    </DirectEvents>
                </ext:Button>

                <ext:Button ID="btnCancel" runat="server" Text="Cancelar" Icon="Decline">
                    <Listeners>
                        <Click Handler="#{Window1}.hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
        
        <%-- window para escolher mesas --%>
        <ext:Window 
            ID="Window2" 
            runat="server" 
            Closable="false"
            Resizable="false"
            Height="350" 
            Icon="Add" 
            Title="Mesas"
            Draggable="false"
            Width="550"
            Modal="true"
            Padding="5"
            Hidden="True"
            Layout="Form">
            <Items>
                
                <ext:GridPanel 
                    ID="grid_mesa" 
                    runat="server" 
                    Height="280"
                    Frame="true"
                    StoreID="store_mesa_win"
                    StripeRows="true"
                    Title="Mesas"
                    Collapsible="true"
                    AnimCollapse="false"
                    Icon="Door"
                    TrackMouseOver="false"
                    >
                        <ColumnModel ID="ColumnModel2" runat="server">
                        <Columns>
                            <ext:NumberColumn DataIndex="nuMesa" Header="Mesa" Format="999" Width="50" />
                            <ext:Column DataIndex="qtdLugares" Header="Lugares" Width="100" />
                            <ext:Column DataIndex="observacao" Header="Observação" Width="300" />
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" />
                    <Plugins>
                        <ext:GridFilters runat="server" ID="filtro" Local="true">
                            <Filters>
                                <ext:NumericFilter DataIndex="mesa" />
                                <ext:StringFilter DataIndex="qtd" />
                                <ext:StringFilter DataIndex="observacao" />
                            </Filters>
                        </ext:GridFilters>
                    </Plugins>
                    <SelectionModel>
                        <ext:CheckboxSelectionModel ID="check" runat="server" RowSpan="2" />
                    </SelectionModel>
                </ext:GridPanel>

            </Items>

            <Buttons>
                <ext:Button ID="Button1" runat="server" Text="Escolher mesa(s)" Icon="Add">
                    <Listeners>
                        <Click Handler="#{Window2}.hide();" />
                    </Listeners>
                </ext:Button>
                <ext:Button ID="Button3" runat="server" Text="Cancelar" Icon="Decline">
                    <Listeners>
                        <Click Handler="#{Window2}.hide();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>



    <ext:TaskManager ID="TaskManager1" runat="server" Enabled="true">
        <Tasks>
            <ext:Task>
                <Listeners>
                    <Update Handler="#{lbData}.setText(new Date().dateFormat('d/m/y H:i:s'));" />
                </Listeners>
            </ext:Task>

            <ext:Task Interval="15000" >
                <DirectEvents>
                        <Update OnEvent="Atualiza">
                            <EventMask 
                                ShowMask="false" 
                                Target="Page"
                                MinDelay="350"
                                />
                        </Update>
                </DirectEvents>            
                </ext:Task>
        </Tasks>
    </ext:TaskManager>
    </form>
</body>
</html>
