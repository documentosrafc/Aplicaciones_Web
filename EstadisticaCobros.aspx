<%@ Page Title="Estadistica de Cobros" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="EstadisticaCobros.aspx.cs" Inherits="EstadisticaCobros" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                Height="641px" InteractiveDeviceInfos="(Collection)" InternalBorderStyle="NotSet"
                ProcessingMode="Remote" ShowBackButton="False" ShowCredentialPrompts="False"
                ShowDocumentMapButton="False" ShowFindControls="False" ShowParameterPrompts="False"
                ShowPrintButton="False" ShowRefreshButton="False" ShowWaitControlCancelLink="False"
                ShowZoomControl="False" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                Width="914px" ZoomMode="PageWidth">
                <ServerReport ReportServerUrl="http://wurthserver:20000/ReportServer" ReportPath="/Reportes_Aplicaciones/Consulta Vendedores/CON_VEN_003 Grafico_Cobros Comparativo" />
            </rsweb:ReportViewer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
