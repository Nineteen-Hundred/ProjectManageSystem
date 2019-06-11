using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class manager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void submit_Click(object sender, EventArgs e)
    {
        if (this.username.Text != "" && this.password.Text != "")
        {
            String connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT password FROM guanliyuan WHERE username='" + this.username.Text + "'";

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read() && dr[0].ToString() == this.password.ToString())
            {
                dr.Close();
                conn.Close();
                conn.Dispose();

                Response.Redirect("platform/tongji.aspx");
            }
            else
            {
                dr.Close();
                conn.Close();
                conn.Dispose();

                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"用户名或密码错误，请重试！\")</script>");
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"用户名和密码不能为空！\")</script>");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
}