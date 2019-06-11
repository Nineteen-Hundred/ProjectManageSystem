using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using suoluetu;
using System.IO;
using DataCrypto;
using System.Data;
using securitycheck;
using System.Configuration;
using System.Data.SqlClient;
using sendmail;

public partial class apply : System.Web.UI.Page
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
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT COUNT(*) FROM project";

            SqlDataReader dr = cmd.ExecuteReader();

            int i;
            if (dr.Read())
            {
                i = Convert.ToInt32(dr[0].ToString().Trim());
            }
            else
            {
                i = 0;
            }

            i++;

            this.prono.Text = "ISN" + i.ToString("0000");

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

    protected void submit_Click(object sender, EventArgs e)
    {
        if (this.password.Text != this.ensure.Text)
        {
            Response.Write("<script>alert(\"两次密码不一致！\")</script>");
        }
        else
        {

            if (this.proname.Text != "" && this.promoney.Text != "" && this.intro.Text != "" && this.password.Text != "" && this.ensure.Text != "")
            {
                HashMethod hm = new HashMethod();

                string temp1 = this.prono.Text;
                string temp2 = hm.Encrypto(this.password.Text);
                string temp3 = this.proname.Text;
                string temp4 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string temp5 = this.promoney.Text;
                string temp6 = Session["yonghuming"].ToString();
                string temp7 = this.intro.Text;

                string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                // 填充项目汇总表
                cmd.CommandText = "INSERT INTO project VALUES('" + temp1 + "','" + temp2 + "','" + temp3 + "','" + temp4 + "','" + temp5
                     + "','" + temp6 + "','" + temp7 + "','" + datepicker.Text + "')";

                cmd.ExecuteNonQuery();

                // 填充个人历史表
                cmd.CommandText = "INSERT INTO history VALUES('" + Session["yonghuming"].ToString() + "','" + this.prono.Text + "','" + this.proname.Text + "')";

                cmd.ExecuteNonQuery();

                // 创建项目表并赋初值
                cmd.CommandText = "CREATE TABLE pro" + this.prono.Text + " (username nvarchar(50),name nvarchar(50),idcard nvarchar(50),gender nvarchar(50),company nvarchar(50),email nvarchar(50),money float,spent float)";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO pro" + this.prono.Text + " VALUES('" + Session["yonghuming"].ToString() + "','" + Session["xingming"].ToString() + "','" + Session["shenfenzhenghao"].ToString()
                    + "','" + Session["xingbie"].ToString() + "','" + Session["gongsi"].ToString() + "','" + Session["emaildizhi"].ToString() + "','" + "0','0')";
                cmd.ExecuteNonQuery();

                // 填充公告栏表
                cmd.CommandText = "INSERT INTO publics VALUES('" + this.prono.Text + "','" + "暂无公告内容。','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                cmd.ExecuteNonQuery();

                // 插入演示视频记录
                cmd.CommandText = "INSERT INTO files VALUES('help.exe','" + this.prono.Text + "','其它','科研项目管理系统的演示视频，可以帮助用户快速学习使用该系统。','System','是','none','System','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                cmd.ExecuteNonQuery();

                // 插入软件截图记录
                for(int i=1;i<=3;i++)
                {
                    cmd.CommandText = "INSERT INTO gallery VALUES('管理平台运行截图','平台截图','" + this.prono.Text + "','platform" + i.ToString() + ".jpg')";
                    cmd.ExecuteNonQuery();
                }

                cmd.CommandText = "INSERT INTO gallery VALUES('控制中心截图','控制中心','" + this.prono.Text + "','control.jpg')";
                cmd.ExecuteNonQuery();

                sendmailclass sm = new sendmailclass();

                string ttemp1 = Session["emaildizhi"].ToString();
                string ttemp2 = "您已创建了一个名为“" + this.proname.Text + "”的项目，项目账号" + this.prono.Text + "，如果你你收到此条信息，表示申请过程已成功。如果并非您本人操作，请及时向平台负责人反映！";
                string ttemp3 = "项目创建提醒";

                sm.sendmailfunction(ttemp1, ttemp2, ttemp3);

                conn.Close();
                conn.Dispose();

                Response.Redirect("login.aspx");
            }
            else
            {
                Response.Write("<script>alert(\"请完整填写所有信息！\")</script>");
            }
        }
    }
}