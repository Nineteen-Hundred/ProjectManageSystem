<%@ Page Language="C#" AutoEventWireup="true" CodeFile="control.aspx.cs" Inherits="manage_control" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta http-equiv="content-type" content="text/html; charset=UTF-8">
	<meta charset="utf-8">
	<title>控制中心——科研项目管理系统</title>
	<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1, user-scalable=no">
	<meta name="description" content="">
	<meta name="author" content="">
	<!-- STYLESHEETS --><!--[if lt IE 9]><script src="js/flot/excanvas.min.js"></script><script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script><script src="http://css3-mediaqueries-js.googlecode.com/svn/trunk/css3-mediaqueries.js"></script><![endif]-->
	<link rel="stylesheet" type="text/css" href="css/cloud-admin.css" >
	<link rel="stylesheet" type="text/css"  href="css/themes/default.css" id="skin-switcher" >
	<link rel="stylesheet" type="text/css"  href="css/responsive.css" >
	<link href="../js/jquery-ui.css" rel="Stylesheet" />
    <script src="../js/external/jquery/jquery.js"></script>
    <script src="../js/jquery-ui.js"></script>
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
    <!-- DATE RANGE PICKER -->
    <link rel="stylesheet" type="text/css" href="js/bootstrap-daterangepicker/daterangepicker-bs3.css" />
    <!-- JQUERY UI-->
    <link rel="stylesheet" type="text/css" href="js/jquery-ui-1.10.3.custom/css/custom-theme/jquery-ui-1.10.3.custom.min.css" />
    <!-- BOOTSTRAP SWITCH -->
    <link rel="stylesheet" type="text/css" href="js/bootstrap-switch/bootstrap-switch.min.css" />
    <!-- FLAGS -->
    <link rel="stylesheet" type="text/css" href="css/flags/flags.min.css" />
	<!-- FONTS -->
	<!--<link href='http://fonts.useso.com/css?family=Open+Sans:300,400,600,700' rel='stylesheet' type='text/css'>-->
    <script>
        $(function () {
            $("#datepicker").datepicker();
        });
    </script>
