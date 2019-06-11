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
using sendmail;
using System.EnterpriseServices;
using System.IO;
using System.Net.Mail;
using System.Net;

public partial class manage_control : System.Web.UI.Page
{
    public string member;
    public string renshu;
    public string liuliang;
    public string renwu;
    public string tempnum;
    public string webchat;
    public string chatnum;
    public string chatnum1;
    public string tempnum1;

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

        if (Session["guanliyuan"].ToString() != Session["yonghuming"].ToString())
        {
            Response.Redirect("frontpage.aspx");
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

            if (!IsPostBack)
            {
                // 填充新成员审批下拉菜单
                cmd.CommandText = "SELECT username,name FROM applies WHERE prono = '" + Session["xiangmuhao"].ToString() + "'";

                dr = cmd.ExecuteReader();

                this.shenpiuser.DataSource = dr;
                this.shenpiuser.DataTextField = "name";
                this.shenpiuser.DataValueField = "username";
                this.shenpiuser.DataBind();

                dr.Close();

                // 填充审批文件下拉菜单
                cmd.CommandText = "SELECT info,filename FROM files WHERE projectno='" + Session["xiangmuhao"].ToString() + "' and checked='否'";
                dr = cmd.ExecuteReader();

                this.chengguouser.DataSource = dr;
                this.chengguouser.DataTextField = "info";
                this.chengguouser.DataValueField = "filename";
                this.chengguouser.DataBind();

                dr.Close();

                // 填充周报告下拉菜单
                cmd.CommandText = "SELECT proname,filename FROM weekreport WHERE projectno='" + Session["xiangmuhao"].ToString() + "' and checked='否'";
                dr = cmd.ExecuteReader();

                this.zhoubaogaotitle.DataSource = dr;
                this.zhoubaogaotitle.DataTextField = "proname";
                this.zhoubaogaotitle.DataValueField = "filename";
                this.zhoubaogaotitle.DataBind();

                dr.Close();

                // 填充经费分配和报销以及发送私信的下拉菜单
                cmd.CommandText = "SELECT username,name FROM pro" + Session["xiangmuhao"].ToString();
                dr = cmd.ExecuteReader();

                this.fenpeiuser.DataSource = dr;
                this.fenpeiuser.DataTextField = "name";
                this.fenpeiuser.DataValueField = "username";
                this.fenpeiuser.DataBind();

                dr.Close();

                dr = cmd.ExecuteReader();

                this.baoxiaouser.DataSource = dr;
                this.baoxiaouser.DataTextField = "name";
                this.baoxiaouser.DataValueField = "username";
                this.baoxiaouser.DataBind();

                dr.Close();

                dr = cmd.ExecuteReader();

                this.sixinuser.DataSource = dr;
                this.sixinuser.DataTextField = "name";
                this.sixinuser.DataValueField = "username";
                this.sixinuser.DataBind();

                dr.Close();

                // 填充修改进度下拉菜单
                cmd.CommandText = "SELECT name,number FROM task WHERE projectno ='" + Session["xiangmuhao"].ToString() + "'";
                dr = cmd.ExecuteReader();

                this.xiugaititle.DataSource = dr;
                this.xiugaititle.DataTextField = "name";
                this.xiugaititle.DataValueField = "name";
                this.xiugaititle.DataBind();
               
                dr.Close();

                // 填充周次初始值
                this.datepicker.Text = DateTime.Now.ToString("MM/dd/yyyy");
            }

            cmd.CommandText = "SELECT number FROM task WHERE name='" + this.xiugaititle.SelectedItem.Text + "' and projectno='"
                + Session["xiangmuhao"].ToString() + "'";
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                this.xiugaitemp.Text = dr[0].ToString().Trim();

                dr.Close();
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

    protected void shenpingyes_Click(object sender, EventArgs e)
    {
        if(this.shenpiuser.Text!="" && this.shenpicheck.Checked)
        {
            bool flag = true;

            string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "SELECT name,gender,company,email,idcard FROM main WHERE username = '" + this.shenpiuser.SelectedValue.ToString().Trim()
                + "'";

                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();

                string username = this.shenpiuser.SelectedValue.ToString().Trim();
                string name = dr[0].ToString().Trim();
                string gender = dr[1].ToString().Trim();
                string company = dr[2].ToString().Trim();
                string email = dr[3].ToString().Trim();
                string idcard = dr[4].ToString().Trim();
                string money = "0";
                string spent = "0";

                dr.Close();

                // 发送邮件给操作对象
                sendmailclass smm = new sendmailclass();
                smm.sendmailfunction(email, "您申请加入项目“" + Session["xiangmuming"].ToString() + "”的请求已被同意，请登录查看！", "项目申请回执");

                // 将成员移入项目组
                cmd.CommandText = "INSERT INTO pro" + Session["xiangmuhao"].ToString() + " VALUES('" + username + "','" + name + "','" + idcard + "','" + gender + "','" + company + "','"
                    + email + "'," + money + "," + spent + ")";

                cmd.ExecuteNonQuery();

                // 从申请者名单中除去该名成员
                cmd.CommandText = "DELETE FROM applies WHERE username = '" + username + "'";
                cmd.ExecuteNonQuery();

                // 将该项目放入该名成员的历史记录之中
                cmd.CommandText = "INSERT INTO history VALUES('" + username + "','" + Session["xiangmuhao"].ToString()
                    + "','" + Session["xiangmuming"].ToString() + "')";

                cmd.ExecuteNonQuery();

                // 将该成员的加入记录放入项目活动之中
                cmd.CommandText = "INSERT INTO messages VALUES('" + DateTime.Now.ToString("yyyy/MM/dd hh:mm") + "','" + Session["xiangmuhao"].ToString()
                    + "',' " + this.shenpiuser.SelectedItem.Text + " 经过审核加入了本项目。','成员')";

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
                if(flag)
                {
                    this.shenpicheck.Checked = false;

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT username,name FROM applies WHERE prono = '" + Session["xiangmuhao"].ToString() + "'";

                    SqlDataReader dr = cmd.ExecuteReader();

                    this.shenpiuser.DataSource = dr;
                    this.shenpiuser.DataTextField = "name";
                    this.shenpiuser.DataValueField = "username";
                    this.shenpiuser.DataBind();

                    dr.Close();

                    conn.Close();
                    conn.Dispose();

                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"操作成功！\")</script>");
                }
                else
                {
                    conn.Close();
                    conn.Dispose();
                    this.shenpicheck.Checked = false;
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"操作失败，请检查后重试！\")</script>");
                }
            }
        }
        else
        {
            this.shenpicheck.Checked = false;
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查您输入的信息并确认！\")</script>");
        }
    }

    protected void chengguoyes_Click(object sender, EventArgs e)
    {
        bool flag = true;

        if(this.chengguocheck.Checked && this.chengguouser.Text!="")
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            try
            {
                // 发送邮件给操作对象
                sendmailclass sd = new sendmailclass();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "SELECT email FROM files WHERE filename='" + this.chengguouser.SelectedItem.Value.ToString().Trim() + "'";
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();

                string value1 = dr[0].ToString().Trim();  // 邮件地址
                string value2 = "您在项目“" + Session["xiangmuming"].ToString() + "”中所上传的成果已通过审核！";
                string value3 = "审核结果通知";

                sd.sendmailfunction(value1, value2, value3);

                dr.Close();

                // 更新成果的审批状态
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE files SET checked ='是' WHERE filename='" + this.chengguouser.SelectedItem.Value.ToString().Trim() + "'";
                cmd.ExecuteNonQuery();

                // 添加最近活动记录
                cmd.CommandText = "INSERT INTO messages VALUES('" + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + "','" + Session["xiangmuhao"].ToString()
                + "','文件 \"" + this.chengguouser.SelectedItem.Value.ToString().Trim() + "\" 已通过审核。','成果')";

                cmd.ExecuteNonQuery();

                // 发送私信给操作对象
                cmd.CommandText = "SELECT username FROM files WHERE filename='" + this.chengguouser.SelectedItem.Value.ToString().Trim() + "'";
                dr = cmd.ExecuteReader();
                dr.Read();

                cmd.CommandText = "INSERT INTO webchat VALUES('" + Session["yonghuming"].ToString() + "','" + dr[0].ToString().Trim() + "','您在项目“" + Session["xiangmuming"].ToString() + "”中所上传的成果已审批通过，请前往查看！','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','否')";
                dr.Close();
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
                if (flag)
                {
                    conn.Close();
                    conn.Dispose();
                    Response.Redirect("result.aspx");
                }
                else
                {
                    conn.Close();
                    conn.Dispose();
                    this.chengguocheck.Checked = false;
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查输入信息并保证网络通畅！\")</script>");
                }
            }
        }
        else
        {
            this.chengguocheck.Checked = false;
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查您输入的信息并确认！\")</script>");
        }
    }

    protected void shenpino_Click(object sender, EventArgs e)
    {
        if (this.shenpicheck.Checked && this.shenpiuser.Text != "")
        {
            bool flag = true;

            string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            try
            {
                // 向操作对象发送邮件
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                sendmailclass sd = new sendmailclass();
                cmd.CommandText = "SELECT email FROM main WHERE username = '" + this.shenpiuser.SelectedItem.Value.ToString().Trim() + "'";
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();

                string value1 = dr[0].ToString().Trim();
                string value2 = "您在项目“" + Session["xiangmuming"].ToString() + "”中提交的申请被拒绝！";
                string value3 = "用户申请结果";

                sd.sendmailfunction(value1, value2, value3);

                dr.Close();

                // 从文件列表中删除相关文件 
                cmd.CommandText = "DELETE applies WHERE username = '" + this.shenpiuser.SelectedItem.Value.ToString().Trim() + "'";
                cmd.ExecuteNonQuery();

                // 向操作对象发送私信
                cmd.CommandText = "INSERT INTO webchat VALUES('" + Session["yonghuming"].ToString() + "','" + this.shenpiuser.SelectedItem.Value.ToString().Trim() 
                    + "','您在项目“" + Session["xiangmuming"].ToString() + "”中提交的申请被拒绝，备注为：" + this.jujueliyou.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','否')";
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
                if (flag)
                {
                    this.shenpicheck.Checked = false;

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT username,name FROM applies WHERE prono = '" + Session["xiangmuhao"].ToString() + "'";

                    SqlDataReader dr = cmd.ExecuteReader();

                    this.shenpiuser.DataSource = dr;
                    this.shenpiuser.DataTextField = "name";
                    this.shenpiuser.DataValueField = "username";
                    this.shenpiuser.DataBind();

                    dr.Close();

                    conn.Close();
                    conn.Dispose();

                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"操作成功！\")</script>");
                }
                else
                {
                    conn.Close();
                    conn.Dispose();
                    this.shenpicheck.Checked = false;
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"操作失败，请检查后重试！\")</script>");
                }
            }
        }
        else
        {
            this.shenpicheck.Checked = false;
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查输入信息并确认！\")</script>");
        }
    }

    protected void chengguono_Click(object sender, EventArgs e)
    {
        bool flag = true;

        if (this.chengguocheck.Checked && this.chengguouser.Text != "")
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                // 向操作对象发送邮件
                sendmailclass sd = new sendmailclass();

                cmd.CommandText = "SELECT email FROM files WHERE filename = '" + this.chengguouser.SelectedItem.Value.ToString().Trim() + "'";
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();

                string value1 = dr[0].ToString().Trim();
                string value2 = "您在项目“" + Session["xiangmuming"].ToString() + "”中所上传的成果未通过审核！";
                string value3 = "审核结果通知";

                sd.sendmailfunction(value1, value2, value3);

                dr.Close();

                // 向操作对象发送私信
                cmd.CommandText = "SELECT username FROM files WHERE filename='" + this.chengguouser.SelectedItem.Value.ToString().Trim() + "'";
                dr = cmd.ExecuteReader();
                dr.Read();

                cmd.CommandText = "INSERT INTO webchat VALUES('" + Session["yonghuming"].ToString() + "','" + dr[0].ToString().Trim() + "','您在项目“" + Session["xiangmuming"].ToString() + "”中所上传的成果未通过审批，请重新上传！','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','否')";
                dr.Close();
                cmd.ExecuteNonQuery();

                // 从文件列表中删除相关文件
                cmd.CommandText = "DELETE files WHERE filename = '" + this.chengguouser.SelectedItem.Value.ToString().Trim() + "'";
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
                if (flag)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT info,filename FROM files WHERE projectno='" + Session["xiangmuhao"].ToString() + "' and checked='否'";

                    SqlDataReader dr = cmd.ExecuteReader();

                    this.chengguouser.DataSource = dr;
                    this.chengguouser.DataTextField = "info";
                    this.chengguouser.DataValueField = "filename";
                    this.chengguouser.DataBind();

                    dr.Close();

                    conn.Close();
                    conn.Dispose();

                    this.chengguocheck.Checked = false;

                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"操作成功！\")</script>");
                }
            }
        }
        else
        {
            this.chengguocheck.Checked = false;
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查输入信息并确认！\")</script>");
        }
    }

    protected void chengguodown_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.chengguouser.SelectedItem.Text != "")
            {
                Response.Redirect("../files/" + this.chengguouser.SelectedItem.Value.ToString());
            }
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>laert(\"请先选择一个文件！\")</script>");
        }
    }

    protected void fenpeiyes_Click(object sender, EventArgs e)
    {
        if(this.fenpeiuser.SelectedItem.Text!="" && this.fenpeicheck.Checked && this.fenpeititle.Text!="" && this.fenpeinum.Text!="" )
        {
            bool flag = true; // 判断操作是否正常进行

            string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                // 发送邮件给操作对象
                cmd.CommandText = "SELECT email FROM main WHERE username = '" + this.fenpeiuser.SelectedItem.Value.ToString().Trim() + "'";
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();

                string value1 = dr[0].ToString().Trim();
                string value2 = "您在项目“" + Session["xiangmuming"].ToString() + "”中获得 " + this.fenpeinum.Text + "元 经费分配，请前往查看！";
                string value3 = "经费变更通知";

                sendmailclass smm = new sendmailclass();
                smm.sendmailfunction(value1, value2, value3);

                dr.Close();

                // 更新数据库中经费的数额
                cmd.CommandText = "UPDATE pro" + Session["xiangmuhao"].ToString() + " SET money = money + "
                + Convert.ToDouble(this.fenpeinum.Text)/10000 + " WHERE username = '" + this.fenpeiuser.SelectedItem.Value.ToString().Trim() + "'";
                cmd.ExecuteNonQuery();

                // 添加最近活动数据库记录
                cmd.CommandText = "INSERT INTO bonus VALUES('" + this.fenpeiuser.SelectedItem.Value.ToString().Trim()+ "','" 
                    + this.fenpeiuser.SelectedItem.Text.ToString().Trim()
                    + "'," +  this.fenpeinum.Text + ",'" + this.fenpeileibie.Text + this.fenpeititle.Text + "','" + DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss") + "','"
                    + Session["xiangmuhao"].ToString() + "')";
                cmd.ExecuteNonQuery();

                // 发送私信给操作对象
                cmd.CommandText = "INSERT INTO webchat VALUES('" + Session["yonghuming"].ToString() + "','" + this.fenpeiuser.SelectedItem.Value.ToString().Trim() + "','您在项目“"
                    + Session["xiangmuming"].ToString() + "中获得了" + this.fenpeinum.Text + "元经费，请前往查看！','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','否')";
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                conn.Close();
                conn.Dispose();
                flag = false;
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请输入合法的金额，并保证网络通畅！\")</script>");
            }
            finally
            {
                conn.Close();
                conn.Dispose();

                if(flag)
                {
                    this.fenpeititle.Text = "";
                    this.fenpeinum.Text = "";
                    this.fenpeicheck.Checked = false;
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
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查输入信息并确认！\")</script>");
        }
    }

    protected void baoxiaoyes_Click(object sender, EventArgs e)
    {
        if (this.baoxiaouser.SelectedItem.Text != "" && this.baoxiaocheck.Checked && this.baoxiaotitle.Text != "" && this.baoxiaonum.Text != "")
        {
            bool flag = true; // 判断操作是否正常进行

            string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                // 发送邮件给操作对象
                cmd.CommandText = "SELECT email FROM main WHERE username = '" + this.baoxiaouser.SelectedItem.Value.ToString().Trim() + "'";
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();

                string value1 = dr[0].ToString().Trim();
                string value2 = "您在项目“" + Session["xiangmuming"].ToString() + "”中报销 " + this.baoxiaonum.Text + "元 经费，请前往查看！";
                string value3 = "经费变更通知";

                sendmailclass smm = new sendmailclass();
                smm.sendmailfunction(value1, value2, value3);

                dr.Close();

                // 更新数据库中经费的数额
                cmd.CommandText = "UPDATE pro" + Session["xiangmuhao"].ToString() + " SET spent = spent + "
                + Convert.ToDouble(this.baoxiaonum.Text)/10000  + " WHERE username = '" + this.baoxiaouser.SelectedItem.Value.ToString().Trim() + "'";
                cmd.ExecuteNonQuery();

                // 添加最近活动数据库记录
                cmd.CommandText = "INSERT INTO moneyrecord VALUES('" + this.baoxiaouser.SelectedItem.Value.ToString().Trim()
                    + "','" + this.baoxiaouser.SelectedItem.Text.ToString().Trim()
                    + "'," + this.baoxiaonum.Text + ",'" + this.baoxiaoleibie.Text + this.baoxiaotitle.Text + "','" + DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss") + "','"
                    + Session["xiangmuhao"].ToString() + "')";
                cmd.ExecuteNonQuery();

                // 发送私信给操作对象
                cmd.CommandText = "INSERT INTO webchat VALUES('" + Session["yonghuming"].ToString() + "','" + this.baoxiaouser.SelectedItem.Value.ToString().Trim() + "','您在项目“"
                    + Session["xiangmuming"].ToString() + "中报销了" + this.baoxiaonum.Text + "元经费，请前往查看！','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','否')";
                cmd.ExecuteNonQuery();
            }
            catch
            {
                conn.Close();
                conn.Dispose();
                flag = false;
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请输入合法的金额，并保证网络通畅！\")</script>");
            }
            finally
            {
                conn.Close();
                conn.Dispose();

                if (flag)
                {
                    this.baoxiaotitle.Text = "";
                    this.baoxiaonum.Text = "";
                    this.baoxiaocheck.Checked = false;
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
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查输入信息并确认！\")</script>");
        }
    }

    protected void xinjianyes_Click(object sender, EventArgs e)
    {
        bool flag = true;

        if(this.xinjiantitle.Text!="" && this.xinjiancheck.Checked)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            try
            {
                // 给管理员发送邮件
                string temp1 = Session["emaildizhi"].ToString();
                string temp2 = "新建任务";
                string temp3 = "您在项目“" + Session["xiangmuming"].ToString() + "”中建立了一个名为“" + this.xinjiantitle.Text + "”的任务，请前往查看！";

                sendmailclass sd = new sendmailclass();
                sd.sendmailfunction(temp1, temp3, temp2);

                // 将任务写入数据库
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO task VALUES('" + this.xinjiantitle.Text + "','" + Session["xiangmuhao"].ToString() + "','" + this.xinjianvalue.Text + "')";

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
                if(flag)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT name,number FROM task WHERE projectno ='" + Session["xiangmuhao"].ToString() + "'";

                    SqlDataReader dr = cmd.ExecuteReader();

                    this.xiugaititle.DataSource = dr;
                    this.xiugaititle.DataTextField = "name";
                    this.xiugaititle.DataValueField = "number";
                    this.xiugaititle.DataBind();
                    this.xiugaivalue.Text = this.xiugaititle.SelectedItem.Value.ToString();

                    dr.Close();
                    conn.Close();
                    conn.Dispose();

                    this.xinjiantitle.Text = "";
                    this.xinjiancheck.Checked = false;
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"操作成功！\")</script>");
                }
                else
                {
                    conn.Close();
                    conn.Dispose();
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查输入信息并保证网络通畅！\")</script>");
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查输入信息并确认！\")</script>");
        }
    }

    protected void xugaiyes_Click(object sender, EventArgs e)
    {
        bool flag = true;

        if(this.xiugaititle.SelectedItem.Text!="" && this.xiugaicheck.Checked)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            try
            {
                // 向管理员发送邮件
                string temp1 = Session["emaildizhi"].ToString();
                string temp2 = "修改任务进度";
                string temp3 = "您在项目“" + Session["xiangmuming"].ToString() + "”中修改了“" + this.xinjiantitle.Text + "”这一任务的进度，请前往查看！";

                sendmailclass sd = new sendmailclass();
                sd.sendmailfunction(temp1, temp3, temp2);

                // 更新数据库
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "UPDATE task SET number='" + this.xiugaivalue.Text + "' WHERE name='" + this.xiugaititle.SelectedItem.Text.ToString() + "' and "
                    + "projectno = '" + Session["xiangmuhao"].ToString() + "'";

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
                if(flag)
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"操作成功！\")</script>");
                }
                else
                {
                    conn.Close();
                    conn.Dispose();
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查输入并保证网络畅通！\")</script>");
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查输入信息并确认！\")</script>");
        }
    }

    protected void youjianno_Click(object sender, EventArgs e)
    {
        security sc = new security();
        if (sc.lunwen(Path.GetExtension(this.youjianfile.FileName)) || sc.zhuanli(Path.GetExtension(this.youjianfile.FileName)))
        {
            string filename = DateTime.Now.ToString("MMddhhmmss") + this.youjianfile.FileName;
            this.youjianfile.SaveAs(Server.MapPath("/") + "\\attachments\\" + filename);

            this.youjianfujian.Text = filename;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请选择合法的格式！\")</script>");
        }
    }

    protected void youjianyes_Click(object sender, EventArgs e)
    {
        if (this.youjiantitle.Text != "" && this.youjianzhengwen.Text != "" && this.youjiancheck.Checked)
        {
			string error = "success";
            bool flag = true;

            string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            try
            {
                string youxiang;
                string mingzi = "";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT name,email from pro" + Session["xiangmuhao"].ToString();
                cmd.Connection = conn;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    youxiang = dr[1].ToString().Trim();
                    mingzi = dr[0].ToString().Trim();

                    fasong(youxiang, mingzi);
                }
                else
                {
                    throw new Exception();
                }

                while (dr.Read())
                {
                    youxiang = dr[1].ToString().Trim();
                    mingzi = dr[0].ToString().Trim();

                    fasong(youxiang, mingzi);
                }

                dr.Close();
            }
            catch(Exception ex)
            {
                conn.Close();
                conn.Dispose();
                flag = false;
				error = ex.Message;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                if (flag)
                {
                    this.youjiantitle.Text = "";
                    this.youjianzhengwen.Text = "";
                    this.youjianmima.Text = "";
                    this.youjiancheck.Checked = false;
                    this.youjianfujian.Text = "";

                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"操作成功！\")</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查输入信息并保证网络通畅！" + error + "\")</script>");
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"输入信息不完整，请检查！\")</script>");
        }
    }

    protected void fasong(string mubiao, string mingzi)
    {
        /* string server;
        string mailname;
        string address;

        string youxiangdizhi = Session["emaildizhi"].ToString();

        string[] dizhi = youxiangdizhi.Split('@');

        server = "smtp." + dizhi[1];
        mailname = dizhi[0];
        address = youxiangdizhi;


        System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();

        SmtpClient mysmtp = new SmtpClient(server, 25);
        mysmtp.Credentials = new NetworkCredential(mailname, this.youjianmima.Text);

        if (this.youjianfujian.Text != "")
        {
            Attachment att = new Attachment(Server.MapPath("/") + "\\attachments\\" + this.youjianfujian.Text);
            m.Attachments.Add(att);
        }

        m.To.Add(new MailAddress(mubiao));
        m.From = new MailAddress(address);
        m.Body = this.youjianzhengwen.Text;
        m.Subject = this.youjiantitle.Text;

        mysmtp.Send(m); */
		
		System.Web.Mail.MailMessage mail = new System.Web.Mail.MailMessage();
		mail.To = mubiao;
		mail.From = "yicloudpro@163.com";
		mail.Subject = this.youjiantitle.Text;
		mail.BodyFormat = System.Web.Mail.MailFormat.Html;
		mail.Body = this.youjianzhengwen.Text;
		mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1"); //basic authentication
		mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "yicloudpro@163.com"); //set your username here
		mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "shyspy715"); //set your password here
		mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 465);//set port
		mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true");//set is ssl  

		if(this.youjianfujian.Text!="")
		{
			mail.Attachments.Add(new System.Web.Mail.MailAttachment(Server.MapPath("/") + "\\attachments\\" + this.youjianfujian.Text));
		}
		System.Web.Mail.SmtpMail.SmtpServer = "smtp.163.com";
		System.Web.Mail.SmtpMail.Send(mail);
    }

    protected void sixinyes_Click(object sender, EventArgs e)
    {
        if (this.sixinuser.SelectedItem.Text != "" && this.sixincheck.Checked && this.sixintitle.Text != "")
        {
            bool flag = true;

            string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO webchat VALUES('" + Session["yonghuming"].ToString() + "','" + this.sixinuser.SelectedItem.Value.ToString().Trim()
                    + "','" + this.sixintitle.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','否')";
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
                    this.sixincheck.Checked = false;
                    this.sixintitle.Text = "";

                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"操作成功！\")</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查输入信息并确认！\")</script>");
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查输入信息并确认！\")</script>");
        }
    }

    protected void gonggaoyes_Click(object sender, EventArgs e)
    {
        if(this.gonggaotitle.Text!="" && this.gonggaocheck.Checked)
        {
            bool flag = true;

            string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO publics VALUES('" + Session["xiangmuhao"].ToString() + "','" + this.gonggaotitle.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss") + "')";

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
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"发布成功！\")</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查输入并保证网络畅通！\")</script>");
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请输入完整信息并确认！\")</script>");
        }
    }
    protected void zhoubaogaoyes_Click(object sender, EventArgs e)
    {
        bool flag = true;
		string error = "success";

        if (this.zhoubaogaoqueren.Checked && this.zhoubaogaotitle.Text != "")
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            try
            {
                // 发送邮件给操作对象
                sendmailclass sd = new sendmailclass();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "SELECT username FROM weekreport WHERE filename='" + this.zhoubaogaotitle.SelectedItem.Value.ToString().Trim() + "'";
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
				
				string tmpuser = dr[0].ToString().Trim();
				
				dr.Close();
				
				cmd.CommandText = "SELECT email FROM main WHERE username='" + tmpuser + "'";
				dr = cmd.ExecuteReader();
				dr.Read();

                string value1 = dr[0].ToString().Trim();  // 邮件地址
                string value2 = "您在项目“" + Session["xiangmuming"].ToString() + "”中所上传的周报告已通过审核！";
                string value3 = "审核结果通知";

                sd.sendmailfunction(value1, value2, value3);

                dr.Close();

                // 更新成果的审批状态
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE weekreport SET checked ='是' WHERE filename='" + this.zhoubaogaotitle.SelectedItem.Value.ToString().Trim() + "'";
                cmd.ExecuteNonQuery();

                // 添加最近活动记录(小事件，没有必要添加记录)
                //cmd.CommandText = "INSERT INTO messages VALUES('" + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + "','" + Session["xiangmuhao"].ToString()
                //+ "','文件 \"" + this.chengguouser.SelectedItem.Value.ToString().Trim() + "\" 已通过审核。','成果')";

                cmd.ExecuteNonQuery();

                // 发送私信给操作对象
                cmd.CommandText = "SELECT username FROM weekreport WHERE filename='" + this.zhoubaogaotitle.SelectedItem.Value.ToString().Trim() + "'";
                dr = cmd.ExecuteReader();
                dr.Read();

                cmd.CommandText = "INSERT INTO webchat VALUES('" + Session["yonghuming"].ToString() + "','" + dr[0].ToString().Trim() + "','您在项目“" + Session["xiangmuming"].ToString() + "”中所上传的周报告已审批通过，请前往查看！','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','否')";
                dr.Close();
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
				error = ex.Message;
                conn.Close();
                conn.Dispose();
                flag = false;
            }
            finally
            {
                if (flag)
                {
                    conn.Close();
                    conn.Dispose();
                    //ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"操作成功，请前往周报告栏目查看或刷新控制中心！\")</script>");
					Response.Redirect("control.aspx");
                }
                else
                {
                    conn.Close();
                    conn.Dispose();
                    this.chengguocheck.Checked = false;
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查输入信息并保证网络通畅！" + error + "\")</script>");
                }
            }
        }
        else
        {
            this.chengguocheck.Checked = false;
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查您输入的信息并确认！\")</script>");
        }
    }
    protected void zhoubaogaono_Click(object sender, EventArgs e)
    {
        bool flag = true;
		string error = "success";

        if (this.zhoubaogaoqueren.Checked && this.zhoubaogaotitle.Text != ""&&this.zhoubaogaobeizhu.Text!=null)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                // 向操作对象发送邮件
                sendmailclass sd = new sendmailclass();

                cmd.CommandText = "SELECT username FROM weekreport WHERE filename = '" + this.zhoubaogaotitle.SelectedItem.Value.ToString().Trim() + "'";
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
				
				string tmpusername = dr[0].ToString().Trim();
				dr.Close();
				
				cmd.CommandText = "SELECT email FROM main WHERE username ='" + tmpusername + "'";
				dr = cmd.ExecuteReader();
				dr.Read();

                string value1 = dr[0].ToString().Trim();
                string value2 = "您在项目“" + Session["xiangmuming"].ToString() + "”中所上传的周报告未通过审核！";
                string value3 = "审核结果通知";

                sd.sendmailfunction(value1, value2, value3);

                dr.Close();

                // 向操作对象发送私信
                cmd.CommandText = "SELECT username FROM weekreport WHERE filename='" + this.zhoubaogaotitle.SelectedItem.Value.ToString().Trim() + "'";
                dr = cmd.ExecuteReader();
                dr.Read();

                cmd.CommandText = "INSERT INTO webchat VALUES('" + Session["yonghuming"].ToString() + "','" + dr[0].ToString().Trim() + "','您在项目“" + Session["xiangmuming"].ToString() + "”中所上传的成果未通过审核，备注：" + this.zhoubaogaobeizhu.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','否')";
                dr.Close();
                cmd.ExecuteNonQuery();

                // 从文件列表中删除相关文件
                cmd.CommandText = "DELETE weekreport WHERE filename = '" + this.zhoubaogaotitle.SelectedItem.Value.ToString().Trim() + "'";
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                conn.Close();
                conn.Dispose();
                flag = false;
				error = ex.Message;
            }
            finally
            {
                if (flag)
                {
					/*
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"操作成功！\")</script>");
					
					SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT name,filename FROM files WHERE projectno='" + Session["xiangmuhao"].ToString() + "' and checked='否'";

                    SqlDataReader dr = cmd.ExecuteReader();

                    this.zhoubaogaotitle.DataSource = dr;
                    this.zhoubaogaotitle.DataTextField = "name";
                    this.zhoubaogaotitle.DataValueField = "filename";
                    this.zhoubaogaotitle.DataBind();

                    dr.Close();

                    conn.Close();
                    conn.Dispose();

                    this.zhoubaogaoqueren.Checked = false;
					*/
					//ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"操作成功！" + error + "\")</script>");
					Response.Redirect("control.aspx");
                }
				else
				{
					ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查输入信息并确认！" + error + "\")</script>");
				}
            }
        }
        else
        {
            this.zhoubaogaoqueren.Checked = false;
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查输入信息并确认！\")</script>");
        }
    }
    protected void zhouciyes_Click(object sender, EventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();

        int i = 0;

        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE project SET start='" + datepicker.Text + "' WHERE username='" + Session["xiangmuhao"].ToString() + "'";

            i = cmd.ExecuteNonQuery();
        }
        catch
        {
        }
        finally
        {
            conn.Close();
            conn.Dispose();

            if (i > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"操作成功！\")</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"操作失败！\")</script>");
            }
        }
    }
    protected void zhoubaogaodown_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.zhoubaogaotitle.SelectedItem.Text != "")
            {
                Response.Redirect("../files/" + this.zhoubaogaotitle.SelectedItem.Value.ToString());
            }
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>laert(\"请先选择一个文件！\")</script>");
        }
    }
}