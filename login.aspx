<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>项目登录界面</title>
    <link href="styles/default.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="back" style="position:absolute;width:99%;height:98%">
    <div style="position:fixed">
    <img src="http://rs.xidian.edu.cn/data/attachment/forum/201412/02/161653nysryac447rybqnz.jpg" style="position:fixed;width:99%;height:98%" />
    </div>
    <div style="position:absolute;width:300px;left:40%;top:40%;text-align:center">

    <asp:DropDownList ID="prono" runat="server" CssClass="inputstyle"></asp:DropDownList><br /><br />
    <asp:TextBox ID="password"  runat="server" Text="请输入密码" class="inputstyle" TextMode="Password"></asp:TextBox><br /><br />
    <asp:Button ID="submit" Text="登录" runat="server" class="buttonstyle" 
            onclick="submit_Click"/><br /><br />
    <a href="apply.aspx">点击注册</a>

    </div>
    
    </div>
    
    </form>
</body>
</html>
