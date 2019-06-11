<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ui.aspx.cs" Inherits="ui1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<!--[if IE 7]><html class="ie7" lang="zh"><![endif]-->
<!--[if gt IE 7]><!-->
<html lang="zh">
<!--<![endif]-->
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <title>用户界面</title>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=NxHVFwyEbrXOcD3K1OcLscHn"></script>
    <script type="text/javascript" src="js/swfobject.js"></script>
    <script type="text/javascript">
        var cacheBuster = "?t=" + Date.parse(new Date());
        var stageW = 980; //"100%";
        var stageH = 402; //"100%";
        var attributes = {};
        attributes.id = 'show_pro_img';
        attributes.name = attributes.id;
        var params = {};
        params.bgcolor = "#ffffff";
        var flashvars = {};
        flashvars.componentWidth = stageW;
        flashvars.componentHeight = stageH;
        flashvars.pathToFiles = "xixi1/";
        flashvars.xmlPath = "lrtk.xml";
        swfobject.embedSWF("xixi1/preview.swf" + cacheBuster, attributes.id, stageW, stageH, "9.0.124", "xixi1/expressInstall.swf", flashvars, params);			
    </script>
</head>
<body style="background-color: White">
    <form id="form1" runat="server">
    <div style="margin-left: auto; margin-right: auto; width: 1300px">
        <div style="width: 1120px; height: 420px; margin-left: 40px">
            <div style="width: 500px; float: left">
                <a href="Default.aspx">
                    <img src="../image/biaoti.jpg" /></a>
            </div>
            <div style="width: 400px; float: right; margin-top: 60px; font-size: 14px; font-family: 微软雅黑">
                <img src="../image/biaoti1.jpg" style="margin-bottom: 15px" />
                <p>
                </p>
                账号：<asp:TextBox ID="username" Style="width: 100px" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;密码：<asp:TextBox
                    ID="password" TextMode="Password" Style="width: 130px" runat="server"></asp:TextBox>
                <asp:Button ID="submit" runat="server" Text="申请加入" OnClick="submit_Click" />
                <a href="apply.aspx">
                    <img src="image/zhuce.jpg" style="width: 150px; border-radius: 6px; margin-top: 20px;
                        margin-left: 40px" /></a> <a href="login.aspx">
                            <img src="image/anniu.jpg" style="width: 150px; border-radius: 6px; margin-top: 20px;
                                margin-left: 40px" /></a>
            </div>
            <div style="width: 200px; float: left;">
            </div>
        </div>
        <div style="width: 96%; background-color: Orange; font-family: 微软雅黑; font-size: 30px;
            border-radius: 8px; height: 60px; margin-left: auto; margin-right: auto; color: White;
            font-weight: bold; padding-top: 12px">
            <div style="margin-left: 30px">
                个人信息</div>
        </div>
        <div style="width: 700px; height: 300px; margin-left: auto; margin-right: auto">
            <div style="width: 220px; height: 220px; float: left; margin-top: 30px; margin-left: 60px">
                <%=code1 %>
            </div>
            <div style="float: right; width: 400px; font-family: 微软雅黑; font-size: large; margin-top: 44px;
                line-height: 36px">
                姓名：<%=code2 %><br />
                性别：<%=code3 %><br />
                单位：<%=code4 %><br />
                电话：<%=code5 %><br />
                邮箱：<%=code6 %><br />
                身份证号：<%=code7 %><br />
                <a href="manage/change.aspx" style="font-size: 12px">修改个人信息</a>
            </div>
        </div>
        <br />
        <div style="width: 96%; background-color: Orange; font-family: 微软雅黑; font-size: 30px;
            border-radius: 8px; height: 60px; margin-left: auto; margin-right: auto; color: White;
            font-weight: bold; padding-top: 12px">
            <div style="margin-left: 30px">
                访问记录</div>
        </div>
        <div style="width: 900px; height: 300px; margin-left: 200px; font-family: 微软雅黑; line-height: 32px">
            <div style="text-align: center; margin-top: 40px; font-size: 16px">
                <%=jilu() %>
            </div>
        </div>
        <div style="width: 96%; background-color: Orange; font-family: 微软雅黑; font-size: 30px;
            border-radius: 8px; height: 60px; margin-left: auto; margin-right: auto; color: White;
            font-weight: bold; padding-top: 12px">
            <div style="margin-left: 30px">
                更新日志</div>
        </div>
        <div class="content" style="width: 1200px; margin-top: 40px">
            <div class="wrapper">
                <div class="light">
                    <i></i>
                </div>
                <hr class="line-left" />
                <hr class="line-right" />
                <div class="main">
                    <h1 class="title">
                        系统更新日志</h1>
						
						
					<div class="year">
                        <h2>
                            <a href="#">2017年<i></i></a></h2>
                        <div class="list">
                            <ul>
                                <li class="cls">
                                    <p class="date">
                                        8月30日</p>
                                    <p class="intro">
                                        服务器迁移，调整功能并修复问题</p>
                                    <p class="version">
                                        2.1.0.0</p>
                                    <div class="more">
                                        <p>
                                            1. 将网站迁移到阿里云服务器，存储空间40G，带宽1M，内存1G，现有资源可以稳定运行网站系统</p>
                                        <p>
                                            2. 分析并修复了项目首页、周报告、成果管理等页面加载过程中长时间挂起的问题，删除了影响请求执行的相关外链</p>
										<p>
                                            3. 重新开放了邮件通知服务，各类与成果和经费相关的操作，会自动邮件通知相关用户</p>
										<p>
                                            4. 重新开放了邮件群发功能，管理员可群发带有附件的通知邮件至项目内所有成员</p>
										<p>
											5. 对2017年9月2日之前的文件停止下载服务，以精简服务器文件存储</p>
										<p>
											6. 由于带宽有限，且服务器位于外网无法免流量，目前服务器已停止了语音和视频通话中转服务，以往用于校内视频通话的APP和网页将不再使用</p>
										<p>
											7. 因实际需求较少，且为了便于服务器运行维护，关闭了实验室论坛网站</p>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>

                    <div class="year">
                        <h2>
                            <a href="#">2015年<i></i></a></h2>
                        <div class="list">
                            <ul>
                                <li class="cls highlight">
                                    <p class="date">
                                        11月27日</p>
                                    <p class="intro">
                                        优化管理结构，修复部分问题</p>
                                    <p class="version">
                                        2.0.1.1</p>
                                    <div class="more">
                                        <p>
                                            1. 将访问量统计图更换为经费使用情况分类统计图</p>
                                        <p>
                                            2. 新增了周报告审核模块</p>
                                        <p>
                                            3. 周报告和成果审核拒绝时可附加备注信息</p>
                                        <p>
                                            4. 经费报销或分配时可自行选择所属大类</p>
                                        <p>
                                            5. 修复了周报告时间与备注显示错位的问题</p>
                                        <p>
                                            6. 新增了成果和周报告的删除功能，附带提示页面</p>
                                    </div>
                                </li>
                                <li class="cls highlight">
                                    <p class="date">
                                        10月17日</p>
                                    <p class="intro">
                                        针对反馈深度优化，系统友好性大幅提升</p>
                                    <p class="version">
                                        2.0.1.0</p>
                                    <div class="more">
                                        <p>
                                            1. 优化了期刊论文、会议论文、申请专利、授权专利的上传模式</p>
                                        <p>
                                            2. 更新了“更新日志”栏目的外观及结构</p>
                                        <p>
                                            3. 将原有的各类比例统计信息改为天气情况</p>
                                        <p>
                                            4. 因校内网连接不稳定，为防止不响应情况，屏蔽了原有的邮件通知和邮件群发</p>
                                        <p>
                                            5. 所有统计图表居中显示</p>
                                        <p>
                                            6. 调整了控制中心各栏目的显示顺序</p>
                                        <p>
                                            7. 为防止私信过多，“已阅”按钮不可选，将“已阅”按钮调整至私信上方</p>
                                        <p>
                                            8. 重写了登录界面的UI</p>
                                    </div>
                                </li>
                                <li class="cls">
                                    <p class="date">
                                        9月30日</p>
                                    <p class="intro">
                                        修复设计性错误</p>
                                    <p class="version">
                                        2.0.0.2</p>
                                    <div class="more">
                                        <p>
                                            1. 修复“聊天窗口未按照时间逆序排列”的问题</p>
                                        <p>
                                            2. 修复“聊天窗口未过滤字符串”的问题</p>
                                        <p>
                                            3. 修复“申请加入管理员时未正常发送邮件”的问题</p>
                                        <p>
                                            4. 修复“修改进度时无法选中特定项”的问题</p>
                                        <p>
                                            5. 修复“百度搜索入口指向错误”的问题</p>
                                        <p>
                                            6. 增加了普通用户的成果浏览权限的限制</p>
                                        <p>
                                            7. 增加了普通用户的经费查看权限的限制</p>
                                    </div>
                                </li>
                                <li class="cls highlight">
                                    <p class="date">
                                        9月16日</p>
                                    <p class="intro">
                                        科研项目管理系统 2.0 上线！</p>
                                    <p class="version">
                                        2.0.0.1</p>
                                    <div class="more">
                                        <p>
                                            1. 全新UI，简洁亮丽，支持更换皮肤，支持界面元素编辑</p>
                                        <p>
                                            2. 新增私信系统，可实现操作即时通知和管理员定向通知</p>
                                        <p>
                                            3. 新增任务管理系统，方便项目任务周期管理</p>
                                        <p>
                                            4. 新增软件截图栏目，可实现按类别分组，动画切换</p>
                                        <p>
                                            5. 优化聊天系统</p>
                                        <p>
                                            6. 优化成员管理机制，更改头像图片尺寸</p>
                                    </div>
                                </li>
                                <li class="cls">
                                    <p class="date">
                                        1月17日</p>
                                    <p class="intro">
                                        常规更新</p>
                                    <p class="version">
                                        1.0.0.7</p>
                                    <div class="more">
                                        <p>
                                            1. 解决了连接不能销毁的Bug，防止服务器过载崩溃</p>
                                        <p>
                                            2. 修复了对IE浏览器的支持，目前支持IE11浏览器</p>
                                        <p>
                                            3. 对部分数据库连接语句进行了优化，避免空连接出现</p>
                                    </div>
                                </li>
                                <li class="cls">
                                    <p class="date">
                                        1月15日</p>
                                    <p class="intro">
                                        常规更新</p>
                                    <p class="version">
                                        1.0.0.6</p>
                                    <div class="more">
                                        <p>
                                            1. 优化了弹出消息机制，提示时不再出现白屏</p>
                                        <p>
                                            2. 登录界面输入全空时仅提示一次，不再多次提示</p>
                                        <p>
                                            3. 经过测试，网页在IE11浏览器下完全显示正常，因此去掉了屏蔽IE浏览器的限制</p>
                                        <p>
                                            4. 对成果页面点击“请选择类别”显示错乱的情况进行了修复</p>
                                        <p>
                                            5. 放宽了对注册用户时照片格式的限制</p>
                                        <p>
                                            6. 添加了错误转向页面，屏蔽了错误的详细信息</p>
                                    </div>
                                </li>
                                <li class="cls">
                                    <p class="date">
                                        1月14日</p>
                                    <p class="intro">
                                        常规更新</p>
                                    <p class="version">
                                        1.0.0.5</p>
                                    <div class="more">
                                        <p>
                                            1. 修复了注册用户性别恒为“男”的错误</p>
                                        <p>
                                            2. 优化了注册表操作流程，修复了在长期运行之后的连接数满载问题</p>
                                        <p>
                                            3. 修复了申请加入项目页面出现崩溃的现象</p>
                                        <p>
                                            4. 放宽敏感字符过滤限制，对常用词汇不进行过滤</p>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="year">
                        <h2>
                            <a href="#">2014年<i></i></a></h2>
                        <div class="list">
                            <ul>
                                <li class="cls">
                                    <p class="date">
                                        12月14日</p>
                                    <p class="intro">
                                        常规更新</p>
                                    <p class="version">
                                        1.0.0.4</p>
                                    <div class="more">
                                        <p>
                                            1. 修复了注册用户性别恒为“男”的错误</p>
                                        <p>
                                            2. 优化了注册表操作流程，修复了在长期运行之后的连接数满载问题</p>
                                        <p>
                                            3. 修复了申请加入项目页面出现崩溃的现象</p>
                                        <p>
                                            4. 放宽敏感字符过滤限制，对常用词汇不进行过滤</p>
                                    </div>
                                </li>
                                <li class="cls">
                                    <p class="date">
                                        10月10日</p>
                                    <p class="intro">
                                        常规更新</p>
                                    <p class="version">
                                        1.0.0.3</p>
                                    <div class="more">
                                        <p>
                                            1. 重写了UI页面前台代码，使布局和外观更合理</p>
                                        <p>
                                            2. 添加了“周报告”一栏，提供周报告的上传和下载功能</p>
                                    </div>
                                </li>
                                <li class="cls">
                                    <p class="date">
                                        9月27日</p>
                                    <p class="intro">
                                        常规更新</p>
                                    <p class="version">
                                        1.0.0.2</p>
                                    <div class="more">
                                        <p>
                                            1. 修复了首页消息将经费单位显示为“元”的错误</p>
                                        <p>
                                            2. 调整了报销一栏中类别和备注的位置</p>
                                        <p>
                                            3. 在报销和分配部分的份额框后添加单位提醒</p>
                                        <p>
                                            4. 修复了成果上传时多个类别重叠显示的问题</p>
                                        <p>
                                            5. 将上传照片的分辨率改为200*250</p>
                                        <p>
                                            6. 优化了成果上传页面的刷新机制，保证了日期下拉菜单的正常使用</p>
                                        <p>
                                            7. 补充了部分新增功能的安全审核</p>
                                        <p>
                                            8. 对登录界面进行重新编写，使界面更加实用美观</p>
                                    </div>
                                </li>
                                <li class="cls highlight">
                                    <p class="date">
                                        9月26日</p>
                                    <p class="intro">
                                        科研项目管理系统第一版正式上线！</p>
                                    <p class="version">
                                        1.0.0.1</p>
                                    <div class="more">
                                        <p>
                                            1. 登录项目页面中，项目号改为下拉菜单选择</p>
                                        <p>
                                            2. 将经费分配功能放置到成员信息页面中</p>
                                        <p>
                                            3. 根据实际需要，重新编写了经费管理模块</p>
                                        <p>
                                            4. 去掉了UI界面中项目历史一栏内容</p>
                                        <p>
                                            5. 对安全机制进行优化，不再对敏感字符串进行检测</p>
                                        <p>
                                            6. 成果上传模块中日期部分改为下拉菜单选择模式</p>
                                        <p>
                                            7. 用户注册时将自动检测邮箱地址正确性</p>
                                        <p>
                                            8. 对背景图片进行重新选择</p>
                                        <p>
                                            9. 增加项目申请的入口</p>
                                        <p>
                                            10. 消息群发时，自动获取邮箱服务器各项信息</p>
                                        <p>
                                            11. 经费管理模块统一使用万元作为单位，int数据类型改为float类型</p>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <!--

        <li class="cls">
                                    <p class="date">
                                        1月17日</p>
                                    <p class="intro">
                                        常规更新</p>
                                    <p class="version">
                                        1.0.0.7</p>
                                    <div class="more">
                                    </div>
                                </li>



