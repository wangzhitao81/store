<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.Test.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ul>
            <li>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="预授权" />
                <asp:TextBox ID="txtOrigQid" runat="server" Width="219px" ToolTip="交易流水号"></asp:TextBox>
            </li>
            <li>
                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="撤销预授权" />
                <asp:TextBox ID="txtUndoQid" runat="server" Width="235px"></asp:TextBox>
            </li>
            <li>
                <asp:Button ID="btnComplete" runat="server" OnClick="btnComplete_Click" Text="完成预授权" />
                <asp:TextBox ID="txtCompleteQid" runat="server" Width="235px"></asp:TextBox>
            </li>
    </ul>
    </div>
    </form>
</body>
</html>
