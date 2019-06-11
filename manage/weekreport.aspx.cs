using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using securitycheck;
using procode;
using System.IO;
using sendmail;
using System.Globalization;

public partial class manage_weekreport : System.Web.UI.Page
{
    public string member;
    public string liuliang;
    public string renwu;
    public string tempnum;
    public string webchat;
    public string chatnum;
    public string chatnum1;
    public string tempnum1;
    public string zhoubaogao;
    public string wangqizhoubaogao;
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

            // 填充周次下拉菜单
            /*
            int total;

            cmd.CommandText = "SELECT weeknum FROM weekreport WHERE projectno = '" + Session["xiangmuhao"].ToString() + "' and"
                + " username='" + Session["yonghuming"].ToString() + "' ORDER BY weeknum DESC";
            dr = cmd.ExecuteReader();

            if(dr.Read())
            {
                total = Convert.ToInt16(dr[0].ToString().Trim()) + 1;
            }
            else
            {
                total = 1;
            }

            dr.Close();

            for(int i=0;i<=5;i++,total++)
            {
                ListItem li = new ListItem();
                li.Text = total.ToString();
                li.Value = total.ToString();
                this.shangchuanuser.Items.Add(li);                
            }
             * */


            if (!IsPostBack)
            {
                // 填充周报告姓名下拉菜单
                cmd.CommandText = "SELECT name FROM pro" + Session["xiangmuhao"].ToString();
                dr = cmd.ExecuteReader();

                this.renming.DataSource = dr;
                this.renming.DataTextField = "name";
                this.renming.DataValueField = "name";
                this.renming.DataBind();

                dr.Close();

                // 填充周次下拉菜单
                cmd.CommandText = "SELECT start FROM project WHERE username='" + Session["xiangmuhao"].ToString() + "'";
                dr = cmd.ExecuteReader();

                int jiange = 1;
                DateTime chushi = DateTime.Now;

                if (dr.Read())
                {
                    string[] items = dr[0].ToString().Trim().Split('/');
                    chushi = new DateTime(Convert.ToInt16(items[2]), Convert.ToInt16(items[0]), Convert.ToInt16(items[1]));
                }

                TimeSpan ts = DateTime.Now.Subtract(chushi);
                jiange = (int)ts.TotalDays / 7 + 1;

                ListItem li = new ListItem();
                li.Text = jiange.ToString();
                li.Value = jiange.ToString();
                this.shangchuanuser.Items.Add(li);

                for (int i = 1; i < jiange + 1; i++)
                {
                    ListItem li1 = new ListItem();
                    li1.Text = i.ToString();
                    li1.Value = i.ToString();
                    this.zhoucixiala.Items.Add(li1);
                }

                dr.Close();
            }
            
            // 填充周报告表格
            if(Session["yonghuming"].ToString() == Session["guanliyuan"].ToString())  // 如果本人即管理员，则可以看到所有人的周报告
            {
                cmd.CommandText = "SELECT weeknum,name,proname,filename,dateandtime,checked FROM weekreport WHERE projectno ='" + Session["xiangmuhao"].ToString() + "' and weeknum=" + this.shangchuanuser.SelectedItem.Text + " ORDER BY weeknum";
                dr = cmd.ExecuteReader();

                while(dr.Read())
                {
                    zhoubaogao = zhoubaogao + maker.weekreport(dr[0].ToString().Trim(), dr[1].ToString().Trim(), dr[2].ToString().Trim(), dr[3].ToString().Trim(), dr[4].ToString().Trim(),dr[5].ToString().Trim());
                }

                dr.Close();
            }
            else  // 否则只能看到本人的周报告
            {
                cmd.CommandText = "SELECT weeknum,name,proname,filename,dateandtime,checked FROM weekreport WHERE projectno ='" + Session["xiangmuhao"].ToString() + "' and username='" + Session["yonghuming"].ToString() + "' and weeknum='" + this.shangchuanuser.SelectedItem.Text + "' ORDER BY weeknum";
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    zhoubaogao = zhoubaogao + maker.weekreport(dr[0].ToString().Trim(), dr[1].ToString().Trim(), dr[2].ToString().Trim(), dr[3].ToString().Trim(), dr[4].ToString().Trim(),dr[5].ToString().Trim());
                }

