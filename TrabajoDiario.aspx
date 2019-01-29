<%@ Page Title="Trabajos Diarios" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TrabajoDiario.aspx.cs" Inherits="TrabajoDiario" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<%--    <link href="Scripts/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-2.2.4.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<h3>Desde esta pantalla podra visualizar el trabajo diario de cada uno de sus vendedores</h3>
<hr />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="Label15" runat="server" Text="Seleccione su Vendedor" Style="color: #FF9933;
                            font-weight: 700; font-size: small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control" Width="192px"
                            OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                        <asp:Label ID="Label9" runat="server" Text="" Style="color: #FF9933; font-weight: 700;
                            font-size: small"></asp:Label>
                    </td>
                    <td>
                    &nbsp;
                        <asp:Button ID="Buscar" runat="server" Text="Buscar" CssClass="btn btn-md btn-info" />
                    </td>
                </tr>
            </table>
            <hr />
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="917px">
        </rsweb:ReportViewer>
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

