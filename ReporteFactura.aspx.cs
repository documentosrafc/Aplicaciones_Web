using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.Common;

public partial class ReporteFactura : System.Web.UI.Page
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
            //QuitarPermisos();
            //EmitirPermiso();

            ReportParameter[] parametr = new ReportParameter[2];
            string Cliente = Session["cliente"].ToString();
            string Factura = Session["factura"].ToString();
            parametr[0] = new ReportParameter("CLIENTE", Cliente);
            parametr[1] = new ReportParameter("FACTURA", Factura);
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
    //public void QuitarPermisos()
    //{
    //    var Menu = Page.Master.FindControl("NavigationMenu") as Menu;
    //    Menu.Items[0].Enabled = false;
    //    Menu.Items[1].Enabled = false;
    //    Menu.Items[2].Enabled = false;
    //    Menu.Items[3].Enabled = false;
    //    Menu.Items[4].Enabled = false;
    //    Menu.Items[5].Enabled = false;
    //    Menu.Items[6].Enabled = false;
    //    Menu.Items[7].Enabled = false;
    //    Menu.Items[8].Enabled = false;
    //}
    //public void EmitirPermiso()
    //{
    //    var Menu = Page.Master.FindControl("NavigationMenu") as Menu;

    //    if (Session["Privilegios"].ToString() == "1")
    //    {//estos son los administratores
    //        Menu.Items[0].Enabled = true;
    //        Menu.Items[1].Enabled = true;
    //        Menu.Items[2].Enabled = true;
    //        Menu.Items[3].Enabled = true;
    //        Menu.Items[4].Enabled = true;
    //        Menu.Items[4].Enabled = true;
    //        Menu.Items[5].Enabled = true;
    //        Menu.Items[6].Enabled = true;
    //        Menu.Items[7].Enabled = true;
    //        Menu.Items[8].Enabled = true;
    //    }
    //    if (Session["Privilegios"].ToString() == "0")
    //    {//personal de venta
    //        Menu.Items[0].Enabled = true;
    //        Menu.Items[1].Enabled = true;
    //        Menu.Items[2].Enabled = true;
    //        Menu.Items[3].Enabled = true;
    //        Menu.Items[4].Enabled = true;
    //        Menu.Items[5].Enabled = true;
    //        Menu.Items[6].Enabled = true;
    //        Menu.Items[7].Enabled = true;
    //        // Menu.Items[8].Enabled = true;
    //    }
    //    if (Session["Privilegios"].ToString() == "3")
    //    {//personal de Recepcion     
    //        Menu.Items[5].Enabled = true;
    //        Menu.Items[6].Enabled = true;
    //    }
    //}
}