<div class="year">
                        <h2>
                            <a href="#">2015年<i></i></a></h2>
                        <div class="list">
                        <div>



        <div style="width:772px;height:740px;margin-right:auto;margin-left:auto;padding-top:60px;font-size:small;color:rgba(40,40,40,1)">
            <div style="margin-left:120px">

            <p style="font-weight:bold">2015.01.17 Version 1.0.0.7 （18033）</p>
            <p>1、使用性能监视器对连接数进行控制，彻底解决了连接不能销毁的BUG，防止服务器过载损坏</p>
            <p>2、修复了对IE浏览器的支持，目前支持IE11浏览器</p>
            <p>3、对部分数据库连接语句进行了优化，避免空连接出现</p>
            -------------------------------------------------------------<br />

            <p style="font-weight:bold">2015.01.15 Version 1.0.0.6 （17750）</p>
            <p>1、优化了弹出消息机制，提示时不再出现白屏</p>
            <p>2、登录界面输入全空时仅提示一次，不再多次提示</p>
            <p>3、经过测试，网页在IE11浏览器下完全显示正常，因此去掉了屏蔽IE浏览器的限制</p>
            <p>4、对成果页面点击“请选择类别”显示错乱的情况进行了修复</p>
            <p>5、放宽了对注册用户时照片格式的限制</p>
            <p>6、添加了错误转向页面，屏蔽了错误的详细信息</p>
            -------------------------------------------------------------<br />

            <p style="font-weight:bold">2014.12.14 Version 1.0.0.5 （178401）</p>
            <p>--1、修复了注册用户性别恒为“男”的错误</p>
            <p>--2、优化了注册表操作流程，修复了在长期运行之后的连接数满载问题</p>
            <p>--3、修复了申请加入项目页面出现崩溃的现象</p>
            <p>--4、放宽敏感字符过滤限制，对常用词汇不进行过滤</p>
            -------------------------------------------------------------<br />
            
            <p style="font-weight:bold">2014.10.10 Version 1.0.0.4 （174402）</p>
            <p>--1、重写了UI页面前台代码，使布局和外观更合理</p>
            <p>--2、添加了“周报告”一栏，提供周报告的上传和下载功能</p>
            -------------------------------------------------------------<br />

            <p style="font-weight:bold">2014.09.27 Version 1.0.0.3 （173903）</p>
            <p>--1、修复了首页消息将经费单位显示为“元”的错误</p>
            <p>--2、调整了报销一栏中类别和备注的位置</p>
            <p>--3、在报销和分配部分的份额框后添加单位提醒</p>
            <p>--4、修复了成果上传时多个类别重叠显示的问题</p>
            <p>--5、将上传照片的分辨率改为200*250</p>
            -------------------------------------------------------------<br />

            <p style="font-weight:bold">2014.09.27 Version 1.0.0.2 （173426）</p>
            <p>--1、优化了成果上传页面的刷新机制，保证了日期下拉菜单的正常使用</p>
            <p>--2、补充了部分新增功能的安全审核</p>
            <p>--3、对登录界面进行重新编写，使界面更加实用美观</p>
            -------------------------------------------------------------<br />

            <p style="font-weight:bold">2014.09.26 Version 1.0.0.1 （168992）</p>
            <p>--1、登录项目页面中，项目号改为下拉菜单选择</p>
            <p>--2、将经费分配功能放置到成员信息页面中</p>
            <p>--3、根据实际需要，重新编写了经费管理模块</p>
            <p>--4、去掉了UI界面中项目历史一栏内容</p>
            <p>--5、对安全机制进行优化，不再对敏感字符串进行检测</p>
            <p>--6、成果上传模块中日期部分改为下拉菜单选择模式</p>
            <p>--7、用户注册时将自动检测邮箱地址正确性</p>
            <p>--8、对背景图片进行重新选择</p>
            <p>--9、增加项目申请的入口</p>
            <p>--10、消息群发时，自动获取邮箱服务器各项信息</p>
            <p>--11、经费管理模块统一使用万元作为单位，int数据类型改为float类型</p>
        </div>
        -->
        </div>
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
    </form>
</body>
</html>
