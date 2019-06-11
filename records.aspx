<%@ Page Language="C#" AutoEventWireup="true" CodeFile="records.aspx.cs" Inherits="records" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>公告栏记录</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="content" style="width: 1200px; margin-top: 40px">
        <div class="wrapper">
            <div class="light">
                <i></i>
            </div>
            <hr class="line-left" />
            <hr class="line-right" />
            <div class="main">
                <h1 class="title">
                    公告历史记录</h1>
                <div class="year">
                    <h2>
                        <a href="#">V 2.0版本<i></i></a></h2>
                    <div class="list">
                        <ul>
                            <%=result %>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript">
    $(".main .year .list").each(function (e, target) {
        var $target = $(target),
	        $ul = $target.find("ul");
        $target.height($ul.outerHeight()), $ul.css("position", "absolute");
    });
    $(".main .year>h2>a").click(function (e) {
        e.preventDefault();
        $(this).parents(".year").toggleClass("close");
    });
</script>
</html>
