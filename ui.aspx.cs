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
using ipchaxun;
using DataCrypto;

public partial class ui1 : System.Web.UI.Page
{
    public string code1;
    public string code2;
    public string code3;
    public string code4;
    public string code5;
    public string code6;
    public string code7;

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
            Response.Redirect("alert.aspx");
        }

        string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();

        try {
            string ipdizhi = Request.UserHostAddress.ToString();

            IPSearch ips = new IPSearch();

            IPSearch.IPLocation wulidizhi;

            if (ipdizhi != "::1")
            {
                wulidizhi = ips.GetIPLocation(ipdizhi);
            }
            else
            {
                ipdizhi = "127.0.0.1";
                wulidizhi.country = "测试服务器";
                wulidizhi.area = "内部人员";
            }

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO userrecords VALUES('" + ipdizhi + "','" + wulidizhi.country + wulidizhi.area
               + "','" + Session["yonghuming"].ToString() + "','" + Session["xingming"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "SELECT * FROM main WHERE username = '" + Session["yonghuming"].ToString() + "'";
            SqlDataReader dr = cmd.ExecuteReader();

            code1 = "<img style=\"border-radius:8px;margin-top:14px;border:4px solid White;width:170px;height:170px\" src=\"pictures/" + Session["yonghuming"].ToString() + ".jpg\" />";

            dr.Read();
            code2 = dr[2].ToString().Trim();
            code3 = dr[3].ToString().Trim();
            code4 = dr[4].ToString().Trim();
            code5 = dr[5].ToString().Trim();
            code6 = dr[6].ToString().Trim();
            code7 = dr[7].ToString().Trim();

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

    protected string jilu()
    {
        string temp = "";
        int i = 0;

        string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "SELECT dateandtime,ipaddress,wulidizhi FROM userrecords WHERE username = '" + Session["yonghuming"].ToString() + "' ORDER BY dateandtime DESC";

        SqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read() && i<=6)
        {
            temp = temp + "<a style=\"margin-right:100px\">" + dr[0].ToString().Trim() + "</a><a style=\"margin-right:100px\">" + dr[1].ToString().Trim() + "</a>" + dr[2].ToString().Trim() + "<br/>";
            i++;
        }

        conn.Close();
        conn.Dispose();

        return temp;
    }

    protected string lishi()
    {
        string code = "";

        string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();

        try {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT projectname FROM history WHERE username='" + Session["yonghuming"].ToString() + "'";

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                code = code + "<a>" + dr[0].ToString().Trim() + "</a></br>";
            }
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

        return code;
    }
    protected void submit_Click(object sender, EventArgs e)
    {
        danger(this.username.Text);
        danger(this.password.Text);

        string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();

        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT password,leader,money,name FROM project WHERE username = '" + this.username.Text + "'";

            SqlDataReader dr = cmd.ExecuteReader();

            HashMethod hm = new HashMethod();

            if (dr.Read())
            {
                if (hm.Encrypto(this.password.Text) == dr[0].ToString().Trim())
                {
                    Session["xiangmuhao"] = this.username.Text;
                    Session["guanliyuan"] = dr[1].ToString().Trim();
                    Session["money"] = dr[2].ToString().Trim();
                    Session["xiangmuming"] = dr[3].ToString().Trim();

                    conn.Close();
                    conn.Dispose();

                    Response.Redirect("manage/temp.aspx");
                }
                else
                {
                    conn.Close();
                    conn.Dispose();

                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('用户名或密码错误！')</script>");
                }
            }
            else
            {
                conn.Close();
                conn.Dispose();

                ClientScript.RegisterStartupScript(GetType(),"","<script>alert('用户名或密码错误！')</script>");
            }
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

    protected void danger(string canshu)
    {
        security sc = new security();
        if (sc.CheckBadStr(canshu) == 1)
        {
            Response.Redirect("alert.aspx");
        }

        if (sc.CheckBadStr(canshu) == 2)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('内容填写不完全！')</script>");
        }
    }
}
