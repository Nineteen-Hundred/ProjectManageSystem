<%@ Page Language="C#" AutoEventWireup="true" CodeFile="apply.aspx.cs" Inherits="apply" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>项目申请</title>
    <link  href = "Styles/register.css" type="text/css" rel="Stylesheet"/>
    <link href="js/jquery-ui.css" rel="Stylesheet" />
    <script src="js/external/jquery/jquery.js"></script>
    <script src="js/jquery-ui.js"></script>
    <script>
        $(function() {
            $( "#datepicker" ).datepicker();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="title" class="title">
        <a href="ui.aspx"><img src="apply.fw.png"/></a>
    </div>

    <div id="content" class="content" runat="server" style="background-color:Orange">
    <p>注意事项：</p>
    <p>1、请认真填写下列内容并确保真实性，否则项目将面临注销风险。</p>
    <p>2、管理员可自行修改项目部分信息，如有疑问可向平台负责人咨询。</p>
    <p>3、如果管理员账号信息泄露，请及时与平台负责人联络。</p>
    </div>
    
    <div id="content2" class="content" runat="server" style="border:0">
        <p></p>项目账号：<asp:TextBox ID="prono" runat="server" ReadOnly="true" Width="200px"></asp:TextBox><a>（项目账号不可修改）</a>
        <p></p>项目密码：<asp:TextBox ID="password" TextMode="Password" Width="240px" runat="server"></asp:TextBox>
        <p></p>确认密码：<asp:TextBox ID="ensure" TextMode="Password" Width="240px" runat="server"></asp:TextBox>
        <p></p>项目名称：<asp:TextBox ID="proname" runat="server" Width="240px"></asp:TextBox>
        <p></p>初始经费：<asp:TextBox ID="promoney" runat="server" Width="100px"></asp:TextBox> 万元
        <p></p>起始日期：<asp:TextBox ID="datepicker" runat="server" Width="240px"></asp:TextBox>
        <p></p>项目简介：<asp:TextBox ID="intro" runat="server" TextMode="MultiLine" Rows="6" Columns="36"></asp:TextBox>
        <p></p><asp:Button ID="submit" runat="server" Text="点击申请" 
            style="width:120px;height:30px;margin-left:120px" onclick="submit_Click" />
    </div>
    </form>
    <hr />
     
</body>
</html>
