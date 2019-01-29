<%@ Page Title="Reporte Diario de Vendedores" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="InformacionDiarioVendedores.aspx.cs" Inherits="InformacionDiarioVendedores" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
   
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-2.2.4.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        function Confirmarenvio() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("¿Está usted segur@ que desea enviar este acompañamiento?")) {
                confirm_value.value = "Si";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
     <style type="text/css">
        .auto-style2
        {
            color: #CC6600;
            font-style: italic;
            font-weight: 700;
        }      
        #Background
        {
            position: fixed;
            top: 0px;
            bottom: 0px;
            left: 0px;
            right: 0px;
            overflow: hidden;
            padding: 0;
            margin: 0;
            background-color: #F0F0F0;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 100000;
        }
        #Progress
        {
            position: fixed;
            top: 40%;
            left: 40%;
            height: 20%;
            width: 20%;
            z-index: 100001;
            background-color: #FFFFFF;
            border: 1px solid Gray;
            background-image: url('PRUEBA.gif');
            background-repeat: no-repeat;
            background-position: center;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <br />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="Background">
            </div>
            <div id="Progress">
                <h6>
                    <p style="text-align: center">
                        <b>Un momento por favor...
                            <br />
                        </b>
                    </p>
                </h6>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <h3>
        Desde esta pantalla podrá detallar su trabajo diario</h3>
    <hr />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>                    
            <div class="row">
                <div class="col-md-5">         
                    <h5>
                        Información de Venta</h5>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                &nbsp;
                                <asp:Label ID="Label3" runat="server" Text="No. Visita *" Style="color: #FF9900;
                                    font-weight: 700; font-style: italic; font-size: small"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                                <asp:Label ID="Label4" runat="server" Text="No. Pedido" Style="color: #FF9900; font-weight: 700;
                                    font-style: italic; font-size: small"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                                <asp:Label ID="Label5" runat="server" Text="No. Posiciones" Style="color: #FF9900;
                                    font-weight: 700; font-style: italic; font-size: small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="novisita" runat="server" Width="100px" ReadOnly="true" Text="0" CssClass="form-control" ToolTip="Comprador contactado y ofrecido productos o con gestion de cobros"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="novisita_FilteredTextBoxExtender" FilterType="Numbers"
                                    runat="server" Enabled="True" TargetControlID="novisita">
                                </asp:FilteredTextBoxExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="nopedido" runat="server" Width="100px" CssClass="form-control" Text="0" ReadOnly="true" ToolTip="Total de pedidos realizados durante este acompañamiento"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="nopedido_FilteredTextBoxExtender" FilterType="Numbers"
                                    runat="server" Enabled="True" TargetControlID="nopedido">
                                </asp:FilteredTextBoxExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="noposiciones" runat="server" Width="100px" CssClass="form-control" Text="0" ReadOnly="true"
                                    ToolTip="Total de posiciones que fueron incluidas en todos los pedidos realizados en este acompañamiento"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="noposiciones_FilteredTextBoxExtender" FilterType="Numbers"
                                    runat="server" Enabled="True" TargetControlID="noposiciones">
                                </asp:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                          <td>
                                &nbsp;
                                <asp:Label ID="Label31" runat="server" Text="Monto Pedidos" Style="color: #FF9900;
                                    font-weight: 700; font-style: italic; font-size: small"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                                <asp:Label ID="Label6" runat="server" Text="No. Cobros *" Style="color: #FF9900;
                                    font-weight: 700; font-style: italic; font-size: small"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                                <asp:Label ID="Label7" runat="server" Text="Monto Cobros *" Style="color: #FF9900;
                                    font-weight: 700; font-style: italic; font-size: small"></asp:Label>
                            </td>        
                        </tr>
                        <tr>
                             <td>
                                <asp:TextBox ID="montopedidos" runat="server" Width="100px" CssClass="form-control"
                                    ToolTip="Monto total de pedidos realizados durante este acompañamiento"
                                    ReadOnly="true" Text="0"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Numbers"
                                    runat="server" Enabled="True" TargetControlID="montopedidos">
                                </asp:FilteredTextBoxExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="nocobros" runat="server" Width="100px" CssClass="form-control"
                                    ToolTip="Cantidad total de clientes que pagaron durante este acompañamiento"
                                    ReadOnly="true" Text="0"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="nocobros_FilteredTextBoxExtender" FilterType="Numbers"
                                    runat="server" Enabled="True" TargetControlID="nocobros">
                                </asp:FilteredTextBoxExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="montocobros" runat="server" Width="100px" CssClass="form-control"
                                    ToolTip="Monto total cobrados a todos los clientes que pagaron o fueron visiatados durante este acompañamiento"
                                   ReadOnly="true" Text="0"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="montocobros_FilteredTextBoxExtender" FilterType="Numbers, Custom"
                                    ValidChars="." runat="server" Enabled="True" TargetControlID="montocobros">
                                </asp:FilteredTextBoxExtender>
                            </td>               
                        </tr>
                        <tr>
                            <td>
                              &nbsp;
                                <asp:Label ID="Label8" runat="server" Text="No. Cotiz. *" Style="color: #FF9900;
                                    font-weight: 700; font-style: italic; font-size: small"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                                <asp:Label ID="Label9" runat="server" Text="Monto Cotiz. *" Style="color: #FF9900;
                                    font-weight: 700; font-style: italic; font-size: small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                        <td>
                           <asp:TextBox ID="nocotizacion" runat="server" Width="100px" CssClass="form-control"
                                    ToolTip="Cantidad total de cotizaciones realizadas durante este acompañamiento"
                                    ReadOnly="true" Text="0"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="nocotizacion_FilteredTextBoxExtender" FilterType="Numbers"
                                    runat="server" Enabled="True" TargetControlID="nocotizacion">
                                </asp:FilteredTextBoxExtender>
                        </td>
                            <td>
                                <asp:TextBox ID="montocotizaciones" runat="server" Width="100px" CssClass="form-control"
                                   ToolTip="Monto total de todas las cotizaciones realizadas en este acompañamiento"
                                     ReadOnly="true" Text="0"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="montocotizaciones_FilteredTextBoxExtender" FilterType="Numbers, Custom"
                                    ValidChars="." runat="server" Enabled="True" TargetControlID="montocotizaciones">
                                </asp:FilteredTextBoxExtender>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-md-5">
                    <h5>
                        Información de Visita</h5>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr align="center">
                            <td colspan="3">
                                &nbsp;
                                <asp:Label ID="Label17" runat="server" Text="CLIENTES" Style="color: #FF9900; font-weight: 700;
                                    font-style: italic; font-size: small"></asp:Label>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                &nbsp;
                                <asp:Label ID="Label10" runat="server" Text="Nuevos" Style="color: #FF9900; font-weight: 700;
                                    font-style: italic; font-size: small"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                                <asp:Label ID="Label11" runat="server" Text="Recuperados" Style="color: #FF9900;
                                    font-weight: 700; font-style: italic; font-size: small"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                                <asp:Label ID="Label12" runat="server" Text="Prob. Cobros" Style="color: #FF9900;
                                    font-weight: 700; font-style: italic; font-size: small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="noclientesnuevosvisitados" runat="server" Width="100px" CssClass="form-control" Text="0"
                                    ReadOnly="true" ToolTip="Cantidad total de clientes nuevos que fueron visitados en este acompañamiento"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="noclientesnuevosvisitados_FilteredTextBoxExtender"
                                    FilterType="Numbers" runat="server" Enabled="True" TargetControlID="noclientesnuevosvisitados">
                                </asp:FilteredTextBoxExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="noclientesrecuperadosvisitados" runat="server" Width="100px" CssClass="form-control" Text="0"
                                    ReadOnly="true" ToolTip="Cantidad total de clientes que fueron recuperados en este acompañamiento"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="clientesproblemasdecobros" runat="server" Width="100px" CssClass="form-control" Text="0"
                                    ReadOnly="true" ToolTip="Cantidad total de los clientes con problemas de cobros que fueron visiatos en todo este acompañamiento"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-md-3">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                &nbsp;
                                <asp:Label ID="Label13" runat="server" Text="Cant. Art. Nuevos Vend" Style="color: #FF9900;
                                    font-weight: 700; font-style: italic; font-size: small"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                                <asp:Label ID="Label14" runat="server" Text="Ctes. Fuera de Ruta" Style="color: #FF9900;
                                    font-weight: 700; font-style: italic; font-size: small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="articulosnuevos" runat="server" Width="150px" CssClass="form-control" Text="0"
                                    ToolTip="Cantidad total de articulos nuevos que vendio u ofrecio a todos los clientes visitados"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="fueraderuta" runat="server" Width="160px" CssClass="form-control" Text="0" ReadOnly="true"
                                    ToolTip="Cantidad de clientes que fuerón visitados fuera de la ruta pautada"></asp:TextBox>        
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <br />
                       <asp:Image ID="Image2" runat="server" ImageUrl="~/1.png" Height="36px" 
                Width="36px" />
            <h4>
                Analisis de Visitas</h4>
            <hr />
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        &nbsp;
                        <asp:Label ID="Label16" runat="server" Text="Seleccione el cliente *" Style="color: #FF9900;
                            font-weight: 700; font-style: italic; font-size: small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" AutoPostBack="true"
                            ToolTip="Todos los clientes que corresponden al vendedor seleccionado" Width="211px"
                            OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="subiranalisis" runat="server" Text="Subir Analisis" ToolTip="Boton usado para subir el analisis o el detalle del acompañamiento que realizo con el vendedor seleccionado y solo funciona cuando todos los campos estan debidamente rellenados"
                            CssClass="btn btn-md btn-info" OnClick="subiranalisis_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;
                     <%--   <asp:Label ID="Label18" runat="server" Text="" Style="color: Black; font-weight: 700;
                            font-size: small"></asp:Label>--%>

                            <asp:TextBox ID="Label18" runat="server" Width="407px" CssClass="form-control"                                    
                                ToolTip="Cantidad de Infos mostrados al cliente" ReadOnly="true"></asp:TextBox>
                            
                    </td>
                </tr>
            </table>
            <div class="row">
                <div class="col-md-5">
                    <h5>
                        Información de Ruta</h5>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                &nbsp;
                                <asp:Label ID="Label19" runat="server" Text="Visitado" Style="color: #FF9900; font-weight: 700;
                                    font-style: italic; font-size: small"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                                <asp:Label ID="Label20" runat="server" Text="Promos" Style="color: #FF9900; font-weight: 700;
                                    font-style: italic; font-size: small"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                                <asp:Label ID="Label21" runat="server" Text="No. Infos" Style="color: #FF9900; font-weight: 700;
                                    font-style: italic; font-size: small"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                                <asp:Label ID="Label22" runat="server" Text="Placas" Style="color: #FF9900; font-weight: 700;
                                    font-style: italic; font-size: small"></asp:Label>
                            </td>                         
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="RadioButton7" runat="server" Text="Si" AutoPostBack="true" OnCheckedChanged="RadioButton7_CheckedChanged" />
                                <asp:RadioButton ID="RadioButton8" runat="server" Text="No" AutoPostBack="true" OnCheckedChanged="RadioButton8_CheckedChanged" />
                            </td>
                            <td>
                                <asp:RadioButton ID="RadioButton9" runat="server" Text="Si" AutoPostBack="true" OnCheckedChanged="RadioButton9_CheckedChanged" />
                                <asp:RadioButton ID="RadioButton10" runat="server" Text="No" AutoPostBack="true"
                                    OnCheckedChanged="RadioButton10_CheckedChanged" />
                            </td>
                            <td>
                                <asp:TextBox ID="Noinfosmostrados" runat="server" Width="68px" CssClass="form-control"
                                    ToolTip="Cantidad de Infos mostrados al cliente"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RadioButton ID="RadioButton11" runat="server" Text="Si" AutoPostBack="true"
                                    OnCheckedChanged="RadioButton11_CheckedChanged" />
                                <asp:RadioButton ID="RadioButton12" runat="server" Text="No" AutoPostBack="true"
                                    OnCheckedChanged="RadioButton12_CheckedChanged" />
                            </td>                            
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                                <asp:Label ID="Label23" runat="server" Text="Piedras" Style="color: #FF9900; font-weight: 700;
                                    font-style: italic; font-size: small"></asp:Label>
                            </td>
                               <td colspan="3" align="center">
                                <asp:Label ID="Label30" runat="server" Text="Clientes" Style="color: #FF9900; font-weight: 700;
                                    font-style: italic; font-size: small"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="RadioButton13" runat="server" Text="Si" AutoPostBack="true"
                                    OnCheckedChanged="RadioButton13_CheckedChanged" />
                                <asp:RadioButton ID="RadioButton14" runat="server" Text="No" AutoPostBack="true"
                                    OnCheckedChanged="RadioButton14_CheckedChanged" />
                            </td>

                            <td> <asp:RadioButton ID="RadioButton3" runat="server" Text="Recup." 
                                    AutoPostBack="true" oncheckedchanged="RadioButton3_CheckedChanged" /></td>
                            <td> <asp:RadioButton ID="RadioButton4" runat="server" Text="Pro. Cobr." 
                                    AutoPostBack="true" oncheckedchanged="RadioButton4_CheckedChanged" /></td>
                            <td> 
                                <asp:CheckBox ID="RadioButton5" runat="server" Text="Fuera Ruta" />
                            </td>
                        </tr>                     
                    </table>
                    <table border="0" cellpadding="0" cellspacing="0">
                           <tr>
                        <td>
                            <asp:CheckBox ID="RadioButton6" runat="server" Text="Entrega Mercancia" />
                               </td>
                        </tr>
                    </table>
                </div>
                <div class="col-md-5">
                    <h5>
                        Información del cliente</h5>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                &nbsp;
                                <asp:Label ID="Label24" runat="server" Text="No. Operario *" Style="color: #FF9900;
                                    font-weight: 700; font-style: italic; font-size: small"></asp:Label>
                            </td>
                            <td colspan="2">
                                &nbsp;
                                <asp:Label ID="Label25" runat="server" Text="Correo *" Style="color: #FF9900; font-weight: 700;
                                    font-style: italic; font-size: small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="OPERARIO" runat="server" Width="100px" CssClass="form-control" ToolTip="Cantidad de personas que usan nuestros productos en casa del cliente"></asp:TextBox>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="CORREO" runat="server" Width="280px" CssClass="form-control" ToolTip="Correo que tiene o desea colocar el cliente seleccionado"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                &nbsp;
                                <asp:Label ID="Label26" runat="server" Text="Actividad *" Style="color: #FF9900;
                                    font-weight: 700; font-style: italic; font-size: small"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                                <asp:Label ID="Label27" runat="server" Text="M. Cobrado *" Style="color: #FF9900;
                                    font-weight: 700; font-style: italic; font-size: small"></asp:Label>
                            </td>
                            <td colspan="2">
                                &nbsp;
                                <asp:Label ID="Label28" runat="server" Text="M. Cotización *" Style="color: #FF9900;
                                    font-weight: 700; font-style: italic; font-size: small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control" AutoPostBack="true"
                                    Width="100px" ToolTip="División a la que pertenece el cliente seleccionado">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem>Auto</asp:ListItem>
                                    <asp:ListItem>Cargo</asp:ListItem>
                                    <asp:ListItem>Metal</asp:ListItem>
                                    <asp:ListItem>Reventa Auto</asp:ListItem>
                                    <asp:ListItem>Reventa Metal</asp:ListItem>
                                    <asp:ListItem>Construccion</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="MONTOCOBRADO" runat="server" Width="100px" CssClass="form-control"
                                    ToolTip="Monto cobra a clientes"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="MONTOCOBRADO_FilteredTextBoxExtender" FilterType="Numbers, Custom"
                                    ValidChars="." runat="server" Enabled="True" TargetControlID="MONTOCOBRADO">
                                </asp:FilteredTextBoxExtender>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="MCOTIZACIONES" runat="server" Width="100px" CssClass="form-control"
                                    ToolTip="Monto que cotizo al cliente seleccionado"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="MCOTIZACIONES_FilteredTextBoxExtender" FilterType="Numbers, Custom"
                                    ValidChars="." runat="server" Enabled="True" TargetControlID="MCOTIZACIONES">
                                </asp:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                         <td>
                         &nbsp;
                            <asp:Label ID="Label32" runat="server" Text="Monto Pedido" Style="color: #FF9900;
                                font-weight: 700; font-style: italic; font-size: small"></asp:Label>
                                </td>
                                <td>
                                &nbsp;
                            <asp:Label ID="Label33" runat="server" Text="No. Posiciones" Style="color: #FF9900;
                                font-weight: 700; font-style: italic; font-size: small"></asp:Label>
                                </td>
                        </tr>
                        <tr>
                               <td>
                                <asp:TextBox ID="Mpedido" runat="server" Width="100px" CssClass="form-control"
                                    ToolTip="Monto del pedido que realizo al cliente seleccionado"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="Numbers, Custom"
                                    ValidChars="." runat="server" Enabled="True" TargetControlID="Mpedido">
                                </asp:FilteredTextBoxExtender>
                            </td>

                                <td>
                                <asp:TextBox ID="noposicion" runat="server" Width="100px" CssClass="form-control"
                                    ToolTip="Numero de posiciones que realizo al pedido del cliente seleccionado"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" FilterType="Numbers, Custom"
                                    ValidChars="." runat="server" Enabled="True" TargetControlID="noposicion">
                                </asp:FilteredTextBoxExtender>
                            </td>
                        
                        </tr>
                    </table>
                </div>
                <br />
                <div class="col-md-10">
                    <h5>
                        Detalle de Analisis</h5>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateDeleteButton="True" Width="925px"
                        OnRowDeleting="GridView1_RowDeleting">
                    </asp:GridView>
                </div>
                <br />
                <div class="col-md-5">
                           <asp:Image ID="Image3" runat="server" ImageUrl="~/2.png" Height="36px" 
                Width="36px" />
                    <h5>
                        Información de Clientes Nuevos</h5>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                &nbsp;
                                <asp:Label ID="Label29" runat="server" Text="Nombres clientes nuevos" Style="color: #FF9900;
                                    font-weight: 700; font-style: italic; font-size: small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="nombreposibleclientenuevo" runat="server" CssClass="form-control"
                                    Width="213px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="Subir" runat="server" Text="Subir Nombre" CssClass="btn btn-md btn-info"
                                    OnClick="Subir_Click" Enabled="False" />
                            </td>
                            <td>
                                <asp:Button ID="eliminarnombre" runat="server" Text="Eliminar Nombre" CssClass="btn btn-md btn-info"
                                    OnClick="eliminarnombre_Click" Enabled="False" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ListBox ID="ListBox1" runat="server" Height="248px" Width="213px" CssClass="form-control">
                                </asp:ListBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <hr />
            <center>
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:Button ID="Enviar" runat="server" Text="Enviar" CssClass="btn btn-md btn-info" OnClientClick="Confirmarenvio()"
                                OnClick="Enviar_Click" />
                        </td>
                            <td>
                            <asp:Button ID="Guardar" runat="server" Text="Guardar" CssClass="btn btn-md btn-info" onclick="Guardar_Click"/>
                        </td>

                        <td>
                            <asp:Button ID="Cancelar" runat="server" Text="Cancelar" CssClass="btn btn-md btn-info"
                                OnClick="Cancelar_Click" />
                        </td>
                    </tr>
                </table>
            </center>
            <hr />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>