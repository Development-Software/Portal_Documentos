<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html lang="en" class="">
<head>
    <title></title>
    <meta charset="UTF-8">
    <link href="Content/login.css" rel="stylesheet" />

    <script>
        window.console = window.console || function (t) { };
    </script>
    <script>
        if (document.location.search.match(/type=embed/gi)) {
            window.parent.postMessage("resize", "*");
        }
    </script>
    <style>
        .btn_login {
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
            outline: 0;
            background-color: white;
            border: 0;
            padding: 10px 15px;
            color: #53e3a6;
            border-radius: 3px;
            width: 250px;
            cursor: pointer;
            font-size: 18px;
            transition-duration: 0.25s;
        }

            .btn_login:hover {
                background-color: #f5f7f9;
            }

        .test {
            background-color: aquamarine !important;
        }
    </style>
</head>
<body>
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel_1" runat="server">
            <ContentTemplate>
                <div class="wrapper">
                    <div class="container">
                        <h1>Bienvenido</h1>
                        <div id="formulario">
                            <asp:TextBox ID="txtusername" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtpassword" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:Button ID="Button1" runat="server" Text="Entrar" CssClass="btn_login" OnClientClick="Entrar();" OnClick="btn_login_Click" />
                        </div>
                        <div id="loader" class="loader10" style="display: none;">
                        </div>
                    </div>
                    	<ul class="bg-bubbles">
		<li></li>
		<li></li>
		<li></li>
		<li></li>
		<li></li>
		<li></li>
		<li></li>
		<li></li>
		<li></li>
		<li></li>
	</ul>
                </div>
                <script>
                    function Entrar() {
                        const idElemento_u = 'txtusername';
                        const idElemento_p = 'txtpassword';
                        let username = document.getElementById(idElemento_u).value;
                        let password = document.getElementById(idElemento_p).value;
                        if (username != 0 && password != 0) {
                            $('#formulario').fadeOut('slow');
                            $('.wrapper').addClass('form-success');
                            $('#loader').delay(1000).fadeIn('slow');
                            //$('#loader').attr('style', 'display:block');
                            return false;
                        }
                    }
                </script>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="Button1" />
            </Triggers>
        </asp:UpdatePanel>

        <script src="https://cpwebassets.codepen.io/assets/common/stopExecutionOnTimeout-157cd5b220a5c80d4ff8e0e70ac069bffd87a61252088146915e8726e5d9f147.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>

    </form>
</body>
</html>
