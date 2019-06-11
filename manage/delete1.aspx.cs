using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class manage_delete1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["yonghuming"].ToString() != Session["guanliyuan"].ToString())
        {
            Response.Redirect("weekreport.aspx");
        }
        this.info.Text = "您要删除的文件是：" + Request.QueryString["path"] + "</br>文件信息为：" + Request.QueryString["info"] + "</br>";
    }
    protected void delete_Click(object sender, EventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();

        int i = 0;

        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE weekreport WHERE projectno='" + Session["xiangmuhao"].ToString() + "' and filename='" + Request.QueryString["path"].ToString()
                + "' and proname ='" + Request.QueryString["info"].ToString() + "'";

            i = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            conn.Close();
            conn.Dispose();
        }
        finally
        {
            conn.Close();
            conn.Dispose();

            if (i > 0)
            {
                Response.Redirect("weekreport.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script>alert(\"操作失败，请检查文件信息！\")</script>");
            }
        }
    }
    protected void return_Click(object sender, EventArgs e)
    {
        Response.Redirect("weekreport.aspx");
    }
}