<%@ Page Language="C#" AutoEventWireup="true" CodeFile="upload.aspx.cs" Inherits="manage_upload" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta http-equiv="content-type" content="text/html; charset=UTF-8">
	<meta charset="utf-8">
	<title>上传成果——科研项目管理系统</title>
	<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1, user-scalable=no">
	<meta name="description" content="">
	<meta name="author" content="">
	<!-- STYLESHEETS --><!--[if lt IE 9]><script src="js/flot/excanvas.min.js"></script><script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script><script src="http://css3-mediaqueries-js.googlecode.com/svn/trunk/css3-mediaqueries.js"></script><![endif]-->
	<link rel="stylesheet" type="text/css" href="css/cloud-admin.css" >
	<link rel="stylesheet" type="text/css"  href="css/themes/default.css" id="skin-switcher" >
	<link rel="stylesheet" type="text/css"  href="css/responsive.css" >
	
    <!-- ANIMATE -->
    <link rel="stylesheet" type="text/css" href="css/animatecss/animate.min.css" />
    <!-- DATE RANGE PICKER -->
    <link rel="stylesheet" type="text/css" href="js/bootstrap-daterangepicker/daterangepicker-bs3.css" />
    <!-- TODO -->
    <link rel="stylesheet" type="text/css" href="js/jquery-todo/css/styles.css" />
    <!-- FULL CALENDAR -->
    <link rel="stylesheet" type="text/css" href="js/fullcalendar/fullcalendar.min.css" />
    <!-- GRITTER -->
    <link rel="stylesheet" type="text/css" href="js/gritter/css/jquery.gritter.css" />
	<link href="font-awesome/css/font-awesome.min.css" rel="stylesheet">
	<!-- DATE RANGE PICKER -->
	<link rel="stylesheet" type="text/css" href="js/bootstrap-daterangepicker/daterangepicker-bs3.css" />
	<!-- TYPEAHEAD -->
	<link rel="stylesheet" type="text/css" href="js/typeahead/typeahead.css" />
	<!-- FILE UPLOAD -->
	<link rel="stylesheet" type="text/css" href="js/bootstrap-fileupload/bootstrap-fileupload.min.css" />
	<!-- SELECT2 -->
	<link rel="stylesheet" type="text/css" href="js/select2/select2.min.css" />
	<!-- UNIFORM -->
	<link rel="stylesheet" type="text/css" href="js/uniform/css/uniform.default.min.css" />
	<!-- JQUERY UPLOAD -->
	<!-- blueimp Gallery styles -->
	<link rel="stylesheet" href="js/blueimp/gallery/blueimp-gallery.min.css">
	<!-- CSS to style the file input field as button and adjust the Bootstrap progress bars -->
	<link rel="stylesheet" href="js/jquery-upload/css/jquery.fileupload.css">
	<link rel="stylesheet" href="js/jquery-upload/css/jquery.fileupload-ui.css">

    <!-- BOOTSTRAP SWITCH -->
    <link rel="stylesheet" type="text/css" href="js/bootstrap-switch/bootstrap-switch.min.css" />
    <!-- FLAGS -->
    <link rel="stylesheet" type="text/css" href="css/flags/flags.min.css" />
	<!-- FONTS -->
	<!--<link href='http://fonts.useso.com/css?family=Open+Sans:300,400,600,700' rel='stylesheet' type='text/css'>-->
