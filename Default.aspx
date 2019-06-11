<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>科研项目管理系统 V1.0</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <link rel="stylesheet" href="css/style1.css" />
    <!--<link href="styles/default.css" type="text/css" rel="Stylesheet" />-->
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        document.write("<div class=\"login-container\" style=\"margin-top:" + (document.documentElement.clientHeight - 420) / 2 + "px\">");
    </script>
    <!--<div class="login-container">-->
        <h1 style="line-height:50px">
            科研项目管理系统 <br />V 2.0</h1>
        <div class="connect">
            <p>
                全新上线</p>
        </div>
        <form action="" method="post" id="loginForm">
        <div>
            <asp:TextBox runat="server" ID="username" name="username" class="username" placeholder="用户名"
                autocomplete="off"></asp:TextBox>
        </div>
        <div>
            <asp:TextBox ID="password" type="password" runat="server" name="password" class="password" placeholder="密码"
                oncontextmenu="return false" onpaste="return false"></asp:TextBox>
        </div>
        <asp:Button ID="submit" type="submit" OnClick="submit_Click" class="register-tis" runat="server" Text="登录系统">
        </asp:Button>
        </form>
		
		<asp:Button ID="register" onClick="register_Click" class="register-tis" runat="server" Text="注册新帐号"></asp:Button>
    </div>
    </form>
    <script src="js/jquery.min.js"></script>
    <script src="js/common.js"></script>
    <!--背景图片自动更换-->
    <script src="js/supersized.3.2.7.min.js"></script>
    <script src="js/supersized-init.js"></script>
    <!--表单验证-->
    <script src="js/jquery.validate.min.js?var1.14.0"></script>
</body>
</html>
