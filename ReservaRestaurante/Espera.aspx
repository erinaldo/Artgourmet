<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Espera.aspx.cs" Inherits="Artebit.Restaurante.Reserva.Espera" %>

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
        
        carregaRgridespera();
        carregaRgridqtd();

        if (!IsPostBack)
        {
            DateFilter df = (DateFilter)GridFilters1.Filters[3];

            df.OnValue = DateTime.Now;
        }
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
            compl[2] = "3";
            bool conf = (bool) perfil.ExecutaFuncao(prf, Funcoes.Verificar, compl);
            if (conf)
                cancela.Value = "1";

            compl[2] = "2";
            conf = (bool) perfil.ExecutaFuncao(prf, Funcoes.Verificar, compl);
            if (conf)
                atualiza.Value = "1";

            compl[2] = "4";
            conf = (bool) perfil.ExecutaFuncao(prf, Funcoes.Verificar, compl);
            if (conf)
                atende.Value = "1";
        }
    }

    protected void Atualiza(object sender, DirectEventArgs e)
    {
        //this.lbMesas1.Text = DateTime.Now.ToString("HH:mm:ss");
    }



    // Função para carregar dados da gridpanel de Qtd
    protected void carregaRgridqtd()
    {
        using (Contexto.Atual = new Restaurante())
        {
            // Espera
            GrupoMesa grupo = new GrupoMesa();
            RGRUPOMESA grp = new RGRUPOMESA();
            List<string> compl = new List<string>();

            IQueryable<RGRUPOMESA> lista_grupo =
                (IQueryable<RGRUPOMESA>) grupo.ExecutaFuncao(grp, Funcoes.BuscarLista, compl);
            var dados = from a in lista_grupo
                        select new
                                   {
                                       idqtd = a.idGrupo,
                                       descricao = a.descricao
                                   };
            store_qtd.DataSource = dados;

            store_qtd.DataBind();
        }
    }
    // ==================================================================================================
    
    
    
    // Função para carregar dados da gridpanel de mesa
    protected void carregaRgridmesa()
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
    
    

    // Função para carregar dados da gridpanel de Espera
    protected void carregaRgridespera()
    {
        using (Contexto.Atual = new Restaurante())
        {
            // Espera
            Espera espera = new Espera();
            RFILAESPERA res = new RFILAESPERA();
            List<string> compl = new List<string>();

            IQueryable<RFILAESPERA> lista_espera =
                (IQueryable<RFILAESPERA>) espera.ExecutaFuncao(res, Funcoes.BuscarLista, compl);
            var dados = from a in lista_espera
                        select new
                                   {
                                       cliente = a.cliente,
                                       qtd = a.RGRUPOMESA.descricao,
                                       status = a.RSTATFILA.descricao,
                                       idqtd = a.qtdLugares,
                                       idStatus = a.idStatus,
                                       datainclusao = a.dataInclusao,
                                       idfila = a.idFila,
                                       hora =
                            System.Data.Objects.EntityFunctions.DiffMinutes(a.dataInclusao, DateTime.Now)
                                   };


            store_espera.DataSource = dados;
            store_espera.DataBind();
        }
    }
    // ==================================================================================================
    
    
    
    // Função para window de Espera
    protected void funcao_espera(object sender, DirectEventArgs e)
    {
        using (Contexto.Atual = new Restaurante())
        {
            string msg = "";

            try
            {
                Espera espera = new Espera();
                RFILAESPERA res = new RFILAESPERA();
                List<string> compl = new List<string>();

                // Se não for adicionar
                if (hd_tipo.Value.ToString() != "1")
                {
                    res.idFila = Convert.ToInt32(hd_id.Value);
                    res = (RFILAESPERA) espera.ExecutaFuncao(res, Funcoes.Buscar, compl);
                }

                res.cliente = txt_cliente.Text;
                res.qtdLugares = Convert.ToInt32(cmb_qtd.Value);
                res.idStatus = 10;

                if (msg == "")
                {
                    bool conf = false;

                    if (hd_tipo.Value.ToString() != "1")
                    {
                        if (hd_tipo.Value.ToString() == "2")
                        {
                            // Atualizar
                            conf = (bool) espera.ExecutaFuncao(res, Funcoes.Atualizar, compl);
                        }
                        else
                        {
                            // Atender
                            RowSelectionModel sm = this.grid_mesa.SelectionModel.Primary as RowSelectionModel;

                            List<GMESA> lista_mesa = new List<GMESA>();

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

                                if (hd_tipo.Value.ToString() == "3")
                                {
                                    if (cont == 0)
                                    {
                                        conta.nuMesa = msa.nuMesa;
                                        cont++;
                                    }
                                    else
                                    {
                                        AMESASASSOCIADAS mes = new AMESASASSOCIADAS();
                                        mes.idEmpresa = Memoria.Empresa.Value;
                                        mes.idFilial = Memoria.Filial.Value;
                                        mes.nuMesa = msa.nuMesa;
                                        conta.AMESASASSOCIADAS.Add(mes);
                                    }
                                }
                                
                                msa.idStatus = 1;
                                

                                lista_mesa.Add(msa);
                            }

                            conf = (bool) espera.ExecutaFuncao(res, Funcoes.Atender, compl);
                            if (conf)
                            {
                                conf = (bool)cconta.ExecutaFuncao(conta, Funcoes.Adicionar, null);
                            }
                        }
                    }
                    else
                    {
                        // Adicionar                  
                        conf = (bool) espera.ExecutaFuncao(res, Funcoes.Adicionar, compl);
                    }

                    if (conf)
                    {
                        if (hd_tipo.Value.ToString() != "1")
                        {
                            if (hd_tipo.Value.ToString() == "2")
                            {
                                msg = "Cliente atualizado com sucesso.";
                            }
                            else
                            {
                                if (hd_tipo.Value.ToString() == "3")
                                {
                                    msg = "Cliente atendido com sucesso.";
                                }
                            }
                        }
                        else
                        {
                            msg = "Cliente cadastrado com sucesso.";
                        }
                        Window1.Hide();
                    }
                    else
                    {
                        if (hd_tipo.Value.ToString() != "1")
                        {
                            if (hd_tipo.Value.ToString() == "2")
                            {
                                msg = "Erro ao atualizar cliente, por favor confira todos os campos.";
                            }
                            else
                            {
                                if (hd_tipo.Value.ToString() == "3")
                                {
                                    msg = "Erro ao atender cliente, por favor confira todos os campos.";
                                }
                            }
                        }
                        else
                        {
                            msg = "Erro ao cadastrar cliente, por favor confira todos os campos.";
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
            grid_espera.Reload();
        }
    }
    // ==================================================================================================

    
    
    // Função para definir ações dos botões da gridpanel de Espera
    protected void gridespera_acao(object sender, DirectEventArgs e)
    {
        using (Contexto.Atual = new Restaurante())
        {
            string msg = "";
            int idEspera = Convert.ToInt32(e.ExtraParams["id"]);
            string comando = e.ExtraParams["command"];
            // cancela, atualiza, aprova, atende

            Espera espera = new Espera();
            RFILAESPERA res = new RFILAESPERA();
            List<string> compl = new List<string>();

            res.idFila = idEspera;

            res = (RFILAESPERA) espera.ExecutaFuncao(res, Funcoes.Buscar, compl);

            // Comando de cancelar
            if (comando == "cancela")
            {
                if ((bool) espera.ExecutaFuncao(res, Funcoes.Cancelar, compl))
                {
                    msg = "Espera de cliente cancelada com sucesso.";
                }
                else
                {
                    msg = "Erro ao cancelar espera de cliente.";
                }

            }
            else
            {

                txt_cliente.Text = res.cliente;
                hd_id.Value = res.idFila;
                cmb_qtd.Value = res.qtdLugares;

                // Comando de atualizar
                if (comando == "atualiza")
                {
                    Window1.Title = "Atualiza Cliente";
                    Window1.Icon = Icon.ApplicationEdit;
                    btn_Espera.Text = "Atualizar";
                    btn_Espera.Icon = Icon.BookmarkEdit;
                    btn_winmesa.Hidden = true;
                    hd_tipo.Value = "2";
                }
                else
                {
                    // Comando de atender
                    if (comando == "atende")
                    {
                        Window1.Title = "Atende Espera";
                        Window1.Icon = Icon.ApplicationFormMagnify;
                        btn_Espera.Text = "Atender";
                        btn_Espera.Icon = Icon.ApplicationLightning;
                        hd_tipo.Value = "3";
                        btn_winmesa.Hidden = false;

                        carregaRgridmesa();
                    }
                }

                Window1.Show();
            }


            if (msg != "")
            {
                X.Msg.Alert("Alerta", msg).Show();
                grid_espera.Reload();
            }
        }
    }
    // ==================================================================================================
    
    
    
    // Função para abrir janela de nova Espera
    protected void nova_espera(object sender, DirectEventArgs e)
    {
        txt_cliente.Text = "";
        hd_id.Value = "";
        hd_tipo.Value = "1";
        cmb_qtd.Clear();

        Window1.Title = "Novo cliente";
        Window1.Icon = Icon.ApplicationFormAdd;
        btn_Espera.Text = "Cadastrar";
        btn_Espera.Icon = Icon.ApplicationAdd;
        btn_winmesa.Hidden = true;
        Window1.Show();
        carregaRgridmesa();
    }

</script>

<script type="text/javascript">

    // Função que decide qual botão de ação irá aparecer dependendo do status
    var prepareCommand = function (grid, command, record, row) {
        if (record.data.idStatus == 9 || record.data.idStatus == 11) {
            command.hidden = true;
            command.hideMode = 'visibility';
        }
        if (command.command == 'atende' && record.data.idStatus != 10) {
            command.hidden = true;
            command.hideMode = 'visibility';
        } else {

            if (document.getElementById("atende").value == "1" && command.command == 'atende') {
                command.hidden = true;
                command.hideMode = 'visibility';

            }
        }
        if (command.command == 'cancela' && (record.data.idStatus == 9 || record.data.idStatus == 11)) {
            command.hidden = true;
            command.hideMode = 'visibility';
        } else {

            if (document.getElementById("cancela").value == "1" && command.command == 'cancela') {
                command.hidden = true;
                command.hideMode = 'visibility';

            }
        }
        if (command.command == 'atualiza' && (record.data.idStatus == 9 || record.data.idStatus == 11)) {
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
    <title>..:: Esperas ::..</title>
    
   
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

         <input type="hidden" id="cancela" runat="server" />
          <input type="hidden" id="atualiza" runat="server" />
           <input type="hidden" id="atende" runat="server" />
        
        <%-- Store é o datasource, o resultado dos selects --%>
        <%-- Store da lista de resevas --%>
        <ext:Store ID="store_espera" runat="server" GroupField="qtd">
            <Reader>
                <ext:JsonReader IDProperty="idfila">
                    <Fields>
                        <ext:RecordField Name="cliente" Type="String" />
                        <ext:RecordField Name="qtd" Type="String" />
                        <ext:RecordField Name="status" Type="String" />
                        <ext:RecordField Name="idfila" Type="Int" />
                        <ext:RecordField Name="idqtd" Type="Int" />
                        <ext:RecordField Name="idStatus" Type="Int" />
                        <ext:RecordField Name="datainclusao" Type="Date" />
                        <ext:RecordField Name="hora" Type="Int" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <SortInfo Field="datainclusao" Direction="ASC" />
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

        <%-- Store da lista de qtd lugares --%>
        <ext:Store ID="store_qtd" runat="server">
            <Reader>
                 <ext:JsonReader IDProperty="idqtd">
                    <Fields>
                        <ext:RecordField Name="descricao" Type="String"/>
                        <ext:RecordField Name="idqtd" Type="Int" />
                    </Fields>
                 </ext:JsonReader>
            </Reader>    
        </ext:Store>
        
        
        <ext:GridPanel 
            ID="grid_espera" 
            runat="server" 
            Frame="true"
            StoreID="store_espera"
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
                    <ext:TemplateColumn DataIndex="" MenuDisabled="true" Header="Tempo de espera">
                        <Template ID="Template1" runat="server">
                            <Html>
						        <tpl for=".">
							        {hora} minuto(s)
						        </tpl>
					        </Html>
                        </Template>
                    </ext:TemplateColumn>
                    <ext:Column Header="Cliente" DataIndex="cliente" />
                    <ext:DateColumn Header="Data" DataIndex="datainclusao" Format="dd/MM/yyyy" Hidden="true" />
                    <ext:Column Header="Quantidade de lugares" DataIndex="qtd" Width="250" />
                    <ext:Column Header="Status" DataIndex="status" Width="100" />
                    <ext:Column Header="idStatus" DataIndex="idStatus" Width="100" Hidden="True" />
                    <ext:Column Header="idfila" DataIndex="idfila" Width="100" Hidden="True" />  
                    <ext:ImageCommandColumn Width="300">
                        <Commands>
                            <ext:ImageCommand CommandName="cancela" Icon="Delete" Text="Cancelar">
                                <ToolTip Text="Remover" />
                            </ext:ImageCommand>

                            <ext:ImageCommand CommandName="atualiza" Icon="TableEdit" Text="Atualizar">
                                <ToolTip Text="Atualizar" />
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
                <ext:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <ext:Button ID="Button2" runat="server" Text="Novo cliente" Icon="Add">
                            <DirectEvents>
                                <Click OnEvent="nova_espera"></Click>
                            </DirectEvents>
                        </ext:Button>
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <Plugins>
                <ext:GridFilters runat="server" ID="GridFilters1" Local="true">
                    <Filters>
                        <ext:StringFilter DataIndex="cliente" />
                        <ext:StringFilter DataIndex="qtd" />
                        <ext:StringFilter DataIndex="status" Value="Pendente" />
                        <ext:DateFilter DataIndex="datainclusao">
                            <DatePickerOptions runat="server" TodayText="Agora" />
                        </ext:DateFilter>
                    </Filters>
                </ext:GridFilters>
            </Plugins>
            <DirectEvents>
                <Command OnEvent="gridespera_acao">                                       
                    <ExtraParams>
                        <ext:Parameter Name="id" Value="record.id" Mode="Raw" />
                        <ext:Parameter Name="command" Value="command" Mode="Raw"/>
                    </ExtraParams>
                </Command> 
                <RowDblClick OnEvent="gridespera_acao">
                    <ExtraParams>
                        <ext:Parameter Name="id" Value="grid_espera.getRowsValues({ selectedOnly: true })[0].idfila" Mode="Raw" />
                        <ext:Parameter Name="command" Value="atualiza" Mode="Value"/>
                    </ExtraParams>
                </RowDblClick>
            </DirectEvents>
            <View>
                <ext:GroupingView
                    ID="GroupingView1"
                    HideGroupedColumn="false"
                    runat="server" 
                    ForceFit="true"
                    StartCollapsed="false"
                    GroupTextTpl='<span></span>{text} ({[values.rs.length]} {[values.rs.length > 1 ? "Esperas" : "Espera"]})'
                    EnableRowBody="true">
                </ext:GroupingView>
            </View>
            <SelectionModel>
                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server"/>
            </SelectionModel>
        </ext:GridPanel>

        
        <%-- window para adicionar nova Espera --%>
        <ext:Window 
            ID="Window1" 
            runat="server" 
            Closable="false"
            Resizable="false"
            Height="350" 
            Icon="Add" 
            Title="Nova Espera"
            Draggable="false"
            Width="550"
            Modal="true"
            Padding="5"
            Hidden="True"
            Layout="Form">
            <Items>
                <%-- ID da Espera --%>
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

                <%-- Quantidade de lugares --%>
                <ext:ComboBox 
                    ID="cmb_qtd" 
                    runat="server"
                    StoreID="store_qtd" 
                    FieldLabel="Quantidade de lugar(es)"
                    Width="250"
                    Editable="false"
                    ValueField="idqtd"
                    DisplayField="descricao"
                    TypeAhead="true" 
                    Mode="Local"
                    ForceSelection="true"
                    EmptyText="Escolha..."
                    SelectOnFocus="true">
                    </ext:ComboBox>
                
                <ext:Button ID="btn_winmesa" Hidden="false" runat="server" Text="Mesas" Icon="FolderTable">
                    <Listeners>
                        <Click Handler="#{Window2}.show();" />
                    </Listeners>
                </ext:Button>

            </Items>
            <Buttons>
                <ext:Button ID="btn_Espera" runat="server" Text="Adicionar" Icon="Add">
                    <DirectEvents>
                        <Click OnEvent="funcao_espera">
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
