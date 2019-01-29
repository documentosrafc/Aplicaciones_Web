using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.Common;

public partial class cotizaciones : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["NombreCompleto"] == null)
        {
            Response.Redirect("~/Default.aspx");
        }
        if (!IsPostBack)
        {
            this.Form.Attributes.Add("autocomplete", "off");
 
            ReportParameter[] parametr = new ReportParameter[1];
            string cotiza = Session["numcotiza"].ToString();
            parametr[0] = new ReportParameter("COTIZACIONNUM", cotiza);            
            this.ReportViewer1.ServerReport.SetParameters(parametr);
        }
    }
}