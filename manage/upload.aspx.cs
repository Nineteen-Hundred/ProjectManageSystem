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
using sendmail;
using System.IO;

public partial class manage_upload : System.Web.UI.Page
{
    public string member;
    public string liuliang;
    public string renwu;
    public string tempnum;
    public string webchat;
    public string chatnum;
    public string chatnum1;
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

            // 填充截图类别下拉菜单
            if (!IsPostBack)
            {
                cmd.CommandText = "SELECT type FROM gallery WHERE projectno = '" + Session["xiangmuhao"].ToString() + "' GROUP BY type";
                dr = cmd.ExecuteReader();

                this.ruanjianjietu.DataSource = dr;
                this.ruanjianjietu.DataTextField = "type";
                this.ruanjianjietu.DataValueField = "type";
                this.ruanjianjietu.DataBind();

                dr.Close();

                ListItem li = new ListItem();
                li.Text = "添加新类别";
                li.Value = "添加新类别";
                this.ruanjianjietu.Items.Add(li);
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

            cmd.CommandText = "SELECT COUNT(*) FROM weekreport WHERE projectno='" + Session["xiangmuhao"].ToString() + "' and checked = '否'";
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                total = total + Convert.ToInt16(dr[0].ToString().Trim());
            }

            dr.Close();

            cmd.CommandText = "SELECT COUNT(*) FROM applies WHERE prono='" + Session["xiangmuhao"].ToString() + "'";
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

    protected void qklwsubmit_Click(object sender, EventArgs e)
    {
        if(this.qklwcheck.Checked == true)
        {
            bool flag = true;

            security sc = new security();

            if(this.qikanlunwen.Text != "" && this.qikanriqi.Text!="")
            {
                string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();

                try
                {
                    // 给管理员发送邮件
                    sendmailclass sd = new sendmailclass();

                    string value1 = Session["emaildizhi"].ToString();
                    string value2 = "您的项目“" + Session["xiangmuming"].ToString() + "”有新成果上传，请前往查看！";
                    string value3 = "成果更新提醒";

                    sd.sendmailfunction(value1, value2, value3);

                    // 写入数据库
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    string filename = shengcheng(Path.GetExtension(this.qklwfile.FileName));
                    string information = this.qikanlunwen.Text + "_" + this.qikanqikan.Text + "_" + this.qikanriqi.Text;

                    cmd.CommandText = "INSERT INTO files VALUES('" + filename + "','" + Session["xiangmuhao"].ToString()
                        + "','" + "期刊论文" + "','" + information + "','" + Session["yonghuming"].ToString()
                        + "','否','" + Session["emaildizhi"].ToString() + "','" + Session["xingming"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    cmd.ExecuteNonQuery();

                    // 上传文件
                    this.qklwfile.SaveAs(Server.MapPath("/") + "\\files\\" + filename);

                    // 给管理员发送私信
                    cmd.CommandText = "INSERT INTO webchat VALUES('" + Session["yonghuming"].ToString() + "','" + Session["guanliyuan"].ToString()
                        + "','有新的成果上传，请及时审核！','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','否')";
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

                    if (flag)
                    {
                        this.hylwcheck.Checked = false;
                        this.huiyilunwen.Text = "";
                        ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"操作成功！\")</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查输入并保证网络畅通！\")</script>");
                    }
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查您输入的信息并注意文件格式！\")</script>");
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查您输入的信息并确认！\")</script>");
        }
    }

    protected void testbutton_Click(object sender, EventArgs e)
    {
    }

    protected string shengcheng(string format)
    {
        string temp1 = DateTime.Now.ToString("yyyyMMddhhmmss");
        string temp2 = Session["xingming"].ToString();

        string temp3 = temp1 + "By" + temp2 + format;

        return temp3;
    }

