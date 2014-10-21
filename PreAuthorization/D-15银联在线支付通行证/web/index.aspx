<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="index.aspx.cs" Inherits="Index" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        #flist li{width:20%;float:left;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ul id="flist">
            <%=GetUserInfo()%>
        </ul>
    <br />
    
    <asp:Button ID="btnSend" Text="快捷登录" runat="server" OnClick="btnSend_Click" />
    </div>
    <br />
    <a href="<%=GetAddressInfo()%>">获取地址信息</a>
    <div>
    </div>
    </form>
</body>
</html>