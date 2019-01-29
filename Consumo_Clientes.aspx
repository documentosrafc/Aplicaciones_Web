<%@ Page Title="Consumo de Clientes" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Consumo_Clientes.aspx.cs" Inherits="Consumo_Clientes" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-2.2.4.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>

     <style type="text/css">            
            #Background
        {
            position: fixed;
            top: 0px;
            bottom: 0px;
            left : 0px;
            right: 0px;
            overflow: hidden;
            padding : 0;
            margin: 0;
            background-color: #F0F0F0;            
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index:100000;
        }
        #Progress
        {
            position: fixed;
            top:40%;
            left:40%;
            height:20%;
            width:20%;
            z-index:100001;
            background-color:#FFFFFF;
            border:1px solid Gray;
            background-image:url('PRUEBA.gif');
            background-repeat: no-repeat;            
            background-position:center;
        }          
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <h3>Consumo de Clientes</h3>
    <h5>Desde esta pantalla podrá visualizar el consumo de los clientes, solo tiene que escribir el codigo del cliente.</h5>
    <hr />
    
    <asp:UpdateProgress ID="UpdateProgress5" runat="server">
    <ProgressTemplate> 
    <div id="Background"></div>
    <div id="Progress">
    <h6><p style="text-align:center"><b>Un momento por favor... <br /></b></p></h6>
    </div>  
    </ProgressTemplate>
    </asp:UpdateProgress>
    
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td><asp:Label ID="Label1" runat="server" Text="Escriba el Cod. del Cliente" style="color: #FF9933; font-weight: 700; font-size: small"></asp:Label></td>
            </tr>
            <tr>
            <td><asp:TextBox ID="codcliente" runat="server" CssClass="form-control"></asp:TextBox></td>     
            <td>&nbsp; <asp:Button ID="gconsumo" runat="server" Text="Generar Consumo" 
                    CssClass="btn btn-md btn-info" onclick="gconsumo_Click" /> </td>
            </tr>
        </table><br />

        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="918px" 
            Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
            ProcessingMode="Remote" ShowBackButton="False" 
            ShowCredentialPrompts="False" ShowWaitControlCancelLink="False" 
            ShowZoomControl="False" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="14pt" ZoomMode="PageWidth" Height="836px" 
            ShowParameterPrompts="False" ShowPrintButton="False" ShowRefreshButton="False">
            <ServerReport ReportServerUrl="http://wurthserver:20000/ReportServer"
            ReportPath="/Reportes_Aplicaciones/Consulta Vendedores/CON_VEN_005_Consumo de Clientes_por_vendedores" />                
        </rsweb:ReportViewer> 
    
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