</head>
<body>
    <form id="form1" runat="server">
    <!-- HEADER -->
    <header class="navbar clearfix navbar-fixed-top" id="header">
        <div class="container">
            <div class="navbar-brand">
                <!-- COMPANY LOGO -->
                <a href="frontpage.aspx">
                    <img src="img/logo/logo.png" alt="Cloud Admin Logo" class="img-responsive" height="30" width="120">
                </a>
                <!-- /COMPANY LOGO -->
                <!-- TEAM STATUS FOR MOBILE -->
                <div class="visible-xs">
                    <a href="#" class="team-status-toggle switcher btn dropdown-toggle">
                        <i class="fa fa-users"></i>
                    </a>
                </div>
                <!-- /TEAM STATUS FOR MOBILE -->
                <!-- SIDEBAR COLLAPSE -->
                <div id="sidebar-collapse" class="sidebar-collapse btn">
                    <i class="fa fa-bars"
                       data-icon1="fa fa-bars"
                       data-icon2="fa fa-bars"></i>
                </div>
                <!-- /SIDEBAR COLLAPSE -->
            </div>
            <!-- NAVBAR LEFT -->
            <ul class="nav navbar-nav pull-left hidden-xs" id="navbar-left">
                <li class="dropdown">
                    <a href="#" class="team-status-toggle dropdown-toggle tip-bottom" data-toggle="tooltip" title="Toggle Team View">
                        <i class="fa fa-users"></i>
                        <span class="name" style="font-family:微软雅黑">成员信息</span>
                        <i class="fa fa-angle-down"></i>
                    </a>
                </li>
                <li class="dropdown">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                        <i class="fa fa-cog"></i>
                        <span class="name">界面皮肤</span>
                        <i class="fa fa-angle-down"></i>
                    </a>
                    <ul class="dropdown-menu skins">
                        <li class="dropdown-title">
                            <span><i class="fa fa-leaf"></i> 主题皮肤</span>
                        </li>
                        <li><a href="#" data-skin="default">默认</a></li>
                        <li><a href="#" data-skin="night">暗夜</a></li>
                        <li><a href="#" data-skin="earth">土地</a></li>
                        <li><a href="#" data-skin="utopia">乌托邦</a></li>
                        <li><a href="#" data-skin="nature">自然</a></li>
                        <li><a href="#" data-skin="graphite">石墨</a></li>
                    </ul>
                </li>
                <li class="dropdown">
                    <a href="upload.aspx" class="dropdown-toggle">
                        <i class="fa fa-file-o"></i>
                        <span class="name">上传成果</span>
                        <i class="fa fa-angle-right"></i>
                    </a>
                </li>
            </ul>
            <!-- /NAVBAR LEFT -->
            <!-- BEGIN TOP NAVIGATION MENU -->
            <ul class="nav navbar-nav pull-right">
                <!-- BEGIN NOTIFICATION DROPDOWN -->
                <!-- END NOTIFICATION DROPDOWN -->
                <!-- BEGIN INBOX DROPDOWN -->
                <li class="dropdown" id="header-message">
						<a href="#" class="dropdown-toggle" data-toggle="dropdown">
						<i class="fa fa-envelope"></i>
						<span class="badge"><%=chatnum1 %></span>
						</a>
						<ul class="dropdown-menu inbox">
							<li class="dropdown-title">
								<span><i class="fa fa-envelope-o"></i> 共有 <%=chatnum %> 条未读私信</span>
								<span class="compose pull-right tip-right" title="Compose message"></span>
							</li>
							<%=webchat %>
							<li class="footer">
								<a href="webchatclean.aspx">已阅所有信息 <i class="fa fa-arrow-circle-right"></i></a>
							</li>
						</ul>
					</li>
					<!-- END INBOX DROPDOWN -->
					<!-- BEGIN TODO DROPDOWN -->
					<li class="dropdown" id="header-tasks">
						<a href="#" class="dropdown-toggle" data-toggle="dropdown">
						<i class="fa fa-tasks"></i>
						<span class="badge"><%=tempnum1 %></span>
						</a>
						<ul class="dropdown-menu tasks">
							<li class="dropdown-title">
								<span><i class="fa fa-check"></i> 当前共有 <%=tempnum %> 个任务</span>
							</li>
                            <%=renwu %>
						</ul>
					</li>
                <!-- END TODO DROPDOWN -->
                <!-- BEGIN USER LOGIN DROPDOWN -->
                <li class="dropdown user" id="header-user">
						<a href="#" class="dropdown-toggle" data-toggle="dropdown">
							<img alt="" src="../pictures/<%=Session["yonghuming"].ToString() %>.jpg" />
							<span class="username"><%=Session["xingming"].ToString() %></span>
							<i class="fa fa-angle-down"></i>
						</a>
						<ul class="dropdown-menu">

							<li><a href="change.aspx"><i class="fa fa-cog"></i> &nbsp;账号设置</a></li>
							<li><a href="mailto:tixilinbi123@163.com"><i class="fa fa-cloud-download"></i> &nbsp;联系我们</a></li>
							<li><a href="../ui.aspx"><i class="fa fa-power-off"></i> &nbsp;注销</a></li>
						</ul>
					</li>
                <!-- END USER LOGIN DROPDOWN -->
            </ul>
            <!-- END TOP NAVIGATION MENU -->
        </div>
        <!-- TEAM STATUS -->
        <div class="container team-status" id="team-status">
            <div id="scrollbar">
                <div class="handle">
                </div>
            </div>
            <div id="teamslider">
			  <ul class="team-list">
                  <%=member%>
			  </ul>
			</div>
        </div>
        <!-- /TEAM STATUS -->
    </header>
    <!--/HEADER -->
    <div class="copyrights">Collect from <a href="http://www.cssmoban.com/">企业网站模板</a></div>
    <!-- PAGE -->
    <section id="page">
        <!-- SIDEBAR -->
        <div id="sidebar" class="sidebar sidebar-fixed">
            <div class="sidebar-menu nav-collapse">
                <div class="divide-20"></div>
                <!-- SEARCH BAR -->
                <div id="search-bar">
                    <form action="http://www.baidu.com/baidu" target="_blank">
                        <input class="search" type="text" name="word" placeholder="百度搜索入口"><i class="fa fa-search search-icon"></i>
                    </form>
                </div>
                <!-- /SEARCH BAR -->
                <!-- SIDEBAR QUICK-LAUNCH -->
                <!-- <div id="quicklaunch">
                <!-- /SIDEBAR QUICK-LAUNCH -->
                <!-- SIDEBAR MENU -->
                <ul>
                    <li class="">
                        <a href="frontpage.aspx">
                            <i class="fa fa-tachometer fa-fw"></i> <span class="menu-text">管理首页</span>
                            <span class="selected"></span>
                        </a>
                    </li>
                    <li class="has-sub active">
                        <a href="javascript:;" class="">
                            <i class="fa fa-bookmark-o fa-fw"></i> <span class="menu-text">成果管理</span>
                            <span class="arrow open"></span>
                            <span class="selected"></span>
                        </a>
                        <ul class="sub">
                            <li><a class="" href="result.aspx"><span class="sub-menu-text">查看成果</span></a></li>
                            <li class="current"><a><span class="sub-menu-text">上传成果</span></a></li>
                            <li><a class="" href="weekreport.aspx"><span class="sub-menu-text">周报告</span></a></li>
                        </ul>
                    </li>
                    <li><a class="" href="money.aspx"><i class="fa fa-desktop fa-fw"></i> <span class="menu-text">查看经费</span></a></li>
                    <li><a class="" href="gallery.aspx"><i class="fa fa-picture-o fa-fw"></i> <span class="menu-text">软件截图</span></a></li>
                    <li><a class="" href="control.aspx"><i class="fa fa-envelope-o fa-fw"></i> <span class="menu-text">控制中心<%=tixing %></span></a></li>
                    <li><a class="" href="table.aspx"><i class="fa fa-calendar fa-fw"></i> <span class="menu-text">水晶报表</span></a></li>
                </ul>
                <!-- /SIDEBAR MENU -->
            </div>
        </div>
        <!-- /SIDEBAR -->
        <div id="main-content">
            <!-- SAMPLE BOX CONFIGURATION MODAL FORM-->
            <div class="modal fade" id="box-config" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">Box Settings</h4>
                        </div>
                        <div class="modal-body">
                            Here goes box setting content.
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <button type="button" class="btn btn-primary">Save changes</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /SAMPLE BOX CONFIGURATION MODAL FORM-->
            <div class="container">
                <div class="row">
                    <div id="content" class="col-lg-12" style="min-height:300px">
                        <!-- PAGE HEADER-->
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="page-header">
                                    <!-- STYLER -->
                                    <!-- /STYLER -->
                                    <!-- BREADCRUMBS -->
                                    <ul class="breadcrumb">
                                        <li>
                                            <i class="fa fa-home"></i>
                                            <a href="frontpage.aspx">科研项目管理系统</a>
                                        </li>
                                        <li>上传成果</li>
                                    </ul>
                                    <!-- /BREADCRUMBS -->
                                </div>
                            </div>
                        </div>
                        <!-- /PAGE HEADER -->

                        <!--第一行-->
                        <div class="row">
                            <div class="col-md-6">
                                <div class="box border primary">
                                    <div class="box-title">
                                        <h4 style="font-family:微软雅黑"><i class="fa fa-bars"></i>上传期刊论文</h4>
                                        <div class="tools hidden-xs">
                                            <a href="#box-config" data-toggle="modal" class="config">
                                                <i class="fa fa-cog"></i>
                                            </a>
                                            <a href="javascript:;" class="reload">
                                                <i class="fa fa-refresh"></i>
                                            </a>
                                            <a href="javascript:;" class="collapse">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                            <a href="javascript:;" class="remove">
                                                <i class="fa fa-times"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="box-body big">  
                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">标题：</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox id="qikanlunwen" CssClass="form-control" placeholder="请输入论文标题" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">期刊：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <asp:TextBox id="qikanqikan" CssClass="form-control" placeholder="请输入期刊名称" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">日期：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <asp:TextBox id="qikanriqi" CssClass="form-control" placeholder="请输入发表年月（如 201510）" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">文件：</label>
                                                <div class="col-sm-10">
                                                    <asp:FileUpload ID="qklwfile" style="height:30px;width:380px" runat="server"/>
                                                </div>
                                            </div>
                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label"></label>
                                                <div class="col-sm-10"style="margin-top:20px;margin-bottom:20px">
                                            <div style="text-align:left;">支持格式：doc(x), xls(x), ppt(x), zip, rar, pdf</div> 
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label" style="margin-bottom:20px">确认：</label>
                                                <div class="col-sm-10">
                                                    <div  style="margin-bottom:20px" class="make-switch switch-small" data-on="success" data-off="danger" data-on-label="是" data-off-label="否">
                                                        <asp:CheckBox ID="qklwcheck" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group" style="text-align:center">
                                                <asp:Button ID="qklwsubmit" CssClass="btn btn-danger" Text="上传期刊论文" runat="server" OnClick="qklwsubmit_Click" />
                                            </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="box border orange">
                                    <div class="box-title">
                                        <h4 style="font-family:微软雅黑"><i class="fa fa-envelope"></i>上传会议论文</h4>
                                        <div class="tools hidden-xs">
                                            <a href="#box-config" data-toggle="modal" class="config">
                                                <i class="fa fa-cog"></i>
                                            </a>
                                            <a href="javascript:;" class="reload">
                                                <i class="fa fa-refresh"></i>
                                            </a>
                                            <a href="javascript:;" class="collapse">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                            <a href="javascript:;" class="remove">
                                                <i class="fa fa-times"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="box-body big">
                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">标题：</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox id="huiyilunwen" CssClass="form-control" placeholder="请输入论文标题" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">会议：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <asp:TextBox id="huiyihuiyi" CssClass="form-control" placeholder="请输入会议名称" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">地点：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <asp:TextBox id="huiyididian" CssClass="form-control" placeholder="请输入会议所在地点" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">日期：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <asp:TextBox id="huiyiriqi" CssClass="form-control" placeholder="请输入会议时间（如 201510）" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">照片：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <asp:TextBox id="huiyizhaopian" CssClass="form-control" placeholder="是否上传出席人照片（是 或 否）" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">文件：</label>
                                                <div class="col-sm-10">
                                                    <asp:FileUpload ID="hylwfile" style="height:30px;width:380px" runat="server"/>
                                                </div>
                                            </div>
                                           <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label"></label>
                                                <div class="col-sm-10"style="margin-top:20px;margin-bottom:20px">
                                                   
                                            <div style="text-align:left;">支持格式：doc(x), xls(x), ppt(x), zip, rar, pdf</div> 
                                                </div>
                                            </div>
                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label" style="margin-bottom:20px">确认：</label>
                                                <div class="col-sm-10">
                                                    <div style="margin-bottom:20px" class="make-switch switch-small" data-on="success" data-off="danger" data-on-label="是" data-off-label="否">
                                                        <asp:CheckBox ID="hylwcheck" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group" style="text-align:center">
                                                <asp:Button ID="hylwsubmit" CssClass="btn btn-primary" Text="上传会议论文" runat="server" OnClick="hylwsubmit_Click" />
                                            </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!--第二行-->
                        <div class="row">
                            <div class="col-md-6">
                                <div class="box border red">
                                    <div class="box-title">
                                        <h4 style="font-family:微软雅黑"><i class="fa fa-adjust"></i>上传授权专利</h4>
                                        <div class="tools hidden-xs">
                                            <a href="#box-config" data-toggle="modal" class="config">
                                                <i class="fa fa-cog"></i>
                                            </a>
                                            <a href="javascript:;" class="reload">
                                                <i class="fa fa-refresh"></i>
                                            </a>
                                            <a href="javascript:;" class="collapse">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                            <a href="javascript:;" class="remove">
                                                <i class="fa fa-times"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="box-body big">
                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">姓名：</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox id="shouquanzhuanli" CssClass="form-control" placeholder="请输入姓名" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">号码：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <asp:TextBox id="shouquanhaoma" CssClass="form-control" placeholder="请输入授权号" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">国家：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <asp:TextBox id="shouquanguojia" CssClass="form-control" placeholder="请输入专利类别" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">日期：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <asp:TextBox id="shouquanriqi" CssClass="form-control" placeholder="请输入授权日期" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">文件：</label>
                                                <div class="col-sm-10">
                                                    <asp:FileUpload ID="shouquanzhlfile" style="height:30px;width:380px" runat="server"/>
                                                </div>
                                            </div>

                                           <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label"></label>
                                                <div class="col-sm-10"style="margin-top:20px;margin-bottom:20px">
                                                    <div style="text-align:left;">支持格式：doc(x), xls(x), ppt(x), zip, rar, pdf</div> 
                                                    
                                                </div>
                                            </div>
                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label" style="margin-bottom:20px">确认：</label>
                                                <div class="col-sm-10">
                                                    <div style="margin-bottom:20px" class="make-switch switch-small" data-on="success" data-off="danger" data-on-label="是" data-off-label="否">
                                                        <asp:CheckBox ID="shouquanzhlcheck" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group" style="text-align:center">
                                                <asp:Button ID="shouquanzhlsubmit" CssClass="btn btn-danger" Text="上传授权专利" runat="server" OnClick="shouquanzhlsubmit_Click" />
                                            </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="box border green">
                                    <div class="box-title">
                                        <h4 style="font-family:微软雅黑"><i class="fa fa-bookmark-o"></i>上传申请专利</h4>
                                        <div class="tools hidden-xs">
                                            <a href="#box-config" data-toggle="modal" class="config">
                                                <i class="fa fa-cog"></i>
                                            </a>
                                            <a href="javascript:;" class="reload">
                                                <i class="fa fa-refresh"></i>
                                            </a>
                                            <a href="javascript:;" class="collapse">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                            <a href="javascript:;" class="remove">
                                                <i class="fa fa-times"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="box-body big">
                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">姓名：</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox id="shenqingzhuanli" CssClass="form-control" placeholder="请输入申请者" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">号码：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <asp:TextBox id="shenqinghaoma" CssClass="form-control" placeholder="请输申请号" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">国家：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <asp:TextBox id="shenqingguojia" CssClass="form-control" placeholder="请输入申请专利类型" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">日期：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <asp:TextBox id="shenqingriqi" CssClass="form-control" placeholder="请输入申请日期" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">文件：</label>
                                                <div class="col-sm-10">
                                                    <asp:FileUpload ID="shenqingzhlfile" style="height:30px;width:380px" runat="server"/>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label"></label>
                                                <div class="col-sm-10"style="margin-top:20px;margin-bottom:20px">
                                            <div style="text-align:left;">支持格式：doc(x), xls(x), ppt(x), zip, rar, pdf</div> 
                                                    
                                                </div>
                                            </div>
                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label" style="margin-bottom:20px">确认：</label>
                                                <div class="col-sm-10">
                                                    <div style="margin-bottom:20px" class="make-switch switch-small" data-on="success" data-off="danger" data-on-label="是" data-off-label="否">
                                                        <asp:CheckBox ID="shenqingzhlcheck" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group" style="text-align:center">
                                                <asp:Button ID="shenqingzhlsubmit" CssClass="btn btn-primary" Text="上传申请专利" runat="server" OnClick="shenqingzhlsubmit_Click" />
                                            </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!--第三行-->
                        <div class="row">
                            <div class="col-md-6">
                                <div class="box border blue">
                                    <div class="box-title">
                                        <h4 style="font-family:微软雅黑"><i class="fa fa-calendar"></i>上传软件截图</h4>
                                        <div class="tools hidden-xs">
                                            <a href="#box-config" data-toggle="modal" class="config">
                                                <i class="fa fa-cog"></i>
                                            </a>
                                            <a href="javascript:;" class="reload">
                                                <i class="fa fa-refresh"></i>
                                            </a>
                                            <a href="javascript:;" class="collapse">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                            <a href="javascript:;" class="remove">
                                                <i class="fa fa-times"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="box-body big">
                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">类别：</label>
                                                <div class="col-sm-10">
                                                    <asp:DropDownList ID="ruanjianjietu" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:100px">
                                                <label class="col-sm-2 control-label">添加：</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox CssClass="form-control" ID="rjjtleibie" runat="server" placeholder="请输入新类别"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:20px">
                                                <label class="col-sm-2 control-label">简介：</label>
                                                <div class="col-sm-10"  style="margin-bottom:20px">
                                                    <asp:TextBox CssClass="form-control" ID="rjjttitle" runat="server" placeholder="请输入简介"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">文件：</label>
                                                <div class="col-sm-10">
                                                    <asp:FileUpload ID="rjjtfile" style="height:30px;width:380px" runat="server"/>
                                                </div>
                                            </div>

                                            <div style="font-family:微软雅黑;text-align:center">支持格式：jpg, png</div><br />

                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">确认：</label>
                                                <div class="col-sm-10">
                                                    <div class="make-switch switch-small" data-on="success" data-off="danger" data-on-label="是" data-off-label="否">
                                                        <asp:CheckBox ID="rjjtcheck" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group" style="text-align:center">
                                                <asp:Button ID="rjjtsubmit" CssClass="btn btn-danger" Text="上传软件截图" runat="server" OnClick="rjjtsubmit_Click" />
                                            </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="box border purple">
                                    <div class="box-title">
                                        <h4 style="font-family:微软雅黑"><i class="fa fa-home"></i>上传其他成果</h4>
                                        <div class="tools hidden-xs">
                                            <a href="#box-config" data-toggle="modal" class="config">
                                                <i class="fa fa-cog"></i>
                                            </a>
                                            <a href="javascript:;" class="reload">
                                                <i class="fa fa-refresh"></i>
                                            </a>
                                            <a href="javascript:;" class="collapse">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                            <a href="javascript:;" class="remove">
                                                <i class="fa fa-times"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="box-body big">
                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">信息：</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox id="qitachengguo" CssClass="form-control" placeholder="请输入简介" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">文件：</label>
                                                <div class="col-sm-10">
                                                    <asp:FileUpload ID="qtcgfile" style="height:30px;width:380px" runat="server"/>
                                                </div>
                                            </div>

                                            <div style="font-family:微软雅黑;text-align:center">支持格式：以上全部</div><br />

                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">确认：</label>
                                                <div class="col-sm-10">
                                                    <div class="make-switch switch-small" data-on="success" data-off="danger" data-on-label="是" data-off-label="否">
                                                        <asp:CheckBox ID="qtcgcheck" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group" style="text-align:center">
                                                <asp:Button ID="qtcgsubmit" CssClass="btn btn-primary" Text="上传其它成果" runat="server" OnClick="qtcgsubmit_Click" />
                                            </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <!-- /CALENDAR & CHAT -->
                        <div class="footer-tools">
                            <span class="go-top">
                                <i class="fa fa-chevron-up"></i> 返回顶部
                            </span>
                        </div>

                    </div><!-- /CONTENT-->
                </div>
            </div>
        </div>

    </section>
    <!--/PAGE -->
    <!-- JAVASCRIPTS -->
    <!-- Placed at the end of the document so the pages load faster -->
    <!-- JQUERY -->
    <script src="js/jquery/jquery-2.0.3.min.js"></script>
    <!-- JQUERY UI-->
    <script src="js/jquery-ui-1.10.3.custom/js/jquery-ui-1.10.3.custom.min.js"></script>
    <!-- BOOTSTRAP -->
    <script src="bootstrap-dist/js/bootstrap.min.js"></script>


    <!-- DATE RANGE PICKER -->
    <script src="js/bootstrap-daterangepicker/moment.min.js"></script>

    <script src="js/bootstrap-daterangepicker/daterangepicker.min.js"></script>
    <!-- SLIMSCROLL -->
    <script type="text/javascript" src="js/jQuery-slimScroll-1.3.0/jquery.slimscroll.min.js"></script>
    <!-- SLIMSCROLL -->
    <script type="text/javascript" src="js/jQuery-slimScroll-1.3.0/jquery.slimscroll.min.js"></script>
    <script type="text/javascript" src="js/jQuery-slimScroll-1.3.0/slimScrollHorizontal.min.js"></script>
    <!-- BLOCK UI -->
    <script type="text/javascript" src="js/jQuery-BlockUI/jquery.blockUI.min.js"></script>
    <!-- SPARKLINES -->
    <script type="text/javascript" src="js/sparklines/jquery.sparkline.min.js"></script>
    <!-- EASY PIE CHART -->
    <script src="js/jquery-easing/jquery.easing.min.js"></script>
    <script type="text/javascript" src="js/easypiechart/jquery.easypiechart.min.js"></script>
    <!-- FLOT CHARTS -->
    <script src="js/flot/jquery.flot.min.js"></script>
    <script src="js/flot/jquery.flot.time.min.js"></script>
    <script src="js/flot/jquery.flot.selection.min.js"></script>
    <script src="js/flot/jquery.flot.resize.min.js"></script>
    <script src="js/flot/jquery.flot.pie.min.js"></script>
    <script src="js/flot/jquery.flot.stack.min.js"></script>
    <script src="js/flot/jquery.flot.crosshair.min.js"></script>
    <!-- TODO -->
    <script type="text/javascript" src="js/jquery-todo/js/paddystodolist.js"></script>
    <!-- TIMEAGO -->
    <script type="text/javascript" src="js/timeago/jquery.timeago.min.js"></script>
    <!-- FULL CALENDAR -->
    <script type="text/javascript" src="js/fullcalendar/fullcalendar.min.js"></script>
    <!-- COOKIE -->
    <script type="text/javascript" src="js/jQuery-Cookie/jquery.cookie.min.js"></script>
    <!-- GRITTER -->
    <script type="text/javascript" src="js/gritter/js/jquery.gritter.min.js"></script>
    <!-- BOOTSTRAP SWITCH -->
    <script type="text/javascript" src="js/bootstrap-switch/bootstrap-switch.min.js"></script>
    <script type="text/javascript" src="js/jQuery-Knob/js/jquery.knob.min.js"></script>
    <!-- CUSTOM SCRIPT -->
    <script src="js/script.js"></script>
    <script>
		jQuery(document).ready(function() {
		    App.setPage("fixed_header_sidebar");  //Set current page
			App.init(); //Initialise plugins and elements
		});
    </script>
    <!-- /JAVASCRIPTS -->
    </form>
</body>
</html>
