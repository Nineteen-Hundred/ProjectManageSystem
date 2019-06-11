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
using sendmail;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id;
        id = Request.Browser.Id.ToString();

        if (id.IndexOf("ie")>-1)
        {
            
        }
        security sc = new security();
        int xx = sc.flag("临时参数", Request.UserHostAddress.ToString());

        if (xx == 1)
        {
            Response.Redirect("alert.aspx");
        }

        string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();

        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM server";

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Session["gongsi"] = dr[0].ToString().Trim();
                Session["youxiang"] = dr[1].ToString().Trim();
                Session["guanliyuan"] = dr[2].ToString().Trim();
            }
            else
            {
                Session["gongsi"] = "";
                Session["youxiang"] = "";
                Session["guanliyuan"] = "";
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
		
		ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请注意：\\n1. 网站目前位于阿里云服务器，所有文件处于开放状态，请在上传前自行加密。\\n2. "
		+ "由于带宽限制，首次加载带有图片的页面耗时较长，请耐心等待图片加载完成，后续加载将不再有延迟。\\n3. 网站重新开放了邮件通知服务，为了保证您及时接收相关消息，"
		+ "请检查垃圾邮件设置（特别是西电学生邮箱），防止邮件被垃圾邮件网关拦截。\\n4."
		+ "  由于带宽限制，上传大文件会耗时较长，请耐心等待。\\n5. 为了加快校内同学访问速度，站内较大的图片资源均放置在校内服务器，外网或手机访问时将无法加载背景图片"
		+ "，但不影响系统的正常使用。\\n6. 为精简服务器文件，2017年9月2日前的文件不再提供下载服务，如需相关文件资料，请自行向管理员申请。\\n7. 网站迁移至阿里云"
		+ "服务器后，可能受到服务器停机维护、断电等情况影响，对此我们会提前通知，如果发现网站无法访问，也请及时通知管理员进行维护。\")</script>");
    }
    protected void submit_Click(object sender, EventArgs e)
    {
        danger(this.username.Text);
        danger(this.password.Text);

        HashMethod hm = new HashMethod();
        
        string yonghuming;
        string mima;

        yonghuming = this.username.Text;
        mima = hm.Encrypto(this.password.Text);

        string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();

        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT password,name,email,company,gender,idcard FROM main WHERE username = '" + yonghuming + "'";

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                if (dr[0].ToString().Trim() == mima)
                {
                    Session["yonghuming"] = this.username.Text;
                    Session["xingming"] = dr[1].ToString().Trim();
                    Session["emaildizhi"] = dr[2].ToString().Trim();
                    Session["gongsi"] = dr[3].ToString().Trim();
                    Session["xingbie"] = dr[4].ToString().Trim();
                    Session["shenfenzhenghao"] = dr[5].ToString().Trim();

                    conn.Close();
                    conn.Dispose();

                    Response.Redirect("ui.aspx");
                }
                else
                {
                    conn.Close();
                    conn.Dispose();

                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"您输入的信息有误，请重新输入\")</script>");
                }

                dr.Close();
            }
            else
            {
                dr.Close();
                conn.Close();
                conn.Dispose();

                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"您输入的信息有误，请重新输入\")</script>");
            }
        }
        catch
        {
            conn.Close();
            conn.Dispose();
        }
        finally
        {
        }
    }
	
	protected void register_Click(object sender, EventArgs e)
	{
		Response.Redirect("register.aspx");
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
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"内容填写不完全，请重新填写！\")</script>");
        }
    }
}