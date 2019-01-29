<%@ Page Title="Reposición de Gastos" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="ReposicionGastos.aspx.cs" Inherits="ReposicionGastos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-2.2.4.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Confirmarenvio() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("¿Está usted seguro que desea enviar esta reposición de gastos?")) {
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
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h3>
                Reposición de Gastos</h3>
            <h4>
                <i>Desde Esta pantalla podrá realizar el registro de los gastos en los que ha incurrido</i></h4>
            <hr />
            <table border="1" cellpadding="0" cellspacing="0" class="table">
                <tr>
                    <td>
                        <b>
                            <asp:Label ID="Label2" runat="server" Text="Empleado a Reponer: "></asp:Label></b>
                    </td>
                    <td>
                        <b>
                            <asp:Label ID="DropDownList3" runat="server" Text=""></asp:Label>
                        </b>
                    </td>
                    <td>
                        <b>
                            <asp:Label ID="Label7" runat="server" Text="Fecha: "></asp:Label></b>
                    </td>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <table border="1" cellpadding="1" cellspacing="1">
                <tr>
                    <td colspan="2" align="center">
                        <asp:Label ID="Label13" runat="server" Text="Tasa del Día"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <asp:Label ID="Label9" runat="server" Text="DOLARES"></asp:Label></b>
                    </td>
                    <td>
                        <asp:Label ID="Label10" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <asp:Label ID="Label11" runat="server" Text="EUROS"></asp:Label></b>
                    </td>
                    <td>
                        <asp:Label ID="Label12" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <hr />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Listado de Campos Vacíos"
                ForeColor="Red" />
            <table>
                <tr>
                    <td>
                        <b>
                            <asp:Label ID="Label14" runat="server" Text="Fecha"></asp:Label></b>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FechaLiquidacion"
                            ErrorMessage="La fecha es necesaria" ForeColor="Red" Font-Size="Medium" Enabled="false">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        &nbsp;<b><asp:Label ID="Label15" runat="server" Text="RNC"></asp:Label></b>
                    </td>
                    <td>
                        &nbsp;<b><asp:Label ID="Label4" runat="server" Text="Dónde Consumió"></asp:Label></b>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Empresadondecomsumio"
                            ErrorMessage="El establecimiento es necesario" ForeColor="Red" Font-Size="Medium"
                            Enabled="false">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        &nbsp;<b><asp:Label ID="Label1" runat="server" Text="Moneda"></asp:Label></b>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DropDownList4"
                            ErrorMessage="La moneda es necesaria" ForeColor="Red" Font-Size="Medium" Enabled="false">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        &nbsp;<b><asp:Label ID="Label19" runat="server" Text="Concepto"></asp:Label></b>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="DropDownList1"
                            ErrorMessage="El concepto es necesario" ForeColor="Red" Font-Size="Medium" Enabled="false">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        &nbsp;<b><asp:Label ID="Label16" runat="server" Text="NCF"></asp:Label></b>
                    </td>
                    <td>
                        &nbsp;<b><asp:Label ID="Label17" runat="server" Text="Monto"></asp:Label></b>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="MontoLiquida"
                            ErrorMessage="El monto es necesario" ForeColor="Red" Font-Size="Medium" Enabled="false">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="FechaLiquidacion" runat="server" CssClass="form-control" Width="129px"
                            AutoPostBack="True" OnTextChanged="FechaLiquidacion_TextChanged"></asp:TextBox>
                        <asp:CalendarExtender ID="FechaLiquidacion_CalendarExtender" runat="server" Enabled="True"
                            TargetControlID="FechaLiquidacion" Format="MM/dd/yyyy">
                        </asp:CalendarExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="FacturaLiquidacion" runat="server" CssClass="form-control" Width="127px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="Empresadondecomsumio" runat="server" CssClass="form-control" Width="163px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control" OnTextChanged="DropDownList1_TextChanged"
                            Width="70px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>USD</asp:ListItem>
                            <asp:ListItem>EUR</asp:ListItem>
                            <asp:ListItem>DOP</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" AutoPostBack="True"
                            OnTextChanged="DropDownList1_TextChanged">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>Peaje</asp:ListItem>
                            <asp:ListItem>Combustible</asp:ListItem>
                            <asp:ListItem>Dieta Individual</asp:ListItem>
                            <asp:ListItem>Dieta Acompanamiento</asp:ListItem>
                            <asp:ListItem>Dieta Acomp. X3</asp:ListItem>
                            <asp:ListItem>Estadia Hotel</asp:ListItem>
                            <asp:ListItem>Taxi</asp:ListItem>
                            <asp:ListItem>Compras</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="NCFLiquida" runat="server" CssClass="form-control" Width="166px"
                            MaxLength="19"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="MontoLiquida" runat="server" CssClass="form-control" Width="62px"
                            AutoPostBack="True" OnTextChanged="MontoLiquida_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;<b><asp:Label ID="Label3" runat="server" Text="M. Reclamar"></asp:Label></b>
                    </td>
                    <td colspan="3">
                        <b>
                            <asp:Label ID="Label18" runat="server" Text="Acompañante"></asp:Label></b>
                    </td>
                    <td>
                        <b>
                            <asp:Label ID="Label5" runat="server" Text="Total de Gastos"></asp:Label></b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="MontoTotal" runat="server" CssClass="form-control" Width="127px"
                            ReadOnly="True"></asp:TextBox>
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" Enabled="false">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="Totalgastos" runat="server" ReadOnly="True" CssClass="form-control">0.00</asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <table border="0" cellpadding="0" cellspacing="0" class="table">
                <tr>
                    <td>
                        <asp:Button ID="subirfact" runat="server" Text="Subir Factura" CssClass="btn btn-info btn-md"
                            OnClick="subirfact_Click" />
                    </td>
                </tr>
            </table>
            <h3>
                Listado de gastos subidos a la plataforma</h3>
            <asp:GridView ID="GridView1" runat="server" Width="949px" AutoGenerateDeleteButton="True"
                OnRowDeleting="GridView1_RowDeleting" ShowFooter="True">
            </asp:GridView>
            <hr />
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Button ID="Enviarliquidacion" runat="server" Text="Envíar Liquidación" CssClass="btn btn-info btn-md"
                            OnClick="Enviarliquidacion_Click" OnClientClick="Confirmarenvio()" />
                    </td>
                </tr>
            </table>
            <hr />
            <h3>
                <i>Listado de Reposiciones Pendiente de Pagar</i></h3>
            <hr />
            <asp:GridView ID="GridView2" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                Width="928px" OnRowCommand="GridView2_RowCommand">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:ButtonField CommandName="Detalle" HeaderText="Detalle" Text="Detalle" />
                </Columns>
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                <SortedDescendingHeaderStyle BackColor="#820000" />
            </asp:GridView>
            <h5>
                Detalle reposiciones</h5>
            <asp:GridView ID="GridView3" runat="server" Width="928px">
            </asp:GridView>
            <hr />
            <h3>
                <i>Listado de Reposiciones Pagadas</i></h3>
            <hr />
            <asp:GridView ID="GridView4" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                Width="928px" OnRowCommand="GridView4_RowCommand">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:ButtonField CommandName="Detalle" HeaderText="Detalle" Text="Detalle" />
                </Columns>
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                <SortedDescendingHeaderStyle BackColor="#820000" />
            </asp:GridView>
            <h5>
                Detalle reposiciones</h5>
            <asp:GridView ID="GridView5" runat="server" Width="928px">
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
