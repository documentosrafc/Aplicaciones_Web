<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Detalle_Pedidos_Especiales.aspx.cs" Inherits="Detalle_Pedidos_Especiales" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-2.2.4.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h4>
                Desde esta pantalla podra visualizar todos los pedidos especiales</h4>
            <hr />
            <h5>
                Listado de Pedidos Especiales</h5>
            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" Width="912px"
                ShowFooter="True" ShowHeaderWhenEmpty="True" OnRowCommand="GridView1_RowCommand">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:ButtonField CommandName="Detalle" HeaderText="Detalle" Text="Detalle" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#B40404" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#B40404" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#B40404" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <h5>
                Detalle de Pedido</h5>
            <asp:GridView ID="GridView2" runat="server" CellPadding="4" ForeColor="#333333" Width="912px"
                ShowFooter="True" ShowHeaderWhenEmpty="True">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#B40404" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#B40404" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#B40404" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
