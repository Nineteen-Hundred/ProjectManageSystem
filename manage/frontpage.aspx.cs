using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using procode;
using System.Data.SqlClient;
using System.Configuration;
using securitycheck;
using ipchaxun;
using System.Data;
using System.Text.RegularExpressions;

public partial class manage_frontpage1 : System.Web.UI.Page
{
    public string member="";
    public string gonggao;
    public string shijian;
    public string rszb;
    public string jfzb;
    public string cgzb;
    public string renshu;
    public string liuliang;
    public string renwu="";
    public string tempnum;
    public string webchat;
    public string chatnum;
    public string chatnum1;
    public string tempnum1;
    public string activity;
    public string chatwindow;
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

        //try {
            // 获取登录用户的IP地址并写入数据库
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = "SELECT * FROM pro" + Session["xiangmuhao"].ToString() + " WHERE username = '" + Session["yonghuming"].ToString() + "'";

            SqlDataReader dr = cmd.ExecuteReader();

            if (!dr.Read())
            {
                dr.Close();
                Response.Redirect("temp.aspx");  // 如果查询不到结果，说明该成员尚未加入项目，跳转至提示页面
            }

            string ipdizhi;
            ipdizhi = Request.UserHostAddress.ToString();

            dr.Close();

            IPSearch.IPLocation wulidizhi;

            if (ipdizhi != "::1")
            {
                IPSearch ips = new IPSearch();
                wulidizhi = ips.GetIPLocation(ipdizhi);
            }
            else
            {
                ipdizhi = "127.0.0.1";
                wulidizhi.country = "维护地址";
                wulidizhi.area = "主机";
            }

            cmd.CommandText = "INSERT INTO records VALUES('" + Session["yonghuming"].ToString() + "','" + Session["xingming"].ToString()
                + "','" + DateTime.Now.ToString("MM-dd") + "','" + DateTime.Now.ToLongTimeString().ToString() + "','" + ipdizhi
                + "','" + Session["xiangmuhao"].ToString() + "','" + wulidizhi.country + wulidizhi.area + "')";
            cmd.ExecuteNonQuery();
            dr.Close();  // 关闭读取器，以便之后的代码可以调用该读取器

            // 绘制项目成果汇总图
            string[] name = new string[5] { "期刊论文", "会议论文", "申请专利", "授权专利", "其它" };
            string[] number = new string[5];

            /*
             * 以下过程可以循环使用
             */

