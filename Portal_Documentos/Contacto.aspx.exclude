<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Contacto.aspx.cs" Inherits="Faqs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="Content/Bootstrap/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/Bootstrap/bootstrap.js"></script>
    <link href="Content/Otros/jquery-ui_faqs.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link href="https://fonts.googleapis.com/css?family=Raleway" rel="stylesheet">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">
    <script>
        $(function () {
            $("#accordion").accordion({
                collapsible: true
            });
        });

        function edit() {
            var elemento = document.querySelector('#accordion');
            var elemento_1 = document.querySelector('#Edit');
            var elemento_2 = document.querySelector('#Save');
            elemento.setAttribute("contenteditable", "true");
            elemento_1.setAttribute("style", "display:none");
            elemento_2.setAttribute("style", "display:inline");
        }
        function save() {
            var elemento = document.querySelector('#accordion');
            var elemento_1 = document.querySelector('#Edit');
            var elemento_2 = document.querySelector('#Save');
            elemento.setAttribute("contenteditable", "false");
            elemento_2.setAttribute("style", "display:none");
            elemento_1.setAttribute("style", "display:inline");
        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="font-family:raleway">
        <h1>ULA Online</h1>
        <h3 style="font-size:x-large">Tel. 800 0011 852</h3>
        <ul style="font-size:large">
            <li>Si deseas contactar a tu Asesor elige la opci&oacute;n 2</li>
            <li>Si deseas contactar a Servicios Escolares elige la opci&oacute;n 3</li>
        </ul>
        <h3 style="font-size:x-large"><i class="fas fa-clock"></i>Horario de atenci&oacute;n:</h3>
        <ul style="font-size:large">
            <li>Lunes a jueves de 8:00 a 20:00 horas</li>
            <li>Viernes de 8:00 a 16:00 horas</li>
            <li>S&aacute;bado de 9:00 a 14:00 horas</li>
        </ul>
        <h2 style="font-size:x-large">Mesa de Soporte T&eacute;cnico</h2>
        <h3 style="font-size:x-large">Tel. (55) 8500 8100 ext. 8292</h3>
        <h3 style="font-size:x-large"><a href="mailto:soporte@ula.edu.mx">soporte@ula.edu.mx</a></h3><br />
        <h3 style="font-size:x-large"><i class="fas fa-clock"></i>Horario de atenci&oacute;n:</h3>
        <ul style="font-size:large">
            <li>Lunes a s&aacute;bado de 7:00 a 22:00 horas</li>
            <li>Domingo 9:00 a 22:00 horas</li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="FootContent" runat="server" ContentPlaceHolderID="FooterContent">
    <table width="100%" align="center" style="font-family: Raleway;">
        <tr valign="bottom">
            <td align="center">
                <asp:Label runat="server" ForeColor="#3c9ab6" Font-Bold="true" ID="lblBottomHelp">Si tienes alguna duda, comunícate con tu Asesor al 01 800 0011 852 opción 2</asp:Label>
            </td>
        </tr>
    </table>
    <%--<h3>Session Idle:&nbsp;<span id="secondsIdle"></span>&nbsp;seconds.</h3>--%>

    <link href="Content/Bootstrap/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/Sweet_Alert/sweetalert.js"></script>
    <link href="Content/Sweet_Alert/sweetalert.css" rel="stylesheet" />
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>--%>

    <script type="text/javascript">

        function SessionExpireAlert(timeout) {
            var seconds = timeout / 1000;
            $("#secondsIdle").html(seconds);
            $("#seconds").html(seconds);
            setInterval(function () {
                seconds--;
                $("#secondsIdle").html(seconds);
                $("#seconds").html(seconds);
            }, 1000);
            setTimeout(function () {
                //Show Popup before 20 seconds of timeout.
                swal({
                     //title: 'Sesión por Expirar',
                    html: '<h2 class="swal2-title" id="swal2-title"><img src="images/icono_sesion_alert.png" style="width: 100px;"><br><br>Sesión por expirar</h2>Tu sesión esta apunto de expirar.<br>¿Deseas continuar en nuestro portal?',
                    //type: 'info',
                    confirmButtonText: 'Si,continuar'
                }).then(function () {
                    window.location = window.location.href;
                })
            }, timeout - 60 * 1000);
            setTimeout(function () {
                window.location = "Logout.aspx";
            }, timeout);
        };
    </script>
</asp:Content>

