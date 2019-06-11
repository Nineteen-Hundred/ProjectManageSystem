<%@ Page Language="C#" AutoEventWireup="true" CodeFile="manager.aspx.cs" Inherits="manager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>后台管理登录页面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:1300px;margin-left:auto;margin-right:auto;">
        <div style="top:25%;position:absolute;left:40%;text-align:center;border:2px dotted Orange;padding:15px 15px 15px 15px">
            <img src="image/houtai.png" style="margin-bottom:30px"/><br />
            请输入账号：<asp:TextBox ID="username" runat="server" style="width:180px;height:30px;margin-bottom:30px;"></asp:TextBox><br />
            请输入密码：<asp:TextBox ID="password" runat="server" TextMode="Password" style="width:180px;height:30px;margin-bottom:30px;"></asp:TextBox><br />
            <asp:Button ID="submit" text="点击登录" runat="server" 
                style="width:80px;height:36px;margin-right:40px;" onclick="submit_Click" />
            <asp:Button ID="Button1" text="返回首页" runat="server" 
                style="width:80px;height:36px" onclick="Button1_Click" />
        </div>
    </div>
    </form>
</body>
</html>
