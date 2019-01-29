<%@ Page Title="Actualizaciones de Datos por Vendedores" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="ActualizacionesDatosVendedores.aspx.cs" Inherits="ActualizacionesDatosVendedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .form-control
        {
        }
    </style>
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-2.2.4.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
            <h3>
                Actualizaciones de Datos Realizadas por sus Vendedores</h3>
            <br />
            <h5>
                Desde esta pantalla podrá visualizar todas las actualizaciones de datos de clientes
                que han realizados sus vendedores</h5>
            <hr />
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Seleccione su Vendedor" Style="color: #FF9933;
                            font-weight: 700; font-size: small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" Width="192px"
                            AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                        <asp:Label ID="Label2" runat="server" Text="" Style="color: #FF9933; font-weight: 700;
                            font-size: small"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <h3>
                Listado de Actualizaciones Pendientes</h3>
            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" Width="918px"
                ShowFooter="True" ShowHeaderWhenEmpty="True" CssClass="form-control">
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
            <h3>
                Lisado de Actualizaciones Realizadas</h3>
            <asp:GridView ID="GridView2" runat="server" CellPadding="4" ForeColor="#333333" Width="920px"
                ShowFooter="True" ShowHeaderWhenEmpty="True" CssClass="form-control">
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
