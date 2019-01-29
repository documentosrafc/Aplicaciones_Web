using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.Common;
using System.Data;
using System.Data.SqlClient;

public partial class ConsumoporVendedor : System.Web.UI.Page
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
            habilitarmenu();
            CargarVendedores();
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
        if (codcliente.Text == string.Empty)
        {
            var a = "alert(\'Para poder realizar la busqueda del consumo  es necesario que escriba el codigo del cliente\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
        }
        else
        {
            ReportParameter[] parametr = new ReportParameter[2];
            string idvendedor = DropDownList1.Text;
            string cod_cliente = codcliente.Text;
            parametr[0] = new ReportParameter("VENDEDOR", idvendedor);
            parametr[1] = new ReportParameter("CLIENTE", cod_cliente);
            this.ReportViewer1.ServerReport.SetParameters(parametr);
        }
    }
}