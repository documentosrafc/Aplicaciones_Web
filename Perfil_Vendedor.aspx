<%@ Page Title="Perfil de Vendedor" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Perfil_Vendedor.aspx.cs" Inherits="Perfil_Vendedor" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
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
</asp:Content>

