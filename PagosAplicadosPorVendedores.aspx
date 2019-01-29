<%@ Page Title="Pagos Aplicados de mis Vendedores" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PagosAplicadosPorVendedores.aspx.cs" Inherits="PagosAplicadosPorVendedores" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

    <link href="Scripts/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-2.2.4.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        
    <h4>Desde Esta pantalla podrá generar los pagos de sus vendedores</h4>
    <hr />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Seleccione su Vendedor" Style="color: #FF9933;
                            font-weight: 700; font-size: small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" 
                            Width="192px" AutoPostBack="true" 
                            onselectedindexchanged="DropDownList1_SelectedIndexChanged1">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                        <asp:Button ID="Buscar" runat="server" Text="Buscar" 
                            CssClass="btn btn-md btn-info" onclick="Buscar_Click1"/>
                    </td>
                </tr>
                <tr>
                <td>
                  <asp:Label ID="Label2" runat="server" Style="color: #FF9933; font-weight: 700; font-size: small"></asp:Label>
                </td>
                </tr>
            </table>  

    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

