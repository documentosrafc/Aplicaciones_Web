<%@ Page Title="Pedidos Especiales" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="Pedidos_Especiales.aspx.cs" Inherits="Pedidos_Especiales" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-2.2.4.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        // This function will execute after file uploaded successfully
        function uploadComplete() {
            document.getElementById('<%=lblMsg.ClientID %>').innerHTML = "Archivo subido";
        }
        // This function will execute if file upload fails
        function uploadError() {
            document.getElementById('<%=lblMsg.ClientID %>').innerHTML = "Fallo al subir archivo.";
        }
    </script>
    <style type="text/css">
        .style1
        {
            color: #FF9900;
            font-weight: bold;
            font-style: italic;
        }
    </style>
    <style type="text/css">
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

    <style type="text/css">
        .modalBackground
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
        .modalpopup
        {
            padding: 20px 0px 24px 10px;
            position: relative;
            width: 645px;
            height: 565px;
            background-color: White;
            border: 1px solid black;
            top: 0px;
            left: 0px;
        }
    </style>
    <script type="text/javascript">
        function Confirmarenvio() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("¿Está usted seguro que desea generar esta pedido especial?")) {
                confirm_value.value = "Si";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <%--    <script type = "text/javascript">
    var ventana_secundaria 

    function abrirVentana(){ 
    ventana_secundaria = window.open("test.aspx","miventana","width=300,height=200,menubar=no") 
    }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <h3>
        Desde esta pantalla podrá registrar los pedidos especiales</h3>
    <hr />
    <asp:UpdateProgress ID="UpdateProgress5" runat="server">
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" CssClass="modalpopup">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        </updatepanel>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    &nbsp;<asp:Label ID="Label15" runat="server" Text="Código o nombre del artículo sin espacios"
                                        CssClass="style1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="buscardescripcion" runat="server" CssClass="form-control" Width="280px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Buscardes" runat="server" Text="Buscar" CssClass="btn btn-md btn-info"
                                        OnClick="Buscardes_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="cancelarbus" runat="server" Text="Cancelar" CssClass="btn btn-md btn-info"
                                        OnClick="cancelarbus_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:GridView ID="GridView2" runat="server" CellPadding="4" ForeColor="#333333" Width="615px"
                            AllowPaging="True" PageSize="15" ShowFooter="True" ShowHeaderWhenEmpty="True"
                            OnPageIndexChanging="GridView2_PageIndexChanging" OnRowCommand="GridView2_RowCommand">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:ButtonField CommandName="Seleccionar" Text="Seleccionar" />
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#B40404" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#B40404" Font-Bold="True" ForeColor="White" />
                            <PagerSettings Position="TopAndBottom" />
                            <PagerStyle BackColor="#B40404" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
            <input id="dummy" type="button" style="display: none" runat="server" />
            <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" PopupControlID="panel1"
                TargetControlID="dummy" PopupDragHandleControlID="panel1" BackgroundCssClass="modalBackground"
                DropShadow="true">
            </asp:ModalPopupExtender>
            <asp:Image ID="Image3" runat="server" ImageUrl="~/1.png" Height="41px" Width="40px"/>
            &nbsp;
            <asp:Label ID="Label16" runat="server" Text="PRIMER PASO" CssClass="style1"></asp:Label>
            <h5>Seleccione el aereo y coloque los datos del cliente</h5>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                     
                        <asp:Label ID="Label1" runat="server" Text="Seleccione el Aereo" CssClass="style1"></asp:Label>
                    </td>
                    <td>
                       
                        <asp:Label ID="Label4" runat="server" Text="Cod. de Cliente" CssClass="style1"></asp:Label>
                    </td>
                    <td>
                    
                        <asp:Label ID="Label5" runat="server" Text="Nombre Cliente" CssClass="style1"></asp:Label>
                    </td>
                    <td>
                        
                        <asp:Label ID="Label2" runat="server" Text="Fecha Limite" CssClass="style1"></asp:Label>
                    </td>
                    <td>
                       
                        <asp:Label ID="Label3" runat="server" Text="Fecha Llegada" CssClass="style1"></asp:Label>
                    </td>
                    <td>
                    
                        <asp:Label ID="Label6" runat="server" Text="Vendedor" CssClass="style1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" Width="153px" CssClass="form-control"
                            AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="codcliente" runat="server" CssClass="form-control" Width="105px"
                            AutoPostBack="True" OnTextChanged="codcliente_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="nombrecliente" runat="server" CssClass="form-control" Width="210px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="fechalimite" runat="server" CssClass="form-control" Width="95px"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="fechallegada" runat="server" CssClass="form-control" Width="100px"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="vendedor" runat="server" CssClass="form-control" Width="105px" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <hr />
               <asp:Image ID="Image4" runat="server" ImageUrl="~/2.png" Height="41px" Width="40px"/>
            &nbsp;
            <asp:Label ID="Label17" runat="server" Text="SEGUNDO PASO" CssClass="style1"></asp:Label>
            <h5>
                Inserta los artículos para el aereo seleccionado</h5>
            <hr />
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        &nbsp;&nbsp;<asp:Label ID="Label10" runat="server" Text="Cod. Articulo" CssClass="style1"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                        &nbsp;&nbsp;<asp:Label ID="Label11" runat="server" Text="Descripción" CssClass="style1"></asp:Label>
                    </td>
                    <td>
                        &nbsp;&nbsp;<asp:Label ID="Label12" runat="server" Text="Cantidad" CssClass="style1"></asp:Label>
                    </td>            
                    <td>
                        &nbsp;&nbsp;<asp:Label ID="Label13" runat="server" Text="Precio" CssClass="style1"></asp:Label>
                    </td>
                    <td>
                        &nbsp;&nbsp;<asp:Label ID="Label14" runat="server" Text="Sub Total" CssClass="style1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="codArticulo" runat="server" CssClass="form-control" Width="95px"
                            AutoPostBack="True"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="Buscar_Articulos" runat="server" Text="Buscar Art" CssClass="btn btn-md btn-info"
                            OnClick="Buscar_Articulos_Click1" />
                    </td>
                    <td>
                        <asp:TextBox ID="Descripcion" runat="server" CssClass="form-control" Width="250px"></asp:TextBox>
                    </td>
                    <td>
                     <asp:TextBox ID="Cantidad" runat="server" CssClass="form-control" Width="65px" AutoPostBack="True" OnTextChanged="Cantidad_TextChanged"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                            TargetControlID="Cantidad">
                        </asp:FilteredTextBoxExtender>                        
                    </td>
             
                    <td>
                       <asp:TextBox ID="Precio" runat="server" CssClass="form-control" Width="85px"></asp:TextBox>
                    </td>            
                    <td>
                        <asp:TextBox ID="Sub_Total" runat="server" CssClass="form-control" Width="90px" ReadOnly="true"
                            Text="0.00"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Button ID="SubirArticulo" runat="server" Text="Insertar" CssClass="btn btn-md btn-info"
                            OnClick="SubirArticulo_Click" />
                    </td>
                    <td>
                        <asp:Button ID="Limpiar" runat="server" Text="Limpiar" CssClass="btn btn-md btn-info"
                            OnClick="Limpiar_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateDeleteButton="True"
                CellPadding="4" ForeColor="#333333" PageSize="25" ShowFooter="True" ShowHeaderWhenEmpty="True"
                Width="885px" OnRowDeleting="GridView1_RowDeleting">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#B40404" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#B40404" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#B40404" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <table border="0" cellpadding="0" cellspacing="0" align="right">
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Sub Total" CssClass="style1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="totalsub" runat="server" CssClass="form-control" ReadOnly="true"
                            Text="0.00"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="ITBIS" CssClass="style1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="totalitbis" runat="server" CssClass="form-control" ReadOnly="true"
                            Text="0.00"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="Total Gral.:" CssClass="style1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="totalgral" runat="server" CssClass="form-control" ReadOnly="true"
                            Text="0.00"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br /> 
            <p>
               <asp:Image ID="Image5" runat="server" ImageUrl="~/3.png" Height="41px" Width="40px"/>
            &nbsp;
            <asp:Label ID="Label21" runat="server" Text="TERCER PASO" CssClass="style1"></asp:Label>
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            &nbsp;
                            <asp:Label ID="Label18" runat="server" Text="Subir foto del Pedido o Backorder" CssClass="style1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:AsyncFileUpload ID="AsyncFileUpload1" runat="server" OnUploadedComplete="AsyncFileUpload1_UploadedComplete"
                                ThrobberID="Image1" />
                        </td>
                        <td>
                            <asp:Label ID="lblMsg" runat="server" CssClass="style5" Visible="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Image ID="Image1" runat="server" Height="16px" ImageUrl="~/loading.gif" Visible="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                            <asp:Label ID="Label19" runat="server" Text="Subir Foto de la orden compra (Si Aplica)" CssClass="style1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:AsyncFileUpload ID="AsyncFileUpload2" runat="server" ThrobberID="Image2" OnUploadedComplete="AsyncFileUpload2_UploadedComplete" />
                        </td>
                        <td>
                            <asp:Label ID="Label20" runat="server" CssClass="style5" Visible="true"></asp:Label>
                        </td>
                        <td>
                            <asp:Image ID="Image2" runat="server" Height="16px" ImageUrl="~/loading.gif" Visible="true" />
                        </td>
                    </tr>
                </table>
            </p>
            <br />
            <asp:Image ID="Image6" runat="server" ImageUrl="~/4.png" Height="41px" Width="40px"/>
            &nbsp;
            <asp:Label ID="Label22" runat="server" Text="CUARTO PASO" CssClass="style1"></asp:Label>
            <br />
            <br />
                  <asp:Button ID="Generar" runat="server" Text="Envíar Pedido" CssClass="btn btn-md btn-info"
                OnClick="Generar_Click" OnClientClick="Confirmarenvio()" />
            <asp:Button ID="cancelar" runat="server" Text="Cancelar" CssClass="btn btn-md btn-info"
                OnClick="cancelar_Click" />
            <asp:Button ID="Consultar" runat="server" Text="Consultar Pedidos Especiales" CssClass="btn btn-md btn-info"
                OnClick="Consultar_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
