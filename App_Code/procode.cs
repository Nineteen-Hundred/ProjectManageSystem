using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Collections;

/// <summary>
/// procode 主要用于通过后台的数据库操作生成前台可见的HTML代码，实现页面元素的动态更新
/// </summary>
namespace procode
{
    public class codemaker
    {
        public string member(string username, string name, string money, string spent, string unit, string email,bool flag)
        {
            string result;

            if (flag)
            {
                string color;
                
                if (Convert.ToDouble(money) > Convert.ToDouble(spent))
                {
                    color = "blue";
                }
                else
                {
                    color = "red";
                }
                result = "<li><a href = \"javascript:void(0);\" ><span class=\"image\"><img  style=\"width:100px;height:100px\" src = \"../pictures/" + username + ".jpg\" alt=\"\" /></span><span class=\"title\">" + name + "</span>"
                        + "<span class=\"status\"><div class=\"field\"><span class=\"badge badge-green\">单位</span> &nbsp;&nbsp;&nbsp;" + unit + "</span></div>"
                        + "<div class=\"field\"><span class=\"badge\">邮箱</span> &nbsp;&nbsp;&nbsp;" + email + "</span></div><div class=\"field\"><span class=\"badge badge-" + color + "\" > 经费</span> &nbsp;&nbsp;&nbsp;" + money + "元（已使用" + spent
                        + "元）</span></div></span></a></li>";
            }
            else
            {
                result = "<li><a href = \"javascript:void(0);\" ><span class=\"image\"><img  style=\"width:100px;height:100px\" src = \"../pictures/" + username + ".jpg\" alt=\"\" /></span><span class=\"title\">" + name + "</span>"
                        + "<span class=\"status\"><div class=\"field\"><span class=\"badge badge-green\">单位</span> &nbsp;&nbsp;&nbsp;" + unit + "</span></div>"
                        + "<div class=\"field\"><span class=\"badge\">邮箱</span> &nbsp;&nbsp;&nbsp;" + email + "</span></div><div class=\"field\"><span class=\"badge badge-red\" > 经费</span> &nbsp;&nbsp;&nbsp;暂无权限</span></div></span></a></li>";
            }

            return result;
        }

        public string myself(string username, string name, string phone, string unit, string email)
        {
            string result = "<li class=\"current\"><a href = \"javascript:void(0);\" ><span class=\"image\"><img  style=\"width:100px;height:100px\" src = \"../pictures/" + username + ".jpg\" alt=\"\" /></span><span class=\"title\">" + name + "</span>"
                    + "<span class=\"status\"><div class=\"field\"><span class=\"badge badge-green\">单位</span> &nbsp;&nbsp;&nbsp;" + unit + "</span></div>"
                    + "<div class=\"field\"><span class=\"badge badge-blue\">电话</span> &nbsp;&nbsp;&nbsp;" + phone + "</span></div><div class=\"field\"><span class=\"badge\">邮箱</span>&nbsp;&nbsp;&nbsp;" + email
                    + "</span></div></span></a></li>";
            return result;
        }

        public string progress(string percent, string info)
        {
            string color;
            string code = "";

            int number = Convert.ToInt32(percent);

            if(number < 30)
            {
                color = "danger";
            }
            else if(number < 70)
            {
                color = "warning";
            }
            else
            {
                color = "success";
            }

            code = "<li><a><span class=\"header clearfix\"><span class=\"pull-left\">" + info + "</span><span class=\"pull-right\">" + percent + "%</span></span>"
                  + "<div class=\"progress progress-striped active\"><div class=\"progress-bar progress-bar-" + color + "\" role=\"progressbar\" aria-valuenow=\"70\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"width:" + percent + "%;\">"
                  + "<span class=\"sr-only\">" + percent + "% Complete</span></div></div></a></li>";

            return code;
        }

        public string webchat(string username, string name, string date, string message)
        {
            string result = "<li><a><img src = \"../pictures/" + username + ".jpg\"/><span class=\"body\"><span class=\"from\">" + name + "</span><span class=\"message\" style=\"text-indent:1em\"> "
                            + message + "</span> <span class=\"time\"><i class=\"fa fa-clock-o\"></i>"
                            + "<span> " + date + "</span></span></span> </a></li>";
            return result;
        }

        public string activity(string content, string date, string type)
        {
            string color;
            string kind;
            
            if(type=="经费")
            {
                return "";
            }
            else if(type=="成员")
            {
                color = "primary";
                kind = "edit";
            }
            else if(type=="成果")
            {
                color = "success";
                kind = "check";
            }
            else
            {
                color = "danger";
                kind = "picture-o";
            }

            string result = "<div class=\"feed-activity clearfix\"><div><i class=\"pull-left roundicon fa fa-" + kind + " btn btn-" + color + "\"></i>" + content + "<br/>"
                            + "</div><div class=\"time\"><i class=\"fa fa-clock-o\"></i>" + date + "</div></div>";

            return result;
        }

