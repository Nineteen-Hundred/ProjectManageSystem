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
using securitycheck;
using sendmail;

public partial class register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        security sc = new security();
        int xx = sc.flag("临时变量", Request.UserHostAddress.ToString());

        if (xx == 1)
        {
            Response.Redirect("alert.aspx");
        }

        if (!IsPostBack)
        {
            this.male.Checked = true;
        }
    }

    protected string information()
    {
        string info;

        if (Session["gongsi"]!=null && Session["gongsi"].ToString() != "")
        {
            info = "<p style=\"font-size:small;color:Green\">您所在的系统已注册为\"" + Session["gongsi"].ToString() + "\"，管理员为\"" + Session["guanliyuan"].ToString() + "\"，如果您遇到了问题，请发送邮件至\"" + Session["youxiang"].ToString() + "\"和管理员联系！</p>";
        }
        else
        {
            info = "<p style=\"font-size:small;color:Red\">警告：你所使用的系统尚未注册，部分功能可能无法正常使用，且面临严重安全问题。</p>";
        }
        return info;
    }
    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        int length;

        length = this.username.Text.Length;

        if ((length > 6) && (length < 15))
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false;
        }
    }
    protected void passworden_ServerValidate(object source, ServerValidateEventArgs args)
    {
        int length;
        string temp;

        temp = this.password.Text;

        length = temp.Length;

        if ((length > 5) && (length < 17))
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false;
        }
    }
    protected void submit_Click(object sender, EventArgs e)
    {
        danger(this.username.Text);
        danger(this.password.Text);
        danger(this.ensure.Text);
        danger(this.name.Text);
        danger(this.company.Text);
        danger(this.phone.Text);
        danger(this.email.Text);
        danger(this.idcard.Text);

        string gender1;

        if (this.male.Checked)
        {
            gender1 = "男";
        }
        else
        {
            gender1 = "女";
        }

        

        try
        {
            sendmailclass smm = new sendmailclass();

            smm.sendmailfunction(this.email.Text, "您以此邮箱在科研项目管理系统中申请了用户，如要修改，请联系负责人！", "申请回执");
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请检查您输入的邮箱是否正确！\")</script>");
        }
        finally
        {
        }

        string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();

        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM main where username = '" + this.username.Text + "'";

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                conn.Close();
                conn.Dispose();

                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"您所申请的用户名已被占用！\")</script>");
            }
            else
            {
                /*
                 * 首先对图片进行上传，如果图片上传失败，将不会进行写入数据库等操作
                */

                dr.Close();

                if (shangchuan.FileName != "" && (Path.GetExtension(shangchuan.FileName) == ".jpg" || (Path.GetExtension(shangchuan.FileName) == ".png"
                    || Path.GetExtension(shangchuan.FileName) == ".JPG" || (Path.GetExtension(shangchuan.FileName) == ".PNG")))) //上传图片文件且文本框值不为空
                {

                    if (File.Exists(Server.MapPath("./") + "\\pictures\\" + this.username.Text + "13052425.jpg"))       //如果文件已存在，则删除已有文件
                    {
                        File.Delete(Server.MapPath("./") + "\\pictures\\" + this.username.Text + "13052425.jpg");
                    }

                    shangchuan.SaveAs(Server.MapPath("./") + "\\pictures\\" + this.username.Text + "13052425.jpg");      //将文件保存到服务器中

                    suolue sl = new suolue();           //进行类的实例化

                    string s1 = Server.MapPath("./") + "\\pictures\\" + this.username.Text + "13052425.jpg";
                    string s2 = Server.MapPath("./") + "\\pictures\\" + this.username.Text + ".jpg";
                    int s3 = 150;
                    int s4 = 150;
                    string s5 = "any";

                    sl.MakeThumbnail(s1, s2, s3, s4, s5);         //调用函数进行缩略图的生成和保存

                    File.Delete(Server.MapPath("./") + "\\pictures\\" + this.username.Text + "13052425.jpg");

                    HashMethod hm = new HashMethod();

                    string command;
                    command = "INSERT INTO main VALUES('" + this.username.Text + "','" + hm.Encrypto(this.password.Text) + "','" + this.name.Text
                        + "','" + gender1 + "','" + this.company.Text + "','" + this.phone.Text + "','" + this.email.Text + "','" + this.idcard.Text + "')";

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = conn;
                    cmd1.CommandText = command;

                    int i=1;

                    i = cmd1.ExecuteNonQuery();

                    if (i > 0)
                    {
                        conn.Close();
                        conn.Dispose();

                        Session["yonghuming"] = this.username.Text;
                        Session["xingming"] = this.name.Text;
                        Session["emaildizhi"] = this.email.Text;
                        Session["gongsi"] = this.company.Text;
                        Session["xingbie"] = gender1;
                        Session["shenfenzhenghao"] = this.idcard.Text;


                        Response.Redirect("ui.aspx");
                    }
                    else
                    {
                        conn.Close();
                        conn.Dispose();

                        ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"申请过程出现错误，请重试！\")</script>");
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"图片格式不支持！\")</script>");
                }
            }
        }
        catch
        {
            conn.Close();
            conn.Dispose();
            ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"您输入的内容不合法，请检查后重试！\")</script>");
        }
        finally
        {
            conn.Close();
            conn.Dispose();
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