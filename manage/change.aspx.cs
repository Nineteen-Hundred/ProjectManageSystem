using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Sql;
using securitycheck;
using DataCrypto;
using sendmail;
using System.IO;
using suoluetu;

public partial class manage_change : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM main WHERE username='" + Session["yonghuming"].ToString() + "'";

                SqlDataReader dr = cmd.ExecuteReader();

                dr.Read();

                this.username.Text = dr[0].ToString().Trim();
                this.name.Text = dr[2].ToString().Trim();
                if (dr[3].ToString().Trim() == "男")
                {
                    this.male.Checked = true;
                }
                else
                {
                    this.female.Checked = true;
                }
                this.company.Text = dr[4].ToString().Trim();
                this.phone.Text = dr[5].ToString().Trim();
                this.email.Text = dr[6].ToString().Trim();
                this.idcard.Text = dr[7].ToString().Trim();
            }
            catch(Exception ex)
            {
                conn.Close();
                conn.Dispose();
                Response.Redirect("error.aspx");
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        
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

        danger(this.yuan.Text);
        danger(this.password.Text);
        danger(this.ensure.Text);
        danger(this.company.Text);
        danger(this.phone.Text);
        danger(this.name.Text);


       
        string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();

        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT password FROM main WHERE username = '" + Session["yonghuming"].ToString() + "'";

            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            HashMethod hm = new HashMethod();

            if (hm.Encrypto(this.yuan.Text) == dr[0].ToString().Trim())
            {
                dr.Close();
                /*
                 * 以下为添加代码
                 */

                string gender1;

                if (this.male.Checked)
                {
                    gender1 = "男";
                }
                else
                {
                    gender1 = "女";
                }


                /*
                 * 图片一栏是否为空，如果为空则直接跳过该步骤
                 */

                if (this.shangchuan.FileName != "")
                {
                    /*
                     * 首先对图片进行上传，如果图片上传失败，将不会进行写入数据库等操作
                    */

                    if (Path.GetExtension(shangchuan.FileName) == ".jpg")  //上传图片文件且文本框值不为空
                    {

                        if (File.Exists(Server.MapPath("/") + "\\pictures\\" + this.username.Text + "13052425.jpg"))       //如果文件已存在，则删除已有文件
                        {
                            File.Delete(Server.MapPath("/") + "\\pictures\\" + this.username.Text + "13052425.jpg");
                        }

                        if (File.Exists(Server.MapPath("/") + "\\pictures\\" + this.username.Text + ".jpg"))       //如果文件已存在，则删除已有文件
                        {
                            File.Delete(Server.MapPath("/") + "\\pictures\\" + this.username.Text + ".jpg");
                        }

                        shangchuan.SaveAs(Server.MapPath("/") + "\\pictures\\" + this.username.Text + "13052425.jpg");      //将文件保存到服务器中

                        suolue sl = new suolue();           //进行类的实例化

                        string s1 = Server.MapPath("/") + "\\pictures\\" + this.username.Text + "13052425.jpg";
                        string s2 = Server.MapPath("/") + "\\pictures\\" + this.username.Text + ".jpg";
                        int s3 = 170;
                        int s4 = 170;
                        string s5 = "any";

                        sl.MakeThumbnail(s1, s2, s3, s4, s5);         //调用函数进行缩略图的生成和保存

                        File.Delete(Server.MapPath("/") + "\\pictures\\" + this.username.Text + "13052425.jpg");
                    }
                }

                string command;
                command = "UPDATE main SET username='" + this.username.Text + "', password='" + hm.Encrypto(this.ensure.Text) + "',name='" + this.name.Text
                        + "',gender='" + gender1 + "',company='" + this.company.Text + "',phone='" + this.phone.Text + "',email='" + this.email.Text + "',idcard='" + this.idcard.Text + "' WHERE username='"
                        + Session["yonghuming"].ToString() + "'";

                cmd.CommandText = command;

                int i;

                i = cmd.ExecuteNonQuery();

                if (i > 0)
                {
                    Session["yonghuming"] = this.username.Text;
                    Session["xingming"] = this.name.Text;
                    Session["emaildizhi"] = this.email.Text;
                    Session["gongsi"] = this.company.Text;
                    Session["xingbie"] = gender1;

                    dr.Close();

                    conn.Close();
                    conn.Dispose();

                }
                else
                {
                    dr.Close();
                    conn.Close();
                    conn.Dispose();

                    ClientScript.RegisterStartupScript(GetType(), "","<script>alert(\"申请过程出现错误，请重试！\")</script>");
                }

                sendmailclass smm = new sendmailclass();

                smm.sendmailfunction(this.email.Text, "您在科研项目管理系统中的资料已修改成功！", "申请回执");

                Response.Redirect("../ui.aspx");
            }
            else
            {
                conn.Close();
                conn.Dispose();
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"请输入正确的原密码！\")</script>");
            }
        }
        catch(Exception ex)
        {
            conn.Close();
            conn.Dispose();
            Response.Write("<script>alert(\"" + ex.Message + "\")</script>");
            //Response.Redirect("../error.aspx");
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