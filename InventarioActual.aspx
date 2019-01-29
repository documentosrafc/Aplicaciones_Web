<%@ Page Title="Inventario Actual" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="InventarioActual.aspx.cs" Inherits="InventarioActual" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="917px" Font-Names="Verdana"
                Font-Size="8pt" Height="1439px" InteractiveDeviceInfos="(Collection)" ProcessingMode="Remote"
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" ZoomMode="PageWidth"
                ShowBackButton="False" ShowCredentialPrompts="False" ShowDocumentMapButton="False"
                ShowFindControls="true" ShowPageNavigationControls="true" ShowParameterPrompts="False"
                ShowPromptAreaButton="False" ShowRefreshButton="False" ShowZoomControl="true">
                <ServerReport ReportPath="/Operaciones/Inventario/OPE_INV_001_Existencia de Productos"
                    ReportServerUrl="http://wurthserver:20000/ReportServer" />
            </rsweb:ReportViewer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
