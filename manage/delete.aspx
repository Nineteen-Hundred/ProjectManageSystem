<%@ Page Language="C#" AutoEventWireup="true" CodeFile="delete.aspx.cs" Inherits="manage_delete" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="content-type" content="text/html; charset=gb2312" />
<meta charset="gb2312" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center;margin-top:300px">
        <asp:Label ID="info" runat="server" style="font-size:20px;line-height:50px"></asp:Label>
        <asp:Button id="delete" runat="server" Text="确认删除" style="width:80px;height:35px;margin-top:20px;margin-right:20px" onclick="delete_Click" />
        <asp:Button ID="return" runat="server" Text="返回列表" style="width:80px;height:35px" onclick="return_Click" />
    </div>
    </form>
</body>
</html>