                dr.Close();
            }

            // 填充往期周报告表格
            string comm;

            if (this.zhouciorrenming.Checked)
            {
                comm = " and weeknum='" + this.zhoucixiala.SelectedItem.Text + "'";
            }
            else
            {
                comm = " and name='" + this.renming.SelectedItem.Text + "'";
            }
            if (Session["yonghuming"].ToString() == Session["guanliyuan"].ToString())  // 如果本人即管理员，则可以看到所有人的周报告
            {
                cmd.CommandText = "SELECT weeknum,name,proname,filename,dateandtime,checked FROM weekreport WHERE projectno ='" + Session["xiangmuhao"].ToString() + "'" + comm + " ORDER BY weeknum";
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    wangqizhoubaogao = wangqizhoubaogao + maker.weekreport(dr[0].ToString().Trim(), dr[1].ToString().Trim(), dr[2].ToString().Trim(), dr[3].ToString().Trim(), dr[4].ToString().Trim(), dr[5].ToString().Trim());
                }

                dr.Close();
            }
            else  // 否则只能看到本人的周报告
            {
                cmd.CommandText = "SELECT weeknum,name,proname,filename,dateandtime,checked FROM weekreport WHERE projectno ='" + Session["xiangmuhao"].ToString() + "' and username='" + Session["yonghuming"].ToString() + "' ORDER BY weeknum";
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    wangqizhoubaogao = wangqizhoubaogao + maker.weekreport(dr[0].ToString().Trim(), dr[1].ToString().Trim(), dr[2].ToString().Trim(), dr[3].ToString().Trim(), dr[4].ToString().Trim(), dr[5].ToString().Trim());
                }

                dr.Close();
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

    protected void shangchuanyes_Click(object sender, EventArgs e)
    {
        if(this.shangchuanuser.SelectedItem.Text!="" && this.shangchuancheck.Checked && this.shangchuanfile.FileName!="" && this.shangchuantitle.Text!="")
        {
            bool flag = true;

            string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            try
            {
                // 发送邮件给管理员
                string temp1;
                string temp2;
                string temp3;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT email FROM main WHERE username='" + Session["guanliyuan"].ToString() + "'";

                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();

                temp1 = dr[0].ToString().Trim();
                temp2 = "周报告上传通知";
                temp3 = "您的项目“" + Session["xiangmuming"].ToString() + "”中有新的周报告上传，请前往查看！";

                dr.Close();

                sendmailclass sd = new sendmailclass();
                //sd.sendmailfunction(temp1, temp3, temp2);

                // 将信息写入数据库
                string filename = shengcheng(Path.GetExtension(this.shangchuanfile.FileName));

                cmd.CommandText = "INSERT INTO weekreport VALUES('" + Session["xiangmuhao"].ToString() + "','" + Session["yonghuming"].ToString() + "','"
                    + Session["xingming"].ToString() + "','" + this.shangchuanuser.SelectedItem.Text + "','" + this.shangchuantitle.Text + "','"
                    + filename + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','否')";

                cmd.ExecuteNonQuery();

                // 保存文件
                this.shangchuanfile.SaveAs(Server.MapPath("/") + "\\files\\" + filename);
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

                if(flag)
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"操作成功！\")</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查输入信息并保证网络畅通！\")</script>");
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查您输入的信息并注意文件格式！\")</script>");
        }
    }

    protected string shengcheng(string format)
    {
        string temp1 = DateTime.Now.ToString("yyyyMMddhhmmss");
        string temp2 = Session["xingming"].ToString();

        string temp3 = temp1 + "By" + temp2 + format;

        return temp3;
    }
}