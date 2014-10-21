//Code by jjs
//src : 弹出页面url
//width: 宽度
//height: 高度
//showfun: 打开页面时调用
//closefun: 关闭页面时的回调
function OpenMyModal(src, width, height, showfun, closefun) {
    var frame = '<iframe width="' + width + '" height="' + height + '"src="' + src + '" frameborder="no" border="0" marginwidth="0" marginheight="0" scrolling="no" allowtransparency="yes"></iframe>';
    var option = {
        escClose: true,
        close: true,
        minHeight: height,
        minWidth: width,
        autoResize: true
    };
    if (showfun != null) {
        option.onShow = showfun();
    }
    if (closefun != null) {
        option.onClose = closefun();
    }

    return $.modal(frame, option);
}
//设置cookeis 到时候可以删仅作演示
function setCookie(c_name, value, expiredays) {
    var exdate = new Date()
    exdate.setDate(exdate.getDate() + expiredays)
    document.cookie = c_name + "=" + escape(value) +
    ((expiredays == null || expiredays == 1) ? "" : ";expires=" + exdate.toGMTString())
}

function getCookie(c_name) {
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(c_name + "=")
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1
            c_end = document.cookie.indexOf(";", c_start)
            if (c_end == -1) c_end = document.cookie.length
            return unescape(document.cookie.substring(c_start, c_end))
        }
    }
    return ""
}
//获取浏览器高度
function getWinHeight() {
    if ($.browser.msie) {
        return document.compatMode == "CSS1Compat" ? document.documentElement.clientHeight : document.body.clientHeight;
    }
    else {
        return self.innerHeight;
    }
}
//获取浏览器宽度
function getWinWidth() {
    if ($.browser.msie) {
        return document.compatMode == "CSS1Compat" ? document.documentElement.clientWidth : document.body.clientWidth;
    }
    else {
        return self.innerWidth;
    }
}

//验证输入框只能输入数字
$.fn.numeral = function () {
    $(this).css("ime-mode", "disabled");
    this.bind("keydown", function (event) {
        var keyCode = event.which;
        if (event.ctrlKey && event.keyCode == 86) //ctrl+v
        {
            return true;
        }
    })
    this.bind("keypress", function (event) {
        var keyCode = event.which;
        if (keyCode != 8 && keyCode != 0) {
            if (keyCode == 46 || (keyCode >= 48 && keyCode <= 57))
                return true;
            else
                return false;
        }
    });

    this.bind("paste", function () {
        var el = $(this);
        setTimeout(function () {
            var text = $(el).val();
            if (/[^\d]/.test(text)) {
                alert("请输入数字");
                $(el).val("");
            }
        }, 100);

    });
    this.bind("dragenter", function () {
        return false;
    });

};



/*验证信息*/
var RegObject = function () {
    //日期格式验证
    var RegDate = function (MatchText) {
        if (!/^([1|2]\d{3})-((0\d)|(1[0-2]))-(([0-2]\d)|(3[0|1]))$/.test(MatchText)) return false;
        var s = MatchText.split("-");
        if (s[0] < 1800) return false;
        var d = new Date(s[0], s[1] - 1, s[2]);
        return ((d.getFullYear() == s[0]) && ((d.getMonth() + 1) == s[1]) && (d.getDate() == s[2]));
    };

    return {
        //数字或者小数点验证
        Money: function (MatchText) {
            var testReturn = true;
            testReturn = /^\d*(?:\.\d{0,2})?$/.test(MatchText);
            if (testReturn == true) {
                var a = String(MatchText.split('.')[0])
                var re = /^[1-9]+\d*$/
                if (a == "0") { return true }
                if (!re.test(a)) {
                    return false;
                }
                else {
                    return true;
                }
            }
            return false
        },
        //数字验证(>=0的数字)
        Number: function (MatchText) {
            if (MatchText == "0") return true;
            return /^[1-9]+\d*$/.test(MatchText);
        },
        //整形数字验证
        Integer: function (MatchText) {
            //if (MatchText == "0") return true;
            return /^[1-9]+\d*$/.test(MatchText);
        },
        //数字验证
        Digital: function (MatchText) {
            return /\D/.test(MatchText);
        },
        Date: function (MatchText) {
            return RegDate(MatchText);
        },
        YMDate: function (MatchText) {
            return RegDate(MatchText + "-01");
        },
        //用户名不能全为数字
        LoginAccount: function (MatchText) {
            //return /^[A-Za-z0-9]{6,20}$/.test(MatchText);
            return /^\w*[a-zA-Z]+\w*$/.test(MatchText);
        },
        PassWord: function (MatchText) {
            return /^[A-Za-z0-9]{6,20}$/.test(MatchText);
        },
        ZipCode: function (MatchText) {
            return /^\d{6}$/.test(MatchText);
        },
        Phone: function (MatchText) {
            return /^(\d{3,5}-)?\d{6,8}(-\d{1,5})?$/.test(MatchText);
        },
        Mobile: function (MatchText) {
            return /^1[3|4|5|8][0-9]\d{8}$/.test(MatchText);
        },
        Email: function (MatchText) {
            return /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/.test(MatchText);
        },
        //机构代码验证
        InstitutionCode: function (MatchText) {
            return /^\w{8}-?\w{1}$/.test(MatchText);
        },
        //身份证格式验证
        Indentity: function (MatchText) {
            return /^\d{15}$|^\d{18}$|^\d{17}[xX]$/.test(MatchText);
        },
        //资格证书编码
        Credent: function (MatchText) {
            return (/^\w{27}$/.test(MatchText)) || (/^\w{24}$/.test(MatchText));
        },
        //执业证书编码
        License: function (MatchText) {
            return /^\d{15}$/.test(MatchText);
        }
    }
}();

