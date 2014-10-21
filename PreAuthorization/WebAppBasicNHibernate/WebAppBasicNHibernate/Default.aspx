<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAppBasicNHibernate.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Operation:
        <asp:Label ID="lblOperations" runat="server"></asp:Label>
        <br />
        <asp:Button ID="btnExecuteNHibernateOperations" 
            Text="Execute NHibernate operations" runat="server" 
            onclick="btnExecuteNHibernateOperations_Click" />
    </div>
    </form>
</body>
</html>
