<%@ Page Title="Reporte de Cotizacion" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="cotizaciones.aspx.cs" Inherits="cotizaciones" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" Height="721px" InteractiveDeviceInfos="(Collection)" 
        ProcessingMode="Remote" WaitMessageFont-Names="Verdana" 
        WaitMessageFont-Size="14pt" Width="927px" ShowBackButton="False" 
        ShowCredentialPrompts="False" ShowDocumentMapButton="False" 
        ShowFindControls="False" ShowParameterPrompts="False" ShowRefreshButton="False" 
        ShowWaitControlCancelLink="False" ShowZoomControl="False">
        <ServerReport ReportPath="/Reportes_Aplicaciones/Consulta Vendedores/CON_VEN_012 Cotizaciones" 
            ReportServerUrl="http://wurthserver:20000/ReportServer" />
    </rsweb:ReportViewer>
</asp:Content>

