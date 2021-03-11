<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Privacidad.aspx.cs" Inherits="Privacidad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="Content/Bootstrap/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/Bootstrap/bootstrap.js"></script>
    
    <link href="https://fonts.googleapis.com/css?family=Raleway" rel="stylesheet">
    <%--<script src="http://repositorio.ula.edu.mx/Repositorio_UAT/Scripts/Sweet_Alert/sweetalert2.min.js"></script>--%>
    <script src="Scripts/Sweet_Alert/sweetalert2.min.js"></script>
   <script>
        var Contenido = '<%=Aviso%>';
        function terms() {
            swal({
                title: 'Aviso de Privacidad',
                html: Contenido,
                input: 'checkbox',
                inputValue: 0,
                closeOnClickOutside: false,
                allowEscapeKey : false,
                allowOutsideClick: false,
                showCancelButton: true,
                inputPlaceholder:
                    'He leído y acepto los términos y condiciones de privacidad',
                confirmButtonText: 'Continuar',
                inputValidator: function (result) {
                    return !result && 'Debe aceptar los términos de privacidad antes de continuar.'

                }
            }).then(function (result) {
                if (result.value) {
                    window.location = "CargaDocumentos.aspx?Contrato=1";
                } else if (result.dismiss == 'cancel') { window.location = "Inicio.aspx"; }
                document.getElementById("acepto_priv").value = 1;
            });

        }
        
   </script>
    <script>
        
        function valida_alumno() {
            swal({
                title: 'Alumno no encontrado',
                html:'No fue posible encontrar tus documentos en el sistema.</br> Te invitamos a notificar el problema a la <a href="mailto:soporte@ula.edu.mx?Subject=Portal%20de%20Documentos-Alumno%20no%20registrado%20en%20BD"> Mesa de Ayuda</a> para que puedas cargar tus documentos.',
                type: 'info',
            }).then(willDelete => {
                    if (willDelete) {
                        window.location = "Inicio.aspx";
                        //swal("Deleted!", "Your imaginary file has been deleted!", "success");
                    }
                });
        }
    </script>
    <style>
        .swal2-popup{
            width:60% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" Runat="Server">
</asp:Content>

