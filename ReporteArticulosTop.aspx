<%@ Page Title="Top 100 Articulos Vendidos por Mi" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ReporteArticulosTop.aspx.cs" Inherits="ReporteArticulosTop" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-2.2.4.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>

    <style type="text/css">
        .style1
        {
            color: #FF9900;
            font-weight: bold;
            font-style: italic;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td> &nbsp;<asp:Label ID="Label1" runat="server" Text="Seleccione la Fecha Inicial" 
                        CssClass="style1"></asp:Label> </td>
                <td> &nbsp;<asp:Label ID="Label2" runat="server" Text="Seleccione la Fecha Final" 
                        CssClass="style1"></asp:Label> </td>
            </tr>
            <tr>
            <td> <asp:TextBox ID="FechaInicial" runat="server" Width="190px" CssClass="form-control"></asp:TextBox> 
            <asp:CalendarExtender ID="FechaInicial_CalendarExtender" runat="server" Enabled="True"
             TargetControlID="FechaInicial"></asp:CalendarExtender>
            </td>
            <td> <asp:TextBox ID="FechaFinal" runat="server" Width="190px" CssClass="form-control"></asp:TextBox> 
             <asp:CalendarExtender ID="FechaFinal_CalendarExtender" runat="server" Enabled="True"
             TargetControlID="FechaFinal"></asp:CalendarExtender>
            </td>
            <td><asp:Button ID="Generar" runat="server" Text="Generar Reporte" 
                    CssClass="btn btn-md btn-info" onclick="Generar_Click" /> </td>
            </tr>
        </table>
        <hr />      
    </ContentTemplate>
    </asp:UpdatePanel>
         <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
        Height="703px" InteractiveDeviceInfos="(Collection)" ProcessingMode="Remote"
        ShowBackButton="False" ShowCredentialPrompts="False" ShowDocumentMapButton="False"
        ShowParameterPrompts="False" ShowRefreshButton="False" ShowZoomControl="False"
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="934px" ZoomMode="PageWidth">
        <ServerReport ReportPath="/Reportes_Aplicaciones/Consulta Vendedores/CON_VEN_009_Top 100 Articulos que mas se Venden por Vendedor"
            ReportServerUrl="http://wurthserver:20000/ReportServer" />
    </rsweb:ReportViewer>
</asp:Content>

