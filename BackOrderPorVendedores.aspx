<%@ Page Title="BackOrder por Vendedor" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="BackOrderPorVendedores.aspx.cs" Inherits="BackOrderPorVendedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-2.2.4.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
    <style type="text/css">
        .form-control
        {
        }
    </style>
    <style type="text/css">
        .auto-style2
        {
            color: #CC6600;
            font-style: italic;
            font-weight: 700;
        }
        
        #Background
        {
            position: fixed;
            top: 0px;
            bottom: 0px;
            left: 0px;
            right: 0px;
            overflow: hidden;
            padding: 0;
            margin: 0;
            background-color: #F0F0F0;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 100000;
        }
        #Progress
        {
            position: fixed;
            top: 40%;
            left: 40%;
            height: 20%;
            width: 20%;
            z-index: 100001;
            background-color: #FFFFFF;
            border: 1px solid Gray;
            background-image: url('PRUEBA.gif');
            background-repeat: no-repeat;
            background-position: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="Background">
            </div>
            <div id="Progress">
                <h6>
                    <p style="text-align: center">
                        <b>Un momento por favor...
                            <br />
                        </b>
                    </p>
                </h6>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h3>
                BackOrder por Vendedores</h3>
            <br />
            <h5>
                Desde esta pantalla podrá generar los backorders de sus vendedores</h5>
            <hr />
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Seleccione su Vendedor" Style="color: #FF9933;
                            font-weight: 700; font-size: small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" Width="192px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                        <asp:Button ID="Button1" runat="server" Text="Generar BackOrder" CssClass="btn btn-md btn-info"
                            OnClick="Button1_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
