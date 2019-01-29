<%@ Page Title="Datos de sus Vendedores" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="InformacionVentasVendedor.aspx.cs" Inherits="Principal" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%--    <link href="Scripts/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-2.2.4.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>--%>
    <style>
        .accordionCabecera
        {
            border: 1px solid black;
            background-color: #B40404;
            font-family: Arial, Sans-Serif;
            color: #FFFFFF;
            font-size: 14px;
            font-weight: bold;
            padding: 4px;
            margin-top: 4px;
            cursor: pointer;
        }
        
        .accordionContenido
        {
            font-family: Sans-Serif;
            background-color: #FFFFFF;
            border: 1px solid black;
            border-top: none;
            font-size: 12px;
            padding: 7px;
        }
    </style>
    <style type="text/css">
        .auto-style2
        {
            color: #CC6600;
            font-style: italic;
            font-weight: 700;
        }
        
        .style1
        {
            color: #FFFFFF;
            font-style: italic;
            font-weight: bold;
            font-size: small;
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
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
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
        Desde esta página podrá realizar la búsqueda de las actividades realizadas por sus
        vendedores (as).
    </h3>
    <br />
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
                        <asp:Label ID="Label9" runat="server" Text="" Style="color: #FF9933; font-weight: 700;
                            font-size: small"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:Accordion ID="Accordion1" runat="server" Width="921px" FadeTransitions="True"
                FramesPerSecond="50" TransitionDuration="200" HeaderCssClass="accordionCabecera"
                ContentCssClass="accordionContenido" CssClass="style2" Height="2669px">
                <Panes>
                    <asp:AccordionPane ID="AccordionPane4" runat="server">
                        <Header>
                            Listado de Clientes</Header>
                        <Content>
                            <h4>
                                Cartera de clientes</h4>
                            <hr />
                            <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
                            <asp:GridView ID="GridView4" runat="server" Width="891px" AllowPaging="True" CellPadding="4"
                                ForeColor="#333333" OnRowCommand="GridView4_RowCommand" OnPageIndexChanging="GridView4_PageIndexChanging"
                                CssClass="form-control">
                                <Columns>
                                    <asp:ButtonField CommandName="Estado" Text="Estado" />
                                    <asp:ButtonField CommandName="Estado_RNC" Text="Estado por RNC" />
                                </Columns>
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
                        </Content>
                    </asp:AccordionPane>
                    <asp:AccordionPane runat="server">
                        <Header>
                            Facturación detallada de Clientes</Header>
                        <Content>
                            <h4>
                                Listado de facturas</h4>
                            <hr />
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Seleccione el tipo de Búsqueda" Style="color: #FF6600;
                                            font-weight: 700"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Escriba la información" Style="color: #FF6600;
                                            font-weight: 700"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span>
                                            <asp:DropDownList ID="DropDownList2" runat="server" Width="180px" CssClass="form-control">
                                                <asp:ListItem>Cod. Cliente</asp:ListItem>
                                                <asp:ListItem>Nombre Cliente</asp:ListItem>
                                                <asp:ListItem>RNC / Cedula</asp:ListItem>
                                            </asp:DropDownList>
                                        </span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="DatosBusca" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;<asp:Button ID="Buscar" runat="server" Text="Buscar" OnClick="Buscar_Click"
                                            CssClass="btn btn-md btn-info" />
                                    </td>
                                    <td>
                                        <asp:Button ID="CancelarDatas" runat="server" Text="Cancelar" OnClick="CancelarDatas_Click"
                                            CssClass="btn btn-md btn-info" />
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="GridView3" runat="server" AllowPaging="True" BorderColor="#003366"
                                BorderStyle="Groove" BorderWidth="1px" HorizontalAlign="Left" PageSize="7" Width="916px"
                                OnRowCommand="GridView3_RowCommand" OnPageIndexChanging="GridView3_PageIndexChanging"
                                CssClass="form-control">
                                <Columns>
                                    <asp:ButtonField CommandName="Seleccionar" Text="Seleccionar" />
                                </Columns>
                                <HeaderStyle HorizontalAlign="Left" />
                                <RowStyle BorderColor="#003366" BorderStyle="Groove" BorderWidth="1px" HorizontalAlign="Left" />
                                <SelectedRowStyle BorderColor="#003366" BorderStyle="Groove" BorderWidth="1px" HorizontalAlign="Left" />
                            </asp:GridView>
                            <div runat="server" id="MyDiv" visible="true">
                                <br />
                                <h4>
                                    Facturas</h4>
                                <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" Width="888px"
                                    AllowPaging="true" OnRowCommand="GridView1_RowCommand" OnPageIndexChanging="GridView1_PageIndexChanging"
                                    CssClass="form-control">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:ButtonField CommandName="Detalle" HeaderText="Detalle" Text="Detalle" />
                                        <asp:ButtonField CommandName="Reporte" HeaderText="Reporte" Text="Reporte" />
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
                                <h4>
                                    Detalle de Facturas</h4>
                                <asp:GridView ID="GridView2" runat="server" CellPadding="4" ForeColor="#333333" Width="888px"
                                    CssClass="form-control">
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
                            </div>
                        </Content>
                    </asp:AccordionPane>
                    <asp:AccordionPane ID="AccordionPane2" runat="server">
                        <Header>
                            Facturas Pedendientes de Pago</Header>
                        <Content>
                            <h4>
                                Saldos de Cliente</h4>
                            <hr />
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Escriba el código del cliente" Style="color: #FF6600;
                                            font-weight: 700"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp<asp:Label ID="Label13" runat="server" Text="Total Deuda Cliente" Style="color: #FF6600;
                                            font-weight: 700"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="CodCliente" runat="server" Width="160" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="Busc" runat="server" Text="Buscar" OnClick="Busc_Click" CssClass="btn btn-md btn-info" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp<asp:LinkButton ID="LinkButton1" runat="server" Enabled="false" OnClick="LinkButton1_Click"
                                            Font-Size="X-Large"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                            <hr />
                            <asp:GridView ID="GridView6" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333"
                                ShowFooter="True" ShowHeaderWhenEmpty="True" Width="906px" OnPageIndexChanging="GridView6_PageIndexChanging"
                                CssClass="form-control">
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
                        </Content>
                    </asp:AccordionPane>
                    <asp:AccordionPane ID="AccordionPane3" runat="server">
                        <Header>
                            Información Resumida de Ventas</Header>
                        <Content>
                            <h4>
                                Ventas</h4>
                            <hr />
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Seleccione la fecha inicial" Style="color: #FF6600;
                                            font-weight: 700"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="Seleccione la fecha final" Style="color: #FF6600;
                                            font-weight: 700"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="Escriba los días laborables" Style="color: #FF6600;
                                            font-weight: 700"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="Escriba los días trabajados" Style="color: #FF6600;
                                            font-weight: 700"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="FECHAINICIAL" runat="server" Width="150" CssClass="form-control"></asp:TextBox>
                                        <asp:CalendarExtender ID="FECHAINICIAL_CalendarExtender" runat="server" Enabled="True"
                                            TargetControlID="FECHAINICIAL" Format="MM/dd/yyyy">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="FECHAFINAL" runat="server" Width="150" CssClass="form-control"></asp:TextBox>
                                        <asp:CalendarExtender ID="FECHAFINAL_CalendarExtender" runat="server" Enabled="True"
                                            TargetControlID="FECHAFINAL" Format="MM/dd/yyyy">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="DIASLABORABLES" runat="server" Width="150" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="DIASLABORADOS" runat="server" Width="150" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="BuscarInformacionVentas" runat="server" Text="Buscar" OnClick="BuscarInformacionVentas_Click"
                                            CssClass="btn btn-md btn-info" />
                                    </td>
                                </tr>
                            </table>
                            <hr />
                            <asp:GridView ID="GridView5" runat="server" CellPadding="4" ForeColor="#333333" ShowFooter="True"
                                ShowHeaderWhenEmpty="True" Width="912px" CssClass="form-control">
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
                        </Content>
                    </asp:AccordionPane>
                    <asp:AccordionPane ID="AccordionPane1" runat="server">
                        <Header>
                            Información Detallada de Ventas</Header>
                        <Content>
                            <h4>
                                Diario de Facturas</h4>
                            <hr />
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" Text="Seleccione la fecha inicial" Style="color: #FF6600;
                                            font-weight: 700"></asp:Label>
                                    </td>
                                    &nbsp;
                                    <td>
                                        <asp:Label ID="Label12" runat="server" Text="Seleccione la fecha final" Style="color: #FF6600;
                                            font-weight: 700"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="finicial" runat="server" Width="150" CssClass="form-control"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="finicial"
                                            Format="MM/dd/yyyy">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ffinal" runat="server" Width="150" CssClass="form-control"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="ffinal"
                                            Format="MM/dd/yyyy">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;<asp:Button ID="Busca" runat="server" Text="Buscar" OnClick="Busca_Click"
                                            CssClass="btn btn-md btn-info" />
                                    </td>
                                </tr>
                            </table>
                            <hr />
                            <asp:GridView ID="GridView7" runat="server" CellPadding="4" ForeColor="#333333" ShowFooter="True"
                                ShowHeaderWhenEmpty="True" Width="912px" CssClass="form-control">
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
                        </Content>
                    </asp:AccordionPane>
                </Panes>
            </asp:Accordion>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
