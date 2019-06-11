using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Collections;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
///security 的摘要说明
/// </summary>
namespace securitycheck
{
    public class security
    {
        public int CheckBadStr(string str)
        {
            //检测参数是否为空
            if (str.Trim() != "" && str.Length > 0)
            {
                //检测是否有sql危险字符
                if (Regex.IsMatch(str, @"[-|;|\/|\(|\)|\[|\]|\}|\{|%|\*|!|\']"))
                {
                    return 1;
                }

                else
                {
                    //判断是否有非法字符
                    
                    ArrayList badStr = new ArrayList();
                    
                    badStr.Add("xp_cmdshell");
                    badStr.Add("truncate");
                    badStr.Add("net user");
                    badStr.Add("exec");
                    badStr.Add("net localgroup");
                    badStr.Add("select");
                    badStr.Add("asc");
                    badStr.Add("char");
                    badStr.Add("mid");
                    badStr.Add("insert");
                    badStr.Add("order");
                    badStr.Add("delete");
                    badStr.Add("drop");
                    
                    badStr.Add("1=1");
                    badStr.Add("1=2");
                    

                    string tempStr = str.ToLower();
                    for (int i = 0; i < badStr.Count; i++)
                    {
                        if (tempStr.IndexOf(badStr[i].ToString()) > -1)
                        {
                            return 1;

                        }

                    }

                    return 0;
                }
            }

            else
            {
                return 2;
            
            }
        }

        public int flag(string username, string ipdizhi)
        {
            int i1;
            int i2;

            string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT COUNT(*) FROM danger WHERE username = '" + username + "'";

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                i1 = Convert.ToInt32(dr[0].ToString().Trim());
            }
            else
            {
                i1 = 0;
            }

            dr.Close();

            cmd.CommandText = "SELECT COUNT(*) FROM danger WHERE ipaddress = '" + ipdizhi + "'";

            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                i2 = Convert.ToInt32(dr[0].ToString().Trim());
            }
            else
            {
                i2 = 0;
            }

            if (i1 > 2 || i2 > 2)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        // 这一函数用以判断期刊论文的格式是否正确
        public bool lunwen(string format)
        {
            ArrayList standard = new ArrayList();
            standard.Add(".doc");
            standard.Add(".docx");
            standard.Add(".xls");
            standard.Add(".xlsx");
            standard.Add(".ppt");
            standard.Add(".pptx");
            standard.Add(".zip");
            standard.Add(".rar");
            standard.Add(".pdf");

            string temp = format.ToLower();
            int i;

            for(i=0;i<=standard.Count-1;i++)
            {
                if(format==standard[i].ToString())
                {
                    break;
                }
            }

            if(i==standard.Count)
            {
                return false;  // 合规为真
            }
            else
            {
                return true;  // 合规为假
            }
        }

        public bool zhuanli(string format)
        {
            ArrayList standard = new ArrayList();
            standard.Add(".jpg");
            standard.Add(".png");
            standard.Add(".bmp");
            standard.Add(".zip");
            standard.Add(".rar");
            standard.Add(".pdf");

            string temp = format.ToLower();
            int i;

            for(i=0;i<=standard.Count-1;i++)
            {
                if(format==standard[i].ToString())
                {
                    break;
                }
            }

            if(i==standard.Count)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool pictures(string format)
        {
            ArrayList standard = new ArrayList();
            standard.Add(".jpg");
            standard.Add(".png");
            standard.Add(".bmp");

            string temp = format.ToLower();
            int i;

            for (i = 0; i <= standard.Count - 1; i++)
            {
                if (format == standard[i].ToString())
                {
                    break;
                }
            }

            if (i == standard.Count)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool qita(string format)
        {
            ArrayList standard = new ArrayList();
            standard.Add(".doc");
            standard.Add(".docx");
            standard.Add(".xls");
            standard.Add(".xlsx");
            standard.Add(".ppt");
            standard.Add(".pptx");
            standard.Add(".zip");
            standard.Add(".rar");
            standard.Add(".pdf");
            standard.Add(".jpg");
            standard.Add("png");
            standard.Add(".bmp");

            string temp = format.ToLower();
            int i;

            for (i = 0; i <= standard.Count - 1; i++)
            {
                if (format == standard[i].ToString())
                {
                    break;
                }
            }

            if (i == standard.Count)
            {
                return false;  // 合规为真
            }
            else
            {
                return true;  // 合规为假
            }
        }
    }
}