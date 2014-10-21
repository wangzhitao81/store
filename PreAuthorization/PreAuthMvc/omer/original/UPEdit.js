var UPEdit_IE32_CLASSID = "0E48410F-D1B8-472A-85DB-27F3D77284CE";
var UPEdit_IE32_CAB = "UPEditor.cab#version=1,0,0,4";
var UPEdit_IE32_EXE = "UPEditorIE.exe";

var UPEdit_IE64_CLASSID = "2A688E44-F42D-4134-B37A-E56FDFD5025A";
var UPEdit_IE64_CAB = "UPEditorX64.cab#version=1,0,0,3";
var UPEdit_IE64_EXE = "UPEditorX64.exe";

var UPEdit_FF = "UPEditorFF.exe";
var UPEdit_Linux32 = "UPEditorLinux.tar.bz2";
var UPEdit_Linux64 = "UPEditorLinux64.tar.bz2";
var UPEdit_FF_VERSION = "2.0.9.0";
var UPEdit_Linux_VERSION = "1.0.0.6";

var UPEdit_MacOs = "UPEditor.dmg";
var UPEdit_MacOs_VERSION = "1.0.0.3";

var UPEdit_MacOs_Safari = "UPEditorSafari.dmg";
var UPEdit_MacOs_Safari_VERSION = "1.0.0.1";

; $.extend({
    upeDefaultKeyDown: function (fn, keyCode, nextElementId) {
        if ($.isFunction(fn)) {
            fn(keyCode, nextElementId);
        }
        else {
            //var x = document.getElementById(nextElementId);
            //if(x){
            //	x.focus();
            //}
            setTimeout(function () { document.getElementById(nextElementId).focus(); }, 100);
        }
    }
});

