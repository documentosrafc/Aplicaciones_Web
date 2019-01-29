<%@ Page Title="Listado de Contacto" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="ListadeContacto.aspx.cs" Inherits="ListadeContacto" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <h5>
        Esta pantalla le permitirá visualizar la lista de contactos de la empresa</h5>
    <hr />
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
        InteractiveDeviceInfos="(Collection)" ProcessingMode="Remote" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="14pt" Width="912px" ShowBackButton="False" ShowParameterPrompts="False"
        ShowPrintButton="False" ShowRefreshButton="False" ShowWaitControlCancelLink="False"
        ShowZoomControl="False" ZoomMode="PageWidth" Height="1302px">
        <ServerReport ReportPath="/Recursos Humanos/RH_EMP_002_Lista_de_Contacto" ReportServerUrl="http://wurthserver:20000/ReportServer" />
    </rsweb:ReportViewer>
</asp:Content>
