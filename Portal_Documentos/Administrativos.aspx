<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Administrativos.aspx.cs" Inherits="Administrativos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Acceso a Sistema</title>
    <link href="Content/Bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="Content/Sweet_Alert/sweetalert.css" rel="stylesheet" />
    <script src="Scripts/Sweet_Alert/sweetalert.js"></script>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">
    <script src="Scripts/Bootstrap/bootstrap.js"></script>
    <link href="https://fonts.googleapis.com/css?family=Raleway" rel="stylesheet">
    <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />
    <style type="text/css">
        .auto-style1 {
            margin-top: 30px;
        }

        .auto-style2 {
            height: 26px;
        }

        .auto-style3 {
            height: 647px;
            width: 544px;
        }

        .auto-style4 {
            width: 80%;
            margin-top: 10px;
        }
        .tabla_img {
            width: 10%;
            align-content:center;
        }
        .tabla_img2 {
            width: 25%;
        }
    </style>
    <script>
        function no_existe(){
            swal({
                //title: 'Documentos Existentes',
                //text: "",
                //type: 'info',
                html:'<h2 class="swal2-title" id="swal2-title"><img src="images/icono_sesion_alert.png" style="width: 100px;"><br><br>Usuario no existe</h2>Su usuario no esta dado de alta en el sistema, favor de contactar al Administrador'
                //showCancelButton: false,
                //cancelButtonText: 'No',
                //confirmButtonText: 'Ok'
            })
        }

        function datos(){
            swal({
                //title: 'Documentos Existentes',
                //text: "",
                //type: 'info',
                html:'<h2 class="swal2-title" id="swal2-title"><img src="images/icono_sesion_alert.png" style="width: 100px;"><br><br>Usuario y/o Contraseña incorrecta</h2>Favor de validar los datos ingresados'
                //showCancelButton: false,
                //cancelButtonText: 'No',
                //confirmButtonText: 'Ok'
            })
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <center>
        <div style="background-color:#e7ecee; " class="auto-style4"> 
            <br />
            <br />
            <br />
            <center class="auto-style1">
                
                <table class="auto-style3" >
                    <tr>
                        <td class="auto-style2" style="background-image:url('Images/Diseno_Nuevo/Acceso.jpg'); background-repeat:no-repeat; width:100%">
                            <br />
                            <br />
                           
                        <table style="width:100%;">
                            <tr>
                                <td class="tabla_img2">&nbsp;</td>
                                <td class="tabla_img"><div class="input-group-text"><asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Administrativos/usuario.png" ImageAlign="Middle"/></div></td>
                                <td><asp:TextBox ID="txtUsuario" runat="server" Width="100%" CssClass="form-control"></asp:TextBox></td>
                                <td class="tabla_img2">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td class="tabla_img"> <div class="input-group-text"> <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Administrativos/bloqueado.png" ImageAlign="Middle"/></div> </td>
                                <td> <asp:TextBox ID="txtPassword" runat="server" Width="100%" CssClass="form-control" TextMode="Password"></asp:TextBox> </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td align="center" colspan="2"><asp:Button ID="cmdEntrar" runat="server" Text="Entrar" onclick="cmdEntrar_Click" CssClass="btn" Width="100%" BackColor="#e9ecef"/></td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                           
                        </td>
                    </tr>
                </table>
            </center>
            <br />
            <br />
            <br />
            <br />
        </div>
            </center>
    </form>
</body>
</html>
