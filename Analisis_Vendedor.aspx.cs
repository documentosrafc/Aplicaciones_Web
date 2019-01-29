using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

public partial class Analisis_Vendedor : System.Web.UI.Page
{
    Conexion con = new Conexion();
    SqlCommand com = new SqlCommand();
    SqlDataAdapter data = new SqlDataAdapter();
    DataTable table = new DataTable();

    string pregunta1, pregunta2, pregunta3, pregunta4, pregunta5, pregunta6, pregunta7, pregunta8, pregunta9, pregunta12, pregunta13, pregunta14 = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["NombreCompleto"] == null)
        {
            Response.Redirect("~/Default.aspx");
        }
        if (!IsPostBack)
        {
            this.Form.Attributes.Add("autocomplete", "off");
            CargarVendedores();
            habilitarmenu();
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
                DropDownList1.Items.Add(table.Rows[i]["COD_VENDEDOR"].ToString());
            }
            else
            {
                DropDownList1.Items.Add(table.Rows[i]["COD_VENDEDOR"].ToString());
            }
        }
    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton2.Checked = false;
        RadioButton3.Checked = false;
    }
    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton1.Checked = false;
        RadioButton3.Checked = false;
    }
    protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton2.Checked = false;
        RadioButton1.Checked = false;
    }
    protected void RadioButton4_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton5.Checked = false;
        RadioButton6.Checked = false;
    }
    protected void RadioButton5_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton4.Checked = false;
        RadioButton6.Checked = false;
    }
    protected void RadioButton6_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton5.Checked = false;
        RadioButton4.Checked = false;
    }
    protected void RadioButton7_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton8.Checked = false;
        RadioButton9.Checked = false;
    }
    protected void RadioButton8_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton7.Checked = false;
        RadioButton9.Checked = false;
    }
    protected void RadioButton9_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton7.Checked = false;
        RadioButton8.Checked = false;
    }
    protected void RadioButton10_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton11.Checked = false;
        RadioButton12.Checked = false;
    }
    protected void RadioButton11_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton10.Checked = false;
        RadioButton12.Checked = false;
    }
    protected void RadioButton12_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton10.Checked = false;
        RadioButton11.Checked = false;
    }
    protected void RadioButton13_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton14.Checked = false;
        RadioButton15.Checked = false;
    }
    protected void RadioButton14_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton13.Checked = false;
        RadioButton15.Checked = false;
    }
    protected void RadioButton15_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton13.Checked = false;
        RadioButton14.Checked = false;
    }
    protected void RadioButton16_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton17.Checked = false;
        RadioButton18.Checked = false;
    }
    protected void RadioButton17_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton16.Checked = false;
        RadioButton18.Checked = false;
    }
    protected void RadioButton18_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton16.Checked = false;
        RadioButton17.Checked = false;
    }
    protected void RadioButton19_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton20.Checked = false;
        RadioButton21.Checked = false;
    }
    protected void RadioButton20_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton19.Checked = false;
        RadioButton21.Checked = false;
    }
    protected void RadioButton21_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton19.Checked = false;
        RadioButton20.Checked = false;
    }
    protected void RadioButton22_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton23.Checked = false;
        RadioButton24.Checked = false;
    }
    protected void RadioButton23_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton22.Checked = false;
        RadioButton24.Checked = false;
    }
    protected void RadioButton24_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton22.Checked = false;
        RadioButton23.Checked = false;
    }
    protected void RadioButton25_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton26.Checked = false;
        RadioButton27.Checked = false;
    }
    protected void RadioButton26_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton25.Checked = false;
        RadioButton27.Checked = false;
    }
    protected void RadioButton27_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton25.Checked = false;
        RadioButton26.Checked = false;
    }
    //protected void RadioButton28_CheckedChanged(object sender, EventArgs e)
    //{
    //    RadioButton29.Checked = false;
    //    RadioButton30.Checked = false;
    //}
    //protected void RadioButton29_CheckedChanged(object sender, EventArgs e)
    //{
    //    RadioButton28.Checked = false;
    //    RadioButton30.Checked = false;
    //}
    //protected void RadioButton30_CheckedChanged(object sender, EventArgs e)
    //{
    //    RadioButton28.Checked = false;
    //    RadioButton29.Checked = false;
    //}
    //protected void RadioButton31_CheckedChanged(object sender, EventArgs e)
    //{
    //    RadioButton32.Checked = false;
    //    RadioButton33.Checked = false;
    //}
    //protected void RadioButton32_CheckedChanged(object sender, EventArgs e)
    //{
    //    RadioButton31.Checked = false;
    //    RadioButton33.Checked = false;
    //}
    //protected void RadioButton33_CheckedChanged(object sender, EventArgs e)
    //{
    //    RadioButton31.Checked = false;
    //    RadioButton32.Checked = false;
    //}
    protected void RadioButton34_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton35.Checked = false;
        RadioButton36.Checked = false;
    }
    protected void RadioButton35_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton34.Checked = false;
        RadioButton36.Checked = false;
    }
    protected void RadioButton36_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton34.Checked = false;
        RadioButton35.Checked = false;
    }
    protected void RadioButton37_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton38.Checked = false;
        RadioButton39.Checked = false;
    }
    protected void RadioButton38_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton37.Checked = false;
        RadioButton39.Checked = false;
    }
    protected void RadioButton39_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton37.Checked = false;
        RadioButton38.Checked = false;
    }
    protected void RadioButton40_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton41.Checked = false;
        RadioButton42.Checked = false;
    }
    protected void RadioButton41_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton40.Checked = false;
        RadioButton42.Checked = false;
    }
    protected void RadioButton42_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton40.Checked = false;
        RadioButton41.Checked = false;
    }
    protected void Cancelar_Click(object sender, EventArgs e)
    {
        DropDownList1.Text = "Vendedores";
        RadioButton1.Checked = false;
        RadioButton2.Checked = false;
        RadioButton3.Checked = false;
        RadioButton4.Checked = false;
        RadioButton5.Checked = false;
        RadioButton6.Checked = false;
        RadioButton7.Checked = false;
        RadioButton8.Checked = false;
        RadioButton9.Checked = false;
        RadioButton10.Checked = false;
        RadioButton11.Checked = false;
        RadioButton12.Checked = false;
        RadioButton13.Checked = false;
        RadioButton14.Checked = false;
        RadioButton15.Checked = false;
        RadioButton16.Checked = false;
        RadioButton17.Checked = false;
        RadioButton18.Checked = false;
        RadioButton19.Checked = false;
        RadioButton20.Checked = false;
        RadioButton21.Checked = false;
        RadioButton22.Checked = false;
        RadioButton23.Checked = false;
        RadioButton24.Checked = false;
        RadioButton25.Checked = false;
        RadioButton26.Checked = false;
        RadioButton27.Checked = false;
        //RadioButton28.Checked = false;
        //RadioButton29.Checked = false;
        //RadioButton30.Checked = false;
        //RadioButton31.Checked = false;
        //RadioButton32.Checked = false;
        //RadioButton33.Checked = false;
        RadioButton34.Checked = false;
        RadioButton35.Checked = false;
        RadioButton36.Checked = false;
        RadioButton37.Checked = false;
        RadioButton38.Checked = false;
        RadioButton39.Checked = false;
        RadioButton40.Checked = false;
        RadioButton41.Checked = false;
        RadioButton42.Checked = false;
        Comentarios.Text = string.Empty;
    }
    protected void Enviar_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue.Substring(confirmValue.Length - 2) == "Si")
        {            
            if (DropDownList1.Text != "Vendedores")
            {
                if (RadioButton1.Checked == true || RadioButton2.Checked == true || RadioButton3.Checked == true)
                {
                    if (RadioButton1.Checked == true) pregunta1 = "Fuerte"; else if (RadioButton2.Checked == true) pregunta1 = "A Manejar"; else if (RadioButton3.Checked == true) pregunta1 = "Riesgo";
                    //pregunta 1
                    if (RadioButton4.Checked == true || RadioButton5.Checked == true || RadioButton6.Checked == true)
                    {
                        if (RadioButton4.Checked == true) pregunta2 = "Fuerte"; else if (RadioButton5.Checked == true) pregunta2 = "A Manejar"; else if (RadioButton6.Checked == true) pregunta2 = "Riesgo";
                        //pregunta 2
                        if (RadioButton7.Checked == true || RadioButton8.Checked == true || RadioButton9.Checked == true)
                        {
                            if (RadioButton7.Checked == true) pregunta3 = "Fuerte"; else if (RadioButton8.Checked == true) pregunta3 = "A Manejar"; else if (RadioButton9.Checked == true) pregunta3 = "Riesgo";
                            //pregunta 3
                            if (RadioButton10.Checked == true || RadioButton11.Checked == true || RadioButton12.Checked == true)
                            {
                                if (RadioButton10.Checked == true) pregunta4 = "Fuerte"; else if (RadioButton11.Checked == true) pregunta4 = "A Manejar"; else if (RadioButton12.Checked == true) pregunta4 = "Riesgo";
                                //pregunta 4
                                if (RadioButton13.Checked == true || RadioButton14.Checked == true || RadioButton15.Checked == true)
                                {
                                    if (RadioButton13.Checked == true) pregunta5 = "Fuerte"; else if (RadioButton14.Checked == true) pregunta5 = "A Manejar"; else if (RadioButton15.Checked == true) pregunta5 = "Riesgo";
                                    //pregunta 5
                                    if (RadioButton16.Checked == true || RadioButton17.Checked == true || RadioButton18.Checked == true)
                                    {
                                        if (RadioButton16.Checked == true) pregunta6 = "Fuerte"; else if (RadioButton17.Checked == true) pregunta6 = "A Manejar"; else if (RadioButton18.Checked == true) pregunta6 = "Riesgo";
                                        //pregunta 6
                                        if (RadioButton19.Checked == true || RadioButton20.Checked == true || RadioButton21.Checked == true)
                                        {
                                            if (RadioButton19.Checked == true) pregunta7 = "Fuerte"; else if (RadioButton20.Checked == true) pregunta7 = "A Manejar"; else if (RadioButton21.Checked == true) pregunta7 = "Riesgo";
                                            //pregunta 7
                                            if (RadioButton22.Checked == true || RadioButton23.Checked == true || RadioButton24.Checked == true)
                                            {
                                                if (RadioButton22.Checked == true) pregunta8 = "Fuerte"; else if (RadioButton23.Checked == true) pregunta8 = "A Manejar"; else if (RadioButton24.Checked == true) pregunta8 = "Riesgo";
                                                //pregunta 8
                                                if (RadioButton25.Checked == true || RadioButton26.Checked == true || RadioButton27.Checked == true)
                                                {
                                                    if (RadioButton25.Checked == true) pregunta9 = "Fuerte"; else if (RadioButton26.Checked == true) pregunta9 = "A Manejar"; else if (RadioButton27.Checked == true) pregunta9 = "Riesgo";
                                                    //pregunta 9
                                                    //if (RadioButton28.Checked == true || RadioButton29.Checked == true || RadioButton30.Checked == true)
                                                    //{
                                                    //    if (RadioButton28.Checked == true) pregunta10 = "Fuerte"; else if (RadioButton29.Checked == true) pregunta10 = "A Manejar"; else if (RadioButton30.Checked == true) pregunta10 = "Riesgo";
                                                        //pregunta 10
                                                        //if (RadioButton31.Checked == true || RadioButton32.Checked == true || RadioButton33.Checked == true)
                                                        //{
                                                        //    if (RadioButton31.Checked == true) pregunta11 = "Fuerte"; else if (RadioButton32.Checked == true) pregunta11 = "A Manejar"; else if (RadioButton33.Checked == true) pregunta11 = "Riesgo";
                                                            //pregunta 11
                                                            if (RadioButton34.Checked == true || RadioButton35.Checked == true || RadioButton36.Checked == true)
                                                            {
                                                                if (RadioButton34.Checked == true) pregunta12 = "Fuerte"; else if (RadioButton35.Checked == true) pregunta12 = "A Manejar"; else if (RadioButton36.Checked == true) pregunta12 = "Riesgo";
                                                                //pregunta 12
                                                                if (RadioButton37.Checked == true || RadioButton38.Checked == true || RadioButton39.Checked == true)
                                                                {
                                                                    if (RadioButton37.Checked == true) pregunta13 = "Fuerte"; else if (RadioButton38.Checked == true) pregunta13 = "A Manejar"; else if (RadioButton39.Checked == true) pregunta13 = "Riesgo";
                                                                    //pregunta 13
                                                                    if (RadioButton40.Checked == true || RadioButton41.Checked == true || RadioButton42.Checked == true)
                                                                    {
                                                                        if (RadioButton40.Checked == true) pregunta14 = "Fuerte"; else if (RadioButton41.Checked == true) pregunta14 = "A Manejar"; else if (RadioButton42.Checked == true) pregunta14 = "Riesgo";
                                                                        //pregunta 14
                                                                        if (Comentarios.Text == string.Empty)
                                                                        {
                                                                            //pregunta 15
                                                                            var a = "alert(\'Debe colocar algún comentario para poder envíar la información\');";
                                                                            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                                                                        }
                                                                        else
                                                                        {
                                                                            //aqui guardo las informaciones
                                                                            con.cn.Open();
                                                                            string guardaranalisis = @"Desarrollo.ConVen.GuardarAnalsisVendedores";
                                                                            com = new SqlCommand(guardaranalisis, con.cn);
                                                                            com.CommandType = CommandType.StoredProcedure;
                                                                            com.Parameters.Add("@CODVENDEDOR",SqlDbType.VarChar,15).Value = DropDownList1.Text;
                                                                            com.Parameters.Add("@CODJEFA",SqlDbType.VarChar,15).Value =Session["idvendedor"].ToString();
                                                                            com.Parameters.Add("@PREGUNTA1",SqlDbType.VarChar,10).Value =pregunta1;
                                                                            com.Parameters.Add("@PREGUNTA2", SqlDbType.VarChar, 10).Value = pregunta2;
                                                                            com.Parameters.Add("@PREGUNTA3", SqlDbType.VarChar, 10).Value = pregunta3;
                                                                            com.Parameters.Add("@PREGUNTA4", SqlDbType.VarChar, 10).Value = pregunta4;
                                                                            com.Parameters.Add("@PREGUNTA5", SqlDbType.VarChar, 10).Value = pregunta5;
                                                                            com.Parameters.Add("@PREGUNTA6", SqlDbType.VarChar, 10).Value = pregunta6;
                                                                            com.Parameters.Add("@PREGUNTA7", SqlDbType.VarChar, 10).Value = pregunta7;
                                                                            com.Parameters.Add("@PREGUNTA8", SqlDbType.VarChar, 10).Value = pregunta8;
                                                                            com.Parameters.Add("@PREGUNTA9", SqlDbType.VarChar, 10).Value = pregunta9;
                                                                            //com.Parameters.Add("@PREGUNTA10", SqlDbType.VarChar, 10).Value = pregunta10;
                                                                            //com.Parameters.Add("@PREGUNTA11", SqlDbType.VarChar, 10).Value = pregunta11;
                                                                            com.Parameters.Add("@PREGUNTA12", SqlDbType.VarChar, 10).Value = pregunta12;
                                                                            com.Parameters.Add("@PREGUNTA13", SqlDbType.VarChar, 10).Value = pregunta13;
                                                                            com.Parameters.Add("@PREGUNTA14", SqlDbType.VarChar, 10).Value = pregunta14;
                                                                            com.Parameters.Add("@COMENTARIO", SqlDbType.VarChar, 50).Value = Comentarios.Text;
                                                                            com.Parameters.Add("@ACCIONATOMAR", SqlDbType.VarChar, 50).Value = AccionTomar.Text;
                                                                            com.Parameters.Add("@TIEMPOLIMITE", SqlDbType.VarChar, 50).Value = TipoLimite.Text;
                                                                            com.UpdatedRowSource = UpdateRowSource.None;
                                                                            com.ExecuteNonQuery();
                                                                            con.cn.Close();
                                                                            
                                                                            //#region Preparacion de Correo
                                                                            //MailMessage mail = new MailMessage();
                                                                            //SmtpClient SmtpServer = new SmtpClient("mail.wurth.com.do");
                                                                            //SmtpServer.Port = 25;
                                                                            //SmtpServer.Credentials = new System.Net.NetworkCredential("servicioswurth@wurth.com.do", "Wservice01*");
                                                                            //mail.From = new MailAddress("servicioswurth@wurth.com.do");

                                                                            //mail.Subject = "Reporte de Acompañamiento con la o el  vendedor (a) " + DropDownList1.Text + " - " + Label2.Text;
                                                                            //mail.Body = "Después de un Cordial Saludo \n\n Les informo que acabo de registrar el acompañamiento realizado en el día de hoy con mi vendedor@ " + DropDownList1.Text + " - " + Label2.Text + "\n\n Se despide de usted " + Session["idvendedor"].ToString() + "-" + Session["NombreCompleto"].ToString() + " \n\n Si asi lo desea puede ir al reporte y generar la información detallada del acompañamiento del día de hoy, solo colocando el número de vendedor (a), \n\n para ir presione el siguiente link http://wurthserver:20000/Reports/Pages/Report.aspx?ItemPath=%2fReportes_Aplicaciones%2fConsulta+Vendedores%2fCON_VEN_011+Evaluacion+de+Vendedor \n\n Favor no responder este mensaje ya que fue generado por sistema automático";
                                                                            //mail.To.Add("aramirez@wurth.com.do");
                                                                            //mail.To.Add("csuero@wurth.com.do");
                                                                            //mail.CC.Add("rferrer@wurth.com.do");
                                                                            //SmtpServer.Send(mail);
                                                                            //#endregion      
                                                                      
                                                                            var a = "alert(\'Analisis enviado correctamente, gracias por usar nuestra plataforma...\');";
                                                                            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                                                                            Cancelar_Click(sender, e);
                                                                            Response.Redirect("~/informacionDiarioJefas.aspx");
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        var a = "alert(\'Debe seleccionar una opción en la pregunta 14, no respondío la pregunta número 14\');";
                                                                        ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    var a = "alert(\'Debe seleccionar una opción en la pregunta 13, no respondío la pregunta número 13\');";
                                                                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                var a = "alert(\'Debe seleccionar una opción en la pregunta 12, no respondío la pregunta número 12\');";
                                                                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                                                            }
                                                        //}
                                                        //else
                                                        //{
                                                        //    var a = "alert(\'Debe seleccionar una opción en la pregunta 11, no respondío la pregunta número 11\');";
                                                        //    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                                                        //}
                                                    //}
                                                    //else
                                                    //{
                                                    //    var a = "alert(\'Debe seleccionar una opción en la pregunta 10, no respondío la pregunta número 10\');";
                                                    //    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                                                    //}
                                                }
                                                else
                                                {
                                                    var a = "alert(\'Debe seleccionar una opción en la pregunta 9, no respondío la pregunta número 9\');";
                                                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                                                }
                                            }
                                            else
                                            {
                                                var a = "alert(\'Debe seleccionar una opción en la pregunta 8, no respondío la pregunta número 8\');";
                                                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                                            }
                                        }
                                        else
                                        {
                                            var a = "alert(\'Debe seleccionar una opción en la pregunta 7, no respondío la pregunta número 7\');";
                                            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                                        }
                                    }
                                    else
                                    {
                                        var a = "alert(\'Debe seleccionar una opción en la pregunta 6, no respondío la pregunta número 6\');";
                                        ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                                    }
                                }
                                else
                                {
                                    var a = "alert(\'Debe seleccionar una opción en la pregunta 5, no respondío la pregunta número 5\');";
                                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                                }
                            }
                            else
                            {
                                var a = "alert(\'Debe seleccionar una opción en la pregunta 4, no respondío la pregunta número 4\');";
                                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                            }
                        }
                        else
                        {
                            var a = "alert(\'Debe seleccionar una opción en la pregunta 3, no respondío la pregunta número 3\');";
                            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                        }
                    }
                    else
                    {
                        var a = "alert(\'Debe seleccionar una opción en la pregunta 2, no respondío la pregunta número 2\');";
                        ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                    }
                }
                else
                {
                    var a = "alert(\'Debe seleccionar una opción en la pregunta 1, no respondío la pregunta número 1\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                }
            }
            else
            {
                var a = "alert(\'Debe seleccionar algún vendedor para el cual le hara el analisis\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            }
        }
    }
}