        public string chatwindow(string name, string content, string date, string username)
        {
            string result;

            result = "<li class=\"media\"><a class=\"pull-left\"><img class=\"media-object\" style=\"width:70px;height:70px\" alt=\"Generic placeholder image\" src=\"../pictures/" + username + ".jpg\"></a>"
                        + "<div class=\"media-body chat-pop\"><h4 class=\"media-heading\">" + name + "<span class=\"pull-right\"><i class=\"fa fa-clock-o\"></i>"
                        + "<abbr class=\"timeago\" title=\"Oct 9, 2013\">" + date + "</abbr> </span></h4>"
                        + "<p>" + content + "</p></div></li>";

            return result;
        }

        public string chatwindow2(string name, string content,string date, string username)
        {
            string result = "<li class=\"media\"><a class=\"pull-right\"><img class=\"media-object\" style=\"height:70px;width:70px\" alt=\"Generic placeholder image\" src=\"../pictures/" + username + ".jpg\">"
                            + "</a><div class=\"pull-right media-body chat-pop mod\"><h4 class=\"media-heading\">"
                            + name + "<span class=\"pull-left\"><abbr class=\"timeago\" title=\"Oct 10, 2013\" >" + date + "</abbr> <i class=\"fa fa-clock-o\"></i></span></h4></h4>"
                            + content + "</div></li>";

            return result;
        }

        public string qklw(string no, string info, string path, string name, string date)
        {
            string result = "<tr><td>" + no + "</td><td>" + date + "</td><td>" + name + "</td><td style = \"width:800px\">" + info + "</td><td><a href=\"../files/" + path + "\">下载</a> </td>"
                + "<td><a href=\"delete.aspx?path=" + path + "&info=" + info + "\">删除</a></td></tr>";
                                                
            return result;
        }

        public string qklwpt(string no, string info, string path, string name, string date)
        {
            string result = "<tr><td>" + no + "</td><td>" + date + "</td><td>" + name + "</td><td style = \"width:800px\">" + info + "</td><td><a>下载</a> </td>"
                + "<td><a>删除</a></td></tr>";

            return result;
        }

        public string gallerytitle(string type, int i)
        {
            ArrayList color = new ArrayList();
            color.Add("info");
            color.Add("danger");
            color.Add("success");
            color.Add("warning");

            string result = "<a style=\"margin-right:4px\" href =\"#\" class=\"btn btn-" + color[i%4].ToString() + "\" data-filter=\".type" + i.ToString() + "\">" + type + "</a>";

            return result;
        }

        public string galleryoption(string type, int i)
        {
            return "<option value = \".type" + i.ToString() + "\"> " + type + " </option>";
        }

        public string gallerycontent(string type, string title, string filename, int i)
        {
            string result = "<div class=\"col-md-3 type" + i.ToString() + " item\"><div class=\"filter-content\"><img src = \"../pictures/" + filename + "\" alt=\"\" class=\"img-responsive\" />"
                            + "<div class=\"hover-content\"><h4>" + title + "</h4><a class=\"btn btn-success hover-link\"><i class=\"fa fa-edit fa-1x\"></i>"
                            + "</a><a class=\"btn btn-warning hover-link colorbox-button\" href=\"../pictures/" + filename + "\" title=\"" + title + "\"><i class=\"fa fa-search-plus fa-1x\"></i>"
                            + "</a></div></div></div>";

            return result;
        }

        public string weekreport(string no, string name, string info, string path, string date,string check)
        {
            string result = "<tr><td>" + no + "</td><td>" + name + "</td><td>" + date + "</td><td>" + info + "</td><td>" + check + "</td><td><a href=\"../files/" + path + "\">下载</a> </td>"
                + "<td><a href=\"delete1.aspx?path=" + path + "&info=" + info + "\">删除</a></td></tr>";

            return result;
        }

        public string mingxi(string num, string info, string time, string money, bool isGuest, string name, string projectno,bool isAdmin)
        {
            string canOpera;

            if(isGuest&&!isAdmin)
            {
                canOpera = "删除";
            }
            else
            {
                canOpera = "<a href=\"moneydelete.aspx?time=" + time + "&prono=" + projectno + "\">删除</a>";
            }

            string result = "<tr><td>" + num + "</td><td>" + name + "</td><td>" + money + "元" + "，备注：" + info + "</td><td>" + time + "</td><td>" + canOpera + "</td></tr>";

            return result;
        }

        public string records(string date, string content)
        {
            return "<li class=\"cls highlight\"><p class=\"date\">" + date + "</p><div class=\"more\"><p>" + content + "</p></div></li>";
        }
    }
}