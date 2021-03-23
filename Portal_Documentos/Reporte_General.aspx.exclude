<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Reporte_General.aspx.cs" Inherits="Reporte_General" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="Content/Bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="Content/Otros/paginacion.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Raleway" rel="stylesheet">
    <style>
        .oculto {
            display: none;
        }
    </style>
    <script type="text/javascript">

        //document.onload = hide_loading_div();

        function show_loading_div() {
            var my_loading_div = document.getElementById('the_loading_div');
            my_loading_div.style.display = 'block';
        }

        function hide_loading_div() {
            var my_loading_div = document.getElementById('the_loading_div');
            my_loading_div.style.display = 'none';
        }
    </script>

    <style type="text/css">
        .class_of_the_loading_div {
            position: fixed;
            width: 100%;
            height: 100%;
            top: 0;
            left: 0;
            background-color: rgba(0,0,0,0.75);
            text-align: center;
            display: none;
        }
    </style>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
        $(function () {
            var progressTimer,
                progressbar = $("#progressbar"),
                progressLabel = $(".progress-label"),
                //dialogButtons = [{
                //    text: "Cancelar Descarga",
                //    click: closeDownload
                //}],
                dialog = $("#dialog").dialog({
                    autoOpen: false,
                    closeOnEscape: false,
                    resizable: false,
                    //buttons: dialogButtons,
                    open: function () {
                        progressTimer = setTimeout(progress, 2000);
                    },
                    beforeClose: function () {
                        downloadButton.button("option", {
                            disabled: false,
                            label: "Descarga Iniciada"
                        });
                    }
                }),
                downloadButton = $("#downloadButton")
                    .button()
                    .on("click", function () {
                        $(this).button("option", {
                            disabled: true,
                            label: "Descargando..."
                        });
                        dialog.dialog("open");
                    });

            progressbar.progressbar({
                value: false,
                change: function () {
                    progressLabel.text("Progreso: " + progressbar.progressbar("value") + "%");
                },
                complete: function () {
                    progressLabel.text("Descarga Completa!");
                    dialog.dialog("option", "buttons", [{
                        text: "Cerrar",
                        click: closeDownload
                    }]);
                    $(".ui-dialog button").last().trigger("focus");
                }
            });

            function progress() {
                var val = progressbar.progressbar("value") || 0;

                progressbar.progressbar("value", val + Math.floor(Math.random() * 3));

                if (val <= 99) {
                    progressTimer = setTimeout(progress, 50);
                }
            }

            function closeDownload() {
                clearTimeout(progressTimer);
                dialog
                    //.dialog("option", "buttons", dialogButtons)
                    .dialog("close");
                progressbar.progressbar("value", false);
                progressLabel
                    .text("Comenzando Descarga...");
                downloadButton.trigger("focus");
            }
        });
    </script>
    <style>
        #progressbar {
            margin-top: 20px;
        }

        .progress-label {
            font-weight: bold;
            text-shadow: 1px 1px 0 #fff;
        }

        .ui-dialog-titlebar-close {
            display: none;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <%--<center><asp:Label ID="Label1" runat="server" Text="Reporte General" Font-Size="X-Large"></asp:Label></center>--%>
    <asp:GridView ID="GridViewReport" runat="server" HorizontalAlign="Center" Width="100%" CssClass="table-striped table-responsive" Font-Size="7px" OnPageIndexChanging="GridViewReport_PageIndexChanging" PageSize="20" AllowPaging="True">
        <HeaderStyle CssClass="table-header_1" />
        <PagerSettings Mode="NumericFirstLast" FirstPageText="Primero" LastPageText="Ultimo" PageButtonCount="4" />
        <PagerStyle CssClass="pagination-ys" BorderColor="White" HorizontalAlign="Center" />
    </asp:GridView>

    <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center" Width="100%" CssClass="oculto" Font-Size="7px">
    </asp:GridView>

    <div id="dialog" title="Descarga de Reporte" style="display:none">
        <div class="progress-label">Comenzando Descarga...</div>
        <div id="progressbar"></div>
    </div>
    <div style="text-align:center;"><button id="downloadButton" class="btn btn-logout"><a href="Download_Report.aspx" style="color:#fff; text-decoration:none;">Exportar</a></button></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="Server">
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


