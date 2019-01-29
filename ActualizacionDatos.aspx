<%@ Page Title="Actualización de Datos" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="ActualizacionDatos.aspx.cs" Inherits="ActualizacionDatos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-2.2.4.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 221px;
        }
    </style>
    <script type="text/javascript">
        function Confirmarenvio() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("¿Está seguro que desea enviar estos nuevos datos de cliente para que sean actualizados?")) {
                confirm_value.value = "Si";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h3>
                Actualización de Datos</h3>
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
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Escriba el Código del Cliente" Style="font-weight: 700;
                            color: #FF9933"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="nocliente" runat="server" Width="211px" CssClass="form-control"></asp:TextBox>
                        <asp:TextBoxWatermarkExtender ID="nocliente_TextBoxWatermarkExtender" runat="server"
                            Enabled="True" TargetControlID="nocliente" WatermarkText="Ejemplo 305, 11525, 6356">
                        </asp:TextBoxWatermarkExtender>
                    </td>
                    <td>
                        &nbsp;<asp:Button ID="BuscarCliente" runat="server" Text="Buscar" Width="115px" CssClass="btn btn-info btn-md"
                            OnClick="BuscarCliente_Click" />
                    </td>
                </tr>
            </table>
            <hr />
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Código del Cliente" Style="font-weight: 700;
                            color: #FF9933"></asp:Label>
                    </td>
                    <td colspan="2">
                        &nbsp;<asp:Label ID="Label3" runat="server" Text="Nombre del Cliente" Style="font-weight: 700;
                            color: #FF9933"></asp:Label>
                    </td>
                    <td>
                        &nbsp;<asp:Label ID="Label6" runat="server" Text="Razón Social" Style="font-weight: 700;
                            color: #FF9933"></asp:Label>
                    </td>
                    <td>
                        &nbsp;<asp:Label ID="Label7" runat="server" Text="Cod. Vendedor" Style="font-weight: 700;
                            color: #FF9933"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="CodigoCliente" runat="server" MaxLength="10" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="Nombrecliente" runat="server" Width="317px" CssClass="form-control"
                            MaxLength="50"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="RazonSocial" runat="server" Width="186px" MaxLength="50" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="Codvendedor" runat="server" MaxLength="10" CssClass="form-control"
                            ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Style="font-weight: 700; color: #FF9933" Text="Teléfono"></asp:Label>
                    </td>
                    <td>
                        &nbsp;<asp:Label ID="Label5" runat="server" Style="font-weight: 700; color: #FF9933"
                            Text="Fax"></asp:Label>
                    </td>
                    <td>
                        &nbsp;<asp:Label ID="Label8" runat="server" Style="font-weight: 700; color: #FF9933"
                            Text="Email"></asp:Label>
                    </td>
                    <td>
                        &nbsp;<asp:Label ID="Label9" runat="server" Style="font-weight: 700; color: #FF9933"
                            Text="RNC ó Cédula"></asp:Label>
                    </td>
                    <td>
                        &nbsp;<asp:Label ID="Label10" runat="server" Style="font-weight: 700; color: #FF9933"
                            Text="Gerente Comercial"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="Telefono" runat="server" MaxLength="13" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="Fax" runat="server" Width="131px" MaxLength="13" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="Email" runat="server" Width="188px" MaxLength="75" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="RNC" runat="server" Width="186px" MaxLength="13" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="Gerentecomercial" runat="server" MaxLength="25" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="Label11" runat="server" Style="font-weight: 700; color: #FF9933" Text="Nombre del Financiero"></asp:Label>
                    </td>
                    <td>
                        &nbsp;<asp:Label ID="Label12" runat="server" Style="font-weight: 700; color: #FF9933"
                            Text="Forma de Pago"></asp:Label>
                    </td>
                    <td>
                        &nbsp;<asp:Label ID="Label13" runat="server" Style="font-weight: 700; color: #FF9933"
                            Text="Días Pago"></asp:Label>
                    </td>
                    <td>
                        &nbsp;<asp:Label ID="Label14" runat="server" Style="font-weight: 700; color: #FF9933"
                            Text="Sector"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="Financiero" runat="server" Width="197px" MaxLength="25" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" Width="190px" CssClass="form-control">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="Cash">Efectivo</asp:ListItem>
                            <asp:ListItem>Cheque</asp:ListItem>
                            <asp:ListItem Value="Cred. Card">Tarjeta</asp:ListItem>
                            <asp:ListItem Value="Tranfer">Transferencia</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="Dipagos" runat="server" Width="184px" MaxLength="2" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="SECTOR" runat="server" Width="178px" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;<asp:Label ID="Label17" runat="server" Style="font-weight: 700; color: #FF9933"
                            Text="División"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label18" runat="server" Style="font-weight: 700; color: #FF9933" Text="Sub-División"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label19" runat="server" Style="font-weight: 700; color: #FF9933" Text="No. Empleados"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="DropDownList2" runat="server" Width="199px" CssClass="form-control">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList3" runat="server" Width="191px" CssClass="form-control">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="NOEMPLEADO" runat="server" Width="178px" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="4">
                        <asp:Label ID="Label15" runat="server" Style="font-weight: 700; color: #FF9933" Text="Dirección"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:TextBox ID="direccion" runat="server" Width="720px" MaxLength="150" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="Label16" runat="server" Style="font-weight: 700; color: #FF9933" Text="Comentario"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:TextBox ID="Comentario" runat="server" Width="720px" MaxLength="150" CssClass="form-control"
                            Height="59px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Seleccione la Imagen de la Actualización:
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:AsyncFileUpload ID="AsyncFileUpload1" runat="server" CssClass="form-control"
                            OnUploadedComplete="AsyncFileUpload1_UploadedComplete" />
                    </td>
                </tr>
            </table>
            <br />
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Button ID="enviar" runat="server" Text="Enviar a Actualizar" CssClass="btn btn-info btn-md"
                            OnClick="enviar_Click" OnClientClick="Confirmarenvio()" />
                    </td>
                    <td>
                        &nbsp;<asp:Button ID="Cancelar" runat="server" Text="Cancelar" CssClass="btn btn-info btn-md"
                            OnClick="Cancelar_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Listado de Clientes Pendiente de Actualizar</asp:LinkButton><br />
            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">Listado de Clientes Actualizados</asp:LinkButton>
            <br />
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333"
                ShowFooter="True" ShowHeaderWhenEmpty="True" Visible="False" Width="913px">
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
            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" Visible="False">Cerrar Resultados</asp:LinkButton>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
