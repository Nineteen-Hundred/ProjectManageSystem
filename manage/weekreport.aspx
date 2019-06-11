<%@ Page Language="C#" AutoEventWireup="true" CodeFile="weekreport.aspx.cs" Inherits="manage_weekreport" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta http-equiv="content-type" content="text/html; charset=UTF-8">
	<meta charset="utf-8">
	<title>周报告——科研项目管理系统</title>
	<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1, user-scalable=no">
	<meta name="description" content="">
	<meta name="author" content="">
	<link rel="stylesheet" type="text/css" href="css/cloud-admin.css" >
	<link rel="stylesheet" type="text/css"  href="css/themes/default.css" id="skin-switcher" >
	<link rel="stylesheet" type="text/css"  href="css/responsive.css" >
	<!-- STYLESHEETS --><!--[if lt IE 9]><script src="js/flot/excanvas.min.js"></script><script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script><script src="http://css3-mediaqueries-js.googlecode.com/svn/trunk/css3-mediaqueries.js"></script><![endif]-->
	<link href="font-awesome/css/font-awesome.min.css" rel="stylesheet">
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
	<!-- HEADER -->
    <form runat="server">
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
							data-icon2="fa fa-bars" ></i>
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
				<%=member %>
			  </ul>
			</div>
		  </div>
		<!-- /TEAM STATUS -->
	</header>
	<!--/HEADER -->
	<div class="copyrights">Collect from <a href="http://www.cssmoban.com/" >企业网站模板</a></div>
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
									<li><a class="" href="upload.aspx"><span class="sub-menu-text">上传成果</span></a></li>
									<li class="current"><a><span class="sub-menu-text">周报告</span></a></li>
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
										<li>周报告</li>
									</ul>
									<!-- /BREADCRUMBS -->
								</div>
							</div>
						</div>
						<!-- /PAGE HEADER -->

                        <!--会议论文栏目-->
                        <div class="row" style="font-family:微软雅黑">
                            <div class="col-md-3"></div>
                            <div class="col-md-6">
                                <!-- BOX -->
                                <div class="box border green">
                                    <div class="box-title">
                                        <h4 style="font-family:微软雅黑"><i class="fa fa-bookmark-o"></i>上传周报告</h4>
                                        <div class="tools">
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
                                    <div class="box-body big" style="font-family:微软雅黑">
                                         <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">周次：</label>
                                                <div class="col-sm-10">
                                                   <asp:DropDownList Enabled="false" ID="shangchuanuser" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:20px">
                                                <label class="col-sm-2 control-label">简介：</label>
                                                <div class="col-sm-10"  style="margin-bottom:20px">
                                                    <asp:TextBox ID="shangchuantitle" runat="server" placeholder="输入格式：姓名-周次-所做工作" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group" style="margin-bottom:20px">
                                                <label class="col-sm-2 control-label">文件：</label>
                                                <div class="col-sm-10"  style="margin-bottom:20px">
                                                    <asp:FileUpload ID="shangchuanfile" runat="server"/>
                                                </div>
                                            </div>

                                            <div style="text-align:center;font-family:微软雅黑;">支持格式：doc(x), xls(x), ppt(x), zip, rar, pdf</div><br />

                                            <div class="form-group" style="margin-bottom:50px">
                                                <label class="col-sm-2 control-label">确认：</label>
                                                <div class="col-sm-10">
                                                    <div class="make-switch switch-small" data-on="success" data-off="danger" data-on-label="是" data-off-label="否">
                                                        <asp:CheckBox ID="shangchuancheck" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group" style="text-align:center">
                                                <asp:Button ID="shangchuanyes" CssClass="btn btn-danger" Text="上传周报告" runat="server" OnClick="shangchuanyes_Click" />
                                            </div>
                                    </div>
                                </div>


                                <!-- /BOX -->
                            </div>
                            <div class="col-md-3"></div>
                        </div>

                        <!--期刊论文栏目-->
                        <div class="row">
                            <div class="col-md-12">
                                <!-- BOX -->
                                <div class="box border orange">
                                    <div class="box-title">
                                        <h4 style="font-family:微软雅黑"><i class="fa fa-hand-o-up"></i>周报告</h4>
                                        <div class="tools">
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
                                    <div class="box-body" style="font-family:微软雅黑">
                                        <table class="table table-hover" style="font-family:微软雅黑">
                                            <thead>
                                                <tr>
                                                    <th>-</th>
                                                    <th>姓名</th>
                                                    <th>上传时间</th>
                                                    <th>简介</th>
                                                    <th>审核</th>
                                                    <th>下载</th>
                                                    <th>删除</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <%=zhoubaogao %>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <!-- /BOX -->
                            </div>
                        </div>

                        <!--往期周报告-->
                        <div class="row">
                            <div class="col-md-12">
                                <!-- BOX -->
                                <div class="box border blue">
                                    <div class="box-title">
                                        <h4 style="font-family:微软雅黑"><i class="fa fa-hand-o-up"></i>往期周报告</h4>
                                        <div class="tools">
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
                                    <div class="box-body" style="font-family:微软雅黑">
                                        <div class="col-sm-4"></div>
                                        <div class="col-sm-1">
                                            <asp:DropDownList CssClass="form-control" ID="renming" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:DropDownList ID="zhoucixiala" CssClass="form-control" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            <div class="make-switch switch-small" data-on="success" data-off="danger" data-on-label="周次" data-off-label="姓名">
                                                 <asp:CheckBox ID="zhouciorrenming" runat="server" />
                                            </div>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:Button ID="chaxun" CssClass="btn btn-info" runat="server" Text="查询" />
                                        </div>
                                        <div class="col-sm-4" style="margin-bottom:60px"></div>
                                        <table class="table table-hover" style="font-family:微软雅黑">
                                            <thead>
                                                <tr>
                                                    <th>-</th>
                                                    <th>姓名</th>
                                                    <th>上传时间</th>
                                                    <th>简介</th>
                                                    <th>审核</th>
                                                    <th>下载</th>
                                                    <th>删除</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <%=wangqizhoubaogao %>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <!-- /BOX -->
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
    </form>
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
	<script type="text/javascript" src="js/jQuery-slimScroll-1.3.0/jquery.slimscroll.min.js"></script><script type="text/javascript" src="js/jQuery-slimScroll-1.3.0/slimScrollHorizontal.min.js"></script>
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
</body>
</html>