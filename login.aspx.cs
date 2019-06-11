using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using DataCrypto;
using securitycheck;

public partial class login : System.Web.UI.Page
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
            Response.Redirect("alert.aspx");
        }

        string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();

        if (!IsPostBack)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT projectno,projectname FROM history WHERE username = '" + Session["yonghuming"].ToString() + "'";

                SqlDataReader dr = cmd.ExecuteReader();

                this.prono.DataSource = dr;

                this.prono.DataTextField = "projectname";
                this.prono.DataValueField = "projectno";

                this.prono.DataBind();
            }
            catch
            {
                conn.Close();
                conn.Dispose();
                this.prono.Text = "获取项目信息出错，请重新登录！";

            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
    protected void submit_Click(object sender, EventArgs e)
    {
        danger(this.prono.Text);
        danger(this.password.Text);

        string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "SELECT password,leader,money,name FROM project WHERE username = '" + this.prono.Text + "'";

        SqlDataReader dr = cmd.ExecuteReader();
        
        HashMethod hm = new HashMethod();

        if (dr.Read())
        {
            if (hm.Encrypto(this.password.Text) == dr[0].ToString().Trim())
            {
                Session["xiangmuhao"] = this.prono.Text;
                Session["guanliyuan"] = dr[1].ToString().Trim();
                Session["money"] = dr[2].ToString().Trim();
                Session["xiangmuming"] = dr[3].ToString().Trim();

                conn.Close();
                conn.Dispose();

                Response.Redirect("manage/frontpage.aspx");
            }
            else
            {
                conn.Close();
                conn.Dispose();

                ClientScript.RegisterStartupScript(GetType(), "","<script>alert(\"用户名或密码错误，请重新输入！\")</script>");
            }
        }
        else
        {
            conn.Close();
            conn.Dispose();

            ClientScript.RegisterStartupScript(GetType(), "","<script>alert(\"用户名或密码错误，请重新输入！\")</script>");
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
            ClientScript.RegisterStartupScript(GetType(), "","<script>alert(\"内容填写不完全，请重新填写！\")</script>");
        }
    }
}