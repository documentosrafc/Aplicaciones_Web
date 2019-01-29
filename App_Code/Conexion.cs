using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Conexion
/// </summary>
public class Conexion
{
	public Conexion()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public SqlConnection cn = new SqlConnection(@"Data Source=WURTHSERVER; INITIAL CATALOG=DESARROLLO; USER ID=Reportes; password= Reportes");

   
    
    
    //public bool log(string usuario, string contrasena)
    //{
    //    if (usuario != "admin")
    //    {
    //        return false;
    //    }
    //    else if (contrasena != "admin")
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        return true;
    //    }
    //}
}