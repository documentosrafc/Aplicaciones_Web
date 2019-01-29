using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

using System.Text.RegularExpressions;

public partial class ReposicionGastos : System.Web.UI.Page
{
    Conexion con = new Conexion();
    SqlCommand com = new SqlCommand();
    SqlDataAdapter data = new SqlDataAdapter();
    DataTable table = new DataTable();
    DataTable dt = new DataTable();

    int numero = 0;
    string numeroreposicion = string.Empty;
    public string valor;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["NombreCompleto"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                habilitarmenu();
                this.Form.Attributes.Add("Autocomplete", "off");
                CargarTasas();
                cargarempleados();
                Cargar_reposiciones();
                Session["dt"] = null;

                DropDownList3.Text = Session["NombreCompleto"].ToString();                
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
    public void CargarTasas()
    {
        #region Tasas del Dia
        string EUROS = @"SELECT EXCHRATE/100 AS TASA_EURO FROM [10.0.0.54].AX50DOM_LIVE.DBO.EXCHRATES
                        WHERE FROMDATE = CONVERT(VARCHAR,GETDATE(),112) AND CURRENCYCODE ='EUR'";
        data = new SqlDataAdapter(EUROS, con.cn);
        table = new DataTable();
        data.Fill(table);

        if (table.Rows.Count > 0)
        {
            Label12.Text = string.Format("{0:#,##0.00}", float.Parse(table.Rows[0]["TASA_EURO"].ToString()));
        }
        else { Label12.Text = "0.00"; }


        string DOLARES = @"SELECT EXCHRATE/100 AS TASA_DOLAR FROM [10.0.0.54].AX50DOM_LIVE.DBO.EXCHRATES
                        WHERE FROMDATE = CONVERT(VARCHAR,GETDATE(),112)AND CURRENCYCODE ='USD'";
        data = new SqlDataAdapter(DOLARES, con.cn);
        table = new DataTable();
        data.Fill(table);

        if (table.Rows.Count > 0)
        {
            Label10.Text = string.Format("{0:#,##0.00}", float.Parse(table.Rows[0]["TASA_DOLAR"].ToString()));
        }
        else { Label10.Text = "0.00"; }
        #endregion
        Label8.Text = DateTime.Now.ToShortDateString();
    }
    public void cargarempleados()
    {
        #region Cargar Empleados Activos
        string CargarEmpleadosActivos = @"SELECT NO_VENDEDOR + ' '+ NOMBRE_APELLIDO AS EMPLEADOS FROM Desarrollo.ListCont.LISTADECONTACTO
                                            WHERE ESTADO=0 ORDER BY NO_VENDEDOR  ";
        data = new SqlDataAdapter(CargarEmpleadosActivos, con.cn);
        table = new DataTable();
        data.Fill(table);

        if (table.Rows.Count > 0)
        {
            //DropDownList3.Items.Clear();
            DropDownList2.Items.Clear();

            for (int i = 0; i < table.Rows.Count; i++)
            {
               // DropDownList3.Items.Add(table.Rows[i]["EMPLEADOS"].ToString());
                DropDownList2.Items.Add(table.Rows[i]["EMPLEADOS"].ToString());
            }
        }
        #endregion
    }
    public DataTable creaciontabletemp()
    {
        Session["dt"] = null;
        dt.Columns.Add("No", typeof(int));
        dt.Columns.Add("FECHA", typeof(string));
        dt.Columns.Add("FACT", typeof(string));
        dt.Columns.Add("CONSUMIO", typeof(string));
        dt.Columns.Add("CONCEPTO", typeof(string));
        dt.Columns.Add("NCF", typeof(string));
        dt.Columns.Add("MTO_RECLAMA", typeof(string));
        dt.Columns.Add("ACOMPANANTE", typeof(string));
        dt.Columns.Add("MONEDA", typeof(string));
        dt.Columns.Add("MTO_ORIGI", typeof(string));
        dt.Columns.Add("TASA", typeof(string));
        return dt;
    }
    public void BORRARCAMPOS()
    {
        FechaLiquidacion.Text = string.Empty;
        FacturaLiquidacion.Text = string.Empty;
        Empresadondecomsumio.Text = string.Empty;
        if (DropDownList2.Enabled == true) DropDownList2.Enabled = false;
        DropDownList4.Text = string.Empty;
        DropDownList1.Text = string.Empty;
        NCFLiquida.Text = string.Empty;
        MontoLiquida.Text = string.Empty;
        MontoTotal.Text = string.Empty;

    }
    public void GenerarCodigo()
    {
        string buscarnumero = "SELECT NUMERO FROM DESARROLLO.REPGAS.CONFIG";
        com = new SqlCommand(buscarnumero, con.cn);
        data = new SqlDataAdapter(com);
        table = new DataTable();
        data.Fill(table);
        numeroreposicion = table.Rows[0]["numero"].ToString();

        if (numeroreposicion.Length == 1)
        {
            numeroreposicion = "0000" + numeroreposicion;
        }
        else if (numeroreposicion.Length == 2)
        {
            numeroreposicion = "000" + numeroreposicion;
        }
        else if (numeroreposicion.Length == 3)
        {
            numeroreposicion = "00" + numeroreposicion;
        }
        else if (numeroreposicion.Length == 4)
        {
            numeroreposicion = "0" + numeroreposicion;
        }
    }
    public void Cargar_reposiciones()
    {
        string buscar_reposiciones = @"DESARROLLO.REPGAS.CargarReposicionesEnviadas";
        com = new SqlCommand(buscar_reposiciones, con.cn);
        com.Parameters.Add("@EMPLEADO", SqlDbType.VarChar, 100).Value = Session["NombreCompleto"].ToString();
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
        com.Parameters.Add("@EMPLEADO", SqlDbType.VarChar, 50).Value = Session["NombreCompleto"].ToString();
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
    protected void DropDownList1_TextChanged(object sender, EventArgs e)
    {
        if (DropDownList1.Text == "Dieta Acompanamiento")
        {
            DropDownList2.Enabled = true;
        }
        else if (DropDownList1.Text == "Dieta Acomp. X3")
        {
            DropDownList2.Enabled = true;
        }
        else
        {
            DropDownList2.Enabled = false;
        }
    }
    protected void MontoLiquida_TextChanged(object sender, EventArgs e)
    {
        if (MontoLiquida.Text != string.Empty)
        {
            if (DropDownList4.Text == "USD")
            {
                MontoTotal.Text = string.Format("{0:#,##0.00}", float.Parse(MontoLiquida.Text) * float.Parse(Label10.Text));
            }
            else if (DropDownList4.Text == "EUR")
            {
                MontoTotal.Text = string.Format("{0:#,##0.00}", float.Parse(MontoLiquida.Text) * float.Parse(Label12.Text));
            }
            else if (DropDownList4.Text == "DOP")
            {
                MontoTotal.Text = string.Format("{0:#,##0.00}", float.Parse(MontoLiquida.Text));
            }
        }
    }
    protected void subirfact_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(2000);
        if (FechaLiquidacion.Text == string.Empty || Empresadondecomsumio.Text == string.Empty || DropDownList4.Text == string.Empty || DropDownList1.Text == string.Empty || MontoLiquida.Text == string.Empty || MontoTotal.Text == string.Empty)
        {
            var a = "alert(\'Para poder subir los datos de una factura para su debida reposición es necesario que coloque las informaciones en los campos con asteriscos (*)\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);

            RequiredFieldValidator1.Enabled = true;
            RequiredFieldValidator2.Enabled = true;
            RequiredFieldValidator3.Enabled = true;
            RequiredFieldValidator4.Enabled = true;
            RequiredFieldValidator5.Enabled = true;
        }
        else
        {
            if (NCFLiquida.Text != string.Empty)
            {
                if (NCFLiquida.Text.Length < 11)
                {
                    var a = "alert(\'Si usted va a colocar un NCF es necesario que lo coloque completo, los NCFs tienen una logitud de 11 caracteres\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                }
                else if (DropDownList2.Enabled == true && DropDownList3.Text == DropDownList2.Text)
                {
                    var a = "alert(\'Usted por motivo de seguridad no se puede seleccionar como acompañante. favor cambiar\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);                    
                }
                else
                {
                    numero = GridView1.Rows.Count + 1;
                    Totalgastos.Text = string.Format("{0:#,##0.00}", float.Parse(Totalgastos.Text) + float.Parse(MontoTotal.Text));

                    if (Session["dt"] == null)
                    {
                        DataTable dt = creaciontabletemp();
                        DataRow datarow;
                        datarow = dt.NewRow();
                        datarow["No"] = numero;
                        datarow["FECHA"] = FechaLiquidacion.Text;
                        datarow["FACT"] = FacturaLiquidacion.Text;
                        datarow["CONSUMIO"] = Empresadondecomsumio.Text;
                        datarow["CONCEPTO"] = DropDownList1.Text;
                        datarow["NCF"] = NCFLiquida.Text;
                        datarow["MTO_RECLAMA"] = string.Format("{0:#,##0.00}", float.Parse(MontoTotal.Text));
                        if (DropDownList2.Enabled == true) { datarow["ACOMPANANTE"] = DropDownList2.Text; }
                        else { datarow["ACOMPANANTE"] = null; }
                        datarow["MONEDA"] = DropDownList4.Text;
                        datarow["MTO_ORIGI"] = MontoLiquida.Text;
                        if (DropDownList4.Text == "USD") { datarow["TASA"] = Label10.Text; } else if (DropDownList4.Text == "EUR") { datarow["TASA"] = Label12.Text; } else if (DropDownList4.Text == "DOP") { datarow["TASA"] = 1; }
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
                        datarow["No"] = numero;
                        datarow["FECHA"] = FechaLiquidacion.Text;
                        datarow["FACT"] = FacturaLiquidacion.Text;
                        datarow["CONSUMIO"] = Empresadondecomsumio.Text;
                        datarow["CONCEPTO"] = DropDownList1.Text;
                        datarow["NCF"] = NCFLiquida.Text;
                        datarow["MTO_RECLAMA"] = string.Format("{0:#,##0.00}", float.Parse(MontoTotal.Text));
                        if (DropDownList2.Enabled == true) { datarow["ACOMPANANTE"] = DropDownList2.Text; }
                        else { datarow["ACOMPANANTE"] = null; }
                        datarow["MONEDA"] = DropDownList4.Text;
                        datarow["MTO_ORIGI"] = MontoLiquida.Text;
                        if (DropDownList4.Text == "USD") { datarow["TASA"] = Label10.Text; } else if (DropDownList4.Text == "EUR") { datarow["TASA"] = Label12.Text; } else if (DropDownList4.Text == "DOP") { datarow["TASA"] = 1; }
                        dt.Rows.Add(datarow);
                        Session["dt"] = dt;
                        GridView1.DataSource = new DataView(dt);
                        GridView1.DataBind();
                    }
                    BORRARCAMPOS();
                    DropDownList3.Enabled = false;

                    RequiredFieldValidator1.Enabled = false;
                    RequiredFieldValidator2.Enabled = false;
                    RequiredFieldValidator3.Enabled = false;
                    RequiredFieldValidator4.Enabled = false;
                    RequiredFieldValidator5.Enabled = false;
                }
            }
            else
            {
                if (DropDownList2.Enabled == true && DropDownList3.Text == DropDownList2.Text)
                {
                    var a = "alert(\'Usted por motivo de seguridad no se puede seleccionar como acompañante. favor cambiar\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                }
                else
                {
                    numero = GridView1.Rows.Count + 1;
                    Totalgastos.Text = string.Format("{0:#,##0.00}", float.Parse(Totalgastos.Text) + float.Parse(MontoTotal.Text));

                    if (Session["dt"] == null)
                    {
                        DataTable dt = creaciontabletemp();
                        DataRow datarow;
                        datarow = dt.NewRow();
                        datarow["No"] = numero;
                        datarow["FECHA"] = FechaLiquidacion.Text;
                        datarow["FACT"] = FacturaLiquidacion.Text;
                        datarow["CONSUMIO"] = Empresadondecomsumio.Text;
                        datarow["CONCEPTO"] = DropDownList1.Text;
                        datarow["NCF"] = NCFLiquida.Text;
                        datarow["MTO_RECLAMA"] = string.Format("{0:#,##0.00}", float.Parse(MontoTotal.Text));
                        if (DropDownList2.Enabled == true) { datarow["ACOMPANANTE"] = DropDownList2.Text; }
                        else { datarow["ACOMPANANTE"] = null; }
                        datarow["MONEDA"] = DropDownList4.Text;
                        datarow["MTO_ORIGI"] = MontoLiquida.Text;
                        if (DropDownList4.Text == "USD") { datarow["TASA"] = Label10.Text; } else if (DropDownList4.Text == "EUR") { datarow["TASA"] = Label12.Text; } else if (DropDownList4.Text == "DOP") { datarow["TASA"] = 1; }
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
                        datarow["No"] = numero;
                        datarow["FECHA"] = FechaLiquidacion.Text;
                        datarow["FACT"] = FacturaLiquidacion.Text;
                        datarow["CONSUMIO"] = Empresadondecomsumio.Text;
                        datarow["CONCEPTO"] = DropDownList1.Text;
                        datarow["NCF"] = NCFLiquida.Text;
                        datarow["MTO_RECLAMA"] = string.Format("{0:#,##0.00}", float.Parse(MontoTotal.Text));
                        if (DropDownList2.Enabled == true) { datarow["ACOMPANANTE"] = DropDownList2.Text; }
                        else { datarow["ACOMPANANTE"] = null; }
                        datarow["MONEDA"] = DropDownList4.Text;
                        datarow["MTO_ORIGI"] = MontoLiquida.Text;
                        if (DropDownList4.Text == "USD") { datarow["TASA"] = Label10.Text; } else if (DropDownList4.Text == "EUR") { datarow["TASA"] = Label12.Text; } else if (DropDownList4.Text == "DOP") { datarow["TASA"] = 1; }
                        dt.Rows.Add(datarow);
                        Session["dt"] = dt;
                        GridView1.DataSource = new DataView(dt);
                        GridView1.DataBind();
                    }
                    BORRARCAMPOS();
                    DropDownList3.Enabled = false;

                    RequiredFieldValidator1.Enabled = false;
                    RequiredFieldValidator2.Enabled = false;
                    RequiredFieldValidator3.Enabled = false;
                    RequiredFieldValidator4.Enabled = false;
                    RequiredFieldValidator5.Enabled = false;
                }                
            }
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow ro = GridView1.Rows[e.RowIndex];
        DataTable dttemp = (Session["dt"]) as DataTable;
        for (int i = 0; i < dttemp.Rows.Count; i++)
        {
            if (dttemp.Rows[i]["No"].ToString() == ro.Cells[1].Text)
            {
                Totalgastos.Text = string.Format("{0:#,##0.00}", float.Parse(Totalgastos.Text) - float.Parse(ro.Cells[7].Text));
                dttemp.Rows.RemoveAt(i);
            }
        }
        Session["dt"] = dttemp;
        GridView1.DataSource = new DataView(dttemp);
        GridView1.DataBind();
    }
    protected void Enviarliquidacion_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(2500);
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue.Substring(confirmValue.Length - 2) == "Si")
        {
            if (GridView1.Rows.Count <= 0 || float.Parse(Totalgastos.Text) == 0 || Totalgastos.Text == "0.00")
            {
                var a = "alert(\'Para poder enviar una reposición de gastos es necesario que coloque todas las facturas correspondientes\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);

                RequiredFieldValidator1.Enabled = true;
                RequiredFieldValidator2.Enabled = true;
                RequiredFieldValidator3.Enabled = true;
                RequiredFieldValidator4.Enabled = true;
                RequiredFieldValidator5.Enabled = true;
            }
            else
            {                            
                con.cn.Open();
                string guardacabecera = "Desarrollo.RepGas.InsertarReposicionesGastos_Cab";
                com = new SqlCommand(guardacabecera, con.cn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@EMPLEADOSOLICITA", SqlDbType.VarChar, 50).Value = DropDownList3.Text;
                com.Parameters.Add("@FECHASOLICITUD", SqlDbType.DateTime).Value = DateTime.Parse(Label8.Text);
                com.UpdatedRowSource = UpdateRowSource.None;
                com.ExecuteNonQuery();
                con.cn.Close();

                GenerarCodigo();

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    float montopagarempresa = 0;

                    if (GridView1.Rows[i].Cells[5].Text.Trim() == "Dieta Acompanamiento")
                    {
                        if (float.Parse(GridView1.Rows[i].Cells[7].Text.Trim()) <= 600)
                        {
                            montopagarempresa = float.Parse(GridView1.Rows[i].Cells[7].Text.Trim());
                        }
                        else
                        {
                            montopagarempresa = 600;
                        }
                    }
                    else if (GridView1.Rows[i].Cells[5].Text.Trim() == "Dieta Acomp. X3")
                    {
                        if (float.Parse(GridView1.Rows[i].Cells[7].Text.Trim()) <= 900)
                        {
                            montopagarempresa = float.Parse(GridView1.Rows[i].Cells[7].Text.Trim());
                        }
                        else
                        {
                            montopagarempresa = 900;
                        }
                    }
                    else
                    {
                        if (float.Parse(GridView1.Rows[i].Cells[7].Text.Trim()) <= 300) { montopagarempresa = float.Parse(GridView1.Rows[i].Cells[7].Text.Trim()); }
                        else { montopagarempresa = 300; }
                    }
                    con.cn.Open();
                    string guardalineas = "Desarrollo.RepGas.InsertarReposicionGastos_Det";
                    com = new SqlCommand(guardalineas, con.cn);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@FECHAFACTURA", SqlDbType.DateTime).Value = DateTime.Parse(GridView1.Rows[i].Cells[2].Text);
                    com.Parameters.Add("@NUMEROFACTURA", SqlDbType.VarChar, 20).Value = HttpUtility.HtmlDecode(GridView1.Rows[i].Cells[3].Text.Trim());
                    com.Parameters.Add("@EMPRESACONSUMO", SqlDbType.VarChar, 50).Value = HttpUtility.HtmlDecode(GridView1.Rows[i].Cells[4].Text.Trim());
                    com.Parameters.Add("@MONEDA", SqlDbType.VarChar, 3).Value = GridView1.Rows[i].Cells[9].Text.Trim();
                    com.Parameters.Add("@CONCEPTO", SqlDbType.VarChar, 15).Value = GridView1.Rows[i].Cells[5].Text.Trim();
                    com.Parameters.Add("@NCF", SqlDbType.VarChar, 19).Value = HttpUtility.HtmlDecode(GridView1.Rows[i].Cells[6].Text.Trim());
                    com.Parameters.Add("@MONTOORIGINAL", SqlDbType.Money).Value = float.Parse(GridView1.Rows[i].Cells[10].Text.Trim());
                    com.Parameters.Add("@MONTOCAMBIADO", SqlDbType.Money).Value = float.Parse(GridView1.Rows[i].Cells[7].Text.Trim());
                    com.Parameters.Add("@ACOMPANANTE", SqlDbType.VarChar, 50).Value = HttpUtility.HtmlDecode(GridView1.Rows[i].Cells[8].Text.Trim());
                    com.Parameters.Add("@MONTOPAGAEMPRESA", SqlDbType.Money).Value = montopagarempresa;
                    com.Parameters.Add("@TASA", SqlDbType.Money).Value = float.Parse(GridView1.Rows[i].Cells[11].Text.Trim());
                    com.UpdatedRowSource = UpdateRowSource.None;
                    com.ExecuteNonQuery();
                    con.cn.Close();
                }
                GridView1.DataSource = null;
                GridView1.DataBind();
                Totalgastos.Text = "0.00";

                string numerodeconfirmacion = "SELECT MAX(COD_REPOSICION) AS REP FROM Desarrollo.RepGas.REPOSICION_CAB where EMPLEADO_SOLICITA='" + DropDownList3.Text + "'";
                data = new SqlDataAdapter(numerodeconfirmacion, con.cn);
                table = new DataTable();
                data.Fill(table);
                Session["Condirmacion"] = table.Rows[0]["REP"].ToString();

                #region Enviar correo
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("mail.wurth.com.do");
                SmtpServer.Port = 25;
                SmtpServer.Credentials = new System.Net.NetworkCredential("servicioswurth@wurth.com.do", "Wservice01*");
                mail.From = new MailAddress("servicioswurth@wurth.com.do");

                mail.Subject = "Reposición de Gastos para " + DropDownList3.Text;
                mail.Body = "Después de un Cordial Saludo \n\n Les comunico que acabo de registrar la (s) factura (s) correspondiente a los gastos en que he incurrido y que lleva como número de reposición: " + Session["Condirmacion"].ToString() + " \n\n Se despide de usted " + DropDownList3.Text + " \n\nFavor no responder este mensaje ya que fue generado por sistema automático";
                mail.To.Add("aramirez@wurth.com.do");
                //mail.CC.Add("rferrer@wurth.com.do");
                SmtpServer.Send(mail);
                #endregion                
         
                Response.Redirect("~/Confirmacion.aspx");          
            }
        }
    }
    protected void FechaLiquidacion_TextChanged(object sender, EventArgs e)
    {
        //TimeSpan ts = DateTime.Today - DateTime.Parse(FechaLiquidacion.Text);       
        //if (ts.Days >= 25)
        //{            
        //    var a = "alert(\'La fecha que está insertando o ha seleccionado excede el mes de vigencia de los impuestos.\');";
        //    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
        //    FechaLiquidacion.Text = string.Empty;
        //}
        //else Format="dd/MM/yyyy"

        if (DateTime.Parse(FechaLiquidacion.Text).Month != DateTime.Today.Month)
        {
            if (DateTime.Today.Day > 5)
            {
                var a = "alert(\'Esta factura no puede ser registrada debido a que ha excedido la fecha para el registro.\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                FechaLiquidacion.Text = string.Empty;
            }
        }
        else if (DateTime.Compare(DateTime.Today, DateTime.Parse(FechaLiquidacion.Text)) == -1)
        {
            var a = "alert(\'No es valido colocar fecha futurita de consumo.\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            FechaLiquidacion.Text = string.Empty;
        }
    }
    //protected void NCFLiquida_TextChanged(object sender, EventArgs e)
    //{
    //    //string buscarNCF = @"SELECT * FROM Desarrollo.RepGas.REPOSICION_DET A WHERE A.NCF='" + NCFLiquida.Text + "'";
    //    //data = new SqlDataAdapter(buscarNCF, con.cn);
    //    //table = new DataTable();
    //   // data.Fill(table);

    //    //if (table.Rows.Count > 0)
    //    //{
    //      //  var a = "alert(\'El conmprobante que fiscal que acabas de escribir ya fue registrado, favor verificar e intentar nuevamente\');";
    //       // ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
    //       // NCFLiquida.Text = string.Empty;
    //    //}        
    //}
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