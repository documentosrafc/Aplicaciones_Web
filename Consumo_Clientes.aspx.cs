using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.Common;

public partial class Consumo_Clientes : System.Web.UI.Page
{  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["NombreCompleto"] == null)
        {
            Response.Redirect("~/Default.aspx");
        }
        if (!IsPostBack)
        {
            habilitarmenu();
           this.Form.Attributes.Add("autocomplete", "off");           
        }
    }
    private void habilitarmenu()
    {
        var Menu = Page.Master.FindControl("Menu1") as Menu;
        if (Session["JEFAZONA"].ToString() == "1")
        {
            Menu.Visible = true;
        }
    }   
    protected void gconsumo_Click(object sender, EventArgs e)
    {
        if (codcliente.Text == string.Empty)
        {
            var a = "alert(\'Para poder realizar la busqueda del consumo  es necesario que escriba el codigo del cliente\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
        }
        else
        {
            ReportParameter[] parametr = new ReportParameter[2];
            string idvendedor = Session["idvendedor"].ToString();
            string cod_cliente = codcliente.Text;
            parametr[0] = new ReportParameter("VENDEDOR", idvendedor);
            parametr[1] = new ReportParameter("CLIENTE", cod_cliente);
            this.ReportViewer1.ServerReport.SetParameters(parametr);
        }
    }
}