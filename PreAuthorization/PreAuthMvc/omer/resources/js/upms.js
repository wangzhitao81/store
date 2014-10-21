/* 
 * @Description: 商户及收单服务网站前端框架
 * @Author: xiajun
 * @Update: xiajun(2012-12-27) 
 * @copyright:版权所有(C)，中国银联股份有限公司，2002－2015，所有权利保留
 */
(function (window, undefined) {
    // 定义局部变量
    var document = window.document,
		navigator = window.navigator,
		location = window.location,
		$ = window.jQuery,
		jQuery = window.jQuery;
    // 国际化
    var LANG = {};
    if (window.UPLANG) {
        LANG = window.UPLANG;
    }

    // ajax方法全局S
    $.ajaxSetup({
        statusCode: {
            500: function () {
                window.location.href = upmsUtils.getDomainHead() + "page/error500.jsp";
            }, 404: function () {
                window.location.href = upmsUtils.getDomainHead() + "page/error404.jsp";
            }
        }
    });
    // ajax方法全局E

    // 公共参数
    var PUBVAR = {};
    // cookie操作工具类
    var cookieUtils = {
        get: function (name) {// 获取cookie值
            var cookieName = encodeURIComponent(name) + "=",
				cookieStart = document.cookie.indexOf(cookieName),
				cookieValue = null;
            if (cookieStart > -1) {
                var cookieEnd = document.cookie.indexOf(";", cookieStart);
                if (cookieEnd == -1) {
                    cookieEnd = document.cookie.length;
                }
                cookieValue = decodeURIComponent(document.cookie.substring(cookieStart + cookieName.length, cookieEnd));
            }
            return cookieValue;
        },
        set: function (sName, sValue, oExpires, sPath, sDomain, bSecure) {// 设置cookie值
            var currDate = new Date(),
            	sExpires = typeof oExpires == 'undefined' ? '' : ';expires=' + new Date(currDate.getTime() + (oExpires * 24 * 60 * 60 * 1000)).toUTCString();
            document.cookie = sName + '=' + sValue + sExpires + ((sPath == null) ? '' : (' ;path=' + sPath)) + ((sDomain == null) ? '' : (' ;domain=' + sDomain)) + ((bSecure == true) ? ' ; secure' : '');
        },
        unset: function (name, path, domain, secure) {// 删除cookie值
            this.set(name, "", new Date(0), path, domain, secure);
        }
    };
    // upms框架通用工具类
    var upmsUtils = {
        getSysLanguage: function () {// 获取系统语言
            var sysLang = navigator.systemLanguage,
				userLang = navigator.userLanguage,
				bLang = navigator.language;

            if (sysLang != undefined) {
                return sysLang.toLowerCase();
            } else if (userLang != undefined) {
                return userLang.toLowerCase();
            } else if (bLang != undefined) {
                return bLang.toLowerCase();
            } else {// 默认
                return "zh-cn";
            }
        },
        formatToFen: function (data) {
            var afterDot = 0;
            var beforeDot = 0;
            var array = data.split(".");
            var lens = array.length;
            if (2 < lens) {
                return data;
            }
            if (1 == lens) {
                return parseInt(array[0], 10) * 100;
            } else if (2 == lens) {
                beforeDot = parseInt(array[0], 10);
                afterDot = (1 == array[1].length) ? (parseInt(array[1], 10) * 10)
						: (parseInt(array[1], 10));
                return beforeDot * 100 + afterDot;
            }
        },
        formatToFenByPoint: function (data, point) {
            var _point = parseInt(point, 10);
            if (0 == _point) {
                return data;
            }
            var xP = 100;
            if (3 == _point) {
                xP = 1000;
            }
            var afterDot = 0;
            var beforeDot = 0;
            var array = data.split(".");
            var lens = array.length;
            if (2 < lens) {
                return data;
            }
            if (1 == lens) {
                return parseInt(array[0], 10) * xP;
            } else if (2 == lens) {
                beforeDot = parseInt(array[0], 10);
                afterDot = (1 == array[1].length) ? (parseInt(array[1], 10) * 10)
						: (parseInt(array[1], 10));
                return beforeDot * xP + afterDot;
            }
        },
        getDomainHead: function () {// 获取网站url头  例如：http://127.0.0.1/UPMS/
            var sHost = location.host,
				sProtocol = location.protocol,
				sPathNm = location.pathname;
            var arrPathNm = sPathNm.split("\/");
            var domainHead = sProtocol + "\/\/" + sHost + "\/";
            if (arrPathNm.length > 1) {
                domainHead += arrPathNm[1] + "\/";
            }
            if (domainHead.substr(domainHead.length - 1) != "\/") {
                domainHead = domainHead + "\/";
            }
            return domainHead;
        }
    };
    // 校验工具类
    var checkUtils = {
        infoTip: function () {// 信息提示
            return "";
        },
        isEmptyStr: function () {// 判断字符串是否为空
            var str = arguments[0];
            if (str === undefined || str === null || $.trim(str) === "") {
                return true;
            } else {
                return false;
            }
        },
        isNotEmptyStr: function () {// 判断字符串是否不为空
            var str = arguments[0];
            if (str === undefined || str === null || $.trim(str) === "") {
                return false;
            } else {
                return true;
            }
        },
        isNull: function () {// 判断变量是不是为空
            var obj = arguments[0];
            if (obj === undefined || obj === null) {
                return true;
            } else {
                return false;
            }
        },
        isNotNull: function () {// 判断变量是不是不为空
            var obj = arguments[0];
            if (obj === undefined || obj === null) {
                return false;
            } else {
                return true;
            }
        },
        isChinese: function () {// 是否是中文
            var str = arguments[0];
            var regex = /.*[\u4e00-\u9fa5]+.*$/;
            return regex.test(str);
        },
        isNotChinese: function () {// 是否不是是中文
            var str = arguments[0];
            var regex = /.*[\u4e00-\u9fa5]+.*$/;
            return !regex.test(str);
        },
        isEmail: function () {//校验email格式
            var str = arguments[0];
            var regex = /^([a-zA-Z0-9_-]{1,})((.[a-zA-Z0-9_-]{1,}){0,})@([a-zA-Z0-9_-]{1,})((.[a-zA-Z0-9_-]{1,}){1,})$/;
            return regex.test(str);
        },
        isMobilePhone: function () {// 校验手机号码
            var str = arguments[0];
            var regex = /^1[3|4|5|8][0-9]\d{4,8}$/;
            return regex.test(str);
        },
        isPhone: function () {// 校验电话号码
            var str = arguments[0];
            var regex = /^(\d{3,4}-)?\d{7,8}$/;
            return regex.test(str);
        },
        isMoney: function () {// 判断是否是货币格式  正数
            var str = arguments[0];
            var regex = /^\d+(\.\d{1,2})?$/;
            return regex.test(str);
        },
        isMoneyForPoint: function (money, point) {
            var intPoint = parseInt(point, 10);
            var regex;
            switch (intPoint) {
                case 0:
                    if (-1 != money.indexOf(".")) {
                        return false;
                    }
                case 1:
                    regex = /^\d+(\.\d{1})?$/;
                    break;
                case 2:
                    regex = /^\d+(\.\d{1,2})?$/;
                    break;
                case 3:
                    regex = /^\d+(\.\d{1,3})?$/;
                    break;
                default:
                    regex = /^\d+(\.\d{1,2})?$/;
                    break;
            }
            return regex.test(money);
        },
        isZInt: function () {// 是否是正整数
            var str = arguments[0];
            var regex = /^[1-9]\d*$/;
            return regex.test(str);
        },
        formatToFen: function (data) {//货币转化为分
            var afterDot = 0;
            var beforeDot = 0;
            var array = data.split(".");
            var lens = array.length;
            if (2 < lens) {
                return data;
            }
            if (1 == lens) {
                return parseInt(array[0], 10) * 100;
            } else if (2 == lens) {
                beforeDot = parseInt(array[0], 10);
                afterDot = (1 == array[1].length) ? (parseInt(array[1], 10) * 10)
						: (parseInt(array[1], 10));
                return beforeDot * 100 + afterDot;
            }
        },
        isW: function (str) { //全角字符
            var regex = /^[\w]/;
            return !regex.test(str);
        },
        isLastMatch: function (str1, str2) {// 字符1是否以字符串2结束
            var index = str1.lastIndexOf(str2);
            if (!(str1.length == index + str2.length)) {
                return false;
            }
            return true;
        },
        isMatch: function (str1, str2) {// 字符1是包含字符串2
            var index = str1.indexOf(str2);
            if (index == -1) {
                return false;
            }
            return true;
        },
        isStartMatch: function (str1, str2) {// 字符1是否以字符2开始
            var sStartStr = str1.substr(0, 2);
            if (sStartStr == str2) {
                return true;
            }
            return false;
        },
        check6Date: function (upfilepath) {
            var myDate = new Date(),
				month = myDate.getMonth() + 1,
				date = myDate.getDate();
            month = $.trim('' + month);
            date = $.trim('' + date);
            if (month.length == 1) {
                month = '0' + month;
            }
            if (date.length == 1) {
                date = '0' + date;
            }
            var dtStr = $.trim(myDate.getFullYear() + "").substr(2, 2) + month + date,
				fileDate = upfilepath.substr(upfilepath.length - 13, 6);
            // 获得文件中的时间
            if (checkUtils.isMatch(dtStr, fileDate)) {
                return true;
            }
            return false;
        },
        getCurrDate: function (dtPattern) {// yymmdd 和 yyyymmdd
            var myDate = new Date(), month = myDate.getMonth() + 1, date = myDate.getDate();
            month = '' + month;
            date = '' + date;
            if (month.length == 1) {
                month = '0' + month;
            }
            if (date.length == 1) {
                date = '0' + date;
            }
            if (dtPattern.toLowerCase() == 'yymmdd') {
                return (myDate.getFullYear() + "").substr(2, 2) + month + date;
            } else if (dtPattern.toLowerCase() == 'yyyymmdd') {
                return myDate.getFullYear() + month + date;
            }
        },
        check8Date: function (upfilepath) {
            var myDate = new Date(),
				month = myDate.getMonth() + 1,
				date = myDate.getDate();
            month = $.trim('' + month);
            date = $.trim('' + date);
            if (month.length == 1) {
                month = '0' + month;
            }
            if (date.length == 1) {
                date = '0' + date;
            }
            var dtStr = myDate.getFullYear() + month + date,
				fileDate = upfilepath.substr(upfilepath.length - 17, 8);
            // 获得文件中的时间
            if (checkUtils.isMatch(dtStr, fileDate)) {
                return true;
            }
            return false;
        }
    };
    // 统一商户网站的首页
    var merhome = {
        /*登录页面 start*/
        logintime: "",
        loginindex: 1,
        loginshowimg: function (num) {
            num = num > 3 ? 1 : num;
            merhome.loginindex = num;
            $(".imgnum span").removeClass("onselect").eq(merhome.loginindex - 1).addClass("onselect");
            $("#banner_img li").hide().stop(true, true).eq(merhome.loginindex - 1).fadeIn("slow");
            merhome.loginindex = merhome.loginindex + 1 > 3 ? 1 : merhome.loginindex + 1;
            merhome.logintime = setTimeout("upms.merhome.loginshowimg(" + merhome.loginindex + ")", 4000);
        },
        logininit: function () {
            // 大图滚动	
            merhome.loginshowimg(merhome.loginindex);
            // 鼠标移入移出
            $(".imgnum span").hover(function () {
                clearTimeout(merhome.logintime);
                var icon = $(this).text();
                $(".imgnum span").removeClass("onselect").eq(icon - 1).addClass("onselect");
                $("#banner_img li").hide().stop(true, true).eq(icon - 1).fadeIn("slow");
            }, function () {
                merhome.loginindex = $(this).text() > 4 ? 1 : parseInt($(this).text()) + 1;
                merhome.logintime = setTimeout("upms.merhome.loginshowimg(" + merhome.loginindex + ")", 4000);
            });
            //登录框透明值
            $(".wrap_form").animate({ opacity: "0.5" }, 100);
            //输入框点击
            $(".ipt_txt").focusin(function () {
                $(this).removeClass().addClass("ipt_txt").addClass("txtclick");
                $(this).parents().next(".note_info").removeClass().addClass('note_info').html('');
            }).blur(function () {
                $(this).removeClass("txtclick");
            });
            // 国际化
            upms.WEBLANG.initLang($(".language"));
            // 验证码刷新
            upms.initRandCode("captchaCodeImg");
        },
        /*登录页面 end*/
        /*入网须知 start*/
        jnettime: "",
        jnetindex: 1,
        jnetshowimg: function (num) {
            num = num > 2 ? 1 : num;
            merhome.jnetindex = num;
            $(".imgnum span").removeClass("onselect").eq(merhome.jnetindex - 1).addClass("onselect");
            $("#banner_img li").hide().stop(true, true).eq(merhome.jnetindex - 1).fadeIn("slow");
            merhome.jnetindex = merhome.jnetindex + 1 > 2 ? 1 : merhome.jnetindex + 1;
            merhome.jnettime = setTimeout("upms.merhome.jnetshowimg(" + merhome.jnetindex + ")", 3000);
        },
        joinnetinit: function () {
            // 大图滚动
            merhome.jnetshowimg(merhome.jnetindex);
            //鼠标移入移出
            $(".imgnum span").hover(function () {
                clearTimeout(merhome.jnettime);
                var icon = $(this).text();
                $(".imgnum span").removeClass("onselect").eq(icon - 1).addClass("onselect");
                $("#banner_img li").hide().stop(true, true).eq(icon - 1).fadeIn("slow");
            }, function () {
                merhome.jnetindex = $(this).text() > 3 ? 1 : parseInt($(this).text()) + 1;
                merhome.jnettime = setTimeout("upms.merhome.jnetshowimg(" + merhome.jnetindex + ")", 3000);
            });
            //内容轮换
            $(".sub_left_txt").bind("mouseover", function () {
                if (!$(this).hasClass("no_show")) {
                    return false;
                }
                var ctn_id = $(this).index();
                $(this).removeClass("no_show");
                $(".right_text").removeClass("txt_show");
                if (ctn_id == 1) {
                    $(this).removeClass("sub2");
                    $(".sub_left_txt").eq(0).addClass("no_show sub1");
                    $(".right_text").eq(1).addClass("txt_show");
                }
                else {
                    $(this).removeClass("sub1");
                    $(".sub_left_txt").eq(1).addClass("no_show sub2");
                    $(".right_text").eq(0).addClass("txt_show");
                }
            });
        },
        /*入网须知 end*/
        /*接入指南 start*/
        guidetimer: {},
        guideindex: "",
        guideleft: "",
        guideplaybanner: function (index) {
            merhome.guideleft = $(".guide_banner li").eq(index).position().left;
            if (index == 4) {
                $(".tab_dr").css({ left: merhome.guideleft + 1, width: 194 });
            } else {
                $(".tab_dr").css({ left: merhome.guideleft + 1, width: 198 });
            }
            $(".guide_banner img").addClass("dn");
            $(".guide_banner img").eq(index).removeClass("dn");
        },
        guideinit: function () {
            $(".guide_banner .tab_bg").animate({ opacity: "0.5" }, 100);
            //鼠标移动效果
            $(".guide_banner li").mouseover(function () {
                clearInterval(merhome.guidetimer);
                merhome.guideleft = $(this).position().left;
                merhome.guideindex = $(this).index()
                merhome.guideplaybanner(merhome.guideindex);
                $(".guide_help").addClass("hide_guide");
                $(".guide_help").eq($(this).index()).removeClass("hide_guide");
            });
            //自动播放
            $(".guide_banner").hover(function () {
                clearInterval(merhome.guidetimer);
            }, function () {
                merhome.guidetimer = setInterval(function () {
                    merhome.guideindex++;
                    if (merhome.guideindex == 5) {
                        merhome.guideindex = 0;
                    }
                    merhome.guideplaybanner(merhome.guideindex);
                    $(".guide_help").addClass("hide_guide");
                    $(".guide_help").eq(merhome.guideindex).removeClass("hide_guide");
                }, 3000);
            }).trigger("mouseleave");
        },
        /*接入指南 end*/
        /*运营服务 start*/
        servicetimer: {},
        serviceindex: "",
        serviceleft: "",
        serviceplaybanner: function (index) {
            merhome.serviceleft = $(".service_banner li").eq(index).position().left;
            if (index == 3) {
                $(".tab_dr").css({ left: merhome.serviceleft + 1, width: 243 });
            } else {
                $(".tab_dr").css({ left: merhome.serviceleft + 1, width: 247 });
            }
            $(".service_banner img").addClass("dn");
            $(".service_banner img").eq(index).removeClass("dn");
        },
        serviceinit: function () {
            $(".service_banner .tab_bg").animate({ opacity: "0.5" }, 100);
            //鼠标移动效果
            $(".service_banner li").mouseover(function () {
                clearInterval(merhome.servicetimer);
                merhome.serviceleft = $(this).position().left;
                merhome.serviceindex = $(this).index();
                merhome.serviceplaybanner(merhome.serviceindex);
                $(".guide_help").addClass("hide_guide");
                $(".guide_help").eq($(this).index()).removeClass("hide_guide");
            });
            //自动播放
            $(".service_banner").hover(function () {
                clearInterval(merhome.servicetimer);
            }, function () {
                merhome.servicetimer = setInterval(function () {
                    merhome.serviceindex++;
                    if (merhome.serviceindex == 4) {
                        merhome.serviceindex = 0;
                    }
                    merhome.serviceplaybanner(merhome.serviceindex);
                    $(".guide_help").addClass("hide_guide");
                    $(".guide_help").eq(merhome.serviceindex).removeClass("hide_guide");
                }, 3000);
            }).trigger("mouseleave");
        }
        /*运营服务 end*/
        /*使用帮助 start*/

        /*使用帮助 end*/
    };
    // 个性化的内部对象
    var WEBLOGIN = {
        loginDynamicCss: function () {// 登录页面动态样式
            $(".ipt_txt").bind("blur", function () {
                $(this).removeClass("focusline");
            });
            $(".ipt_txt").bind("focus", function () {
                $(this).addClass("focusline");
            });
        }
    };
    // 个性化国际化内部对象
    var WEBLANG = {
        initLang: function ($obj) {// 国际化方法
            if ($obj.find("span").length == 0) {// 如果国际化下拉框无法正常初始化，则从cookie取语言标记
                window.location.href = upmsUtils.getDomainHead();
            }
            $obj.hover(function () {// 头部 国际化语言切换
                $("div", this).show();
            }, function () {
                $("div", this).hide();
            });
        }
    };
    //  个性化主页面
    var WEBMAIN = {
        initLayout: function () {// 主页面布局初始化
            var _winWidth = window.screen.width;
            $(".body-bg-left").css("width", _winWidth - 42);
            $(".nav_right").css("width", _winWidth - 365);
            upms.$webObj.css("width", _winWidth - 310);
        },
        initMenu: function (defUrl) {// 菜单初始化
            $(".left_menu .p_menu").click(function () {
                var chk = $(".menu_open", this);
                $(this).next("ul").slideToggle("normal", function () {
                    if ($(this).is(":visible")) {
                        chk.addClass("menu_close");
                    } else {
                        chk.removeClass("menu_close");
                    }
                });
            });
            $(".left_menu li a").click(function () {
                $(this).parent().click();
                return false;
            });
            $(".left_menu li").mouseover(function () {
                $(this).addClass("li_hover");
            }).mouseout(function () {
                $(this).removeClass("li_hover");
            }).click(function () {
                $(".left_menu li").removeClass("li_check li_hover");
                $(".left_menu li em").removeClass("em_check");
                $(this).addClass("li_check");
                $("em", this).addClass("em_check");
                var oAobj = $("a", this);
                var _link = oAobj.attr("href");
                //upms.$webObj.load(_link);
                upms.$load(upms.$webObj, _link, '', function () {
                    upms.hideOverLay();// 关闭遮罩
                });
                $("#currAddr_main").text($(".title", $(this).parent("ul").prev("div")).text());
                $("#currAddr_child").text(oAobj.text());
                //upms.hideOverLay();
            });

            //upms.$webObj.load(defUrl);
            //upms.$load(upms.$webObj, defUrl);

            $(".left_menu .home").click(function () {
                upms.$webObj.load(defUrl);
                upms.$load(upms.$webObj, defUrl);
                $("#currAddr_main").text(LANG.websit_home_lang);
                $("#currAddr_child").text(LANG.websit_main_welcome);
            })

            WEBMAIN.menuClickEvt();
        },
        menuClickEvt: function () {// 菜单点击事件
            //debugger;
            $(".left_menu > ul > li > a").each(function (ind, elem) {
                var aqObj = $(elem),
					actionUrl = aqObj.attr("action");
                aqObj.parent("li").bind("click", function () {
                    upms.showOverLay();// 打开遮罩
                    upms.clearWebObj();// 清空webObj对象
                    var $newPgDiv = upms.createPageDiv();
                    var data = {};
                    //$newPgDiv.load(actionUrl,data,function(){upms.hideOverLay();});
                    upms.$load($newPgDiv, actionUrl, data, function () { upms.hideOverLay(); });
                });
            });
        }
    };
    // 页面跳转
    var forward = {
        dialogId: "_forward_dialog_modal_",
        execute: function (params) {// 跳转页面执行方法
            var sUrl = $.trim(params.url),
				sDialog = params.dialog,
				sDialogTitle = params.dialogtitle,// 对话框的标题
				sDialogTip = params.dialogtip,
				sBtnName = $.trim(params.btnname);
            var $currPgDiv = upms.getCurrPageDiv();

            if (checkUtils.isEmptyStr(sDialogTip)) {
                sDailogTip = "你确定要操作吗?";
            }

            if (checkUtils.isEmptyStr(sDialogTitle)) {
                sDialogTitle = "信息提示";
            }

            $("a[name='" + sBtnName + "']", $currPgDiv).each(function (ind, elem) {
                $(elem).bind("click", function () {
                    if (sDialog != "open") {
                        upms.showOverLay();// 打开遮罩
                        var parentObj = $(elem).parent(),
							selKeys = $.trim($(elem).attr("sendparam")).split(",");
                        var data = {};// 传到服务端的参数
                        for (var i = 0; i < selKeys.length; i++) {
                            var tmpDataObj = {};
                            tmpDataObj[selKeys[i]] = $("input[name='" + selKeys[i] + "']", parentObj).val();
                            $.extend(data, tmpDataObj);
                        }
                        upms.saveHisPageDiv();// 保存历史记录
                        var $newPgDiv = upms.createPageDiv();
                        /*$newPgDiv.load(sUrl,data,function(){
							upms.hideOverLay();// 关闭遮罩
						});	*/
                        upms.$load($newPgDiv, sUrl, data, function () {
                            upms.hideOverLay();// 关闭遮罩
                        });
                    } else {
                        forward.createDialog(sDialogTitle, sDialogTip, $currPgDiv);
                        $("#" + forward.dialogId, $currPgDiv).dialog({
                            autoOpen: true,
                            modal: true,
                            dialogClass: "dialogfont",
                            buttons: {
                                "确定": function () {
                                    upms.showOverLay();// 打开遮罩
                                    var parentObj = $(elem).parent(),
										selKeys = $.trim($(elem).attr("sendparam")).split(",");
                                    var data = {};// 传到服务端的参数
                                    for (var i = 0; i < selKeys.length; i++) {
                                        var tmpDataObj = {};
                                        tmpDataObj[selKeys[i]] = $("input[name='" + selKeys[i] + "']", parentObj).val();
                                        $.extend(data, tmpDataObj);
                                    }
                                    upms.saveHisPageDiv();// 保存历史记录
                                    var $newPgDiv = upms.createPageDiv();
                                    /*$newPgDiv.load(sUrl,data,function(){
										upms.hideOverLay();// 关闭遮罩
									});	*/
                                    upms.$load($newPgDiv, sUrl, data, function () {
                                        upms.hideOverLay();// 关闭遮罩
                                    });
                                    $(this).dialog("close");
                                },
                                "取消": function () {
                                    $(this).dialog("close");
                                }
                            }
                        });
                    }
                });
            });
        },
        createDialog: function (sTitle, sInfoTip, $currPgDiv) {
            var arrHtml = new Array();
            arrHtml.push(" <div id='" + forward.dialogId + "' title='" + sTitle + "' style='display:none'>");
            arrHtml.push("<center><p class='dialogTip'>" + sInfoTip + "</p></center>");
            arrHtml.push("</div>");
            $currPgDiv.append(arrHtml.join(""));
        }
    };
    /*返回按钮操作*/
    var history = {
        go: function (params) {
            if ($.type(params) === "string") {
                upmsTipManage.clearUpmsTipObj();// 清空历史提示信息
                var $currPgDiv = upms.getCurrPageDiv();
                $("#" + params, $currPgDiv).bind("click", function () {
                    upms.hisGoPageDiv();
                });
            } else {
                if ($.isPlainObject(params) && !$.isEmptyObject(params)) {
                    var sBtnId = params.btnid,
						oParams = params.params,
						sModel = params.model;// requery：重查  clear：清空所有输入框的值 

                    switch (sModel) {
                        case "requery":
                            upmsTipManage.clearUpmsTipObj();// 清空历史提示信息
                            var $currPgDiv = upms.getCurrPageDiv();
                            $("#" + sBtnId, $currPgDiv).bind("click", function () {
                                upms.hisGoPageDiv();
                                $currPgDiv = upms.getCurrPageDiv();
                                pagequery.callbackQuery($currPgDiv);
                            });
                            break;
                        case "clear":
                            upmsTipManage.clearUpmsTipObj();// 清空历史提示信息
                            var $currPgDiv = upms.getCurrPageDiv();
                            $("#" + sBtnId, $currPgDiv).bind("click", function () {
                                upms.hisGoPageDiv();
                                $currPgDiv = upms.getCurrPageDiv();
                                $(":text", $currPgDiv).val("");// 清空文本框的值
                                // 初始化下拉框的值为空 S
                                var $selNull = $("select option[value='']", $currPgDiv);
                                if ($selNull.length == 1) {
                                    $selNull.attr("selected", "selected");
                                }
                                // 初始化下拉框的值为空 E
                                // 清空单选框
                                $(":radio", $currPgDiv).removeAttr("checked");
                                // 清空多选框
                                $(":checkbox", $currPgDiv).removeAttr("checked");
                                // 清空文本域
                                $("textarea", $currPgDiv).val("");
                                // 文件上传
                                $(":file", $currPgDiv).val("");
                                // 密码框
                                $(":password", $currPgDiv).val("");
                            });
                            break;
                        case "resetvalue":
                            upmsTipManage.clearUpmsTipObj();// 清空历史提示信息
                            var $currPgDiv = upms.getCurrPageDiv();
                            $("#" + sBtnId, $currPgDiv).bind("click", function () {
                                upms.hisGoPageDiv();
                                $currPgDiv = upms.getCurrPageDiv();
                                if (checkUtils.isNotNull(oParams) && $.isArray(oParams) && oParams.length > 0) {
                                    for (var i = 0, len = oParams.length; i < len; i++) {
                                        var tmpObj = oParams[i];
                                        var sTargetId = tmpObj.targetid,
											sTargetName = tmpObj.targetname,
											sType = tmpObj.type,
											sValue = tmpObj.value;
                                        switch (sType) {
                                            case "text":
                                                if (checkUtils.isNotEmptyStr(sTargetId)) {
                                                    $("#" + sTargetId, $currPgDiv).val(sValue);
                                                } else if (checkUtils.isNotEmptyStr(sTargetName)) {
                                                    $("input[type='text'][name='" + sTargetName + "']", $currPgDiv).val(sValue);
                                                }
                                                break;
                                            case "select":
                                                if (checkUtils.isNotEmptyStr(sTargetId)) {
                                                    $("#" + sTargetId + " option[value='" + sValue + "']", $currPgDiv).attr("selected", "selected");
                                                } else if (checkUtils.isNotEmptyStr(sTargetName)) {
                                                    $("select[name='" + sTargetName + "'] option[value='" + sValue + "']", $currPgDiv).attr("selected", "selected");
                                                }
                                                break;
                                            case "radio":
                                                if (checkUtils.isNotEmptyStr(sTargetId)) {
                                                    $("input:radio[id='" + sTargetId + "']", $currPgDiv).removeAttr("checked");
                                                    $("input:radio[id='" + sTargetId + "'][value='" + sValue + "']", $currPgDiv).attr("checked", "checked");
                                                } else if (checkUtils.isNotEmptyStr(sTargetName)) {
                                                    $("input:radio[name='" + sTargetName + "']", $currPgDiv).removeAttr("checked");
                                                    $("input:radio[name='" + sTargetName + "'][value='" + sValue + "']", $currPgDiv).attr("checked", "checked");
                                                }
                                                break;
                                            case "checkbox":
                                                if (checkUtils.isNotEmptyStr(sTargetId)) {
                                                    $("input:checkbox[id='" + sTargetId + "']", $currPgDiv).removeAttr("checked");
                                                    $("input:checkbox[id='" + sTargetId + "'][value='" + sValue + "']", $currPgDiv).attr("checked", "checked");
                                                } else if (checkUtils.isNotEmptyStr(sTargetName)) {
                                                    $("input:checkbox[name='" + sTargetName + "']", $currPgDiv).removeAttr("checked");
                                                    $("input:checkbox[name='" + sTargetName + "'][value='" + sValue + "']", $currPgDiv).attr("checked", "checked");
                                                }
                                                break;
                                            case "textarea":
                                                if (checkUtils.isNotEmptyStr(sTargetId)) {
                                                    $("#" + sTargetId, $currPgDiv).val(sValue);
                                                } else if (checkUtils.isNotEmptyStr(sTargetName)) {
                                                    $("textarea[name='" + sTargetName + "']").val(sValue);
                                                }
                                                break;
                                            case "file":
                                                if (checkUtils.isNotEmptyStr(sTargetId)) {
                                                    $("#" + sTargetId, $currPgDiv).val(sValue);
                                                } else {
                                                    $("input[type='file'][name='" + sTargetName + "']", $currPgDiv).val(sValue);
                                                }
                                                break;
                                            case "password":
                                                if (checkUtils.isNotEmptyStr(sTargetId)) {
                                                    $("#" + sTargetId, $currPgDiv).val(sValue);
                                                } else if (checkUtils.isNotEmptyStr(sTargetName)) {
                                                    $("input[type='password'][name='" + sTargetName + "']", $currPgDiv).val(sValue);
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            });
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    };
    // 分页查询
    var pagequery = {
        pageResultDivId: "",// 查询结果div的id值
        cacheValidateArr: [],// 缓存校验对象
        cacheErrInfoId: "",// 错误信息显示id
        execute: function (params) {// 分页查询方法
            var $currPgDiv = upms.getCurrPageDiv();
            debugger;
            pagequery.pageResultDivId = "";
            var sQuFrmId = params.queryformid,// 查询form的id
				sReqType = params.reqtype,// 请求类型
				sQuBtnId = params.querybtnid,// 查询按钮的id值
				sQuUrl = params.queryurl,// 分页查询的url
				sResDivId = params.resultdivid;// 查询结果div的id值
            pagequery.pageResultDivId = sResDivId;
            
            pagequery.savePageQueryParams($currPgDiv, sReqType, sQuFrmId, sQuUrl);// 保存分页查询的重要参数

            $("#" + sQuBtnId, $currPgDiv).bind("click", function () {// 给查询按钮添加查询事件
                debugger;
                upmsTipManage.clearUpmsTipObj();// 清空历史提示信息
                upms.showOverLay();// 开启遮罩
                var sQuParams = pagequery.quParamForStr(sQuFrmId, $currPgDiv);// 转化查询条件
                upms.ajax({
                    type: sReqType,
                    url: sQuUrl,
                    data: sQuParams,
                    success: function (retmsg) {
                        $("#" + sResDivId, $currPgDiv).html(retmsg);
                        upms.hideOverLay();// 关闭遮罩
                    },
                    error: function (retmsg) {
                        alert(retmsg);
                        upms.hideOverLay();// 关闭遮罩
                    }
                });
            });
        },
        verifyExecute: function (params) {// 带校验的查询
            var $currPgDiv = upms.getCurrPageDiv();
            pagequery.pageResultDivId = "";
            var sQuFrmId = params.queryformid,// 查询form的id
				sReqType = params.reqtype,// 请求类型
				sQuBtnId = params.querybtnid,// 查询按钮的id值
				sQuUrl = params.queryurl,// 分页查询的url,
				arrValidate = params.validate,// 校验规则
				sErrInfoId = params.errinfoid,// 提示错误信息的id
				sResDivId = params.resultdivid;// 查询结果div的id值
            if (checkUtils.isEmptyStr(sReqType)) {
                sReqType = "post";
            }
            debugger;
            pagequery.pageResultDivId = sResDivId;
            pagequery.cacheValidateArr = arrValidate;
            pagequery.cacheErrInfoId = sErrInfoId;

            pagequery.savePageQueryParams($currPgDiv, sReqType, sQuFrmId, sQuUrl);// 保存分页查询的重要参数

            $("#" + sQuBtnId, $currPgDiv).bind("click", function () {// 给查询按钮添加查询事件
                upmsTipManage.clearUpmsTipObj();// 清空历史提示信息
                pagequery.clearErrInfo(sErrInfoId, $currPgDiv);
                var flag = true;
                pagequery.setFormChkFlag(sQuFrmId, $currPgDiv);// 设置校验标记
                if ($.isArray(arrValidate)) {
                    for (var i = 0; i < arrValidate.length; i++) {
                        var obj = arrValidate[i];
                        var retVal = pagequery.validateForQuery(obj, sErrInfoId, $currPgDiv);
                        if ("nopass" == retVal) {
                            flag = false;
                            break;
                        }
                    }
                }
                if (flag) {
                    upms.showOverLay();// 开启遮罩
                    var sQuParams = pagequery.quParamForStr(sQuFrmId, $currPgDiv);// 转化查询条件
                    upms.ajax({
                        type: sReqType,
                        url: sQuUrl,
                        data: sQuParams,
                        success: function (retmsg) {
                            $("#" + sResDivId, $currPgDiv).html(retmsg);
                            upms.hideOverLay();// 关闭遮罩
                        },
                        error: function (retmsg) {
                            alert(retmsg);
                            upms.hideOverLay();// 关闭遮罩
                        }
                    });
                }
            });
        },
        savePageQueryParams: function ($currPgDiv, sReqType, sQuFrmId, sQuUrl, sOperType) {// 保存分页查询的参数
            debugger;
            var HISPGPREF = "hggp_";
            var sCurrPgDivId = $currPgDiv.attr("id");
            var $hisReqTp = $("#" + sCurrPgDivId + HISPGPREF + "type", $currPgDiv),
				$hisFrm = $("#" + sCurrPgDivId + HISPGPREF + "frm", $currPgDiv),
				$operType = $("#" + sCurrPgDivId + HISPGPREF + "opertype", $currPgDiv),
				$hisUrl = $("#" + sCurrPgDivId + HISPGPREF + "url", $currPgDiv);
            if (checkUtils.isNotEmptyStr(sOperType)) {// 存储类型，如果值为paging
                if ($operType.length == 0) {
                    $operType = $("<input type='hidden' id='" + sCurrPgDivId + HISPGPREF + "opertype' value='" + sOperType + "' />");
                    $currPgDiv.append($operType);
                } else {
                    $operType.val(sOperType);
                }
            }
            if (checkUtils.isNotEmptyStr(sReqType)) {// 存储请求类型
                if ($hisReqTp.length == 0) {
                    $hisReqTp = $("<input type='hidden' id='" + sCurrPgDivId + HISPGPREF + "type' value='" + sReqType + "' />");
                    $currPgDiv.append($hisReqTp);
                } else {
                    $hisReqTp.val(sReqType);
                }
            }
            if (checkUtils.isNotEmptyStr(sQuFrmId)) {// 存储参数的form
                if ($hisFrm.length == 0) {
                    $hisFrm = $("<input type='hidden' id='" + sCurrPgDivId + HISPGPREF + "frm' value='" + sQuFrmId + "' />");
                    $currPgDiv.append($hisFrm);
                } else {
                    $hisFrm.val(sQuFrmId);
                }
            }
            if (checkUtils.isNotEmptyStr(sQuUrl)) {// 存储url
                if ($hisUrl.length == 0) {
                    $hisUrl = $("<input type='hidden' id='" + sCurrPgDivId + HISPGPREF + "url' value='" + sQuUrl + "' />");
                    $currPgDiv.append($hisUrl);
                } else {
                    $hisUrl.val(sQuUrl);
                }
            }
        },
        callbackQuery: function ($currPgDiv, callback) {// 回调查询  用于想grid对话框确定按钮回调查询
            debugger;
            var HISPGPREF = "hggp_";
            var sCurrPgDivId = $currPgDiv.attr("id");

            var $hisReqTp = $("#" + sCurrPgDivId + HISPGPREF + "type", $currPgDiv),
				$hisFrm = $("#" + sCurrPgDivId + HISPGPREF + "frm", $currPgDiv),
				$operType = $("#" + sCurrPgDivId + HISPGPREF + "opertype", $currPgDiv),
				$hisUrl = $("#" + sCurrPgDivId + HISPGPREF + "url", $currPgDiv);

            var sHisReqTp = "", sHisFrm = "", sHisUrl = "", sOperType = "";

            var flag = true;

            if ($hisReqTp.length > 0) {
                sHisReqTp = $.trim($hisReqTp.val());
            } else {
                flag = false;
            }

            if ($operType.length > 0) {
                sOperType = $.trim($operType.val());
            }

            if ($hisFrm.length > 0) {
                sHisFrm = $.trim($hisFrm.val());
            } else {
                if (checkUtils.isEmptyStr(sOperType)) {
                    flag = false;
                } else {
                    if (sOperType != "paging") {
                        flag = false;
                    }
                }
            }

            if ($hisUrl.length > 0) {
                sHisUrl = $.trim($hisUrl.val());
            } else {
                flag = false;
            }

            if (flag) {
                if (sOperType != "paging") {
                    var chkFlag = true;// 校验标记
                    if (pagequery.getFormChkFlag(sHisFrm, $currPgDiv) && $.isArray(pagequery.cacheValidateArr)) {
                        pagequery.clearErrInfo(pagequery.cacheErrInfoId, $currPgDiv);
                        var arrValidate = pagequery.cacheValidateArr;
                        for (var i = 0; i < arrValidate.length; i++) {
                            var obj = arrValidate[i];
                            var retVal = pagequery.validateForQuery(obj, pagequery.cacheErrInfoId, $currPgDiv);
                            if ("nopass" == retVal) {
                                chkFlag = false;
                                break;
                            }
                        }
                    }
                    if (chkFlag) {
                        upms.showOverLay();// 开启遮罩
                        var sQuParams = pagequery.quParamForStr(sHisFrm, $currPgDiv);// 转化查询条件
                        upms.ajax({
                            type: sHisReqTp,
                            url: sHisUrl,
                            data: sQuParams,
                            success: function (retmsg) {
                                debugger;
                                $("#" + pagequery.pageResultDivId, $currPgDiv).html(retmsg);
                                upms.hideOverLay();// 关闭遮罩
                            },
                            error: function (retmsg) {
                                alert(retmsg);
                                upms.hideOverLay();// 关闭遮罩
                            }
                        });
                    }
                } else {
                    upms.ajax({
                        type: sHisReqTp,
                        url: sHisUrl,
                        data: {},
                        success: function (retmsg) {
                            debugger;
                            $("#" + pagequery.pageResultDivId, $currPgDiv).html(retmsg);
                            if (checkUtils.isNotNull(callback) && $.isFunction(callback)) {
                                callback();
                            }
                            upms.hideOverLay();// 关闭遮罩
                        },
                        error: function (retmsg) {
                            alert(retmsg);
                            upms.hideOverLay();// 关闭遮罩
                        }
                    });
                }
            }
        },
        setFormChkFlag: function (sQuFrmId, $currPgDiv) {// 设置是否需要校验的标记
            $("#" + sQuFrmId, $currPgDiv).attr("oncheckflag", "true");
        },
        getFormChkFlag: function (sQuFrmId, $currPgDiv) {// 获取校验标记
            var sFrmChkFlag = $("#" + sQuFrmId, $currPgDiv).attr("oncheckflag");
            if (checkUtils.isEmptyStr(sFrmChkFlag)) {
                return false;
            } else {
                if (sFrmChkFlag == "true") {
                    return true;
                } else {
                    return false;
                }
            }
        },
        clearErrInfo: function (sErrInfoId, $currPgDiv) {// 清除错误信息
            $("#" + sErrInfoId, $currPgDiv).html("");
            $("#" + sErrInfoId, $currPgDiv).hide();
        },
        onFalse: function (sErrInfoId, sErrMsg, $currPgDiv) {
            $("#" + sErrInfoId, $currPgDiv).html(sErrMsg);
            $("#" + sErrInfoId, $currPgDiv).show();
        },
        validateForQuery: function (obj, sErrInfoId, $currPgDiv) {
            var sTargetId = obj.targetid,
				sTargetName = obj.targetname,
				sType = obj.type,
				fOncheck = obj.oncheck,
				sFalseDesc = obj.falsedesc;
            switch (sType) {
                case "text":
                    var $obj = upms.transTo$obj(sTargetId, sTargetName, sType, $currPgDiv);
                    if (!$.isEmptyObject($obj)) {
                        if (fOncheck($obj.val()) == true) {// 校验通过
                            return "pass";
                        } else if (fOncheck($obj.val()) == false) {// 校验不通过
                            pagequery.onFalse(sErrInfoId, sFalseDesc, $currPgDiv);
                            return "nopass";
                        }
                    } else {
                        return "next";
                    }
                    break;
                default:
                    break;
            }
        },
        quParamForStr: function (sQuFrmId, $currPgDiv) {// 转化查询参数
            return $("#" + sQuFrmId, $currPgDiv).serialize();
        },
        initpaging: function (params) {// 分页初始化
            debugger;
            var $currPgDiv = upms.getCurrPageDiv();
            var iPageNo = parseInt($("#pageNo", $currPgDiv).val()),// 当前页
				iTotalPages = parseInt($("#totalPages", $currPgDiv).val()),// 总页数
				sQueryFormId = params.queryformid,// 
				sPgBtnId = params.pgbtnid,// 存放数字按钮元素的id值
				sOperType = params.opertype,
				sResDivId = params.resultdivid,// 查询结果div的id值
				sUrl = params.url;// 分页查询的url
            if (checkUtils.isEmptyStr(pagequery.pageResultDivId) && checkUtils.isNotEmptyStr(sResDivId)) {
                pagequery.pageResultDivId = sResDivId;
            }
            if (checkUtils.isNotEmptyStr(sOperType) && sOperType == 'requery') {
                pagequery.savePageQueryParams($currPgDiv, "post", "", sUrl, "paging");
            }

            var $pgBtn = $("#" + sPgBtnId, $currPgDiv);
            var htmlArr = new Array();

            // 当当前页数不在可使用页数范围
            if (iPageNo > iTotalPages) {
                iPageNo = iTotalPages;
            } else if (iPageNo < 1) {
                iPageNo = 1;
            }

            if (iTotalPages <= 5) {// 总记录少于5页
                htmlArr = new Array();
                htmlArr.push('<a href="javascript: void(0);" id="firstPage">首页</a>');
                htmlArr.push('<a href="javascript: void(0);" id="prePage">上一页</a>');
                for (var i = 1; i <= iTotalPages; i++) {
                    if (iPageNo == i) {
                        htmlArr.push('<a href="javascript: void(0);" class="checked" id="pgnum' + i + '">' + i + '</a>');
                    } else {
                        htmlArr.push('<a href="javascript: void(0);" id="pgnum' + i + '">' + i + '</a>');
                    }
                }
                htmlArr.push('<a href="javascript: void(0);" id="nextPage">下一页</a>');
                htmlArr.push('<a href="javascript: void(0);" id="endPage">末页</a>');
            } else if (iTotalPages > 5) {// 当总页数大于例如5页时候
                if ((iPageNo - 2) <= 1) {
                    htmlArr = new Array();
                    htmlArr.push('<a href="javascript: void(0);" id="firstPage">首页</a>');
                    htmlArr.push('<a href="javascript: void(0);" id="prePage">上一页</a>');
                    for (var i = 1; i <= 5; i++) {
                        if (iPageNo == i) {
                            htmlArr.push('<a href="javascript: void(0);" class="checked" id="pgnum' + i + '">' + i + '</a>');
                        } else {
                            htmlArr.push('<a href="javascript: void(0);" id="pgnum' + i + '">' + i + '</a>');
                        }
                    }
                    htmlArr.push('<a class="ellipsis">...</a>');
                    htmlArr.push('<a href="javascript: void(0);" id="nextPage">下一页</a>');
                    htmlArr.push('<a href="javascript: void(0);" id="endPage">末页</a>');
                } else if ((iPageNo + 2) >= iTotalPages) {
                    htmlArr.push('<a href="javascript: void(0);" id="firstPage">首页</a>');
                    htmlArr.push('<a href="javascript: void(0);" id="prePage">上一页</a>');
                    htmlArr.push('<a class="ellipsis">...</a>');
                    for (var i = (iTotalPages - 4) ; i <= iTotalPages; i++) {
                        if (iPageNo == i) {
                            htmlArr.push('<a href="javascript: void(0);" class="checked" id="pgnum' + i + '">' + i + '</a>');
                        } else {
                            htmlArr.push('<a href="javascript: void(0);" id="pgnum' + i + '">' + i + '</a>');
                        }
                    }
                    htmlArr.push('<a href="javascript: void(0);" id="nextPage">下一页</a>');
                    htmlArr.push('<a href="javascript: void(0);" id="endPage">末页</a>');
                } else {
                    htmlArr.push('<a href="javascript: void(0);" id="firstPage">首页</a>');
                    htmlArr.push('<a href="javascript: void(0);" id="prePage">上一页</a>');
                    htmlArr.push('<a class="ellipsis">...</a>');
                    for (var i = (iPageNo - 2) ; i <= (iPageNo + 2) ; i++) {
                        if (iPageNo == i) {
                            htmlArr.push('<a href="javascript: void(0);" class="checked" id="pgnum' + i + '">' + i + '</a>');
                        } else {
                            htmlArr.push('<a href="javascript: void(0);" id="pgnum' + i + '">' + i + '</a>');
                        }
                    }
                    htmlArr.push('<a class="ellipsis">...</a>');
                    htmlArr.push('<a href="javascript: void(0);" id="nextPage">下一页</a>');
                    htmlArr.push('<a href="javascript: void(0);" id="endPage">末页</a>');
                }
            }
            if (htmlArr.length > 0) {
                $pgBtn.append(htmlArr.join(""));
                pagequery.pagingEvt(params, $currPgDiv);
            }

        },
        pagingEvt: function (params, $currPgDiv) {// 分页的点击事件
            //debugger;
            var sQueryFormId = params.queryformid,// 查询条件的formid
				sResDivId = params.resultdivid,// 查询结果div的id值
				sUrl = params.url;// 分页查询的url

            if (checkUtils.isEmptyStr(sResDivId)) {
                sResDivId = pagequery.pageResultDivId;
            }

            $("a[id^='pgnum']", $currPgDiv).each(function (ind, elem) {// 数字按钮点击事件
                $(elem).bind("click", function () {
                    upmsTipManage.clearUpmsTipObj();// 清空历史提示信息
                    var chkFlag = true;// 校验标记
                    if (pagequery.getFormChkFlag(sQueryFormId, $currPgDiv) && $.isArray(pagequery.cacheValidateArr)) {
                        pagequery.clearErrInfo(pagequery.cacheErrInfoId, $currPgDiv);
                        var arrValidate = pagequery.cacheValidateArr;
                        for (var i = 0; i < arrValidate.length; i++) {
                            var obj = arrValidate[i];
                            var retVal = pagequery.validateForQuery(obj, pagequery.cacheErrInfoId, $currPgDiv);
                            if ("nopass" == retVal) {
                                chkFlag = false;
                                break;
                            }
                        }
                    }
                    if (chkFlag) {
                        upms.showOverLay();// 开启遮罩
                        var pgNumID = $(elem).attr("id"),
							pgUrl = $.trim(sUrl);
                        var pageNo = parseInt(pgNumID.substring("pgnum".length, pgNumID.length)),
							totalPages = parseInt($.trim($('#totalPages', $currPgDiv).val()));
                        var pgAjaxStr = pagequery.quParamForStr(sQueryFormId, $currPgDiv);
                        if (checkUtils.isEmptyStr(pgAjaxStr)) {
                            pgAjaxStr = "pageNo=" + pageNo;
                        } else {
                            pgAjaxStr += "&pageNo=" + pageNo;
                        }
                        upms.ajax({
                            type: "post",
                            url: pgUrl,
                            data: pgAjaxStr,
                            success: function (retmsg) {
                                $("#" + sResDivId, $currPgDiv).html(retmsg);
                                upms.hideOverLay();// 关闭遮罩
                            },
                            error: function (retmsg) {
                                alert(retmsg);
                                upms.hideOverLay();// 关闭遮罩
                            }
                        });
                    }
                });
            });

            $("#goBtn", $currPgDiv).bind("click", function () {// 分页的确定按钮
                debugger;
                upmsTipManage.clearUpmsTipObj();// 清空历史提示信息
                var chkFlag = true;// 校验标记
                if (pagequery.getFormChkFlag(sQueryFormId, $currPgDiv) && $.isArray(pagequery.cacheValidateArr)) {
                    pagequery.clearErrInfo(pagequery.cacheErrInfoId, $currPgDiv);
                    var arrValidate = pagequery.cacheValidateArr;
                    for (var i = 0; i < arrValidate.length; i++) {
                        var obj = arrValidate[i];
                        var retVal = pagequery.validateForQuery(obj, pagequery.cacheErrInfoId, $currPgDiv);
                        if ("nopass" == retVal) {
                            chkFlag = false;
                            break;
                        }
                    }
                }
                if (chkFlag) {
                    upms.showOverLay();// 开启遮罩
                    var iptPageNo = isNaN(parseInt($("#goPageNo", $currPgDiv).val())) ? 1 : parseInt($("#goPageNo", $currPgDiv).val()),
						pageNo = parseInt($.trim($("#pageNo", $currPgDiv).val())),
						totalPages = parseInt($.trim($('#totalPages', $currPgDiv).val())),
						pgUrl = $.trim(sUrl);
                    if (iptPageNo < 1) {
                        pageNo = 1;
                    } else if (iptPageNo > totalPages) {
                        pageNo = totalPages;
                    } else {
                        pageNo = iptPageNo;
                    }
                    var pgAjaxStr = pagequery.quParamForStr(sQueryFormId, $currPgDiv);
                    if (checkUtils.isEmptyStr(pgAjaxStr)) {
                        pgAjaxStr = "pageNo=" + pageNo;
                    } else {
                        pgAjaxStr += "&pageNo=" + pageNo;
                    }
                    upms.ajax({
                        type: "post",
                        url: pgUrl,
                        data: pgAjaxStr,
                        success: function (retmsg) {
                            $("#" + sResDivId, $currPgDiv).html(retmsg);
                            upms.hideOverLay();// 关闭遮罩
                        },
                        error: function (retmsg) {
                            alert(retmsg);
                            upms.hideOverLay();// 关闭遮罩
                        }
                    });
                }
            });

            $("#firstPage", $currPgDiv).bind("click", function () {// 首页
                debugger;
                upmsTipManage.clearUpmsTipObj();// 清空历史提示信息
                var chkFlag = true;// 校验标记
                if (pagequery.getFormChkFlag(sQueryFormId, $currPgDiv) && $.isArray(pagequery.cacheValidateArr)) {
                    pagequery.clearErrInfo(pagequery.cacheErrInfoId, $currPgDiv);
                    var arrValidate = pagequery.cacheValidateArr;
                    for (var i = 0; i < arrValidate.length; i++) {
                        var obj = arrValidate[i];
                        var retVal = pagequery.validateForQuery(obj, pagequery.cacheErrInfoId, $currPgDiv);
                        if ("nopass" == retVal) {
                            chkFlag = false;
                            break;
                        }
                    }
                }
                if (chkFlag) {
                    upms.showOverLay();// 开启遮罩
                    var pgUrl = $.trim(sUrl),
						pageNo = parseInt(1),
						totalPages = parseInt($.trim($('#totalPages', $currPgDiv).val()));
                    var pgAjaxStr = pagequery.quParamForStr(sQueryFormId, $currPgDiv);
                    if (checkUtils.isEmptyStr(pgAjaxStr)) {
                        pgAjaxStr = "pageNo=" + pageNo;
                    } else {
                        pgAjaxStr += "&pageNo=" + pageNo;
                    }
                    upms.ajax({
                        type: "post",
                        url: pgUrl,
                        data: pgAjaxStr,
                        success: function (retmsg) {
                            $("#" + sResDivId, $currPgDiv).html(retmsg);
                            upms.hideOverLay();// 关闭遮罩
                        },
                        error: function (retmsg) {
                            alert(retmsg);
                            upms.hideOverLay();// 关闭遮罩
                        }
                    });
                }
            });

            $("#prePage", $currPgDiv).bind("click", function () {// 上一页
                debugger;
                upmsTipManage.clearUpmsTipObj();// 清空历史提示信息
                var chkFlag = true;// 校验标记
                if (pagequery.getFormChkFlag(sQueryFormId, $currPgDiv) && $.isArray(pagequery.cacheValidateArr)) {
                    pagequery.clearErrInfo(pagequery.cacheErrInfoId, $currPgDiv);
                    var arrValidate = pagequery.cacheValidateArr;
                    for (var i = 0; i < arrValidate.length; i++) {
                        var obj = arrValidate[i];
                        var retVal = pagequery.validateForQuery(obj, pagequery.cacheErrInfoId, $currPgDiv);
                        if ("nopass" == retVal) {
                            chkFlag = false;
                            break;
                        }
                    }
                }
                if (chkFlag) {
                    upms.showOverLay();// 开启遮罩
                    var pgUrl = $.trim(sUrl),
						pageNo = parseInt($.trim($("#pageNo", $currPgDiv).val())),
						totalPages = parseInt($.trim($('#totalPages', $currPgDiv).val()));
                    if (pageNo > 1) {
                        pageNo = pageNo - 1;
                    } else {
                        pageNo = 1;
                    }
                    var pgAjaxStr = pagequery.quParamForStr(sQueryFormId, $currPgDiv);
                    if (checkUtils.isEmptyStr(pgAjaxStr)) {
                        pgAjaxStr = "pageNo=" + pageNo;
                    } else {
                        pgAjaxStr += "&pageNo=" + pageNo;
                    }
                    upms.ajax({
                        type: "post",
                        url: pgUrl,
                        data: pgAjaxStr,
                        success: function (retmsg) {
                            $("#" + sResDivId, $currPgDiv).html(retmsg);
                            upms.hideOverLay();// 关闭遮罩
                        },
                        error: function (retmsg) {
                            alert(retmsg);
                            upms.hideOverLay();// 关闭遮罩
                        }
                    });
                }
            });

            $("#nextPage", $currPgDiv).bind("click", function () {// 下一页
                debugger;
                upmsTipManage.clearUpmsTipObj();// 清空历史提示信息
                var chkFlag = true;// 校验标记
                if (pagequery.getFormChkFlag(sQueryFormId, $currPgDiv) && $.isArray(pagequery.cacheValidateArr)) {
                    pagequery.clearErrInfo(pagequery.cacheErrInfoId, $currPgDiv);
                    var arrValidate = pagequery.cacheValidateArr;
                    for (var i = 0; i < arrValidate.length; i++) {
                        var obj = arrValidate[i];
                        var retVal = pagequery.validateForQuery(obj, pagequery.cacheErrInfoId, $currPgDiv);
                        if ("nopass" == retVal) {
                            chkFlag = false;
                            break;
                        }
                    }
                }
                if (chkFlag) {
                    upms.showOverLay();// 开启遮罩
                    var pgUrl = $.trim(sUrl),
						pageNo = parseInt($.trim($("#pageNo", $currPgDiv).val())),
						totalPages = parseInt($.trim($('#totalPages', $currPgDiv).val()));
                    if (pageNo < totalPages) {
                        pageNo = pageNo + 1;
                    } else {
                        pageNo = totalPages;
                    }
                    var pgAjaxStr = pagequery.quParamForStr(sQueryFormId, $currPgDiv);
                    if (checkUtils.isEmptyStr(pgAjaxStr)) {
                        pgAjaxStr = "pageNo=" + pageNo;
                    } else {
                        pgAjaxStr += "&pageNo=" + pageNo;
                    }
                    upms.ajax({
                        type: "post",
                        url: pgUrl,
                        data: pgAjaxStr,
                        success: function (retmsg) {
                            $("#" + sResDivId, $currPgDiv).html(retmsg);
                            upms.hideOverLay();// 关闭遮罩
                        },
                        error: function (retmsg) {
                            alert(retmsg);
                            upms.hideOverLay();// 关闭遮罩
                        }
                    });
                }
            });
            $("#endPage", $currPgDiv).bind("click", function () {// 末页
                debugger;
                upmsTipManage.clearUpmsTipObj();// 清空历史提示信息
                var chkFlag = true;// 校验标记
                if (pagequery.getFormChkFlag(sQueryFormId, $currPgDiv) && $.isArray(pagequery.cacheValidateArr)) {
                    pagequery.clearErrInfo(pagequery.cacheErrInfoId, $currPgDiv);
                    var arrValidate = pagequery.cacheValidateArr;
                    for (var i = 0; i < arrValidate.length; i++) {
                        var obj = arrValidate[i];
                        var retVal = pagequery.validateForQuery(obj, pagequery.cacheErrInfoId, $currPgDiv);
                        if ("nopass" == retVal) {
                            chkFlag = false;
                            break;
                        }
                    }
                }
                if (chkFlag) {
                    upms.showOverLay();// 开启遮罩
                    var pageNo = parseInt($.trim($('#pageNo', $currPgDiv).val())),
						pgUrl = $.trim(sUrl),
						totalPages = parseInt($.trim($('#totalPages', $currPgDiv).val()));
                    if (pageNo != totalPages) {
                        pageNo = totalPages;
                    }
                    var pgAjaxStr = pagequery.quParamForStr(sQueryFormId, $currPgDiv);
                    if (checkUtils.isEmptyStr(pgAjaxStr)) {
                        pgAjaxStr = "pageNo=" + pageNo;
                    } else {
                        pgAjaxStr += "&pageNo=" + pageNo;
                    }
                    upms.ajax({
                        type: "post",
                        url: pgUrl,
                        data: pgAjaxStr,
                        success: function (retmsg) {
                            $("#" + sResDivId, $currPgDiv).html(retmsg);
                            upms.hideOverLay();// 关闭遮罩
                        },
                        error: function (retmsg) {
                            alert(retmsg);
                            upms.hideOverLay();// 关闭遮罩
                        }
                    });
                }
            });
        }
    };
    // 查询列表的dialog操作
    var griddialog = {
        dialogId: "_grid_dialog_modal_",
        selfexecute: function (params) {
            // to do ....
        },
        signcert: function (params) {// 签名证书
            var sUrl = params.url,// 请求的url
				sBtnName = $.trim(params.btnname),
				sDialogTitle = params.dialogtitle,// 对话框的标题
				sErrInfoId = params.errinfoid,
				sErrInfoTip = params.errinfotip,
				sSuccInfoId = params.succinfoid,
				sSuccInfoTip = params.succinfotip,
				sDialogTip = params.dialogtip;// 对话框的显示内容
            var $currPgDiv = upms.getCurrPageDiv();
            sDailogTip = checkUtils.isEmptyStr(sDialogTip) ? "你确定要操作吗?" : sDailogTip;
            sDialogTitle = checkUtils.isEmptyStr(sDialogTip) ? "信息提示?" : sDialogTitle;
            var tipArrs = new Array();
            if (checkUtils.isNotEmptyStr(sErrInfoId)) {
                tipArrs.push(sErrInfoId);
            }
            if (checkUtils.isNotEmptyStr(sSuccInfoId)) {
                tipArrs.push(sSuccInfoId);
            }
            upmsTipManage.saveUpmsTipObj(tipArrs);

            $("a[name='" + sBtnName + "']", $currPgDiv).each(function (ind, elem) {
                $(elem).bind("click", function () {
                    var innerFlag = true;
                    var errmsg = signcert.signData("sharpxiajun");
                    if (!errmsg.flag) {
                        innerFlag = false;
                        griddialog.createDialogForId(griddialog.dialogId + "signcert", sDialogTitle, errmsg.desc, $currPgDiv, "error");
                        $("#" + griddialog.dialogId + "signcert", $currPgDiv).dialog({
                            autoOpen: true,
                            modal: true,
                            dialogClass: "dialogfont",
                            buttons: {
                                "确定": function () {
                                    $(this).dialog("close");
                                }
                            }
                        });
                    }
                    if (innerFlag) {
                        if (checkUtils.isNotEmptyStr(sErrInfoId)) {
                            griddialog.clearErrInfo(sErrInfoId, $currPgDiv);
                        }
                        if (checkUtils.isNotEmptyStr(sSuccInfoId)) {
                            griddialog.clearErrInfo(sSuccInfoId, $currPgDiv);
                        }
                        griddialog.createDialog(sDialogTitle, sDialogTip, $currPgDiv);
                        $("#" + griddialog.dialogId, $currPgDiv).dialog({
                            autoOpen: true,
                            modal: true,
                            dialogClass: "dialogfont",
                            buttons: {
                                "确定": function () {
                                    upms.showOverLay();// 打开遮罩
                                    var parentObj = $(elem).parent(),
										selKeys = $.trim($(elem).attr("sendparam")).split(",");
                                    var data = {};// 传到服务端的参数
                                    for (var i = 0; i < selKeys.length; i++) {
                                        var tmpDataObj = {};
                                        tmpDataObj[selKeys[i]] = $("input[name='" + selKeys[i] + "']", parentObj).val();
                                        $.extend(data, tmpDataObj);
                                    }
                                    var sCertSigned = $.trim($("#signedData", $currPgDiv).val());
                                    $.extend(data, { "signedData": sCertSigned });
                                    upms.ajax({
                                        type: "post",
                                        url: sUrl,
                                        data: data,
                                        success: function (retmsg) {
                                            if (checkUtils.isNotNull(retmsg)) {
                                                if (retmsg.jsonFlag) {// 成功
                                                    var callback = function () {
                                                        if (checkUtils.isNotEmptyStr(sSuccInfoId)) {
                                                            if (checkUtils.isEmptyStr(sSuccInfoTip)) {
                                                                sSuccInfoTip = "操作成功";
                                                            }
                                                            if (checkUtils.isNotEmptyStr(retmsg.jsonMsg)) {
                                                                griddialog.onFalse(sSuccInfoId, retmsg.jsonMsg, $currPgDiv);
                                                            } else {
                                                                griddialog.onFalse(sSuccInfoId, sSuccInfoTip, $currPgDiv);
                                                            }
                                                        }
                                                    }
                                                    pagequery.callbackQuery($currPgDiv, callback);
                                                    if (checkUtils.isNotEmptyStr(sSuccInfoId)) {
                                                        if (checkUtils.isNotEmptyStr(retmsg.jsonMsg)) {
                                                            griddialog.onFalse(sSuccInfoId, retmsg.jsonMsg, $currPgDiv);
                                                        } else {
                                                            griddialog.onFalse(sSuccInfoId, sSuccInfoTip, $currPgDiv);
                                                        }
                                                    }
                                                } else {// 失败
                                                    if (checkUtils.isEmptyStr(sErrInfoTip)) {
                                                        sErrInfoTip = "操作失败";
                                                    }

                                                    if (checkUtils.isEmptyStr(sErrInfoId)) {
                                                        if (checkUtils.isNotEmptyStr(retmsg.jsonMsg)) {
                                                            alert(retmsg.jsonMsg);
                                                        } else {
                                                            alert(sErrInfoTip);
                                                        }
                                                    } else {
                                                        if (checkUtils.isNotEmptyStr(retmsg.jsonMsg)) {
                                                            griddialog.onFalse(sErrInfoId, retmsg.jsonMsg, $currPgDiv);
                                                        } else {;
                                                            griddialog.onFalse(sErrInfoId, sErrInfoTip, $currPgDiv);
                                                        }
                                                    }
                                                }
                                            }
                                            upms.hideOverLay();// 关闭遮罩
                                        },
                                        error: function (retmsg) {
                                            alert(retmsg);
                                            upms.hideOverLay();// 关闭遮罩
                                        }
                                    });
                                    $(this).dialog("close");
                                },
                                "取消": function () {
                                    $(this).dialog("close");
                                }
                            }
                        });

                    }
                });
            });
        },
        execute: function (params) {
            var sUrl = params.url, // 请求的url
                sBtnName = $.trim(params.btnname),
                sDialogTitle = params.dialogtitle, // 对话框的标题
                sErrInfoId = params.errinfoid,
                sErrInfoTip = params.errinfotip,
                sSuccInfoId = params.succinfoid,
                sCheckSms = params.checksms, // 是否需要短信验证 open为需要 默认是close
                sSmsTip = params.smstip, // 短信验证提示语言
                sSuccInfoTip = params.succinfotip,
                sDialogTip = params.dialogtip;// 对话框的显示内容
            //debugger;
            var $currPgDiv = upms.getCurrPageDiv();
            if (checkUtils.isEmptyStr(sDialogTip)) {
                sDailogTip = "你确定要操作吗?";
            }
            if (checkUtils.isEmptyStr(sDialogTitle)) {
                sDialogTitle = "信息提示";
            }
            if (checkUtils.isEmptyStr(sSmsTip)) {
                sSmsTip = "短信验证不通过!";
            }
            if (sCheckSms != "open") {
                sCheckSms = "close";
            }
            var tipArrs = new Array();
            if (checkUtils.isNotEmptyStr(sErrInfoId)) {
                tipArrs.push(sErrInfoId);
            }
            if (checkUtils.isNotEmptyStr(sSuccInfoId)) {
                tipArrs.push(sSuccInfoId);
            }
            upmsTipManage.saveUpmsTipObj(tipArrs);

            $("a[name='" + sBtnName + "']", $currPgDiv).each(function (ind, elem) {
                $(elem).bind("click", function () {
                    //debugger;
                    var innerFlag = true;
                    if (sCheckSms == "open") {
                        if (sms.getSmsCheckFlag() == false) {
                            innerFlag = false;
                            griddialog.createDialogForId(griddialog.dialogId + "sms", sDialogTitle, sSmsTip, $currPgDiv);
                            $("#" + griddialog.dialogId + "sms", $currPgDiv).dialog({
                                autoOpen: true,
                                modal: true,
                                dialogClass: "dialogfont",
                                buttons: {
                                    "确定": function () {
                                        $(this).dialog("close");
                                    }
                                }
                            });
                        }
                    }
                    if (innerFlag) {

                        if (checkUtils.isNotEmptyStr(sErrInfoId)) {
                            griddialog.clearErrInfo(sErrInfoId, $currPgDiv);
                        }
                        if (checkUtils.isNotEmptyStr(sSuccInfoId)) {
                            griddialog.clearErrInfo(sSuccInfoId, $currPgDiv);
                        }
                        griddialog.createDialog(sDialogTitle, sDialogTip, $currPgDiv);
                        $("#" + griddialog.dialogId, $currPgDiv).dialog({
                            autoOpen: true,
                            modal: true,
                            dialogClass: "dialogfont",
                            buttons: {
                                "确定": function () {
                                    upms.showOverLay();// 打开遮罩
                                    var parentObj = $(elem).parent(),
										selKeys = $.trim($(elem).attr("sendparam")).split(",");
                                    var data = {};// 传到服务端的参数
                                    for (var i = 0; i < selKeys.length; i++) {
                                        var tmpDataObj = {};
                                        tmpDataObj[selKeys[i]] = $("input[name='" + selKeys[i] + "']", parentObj).val();
                                        $.extend(data, tmpDataObj);
                                    }
                                    upms.ajax({
                                        type: "post",
                                        url: sUrl,
                                        data: data,
                                        datatype:"json",
                                        success: function (retmsg) {
                                            debugger;
                                            retmsg = JSON.parse(retmsg);
                                            if (checkUtils.isNotNull(retmsg)) {
                                                if (retmsg.jsonFlag) {// 成功
                                                    var callback = function() {
                                                        if (checkUtils.isNotEmptyStr(sSuccInfoId)) {
                                                            if (checkUtils.isEmptyStr(sSuccInfoTip)) {
                                                                sSuccInfoTip = "操作成功";
                                                            }
                                                            if (checkUtils.isNotEmptyStr(retmsg.jsonMsg)) {
                                                                griddialog.onFalse(sSuccInfoId, retmsg.jsonMsg, $currPgDiv);
                                                            } else {
                                                                griddialog.onFalse(sSuccInfoId, sSuccInfoTip, $currPgDiv);
                                                            }
                                                        }
                                                    };
                                                    debugger;
                                                    pagequery.callbackQuery($currPgDiv, callback);                                                    
                                                    if (checkUtils.isNotEmptyStr(sSuccInfoId)) {
                                                        if (checkUtils.isNotEmptyStr(retmsg.jsonMsg)) {
                                                            griddialog.onFalse(sSuccInfoId, retmsg.jsonMsg, $currPgDiv);
                                                        } else {
                                                            griddialog.onFalse(sSuccInfoId, sSuccInfoTip, $currPgDiv);
                                                        }
                                                    }
                                                } else {// 失败
                                                    if (checkUtils.isEmptyStr(sErrInfoTip)) {
                                                        sErrInfoTip = "操作失败";
                                                    }

                                                    if (checkUtils.isEmptyStr(sErrInfoId)) {
                                                        if (checkUtils.isNotEmptyStr(retmsg.jsonMsg)) {
                                                            alert(retmsg.jsonMsg);
                                                        } else {
                                                            alert(sErrInfoTip);
                                                        }
                                                    } else {
                                                        if (checkUtils.isNotEmptyStr(retmsg.jsonMsg)) {
                                                            griddialog.onFalse(sErrInfoId, retmsg.jsonMsg, $currPgDiv);
                                                        } else {;
                                                            griddialog.onFalse(sErrInfoId, sErrInfoTip, $currPgDiv);
                                                        }
                                                    }
                                                }
                                            }
                                            upms.hideOverLay();// 关闭遮罩
                                        },
                                        error: function (retmsg) {
                                            alert(retmsg);
                                            upms.hideOverLay();// 关闭遮罩
                                        }
                                    });
                                    $(this).dialog("close");
                                },
                                "取消": function () {
                                    $(this).dialog("close");
                                }
                            }
                        });

                    }
                });
            });
        },
        createDialogForId: function (createDialogId, sTitle, sInfoTip, $currPgDiv, infoType) {
            var arrHtml = new Array();
            if (infoType == "error") {
                arrHtml.push("<div id='" + createDialogId + "' title='" + sTitle + "' style='display:none'>");
                arrHtml.push("<center><p class='dialogErrTip'>" + sInfoTip + "</p></center>");
                arrHtml.push("</div>");
                $currPgDiv.append(arrHtml.join(""));
            } else {
                arrHtml.push("<div id='" + createDialogId + "' title='" + sTitle + "' style='display:none'>");
                arrHtml.push("<center><p class='dialogTip'>" + sInfoTip + "</p></center>");
                arrHtml.push("</div>");
                $currPgDiv.append(arrHtml.join(""));
            }
        },
        createDialog: function (sTitle, sInfoTip, $currPgDiv) {
            var arrHtml = new Array();
            arrHtml.push("<div id='" + griddialog.dialogId + "' title='" + sTitle + "' style='display:none'>");
            arrHtml.push("<center><p class='dialogTip'>" + sInfoTip + "</p></center>");
            arrHtml.push("</div>");
            $currPgDiv.append(arrHtml.join(""));
        },
        clearErrInfo: function (sErrInfoId, $currPgDiv) {// 清除错误信息
            $("#" + sErrInfoId, $currPgDiv).html("");
            $("#" + sErrInfoId, $currPgDiv).hide();
        },
        onFalse: function (sErrInfoId, sErrMsg, $currPgDiv) {
            $("#" + sErrInfoId, $currPgDiv).html(sErrMsg);
            $("#" + sErrInfoId, $currPgDiv).show();
        }
    };
    // 提示信息统一管理对象
    var upmsTipManage = {
        tipObjArr: [],
        currPgDivId: "",
        saveUpmsTipObj: function (arrParams) {
            if ($.isArray(arrParams)) {
                upmsTipManage.tipObjArr = arrParams;
                var $currPgDiv = upms.getCurrPageDiv();
                upmsTipManage.currPgDivId = $.trim($currPgDiv.attr("id"));
            }
        },
        clearUpmsTipObj: function () {
            if ($.isArray(upmsTipManage.tipObjArr) && upmsTipManage.tipObjArr.length > 0 && checkUtils.isNotEmptyStr(upmsTipManage.currPgDivId)) {
                for (var i = 0, len = upmsTipManage.tipObjArr.length; i < len; i++) {
                    var $currPgObj = $("#" + upmsTipManage.currPgDivId),
						sArrElem = upmsTipManage.tipObjArr[i];
                    $("#" + sArrElem, $currPgObj).html("");
                    $("#" + sArrElem, $currPgObj).hide();
                }
            }
        }
    };
    // 按钮操作
    var handlebtn = {
        dialogId: "_hb_forward_dialog_modal_",
        upfileDialogId: "_hb_upfile_dialog_modal_",
        downfileDialogId: "_hb_downfile_dialog_modal_",
        btnForUpFileParams: {},
        signCertFile: function (params) {// 签名文件的方法
            var sUploadPathId = $.trim(params.uploadpathid),
				sCertPathId = $.trim(params.certpathid),
				sUploadPathTip = $.trim(params.uploadpathtip),
				sCertPathTip = $.trim(params.certpathtip),
				sBtnTip = $.trim(params.btntip),
				sCertType = $.trim(params.certtype),
				sBtnId = $.trim(params.btnid);
            var $currPgDiv = upms.getCurrPageDiv();
            $("#" + sBtnId, $currPgDiv).bind("click", function () {
                var sUpPathPar = $("#" + sUploadPathId, $currPgDiv).val(),
					sCertPathPar = $("#" + sCertPathId, $currPgDiv).val();
                $("#" + sUploadPathTip, $currPgDiv).html("");
                $("#" + sUploadPathTip, $currPgDiv).removeClass("errTip");
                $("#" + sUploadPathTip, $currPgDiv).removeClass("infoTip");
                $("#" + sCertPathTip, $currPgDiv).html("");
                $("#" + sCertPathTip, $currPgDiv).removeClass("errTip");
                $("#" + sCertPathTip, $currPgDiv).removeClass("infoTip");
                $("#" + sBtnTip, $currPgDiv).html("");
                $("#" + sBtnTip, $currPgDiv).removeClass("errTip");
                $("#" + sBtnTip, $currPgDiv).removeClass("infoTip");
                if (checkUtils.isEmptyStr(sUpPathPar)) {
                    $("#" + sUploadPathTip, $currPgDiv).addClass("errTip");
                    $("#" + sUploadPathTip, $currPgDiv).html("文件名不能为空！");
                } else {
                    var sUpFileNmAll = sUpPathPar.substring(sUpPathPar.lastIndexOf("\\") + 1, sUpPathPar.length);
                    if (!(sUpFileNmAll.length == 31 || sUpFileNmAll.length == 42)) {
                        $("#" + sUploadPathTip, $currPgDiv).addClass("errTip");
                        $("#" + sUploadPathTip, $currPgDiv).html("文件名长度不正确！");
                    } else {
                        if (checkUtils.isLastMatch(sUpPathPar, ".txt")) {
                            if (checkUtils.isMatch(sUpPathPar, sCertType)) {
                                if (sUpFileNmAll.length == 42) {// 新版代扣文件
                                    if (checkUtils.check8Date(sUpPathPar)) {
                                        var batNoReg = /^[0-9]{4}$/;
                                        if (batNoReg.test(sUpPathPar.substring(sUpPathPar.length - 9, sUpPathPar.length - 5))) {
                                            if (sUpFileNmAll.substr(37, 1) != "I") {
                                                $("#" + sUploadPathTip, $currPgDiv).addClass("errTip");
                                                $("#" + sUploadPathTip, $currPgDiv).html("文件名要以I字符结尾!");
                                            } else {
                                                if (sUpFileNmAll.substr(2, 8) != "00000000") {
                                                    $("#" + sUploadPathTip, $currPgDiv).addClass("errTip");
                                                    $("#" + sUploadPathTip, $currPgDiv).html("文件名DK后要有0000000!");
                                                } else {
                                                    var signRetObj = signcert.signFile(sUpPathPar);
                                                    if (checkUtils.isNotNull(signRetObj)) {
                                                        if (signRetObj.flag == true) {
                                                            $("#" + sBtnTip, $currPgDiv).addClass("infoTip");
                                                            $("#" + sBtnTip, $currPgDiv).html("生成签名文件成功。");
                                                        } else {
                                                            $("#" + sBtnTip, $currPgDiv).addClass("errTip");
                                                            $("#" + sBtnTip, $currPgDiv).html(signRetObj.desc);
                                                        }
                                                    }
                                                }
                                            }
                                        } else {
                                            $("#" + sUploadPathTip, $currPgDiv).addClass("errTip");
                                            $("#" + sUploadPathTip, $currPgDiv).html("文件名里所包含的批次号正确！");
                                        }
                                    } else {
                                        $("#" + sUploadPathTip, $currPgDiv).addClass("errTip");
                                        $("#" + sUploadPathTip, $currPgDiv).html("文件名里的日期必须是当天！");
                                    }
                                } else if (sUpFileNmAll.length == 31) {// 老版代扣文件
                                    if (checkUtils.check6Date(sUpPathPar)) {
                                        // 新版代扣文件
                                        var batNoReg = /^[0-9]{2}$/;
                                        if (batNoReg.test(sUpPathPar.substring(sUpPathPar.length - 6, sUpPathPar.length - 4))) {
                                            var signRetObj = signcert.signFile(sUpPathPar);
                                            if (checkUtils.isNotNull(signRetObj)) {
                                                if (signRetObj.flag == true) {
                                                    $("#" + sBtnTip, $currPgDiv).addClass("infoTip");
                                                    $("#" + sBtnTip, $currPgDiv).html("生成签名文件成功。");
                                                } else {
                                                    $("#" + sBtnTip, $currPgDiv).addClass("errTip");
                                                    $("#" + sBtnTip, $currPgDiv).html(signRetObj.desc);
                                                }
                                            }
                                        } else {
                                            $("#" + sUploadPathTip, $currPgDiv).addClass("errTip");
                                            $("#" + sUploadPathTip, $currPgDiv).html("文件名里所包含的批次号正确！");
                                        }
                                    } else {
                                        $("#" + sUploadPathTip, $currPgDiv).addClass("errTip");
                                        $("#" + sUploadPathTip, $currPgDiv).html("文件名里的日期必须是当天！");
                                    }
                                }
                            } else {
                                $("#" + sUploadPathTip, $currPgDiv).addClass("errTip");
                                $("#" + sUploadPathTip, $currPgDiv).html("文件名要以DK开头！");
                            }
                        } else {
                            $("#" + sUploadPathTip, $currPgDiv).addClass("errTip");
                            $("#" + sUploadPathTip, $currPgDiv).html("文件名要以.txt结尾！");
                        }
                    }
                }
            });
        },
        downfile: function (params) {// 文件下载
            var sBtnId = params.btnid,// 按钮的id值
				oValidate = params.validate,// 传入校验参数
				sFormId = params.formid,// 请求的form
				oAjaxCheck = params.ajaxcheck,// ajax校验
				sDownUrl = params.downurl;
            var $currPgDiv = upms.getCurrPageDiv();
            var chkFlag = false, sChkModel = "";
            if ($.isPlainObject(oValidate) && !$.isEmptyObject(oValidate)) {
                sChkModel = oValidate.model;
                if (checkUtils.isNotEmptyStr(sChkModel)) {
                    chkFlag = true;
                }
            }
            if (chkFlag) {// 需要校验操作
                switch (sChkModel) {
                    case "formsevent":
                        handlebtn.saveCheckFlag(true, $currPgDiv);
                        handlebtn.validateBtn(oValidate, $currPgDiv);
                        $("#" + sBtnId, $currPgDiv).bind("click", function () {
                            handlebtn.allValidate(oValidate, $currPgDiv);
                            if (handlebtn.getCheckFlag($currPgDiv) == true) {

                                if (checkUtils.isNotNull(oAjaxCheck) && !$.isEmptyObject(oAjaxCheck)) {
                                    var sUrl = oAjaxCheck.url,// 校验的url
										sReqType = oAjaxCheck.reqtype,// 请求方式 例如 get  post
										sDialogTitle = oAjaxCheck.dialogtitle,// 对话框的标题
										sDialogTip = oAjaxCheck.dialogtip;// 对话框提示信息
                                    if (checkUtils.isEmptyStr(sReqType)) {
                                        sReqType = "post";
                                    }
                                    if (checkUtils.isEmptyStr(sDialogTitle)) {
                                        sDialogTitle = "信息提示";
                                    }
                                    if (checkUtils.isEmptyStr(sDialogTip)) {
                                        sDialogTip = "文件下载失败!";
                                    }
                                    upms.ajax({
                                        type: sReqType,
                                        url: sUrl,
                                        data: $("#" + sFormId).serialize(),
                                        success: function (msg) {
                                            if (checkUtils.isNotNull(msg)) {
                                                if (msg.jsonFlag == true) {
                                                    var sReqData = "";
                                                    if (checkUtils.isNotEmptyStr(sFormId)) {
                                                        sReqData = $("#" + sFormId).serialize();
                                                    }
                                                    if (checkUtils.isNotEmptyStr(sReqData)) {
                                                        window.location.href = sDownUrl + "?" + sReqData;
                                                    } else {
                                                        window.location.href = sDownUrl;
                                                    }
                                                } else {
                                                    if (checkUtils.isNotEmptyStr(msg.jsonMsg)) {
                                                        sDialogTip = msg.jsonMsg;
                                                    }
                                                    handlebtn.createDialog(handlebtn.downfileDialogId + sBtnId + "err", sDialogTitle, sDialogTip, $currPgDiv, "error");
                                                    $("#" + handlebtn.downfileDialogId + sBtnId + "err", $currPgDiv).dialog({
                                                        autoOpen: true,
                                                        modal: true,
                                                        dialogClass: "dialogfont",
                                                        buttons: {
                                                            "确定": function () {
                                                                $(this).dialog("close");
                                                            }
                                                        }
                                                    });
                                                }
                                            } else {
                                                handlebtn.createDialog(handlebtn.downfileDialogId + sBtnId + "err", sDialogTitle, sDialogTip, $currPgDiv, "error");
                                                $("#" + handlebtn.downfileDialogId + sBtnId + "err", $currPgDiv).dialog({
                                                    autoOpen: true,
                                                    modal: true,
                                                    dialogClass: "dialogfont",
                                                    buttons: {
                                                        "确定": function () {
                                                            $(this).dialog("close");
                                                        }
                                                    }
                                                });
                                            }
                                        },
                                        error: function (msg) {
                                            handlebtn.createDialog(handlebtn.downfileDialogId + sBtnId + "err", sDialogTitle, sDialogTip, $currPgDiv, "error");
                                            $("#" + handlebtn.downfileDialogId + sBtnId + "err", $currPgDiv).dialog({
                                                autoOpen: true,
                                                modal: true,
                                                dialogClass: "dialogfont",
                                                buttons: {
                                                    "确定": function () {
                                                        $(this).dialog("close");
                                                    }
                                                }
                                            });
                                        }
                                    });
                                } else {
                                    var sReqData = "";
                                    if (checkUtils.isNotEmptyStr(sFormId)) {
                                        sReqData = $("#" + sFormId).serialize();
                                    }
                                    if (checkUtils.isNotEmptyStr(sReqData)) {
                                        window.location.href = sDownUrl + "?" + sReqData;
                                    } else {
                                        window.location.href = sDownUrl;
                                    }
                                }
                            }
                        });
                        break;
                    case "batch":
                        handlebtn.saveCheckFlag(true, $currPgDiv);
                        $("#" + sBtnId, $currPgDiv).bind("click", function () {
                            handlebtn.batchValidate(oValidate, $currPgDiv);
                            if (handlebtn.getCheckFlag($currPgDiv) == true) {

                                if (checkUtils.isNotNull(oAjaxCheck) && !$.isEmptyObject(oAjaxCheck)) {
                                    var sUrl = oAjaxCheck.url,// 校验的url
										sReqType = oAjaxCheck.reqtype,// 请求方式 例如 get  post
										sDialogTitle = oAjaxCheck.dialogtitle,// 对话框的标题
										sDialogTip = oAjaxCheck.dialogtip;// 对话框提示信息
                                    if (checkUtils.isEmptyStr(sReqType)) {
                                        sReqType = "post";
                                    }
                                    if (checkUtils.isEmptyStr(sDialogTitle)) {
                                        sDialogTitle = "信息提示";
                                    }
                                    if (checkUtils.isEmptyStr(sDialogTip)) {
                                        sDialogTip = "文件下载失败!";
                                    }
                                    upms.ajax({
                                        type: sReqType,
                                        url: sUrl,
                                        data: $("#" + sFormId).serialize(),
                                        success: function (msg) {
                                            if (checkUtils.isNotNull(msg)) {
                                                if (msg.jsonFlag == true) {
                                                    var sReqData = "";
                                                    if (checkUtils.isNotEmptyStr(sFormId)) {
                                                        sReqData = $("#" + sFormId).serialize();
                                                    }
                                                    if (checkUtils.isNotEmptyStr(sReqData)) {
                                                        window.location.href = sDownUrl + "?" + sReqData;
                                                    } else {
                                                        window.location.href = sDownUrl;
                                                    }
                                                } else {
                                                    if (checkUtils.isNotEmptyStr(msg.jsonMsg)) {
                                                        sDialogTip = msg.jsonMsg;
                                                    }
                                                    handlebtn.createDialog(handlebtn.downfileDialogId + sBtnId + "err", sDialogTitle, sDialogTip, $currPgDiv, "error");
                                                    $("#" + handlebtn.downfileDialogId + sBtnId + "err", $currPgDiv).dialog({
                                                        autoOpen: true,
                                                        modal: true,
                                                        dialogClass: "dialogfont",
                                                        buttons: {
                                                            "确定": function () {
                                                                $(this).dialog("close");
                                                            }
                                                        }
                                                    });
                                                }
                                            } else {
                                                handlebtn.createDialog(handlebtn.downfileDialogId + sBtnId + "err", sDialogTitle, sDialogTip, $currPgDiv, "error");
                                                $("#" + handlebtn.downfileDialogId + sBtnId + "err", $currPgDiv).dialog({
                                                    autoOpen: true,
                                                    modal: true,
                                                    dialogClass: "dialogfont",
                                                    buttons: {
                                                        "确定": function () {
                                                            $(this).dialog("close");
                                                        }
                                                    }
                                                });
                                            }
                                        },
                                        error: function (msg) {
                                            handlebtn.createDialog(handlebtn.downfileDialogId + sBtnId + "err", sDialogTitle, sDialogTip, $currPgDiv, "error");
                                            $("#" + handlebtn.downfileDialogId + sBtnId + "err", $currPgDiv).dialog({
                                                autoOpen: true,
                                                modal: true,
                                                dialogClass: "dialogfont",
                                                buttons: {
                                                    "确定": function () {
                                                        $(this).dialog("close");
                                                    }
                                                }
                                            });
                                        }
                                    });
                                } else {
                                    var sReqData = "";
                                    if (checkUtils.isNotEmptyStr(sFormId)) {
                                        sReqData = $("#" + sFormId).serialize();
                                    }
                                    if (checkUtils.isNotEmptyStr(sReqData)) {
                                        window.location.href = sDownUrl + "?" + sReqData;
                                    } else {
                                        window.location.href = sDownUrl;
                                    }
                                }
                            }
                        });
                        break;
                    case "single":
                        handlebtn.saveCheckFlag(true, $currPgDiv);
                        $("#" + sBtnId, $currPgDiv).bind("click", function () {
                            handlebtn.singleValidate(oValidate, $currPgDiv);
                            if (handlebtn.getCheckFlag($currPgDiv) == true) {

                                if (checkUtils.isNotNull(oAjaxCheck) && !$.isEmptyObject(oAjaxCheck)) {
                                    var sUrl = oAjaxCheck.url,// 校验的url
										sReqType = oAjaxCheck.reqtype,// 请求方式 例如 get  post
										sDialogTitle = oAjaxCheck.dialogtitle,// 对话框的标题
										sDialogTip = oAjaxCheck.dialogtip;// 对话框提示信息
                                    if (checkUtils.isEmptyStr(sReqType)) {
                                        sReqType = "post";
                                    }
                                    if (checkUtils.isEmptyStr(sDialogTitle)) {
                                        sDialogTitle = "信息提示";
                                    }
                                    if (checkUtils.isEmptyStr(sDialogTip)) {
                                        sDialogTip = "文件下载失败!";
                                    }
                                    upms.ajax({
                                        type: sReqType,
                                        url: sUrl,
                                        data: $("#" + sFormId).serialize(),
                                        success: function (msg) {
                                            if (checkUtils.isNotNull(msg)) {
                                                if (msg.jsonFlag == true) {
                                                    var sReqData = "";
                                                    if (checkUtils.isNotEmptyStr(sFormId)) {
                                                        sReqData = $("#" + sFormId).serialize();
                                                    }
                                                    if (checkUtils.isNotEmptyStr(sReqData)) {
                                                        window.location.href = sDownUrl + "?" + sReqData;
                                                    } else {
                                                        window.location.href = sDownUrl;
                                                    }
                                                } else {
                                                    if (checkUtils.isNotEmptyStr(msg.jsonMsg)) {
                                                        sDialogTip = msg.jsonMsg;
                                                    }
                                                    handlebtn.createDialog(handlebtn.downfileDialogId + sBtnId + "err", sDialogTitle, sDialogTip, $currPgDiv, "error");
                                                    $("#" + handlebtn.downfileDialogId + sBtnId + "err", $currPgDiv).dialog({
                                                        autoOpen: true,
                                                        modal: true,
                                                        dialogClass: "dialogfont",
                                                        buttons: {
                                                            "确定": function () {
                                                                $(this).dialog("close");
                                                            }
                                                        }
                                                    });
                                                }
                                            } else {
                                                handlebtn.createDialog(handlebtn.downfileDialogId + sBtnId + "err", sDialogTitle, sDialogTip, $currPgDiv, "error");
                                                $("#" + handlebtn.downfileDialogId + sBtnId + "err", $currPgDiv).dialog({
                                                    autoOpen: true,
                                                    modal: true,
                                                    dialogClass: "dialogfont",
                                                    buttons: {
                                                        "确定": function () {
                                                            $(this).dialog("close");
                                                        }
                                                    }
                                                });
                                            }
                                        },
                                        error: function (msg) {
                                            handlebtn.createDialog(handlebtn.downfileDialogId + sBtnId + "err", sDialogTitle, sDialogTip, $currPgDiv, "error");
                                            $("#" + handlebtn.downfileDialogId + sBtnId + "err", $currPgDiv).dialog({
                                                autoOpen: true,
                                                modal: true,
                                                dialogClass: "dialogfont",
                                                buttons: {
                                                    "确定": function () {
                                                        $(this).dialog("close");
                                                    }
                                                }
                                            });
                                        }
                                    });
                                } else {
                                    var sReqData = "";
                                    if (checkUtils.isNotEmptyStr(sFormId)) {
                                        sReqData = $("#" + sFormId).serialize();
                                    }
                                    if (checkUtils.isNotEmptyStr(sReqData)) {
                                        window.location.href = sDownUrl + "?" + sReqData;
                                    } else {
                                        window.location.href = sDownUrl;
                                    }
                                }
                            }
                        });
                        break;
                    default:
                        break;
                }
            } else {// 不需要校验的操作
                $("#" + sBtnId, $currPgDiv).bind("click", function () {

                    if (checkUtils.isNotNull(oAjaxCheck) && !$.isEmptyObject(oAjaxCheck)) {
                        var sUrl = oAjaxCheck.url,// 校验的url
							sReqType = oAjaxCheck.reqtype,// 请求方式 例如 get  post
							sDialogTitle = oAjaxCheck.dialogtitle,// 对话框的标题
							sDialogTip = oAjaxCheck.dialogtip;// 对话框提示信息
                        if (checkUtils.isEmptyStr(sReqType)) {
                            sReqType = "post";
                        }
                        if (checkUtils.isEmptyStr(sDialogTitle)) {
                            sDialogTitle = "信息提示";
                        }
                        if (checkUtils.isEmptyStr(sDialogTip)) {
                            sDialogTip = "文件下载失败!";
                        }
                        upms.ajax({
                            type: sReqType,
                            url: sUrl,
                            data: $("#" + sFormId).serialize(),
                            success: function (msg) {
                                if (checkUtils.isNotNull(msg)) {
                                    if (msg.jsonFlag == true) {
                                        var sReqData = "";
                                        if (checkUtils.isNotEmptyStr(sFormId)) {
                                            sReqData = $("#" + sFormId).serialize();
                                        }
                                        if (checkUtils.isNotEmptyStr(sReqData)) {
                                            window.location.href = sDownUrl + "?" + sReqData;
                                        } else {
                                            window.location.href = sDownUrl;
                                        }
                                    } else {
                                        if (checkUtils.isNotEmptyStr(msg.jsonMsg)) {
                                            sDialogTip = msg.jsonMsg;
                                        }
                                        handlebtn.createDialog(handlebtn.downfileDialogId + sBtnId + "err", sDialogTitle, sDialogTip, $currPgDiv, "error");
                                        $("#" + handlebtn.downfileDialogId + sBtnId + "err", $currPgDiv).dialog({
                                            autoOpen: true,
                                            modal: true,
                                            dialogClass: "dialogfont",
                                            buttons: {
                                                "确定": function () {
                                                    $(this).dialog("close");
                                                }
                                            }
                                        });
                                    }
                                } else {
                                    handlebtn.createDialog(handlebtn.downfileDialogId + sBtnId + "err", sDialogTitle, sDialogTip, $currPgDiv, "error");
                                    $("#" + handlebtn.downfileDialogId + sBtnId + "err", $currPgDiv).dialog({
                                        autoOpen: true,
                                        modal: true,
                                        dialogClass: "dialogfont",
                                        buttons: {
                                            "确定": function () {
                                                $(this).dialog("close");
                                            }
                                        }
                                    });
                                }
                            },
                            error: function (msg) {
                                handlebtn.createDialog(handlebtn.downfileDialogId + sBtnId + "err", sDialogTitle, sDialogTip, $currPgDiv, "error");
                                $("#" + handlebtn.downfileDialogId + sBtnId + "err", $currPgDiv).dialog({
                                    autoOpen: true,
                                    modal: true,
                                    dialogClass: "dialogfont",
                                    buttons: {
                                        "确定": function () {
                                            $(this).dialog("close");
                                        }
                                    }
                                });
                            }
                        });
                    } else {
                        var sReqData = "";
                        if (checkUtils.isNotEmptyStr(sFormId)) {
                            sReqData = $("#" + sFormId).serialize();
                        }
                        if (checkUtils.isNotEmptyStr(sReqData)) {
                            window.location.href = sDownUrl + "?" + sReqData;
                        } else {
                            window.location.href = sDownUrl;
                        }
                    }
                });
            }
        },
        uploadfile: function (params) {// 文件上传操作
            var sBtnId = params.btnid,// 按钮的id值
				oValidate = params.validate,// 传入参数
				oUploadFile = params.uploadfile;// 文件上传操作对象
            handlebtn.btnForUpFileParams = oUploadFile;// 缓存参数
            $.extend(handlebtn.btnForUpFileParams, { "btnId": sBtnId });
            var $currPgDiv = upms.getCurrPageDiv();
            var chkFlag = false, sChkModel = "";
            if ($.isPlainObject(oValidate) && !$.isEmptyObject(oValidate)) {
                sChkModel = oValidate.model;
                if (checkUtils.isNotEmptyStr(sChkModel)) {
                    chkFlag = true;
                }
            }
            if (chkFlag) {// 需要校验操作
                switch (sChkModel) {
                    case "formsevent":
                        handlebtn.saveCheckFlag(true, $currPgDiv);
                        handlebtn.validateBtn(oValidate, $currPgDiv);
                        $("#" + sBtnId, $currPgDiv).bind("click", function () {
                            handlebtn.allValidate(oValidate, $currPgDiv);
                            if (handlebtn.getCheckFlag($currPgDiv) == true) {
                                upms.showOverLay();// 开启遮罩
                                var sReqUrl = oUploadFile.requrl,// 请求的url
									arrFormElems = oUploadFile.formElems,
									sDataType = oUploadFile.datatype;
                                ajaxUploadFile.execute({
                                    url: sReqUrl,
                                    formElems: arrFormElems,
                                    datatype: sDataType,
                                    success: handlebtn.upfileSuccess,
                                    error: handlebtn.upfileError
                                });
                            }
                        });
                        break;
                    case "batch":
                        handlebtn.saveCheckFlag(true, $currPgDiv);
                        $("#" + sBtnId, $currPgDiv).bind("click", function () {
                            handlebtn.batchValidate(oValidate, $currPgDiv);
                            if (handlebtn.getCheckFlag($currPgDiv) == true) {
                                upms.showOverLay();// 开启遮罩
                                var sReqUrl = oUploadFile.requrl,// 请求的url
									arrFormElems = oUploadFile.formElems,
									sDataType = oUploadFile.datatype;
                                ajaxUploadFile.execute({
                                    url: sReqUrl,
                                    formElems: arrFormElems,
                                    datatype: sDataType,
                                    success: handlebtn.upfileSuccess,
                                    error: handlebtn.upfileError
                                });
                            }
                        });
                        break;
                    case "single":
                        handlebtn.saveCheckFlag(true, $currPgDiv);
                        $.extend(handlebtn.btnForUpFileParams, { "chkType": "single" });
                        $.extend(handlebtn.btnForUpFileParams, { "chkErrInfoId": oValidate.errinfoid });
                        $("#" + sBtnId, $currPgDiv).bind("click", function () {
                            handlebtn.singleValidate(oValidate, $currPgDiv);
                            if (handlebtn.getCheckFlag($currPgDiv) == true) {
                                upms.showOverLay();// 开启遮罩
                                var sReqUrl = oUploadFile.requrl,// 请求的url
									arrFormElems = oUploadFile.formElems,
									sDataType = oUploadFile.datatype;
                                ajaxUploadFile.execute({
                                    url: sReqUrl,
                                    formElems: arrFormElems,
                                    datatype: sDataType,
                                    success: handlebtn.upfileSuccess,
                                    error: handlebtn.upfileError
                                });
                            }
                        });
                        break;
                    default:

                        break;
                }
            } else {// 不需要校验操作
                $("#" + sBtnId, $currPgDiv).bind("click", function () {
                    var sReqUrl = oUploadFile.requrl,// 请求的url
						arrFormElems = oUploadFile.formElems,
						sDataType = oUploadFile.datatype;
                    ajaxUploadFile.execute({
                        url: sReqUrl,
                        formElems: arrFormElems,
                        datatype: sDataType,
                        success: handlebtn.upfileSuccess,
                        error: handlebtn.upfileError
                    });
                });
            }
        },
        upfileSuccess: function (data, status, type) {// 文件上传 成功的回调函数
            if (status == "success") {
                upms.hideOverLay();// 关闭遮罩
                var sBtnId = handlebtn.btnForUpFileParams.btnid,// 按钮的id值
					sSuccUrl = handlebtn.btnForUpFileParams.succurl,// 请求成功后处理的url  数据返回类型是json的情况下才会使用
					sErrUrl = handlebtn.btnForUpFileParams.errurl,// 请求失败后处理的url 数据返回类型是json的情况下才会使用
					arrFormElems = handlebtn.btnForUpFileParams.formElems,
					sDialogTitle = handlebtn.btnForUpFileParams.dialogtitle,// 对话框的标题
					sSuccDialogTip = handlebtn.btnForUpFileParams.succdialogtip,// 对话框提示信息 成功
					sErrDialogTip = handlebtn.btnForUpFileParams.errdialogtip,// 对话框提示信息 失败
					sChkType = handlebtn.btnForUpFileParams.chkType,
					sChkErrInfoId = handlebtn.btnForUpFileParams.chkErrInfoId,
					sDataType = handlebtn.btnForUpFileParams.datatype;
                if (checkUtils.isEmptyStr(sSuccDialogTip)) {
                    sSuccDialogTip = "文件上传成功!";
                }
                if (checkUtils.isEmptyStr(sErrDialogTip)) {
                    sErrDialogTip = "文件上传失败!";
                }
                if (checkUtils.isEmptyStr(sDialogTitle)) {
                    sDialogTitle = "信息提示";
                }
                var $currPgDiv = upms.getCurrPageDiv();

                if (sChkType == "single") {
                    handlebtn.clearTipInfo(sChkErrInfoId, $currPgDiv);
                }

                switch (type) {
                    case "json":
                        if (data.jsonFlag == true) {
                            if (checkUtils.isNotEmptyStr(data.jsonMsg)) {
                                sSuccDialogTip = data.jsonMsg;
                            }
                            handlebtn.createDialog(handlebtn.upfileDialogId + sBtnId + "succ", sDialogTitle, sSuccDialogTip, $currPgDiv);
                            $("#" + handlebtn.upfileDialogId + sBtnId + "succ", $currPgDiv).dialog({
                                autoOpen: true,
                                modal: true,
                                dialogClass: "dialogfont",
                                close: function () {// 为按钮关闭添加事件
                                    if (checkUtils.isNotEmptyStr(sSuccUrl)) {
                                        upms.saveHisPageDiv();// 保存历史记录
                                        var $newPgDiv = upms.createPageDiv();
                                        /*$newPgDiv.load(sSuccUrl,{jsonFlag:true});*/
                                        upms.$load($newPgDiv, sSuccUrl, { jsonFlag: true });
                                    }
                                },
                                buttons: {
                                    "确定": function () {
                                        $(this).dialog("close");
                                    }
                                }
                            });
                        } else {
                            if (checkUtils.isNotEmptyStr(data.jsonMsg)) {
                                sErrDialogTip = data.jsonMsg;
                            }
                            handlebtn.createDialog(handlebtn.upfileDialogId + sBtnId + "err", sDialogTitle, sErrDialogTip, $currPgDiv, "error");
                            $("#" + handlebtn.upfileDialogId + sBtnId + "err", $currPgDiv).dialog({
                                autoOpen: true,
                                modal: true,
                                dialogClass: "dialogfont",
                                close: function () {// 为按钮关闭添加事件
                                    if (checkUtils.isNotEmptyStr(sErrUrl)) {
                                        upms.saveHisPageDiv();// 保存历史记录
                                        var $newPgDiv = upms.createPageDiv();
                                        /*$newPgDiv.load(sErrUrl,{jsonFlag:false});	*/
                                        upms.$load($newPgDiv, sErrUrl, { jsonFlag: false });
                                    }
                                },
                                buttons: {
                                    "确定": function () {
                                        $(this).dialog("close");
                                    }
                                }
                            });
                        }
                        break;
                    case "html":
                        if (checkUtils.isNotNull(data)) {
                            upms.saveHisPageDiv();// 保存历史记录
                            var $newPgDiv = upms.createPageDiv();
                            $newPgDiv.html(data);
                        }
                        break;
                    default:

                        break;
                }
            }
        },
        upfileError: function (data, status, type) {// 文件上传  失败的回调函数
            if (status == "error") {
                upms.hideOverLay();// 关闭遮罩
                var sDialogTitle = handlebtn.btnForUpFileParams.dialogtitle,// 对话框的标题
					sChkType = handlebtn.btnForUpFileParams.chkType,
					sChkErrInfoId = handlebtn.btnForUpFileParams.chkErrInfoId,
					sBtnId = handlebtn.btnForUpFileParams.btnid;// 按钮的id值
                if (sChkType == "single") {
                    handlebtn.clearTipInfo(sChkErrInfoId, $currPgDiv);
                }
                if (checkUtils.isEmail(data)) {
                    data = "文件上传失败!"
                }
                if (checkUtils.isEmptyStr(sDialogTitle)) {
                    sDialogTitle = "信息提示";
                }
                var $currPgDiv = upms.getCurrPageDiv();
                handlebtn.createDialog(handlebtn.upfileDialogId + sBtnId + "error", sDialogTitle, data, $currPgDiv, "error");
                $("#" + handlebtn.upfileDialogId + sBtnId + "error", $currPgDiv).dialog({
                    autoOpen: true,
                    modal: true,
                    dialogClass: "dialogfont",
                    buttons: {
                        "确定": function () {
                            $(this).dialog("close");
                        }
                    }
                });
            }
        },
        ajaxchksubmit: function (params) {
            // to do.. 带ajax校验的提交操作
        },
        submit: function (params) {// 提交按钮
            var sBtnId = params.btnid,// 按钮的id值
				oValidate = params.validate,// 传入参数
				arrParams = params.params,
				sFormId = params.formid,// 请求的form
				arrUpePwd = params.upepwd,
				sCheckSms = params.checksms,// 是否需要短信验证 open为需要 默认是close
				sSmsTip = params.smstip,// 短信验证提示语言
				sDialog = params.dialog,// 是否需要对话框 
				sDialogTitle = params.dialogtitle,// 对话框的标题
				sDialogTip = params.dialogtip,// 对话框提示信息
				sUrl = params.url;// 请求的url
            var sChkModel = oValidate.model;
            var $currPgDiv = upms.getCurrPageDiv();

            if (checkUtils.isEmptyStr(sDialogTip)) {
                sDialogTip = "你确定要操作吗?";
            }
            if (checkUtils.isEmptyStr(sDialogTitle)) {
                sDialogTitle = "信息提示";
            }
            if (checkUtils.isEmptyStr(sSmsTip)) {
                sSmsTip = "短信验证不通过!";
            }
            if (sCheckSms != "open") {
                sCheckSms = "close";
            }
            switch (sChkModel) {
                case "formsevent":
                    handlebtn.saveCheckFlag(true, $currPgDiv);
                    handlebtn.validateBtn(oValidate, $currPgDiv);
                    $("#" + sBtnId, $currPgDiv).bind("click", function () {
                        var btnSubmitFlag = true;
                        if (sCheckSms == "open") {
                            if (sms.getSmsCheckFlag() == false) {
                                btnSubmitFlag = false;
                                handlebtn.createDialog(handlebtn.selectDialogId + sBtnId + "smssubmit", sDialogTitle, sSmsTip, $currPgDiv);
                                $("#" + handlebtn.selectDialogId + sBtnId + "smssubmit", $currPgDiv).dialog({
                                    autoOpen: true,
                                    modal: true,
                                    dialogClass: "dialogfont",
                                    buttons: {
                                        "确定": function () {
                                            $(this).dialog("close");
                                        }
                                    }
                                });
                            }
                        }
                        if (btnSubmitFlag) {
                            handlebtn.allValidate(oValidate, $currPgDiv);
                            if (handlebtn.getCheckFlag($currPgDiv) == true) {
                                if (sDialog != "open") {
                                    upms.showOverLay();// 开启遮罩
                                    var ajaxParams = upms.transParsToObj(arrParams, $currPgDiv);
                                    upms.saveHisPageDiv();// 保存历史记录
                                    var $newPgDiv = upms.createPageDiv();
                                    /*$newPgDiv.load(sUrl,ajaxParams,function(){
										upms.hideOverLay();// 关闭遮罩
									});*/
                                    upms.$load($newPgDiv, sUrl, ajaxParams, function () {
                                        upms.hideOverLay();// 关闭遮罩
                                    });
                                } else {
                                    handlebtn.createDialog(handlebtn.dialogId + sBtnId + "_submit", sDialogTitle, sDailogTip, $currPgDiv);
                                    $("#" + handlebtn.dialogId + sBtnId + "_submit", $currPgDiv).dialog({
                                        autoOpen: true,
                                        modal: true,
                                        dialogClass: "dialogfont",
                                        buttons: {
                                            "确定": function () {
                                                upms.showOverLay();// 开启遮罩
                                                var ajaxParams = upms.transParsToObj(arrParams, $currPgDiv);
                                                upms.saveHisPageDiv();// 保存历史记录
                                                var $newPgDiv = upms.createPageDiv();
                                                /*$newPgDiv.load(sUrl,ajaxParams,function(){
													upms.hideOverLay();// 关闭遮罩
												});*/
                                                upms.$load($newPgDiv, sUrl, ajaxParams, function () {
                                                    upms.hideOverLay();// 关闭遮罩
                                                });
                                                $(this).dialog("close");
                                            },
                                            "取消": function () {
                                                $(this).dialog("close");
                                            }
                                        }
                                    });
                                }
                            }
                        }
                    });
                    break;
                case "batch":
                    handlebtn.saveCheckFlag(true, $currPgDiv);
                    $("#" + sBtnId, $currPgDiv).bind("click", function () {
                        var btnSubmitFlag = true;
                        if (sCheckSms == "open") {
                            if (sms.getSmsCheckFlag() == false) {
                                btnSubmitFlag = false;
                                handlebtn.createDialog(handlebtn.selectDialogId + sBtnId + "smssubmit", sDialogTitle, sSmsTip, $currPgDiv);
                                $("#" + handlebtn.selectDialogId + sBtnId + "smssubmit", $currPgDiv).dialog({
                                    autoOpen: true,
                                    modal: true,
                                    dialogClass: "dialogfont",
                                    buttons: {
                                        "确定": function () {
                                            $(this).dialog("close");
                                        }
                                    }
                                });
                            }
                        }
                        if (btnSubmitFlag) {
                            if (checkUtils.isNotNull(arrUpePwd) && $.isArray(arrUpePwd) && arrUpePwd.length > 0) {
                                for (var i = 0, len = arrUpePwd.length; i < len; i++) {
                                    if (upepwd.execute(arrUpePwd[i])) {
                                        handlebtn.saveCheckFlag(true, $currPgDiv);
                                    } else {
                                        handlebtn.saveCheckFlag(false, $currPgDiv);
                                    }
                                }
                            }

                            handlebtn.batchValidate(oValidate, $currPgDiv);
                            if (handlebtn.getCheckFlag($currPgDiv) == true) {
                                debugger;
                                if (sDialog != "open") {
                                    upms.showOverLay();// 开启遮罩
                                    var ajaxParams = upms.transParsToObj(arrParams, $currPgDiv);
                                    upms.saveHisPageDiv();// 保存历史记录
                                    var $newPgDiv = upms.createPageDiv();
                                    /*$newPgDiv.load(sUrl,ajaxParams,function(){
										upms.hideOverLay();// 关闭遮罩
									});*/
                                    upms.$load($newPgDiv, sUrl, ajaxParams, function () {
                                        upms.hideOverLay();// 关闭遮罩
                                    });
                                } else {
                                    handlebtn.createDialog(handlebtn.dialogId + sBtnId + "_submit", sDialogTitle, sDialogTip, $currPgDiv);
                                    $("#" + handlebtn.dialogId + sBtnId + "_submit", $currPgDiv).dialog({
                                        autoOpen: true,
                                        modal: true,
                                        dialogClass: "dialogfont",
                                        buttons: {
                                            "确定": function () {
                                                upms.showOverLay();// 开启遮罩
                                                var ajaxParams = upms.transParsToObj(arrParams, $currPgDiv);
                                                upms.saveHisPageDiv();// 保存历史记录
                                                var $newPgDiv = upms.createPageDiv();
                                                /*$newPgDiv.load(sUrl,ajaxParams,function(){
													upms.hideOverLay();// 关闭遮罩
												});*/
                                                upms.$load($newPgDiv, sUrl, ajaxParams, function () {
                                                    upms.hideOverLay();// 关闭遮罩
                                                });
                                                $(this).dialog("close");
                                            },
                                            "取消": function () {
                                                $(this).dialog("close");
                                            }
                                        }
                                    });
                                }

                            }
                        }
                    });
                    break;
                case "single":
                    handlebtn.saveCheckFlag(true, $currPgDiv);
                    $("#" + sBtnId, $currPgDiv).bind("click", function () {
                        var btnSubmitFlag = true;
                        if (sCheckSms == "open") {
                            if (sms.getSmsCheckFlag() == false) {
                                btnSubmitFlag = false;
                                handlebtn.createDialog(handlebtn.selectDialogId + sBtnId + "smssubmit", sDialogTitle, sSmsTip, $currPgDiv);
                                $("#" + handlebtn.selectDialogId + sBtnId + "smssubmit", $currPgDiv).dialog({
                                    autoOpen: true,
                                    modal: true,
                                    dialogClass: "dialogfont",
                                    buttons: {
                                        "确定": function () {
                                            $(this).dialog("close");
                                        }
                                    }
                                });
                            }
                        }
                        if (btnSubmitFlag) {
                            handlebtn.singleValidate(oValidate, $currPgDiv);
                            if (handlebtn.getCheckFlag($currPgDiv) == true) {

                                if (sDialog != "open") {
                                    upms.showOverLay();// 开启遮罩
                                    var ajaxParams = upms.transParsToObj(arrParams, $currPgDiv);
                                    upms.saveHisPageDiv();// 保存历史记录
                                    var $newPgDiv = upms.createPageDiv();
                                    /*$newPgDiv.load(sUrl,ajaxParams,function(){
										upms.hideOverLay();// 关闭遮罩
									});*/
                                    upms.$load($newPgDiv, sUrl, ajaxParams, function () {
                                        upms.hideOverLay();// 关闭遮罩
                                    });
                                } else {
                                    handlebtn.createDialog(handlebtn.dialogId + sBtnId + "_submit", sDialogTitle, sDailogTip, $currPgDiv);
                                    $("#" + handlebtn.dialogId + sBtnId + "_submit", $currPgDiv).dialog({
                                        autoOpen: true,
                                        modal: true,
                                        dialogClass: "dialogfont",
                                        buttons: {
                                            "确定": function () {
                                                upms.showOverLay();// 开启遮罩
                                                var ajaxParams = upms.transParsToObj(arrParams, $currPgDiv);
                                                upms.saveHisPageDiv();// 保存历史记录
                                                var $newPgDiv = upms.createPageDiv();
                                                /*$newPgDiv.load(sUrl,ajaxParams,function(){
													upms.hideOverLay();// 关闭遮罩
												});*/
                                                upms.$load($newPgDiv, sUrl, ajaxParams, function () {
                                                    upms.hideOverLay();// 关闭遮罩
                                                });
                                                $(this).dialog("close");
                                            },
                                            "取消": function () {
                                                $(this).dialog("close");
                                            }
                                        }
                                    });
                                }
                            }
                        }
                    });
                    break;
                default:

                    break;
            }
        },
        showTipInfo: function (sErrInfoId, sFalseDesc, $currPgDiv) {// 显示信息
            $("#" + sErrInfoId, $currPgDiv).html(sFalseDesc);
            $("#" + sErrInfoId, $currPgDiv).show();
        },
        clearTipInfo: function (sErrInfoId, $currPgDiv) {// 清除
            $("#" + sErrInfoId, $currPgDiv).html("");
            $("#" + sErrInfoId, $currPgDiv).hide();
        },
        singleValidate: function (oValidate, $currPgDiv) {// 按钮的单个校验
            var arrParams = oValidate.params,
				sErrInfoId = oValidate.errinfoid;
            var outerFlag = true, sErrMsg = "";

            var tipArrs = [];

            if (checkUtils.isNotEmptyStr(sErrInfoId)) {
                tipArrs.push(sErrInfoId);
                upmsTipManage.saveUpmsTipObj(tipArrs);
            }

            handlebtn.clearTipInfo(sErrInfoId, $currPgDiv);
            for (var i = 0, len = arrParams.length; i < len; i++) {
                var oVObj = arrParams[i];
                var sTargetId = oVObj.targetid,
				    sTargetName = oVObj.targetname,
				    sType = oVObj.type,
				    funCheck = oVObj.oncheck,
			    	sFalseTip = oVObj.falsetip;
                var sFunParams = handlebtn.getFormsElemValue(oVObj, $currPgDiv);
                if ($.isFunction(funCheck)) {
                    if (funCheck(sFunParams) === false) {
                        sErrMsg += "[" + sFalseTip + "]&nbsp;&nbsp;";
                        outerFlag = false;
                    }
                }
            }
            handlebtn.showTipInfo(sErrInfoId, sErrMsg, $currPgDiv);
            if (outerFlag == true) {
                handlebtn.saveCheckFlag(true, $currPgDiv);
            } else {
                handlebtn.saveCheckFlag(false, $currPgDiv);
            }
        },
        batchValidate: function (oValidate, $currPgDiv) {// 按钮的批量校验
            var arrParams = oValidate.params;
            var outerFlag = true;
            for (var i = 0, len = arrParams.length; i < len; i++) {
                var oVObj = arrParams[i];
                var sTargetId = oVObj.targetid,
				    sTargetName = oVObj.targetname,
				    sType = oVObj.type,
				    funCheck = oVObj.oncheck,
				    sInfoId = oVObj.infoid,
			    	sTrueTip = oVObj.truetip,
			    	sFalseTip = oVObj.falsetip,
			    	sSpecial = oVObj.special,
			    	arrCheckArray = oVObj.checkarray,// 批量校验
			    	sInfoTip = oVObj.infotip;
                var specFlag = true;
                if (sSpecial == "pwdcontrol") {
                    if (checkUtils.isNotEmptyStr($("#" + sInfoId, $currPgDiv).html())) {
                        specFlag = false;
                    }
                }
                if (specFlag) {
                    $("#" + sInfoId, $currPgDiv).html("");
                    $("#" + sInfoId, $currPgDiv).removeClass("errTip");
                    $("#" + sInfoId, $currPgDiv).removeClass("infoTip");
                }
                var sFunParams = handlebtn.getFormsElemValue(oVObj, $currPgDiv);

                var checkFlag = "single";

                if (checkUtils.isNotNull(arrCheckArray) && $.isArray(arrCheckArray) && arrCheckArray.length > 0) {
                    checkFlag = "array";
                }

                if (checkFlag == "single") {
                    var specFlag = true;
                    if (sSpecial == "pwdcontrol") {
                        if (checkUtils.isNotEmptyStr($("#" + sInfoId, $currPgDiv).html())) {
                            specFlag = false;
                            outerFlag = false;
                        }
                    }
                    if (specFlag) {
                        if ($.isFunction(funCheck)) {
                            if (funCheck(sFunParams) === false) {
                                $("#" + sInfoId, $currPgDiv).addClass("errTip");
                                if (checkUtils.isNotEmptyStr(sFalseTip)) {
                                    $("#" + sInfoId, $currPgDiv).html(sFalseTip);
                                }
                                outerFlag = false;
                            }
                        }
                    }

                } else if (checkFlag == "array") {
                    var specFlag = true;
                    if (sSpecial == "pwdcontrol") {
                        if (checkUtils.isNotEmptyStr($("#" + sInfoId, $currPgDiv).html())) {
                            specFlag = false;
                            outerFlag = false;
                        }
                    }
                    if (specFlag) {
                        for (var chkInd = 0, chkLen = arrCheckArray.length; chkInd < chkLen; chkInd++) {
                            var oInArrObj = arrCheckArray[chkInd];
                            var sInFalseTip = oInArrObj.falsetip,
								funInCheck = oInArrObj.oncheck;
                            if ($.isFunction(funInCheck)) {
                                var catchRetFlag = funInCheck(sFunParams);
                                if (catchRetFlag === false) {
                                    $("#" + sInfoId, $currPgDiv).addClass("errTip");
                                    if (checkUtils.isNotEmptyStr(sInFalseTip)) {
                                        $("#" + sInfoId, $currPgDiv).html(sInFalseTip);
                                    }
                                    outerFlag = false;
                                    break;
                                } else if (catchRetFlag == "infotip") {
                                    $("#" + sInfoId, $currPgDiv).addClass("errTip");
                                    if (checkUtils.isNotEmptyStr(sInFalseTip)) {
                                        $("#" + sInfoId, $currPgDiv).html(sInFalseTip);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }

                if (outerFlag == true) {
                    handlebtn.saveCheckFlag(true, $currPgDiv);
                } else {
                    handlebtn.saveCheckFlag(false, $currPgDiv);
                }
            }
        },
        allValidate: function (oValidate, $currPgDiv) {// 所有校验项全部校验下
            var arrParams = oValidate.params;
            var outerFlag = true;
            for (var i = 0, len = arrParams.length; i < len; i++) {
                var oVObj = arrParams[i];
                var sTargetId = oVObj.targetid,
				    sTargetName = oVObj.targetname,
				    sType = oVObj.type,
				    funFocus = oVObj.onfocus,
				    funBlur = oVObj.onblur,
				    funClick = oVObj.onclick,
				    funCheck = oVObj.oncheck,
				    sInfoId = oVObj.infoid,
			    	sTrueTip = oVObj.truetip,
			    	sFalseTip = oVObj.falsetip,
			    	sInfoTip = oVObj.infotip,
				    funChange = oVObj.onchange;
                $("#" + sInfoId, $currPgDiv).html("");
                $("#" + sInfoId, $currPgDiv).removeClass("errTip");
                $("#" + sInfoId, $currPgDiv).removeClass("infoTip");
                var sFunParams = handlebtn.getFormsElemValue(oVObj, $currPgDiv);
                if ($.isFunction(funFocus)) {
                    if (funFocus(sFunParams) === false) {
                        $("#" + sInfoId, $currPgDiv).addClass("errTip");
                        if (checkUtils.isNotEmptyStr(sFalseTip)) {
                            $("#" + sInfoId, $currPgDiv).html(sFalseTip);
                        }
                        outerFlag = false;
                    }
                }
                if ($.isFunction(funBlur)) {
                    if (funBlur(sFunParams) === false) {
                        $("#" + sInfoId, $currPgDiv).addClass("errTip");
                        if (checkUtils.isNotEmptyStr(sFalseTip)) {
                            $("#" + sInfoId, $currPgDiv).html(sFalseTip);
                        }
                        outerFlag = false;
                    }
                }
                if ($.isFunction(funClick)) {
                    if (funClick(sFunParams) === false) {
                        $("#" + sInfoId, $currPgDiv).addClass("errTip");
                        if (checkUtils.isNotEmptyStr(sFalseTip)) {
                            $("#" + sInfoId, $currPgDiv).html(sFalseTip);
                        }
                        outerFlag = false;
                    }
                }
                if ($.isFunction(funChange)) {
                    if (funChange(sFunParams) === false) {
                        $("#" + sInfoId, $currPgDiv).addClass("errTip");
                        if (checkUtils.isNotEmptyStr(sFalseTip)) {
                            $("#" + sInfoId, $currPgDiv).html(sFalseTip);
                        }
                        outerFlag = false;
                    }
                }
                if ($.isFunction(funCheck)) {
                    if (funCheck(sFunParams) === false) {
                        $("#" + sInfoId, $currPgDiv).addClass("errTip");
                        if (checkUtils.isNotEmptyStr(sFalseTip)) {
                            $("#" + sInfoId, $currPgDiv).html(sFalseTip);
                        }
                        outerFlag = false;
                    }
                }
            }
            if (outerFlag == true) {
                handlebtn.saveCheckFlag(true, $currPgDiv);
            } else {
                handlebtn.saveCheckFlag(false, $currPgDiv);
            }
        },
        validateBtn: function (oValidate, $currPgDiv) {// 按钮操作校验
            var sModel = oValidate.model;// 校验类型
            switch (sModel) {
                case "formsevent":
                    var arrParams = oValidate.params;
                    for (var i = 0, len = arrParams.length; i < len; i++) {
                        var oVObj = arrParams[i];
                        var sTargetId = oVObj.targetid,
						    sTargetName = oVObj.targetname,
						    sType = oVObj.type,
						    funFocus = oVObj.onfocus,
						    funBlur = oVObj.onblur,
						    funClick = oVObj.onclick,
						    funChange = oVObj.onchange;
                        var $currChkObj = upms.select$obj(sTargetId, sTargetName, sType, $currPgDiv);
                        if ($.isFunction(funFocus)) {// 赋予focus事件
                            $currChkObj.bind("focus", oVObj, function (evt) {
                                var sInfoId = evt.data.infoid,
							    	sTrueTip = evt.data.truetip,
							    	sFalseTip = evt.data.falsetip,
							    	funFocus = evt.data.onfocus,
							    	sInfoTip = evt.data.infotip;
                                $("#" + sInfoId, $currPgDiv).html("");
                                $("#" + sInfoId, $currPgDiv).removeClass("errTip");
                                $("#" + sInfoId, $currPgDiv).removeClass("infoTip");
                                var sFunParams = handlebtn.getFormsElemValue(evt.data, $currPgDiv);
                                if (funFocus(sFunParams) === true) {
                                    $("#" + sInfoId, $currPgDiv).addClass("infoTip");
                                    if (checkUtils.isNotEmptyStr(sTrueTip)) {
                                        $("#" + sInfoId, $currPgDiv).html(sTrueTip);
                                    }
                                } else if (funFocus(sFunParams) === false) {
                                    $("#" + sInfoId, $currPgDiv).addClass("errTip");
                                    if (checkUtils.isNotEmptyStr(sFalseTip)) {
                                        $("#" + sInfoId, $currPgDiv).html(sFalseTip);
                                    }
                                    handlebtn.saveCheckFlag(false, $currPgDiv);
                                } else {
                                    $("#" + sInfoId, $currPgDiv).addClass("infoTip");
                                    if (checkUtils.isNotEmptyStr(sInfoTip)) {
                                        $("#" + sInfoId, $currPgDiv).html(sInfoTip);
                                    }
                                }
                            });
                        }
                        if ($.isFunction(funBlur)) {// blur事件
                            $currChkObj.bind("blur", oVObj, function (evt) {
                                var sInfoId = evt.data.infoid,
							    	sTrueTip = evt.data.truetip,
							    	sFalseTip = evt.data.falsetip,
							    	funBlur = evt.data.onblur,
							    	sInfoTip = evt.data.infotip;
                                $("#" + sInfoId, $currPgDiv).html("");
                                $("#" + sInfoId, $currPgDiv).removeClass("errTip");
                                $("#" + sInfoId, $currPgDiv).removeClass("infoTip");
                                var sFunParams = handlebtn.getFormsElemValue(evt.data, $currPgDiv);
                                if (funBlur(sFunParams) === true) {
                                    $("#" + sInfoId, $currPgDiv).addClass("infoTip");
                                    if (checkUtils.isNotEmptyStr(sTrueTip)) {
                                        $("#" + sInfoId, $currPgDiv).html(sTrueTip);
                                    }
                                } else if (funBlur(sFunParams) === false) {
                                    $("#" + sInfoId, $currPgDiv).addClass("errTip");
                                    if (checkUtils.isNotEmptyStr(sFalseTip)) {
                                        $("#" + sInfoId, $currPgDiv).html(sFalseTip);
                                    }
                                    handlebtn.saveCheckFlag(false, $currPgDiv);
                                } else {
                                    $("#" + sInfoId, $currPgDiv).addClass("infoTip");
                                    if (checkUtils.isNotEmptyStr(sInfoTip)) {
                                        $("#" + sInfoId, $currPgDiv).html(sInfoTip);
                                    }
                                }
                            });
                        }
                        if ($.isFunction(funChange)) {// change事件
                            $currChkObj.bind("change", oVObj, function (evt) {
                                var sInfoId = evt.data.infoid,
							    	sTrueTip = evt.data.truetip,
							    	sFalseTip = evt.data.falsetip,
							    	funChange = evt.data.onchange
                                sInfoTip = evt.data.infotip;
                                $("#" + sInfoId, $currPgDiv).html("");
                                $("#" + sInfoId, $currPgDiv).removeClass("errTip");
                                $("#" + sInfoId, $currPgDiv).removeClass("infoTip");
                                var sFunParams = handlebtn.getFormsElemValue(evt.data, $currPgDiv);
                                if (funChange(sFunParams) === true) {
                                    $("#" + sInfoId, $currPgDiv).addClass("infoTip");
                                    if (checkUtils.isNotEmptyStr(sTrueTip)) {
                                        $("#" + sInfoId, $currPgDiv).html(sTrueTip);
                                    }
                                } else if (funChange(sFunParams) === false) {
                                    $("#" + sInfoId, $currPgDiv).addClass("errTip");
                                    if (checkUtils.isNotEmptyStr(sFalseTip)) {
                                        $("#" + sInfoId, $currPgDiv).html(sFalseTip);
                                    }
                                    handlebtn.saveCheckFlag(false, $currPgDiv);
                                } else {
                                    $("#" + sInfoId, $currPgDiv).addClass("infoTip");
                                    if (checkUtils.isNotEmptyStr(sInfoTip)) {
                                        $("#" + sInfoId, $currPgDiv).html(sInfoTip);
                                    }
                                }
                            });
                        }
                        if ($.isFunction(funClick)) {// click事件
                            $currChkObj.bind("click", oVObj, function (evt) {
                                var sInfoId = evt.data.infoid,
						    		sTrueTip = evt.data.truetip,
						    		sFalseTip = evt.data.falsetip,
						    		funClick = evt.data.onclick,
						    		sInfoTip = evt.data.infotip;
                                $("#" + sInfoId, $currPgDiv).html("");
                                $("#" + sInfoId, $currPgDiv).removeClass("errTip");
                                $("#" + sInfoId, $currPgDiv).removeClass("infoTip");
                                var sFunParams = handlebtn.getFormsElemValue(evt.data, $currPgDiv);
                                if (funClick(sFunParams) === true) {
                                    $("#" + sInfoId, $currPgDiv).addClass("infoTip");
                                    if (checkUtils.isNotEmptyStr(sTrueTip)) {
                                        $("#" + sInfoId, $currPgDiv).html(sTrueTip);
                                    }
                                } else if (funClick(sFunParams) === false) {
                                    $("#" + sInfoId, $currPgDiv).addClass("errTip");
                                    if (checkUtils.isNotEmptyStr(sFalseTip)) {
                                        $("#" + sInfoId, $currPgDiv).html(sFalseTip);
                                    }
                                    handlebtn.saveCheckFlag(false, $currPgDiv);
                                } else {
                                    $("#" + sInfoId, $currPgDiv).addClass("infoTip");
                                    if (checkUtils.isNotEmptyStr(sInfoTip)) {
                                        $("#" + sInfoId, $currPgDiv).html(sInfoTip);
                                    }
                                }
                            });
                        }
                    }
                    break;
                default:

                    break;
            }
        },
        getFormsElemValue: function (obj, $currPgDiv) {
            var sTargetId = obj.targetid,
		    	sTargetName = obj.targetname,
		    	sType = obj.type;
            var $currChkObj = upms.select$obj(sTargetId, sTargetName, sType, $currPgDiv);
            if (sType == "checkbox") {
                var retArr = [];
                $currChkObj.each(function (ind, elem) {
                    retArr.push($.trim($(elem).val()));
                });
                return retArr;
            } else if (sType == "radio") {
                $currChkObj = upms.transTo$obj(sTargetId, sTargetName, sType, $currPgDiv);
                return $.trim($currChkObj.val());
            } else if (sType == "select") {
                $currChkObj = upms.transTo$obj(sTargetId, sTargetName, sType, $currPgDiv);
                return $.trim($currChkObj.val());
            } else {
                return $.trim($currChkObj.val());
            }
        },
        getCheckFlag: function ($currPgDiv) {
            var CHECKFLAG = "_handle_btn_check_flag_";
            var $chkFlagObj = $("#" + CHECKFLAG, $currPgDiv);
            if ($chkFlagObj.length == 0) {
                return true;
            } else {
                if ($chkFlagObj.val() == "false") {
                    return false;
                } else {
                    return true;
                }
            }
        },
        saveCheckFlag: function (flag, $currPgDiv) {// 保存校验标记
            var CHECKFLAG = "_handle_btn_check_flag_";
            var $chkFlagObj = $("#" + CHECKFLAG, $currPgDiv);
            if ($chkFlagObj.length == 0) {
                $chkFlagObj = $("<input type='hidden' id='" + CHECKFLAG + "' name='" + CHECKFLAG + "' value='" + flag + "' />");
                $currPgDiv.append($chkFlagObj);
            } else {
                $chkFlagObj.val(flag);
            }
        },
        selectDialogId: "_select_dialog_btn_model_",
        selectbtn: function (params) {// 带勾选框的跳转
            var sBtnId = params.btnid,// 按钮的id值
				sUrl = params.url,// 请求的url
				sModel = params.model,// 模式 forward 跳转页面  dialog  提示框，对应的url取值不同
				sDialogTitle = params.dialogtitle,// 对话框的标题
				oSelectObj = params.selectobj,
				sCheckSms = params.checksms,// 是否需要短信验证 open为需要 默认是close
				sSmsTip = params.smstip,// 短信验证提示语言
				sErrDialogTip = params.errdialogtip,// 对话框提示信息 失败
				sSuccDialogTip = params.succdialogtip;// 对话框提示信息 成功
            var $currPgDiv = upms.getCurrPageDiv();
            if (checkUtils.isEmptyStr(sSuccDialogTip)) {
                sSuccDialogTip = "操作成功!";
            }
            if (checkUtils.isEmptyStr(sErrDialogTip)) {
                sErrDialogTip = "操作失败!";
            }
            if (checkUtils.isEmptyStr(sDialogTitle)) {
                sDialogTitle = "信息提示";
            }
            if (checkUtils.isEmptyStr(sSmsTip)) {
                sSmsTip = "短信验证不通过!";
            }
            if (sCheckSms != "open") {
                sCheckSms = "close";
            }
            $("#" + sBtnId, $currPgDiv).bind("click", function () {
                var innerFlag = true;
                if (sCheckSms == "open") {
                    if (sms.getSmsCheckFlag() == false) {
                        innerFlag = false;
                        handlebtn.createDialog(handlebtn.selectDialogId + sBtnId + "sms", sDialogTitle, sSmsTip, $currPgDiv);
                        $("#" + handlebtn.selectDialogId + sBtnId + "sms", $currPgDiv).dialog({
                            autoOpen: true,
                            modal: true,
                            dialogClass: "dialogfont",
                            buttons: {
                                "确定": function () {
                                    $(this).dialog("close");
                                }
                            }
                        });
                    }
                }
                if (innerFlag) {
                    if ($.isPlainObject(oSelectObj) && !$.isEmptyObject(oSelectObj)) {
                        var $selObj = undefined, sSelType = undefined, $seledObj = undefined;

                        if (checkUtils.isNotEmptyStr(oSelectObj.type)) {
                            sSelType = oSelectObj.type;
                            if (sSelType == "checkbox") {
                                if (checkUtils.isNotEmptyStr(oSelectObj.targetid)) {
                                    $selObj = $("input[type='checkbox'][id='" + oSelectObj.targetid + "']", $currPgDiv);
                                    $seledObj = $("input[type='checkbox'][id='" + oSelectObj.targetid + "']:checked", $currPgDiv);
                                } else if (checkUtils.isNotEmptyStr(oSelectObj.targetname)) {
                                    $selObj = $("input[type='checkbox'][name='" + oSelectObj.targetname + "']", $currPgDiv);
                                    $seledObj = $("input[type='checkbox'][name='" + oSelectObj.targetname + "']:checked", $currPgDiv);
                                }
                            } else if (sSelType == "radio") {
                                if (checkUtils.isNotEmptyStr(oSelectObj.targetid)) {
                                    $selObj = $("input[type='radio'][id='" + oSelectObj.targetid + "']", $currPgDiv);
                                    $seledObj = $("input[type='radio'][id='" + oSelectObj.targetid + "']:checked", $currPgDiv);
                                } else if (checkUtils.isNotEmptyStr(oSelectObj.targetname)) {
                                    $selObj = $("input[type='radio'][name='" + oSelectObj.targetname + "']", $currPgDiv);
                                    $seledObj = $("input[type='radio'][name='" + oSelectObj.targetname + "']:checked", $currPgDiv);
                                }
                            }
                            if (checkUtils.isNotNull($selObj)) {
                                if ($seledObj.length > 0) {
                                    if (sModel == "forward") {
                                        if (sSelType == "checkbox") {
                                            var spName = oSelectObj.name;
                                            if (checkUtils.isEmptyStr(spName)) {
                                                spName = $seledObj.attr("name");
                                            }
                                            if (checkUtils.isNotEmptyStr(spName)) {
                                                upms.showOverLay();// 打开遮罩
                                                var data = {};
                                                var tmpArr = [];
                                                $seledObj.each(function (ind, elem) {
                                                    tmpArr.push($.trim($(elem).val()));
                                                });
                                                data[spName] = tmpArr.join(",");
                                                upms.saveHisPageDiv();// 保存历史记录
                                                var $newPgDiv = upms.createPageDiv();
                                                /*$newPgDiv.load(sUrl,data,function(){
													upms.hideOverLay();// 关闭遮罩
												});	*/
                                                upms.$load($newPgDiv, sUrl, data, function () {
                                                    upms.hideOverLay();// 关闭遮罩
                                                });
                                            }
                                        } else if (sSelType == "radio") {
                                            var spName = oSelectObj.name;
                                            if (checkUtils.isEmptyStr(spName)) {
                                                spName = $seledObj.attr("name");
                                            }
                                            if (checkUtils.isNotEmptyStr(spName)) {
                                                upms.showOverLay();// 打开遮罩
                                                var data = {};
                                                data[spName] = $seledObj.val();
                                                upms.saveHisPageDiv();// 保存历史记录
                                                var $newPgDiv = upms.createPageDiv();
                                                /*$newPgDiv.load(sUrl,data,function(){
													upms.hideOverLay();// 关闭遮罩
												});	*/
                                                upms.$load($newPgDiv, sUrl, data, function () {
                                                    upms.hideOverLay();// 关闭遮罩
                                                });
                                            }
                                        }
                                    } else if (sModel == "dialog") {
                                        if (sSelType == "checkbox") {
                                            var spName = oSelectObj.name;
                                            if (checkUtils.isEmptyStr(spName)) {
                                                spName = $seledObj.attr("name");
                                            }
                                            if (checkUtils.isNotEmptyStr(spName)) {
                                                upms.showOverLay();// 打开遮罩
                                                var data = {};
                                                var tmpArr = [];
                                                $seledObj.each(function (ind, elem) {
                                                    tmpArr.push($.trim($(elem).val()));
                                                });
                                                data[spName] = tmpArr.join(",");
                                                upms.ajax({
                                                    type: "post",
                                                    url: sUrl,
                                                    data: data,
                                                    success: function (retmsg) {
                                                        if (checkUtils.isNotNull(retmsg)) {
                                                            if (retmsg.jsonFlag) {// 成功
                                                                if (checkUtils.isNotEmptyStr(retmsg.jsonMsg)) {
                                                                    handlebtn.createDialog(handlebtn.selectDialogId + sBtnId + "succ", sDialogTitle, retmsg.jsonMsg, $currPgDiv);
                                                                } else {
                                                                    handlebtn.createDialog(handlebtn.selectDialogId + sBtnId + "succ", sDialogTitle, sSuccDialogTip, $currPgDiv);
                                                                }
                                                                $("#" + handlebtn.selectDialogId + sBtnId + "succ", $currPgDiv).dialog({
                                                                    autoOpen: true,
                                                                    modal: true,
                                                                    dialogClass: "dialogfont",
                                                                    close: function () {// 为按钮关闭添加事件
                                                                        pagequery.callbackQuery($currPgDiv);
                                                                    },
                                                                    buttons: {
                                                                        "确定": function () {
                                                                            $(this).dialog("close");
                                                                        }
                                                                    }
                                                                });
                                                            } else {// 失败
                                                                if (checkUtils.isNotEmptyStr(retmsg.jsonMsg)) {
                                                                    handlebtn.createDialog(handlebtn.selectDialogId + sBtnId + "fail", sDialogTitle, retmsg.jsonMsg, $currPgDiv, "error");
                                                                } else {
                                                                    handlebtn.createDialog(handlebtn.selectDialogId + sBtnId + "fail", sDialogTitle, sErrDialogTip, $currPgDiv, "error");
                                                                }
                                                                $("#" + handlebtn.selectDialogId + sBtnId + "fail", $currPgDiv).dialog({
                                                                    autoOpen: true,
                                                                    modal: true,
                                                                    dialogClass: "dialogfont",
                                                                    buttons: {
                                                                        "确定": function () {
                                                                            $(this).dialog("close");
                                                                        }
                                                                    }
                                                                });
                                                            }
                                                        }
                                                        upms.hideOverLay();// 关闭遮罩
                                                    },
                                                    error: function (retmsg) {
                                                        alert(retmsg);
                                                        upms.hideOverLay();// 关闭遮罩
                                                    }
                                                });
                                            }
                                        } else if (sSelType == "radio") {
                                            var spName = oSelectObj.name;
                                            if (checkUtils.isEmptyStr(spName)) {
                                                spName = $seledObj.attr("name");
                                            }
                                            if (checkUtils.isNotEmptyStr(spName)) {
                                                upms.showOverLay();// 打开遮罩
                                                var data = {};
                                                data[spName] = $seledObj.val();
                                                upms.ajax({
                                                    type: "post",
                                                    url: sUrl,
                                                    data: data,
                                                    success: function (retmsg) {
                                                        if (checkUtils.isNotNull(retmsg)) {
                                                            if (retmsg.jsonFlag) {// 成功
                                                                if (checkUtils.isNotEmptyStr(retmsg.jsonMsg)) {
                                                                    handlebtn.createDialog(handlebtn.selectDialogId + sBtnId + "succ", sDialogTitle, retmsg.jsonMsg, $currPgDiv);
                                                                } else {
                                                                    handlebtn.createDialog(handlebtn.selectDialogId + sBtnId + "succ", sDialogTitle, sSuccDialogTip, $currPgDiv);
                                                                }
                                                                $("#" + handlebtn.selectDialogId + sBtnId + "succ", $currPgDiv).dialog({
                                                                    autoOpen: true,
                                                                    modal: true,
                                                                    dialogClass: "dialogfont",
                                                                    close: function () {// 为按钮关闭添加事件
                                                                        pagequery.callbackQuery($currPgDiv);
                                                                    },
                                                                    buttons: {
                                                                        "确定": function () {
                                                                            $(this).dialog("close");
                                                                        }
                                                                    }
                                                                });
                                                            } else {// 失败
                                                                if (checkUtils.isNotEmptyStr(retmsg.jsonMsg)) {
                                                                    handlebtn.createDialog(handlebtn.selectDialogId + sBtnId + "fail", sDialogTitle, retmsg.jsonMsg, $currPgDiv, "error");
                                                                } else {
                                                                    handlebtn.createDialog(handlebtn.selectDialogId + sBtnId + "fail", sDialogTitle, sErrDialogTip, $currPgDiv, "error");
                                                                }
                                                                $("#" + handlebtn.selectDialogId + sBtnId + "fail", $currPgDiv).dialog({
                                                                    autoOpen: true,
                                                                    modal: true,
                                                                    dialogClass: "dialogfont",
                                                                    buttons: {
                                                                        "确定": function () {
                                                                            $(this).dialog("close");
                                                                        }
                                                                    }
                                                                });
                                                            }
                                                        }
                                                        upms.hideOverLay();// 关闭遮罩
                                                    },
                                                    error: function (retmsg) {
                                                        alert(retmsg);
                                                        upms.hideOverLay();// 关闭遮罩
                                                    }
                                                });
                                            }
                                        }
                                    }
                                } else {
                                    handlebtn.createDialog(handlebtn.selectDialogId + sBtnId + "sel", sDialogTitle, "请选择相关记录!", $currPgDiv);
                                    $("#" + handlebtn.selectDialogId + sBtnId + "sel", $currPgDiv).dialog({
                                        autoOpen: true,
                                        modal: true,
                                        dialogClass: "dialogfont",
                                        buttons: {
                                            "确定": function () {
                                                $(this).dialog("close");
                                            }
                                        }
                                    });
                                }
                            }
                        }
                    } else {
                        handlebtn.createDialog(handlebtn.selectDialogId + sBtnId + "err", sDialogTitle, "方法使用不当！", $currPgDiv, "error");
                        $("#" + handlebtn.selectDialogId + sBtnId + "err", $currPgDiv).dialog({
                            autoOpen: true,
                            modal: true,
                            dialogClass: "dialogfont",
                            buttons: {
                                "确定": function () {
                                    $(this).dialog("close");
                                }
                            }
                        });
                    }
                }

            });
        },
        forward: function (params) {// 跳转页面
            var sBtnId = params.btnid,// 按钮的id值
				arrParams = params.params,// 传入参数
				sUrl = params.url,// 请求的url
				sDialog = params.dialog,// 是否需要对话框 
				sDialogTitle = params.dialogtitle,// 对话框的标题
				sCheckSms = params.checksms,// 是否需要短信验证 open为需要 默认是close
				sSmsTip = params.smstip,// 短信验证提示语言
				sSignCert = params.signcert,// 是否需要签名证书
				sDialogTip = params.dialogtip;// 对话框提示信息
            debugger;
            var $currPgDiv = upms.getCurrPageDiv();
            if (checkUtils.isEmptyStr(sDialogTip)) {
                sDialogTip = "你确定要操作吗?";
            }
            if (checkUtils.isEmptyStr(sDialogTitle)) {
                sDialogTitle = "信息提示";
            }
            if (checkUtils.isEmptyStr(sSmsTip)) {
                sSmsTip = "短信验证不通过!";
            }
            if (sCheckSms != "open") {
                sCheckSms = "close";
            }
            if (sSignCert != "open") {
                sSignCert = "close";
            }
            $("#" + sBtnId, $currPgDiv).bind("click", function () {
                var innerFlag = true;
                if (sCheckSms == "open") {
                    if (sms.getSmsCheckFlag() == false) {
                        innerFlag = false;
                        handlebtn.createDialog(handlebtn.selectDialogId + sBtnId + "sms", sDialogTitle, sSmsTip, $currPgDiv);
                        $("#" + handlebtn.selectDialogId + sBtnId + "sms", $currPgDiv).dialog({
                            autoOpen: true,
                            modal: true,
                            dialogClass: "dialogfont",
                            buttons: {
                                "确定": function () {
                                    $(this).dialog("close");
                                }
                            }
                        });
                    }
                }
                if (sSignCert == "open") {
                    var errmsg = signcert.signData("sharpxiajun");
                    if (!errmsg.flag) {
                        innerFlag = false;
                        handlebtn.createDialog(handlebtn.selectDialogId + sBtnId + "signcert", sDialogTitle, errmsg.desc, $currPgDiv);
                        $("#" + handlebtn.selectDialogId + sBtnId + "signcert", $currPgDiv).dialog({
                            autoOpen: true,
                            modal: true,
                            dialogClass: "dialogfont",
                            buttons: {
                                "确定": function () {
                                    $(this).dialog("close");
                                }
                            }
                        });
                    }
                }
                if (innerFlag) {
                    if (sDialog != "open") {
                        upms.showOverLay();// 打开遮罩
                        var data = upms.transParsToObj(arrParams, $currPgDiv);
                        upms.saveHisPageDiv();// 保存历史记录
                        var $newPgDiv = upms.createPageDiv();
                        /*$newPgDiv.load(sUrl,data,function(){
							upms.hideOverLay();// 关闭遮罩
						});*/
                        upms.$load($newPgDiv, sUrl, data, function () {
                            upms.hideOverLay();// 关闭遮罩
                        });
                    } else {
                        handlebtn.createDialog(handlebtn.dialogId + sBtnId, sDialogTitle, sDialogTip, $currPgDiv);
                        $("#" + handlebtn.dialogId + sBtnId, $currPgDiv).dialog({
                            autoOpen: true,
                            modal: true,
                            dialogClass: "dialogfont",
                            buttons: {
                                "确定": function () {
                                    upms.showOverLay();// 打开遮罩
                                    var data = upms.transParsToObj(arrParams, $currPgDiv);
                                    upms.saveHisPageDiv();// 保存历史记录
                                    var $newPgDiv = upms.createPageDiv();
                                    /*$newPgDiv.load(sUrl,data,function(){
										upms.hideOverLay();// 关闭遮罩
									});*/
                                    upms.$load($newPgDiv, sUrl, data, function () {
                                        upms.hideOverLay();// 关闭遮罩
                                    });
                                    $(this).dialog("close");
                                },
                                "取消": function () {
                                    $(this).dialog("close");
                                }
                            }
                        });
                    }
                }
            });
        },
        createDialog: function (sDialogId, sTitle, sInfoTip, $currPgDiv, infoType) {
            if (checkUtils.isNull($currPgDiv)) {
                $currPgDiv = upms.getCurrPageDiv();
            }
            var arrHtml = new Array();
            if (infoType == "error") {
                arrHtml.push(" <div id='" + sDialogId + "' title='" + sTitle + "' style='display:none'>");
                arrHtml.push("<center><p class='dialogErrTip'>" + sInfoTip + "</p></center>");
                arrHtml.push("</div>");
                $currPgDiv.append(arrHtml.join(""));
            } else {
                arrHtml.push(" <div id='" + sDialogId + "' title='" + sTitle + "' style='display:none'>");
                arrHtml.push("<center><p class='dialogTip'>" + sInfoTip + "</p></center>");
                arrHtml.push("</div>");
                $currPgDiv.append(arrHtml.join(""));
            }
        }
    };
    // 文件上传
    var ajaxUploadFile = {
        IFRAME_PREF: "_upms_ajax_file_up_iframe_",
        FORM_PREF: "_upms_ajax_file_up_frm_",
        execute: function (params) {// ajax文件上传对外执行方法
            var sUrl = params.url,
				arrFormElems = params.formElems,
				funSuccess = params.success,
				funError = params.error,
				sDataType = params.datatype;
            var currTimeId = new Date().getTime();
            var $form = ajaxUploadFile.createUpFileForm(currTimeId, arrFormElems);
            var $ifrm = ajaxUploadFile.createUpFileIframe(currTimeId, sUrl);
            var sCurrFrameId = ajaxUploadFile.IFRAME_PREF + currTimeId,
				sCurrFormId = ajaxUploadFile.FORM_PREF + currTimeId;

            // 定义事件的回调函数 S
            var upFileCallBack = function () {
                var xml = {};
                var oIframe = document.getElementById(sCurrFrameId);
                try {
                    if (oIframe.contentWindow) {
                        xml.responseText = oIframe.contentWindow.document.body ? oIframe.contentWindow.document.body.innerHTML : null;
                        xml.responseXML = oIframe.contentWindow.document.XMLDocument ? oIframe.contentWindow.document.XMLDocument : oIframe.contentWindow.document;
                    } else if (oIframe.contentDocument) {
                        xml.responseText = oIframe.contentDocument.document.body ? oIframe.contentDocument.document.body.innerHTML : null;
                        xml.responseXML = oIframe.contentDocument.document.XMLDocument ? oIframe.contentDocument.document.XMLDocument : oIframe.contentDocument.document;
                    }
                    if (!$.isEmptyObject(xml)) {
                        var retArr = ajaxUploadFile.uploadFileData(xml, sDataType);
                        if ($.isArray(retArr) && retArr.length == 2) {
                            if ($.isFunction(funSuccess)) {// 成功处理函数
                                funSuccess(retArr[1], "success", retArr[0]);
                            }
                        } else {
                            if ($.isFunction(funError)) {
                                funError("返回参数失败", "error", "html");// 失败处理函数
                            }
                        }
                    }
                } catch (e) {
                    if ($.isFunction(funError)) {
                        funError(e, "error", "html");// 失败处理函数
                    }
                }

                $(oIframe).unbind();

                setTimeout(function () {
                    try {
                        $(oIframe).remove();
                        $form.remove();
                    } catch (e) {
                        alert(e);
                    }
                }, 100);
                xml = {};
            };
            // 定义事件的回调函数 E

            try {
                $form.attr('action', sUrl);
                if ($form.get(0).encoding) {
                    $form.attr("encoding", "multipart/form-data");
                } else {
                    $form.attr("enctype", "multipart/form-data");
                }
                $form.attr('target', sCurrFrameId);
                $form.submit();
            } catch (e) {
                if ($.isFunction(funError)) {
                    funError(e, "error", "html");// 失败处理函数
                }
            }

            if (window.attachEvent) {
                document.getElementById(sCurrFrameId).attachEvent('onload', upFileCallBack);
            } else {
                document.getElementById(sCurrFrameId).addEventListener('load', upFileCallBack, false);
            }
            return;
        },
        createUpFileIframe: function (currTimeId, sUrl) {// 构建用于文件上传的iframe
            var sFrameId = ajaxUploadFile.IFRAME_PREF + currTimeId;
            var $upIFrame = $("<iframe id='" + sFrameId + "' name='" + sFrameId + "' />");
            $upIFrame.css('position', 'absolute');
            $upIFrame.css('top', '-1000px');
            $upIFrame.css('left', '-1000px');
            //$upIFrame.attr('src',sUrl);
            $('body').append($upIFrame);
            return $upIFrame;
        },
        createUpFileForm: function (currTimeId, arrFormElems) {// 创建用于文件上传的隐藏form
            var sFormId = ajaxUploadFile.FORM_PREF + currTimeId;
            var $upForm = $("<form action='' method='POST' name='" + sFormId + "' id='" + sFormId + "' enctype='multipart/form-data'></form>");
            for (var i = 0, len = arrFormElems.length; i < len; i++) {
                var sOldElemParams = arrFormElems[i];
                var sTargetId = sOldElemParams.targetid,
					sTargetName = sOldElemParams.targetname,
					sType = sOldElemParams.type;
                var $currPgDiv = upms.getCurrPageDiv();
                var $oldElem = upms.select$obj(sTargetId, sTargetName, sType, $currPgDiv);
                var sNewElemId = ajaxUploadFile.FORM_PREF + currTimeId + $oldElem.attr("id");
                var $newElem = $oldElem.clone();
                $oldElem.before($newElem);
                $oldElem.attr("id", sNewElemId);
                if (sType == "select") {
                    var sSelVal = $("option:selected", $oldElem).val();
                    $("option[value='" + sSelVal + "']", $newElem).attr("selected", "selected");
                }
                if (sType == "textarea") {
                    $newElem.val($oldElem.val());
                }
                if (sType == "radio") {
                    //console.log($oldElem.val());
                    //console.log($(":checked",$oldElem).val());
                }
                $upForm.append($oldElem);
            }
            $upForm.css('position', 'absolute');
            $upForm.css('top', '-1200px');
            $upForm.css('left', '-1200px');
            $upForm.appendTo('body');
            return $upForm;
        },
        uploadFileData: function (xml, sDataType) {
            var data = xml.responseText;
            var retArr = [];
            switch (sDataType) {
                case "json":
                    retArr = [];
                    retArr.push("json");
                    var retObj;
                    try {
                        retObj = eval("(" + data + ")");
                    } catch (e) {
                        retObj = eval("(" + $(data).html() + ")");
                    }
                    retArr.push(retObj);
                    return retArr;
                    break;
                case "html":
                    retArr = [];
                    retArr.push("html");
                    retArr.push($(data).html());
                    return retArr;
                    break;
                case "jsonhtml":
                    var retObj;
                    try {
                        retArr = [];
                        retObj = eval("(" + data + ")");
                        retArr.push("json");
                        retArr.push(retObj);
                        return retArr;
                    } catch (e) {
                        try {
                            retArr = [];
                            retObj = eval("(" + $(data).html() + ")");
                            retArr.push("json");
                            retArr.push(retObj);
                            return retArr;
                        } catch (e) {
                            retArr = [];
                            retArr.push("html");
                            retArr.push(data);
                            return retArr;
                        }
                    }
                    break;
                default:
                    retArr.push("html");
                    retArr.push(data);
                    return retArr;
                    break;
            }
        }
    };
    // 短信验证
    var sms = {
        isSmsWait: true,
        smsTimesId: undefined,
        smsDivId: undefined,
        getSmsCheckFlag: function () {//  判断是否进行过短信验证
            var $currPgDiv = upms.getCurrPageDiv();
            var flag = $.trim($("#smsCheckFlag", $currPgDiv).val());
            if (flag == "true") {
                return true;
            } else {
                return false;
            }
        },
        load: function (params) {// 加载短信的代码
            var sDivId = params.divid,// 加载短信验证的div的id值
				sUrl = params.url,// 请求的url
				sModel = params.model;// 请求模式  request 或 session
            sms.smsDivId = sDivId;// 记录显示的div
            var $currPgDiv = upms.getCurrPageDiv();
            if (checkUtils.isNotEmptyStr(sms.smsTimesId)) {
                window.clearTimeout(sms.smsTimesId);
                sms.smsTimesId = undefined;
            }
            if (checkUtils.isNotEmptyStr(sModel)) {
                if (sModel == "request") {
                    /*$("#" + sDivId,$currPgDiv).load(sUrl,{},function(){
						$("#smsModel",$currPgDiv).val("request_model");
					});*/
                    upms.$load($("#" + sDivId, $currPgDiv), sUrl, {}, function () {
                        $("#smsModel", $currPgDiv).val("request_model");
                    });
                } else if (sModel == "session") {
                    /*$("#" + sDivId,$currPgDiv).load(sUrl,{},function(){
						$("#smsModel",$currPgDiv).val("session_model");
					});*/
                    upms.$load($("#" + sDivId, $currPgDiv), sUrl, {}, function () {
                        $("#smsModel", $currPgDiv).val("session_model");
                    });
                }
            }
        },
        install: function () {// 安装短信发送功能
            sms.isSmsWait = true;
            var $currPgDiv = upms.getCurrPageDiv();
            $("#sendSmsBtn", $currPgDiv).bind("click", function () {// 获取短信信息
                sms.send();
            });
            $("#checkSmsBtn", $currPgDiv).bind("click", function () {// 验证短信信息
                var chkCode = $("#validateCode", $currPgDiv).val();
                if (checkUtils.isEmptyStr(chkCode)) {
                    $("#sendResM", $currPgDiv).html("请输入短信验证码!").removeClass("greenC").addClass("redC").show();
                } else {
                    sms.validate();
                }
            });
        },
        send: function () {
            var $currPgDiv = upms.getCurrPageDiv();
            upms.ajax({
                type: "post",
                url: $.trim($("#sendSmsUrl", $currPgDiv).val()),
                success: function (msg) {
                    if (msg.respCode == '0000') {
                        $("#respCode", $currPgDiv).val(msg.respCode);
                        $("#validateOrderNum", $currPgDiv).val(msg.validateOrderNum);
                        $("#validateTm", $currPgDiv).val(msg.validateTm);
                        $("#sendResM", $currPgDiv).html('验证码30分钟内有效,若未收到,1分钟后可重新获取').removeClass("redC").addClass("greenC").show();
                        $("#checkSmsBtn", $currPgDiv).show();
                        sms.waitSend(60);
                        sms.isSmsWait = true;
                    } else {
                        $("#sendResM", $currPgDiv).html(msg.jsonMsg).removeClass("greenC").addClass("redC").show();
                    }
                },
                error: function (msg) {
                    alert(msg);
                }
            });
        },
        waitSend: function (s) {// 等待发送
            var $currPgDiv = upms.getCurrPageDiv();
            if (!sms.isSmsWait) {
                return;
            } else {
                if (s == 0) {
                    $("#sendSmsBtn", $currPgDiv).val('重新发送');
                    $("#sendSmsBtn", $currPgDiv).bind("click", function () {
                        sms.send();
                    });
                    return;
                } else {
                    $("#sendSmsBtn", $currPgDiv).val(s + '秒后可重发');
                    $("#sendSmsBtn", $currPgDiv).unbind("click");
                }
                --s;
                sms.smsTimesId = window.setTimeout("upms.sms.waitSend(" + s + ")", 1000);
            }
        },
        validate: function () {
            var $currPgDiv = upms.getCurrPageDiv();
            var params = {
                "phoneSmsCode": $.trim($("#validateCode", $currPgDiv).val()),
                "validateOrderNum": $.trim($("#validateOrderNum", $currPgDiv).val()),
                "validateTm": $.trim($("#validateTm", $currPgDiv).val()),
                "smsModel": $.trim($("#smsModel", $currPgDiv).val())
            };
            upms.ajax({
                type: "post",
                url: $.trim($("#checkUrl", $currPgDiv).val()),
                data: params,
                success: function (msg) {
                    if (msg.respCode == '0000' && checkUtils.isNotEmptyStr(msg.smsModel)) {

                        var flag = false;
                        $("#smsCheckFlag", $currPgDiv).val("false");
                        if (msg.smsModel == "session_model") {
                            flag = true;
                            $("#smsCheckFlag", $currPgDiv).val("true");
                        } else if (msg.smsModel == "request_model") {
                            if (msg.isChkSmsReq == '1') {
                                flag = true;
                                $("#smsCheckFlag", $currPgDiv).val("true");
                            }
                        }
                        if (flag) {
                            var n = 3;
                            for (var i = 0; i <= n; i++) {
                                window.setTimeout("upms.sms.smstimes(" + i + "," + n + ",function(){$('#" + sms.smsDivId + "',upms.getCurrPageDiv()).hide();return false;})", 1000);
                            }
                        } else {
                            $("#smsCheckFlag", $currPgDiv).val("false");
                            $("#sendResM", $currPgDiv).html("短信验证失败!").removeClass("greenC").addClass("redC").show();
                            if (checkUtils.isNotEmptyStr(sms.smsTimesId)) {
                                window.clearTimeout(sms.smsTimesId);
                                sms.smsTimesId = undefined;
                            }
                            $("#sendSmsBtn", $currPgDiv).val('重新发送短信').show();
                            $("#sendSmsBtn", $currPgDiv).bind("click", function () {
                                sms.send();
                            });
                        }
                    } else {
                        $("#sendResM", $currPgDiv).html(msg.jsonMsg).removeClass("greenC").addClass("redC").show();
                        if (checkUtils.isNotEmptyStr(sms.smsTimesId)) {
                            window.clearTimeout(sms.smsTimesId);
                            sms.smsTimesId = undefined;
                        }
                        $("#sendSmsBtn", $currPgDiv).val('重新发送短信').show();
                        $("#sendSmsBtn", $currPgDiv).bind("click", function () {
                            sms.send();
                        });
                    }
                },
                error: function (msg) {
                    alert(msg);
                }
            });
        },
        smstimes: function (i, j, callback) {
            if (i != j) {
                return;
            } else {
                callback();
            }
        }
    };
    // 查询列表的相关操作
    var grid = {
        gridhover: function (str) {
            var $currPgDiv = upms.getCurrPageDiv();
            var $obj = $(str, $currPgDiv);
            $("tr:not(:first)", $obj).hover(function () {
                $(this).addClass("over");
            }, function () {
                $(this).removeClass("over");
            });
        },
        radio: function (params) {// 给单选框添加个性化事件
            var $currPgDiv = upms.getCurrPageDiv();
            var $gridrad = undefined;
            if (checkUtils.isNotEmptyStr(params.targetid)) {
                $gridrad = $("input[type='radio'][id='" + params.targetid + "']", $currPgDiv);
            } else if (checkUtils.isNotEmptyStr(params.targetname)) {
                $gridrad = $("input[type='radio'][name='" + params.targetname + "']", $currPgDiv);
            }
            if (checkUtils.isNotNull($gridrad)) {
                $gridrad.bind("click", function () {
                    if ($(this).attr("checked")) {
                        $gridrad.parent().parent().removeClass("select");
                        $(this).parent().parent().addClass("select");
                    }
                });
            }
        },
        checkboxall: function (params) {// 查询列表全选框操作
            var $currPgDiv = upms.getCurrPageDiv();
            var oAllCheckbox = params.allcheckbox,
				oOneCheckbox = params.onecheckbox;
            var $allchkbox = undefined, $onechkbox = undefined;
            if (checkUtils.isNotEmptyStr(oAllCheckbox.targetid)) {
                $allchkbox = $("#" + oAllCheckbox.targetid, $currPgDiv);
            } else if (checkUtils.isNotEmptyStr(oAllCheckbox.targetname)) {
                $allchkbox = $("input[type='checkbox'][name='" + oAllCheckbox.targetname + "']", $currPgDiv);
            }
            if (checkUtils.isNotEmptyStr(oOneCheckbox.targetid)) {
                $onechkbox = $("input[type='checkbox'][id='" + oOneCheckbox.targetid + "']", $currPgDiv);
            } else if (checkUtils.isNotEmptyStr(oOneCheckbox.targetname)) {
                $onechkbox = $("input[type='checkbox'][name='" + oOneCheckbox.targetname + "']", $currPgDiv);
            }
            if (checkUtils.isNotNull($allchkbox) && checkUtils.isNotNull($onechkbox)) {
                $allchkbox.bind("click", function () {
                    if ($allchkbox.attr("checked")) {
                        $onechkbox.attr("checked", "checked");
                        $onechkbox.parent().parent().addClass("select");
                    } else {
                        $onechkbox.removeAttr("checked");
                        $onechkbox.parent().parent().removeClass("select");
                    }
                });
                $onechkbox.bind("click", function () {
                    if ($(this).attr("checked")) {
                        $(this).parent().parent().addClass("select");
                    } else {
                        $(this).parent().parent().removeClass("select");
                    }
                });
            }
        },
        downDialogId: "_grid_downfile_dialog_model_",
        createDialog: function (sDialogId, sTitle, sInfoTip, $currPgDiv, infoType) {
            var arrHtml = new Array();
            if (infoType == "error") {
                arrHtml.push(" <div id='" + sDialogId + "' title='" + sTitle + "' style='display:none'>");
                arrHtml.push("<center><p class='dialogErrTip'>" + sInfoTip + "</p></center>");
                arrHtml.push("</div>");
                $currPgDiv.append(arrHtml.join(""));
            } else {
                arrHtml.push("<div id='" + sDialogId + "' title='" + sTitle + "' style='display:none'>");
                arrHtml.push("<center><p class='dialogTip'>" + sInfoTip + "</p></center>");
                arrHtml.push("</div>");
                $currPgDiv.append(arrHtml.join(""));
            }
        },
        downfile: function (params) {// 文件下载
            var sBtnName = $.trim(params.btnname),
				sDialog = params.dialog,
				sDownUrl = params.downurl,
				oAjaxCheck = params.ajaxcheck;// ajax校验
            var $currPgDiv = upms.getCurrPageDiv();
            $("a[name='" + sBtnName + "']", $currPgDiv).each(function (ind, elem) {
                $(elem).bind("click", function () {
                    if (checkUtils.isNotNull(oAjaxCheck) && !$.isEmptyObject(oAjaxCheck)) {
                        var sUrl = oAjaxCheck.url,// 校验的url
							sReqType = oAjaxCheck.reqtype,// 请求方式 例如 get  post
							sDialogTitle = oAjaxCheck.dialogtitle,// 对话框的标题
							sDialogTip = oAjaxCheck.dialogtip;// 对话框提示信息
                        var parentObj = $(elem).parent(),
							selKeys = $.trim($(elem).attr("sendparam")).split(",");
                        var data = {}, sData = "";// 传到服务端的参数
                        for (var i = 0; i < selKeys.length; i++) {
                            var tmpDataObj = {};
                            var sGetPar = $("input[name='" + selKeys[i] + "']", parentObj).val();
                            tmpDataObj[selKeys[i]] = sGetPar;
                            $.extend(data, tmpDataObj);
                            if (checkUtils.isEmptyStr(sData)) {
                                sData += selKeys[i] + "=" + sGetPar;
                            } else {
                                sData += "&" + selKeys[i] + "=" + sGetPar;
                            }
                        }
                        if (checkUtils.isEmptyStr(sReqType)) {
                            sReqType = "post";
                        }
                        if (checkUtils.isEmptyStr(sDialogTitle)) {
                            sDialogTitle = "信息提示";
                        }
                        if (checkUtils.isEmptyStr(sDialogTip)) {
                            sDialogTip = "文件下载失败!";
                        }
                        upms.ajax({
                            type: sReqType,
                            url: sUrl,
                            data: data,
                            success: function (msg) {
                                if (checkUtils.isNotNull(msg)) {
                                    if (msg.jsonFlag == true) {
                                        if (checkUtils.isNotEmptyStr(sData)) {
                                            window.location.href = sDownUrl + "?" + sData;
                                        } else {
                                            window.location.href = sDownUrl;
                                        }
                                    } else {
                                        if (checkUtils.isNotEmptyStr(msg.jsonMsg)) {
                                            sDialogTip = msg.jsonMsg;
                                        }
                                        grid.createDialog(grid.downDialogId, sDialogTitle, sDialogTip, $currPgDiv, "error");
                                        $("#" + grid.downDialogId, $currPgDiv).dialog({
                                            autoOpen: true,
                                            modal: true,
                                            dialogClass: "dialogfont",
                                            buttons: {
                                                "确定": function () {
                                                    $(this).dialog("close");
                                                }
                                            }
                                        });
                                    }
                                }
                            },
                            error: function (msg) {
                                grid.createDialog(grid.downDialogId, sDialogTitle, sDialogTip, $currPgDiv, "error");
                                $("#" + grid.downDialogId, $currPgDiv).dialog({
                                    autoOpen: true,
                                    modal: true,
                                    dialogClass: "dialogfont",
                                    buttons: {
                                        "确定": function () {
                                            $(this).dialog("close");
                                        }
                                    }
                                });
                            }
                        });
                    }
                });
            });
        }
    };
    // 联动下拉框
    var linkselect = {
        execute: function (params) {
            var $currPgDiv = upms.getCurrPageDiv();
            if ($.isArray(params)) {
                var len = params.length;
                if (len > 0) {
                    linkselect.iterSelectFtn(0, len, {}, {}, params);
                }
                for (var i = 0, leng = params.length; i < leng; i++) {
                    var sTargetId = params[i].targetid;
                    if ($("#" + sTargetId, $currPgDiv).children().length == 0) {
                        $("#" + sTargetId, $currPgDiv).append("<option value=''>---请选择---<\/option>");
                    }
                }
            }
        },
        iterSelectFtn: function (i, len, getOParams, selObj, oParams) {
            var $currPgDiv = upms.getCurrPageDiv();
            if (oParams[i].rank == (i + 1)) {
                if (checkUtils.isNotNull(oParams[i].params) && !$.isEmptyObject(oParams[i].params)) {
                    getOParams = oParams[i].params;
                }
                $.extend(getOParams, { "rank": i });
                if (checkUtils.isNotNull(selObj) && !$.isEmptyObject(selObj)) {
                    $.extend(getOParams, selObj);
                }
                upms.ajax({
                    type: "post",
                    url: oParams[i].url,
                    data: getOParams,
                    success: function (msg) {
                        if (checkUtils.isNotNull(msg)) {
                            var sRank = msg["rank"];
                            var retJson = oParams[sRank].retjson,
								sTargetId = oParams[sRank].targetid;
                            if (checkUtils.isNotNull(msg[retJson])) {
                                var retObj = $.parseJSON(msg[retJson]);
                                if ($("#" + sTargetId, $currPgDiv).children().length != 0) {
                                    $("#" + sTargetId, $currPgDiv).empty();
                                }
                                for (var key in retObj) {
                                    $("#" + sTargetId, $currPgDiv).append("<option value='" + key + "'>" + retObj[key] + "</option>");
                                }
                                if ((sRank + 1) < len) {
                                    var sSendParams = oParams[sRank + 1].sendparam;
                                    $("#" + sTargetId, $currPgDiv).bind("change", { "ranknum": (parseInt(sRank) + 1) }, function (event) {
                                        var selVal = $.trim($("#" + sTargetId + " option:selected", $currPgDiv).val());

                                        selObj = {};
                                        selObj[sSendParams] = selVal;

                                        for (var ind = (sRank + 1) ; ind < len; ind++) {
                                            var newTargetId = oParams[ind].targetid;
                                            if ($("#" + newTargetId, $currPgDiv).children() != 0) {
                                                $("#" + newTargetId, $currPgDiv).empty();
                                                $("#" + newTargetId, $currPgDiv).append("<option value=''>---请选择---<\/option>");
                                            }
                                        }
                                        linkselect.iterSelectFtn(event.data.ranknum, len, {}, selObj, oParams);
                                    });
                                }
                            }
                        }
                    },
                    error: function (msg) {
                        alert(msg);
                    }
                });
            }
        }
    };
    // 签名证书的问题
    var signcert = {
        checkDate: function (control, certDateStr) {// 验证证书日期
            if (certDateStr != undefined && certDateStr != null) {
                var certDate = new Date(certDateStr),
					curDate = new Date(),
					t = Date.parse(curDate) - Date.parse(certDate),
					year = certDate.getUTCFullYear(), month_temp = certDate.getUTCMonth(), month = month_temp + 1,
					day = certDate.getUTCDate(),
					hintMsg = "您的证书将于" + year + "年" + month + "月" + day + "日" + "到期,请在到期日前更换";
                if (t > 0) {
                    return { flag: false, desc: "您的证书已经过期" };
                } else if (t == 0) {
                    return { flag: false, desc: "您的证书今天过期" };
                } else {
                    var showd = (-t) / (1000 * 3600 * 24), viewd = showd.toFixed(0);
                    if (viewd < 90) {
                        return { flag: true, desc: hintMsg };
                    }
                    return { flag: true, desc: "NULL" };
                }
            }
        },
        signData: function (str) {// 签名认证的方法
            var certDateStr = undefined, retObj = undefined;
            if (checkUtils.isNull(document.all) || checkUtils.isNull(document.all.signcontrol)) {
                retObj = { flag: false, desc: "签名证书无法使用，请使用IE浏览器!" };
                return retObj;
            }
            var control = document.all.signcontrol;
            try {
                try {
                    control.CFCA_SelectUserCerts(true);
                    certDateStr = control.CFCA_GetCertValidToDate(true);
                } catch (e) {
                    var errmsg = e.message;
                    errmsg = unescape(errmsg.replace(/&#x/g, '%u').replace(/;/g, ''));
                    retObj = { flag: false, desc: "签名认证失败，请重新再试!" };
                }
                retObj = signcert.checkDate(control, certDateStr);
                if (retObj.flag == true) {
                    signData = control.CFCA_SignData(str);
                    var $currPgDiv = upms.getCurrPageDiv();
                    var $signedData = $('#signedData', $currPgDiv);
                    if ($signedData.length == 0) {
                        $currPgDiv.append("<input type=\"hidden\" id=\"signedData\" name=\"signedData\" value=\"\" />");
                        $('#signedData', $currPgDiv).val(signData);
                    } else {
                        $signedData.val(signData);
                    }
                }
            } catch (e) {
                var errmsg = e.message;
                errmsg = unescape(errmsg.replace(/&#x/g, '%u').replace(/;/g, ''));
                retObj = { flag: false, desc: "签名认证失败，请重新再试!" };
            }
            /*if (checkUtils.isNull(retObj) || $.isEmptyObject(retObj)){
				retObj =  {flag:false,desc:"签名认证失败，请重新再试!"};
			}*/
            return retObj;
        },
        signFile: function (upfilepath) {// 生成签名文件
            var certDateStr = undefined, retObj = undefined, sSignFilePath = undefined;
            if (checkUtils.isNull(document.all) || checkUtils.isNull(document.all.signcontrol)) {
                retObj = { flag: false, desc: "签名证书无法使用，请使用IE浏览器!", filepath: "none" };
                return retObj;
            }
            var control = document.all.signcontrol;
            try {

                try {
                    control.CFCA_SelectUserCerts(true);
                    certDateStr = control.CFCA_GetCertValidToDate(true);
                } catch (e) {
                    var errmsg = e.message;
                    errmsg = unescape(errmsg.replace(/&#x/g, '%u').replace(/;/g, ''));
                    retObj = { flag: false, desc: "生成签名文件失败1！", filepath: "none" };
                }
                retObj = signcert.checkDate(control, certDateStr);
                if (retObj.flag == true) {
                    sSignFilePath = control.CFCA_SignFile(upfilepath);
                    retObj["filepath"] = sSignFilePath;
                }

            } catch (e) {
                var errmsg = e.message;
                errmsg = unescape(errmsg.replace(/&#x/g, '%u').replace(/;/g, ''));
                retObj = { flag: false, desc: "生成签名文件失败2！", filepath: "none" };
            }
            /*if (checkUtils.isNull(retObj) || $.isEmptyObject(retObj)){
				retObj =  {flag:false,desc:"签名认证失败，请重新再试!",filepath:"none"};
			}*/
            return retObj;
        }
    };
    // 密码控件
    var upepwd = {
        execute: function (params) {
            var $currPgDiv = upms.getCurrPageDiv();
            var oPwdObj = params.pwdobj,
				sFactorId = params.factorId,
				sInfoId = params.infoid,
				sSaveElemId = params.saveElemId;
            /*清除错误信息S*/
            $("#" + sInfoId, $currPgDiv).html("");
            $("#" + sInfoId, $currPgDiv).removeClass("errTip");
            $("#" + sInfoId, $currPgDiv).removeClass("infoTip");
            /*清除错误信息E*/
            var sFactorVal = $.trim($("#" + sFactorId).val()).toLowerCase();
            var oPwdResult = oPwdObj.result(sFactorVal);
            if (oPwdResult.resp != 00) {
                $("#" + sInfoId, $currPgDiv).addClass("errTip");
                $("#" + sInfoId, $currPgDiv).html(oPwdResult.errMsg());
                return false;
            } else {
                $("#" + sSaveElemId, $currPgDiv).val(oPwdResult.cypher);
                return true;
            }
            $("#" + sInfoId, $currPgDiv).html("密码控件错误!");
            return false;
        }
    };
    // 定义upms对象
    var upms = {
        version: "1.1",// 版本号
        upmsPagePref: "_upms_page_layer_no_",// 页面前缀
        $webObj: {},
        webObjLen: 0,
        installWebObj: function ($webObj) {
            upms.$webObj = $webObj;
            upms.webObjLen = $webObj.length;
        },
        clearWebObj: function () {
            if (!$.isEmptyObject(upms.$webObj)) {
                upms.$webObj.html("");
            }
        },
        createPageDiv: function () {
            if (!$.isEmptyObject(upms.$webObj)) {
                var webChildLen = upms.$webObj.children().length;
                var $newPageDiv = $("<div id='" + upms.upmsPagePref + webChildLen + "'></div>");
                upms.$webObj.append($newPageDiv);
                return $newPageDiv;
            }
            return null;
        },
        getCurrPageDiv: function () {
            if (!$.isEmptyObject(upms.$webObj)) {
                var webChildLen = upms.$webObj.children().length;
                var iCurrPage = webChildLen - 1;
                return $("div#" + upms.upmsPagePref + iCurrPage, upms.$webObj);
            }
            return null;
        },
        getPageDiv: function (num) {
            if (!$.isEmptyObject(upms.$webObj) && num >= 0) {
                var webChildLen = upms.$webObj.children().length;
                if (num > (webChildLen - 1)) {
                    num = webChildLen - 1;
                }
                return $("div#" + upms.upmsPagePref + num, upms.$webObj);
            }
            return null;
        },
        removePageDiv: function (num) {
            if (!$.isEmptyObject(upms.$webObj) && num >= 0) {
                var webChildLen = upms.$webObj.children().length;
                if (num > (webChildLen - 1)) {
                    num = webChildLen - 1;
                }
                upms.$webObj.remove("div#" + upms.upmsPagePref + num);
            }
        },
        saveHisPageDiv: function () {
            if (!$.isEmptyObject(upms.$webObj)) {
                var webChildLen = upms.$webObj.children().length;
                var iHisNo = webChildLen - 1;
                $("div#" + upms.upmsPagePref + iHisNo, upms.$webObj).css("display", "none");
            }
        },
        hisGoPageDiv: function () {
            if (!$.isEmptyObject(upms.$webObj)) {
                var webChildLen = upms.$webObj.children().length;
                var iCurrPage = webChildLen - 1,
					iPrefPage = webChildLen - 2;
                $("div#" + upms.upmsPagePref + iCurrPage, upms.$webObj).remove();
                $("div#" + upms.upmsPagePref + iPrefPage, upms.$webObj).css("display", "block");
            }
        },
        initRandCode: function (imgID) {// 初始化验证码
            $("#" + imgID).bind("click", function () {
                upms.refreshRandCode(imgID);
            });
            upms.refreshRandCode(imgID);
        },
        refreshRandCode: function (imgID) {// 刷新验证码
            var srcVal = $.trim($("#" + imgID).attr("src"));
            if (checkUtils.isNotEmptyStr(srcVal)) {
                var srcArr = srcVal.split("\?");
                if (checkUtils.isNotEmptyStr(srcArr[0])) {
                    srcVal = srcArr[0] + "?t=" + new Date().getTime();
                }
            }
            $("#" + imgID).attr('src', srcVal);
        },
        validateForms: function (params) {// 统一校验框架
            if ($.isPlainObject(params) && !$.isEmptyObject(params)) {// 判断传入参数是不是对象同时对象不能为空
                // 将对象的属性值存入到局部变量里
                var sModel = params.model,
					sErrInfoId = params.errInfoId,
					fOnTrue = params.onTrue,
					fOnFalse = params.onFalse,
					fOnSuccess = params.onSuccess,
					fOnError = params.onError,
					arrParams = params.params,
					oOnAjax = params.onAjax;
                switch (sModel) {
                    case "single":// {targetId:'',targetName:'',$target:'',type:'',onCheck:'',trueDesc:'',falseDesc:'',onTrue:'',onFalse:''}
                        if ($.isArray(arrParams)) {// 参数对象必须为数组
                            for (var i = 0, len = arrParams.length; i < len; i++) {
                                var obj = arrParams[i];
                                var retVal = upms.validateForSingle(obj, sErrInfoId, fOnTrue, fOnFalse);
                                if ("nopass" == retVal) {
                                    if (fOnError != undefined && $.isFunction(fOnError)) {
                                        fOnError();
                                    }
                                    return false;
                                }
                            }
                            if (oOnAjax != undefined && $.isPlainObject(oOnAjax) && !$.isEmptyObject(oOnAjax)) {// 判断是否要执行ajax校验操作
                                var isCallbackflag = oOnAjax.callbackflag;
                                upms.ajax({
                                    type: oOnAjax.type,
                                    url: oOnAjax.url,
                                    data: oOnAjax.data,
                                    success: function (msg) {
                                        if (oOnAjax.success != undefined && $.isFunction(oOnAjax.success)) {
                                            var isFlag = oOnAjax.success(msg);
                                            if (isFlag == true && isCallbackflag != false && fOnSuccess != undefined && $.isFunction(fOnSuccess)) {
                                                fOnSuccess();
                                            } else if (isFlag == false && isCallbackflag != false && fOnError != undefined && $.isFunction(fOnError)) {
                                                fOnError();
                                            }
                                            return true;
                                        }
                                    },
                                    error: function (msg) {
                                        if (oOnAjax.error != undefined && $.isFunction(oOnAjax.error)) {
                                            if (oOnAjax.error != undefined && $.isFunction(oOnAjax.error)) {
                                                var isFlag = oOnAjax.error(msg);
                                                if (isFlag == true && isCallbackflag != false && fOnSuccess != undefined && $.isFunction(fOnSuccess)) {
                                                    fOnSuccess();
                                                } else if (isFlag == false && isCallbackflag != false && fOnError != undefined && $.isFunction(fOnError)) {
                                                    fOnError();
                                                }
                                                return true;
                                            }
                                        }
                                    }
                                });
                            }
                            return true;
                        }
                        if (fOnError != undefined && $.isFunction(fOnError)) {
                            fOnError();
                        }
                        return false;
                        break;
                    case "bacth":

                        break;
                    default:

                        break;
                }
            }
        },
        validateForSingle: function (obj, sErrInfoId, fOnTrue, fOnFalse) {
            // 用局部变量存储obj的属性值
            var sTargetId = obj.targetId,
				sTargetName = obj.targetName,
				o$TargetObj = obj.$targetObj,
				oTargetObj = obj.targetObj,
				sType = obj.type,
				fOncheck = obj.onCheck,
				sTrueDesc = obj.trueDesc,
				sFalseDesc = obj.falseDesc;
            switch (sType) {
                case "text":
                    var $obj = upms.trans$obj(sTargetId, sTargetName, o$TargetObj, oTargetObj, sType);
                    if (!$.isEmptyObject($obj)) {
                        if (fOncheck($obj.val()) == true) {// 校验通过
                            if (fOnTrue != undefined && $.isFunction(fOnTrue)) {
                                fOnTrue(sTrueDesc);
                            } else {
                                upms.validateClearMsg(sErrInfoId);
                            }
                            return "pass";
                        } else if (fOncheck($obj.val()) == false) {// 校验不通过
                            if (fOnFalse != undefined && $.isFunction(fOnFalse)) {
                                fOnFalse(sFalseDesc);
                            } else {
                                upms.validateShowMsg(sErrInfoId, sFalseDesc);
                            }
                            return "nopass";
                        }
                    } else {
                        return "next";
                    }
                    break;
                default:

                    break;
            }
        },
        validateShowMsg: function (sErrInfoId, sFalseDesc) {
            $("#" + sErrInfoId).html(sFalseDesc);
            $("#" + sErrInfoId).show();
        },
        validateClearMsg: function (sErrInfoId) {
            $("#" + sErrInfoId).html("");
            $("#" + sErrInfoId).hide();
        },
        select$obj: function (sTargetId, sTargetName, sType, $currPgDiv) {// 选择jQuery对象
            switch (sType) {
                case "text":// 文本框以及隐藏域和密码框
                    if (checkUtils.isNotEmptyStr(sTargetId)) {
                        return $("#" + sTargetId, $currPgDiv);
                    } else if (checkUtils.isNotEmptyStr(sTargetName)) {
                        return $("input[type='text'][name='" + sTargetName + "']", $currPgDiv);
                    }
                    return null;
                    break;
                case "select":// 下拉框
                    if (checkUtils.isNotEmptyStr(sTargetId)) {
                        return $("#" + sTargetId, $currPgDiv);
                    } else if (checkUtils.isNotEmptyStr(sTargetName)) {
                        return $("select[name='" + sTargetName + "']", $currPgDiv);
                    }
                    return null;
                    break;
                case "radio":// 单选框
                    if (checkUtils.isNotEmptyStr(sTargetId)) {
                        return $("input:radio[id='" + sTargetId + "']", $currPgDiv);
                    } else if (checkUtils.isNotEmptyStr(sTargetName)) {
                        return $("input:radio[name='" + sTargetName + "']", $currPgDiv);
                    }
                    return null;
                    break;
                case "checkbox":// 多选框
                    if (checkUtils.isNotEmptyStr(sTargetId)) {
                        return $("input:checkbox[id='" + sTargetId + "']", $currPgDiv);
                    } else if (checkUtils.isNotEmptyStr(sTargetName)) {
                        return $("input:checkbox[name='" + sTargetName + "']", $currPgDiv);
                    }
                    return null;
                    break;
                case "textarea":// 文本域 
                    if (checkUtils.isNotEmptyStr(sTargetId)) {
                        return $("#" + sTargetId, $currPgDiv);
                    } else if (checkUtils.isNotEmptyStr(sTargetName)) {
                        return $("textarea[name='" + sTargetName + "']", $currPgDiv);
                    }
                    return null;
                    break;
                case "file":// 文件上传
                    if (checkUtils.isNotEmptyStr(sTargetId)) {
                        return $("#" + sTargetId, $currPgDiv);
                    } else {
                        return $("input[type='file'][name='" + sTargetName + "']", $currPgDiv);
                    }
                    return null;
                    break;
                default:
                    return null;
                    break;
            }
        },
        transTo$obj: function (sTargetId, sTargetName, sType, $currPgDiv) {// 取值的jQuery对象
            switch (sType) {
                case "text":// 文本框以及隐藏域和密码框
                    if (checkUtils.isNotEmptyStr(sTargetId)) {
                        return $("#" + sTargetId, $currPgDiv);
                    } else if (checkUtils.isNotEmptyStr(sTargetName)) {
                        return $("input[type='text'][name='" + sTargetName + "']", $currPgDiv);
                    }
                    return null;
                    break;
                case "select":// 下拉框
                    if (checkUtils.isNotEmptyStr(sTargetId)) {
                        return $("#" + sTargetId + " option:selected", $currPgDiv);
                    } else if (checkUtils.isNotEmptyStr(sTargetName)) {
                        return $("select[name='" + sTargetName + "']  option:selected", $currPgDiv);
                    }
                    return null;
                    break;
                case "radio":// 单选框
                    if (checkUtils.isNotEmptyStr(sTargetId)) {
                        return $("input:radio[id='" + sTargetId + "'][checked]", $currPgDiv);
                    } else if (checkUtils.isNotEmptyStr(sTargetName)) {
                        return $("input:radio[name='" + sTargetName + "'][checked]", $currPgDiv);
                    }
                    return null;
                    break;
                case "checkbox":// 多选框
                    if (checkUtils.isNotEmptyStr(sTargetId)) {
                        return $("input:checkbox[id='" + sTargetId + "'][checked]", $currPgDiv);
                    } else if (checkUtils.isNotEmptyStr(sTargetName)) {
                        return $("input:checkbox[name='" + sTargetName + "'][checked]", $currPgDiv);
                    }
                    return null;
                    break;
                case "textarea":// 文本域 
                    if (checkUtils.isNotEmptyStr(sTargetId)) {
                        return $("#" + sTargetId, $currPgDiv);
                    } else if (checkUtils.isNotEmptyStr(sTargetName)) {
                        return $("textarea[name='" + sTargetName + "']");
                    }
                    return null;
                    break;
                case "file":// 文件上传
                    if (checkUtils.isNotEmptyStr(sTargetId)) {
                        return $("#" + sTargetId, $currPgDiv);
                    } else {
                        return $("input[type='file'][name='" + sTargetName + "']", $currPgDiv);
                    }
                    return null;
                    break;
                default:
                    return null;
                    break;
            }
        },
        trans$obj: function (sTargetId, sTargetName, o$TargetObj, oTargetObj, sType) {
            switch (sType) {
                case "text":// 文本框以及隐藏域和密码框
                    if (checkUtils.isNotEmptyStr(sTargetId)) {
                        return $("#" + sTargetId);
                    } else if (checkUtils.isNotEmptyStr(sTargetName)) {
                        return $("input[type='text'][name='" + sTargetName + "']");
                    }

                    if ($.isEmptyObject(o$TargetObj)) {
                        if ($.isEmptyObject(oTargetObj)) {
                            return null;
                        } else {
                            return $(oTargetObj);
                        }
                    } else {
                        return o$TargetObj;
                    }

                    return null;
                    break;
                case "select":// 下拉框

                    break;
                case "radio":// 单选框

                    break;
                case "checkbox":// 多选框

                    break;
                case "textarea":// 文本域 

                    break;
                default:
                    return null;
                    break;
            }
        },
        transParsToObj: function (params, $currPgDiv) {// 将upms自定义参数对象转化为json对象
            var retJObj = {};
            if ($.isArray(params)) {
                for (var i = 0, len = params.length; i < len; i++) {
                    var oElem = params[i];
                    if ($.isPlainObject(oElem) && !$.isEmptyObject(oElem)) {
                        var sName = $.trim(oElem.name),
							sType = $.trim(oElem.type),
							sValue = $.trim(oElem.value),
							sTargetId = $.trim(oElem.targetid),
							sTargetName = $.trim(oElem.targetname);
                        switch (sType) {
                            case "text":// 文本框以及隐藏域和密码框
                                var $obj = upms.transTo$obj(sTargetId, sTargetName, sType, $currPgDiv);
                                if (checkUtils.isNotNull($obj)) {
                                    var tmpObj = {};
                                    tmpObj[sName] = $.trim($obj.val());
                                    $.extend(retJObj, tmpObj);
                                }
                                break;
                            case "select":// 下拉框
                                var $obj = upms.transTo$obj(sTargetId, sTargetName, sType, $currPgDiv);
                                if (checkUtils.isNotNull($obj)) {
                                    var tmpObj = {};
                                    tmpObj[sName] = $.trim($obj.val());
                                    $.extend(retJObj, tmpObj);
                                }
                                break;
                            case "radio":// 单选框
                                var $obj = upms.transTo$obj(sTargetId, sTargetName, sType, $currPgDiv);
                                if (checkUtils.isNotNull($obj)) {
                                    var tmpObj = {};
                                    tmpObj[sName] = $.trim($obj.val());
                                    $.extend(retJObj, tmpObj);
                                }
                                break;
                            case "checkbox":// 多选框
                                var $obj = upms.transTo$obj(sTargetId, sTargetName, sType, $currPgDiv);
                                var chkarr = [], tmpObj = {};
                                $obj.each(function (ind, elem) {
                                    chkarr.push($.trim($(elem).val()));
                                });
                                tmpObj[sName] = chkarr.join(",");
                                $.extend(retJObj, tmpObj);
                                break;
                            case "textarea":// 文本域 
                                var $obj = upms.transTo$obj(sTargetId, sTargetName, sType, $currPgDiv);
                                if (checkUtils.isNotNull($obj)) {
                                    var tmpObj = {};
                                    tmpObj[sName] = $.trim($obj.val());
                                    $.extend(retJObj, tmpObj);
                                }
                                break;
                            case "file":// 文件上传
                                var $obj = upms.transTo$obj(sTargetId, sTargetName, sType, $currPgDiv);
                                if (checkUtils.isNotNull($obj)) {
                                    var tmpObj = {};
                                    tmpObj[sName] = $.trim($obj.val());
                                    $.extend(retJObj, tmpObj);
                                }
                                break;
                            case "default":
                                var tmpObj = {};
                                tmpObj[sName] = sValue;
                                $.extend(retJObj, tmpObj);
                                break;
                            default:

                                break;
                        }

                    }
                }
            }
            return retJObj;
        },
        changeParamsToObj: function (params) {// 将upms自定义参数对象转化为json对象
            var retJObj = {};
            if ($.isArray(params)) {
                for (var i = 0; i < params.length; i++) {
                    var oElem = params[i];
                    if ($.isPlainObject(oElem) && !$.isEmptyObject(oElem)) {
                        var sName = $.trim(oElem.name),
							sType = $.trim(oElem.type),
							sTargetId = $.trim(oElem.targetId),
							sTargetName = $.trim(oElem.targetName),
							oTargetObj = oElem.targetObj,
							o$TargetObj = oElem.$targetObj;

                        switch (sType) {
                            case "text":
                                var $obj = upms.trans$obj(sTargetId, sTargetName, o$TargetObj, oTargetObj, sType);
                                if (!$.isEmptyObject($obj)) {
                                    var tmpObj = {};
                                    tmpObj[sName] = $.trim($obj.val());
                                    $.extend(retJObj, tmpObj);
                                }
                                break;
                            default:

                                break;
                        }
                    }
                }
            }

            return retJObj;
        },
        installOverLay: function (params) {// 安装ajax遮罩
            $("body").remove("#_overlaydiv_");
            $("body").remove("#_olcontent_");

            var sType = "font";
            var sFontContent = LANG.overlay_font_tip;
            // 缺省对象
            var $overLayDiv = $("<div id='_overlaydiv_' style='z-index: 1000; border: medium none; margin: 0px; padding: 0px; background-color: rgb(204, 204, 242); opacity: 0.7; filter: Alpha(opacity=70);cursor: wait; position: fixed;display:none;'></div>"),
			    $overLayContent = $("<div id='_olcontent_' style='z-index: 1001; position: fixed; padding: 0px; margin: 0px; text-align: left; color: rgb(238, 238, 238); border: medium none; background-color: rgb(238, 238, 238); cursor: auto; display:none; font-size:8px;'></div>"),
			    $overlayH1 = $("<h1></h1>"),
			    $fontObj = $("<font id='_ovlayTip_' style='color:black; font-style:微软雅黑; font-size: 14px;'></font>");

            if ($.isPlainObject(params) && !$.isEmptyObject(params)) {
                var sOverlayBgColor = params.overlaybgcolor,
					sContentBgColor = params.contentbgcolor,
					sOpacity = params.opacity,
					oFontCss = params.fontcss,
					oImgCss = params.imgcss,
					sImgUrl = params.imgurl;
                if (checkUtils.isNotEmptyStr(params.type)) {
                    sType = params.type;
                }
                if (checkUtils.isNotEmptyStr(params.fontcontent)) {
                    sFontContent = params.fontcontent;
                }
                if (checkUtils.isNotEmptyStr(sOverlayBgColor)) {
                    $overLayDiv.css("background-color", sOverlayBgColor);
                }
                if (checkUtils.isNotEmptyStr(sContentBgColor)) {
                    $overLayContent.css("background-color", sContentBgColor);
                }
                if (checkUtils.isNotNull(sOpacity) && !isNaN(parseFloat(sOpacity))) {
                    $overLayDiv.css("opacity", parseFloat(sOpacity));
                    var ieOpacity = parseInt(parseFloat(sOpacity) * 100);
                    $overLayDiv.css("filter", "Alpha(opacity=" + ieOpacity + ")");
                }
                if (sType != "img") {// 非图片 就是显示文字
                    if ($.isPlainObject(oFontCss) && !$.isEmptyObject(oFontCss)) {
                        for (var keyCss in oFontCss) {
                            if (checkUtils.isNotEmptyStr(keyCss)) {
                                $fontObj.css(keyCss, oFontCss[keyCss]);
                            }
                        }
                    }
                } else {// 图片
                    // to do.....
                }
            }

            if (sType != "img") {// 非图片 就是显示文字
                $fontObj.text(sFontContent);
                $overlayH1.append($fontObj);
                $overLayContent.append($overlayH1);
                $("body").append($overLayDiv);
                $("body").append($overLayContent);
                $(window).bind("scroll", function () {
                    var oldDisplayCss = $overLayDiv.css("display"),
						olContDisplayCss = $overLayContent.css("display");
                    if (oldDisplayCss != 'none' && olContDisplayCss != 'none') {
                        upms.showOverLay();
                    }
                });
            } else {
                // to do.....
            }
        },
        showOverLay: function () {// 显示ajax遮罩效果
            var $overlaydiv = $("#_overlaydiv_"),
				$olcontentdiv = $("#_olcontent_");
            var olDivLen = $overlaydiv.length,
				olConDivLen = $olcontentdiv.length;
            if (!upms.ishasoverLay(olDivLen, olConDivLen)) {
                installOverLay();
            }
            upms.hideOverLay();
            if (upms.webObjLen != 0) {
                var wScrollLeft = $(window).scrollLeft(),
					wScrollTop = $(window).scrollTop(),
					ovlayWidth = upms.$webObj.outerWidth(),
					ovlayHeight = upms.$webObj.outerHeight(),
					ovlayOffSet = upms.$webObj.offset();

                $("#_overlaydiv_").css("height", ovlayHeight);
                $("#_overlaydiv_").css("width", ovlayWidth);
                $("#_overlaydiv_").css("left", ovlayOffSet.left - wScrollLeft);
                $("#_overlaydiv_").css("top", ovlayOffSet.top - wScrollTop);
                $("#_overlaydiv_").css("display", "block");
                upms.centerOverLay($("#_overlaydiv_"), $("#_olcontent_"), wScrollLeft, wScrollTop);
                $("#_olcontent_").css("display", "inline");
            }
        },
        ishasoverLay: function (olDivLen, olConDivLen) {// 缺省安装
            if (olDivLen == 0 || olConDivLen == 0) {
                $("body").remove("#_overlaydiv_");
                $("body").remove("#_olcontent_");
                return false;
            }
            return true;
        },
        centerOverLay: function (parentNode, msgNode, wScrollLeft, wScrollTop) {// 遮罩 居中方法
            var sLeft = parentNode.offset().left + ((parentNode.outerWidth() - msgNode.outerWidth())) / 2;
            var sTop = parentNode.offset().top + ((parentNode.outerHeight() - msgNode.outerHeight())) / 2;
            msgNode.css("top", sTop - wScrollTop);
            msgNode.css("left", sLeft - wScrollLeft);
        },
        hideOverLay: function () {// 隐藏ajax遮罩效果
            $("#_overlaydiv_").css("display", "none");
            $("#_olcontent_").css("display", "none");
        },
        reqSessId: function (data) {// 封装sessionUUID
            if (data == undefined) {
                data = {};
            }
            var sSessUUID = $("#sessionUUID").val();
            var oReqData = { "sessionUUID": sSessUUID },
				sReqData = "&sessionUUID=" + sSessUUID;
            if ($.isPlainObject(data) == true) {
                $.extend(oReqData, data);
                data = oReqData;
            } else {
                data = data + sReqData;
            }
            return data;
        },
        ajax: function (ajaxData) {// 封装ajax请求
            ajaxData.data = upms.reqSessId(ajaxData.data);
            $.ajax(ajaxData);
        },
        $load: function ($obj, url, data, callback) {// 封装load方法
            //debugger;
            data = upms.reqSessId(data);
            $obj.load(url, data, callback);
        },
        WEBMAIN: WEBMAIN,
        WEBLOGIN: WEBLOGIN,
        WEBLANG: WEBLANG,
        pagequery: pagequery,// 分页查询
        forward: forward,// 页面跳转
        history: history,// 返回操作
        griddialog: griddialog,
        handlebtn: handlebtn,
        sms: sms,
        grid: grid,
        merhome: merhome,
        signcert: signcert,
        linkselect: linkselect,
        ajaxUploadFile: ajaxUploadFile,// ajax文件上传
        cookieUtils: cookieUtils,// cookie工具类
        checkUtils: checkUtils,// 校验工具类
        upmsUtils: upmsUtils// upms框架通用工具类
    };

    // 将upms对象安装到window对象
    window.upms = window.UPMS = window._$ = upms;
})(window);