<%@ Page Title="Registro de Pagos" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="DepositosVendedor.aspx.cs" Inherits="DepositosVendedor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-2.2.4.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Confirmarenvio() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("¿Está seguro que desea enviar estos pagos a ser validados por CXC?")) {
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
        .style2
        {
            color: #FF9900;
            font-weight: bold;
        }
    </style>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
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
            <h2>
                Registro de pagos</h2>
            <hr />
            <h3>
                Desde esta pantalla podrá realizar el registro de los pagos de cada uno de sus clientes</h3>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    Espere por favor...</ProgressTemplate>
            </asp:UpdateProgress>
            <br />
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Cod. Cliente" Style="color: #FF9900;
                            font-size: small; font-weight: 700"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="codcliente" runat="server" Width="183px" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;<asp:Button ID="buscar" runat="server" Text="Buscar" OnClick="buscar_Click"
                            CssClass="btn btn-info btn-md" />
                    </td>
                </tr>
            </table>
            <hr />
            <h3>
                Listado de Facturas por cobrar</h3>
            <hr />
            <asp:GridView ID="GridView1" runat="server" Width="918px" AllowPaging="True" ShowHeaderWhenEmpty="True"
                OnPageIndexChanging="GridView1_PageIndexChanging">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Text="Todos" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox2" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Monto a Pagar
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="MontoPagado" runat="server" CssClass="form-control"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <hr />
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Monto a Pagar" CssClass="style2"></asp:Label>
                    </td>
                    <td>
                        &nbsp;<asp:Label ID="Label3" runat="server" Text="Recibo de Cobro" CssClass="style2"></asp:Label>
                    </td>
                    <td>
                        &nbsp;<asp:Label ID="Label4" runat="server" Text="Tipo de Pago" CssClass="style2"></asp:Label>
                    </td>
                    <td>
                        &nbsp;<asp:Label ID="Label7" runat="server" Text="No. Cuenta" CssClass="style2"></asp:Label>
                    </td>
                    <td>
                        &nbsp;<asp:Label ID="Label5" runat="server" Text="No. Cheque" CssClass="style2"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="MontoPagar" runat="server" ReadOnly="True" CssClass="form-control">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ReciboCobro" runat="server" OnTextChanged="ReciboCobro_TextChanged"
                            CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" Width="150px" AutoPostBack="True"
                            OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="form-control">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>Efectivo</asp:ListItem>
                            <asp:ListItem>Cheque</asp:ListItem>
                            <asp:ListItem>Tarjeta Credito</asp:ListItem>
                            <asp:ListItem>Transferencia</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList2" runat="server" Width="150px" CssClass="form-control"
                            AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="702-55627-5">Popular Peso</asp:ListItem>
                            <asp:ListItem Value="718-75798-2">Popular Dolar</asp:ListItem>
                            <asp:ListItem Value="2192982-001-7">BHD Leon Peso</asp:ListItem>
                            <asp:ListItem Value="2192982-002-5">BHD Leon Dolar</asp:ListItem>
                            <asp:ListItem Value="240-014077-9">BanReservas Peso</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="nocheque" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Banco" CssClass="style2"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="Banconombre" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <br />
                        <asp:Button ID="SubirPagos" runat="server" Text="Subir Pagos" OnClick="SubirPagos_Click"
                            CssClass="btn btn-info btn-md" />
                    </td>
                    <td>
                        <br />
                        &nbsp;<asp:Button ID="limpiar" runat="server" Text="Limpiar Búsqueda" OnClick="limpiar_Click"
                            CssClass="btn btn-info btn-md" />
                    </td>
                </tr>
            </table>
            <hr />
            <h3>
                Listado de Pagos Subidos</h3>
            <asp:GridView ID="GridView2" runat="server" CellPadding="4" ForeColor="#333333" ShowFooter="True"
                ShowHeaderWhenEmpty="True" Width="915px" AutoGenerateDeleteButton="True" OnRowDeleting="GridView2_RowDeleting">
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
            <hr />
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Button ID="enviarpagos" runat="server" Text="Enviar Pagos a CXC" CssClass="btn btn-info btn-md"
                            OnClick="enviarpagos_Click" OnClientClick="Confirmarenvio()" />
                    </td>
                    <td>
                        &nbsp;<asp:Button ID="Cancelar" runat="server" Text="Cancelar Proceso" CssClass="btn btn-info btn-md"
                            OnClick="Cancelar_Click" />
                    </td>
                </tr>
            </table>
            <hr />
            <strong>
                <h3>
                    Estados de depósito en cuentas por cobrar</h3>
            </strong>
            <hr />
            <asp:GridView ID="GridView3" runat="server" AllowPaging="True" OnPageIndexChanging="GridView3_PageIndexChanging"
                ShowFooter="True" ShowHeaderWhenEmpty="True" Width="911px" OnRowCommand="GridView3_RowCommand">
                <Columns>
                    <asp:ButtonField CommandName="Detalle" HeaderText="Detalle" Text="Detalle" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:GridView ID="GridView4" runat="server" ShowHeaderWhenEmpty="True" Width="911px">
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
