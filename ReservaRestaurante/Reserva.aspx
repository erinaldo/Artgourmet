<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reserva.aspx.cs" Inherits="Artebit.Restaurante.Reserva.Reserva" %>

<%@ Import Namespace="Artebit.Restaurante.Global.Modelo" %>
<%@ Import Namespace="Artebit.Restaurante.Global.RegrasNegocio.Global" %>
<%@ Import Namespace="Artebit.Restaurante.Global.RegrasNegocio.Reserva" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="Ext.Net.UX" Namespace="Ext.Net.UX" TagPrefix="ux" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        MemoriaWeb.ValidaSessao();     
        
        carregaRgridreserva();

        if (!IsPostBack)
        {
           DateFilter df = (DateFilter)GridFilters1.Filters[4];

            df.OnValue = DateTime.Now;
        }
        //carregar_perfil();        
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
            compl[1] = "Reserva";


            compl[2] = "1";
            grid_reserva.TopBar.Toolbar.Visible = (bool) perfil.ExecutaFuncao(prf, Funcoes.Verificar, compl);

            compl[2] = "2";
            bool conf = (bool) perfil.ExecutaFuncao(prf, Funcoes.Verificar, compl);
            if (conf)
                atualiza.Value = "1";

            compl[2] = "3";
            conf = (bool) perfil.ExecutaFuncao(prf, Funcoes.Verificar, compl);
            if (conf)
                cancela.Value = "1";

            compl[2] = "4";
            conf = (bool) perfil.ExecutaFuncao(prf, Funcoes.Verificar, compl);
            if (conf)
                atende.Value = "1";

            compl[2] = "5";
            conf = (bool) perfil.ExecutaFuncao(prf, Funcoes.Verificar, compl);
            if (conf)
                aprova.Value = "1";
        }
    }
    


    protected void Atualiza(object sender, DirectEventArgs e)
    {
        //this.lbMesas1.Text = DateTime.Now.ToString("HH:mm:ss");
    }
    
    
    
    // Função para carregar dados da gridpanel de mesa da grid de reseva
    protected void carregaRgridmesareserva(RRESERVA obj)
    {
        using (Contexto.Atual = new Restaurante())
        {
            // Mesa
            Mesa mesa = new Mesa();
            GMESA mes = new GMESA();
            List<string> compl = new List<string>();

            IQueryable<GMESA> lista_mesa_reserva =
                (IQueryable<GMESA>) mesa.ExecutaFuncao(mes, Funcoes.BuscarListaPorReserva, compl, obj);

            var dados = from a in lista_mesa_reserva
                        select new
                                   {
                                       mesa = a.nuMesa,
                                       observacao = a.observacao,
                                       qtd = a.RGRUPOMESA.descricao,
                                       status = a.GSTATMESA.descricao,
                                       idqtd = a.qtdLugares,
                                       idStatus = a.idStatus
                                   };

            store_mesa.DataSource = dados;
            store_mesa.DataBind();

        }
        Window3.Show();

    }
    // ==================================================================================================
    
    
    
    // Função para carregar dados da gridpanel de mesa
    protected void carregaRgridmesa(RRESERVA obj)
    {
        using (Contexto.Atual = new Restaurante())
        {
            // Mesa
            Mesa mesa = new Mesa();
            GMESA mes = new GMESA();
            List<string> compl = new List<string>();
            mes.idStatus = 2;

            IQueryable<GMESA> lista_mesa =
                (IQueryable<GMESA>) mesa.ExecutaFuncao(mes, Funcoes.BuscarListaEspecifica, compl, obj);
            IQueryable<GMESA> lista_mesa_reserva =
                (IQueryable<GMESA>) mesa.ExecutaFuncao(mes, Funcoes.BuscarListaPorReserva, compl, obj);

            var lista_mesa_final = lista_mesa;

            if (obj != null)
            {
                lista_mesa_final = lista_mesa.Union(lista_mesa_reserva);
            }

            var dados = from a in lista_mesa_final
                        select new
                                   {
                                       mesa = a.nuMesa,
                                       observacao = a.observacao,
                                       qtd = a.RGRUPOMESA.descricao,
                                       status = a.GSTATMESA.descricao,
                                       idqtd = a.qtdLugares,
                                       idStatus = a.idStatus
                                   };

            store_mesa.DataSource = dados;
            store_mesa.DataBind();
        }
    }
    // ==================================================================================================
    
    

    // Função para carregar dados da gridpanel de reserva
    protected void carregaRgridreserva()
    {
        using (Contexto.Atual = new Restaurante())
        {
            // Reserva
            Reserva reserva = new Reserva();
            RRESERVA res = new RRESERVA();
            List<string> compl = new List<string>();

            IQueryable<RRESERVA> lista_reserva =
                (IQueryable<RRESERVA>) reserva.ExecutaFuncao(res, Funcoes.BuscarLista, compl);
            var dados = from a in lista_reserva
                        select new
                                   {
                                       cliente = a.cliente,
                                       data = a.data,
                                       qtd = a.RGRUPOMESA.descricao,
                                       status = a.RSTATRESERVA.descricao,
                                       idqtd = a.qtdLugares,
                                       idStatus = a.status,
                                       telefone = a.telefone,
                                       idReserva = a.idReserva,
                                       observacao = a.observacao,
                                       horario = a.horario,
                                       horariolimite = a.horarioLimite
                                   };
            store_reserva.DataSource = dados;
            store_reserva.DataBind();

        }
    }
    // ==================================================================================================
    
    
    
    // Função para window de reserva
    protected void funcao_reserva(object sender, DirectEventArgs e)
    {
        using (Contexto.Atual = new Restaurante())
        {
            string msg = "";

            try
            {
                Reserva reserva = new Reserva();
                RRESERVA res = new RRESERVA();
                List<string> compl = new List<string>();


                // Se não for adicionar
                if (hd_tipo.Value.ToString() != "1")
                {
                    res.idReserva = Convert.ToInt32(hd_id.Value);
                    res = (RRESERVA) reserva.ExecutaFuncao(res, Funcoes.BuscarVazio, compl);

                }

                RowSelectionModel sm = this.grid_mesa.SelectionModel.Primary as RowSelectionModel;

                ACONTA conta = new ACONTA();
                PreConta cconta = new PreConta();
                int cont = 0;


                foreach (SelectedRow row in sm.SelectedRows)
                {
                    GMESA msa = new GMESA();
                    Mesa mesa = new Mesa();

                    int id = Convert.ToInt32(row.RecordID);
                    msa.nuMesa = id;

                    msa = (GMESA) mesa.ExecutaFuncao(msa, Funcoes.Buscar, compl, null);

                    if (hd_tipo.Value.ToString() == "2" || hd_tipo.Value.ToString() == "1")
                    {
                        // Atualizar ou Adicionar
                        msa.idStatus = 3;
                    }
                    else
                    {
                        if (hd_tipo.Value.ToString() == "3")
                        {
                            // Atender
                            msa.idStatus = 1;
                        }
                    }

                    RRESMESA mesaReserva = new RRESMESA();
                    mesaReserva.idEmpresa = msa.idEmpresa;
                    mesaReserva.idFilial = msa.idFilial;
                    mesaReserva.nuMesa = msa.nuMesa;
                    //mesaReserva.idReserva = 10;

                    if (hd_tipo.Value.ToString() == "3")
                    {
                        if(cont == 0)
                        {
                            conta.nuMesa = mesaReserva.nuMesa;
                            cont++;
                        }else
                        {
                            AMESASASSOCIADAS mes = new AMESASASSOCIADAS();
                            mes.idEmpresa = Memoria.Empresa.Value;
                            mes.idFilial = Memoria.Filial.Value;
                            mes.nuMesa = mesaReserva.nuMesa;
                            conta.AMESASASSOCIADAS.Add(mes);
                        }
                    }
                    
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

                    if (hd_tipo.Value.ToString() != "1")
                    {
                        if (hd_tipo.Value.ToString() == "2")
                        {
                            // Atualizar
                            conf = (bool) reserva.ExecutaFuncao(res, Funcoes.Atualizar, compl);
                        }
                        else
                        {
                            // Atender
                            conf = (bool) reserva.ExecutaFuncao(res, Funcoes.Atender, compl);
                            if(conf)
                            {
                                conf = (bool) cconta.ExecutaFuncao(conta, Funcoes.Adicionar, null);
                            }

                        }
                    }
                    else
                    {
                        // Adicionar                  
                        conf = (bool) reserva.ExecutaFuncao(res, Funcoes.Adicionar, compl);
                    }

                    if (conf)
                    {
                        if (hd_tipo.Value.ToString() != "1")
                        {
                            if (hd_tipo.Value.ToString() == "2")
                            {
                                msg = "Reserva atualizada com sucesso.";
                            }
                            else
                            {
                                if (hd_tipo.Value.ToString() == "3")
                                {
                                    msg = "Reserva atendida com sucesso.";
                                }
                            }
                        }
                        else
                        {
                            msg = "Reserva cadastrada com sucesso.";
                        }
                        Window1.Hide();
                    }
                    else
                    {
                        if (hd_tipo.Value.ToString() != "1")
                        {
                            if (hd_tipo.Value.ToString() == "2")
                            {
                                msg = "Erro ao atualizar reserva, por favor confira todos os campos.";
                            }
                            else
                            {
                                if (hd_tipo.Value.ToString() == "3")
                                {
                                    msg = "Erro ao atender reserva, por favor confira todos os campos.";
                                }
                            }
                        }
                        else
                        {
                            msg = "Erro ao cadastrar reserva, por favor confira todos os campos.";
                        }

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
        grid_reserva.Reload();
    }
    // ==================================================================================================

    
    
    // Função para definir ações dos botões da gridpanel de reserva
    protected void gridreserva_acao(object sender, DirectEventArgs e)
    {
        using (Contexto.Atual = new Restaurante())
        {
            string msg = "";
            int idreserva = Convert.ToInt32(e.ExtraParams["id"]);
            string comando = e.ExtraParams["command"];
            // cancela, atualiza, aprova, atende

            Reserva reserva = new Reserva();
            RRESERVA res = new RRESERVA();
            List<string> compl = new List<string>();

            res.idReserva = idreserva;

            res = (RRESERVA) reserva.ExecutaFuncao(res, Funcoes.Buscar, compl);

            // Comando para mostrar as mesas
            if (comando == "mesa")
            {
                carregaRgridmesareserva(res);
            }
            else
            {

                // Comando de cancelar
                if (comando == "cancela")
                {
                    if ((bool) reserva.ExecutaFuncao(res, Funcoes.Cancelar, compl))
                    {
                        msg = "Reserva cancelada com sucesso.";
                    }
                    else
                    {
                        msg = "Erro ao cancelar reserva.";
                    }

                }
                else
                {
                    // Comando de aprovar
                    if (comando == "aprova")
                    {
                        res.status = 5;

                        RParametros parametro = new RParametros();
                        RPARAM param = new RPARAM();

                        param = (RPARAM) parametro.ExecutaFuncao(param, Funcoes.Buscar, null);

                        string mensagem = param.corpoEmail1;

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

                        string assunto = param.assuntoEmail1;

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

                        Email.CriaEmail(mensagem, assunto, res.emailCliente);


                        foreach (RRESMESA rr in res.RRESMESA)
                        {
                            rr.GMESA.idStatus = 3; //Reservada
                        }

                        if ((bool) reserva.ExecutaFuncao(res, Funcoes.Atualizar, compl))
                        {
                            msg = "Reserva aprovada com sucesso.";
                        }
                        else
                        {
                            msg = "Erro ao aprovar reserva.";
                        }

                    }
                    else
                    {
                        string data = res.data.Value.ToString("dd/MM/yyyy");
                        string horario = res.horario.Value.ToString("HH:mm");
                        string horariolimite = res.horarioLimite.Value.ToString("HH:mm");

                        txt_cliente.Text = res.cliente;
                        txt_telefone.Text = res.telefone;
                        txt_data.Text = data;
                        txt_horario.Text = horario;
                        txt_horariolimite.Text = horariolimite;
                        hd_id.Value = res.idReserva;

                        // Comando de atualizar
                        if (comando == "atualiza")
                        {
                            Window1.Title = "Atualiza reserva";
                            Window1.Icon = Icon.ApplicationEdit;
                            btn_reserva.Text = "Atualizar";
                            btn_reserva.Icon = Icon.BookmarkEdit;
                            hd_tipo.Value = "2";
                        }
                        else
                        {
                            // Comando de atender
                            if (comando == "atende")
                            {
                                Window1.Title = "Atende reserva";
                                Window1.Icon = Icon.ApplicationFormMagnify;
                                btn_reserva.Text = "Atender";
                                btn_reserva.Icon = Icon.ApplicationLightning;
                                hd_tipo.Value = "3";
                            }
                        }

                        Window1.Show();

                        carregaRgridmesa(res);

                        RowSelectionModel sm = this.grid_mesa.SelectionModel.Primary as RowSelectionModel;
                        sm.ClearSelections();
                        foreach (RRESMESA mesa in res.RRESMESA)
                        {
                            sm.SelectedRows.Add(new SelectedRow(Convert.ToString(mesa.nuMesa)));
                        }
                        sm.UpdateSelection();

                    }
                }
            }



            if (msg != "")
            {
                X.Msg.Alert("Alerta", msg).Show();
                grid_reserva.Reload();
            }
        }
    }
    // ==================================================================================================
    
    
    
    // Função para abrir janela de nova reserva
    protected void nova_reserva(object sender, DirectEventArgs e)
    {
        using (Contexto.Atual = new Restaurante())
        {
            RParametros parametro = new RParametros();
            RPARAM param = new RPARAM();
            List<string> compl = new List<string>();

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
            carregaRgridmesa(null);
        }
    }

    


</script>


<script type="text/javascript">
    
    // Função que decide qual botão de ação irá aparecer dependendo do status
    var prepareCommand = function (grid, command, record, row) {
        if (command.command == 'aprova' && record.data.idStatus != 7) {
            command.hidden = true;
            command.hideMode = 'visibility';
        } else {

            if (document.getElementById("aprova").value == "1" && command.command == 'aprova') {
                command.hidden = true;
                command.hideMode = 'visibility';

            }
        }
        if (command.command == 'atende' && record.data.idStatus != 5) {
            command.hidden = true;
            command.hideMode = 'visibility';
        } else {

            if (document.getElementById("atende").value == "1" && command.command == 'atende') {
                command.hidden = true;
                command.hideMode = 'visibility';

            }
        }
        if (command.command == 'cancela' && (record.data.idStatus == 6 || record.data.idStatus == 8)) {
            command.hidden = true;
            command.hideMode = 'visibility';
        } else {

            if (document.getElementById("cancela").value == "1" && command.command == 'cancela') {
                command.hidden = true;
                command.hideMode = 'visibility';

            }
        }
        if (command.command == 'atualiza' && (record.data.idStatus == 6 || record.data.idStatus == 8)) {
            command.hidden = true;
            command.hideMode = 'visibility';
        } else {

            if (document.getElementById("atualiza").value == "1" && command.command == 'atualiza') {
                command.hidden = true;
                command.hideMode = 'visibility';

            }
        }

    };
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

        <input type="hidden" id="aprova" runat="server" />
        <input type="hidden" id="atende" runat="server" />
        <input type="hidden" id="cancela" runat="server" />
        <input type="hidden" id="atualiza" runat="server" /> 
        
        <%-- Store é o datasource, o resultado dos selects --%>
        <%-- Store da lista de resevas --%>
        <ext:Store ID="store_reserva" runat="server">
            <Reader>
                <ext:JsonReader IDProperty="idReserva">
                    <Fields>
                        <ext:RecordField Name="observacao" Type="String" />
                        <ext:RecordField Name="idReserva" Type="Int" />
                        <ext:RecordField Name="telefone" Type="String" />
                        <ext:RecordField Name="cliente" Type="String" />
                        <ext:RecordField Name="qtd" Type="String" />
                        <ext:RecordField Name="status" Type="String" />
                        <ext:RecordField Name="idqtd" Type="Int" />
                        <ext:RecordField Name="idStatus" Type="Int" />
                        <ext:RecordField Name="data" Type="Date" />
                        <ext:RecordField Name="horario" Type="Date" />
                        <ext:RecordField Name="horariolimite" Type="Date" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <SortInfo Field="horario" Direction="ASC" />
        </ext:Store>

        <%-- Store da lista de mesas --%>
        <ext:Store ID="store_mesa" runat="server">
            <Reader>
                 <ext:JsonReader IDProperty="mesa">
                    <Fields>
                        <ext:RecordField Name="mesa" Type="Int" />
                        <ext:RecordField Name="observacao" Type="String"/>
                        <ext:RecordField Name="qtd" Type="String"/>
                        <ext:RecordField Name="status" Type="String"/>
                        <ext:RecordField Name="idqtd" Type="Int" />
                        <ext:RecordField Name="idStatus" Type="Int" />
                    </Fields>
                 </ext:JsonReader>
            </Reader>    
        </ext:Store>
        
        
        <ext:GridPanel 
            ID="grid_reserva" 
            runat="server" 
            Frame="true"
            StoreID="store_reserva"
            StripeRows="true"
            Collapsible="false"
            AnimCollapse="false"
            TrackMouseOver="true"
            AnchorHorizontal="100%"
            Height="560"
            AutoDestroy="true"
            AutoWidth="true"
            AutoFocus="true"
            HideBorders="true"
            AutoExpandColumn="cliente"             
            >
                <ColumnModel ID="ColumnModel1" runat="server">
                <Columns>
                    <ext:RowNumbererColumn />
                    <ext:DateColumn DataIndex="data" Header="Data" Format="dd/MM/yyyy" Width="100" />
                    <ext:Column Header="Cliente" DataIndex="cliente" />
                    <ext:Column Header="Telefone" DataIndex="telefone" Width="100" />
                    <ext:DateColumn DataIndex="horario" Header="Horário" Format="HH:mm" Width="100" />
                    <ext:DateColumn DataIndex="horariolimite" Header="Horário Limite" Format="HH:mm" Hidden="true" Width="100" />
                    <ext:Column Header="Status" DataIndex="status" Width="100" />
                    <ext:Column Header="Observação" DataIndex="observacao" Width="250" Hidden="True" />
                    <ext:Column Header="Quantdidade de Lugares" DataIndex="qtd" Width="250" Hidden="True" />
                    <ext:Column Header="idStatus" DataIndex="idStatus" Width="100" Hidden="True" />
                    <ext:Column Header="idreserva" DataIndex="idreserva" Width="100" Hidden="True" />  
                    <ext:ImageCommandColumn Width="200" Header="Mesas">
                        <Commands>
                            <ext:ImageCommand CommandName="mesa" Icon="TextListBullets" Text="Mesas">
                                <ToolTip Text="Mesas" />
                            </ext:ImageCommand>
                        </Commands>
                    </ext:ImageCommandColumn>
                    <ext:ImageCommandColumn Width="300">
                        <Commands>
                            <ext:ImageCommand CommandName="cancela" Icon="Delete" Text="Cancelar">
                                <ToolTip Text="Remover" />
                            </ext:ImageCommand>

                            <ext:ImageCommand CommandName="atualiza" Icon="TableEdit" Text="Atualizar">
                                <ToolTip Text="Atualizar" />
                            </ext:ImageCommand>

                            <ext:ImageCommand CommandName="aprova" Icon="Accept" Text="Aprovar">
                                <ToolTip Text="Aprovar" />
                            </ext:ImageCommand>

                            <ext:ImageCommand CommandName="atende" Icon="FilmAdd" Text="Atender">
                                <ToolTip Text="Atender" />
                            </ext:ImageCommand>
                        </Commands>

                        <PrepareCommand Fn="prepareCommand" />
                    </ext:ImageCommandColumn>
                </Columns>
            </ColumnModel>
            <LoadMask ShowMask="true" />
            <TopBar>
                <ext:Toolbar ID="Toolbar1" runat="server" >
                    <Items>
                        <ext:Button ID="Button2" runat="server" Text="Nova reserva" Icon="Add">
                            <DirectEvents>
                                <Click OnEvent="nova_reserva"></Click>
                            </DirectEvents>
                        </ext:Button>
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <Plugins>
                <ext:GridFilters runat="server" ID="GridFilters1" Local="true">
                    <Filters>
                        <ext:StringFilter DataIndex="cliente" />
                        <ext:StringFilter DataIndex="observacao" />
                        <ext:StringFilter DataIndex="status" Value="Confirmada" />
                        <ext:StringFilter DataIndex="telefone" />
                        <ext:DateFilter DataIndex="data">
                            <DatePickerOptions runat="server" TodayText="Agora" />
                        </ext:DateFilter>
                    </Filters>
                </ext:GridFilters>
            </Plugins>
            <DirectEvents>
                <Command OnEvent="gridreserva_acao">                                       
                    <ExtraParams>
                        <ext:Parameter Name="id" Value="record.id" Mode="Raw" />
                        <ext:Parameter Name="command" Value="command" Mode="Raw"/>
                    </ExtraParams>
                </Command> 
                <RowDblClick OnEvent="gridreserva_acao">
                    <ExtraParams>
                        <ext:Parameter Name="id" Value="grid_reserva.getRowsValues({ selectedOnly: true })[0].idReserva" Mode="Raw" />
                        <ext:Parameter Name="command" Value="atualiza" Mode="Value"/>
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
                        <Click OnEvent="funcao_reserva">
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
                    StoreID="store_mesa"
                    StripeRows="true"
                    Title="Mesas"
                    Collapsible="true"
                    AnimCollapse="false"
                    Icon="Door"
                    TrackMouseOver="false"
                    >
                        <ColumnModel ID="ColumnModel2" runat="server">
                        <Columns>
                            <ext:NumberColumn DataIndex="mesa" Header="Mesa" Format="999" Width="50" />
                            <ext:Column DataIndex="qtd" Header="Lugares" Width="100" />
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
        
        <%-- window para mesa --%>
        <ext:Window 
            ID="Window3" 
            runat="server" 
            Closable="false"
            Resizable="false"
            Height="350" 
            Icon="ApplicationSideList" 
            Title="Mesas"
            Draggable="false"
            Width="550"
            Modal="true"
            Padding="5"
            Hidden="True"
            Layout="Form">
            <Items>
                
                <ext:GridPanel 
                    ID="GridPanel1" 
                    runat="server" 
                    Height="300"
                    Frame="true"
                    StoreID="store_mesa"
                    StripeRows="true"
                    Title="Mesas"
                    Collapsible="true"
                    AnimCollapse="false"
                    Icon="ApplicationViewList"
                    TrackMouseOver="false"
                    >
                        <ColumnModel ID="ColumnModel3" runat="server">
                        <Columns>
                            <ext:NumberColumn DataIndex="mesa" Header="Mesa" Format="999" Width="50" />
                            <ext:Column DataIndex="qtd" Header="Lugares" Width="100" />
                            <ext:Column DataIndex="observacao" Header="Observação" Width="300" />
                        </Columns>
                    </ColumnModel>
                    <LoadMask ShowMask="true" />
                    <Plugins>
                        <ext:GridFilters runat="server" ID="GridFilters2" Local="true">
                            <Filters>
                                <ext:NumericFilter DataIndex="mesa" />
                                <ext:StringFilter DataIndex="qtd" />
                                <ext:StringFilter DataIndex="observacao" />
                            </Filters>
                        </ext:GridFilters>
                    </Plugins>
                </ext:GridPanel>

            </Items>

            <Buttons>
                <ext:Button ID="Button5" runat="server" Text="Fechar" Icon="Decline">
                    <Listeners>
                        <Click Handler="#{Window3}.hide();" />
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
