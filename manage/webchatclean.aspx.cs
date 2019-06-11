using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class manage_webchatclean : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();

        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE webchat SET flag = '是' WHERE receiveuser = '" + Session["yonghuming"].ToString() + "'";

            cmd.ExecuteNonQuery();

            conn.Close();
            conn.Dispose();
        }
        catch
        {
            conn.Close();
            conn.Dispose();
        }
        finally
        {
            conn.Close();
            conn.Dispose();

            Response.Redirect("frontpage.aspx");
        }
    }
}