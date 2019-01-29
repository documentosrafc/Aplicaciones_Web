using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
//using System.Net.Mail;

public partial class CotizacionesWeb : System.Web.UI.Page
{
    Conexion con = new Conexion();
    SqlCommand com = new SqlCommand();
    SqlDataAdapter data = new SqlDataAdapter();
    DataTable table = new DataTable();
    DataTable dt = new DataTable();

    float descu = 0;

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

            if (Session["Modi"] == null)
            {
                Session["dt"] = null;
                creaciontabletemp();
                GridView1.DataSource = new DataView(dt);
                GridView1.DataBind();
                vendedor.Text = Session["NombreCompleto"].ToString();
                Session["numcotiza"] = null;

                if (Session["cod_cliente"] != null)
                {                    
                    codcliente.Text = Session["cod_cliente"].ToString();
                    codcliente_TextChanged(sender, e);
                }
            }
            else
            {
                Session["dt"] = null;
                vendedor.Text = Session["NombreCompleto"].ToString();
                cargarcotizacionamodificar(int.Parse(Session["Modi"].ToString()));
            }
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
    public DataTable creaciontabletemp()
    {
        Session["dt"] = null;
        dt.Columns.Add("COD_ART", typeof(string));
        dt.Columns.Add("DESCRICPION", typeof(string));
        dt.Columns.Add("PRECIO", typeof(float));
        dt.Columns.Add("CANTIDAD", typeof(int));
        dt.Columns.Add("DESC", typeof(float));
        dt.Columns.Add("SUB_TOTAL", typeof(float));
        return dt;
    }
    private void cargarcotizacionamodificar(int nocotizacion)
    {
        string buscarcabecera = "SELECT * FROM DESARROLLO.CONVEN.COTIZACIONES_CAB WHERE IDCOTIZACION =" + int.Parse(Session["Modi"].ToString()) + "";
        com = new SqlCommand(buscarcabecera, con.cn);
        data = new SqlDataAdapter(com);
        table = new DataTable();
        data.Fill(table);

        codcliente.Text = table.Rows[0]["CODCLIENTE"].ToString();
        nombrecliente.Text = table.Rows[0]["NOMBRECLIENTE"].ToString();
        RNC.Text = table.Rows[0]["RNC"].ToString();        
        //totalsub.Text = string.Format("{0:#,##0.00}", float.Parse(table.Rows[0]["SUBTOTAL"].ToString()));
        //totalitbis.Text = string.Format("{0:#,##0.00}", float.Parse(table.Rows[0]["ITBIS"].ToString()));
        //totalgral.Text = string.Format("{0:#,##0.00}", float.Parse(table.Rows[0]["TOTALGRAL"].ToString()));

        string buscardestalle = "SELECT * FROM DESARROLLO.CONVEN.COTIZACIONES_DET WHERE IDPEDIDO =" + int.Parse(Session["Modi"].ToString()) + "";
        com = new SqlCommand(buscardestalle, con.cn);
        data = new SqlDataAdapter(com);
        table = new DataTable();
        data.Fill(table);

        for (int i = 0; i < table.Rows.Count; i++)
        {
            Label16.Text = table.Rows.Count.ToString();

            if (Session["dt"] == null)
            {
                DataTable dt = creaciontabletemp();
                DataRow datarow;
                datarow = dt.NewRow();
                datarow["COD_ART"] = table.Rows[i]["CODARTICULO"].ToString();
                datarow["DESCRICPION"] =HttpUtility.HtmlDecode(table.Rows[i]["DESCRIPCION"].ToString());
                datarow["PRECIO"] = string.Format("{0:#,##0.00}", float.Parse(table.Rows[i]["PRECIO"].ToString()));
                datarow["CANTIDAD"] = int.Parse(table.Rows[i]["CANTIDAD"].ToString());
                datarow["DESC"] = string.Format("{0:#,##0.00}", float.Parse(table.Rows[i]["DESCUENTO"].ToString()));
                datarow["SUB_TOTAL"] = string.Format("{0:#,##0.00}", float.Parse(table.Rows[i]["SUBTOTAL"].ToString()));
                dt.Rows.Add(datarow);
                Session["dt"] = dt;
                GridView1.DataSource = new DataView(dt);
                GridView1.DataBind();                
            }
            else
            {
                DataTable dt = (Session["dt"]) as DataTable;
                DataRow datarow;
                datarow = dt.NewRow();
                datarow["COD_ART"] = table.Rows[i]["CODARTICULO"].ToString();
                datarow["DESCRICPION"] = HttpUtility.HtmlDecode(table.Rows[i]["DESCRIPCION"].ToString());
                datarow["PRECIO"] = string.Format("{0:#,##0.00}", float.Parse(table.Rows[i]["PRECIO"].ToString()));
                datarow["CANTIDAD"] = int.Parse(table.Rows[i]["CANTIDAD"].ToString());
                datarow["DESC"] = string.Format("{0:#,##0.00}", float.Parse(table.Rows[i]["DESCUENTO"].ToString()));
                datarow["SUB_TOTAL"] = string.Format("{0:#,##0.00}", float.Parse(table.Rows[i]["SUBTOTAL"].ToString()));
                dt.Rows.Add(datarow);
                Session["dt"] = dt;
                GridView1.DataSource = new DataView(dt);
                GridView1.DataBind();
            }
        }
        recalcularcotizacion();
    }
    protected void SubirArticulo_Click(object sender, EventArgs e)
    {
        if (codcliente.Text == string.Empty || nombrecliente.Text == string.Empty || codArticulo.Text == string.Empty || Descripcion.Text == string.Empty || Precio.Text == string.Empty || Cantidad.Text == string.Empty || Sub_Total.Text == string.Empty || RNC.Text == string.Empty)
        {
            var a = "alert(\'Para poder insertar una linea es necesario que llene todos los campos.\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
        }
        else if (codArticulo.Text.Length <= 2)
        {
            var a = "alert(\'El código de artículo que esta intentando buscar es muy corto.\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
        }
        else if (float.Parse(Precio.Text) <= 0)
        {
            var a = "alert(\'El precio que esta colocando no puede menor que cero.\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
        }
        else if (float.Parse(Cantidad.Text) <= 0)
        {
            var a = "alert(\'La cantidad que esta colocando no puede ser menor a cero.\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
        }
        else if (float.Parse(Sub_Total.Text) <= 0)
        {
            var a = "alert(\'El sub total que esta intentando insertar no puede ser menor a cero.\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
        }
        else
        {
            if (Session["dt"] == null)
            {
                DataTable dt = creaciontabletemp();
                DataRow datarow;
                datarow = dt.NewRow();
                datarow["COD_ART"] = codArticulo.Text;
                datarow["DESCRICPION"] = Descripcion.Text;
                datarow["PRECIO"] = string.Format("{0:#,##0.00}", float.Parse(Precio.Text));
                datarow["CANTIDAD"] = int.Parse(Cantidad.Text);
                datarow["DESC"] = string.Format("{0:#,##0.00}", float.Parse(Descuento.Text));
                datarow["SUB_TOTAL"] = string.Format("{0:#,##0.00}", float.Parse(Sub_Total.Text));
                dt.Rows.Add(datarow);
                Session["dt"] = dt;
                GridView1.DataSource = new DataView(dt);
                GridView1.DataBind();
            }
            else
            {
                DataTable dt = (Session["dt"]) as DataTable;
                DataRow datarow;
                datarow = dt.NewRow();
                datarow["COD_ART"] = codArticulo.Text;
                datarow["DESCRICPION"] = Descripcion.Text;
                datarow["PRECIO"] = string.Format("{0:#,##0.00}", float.Parse(Precio.Text));
                datarow["CANTIDAD"] = int.Parse(Cantidad.Text);
                datarow["DESC"] = string.Format("{0:#,##0.00}", float.Parse(Descuento.Text));
                datarow["SUB_TOTAL"] = string.Format("{0:#,##0.00}", float.Parse(Sub_Total.Text));
                dt.Rows.Add(datarow);
                Session["dt"] = dt;
                GridView1.DataSource = new DataView(dt);
                GridView1.DataBind();
            }
            Label16.Text = Convert.ToString(float.Parse(Label16.Text) + 1);
            totalsub.Text = string.Format("{0:#,##0.00}", float.Parse(totalsub.Text) + float.Parse(Sub_Total.Text));
            float canclularitbis = float.Parse(Sub_Total.Text) * float.Parse("0.18");
            totalitbis.Text = string.Format("{0:#,##0.00}", float.Parse(totalitbis.Text) + canclularitbis);
            totalgral.Text = string.Format("{0:#,##0.00}", float.Parse(totalsub.Text) + float.Parse(totalitbis.Text));

            Limpiar_Click(sender, e);
        }
    }
    protected void codcliente_TextChanged(object sender, EventArgs e)
    {
        string buscar_cliente = @"SELECT * FROM [10.0.0.54].AX50DOM_LIVE.DBO.CUSTTABLE WHERE ACCOUNTNUM='" + codcliente.Text + "'";
        data = new SqlDataAdapter(buscar_cliente, con.cn);
        table = new DataTable();
        data.Fill(table);

        if (table.Rows.Count > 0)
        {
            if (table.Rows[0]["wpmsalesunitid"].ToString() != Session["idvendedor"].ToString())
            {
                var a = "alert(\'El cliente que desea buscar no pertenece a su cartera, verifique el código e intente nuevamente\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                codcliente.Text = string.Empty;
            }
            else
            {
                codcliente.ReadOnly = true;
                nombrecliente.Text = HttpUtility.HtmlDecode(table.Rows[0]["NAME"].ToString());
                RNC.Text = HttpUtility.HtmlDecode(table.Rows[0]["VATNUM"].ToString());
                nombrecliente.ReadOnly = true;
            }
        }
        else
        {
            var a = "alert(\'No hemos encontrado ningún cliente que contenga el número escrito pero puede continuar con un cliente que no exista.\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            nombrecliente.Focus();
        }
    }
    protected void cancelar_Click(object sender, EventArgs e)
    {        
        codcliente.ReadOnly = false;
        codcliente.Text = string.Empty;
        nombrecliente.Text = string.Empty;
        nombrecliente.ReadOnly = false;
        codArticulo.Text = string.Empty;
        Descripcion.Text = string.Empty;
        Precio.Text = string.Empty;
        Cantidad.Text = string.Empty;
        RNC.Text = string.Empty;
        Sub_Total.Text = "0.00";
        totalsub.Text = "0.00";
        totalitbis.Text = "0.00";
        totalgral.Text = "0.00";
        Descuento.Text = "0";
        //Page_Load(sender, e);
        Session["dt"] = null;
        creaciontabletemp();
        GridView1.DataSource = new DataView(dt);
        GridView1.DataBind();
        Session["numcotiza"] = null;

        if (Session["Modi"] != null)
        {
            Session["Modi"] = null;
            Consultar_Click(sender, e);
        }
    }
    public void recalcularcotizacion()
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            totalsub.Text = string.Format("{0:#,##0.00}", float.Parse(totalsub.Text) + float.Parse(GridView1.Rows[i].Cells[6].Text));
            float canclularitbis = float.Parse(totalsub.Text) * float.Parse("0.18");
            totalitbis.Text = string.Format("{0:#,##0.00}", canclularitbis);
            totalgral.Text = string.Format("{0:#,##0.00}", float.Parse(totalsub.Text) + float.Parse(totalitbis.Text));
        }
        
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow ro = GridView1.Rows[e.RowIndex];
        DataTable dttemp = (Session["dt"]) as DataTable;
        for (int i = 0; i < dttemp.Rows.Count; i++)
        {
            if (dttemp.Rows[i]["COD_ART"].ToString() == ro.Cells[1].Text)
            {
                totalsub.Text = string.Format("{0:#,##0.00}", float.Parse(totalsub.Text) - float.Parse(ro.Cells[6].Text));
                float canclularitbis = float.Parse(ro.Cells[6].Text) * float.Parse("0.18");
                totalitbis.Text = string.Format("{0:#,##0.00}", float.Parse(totalitbis.Text) - canclularitbis);
                totalgral.Text = string.Format("{0:#,##0.00}", float.Parse(totalsub.Text) + float.Parse(totalitbis.Text));
                dttemp.Rows.RemoveAt(i);
                Label16.Text = Convert.ToString(float.Parse(Label16.Text) - 1);
            }
        }        
        Session["dt"] = dttemp;
        GridView1.DataSource = new DataView(dttemp);
        GridView1.DataBind();
        //recalcularcotizacion();
    }
    protected void Limpiar_Click(object sender, EventArgs e)
    {
        codArticulo.Text = string.Empty;
        Descripcion.Text = string.Empty;
        Precio.Text = string.Empty;
        Cantidad.Text = string.Empty;
        Descuento.Text = string.Empty;
        Sub_Total.Text = "0.00";
        Descuento.Text = "0";
    }
    protected void Buscar_Articulos_Click1(object sender, EventArgs e)
    {
        if(codArticulo.Text != string.Empty)
        {
            buscardescripcion.Text = codArticulo.Text;
            Panel1_ModalPopupExtender.Show();
            Buscardes_Click(sender, e);
        }
        else
        {        
        string articulosgeneral = @"SELECT A.ITEMID as COD_ART, B.ITEMNAME AS DECRIPCION,CONVERT(VARCHAR,CAST(A.PRICE * 8 AS MONEY),1) AS PECIO	                             
                                    FROM [10.0.0.54].AX50DOM_LIVE.DBO.INVENTTABLEMODULE A
                                    INNER JOIN [10.0.0.54].AX50DOM_LIVE.DBO.INVENTTABLE B ON A.ITEMID=B.ITEMID WHERE A.MODULETYPE=1";
        data = new SqlDataAdapter(articulosgeneral, con.cn);
        table = new DataTable();
        data.Fill(table);

        GridView2.DataSource = new DataView(table);
        GridView2.DataBind();

        Panel1_ModalPopupExtender.Show();
        }
    }
    protected void Buscardes_Click(object sender, EventArgs e)
    {
        string buscardescr = @"SELECT A.ITEMID as COD_ART, B.ITEMNAME AS DECRIPCION, CONVERT(VARCHAR,CAST(A.PRICE * 8 AS MONEY),1) AS PECIO FROM [10.0.0.54].AX50DOM_LIVE.DBO.INVENTTABLEMODULE A
                                INNER JOIN [10.0.0.54].AX50DOM_LIVE.DBO.INVENTTABLE B ON A.ITEMID=B.ITEMID
                                WHERE A.MODULETYPE=1 AND REPLACE(A.ITEMID,' ','') + B.ITEMNAME LIKE '%" + buscardescripcion.Text + "%'";
        data = new SqlDataAdapter(buscardescr, con.cn);
        table = new DataTable();
        data.Fill(table);

        GridView2.DataSource = new DataView(table);
        GridView2.DataBind();

        Panel1_ModalPopupExtender.Show();
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView row = (GridView)sender;
        row.PageIndex = e.NewPageIndex;
        Buscardes_Click(sender, e);

        GridView2.DataSource = new DataView(table);
        GridView2.DataBind();

        Panel1_ModalPopupExtender.Show();
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow ro = GridView2.Rows[index];

        if (e.CommandName == "Seleccionar")
        {
            codArticulo.Text = HttpUtility.HtmlDecode(ro.Cells[1].Text);
            Descripcion.Text = HttpUtility.HtmlDecode(ro.Cells[2].Text);
            Precio.Text = ro.Cells[3].Text;
            Cantidad.Focus();

            Panel1_ModalPopupExtender.Hide();
        }
    }
    protected void Generar_Click(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(1500);
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Substring(confirmValue.Length - 2) == "Si")
            {
                if (GridView1.Rows.Count <= 0 || totalsub.Text == "0" || totalitbis.Text == "0.00" || totalgral.Text == "0.00")
                {
                    var a = "alert(\'Para poder generar esta cotización es necesario que inserte los artículos y la información de los clientes.\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                }
                else 
                {
                    if (Session["Modi"] != null)
                    {
                        con.cn.Open();
                        string borrardeta = "DELETE FROM DESARROLLO.CONVEN.COTIZACIONES_DET WHERE IDPEDIDO=" + int.Parse(Session["Modi"].ToString()) + "";
                        com = new SqlCommand(borrardeta, con.cn);
                        com.UpdatedRowSource = UpdateRowSource.None;
                        com.ExecuteNonQuery();

                        string borrarcabe = "DELETE FROM DESARROLLO.CONVEN.COTIZACIONES_CAB WHERE IDCOTIZACION=" + int.Parse(Session["Modi"].ToString()) + "";
                        com = new SqlCommand(borrarcabe, con.cn);
                        com.UpdatedRowSource = UpdateRowSource.None;
                        com.ExecuteNonQuery();
                        con.cn.Close();
                    }

                    con.cn.Open();
                    string insertarpedidosespecialescab = "DESARROLLO.CONVEN.InsertarcotizacionesCab";
                    com = new SqlCommand(insertarpedidosespecialescab, con.cn);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@CODCLIENTE", SqlDbType.VarChar, 15).Value = codcliente.Text;
                    com.Parameters.Add("@NOMBRECLIENTE", SqlDbType.VarChar, 70).Value = HttpUtility.HtmlDecode(nombrecliente.Text);
                    com.Parameters.Add("RNC", SqlDbType.VarChar, 13).Value = RNC.Text;
                    com.Parameters.Add("@VENDEDOR", SqlDbType.VarChar, 30).Value = HttpUtility.HtmlDecode(vendedor.Text);
                    com.Parameters.Add("@SUBTOTAL", SqlDbType.Money).Value = float.Parse(totalsub.Text);
                    com.Parameters.Add("@ITBIS", SqlDbType.Money).Value = float.Parse(totalitbis.Text);
                    com.Parameters.Add("@TOTALGRAL", SqlDbType.Money).Value = float.Parse(totalgral.Text);
                    if (Session["Modi"] == null) { com.Parameters.Add("@MODIFICAR", SqlDbType.Int).Value = 0; }
                    else { com.Parameters.Add("@MODIFICAR", SqlDbType.Int).Value = int.Parse(Session["Modi"].ToString()); }
                    com.UpdatedRowSource = UpdateRowSource.None;
                    com.ExecuteNonQuery();
                    con.cn.Close();

                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        con.cn.Open();
                        string InsertarPedidosEspecialesDet = "DESARROLLO.CONVEN.InsertarcotizacionesDet";
                        com = new SqlCommand(InsertarPedidosEspecialesDet, con.cn);
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.Add("@CODARTICULO", SqlDbType.VarChar, 20).Value = GridView1.Rows[i].Cells[1].Text;
                        com.Parameters.Add("@DEDSCRIPCION", SqlDbType.VarChar, 70).Value = HttpUtility.HtmlDecode(GridView1.Rows[i].Cells[2].Text);
                        com.Parameters.Add("@PRECIO", SqlDbType.Money).Value = float.Parse(GridView1.Rows[i].Cells[3].Text);
                        com.Parameters.Add("@CANTIDAD", SqlDbType.Int).Value = int.Parse(GridView1.Rows[i].Cells[4].Text);
                        com.Parameters.Add("@DESCUENTO", SqlDbType.Money).Value = float.Parse(GridView1.Rows[i].Cells[5].Text);
                        com.Parameters.Add("@SUBTOTAL", SqlDbType.Money).Value = float.Parse(GridView1.Rows[i].Cells[6].Text);
                        if (Session["Modi"] == null) { com.Parameters.Add("@MODIFICAR", SqlDbType.Int).Value = 0; }
                        else { com.Parameters.Add("@MODIFICAR", SqlDbType.Int).Value = int.Parse(Session["Modi"].ToString()); }
                        com.UpdatedRowSource = UpdateRowSource.None;
                        com.ExecuteNonQuery();
                        con.cn.Close();
                    }
                    var a = "alert(\'Cotización generada correctamente, gracias por usar nuestra plataforma.\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);

                    if (Session["Modi"] == null)
                    {
                        string NUMEROCOTIZ = @"SELECT MAX(A.IDCOTIZACION) AS COTIZACIONESNUM FROM Desarrollo.CONVEN.COTIZACIONES_CAB A
                        WHERE  NOMBRECLIENTE='" + nombrecliente.Text + "' AND VENDEDOR ='" + vendedor.Text + "' AND CONVERT(VARCHAR,A.FECHACREACION,112)=CONVERT(VARCHAR,GETDATE(),112)";
                        data = new SqlDataAdapter(NUMEROCOTIZ, con.cn);
                        table = new DataTable();
                        data.Fill(table);

                        cancelar_Click(sender, e);
                        Session["numcotiza"] = table.Rows[0]["COTIZACIONESNUM"].ToString();
                    }
                    else
                    {
                        int n = int.Parse(Session["Modi"].ToString());
                        Session["numcotiza"] = Session["Modi"].ToString();
                        Session["Modi"] = null;
                    }
                    Response.Redirect("~/cotizaciones.aspx");
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            con.cn.Close();
        }
    }
    protected void cancelarbus_Click(object sender, EventArgs e)
    {
        Panel1_ModalPopupExtender.Hide();
    }
    protected void Consultar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Detalle_Cotizaciones.aspx");
    }    
    protected void Descuento_TextChanged(object sender, EventArgs e)
    {
        if (Descuento.Text != string.Empty)
        {
            if (float.Parse(Descuento.Text) > 0)
            {
                //Descuento.Text = Convert.ToString(float.Parse(Descuento.Text) / 100);
                Sub_Total.Text = Convert.ToString(float.Parse(Cantidad.Text) * float.Parse(Precio.Text));
                descu = float.Parse(Sub_Total.Text) * float.Parse(Descuento.Text);
                Sub_Total.Text = string.Format("{0:#,##0.00}", float.Parse(Sub_Total.Text) - float.Parse(Sub_Total.Text) * (float.Parse(Descuento.Text) / 100));
            }
        }
    }
    protected void Precio_TextChanged(object sender, EventArgs e)
    {
        if (Cantidad.Text != string.Empty)
        {
            Sub_Total.Text = string.Format("{0:#,##0.00}", float.Parse(Precio.Text) * int.Parse(Cantidad.Text));            
        }
    }
}