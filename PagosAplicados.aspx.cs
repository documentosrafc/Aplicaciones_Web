using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.Common;

public partial class PagosAplicados : System.Web.UI.Page
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
            GenerarReportepagos();
        }
    }
    public void GenerarReportepagos()
    {
        this.Form.Attributes.Add("autocomplete", "off");
        ReportParameter[] parametr = new ReportParameter[1];
        string Vendedor = Session["idvendedor"].ToString();
        parametr[0] = new ReportParameter("VENDEDORES", Vendedor);
        this.ReportViewer1.ServerReport.SetParameters(parametr);
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