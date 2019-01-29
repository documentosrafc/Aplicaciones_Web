using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Detalle_Cotizaciones : System.Web.UI.Page
{
    Conexion con = new Conexion();
    SqlCommand com = new SqlCommand();
    SqlDataAdapter data = new SqlDataAdapter();
    DataTable table = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Form.Attributes.Add("autocomplete", "off");

        if (Session["NombreCompleto"] == null)
        {
            Response.Redirect("~/Default.aspx");
        }     
        if (!IsPostBack)
        {
            habilitarmenu();
            Cargarpantalla();
        }
    }
    private void Cargarpantalla()
    {
        string cargarpedidos = @"SELECT IDCOTIZACION AS ID_COT, CODCLIENTE AS CLIENTE, NOMBRECLIENTE AS NOMBRE, RNC, VENDEDOR,
                                 CONVERT(VARCHAR,CAST(SUBTOTAL AS MONEY),1) AS TOTAL, CONVERT(VARCHAR,CAST(ITBIS AS MONEY),1) AS ITBIS,
                                 CONVERT(VARCHAR,CAST(TOTALGRAL AS MONEY),1) AS TOTAL_GRAL,
                                 CONVERT(VARCHAR,FECHACREACION,110) AS F_CREACION  FROM DESARROLLO.CONVEN.COTIZACIONES_CAB
                                 WHERE VENDEDOR='" + Session["NombreCompleto"].ToString() + "' ORDER BY FECHACREACION DESC";
        data = new SqlDataAdapter(cargarpedidos, con.cn);
        table=new DataTable();
        data.Fill(table);

        GridView1.DataSource=new DataView(table);
        GridView1.DataBind();
    }
    private void habilitarmenu()
    {
        var Menu = Page.Master.FindControl("Menu1") as Menu;
        if (Session["JEFAZONA"].ToString() == "1")
        {
            Menu.Visible = true;
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Detalle")
        {
            string buscardetallepedidos = @"SELECT CODARTICULO, DESCRIPCION, CONVERT(VARCHAR,CAST(PRECIO AS MONEY),1) AS PRECIO, CANTIDAD, 
                                            CONVERT(VARCHAR,CAST(SUBTOTAL AS MONEY),1) AS SUB_TOTAL, CONVERT(VARCHAR,CAST((SUBTOTAL*0.18)+SUBTOTAL AS MONEY),1) AS TOTAL_GRAL
                                            FROM Desarrollo.CONVEN.COTIZACIONES_DET WHERE IDPEDIDO='" + GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text + "'";
            data = new SqlDataAdapter(buscardetallepedidos,con.cn);
            table = new DataTable();
            data.Fill(table);

            GridView2.DataSource = new DataView(table);
            GridView2.DataBind();
        }
        if (e.CommandName == "Modificar")
        {
            Session["Modi"] = GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
            Response.Redirect("~/CotizacionesWeb.aspx");
        }
        if (e.CommandName == "Imprimir")
        {
            Session["numcotiza"] = GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
            Response.Redirect("~/cotizaciones.aspx");
        }
    }
 }