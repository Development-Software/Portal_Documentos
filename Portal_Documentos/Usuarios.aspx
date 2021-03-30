<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Usuarios.aspx.cs" Inherits="Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="Content/Sweet_Alert/sweetalert.css" rel="stylesheet" />
    <link href="Content/Bootstrap/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/Bootstrap/bootstrap.js"></script>
    <script src="Scripts/Sweet_Alert/sweetalert.js"></script>
    <link href="https://fonts.googleapis.com/css?family=Raleway" rel="stylesheet">
    <script>
        function Agrega_usuario() {
            swal("Usuario agregado exitosamente", "", "success")
                .then(willDelete => {
                    if (willDelete) {
                        window.location = "Usuarios.aspx";
                    }
                });
        }
        function Actualiza_usuario() {
            swal("Usuario actualizado exitosamente", "", "success")
                .then(willDelete => {
                    if (willDelete) {
                        window.location = "Usuarios.aspx";
                    }
                });
        }
        function Elimina_usuario() {
            swal("Usuario eliminado exitosamente", "", "success")
                .then(willDelete => {
                    if (willDelete) {
                        window.location = "Usuarios.aspx";
                    }
                });
        }
    </script>
    <style>
        .oculto{
            display:none;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ID="Contenido" ContentPlaceHolderID="MainContent">
    <%--<center><h2>Mantenimiento de Usuarios</h2></center><br />--%>
    <div id="contenido" >
    <div id="agregar" style="margin-top:50px; text-align:right;"><asp:Button ID="add_user" runat="server" Text="Agregar Usuario" CssClass="btn btn-logout" OnClick="add_user_Click"/></div>
    <div id="table_users" style="margin-top:100px;">
    <div id="add_user_1">
    <asp:Table ID="tabla_add" runat="server" HorizontalAlign="Center">
        <asp:TableRow>
            <asp:TableCell Width="100px" HorizontalAlign="Center">USUARIO</asp:TableCell>
            <asp:TableCell Width="200px" HorizontalAlign="Center">CONTRASEÑA</asp:TableCell>
            <asp:TableCell Width="300px" HorizontalAlign="Center">NOMBRE</asp:TableCell>
            <asp:TableCell Width="300px" HorizontalAlign="Center">CORREO</asp:TableCell>
            <asp:TableCell Width="150px" HorizontalAlign="Center">ROL</asp:TableCell>
            
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell><asp:TextBox ID="Txtuser" runat="server" Visible="True" WIDTH="100px" AutoPostBack="false" CssClass="form-control" Height="30px"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox ID="Textpass" runat="server" Visible="True" WIDTH="200px" AUTOPOSTBACK="false" CssClass="form-control" Height="30px" TextMode="Password"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox ID="TxtNombre" runat="server" Visible="True" WIDTH="300px" AUTOPOSTBACK="false" CssClass="form-control" Height="30px"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox ID="Txtcorreo" runat="server" Visible="True" WIDTH="300px" AUTOPOSTBACK="false" CssClass="form-control" Height="30px"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:dropdownlist ID="CboRoles" RUNAT="server" WIDTH="150px" AutoPostBack="false"  CssClass="form-control" Height="30px" Font-Size="10px"></asp:dropdownlist></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
                <div id="acciones" runat="server" style="text-align:center;" visible="false">
            <asp:ImageButton ID="Agregar_cmd" runat="server" ImageUrl="~/Images/Usuarios/add.png" Height="30px" Width="30px"
                                  TOOLTIP="Guardar" IMAGEALIGN="Middle" OnClick="Agregar_Click"/>
            <asp:ImageButton ID="update" runat="server" ImageUrl="~/Images/Usuarios/update.png" Height="30px" Width="30px"
                                  TOOLTIP="Actualizar" IMAGEALIGN="Middle" OnClick="Update_Click" Visible="False"/>
            <asp:ImageButton ID="delete" runat="server" ImageUrl="~/Images/Usuarios/trash.png" Height="30px" Width="30px"
                                  TOOLTIP="Eliminar" IMAGEALIGN="Middle" OnClick="Borrar_Click" Visible="False" />
            <asp:ImageButton ID="cancel" runat="server" ImageUrl="~/Images/Usuarios/cancel.png" Height="30px" Width="30px"
                                  TOOLTIP="Cancelar" IMAGEALIGN="Middle" OnClick="cancel_Click" Visible="False"/>
        </div>
    </div>
                    </br>
                    <%--<table id="t_1" runat="server" align="center" class="table-responsive" width="1000px" border="1"></table>--%>
                     <table id="t_2" runat="server" align="center" width="100%" cellpadding="1" cellspacing="1">
                        <tr>
                                <td colspan="2" align="center">
                                    
                                    <%--<ASP:LABEL ID="LabelEgresados" runat="server" 
                                        Text="Listado de Usuarios" style="font-weight: 700; font-size: large"></ASP:LABEL>--%>
                                </td>
                        </tr>
                            </table>
                                    <%--<table id="t_3" runat="server" align="center" class="table-responsive" width="1000px" border="1"></table>--%>
                        <table align="center" width="100%" cellpadding="1" cellspacing="1">

                        <tr>
                                <td colspan="2" align="center">
                                    
                                </td>
                        </tr>
                        <tr>
                                <td colspan="2" align="center">
                                    <ASP:GRIDVIEW ID="Users" RUNAT="server"
                                            autogeneratecolumns="False"
                                            ROWSTYLE-FONT-SIZE="Small"
                                            onselectedindexchanged="GridUsuarios_Click" Width="100%" CssClass="table table-bordered" HeaderStyle-CssClass="table-header_1">
                                            <Columns>
                                                 <asp:TemplateField ShowHeader="False">
                                                     <ItemTemplate>
                                                         <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="Select" ImageUrl="Images/Usuarios/lapiz.png" Text="" />
                                                     </ItemTemplate>
                                                     <ControlStyle Height="20px" Width="20px" />
                                                 </asp:TemplateField>
                                                 <asp:BoundField DataField="Login" 
                                                     HeaderText="Usuario" 
                                                     SortExpression="Nombre" />
                                                <asp:BoundField DataField="Nombre" 
                                                     HeaderText="Nombre" 
                                                     SortExpression="nombre" />
                                                <asp:BoundField DataField="Rol" 
                                                     HeaderText="Rol" 
                                                     SortExpression="Rol" />
                                                <asp:BoundField DataField="Email" 
                                                     HeaderText="Correo" 
                                                     SortExpression="Email" />
                                                <asp:BoundField DataField="FechaUltModif" 
                                                     HeaderText="Fecha Registro" 
                                                     SortExpression="FechaUltModif" />
                                            </Columns>
                                            <selectedrowstyle 
                                             forecolor="Black"
                                             font-bold="true" CssClass="selected" /> 
                                            <HEADERSTYLE FORECOLOR="White" />
                                            <ROWSTYLE WIDTH="200px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </ASP:GRIDVIEW>
                                    
                                </td>
                        </tr>
                    </table>
        </div></div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" Runat="Server">
    <%--<h3>Session Idle:&nbsp;<span id="secondsIdle"></span>&nbsp;seconds.</h3>--%>
        <link href="Content/Bootstrap/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/Sweet_Alert/sweetalert.js"></script>
    <link href="Content/Sweet_Alert/sweetalert.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

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