//光标事件
function UPOPUtils() { }
UPOPUtils.KEY_CODE = {
    Backspace: 8,
    Tab: 9,
    Shift: 16,
    Space: 32,
    Del: 46,
    "0": 48,
    "9": 57,
    zero: 96,
    nine: 105,
    ";": 59,
    "=": 61,
    A: 65,
    C: 67,
    F: 70,
    V: 86,
    Z: 90,
    "-": 109,
    ",": 188,
    ".": 190,
    dot: 110,
    "/": 191,
    "`": 192,
    "[": 219,
    "\\": 220,
    "]": 221,
    "'": 222
};
UPOPUtils.isNull = function (a) {
    return ((a === undefined) || (a === null))
};
UPOPUtils.isInputKey = function (b) {
    var a = UPOPUtils.KEY_CODE;
    return (b == "17" || (b >= a["0"] && b <= a["9"]) || (b >= a.zero && b <= a.nine))
};
UPOPUtils.isValidKey = function (b) {
    var a = UPOPUtils.KEY_CODE;
    if (b >= a["0"] && b <= a["9"]) {
        return true
    }
    if (b >= a.zero && b <= a.nine) {
        return true
    }
    if (b == a.Backspace) {
        return true
    }
    return false
};
UPOPUtils.setCursor = function (c, b, a) {
    if (c.setSelectionRange) {
        c.focus();
        c.setSelectionRange(b, a)
    } else {
        if (c.createTextRange) {
            range = c.createTextRange();
            range.collapse(true);
            range.moveEnd("character", a);
            range.moveStart("character", b);
            range.select()
        }
    }
};
UPOPUtils.getCursorPosition = function (c) {
    var b = 0;
    if (document.selection) {
        c.focus();
        var a = document.selection.createRange();
        a.moveStart("character", -c.value.length);
        b = a.text.length
    } else {
        if (c.selectionStart || c.selectionStart == "0") {
            b = c.selectionStart
        }
    }
    return (b)
};
UPOPUtils.isSMSCode = function (a) {
    a = a.replace(/[ ]/g, "");
    return isValidSmsCode(a)
};
UPOPUtils.isMobilePhone = function (a) {
    a = a.replace(/[ ]/g, "");
    return isValidCellPhone(a)
};
UPOPUtils.isNull = function (a) {
    return ((a === undefined) || (a === null))
};

/*公用页面脚本*/
// 获取短信验证码
$(function () {
    //设置提醒获取验证码倒计时
    $(".yzm").bind("click mousedown", function () {
        if (checkSms() == false) {
            return false;
        }
        setTimeout(function () {
            $(".yzm").addClass('yzm_btn_dis');
            $(".yzm").val('验证码已发送').attr("disabled", "disabled");
            $('.SmsText').addClass('txt_success').removeClass("txt_error").html("<span>60</span>秒内未收到短信验证码，可点击重新获取").show();
            doSmsCountingBack(6, $(".yzm_btn_dis"), "yzm_btn_dis");
        }, 10)
    });
    //还原发送前提示状态
    function ReductionMsg() {
        $('.SmsText').html("").removeClass("txt_success");
    }
    //发送短信成功后，倒计时效果
    function doSmsCountingBack(smsCountBack, smsBtnObj, smsLoadClassName) {
        if (smsCountBack > 0) {
            $(".CardSmsText span").html(smsCountBack);
            --smsCountBack;
            trySend = setTimeout(function () {
                doSmsCountingBack(smsCountBack, smsBtnObj, smsLoadClassName);
            }, 1000);
        } else {
            smsBtnObj.removeClass(smsLoadClassName).removeAttr('disabled');
            $(".yzm").val("重新获取验证码")
            ReductionMsg();//重置短信下方提示信息
        }
    }

});

$(function () {
    //计算浏览器窗口大小设置页面宽度
    var _winWidth = getWinWidth();
    $(".body-bg-left").css("width", _winWidth - 42);
    $(".nav_right").css("width", _winWidth - 315);
    $(".main_content").css("width", _winWidth - 310);
    //头部语言切换
    $(".language").hover(function () {
        $("div", this).show();
    }, function () {
        $("div", this).hide();
    });

    //左侧菜单
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
        var _link = $("a", this).attr("href");
        $(".main_content").load(_link);
    });
});

