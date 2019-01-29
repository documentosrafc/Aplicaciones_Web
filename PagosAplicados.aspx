<%@ Page Title="Pagos Aplicados" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PagosAplicados.aspx.cs" Inherits="PagosAplicados" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<h4><strong>Desde esta pantalla podrá ver los pagos que se han aplicados a sus clientes</strong></h4>
<br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>        
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="1093px" 
        Width="914px" Font-Names="Verdana" Font-Size="8pt" 
        InteractiveDeviceInfos="(Collection)" ProcessingMode="Remote" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" 
            ShowBackButton="False" ShowCredentialPrompts="False" 
            ShowDocumentMapButton="False" ShowFindControls="False" 
            ShowParameterPrompts="False" ShowPrintButton="False" ShowRefreshButton="False" 
            ShowWaitControlCancelLink="False" ShowZoomControl="False">
        <ServerReport ReportPath="/Reportes_Aplicaciones/Consulta Vendedores/CON_VEN_008 Cobros Aplicados por Vendedores" 
            ReportServerUrl="http://wurthserver:20000/ReportServer" />
    </rsweb:ReportViewer>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

