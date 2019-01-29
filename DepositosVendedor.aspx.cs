using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

using System.Globalization;
using System.Text.RegularExpressions;
using System.Net.Mail;

public partial class DepositosVendedor : System.Web.UI.Page
{
    Conexion con = new Conexion();
    SqlCommand com = new SqlCommand();
    SqlDataAdapter data = new SqlDataAdapter();
    DataTable table = new DataTable();

    DataTable dt = new DataTable();  
        
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
            cargarestadodepositos();
        }
    }
    protected void buscar_Click(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(2000);

            if (codcliente.Text == string.Empty)
        {
            var a = "alert(\'Para poder buscar el saldo de algún cliente es necesario que escriba el número del cliente\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

            string Buscarsaldo = @"SELECT LTRIM(B.INVOICE) AS FACTURA, LTRIM(C.ACCOUNTNUM) AS ID,LTRIM(C.NAME) AS NOMBRE,
                                    CONVERT(VARCHAR,A.TRANSDATE, 101) AS FECHA, CONVERT(VARCHAR,CAST(B.AMOUNTCUR AS MONEY),1) AS MONTO_FAT, 
                                    CONVERT(VARCHAR,CAST(A.AMOUNTMST AS MONEY),1) AS SALDO, LTRIM(C.WPMSALESUNITID) AS VEND, ISNULL(D.CURRENCYCODE,'') AS MONEDA
                                    FROM [10.0.0.54].AX50DOM_LIVE.DBO.CUSTTRANSOPEN A INNER JOIN [10.0.0.54].AX50DOM_LIVE.DBO.CUSTTRANS B ON A.REFRECID=B.RECID
                                    INNER JOIN [10.0.0.54].AX50DOM_LIVE.DBO.CUSTTABLE C ON A.ACCOUNTNUM=C.ACCOUNTNUM 
                                    LEFT OUTER JOIN [10.0.0.54].AX50DOM_LIVE.DBO.CUSTINVOICEJOUR D ON B.ACCOUNTNUM=D.INVOICEACCOUNT  AND B.INVOICE=D.INVOICEID
                                    WHERE LTRIM(A.ACCOUNTNUM)='" + codcliente.Text + "' AND A.AMOUNTMST > 1";
            data = new SqlDataAdapter(Buscarsaldo, con.cn);
            table = new DataTable();
            data.Fill(table);

            if (table.Rows.Count > 0)
            {
                if (table.Rows[0]["VEND"].ToString() == Session["idvendedor"].ToString())
                {
                    GridView1.DataSource = new DataView(table);
                    GridView1.DataBind();
                }
                else if (table.Rows[0]["VEND"].ToString() == "")
                {
                    GridView1.DataSource = new DataView(table);
                    GridView1.DataBind();                    
                }
                else
                {
                    var a = "alert(\'El cliente que busca no pertenece a su cartera\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                }
            }
            else
            {
                var a = "alert(\'El cliente que busca no existe, verifique el código escrito e intente nuevamente\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            }
        }
        }
        catch (Exception)
        {            
            throw;
        }
    }
    protected void SubirPagos_Click(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(2000);
            if (codcliente.Text == string.Empty || ReciboCobro.Text == string.Empty || DropDownList1.Text == string.Empty || GridView1.Rows.Count<=0)
            {
                var a = "alert(\'Para poder subir un pago es necesario que seleccione las facturas a afectar y rellene las demas cajas de textos\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            }
            else if(ReciboCobro.Text.Length <= 4)
            {
                var a = "alert(\'El el recibo de cobro no es valido por que tiene menos de 5 ó 6 digistos, debe colocar un numero valido e intente nuevamente\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            }
            else
            {                                  
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow ro = GridView1.Rows[i];
                    CheckBox f = (CheckBox)ro.FindControl("Checkbox2");
                    TextBox tex = (TextBox)ro.FindControl("MontoPagado");                    

                    if (f.Checked == true && tex.Text != string.Empty)
                    {
                        MontoPagar.Text = string.Format("{0:#,##0.00}", float.Parse(MontoPagar.Text) + float.Parse(tex.Text));

                        if (Session["dt"] == null)
                        {                            
                            DataTable dt = creaciontabletemp();
                            DataRow datarow;
                            datarow = dt.NewRow();
                            datarow["COD_CLIENTE"] = codcliente.Text;
                            datarow["FACTURA"] = HttpUtility.HtmlDecode(GridView1.Rows[i].Cells[2].Text);
                            datarow["MONTO_PAGADO"] = tex.Text;
                            datarow["RECIBO_PAGO"] = ReciboCobro.Text;
                            datarow["TIPO_PAGO"] = DropDownList1.Text;
                            datarow["CUENTA"] = DropDownList2.Text;
                            datarow["BANCO"] = Banconombre.Text;
                            datarow["NO_CHEQUE"] = nocheque.Text;
                            datarow["MONEDA"] = HttpUtility.HtmlDecode(GridView1.Rows[i].Cells[9].Text);
                            dt.Rows.Add(datarow);
                            Session["dt"] = dt;
                            GridView2.DataSource = new DataView(dt);
                            GridView2.DataBind();
                        }
                        else
                        {                            
                            DataTable dt= (Session["dt"]) as DataTable;
                            DataRow datarow;
                            datarow = dt.NewRow();
                            datarow["COD_CLIENTE"] = codcliente.Text;
                            datarow["FACTURA"] = HttpUtility.HtmlDecode(GridView1.Rows[i].Cells[2].Text);
                            datarow["MONTO_PAGADO"] = tex.Text;
                            datarow["RECIBO_PAGO"] = ReciboCobro.Text;
                            datarow["TIPO_PAGO"] = DropDownList1.Text;
                            datarow["CUENTA"] = DropDownList2.Text;
                            datarow["BANCO"] = Banconombre.Text;
                            datarow["NO_CHEQUE"] = nocheque.Text;
                            datarow["MONEDA"] = HttpUtility.HtmlDecode(GridView1.Rows[i].Cells[9].Text);
                            dt.Rows.Add(datarow);
                            Session["dt"] = dt;
                            GridView2.DataSource = new DataView(dt);
                            GridView2.DataBind();
                        }                        
                    }
                    else if (f.Checked == true && tex.Text == string.Empty)
                    {
                        var a = "alert(\'Lo sentimos pero no podemos subir un pago si usted no digita el monto pagado por pate del cliente\');";
                        ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                        tex.Focus();
                    }
                    else if (f.Checked == false && tex.Text != string.Empty)
                    {
                        var a = "alert(\'Lo sentimos pero no podemos subir un pago si usted no selecciona la factura aunque haya colocado el monto pagado por le cliente\');";
                        ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                    }
                }
                limpiar_Click(sender, e);
            }                        
         }
        catch (Exception)
        {            
            throw;
        }
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            GridViewRow ro = GridView1.Rows[i];
            CheckBox ch = (CheckBox)sender;
            CheckBox ch2 = (CheckBox)ro.FindControl("CheckBox2");
            TextBox tex = (TextBox)ro.FindControl("MontoPagado");

            if (ch.Checked == true)
            {
                ch2.Checked = true;
               // tex.Enabled = true;  
            }
            else
            {
                ch2.Checked = false;
                //tex.Enabled = false; 
            }            
        }
    }
    public DataTable creaciontabletemp()
    {
        Session["dt"] = null;           
        dt.Columns.Add("COD_CLIENTE", typeof(string));
        dt.Columns.Add("FACTURA", typeof(string));
        dt.Columns.Add("MONTO_PAGADO", typeof(string));
        dt.Columns.Add("RECIBO_PAGO", typeof(string));
        dt.Columns.Add("TIPO_PAGO", typeof(string));
        dt.Columns.Add("CUENTA", typeof(string));
        dt.Columns.Add("BANCO", typeof(string));
        dt.Columns.Add("NO_CHEQUE", typeof(string));
        dt.Columns.Add("MONEDA", typeof(string));
        return dt;
    }
    protected void limpiar_Click(object sender, EventArgs e)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        codcliente.Text = string.Empty;
        DropDownList1.Text = string.Empty;
        DropDownList2.Text = string.Empty;
        ReciboCobro.Text = string.Empty;
        nocheque.Text = string.Empty;
        //MontoPagar.Text = string.Empty;
        Banconombre.Text = string.Empty;
        codcliente.Focus();
        //Session["dt"] = null;  
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView row = (GridView)sender;
        row.PageIndex = e.NewPageIndex;
        buscar_Click(sender, e);
        GridView1.DataSource = new DataView(table);
        GridView1.DataBind();
    }
    protected void ReciboCobro_TextChanged(object sender, EventArgs e)
    {
        //if (GridView2.Rows.Count > 0)
        //{
        //    for (int i = 0; i < GridView2.Rows.Count; i++)
        //    {                 
        //         if (GridView2.Rows[i].Cells[4].Text == ReciboCobro.Text)
        //         {
        //             var a = "alert(\'El numero de recibo ya fue usado en otro cliente, favor revisar e intenten nuevamente\');";
        //             ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
        //             ReciboCobro.Focus();
        //         }
        //       }
        //}
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.Text == "Cheque")
        {
            nocheque.ReadOnly = false;            
            nocheque.Focus();
            nocheque.Text = string.Empty;
        }
        else
        {            
            nocheque.ReadOnly = true;            
            nocheque.Text = string.Empty;
        }
    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {        
        GridViewRow ro = GridView2.Rows[e.RowIndex];        
        DataTable dttemp = (Session["dt"]) as DataTable;     
        for (int i = 0; i < dttemp.Rows.Count; i++)
        {                                
            if (dttemp.Rows[i]["FACTURA"].ToString() == ro.Cells[2].Text) 
            {
                MontoPagar.Text = string.Format("{0:#,##0.00}", float.Parse(MontoPagar.Text) - float.Parse(ro.Cells[3].Text));
                dttemp.Rows.RemoveAt(i);
            }                                        
        }
        Session["dt"] = dttemp;
        GridView2.DataSource = new DataView(dttemp);
        GridView2.DataBind();              
    }
    protected void Cancelar_Click(object sender, EventArgs e)
    {        
        codcliente.Text = string.Empty;
        codcliente.Focus();

        MontoPagar.Text = "0";
        GridView1.DataSource = null;
        GridView1.DataBind();

        GridView2.DataSource=null;
        GridView2.DataBind();
        Session["dt"] = null;           
    }
    protected void enviarpagos_Click(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(1500);
              string confirmValue = Request.Form["confirm_value"];
              if (confirmValue.Substring(confirmValue.Length - 2) == "Si")
              {
                  if (float.Parse(MontoPagar.Text) <= 0 || GridView2.Rows.Count <= 0)
                  {
                      var a = "alert(\'Lo sentimos, pero no podemos enviar un deposito sin que primero haya subido todos los documentos a los que se le valla a aplicar algún pago\');";
                      ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                      codcliente.Focus();
                  }
                  else
                  {
                      con.cn.Open();
                      string insertarCabe = @"Desarrollo.ConVen.InsertarDepositos_Cabe";
                      com = new SqlCommand(insertarCabe, con.cn);
                      com.CommandType = CommandType.StoredProcedure;
                      com.Parameters.Add("@MONTOPAGAR", SqlDbType.Float).Value = float.Parse(MontoPagar.Text);
                      com.Parameters.Add("@CODVENDEDOR", SqlDbType.VarChar, 10).Value = Session["idvendedor"].ToString();
                      com.Parameters.Add("@ENVIADOPOR", SqlDbType.VarChar, 25).Value = Session["NombreCompleto"].ToString();                      
                      com.UpdatedRowSource = UpdateRowSource.None;
                      com.ExecuteNonQuery();
                      con.cn.Close();

                      int buscarcodigo = 0;
                      for (int i = 0; i < GridView2.Rows.Count; i++)
                      {                          
                          con.cn.Open();
                          string InsertarDetalle = @"Desarrollo.ConVen.InsertarDepositoDet";
                          com = new SqlCommand(InsertarDetalle, con.cn);
                          com.CommandType = CommandType.StoredProcedure;
                          com.Parameters.Add("@BUSCARCODIGO", SqlDbType.Int).Value = buscarcodigo;                  
                          com.Parameters.Add("@CODCLIENTE", SqlDbType.VarChar, 10).Value = GridView2.Rows[i].Cells[1].Text.Trim();
                          com.Parameters.Add("@FACTURA", SqlDbType.VarChar, 15).Value =HttpUtility.HtmlDecode(GridView2.Rows[i].Cells[2].Text.Trim());
                          com.Parameters.Add("@SALDO_FACTURA", SqlDbType.Float).Value = float.Parse(GridView2.Rows[i].Cells[3].Text);
                          com.Parameters.Add("@RECIBOPAGO", SqlDbType.Int).Value = int.Parse(GridView2.Rows[i].Cells[4].Text);
                          com.Parameters.Add("@TIPOPAGO", SqlDbType.VarChar, 15).Value = GridView2.Rows[i].Cells[5].Text.Trim();
                          com.Parameters.Add("@MONEDA", SqlDbType.VarChar, 4).Value = HttpUtility.HtmlDecode(GridView2.Rows[i].Cells[9].Text.Trim());
                          com.Parameters.Add("@CUENTA", SqlDbType.VarChar, 30).Value = GridView2.Rows[i].Cells[6].Text.Trim();
                          
                          string ban = HttpUtility.HtmlDecode(GridView2.Rows[i].Cells[8].Text.Trim());
                          if (ban.Trim() != "")
                          {
                              com.Parameters.Add("@CHEQUENO", SqlDbType.Int).Value = int.Parse(GridView2.Rows[i].Cells[8].Text.Trim());
                          }
                          ban = HttpUtility.HtmlDecode(GridView2.Rows[i].Cells[7].Text.Trim().Trim());
                          if (ban.Trim() != "")
                          {
                              com.Parameters.Add("@BANCO", SqlDbType.VarChar, 25).Value = GridView2.Rows[i].Cells[7].Text.Trim();
                          }

                          com.UpdatedRowSource = UpdateRowSource.None;
                          com.ExecuteNonQuery();
                          con.cn.Close();
                          buscarcodigo++;
                      }
                      
                      #region Enviar correo
                      MailMessage mail = new MailMessage();
                      SmtpClient SmtpServer = new SmtpClient("mail.wurth.com.do");
                      SmtpServer.Port = 25;
                      SmtpServer.Credentials = new System.Net.NetworkCredential("servicioswurth@wurth.com.do", "Wservice01*");
                      mail.From = new MailAddress("servicioswurth@wurth.com.do");

                      mail.Subject = "Registro de Pago " + Session["idvendedor"].ToString();
                      mail.Body = "Después de un Cordial Saludo \n\n Les comunico que acabo de enviar un deposito para que el mismo sea validado cuando Daniel realice la actualización de registros "+"\n\n Se despide de ustedes la o el vendedor o vendedora " + Session["idvendedor"].ToString() + " \n\nFavor no responder este mensaje ya que fue generado por sistema automático";
                      mail.To.Add("cobros@wurth.com.do");
                      //mail.CC.Add("rferrer@wurth.com.do");
                      SmtpServer.Send(mail);
                      #endregion
                      var a = "alert(\'Pago Enviado correctamente, gracias por usar nuestra plataforma...\');";
                      ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);

                      Cancelar_Click(sender, e);
                      cargarestadodepositos();
                  }
              }
        }
        catch (Exception)
        {            
            throw;
        }
    }
    public void cargarestadodepositos()
    {
        string estadodepositos = @"DESARROLLO.CONVEN.ESTADODEPOSITOS";
        com = new SqlCommand(estadodepositos, con.cn);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@VENDEDOR", SqlDbType.VarChar, 10).Value = Session["idvendedor"].ToString();
        data = new SqlDataAdapter(com);
        table = new DataTable();
        data.Fill(table);

        GridView3.DataSource = new DataView(table);
        GridView3.DataBind();        
    }
    private void habilitarmenu()
    {
        var Menu = Page.Master.FindControl("Menu1") as Menu;
        if (Session["JEFAZONA"].ToString() == "1")
        {
            Menu.Visible = true;
        }
    }
    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView row = (GridView)sender;
        row.PageIndex = e.NewPageIndex;

        cargarestadodepositos();
    }
    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        System.Threading.Thread.Sleep(1500);
        if (e.CommandName == "Detalle")
        {
            string buscardetallependiente = @" SELECT B.CODCLIENTE AS CLIENTE,LTRIM(A.NAME) AS NOMBRE, B.FACTURA, CONVERT(VARCHAR,CAST(B.SALDO_FACTURA AS MONEY),1) AS MONTO_PAGADO,
                                           CONVERT(VARCHAR,B.FECHAENVIO,105) AS FECHA_DEPOSITO, B.RECIBOPAGO AS REC_PAGO,B.TIPOPAGO, B.CHEQUENO, B.BANCO, B.MONEDA,
                                           CASE WHEN B.ESTADO = 1 THEN 'APLICADO' WHEN B.ESTADO = 0 THEN 'PENDIENTE' END AS ESTADO FROM Desarrollo.ConVen.REGISTROPAGOS_DETALLE B
                                           INNER JOIN [10.0.0.54].AX50DOM_LIVE.DBO.CUSTTABLE A ON LTRIM(A.ACCOUNTNUM)=LTRIM(B.CODCLIENTE)
                                           WHERE B.CODPAGO='" + GridView3.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text + "'";
            data = new SqlDataAdapter(buscardetallependiente, con.cn);
            table = new DataTable();
            data.Fill(table);
            GridView4.DataSource = new DataView(table);
            GridView4.DataBind();
        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {        
        if (DropDownList2.Text == "702-55627-5" || DropDownList2.Text == "718-75798-2")
        {
            Banconombre.Text = "Banco Popular";
        }
        else if (DropDownList2.Text == "2192982-001-7" || DropDownList2.Text == "2192982-002-5")
        {
            Banconombre.Text = "Banco BHD Leon";
        }
        else if (DropDownList2.Text == "240-014077-9")
        {
            Banconombre.Text = "Banco de Reservas";
        }
    }
}