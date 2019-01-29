<%@ Page Title="Previsión fin Mes" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="PrevisionFinMes.aspx.cs" Inherits="PrevisionFinMes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style1
        {
            color: #FF9933;
            font-weight: bold;
            font-size: small;
        }
        .style2
        {
            font-style: italic;
            font-weight: bold;
            color: #FFFFFF;
        }
    </style>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-2.2.4.min.js" type="text/javascript"></script>
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function Confirmarenvio() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("¿Está usted seguro que desea guardar esta información?")) {
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
                Previsión para fin de Mes</h3>
            <hr />
            <br />
            <br />
            <table border="1" cellpadding="0" cellspacing="0" class="table-bordered">
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Mes:" CssClass="style1"></asp:Label>
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label4" runat="server" Text="Fecha:" CssClass="style1"></asp:Label>
                    </td>
                    <td>
                        &nbsp;&nbsp;
                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
            </table>
            <hr />
            <table border="2" cellpadding="0" cellspacing="0">
                <tr bgcolor="#B40404">
                    <td colspan="3" align="center">
                        <asp:Label ID="Label5" runat="server" Text="Objetivos Mes de Ventas y Cobros" CssClass="style2"></asp:Label>
                    </td>
                    <td colspan="2" align="center">
                        <asp:Label ID="Label9" runat="server" Text="Pedidos" CssClass="style2"></asp:Label>
                    </td>
                    <td colspan="3" align="center">
                        <asp:Label ID="Label12" runat="server" Text="Clientes" CssClass="style2"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Ventas" CssClass="style1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Cobros" CssClass="style1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="GP" CssClass="style1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="Pedidos" CssClass="style1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="Posiciones" CssClass="style1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label13" runat="server" Text="Activos" CssClass="style1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label14" runat="server" Text="Nuevos" CssClass="style1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label15" runat="server" Text="Recuperados" CssClass="style1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="ventas" runat="server" CssClass="form-control" Width="94px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="cobros" runat="server" CssClass="form-control" Width="94px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="gp" runat="server" CssClass="form-control" Width="94px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="pedidos" runat="server" CssClass="form-control" Width="94px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="posiciones" runat="server" CssClass="form-control" Width="94px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="cactivos" runat="server" CssClass="form-control" Width="102px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="cnuevos" runat="server" CssClass="form-control" Width="102px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="crecuperados" runat="server" CssClass="form-control" Width="150px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Button ID="Guardar" runat="server" Text="Guardar" CssClass="btn btn-md btn-info"
                            OnClick="Guardar_Click" OnClientClick="Confirmarenvio()" />
                    </td>
                    <td>
                        <asp:Button ID="Cancelar" runat="server" Text="Cancelar" CssClass="btn btn-md btn-info"
                            OnClick="Cancelar_Click" />
                    </td>
                </tr>
            </table>
            <hr />
            <h4>
                Lista de Previsiones Mensuales</h4>
            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" Width="937px"
                OnRowCommand="GridView1_RowCommand" ShowFooter="True" ShowHeaderWhenEmpty="True">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:ButtonField CommandName="Modificar" HeaderText="Modificar" Text="Modificar" />
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
            <hr />
            <h4>
                Historico de Modificaciones</h4>
            <asp:GridView ID="GridView2" runat="server" CellPadding="4" ForeColor="#333333" ShowFooter="True"
                ShowHeaderWhenEmpty="True" Width="932px">
                <AlternatingRowStyle BackColor="White" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
