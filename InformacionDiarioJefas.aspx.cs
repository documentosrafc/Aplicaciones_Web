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

public partial class InformacionDiarioJefas : System.Web.UI.Page
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
            Cancelar_Click(sender, e);
            CargarVendedores();
            Session["clientesnuevos"] = 0;            
            Session["montocotizadosglobal"] = null;
            Session["montocobradosglobal"] = null;
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
                DropDownList1.Items.Add(Session["idvendedor"].ToString());
                DropDownList1.Items.Add(table.Rows[i]["COD_VENDEDOR"].ToString());
            }
            else
            {
                DropDownList1.Items.Add(table.Rows[i]["COD_VENDEDOR"].ToString());
            }
        }
    }
    public DataTable creaciontabletemp()
    {
        Session["dt"] = null;
        dt.Columns.Add("COD_CLIENTE", typeof(string));
        dt.Columns.Add("VI", typeof(string));
        dt.Columns.Add("PROM", typeof(string));
        dt.Columns.Add("INFO", typeof(string));
        dt.Columns.Add("PLACAS", typeof(string));
        dt.Columns.Add("PIEDRAS", typeof(string));
        dt.Columns.Add("OPER", typeof(string));
        dt.Columns.Add("CORREO", typeof(string));
        dt.Columns.Add("ACTI", typeof(string));
        dt.Columns.Add("M_COBR", typeof(string));
        dt.Columns.Add("M_COTI", typeof(string));
        dt.Columns.Add("F_RUTA", typeof(string));
        dt.Columns.Add("RECUP", typeof(string));
        dt.Columns.Add("PRO_COB", typeof(string));
        dt.Columns.Add("PEDI", typeof(string));
        dt.Columns.Add("POS", typeof(string));
        dt.Columns.Add("ENMERCA", typeof(string));
        return dt;
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
    private void GuardarAcopanamiento()
    {
        System.Threading.Thread.Sleep(1000);
        if (DropDownList1.Text == "Vendedores" || Label2.Text == string.Empty || rutaefectuada.Text == string.Empty || GridView1.Rows.Count <= 0)
        {
            var a = "alert(\'Para poder enviar un acompañamiento es necesario que rellene todos los campos con asteriscos (*) y colocar el detalle de cada uno de los clientes visitados\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
        }
        else
        {
            con.cn.Open();
            string GuardarCab = "Desarrollo.ConVen.GuardarAcompanamientos_Cab";
            com = new SqlCommand(GuardarCab, con.cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@COD_VENDEDOR", SqlDbType.VarChar, 20).Value = DropDownList1.Text;
            com.Parameters.Add("@COD_JEFA", SqlDbType.VarChar, 20).Value = Session["idvendedor"].ToString();
            com.Parameters.Add("@RUTA_EFECTUADA", SqlDbType.VarChar, 60).Value = rutaefectuada.Text;
            com.Parameters.Add("@NOVISITA", SqlDbType.Int).Value = int.Parse(novisita.Text);
            if (nopedido.Text == string.Empty) { com.Parameters.Add("@NOPEDIDO", SqlDbType.Int).Value = 0; } else { com.Parameters.Add("@NOPEDIDO", SqlDbType.Int).Value = int.Parse(nopedido.Text); }
            if (noposiciones.Text == string.Empty) { com.Parameters.Add("@NOPOSICIONES", SqlDbType.Int).Value = 0; } else { com.Parameters.Add("@NOPOSICIONES", SqlDbType.Int).Value = int.Parse(noposiciones.Text); }
            com.Parameters.Add("@NOCOBROS", SqlDbType.Int).Value = int.Parse(nocobros.Text);
            com.Parameters.Add("@NOCOTIZACION", SqlDbType.Int).Value = int.Parse(nocotizacion.Text);
            com.Parameters.Add("@MONTOCOBROS", SqlDbType.Money).Value = float.Parse(montocobros.Text);
            com.Parameters.Add("@MONTOCOTIZACION", SqlDbType.Money).Value = float.Parse(montocotizaciones.Text);
            if (noclientesnuevosvisitados.Text == string.Empty) { com.Parameters.Add("@NOCLIENTESNUEVOS", SqlDbType.Int).Value = 0; } else { com.Parameters.Add("@NOCLIENTESNUEVOS", SqlDbType.Int).Value = int.Parse(noclientesnuevosvisitados.Text); }
            if (noclientesrecuperadosvisitados.Text == string.Empty) { com.Parameters.Add("@NOCLIENTESRECUPERADOS", SqlDbType.Int).Value = 0; } else { com.Parameters.Add("@NOCLIENTESRECUPERADOS", SqlDbType.Int).Value = int.Parse(noclientesnuevosvisitados.Text); }
            if (clientesproblemasdecobros.Text == string.Empty) { com.Parameters.Add("@NOCLIENTES_PRON_COBROS", SqlDbType.Int).Value = 0; } else { com.Parameters.Add("@NOCLIENTES_PRON_COBROS", SqlDbType.Int).Value = int.Parse(clientesproblemasdecobros.Text); }
            if (articulosnuevos.Text == string.Empty) { com.Parameters.Add("@CANT_ART_NUEVOS_VENDIDOS", SqlDbType.Int).Value = 0; } else { com.Parameters.Add("@CANT_ART_NUEVOS_VENDIDOS", SqlDbType.Int).Value = int.Parse(articulosnuevos.Text); }
            if (fueraderuta.Text == string.Empty) { com.Parameters.Add("@NOCLINTESFUERADERUTA", SqlDbType.Int).Value = 0; } else { com.Parameters.Add("@NOCLINTESFUERADERUTA", SqlDbType.Int).Value = int.Parse(noclientesrecuperadosvisitados.Text); }
            com.Parameters.Add("@ACOMPANAMIENTO", SqlDbType.VarChar, 2).Value = DropDownList4.Text;
            com.Parameters.Add("@MONTOPEDIDOS", SqlDbType.Money).Value = float.Parse(montopedidos.Text);
            com.UpdatedRowSource = UpdateRowSource.None;
            com.ExecuteNonQuery();

            string DEL = @"DELETE FROM Desarrollo.ConVen.ACOMPANAMIENTO_DET WHERE COD_VENDEDOR = '" + DropDownList1.Text + "' AND FECHA = CONVERT(VARCHAR,GETDATE(),112)";
            com = new SqlCommand(DEL, con.cn);
            com.UpdatedRowSource = UpdateRowSource.None;
            com.ExecuteNonQuery();
            con.cn.Close();

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                con.cn.Open();
                string guadarDet = @"Desarrollo.ConVen.GuardarAcompanamiento_Det";
                com = new SqlCommand(guadarDet, con.cn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@COD_VENDEDOR", SqlDbType.VarChar, 20).Value = DropDownList1.Text;
                com.Parameters.Add("@COD_CLIENTE", SqlDbType.VarChar, 20).Value = GridView1.Rows[i].Cells[1].Text.Trim();
                com.Parameters.Add("@VISITADO", SqlDbType.VarChar, 2).Value = GridView1.Rows[i].Cells[2].Text.Trim();
                com.Parameters.Add("@PROMO", SqlDbType.VarChar, 2).Value = GridView1.Rows[i].Cells[3].Text.Trim();
                com.Parameters.Add("@NOINFOS", SqlDbType.Int).Value = int.Parse(GridView1.Rows[i].Cells[4].Text);
                com.Parameters.Add("@PLACAS", SqlDbType.VarChar, 2).Value = GridView1.Rows[i].Cells[5].Text.Trim();
                com.Parameters.Add("@PIEDRAS", SqlDbType.VarChar, 2).Value = GridView1.Rows[i].Cells[6].Text.Trim();
                com.Parameters.Add("@NO_OPER", SqlDbType.Int).Value = int.Parse(GridView1.Rows[i].Cells[7].Text.Trim());
                com.Parameters.Add("@CORREO", SqlDbType.VarChar, 50).Value = GridView1.Rows[i].Cells[8].Text.Trim();
                com.Parameters.Add("@ACTIVIDAD", SqlDbType.VarChar, 15).Value = GridView1.Rows[i].Cells[9].Text.Trim();
                com.Parameters.Add("@MONTO_COBRADO", SqlDbType.Money).Value = float.Parse(GridView1.Rows[i].Cells[10].Text.Trim());
                com.Parameters.Add("@MONTO_COTIZACION", SqlDbType.Money).Value = float.Parse(GridView1.Rows[i].Cells[11].Text.Trim());
                com.Parameters.Add("@FRUTA", SqlDbType.VarChar, 2).Value = GridView1.Rows[i].Cells[12].Text.Trim();
                com.Parameters.Add("@RECUP", SqlDbType.VarChar, 2).Value = GridView1.Rows[i].Cells[13].Text.Trim();
                com.Parameters.Add("@PROCOB", SqlDbType.VarChar, 2).Value = GridView1.Rows[i].Cells[14].Text.Trim();
                com.Parameters.Add("@MONTO_PED", SqlDbType.Float).Value = float.Parse(GridView1.Rows[i].Cells[15].Text.Trim());
                com.Parameters.Add("@NO_PED", SqlDbType.Int).Value = int.Parse(GridView1.Rows[i].Cells[16].Text.Trim());
                com.Parameters.Add("@EMERCANCIA", SqlDbType.VarChar, 2).Value = GridView1.Rows[i].Cells[17].Text.ToString();
                com.UpdatedRowSource = UpdateRowSource.None;
                com.ExecuteNonQuery();
                con.cn.Close();
            }
            if (ListBox1.Items.Count > 0)
            {
                con.cn.Open();
                string DELCLIE = @"DELETE FROM Desarrollo.CONVEN.CLIENTES_NUEVOS_ACOMP WHERE COD_VENDEDOR='" + DropDownList1.Text + "' AND FECHA=CONVERT(VARCHAR,GETDATE(),112)";
                com = new SqlCommand(DELCLIE, con.cn);
                com.UpdatedRowSource = UpdateRowSource.None;
                com.ExecuteNonQuery();
                con.cn.Close();

                for (int i = 0; i < ListBox1.Items.Count; i++)
                {
                    con.cn.Open();
                    string guadarDet = @"Desarrollo.ConVen.GuadarAcompClientesNuevos";
                    com = new SqlCommand(guadarDet, con.cn);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@COD_VENDEDOR", SqlDbType.VarChar, 20).Value = DropDownList1.Text;
                    com.Parameters.Add("@NOMBRE_CLIENTE", SqlDbType.VarChar, 50).Value = ListBox1.Items[i].Text;
                    com.UpdatedRowSource = UpdateRowSource.None;
                    com.ExecuteNonQuery();
                    con.cn.Close();
                }
            }
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string consultar_nombre = @"SELECT * FROM DESARROLLO.LISTCONT.LISTADECONTACTO WHERE NO_VENDEDOR='" + DropDownList1.Text + "'";
        com = new SqlCommand(consultar_nombre, con.cn);
        data = new SqlDataAdapter(com);
        table = new DataTable();
        data.Fill(table);

        if (table.Rows.Count > 0)
        {
            Label2.Text = table.Rows[0]["NOMBRE_APELLIDO"].ToString();
            DropDownList1.Enabled = false;
        }

        //aqui estoy llenando todos los clientes que estan asignado al vendedor seleccionado.
        string buscarclientes = @"SELECT A.ACCOUNTNUM AS CODIGO FROM [10.0.0.54].AX50DOM_LIVE.DBO.CUSTTABLE A WHERE A.WPMSALESUNITID='" + DropDownList1.Text + "'";
        data = new SqlDataAdapter(buscarclientes, con.cn);
        table = new DataTable();
        data.Fill(table);
        if (table.Rows.Count > 0)
        {
            DropDownList2.Items.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (DropDownList2.Items.Count <= 0)
                {
                    DropDownList2.Items.Add("Clientes");
                    DropDownList2.Items.Add("Cliente Nuevo");
                    DropDownList2.Items.Add(table.Rows[i]["CODIGO"].ToString());
                }
                else
                {
                    DropDownList2.Items.Add(table.Rows[i]["CODIGO"].ToString());
                }
            }
            //AQUI VAMOS A HACER UN BUCLE DONDE VAMOS A COLOCAR TODAS LAS INFORMACIONES CUANDO SE SELECCIONE EL VENDEDOR
            string BUSCARACOMPANAMIENTOPENDIENTE = @"SELECT * FROM Desarrollo.ConVen.ACOMPANAMIENTO_CAB A 
                                                    INNER JOIN  Desarrollo.ConVen.ACOMPANAMIENTO_DET B ON A.COD_VENDEDOR=B.COD_VENDEDOR AND A.FECHA_REGISTRO = B.FECHA
                                                    WHERE A.COD_VENDEDOR ='" + DropDownList1.Text + "' AND A.FECHA_REGISTRO = CONVERT(VARCHAR,GETDATE(),112) AND A.ESTADO = 0";
            com = new SqlCommand(BUSCARACOMPANAMIENTOPENDIENTE, con.cn);
            data = new SqlDataAdapter(com);
            table = new DataTable();
            data.Fill(table);

            if (table.Rows.Count > 0)
            {
                novisita.Text = table.Rows[0]["NOVISITA"].ToString();
                nocobros.Text = table.Rows[0]["NOCOBROS"].ToString();
                montocobros.Text = string.Format("{0:#,##0.00}", float.Parse(table.Rows[0]["MONTOCOBROS"].ToString()));
                nocotizacion.Text = table.Rows[0]["NOCOTIZACION"].ToString();
                montocotizaciones.Text = string.Format("{0:#,##0.00}", float.Parse(table.Rows[0]["MONTOCOTIZACION"].ToString()));
                rutaefectuada.Text = table.Rows[0]["RUTA_EFECTUADA"].ToString();

                if (int.Parse(table.Rows[0]["NOPEDIDO"].ToString()) <= 0) { nopedido.Text = "0"; } else { nopedido.Text = table.Rows[0]["NOPEDIDO"].ToString(); }
                if (int.Parse(table.Rows[0]["NOPOSICIONES"].ToString()) <= 0) { noposiciones.Text = "0"; } else { noposiciones.Text = table.Rows[0]["NOPOSICIONES"].ToString(); }
                if (int.Parse(table.Rows[0]["NOCLIENTESNUEVOS"].ToString()) <= 0) { noclientesnuevosvisitados.Text = "0"; } else { noclientesnuevosvisitados.Text = table.Rows[0]["NOCLIENTESNUEVOS"].ToString(); }
                if (int.Parse(table.Rows[0]["NOCLIENTESRECUPERADOS"].ToString()) <= 0) { noclientesrecuperadosvisitados.Text = "0"; } else { noclientesrecuperadosvisitados.Text = table.Rows[0]["NOCLIENTESRECUPERADOS"].ToString(); }
                if (int.Parse(table.Rows[0]["NOCLIENTES_PRON_COBROS"].ToString()) <= 0) { clientesproblemasdecobros.Text = "0"; } else { clientesproblemasdecobros.Text = table.Rows[0]["NOCLIENTES_PRON_COBROS"].ToString(); }
                if (int.Parse(table.Rows[0]["CANT_ART_NUEVOS_VENDIDOS"].ToString()) <= 0) { articulosnuevos.Text = "0"; } else { articulosnuevos.Text = table.Rows[0]["CANT_ART_NUEVOS_VENDIDOS"].ToString(); }
                if (int.Parse(table.Rows[0]["NOCLINTESFUERADERUTA"].ToString()) <= 0) { fueraderuta.Text = "0"; } else { fueraderuta.Text = table.Rows[0]["NOCLINTESFUERADERUTA"].ToString(); }

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (Session["dt"] == null)
                    {
                        DataTable dt = creaciontabletemp();
                        DataRow datarow;
                        datarow = dt.NewRow();
                        datarow["COD_CLIENTE"] = table.Rows[i]["COD_CLIENTE"].ToString();
                        datarow["VI"] = table.Rows[i]["VISITADO"].ToString();
                        datarow["PROM"] = table.Rows[i]["PROMO"].ToString();
                        datarow["INFO"] = table.Rows[i]["NOINFOS"].ToString();
                        datarow["PLACAS"] = table.Rows[i]["PLACAS"].ToString();
                        datarow["PIEDRAS"] = table.Rows[i]["PIEDRAS"].ToString();
                        datarow["OPER"] = table.Rows[i]["NO_OPER"].ToString();
                        datarow["CORREO"] = table.Rows[i]["CORREO"].ToString();
                        datarow["ACTI"] = table.Rows[i]["ACTIVIDAD"].ToString();
                        datarow["M_COBR"] = table.Rows[i]["MONTO_COBRADO"].ToString();
                        datarow["M_COTI"] = table.Rows[i]["MONTO_COTIZACION"].ToString();
                        datarow["F_RUTA"] = table.Rows[i]["F_RUTA"].ToString();
                        datarow["RECUP"] = table.Rows[i]["RECUP"].ToString();
                        datarow["PRO_COB"] = table.Rows[i]["PRO_COB"].ToString();
                        datarow["PEDI"] = table.Rows[i]["MONTO_PED"].ToString();
                        datarow["POS"] = table.Rows[i]["NO_POS"].ToString();
                        datarow["ENMERCA"] = table.Rows[i]["EMERCANCIA"].ToString();
                        dt.Rows.Add(datarow);
                        Session["dt"] = dt;

                        montopedidos.Text = Convert.ToString(float.Parse(table.Rows[i]["MONTO_PED"].ToString()));

                        GridView1.DataSource = new DataView(dt);
                        GridView1.DataBind();
                    }
                    else
                    {
                        DataTable dt = (Session["dt"]) as DataTable;
                        DataRow datarow;
                        datarow = dt.NewRow();
                        datarow["COD_CLIENTE"] = table.Rows[i]["COD_CLIENTE"].ToString();
                        datarow["VI"] = table.Rows[i]["VISITADO"].ToString();
                        datarow["PROM"] = table.Rows[i]["PROMO"].ToString();
                        datarow["INFO"] = table.Rows[i]["NOINFOS"].ToString();
                        datarow["PLACAS"] = table.Rows[i]["PLACAS"].ToString();
                        datarow["PIEDRAS"] = table.Rows[i]["PIEDRAS"].ToString();
                        datarow["OPER"] = table.Rows[i]["NO_OPER"].ToString();
                        datarow["CORREO"] = table.Rows[i]["CORREO"].ToString();
                        datarow["ACTI"] = table.Rows[i]["ACTIVIDAD"].ToString();
                        datarow["M_COBR"] = table.Rows[i]["MONTO_COBRADO"].ToString();
                        datarow["M_COTI"] = table.Rows[i]["MONTO_COTIZACION"].ToString();
                        datarow["F_RUTA"] = table.Rows[i]["F_RUTA"].ToString();
                        datarow["RECUP"] = table.Rows[i]["RECUP"].ToString();
                        datarow["PRO_COB"] = table.Rows[i]["PRO_COB"].ToString();
                        datarow["PEDI"] = table.Rows[i]["MONTO_PED"].ToString();
                        datarow["POS"] = table.Rows[i]["NO_POS"].ToString();
                        datarow["ENMERCA"] = table.Rows[i]["EMERCANCIA"].ToString();
                        dt.Rows.Add(datarow);
                        Session["dt"] = dt;

                        montopedidos.Text = Convert.ToString(float.Parse(montopedidos.Text) + float.Parse(table.Rows[i]["MONTO_PED"].ToString()));

                        GridView1.DataSource = new DataView(dt);
                        GridView1.DataBind();
                    }

                }

                string clientesnuevos = @"SELECT * FROM Desarrollo.ConVen.CLIENTES_NUEVOS_ACOMP WHERE COD_VENDEDOR='" + DropDownList1.Text + "' AND FECHA=CONVERT(VARCHAR,GETDATE(),112)";
                com = new SqlCommand(clientesnuevos, con.cn);
                data = new SqlDataAdapter(com);
                table = new DataTable();
                data.Fill(table);

                if (table.Rows.Count > 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        ListBox1.Items.Add(table.Rows[i]["NOMBRE_CLIENTE"].ToString());
                    }
                }
            }
            else
            {
                //aqui valido que ya se haya enviado el acompanamiento por parte del jefe de zona.
                string BUSCARACOMPANAMIENTOENVIADO = @"SELECT * FROM Desarrollo.ConVen.ACOMPANAMIENTO_CAB A                                                     
                                                    WHERE A.COD_VENDEDOR ='" + DropDownList1.Text + "' AND A.FECHA_REGISTRO = CONVERT(VARCHAR,GETDATE(),112) AND A.ESTADO = 1";
                com = new SqlCommand(BUSCARACOMPANAMIENTOENVIADO, con.cn);
                data = new SqlDataAdapter(com);
                table = new DataTable();
                data.Fill(table);

                if (table.Rows.Count > 0)
                {
                    var a = "alert(\'Lo sentimos pero ya usted ha enviado el reporte del acompañamiento que realizo con este vendedor, realize mañana el que le corresponde, gracias.\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);

                    DropDownList1.Text = "Vendedores";
                    DropDownList1.Enabled = true;
                    Label2.Text = string.Empty;
                }
            }
        }
        else
        {
            var a = "alert(\'No hemos encontrado un vendedor con ese número de seleccionado\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);

            DropDownList1.Text = "Vendedores";
            DropDownList1.Enabled = true;
            Label2.Text = string.Empty;
        }
    }
    protected void Cancelar_Click(object sender, EventArgs e)
    {
        Label2.Text = string.Empty;
        DropDownList1.Enabled = true;
        DropDownList1.Text = "Vendedores";
        DropDownList2.Items.Clear();
        GridView1.DataSource = null;
        GridView1.DataBind();
        ListBox1.Items.Clear();
        novisita.Text = "0";
        noposiciones.Text = "0";
        nopedido.Text = "0";
        nocobros.Text = "0";
        nocotizacion.Text = "0";
        montocobros.Text = "0";
        montocotizaciones.Text = "0";
        noclientesnuevosvisitados.Text = "0";
        noclientesrecuperadosvisitados.Text = "0";
        clientesproblemasdecobros.Text = "0";
        RadioButton7.Checked = false;
        RadioButton8.Checked = false;
        RadioButton9.Checked = false;
        RadioButton10.Checked = false;
        RadioButton11.Checked = false;
        RadioButton12.Checked = false;
        RadioButton13.Checked = false;
        RadioButton14.Checked = false;
        RadioButton6.Checked = false;
        fueraderuta.Text = "0";
        articulosnuevos.Text = "0";
        Session["clientesnuevos"] = 0;
        rutaefectuada.Text = string.Empty;
        CORREO.ReadOnly = false;
        OPERARIO.ReadOnly = false;
        DropDownList3.Enabled = true;
        OPERARIO.Text = string.Empty;
        CORREO.Text = string.Empty;
        MCOTIZACIONES.Text = string.Empty;
        MONTOCOBRADO.Text = string.Empty;
        Session["dt"] = null;
        Noinfosmostrados.Text = string.Empty;
        Mpedido.Text = string.Empty;
        noposicion.Text = string.Empty;
        montopedidos.Text = "0";
        DropDownList4.Enabled = true;
        DropDownList4.Text = string.Empty;

    }
    protected void Subir_Click(object sender, EventArgs e)
    {
        //if (nombreposibleclientenuevo.Text == string.Empty)
        //{
        //    var a = "alert(\'Para poder subir un nombre es necesario que escriba el mismo\');";
        //    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
        //}
        //else
        //{
        //    ListBox1.Items.Add(nombreposibleclientenuevo.Text);
        //    nombreposibleclientenuevo.Text = string.Empty;
        //    noclientesnuevosvisitados.Text = Convert.ToString(ListBox1.Items.Count);
        //}
    }
    protected void eliminarnombre_Click(object sender, EventArgs e)
    {
        if (ListBox1.Items.Count <= 0)
        {
            var a = "alert(\'No podemos eliminar un nombre que no ha insertado\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
        }
        else
        {
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex);
            noclientesnuevosvisitados.Text = Convert.ToString(ListBox1.Items.Count);
        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList2.Text != "Cliente Nuevo")
        {
            string buscarcliente = @"SELECT A.NAME AS NOMBRE, A.EMAIL AS CORREO, B.NUMBEROFEMPLOYEES AS OPERARIO, A.Dimension AS ACTIVIDAD
                                FROM [10.0.0.54].AX50DOM_LIVE.DBO.CUSTTABLE A INNER JOIN [10.0.0.54].AX50DOM_LIVE.DBO.DirOrganizationDetail B 
                                ON A.PARTYID=B.PARTYID WHERE A.ACCOUNTNUM='" + DropDownList2.Text + "' AND A.WPMSALESUNITID='" + DropDownList1.Text + "'";
            com = new SqlCommand(buscarcliente, con.cn);
            data = new SqlDataAdapter(com);
            table = new DataTable();
            data.Fill(table);

            if (table.Rows.Count > 0)
            {
                DropDownList3.Text = "";

                Label18.Text = table.Rows[0]["NOMBRE"].ToString().Trim();
                OPERARIO.Text = table.Rows[0]["OPERARIO"].ToString().Trim();
                DropDownList3.Text = table.Rows[0]["ACTIVIDAD"].ToString().Trim();
                CORREO.Text = table.Rows[0]["CORREO"].ToString().Trim();
            }
        }
        else if (DropDownList2.Text == "Cliente Nuevo")
        {
            Label18.ReadOnly = false;
            Label18.Text = string.Empty;
            Label18.Focus();
        }
    }
    protected void RadioButton7_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton8.Checked == true)
        {
            RadioButton8.Checked = false;
        }
    }
    protected void RadioButton8_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton7.Checked == true)
        {
            RadioButton7.Checked = false;
        }
    }
    protected void RadioButton9_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton10.Checked == true)
        {
            RadioButton10.Checked = false;
        }
    }
    protected void RadioButton10_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton9.Checked == true)
        {
            RadioButton9.Checked = false;
        }
    }
    protected void RadioButton11_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton12.Checked == true)
        {
            RadioButton12.Checked = false;
        }
    }
    protected void RadioButton12_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton11.Checked == true)
        {
            RadioButton11.Checked = false;
        }
    }
    protected void RadioButton13_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton14.Checked == true)
        {
            RadioButton14.Checked = false;
        }
    }
    protected void RadioButton14_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton13.Checked == true)
        {
            RadioButton13.Checked = false;
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow ro = GridView1.Rows[e.RowIndex];
        DataTable dttemp = (Session["dt"]) as DataTable;
        for (int i = 0; i < dttemp.Rows.Count; i++)
        {
            if (dttemp.Rows[i]["COD_CLIENTE"].ToString() == ro.Cells[1].Text)
            {
                if (ro.Cells[2].Text == "Si") { novisita.Text = Convert.ToString(float.Parse(novisita.Text) - 1); }
                if (float.Parse(ro.Cells[10].Text) > 0) { montocobros.Text = Convert.ToString(float.Parse(montocobros.Text) - float.Parse(ro.Cells[10].Text)); nocobros.Text = Convert.ToString(float.Parse(nocobros.Text) - 1); }
                if (float.Parse(ro.Cells[11].Text) > 0) { montocotizaciones.Text = Convert.ToString(float.Parse(montocotizaciones.Text) - float.Parse(ro.Cells[11].Text)); nocotizacion.Text = Convert.ToString(float.Parse(nocotizacion.Text) - 1); }

                if (ro.Cells[12].Text == "Si") { fueraderuta.Text = Convert.ToString(float.Parse(fueraderuta.Text) - 1); }
                if (ro.Cells[13].Text == "Si") { noclientesrecuperadosvisitados.Text = Convert.ToString(float.Parse(noclientesrecuperadosvisitados.Text) - 1); }
                if (ro.Cells[14].Text == "Si") { clientesproblemasdecobros.Text = Convert.ToString(float.Parse(clientesproblemasdecobros.Text) - 1); }
                if (float.Parse(ro.Cells[15].Text) >= 0) { nopedido.Text = Convert.ToString(float.Parse(nopedido.Text) - 1); montopedidos.Text = Convert.ToString(float.Parse(montopedidos.Text) - float.Parse(ro.Cells[15].Text)); }
                if (float.Parse(ro.Cells[16].Text) >= 0) { noposiciones.Text = Convert.ToString(float.Parse(noposiciones.Text) - float.Parse(ro.Cells[16].Text)); }

                dttemp.Rows.RemoveAt(i);

                if (ListBox1.Items.Count > 0)
                {
                    for (int L = 0; L < ListBox1.Items.Count; L++)
                    {
                        if (ListBox1.Items[L].Text.Substring(0, 3) == ro.Cells[1].Text)
                        {
                            ListBox1.Items.RemoveAt(L);
                        }
                    }
                }
            }
        }
        Session["dt"] = dttemp;
        GridView1.DataSource = new DataView(dttemp);
        GridView1.DataBind();
    }
    protected void subiranalisis_Click(object sender, EventArgs e)
    {
        string numerocliente;

        if (DropDownList4.Text == string.Empty || DropDownList1.Text == "Vendedores" || DropDownList2.Text == "Clientes" || Label2.Text == string.Empty || Label18.Text == string.Empty || rutaefectuada.Text == string.Empty)
        {
            var a = "alert(\'Para poder subir un análisis es necesario que llene todos los campos que tienen asteriscos (*)\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
        }
        else if (CORREO.Text != string.Empty && Email_Bien_Escrito(CORREO.Text) == false)
        {
            var a = "alert(\'El campo de corre electronico no esta correto, si desea colocar un correo pues hagalo bien de lo contrario dejelo vacio\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
        }
        else
        {
            if (RadioButton3.Checked == true) { noclientesrecuperadosvisitados.Text = Convert.ToString(float.Parse(noclientesrecuperadosvisitados.Text) + 1); }
            if (RadioButton4.Checked == true) { clientesproblemasdecobros.Text = Convert.ToString(float.Parse(clientesproblemasdecobros.Text) + 1); }
            if (RadioButton5.Checked == true) { fueraderuta.Text = Convert.ToString(float.Parse(fueraderuta.Text) + 1); }
            if (Session["dt"] == null)
            {
                numerocliente = DropDownList2.Text;
                if (DropDownList2.Text == "Cliente Nuevo") { numerocliente = "CN" + Convert.ToString(int.Parse(Session["clientesnuevos"].ToString()) + 1); ListBox1.Items.Add(numerocliente + " " + Label18.Text); noclientesnuevosvisitados.Text = Convert.ToString(ListBox1.Items.Count); Session["clientesnuevos"] = int.Parse(Session["clientesnuevos"].ToString()) + 1; }
                DataTable dt = creaciontabletemp();
                DataRow datarow;
                datarow = dt.NewRow();
                datarow["COD_CLIENTE"] = numerocliente;
                if (RadioButton7.Checked == true) { datarow["VI"] = "Si"; novisita.Text = Convert.ToString(float.Parse(novisita.Text) + 1); } else { datarow["VI"] = "No"; }
                if (RadioButton9.Checked == true) { datarow["PROM"] = "Si"; } else { datarow["PROM"] = "No"; }
                if (Noinfosmostrados.Text == string.Empty) { datarow["INFO"] = "0"; } else { datarow["INFO"] = Noinfosmostrados.Text; }
                if (RadioButton11.Checked == true) { datarow["PLACAS"] = "Si"; } else { datarow["PLACAS"] = "No"; }
                if (RadioButton13.Checked == true) { datarow["PIEDRAS"] = "Si"; } else { datarow["PIEDRAS"] = "No"; }
                datarow["OPER"] = OPERARIO.Text;
                if (CORREO.Text == string.Empty) { datarow["CORREO"] = "nocorreo@nocorreo.com"; } else { datarow["CORREO"] = CORREO.Text; }
                datarow["ACTI"] = DropDownList3.Text;
                if (MONTOCOBRADO.Text == string.Empty) { datarow["M_COBR"] = "0"; } else { datarow["M_COBR"] = string.Format("{0:#,##0.00}", float.Parse(MONTOCOBRADO.Text)); montocobros.Text = string.Format("{0:#,##0.00}", float.Parse(montocobros.Text) + float.Parse(MONTOCOBRADO.Text)); nocobros.Text = Convert.ToString(float.Parse(nocobros.Text) + 1); }
                if (MCOTIZACIONES.Text == string.Empty) { datarow["M_COTI"] = "0"; } else { datarow["M_COTI"] = string.Format("{0:#,##0.00}", float.Parse(MCOTIZACIONES.Text)); montocotizaciones.Text = string.Format("{0:#,##0.00}", float.Parse(montocotizaciones.Text) + float.Parse(MCOTIZACIONES.Text)); nocotizacion.Text = Convert.ToString(float.Parse(nocotizacion.Text) + 1); }

                if (Mpedido.Text == string.Empty) { datarow["PEDI"] = "0"; } else { datarow["PEDI"] = Mpedido.Text; nopedido.Text = Convert.ToString(float.Parse(nopedido.Text) + 1); montopedidos.Text = Convert.ToString(float.Parse(montopedidos.Text) + float.Parse(Mpedido.Text)); }
                if (noposicion.Text == string.Empty) { datarow["POS"] = "0"; } else { datarow["POS"] = noposicion.Text; noposiciones.Text = Convert.ToString(float.Parse(noposiciones.Text) + float.Parse(noposicion.Text)); }

                if (RadioButton5.Checked == true) { datarow["F_RUTA"] = "Si"; } else { datarow["F_RUTA"] = "No"; }
                if (RadioButton3.Checked == true) { datarow["RECUP"] = "Si"; } else { datarow["RECUP"] = "No"; }
                if (RadioButton4.Checked == true) { datarow["PRO_COB"] = "Si"; } else { datarow["PRO_COB"] = "No"; }
                if (RadioButton6.Checked == true) { datarow["ENMERCA"] = "Si"; } else { datarow["ENMERCA"] = "No"; }

                dt.Rows.Add(datarow);

                Session["dt"] = dt;
                GridView1.DataSource = new DataView(dt);
                GridView1.DataBind();
            }
            else
            {
                numerocliente = DropDownList2.Text;
                if (DropDownList2.Text == "Cliente Nuevo") { numerocliente = "CN" + Convert.ToString(int.Parse(Session["clientesnuevos"].ToString()) + 1); ListBox1.Items.Add(numerocliente + " " + Label18.Text); noclientesnuevosvisitados.Text = Convert.ToString(ListBox1.Items.Count); Session["clientesnuevos"] = int.Parse(Session["clientesnuevos"].ToString()) + 1; }

                DataTable dt = (Session["dt"]) as DataTable;
                DataRow datarow;
                datarow = dt.NewRow();
                datarow["COD_CLIENTE"] = numerocliente;
                if (RadioButton7.Checked == true) { datarow["VI"] = "Si"; novisita.Text = Convert.ToString(float.Parse(novisita.Text) + 1); } else { datarow["VI"] = "No"; }
                if (RadioButton9.Checked == true) { datarow["PROM"] = "Si"; } else { datarow["PROM"] = "No"; }
                if (Noinfosmostrados.Text == string.Empty) { datarow["INFO"] = "0"; } else { datarow["INFO"] = Noinfosmostrados.Text; }
                if (RadioButton11.Checked == true) { datarow["PLACAS"] = "Si"; } else { datarow["PLACAS"] = "No"; }
                if (RadioButton13.Checked == true) { datarow["PIEDRAS"] = "Si"; } else { datarow["PIEDRAS"] = "No"; }
                datarow["OPER"] = OPERARIO.Text;
                if (CORREO.Text == string.Empty) { datarow["CORREO"] = "nocorreo@nocorreo.com"; } else { datarow["CORREO"] = CORREO.Text; }
                datarow["ACTI"] = DropDownList3.Text;
                if (MONTOCOBRADO.Text == string.Empty) { datarow["M_COBR"] = "0"; } else { datarow["M_COBR"] = string.Format("{0:#,##0.00}", float.Parse(MONTOCOBRADO.Text)); montocobros.Text = string.Format("{0:#,##0.00}", float.Parse(montocobros.Text) + float.Parse(MONTOCOBRADO.Text)); nocobros.Text = Convert.ToString(float.Parse(nocobros.Text) + 1); }
                if (MCOTIZACIONES.Text == string.Empty) { datarow["M_COTI"] = "0"; } else { datarow["M_COTI"] = string.Format("{0:#,##0.00}", float.Parse(MCOTIZACIONES.Text)); montocotizaciones.Text = string.Format("{0:#,##0.00}", float.Parse(montocotizaciones.Text) + float.Parse(MCOTIZACIONES.Text)); nocotizacion.Text = Convert.ToString(float.Parse(nocotizacion.Text) + 1); }

                if (Mpedido.Text == string.Empty) { datarow["PEDI"] = "0"; } else { datarow["PEDI"] = Mpedido.Text; nopedido.Text = Convert.ToString(float.Parse(nopedido.Text) + 1); montopedidos.Text = Convert.ToString(float.Parse(montopedidos.Text) + float.Parse(Mpedido.Text)); }
                if (noposicion.Text == string.Empty) { datarow["POS"] = "0"; } else { datarow["POS"] = noposicion.Text; noposiciones.Text = Convert.ToString(float.Parse(noposiciones.Text) + float.Parse(noposicion.Text)); }

                if (RadioButton5.Checked == true) { datarow["F_RUTA"] = "Si"; } else { datarow["F_RUTA"] = "No"; }
                if (RadioButton3.Checked == true) { datarow["RECUP"] = "Si"; } else { datarow["RECUP"] = "No"; }
                if (RadioButton4.Checked == true) { datarow["PRO_COB"] = "Si"; } else { datarow["PRO_COB"] = "No"; }
                if (RadioButton6.Checked == true) { datarow["ENMERCA"] = "Si"; } else { datarow["ENMERCA"] = "No"; }
                dt.Rows.Add(datarow);
                Session["dt"] = dt;
                GridView1.DataSource = new DataView(dt);
                GridView1.DataBind();
            }
            RadioButton7.Checked = false;
            RadioButton8.Checked = false;
            RadioButton9.Checked = false;
            RadioButton10.Checked = false;
            RadioButton11.Checked = false;
            RadioButton12.Checked = false;
            RadioButton13.Checked = false;
            RadioButton14.Checked = false;
            RadioButton3.Checked = false;
            RadioButton4.Checked = false;
            RadioButton5.Checked = false;
            RadioButton6.Checked = false;
            OPERARIO.Text = string.Empty;
            OPERARIO.ReadOnly = false;
            CORREO.Text = string.Empty;
            CORREO.ReadOnly = false;
            Noinfosmostrados.Text = string.Empty;
            DropDownList2.Text = "Clientes";
            Label18.Text = string.Empty;
            DropDownList3.Text = "";
            MONTOCOBRADO.Text = string.Empty;
            MCOTIZACIONES.Text = string.Empty;
            Mpedido.Text = string.Empty;
            noposicion.Text = string.Empty;
            Label18.ReadOnly = true;
        }
    }
    protected void Enviar_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue.Substring(confirmValue.Length - 2) == "Si")
        {
            if (GridView1.Rows.Count <= 0 || int.Parse(novisita.Text) <= 0 || Label2.Text == string.Empty || Label8.Text == string.Empty)
            {
                var a = "alert(\'para poder envíar un acompañamiento debe primero que colocar todas las informaciones que solicitan.\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            }
            else
            {
                GuardarAcopanamiento();

                con.cn.Open();
                string ENVIARACOMP = @"UPDATE Desarrollo.ConVen.ACOMPANAMIENTO_CAB SET ESTADO = 1 WHERE COD_VENDEDOR='" + DropDownList1.Text + "' AND FECHA_REGISTRO=CONVERT(VARCHAR,GETDATE(),112)";
                com = new SqlCommand(ENVIARACOMP, con.cn);
                com.UpdatedRowSource = UpdateRowSource.None;
                com.ExecuteNonQuery();
                con.cn.Close();

                #region Preparacion de Correo
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("mail.wurth.com.do");
                SmtpServer.Port = 25;
                SmtpServer.Credentials = new System.Net.NetworkCredential("servicioswurth@wurth.com.do", "Wservice01*");
                mail.From = new MailAddress("servicioswurth@wurth.com.do");

                mail.Subject = "Reporte de Acompañamiento con la o el  vendedor (a) " + DropDownList1.Text + " - " + Label2.Text;
                mail.Body = "Después de un Cordial Saludo \n\n Les informo que acabo de registrar el acompañamiento realizado en el día de hoy con mi vendedor@ " + DropDownList1.Text + " - " + Label2.Text + "\n\n Se despide de usted " + Session["idvendedor"].ToString() + "-" + Session["NombreCompleto"].ToString() + " \n\n Si asi lo desea puede ir al reporte y generar la información detallada del acompañamiento del día de hoy, solo colocando el número de vendedor (a), \n\n para ir presione el siguiente link http://wurthserver:20000/Reports/Pages/Report.aspx?ItemPath=%2fReportes_Aplicaciones%2fConsulta+Vendedores%2fCON_VEN_007_Reporte_Acompanamiento \n\n Favor no responder este mensaje ya que fue generado por sistema automático";
                mail.To.Add("aramirez@wurth.com.do");
                mail.To.Add("csuero@wurth.com.do");
                mail.CC.Add("nurena@wurth.com.do");
                mail.CC.Add("pgargallo@wurth.com.do");
                mail.CC.Add("fminaya@wurth.com.do");
                SmtpServer.Send(mail);
                #endregion

                var a = "alert(\'Acompanamiento envíado correctamente, gracias por usar nuestra plataforma\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                Cancelar_Click(sender, e);
                Response.Redirect("~/Analisis_Vendedor.aspx");
            }
        }
    }
    protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton4.Checked = false;
    }
    protected void RadioButton4_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton3.Checked = false;
    }
    protected void Guardar_Click(object sender, EventArgs e)
    {
        GuardarAcopanamiento();

        var g = "alert(\'Acompañamiento Guardado correctamente\');";
        ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", g, true);

        Cancelar_Click(sender, e);        
    }
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList4.Enabled = false;
    }
}