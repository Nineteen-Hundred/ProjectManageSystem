using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using securitycheck;
using sendmail;

public partial class manage_temp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["yonghuming"] == null)
        {
            Response.Redirect("default.aspx");
        }

        security sc = new security();
        int xx = sc.flag(Session["yonghuming"].ToString(), Request.UserHostAddress.ToString());

        if (xx == 1)
        {
            Response.Redirect("../alert.aspx");
        }

        string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();

        try
        {
            string temp1 = Session["yonghuming"].ToString();
            string temp2 = Session["xingming"].ToString();
            string temp3 = Session["xiangmuhao"].ToString();
            string temp4 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = "SELECT * FROM pro" + temp3 + " WHERE username = '" + temp1 + "'";
            SqlDataReader dr = cmd.ExecuteReader();

            if (!dr.Read())
            {
                cmd.CommandText = "INSERT INTO applies VALUES('" + temp1 + "','" + temp2 + "','" + temp3 + "','" + temp4 + "')";

                dr.Close();
                cmd.ExecuteNonQuery();
            }
            else
            {
                dr.Close();
            }

            sendmailclass sd = new sendmailclass();
            cmd.CommandText = "SELECT email FROM main WHERE username='" + Session["guanliyuan"].ToString() + "'";
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                string str1 = dr[0].ToString().Trim();
                string str2 = "您管理的项目“" + Session["xiangmuming"].ToString() + "”有新成员加入！";
                string str3 = "成员消息";

                dr.Close();

                sd.sendmailfunction(str1, str2, str3);
            }
            else
            {
                Exception ex = new Exception();
                throw ex;
            }
        }
        catch
        {
            conn.Close();
            conn.Dispose();
            Response.Redirect("../default.aspx");
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
    }
}