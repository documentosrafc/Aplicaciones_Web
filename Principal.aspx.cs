using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.Common;

public partial class Principal : System.Web.UI.Page
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
            cargardiasdecartera();
            this.Form.Attributes.Add("autocomplete", "off");

            BuscarListadoClientes(Session["idvendedor"].ToString().Trim());
            int dias = DateTime.Today.Day - 1;
            FECHAINICIAL.Text = DateTime.Today.AddDays(-dias).ToShortDateString();
            FECHAFINAL.Text = DateTime.Today.ToShortDateString();
            Label10.Text = Session["NombreCompleto"].ToString(); 
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
    public void cargardiasdecartera()
    {
        string carterasdias = "Desarrollo.conven.diascartera";
        com = new SqlCommand(carterasdias, con.cn);
        com.CommandType = CommandType.StoredProcedure;        
        com.Parameters.Add("@VENDEDOR", SqlDbType.VarChar, 10).Value = Session["idvendedor"].ToString().Trim();
        data = new SqlDataAdapter(com);
        table = new DataTable();
        data.Fill(table);

        if (table.Rows.Count > 0)
        {
            Label16.Text = string.Format("{0:###,###,###,##0}", float.Parse(table.Rows[0]["Dias_Cartera"].ToString())) + " Días ";
        }        
    }
    #region Facturacion Clientes        
    public void busca(string factura, string Cliente, string pedido)
    {       
        string buscardetalle = @"Desarrollo.ConVen.BuscarDetalleFacturacion";
        com = new SqlCommand(buscardetalle, con.cn);
        com.CommandType = CommandType.StoredProcedure;
        com.Parameters.Add("@FACTURA", SqlDbType.VarChar, 10).Value = factura;
        com.Parameters.Add("@PEDIDO", SqlDbType.VarChar, 10).Value = pedido;
        com.Parameters.Add("@VENDEDOR", SqlDbType.VarChar, 10).Value = Session["idvendedor"].ToString();
        data = new SqlDataAdapter(com);
        table = new DataTable();
        data.Fill(table);

        GridView2.DataSource = new DataView(table);
        GridView2.DataBind();
    }
    protected void Buscar_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1500);
        if (DatosBusca.Text == string.Empty)
        {
            var a = "alert(\'Para poder buscar una historial de facturas para algún cliente es necesario que inserte la información.\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
        }
        else if (DropDownList2.Text == "Cod. Cliente")
        {
            string BUSCARFACTURAS = "Desarrollo.ConVen.FacturasClientes";
            com = new SqlCommand(BUSCARFACTURAS, con.cn);
            com.Parameters.Add("@CLIENTE", SqlDbType.VarChar, 20).Value = DatosBusca.Text;
            com.Parameters.Add("@VENDEDOR", SqlDbType.VarChar, 10).Value = Session["idvendedor"].ToString(); 
            com.CommandType = CommandType.StoredProcedure;
            data = new SqlDataAdapter(com);
            table = new DataTable();
            data.Fill(table);

            if (table.Rows.Count > 0)
            {
                Session["codcliente"] = table.Rows[0]["CLIENTE"].ToString().Trim();
                GridView1.DataSource = new DataView(table);
                GridView1.DataBind();

                GridView2.DataSource = null;
                GridView2.DataBind();
            }
            else
            {
                var a = "alert(\'Lo sentimos, pero no hemos encontrado el cliente que busca o el mismo no pertenece a su cartera\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            }
        }
        else if (DropDownList2.Text == "Nombre Cliente")
        {           
            string buscsarinforcliente = "Desarrollo.ConVen.Info_Clientes";
            com = new SqlCommand(buscsarinforcliente, con.cn);
            com.Parameters.Add("@CLIENTE", SqlDbType.VarChar, 20).Value = DatosBusca.Text;            
            com.Parameters.Add("@VENDEDOR", SqlDbType.VarChar, 10).Value = Session["idvendedor"].ToString(); 
            com.CommandType = CommandType.StoredProcedure;
            data = new SqlDataAdapter(com);
            table = new DataTable();
            data.Fill(table);

            if (table.Rows.Count > 0)
            {
                GridView3.DataSource = new DataView(table);
                GridView3.DataBind();
                GridView3.Visible = true;
                MyDiv.Visible = false;
            }
            else
            {
                var a = "alert(\'Lo sentimos, pero no hemos encontrado el cliente que busca con la información suministrada, verifique e intente nuevamente\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            }
        }
        else if (DropDownList2.Text == "RNC / Cedula")
        {
            string BUSCARFACTURAS = "Desarrollo.ConVen.FacturasClientes";
            com = new SqlCommand(BUSCARFACTURAS, con.cn);
            com.Parameters.Add("@CLIENTE", SqlDbType.VarChar, 20).Value = DatosBusca.Text;
            com.Parameters.Add("@BUSCAR", SqlDbType.VarChar, 20).Value = "1";
            com.Parameters.Add("@VENDEDOR", SqlDbType.VarChar, 10).Value = Session["idvendedor"].ToString();
            com.CommandType = CommandType.StoredProcedure;
            data = new SqlDataAdapter(com);
            table = new DataTable();
            data.Fill(table);

            if (table.Rows.Count > 0)
            {
                Session["codcliente"] = table.Rows[0]["CLIENTE"].ToString().Trim();
                GridView1.DataSource = new DataView(table);
                GridView1.DataBind();

                GridView2.DataSource = null;
                GridView2.DataBind();
            }
            else
            {
                var a = "alert(\'Lo sentimos, pero no hemos encontrado historia de facturación con el RNC que ha escrito\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            }
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        System.Threading.Thread.Sleep(1500);
        if (Convert.ToString(e.CommandArgument) != "Next" && Convert.ToString(e.CommandArgument) != "Last" && Convert.ToString(e.CommandArgument) != "First" && Convert.ToString(e.CommandArgument) != "Prev")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow ro = GridView1.Rows[index];

            if (e.CommandName == "Detalle")
            {
                busca(ro.Cells[3].Text.Trim(), Session["codcliente"].ToString().Trim(), ro.Cells[2].Text.Trim());
            }

            if (e.CommandName == "Reporte")
            {
                Session["cliente"] = DatosBusca.Text;
                Session["factura"] = ro.Cells[3].Text.Trim();
                Response.Redirect("~/ReporteFactura.aspx");
            }
        }
    }
    protected void CancelarDatas_Click(object sender, EventArgs e)
    {
        Session["codcliente"] = null;
        DatosBusca.Text = string.Empty;
        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView2.DataSource = null;
        GridView2.DataBind();
    }
    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        System.Threading.Thread.Sleep(1500);

        if (Convert.ToString(e.CommandArgument) != "Next" && Convert.ToString(e.CommandArgument) != "Last" && Convert.ToString(e.CommandArgument) != "First" && Convert.ToString(e.CommandArgument) != "Prev")
        {
            if (e.CommandName == "Seleccionar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow ro = GridView3.Rows[index];

                DatosBusca.Text = ro.Cells[1].Text.Trim();
                DropDownList2.Text = "Cod. Cliente";

                GridView3.DataSource = null;
                GridView3.DataBind();
                GridView3.Visible = false;
                MyDiv.Visible = true;

                Buscar_Click(sender, e);
            }
        }
    }
    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        System.Threading.Thread.Sleep(1500);

        GridView row = (GridView)sender;
        row.PageIndex = e.NewPageIndex;

        Buscar_Click(sender, e);

        GridView3.DataSource = new DataView(table);
        GridView3.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        System.Threading.Thread.Sleep(1500);

        GridView row = (GridView)sender;
        row.PageIndex = e.NewPageIndex;

        Buscar_Click(sender, e);

        GridView1.DataSource = new DataView(table);
        GridView1.DataBind();
    }
    #endregion
    #region Listado de Clientes por Vendedor
    public void BuscarListadoClientes(string novendedor)
    {
        string buscar = @"SELECT LTRIM(A.ACCOUNTNUM) AS COD, LTRIM(A.NAME) AS NOMBRE,A.VATNUM AS RNC,A.PHONE AS TELEFONO, A.EMAIL,
                        CONVERT(VARCHAR,CAST(ISNULL((SELECT SUM(C.SALESBALANCE*C.EXCHRATE)/100 FROM [10.0.0.54].AX50DOM_LIVE.DBO.CUSTINVOICEJOUR C WHERE A.ACCOUNTNUM=C.INVOICEACCOUNT AND YEAR(C.INVOICEDATE)=YEAR(GETDATE())-1),0) AS MONEY),1) AS AÑO_ANTERIOR,
                        CONVERT(VARCHAR,CAST(ISNULL((SELECT SUM(C.SALESBALANCE*C.EXCHRATE)/100 FROM [10.0.0.54].AX50DOM_LIVE.DBO.CUSTINVOICEJOUR C WHERE A.ACCOUNTNUM=C.INVOICEACCOUNT AND YEAR(C.INVOICEDATE)=YEAR(GETDATE())),0) AS MONEY),1) AS AÑO_ACTUAL
                        ,ISNULL(CONVERT(VARCHAR, CAST((SELECT SUM(R.AMOUNTCUR)FROM [10.0.0.54].AX50DOM_LIVE.DBO.CUSTTRANSOPEN R WHERE R.ACCOUNTNUM=A.ACCOUNTNUM) AS MONEY),1),0.00) AS SALDO
                        FROM [10.0.0.54].AX50DOM_LIVE.DBO.CUSTTABLE A INNER JOIN [10.0.0.54].AX50DOM_LIVE.DBO.EMPLTABLE B ON A.WPMSALESUNITID=B.EMPLID                        
                        WHERE LTRIM(A.WPMSALESUNITID) NOT IN('058','400') AND LTRIM(A.WPMSALESUNITID)='" + Session["idvendedor"].ToString().Trim() + "' ORDER BY A.ACCOUNTNUM";
        com = new SqlCommand(buscar, con.cn);
        data = new SqlDataAdapter(com);
        table = new DataTable();
        data.Fill(table);
        Label8.Text ="Total de Cartera de Clientes "+ Convert.ToString(table.Rows.Count);

        GridView4.DataSource = new DataView(table);
        GridView4.DataBind();
    }
    protected void GridView4_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView row = (GridView)sender;
        row.PageIndex = e.NewPageIndex;

        BuscarListadoClientes(Session["idvendedor"].ToString().Trim());
    }
    protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (Convert.ToString(e.CommandArgument) != "Next" && Convert.ToString(e.CommandArgument) != "Last" && Convert.ToString(e.CommandArgument) != "First" && Convert.ToString(e.CommandArgument) != "Prev")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow ro = GridView4.Rows[index];

            if (e.CommandName == "Estado")
            {
                if (ro.Cells[9].Text == "0.00" || ro.Cells[9].Text == "&nbsp;")
                {
                    var a = "alert(\'Este cliente no cuenta con deudas con la empresa, por dicha razón su estado no puede ser generado.\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                }
                else
                {
                    Session["cliente"] = ro.Cells[2].Text;
                    Session["reporteamostrar"] = "/Reportes_Aplicaciones/Sistema Call Center/CALL_CEN_006 Estado de Cuenta Cliente";
                    Response.Redirect("~/SaldoClientes.aspx");
                }
            }
            else if (e.CommandName == "Estado_RNC")
            {
                if (ro.Cells[9].Text == "0.00" || ro.Cells[9].Text == "&nbsp;")
                {
                    var a = "alert(\'Este cliente no cuenta con deudas con la empresa, por dicha razón su estado no puede ser generado.\');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                }
                else
                {
                    Session["cliente"] = ro.Cells[4].Text;
                    Session["reporteamostrar"] = "/Finanzas/Cuentas por Cobrar/FIN_CXC_009_Estado_de_Cuenta_Cliente_por_RNC";
                    Response.Redirect("~/SaldoClientes.aspx");
                }
            }
        }
    }
    #endregion   
    #region Saldos Clientes    
    protected void Busc_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1500);

        if (CodCliente.Text == string.Empty)
        {
            var a = "alert(\'Para poder buscar el saldo de algún cliente es necesario que escriba el número del cliente\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
        }
        else
        {
            GridView6.DataSource = null;
            GridView6.DataBind();

            string Buscarsaldo = @"	SELECT LTRIM(C.ACCOUNTNUM) AS ID,LTRIM(C.NAME) AS NOMBRE, C.VATNUM AS RNC,ISNULL(D.INVOICEID,RIGHT(LEFT(B.TXT,18),6)) AS FACTURA, 
		                            CONVERT(VARCHAR,A.TRANSDATE, 101) AS FECHA,	CONVERT(VARCHAR,CAST(A.AMOUNTCUR AS MONEY),1) AS MONTO_FAT,
		                            CONVERT(VARCHAR,CAST(A.AMOUNTMST AS MONEY),1) AS SALDO,	LTRIM(C.ADDRESS) AS DIRECCION, C.PHONE AS TELEFONO,
		                            LTRIM(C.WPMSALESUNITID) AS VEND

		                            FROM [10.0.0.54].AX50DOM_LIVE.DBO.CUSTTRANSOPEN A 
		                            INNER JOIN [10.0.0.54].AX50DOM_LIVE.DBO.CUSTTRANS B ON A.REFRECID=B.RECID
		                            INNER JOIN [10.0.0.54].AX50DOM_LIVE.DBO.CUSTTABLE C ON A.ACCOUNTNUM=C.ACCOUNTNUM 
		                            LEFT JOIN [10.0.0.54].AX50DOM_LIVE.DBO.CUSTINVOICEJOUR D ON B.ACCOUNTNUM=D.INVOICEACCOUNT AND B.INVOICE=D.INVOICEID
		                            WHERE LTRIM(A.ACCOUNTNUM)='" + CodCliente.Text + "' AND A.AMOUNTMST > 1  -- UNION ";

//            string buscarsaldo1 = @"SELECT LTRIM(B.ACCOUNTNUM) AS ID,LTRIM(G.NAME) AS NOMBRE, G.VATNUM AS RNC,B.TXT AS FACTURA,
//                                CONVERT(VARCHAR,B.transdate,101) AS FECHA,CONVERT(VARCHAR,CAST(B.AMOUNTCUR AS MONEY),1) AS MONTO_FAT,
//                                CONVERT(VARCHAR,CAST(B.AMOUNTMST AS MONEY),1) AS SALDO, LTRIM(G.ADDRESS) AS DIRECCION, G.PHONE AS TELEFONO,
//                                 G.WPMSALESUNITID AS VEND
//                                FROM [10.0.0.54].AX50DOM_LIVE.DBO.CUSTTABLE G 
//                                INNER JOIN [10.0.0.54].AX50DOM_LIVE.DBO.CUSTTRANS B ON G.ACCOUNTNUM = B.ACCOUNTNUM
//                                INNER JOIN [10.0.0.54].AX50DOM_LIVE.DBO.EMPLTABLE C ON C.EMPLID = G.WPMSALESUNITID 
//                                WHERE LTRIM(G.ACCOUNTNUM)='" + CodCliente.Text + "' AND B.SETTLEAMOUNTCUR = 0 AND B.TXT<>''";

            data = new SqlDataAdapter(Buscarsaldo /*+ buscarsaldo1*/, con.cn);
            table = new DataTable();
            data.Fill(table);

            if (table.Rows.Count > 0)
            {
                if (table.Rows[0]["VEND"].ToString() == Session["idvendedor"].ToString())
                {
                    GridView6.DataSource = new DataView(table);
                    GridView6.DataBind();
                    LinkButton1.Enabled = true;

                    float monto = 0;
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        monto = monto + float.Parse(table.Rows[i]["SALDO"].ToString());
                    }
                    LinkButton1.Text = string.Format("{0:#,##0.00}", monto);
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
    protected void GridView6_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        System.Threading.Thread.Sleep(1500);

        GridView row = (GridView)sender;
        row.PageIndex = e.NewPageIndex;

        Busc_Click(sender, e);

        GridView6.DataSource = new DataView(table);
        GridView6.DataBind();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (LinkButton1.Text != "0.00")
        {
            Session["cliente"] = CodCliente.Text;
            Session["reporteamostrar"] = "/Reportes_Aplicaciones/Sistema Call Center/CALL_CEN_006 Estado de Cuenta Cliente";
            Response.Redirect("~/SaldoClientes.aspx");
        }
    }
    #endregion
    #region Ventas de Vendedores        
    public void CargarVentasCobros(DateTime FechaFinicio, DateTime fechahasta,int diaslaborables, int diastrabajados)
{
    string cargaventas = "Desarrollo.ConVen.Facturacionvendedores";
    com = new SqlCommand(cargaventas, con.cn);
    com.Parameters.Add("@FECHAINICIAL", SqlDbType.DateTime).Value = FechaFinicio;
    com.Parameters.Add("@FECHAFINAL", SqlDbType.DateTime).Value = fechahasta;
    com.Parameters.Add("@DIASLABORABLES", SqlDbType.Int).Value = diaslaborables;
    com.Parameters.Add("@DIASTRABAJADOS", SqlDbType.Int).Value = diastrabajados;
    com.Parameters.Add("@VENDEDOR", SqlDbType.VarChar, 10).Value = Session["idvendedor"].ToString();
    com.CommandType = CommandType.StoredProcedure;
    data = new SqlDataAdapter(com);
    table = new DataTable();
    data.Fill(table);

    GridView5.DataSource = new DataView(table);
    GridView5.DataBind();
}
    protected void BuscarInformacionVentas_Click(object sender, EventArgs e)
{
    System.Threading.Thread.Sleep(2500);

    if (FECHAINICIAL.Text == string.Empty || FECHAFINAL.Text == string.Empty || DIASLABORABLES.Text == string.Empty || DIASLABORADOS.Text == string.Empty)
    {
        var a = "alert(\'Para poder buscar sus ventas y devoluciones es necesario que llene todos los campos \');";
        ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
    }
    else
    {
        int compa = DateTime.Compare(DateTime.Parse(FECHAINICIAL.Text), DateTime.Parse(FECHAFINAL.Text));
        if (compa == 1)
        {
            var a = "alert(\'La fecha final es menor a la fecha inicial, favor verificar e intente nuevamente\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
        }
        else
        {
            CargarVentasCobros(DateTime.Parse(FECHAINICIAL.Text), DateTime.Parse(FECHAFINAL.Text), int.Parse(DIASLABORABLES.Text), int.Parse(DIASLABORADOS.Text));
        }
    }
}
    #endregion
    #region Informacion detalla de ventas      
    protected void Busca_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1500);
        if (finicial.Text == string.Empty || ffinal.Text == string.Empty)
        {
            var a = "alert(\'Para poder buscar el detalle de sus ventas y devoluciones es necesario que llene todos los campos\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
        }
        else
        {
            int compa = DateTime.Compare(DateTime.Parse(finicial.Text), DateTime.Parse(ffinal.Text));
            if (compa == 1)
            {
                var a = "alert(\'La fecha final es menor a la fecha inicial, favor verificar e intente nuevamente\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            }
            else
            {
                string buscarfacturacion = @"Desarrollo.ConVen.FacturasGeneradasporVendedor";
                com = new SqlCommand(buscarfacturacion, con.cn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@FECHAINICIAL", SqlDbType.DateTime).Value = DateTime.Parse(finicial.Text);
                com.Parameters.Add("@FECHAFINAL", SqlDbType.DateTime).Value = DateTime.Parse(ffinal.Text);
                com.Parameters.Add("@VENDEDOR", SqlDbType.VarChar, 10).Value = Session["idvendedor"].ToString();
                data = new SqlDataAdapter(com);
                table = new DataTable();
                data.Fill(table);

                GridView7.DataSource = new DataView(table);
                GridView7.DataBind();
            }
        }
    }
    #endregion      
    #region Existencia de Clientes
    protected void Bus_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1500);
        if (RNC_Cliente.Text == string.Empty)
        {
            var a = "alert(\'Para poder consultar la existencia del cliente es necesario que inserte la información suministrada por él.\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
        }
        else if (DropDownList1.Text == "Telefono")
        {
            string BUSCARFACTURAS = @"SELECT LTRIM(A.ACCOUNTNUM) AS COD_CLIENTE, A.NAME AS NOMBRE_CLIENTE, A.VATNUM AS RNC, LTRIM(A.PHONE) AS TELEFONO, 
                                    LTRIM(A.EMAIL) AS CORREO, LTRIM(A.WPMSALESUNITID) AS COD_VEND, C.NAME AS NOMBRE_VENDEDOR 
                                    FROM [10.0.0.54].AX50DOM_LIVE.DBO.CUSTTABLE A INNER JOIN [10.0.0.54].AX50DOM_LIVE.DBO.EMPLTABLE B ON A.WPMSALESUNITID = B.EMPLID 
                                    INNER JOIN [10.0.0.54].AX50DOM_LIVE.DBO.DIRPARTYTABLE C ON B.PARTYID=C.PARTYID
                                    WHERE REPLACE(REPLACE(A.PHONE,' ',''),'-','') LIKE '%" + RNC_Cliente.Text + "%'";
            com = new SqlCommand(BUSCARFACTURAS, con.cn);
            data = new SqlDataAdapter(com);
            table = new DataTable();
            data.Fill(table);

            if (table.Rows.Count > 0)
            {
                GridView8.DataSource = new DataView(table);
                GridView8.DataBind();
            }
            else
            {
                var a = "alert(\'Lo sentimos, pero no hemos encontrado el cliente que busca o el mismo no pertenece a su cartera\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            }
        }
        else if (DropDownList1.Text == "Nombre")
        {
            string buscsarinforcliente = @"SELECT LTRIM(A.ACCOUNTNUM) AS COD_CLIENTE, A.NAME AS NOMBRE_CLIENTE, A.VATNUM AS RNC, LTRIM(A.PHONE) AS TELEFONO, 
                                        LTRIM(A.EMAIL) AS CORREO, LTRIM(A.WPMSALESUNITID) AS COD_VEND, C.NAME AS NOMBRE_VENDEDOR FROM [10.0.0.54].AX50DOM_LIVE.DBO.CUSTTABLE A 
                                        INNER JOIN [10.0.0.54].AX50DOM_LIVE.DBO.EMPLTABLE B ON A.WPMSALESUNITID = B.EMPLID INNER JOIN [10.0.0.54].AX50DOM_LIVE.DBO.DIRPARTYTABLE C ON B.PARTYID=C.PARTYID
                                         WHERE LTRIM(A.NAME) LIKE '%" + RNC_Cliente.Text + "%'";
            com = new SqlCommand(buscsarinforcliente, con.cn);
            data = new SqlDataAdapter(com);
            table = new DataTable();
            data.Fill(table);

            if (table.Rows.Count > 0)
            {
                GridView8.DataSource = new DataView(table);
                GridView8.DataBind();
            }
            else
            {
                var a = "alert(\'Lo sentimos, pero no hemos encontrado el cliente que busca con la información suministrada, verifique e intente nuevamente\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            }
        }
        else if (DropDownList1.Text == "RNC o Cedula")
        {
            string BUSCARFACTURAS = @"SELECT LTRIM(A.ACCOUNTNUM) AS COD_CLIENTE, A.NAME AS NOMBRE_CLIENTE, A.VATNUM AS RNC,
                                    LTRIM(A.PHONE) AS TELEFONO, LTRIM(A.EMAIL) AS CORREO, LTRIM(A.WPMSALESUNITID) AS COD_VEND, C.NAME AS NOMBRE_VENDEDOR
                                    FROM [10.0.0.54].AX50DOM_LIVE.DBO.CUSTTABLE A INNER JOIN [10.0.0.54].AX50DOM_LIVE.DBO.EMPLTABLE B ON A.WPMSALESUNITID = B.EMPLID
                                    INNER JOIN [10.0.0.54].AX50DOM_LIVE.DBO.DIRPARTYTABLE C ON B.PARTYID=C.PARTYID
                                    WHERE REPLACE(REPLACE(LTRIM(A.VATNUM),' ',''),'-','') LIKE '%" + RNC_Cliente.Text + "%'";
            com = new SqlCommand(BUSCARFACTURAS, con.cn);
            data = new SqlDataAdapter(com);
            table = new DataTable();
            data.Fill(table);

            if (table.Rows.Count > 0)
            {
                GridView8.DataSource = new DataView(table);
                GridView8.DataBind();
            }
            else
            {
                var a = "alert(\'Lo sentimos, pero no hemos encontrado historia de facturación con el RNC que ha escrito\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            }
        }
    }
    protected void GridView8_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView row = (GridView)sender;
        row.PageIndex = e.NewPageIndex;
        Bus_Click(sender, e);

        GridView3.DataSource = new DataView(table);
        GridView3.DataBind();
    }
    #endregion
}