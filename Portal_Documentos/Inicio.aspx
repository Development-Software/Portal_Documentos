<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Inicio.aspx.cs" Inherits="Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="Content/Bootstrap/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/Bootstrap/bootstrap.js"></script>
    <link href="https://fonts.googleapis.com/css?family=Raleway" rel="stylesheet">
        <link href="Content/Otros/jquery-ui.css" rel="stylesheet" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.5.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.microsoft.com/ajax/jquery.ui/1.8.6/jquery-ui.min.js"></script>
    <style type="text/css">
        .auto-style1 {
            height: 25px;
        }
    </style>
    <script type="text/javascript">

        function video_modal(contentUrl, closeurl) {
            var $dialog = $('<div class=""></div>')
                .html('<iframe style="border: 0px;overflow-x:hidden;" src="' + contentUrl + '" width="100%" height="100%" scrolling="no"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 600,
                    width: 800,
                    closeOnEscape: false,
                    dialogClass: 'hide-close',
                    show: {
                        effect: "fade",
                        duration: 1000
                    },
                    title: ('<div style="text-align: right;"><a href="' + closeurl + '"><img src="Images/Carga_Documentos/Close.png" alt=""></a></div>')
                });
            $dialog.css('overflow', 'hidden');
            //$dialog.css('background-color', 'black');
            $dialog.dialog('open'); return false;

        }
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <%-- <center><h2>Bienvenido al Sistema de Entrega de Documentación Electrónica</h2></center>
    <center><h3>Servicios Escolares - ULA</h3></center><br />--%>
    <table style="margin-left: auto; margin-right: auto; font-family: Raleway; width: 90%; font-size: 15px;">
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Image ID="imgAyuda" runat="server" ImageUrl="~/Images/Inicio/icono_info.png" /></td>
                        <td>
                            <asp:Label runat="server" Font-Size="Medium" Font-Bold="true" ID="lblAyuda">Conoce nuestro sistema:</asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:Image ID="Image9" runat="server" ImageUrl="~/Images/Inicio/flecha_morada.png" /></td>
                        <td>
                            <p><span>En este sitio podr&aacute;s subir los documentos que requerimos para hacer v&aacute;lidos tus estudios en la Universidad Latinoamericana (ULA). En el video tutorial te explicamos paso a paso c&oacute;mo utilizar este Sistema. &iexcl;Es muy f&aacute;cil!</span></p>
                            <p>&nbsp;</p>
                            <p><span>Por disposici&oacute;n de la Secretaria de Educaci&oacute;n P&uacute;blica (SEP), y de acuerdo con la normatividad interna vigente, es importante que subas tu documentaci&oacute;n completa en un plazo m&aacute;ximo de 90 d&iacute;as naturales, a partir de la fecha de tu primer d&iacute;a de clases. En caso de no entregar la documentaci&oacute;n en el plazo estipulado, la ULA podr&aacute; suspender tu inscripci&oacute;n y&nbsp; los estudios cursados hasta ese momento no tendr&aacute;n validez ante la SEP. </span></p>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <asp:Image ID="Image1" runat="server" /></td>
                        <td class="auto-style1">
                            <p>Tambi&eacute;n y por disposici&oacute;n de la SEP, la ULA queda facultada para solicitar el dictamen de tus documentos a las Instituciones que los emiten. En caso de presentar documentaci&oacute;n no v&aacute;lida se aplicar&aacute;n las sanciones se&ntilde;aladas por la Autoridad Educativa; y la ULA no reembolsar&aacute; ninguna cantidad que se haya pagado durante este periodo.</p>
                            <p>Por &uacute;ltimo te pedimos consideres las siguientes recomendaciones para aprovechar al m&aacute;ximo la funcionalidad de este sitio:</p>
                            <ul>
                                <li>Si el sistema registra que tienes inactividad de 20 minutos o m&aacute;s terminar&aacute; tu sesi&oacute;n y deber&aacute;s volver a acceder al sistema con tu usuario y contrase&ntilde;a.</li>
                                <li>Antes de empezar a cargar tus documentos se te pedir&aacute; que aceptes las pol&iacute;ticas de privacidad, si no lo aceptas no podr&aacute;s cargar tu documentaci&oacute;n.</li>
                            </ul>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Image ID="Image3" runat="server"/></td>
                        <td>
                            <p>No dejes pasar m&aacute;s tiempo y sube tu documentaci&oacute;n. &iexcl;Hacer v&aacute;lidos tus estudios depende de ti!</p>
                            <p>Atentamente,
                            <br />Tu equipo ULA Online
                            <br />01 800 0011 852 Opci&oacute;n 3</p>
                        </td>
                    </tr>
<%--                    <tr>
                        <td>
                            <asp:Image ID="Image11" runat="server" /></td>
                        <td>
                            <asp:Label runat="server" ID="Label8">Si tuvieras alguna duda consulta nuestra sección de Preguntas Frecuentes. Si no aparece la respuesta que necesitas te pedimos que te pongas en contacto con Mesa de Ayuda para recibir soporte técnico o a Servicios Escolares para cualquier duda con respecto a la documentación.</asp:Label></td>
                    </tr>--%>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>

        <tr>
            <td align="right">
                <%--<asp:Label ID="lblIntro" runat="server" Text="Label"></asp:Label>--%>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="FootContent" runat="server" ContentPlaceHolderID="FooterContent">
    <table width="100%" align="center" style="font-family: Raleway;">
        <tr valign="bottom">
            <td align="center">
                <asp:Label runat="server" ForeColor="#3c9ab6" Font-Bold="true" ID="lblBottomHelp">Si tienes problema para acceder al sistema, por favor contáctanos al 8500-8100 ext. 8292 o a soporte@ula.edu.mx</asp:Label>
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