</head>
<body>
    
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
                    <li class="has-sub">
                        <a href="javascript:;" class="">
                            <i class="fa fa-bookmark-o fa-fw"></i> <span class="menu-text">成果管理</span>
                            <span class="arrow open"></span>
                            <span class="selected"></span>
                        </a>
                        <ul class="sub">
                            <li><a class="" href="result.aspx"><span class="sub-menu-text">查看成果</span></a></li>
                            <li><a class="" href="upload.aspx"><span class="sub-menu-text">上传成果</span></a></li>
                            <li><a class="" href="weekreport.aspx"><span class="sub-menu-text">周报告</span></a></li>
                        </ul>
                    </li>
                    <li><a class="" href="money.aspx"><i class="fa fa-desktop fa-fw"></i> <span class="menu-text">查看经费</span></a></li>
                    <li><a class="" href="gallery.aspx"><i class="fa fa-picture-o fa-fw"></i> <span class="menu-text">软件截图</span></a></li>
                    <li class="active"><a><i class="fa fa-envelope-o fa-fw"></i> <span class="menu-text">控制中心</span></a></li>
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
                </div><a href="frontpage.aspx">科研项目管理系统</a>
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
                                        <li>控制中心</li>
                                    </ul>
                                    <!-- /BREADCRUMBS -->
                                </div>
                            </div>
                        </div>
                        <!-- /PAGE HEADER -->
                        <form id="form1" runat="server">

                        <!--第一行-->
                        <div class="row">
                        <div class="col-md-6">
                                <div class="box border green">
                                    <div class="box-title">
                                        <h4 style="font-family:微软雅黑"><i class="fa fa-bookmark-o"></i>报销经费</h4>
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
                                                <label class="col-sm-2 control-label">成员：</label>
                                                <div class="col-sm-10">
                                                    <asp:DropDownList ID="baoxiaouser" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:20px">
                                                <label class="col-sm-2 control-label">金额：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <asp:TextBox ID="baoxiaonum" runat="server" CssClass="form-control" placeholder="报销金额（单位：元）"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:20px">
                                                <label class="col-sm-2 control-label">类别：</label>
                                                <div class="col-sm-10"  style="margin-bottom:20px">
                                                    <asp:DropDownList ID="baoxiaoleibie"  CssClass="form-control" runat="server">
                                                        <asp:ListItem>材料费</asp:ListItem>
                                                        <asp:ListItem>设备费</asp:ListItem>
                                                        <asp:ListItem>资料费</asp:ListItem>
                                                        <asp:ListItem>差旅费</asp:ListItem>
                                                        <asp:ListItem>通信费</asp:ListItem>
                                                        <asp:ListItem>其他</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                        <div class="form-group">
                                                <label class="col-sm-2 control-label">备注：</label>
                                                <div class="col-sm-10"  style="margin-bottom:20px">
                                                    <asp:TextBox ID="baoxiaotitle" runat="server" CssClass="form-control" placeholder="请输入报销备注"></asp:TextBox>
                                                </div>
                                         </div>

                                            <div class="form-group" style="margin-bottom:20px">
                                                <label class="col-sm-2 control-label">确认：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <div class="make-switch switch-small" data-on="success" data-off="danger" data-on-label="是" data-off-label="否">
                                                        <asp:CheckBox ID="baoxiaocheck" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group" style="text-align:center">
                                                <asp:Button ID="baoxiaoyes" runat="server" Text="报销经费" CssClass="btn btn-primary" OnClick="baoxiaoyes_Click"/>
                                            </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="box border red">
                                    <div class="box-title">
                                        <h4 style="font-family:微软雅黑"><i class="fa fa-adjust"></i>分配经费</h4>
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
                                                <label class="col-sm-2 control-label">成员：</label>
                                                <div class="col-sm-10">
                                                    <asp:DropDownList ID="fenpeiuser" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">金额：</label>
                                                <div class="col-sm-10"  style="margin-bottom:20px">
                                                    <asp:TextBox ID="fenpeinum" runat="server" CssClass="form-control" placeholder="分配金额（单位：元）"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:20px">
                                                <label class="col-sm-2 control-label">类别：</label>
                                                <div class="col-sm-10"  style="margin-bottom:20px">
                                                    <asp:DropDownList ID="fenpeileibie"  CssClass="form-control" runat="server">
                                                        <asp:ListItem>材料费</asp:ListItem>
                                                        <asp:ListItem>设备费</asp:ListItem>
                                                        <asp:ListItem>资料费</asp:ListItem>
                                                        <asp:ListItem>差旅费</asp:ListItem>
                                                        <asp:ListItem>通信费</asp:ListItem>
                                                        <asp:ListItem>其他</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                        <div class="form-group">
                                                <label class="col-sm-2 control-label">备注：</label>
                                                <div class="col-sm-10"  style="margin-bottom:20px">
                                                    <asp:TextBox ID="fenpeititle" runat="server" CssClass="form-control" placeholder="请输入分配备注"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:20px">
                                                <label class="col-sm-2 control-label">确认：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <div class="make-switch switch-small" data-on="success" data-off="danger" data-on-label="是" data-off-label="否">
                                                        <asp:CheckBox ID="fenpeicheck" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group" style="text-align:center">
                                                <asp:Button ID="fenpeiyes" runat="server" Text="分配经费" CssClass="btn btn-primary" OnClick="fenpeiyes_Click"/>
                                            </div>
                                    </div>
                                </div>
                            </div>
                            </div>

                        <!--第二行-->

                        <div class="row">

                        <div class="col-md-6">
                                <div class="box border orange">
                                    <div class="box-title">
                                        <h4 style="font-family:微软雅黑"><i class="fa fa-envelope"></i>周报告审批</h4>
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
                                                <label class="col-sm-2 control-label">文件：</label>
                                                <div class="col-sm-10">
                                                    <asp:DropDownList ID="zhoubaogaotitle" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>

                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">备注：</label>
                                                <div class="col-sm-10"  style="margin-bottom:20px">
                                                    <asp:TextBox ID="zhoubaogaobeizhu" runat="server" CssClass="form-control" placeholder="请输入相关理由"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:20px">
                                                <label class="col-sm-2 control-label">确认：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <div class="make-switch switch-small" data-on="success" data-off="danger" data-on-label="是" data-off-label="否">
                                                        <asp:CheckBox ID="zhoubaogaoqueren" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group" style="text-align:center">
                                                <asp:Button ID="zhoubaogaoyes" runat="server" Text="同意申请" 
                                                    CssClass="btn btn-primary" style="margin-right:40px; height: 39px;" 
                                                    onclick="zhoubaogaoyes_Click" />
                                                <asp:Button ID="zhoubaogaono" runat="server" Text="拒绝申请" 
                                                    CssClass="btn btn-danger" style="margin-right:40px" 
                                                    onclick="zhoubaogaono_Click" />
                                                <asp:Button ID="zhoubaogaodown" runat="server" Text="下载查看" 
                                                    CssClass="btn btn-success" style="margin-right:40px" 
                                                    onclick="zhoubaogaodown_Click" />
                                            </div>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="col-md-6">
                                <div class="box border orange">
                                    <div class="box-title">
                                        <h4 style="font-family:微软雅黑"><i class="fa fa-envelope"></i>新成果审批</h4>
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
                                                <label class="col-sm-2 control-label">文件：</label>
                                                <div class="col-sm-10">
                                                    <asp:DropDownList ID="chengguouser" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>

                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">备注：</label>
                                                <div class="col-sm-10"  style="margin-bottom:20px">
                                                    <asp:TextBox ID="jujueliyou" runat="server" CssClass="form-control" placeholder="请输入相关理由"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:20px">
                                                <label class="col-sm-2 control-label">确认：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <div class="make-switch switch-small" data-on="success" data-off="danger" data-on-label="是" data-off-label="否">
                                                        <asp:CheckBox ID="chengguocheck" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group" style="text-align:center">
                                                <asp:Button ID="chengguoyes" runat="server" Text="同意申请" CssClass="btn btn-primary" style="margin-right:40px; height: 39px;" OnClick="chengguoyes_Click" />
                                                <asp:Button ID="chengguono" runat="server" Text="拒绝申请" CssClass="btn btn-danger" style="margin-right:40px" OnClick="chengguono_Click" />
                                                <asp:Button ID="chengguodown" runat="server" Text="下载查看" CssClass="btn btn-success" style="margin-right:40px" OnClick="chengguodown_Click" />
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
                                        <h4 style="font-family:微软雅黑"><i class="fa fa-calendar"></i>新建任务</h4>
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
                                                <label class="col-sm-2 control-label">名称：</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="xinjiantitle" runat="server" CssClass="form-control" placeholder="请输入任务名称"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">进度：</label>
                                                <div class="col-sm-10" style="text-align:center">
                                                    <asp:TextBox ID="xinjianvalue" runat="server" CssClass="knob" data-width="200" data-displayPrevious=true data-fgColor="#418141" data-skin="tron" data-thickness=".2" value="0"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:20px">
                                                <label class="col-sm-2 control-label">确认：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <div class="make-switch switch-small" data-on="success" data-off="danger" data-on-label="是" data-off-label="否">
                                                        <asp:CheckBox ID="xinjiancheck" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group" style="text-align:center">
                                                <asp:Button ID="xinjianyes" runat="server" Text="新建任务" CssClass="btn btn-primary" OnClick="xinjianyes_Click"/>
                                            </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="box border purple">
                                    <div class="box-title">
                                        <h4 style="font-family:微软雅黑"><i class="fa fa-home"></i>修改进度</h4>
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
                                                <label class="col-sm-2 control-label">名称：</label>
                                                <div class="col-sm-10">
                                                    <asp:DropDownList ID="xiugaititle" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">进度：</label>
                                                <div class="col-sm-10" style="text-align:center">
                                                    <asp:TextBox ID="xiugaitemp" runat="server" class="knob" data-width="170" ReadOnly="true" data-displayPrevious=true data-fgColor="#a1a1a1" data-skin="tron" data-thickness=".2" value="0"></asp:TextBox>
                                                    <asp:TextBox ID="xiugaivalue" runat="server" class="knob" data-width="170" data-displayPrevious=true data-fgColor="#418141" data-skin="tron" data-thickness=".2" value="0"></asp:TextBox>
                                                </div>
                                                
                                            </div>

                                            <div class="form-group" style="margin-bottom:20px">
                                                <label class="col-sm-2 control-label">确认：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <div class="make-switch switch-small" data-on="success" data-off="danger" data-on-label="是" data-off-label="否">
                                                        <asp:CheckBox ID="xiugaicheck" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group" style="text-align:center">
                                                <asp:Button ID="xugaiyes" runat="server" Text="修改任务" CssClass="btn btn-primary" OnClick="xugaiyes_Click"/>
                                            </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        

                        <!--第四行-->
                        <div class="row">
                            <div class="col-md-6">
                                <div class="box border purple">
                                    <div class="box-title">
                                        <h4 style="font-family:微软雅黑"><i class="fa fa-adjust"></i>发布公告</h4>
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
                                            <div class="form-group" style="margin-bottom:20px">
                                                <label class="col-sm-2 control-label">内容：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <asp:TextBox ID="gonggaotitle" runat="server" placeholder="请输入公告内容" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:20px">
                                                <label class="col-sm-2 control-label">确认：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <div class="make-switch switch-small" data-on="success" data-off="danger" data-on-label="是" data-off-label="否">
                                                        <asp:CheckBox ID="gonggaocheck" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group" style="text-align:center">
                                                <asp:Button ID="gonggaoyes" runat="server" CssClass="btn btn-primary" Text ="发布公告" OnClick="gonggaoyes_Click" />
                                            </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="box border green">
                                    <div class="box-title">
                                        <h4 style="font-family:微软雅黑"><i class="fa fa-bookmark-o"></i>发送私信</h4>
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
                                            <div class="form-group" style="margin-bottom:20px">
                                                <label class="col-sm-2 control-label">成员：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <asp:DropDownList ID="sixinuser" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:20px">
                                                <label class="col-sm-2 control-label">内容：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <asp:TextBox ID="sixintitle" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="请输入私信内容"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:20px">
                                                <label class="col-sm-2 control-label">确认：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <div class="make-switch switch-small" data-on="success" data-off="danger" data-on-label="是" data-off-label="否">
                                                        <asp:CheckBox ID="sixincheck" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group" style="text-align:center">
                                                <asp:Button ID="sixinyes" runat="server" CssClass="btn btn-primary" Text="发送私信" OnClick="sixinyes_Click" />
                                            </div>
                                    </div>
                                </div>
                            </div>  
                        </div>

                        <!--第五行-->
                        <div class="row">
                            <div class="col-md-6">
                                <div class="box border primary">
                                    <div class="box-title">
                                        <h4 style="font-family:微软雅黑"><i class="fa fa-bars"></i>新成员审批</h4>
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
                                                    <asp:DropDownList ID="shenpiuser" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:20px">
                                                <label class="col-sm-2 control-label">确认：</label>
                                                
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <div class="make-switch switch-small" data-on="success" data-off="danger" data-on-label="是" data-off-label="否">
                                                        <asp:CheckBox ID="shenpicheck" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group" style="text-align:center">
                                                <asp:Button ID="shenpingyes" runat="server" Text="同意申请" CssClass="btn btn-primary" style="margin-right:40px" OnClick="shenpingyes_Click" />
                                                <asp:Button ID="shenpino" runat="server" Text="拒绝申请" CssClass="btn btn-danger" OnClick="shenpino_Click" />
                                            </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="box border primary">
                                    <div class="box-title">
                                        <h4 style="font-family:微软雅黑"><i class="fa fa-bars"></i>设置初始周次</h4>
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
                                                <label class="col-sm-2 control-label">日期：</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="datepicker" runat="server" CssClass="form-control" placeholder="请选择时间"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:20px">
                                                <label class="col-sm-2 control-label">确认：</label>
                                                
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <div class="make-switch switch-small" data-on="success" data-off="danger" data-on-label="是" data-off-label="否">
                                                        <asp:CheckBox ID="zhouciqueren" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group" style="text-align:center">
                                                <asp:Button ID="zhouciyes" runat="server" Text="设置周次" 
                                                    CssClass="btn btn-primary" style="margin-right:40px" 
                                                    onclick="zhouciyes_Click" />
                                            </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!--第六行-->
                        <div class="row">
                            <div class="col-md-6">
                                <div class="box border red">
                                    <div class="box-title">
                                        <h4 style="font-family:微软雅黑"><i class="fa fa-adjust"></i>邮件群发</h4>
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
											<!--
                                            <div class="form-group" style="margin-bottom:20px">
                                                <label class="col-sm-2 control-label">密码：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <asp:TextBox ID="youjianmima" runat="server" placeholder="请输入您的邮箱密码" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                                </div>
                                            </div> -->

                                            <div class="form-group" style="margin-bottom:20px">
                                                <label class="col-sm-2 control-label">标题：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <asp:TextBox ID="youjiantitle" runat="server" placeholder="请输入您的邮件标题" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:20px">
                                                <label class="col-sm-2 control-label">附件：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <asp:TextBox ID="youjianfujian" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:20px">
                                                <label class="col-sm-2 control-label">正文：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <asp:TextBox ID="youjianzhengwen" runat="server" placeholder="请输入邮件正文" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:20px">
                                                <label class="col-sm-2 control-label">附件：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <asp:FileUpload ID="youjianfile" runat="server" style="height:30px;width:350px" />
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:20px">
                                                <label class="col-sm-2 control-label">确认：</label>
                                                <div class="col-sm-10" style="margin-bottom:20px">
                                                    <div class="make-switch switch-small" data-on="success" data-off="danger" data-on-label="是" data-off-label="否">
                                                        <asp:CheckBox ID="youjiancheck" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group" style="text-align:center">
                                                <asp:Button ID="youjianyes" runat="server" CssClass="btn btn-primary" style="margin-right:40px" Text ="发送邮件" OnClick="youjianyes_Click" />
                                                <asp:Button ID="youjianno" runat="server" CssClass="btn btn-danger" Text ="上传附件" OnClick="youjianno_Click" />
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
        </form>

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
    <!-- CUSTOM SCRIPT -->
    <script src="js/script.js"></script>

    <!--/PAGE -->
    <!-- JAVASCRIPTS -->
    <!-- Placed at the end of the document so the pages load faster -->
    <!-- JQUERY -->


    <!-- DATE RANGE PICKER -->
    <script src="js/bootstrap-daterangepicker/moment.min.js"></script>

    <script src="js/bootstrap-daterangepicker/daterangepicker.min.js"></script>
    <!-- SLIMSCROLL -->
    <script type="text/javascript" src="js/jQuery-slimScroll-1.3.0/jquery.slimscroll.min.js"></script>
    <script type="text/javascript" src="js/jQuery-slimScroll-1.3.0/slimScrollHorizontal.min.js"></script>
    <!-- BLOCK UI -->
    <script type="text/javascript" src="js/jQuery-BlockUI/jquery.blockUI.min.js"></script>
    <!-- ONHOVER -->
    <script type="text/javascript" src="js/bootstrap-onhover/twitter-bootstrap-hover-dropdown.min.js"></script>
    <!-- BOOTSTRAP SWITCH -->
    <script type="text/javascript" src="js/bootstrap-switch/bootstrap-switch.min.js"></script>
    <script type="text/javascript" src="js/jQuery-Knob/js/jquery.knob.min.js"></script>
    <!-- COOKIE -->
    <script type="text/javascript" src="js/jQuery-Cookie/jquery.cookie.min.js"></script>
    <script>
		jQuery(document).ready(function() {
		    App.setPage("sliders_progress");  //Set current page
			App.init(); //Initialise plugins and elements
		});
    </script>
    <!-- /JAVASCRIPTS -->
</body>
</html>