    protected void danger(string canshu)
    {
        security sc = new security();
        if (sc.CheckBadStr(canshu) == 1)
        {
            Response.Redirect("../alert.aspx");
        }

        if (sc.CheckBadStr(canshu) == 2)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"内容填写不完全，请重新填写！\")</script>");
        }
    }

    protected void hylwsubmit_Click(object sender, EventArgs e)
    {
        if (this.hylwcheck.Checked == true)
        {
            string information = this.huiyilunwen.Text + "_" + this.huiyihuiyi.Text + "_" + this.huiyididian.Text + "_"
                + this.huiyiriqi.Text + "_" + this.huiyizhaopian.Text;
            bool flag = true;

            security sc = new security();

            if (this.huiyilunwen.Text != "")
            {
                string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();

                try
                {
                    // 给管理员发送邮件
                    sendmailclass sd = new sendmailclass();

                    string value1 = Session["emaildizhi"].ToString();
                    string value2 = "您的项目“" + Session["xiangmuming"].ToString() + "”有新成果上传，请前往查看！";
                    string value3 = "成果更新提醒";

                    sd.sendmailfunction(value1, value2, value3);

                    // 写入数据库
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    string filename = shengcheng(Path.GetExtension(this.hylwfile.FileName));

                    cmd.CommandText = "INSERT INTO files VALUES('" + filename + "','" + Session["xiangmuhao"].ToString()
                        + "','" + "会议论文" + "','" + information + "','" + Session["yonghuming"].ToString()
                        + "','否','" + Session["emaildizhi"].ToString() + "','" + Session["xingming"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    cmd.ExecuteNonQuery();

                    // 上传文件
                    this.hylwfile.SaveAs(Server.MapPath("/") + "\\files\\" + filename);

                    // 给管理员发送私信
                    cmd.CommandText = "INSERT INTO webchat VALUES('" + Session["yonghuming"].ToString() + "','" + Session["guanliyuan"].ToString()
                        + "','有新的成果上传，请及时审核！','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','否')";
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

                    if(flag)
                    {
                        this.hylwcheck.Checked = false;
                        this.huiyilunwen.Text = "";
                        ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"操作成功！\")</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查输入并保证网络畅通！\")</script>");
                    }
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查您输入的信息并注意文件格式！\")</script>");
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查您输入的信息并确认！\")</script>");
        }
    }

    protected void shouquanzhlsubmit_Click(object sender, EventArgs e)
    {
        if (this.shouquanzhlcheck.Checked == true)
        {
            bool flag = true;

            security sc = new security();

            if (this.shouquanzhuanli.Text != "")
            {
                string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();

                try
                {
                    // 给管理员发送邮件
                    sendmailclass sd = new sendmailclass();

                    string value1 = Session["emaildizhi"].ToString();
                    string value2 = "您的项目“" + Session["xiangmuming"].ToString() + "”有新成果上传，请前往查看！";
                    string value3 = "成果更新提醒";

                    string information = this.shouquanzhuanli.Text + "_" + this.shouquanhaoma.Text + "_" + this.shouquanguojia.Text + "_" + this.shouquanriqi.Text;

                    sd.sendmailfunction(value1, value2, value3);

                    // 写入数据库
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    string filename = shengcheng(Path.GetExtension(this.shouquanzhlfile.FileName));

                    cmd.CommandText = "INSERT INTO files VALUES('" + filename + "','" + Session["xiangmuhao"].ToString()
                        + "','" + "授权专利" + "','" + information + "','" + Session["yonghuming"].ToString()
                        + "','否','" + Session["emaildizhi"].ToString() + "','" + Session["xingming"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    cmd.ExecuteNonQuery();

                    // 上传文件
                    this.shouquanzhlfile.SaveAs(Server.MapPath("/") + "\\files\\" + filename);

                    // 给管理员发送私信
                    cmd.CommandText = "INSERT INTO webchat VALUES('" + Session["yonghuming"].ToString() + "','" + Session["guanliyuan"].ToString()
                        + "','有新的成果上传，请及时审核！','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','否')";
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

                    if(flag)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"操作成功！\")</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查输入并保证网络畅通！\")</script>");
                    }
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查您输入的信息并注意文件格式！\")</script>");
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查您输入的信息并确认！\")</script>");
        }
    }

    protected void shenqingzhlsubmit_Click(object sender, EventArgs e)
    {
        if (this.shenqingzhlcheck.Checked == true)
        {
            bool flag = true;

            security sc = new security();

            if (this.shenqingzhuanli.Text != "")
            {
                string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();

                try
                {
                    // 给管理员发送邮件
                    sendmailclass sd = new sendmailclass();

                    string value1 = Session["emaildizhi"].ToString();
                    string value2 = "您的项目“" + Session["xiangmuming"].ToString() + "”有新成果上传，请前往查看！";
                    string value3 = "成果更新提醒";
                    string information = this.shenqingzhuanli.Text + "_" + this.shenqinghaoma.Text + "_" + this.shenqingguojia.Text
                        + "_" + this.shenqingriqi.Text;

                    sd.sendmailfunction(value1, value2, value3);

                    // 写入数据库
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    string filename = shengcheng(Path.GetExtension(this.shenqingzhlfile.FileName));

                    cmd.CommandText = "INSERT INTO files VALUES('" + filename + "','" + Session["xiangmuhao"].ToString()
                        + "','" + "申请专利" + "','" + information + "','" + Session["yonghuming"].ToString()
                        + "','否','" + Session["emaildizhi"].ToString() + "','" + Session["xingming"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    cmd.ExecuteNonQuery();

                    // 上传文件
                    this.shenqingzhlfile.SaveAs(Server.MapPath("/") + "\\files\\" + filename);

                    // 给管理员发送私信
                    cmd.CommandText = "INSERT INTO webchat VALUES('" + Session["yonghuming"].ToString() + "','" + Session["guanliyuan"].ToString()
                        + "','有新的成果上传，请及时审核！','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','否')";
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

                    if(flag)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"操作成功！\")</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查输入并保证网络畅通！\")</script>");
                    }
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查您输入的信息并注意文件格式！\")</script>");
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查您输入的信息并确认！\")</script>");
        }
    }

    protected void rjjtsubmit_Click(object sender, EventArgs e)
    {
        if (this.rjjtcheck.Checked == true)
        {
            bool flag = true;

            security sc = new security();

            if (sc.pictures(Path.GetExtension(this.rjjtfile.FileName)) && this.rjjttitle.Text!="")
            {
                string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();

                try
                {
                    string leibie;

                    if(this.ruanjianjietu.SelectedItem.Text=="添加新类别")
                    {
                        if(this.rjjtleibie.Text!="")
                        {
                            leibie = this.rjjtleibie.Text;
                        }
                        else
                        {
                            Exception ex = new Exception();
                            throw ex;
                        }
                    }
                    else
                    {
                        leibie = this.ruanjianjietu.SelectedItem.Text;
                    }
                    // 给管理员发送邮件
                    sendmailclass sd = new sendmailclass();

                    string value1 = Session["emaildizhi"].ToString();
                    string value2 = "您的项目“" + Session["xiangmuming"].ToString() + "”有新成果上传，请前往查看！";
                    string value3 = "成果更新提醒";

                    sd.sendmailfunction(value1, value2, value3);

                    // 写入数据库
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    string filename = shengcheng(Path.GetExtension(this.rjjtfile.FileName));

                    cmd.CommandText = "INSERT INTO gallery VALUES('" + this.rjjttitle.Text + "','" + leibie + "','" + Session["xiangmuhao"].ToString()
                        + "','" + filename + "')";
                    cmd.ExecuteNonQuery();

                    // 上传文件
                    this.rjjtfile.SaveAs(Server.MapPath("/") + "\\pictures\\" + filename);

                    // 给管理员发送私信
                    cmd.CommandText = "INSERT INTO webchat VALUES('" + Session["yonghuming"].ToString() + "','" + Session["guanliyuan"].ToString()
                        + "','有新的成果上传，请及时审核！','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','否')";
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

                    if (flag)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"操作成功！\")</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查输入并保证网络畅通！\")</script>");
                    }
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查您输入的信息并注意文件格式！\")</script>");
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查您输入的信息并确认！\")</script>");
        }
    }

    protected void qtcgsubmit_Click(object sender, EventArgs e)
    {
        if (this.qtcgcheck.Checked == true)
        {
            bool flag = true;

            security sc = new security();

            if (this.qitachengguo.Text != "")
            {
                string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();

                try
                {
                    // 给管理员发送邮件
                    sendmailclass sd = new sendmailclass();

                    string value1 = Session["emaildizhi"].ToString();
                    string value2 = "您的项目“" + Session["xiangmuming"].ToString() + "”有新成果上传，请前往查看！";
                    string value3 = "成果更新提醒";

                    sd.sendmailfunction(value1, value2, value3);

                    // 写入数据库
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    string filename = shengcheng(Path.GetExtension(this.qtcgfile.FileName));

                    cmd.CommandText = "INSERT INTO files VALUES('" + filename + "','" + Session["xiangmuhao"].ToString()
                        + "','" + "其它" + "','" + this.qitachengguo.Text + "','" + Session["yonghuming"].ToString()
                        + "','否','" + Session["emaildizhi"].ToString() + "','" + Session["xingming"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    cmd.ExecuteNonQuery();

                    // 上传文件
                    this.qtcgfile.SaveAs(Server.MapPath("/") + "\\files\\" + filename);

                    // 给管理员发送私信
                    cmd.CommandText = "INSERT INTO webchat VALUES('" + Session["yonghuming"].ToString() + "','" + Session["guanliyuan"].ToString()
                        + "','有新的成果上传，请及时审核！','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','否')";
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

                    if(flag)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"操作成功！\")</script>");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查输入并保证网络畅通！\")</script>");
                    }
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查您输入的信息并注意文件格式！\")</script>");
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查您输入的信息并确认！\")</script>");
        }
    }
}