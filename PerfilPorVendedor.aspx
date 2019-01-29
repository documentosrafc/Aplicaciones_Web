<%@ Page Title="Perfil de mi vendedor" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PerfilPorVendedor.aspx.cs" Inherits="PerfilPorVendedor" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
  <link href="Scripts/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-2.2.4.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <h4>Desde esta pantalla podrá generar el perfil de sus vendedores</h4>
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Seleccione su vendedor" Style="color: #FF9900;
                    font-style: italic; font-weight: 700"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server" Width="205px" CssClass="form-control">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;<asp:Button ID="buscar" runat="server" Text="Buscar" CssClass="btn btn-md btn-info"
                    OnClick="buscar_Click" />
            </td>
        </tr>
    </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" Height="735px" InteractiveDeviceInfos="(Collection)" 
        ProcessingMode="Remote" ShowBackButton="False" ShowCredentialPrompts="False" 
        ShowDocumentMapButton="False" ShowFindControls="False" 
        ShowPageNavigationControls="False" ShowParameterPrompts="False" 
        ShowPrintButton="False" ShowRefreshButton="False" 
        ShowWaitControlCancelLink="False" ShowZoomControl="False" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="929px" 
        ZoomMode="PageWidth">
        <ServerReport ReportPath="/Reportes_Aplicaciones/Consulta Vendedores/CON_VEN_006_Perfil del Vendedor" 
            ReportServerUrl="http://wurthserver:20000/ReportServer" />
    </rsweb:ReportViewer>
        </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

