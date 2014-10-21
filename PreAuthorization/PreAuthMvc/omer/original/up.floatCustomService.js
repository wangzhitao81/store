/*
 * @Author	wangzilong
 * @Date	2012-10-8
 * @Version	0.1
 */

(function ($, win) {

    var warning = function (str) {
        str = '<up.floatCS> [ERROR]: ' + str;
        if (win.console)
            win.console.error(str);
        else
            win.alert(str);
    }

    if (win.undefined == $) {
        warning('jquery未存在！');
        return;
    }

    if (win.undefined == win.up) {
        win.up = {};
    }

    var inited = false,
        sys_time = new Date(),
        check_interval = 30,
        screen_width = win.screen.width;

    win.up.floatCS = {
        _el: null,
        _arg: null,
        _icon: null,
        _insertHTML: function () {
            var str = "<style type='text/css'>.rightservice{ position:fixed; right:" + this._arg.right + "px; top:" + this._arg.top + "px; width:" + this._arg.width + "px;z-index:500;}"
                    + "*html,*html body{background-image:url(https://online.unionpay.com/static/cms/img/30/6460cf5d-dd11-43fe-bebc-ac3b7d2adaf1.gif);background-attachment:fixed}"
                    + "*html .rightservice{position:absolute;right:15px;top:expression(eval(document.documentElement.scrollTop)+180); width:53px;}"
                    + ".rightservice a.service,a.unservice{ width:53px; height:85px; background:url(https://online.unionpay.com/static/cms/img/21/d11fda13-3bb9-45c3-aa47-d85e67e5fe47.gif) no-repeat 0 0; display:inline-block; margin-bottom:15px;}"
                    + ".rightservice a.service:hover{ background-position:0 -85px;}"
                    + ".rightservice a.unservice{ background-position:0 -170px;}"
                    + ".rightservice a.service_small,a.unservice_small{ width:38px; height:120px; background:url(https://online.unionpay.com/static/cms/img/21/d11fda13-3bb9-45c3-aa47-d85e67e5fe47.gif) no-repeat 0 -260px; display:inline-block; margin-bottom:15px;}"
                    + ".rightservice a.service_small:hover{ background-position:0 -380px;}"
                    + ".rightservice a.unservice_small{ background-position:0 -500px;}"
                    + "</style>";

            $(str).appendTo($('head'));
            str = "<div class='rightservice'><a href='https://95516.unionpay.com/web/icc/chat/chat?c=1&s=4&st=1&depid=ff8080813daaa230013de380641900ec&' target='_blank' class='" + this._arg._class + "' id='rightservice_img'></a>";
            this._el = $(str);
            this._el.appendTo(win.document.body);
            this._icon = this._el.find('#rightservice_img');
        },
        init: function (arg) {
            if (inited)
                return;
            else
                inited = true;
            arg = arg || {};

            //参数校验
            if (arg.currentTime && !(/^\d{4}\/\d{2}\/\d{2} \d{2}:\d{2}:\d{2}$/.test(arg.currentTime))) {
                warning('参数错误！currentTime 格式为[yyyy/MM/dd hh:mm:ss]');
                return;
            }
            if (arg.startTime && !(/^\d{2}:\d{2}$/.test(arg.startTime))) {
                warning('参数错误！starttTime 格式为[mm:ss]');
                return;
            }
            if (arg.endTime && !(/^\d{2}:\d{2}$/.test(arg.endTime))) {
                warning('参数错误！endTime 格式为[mm:ss]');
                return;
            }
            arg = $.extend({
                currentTime: sys_time,
                startTime: '08:30',
                endTime: '22:30',
                consultUrl: 'https://95516.unionpay.com/web/icc/chat/chat?c=1&s=4&st=1&depid=ff8080813daaa230013de380641900ec&',
                messageUrl: 'https://95516.unionpay.com/web/icc/chat/chat?c=1&s=4&st=1&depid=ff8080813daaa230013de380641900ec&',
                right: screen_width > 1024 ? 10 : 2,
                width: screen_width > 1024 ? 53 : 38,
                top: 152,
                zIndex: 500,
                _class: 'service' + (screen_width <= 1024 ? '_small' : '')
            }, arg);
            arg.currentTime = new Date(arg.currentTime);
            arg.startTime = parseInt(arg.startTime.replace(':', ''), 10);
            arg.endTime = parseInt(arg.endTime.replace(':', ''), 10);

            this._arg = arg;
            //参数校验完毕

            this._insertHTML();
            this.checkTime();


            //自动检测时间
            win.setInterval(function () {
                win.up.floatCS.checkTime();
            }, check_interval * 1000);
            if ($.browser.msie && $.browser.version == "6.0") {
                this._el.css('position', 'absolute');
                var t = this;
                $(win).bind('scroll', function () {
                    t._el.css('top', win.document.documentElement.scrollTop + t._arg.top)
                });
            }
        },
        checkTime: function () {
            var period = new Date() - sys_time,
                now = new Date(this._arg.currentTime.getTime() + period);
            //得出当前时间，并获取时分
            now = now.getHours() * 100 + now.getMinutes();
            //判断是否在设定区间
            if (now >= this._arg.startTime && now <= this._arg.endTime) {
                this._icon.removeClass('un' + this._arg._class).addClass(this._arg._class);
            } else {
                this._icon.removeClass(this._arg._class).addClass('un' + this._arg._class);
            }

        }
    }

    $(win).bind('load', function () {
        win.up.floatCS.init();
    })

})(jQuery, window);