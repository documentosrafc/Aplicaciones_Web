using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.Common;

public partial class BackOrders : System.Web.UI.Page
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

            if (Session["vendor"] != null)
            {
                GenerarReporteBackOrders(Session["vendor"].ToString());
                Session["vendor"] = null;
            }
            else
            {
                GenerarReporteBackOrders(Session["idvendedor"].ToString());
            }
        }   
    }
    public void GenerarReporteBackOrders(string vendedor)
    {
        this.Form.Attributes.Add("autocomplete", "off");
        ReportParameter[] parametr = new ReportParameter[1];
        //string Vendedor = Session["idvendedor"].ToString();
        parametr[0] = new ReportParameter("VENDEDOR", vendedor);
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