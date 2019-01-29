<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ControlDiarioCliente.aspx.cs" Inherits="ControlDiarioCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style1
        {
            color: #FF9900;
            font-weight: bold;
            font-size: small;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <h3>Control Diario de Ventas</h3>
    <h4>En esta pantalla podrás registrar todos los días las informaciones que tienen que ver con los resultados del día</h4>
    <hr />

        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td> <asp:Label ID="Label1" runat="server" Text="Mes: " CssClass="style1"></asp:Label> </td>
                <td>&nbsp;&nbsp;<asp:Label ID="Label4" runat="server" Text="Label"></asp:Label></td>
                <td>&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="Vendedor: " CssClass="style1"></asp:Label> </td>
                <td>&nbsp;&nbsp;<asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></td>
            </tr>
        </table>
        <hr />
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                </td>
            </tr>
        </table>
    
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

