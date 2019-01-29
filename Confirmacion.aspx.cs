using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Confirmacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            habilitarmenu();
            var a = "alert(\'Reposición de gastos enviada correctamente, a continuación podrá encontrar el número de reposición\');";
            ScriptManager.RegisterStartupScript(this, GetType(), "messageBox", a, true);

            Label1.Text = "Por favor guarde este número de reposición y escribalo en la (s) factura (s) que entregará a Recursos Humanos, El número de Reposición es " + Session["Condirmacion"].ToString();
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
}