using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class laert : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string yonghuming;
        string ipdizhi;

        string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();

        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            if (Session["yonghuming"] != null)
            {
                yonghuming = Session["yonghuming"].ToString();
                ipdizhi = "空";
            }
            else
            {
                yonghuming = "空";
                ipdizhi = Request.UserHostAddress.ToString();
            }

            cmd.CommandText = "INSERT INTO danger VALUES('" + ipdizhi + "','" + yonghuming + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            cmd.ExecuteNonQuery();

            if (Session["yonghuming"] != null)
            {
                cmd.CommandText = "SELECT COUNT(*) FROM danger WHERE username = '" + yonghuming + "'";
            }
            else
            {
                cmd.CommandText = "SELECT COUNT(*) FROM danger WHERE ipaddress = '" + ipdizhi + "'";
            }

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() && Convert.ToInt32(dr[0].ToString().Trim()) > 2)
            {
                this.tupian.ImageUrl = "image/close.jpg";
            }
            else
            {
                this.tupian.ImageUrl = "image/alert.jpg";
            }

            dr.Close();
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
        }
    }
}