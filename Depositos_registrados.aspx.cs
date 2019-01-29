using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Depositos_registrados : System.Web.UI.Page
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

        DropDownList3.Items.Clear();
        for (int i = 0; i < table.Rows.Count; i++)
        {
            if (DropDownList3.Items.Count <= 0)
            {
                DropDownList3.Items.Add("Vendedores");
                DropDownList3.Items.Add(table.Rows[i]["COD_VENDEDOR"].ToString());
            }
            else
            {
                DropDownList3.Items.Add(table.Rows[i]["COD_VENDEDOR"].ToString());
            }
        }
    }
    public void habilitarmenu()
    {
        var Menu = Page.Master.FindControl("Menu1") as Menu;
        if (Session["JEFAZONA"].ToString() == "1")
        {
            Menu.Visible = true;
        }
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DropDownList3.Text != "Vendedores")
            {
                string consultar_nombre = @"SELECT * FROM DESARROLLO.LISTCONT.LISTADECONTACTO WHERE NO_VENDEDOR='" + DropDownList3.Text + "'";
                com = new SqlCommand(consultar_nombre, con.cn);
                data = new SqlDataAdapter(com);
                table = new DataTable();
                data.Fill(table);

                if (table.Rows.Count > 0)
                {
                    Label1.Text = table.Rows[0]["NOMBRE_APELLIDO"].ToString();

                    cargarestadodepositos(DropDownList3.Text);
                }
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    public void cargarestadodepositos(string vendedor)
    {
        string estadodepositos = @"DESARROLLO.CONVEN.ESTADODEPOSITOS";
        com = new SqlCommand(estadodepositos, con.cn);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@VENDEDOR", SqlDbType.VarChar, 10).Value = vendedor;
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