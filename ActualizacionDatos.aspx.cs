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


public partial class ActualizacionDatos : System.Web.UI.Page
{
    Conexion con = new Conexion();
    SqlCommand com = new SqlCommand();
    SqlDataAdapter data = new SqlDataAdapter();
    DataTable table = new DataTable();
    string nombreartchivo;
    
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
            cargardivisiones();
        }
    }
    protected void BuscarCliente_Click(object sender, EventArgs e)
    {
        try
        {
            if (nocliente.Text == string.Empty)
            {
                var a = "alert(\'Para poder realizar la busqueda es necesario que llene el campo\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);

                BuscarCliente.Focus();
            }
            else
            {
                string BuscarDatosClientes = @"Desarrollo.CONVEN.INFORMACIONCLIENTES";
                com = new SqlCommand(BuscarDatosClientes, con.cn);
                com.Parameters.Add("@CODCLIENTE", SqlDbType.VarChar, 10).Value = nocliente.Text;
                com.Parameters.Add("@VENDEDOR", SqlDbType.VarChar, 10).Value = Session["idvendedor"].ToString();
                com.CommandType = CommandType.StoredProcedure;
                data = new SqlDataAdapter(com);
                table = new DataTable();
                data.Fill(table);

                if (table.Rows.Count > 0)
                {
                    CodigoCliente.Text = table.Rows[0]["COD_CLIENTE"].ToString();
                    Nombrecliente.Text = table.Rows[0]["NOMBRE_CLIENTE"].ToString();
                    //.Text = table.Rows[0][""].ToString();
                    /*Ciudad.Text = table.Rows[0][""].ToString();*/
                    Telefono.Text = table.Rows[0]["TELEFONO"].ToString();
                    Fax.Text = table.Rows[0]["FAX"].ToString();
                    Email.Text = table.Rows[0]["CORREO"].ToString();
                    RNC.Text = table.Rows[0]["RNC"].ToString();
                    Gerentecomercial.Text = table.Rows[0]["NOMBRE_CONTACTO"].ToString();
                    Financiero.Text = table.Rows[0]["NOMBRE_FINANCIERO"].ToString();
                    DropDownList1.Text = table.Rows[0]["CONDICIONES_PAGO"].ToString();
                    Dipagos.Text = table.Rows[0]["DIAPAGO"].ToString();
                    Codvendedor.Text = table.Rows[0]["COD_VENDEDOR"].ToString();
                    direccion.Text = table.Rows[0]["DIRECCION"].ToString();
                    Comentario.Text = table.Rows[0]["INFORMACION_ADICIONAL"].ToString();
                }
                else
                {
                    var a = "alert(\'UPSS! Lo sentimos, pero el código de cliente que busca no lo hemos encontrado o el mismo no pertenece a su cartera\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);

                    nocliente.Text = string.Empty;
                }
            }
        }
        catch (Exception)
        {            
            throw;
        }
    }
    protected void Cancelar_Click(object sender, EventArgs e)
    {
        CodigoCliente.Text = string.Empty;
        Nombrecliente.Text = string.Empty;
        Telefono.Text = string.Empty;
        Fax.Text = string.Empty;
        Email.Text = string.Empty;
        RNC.Text = string.Empty;
        Gerentecomercial.Text = string.Empty;
        Financiero.Text = string.Empty;
        DropDownList1.Text = string.Empty;
        Codvendedor.Text = string.Empty;
        direccion.Text = string.Empty;
        Comentario.Text = string.Empty;
        nocliente.Text = string.Empty;
        RazonSocial.Text = string.Empty;
        NOEMPLEADO.Text = string.Empty;
        SECTOR.Text = string.Empty;
        Session["Archivo"] = null;
        //DropDownList2.Text = string.Empty;
        //DropDownList3.Text = string.Empty;
    }
    protected void enviar_Click(object sender, EventArgs e)
    {          
       try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue.Substring(confirmValue.Length - 2) == "Si")
            {
                if (CodigoCliente.Text == string.Empty)
                {
                    var a = "alert(\'El código del cliente es necesario que esté lleno para poder envíar a actualizar.\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                }
                else if (Nombrecliente.Text == string.Empty)
                {
                    var a = "alert(\'El nombre del cliente es necesario para que se puede actualizar en el sistema principal\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                    Nombrecliente.Focus();
                }
                else if (RazonSocial.Text == string.Empty)
                {
                    var a = "alert(\'La razón social es necesario para que pueda envíar esta actualización\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                    RazonSocial.Focus();
                }
                else if (Codvendedor.Text == string.Empty)
                {
                    var a = "alert(\'El código de vendedor es necesario para que pueda envíar esta actualización\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                }
                else if (Telefono.Text == string.Empty)
                {
                    var a = "alert(\'El teléfono es necesario para que pueda envíar esta actualización\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                    Telefono.Focus();
                }
                else if (Email.Text == string.Empty)
                {
                    var a = "alert(\'El correo electrónico es necesario para que pueda envíar esta actualización\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                    Email.Focus();
                }
                else if (RNC.Text == string.Empty)
                {
                    var a = "alert(\'El RNC es necesario para que pueda envíar esta actualización\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                    RNC.Focus();
                }
                else if (Gerentecomercial.Text == string.Empty)
                {
                    var a = "alert(\'El nombre del gerente comercial o dueño del establecimiento es necesario para que pueda envíar esta actualización\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                    Gerentecomercial.Focus();
                }
                else if (direccion.Text == string.Empty)
                {
                    var a = "alert(\'La dirección es necesario para que pueda envíar esta actualización\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                    direccion.Focus();
                }
                else if (Dipagos.Text == string.Empty)
                {
                    var a = "alert(\'El dia de pago es necesario para que pueda envíar esta actualización\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                    Dipagos.Focus();
                }
                else if (validarExistencia(CodigoCliente.Text) == true)
                {
                    var a = "alert(\'Actualmente existe una actualización pendiente de realizar para el cliente que esta intentando actualizar, favor contacte a su gestora para que la misma realize dicha acción\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                }
                else if (Email_Bien_Escrito(Email.Text) == false)
                {
                    var a = "alert(\'El correo que intenta enviar no está bien escrito, favor revise e intente nuevamente\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                    Dipagos.Focus();
                }
                else
                {
                    string enviardatosactualizar = @"Desarrollo.CONVEN.EnviarDatosActualizar";
                    con.cn.Open();
                    com = new SqlCommand(enviardatosactualizar, con.cn);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@NOCLIENTE", SqlDbType.VarChar, 10).Value = CodigoCliente.Text;
                    com.Parameters.Add("@NOVENDEDOR", SqlDbType.VarChar, 10).Value = Codvendedor.Text;
                    com.Parameters.Add("@NOMBREEMPRESA", SqlDbType.VarChar, 50).Value = Nombrecliente.Text;
                    com.Parameters.Add("@RAZONSOCIAL", SqlDbType.VarChar, 50).Value = RazonSocial.Text;
                    com.Parameters.Add("@DIRECCION", SqlDbType.VarChar, 150).Value = direccion.Text;
                    com.Parameters.Add("@TELEFONO", SqlDbType.VarChar, 13).Value = Telefono.Text;
                    com.Parameters.Add("@FAX", SqlDbType.VarChar, 13).Value = Fax.Text;
                    com.Parameters.Add("@CORREO", SqlDbType.VarChar, 75).Value = Email.Text;
                    com.Parameters.Add("@GERENTE_COMERCIAL", SqlDbType.VarChar, 25).Value = Gerentecomercial.Text;
                    com.Parameters.Add("@RNC", SqlDbType.VarChar, 13).Value = RNC.Text;
                    com.Parameters.Add("@NOMBRE_FINANCIERO", SqlDbType.VarChar, 25).Value = Financiero.Text;
                    com.Parameters.Add("@FORMA_PAGO", SqlDbType.VarChar, 10).Value = DropDownList1.SelectedValue;
                    com.Parameters.Add("@DIAS_PAGO", SqlDbType.Int).Value = Dipagos.Text;
                    com.Parameters.Add("@COMENTARIO", SqlDbType.VarChar, 100).Value = Comentario.Text;
                    com.Parameters.Add("@QUIENENVIA", SqlDbType.VarChar, 30).Value = Session["NombreCompleto"].ToString();
                    com.Parameters.Add("@VENDEDORENVIA", SqlDbType.VarChar, 10).Value = Session["idvendedor"].ToString();
                    //com.Parameters.Add("@DIVISION", SqlDbType.Int).Value = int.Parse(DropDownList2.Text.Substring(1, 2).Trim());
                    //com.Parameters.Add("@SUBDIVISION", SqlDbType.Int).Value = int.Parse(DropDownList3.Text.Substring(1, 4).Trim());
                    com.Parameters.Add("@SECTOR", SqlDbType.VarChar, 10).Value = SECTOR.Text;
                    if (NOEMPLEADO.Text != string.Empty) {com.Parameters.Add("@NOEMPLEADO", SqlDbType.Int).Value = int.Parse(NOEMPLEADO.Text);}
                    else {com.Parameters.Add("@NOEMPLEADO", SqlDbType.Int).Value = 0; }
                    com.UpdatedRowSource = UpdateRowSource.None;
                    com.ExecuteNonQuery();
                    con.cn.Close();

                    #region Enviar correo
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("mail.wurth.com.do");
                    SmtpServer.Port = 25;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("servicioswurth@wurth.com.do", "Wservice01*");
                    mail.From = new MailAddress("servicioswurth@wurth.com.do");
                    mail.Attachments.Add(new Attachment(Server.MapPath("~/Archivos Subidos/" + Session["Archivo"].ToString())));
                    
                    mail.Subject = "Recepción de documentos de actualización de datos de Clientes";
                    mail.Body = "Después de un Cordial Saludo \n\n Les comunico que acabo de enviar una actualización de datos del cliente código " + CodigoCliente.Text + " " + Nombrecliente.Text + "\n\n Favor entrar al sistema de llamadas para realizar la actualización de este cliente\n\n Se despide de ustedes la o el vendedor o vendedora " + Session["idvendedor"].ToString() + " \n\nFavor no responder este mensaje ya que fue generado por sistema automático";
                    mail.To.Add("cobros@wurth.com.do");
                    SmtpServer.Send(mail);                    
                    #endregion

                    var a = "alert(\'Información enviada correctamente\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);

                    Cancelar_Click(sender, e);
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            string Clientespendiete = "DESARROLLO.ConVen.ClientesPendientesActualizar";
            com = new SqlCommand(Clientespendiete, con.cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@VENDEDOR", SqlDbType.VarChar, 10).Value = Session["idvendedor"].ToString();
            com.Parameters.Add("@INF", SqlDbType.Int).Value = 0;

            data = new SqlDataAdapter(com);
            table = new DataTable();
            data.Fill(table);

            if (table.Rows.Count <= 0)
            {
                var a = "alert(\'Usted no cuenta con clientes pendientes de ser actualizados en esta aplicación\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            }
            else
            {
                GridView1.DataSource = new DataView(table);
                GridView1.DataBind();
                GridView1.Visible = true;
                LinkButton3.Visible = true;
            }
        }
        catch (Exception)
        {            
            throw;
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        try
        {
            string Clientespendiete = "DESARROLLO.ConVen.ClientesActualizados";
            com = new SqlCommand(Clientespendiete, con.cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@VENDEDOR", SqlDbType.VarChar, 10).Value = Session["idvendedor"].ToString();
            com.Parameters.Add("@INF", SqlDbType.Int).Value = 0;

            data = new SqlDataAdapter(com);
            table = new DataTable();
            data.Fill(table);

            if (table.Rows.Count <= 0)
            {
                var a = "alert(\'Usted no cuenta con clientes actualizados desde esta aplicación\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            }
            else
            {
                GridView1.DataSource = new DataView(table);
                GridView1.DataBind();
                GridView1.Visible = true;
                LinkButton3.Visible = true;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        GridView1.Visible = false;
        LinkButton3.Visible = false;
    }
    public void cargardivisiones()
    {
        string divisiones = "SELECT A.DIMENSION AS DIVISION FROM [10.0.0.54].AX50DOM_LIVE.DBO.CUSTTABLE A";
        data = new SqlDataAdapter(divisiones, con.cn);
        table = new DataTable();
        data.Fill(table);

        if (table.Rows.Count > 0)
        {
            DropDownList2.Items.Clear();            
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DropDownList2.Items.Add(table.Rows[i]["DIVISION"].ToString());
            }
        }
        //PENDIENTE DE CAMBIO EN CASO DE QUE EXISTA EN EL SISTEMA NUEVO
        string divisiones1 = "SELECT A.NUM + ' ' + A.DESCRIPTION AS DIVISION FROM Dominica..DIMENSIONS A WHERE A.DIMENSIONCODE=1";
        data = new SqlDataAdapter(divisiones1, con.cn);
        table = new DataTable();
        data.Fill(table);

        if (table.Rows.Count > 0)
        {            
            DropDownList3.Items.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DropDownList3.Items.Add(table.Rows[i]["DIVISION"].ToString());
            }
        }
    }
    public Boolean validarExistencia(string ClienteCod)
    {
        string SPValidarExistencia = @"Desarrollo.ConVen.ValidarExistencia";
        com = new SqlCommand(SPValidarExistencia, con.cn);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@CLIENTE", SqlDbType.VarChar, 10).Value = ClienteCod;
        data = new SqlDataAdapter(com);
        table = new DataTable();
        data.Fill(table);
        if (table.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }        
    }
    public Boolean Email_Bien_Escrito(string email)
    {
        String expresion;
        expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        if (Regex.IsMatch(email, expresion))
        {
            if (Regex.Replace(email, expresion, String.Empty).Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
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
    protected void AsyncFileUpload1_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {      
        nombreartchivo = nocliente.Text +"_"+ AsyncFileUpload1.FileName;
        Session["Archivo"] = nombreartchivo;
        AsyncFileUpload1.SaveAs(Server.MapPath("~/Archivos Subidos/") + nombreartchivo);
    }
}