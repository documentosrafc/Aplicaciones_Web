<%@ Page Title="Ingresar al Sistema" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            color: #CC6600;
            font-weight: bold;
            font-size: small;
        }
    </style>




    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-2.2.4.min.js" type="text/javascript"></script>
   <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>


</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h3>
        Login</h3>
    <br />
    <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Número de Vended@r" CssClass="style1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                    <asp:TextBox ID="Correo" runat="server" Width="205px" script="min-wisth:5%; max-wisth:100%"
                        AutoPostBack="true" OnTextChanged="Correo_TextChanged" class="form-control" placeholder="Número Vended@r"></asp:TextBox>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Contraseña" CssClass="style1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                    <asp:TextBox ID="Contra" runat="server" Width="205px" TextMode="Password" class="form-control"
                        placeholder="Contraseña" script="min-wisth:5%; max-wisth:100%"></asp:TextBox>
                </div>
            </td>
        </tr>
    </table>
    <br />       
    <asp:Button ID="Ingresar" runat="server" Text="Ingresar" OnClick="Ingresar_Click" class="btn btn-info btn-md" />
    <asp:Button ID="Cancelar" runat="server" Text="Cancelar" OnClick="Cancelar_Click" class="btn btn-info btn-md" />        
    </asp:Content>
