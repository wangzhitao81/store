/*
 * @Description: 安装upms库
 * @Author: xiajun
 * @Update: xiajun(2012-12-27) 
 * @copyright:版权所有(C)，中国银联股份有限公司，2002－2015，所有权利保留
 */
(function(window,document,location,undefined){
	if (window.upms || window.UPMS || window._$){
		return;
	}
	// cookie操作工具类
	var cookieUtils = {
		get:function(name){// 获取cookie值
			var cookieName = encodeURIComponent(name) + "=",
				cookieStart = document.cookie.indexOf(cookieName),
				cookieValue = null;
			if (cookieStart > -1){
				var cookieEnd = document.cookie.indexOf(";",cookieStart);
				if (cookieEnd == -1){
					cookieEnd = document.cookie.length;
				}
				cookieValue = decodeURIComponent(document.cookie.substring(cookieStart + cookieName.length,cookieEnd));
			}
			return cookieValue;
		},
		set:function(sName,sValue,oExpires,sPath,sDomain,bSecure){// 设置cookie值
            var currDate = new Date(),
            	sExpires = typeof oExpires == 'undefined'?'':';expires=' + new Date(currDate.getTime() + (oExpires * 24 * 60 * 60* 1000)).toUTCString();
            document.cookie = sName + '=' + sValue + sExpires + ((sPath == null)?'':(' ;path=' + sPath)) + ((sDomain == null)?'':(' ;domain=' + sDomain)) + ((bSecure == true)?' ; secure':'');
		},
		unset:function(name,path,domain,secure){// 删除cookie值
			this.set(name,"",new Date(0),path,domain,secure);
		}		
	};
	// 通用工具类
	var commonUtils = {
        trim:function(str){// 去除左右两边空格
            return str.replace(/(^\s*)|(\s*$)/, "");;
        },
		isEmptyStr:function(str){// 判断变量是否为空
			if (str === undefined || str === null || commonUtils.trim(str) === ""){
				return true;
			}else{
				return false;
			}
		},
		getSysLanguage:function(){// 获取系统语言
			var sysLang = navigator.systemLanguage,
				userLang = navigator.userLanguage,
				bLang = navigator.language;
			
			if (sysLang != undefined){
				return sysLang.toLowerCase();
			}else if (userLang != undefined){
				return userLang.toLowerCase();
			}else if (bLang != undefined){
				return bLang.toLowerCase();
			}else{// 默认
				return "zh-cn";
			}
		},
		getDomainHead:function(){// 获取网站url头  例如：http://127.0.0.1/UPMS/
			var sHost = location.host,
				sProtocol = location.protocol,
				sPathNm = location.pathname;
			var arrPathNm = sPathNm.split("\/");
			var domainHead = sProtocol + "\/\/" + sHost + "\/";
			if (arrPathNm.length > 1){
				domainHead += arrPathNm[1] + "\/";
			}
			if (domainHead.substr(domainHead.length - 1) != "\/"){
				domainHead = domainHead + "\/";
			}
			return domainHead;
		}
	}
	var sLang = cookieUtils.get("WEBSITE_LANG");
	if (commonUtils.isEmptyStr(sLang)){
		sLang = commonUtils.getSysLanguage();
		cookieUtils.set("WEBSITE_LANG",sLang,1000,"/");
	}
	
	var domainHead = commonUtils.getDomainHead();
	document.write("<script src='" + domainHead + "omer/resources/js/upms-lang-" + sLang + ".js'><\/script>");
	document.write("<script src='" + domainHead + "omer/resources/js/upms.js'><\/script>");
})(window,document,location)