using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using securitycheck;
using procode;

public partial class manage_gallery : System.Web.UI.Page
{
    public string member;
    public string shijian;
    public string renshu;
    public string liuliang;
    public string renwu;
    public string tempnum;
    public string webchat;
    public string chatnum;
    public string chatnum1;
    public string tempnum1;
    public string gallerytitle;
    public string galleryoption;
    public string gallerycontent;
    public string tixing;
    protected void Page_Load(object sender, EventArgs e)
    {
        // 进行安全检查
        if (Session["yonghuming"] == null)
        {
            Response.Redirect("../default.aspx");
        }

        if (Session["xiangmuhao"] == null)
        {
            Response.Redirect("../login.aspx");
        }

        security sc = new security();
        int xx = sc.flag(Session["yonghuming"].ToString(), Request.UserHostAddress.ToString());

        if (xx == 1)
        {
            Response.Redirect("../alert.aspx");
        }

        // 建立连接
        string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();

        try
        {
            // 填充成员信息
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT username,name,money,spent,company,email FROM pro" + Session["xiangmuhao"].ToString();

            codemaker maker = new codemaker();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (Session["yonghuming"].ToString() == dr[0].ToString() || Session["yonghuming"].ToString() == Session["guanliyuan"].ToString())
                {
                    member = member + maker.member(dr[0].ToString().Trim(), dr[1].ToString().Trim(), dr[2].ToString().Trim(), dr[3].ToString().Trim(), dr[4].ToString().Trim(), dr[5].ToString().Trim(), true);
                }
                else
                {
                    member = member + maker.member(dr[0].ToString().Trim(), dr[1].ToString().Trim(), dr[2].ToString().Trim(), dr[3].ToString().Trim(), dr[4].ToString().Trim(), dr[5].ToString().Trim(), false);
                }
            }

            dr.Close();

            // 填充任务下拉菜单及任务数量
            cmd.CommandText = "SELECT name,number FROM task WHERE projectno = '" + Session["xiangmuhao"].ToString() + "'";
            dr = cmd.ExecuteReader();
            int num = 0;

            while (dr.Read())
            {
                renwu = renwu + maker.progress(dr[1].ToString().Trim(), dr[0].ToString().Trim());
                num = num + 1;
            }

            tempnum = num.ToString();

            if (num == 0)
            {
                tempnum1 = "";
            }
            else
            {
                tempnum1 = tempnum;
            }

            dr.Close();

            // 填充私信下拉菜单
            string guanliyuan;
            cmd.CommandText = "SELECT name FROM main WHERE username = '" + Session["guanliyuan"].ToString() + "'";
            dr = cmd.ExecuteReader();
            dr.Read();
            guanliyuan = dr[0].ToString().Trim();
            dr.Close();

            cmd.CommandText = "SELECT dateandtime,senduser,message FROM webchat WHERE receiveuser ='" + Session["yonghuming"].ToString() + "' and flag='否'";
            dr = cmd.ExecuteReader();

            num = 0;
            while (dr.Read())
            {
                webchat = webchat + maker.webchat(dr[1].ToString().Trim(), guanliyuan, dr[0].ToString().Trim(), dr[2].ToString().Trim());
                num++;
            }

            chatnum = num.ToString();

            if (num == 0)
            {
                chatnum1 = "";
            }
            else
            {
                chatnum1 = chatnum;
            }

            dr.Close();

            // 填充正文图片部分
            cmd.CommandText = "SELECT title,type,filename FROM gallery WHERE projectno='" + Session["xiangmuhao"].ToString() + "' ORDER BY type";
            dr = cmd.ExecuteReader();

            int cirnum = 0;
            string temp = "";

            while(dr.Read())
            {
                if (temp != dr[1].ToString())
                {
                    cirnum++;
                    temp = dr[1].ToString().Trim();
                }
                gallerycontent = gallerycontent + maker.gallerycontent(dr[1].ToString(), dr[0].ToString(), dr[2].ToString(),cirnum);

            }

            dr.Close();

            cmd.CommandText = "SELECT type FROM gallery WHERE projectno='" + Session["xiangmuhao"].ToString() + "' GROUP BY type";
            dr = cmd.ExecuteReader();

            int cir = 1;

            while(dr.Read())
            {
                gallerytitle = gallerytitle + maker.gallerytitle(dr[0].ToString().Trim(), cir);
                galleryoption = galleryoption + maker.galleryoption(dr[0].ToString().Trim(),cir);
                cir++;
            }

            dr.Close();

            // 填充控制中心数量提醒
            cmd.CommandText = "SELECT COUNT(*) FROM files WHERE projectno='" + Session["xiangmuhao"].ToString() + "' and checked='否'";
            dr = cmd.ExecuteReader();

            int total = 0;

            if (dr.Read())
            {
                total = Convert.ToInt16(dr[0].ToString().Trim());
            }

            dr.Close();

            cmd.CommandText = "SELECT COUNT(*) FROM applies WHERE prono='" + Session["xiangmuhao"].ToString() + "'";
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                total = total + Convert.ToInt16(dr[0].ToString().Trim());
            }

            dr.Close();

            cmd.CommandText = "SELECT COUNT(*) FROM weekreport WHERE projectno='" + Session["xiangmuhao"].ToString() + "' and checked = '否'";
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                total = total + Convert.ToInt16(dr[0].ToString().Trim());
            }

            dr.Close();

            if (total != 0)
            {
                tixing = "<span class=\"badge pull-right\">" + total.ToString() + "</span>";
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
}