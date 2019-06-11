<%@ Page Language="C#" Debug="true" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>注册新用户</title>
    <link href="styles/register.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="title" class="title">
    <a href="Default.aspx"><img src="image/register.png"/></a>
    </div>

    <div id="content" class="content" runat="server" style="background-color:Orange">
    <p>注意事项：</p>
    <p>1、请认真填写下列内容并确保真实性，否则账户将面临注销风险。</p>
    <p>2、账户信息一经注册，将不能自行修改，请谨慎填写。申请后如需修改，请与管理员联系。</p>
    <p>3、上传照片推荐200*200分辨率，过大或过小分辨率将导致图片上传失败或难以辨认，</p>
    <p>4、目前只支持jpg格式的图片类型。</p>
    </div>

    <div id="content2" class="content" runat="server" style="border:0">
    <p></p>用户名：<asp:TextBox ID="username" runat="server"></asp:TextBox> <a>( 用户名可取7-14位 ）</a>
    <p></p>密&nbsp;&nbsp; 码：<asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox><a>（ 密码由6-16位数字和字母组成 ）</a>
    <p></p>确&nbsp;&nbsp; 认：<asp:TextBox ID="ensure" runat="server" TextMode="Password"></asp:TextBox>
    <p></p>姓&nbsp;&nbsp; 名：<asp:TextBox ID="name" runat="server" Width=100px></asp:TextBox>
    <p></p>性&nbsp;&nbsp; 别：<asp:RadioButton ID="male" Text="男" runat="server" GroupName="gender" /><asp:RadioButton ID="female" Text="女" runat="server" GroupName="gender" />
    <p></p>单&nbsp;&nbsp; 位：<asp:TextBox ID="company" runat="server" Width=250px></asp:TextBox>
    <p></p>电&nbsp;&nbsp; 话：<asp:TextBox ID="phone" runat="server" Width=200px></asp:TextBox>
    <p></p>邮&nbsp;&nbsp; 箱：<asp:TextBox ID="email" runat="server" Width=250px></asp:TextBox><a>（ 项目信息和消息通知将发送到该邮箱，请如实填写 ）</a>
    <p></p>身份证：<asp:TextBox ID="idcard" runat="server" Width=250px></asp:TextBox>
    <p></p>头&nbsp;&nbsp; 像：<asp:FileUpload ID="shangchuan" runat="server" /><a>（ 推荐使用200*200分辨率图片 ）</a>
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
    <p></p><asp:Button ID="submit" runat="server" Text="确认注册" 
        style="width:120px;height:30px;font-size:15px; margin-bottom:20px" 
        onclick="submit_Click" />
    </form>
    <hr />
    
    <%Response.Write(information()); %>
</body>
</html>
