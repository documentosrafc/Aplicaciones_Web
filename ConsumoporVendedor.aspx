<%@ Page Title="Consumo por Vendedor y Cliente" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="ConsumoporVendedor.aspx.cs" Inherits="ConsumoporVendedor" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-2.2.4.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                &nbsp;<asp:Label ID="Label1" runat="server" Text="Seleccione su vendedor" Style="color: #FF9900;
                    font-style: italic; font-weight: 700"></asp:Label>
            </td>
              <td colspan="2">
                &nbsp;<asp:Label ID="Label2" runat="server" Text="Escriba el Cod. Cliente" Style="color: #FF9900;
                    font-style: italic; font-weight: 700"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server" Width="205px" CssClass="form-control">
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="codcliente" runat="server" CssClass="form-control"></asp:TextBox>
            </td>
            <td>
                &nbsp;<asp:Button ID="buscar" runat="server" Text="Buscar" CssClass="btn btn-md btn-info"
                    OnClick="buscar_Click" />
            </td>
        </tr>
    </table>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                Height="910px" InteractiveDeviceInfos="(Collection)" ProcessingMode="Remote"
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="914px" ZoomMode="FullPage"
                ShowBackButton="False" ShowCredentialPrompts="False" ShowDocumentMapButton="False"
                ShowFindControls="true" ShowPageNavigationControls="true" ShowParameterPrompts="False"
                ShowPromptAreaButton="False" ShowRefreshButton="False" ShowZoomControl="False"
                ShowPrintButton="False" ShowWaitControlCancelLink="False">
                <ServerReport ReportServerUrl="http://wurthserver:20000/ReportServer" ReportPath="/Reportes_Aplicaciones/Consulta Vendedores/CON_VEN_005_Consumo de Clientes_por_vendedores" />
            </rsweb:ReportViewer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
