using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using procode;
using securitycheck;

public partial class manage_result1 : System.Web.UI.Page
{
    public string qklw;
    public string hylw;
    public string shouquan;
    public string shenqing;
    public string qita;
    public string member;
    public string renwu;
    public string webchat;
    public string chatnum;
    public string chatnum1;
    public string tempnum;
    public string tempnum1;
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

        codemaker maker = new codemaker();

        try
        {
            // 填充期刊论文
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT filename,type,info,username,name,dateandtime FROM files WHERE projectno='" + Session["xiangmuhao"].ToString() + "' and checked = '是' ORDER BY dateandtime DESC,username";

            SqlDataReader dr = cmd.ExecuteReader();

            int qikan = 1;
            int huiyi = 1;
            int shenqingzhl = 1;
            int shouquanzhl = 1;
            int qt = 1;

            while(dr.Read())
            {
                string name;

                if (dr[3].ToString().Trim() == "System")
                {
                    name = "系统";
                }
                else
                {
                    name = dr[4].ToString().Trim();
                }

                if (Session["yonghuming"].ToString() == Session["guanliyuan"].ToString())
                {
                    if (dr[1].ToString().Trim() == "期刊论文")
                    {
                        qklw = qklw + maker.qklw(qikan.ToString(), dr[2].ToString().Trim(), dr[0].ToString().Trim(),name,dr[5].ToString().Trim());
                        qikan++;
                    }
                    else if (dr[1].ToString().Trim() == "会议论文")
                    {
                        hylw = hylw + maker.qklw(huiyi.ToString(), dr[2].ToString().Trim(), dr[0].ToString().Trim(),name,dr[5].ToString().Trim());
                        huiyi++;
                    }
                    else if (dr[1].ToString().Trim() == "授权专利")
                    {
                        shouquan = shouquan + maker.qklw(shouquanzhl.ToString(), dr[2].ToString().Trim(), dr[0].ToString().Trim(), name, dr[5].ToString().Trim());
                        shouquanzhl++;
                    }
                    else if (dr[1].ToString().Trim() == "申请专利")
                    {
                        shenqing = shenqing + maker.qklw(shenqingzhl.ToString(), dr[2].ToString().Trim(), dr[0].ToString().Trim(), name, dr[5].ToString().Trim());
                        shenqingzhl++;
                    }
                    else
                    {
                        qita = qita + maker.qklw(qt.ToString(), dr[2].ToString().Trim(), dr[0].ToString().Trim(), name, dr[5].ToString().Trim());
                        qt++;
                    }
                }
                else
                {
                    if (dr[1].ToString().Trim() == "期刊论文")
                    {
                        qklw = qklw + maker.qklwpt(qikan.ToString(), dr[2].ToString().Trim(), dr[0].ToString().Trim(), name, dr[5].ToString().Trim());
                        qikan++;
                    }
                    else if (dr[1].ToString().Trim() == "会议论文")
                    {
                        hylw = hylw + maker.qklwpt(huiyi.ToString(), dr[2].ToString().Trim(), dr[0].ToString().Trim(), name, dr[5].ToString().Trim());
                        huiyi++;
                    }
                    else if (dr[1].ToString().Trim() == "授权专利")
                    {
                        shouquan = shouquan + maker.qklwpt(shouquanzhl.ToString(), dr[2].ToString().Trim(), dr[0].ToString().Trim(), name, dr[5].ToString().Trim());
                        shouquanzhl++;
                    }
                    else if (dr[1].ToString().Trim() == "申请专利")
                    {
                        shenqing = shenqing + maker.qklwpt(shenqingzhl.ToString(), dr[2].ToString().Trim(), dr[0].ToString().Trim(), name, dr[5].ToString().Trim());
                        shenqingzhl++;
                    }
                    else
                    {
                        qita = qita + maker.qklwpt(qt.ToString(), dr[2].ToString().Trim(), dr[0].ToString().Trim(), name, dr[5].ToString().Trim());
                        qt++;
                    }
                }
            }

            dr.Close();

            // 填充成员信息
            cmd.CommandText = "SELECT username,name,money,spent,company,email FROM pro" + Session["xiangmuhao"].ToString();

            maker = new codemaker();

            dr = cmd.ExecuteReader();
            SqlCommand tmpcmd = new SqlCommand();
            SqlConnection conn1 = new SqlConnection(connStr);
            conn1.Open();
            tmpcmd.Connection = conn1;

            while (dr.Read())
            {
                string spent;
                string totalmoney;
                string username = dr[0].ToString().Trim();
                tmpcmd.CommandText = "SELECT SUM(ALL money) as tomoney FROM moneyrecord WHERE name='" + dr[1].ToString().Trim() + "' and projectno='" + Session["xiangmuhao"].ToString() + "'";
                SqlDataReader moneyReader = tmpcmd.ExecuteReader();
                moneyReader.Read();
                if (moneyReader[0].ToString().Trim() != "")
                {
                    spent = moneyReader[0].ToString().Trim();
                }
                else
                {
                    spent = "0";
                }
                moneyReader.Close();

                tmpcmd.CommandText = "SELECT SUM(ALL money) as tomoney FROM bonus WHERE name='" + dr[1].ToString().Trim() + "' and projectno='" + Session["xiangmuhao"].ToString() + "'";
                SqlDataReader moneyReader1 = tmpcmd.ExecuteReader();
                moneyReader1.Read();
                if (moneyReader1[0].ToString().Trim() != "")
                {
                    totalmoney = moneyReader1[0].ToString().Trim();
                }
                else
                {
                    totalmoney = "0";
                }
                moneyReader1.Close();


                if (Session["yonghuming"].ToString() == dr[0].ToString() || Session["yonghuming"].ToString() == Session["guanliyuan"].ToString())
                {
                    member = member + maker.member(dr[0].ToString().Trim(), dr[1].ToString().Trim(), totalmoney, spent, dr[4].ToString().Trim(), dr[5].ToString().Trim(), true);
                }
                else
                {
                    member = member + maker.member(dr[0].ToString().Trim(), dr[1].ToString().Trim(), totalmoney, spent, dr[4].ToString().Trim(), dr[5].ToString().Trim(), false);
                }
            }

            dr.Close();
            conn1.Close();
            conn1.Dispose();

            // 填充私信下拉菜单
            string guanliyuan;
            cmd.CommandText = "SELECT name FROM main WHERE username = '" + Session["guanliyuan"].ToString() + "'";
            dr = cmd.ExecuteReader();
            dr.Read();
            guanliyuan = dr[0].ToString().Trim();
            dr.Close();

            cmd.CommandText = "SELECT dateandtime,senduser,message FROM webchat WHERE receiveuser ='" + Session["yonghuming"].ToString() + "' and flag='否'";
            dr = cmd.ExecuteReader();

            int num = 0;

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

            // 填充任务下拉菜单及任务数量
            cmd.CommandText = "SELECT name,number FROM task WHERE projectno = '" + Session["xiangmuhao"].ToString() + "'";
            dr = cmd.ExecuteReader();
            num = 0;

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
        catch(Exception ex)
        {
            Response.Write("<script>alert(\"" + ex.Message + "\")</script>");
            conn.Close();
            conn.Dispose();
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
    }

    protected void shanchu(string path, string info)
    {
    }
}