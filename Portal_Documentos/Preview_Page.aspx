<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Preview_Page.aspx.cs" Inherits="Preview_Page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="Content/Bootstrap/bootstrap.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js" type="text/javascript"></script>
    <link href="https://fonts.googleapis.com/css?family=Raleway" rel="stylesheet">
    <title></title>
    <script>
        function slide_down() {
            $("#revision_doc_panel").slideDown("slow");
            $("#guardar").slideDown("slow");
            document.getElementsByTagName("p")[0].setAttribute("style", "display:none");
            var myElement = document.querySelector("#revision_doc_panel");
            myElement.style.display = "block";
            $("#alert").hide();
            $("#success").hide();
            $("#warning1").hide();
            $("#estatus").hide();
            return false;
        }
        function slide_up() {
            $("#revision_doc_panel").slideUp("fast");
            $("#guardar").slideUp("fast");
            document.getElementsByTagName("p")[0].setAttribute("style", "display:block");
            $("#alert").hide();
            $("#success").hide();
            $("#warning1").hide();
            $("#estatus").hide();
            return false;
        }
        function slide_up_save() {
            $("#revision_doc_panel").slideUp("slow");
            $("#guardar").slideUp("slow");
            document.getElementsByTagName("p")[0].setAttribute("style", "display:block");
            $("#alert").hide();
            $("#success").show();
            $("#warning1").hide();
            return false;
        }
        function slide_down_alert() {
            document.getElementsByTagName("p")[0].setAttribute("style", "display:none");
            $("#revision_doc_panel").slideDown("slow");
            $("#guardar").slideDown("slow");
            $("#alert").slideDown("slow");
            $("#warning1").hide();
            return false;
        }
        function slide_up_save_acept() {
            $("#revision_doc_panel").slideUp("slow");
            $("#guardar").slideUp("slow");
            document.getElementsByTagName("p")[0].setAttribute("style", "display:block");
            $("#alert").hide();
            $("#acept").show();
            $("#warning1").hide();
            return false;
        }
        function slide_up_save_acept_1() {
            $("#revision_doc_panel").slideUp("slow");
            $("#guardar").slideUp("slow");
            document.getElementsByTagName("p")[0].setAttribute("style", "display:block");
            $("#alert").hide();
            $("#warning1").show();

            return false;
        }
        function slide_up_invalido() {
            $("#revision_doc_panel").slideUp("slow");
            $("#guardar").slideUp("slow");
            document.getElementsByTagName("p")[0].setAttribute("style", "display:block");
            $("#alert").hide();
            $("#warning1").hide();
            $("#invalido").show();
            return false;
        }
        function slide_down_estatus() {
            $("#estatus").show();
            return false;
        }
    </script>
    <style>
        #revision_doc_panel {
            display: none;
        }

        #guardar {
            display: none;
        }

        #alert, #alert1 {
            display: none;
        }

        #success {
            display: none;
        }

        #acept {
            display: none;
        }

        #warning1 {
            display: none;
        }

        #invalido {
            display: none;
        }

        #estatus {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel_top" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <center><%=ruta_tmp %></center>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <br />


        <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <center><p>
                    <asp:Button ID="RevisaDoc" runat="server" Text="Revisar Documento" CssClass="btn btn-secondary" OnClientClick="slide_down(); return false"></asp:Button>
                    <%--<input id="RevisaDoc" type="button" value="Revisar Documento" onclick="slide_down(); return false" class="btn btn-secondary" />--%>
                    
            </p>
                </center>

                <div id="revision_doc_panel" style="background-color: aliceblue; border: 1px dashed;">
                    <table style="width: 100%;">
                        <tr style="text-align: center">
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Asignar Estatus"></asp:Label></td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Comentarios"></asp:Label></td>
                        </tr>
                        <tr style="text-align: center">
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DropDownEstatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownEstatus_SelectedIndexChanged"></asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="DropDownEstatus" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
                        </tr>
                    </table>

                </div>
                <%--</ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="updatepanel2" runat="server">
            <ContentTemplate>--%>
                <div class="alert alert-danger" role="alert" id="alert">
                    <i class="fas fa-exclamation"></i>Es necesario seleccionar un estatus para poder continuar
                </div>
                <div class="alert alert-success" role="alert" id="success">
                    <i class="far fa-check-circle"></i><b>Cambios guardados exitosamente</b>.
                </div>
                <div class="alert alert-success" role="alert" id="acept">
                    <i class="far fa-check-circle"></i><b>Cambios guardados.</b><br />
                    El documento y sus comentarios han sido registrado en Banner exitosamente.
                </div>
                <div class="alert alert-primary" role="alert" id="estatus">
                    <i class="fas fa-info-circle"></i>El estatus actual del documento es "Aceptado", si cambias su estatus, el registro se actualizara en Banner.
                </div>

                <div class="alert alert-danger" role="alert" id="warning1">
                    <i class="fas fa-exclamation"></i><b>Error al actualizar estatus.</b><br />
                    El estatus del documento no fue posible registrarlo ya que el documento no esta cargado en Banner correctamente. 
                </div>
                <div class="alert alert-warning" role="alert" id="invalido">
                    <i class="fas fa-exclamation"></i><b>Cambios guardados.</b><br />
                    El estatus del documento fue actualizado y se integro una retención en Banner para la inscripción. 
                </div>

                <div id="guardar">
                    <center><asp:Button ID="Guarda_Rev" runat="server" Text="Guardar" CssClass="btn btn-logout" OnClick="Guarda_Rev_Click"/>&nbsp;<asp:Button ID="Button1" runat="server" Text="Cancelar" OnClientClick="slide_up(); return false" CssClass="btn btn-secondary"/></center>
                </div> 
                <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel">
                            <ProgressTemplate>
                                <div>
                                    <img src="Images/loader_puntos.gif" width="5%" style="position: fixed; margin-left: -75px;" />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Guarda_Rev" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
