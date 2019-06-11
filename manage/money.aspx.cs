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
using System.Data;
using System.Text.RegularExpressions;

public partial class manage_money : System.Web.UI.Page
{
    public string member;
    public string renwu;
    public string tempnum;
    public string webchat;
    public string chatnum;
    public string chatnum1;
    public string tempnum1;
    public string mingxi;
    public string tixing;
    public string flag;
    public bool isAdmin;

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

        if (Session["guanliyuan"] == Session["yonghuming"])
        {
            isAdmin = true;
        }
        else
        {
            isAdmin = false;
        }

        // 建立连接
        string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();

        try
        {
            // 确定是否显示搜索框
            if (Session["yonghuming"].ToString() == Session["guanliyuan"].ToString())
            {
                flag = "false";
            }
            else
            {
                flag = "true";
            }

            // 填充成员信息
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT username,name,money,spent,company,email FROM pro" + Session["xiangmuhao"].ToString();

            codemaker maker = new codemaker();

            SqlDataReader dr = cmd.ExecuteReader();

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

            // 填充经费使用情况统计图
            double yi;
            double wei;

            cmd.CommandText = "SELECT SUM(ALL money) AS money FROM moneyrecord WHERE projectno = '" + Session["xiangmuhao"].ToString() + "'";
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr[0].ToString().Trim()!="")
            {
                yi = Convert.ToDouble(dr[0].ToString().Trim());
            }
            else
            {
                yi = 0;
            }
            dr.Close();

