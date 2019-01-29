<%@ Page Title="Lista de Precios Wurth" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="ListadePrecios.aspx.cs" Inherits="ListadePrecios" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <h5>
        Lista de Precios Wurth Dominicana</h5>
    <hr />
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
        InteractiveDeviceInfos="(Collection)" ProcessingMode="Remote" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="14pt" Width="912px" ShowBackButton="False" ShowParameterPrompts="False"
        ShowPrintButton="False" ShowRefreshButton="False" ShowWaitControlCancelLink="False"
        ShowZoomControl="False" ZoomMode="PageWidth" Height="1302px">
        <ServerReport ReportPath="/Reportes_Aplicaciones/Consulta Vendedores/CON_VEN_001_Lista_de_Precios"
            ReportServerUrl="http://wurthserver:20000/ReportServer" />
    </rsweb:ReportViewer>
</asp:Content>