            cmd.CommandText = "SELECT COUNT(*) FROM files WHERE projectno = '" + Session["xiangmuhao"].ToString() + "' and type='期刊论文' and checked='是'";
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                number[0] = dr[0].ToString().Trim();
            }
            else
            {
                number[0] = "0";
            }

            dr.Close();


            /*
             * 以下过程可以循环使用
             */

            cmd.CommandText = "SELECT COUNT(*) FROM files WHERE projectno = '" + Session["xiangmuhao"].ToString() + "' and type='会议论文' and checked='是'";
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                number[1] = dr[0].ToString().Trim();
            }
            else
            {
                number[1] = "0";
            }

            dr.Close();


            /*
             * 以下过程可以循环使用
             */

            cmd.CommandText = "SELECT COUNT(*) FROM files WHERE projectno = '" + Session["xiangmuhao"].ToString() + "' and type='申请专利' and checked='是'";
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                number[2] = dr[0].ToString().Trim();
            }
            else
            {
                number[2] = "0";
            }

            dr.Close();


            /*
             * 以下过程可以循环使用
             */

            cmd.CommandText = "SELECT COUNT(*) FROM files WHERE projectno = '" + Session["xiangmuhao"].ToString() + "' and type='授权专利' and checked='是'";
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                number[3] = dr[0].ToString().Trim();
            }
            else
            {
                number[3] = "0";
            }

            dr.Close();

            /*
             * 以下过程可以循环使用
             */

            cmd.CommandText = "SELECT COUNT(*) FROM files WHERE projectno = '" + Session["xiangmuhao"].ToString() + "' and type='其它' and checked='是'";
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                number[4] = dr[0].ToString().Trim();
            }
            else
            {
                number[4] = "0";
            }

            dr.Close();

            DataTable dt = new DataTable();
            dt.Columns.Add("hengzuobiao", System.Type.GetType("System.String"));
            dt.Columns.Add("zongzuobiao", System.Type.GetType("System.String"));

            for (int i = 0; i <= 4; i++)
            {
                DataRow row = dt.NewRow();
                row["hengzuobiao"] = name[i];
                row["zongzuobiao"] = number[i];
                dt.Rows.Add(row);
            }

            this.Chart1.DataSource = dt;

            this.Chart1.Series[0].XValueMember = "hengzuobiao";
            this.Chart1.Series[0].YValueMembers = "zongzuobiao";

            this.Chart1.ChartAreas["ChartArea1"].AxisX.Title = "类别";
            this.Chart1.ChartAreas["ChartArea1"].AxisY.Title = "数量";
            Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            this.Chart1.Series[0].IsValueShownAsLabel = true;

            // 绘制访问量变化统计图
            /*
            cmd.CommandText = "SELECT COUNT(*),date FROM records WHERE projectno = '" + Session["xiangmuhao"].ToString() + "' GROUP BY date ORDER BY date DESC";
            dr = cmd.ExecuteReader();
            string[] label = new string[7];
            string[] quantity = new string[7];

            int a;

            for (a = 6; a >= 0; a--)
            {
                if (dr.Read())
                {
                    label[a] = dr[1].ToString().Trim();
                    quantity[a] = dr[0].ToString().Trim();
                }
                else
                {
                    break;
                }
            }

            dr.Close();

            DataTable dt1 = new DataTable();
            dt1.Columns.Add("riqi", System.Type.GetType("System.String"));
            dt1.Columns.Add("shuliang", System.Type.GetType("System.String"));
            int b;

            for (b = 0; b < 7; b++)
            {
                DataRow row = dt1.NewRow();
                row["riqi"] = label[b];
                row["shuliang"] = quantity[b];
                dt1.Rows.Add(row);
            }

            this.Chart2.DataSource = dt1;
            this.Chart2.Series[0].XValueMember = "riqi";
            this.Chart2.Series[0].YValueMembers = "shuliang";

            this.Chart2.ChartAreas[0].AxisX.Title = "日期";
            this.Chart2.ChartAreas[0].AxisY.Title = "访问量";
            Chart2.ChartAreas["ChartArea2"].AxisX.Interval = 1;
            this.Chart2.Series[0].IsValueShownAsLabel = true;
            dr.Close();
             */

            // 绘制经费使用情况图
            string[] type = new string[7] { "设备费", "材料费", "资料费", "差旅费", "通信费","劳务费","其它" };
            double[] money = new double[7]{0,0,0,0,0,0,0};

            /*
             * 以下过程可以循环使用
             */

            cmd.CommandText = "SELECT flag,money FROM moneyrecord WHERE projectno = '" + Session["xiangmuhao"].ToString() + "'";
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                //string[] tmpstr = dr[0].ToString().Trim().Split(new char[] { '#', '#' });
                string[] tmpstr = new string[2];
                tmpstr[0] = dr[0].ToString().Trim();
                tmpstr[1] = dr[1].ToString().Trim();

                if (tmpstr[0].IndexOf("设备费") >= 0)
                {
                    money[0] = money[0] + Convert.ToDouble(tmpstr[1]) / 10000;
                }
                else if (tmpstr[0].IndexOf("材料费") >= 0)
                {
                    money[1] = money[1] + Convert.ToDouble(tmpstr[1]) / 10000;
                }
                else if (tmpstr[0].IndexOf("资料费") >= 0)
                {
                    money[2] = money[2] + Convert.ToDouble(tmpstr[1]) / 10000;
                }
                else if (tmpstr[0].IndexOf("差旅费") >= 0)
                {
                    money[3] = money[3] + Convert.ToDouble(tmpstr[1]) / 10000;
                }
                else if (tmpstr[0].IndexOf("通信费") >= 0)
                {
                    money[4] = money[4] + Convert.ToDouble(tmpstr[1]) / 10000;
                }
                else if (tmpstr[0].IndexOf("劳务费") >= 0)
                {
                    money[5] = money[5] + Convert.ToDouble(tmpstr[1]) / 10000;
                }
                else if (tmpstr[0].IndexOf("其它") >= 0)
                {
                    money[6] = money[6] + Convert.ToDouble(tmpstr[1]) / 10000;
                }
            }

            dr.Close();

            DataTable dt1 = new DataTable();
            dt1.Columns.Add("hengzuobiao1", System.Type.GetType("System.String"));
            dt1.Columns.Add("zongzuobiao1", System.Type.GetType("System.String"));

            for (int i = 0; i <= 6; i++)
            {
                DataRow row = dt1.NewRow();
                row["hengzuobiao1"] = type[i];
                row["zongzuobiao1"] = money[i].ToString();
                dt1.Rows.Add(row);
            }

            this.Chart2.DataSource = dt1;

            this.Chart2.Series[0].XValueMember = "hengzuobiao1";
            this.Chart2.Series[0].YValueMembers = "zongzuobiao1";

            this.Chart2.ChartAreas["ChartArea2"].AxisX.Title = "类别";
            this.Chart2.ChartAreas["ChartArea2"].AxisY.Title = "数量";
            Chart2.ChartAreas["ChartArea2"].AxisX.Interval = 1;
            this.Chart2.Series[0].IsValueShownAsLabel = true;

            // 填充公告栏内容
            cmd.CommandText = "SELECT message,dateandtime FROM publics WHERE projectno = '" + Session["xiangmuhao"].ToString() + "' ORDER BY dateandtime DESC";
            dr = cmd.ExecuteReader();

            dr.Read();
            gonggao = dr[0].ToString().Trim();
            shijian = dr[1].ToString().Trim();

            dr.Close();

            // 填充成员信息
            cmd.CommandText = "SELECT username,name,money,spent,company,email FROM pro" + Session["xiangmuhao"].ToString();

            codemaker maker = new codemaker();

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
                if (moneyReader[0].ToString().Trim()!="")
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
                if (moneyReader1[0].ToString().Trim()!="")
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

            // 填充平台人数总占比及总人数
            cmd.CommandText = "SELECT COUNT(*) FROM pro" + Session["xiangmuhao"].ToString();
            dr = cmd.ExecuteReader();
            dr.Read();
            double int1 = Convert.ToDouble(dr[0].ToString());
            renshu = int1.ToString();

            dr.Close();

            cmd.CommandText = "SELECT COUNT(*) FROM main";
            dr = cmd.ExecuteReader();
            dr.Read();
            double int2 = Convert.ToDouble(dr[0].ToString());

            dr.Close();

            double result = int1 / int2 * 100;

            rszb = ((int)result).ToString();

            // 填充平台经费总占比
            cmd.CommandText = "SELECT money FROM project WHERE username='" + Session["xiangmuhao"].ToString() + "'";
            dr = cmd.ExecuteReader();
            dr.Read();

            int1 = Convert.ToDouble(dr[0].ToString());
            dr.Close();

            cmd.CommandText = "SELECT SUM(ALL money) AS tomoney FROM project";
            dr = cmd.ExecuteReader();
            dr.Read();

            int2 = Convert.ToDouble(dr[0].ToString());
            dr.Close();

            result = int1 / int2 * 100;

            jfzb = ((int)result).ToString();

            // 填充平台成果总占比
            cmd.CommandText = "SELECT COUNT(*) FROM files WHERE projectno='" + Session["xiangmuhao"].ToString() + "'";
            dr = cmd.ExecuteReader();
            dr.Read();

            int1 = Convert.ToDouble(dr[0].ToString());
            dr.Close();

            cmd.CommandText = "SELECT COUNT(*) FROM files";
            dr = cmd.ExecuteReader();
            dr.Read();

            int2 = Convert.ToDouble(dr[0].ToString());
            dr.Close();

            result = int1 / int2 * 100;

            cgzb = ((int)result).ToString();

            // 填充访问总流量
            cmd.CommandText = "SELECT COUNT(*) FROM records WHERE projectno = '" + Session["xiangmuhao"].ToString() + "'";
            dr = cmd.ExecuteReader();
            dr.Read();
            liuliang = dr[0].ToString().Trim();

            dr.Close();

            // 填充任务下拉菜单及任务数量
            cmd.CommandText = "SELECT name,number FROM task WHERE projectno = '" + Session["xiangmuhao"].ToString() + "'";
            dr = cmd.ExecuteReader();
            int num = 0;
            
            while(dr.Read())
            {
                renwu = renwu + maker.progress(dr[1].ToString().Trim(), dr[0].ToString().Trim());
                num = num + 1;
            }

            tempnum = num.ToString();

            if(num==0)
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

            cmd.CommandText = "SELECT dateandtime,senduser,message FROM webchat WHERE receiveuser ='" + Session["yonghuming"].ToString() + "' and flag='否' ORDER BY dateandtime DESC";
            dr = cmd.ExecuteReader();

            num = 0;
            while(dr.Read())
            {
                webchat = webchat + maker.webchat(dr[1].ToString().Trim(), guanliyuan, dr[0].ToString().Trim(), dr[2].ToString().Trim());
                num++;
            }

            chatnum = num.ToString();

            if(num==0)
            {
                chatnum1 = "";
            }
            else
            {
                chatnum1 = chatnum;
            }

            dr.Close();

            // 填充最近活动
            cmd.CommandText = "SELECT dateandtime,message,type FROM messages WHERE projectno ='" + Session["xiangmuhao"].ToString() + "' ORDER BY dateandtime DESC";
            dr = cmd.ExecuteReader();
            int tmpint = 0;

            while(dr.Read() && tmpint<=25)
            {
                activity = activity + maker.activity(dr[1].ToString().Trim(), dr[0].ToString().Trim(), dr[2].ToString().Trim());
                tmpint++;
            }

            dr.Close();

            // 填充聊天窗口
            cmd.CommandText = "SELECT name,dateandtime,message,username FROM bbs WHERE projectno='" + Session["xiangmuhao"].ToString() + "' ORDER BY dateandtime DESC";
            dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                if(dr[3].ToString()==Session["yonghuming"].ToString())
                {
                    chatwindow = chatwindow + maker.chatwindow2(dr[0].ToString().Trim(), dr[2].ToString().Trim(), dr[1].ToString().Trim(), dr[3].ToString().Trim());
                }
                else
                {
                    chatwindow = chatwindow + maker.chatwindow(dr[0].ToString().Trim(), dr[2].ToString().Trim(), dr[1].ToString().Trim(), dr[3].ToString().Trim());
                }
            }

            dr.Close();

            // 填充控制中心数量提醒
            cmd.CommandText = "SELECT COUNT(*) FROM files WHERE projectno='" + Session["xiangmuhao"].ToString() + "' and checked='否'";
            dr = cmd.ExecuteReader();

            int total = 0;

            if(dr.Read())
            {
                total = Convert.ToInt16(dr[0].ToString().Trim());
            }

            dr.Close();

            cmd.CommandText = "SELECT COUNT(*) FROM weekreport WHERE projectno='" + Session["xiangmuhao"].ToString() + "' and checked = '否'";
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                total = total + Convert.ToInt16(dr[0].ToString().Trim());
            }

            dr.Close();

            cmd.CommandText = "SELECT COUNT(*) FROM applies WHERE prono='" + Session["xiangmuhao"].ToString() + "'";
            dr = cmd.ExecuteReader();

            if(dr.Read())
            {
                total = total + Convert.ToInt16(dr[0].ToString().Trim());
            }

            dr.Close();

            if(total!=0)
            {
                tixing = "<span class=\"badge pull-right\">" + total.ToString() + "</span>";
            }
            /*
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
             * */
    }

    protected void send_Click(object sender, EventArgs e)
    {
        if (this.message.Text != "")
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO bbs VALUES('" + Session["xiangmuhao"].ToString() + "','" + Session["yonghuming"].ToString() + "','" + Session["xingming"].ToString()
                    + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + this.message.Text + "')";
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
                Response.Redirect("frontpage.aspx#message");
            }
        }
        else
        {
        }
    }
}