            cmd.CommandText = "SELECT SUM(ALL money) AS money FROM bonus WHERE projectno = '" + Session["xiangmuhao"].ToString() + "'";
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr[0].ToString().Trim()!="")
            {
                wei = Convert.ToDouble(dr[0].ToString().Trim()) - yi;
            }
            else
            {
                wei = -yi;
            }
            //dr.Read();

            string[] label = new string[] { "已使用金额", "未使用金额" };
            string[] value = new string[] { yi.ToString(), wei.ToString() };

            DataTable dt = new DataTable();


            dt.Columns.Add("hengzuobiao", System.Type.GetType("System.String"));
            dt.Columns.Add("zongzuobiao", System.Type.GetType("System.String"));

            for (int i = 0; i <= 1; i++)
            {
                DataRow row = dt.NewRow();
                row["hengzuobiao"] = label[i];
                row["zongzuobiao"] = value[i];
                dt.Rows.Add(row);
            }

            this.Chart1.DataSource = dt;

            Chart1.Series["Series1"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Pie;//设置图表类型
            Chart1.Series[0].XValueMember = "hengzuobiao";//X轴数据成员列
            Chart1.Series[0].YValueMembers = "zongzuobiao";//Y轴数据成员列
            Chart1.Series[0].IsVisibleInLegend = true;
            Chart1.Series[0].IsValueShownAsLabel = true;//显示坐标值

            dr.Close();

            // 填充本人经费使用表
            // 填充经费使用情况统计图
            //cmd.CommandText = "SELECT money,spent FROM pro" + Session["xiangmuhao"].ToString() + " WHERE username='" + Session["yonghuming"].ToString() + "'";
            cmd.CommandText = "SELECT SUM(ALL money) AS money FROM moneyrecord WHERE name='" + Session["xingming"].ToString() + "' and projectno='" + Session["xiangmuhao"].ToString() + "'";

            double yi1;
            double wei1;

            dr = cmd.ExecuteReader();
            dr.Read();

            if (dr[0].ToString().Trim()!="")
            {
                yi1 = Convert.ToDouble(dr[0].ToString().Trim());
            }
            else
            {
                yi1 = 0;
            }
            dr.Close();

            cmd.CommandText = "SELECT SUM(ALL money) AS money FROM bonus WHERE name='" + Session["xingming"].ToString() + "' and projectno='" + Session["xiangmuhao"].ToString() + "'";
            dr = cmd.ExecuteReader();
            dr.Read();

            if (dr[0].ToString().Trim()!="")
            {
                wei1 = Convert.ToDouble(dr[0].ToString().Trim()) - yi1;
            }
            else
            {
                wei1 = -yi1;
            }
            dr.Close();
            
            string[] label1 = new string[] { "已使用金额", "未使用金额" };
            string[] value1 = new string[] { yi1.ToString(), wei1.ToString() };

            DataTable dt2 = new DataTable();


            dt2.Columns.Add("hengzuobiao1", System.Type.GetType("System.String"));
            dt2.Columns.Add("zongzuobiao1", System.Type.GetType("System.String"));

            for (int i = 0; i <= 1; i++)
            {
                DataRow row1 = dt2.NewRow();
                row1["hengzuobiao1"] = label1[i];
                row1["zongzuobiao1"] = value1[i];
                dt2.Rows.Add(row1);
            }

            this.Chart2.DataSource = dt2;

            Chart2.Series["Series1"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Pie;//设置图表类型
            Chart2.Series[0].XValueMember = "hengzuobiao1";//X轴数据成员列
            Chart2.Series[0].YValueMembers = "zongzuobiao1";//Y轴数据成员列
            Chart2.Series[0].IsVisibleInLegend = true;
            Chart2.Series[0].IsValueShownAsLabel = true;//显示坐标值

            dr.Close();

            /*
            // 填充经费分配情况统计图
            cmd.CommandText = "SELECT username,name,money FROM pro" + Session["xiangmuhao"].ToString();

            string[] username = new string[300];
            string[] money = new string[300];

            dr.Close();

            dr = cmd.ExecuteReader();

            int temp = 0;

            while (dr.Read())
            {
                username[temp] = dr[1].ToString().Trim() + "（" + dr[2].ToString().Trim() + "万元）";
                money[temp] = dr[2].ToString().Trim();

                temp++;
            }

            DataTable dt1 = new DataTable();
            dt1.Columns.Add("heng", System.Type.GetType("System.String"));
            dt1.Columns.Add("zong", System.Type.GetType("System.String"));

            for (num = 0; num < temp; num++)
            {
                DataRow row1 = dt1.NewRow();
                row1["heng"] = username[num];
                row1["zong"] = money[num];
                dt1.Rows.Add(row1);
            }

            this.Chart2.DataSource = dt1;

            Chart2.Series["Series1"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Pie;//设置图表类型
            Chart2.Series[0].XValueMember = "heng";//X轴数据成员列
            Chart2.Series[0].YValueMembers = "zong";//Y轴数据成员列
            Chart2.Series[0].IsVisibleInLegend = true;
            Chart2.Series[0].IsValueShownAsLabel = true;//显示坐标值

            dr.Close();
            */

            // 填充经费明细表
            cmd.CommandText = "SELECT code FROM mingxi WHERE username='" + Session["yonghuming"].ToString() + "'";
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                mingxi = dr[0].ToString().Trim();

                dr.Close();

                cmd.CommandText = "DELETE mingxi WHERE username='" + Session["yonghuming"].ToString() + "'";
                cmd.ExecuteNonQuery();
            }
            else
            {
                dr.Close();

                if (Session["guanliyuan"].ToString() == Session["yonghuming"].ToString())
                {
                    cmd.CommandText = "SELECT dateandtime,flag,username,money,projectno,name FROM moneyrecord WHERE projectno='" + Session["xiangmuhao"].ToString() + "' ORDER BY dateandtime DESC";
                }
                else
                {
                    cmd.CommandText = "SELECT dateandtime,flag,username,money,projectno,name FROM moneyrecord WHERE name = '" + Session["xingming"].ToString() + "' and projectno='" + Session["xiangmuhao"].ToString() + "' ORDER BY dateandtime DESC";
                }
                
                dr = cmd.ExecuteReader();

                int i = 1;

                while (dr.Read())
                {
                    mingxi = mingxi + maker.mingxi(i.ToString(), dr[1].ToString().Trim(), dr[0].ToString().Trim(),dr[3].ToString().Trim(),
                        isGuest(dr[2].ToString().Trim()),dr[5].ToString().Trim(),dr[4].ToString().Trim(),isAdmin);
   
                    i++;
                }

                dr.Close();

                if (Session["guanliyuan"].ToString() == Session["yonghuming"].ToString())
                {
                    cmd.CommandText = "SELECT dateandtime,flag,username,money,projectno,name FROM bonus WHERE projectno='" + Session["xiangmuhao"].ToString() + "' ORDER BY dateandtime DESC";
                }
                else
                {
                    cmd.CommandText = "SELECT dateandtime,flag,username,money,projectno,name FROM bonus WHERE name = '" + Session["xingming"].ToString() + "' and projectno='" + Session["xiangmuhao"].ToString() + "' ORDER BY dateandtime DESC";
                }

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    mingxi = mingxi + maker.mingxi(i.ToString(), dr[1].ToString().Trim(), dr[0].ToString().Trim(),dr[3].ToString().Trim(),
                        isGuest(dr[2].ToString().Trim()),dr[5].ToString().Trim(),dr[4].ToString().Trim(),isAdmin);

                    i++;
                }

                dr.Close();
            }

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
            conn.Close();
            conn.Dispose();
            Response.Write("<script>alert(\"" + ex.Message + "\")</script>");
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
    }

    protected void chaxunyes_Click(object sender, EventArgs e)
    {
        if (this.chaxuntitle.Text != "")
        {
            bool flag = true;

            string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (Session["yonghuming"].ToString() == Session["guanliyuan"].ToString())
                {
                    cmd.CommandText = "SELECT dateandtime,flag,username,money,projectno,name FROM moneyrecord WHERE projectno='" + Session["xiangmuhao"].ToString()
                    + "' and (flag LIKE '%" + this.chaxuntitle.Text + "%' or name='" + this.chaxuntitle.Text + "')";
                }
                else
                {
                    cmd.CommandText = "SELECT dateandtime,flag,username,money,projectno,name FROM moneyrecord WHERE projectno='" + Session["xiangmuhao"].ToString()
                    + "' and (flag LIKE '%" + this.chaxuntitle.Text + "%' or name = '" + this.chaxuntitle.Text + "') and name = '" + Session["xingming"].ToString() + "'";
                }

                SqlDataReader dr = cmd.ExecuteReader();

                codemaker maker = new codemaker();

                int i = 1;
                mingxi = "";

                while (dr.Read())
                {

                    mingxi = mingxi + maker.mingxi(i.ToString(), dr[1].ToString().Trim(), dr[0].ToString().Trim(),dr[3].ToString().Trim(),
                        isGuest(dr[2].ToString().Trim()),dr[5].ToString().Trim(),dr[4].ToString().Trim(),isAdmin);
                    i++;
                }

                dr.Close();

                if (Session["yonghuming"].ToString() == Session["guanliyuan"].ToString())
                {
                    cmd.CommandText = "SELECT dateandtime,flag,username,money,projectno,name FROM bonus WHERE projectno='" + Session["xiangmuhao"].ToString()
                    + "' and (flag LIKE '%" + this.chaxuntitle.Text + "%' or name='" + this.chaxuntitle.Text + "')";
                }
                else
                {
                    cmd.CommandText = "SELECT dateandtime,flag,username,money,projectno,name FROM bonus WHERE projectno='" + Session["xiangmuhao"].ToString()
                    + "' and (flag LIKE '%" + this.chaxuntitle.Text + "%' or name = '" + this.chaxuntitle.Text + "') and name = '" + Session["xingming"].ToString() + "'";
                }

                dr = cmd.ExecuteReader();

                maker = new codemaker();

                while (dr.Read())
                {

                    mingxi = mingxi + maker.mingxi(i.ToString(), dr[1].ToString().Trim(), dr[0].ToString().Trim(),dr[3].ToString().Trim(),
                        isGuest(dr[2].ToString().Trim()), dr[5].ToString().Trim(), dr[4].ToString().Trim(),isAdmin);
                    i++;
                }

                dr.Close();

                cmd.CommandText = "INSERT INTO mingxi VALUES('" + Session["yonghuming"].ToString() + "','" + mingxi + "')";
                cmd.ExecuteNonQuery();
            }
            catch
            {
                conn.Close();
                conn.Dispose();
                flag = false;
            }
            finally
            {
                conn.Close();
                conn.Dispose();

                if (!flag)
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"查询出现问题！\")</script>");
                }
                else
                {
                    Response.Redirect("money.aspx#mx");
                }
            }
        }
        else
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE mingxi WHERE username='" + Session["yonghuming"].ToString() + "'";
                cmd.ExecuteNonQuery();
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
                Response.Redirect("money.aspx#mx");
            }
        }
    }

    protected bool isGuest(string name)
    {
        if (name == "guest")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}