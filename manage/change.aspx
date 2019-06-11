<%@ Page Language="C#" AutoEventWireup="true" CodeFile="change.aspx.cs" Inherits="manage_change" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改个人信息</title>
    <link href="../styles/register.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">


    <div id="content" class="content" runat="server" style="background-color:Orange">
    <p>注意事项：</p>
    <p>1、信息修改后将造成部分已注册项目信息混乱，请谨慎操作。</p>
    <p>2、请输入正确的密码，否则操作不能进行。</p>
    <p>3、修改操作将以邮箱形式通知用户，因此邮箱信息不能修改。</p>
    <p><a href="../ui.aspx">点击此处返回</a></p>
    </div>

    <div id="content2" class="content" runat="server" style="border:0">
    <p></p>用户名：<asp:TextBox ID="username" runat="server" ReadOnly = "true"></asp:TextBox> <a>( 用户名可取7-14位 ）</a>
    <p></p>原密码：<asp:TextBox ID="yuan" runat="server" textmode="Password"></asp:TextBox>
    <p></p>密&nbsp;&nbsp; 码：<asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox><a>（ 密码由6-16位数字和字母组成 ）</a>
    <p></p>确&nbsp;&nbsp; 认：<asp:TextBox ID="ensure" runat="server" TextMode="Password"></asp:TextBox>
    <p></p>姓&nbsp;&nbsp; 名：<asp:TextBox ID="name" runat="server" Width=100px></asp:TextBox>
    <p></p>性&nbsp;&nbsp; 别：<asp:RadioButton ID="male" Text="男" runat="server" GroupName="gender" /><asp:RadioButton ID="female" Text="女" runat="server" GroupName="gender" />
    <p></p>单&nbsp;&nbsp; 位：<asp:TextBox ID="company" runat="server" Width=250px></asp:TextBox>
    <p></p>电&nbsp;&nbsp; 话：<asp:TextBox ID="phone" runat="server" Width=200px></asp:TextBox>
    <p></p>邮&nbsp;&nbsp; 箱：<asp:TextBox ID="email" runat="server" Width=200px ReadOnly="true"></asp:TextBox>
    <p></p>身份证：<asp:TextBox ID="idcard" runat="server" Width=250px></asp:TextBox>
    <p></p>头&nbsp;&nbsp; 像：<asp:FileUpload ID="shangchuan" runat="server" /><a>（ 推荐使用120*150分辨率图片，仅支持jpg格式 ）</a>
    <p></p>
        <asp:RequiredFieldValidator ID="usernameenen" runat="server" 
            ErrorMessage="用户名未填写" Display="None" ControlToValidate="username"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="passwordenenen" runat="server" 
            ErrorMessage="密码未填写" Display="None" ControlToValidate="password"></asp:RequiredFieldValidator>
        <asp:CustomValidator ID="usernameen" runat="server"  ErrorMessage="用户名格式错误" 
            onservervalidate="CustomValidator1_ServerValidate" 
            ControlToValidate="username" Display="None"></asp:CustomValidator>
        <asp:CustomValidator ID="passworden" runat="server" ErrorMessage="密码格式错误" 
           Display="None" ControlToValidate="password" 
            onservervalidate="passworden_ServerValidate"></asp:CustomValidator>
        <asp:CompareValidator ID="passwordenen" runat="server" ErrorMessage="两次输入密码不一致" 
           Display="None" ControlToCompare="password" ControlToValidate="ensure"></asp:CompareValidator>
        <asp:RequiredFieldValidator ID="nameen" runat="server" ErrorMessage="姓名栏未填写" 
           Display="None" ControlToValidate="name"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="companyen" runat="server" ErrorMessage="单位栏未填写" 
          Display="None"  ControlToValidate="company"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="phoneen" runat="server" 
          Display="None"  ErrorMessage="电话号码未填写" ControlToValidate="phone"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="emailen" runat="server" 
          Display="None"  ErrorMessage="邮箱格式错误" ControlToValidate="email" 
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ID="idcarden" runat="server" 
           Display="None" ErrorMessage="身份证格式错误" ControlToValidate="idcard" 
            ValidationExpression="\d{17}[\d|X]|\d{15}"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="emailenen" runat="server" 
            ErrorMessage="邮箱地址未填写" Display="None" ControlToValidate="email"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="idcardenen" runat="server" 
            ErrorMessage="身份证号码未填写" Display="None" ControlToValidate="idcard"></asp:RequiredFieldValidator>
        <asp:ValidationSummary ID="ValidationSummary1" style="font-size:small;color:Red;font-family:宋体" runat="server" HeaderText="错误信息"                                                                                                                                                                                                                                                                                                                                                                                                                                                      />
    </div>
    <p></p><asp:Button ID="submit" runat="server" Text="修改信息" 
        onclick="submit_Click" />
    </form>
</body>
</html>
