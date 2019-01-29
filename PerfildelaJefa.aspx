<%@ Page Title="Perfil de la Jefa" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PerfildelaJefa.aspx.cs" Inherits="PerfildelaJefa" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="1093px" 
        Width="914px" Font-Names="Verdana" Font-Size="8pt" 
        InteractiveDeviceInfos="(Collection)" ProcessingMode="Remote" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" 
            ShowBackButton="False" ShowCredentialPrompts="False" 
            ShowDocumentMapButton="False" ShowFindControls="False" 
            ShowParameterPrompts="true" ShowPrintButton="False" ShowRefreshButton="False" 
            ShowWaitControlCancelLink="False" ShowZoomControl="False">
            <ServerReport ReportPath="/Gerencia/GEN_GEN_004_Perfil del Jefe" 
                ReportServerUrl="http://wurthserver:20000/ReportServer" />
        </rsweb:ReportViewer>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

