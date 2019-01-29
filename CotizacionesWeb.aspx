<%@ Page Title="Cotizaciones" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CotizacionesWeb.aspx.cs" Inherits="CotizacionesWeb" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

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

    <style type="text/css"> 
   .modalBackground 
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
   .modalpopup
   {
       padding:20px 0px 24px 10px;
       position:relative;
       width:645px;
       height:565px;
       background-color:White;
       border:1px solid black;
            top: 0px;
            left: 0px;
        }
    </style>

     <script type = "text/javascript">
         function Confirmarenvio() {
             var confirm_value = document.createElement("INPUT");
             confirm_value.type = "hidden";
             confirm_value.name = "confirm_value";
             if (confirm("¿Está usted segur@ que desea generar esta cotización?")) {
                 confirm_value.value = "Si";
             } else {
                 confirm_value.value = "No";
             }
             document.forms[0].appendChild(confirm_value);
         }
    </script>

<%--    <script type = "text/javascript">
    var ventana_secundaria 

    function abrirVentana(){ 
    ventana_secundaria = window.open("test.aspx","miventana","width=300,height=200,menubar=no") 
    }
    </script>--%>
    
 </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
      
    <h3>Cotizaciones</h3>
   <hr />    

     <asp:UpdateProgress ID="UpdateProgress5" runat="server">
    <ProgressTemplate> 
    <div id="Background"></div>
    <div id="Progress">
    <h6><p style="text-align:center"><b>Un momento por favor... <br /></b></p></h6>
    </div>  
    </ProgressTemplate>
    </asp:UpdateProgress>

       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>      
    
     <asp:Panel ID="Panel1" runat="server" CssClass="modalpopup">
         <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
         <ContentTemplate>         
           <table border="0" cellpadding="0" cellspacing="0">
               <tr>
                   <td> &nbsp;<asp:Label ID="Label15" runat="server" Text="Escriba el código o nombre del artículo" CssClass="style1"></asp:Label> </td>                   
               </tr>
               <tr>
               <td> <asp:TextBox ID="buscardescripcion" runat="server" CssClass="form-control" Width="280px"></asp:TextBox> </td>  
               <td><asp:Button ID="Buscardes" runat="server" Text="Buscar" CssClass="btn btn-md btn-info" onclick="Buscardes_Click" /></td>                         
               <td><asp:Button ID="cancelarbus" runat="server" Text="Cancelar" CssClass="btn btn-md btn-info" onclick="cancelarbus_Click" /></td>
               </tr>
           </table> 
           <br />
           <asp:GridView ID="GridView2" runat="server" CellPadding="4" ForeColor="#333333" 
                 Width="615px" AllowPaging="True" PageSize="15" ShowFooter="True" 
                 ShowHeaderWhenEmpty="True" onpageindexchanging="GridView2_PageIndexChanging" 
                 onrowcommand="GridView2_RowCommand">
               <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
               <Columns>
                   <asp:ButtonField CommandName="Seleccionar" Text="Seleccionar" />
               </Columns>
               <EditRowStyle BackColor="#999999" />
               <FooterStyle BackColor="#B40404" Font-Bold="True" ForeColor="White" />
               <HeaderStyle BackColor="#B40404" Font-Bold="True" ForeColor="White" />
               <PagerSettings Position="TopAndBottom" />
               <PagerStyle BackColor="#B40404" ForeColor="White" HorizontalAlign="Center" />
               <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
               <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
               <SortedAscendingCellStyle BackColor="#E9E7E2" />
               <SortedAscendingHeaderStyle BackColor="#506C8C" />
               <SortedDescendingCellStyle BackColor="#FFFDF8" />
               <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
           </asp:GridView>
           <br />               
     </ContentTemplate>
         </asp:UpdatePanel>              
    </asp:Panel>
    <input id="dummy" type="button" style="display: none" runat="server" />
    <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" 
     PopupControlID="panel1" TargetControlID="dummy" PopupDragHandleControlID="panel1"
     BackgroundCssClass="modalBackground" DropShadow="true"></asp:ModalPopupExtender>

        <table border="0" cellpadding="0" cellspacing="0">            
            <tr>
                <td>&nbsp;&nbsp;<asp:Label ID="Label4" runat="server" Text="Cod. de Cliente" CssClass="style1"></asp:Label></td>
                <td>&nbsp;&nbsp;<asp:Label ID="Label5" runat="server" Text="Nombre Cliente" CssClass="style1"></asp:Label></td>                      
                <td>&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="RNC" CssClass="style1"></asp:Label></td>   
                <td>&nbsp;&nbsp;<asp:Label ID="Label6" runat="server" Text="Vendedor" CssClass="style1"></asp:Label></td>  
                <td>&nbsp;&nbsp;<asp:Label ID="Label3" runat="server" Text="Cant. Articulos" CssClass="style1"></asp:Label></td>  
            </tr>

            <tr>            
            <td><asp:TextBox ID="codcliente" runat="server" CssClass="form-control" Width="105px" AutoPostBack="True" 
            ontextchanged="codcliente_TextChanged"></asp:TextBox></td>
            <td><asp:TextBox ID="nombrecliente" runat="server" CssClass="form-control" Width="210px"></asp:TextBox></td>          
            <td><asp:TextBox ID="RNC" runat="server" CssClass="form-control" Width="94px"></asp:TextBox></td>  
            <td><asp:TextBox ID="vendedor" runat="server" CssClass="form-control" Width="200px" ReadOnly="true"></asp:TextBox></td>  
            <td>&nbsp;&nbsp;&nbsp;<asp:Label ID="Label16" runat="server" Text="0" CssClass="style1" Font-Size="X-Large"></asp:Label></td>  
            </tr>
        </table>   
        <hr />
        <h5>Inserta los artículos para el aereo seleccionado</h5>
        <hr />

        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>&nbsp;<asp:Label ID="Label10" runat="server" Text="Cod. Artículo" CssClass="style1"></asp:Label></td>
                <td></td>
                <td>&nbsp;<asp:Label ID="Label11" runat="server" Text="Descripción" CssClass="style1"></asp:Label></td>
                <td>&nbsp;<asp:Label ID="Label12" runat="server" Text="Cantidad" CssClass="style1"></asp:Label></td>
                <td>&nbsp;<asp:Label ID="Label13" runat="server" Text="Precio" CssClass="style1"></asp:Label></td>
                <td>&nbsp;<asp:Label ID="Label2" runat="server" Text="Descuento" CssClass="style1"></asp:Label></td>
                <td>&nbsp;<asp:Label ID="Label14" runat="server" Text="Sub Total" CssClass="style1"></asp:Label>
                </td>
            </tr>

                 <tr>
                <td><asp:TextBox ID="codArticulo" runat="server" CssClass="form-control" Width="95px"></asp:TextBox> </td>
                <td> &nbsp;<asp:Button ID="Buscar_Articulos" runat="server" Text="Buscar Art" CssClass="btn btn-md btn-info" onclick="Buscar_Articulos_Click1"/>&nbsp;</td>
                <td><asp:TextBox ID="Descripcion" runat="server" CssClass="form-control" Width="250px"></asp:TextBox> </td>                
                <td><asp:TextBox ID="Cantidad" runat="server" CssClass="form-control" Width="85px"></asp:TextBox></td>
                <td><asp:TextBox ID="Precio" runat="server" CssClass="form-control" Width="85px" AutoPostBack="True" ontextchanged="Precio_TextChanged"></asp:TextBox> </td>
                <td><asp:TextBox ID="Descuento" runat="server" CssClass="form-control" Width="85px"  AutoPostBack="True" ontextchanged="Descuento_TextChanged" Text="0"></asp:TextBox></td>
                <td><asp:TextBox ID="Sub_Total" runat="server" CssClass="form-control" Width="90px" ReadOnly="true" Text="0.00"></asp:TextBox></td>                                
            </tr>
        </table>
        <br />
        <asp:Button ID="SubirArticulo" runat="server" Text="Insertar" CssClass="btn btn-md btn-info" onclick="SubirArticulo_Click" /> 
  &nbsp;<asp:Button ID="Limpiar" runat="server" Text="Limpiar" CssClass="btn btn-md btn-info" onclick="Limpiar_Click"/> 
                        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            AutoGenerateDeleteButton="True" CellPadding="4" ForeColor="#333333" 
            PageSize="25" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="885px" 
            onrowdeleting="GridView1_RowDeleting">
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

             <table border="0" cellpadding="0" cellspacing="0" align="right">
            <tr>
                <td><asp:Label ID="Label7" runat="server" Text="Sub Total" CssClass="style1"></asp:Label></td>      
            </tr>                
            <tr>
              <td><asp:TextBox ID="totalsub" runat="server" CssClass="form-control" ReadOnly="true" Text="0.00"></asp:TextBox></td>              
            </tr>
            <tr>
                  <td><asp:Label ID="Label8" runat="server" Text="ITBIS" CssClass="style1"></asp:Label></td>                
            </tr>
            <tr>
            <td><asp:TextBox ID="totalitbis" runat="server" CssClass="form-control" ReadOnly="true" Text="0.00"></asp:TextBox></td>  
            </tr>
            <tr> 
            <td> <asp:Label ID="Label9" runat="server" Text="Total Gral.:" CssClass="style1"></asp:Label></td>
            </tr>
            <tr>
            <td><asp:TextBox ID="totalgral" runat="server" CssClass="form-control" ReadOnly="true" Text="0.00"></asp:TextBox>
                <br />
                </td> 
            </tr>                        
        </table>        
        <br />
        <asp:Button ID="Generar" runat="server" Text="Generar Cotización" CssClass="btn btn-md btn-info" onclick="Generar_Click" OnClientClick="Confirmarenvio()"/>
        <asp:Button ID="cancelar" runat="server" Text="Cancelar" CssClass="btn btn-md btn-info" onclick="cancelar_Click" />
        <asp:Button ID="Consultar" runat="server" Text="Consultar Cotizaciones" CssClass="btn btn-md btn-info" onclick="Consultar_Click"/>
    </ContentTemplate>
    </asp:UpdatePanel>    
</asp:Content>