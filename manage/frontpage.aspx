<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/manage/frontpage.aspx.cs" Inherits="manage_frontpage1" %>
<!DOCTYPE html>
<html lang="en">
<head>
	<meta http-equiv="content-type" content="text/html; charset=UTF-8">
	<meta charset="utf-8">
	<title>首页——科研项目管理系统</title>
	<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1, user-scalable=no">
	<meta name="description" content="">
	<meta name="author" content="">
	<link rel="stylesheet" type="text/css" href="css/cloud-admin.css" >
	<link rel="stylesheet" type="text/css"  href="css/themes/default.css" id="skin-switcher">
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
	<!-- FONTS -->
	<!--<!--<link href='http://fonts.useso.com/css?family=Open+Sans:300,400,600,700' rel='stylesheet' type='text/css'>-->-->
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
								<span><i class="fa fa-envelope-o"></i>共有 <%=chatnum %> 条未读私信</span> 
								<span class="compose pull-right tip-right" title="Compose message"></span>
							</li>
                            <li class="footer">
								<a href="webchatclean.aspx">已阅所有信息 <i class="fa fa-arrow-circle-right"></i></a>
							</li>
							<%=webchat %>
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
	<div class="copyrights">Collect from <a href="http://www.cssmoban.com/" >企业网站模板</a></div>
	<!-- PAGE -->
	<section id="page">
				<!-- SIDEBAR -->
				<div id="sidebar" class="sidebar sidebar-fixed">
					<div class="sidebar-menu nav-collapse">
						<div class="divide-20"></div>
						<!-- SEARCH BAR -->
						<div id="search-bar">
                            <form action="http://www.baidu.com/baidu"  target="_blank">
                                <input class="search" type="text" name="word" placeholder="百度搜索入口"><i class="fa fa-search search-icon"></i>
                            </form>
                        </div>
						<!-- /SEARCH BAR -->
						
						<!-- SIDEBAR QUICK-LAUNCH -->
						<!-- <div id="quicklaunch">
						<!-- /SIDEBAR QUICK-LAUNCH -->
						
						<!-- SIDEBAR MENU -->
						<ul>
							<li class="active">
								<a>
								<i class="fa fa-tachometer fa-fw"></i> <span class="menu-text">管理首页</span>
								<span class="selected"></span>
								</a>					
							</li>
							<li class="has-sub">
								<a href="javascript:;" class="">
								<i class="fa fa-bookmark-o fa-fw"></i> <span class="menu-text">成果管理</span>
								<span class="arrow"></span>
								</a>
								<ul class="sub">
									<li><a class="" href="result.aspx"><span class="sub-menu-text">查看成果</span></a></li>
									<li><a class="" href="upload.aspx"><span class="sub-menu-text">上传成果</span></a></li>
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
					<div id="content" class="col-lg-12">
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
										<li>管理首页</li>
									</ul>
									<!-- /BREADCRUMBS -->
								</div>
							</div>
						</div>
						<!-- /PAGE HEADER -->
