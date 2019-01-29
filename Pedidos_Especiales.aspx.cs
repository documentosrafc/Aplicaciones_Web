using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Globalization;

public partial class Pedidos_Especiales : System.Web.UI.Page
{
    Conexion con = new Conexion();
    SqlCommand com = new SqlCommand();
    SqlDataAdapter data = new SqlDataAdapter();
    DataTable table = new DataTable();
    DataTable dt = new DataTable();

    string filename;
    string filename1;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Form.Attributes.Add("autocomplete", "off");
        if (Session["NombreCompleto"] == null)
        {
            Response.Redirect("~/Default.aspx");
        }
        if (!IsPostBack)
        {
            Session["NombreArchivo"] = null;
            Session["NombreArchivo1"] = null;
            habilitarmenu();
            cargarpantalla();
            Session["dt"] = null;
            creaciontabletemp();
            GridView1.DataSource = new DataView(dt);
            GridView1.DataBind();
            vendedor.Text = Session["NombreCompleto"].ToString();
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
        dt.Columns.Add("SUB_TOTAL", typeof(float));
        return dt;
    }
    public void cargarpantalla()
    {
        string cargarAereos = @"SELECT NOMBRE_AEREO FROM Desarrollo.Dispo.AEREOS WHERE ESTADO=0";
        data = new SqlDataAdapter(cargarAereos, con.cn);
        table = new DataTable();
        data.Fill(table);        

        DropDownList1.Items.Clear();
        for (int i = 0; i < table.Rows.Count; i++)
        {
            if (DropDownList1.Items.Count <= 0)
            {
                DropDownList1.Items.Add("");
                DropDownList1.Items.Add(table.Rows[i]["NOMBRE_AEREO"].ToString());
            }
            else
            {
                DropDownList1.Items.Add(table.Rows[i]["NOMBRE_AEREO"].ToString());
            }
        }
    }
    protected void SubirArticulo_Click(object sender, EventArgs e)
    {
        if (DropDownList1.Text == string.Empty || codcliente.Text == string.Empty || nombrecliente.Text == string.Empty || codArticulo.Text == string.Empty || Descripcion.Text == string.Empty || Precio.Text == string.Empty || Cantidad.Text == string.Empty || Sub_Total.Text == string.Empty)
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
        else if (float.Parse(Cantidad.Text) < 1)
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
            string consultarexistencia = @"SELECT ISNULL(SUM(B.AVAILPHYSICAL),0) AS DISPONIBLE_FISICO FROM [10.0.0.54].AX50DOM_LIVE.DBO.INVENTSUM B 
                                            INNER JOIN [10.0.0.54].AX50DOM_LIVE.DBO.INVENTDIM C ON B.INVENTDIMID=C.INVENTDIMID
                                            WHERE B.CLOSEDQTY = 0 AND LTRIM(B.ITEMID)='" + codArticulo.Text + "' AND C.INVENTLOCATIONID IN('WH1','WH2','WH4', 'WH14','WH17')";
            com = new SqlCommand(consultarexistencia, con.cn);
            data = new SqlDataAdapter(com);
            table = new DataTable();
            data.Fill(table);

            if (float.Parse(table.Rows[0]["DISPONIBLE_FISICO"].ToString()) < float.Parse(Cantidad.Text))
            {
                if (Session["dt"] == null)
                {
                    DataTable dt = creaciontabletemp();
                    DataRow datarow;
                    datarow = dt.NewRow();
                    datarow["COD_ART"] = codArticulo.Text;
                    datarow["DESCRICPION"] = Descripcion.Text;
                    datarow["PRECIO"] = float.Parse(Precio.Text);
                    datarow["CANTIDAD"] = int.Parse(Cantidad.Text);
                    datarow["SUB_TOTAL"] = float.Parse(Sub_Total.Text);
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
                    datarow["PRECIO"] = float.Parse(Precio.Text);
                    datarow["CANTIDAD"] = int.Parse(Cantidad.Text);
                    datarow["SUB_TOTAL"] = float.Parse(Sub_Total.Text);
                    dt.Rows.Add(datarow);
                    Session["dt"] = dt;
                    GridView1.DataSource = new DataView(dt);
                    GridView1.DataBind();
                }

                totalsub.Text = string.Format("{0:#,##0.00}", float.Parse(totalsub.Text) + float.Parse(Sub_Total.Text));
                float canclularitbis = float.Parse(Sub_Total.Text) * float.Parse("0.18");
                totalitbis.Text = string.Format("{0:#,##0.00}", float.Parse(totalitbis.Text) + canclularitbis);
                totalgral.Text = string.Format("{0:#,##0.00}", float.Parse(totalsub.Text) + float.Parse(totalitbis.Text));

                Limpiar_Click(sender, e);
            }
            else
            {
                var a = "alert(\'Esté artículo cuenta con disponibilidad en nuestro almacén, favor revisar\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            }
        }
    }
    protected void Cantidad_TextChanged(object sender, EventArgs e)
    {
        //CantSolicitada.Text = string.Format("{0:#,##0}",float.Parse(Cantidad.Text)*float.Parse(unidadenbasado.Text));
        if (Cantidad.Text != string.Empty)
        {
            Sub_Total.Text = string.Format("{0:#,##0.00}", float.Parse(Precio.Text) * int.Parse(Cantidad.Text));
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.Text == string.Empty)
        {
            var a = "alert(\'Lo sentimos, pero para poder realizar una busqueda de un aereo es necesario que seleccione el nombre del mismo\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
        }
        else
        {
            string buscarinformacionaereo = "SELECT * FROM Desarrollo.Dispo.AEREOS WHERE ESTADO=0 AND NOMBRE_AEREO='" + DropDownList1.Text + "'";
            com = new SqlCommand(buscarinformacionaereo, con.cn);
            data = new SqlDataAdapter(com);
            table = new DataTable();
            data.Fill(table);

            if (table.Rows.Count > 0)
            {
                DropDownList1.Enabled = false;
                fechalimite.Text = DateTime.Parse(table.Rows[0]["FECHA_LIMITE"].ToString()).ToString("dd/MM/yyyy");
                fechallegada.Text = DateTime.Parse(table.Rows[0]["FECHA_LLEGADA"].ToString()).ToString("dd/MM/yyyy");
            }
            else
            {
                var a = "alert(\'Lo sentimosm pero no hemos encontrado ningún aereo con ese nombre\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            }
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
        DropDownList1.Enabled = true;
        DropDownList1.Text = string.Empty;
        codcliente.ReadOnly = false;
        codcliente.Text = string.Empty;
        nombrecliente.Text = string.Empty;
        nombrecliente.ReadOnly = false;
        fechalimite.Text = string.Empty;
        fechallegada.Text = string.Empty;
        codArticulo.Text = string.Empty;
        Descripcion.Text = string.Empty;
        Precio.Text = string.Empty;
        Cantidad.Text = string.Empty;    
        Sub_Total.Text = "0.00";
        totalsub.Text = "0.00";
        totalitbis.Text = "0.00";
        totalgral.Text = "0.00";
        Page_Load(sender, e);
        Session["dt"] = null;
        creaciontabletemp();
        GridView1.DataSource = new DataView(dt);
        GridView1.DataBind();
        Session["NombreArchivo"] = null;
        Session["NombreArchivo1"] = null;
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow ro = GridView1.Rows[e.RowIndex];
        DataTable dttemp = (Session["dt"]) as DataTable;
        for (int i = 0; i < dttemp.Rows.Count; i++)
        {
            if (dttemp.Rows[i]["COD_ART"].ToString() == ro.Cells[1].Text)
            {             
                totalsub.Text = string.Format("{0:#,##0.00}", float.Parse(totalsub.Text) - float.Parse(ro.Cells[5].Text));
                float canclularitbis = float.Parse(ro.Cells[5].Text) * float.Parse("0.18");
                totalitbis.Text = string.Format("{0:#,##0.00}", float.Parse(totalitbis.Text) - canclularitbis);
                totalgral.Text = string.Format("{0:#,##0.00}", float.Parse(totalsub.Text) + float.Parse(totalitbis.Text));
                dttemp.Rows.RemoveAt(i);
            }
        }
        Session["dt"] = dttemp;
        GridView1.DataSource = new DataView(dttemp);
        GridView1.DataBind();
    }
    //protected void codArticulo_TextChanged(object sender, EventArgs e)
    //{
//        if (codArticulo.Text == string.Empty)
//        {
//            var a = "alert(\'Para poder buscar un articulo es necesario que coloque el código del mismo\');";
//            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
//        }
//        else
//        {
//            string buscararticulo = @"SELECT max(WPMPackageSize) as WPMPackageSize FROM [10.0.0.54].AX50DOM_LIVE.DBO.PriceDiscTable
//                                    WHERE ITEMRELATION='" + codArticulo.Text + "'";
//            data = new SqlDataAdapter(buscararticulo, con.cn);
//            table = new DataTable();
//            data.Fill(table);

//            if (table.Rows.Count > 0)
//            {
//                unidadenbasado.Text = table.Rows[0]["WPMPackageSize"].ToString();                
//            }
//            else
//            {
//                var a = "alert(\'No hemos encontrado ninguna unidad de enbasado para este artículo.\');";
//                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
//            }
//        }
    //}
    protected void Button1_Click1(object sender, EventArgs e)
    {
        //string vtn = "window.open('test.aspx','Dates','scrollbars=no,resizable=no','height=300', 'width=300')";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
    }
    protected void Limpiar_Click(object sender, EventArgs e)
    {
        codArticulo.Text = string.Empty;
        Descripcion.Text = string.Empty;
        Precio.Text = string.Empty;
        Cantidad.Text = string.Empty;
        //unidadenbasado.Text = string.Empty;
        //CantSolicitada.Text = string.Empty;
        Sub_Total.Text = "0.00";
    }
    protected void Buscar_Articulos_Click1(object sender, EventArgs e)
    {
        string articulosgeneral = @"SELECT A.ITEMID as COD_ART, B.ITEMNAME AS DECRIPCION,CONVERT(VARCHAR,CAST(A.PRICE * 8 AS MONEY),1) AS PECIO	                             
                                    FROM [10.0.0.54].AX50DOM_LIVE.DBO.INVENTTABLEMODULE A
                                    INNER JOIN [10.0.0.54].AX50DOM_LIVE.DBO.INVENTTABLE B ON A.ITEMID=B.ITEMID WHERE A.MODULETYPE = 1 AND B.PrimaryVendorId = 1";

        data = new SqlDataAdapter(articulosgeneral, con.cn);
        table = new DataTable();
        data.Fill(table);

        GridView2.DataSource = new DataView(table);
        GridView2.DataBind();

        Panel1_ModalPopupExtender.Show();      
    }
    protected void Buscardes_Click(object sender, EventArgs e)
    {
        string buscardescr = @"SELECT A.ITEMID as COD_ART, B.ITEMNAME AS DECRIPCION, CONVERT(VARCHAR,CAST(A.PRICE * 8 AS MONEY),1) AS PECIO FROM [10.0.0.54].AX50DOM_LIVE.DBO.INVENTTABLEMODULE A
                                INNER JOIN [10.0.0.54].AX50DOM_LIVE.DBO.INVENTTABLE B ON A.ITEMID=B.ITEMID
                                WHERE A.MODULETYPE=1 AND B.PrimaryVendorId = 1 AND REPLACE(REPLACE(A.ITEMID,'0',''),' ', '') + B.ITEMNAME LIKE '%" + buscardescripcion.Text + "%'";
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

                //codArticulo_TextChanged(sender, e);
                Panel1_ModalPopupExtender.Hide();
            }
    }
    protected void Generar_Click(object sender, EventArgs e)
    {
        try 
	     {	        
		System.Threading.Thread.Sleep(2500);
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue.Substring(confirmValue.Length - 2) == "Si")
        {
            if (GridView1.Rows.Count <= 0 || totalsub.Text == "0" || totalitbis.Text == "0.00" || totalgral.Text == "0.00")
            {
                var a = "alert(\'Para poder generar este pedido es necesario que inserte los artículos y la información de los clientes.\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            }
            else if (Session["NombreArchivo"] == null)
            {
                var a = "alert(\'Es necesario que suba el Pedido, el Backorder o la Orden ed Compra a la plataforma.\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            }
            //else if (Session["NombreArchivo1"] == null)
            //{
            //    var a = "alert(\'Es necesario que subas la orden de compra a la plataforma.\');";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            //}
            else
            {
                con.cn.Open();
                string insertarpedidosespecialescab = "DESARROLLO.DISPO.InsertarPedidosEspecialesCab";
                com = new SqlCommand(insertarpedidosespecialescab, con.cn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@NOMBREAEREO", SqlDbType.VarChar, 30).Value = DropDownList1.Text;
                com.Parameters.Add("@CODCLIENTE", SqlDbType.VarChar, 15).Value = codcliente.Text;
                com.Parameters.Add("@NOMBRECLIENTE", SqlDbType.VarChar, 70).Value = nombrecliente.Text;
                com.Parameters.Add("@VENDEDOR", SqlDbType.VarChar, 30).Value = vendedor.Text;
                com.Parameters.Add("@SUBTOTAL", SqlDbType.Money).Value = float.Parse(totalsub.Text);
                com.Parameters.Add("@ITBIS", SqlDbType.Money).Value = float.Parse(totalitbis.Text);
                com.Parameters.Add("@TOTALGRAL", SqlDbType.Money).Value = float.Parse(totalgral.Text);                
                com.UpdatedRowSource = UpdateRowSource.None;
                com.ExecuteNonQuery();
                con.cn.Close();
                
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    con.cn.Open();
                    string InsertarPedidosEspecialesDet = "DESARROLLO.DISPO.InsertarPedidosEspecialesDet";
                    com = new SqlCommand(InsertarPedidosEspecialesDet, con.cn);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@CODARTICULO", SqlDbType.VarChar, 20).Value = GridView1.Rows[i].Cells[1].Text;
                    com.Parameters.Add("@DEDSCRIPCION", SqlDbType.VarChar, 70).Value = GridView1.Rows[i].Cells[2].Text;
                    com.Parameters.Add("@PRECIO", SqlDbType.Money).Value = float.Parse(GridView1.Rows[i].Cells[3].Text);
                    com.Parameters.Add("@CANTIDAD", SqlDbType.Int).Value = int.Parse(GridView1.Rows[i].Cells[4].Text);
                    com.Parameters.Add("@SUBTOTAL", SqlDbType.Money).Value = float.Parse(GridView1.Rows[i].Cells[5].Text);
                    com.UpdatedRowSource = UpdateRowSource.None;
                    com.ExecuteNonQuery();
                    con.cn.Close();                    
                }
                #region Enviar correo
                string buscarultimonumeropedidoespecial = "SELECT MAX(CONVERT(INT,IDPEDIDO)) numero from desarrollo.dispo.pedidos_especiales_cab where vendedor='" + vendedor.Text + "'";
                data=new SqlDataAdapter(buscarultimonumeropedidoespecial, con.cn);
                table = new DataTable();
                data.Fill(table);
              
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("mail.wurth.com.do");
                SmtpServer.Port = 25;
                SmtpServer.Credentials = new System.Net.NetworkCredential("servicioswurth@wurth.com.do", "Wservice01*");
                mail.From = new MailAddress("servicioswurth@wurth.com.do");

                mail.Subject = "Pedido Especial número " + table.Rows[0]["numero"].ToString();
                if (Session["NombreArchivo"] != null)
                {
                    mail.Attachments.Add(new Attachment(Server.MapPath("~/Archivos Subidos/" + Session["NombreArchivo"].ToString())));                    
                }
                if (Session["NombreArchivo1"] != null)
                {
                    mail.Attachments.Add(new Attachment(Server.MapPath("~/Archivos Subidos/" + Session["NombreArchivo1"].ToString())));
                }
                mail.Body = "Después de un Cordial Saludo \n\n Les comunico que acabo de generar un pedido especial para que por favor el mismo sea tomado en cuenta para el aereo : " + DropDownList1.Text + " que esta supuesto a llegar en fecha " + fechallegada.Text + "  \n\n Se despide de usted el vendedor (a) " + vendedor.Text + " \n\nFavor no responder este mensaje ya que fue generado por un sistema automátizado";
                mail.To.Add("asoto@wurth.com.do");
                mail.To.Add("adelrosario@wurth.com.do");
                mail.CC.Add("rferrer@wurth.com.do");
                SmtpServer.Send(mail);
                #endregion
                
                var a = "alert(\'Pedido especial generado correctamente, gracias por usar nuestra plataforma.\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);

                cancelar_Click(sender, e);
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
        Response.Redirect("~/Detalle_Pedidos_Especiales.aspx");
    }
    protected void AsyncFileUpload1_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
        Session["NombreArchivo"] = filename;
        AsyncFileUpload1.SaveAs(Server.MapPath("~/Archivos Subidos/") + filename);  
    }
    protected void AsyncFileUpload2_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        filename1 = System.IO.Path.GetFileName(AsyncFileUpload2.FileName);
        Session["NombreArchivo1"] = filename1;
        AsyncFileUpload2.SaveAs(Server.MapPath("~/Archivos Subidos/") + filename1);  
    }
}