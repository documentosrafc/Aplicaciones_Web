using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class ReposicionesHechas : System.Web.UI.Page
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
            this.Form.Attributes.Add("Autocomplete", "off");

            habilitarmenu();
            CargarVendedores();
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
                DropDownList1.Items.Add(table.Rows[i]["NOMBRE"].ToString());                
            }
            else
            {
                DropDownList1.Items.Add(table.Rows[i]["NOMBRE"].ToString());
            }
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(2000);
        string buscar_reposiciones = @"DESARROLLO.REPGAS.CargarReposicionesEnviadas";
        com = new SqlCommand(buscar_reposiciones, con.cn);
        com.Parameters.Add("@EMPLEADO", SqlDbType.VarChar, 50).Value = DropDownList1.Text;
        com.CommandType = CommandType.StoredProcedure;
        data = new SqlDataAdapter(com);
        table = new DataTable();
        data.Fill(table);

        if (table.Rows.Count > 0)
        {
            GridView2.DataSource = new DataView(table);
            GridView2.DataBind();
        }

        string buscar_reposiciones_pagadas = @"DESARROLLO.REPGAS.CargarReposicionesEnviadasypagadas";
        com = new SqlCommand(buscar_reposiciones_pagadas, con.cn);
        com.Parameters.Add("@EMPLEADO", SqlDbType.VarChar, 50).Value = DropDownList1.Text;
        com.CommandType = CommandType.StoredProcedure;
        data = new SqlDataAdapter(com);
        table = new DataTable();
        data.Fill(table);

        if (table.Rows.Count > 0)
        {
            GridView4.DataSource = new DataView(table);
            GridView4.DataBind();
        }
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Detalle")
        {
            string cargarlineas = @"Desarrollo.RepGas.CargaLineasReposiciones";
            com = new SqlCommand(cargarlineas, con.cn);
            com.Parameters.Add("@COD_REPOSICION", SqlDbType.VarChar, 10).Value = GridView2.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
            com.CommandType = CommandType.StoredProcedure;
            data = new SqlDataAdapter(com);
            table = new DataTable();
            data.Fill(table);

            if (table.Rows.Count > 0)
            {
                GridView3.DataSource = new DataView(table);
                GridView3.DataBind();
            }
        }
    }
    protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Detalle")
        {
            string cargarlineas = @"Desarrollo.RepGas.CargaLineasReposiciones";
            com = new SqlCommand(cargarlineas, con.cn);
            com.Parameters.Add("@COD_REPOSICION", SqlDbType.VarChar, 10).Value = GridView4.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
            com.CommandType = CommandType.StoredProcedure;
            data = new SqlDataAdapter(com);
            table = new DataTable();
            data.Fill(table);

            if (table.Rows.Count > 0)
            {
                GridView5.DataSource = new DataView(table);
                GridView5.DataBind();
            }
        }
    }
}