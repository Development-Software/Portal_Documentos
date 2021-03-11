<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Video_F.aspx.cs" Inherits="Video_F" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="https://fonts.googleapis.com/css?family=Raleway" rel="stylesheet">
    <style>
        .label_1{
            font-family:Raleway;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div>
            <video width="750" autoplay>
                <source src="<%=ruta_video %>" type="video/mp4">
                Your browser does not support HTML5 video.
            </video>
            <asp:UpdatePanel ID="updpnl1" runat="server">
                <ContentTemplate>
            <div>
                <p>
                    <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true" /><asp:Label ID="Label1" runat="server" Text="No mostrar al inicio" CssClass="label_1"></asp:Label>
                    <asp:Button ID="Button1" runat="server" Text="Aceptar" CssClass="btn btn-logout" Visible="false" />
                </p>
            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
