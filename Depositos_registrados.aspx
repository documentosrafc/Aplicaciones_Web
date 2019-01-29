<%@ Page Title="Depositos Registrados por Vendedores" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="Depositos_registrados.aspx.cs" Inherits="Depositos_registrados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-2.2.4.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
    <style type="text/css">
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
    <asp:UpdateProgress ID="UpdateProgress5" runat="server">
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
    <h3>
        Desde esta página podrá realizar la búsqueda de los depositos reagistrados por sus
        vendedores (as).
    </h3>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="Label15" runat="server" Text="Seleccione su Vendedor" Style="color: #FF9933;
                            font-weight: 700; font-size: small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control" Width="192px"
                            OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                        <asp:Label ID="Label1" runat="server" Text="" Style="color: #FF9933; font-weight: 700;
                            font-size: small"></asp:Label>
                    </td>
                </tr>
            </table>
            <hr />
            <strong>
                <h3>
                    Estados de depósito en cuentas por cobrar</h3>
            </strong>
            <hr />
            <asp:GridView ID="GridView3" runat="server" AllowPaging="True" ShowFooter="True"
                ShowHeaderWhenEmpty="True" Width="911px">
                <Columns>
                    <asp:ButtonField CommandName="Detalle" HeaderText="Detalle" Text="Detalle" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:GridView ID="GridView4" runat="server" ShowHeaderWhenEmpty="True" Width="911px">
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
