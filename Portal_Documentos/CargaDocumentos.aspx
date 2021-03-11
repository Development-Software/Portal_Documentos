<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CargaDocumentos.aspx.cs" Inherits="CargaDocumentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="Content/Bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="Content/FileInput/fileinput.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">
    <link href="Content/Otros/jquery-ui.css" rel="stylesheet" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.5.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.microsoft.com/ajax/jquery.ui/1.8.6/jquery-ui.min.js"></script>
    <link href="https://fonts.googleapis.com/css?family=Raleway" rel="stylesheet">
    <script src="Scripts/FileInput/fileinput.js"></script>
    <link href="Content/Sweet_Alert/sweetalert.css" rel="stylesheet" />
    <script src="Scripts/Sweet_Alert/sweetalert.js"></script>
    <script>
        function JQDialog(contentUrl, closeurl) {
            var $dialog = $('<div class=""></div>')
                .html('<iframe style="border: 0px;overflow-x:hidden;" src="' + contentUrl + '" width="100%" height="100%" scrolling="no"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 600,
                    width: 1300,
                    closeOnEscape: false,
                    dialogClass: 'hide-close',
                    show: {
                        effect: "fade",
                        duration: 1000
                    },
                    title: ('<div style="text-align: right;"><a href="' + closeurl + '"><img src="Images/Carga_Documentos/Close.png" alt=""></a></div>')
                });
            $dialog.dialog('open'); return false;

        }
        function JQDialog_preview(contentUrl, closeurl) {
            var $dialog = $('<div class=""></div>')

                .html('<iframe style="border: 0px; " src="' + contentUrl + '" width="100%" height="100%" align="center"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 680,
                    width: 950,
                    closeOnEscape: false,
                    dialogClass: 'hide-close',
                    show: {
                        effect: "fade",
                        duration: 1000
                    },
                    title: ('<table style="background: #45a4be;"><tbody><tr><td style="width: 100%;font-family: sans-serif;"><img alt="" src="Images/Diseno_Nuevo/pleca_verde_preview.png" width="100%"></td><td style="width: 100%;"><a href="' + closeurl + '"><img src="Images/Carga_Documentos/Close.png" alt=""></a></td></tr></tbody></table>')
                });
            $dialog.dialog('open'); return false;

        }
        function cargar_doc(contentUrl, closeurl) {
            swal({
                //title: 'Documentos Existentes',
                //text: "Ya cuentas con un archivo cargado, si deseas continuar se eliminara el archivo actual.\n¿Deseas continuar?",
                //type: 'info',
                html: '<h2 class="swal2-title" id="swal2-title"><img src="images/icono_sesion_alert.png" style="width: 100px;"><br><br>Documentos Existentes</h2>Ya cuentas con un archivo cargado, si deseas continuar se eliminará el archivo actual.<br>¿Deseas continuar?',
                showCancelButton: true,
                cancelButtonText: 'No',
                confirmButtonText: 'Si'
            }).then(function () {
                var $dialog = $('<div class=""></div>')

                    .html('<iframe style="border: 0px; " src="' + contentUrl + '" width="100%" height="100%" align="center"></iframe>')
                    .dialog({
                        autoOpen: false,
                        modal: true,
                        height: 680,
                        width: 950,
                        closeOnEscape: false,
                        dialogClass: 'hide-close',
                        show: {
                            effect: "fade",
                            duration: 1000
                        },
                        title: ('<div style="text-align: right;"><a href="' + closeurl + '"><img src="Images/Carga_Documentos/Close.png" alt=""></a></div>')
                    });
                $dialog.dialog('open');
                return false;
            })
        }
        function JQDialog_Comentarios(contentUrl) {
            var $dialog = $('<div></div>')
                .html('<iframe style="border: 0px;overflow-x:hidden;" src="' + contentUrl + '" width="100%" height="100%"></iframe>')
                .dialog({
                    autoOpen: false,
                    autoResize: true,
                    modal: true,
                    height: 300,
                    width: 1300,
                    //dialogClass: 'hide-close',
                    show: {
                        effect: "fade",
                        duration: 1000
                    },
                    hide: {
                        effect: "fade",
                        duration: 1000
                    }
                    //title: ('<div style="text-align: right;"><a href="' + closeurl + '"><img src="Images/Carga_Documentos/Close.png" alt=""></a></div>')
                });
            $dialog.dialog('open'); return false;

        }

    </script>

    <style>
        .ColumnaOculta {
            display: none;
        }

        .buttonoculto {
            display: none;
        }

        .divoculto {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:HiddenField runat="server" ID="acepto_priv" ClientIDMode="Static" />

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<center><h2>Listado de Alumnos</h2></center>--%>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div id="contenido" style="width: 100%; font-family: Raleway;">
                <br />
                <br />
                <div class="container">
                    <div class="progress" style="width: 50%; height: 35px; margin: auto;">
                        <div id="html_progress_bar" class="progress-bar progress-bar-striped progress-bar-animated" runat="server">
                            <asp:Label ID="lbl_bar" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
                <br />
                <div style="float: left">
                    <i class="fas fa-search" style="font-size: 25px; vertical-align: -webkit-baseline-middle;"></i>
                </div>
                <div>
                    <asp:TextBox ID="search" runat="server" OnTextChanged="search_TextChanged" AutoPostBack="true" Width="30%" CssClass="form-control"></asp:TextBox>
                </div>
                <br />
                <asp:GridView ID="List_Docs" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table-hover table-striped" HorizontalAlign="Center" OnRowCommand="List_Docs_RowCommand" OnRowDataBound="List_Docs_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="IDTipoDocumento" HeaderText="IDTipoDocumento" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">
                            <HeaderStyle CssClass="ColumnaOculta" />
                            <ItemStyle CssClass="ColumnaOculta" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IDDocumento" HeaderText="IDDocumento" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">
                            <HeaderStyle CssClass="ColumnaOculta" />
                            <ItemStyle CssClass="ColumnaOculta" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IDAlumno" HeaderText="IDAlumno" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">
                            <HeaderStyle CssClass="ColumnaOculta" />
                            <ItemStyle CssClass="ColumnaOculta" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Documento" HeaderText="Documento" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">
                            <HeaderStyle CssClass="ColumnaOculta" />
                            <ItemStyle CssClass="ColumnaOculta" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Estatus" HeaderText="Estatus" />
                        <asp:TemplateField HeaderText="Especificaciones del documento">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Descripcion") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Documento") %>' Font-Bold="true"></asp:Label>.
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Comentarios" HeaderText="Comentarios de Revisión" Visible="false" />
                        <asp:BoundField DataField="Minimo" HeaderText="Minimo" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">
                            <HeaderStyle CssClass="ColumnaOculta" />
                            <ItemStyle CssClass="ColumnaOculta" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Maximo" HeaderText="Maximo" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">
                            <HeaderStyle CssClass="ColumnaOculta" />
                            <ItemStyle CssClass="ColumnaOculta" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Formato" HeaderText="Formato" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">
                            <HeaderStyle CssClass="ColumnaOculta" />
                            <ItemStyle CssClass="ColumnaOculta" />
                        </asp:BoundField>
                        <asp:TemplateField ShowHeader="True" HeaderText="Comentarios de Revisión">
                            <ItemTemplate>
                                <%--<a id="pop" href="InputFile.aspx"> <img border="0" src="Images/Carga_Documentos/Upload_n.png"></a>--%>
                                <asp:ImageButton ID="imgComentarios" runat="server" Height="24px"
                                    ImageUrl="~/Images/Carga_Documentos/comentarios.png" CommandName="Comentarios" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                    AlternateText="Subir Archivo" Visible="true" OnClientClick="CallButtonEvent()" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="True" HeaderText="Subir Documento">
                            <ItemTemplate>
                                <%--<a id="pop" href="InputFile.aspx"> <img border="0" src="Images/Carga_Documentos/Upload_n.png"></a>--%>
                                <asp:ImageButton ID="imgExpediente" runat="server" Height="24px"
                                    ImageUrl="~/Images/Carga_Documentos/Upload_n.png" CommandName="Subir" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                    AlternateText="Subir Archivo" Visible="true" OnClientClick="CallButtonEvent()" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="True" HeaderText="Ver Documento">
                            <ItemTemplate>
                                <%--<a id="pop" href="InputFile.aspx"> <img border="0" src="Images/Carga_Documentos/Upload_n.png"></a>--%>
                                <asp:ImageButton ID="imgPreview" runat="server" Height="24px"
                                    ImageUrl="~/Images/Carga_Documentos/preview.png" CommandName="Preview" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                    AlternateText="Vista Previa" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" CssClass="table-header_1" />
                </asp:GridView>
            </div>
            <div>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 50%">Descarga el formato para :"Autorización para validación de antecedentes académicos"</td>
                        <td><a href="Formatos/Autorizacion _Online.pdf" download>
                            <img alt="" src="Images/Carga_Documentos/PDF.png" /></a></td>
                    </tr>
                    <tr>
                        <td style="width: 50%">Descarga el formato :"Carta de Entrega de Documentos Oficiales"</td>
                        <td><a href="Formatos/Carta_Entrega_Documentos.pdf" download>
                            <img alt="" src="Images/Carga_Documentos/PDF.png" /></a></td>
                    </tr>
                    <tr>
                        <td style="width: 50%">Descarga el formato :"Carta de responsabilidad de usuario y contraseña"</td>
                        <td><a href="Formatos/Carta_Usuario_Contraseña.pdf" download>
                            <img alt="" src="Images/Carga_Documentos/PDF.png" /></a></td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="Server">
    <table width="100%" align="center" style="font-family: Raleway;">
        <tr valign="bottom">
            <td align="center">
                <asp:Label runat="server" ForeColor="#3c9ab6" Font-Bold="true" ID="lblBottomHelp">Si tienes alguna duda, comunícate con tu Asesor al 01 800 0011 852 opción 2</asp:Label>
            </td>
        </tr>
    </table>
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
    <script type="text/javascript"> 

        window.onload = function () {

            animateprogress("#progress_bar", 91);

        }
        document.querySelector('#boton').addEventListener('click', function () {
            animateprogress("#progress_bar", 91);

        });


    </script>
</asp:Content>

