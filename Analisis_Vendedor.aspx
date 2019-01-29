<%@ Page Title="Analisis de Vendedores" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Analisis_Vendedor.aspx.cs" Inherits="Analisis_Vendedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-2.2.4.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        function Confirmarenvio() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("¿Está usted segur@ que desea envíar este Analisis?")) {
                confirm_value.value = "Si";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <contenttemplate>
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
            <h4>
                Desde esta pantalla podra reportar el analisis de sus vendedores</h4>
            <hr />
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        &nbsp;
                        <asp:Label ID="Label1" runat="server" Text="Seleccione el Vendedor" Style="color: #FF9900;
                            font-weight: 700; font-style: italic"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" Width="160px" CssClass="form-control">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
            <table border="1" cellpadding="0" cellspacing="0" style="width: 608px">
                <tr>                    
                    <td colspan="6" align="center">
                        <asp:Label ID="Label2" runat="server" Text="Responda los siguientes puntos" Style="color: #FF9900;
                            font-weight: 700; font-style: italic"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>No.</td>                    
                    <td colspan="2" align="center"><asp:Label ID="Label21" runat="server" Text="Temas" Style="color: #FF9900; font-weight: 700;
                            font-style: italic"></asp:Label></td>
                    <td align="center">
                        <asp:Label ID="Label3" runat="server" Text="Fuerte" Style="color: #FF9900; font-weight: 700;
                            font-style: italic"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="Label4" runat="server" Text="A Manejar" Style="color: #FF9900; font-weight: 700;
                            font-style: italic"></asp:Label>
                        <td align="center">
                            <asp:Label ID="Label5" runat="server" Text="Riesgo" Style="color: #FF9900; font-weight: 700;
                                font-style: italic"></asp:Label>
                        </td>
                    </td>
                </tr>
                <tr>
                <td>1</td>
                    <td colspan="2">
                        <asp:Label ID="Label6" runat="server" Text="Uso de Medios" Style="color: #FF9900;
                            font-weight: 700; font-style: italic"></asp:Label>
                    </td>                    
                    <td align="center">
                        <asp:RadioButton ID="RadioButton1" runat="server" AutoPostBack="true" 
                            oncheckedchanged="RadioButton1_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton2" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton2_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton3" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton3_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                <td>2</td>
                    <td colspan="2">
                        <asp:Label ID="Label7" runat="server" Text="Uso de Productos" Style="color: #FF9900;
                            font-weight: 700; font-style: italic"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton4" runat="server" AutoPostBack="true" 
                            oncheckedchanged="RadioButton4_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton5" runat="server" AutoPostBack="true" 
                            oncheckedchanged="RadioButton5_CheckedChanged"/>
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton6" runat="server" AutoPostBack="true" 
                            oncheckedchanged="RadioButton6_CheckedChanged"/>
                    </td>
                </tr>
                <tr>
                <td>3</td>
                    <td colspan="2">
                        <asp:Label ID="Label8" runat="server" Text="Manejo con el Cliente" Style="color: #FF9900;
                            font-weight: 700; font-style: italic"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton7" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton7_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton8" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton8_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton9" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton9_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                <td>4</td>
                    <td colspan="2">
                        <asp:Label ID="Label9" runat="server" Text="Tecnica de Venta" Style="color: #FF9900;
                            font-weight: 700; font-style: italic"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton10" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton10_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton11" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton11_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton12" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton12_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                <td>5</td>
                    <td colspan="2">
                        <asp:Label ID="Label10" runat="server" Text="Gestión de Cobros" Style="color: #FF9900;
                            font-weight: 700; font-style: italic"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton13" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton13_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton14" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton14_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton15" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton15_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                <td>6</td>
                    <td colspan="2">
                        <asp:Label ID="Label11" runat="server" Text="Uso de Procedimientos" Style="color: #FF9900;
                            font-weight: 700; font-style: italic"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton16" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton16_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton17" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton17_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton18" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton18_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                <td>7</td>
                    <td colspan="2">
                        <asp:Label ID="Label12" runat="server" Text="Uso de Promos" Style="color: #FF9900;
                            font-weight: 700; font-style: italic"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton19" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton19_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton20" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton20_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton21" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton21_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                <td>8</td>
                    <td colspan="2">
                        <asp:Label ID="Label13" runat="server" Text="Productos Nuevos" Style="color: #FF9900;
                            font-weight: 700; font-style: italic"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton22" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton22_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton23" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton23_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton24" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton24_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                <td>9</td>
                    <td colspan="2">
                        <asp:Label ID="Label14" runat="server" Text="Liquidator" Style="color: #FF9900;
                            font-weight: 700; font-style: italic"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton25" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton25_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton26" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton26_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton27" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton27_CheckedChanged" />
                    </td>
                </tr>
<%--                <tr>
                <td>10</td>
                    <td colspan="2">
                        <asp:Label ID="Label15" runat="server" Text="Promos" Style="color: #FF9900; font-weight: 700;
                            font-style: italic"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton28" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton28_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton29" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton29_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton30" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton30_CheckedChanged" />
                    </td>
                </tr>--%>
          <%--      <tr>
                <td>11</td>
                    <td colspan="2">
                        <asp:Label ID="Label16" runat="server" Text="Liquidator" Style="color: #FF9900; font-weight: 700;
                            font-style: italic"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton31" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton31_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton32" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton32_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton33" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton33_CheckedChanged" />
                    </td>
                </tr>--%>
                <tr>
                <td>10</td>
                    <td colspan="2">
                        <asp:Label ID="Label17" runat="server" Text="Imagen Personal" Style="color: #FF9900;
                            font-weight: 700; font-style: italic"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton34" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton34_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton35" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton35_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton36" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton36_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                <td>11</td>
                    <td colspan="2">
                        <asp:Label ID="Label18" runat="server" Text="Uso de Fichas" Style="color: #FF9900;
                            font-weight: 700; font-style: italic"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton37" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton37_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton38" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton38_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton39" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton39_CheckedChanged" />
                    </td>
                </tr>                

                 <tr>
                 <td>12</td>
                    <td colspan="2">
                        <asp:Label ID="Label19" runat="server" Text="Cotización y Seguimiento" Style="color: #FF9900;
                            font-weight: 700; font-style: italic"></asp:Label>
                    </td>                    
                    <td align="center">
                        <asp:RadioButton ID="RadioButton40" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton40_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton41" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton41_CheckedChanged" />
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton42" runat="server" AutoPostBack="true"
                            oncheckedchanged="RadioButton42_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                <td>13</td>
                    <td colspan="2">
                        <asp:Label ID="Label20" runat="server" Text="Comentarios" Style="color: #FF9900;
                            font-weight: 700; font-style: italic"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="Comentarios" runat="server" Width="500px" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
          
                <tr>
                <td>14</td>
                    <td colspan="2">
                        <asp:Label ID="Label22" runat="server" Text="Acción a tomar" Style="color: #FF9900;
                            font-weight: 700; font-style: italic"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="AccionTomar" runat="server" Width="500px" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>

                      <tr>
                <td>15</td>
                    <td colspan="2">
                        <asp:Label ID="Label23" runat="server" Text="Tiempo Limite" Style="color: #FF9900;
                            font-weight: 700; font-style: italic"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="TipoLimite" runat="server" Width="500px" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
            </table>        
            <br />
            <asp:Button ID="Enviar" runat="server" Text="Enviar" class="btn btn-info btn-md" onclick="Enviar_Click" OnClientClick="Confirmarenvio()"/>
            <asp:Button ID="Cancelar" runat="server" Text="Cancelar" class="btn btn-info btn-md" onclick="Cancelar_Click" />
        </contenttemplate>
        </asp:UpdatePanel>
</asp:Content>
