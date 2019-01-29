<%@ Page Title="Listado de BackOrders" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="BackOrders.aspx.cs" Inherits="BackOrders" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <h3>
        En esta pantalla podrá encontrar un reporte con el listado de los artículos que
        se han quedado en BackOrder
    </h3>
    <hr />
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
        Height="703px" InteractiveDeviceInfos="(Collection)" ProcessingMode="Remote"
        ShowBackButton="False" ShowCredentialPrompts="False" ShowDocumentMapButton="False"
        ShowParameterPrompts="False" ShowRefreshButton="False" ShowZoomControl="False"
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="934px" ZoomMode="PageWidth">
        <ServerReport ReportPath="/Reportes_Aplicaciones/Consulta Vendedores/CON_VEN_002_BackOrders"
            ReportServerUrl="http://wurthserver:20000/ReportServer" />
    </rsweb:ReportViewer>
</asp:Content>
