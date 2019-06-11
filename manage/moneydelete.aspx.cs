using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class manage_moneydelete : System.Web.UI.Page
{
    private bool isRecord = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["yonghuming"].ToString() != Session["guanliyuan"].ToString())
        {
            Response.Redirect("weekreport.aspx");
        }

        string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "SELECT name,money,flag FROM moneyrecord WHERE dateandtime = '" + Request.QueryString["time"] + "' and projectno='" + Request.QueryString["prono"] + "'";

        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            isRecord = true;
            this.info.Text = dr[0].ToString().Trim() + " " + dr[1].ToString().Trim() + " " + dr[2].ToString().Trim();
            dr.Close();
        }
        else
        {
            isRecord = false;
            try
            {
                dr.Close();
                cmd.CommandText = "SELECT name,money,flag FROM bonus WHERE dateandtime = '" + Request.QueryString["time"] + "' and projectno='" + Request.QueryString["prono"] + "'";
                dr = cmd.ExecuteReader();
                dr.Read();
                this.info.Text = dr[0].ToString().Trim() + " " + dr[1].ToString().Trim() + " " + dr[2].ToString().Trim();
            }
            catch (Exception ex)
            {
                info.Text = ex.Message;
            }
        }
        dr.Close();

        conn.Close();
        conn.Dispose();
    }
    protected void delete_Click(object sender, EventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(connStr);
        conn.Open();

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        string databasename = isRecord ? "moneyrecord" : "bonus";
        cmd.CommandText = "DELETE " + databasename + " WHERE dateandtime = '" + Request.QueryString["time"] + "' and projectno='" + Request.QueryString["prono"] + "'";

        try
        {
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            conn.Close();
            conn.Dispose();
            Response.Write("<script>alert(\"删除出现错误\")</script>");
        }
        Response.Redirect("money.aspx");
    }
    protected void return_Click(object sender, EventArgs e)
    {
        Response.Redirect("money.aspx");
    }
}