<form id="form1" runat="server">
						<!-- DASHBOARD CONTENT -->
						<div class="row">
							<!-- COLUMN 1 -->
							<div class="col-md-6">
								<div class="row">
								  <div class="col-lg-6">
									 <div class="dashbox panel panel-default">
										<div class="panel-body">
										   <div class="panel-left red">
												<i class="fa fa-instagram fa-3x"></i>
										   </div>
										   <div class="panel-right">
												<div class="number"><%=renshu %></div>
												<div class="title">目前项目内部成员</div>
												<span class="label label-success">
													已通过验证</i>
												</span>
										   </div>
										</div>
									 </div>
								  </div>
								  <div class="col-lg-6">
									 <div class="dashbox panel panel-default">
										<div class="panel-body">
										   <div class="panel-left blue">
												<i class="fa fa-twitter fa-3x"></i>
										   </div>
										   <div class="panel-right">
												<div class="number"><%=liuliang %></div>
												<div class="title">项目访问总流量</div>
												<span class="label label-success">
													正常 </i>
												</span>
										   </div>
										</div>
									 </div>
								  </div>
								</div>
								<div class="row">
									<div class="col-md-12">
										<div class="quick-pie panel panel-default">
											<div class="panel-body" style="text-align:center; background-color:#4a708b;border-radius:8px">
                                            <!--
												<div class="col-md-4 text-center">
													<div id="dash_pie_1" class="piechart" data-percent="<%=rszb%>">
														<span class="percent"></span>
													</div>
													<p class="title"  style="color:purple;font-family:微软雅黑">平台总人数占比 <i class="fa fa-angle-right"></i></p>
												</div>
												<div class="col-md-4 text-center">
													<div id="dash_pie_2" class="piechart" data-percent="<%=jfzb %>">
														<span class="percent"></span>
													</div>
													<p class="title" style="color:orange;font-family:微软雅黑">平台总经费占比 <i class="fa fa-angle-right"></i></p>
												</div>
												<div class="col-md-4 text-center">
													<div id="dash_pie_3" class="piechart" data-percent="<%=cgzb %>">
														<span class="percent"></span>
													</div>
													<p class="title" style="color:mediumvioletred;font-family:微软雅黑">平台总成果占比 <i class="fa fa-angle-right"></i></p>
												</div>
                                                -->
                                                <iframe src="http://www.thinkpage.cn/weather/weather.aspx?uid=U787808067&cid=CHSN000000&l=zh-CHS&p=SMART&a=0&u=C&s=3&m=0&x=1&d=3&fc=FFFFFF&bgc=&bc=&ti=0&in=0&li=&ct=iframe" frameborder="0" scrolling="no" width="500" height="100" allowTransparency="true"></iframe>
											</div>
										</div>
									</div>
							   </div>
							</div>
							<!-- /COLUMN 1 -->
							
							<!-- COLUMN 2 -->
							<div class="col-md-6">
                                <div class="box border red">
                                    <div class="box-title">
                                        <h4 style="font-family:微软雅黑"><i class="fa fa-bars"></i>公告栏 -- <%=shijian %><a href="../records.aspx" style="margin-left:20px;color:white">查看历史记录</a></h4>
                                    </div>
                                    <div class="box-body big">
                                        <div class="scroller" data-height="138px" data-always-visible="1" data-rail-visible="1">
                                            <div id="chart-revenue" style="height:1px;visibility:hidden"></div>
                                            <div style="font-size:medium;text-indent:1em;line-height:30px"><%=gonggao%></div>
                                        </div>

                                    </div>
                                </div>
                           </div>
                        </div>
					   <!-- /DASHBOARD CONTENT -->
					   <!-- HERO GRAPH -->
						<div class="row">
							<div class="col-md-12">
								<!-- BOX -->
								<div class="box border blue">
									<div class="box-title">
										<h4 style="font-family:微软雅黑"><i class="fa fa-bars"></i> <span class="hidden-inline-mobile">统计图表</span></h4>
									</div>
									<div class="box-body" style="min-height:420px">
                                                 <div class="form-group">
												 <div class="col-md-6" style="text-align:center">
													<!-- TAB 1 -->
                                                     <asp:Chart ID="Chart1"  runat="server" Width="500" Height="400px" 
                                                     BorderDashStyle="none"  BackSecondaryColor="White" 
                                                     BackGradientStyle="TopBottom" BorderWidth="2px" backcolor=LightBlue
                                                     BorderColor="#1A3B69" BorderlineWidth="0"  style="border-radius:10px;font-family:微软雅黑">
                                                     <Series>
                                                         <asp:Series Name="Series1">
                                                         </asp:Series>
                                                     </Series>
                                                     <Titles>
                                                 <asp:Title Font="微软雅黑, 16pt" Name="Title1" Text="项目进度统计图">
                                                 </asp:Title>
                                                 </Titles>
                                                 <borderskin BackColor="Transparent" BackImageAlignment="Top" BorderColor="Transparent" PageColor="Transparent"></borderskin>
                                                     <ChartAreas>
                                                         <asp:ChartArea Area3DStyle-Enable3D=false Name="ChartArea1" BorderColor="64, 64, 64, 64"  BackSecondaryColor="Transparent" BackColor="0, 246, 246, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                                                  <Area3DStyle Enable3D="false" ></Area3DStyle>
                                                         </asp:ChartArea>
                                                     </ChartAreas>
                                                 </asp:Chart>
                                                 </div>
                                                 <div class="col-md-6" style="text-align:center">
                                                     <!--
                                                     <asp:Chart ID="Chart3"  runat="server" Width="500px" Height="400px" 
                                                     BorderDashStyle="Solid"  BackSecondaryColor="White" 
                                                     BackGradientStyle="TopBottom" BorderWidth="2px" backcolor=LightBlue 
                                                     BorderColor="#1A3B69" style="font-family:微软雅黑;border-radius:10px;margin-left:20px">
                                                     <Series>
                                                         <asp:Series Name="Series2" ChartType="Spline">
                                                         </asp:Series>
                                                     </Series>
                                                     <Titles>
                                                 <asp:Title Font="微软雅黑, 16pt" Name="Title2" Text="访问量统计图">
                                                 </asp:Title>
                                                 </Titles>
                                                     <ChartAreas>
                                                         <asp:ChartArea Area3DStyle-Enable3D=false Name="ChartArea2" BorderColor="64, 64, 64, 64" BackSecondaryColor="Transparent" BackColor="120, 246, 246, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                                                  <Area3DStyle Enable3D="false" ></Area3DStyle>
                                                         </asp:ChartArea>
                                                     </ChartAreas>
                                                 </asp:Chart>
                                                 -->
                                                 <asp:Chart ID="Chart2"  runat="server" Width="500" Height="400px" 
                                                     BorderDashStyle="none"  BackSecondaryColor="White" 
                                                     BackGradientStyle="TopBottom" BorderWidth="2px" backcolor=LightBlue
                                                     BorderColor="#1A3B69" BorderlineWidth="0"  style="border-radius:10px;font-family:微软雅黑">
                                                     <Series>
                                                         <asp:Series Name="Series2">
                                                         </asp:Series>
                                                     </Series>
                                                     <Titles>
                                                 <asp:Title Font="微软雅黑, 16pt" Name="Title2" Text="经费使用情况统计图（单位：万元）">
                                                 </asp:Title>
                                                 </Titles>
                                                 <borderskin BackColor="Transparent" BackImageAlignment="Top" BorderColor="Transparent" PageColor="Transparent"></borderskin>
                                                     <ChartAreas>
                                                         <asp:ChartArea Area3DStyle-Enable3D=false Name="ChartArea2" BorderColor="64, 64, 64, 64"  BackSecondaryColor="Transparent" BackColor="0, 246, 246, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                                                  <Area3DStyle Enable3D="false" ></Area3DStyle>
                                                         </asp:ChartArea>
                                                     </ChartAreas>
                                                 </asp:Chart>
												   <!-- /TAB 1 -->
												 </div>
											</div>
								     </div>
								</div>
								<!-- /BOX -->
							</div>
						</div>
						<!-- /HERO GRAPH -->
						<!-- NEW ORDERS & STATISTICS -->
						<div class="row">
							<!-- NEW ORDERS -->
							<div class="col-md-6">
								<div class="box border purple">
									<div class="box-title">
										<h4><i class="fa fa-columns"></i> <span class="hidden-inline-mobile" style="font-family:微软雅黑">最近活动</span></h4>
									</div>
									<div class="box-body">
                                        <div class="scroller" data-height="450px" data-always-visible="1" data-rail-visible="1">
                                            <%=activity %>
                                        </div>
									</div>
								</div>
							</div>
							<!-- /NEW ORDERS -->
							
							
						<!-- /NEW ORDERS & STATISTICS -->
						
							<!-- CHAT -->
							<div class="col-md-6">
								<!-- BOX -->
								<div class="box border red chat-window">
									<div class="box-title">
										<h4 style="font-family:微软雅黑"><i class="fa fa-comments"></i>聊天窗口</h4>
										
									</div>
									<div class="box-body big">
										<div class="scroller" data-height="338px" data-always-visible="1" data-rail-visible="1">
											<ul class="media-list chat-list">
                                                <%=chatwindow %>
											</ul>
										</div>
										<div class="divide-20"></div>
										<div>
                                            <div class="input-group" id="input" runat="server">
                                                <asp:TextBox ID="message" CssClass="form-control" placeholder="请输入您要说的话" runat="server"></asp:TextBox>
                                                <span class="input-group-btn"><asp:Button ID="send" runat="server" class="btn btn-danger" Text="发送" OnClick="send_Click" /></span>
                                            </div>
										</div>
									</div>
								</div>
								<!-- /BOX -->
							</div>
							<!-- CHAT -->
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
        <div style="visibility:hidden;height:1px">
            <div id="chart-dash" class="chart" style="height:1px"></div>

             <div class="tab-pane fade" id="box_tab2" style="height:1px;width:20px;visibility:hidden">
													<div class="row" style="height:1px">
														<div class="col-md-8"  style="height:1px">
															<div class="demo-container" style="height:1px;display:none">
																<div id="placeholder" class="demo-placeholder"></div>
															</div>
														</div>
														<div class="col-md-4">
															<div class="demo-container" style="height:1px;display:none">
																<div id="overview" class="demo-placeholder" style="display:none"></div>
															</div>		
														</div>
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
	<!-- CUSTOM SCRIPT -->
	<script src="js/script.js"></script>
	<script>
		jQuery(document).ready(function() {		
			App.setPage("index");  //Set current page
			App.init(); //Initialise plugins and elements
		});
	</script>
	<!-- /JAVASCRIPTS -->
    </form>
</body>
</html>