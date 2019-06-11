using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using procode;

public partial class records : System.Web.UI.Page
{
    protected string result;

    protected void Page_Load(object sender, EventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();

        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT dateandtime,message FROM publics WHERE projectno='" + Session["xiangmuhao"].ToString() + "' ORDER BY dateandtime DESC";

            SqlDataReader dr = cmd.ExecuteReader();

            codemaker maker = new codemaker();

            while (dr.Read())
            {
                result = result + maker.records(dr[0].ToString().Trim(), dr[1].ToString().Trim());
            }
        }
        catch
        {
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
    }
}