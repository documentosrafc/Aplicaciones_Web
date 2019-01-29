using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.Common;

public partial class VISporVendedor : System.Web.UI.Page
{
    Conexion con = new Conexion();
    SqlCommand com = new SqlCommand();
    SqlDataAdapter data = new SqlDataAdapter();
    DataTable table = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["NombreCompleto"] == null)
        {
            Response.Redirect("~/Default.aspx");
        }
        if (!IsPostBack)
        {            
            CargarVendedores();
        }
        habilitarmenu();
    }
    private void habilitarmenu()
    {
        var Menu = Page.Master.FindControl("Menu1") as Menu;
        if (Session["JEFAZONA"].ToString() == "1")
        {
            Menu.Visible = true;
        }
    }
    private void CargarVendedores()
    {
        string cargarvendedor = @"Desarrollo.CONVEN.CargarVendedoresdeJefas";
        com = new SqlCommand(cargarvendedor, con.cn);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@JefaZona", SqlDbType.VarChar, 10).Value = Session["NumeroGrupo"].ToString();
        com.Parameters.Add("@VENDEDORA", SqlDbType.VarChar, 10).Value = Session["idvendedor"].ToString();
        data = new SqlDataAdapter(com);
        table = new DataTable();
        data.Fill(table);

        DropDownList1.Items.Clear();
        for (int i = 0; i < table.Rows.Count; i++)
        {
            if (DropDownList1.Items.Count <= 0)
            {
                DropDownList1.Items.Add("Vendedores");
                DropDownList1.Items.Add(table.Rows[i]["COD_VENDEDOR"].ToString());
            }
            else
            {
                DropDownList1.Items.Add(table.Rows[i]["COD_VENDEDOR"].ToString());
            }
        }
    }
    protected void buscar_Click(object sender, EventArgs e)
    {               
        this.Form.Attributes.Add("autocomplete", "off");
        ReportParameter[] parametr = new ReportParameter[3];
        string VENDEDOR = DropDownList1.Text;
        string CAMBIOEURO = "60.09";
        string CAMBIODOLARES = "49.02";
        parametr[0] = new ReportParameter("VENDEDOR", VENDEDOR);
        parametr[1] = new ReportParameter("CAMBIOEURO", CAMBIOEURO);
        parametr[2] = new ReportParameter("CAMBIODOLARES", CAMBIODOLARES);
        this.ReportViewer1.ServerReport.SetParameters(parametr);
    }
}