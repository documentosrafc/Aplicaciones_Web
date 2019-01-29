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


public partial class PagosAplicadosPorVendedores : System.Web.UI.Page
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
    protected void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        string consultar_nombre = @"SELECT * FROM DESARROLLO.LISTCONT.LISTADECONTACTO WHERE NO_VENDEDOR='" + DropDownList1.Text + "'";
        com = new SqlCommand(consultar_nombre, con.cn);
        data = new SqlDataAdapter(com);
        table = new DataTable();
        data.Fill(table);

        if (table.Rows.Count > 0)
        {
            Label2.Text = table.Rows[0]["NOMBRE_APELLIDO"].ToString();
        }
    }
    protected void Buscar_Click1(object sender, EventArgs e)
    {
        if (DropDownList1.Text != string.Empty || DropDownList1.Text != "Vendedores")
        {
            Session["idvendedor"] = DropDownList1.Text;
            Response.Redirect("~/PagosAplicados.aspx");
        }
    }
}