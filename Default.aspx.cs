using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web;

public partial class _Default : System.Web.UI.Page
{
    Conexion con = new Conexion();
    SqlCommand com = new SqlCommand();
    SqlDataAdapter data = new SqlDataAdapter();
    DataTable table = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void Correo_TextChanged(object sender, EventArgs e)
    {
        if (Correo.Text != string.Empty)
        {
            if (Correo.Text.Length <= 2)
            {
                var a = "alert(\'El número de vendedor que ha escrito es muy corto, verifique e intente nuevamente\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
                Correo.Focus();
            }
            else if (ValirExistencia(Correo.Text) == false)
            {
                var a = "alert(\'El número de vendedor que ha insertado no existe en el sistema de consultas, favor contactar al administrador del sistema\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            }
            else
            {
                Contra.Focus();
            }
        }
    }
    public Boolean ValirExistencia(string Novendedor)
    {
        string vali = @"SELECT * FROM DESARROLLO.LISTCONT.LISTADECONTACTO WHERE NO_VENDEDOR='" + Novendedor + "' AND ESTADO = 0";
        com = new SqlCommand(vali, con.cn);
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
    protected void Ingresar_Click(object sender, EventArgs e)
    {
        if (Correo.Text == string.Empty || Contra.Text == string.Empty)
        {
            var a = "alert(\'Para poder accesar al sistema es necesario que escriba el número de vendedor como la clave\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            Correo.Focus();
        }
        else if (Correo.Text.Length <= 2)
        {
            var a = "alert(\'El número de vendedor que ha escrito es muy corto, verifique e intente nuevamente\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            Correo.Focus();
        }
        else if (Contra.Text.Length <= 2)
        {
            var a = "alert(\'La clave que ha colocado es es muy corta, verifique e intente nuevamente\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            Contra.Focus();
        }
        else
        {
            string BUCAR_VENDE = @"SELECT A.*, C.JEFAZONA,ISNULL(C.GRUPO_ADM,0) AS GRUPO_ADM, C.GRUPO,
                                ISNULL((SELECT MAX(R.COD_VENDEDOR) FROM Desarrollo.ListCont.VEND_REPORTE_VENTAS R WHERE R.GRUPO_ADM=C.GRUPO AND R.ESTADO=0 AND R.JEFAZONA=1),0) AS JEFA
                                FROM Desarrollo.ListCont.LISTADECONTACTO A INNER JOIN Desarrollo.ConVen.USUARIOS B ON A.NO_VENDEDOR = B.NOVENDEDOR
                                LEFT OUTER JOIN Desarrollo.ListCont.VEND_REPORTE_VENTAS C ON LTRIM(A.NO_VENDEDOR)=LTRIM(C.COD_VENDEDOR)
                                WHERE A.NO_VENDEDOR='" + Correo.Text + "' AND B.CLAVE='" + Contra.Text + "' AND A.ESTADO = 0";
            com = new SqlCommand(BUCAR_VENDE, con.cn);
            data = new SqlDataAdapter(com);
            table = new DataTable();
            data.Fill(table);
            if (table.Rows.Count > 0)
            {
                Session["NombreCompleto"] = table.Rows[0]["NOMBRE_APELLIDO"].ToString();
                Session["idvendedor"] = table.Rows[0]["NO_VENDEDOR"].ToString();
                Session["JEFAZONA"] = table.Rows[0]["JEFAZONA"].ToString();
                Session["NumeroGrupo"] = table.Rows[0]["GRUPO_ADM"].ToString();
                Session["NOJEFA"] = table.Rows[0]["JEFA"].ToString();

                string Insrt = @"Insert into desarrollo.ConVen.LOG_USUARIOS values('" + table.Rows[0]["NO_VENDEDOR"].ToString() + "',getdate())";
                con.cn.Open();
                com = new SqlCommand(Insrt, con.cn);
                com.UpdatedRowSource = UpdateRowSource.None;
                com.ExecuteNonQuery();
                con.cn.Close();
                Response.Redirect("~/Principal.aspx");
            }
            else
            {
                var a = "alert(\'El usuario con que desea ingresar no es valido, verifique o pongase en contacto con su administrador de sistemas\');";
                ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);
            }
        }
    }
    protected void Cancelar_Click(object sender, EventArgs e)
    {
        Correo.Text = string.Empty;
        Contra.Text = string.Empty;
        Correo.Focus();
    }   
}