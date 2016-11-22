<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mesa.aspx.cs" Inherits="Artebit.Restaurante.Reserva.Mesa" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Artebit.Restaurante.Global.Modelo" %>
<%@ Import Namespace="Artebit.Restaurante.Global.RegrasNegocio.Global" %>
<%@ Import Namespace="Artebit.Restaurante.Global.RegrasNegocio.Reserva" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.UX" Namespace="Ext.Net.UX" TagPrefix="ux" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        MemoriaWeb.ValidaSessao();
        
        carregaRgridmesa();
    }

    protected void carregar_perfil()
    {
        using (Contexto.Atual = new Restaurante())
        {
            Perfil perfil = new Perfil();
            GPERFIL prf = new GPERFIL();
            prf.idPerfil = Convert.ToInt32(Memoria.Perfil);
            List<string> compl = new List<string>();
            compl[0] = "R";
            compl[1] = "Mesa";
            compl[2] = "1";
            bool conf = (bool) perfil.ExecutaFuncao(prf, Funcoes.Verificar, compl);
            if (conf)
                reserva.Value = "1";
        }
    }

    protected void Atualiza(object sender, DirectEventArgs e)
    {
        //this.lbMesas1.Text = DateTime.Now.ToString("HH:mm:ss");
    }
    
    
    
    // Função para carregar dados da gridpanel de Mesa
    protected void carregaRgridmesa()
    {
        using (Contexto.Atual = new Restaurante())
        {
            // Mesa
            Mesa mesa = new Mesa();
            GMESA mes = new GMESA();
            List<string> compl = new List<string>();


            IQueryable<GMESA> lista_mesa = (IQueryable<GMESA>) mesa.ExecutaFuncao(mes, Funcoes.BuscarLista, compl, null);
            var dados = from a in lista_mesa
                        select new
                                   {
                                       nuMesa = a.nuMesa,
                                       status = a.GSTATMESA.descricao,
                                       qtdLugares = a.RGRUPOMESA.descricao,
                                       observacao = a.observacao,
                                       idStatus = a.idStatus
                                   };
            store_mesa.DataSource = dados;
            store_mesa.DataBind();
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
                (IQueryable<GMESA>) mesa.ExecutaFuncao(mes, Funcoes.BuscarListaEspecifica, compl, null);
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
            grid_mesa1.Reload();
        }
    }

    
    // Função para abrir janela de nova reserva    
    protected void nova_reserva(object sender, DirectEventArgs e)
    {
        using (Contexto.Atual = new Restaurante())
        {
            int idmesa = Convert.ToInt32(e.ExtraParams["id"]);
            string comando = e.ExtraParams["command"];
            if (comando == "reserva")
            {
                RParametros parametro = new RParametros();
                RPARAM param = new RPARAM();

                param = (RPARAM) parametro.ExecutaFuncao(param, Funcoes.Buscar, null);


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
            else if (comando == "liberar")
            {
                Mesa mesa = new Mesa();
                GMESA msa = new GMESA();

                msa.nuMesa = idmesa;

                msa = (GMESA) mesa.ExecutaFuncao(msa, Funcoes.Buscar, null, null);
                msa.RRESMESA.Clear();
                msa.idStatus = 2;

                if ((bool) mesa.ExecutaFuncao(msa, Funcoes.Atualizar, null, null))
                {

                    X.Msg.Alert("Alerta", "Mesa liberada com sucesso").Show();
                    grid_mesa1.Reload();
                }
                else
                {
                    X.Msg.Alert("Alerta", "Erro ao liberar").Show();
                    grid_mesa1.Reload();
                }

            }
        }
    }
    
</script>

<script type="text/javascript">
    // Função que decide qual botão de ação irá aparecer dependendo do status
    var prepareCommand = function (grid, command, record, row) {
        if (command.command == 'reserva' && record.data.idStatus != 2) {
            command.hidden = true;
            command.hideMode = 'visibility';
        }
        if (command.command == 'reserva' && record.data.idStatus == 4) {
            command.hidden = true;
            command.hideMode = 'visibility';
        } else {

            if (document.getElementById("reserva").value == "1" && command.command == 'reserva') {
                command.hidden = true;
                command.hideMode = 'visibility';

            }
        }
        if (command.command == 'liberar' && (record.data.idStatus == 2 || record.data.idStatus == 4)) {
            command.hidden = true;
            command.hideMode = 'visibility';
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>..:: Reservas ::..</title>
    
   
    <style type="text/css">
        .x-grid3-cell-inner {
            font-family:"segoe ui",tahoma, arial, sans-serif;
        }
        
        .x-grid-group-hd div {
            font-family:"segoe ui",tahoma, arial, sans-serif;
        }
        
        .x-grid3-hd-inner {
            font-family:"segoe ui",tahoma, arial, sans-serif;
            font-size:12px;
        }
        
        .x-grid3-body .x-grid3-td-Cost {
            background-color:#f1f2f4;
        }
        
        .x-grid3-summary-row .x-grid3-td-Cost {
            background-color:#e1e2e4;
        }     
    </style>

</head>
<body>
 <form id="Form1" runat="server">
   <ext:ResourceManager ID="ResourceManager1" runat="server" />

    <input type="hidden" id="reserva" runat="server" />

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
                        <ext:RecordField Name="idStatus" Type="Int" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <SortInfo Field="nuMesa" Direction="ASC" />
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

        <ext:GridPanel 
            ID="grid_mesa1" 
            runat="server" 
            Frame="true"
            StoreID="store_mesa"
            StripeRows="true"
            Collapsible="false"
            AnimCollapse="false"
            TrackMouseOver="true"
            Height="560"
            AnchorHorizontal="100%"
            AutoDestroy="true"
            AutoWidth="true"
            AutoFocus="true"
            HideBorders="true"
            AutoExpandColumn="observacao"
            >
                <ColumnModel ID="ColumnModel1" runat="server">
                <Columns>
                    <ext:RowNumbererColumn />
                    <ext:Column Header="Mesa" DataIndex="nuMesa"  />
                    <ext:Column Header="Status" DataIndex="status"  />
                    <ext:Column Header="Quantidade de Lugares" DataIndex="qtdLugares" />
                    <ext:Column Header="Observação" DataIndex="observacao" Width="" />
                    <ext:NumberColumn Header="idStatus" DataIndex="idStatus" Hidden="true" />
                    <ext:ImageCommandColumn Width="170" Header="" DataIndex="comandos">
                        <Commands>
                            <ext:ImageCommand CommandName="reserva" Icon="Add" Text="Reservar">
                                <ToolTip Text="Reservar" />
                            </ext:ImageCommand>
                        </Commands>
                        <PrepareCommand Fn="prepareCommand" />
                    </ext:ImageCommandColumn>
                </Columns>
            </ColumnModel>
            <LoadMask ShowMask="true" />
        
            <Plugins>
                <ext:GridFilters runat="server" ID="GridFilters1" Local="true">
                    <Filters>
                        <ext:NumericFilter DataIndex="nuMesa" />
                        <ext:StringFilter DataIndex="status" />
                        <ext:StringFilter DataIndex="qtdLugares" />
                        <ext:StringFilter DataIndex="observacao" />
                    </Filters>
                </ext:GridFilters>
            </Plugins>
            <DirectEvents>
                <Command OnEvent="nova_reserva">                                       
                    <ExtraParams>
                        <ext:Parameter Name="id" Value="record.id" Mode="Raw" />
                        <ext:Parameter Name="command" Value="command" Mode="Raw"/>
                    </ExtraParams>
                </Command> 
                <RowDblClick OnEvent="nova_reserva">
                    <ExtraParams>
                        <ext:Parameter Name="id" Value="grid_mesa1.getRowsValues({ selectedOnly: true })[0].nuMesa" Mode="Raw" />
                        <ext:Parameter Name="command" Value="reserva" Mode="Value"/>
                    </ExtraParams>
                </RowDblClick>
            </DirectEvents>
             <SelectionModel>
                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server"/>
            </SelectionModel>
        </ext:GridPanel>
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

