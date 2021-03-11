<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InputFile.aspx.cs" Inherits="InputFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="Content/Bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Bootstrap/bootstrap2.min.css" rel="stylesheet" />
    <link href="Content/FileInput/fileinput.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">
    <script type="" src="https://code.jquery.com/jquery-3.3.1.min.js" crossorigin="anonymous"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/4.5.1/js/plugins/piexif.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/4.5.1/js/plugins/sortable.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/4.5.1/js/plugins/purify.min.js" type="text/javascript"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.bundle.min.js" crossorigin="anonymous"></script>
    <script src="http://netdna.bootstrapcdn.com/bootstrap/3.1.0/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/4.5.1/js/fileinput.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/4.5.1/themes/fas/theme.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-fileinput/4.5.1/js/locales/es.js"></script>
    <link href="https://fonts.googleapis.com/css?family=Raleway" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">

        <div>
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Diseno_Nuevo/pleca_verde.png" Width="100%"/></div>
<%--        <div style="font-size: xx-large; text-align: center;">
            Cargar Documentos
        </div>--%>
        <div>
            <input id="File1" name="imagenes[]" type="file" class="file-loading btn-file input" accept="<%=formato%>" />
        </div>
        <div id="errorBlock" class="help-block"></div>
        <div>
            <br />
            <br />
            <br />
        </div>
    </form>
</body>
<script>

    $("#File1").fileinput({
        uploadUrl: "UploadPage.aspx?IDTipoDocumento=<%=IDTipoDocumento%>&IDDocumento=<%=IDDocumento%>&IDAlumno=<%=IDAlumno%>",
        uploadAsync: true,
        language: "es",
        overwriteInitial: true,
        showCancel: false,
        showClose: false,
        fileActionSettings: {
            removeIcon: '<i class="fas fa-trash-alt"></i>',
            uploadIcon: '<i class="fas fa-upload"></i>',
            uploadRetryIcon: '<i class="fas fa-redo-alt"></i>',
            downloadIcon: '<i class="fas fa-download"></i>',
            zoomIcon: '<i class="fas fa-search-plus"></i>',
            dragIcon: '<i class="fas fa-arrows-alt"></i>',
            indicatorNew: '<i class="fas fa-plus-circle text-warning"></i>',
            indicatorSuccess: '<i class="fas fa-check-circle text-success"></i>',
            indicatorError: '<i class="fas fa-exclamation-circle text-danger"></i>',
            indicatorLoading: '<i class="fas fa-hourglass text-muted"></i>'
        },
        layoutTemplates: {
            fileIcon: '<i class="fas fa-file kv-caption-icon"></i> ',
            actions: '<div class="file-actions">\n' +
                '    <div class="file-footer-buttons">\n' +
                '         {delete} {zoom}' +
                '    </div>\n' +
                '    {drag}\n' +
                '    <div class="clearfix"></div>\n' +
                '</div>',
        },
        previewZoomButtonIcons: {
            prev: '<i class="fas fa-caret-left fa-lg"></i>',
            next: '<i class="fas fa-caret-right fa-lg"></i>',
            toggleheader: '<i class="fas fa-fw fa-arrows-alt-v"></i>',
            fullscreen: '<i class="fas fa-fw fa-arrows-alt"></i>',
            borderless: '<i class="fas fa-fw fa-external-link-alt"></i>',
            close: '<i class="fas fa-fw fa-times"></i>'
        },
        previewFileIcon: '<i class="fas fa-file"></i>',

        browseIcon: "<i class=\"fas fa-folder-open\"></i> ",
        removeClass: "btn btn-danger",
        removeIcon: "<i class=\"fas fa-trash-alt\"></i> ",
        uploadIcon: "<i class=\"fas fa-file-upload\"></i> ",
        msgValidationErrorIcon: "<i class=\"fas fa-exclamation-circle\"></i>",
        initialPreviewAsData: true,
        //initialPreview: ["http://lorempixel.com/800/460/city/1/"],
        browseOnZoneClick: true,
        //maxFileSize: 100,
        allowedFileExtensions: [<%=formato%>],
        maxFileSize: "<%=tamano_max%>",
        minFileSize: "<%=tamano_min%>",
        elErrorContainer: "#errorBlock"
    });
</script>
</html>
