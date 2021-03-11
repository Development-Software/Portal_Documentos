<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Expires" content="-1" />
<meta http-equiv="CACHE-CONTROL" content="NO-CACHE" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:label ID="lblCas" runat="server" text="Label"></asp:label>
    </div>
    <asp:Label ID="lblResult" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:Label id="lblError" runat="server" Text="Label"></asp:Label>
    <br />
    <p>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </p>
        </form>
</body>
</html>
