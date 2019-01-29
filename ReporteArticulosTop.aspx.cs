using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.Common;

public partial class ReporteArticulosTop : System.Web.UI.Page
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
    protected void Generar_Click(object sender, EventArgs e)
    {
        if (FechaFinal.Text == string.Empty || FechaInicial.Text == string.Empty)
        {
            var a = "alert(\'Para poder realizar una busqueda es necesario que seleccione las fechas\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
        }
        else
        {            
            ReportParameter[] parametr = new ReportParameter[4];
            string Vendedor = Session["idvendedor"].ToString();
            string Finicial = Convert.ToDateTime(FechaInicial.Text).ToShortDateString();
            string Ffinal = Convert.ToDateTime(FechaFinal.Text).ToShortDateString();
            parametr[0] = new ReportParameter("FECHADESDE", Finicial);
            parametr[1] = new ReportParameter("FECHAHASTA", Ffinal);
            parametr[2] = new ReportParameter("VENDEDOR", Vendedor);
            parametr[3] = new ReportParameter("TOP", "100");
            this.ReportViewer1.ServerReport.SetParameters(parametr);
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
}