; (function ($) {
    $.upe = function (options) {
        this.settings = $.extend(true, {}, $.upe.defaults, options);
        this.init();
    };

    $.extend($.upe, {
        defaults: {
            upePath: "./upe/",
            upeId: "",
            upeSk: "",
            upeEdittype: 0,
            upeMode: "1111",
            upeMinlength: 6,
            upeMaxlength: 12,
            upePwdMode: 2,
            upeTabindex: 2,
            upeNextElementId: "",
            upeFontName: "Arial Black",
            upeFontSize: 22,
            upeClass: "",
            upeObjClass: "upeObj",
            upeInstallClass: "upeInstall",//针对安装或升级
            resp: "80", // 未知错误
            errMappingArr: "",
            errMapping: { "00": "正常", "01": "控件未安装", "02": "配置错误", "03": "初始化错误", "04": "seed错误", "05": "输入为空", "06": "输入过短", "07": "输入过长", "08": "非法字符", "09": "加密出错", "10": "调用出错" },
            upeCertIndex: 1,
            enterCallback: null,
            tabCallback: null
        },

        prototype: {

            init: function () {
                this.upeDownText = "请点此安装控件";
                this.osBrowser = this.checkOsBrowser();
                this.upeVersion = this.getVersion();
                this.isInstalled = this.checkInstall();

            },

            checkOsBrowser: function () {
                var userosbrowser;
                if ((navigator.platform == "Win32") || (navigator.platform == "Windows")) {
                    if (navigator.userAgent.indexOf("MSIE") > 0 || navigator.userAgent.indexOf("msie") > 0 || navigator.userAgent.indexOf("Trident") > 0 || navigator.userAgent.indexOf("trident") > 0) {
                        if (navigator.userAgent.indexOf("ARM") > 0) {
                            userosbrowser = 9; //win8 RAM Touch
                        } else {
                            userosbrowser = 1;//windows32ie32
                            this.upeditIEClassid = UPEdit_IE32_CLASSID;
                            this.upeditIECab = UPEdit_IE32_CAB;
                            this.upeditIEExe = UPEdit_IE32_EXE;
                        }
                    } else {
                        userosbrowser = 2; //windowsff
                        this.upeditFFExe = UPEdit_FF;
                    }
                } else if ((navigator.platform == "Win64")) {
                    if (navigator.userAgent.indexOf("Windows NT 6.2") > 0 || navigator.userAgent.indexOf("windows nt 6.2") > 0) {
                        userosbrowser = 1;
                        this.upeditIEClassid = UPEdit_IE32_CLASSID;
                        this.upeditIECab = UPEdit_IE32_CAB;
                        this.upeditEXE = UPEdit_IE32_EXE;
                    } else if (navigator.userAgent.indexOf("MSIE") > 0 || navigator.userAgent.indexOf("msie") > 0 || navigator.userAgent.indexOf("Trident") > 0 || navigator.userAgent.indexOf("trident") > 0) {
                        userosbrowser = 3;//windows64ie64
                        this.upeditIEClassid = UPEdit_IE64_CLASSID;
                        this.upeditIECab = UPEdit_IE64_CAB;
                        this.upeditIEExe = UPEdit_IE64_EXE;
                    } else {
                        userosbrowser = 2;//windowsff
                        this.upeditFFExe = UPEdit_FF;

                    }
                } else if (navigator.userAgent.indexOf("Linux") > 0) {
                    if (navigator.userAgent.indexOf("_64") > 0) {
                        userosbrowser = 4;//linux64
                        this.upeditFFExe = UPEdit_Linux64;
                    } else {
                        userosbrowser = 5;//linux32
                        this.upeditFFExe = UPEdit_Linux32;
                    }
                    if (navigator.userAgent.indexOf("Android") > 0) {
                        userosbrowser = 7;//Android
                    }

                } else if (navigator.userAgent.indexOf("Macintosh") > 0) {
                    if (navigator.userAgent.indexOf("Safari") > 0 && (navigator.userAgent.indexOf("Version/5.1") > 0 || navigator.userAgent.indexOf("Version/5.2") > 0 || navigator.userAgent.indexOf("Version/5.3") > 0 || navigator.userAgent.indexOf("Version/5.4") > 0)) {
                        userosbrowser = 8;//macos Safari 5.1 more
                        this.upeditFFExe = UPEdit_MacOs_Safari;
                    } else if (navigator.userAgent.indexOf("Firefox") > 0 || navigator.userAgent.indexOf("Chrome") > 0) {
                        userosbrowser = 6;//macos
                        this.upeditFFExe = UPEdit_MacOs;
                    } else if (navigator.userAgent.indexOf("Opera") >= 0 && (navigator.userAgent.indexOf("Version/11.6") > 0 || navigator.userAgent.indexOf("Version/11.7") > 0)) {
                        userosbrowser = 6;//macos
                        this.upeditFFExe = UPEdit_MacOs;
                    } else if (navigator.userAgent.indexOf("Safari") >= 0) {
                        userosbrowser = 6;//macos
                        this.upeditFFExe = UPEdit_MacOs;
                    } else {
                        userosbrowser = 0;//other                  
                    }
                }
                return userosbrowser;
            },

            getupeHtml: function () {
                if (this.osBrowser == 1 || this.osBrowser == 3) {

                    return '<span id="' + this.settings.upeId + '_upe" style="display:none;"><OBJECT ID="' + this.settings.upeId + '" CLASSID="CLSID:' + this.upeditIEClassid + '" codebase="'

					        + this.settings.upePath + this.upeditIECab + '" onkeydown="'

					        + 'var fn4Enter = ' + ($.isFunction(this.settings.enterCallback) ? this.settings.enterCallback.toString().replace(/\x22/gi, '\x27') : null)
					        + ';if(13==event.keyCode){$.upeDefaultKeyDown(fn4Enter, event.keyCode, \'' + this.settings.upeNextElementId + '\');}'

					        + 'var fn4Tab = ' + ($.isFunction(this.settings.tabCallback) ? this.settings.tabCallback.toString().replace(/\x22/gi, '\x27') : 'function(e){e.keyCode=9;}')
					        + ';if(9==event.keyCode){$.upeDefaultKeyDown(fn4Tab, event.keyCode, \'' + this.settings.upeNextElementId + '\');}'

					        + '" tabindex="' + this.settings.upeTabindex + '" class="' + this.settings.upeClass + ' ' + this.settings.upeObjClass + '">'

					        + '<param name="edittype" value="' + this.settings.upeEdittype + '"><param name="minlength" value="'

					        + this.settings.upeMinlength + '"><param name="maxlength" value="' + this.settings.upeMaxlength

							+ '"><param name="input10" value="' + this.settings.upeMode + '"><param name="input9" value="' + this.settings.upeCertIndex + '"></OBJECT></span>'

							+ '<div id="' + this.settings.upeId + '_down" class="' + this.settings.upeInstallClass + '" style="text-align:center;display:none;"><a href="' + this.settings.upePath + this.upeditIEExe + '">' + this.upeDownText + '</a></div>';

                } else if (this.osBrowser == 2 || this.osBrowser == 4 || this.osBrowser == 5) {
                    return '<embed ID="' + this.settings.upeId + '" input_1009="$.upeDefaultKeyDown(' + this.settings.tabCallback + ', 9, \'' + this.settings.upeNextElementId + '\')" input_1013="$.upeDefaultKeyDown(' + this.settings.enterCallback + ', 13, \'' + this.settings.upeNextElementId + '\');" input_900="' + this.settings.upeCertIndex + '" init="' + this.settings.upeMode + '" minlength="' + this.settings.upeMinlength + '" maxlength="' + this.settings.upeMaxlength + '" edittype="' + this.settings.upeEdittype + '" type="application/UPEditor" tabindex="' + this.settings.upeTabindex + '" class="' + this.settings.upeClass + ' ' + this.settings.upeObjClass + '" >';

                } else if (this.osBrowser == 6 && navigator.userAgent.indexOf("5.1.2") < 0 && navigator.userAgent.indexOf("5.1.3") < 0 && navigator.userAgent.indexOf("5.1.4") < 0 && navigator.userAgent.indexOf("5.1.5") < 0) {

                    return '<embed ID="' + this.settings.upeId + '" input5="' + this.settings.upeCertIndex + '" input6="' + this.settings.upeMode + '" input7="' + Number(this.settings.upeMinlength) + '" input4="' + Number(this.settings.upeMaxlength) + '" input0="' + Number(this.settings.upeEdittype) + '" type="application/UnionPay-SecurityEdit-plugin" version="' + UPEdit_MacOs_VERSION + '" tabindex="' + this.settings.upeTabindex + '" class="' + this.settings.upeClass + ' ' + this.settings.upeObjClass + '">';

                } else if (this.osBrowser == 8) {

                    return '<embed ID="' + this.settings.upeId + '" input5="' + this.settings.upeCertIndex + '" input6="' + this.settings.upeMode + '" input7="' + Number(this.settings.upeMinlength) + '" input4="' + Number(this.settings.upeMaxlength) + '" input0="' + Number(this.settings.upeEdittype) + '" type="application/UnionPay-SecurityEdit-Safari-plugin" version="' + UPEdit_MacOs_Safari_VERSION + '" tabindex="' + this.settings.upeTabindex + '" class="' + this.settings.upeClass + ' ' + this.settings.upeObjClass + '">';

                } else {

                    return '<div id="' + this.settings.upeId + '_down" class="' + this.settings.upeInstallClass + '" style="text-align:center;">暂不支持此浏览器</div>';

                }
            },
            getupeHtmlspan: function () {
                if (this.osBrowser == 1 || this.osBrowser == 3) {

                    return '<span id="' + this.settings.upeId + '_upe" style="display:none;"><OBJECT ID="' + this.settings.upeId + '" CLASSID="CLSID:' + this.upeditIEClassid + '" codebase="'

					        + this.settings.upePath + this.upeditIECab + '" onkeydown="'

					        + 'var fn4Enter = ' + ($.isFunction(this.settings.enterCallback) ? this.settings.enterCallback.toString().replace(/\x22/gi, '\x27') : null)
					        + ';if(13==event.keyCode){$.upeDefaultKeyDown(fn4Enter, event.keyCode, \'' + this.settings.upeNextElementId + '\');}'

					        + 'var fn4Tab = ' + ($.isFunction(this.settings.tabCallback) ? this.settings.tabCallback.toString().replace(/\x22/gi, '\x27') : 'function(e){e.keyCode=9;}')
					        + ';if(9==event.keyCode){$.upeDefaultKeyDown(fn4Tab, event.keyCode, \'' + this.settings.upeNextElementId + '\');}'

					        + '" tabindex="' + this.settings.upeTabindex + '" class="' + this.settings.upeClass + ' ' + this.settings.upeObjClass + '">'

					        + '<param name="edittype" value="' + this.settings.upeEdittype + '"><param name="minlength" value="'

					        + this.settings.upeMinlength + '"><param name="maxlength" value="' + this.settings.upeMaxlength

							+ '"><param name="input10" value="' + this.settings.upeMode + '"><param name="input9" value="' + this.settings.upeCertIndex + '"></OBJECT></span>'

							+ '<span id="' + this.settings.upeId + '_down" class="' + this.settings.upeInstallClass + '" style="text-align:center;display:none;"><a href="' + this.settings.upePath + this.upeditIEExe + '">' + this.upeDownText + '</a></span>';

                } else if (this.osBrowser == 2 || this.osBrowser == 4 || this.osBrowser == 5) {
                    return '<embed ID="' + this.settings.upeId + '" input_1009="$.upeDefaultKeyDown(' + this.settings.tabCallback + ', 9, \'' + this.settings.upeNextElementId + '\')" input_1013="$.upeDefaultKeyDown(' + this.settings.enterCallback + ', 13, \'' + this.settings.upeNextElementId + '\');" input_900="' + this.settings.upeCertIndex + '" init="' + this.settings.upeMode + '" minlength="' + this.settings.upeMinlength + '" maxlength="' + this.settings.upeMaxlength + '" edittype="' + this.settings.upeEdittype + '" type="application/UPEditor" tabindex="' + this.settings.upeTabindex + '" class="' + this.settings.upeClass + ' ' + this.settings.upeObjClass + '" >';
                } else if (this.osBrowser == 6 && navigator.userAgent.indexOf("5.1.2") < 0 && navigator.userAgent.indexOf("5.1.3") < 0 && navigator.userAgent.indexOf("5.1.4") < 0 && navigator.userAgent.indexOf("5.1.5") < 0) {

                    return '<embed ID="' + this.settings.upeId + '" input5="' + this.settings.upeCertIndex + '" input6="' + this.settings.upeMode + '" input7="' + Number(this.settings.upeMinlength) + '" input4="' + Number(this.settings.upeMaxlength) + '" input0="' + Number(this.settings.upeEdittype) + '" type="application/UnionPay-SecurityEdit-plugin" version="' + UPEdit_MacOs_VERSION + '" tabindex="' + this.settings.upeTabindex + '" class="' + this.settings.upeClass + ' ' + this.settings.upeObjClass + '">';

                } else if (this.osBrowser == 8) {

                    return '<embed ID="' + this.settings.upeId + '" input5="' + this.settings.upeCertIndex + '" input6="' + this.settings.upeMode + '" input7="' + Number(this.settings.upeMinlength) + '" input4="' + Number(this.settings.upeMaxlength) + '" input0="' + Number(this.settings.upeEdittype) + '" type="application/UnionPay-SecurityEdit-Safari-plugin" version="' + UPEdit_MacOs_Safari_VERSION + '" tabindex="' + this.settings.upeTabindex + '" class="' + this.settings.upeClass + ' ' + this.settings.upeObjClass + '">';

                } else {

                    return '<div id="' + this.settings.upeId + '_down" class="' + this.settings.upeInstallClass + '" style="text-align:center;">暂不支持此浏览器</div>';

                }
            },
            getDownHtml: function () {
                if (this.osBrowser == 1 || this.osBrowser == 3) {
                    return '<div id="' + this.settings.upeId + '_down" class="' + this.settings.upeInstallClass + '" style="text-align:center;"><a href="' + this.settings.upePath + this.upeditIEExe + '">' + this.upeDownText + '</a></div>';
                } else if (this.osBrowser == 2 || this.osBrowser == 4 || this.osBrowser == 5 || this.osBrowser == 6 || this.osBrowser == 8) {

                    return '<div id="' + this.settings.upeId + '_down" class="' + this.settings.upeInstallClass + '" style="text-align:center;"><a href="' + this.settings.upePath + this.upeditFFExe + '">' + this.upeDownText + '</a></div>';

                } else {

                    return "";

                }
            },
            getDownHtmlspan: function () {
                if (this.osBrowser == 1 || this.osBrowser == 3) {
                    return '<span id="' + this.settings.upeId + '_down" class="' + this.settings.upeInstallClass + '" style="text-align:center;"><a href="' + this.settings.upePath + this.upeditIEExe + '">' + this.upeDownText + '</a></span>';
                } else if (this.osBrowser == 2 || this.osBrowser == 4 || this.osBrowser == 5 || this.osBrowser == 6 || this.osBrowser == 8) {

                    return '<span id="' + this.settings.upeId + '_down" class="' + this.settings.upeInstallClass + '" style="text-align:center;"><a href="' + this.settings.upePath + this.upeditFFExe + '">' + this.upeDownText + '</a></span>';

                } else {

                    return "";

                }
            },
            load: function () {

                if (!this.checkInstall()) {
                    return this.getDownHtml();
                } else {
                    if (this.osBrowser == 2) {
                        if (this.upeVersion != UPEdit_FF_VERSION) {

                            return this.getDownHtml();
                        }
                    } else if (this.osBrowser == 4 || this.osBrowser == 5) {
                        if (this.upeVersion != UPEdit_Linux_VERSION) {
                            return this.getDownHtml();
                        }
                    } else if (this.osBrowser == 6) {
                        if (this.upeVersion != UPEdit_MacOs_VERSION) {
                            return this.getDownHtml();
                        }
                    } else if (this.osBrowser == 8) {
                        if (this.upeVersion != UPEdit_MacOs_Safari_VERSION) {
                            return this.getDownHtml();
                        }
                    }
                    return this.getupeHtml();
                }
            },

            generate: function () {
                if (this.osBrowser == 2) {
                    if (this.upeVersion != UPEdit_FF_VERSION) {
                        return document.write(this.getDownHtml());
                    }
                } else if (this.osBrowser == 4 || this.osBrowser == 5) {
                    if (this.upeVersion != UPEdit_Linux_VERSION) {
                        return document.write(this.getDownHtml());
                    }
                } else if (this.osBrowser == 6) {
                    if (this.upeVersion != UPEdit_MacOs_VERSION) {
                        return document.write(this.getDownHtml());
                    }
                } else if (this.osBrowser == 8) {
                    if (this.upeVersion != UPEdit_MacOs_Safari_VERSION) {
                        return document.write(this.getDownHtml());
                    }
                }
                return document.write(this.getupeHtml());
            },
            generatespan: function () {
                if (this.osBrowser == 2) {
                    if (this.upeVersion != UPEdit_FF_VERSION) {
                        return document.write(this.getDownHtmlspan());
                    }
                } else if (this.osBrowser == 4 || this.osBrowser == 5) {
                    if (this.upeVersion != UPEdit_Linux_VERSION) {
                        return document.write(this.getDownHtmlspan());
                    }
                } else if (this.osBrowser == 6) {
                    if (this.upeVersion != UPEdit_MacOs_VERSION) {
                        return document.write(this.getDownHtmlspan());
                    }
                } else if (this.osBrowser == 8) {
                    if (this.upeVersion != UPEdit_MacOs_Safari_VERSION) {
                        return document.write(this.getDownHtml());
                    }
                }
                return document.write(this.getupeHtmlspan());
            },
            pwdclear: function () {
                if (this.checkInstall()) {
                    var control = document.getElementById(this.settings.upeId);
                    control.ClearSeCtrl();
                }
            },

            result: function (d) {

                var code = '';

                if (!this.checkInstall()) {

                    code = '01';

                }

                else {

                    try {

                        var control = document.getElementById(this.settings.upeId);

                        if (this.settings.upePwdMode == 1) {
                            if (this.osBrowser == 1 || this.osBrowser == 3) {
                                code = control.GetOutputEx(0, this.settings.upeSk, d);
                            } else if (this.osBrowser == 2 || this.osBrowser == 4 || this.osBrowser == 5) {
                                control.input(901, d);
                                control.input(902, this.settings.upeSk);
                                code = control.output(900);
                            } else if (this.osBrowser == 6 || this.osBrowser == 8) {
                                code = control.get_output6(0, this.settings.upeSk, d);
                            }
                        } else if (this.settings.upePwdMode == 2) {
                            if (this.osBrowser == 1 || this.osBrowser == 3) {
                                code = control.GetOutputEx(1, this.settings.upeSk, d);
                            } else if (this.osBrowser == 2 || this.osBrowser == 4 || this.osBrowser == 5) {
                                control.input(901, d);
                                control.input(902, this.settings.upeSk);
                                code = control.output(901);
                            } else if (this.osBrowser == 6 || this.osBrowser == 8) {
                                code = control.get_output6(1, this.settings.upeSk, d);
                            }
                        } else if (this.settings.upePwdMode == 3) {
                            if (this.osBrowser == 1 || this.osBrowser == 3) {
                                code = control.GetOutputEx(2, this.settings.upeSk, d);
                            } else if (this.osBrowser == 2 || this.osBrowser == 4 || this.osBrowser == 5) {
                                control.input(901, d);
                                control.input(902, this.settings.upeSk);
                                code = control.output(902);
                            } else if (this.osBrowser == 6 || this.osBrowser == 8) {
                                code = control.get_output6(2, this.settings.upeSk, d);
                            }

                        }


                    } catch (err) {


                        code = '03';

                    }


                }
                //alert(code);
                return new $.upeResult(code, this.settings.errMapping);
            },

            machineInfo: function (d) {
                var code = '';

                if (!this.checkInstall()) {

                    code = '01';

                }

                else {

                    try {

                        var control = document.getElementById(this.settings.upeId);

                        if (this.osBrowser == 1 || this.osBrowser == 3) {
                            code = control.GetOutput(this.settings.upeSk, d);
                        } else if (this.osBrowser == 2 || this.osBrowser == 4 || this.osBrowser == 5) {
                            control.input(901, d);
                            control.input(902, this.settings.upeSk);
                            code = control.output(903);
                        } else if (this.osBrowser == 6 || this.osBrowser == 8) {
                            code = control.get_output7(this.settings.upeSk, d);
                        }


                    } catch (err) {

                        code = '03';

                    }


                }

                return new $.upeResult(code, this.settings.errMapping);
            },
            pwdStrength: function () {
                var code = '0';

                if (!this.checkInstall()) {

                    code = '01';

                }

                else {

                    try {

                        var control = document.getElementById(this.settings.upeId);

                        if (this.osBrowser == 1 || this.osBrowser == 3) {
                            code = control.output4;
                        } else if (this.osBrowser == 2 || this.osBrowser == 4 || this.osBrowser == 5) {
                            code = control.output(4);
                        } else if (this.osBrowser == 6 || this.osBrowser == 8) {
                            code = control.get_output4();
                        }

                    } catch (err) {

                        code = '0';

                    }

                }
                return code;

            },

            checkInstall: function () {
                try {
                    if (this.osBrowser == 1) {
                        var comActiveX = new ActiveXObject("UPEditor.UPEditorCtrl.1");

                    } else if (this.osBrowser == 2 || this.osBrowser == 4 || this.osBrowser == 5 || this.osBrowser == 6 || this.osBrowser == 8) {

                        var arr = new Array();
                        if (this.osBrowser == 8) {
                            var upe_info = navigator.plugins['UPEditor Safari'].description;
                        } else {
                            var upe_info = navigator.plugins['UPEditor'].description;
                        }
                        if (upe_info.indexOf(":") > 0) {
                            arr = upe_info.split(":");
                            var upe_version = arr[1];
                        } else {
                            var upe_version = "";
                        }
                    } else if (this.osBrowser == 3) {
                        var comActiveX = new ActiveXObject("UPEditorX64.UPEditorCtrl.1");
                    }
                } catch (e) {
                    return false;
                }
                return true;
            },
            getVersion: function () {
                try {
                    if (this.osBrowser == 1 || this.osBrowser == 3) {
                        var control = document.getElementById(this.settings.upeId);
                        var upe_version = control.output29;
                    } else {
                        var arr = new Array();
                        if (this.osBrowser == 8) {
                            var upe_info = navigator.plugins['UPEditor Safari'].description;
                        } else {
                            var upe_info = navigator.plugins['UPEditor'].description;
                        }
                        if (upe_info.indexOf(":") > 0) {
                            arr = upe_info.split(":");
                            var upe_version = arr[1];
                        } else {
                            var upe_version = "";
                        }
                        if (this.osBrowser == 2) {
                            if (upe_version != undefined && upe_version != "" && upe_version != UPEdit_FF_VERSION) {
                                this.upeDownText = "请点此升级控件";
                            }
                        } else if (this.osBrowser == 4 || this.osBrowser == 5) {
                            if (upe_version != undefined && upe_version != "" && upe_version != UPEdit_Linux_VERSION) {
                                this.upeDownText = "请点此升级控件";
                            }
                        } else if (this.osBrowser == 6) {
                            if (upe_version != undefined && upe_version != "" && upe_version != UPEdit_MacOs_VERSION) {
                                this.upeDownText = "请点此升级控件";
                            }
                        } else if (this.osBrowser == 8) {
                            if (upe_version != undefined && upe_version != "" && upe_version != UPEdit_MacOs_Safari_VERSION) {
                                this.upeDownText = "请点此升级控件";
                            }
                        }
                    }
                    return upe_version;
                } catch (e) {
                    return "";
                }
            },
            refresh4IE: function () {
                if (this.checkInstall()) {
                    if (this.osBrowser == 1 || this.osBrowser == 3) {
                        $('#' + this.settings.upeId + '_upe').show();
                    }
                } else {
                    if (this.osBrowser == 1 || this.osBrowser == 3) {
                        $('#' + this.settings.upeId + '_down').show();
                    }
                }

            }
        }
    });


    $.upeResult = function (options, errMapping) {
        this.settings = $.extend(true, {}, options, errMapping);
        this.init(options, errMapping);
    };
    $.extend($.upeResult, {
        prototype: {
            init: function (options, errMapping) {
                this.errMappingArr = errMapping;
                var arr = new Array();
                if (options.indexOf(":") > 0) {
                    arr = options.split(":");
                    this.resp = arr[0];
                    this.cypher = arr[1];
                } else {
                    this.resp = options;
                    this.cypher = "";

                }
                this.error = (this.resp == null || this.resp == undefined || this.resp.length != 2 || this.resp != "00");
            },

            isError: function () {
                return this.error;
            },

            errMsg: function () {
                if (this.resp != "") {
                    return this.errMappingArr[this.resp];
                } else {
                    return '未知错误:' + this.resp;
                }
            }

        }
    });

})(jQuery);

