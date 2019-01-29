using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class PrevisionFinMes : System.Web.UI.Page
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

            Label1.Text = DateTime.Today.Month.ToString();
            sacarmesletra(Label1.Text);
            Label2.Text = DateTime.Today.Date.ToShortDateString();
            habilitarmenu();
            cargarrevisiones();
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
    public void sacarmesletra(string numeros)
    {
        if (numeros == "1"){Label1.Text = "Enero";}
        else if (numeros == "2"){Label1.Text = "Febrero";}
        else if (numeros == "3"){Label1.Text = "Marzo";}
        else if (numeros == "4"){Label1.Text = "Abril";}
        else if (numeros == "5"){Label1.Text = "Mayo";}
        else if (numeros == "6"){Label1.Text = "Junio";}
        else if (numeros == "7"){Label1.Text = "Julio";}
        else if (numeros == "8"){Label1.Text = "Agosto";}
        else if (numeros == "9"){Label1.Text = "Septiembre";}
        else if (numeros == "10"){Label1.Text = "Octubre";}
        else if (numeros == "11"){Label1.Text = "Noviembre";}
        else if (numeros == "12"){Label1.Text = "Diciembre";}
    }
    public void cargarrevisiones()
    {
        string buscarprevisiones = @"SELECT ID,COD_VENDEDOR AS VEND,VENTAS,COBROS,PEDIDOS, POSICIONES AS POSI,CLIENTES_ACTIVOS AS C_ACTIVOS,CLIENTES_NUEVOS AS C_NUEVOS,CLIENTES_RECUPERADOS AS C_RECUPE, 
                                   CONVERT(VARCHAR,CAST(GP AS MONEY),1) AS GP,FECHA_REGISTRO AS FECHA FROM DESARROLLO.CONVEN.PREVISION_FINMES WHERE COD_VENDEDOR='"+ Session["idvendedor"].ToString() +"'";
        com = new SqlCommand(buscarprevisiones, con.cn);
        data = new SqlDataAdapter(com);
        table = new DataTable();
        data.Fill(table);

        GridView1.DataSource = new DataView(table);
        GridView1.DataBind();


        string buscarprevision_HISTO = @"SELECT ID,COD_VENDEDOR AS VEND,VENTAS,COBROS,PEDIDOS, POSICIONES AS POSI,CLIENTES_ACTIVOS AS C_ACTIVOS,CLIENTES_NUEVOS AS C_NUEVOS,CLIENTES_RECUPERADOS AS C_RECUPE, 
                                     CONVERT(VARCHAR,CAST(GP AS MONEY),1) AS GP,FECHA_REGISTRO AS FECHA FROM DESARROLLO.CONVEN.PREVISION_FINMES_HISTO WHERE COD_VENDEDOR='" + Session["idvendedor"].ToString() + "'";
        com = new SqlCommand(buscarprevision_HISTO, con.cn);
        data = new SqlDataAdapter(com);
        table = new DataTable();
        data.Fill(table);

        GridView2.DataSource = new DataView(table);
        GridView2.DataBind();


    }
    private Boolean validarexistenciames( int mesactual, int año)
    {
        string validar = @"SELECT * FROM DESARROLLO.CONVEN.PREVISION_FINMES WHERE MONTH(FECHA_REGISTRO)=" + mesactual + " AND YEAR(FECHA_REGISTRO)=" + año + " AND COD_VENDEDOR='" + Session["idvendedor"].ToString() + "'";
        data = new SqlDataAdapter(validar, con.cn);
        table = new DataTable();
        data.Fill(table);
        if (table.Rows.Count > 0){return true;}
        else{return false;}
    }
    protected void Cancelar_Click(object sender, EventArgs e)
    {
        Session["Modificar"] = null;
        ventas.Text = string.Empty;
        cobros.Text = string.Empty;
        gp.Text = string.Empty;
        pedidos.Text = string.Empty;
        posiciones.Text = string.Empty;
        cactivos.Text = string.Empty;
        crecuperados.Text = string.Empty;
        cnuevos.Text = string.Empty;
        ventas.Focus();
    }
    protected void Guardar_Click(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(2000);
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Substring(confirmValue.Length - 2) == "Si")
            {
                if (ventas.Text == string.Empty || cobros.Text == string.Empty || gp.Text == string.Empty || pedidos.Text == string.Empty || posiciones.Text == string.Empty || cactivos.Text == string.Empty || crecuperados.Text == string.Empty || cnuevos.Text == string.Empty)
                {
                    var a = "alert(\'Lo sentimos, pero si desea guardar esta información es necesario que rellene todos los campos\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                    ventas.Focus();
                }
                else if (Session["Modificar"] == null)
                {
                    if (validarexistenciames(DateTime.Today.Date.Month, DateTime.Today.Date.Year) == false)
                    {
                        con.cn.Open();
                        string guardar_prevision = @"Desarrollo.ConVen.Guardar_Modificar_Previsiones";
                        com = new SqlCommand(guardar_prevision, con.cn);
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.Add("@MODIFICAR", SqlDbType.VarChar, 1).Value = "1";
                        com.Parameters.Add("@CODIGO_VENDEDOR", SqlDbType.VarChar, 10).Value = Session["idvendedor"].ToString();
                        com.Parameters.Add("@VENTA", SqlDbType.Float).Value = float.Parse(ventas.Text);
                        com.Parameters.Add("@COBROS", SqlDbType.Float).Value = float.Parse(cobros.Text);
                        com.Parameters.Add("@PEDIDOS", SqlDbType.Int).Value = float.Parse(pedidos.Text);
                        com.Parameters.Add("@POSICIONES", SqlDbType.Float).Value = float.Parse(posiciones.Text);
                        com.Parameters.Add("@CLIENTES_ACTIVOS", SqlDbType.Int).Value = int.Parse(cactivos.Text);
                        com.Parameters.Add("@CLIENTES_NUEVOS", SqlDbType.Int).Value = int.Parse(cnuevos.Text);
                        com.Parameters.Add("@CLIENTES_RECUPERADOS", SqlDbType.Int).Value = int.Parse(crecuperados.Text);
                        com.Parameters.Add("@GP", SqlDbType.Float).Value = float.Parse(gp.Text);
                        com.UpdatedRowSource = UpdateRowSource.None;
                        com.ExecuteNonQuery();
                        con.cn.Close();

                        var q = "alert(\'Previsión guardada correctamente...\');";
                        ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", q, true);
                        Cancelar_Click(sender, e);
                        cargarrevisiones();
                    }
                    else
                    {
                        var q = "alert(\'Lo sentimos, pero no puede volver a insertar la previsión en el mismo mes, lo que recomendamos es que modifique la existente\');";
                        ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", q, true);
                    }
                }
                else
                {
                    con.cn.Open();
                    string guardar_prevision = @"Desarrollo.ConVen.Guardar_Modificar_Previsiones";
                    com = new SqlCommand(guardar_prevision, con.cn);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@ID", SqlDbType.VarChar, 1).Value = Session["Modificar"].ToString();
                    com.Parameters.Add("@MODIFICAR", SqlDbType.VarChar, 1).Value = "";
                    com.Parameters.Add("@CODIGO_VENDEDOR", SqlDbType.VarChar, 10).Value = Session["idvendedor"].ToString();
                    com.Parameters.Add("@VENTA", SqlDbType.Float).Value = float.Parse(ventas.Text);
                    com.Parameters.Add("@COBROS", SqlDbType.Float).Value = float.Parse(cobros.Text);
                    com.Parameters.Add("@PEDIDOS", SqlDbType.Int).Value = float.Parse(pedidos.Text);
                    com.Parameters.Add("@POSICIONES", SqlDbType.Float).Value = float.Parse(posiciones.Text);
                    com.Parameters.Add("@CLIENTES_ACTIVOS", SqlDbType.Int).Value = int.Parse(cactivos.Text);
                    com.Parameters.Add("@CLIENTES_NUEVOS", SqlDbType.Int).Value = int.Parse(cnuevos.Text);
                    com.Parameters.Add("@CLIENTES_RECUPERADOS", SqlDbType.Int).Value = int.Parse(crecuperados.Text);
                    com.Parameters.Add("@GP", SqlDbType.Float).Value = float.Parse(gp.Text);
                    com.UpdatedRowSource = UpdateRowSource.None;
                    com.ExecuteNonQuery();
                    con.cn.Close();

                    var q = "alert(\'Previsión modificada correctamente...\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", q, true);
                    Cancelar_Click(sender, e);
                    cargarrevisiones();
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modificar")
        {
            Session["Modificar"] = GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;

            int mesactual = DateTime.Today.Date.Month;
            int mes = DateTime.Parse(GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[11].Text).Month;            
            if (mesactual == mes)
            {
                ventas.Text = GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
                cobros.Text = GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Text;
                pedidos.Text = GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[5].Text;
                posiciones.Text = GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[6].Text;
                cactivos.Text = GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[7].Text;
                cnuevos.Text = GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[8].Text;
                crecuperados.Text = GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[9].Text;
                gp.Text = GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[10].Text;
            }
            else
            {
                var q = "alert(\'Lo sentimos, pero no puedes modificar esta previsión debido a que ya el mes ha pasado\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", q, true);
            }
        }
    }
}