<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ListadoAdministracion.aspx.cs" Inherits="ListadoAdministracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="Content/Bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="Content/Otros/paginacion.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">
    <link href="https://fonts.googleapis.com/css?family=Raleway" rel="stylesheet">
    <style>
        .Oculto{
            display:none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <br />
    <br />
    <%--<center><h2>Listado de Alumnos</h2></center>--%>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div style="float:left" >
                <i class="fas fa-search" style="font-size: 25px;vertical-align: -webkit-baseline-middle;"></i></div>
                <div><asp:TextBox ID="search" runat="server" OnTextChanged="search_TextChanged" AutoPostBack="true" Width="30%" CssClass="form-control"></asp:TextBox></div>
            <asp:TextBox ID="TextBox1" runat="server" CssClass="Oculto"></asp:TextBox>
            <asp:TextBox ID="TextBox2" runat="server" CssClass="Oculto"></asp:TextBox>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:GridView ID="gvAlumnos" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" OnRowCommand="gvAlumnos_RowCommand" Visible="true" ShowHeaderWhenEmpty="True" DataKeyNames="IDAlumno" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvAlumnos_PageIndexChanging" CssClass="table table-bordered">
                <Columns>
                    <asp:BoundField DataField="IDAlumno" HeaderText="Matrícula" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Campus" HeaderText="Campus" />
                    <asp:BoundField DataField="Nivel" HeaderText="Nivel" />
                    <asp:BoundField DataField="Programa" HeaderText="Programa" />
                    <asp:BoundField DataField="Estatus_Alumno" HeaderText="Estatus Alumno" />
                    <asp:BoundField DataField="Estatus_Expediente" HeaderText="Estatus Expediente" />
                    <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />
                    <asp:BoundField DataField="FechaUltNotificacion" HeaderText="Fecha Notificación" ItemStyle-CssClass="Oculto" HeaderStyle-CssClass="Oculto"/>
                    <asp:BoundField DataField="NoAlerta" HeaderText="No. Alerta" ItemStyle-CssClass="Oculto" HeaderStyle-CssClass="Oculto"/>
                    <asp:BoundField DataField="Modalidad" HeaderText="Modalidad" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgExpediente" runat="server" AlternateText="Expediente"
                                CommandName="Expediente" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' ImageUrl="~/Images/Listado_Administracion/Expediente.png" Height="52px" Width="52px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle HorizontalAlign="Center" CssClass="table-header_1"></HeaderStyle>

                <PagerSettings Mode="NumericFirstLast" FirstPageText="Primero" LastPageText="Último" PageButtonCount="4" />
                <PagerStyle CssClass="pagination-ys" BorderColor="White" HorizontalAlign="Center" />

            </asp:GridView>  
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:CheckBox ID="chkSoloCompletos" runat="server"
        Text="Mostrar solo expedientes Completos"
        OnCheckedChanged="chkSoloCompletos_CheckedChanged" AutoPostBack="true" />
    <br />
  
    <asp:Table ID="exportar" runat="server" HorizontalAlign="Center">
<%--        <asp:TableRow HorizontalAlign="Center">
            <asp:TableCell>
                <asp:DropDownList ID="Exportar_Datos" runat="server" OnSelectedIndexChanged="Exportar_Datos_SelectedIndexChanged">
                    <asp:ListItem Value="0">--- Seleccionar ---</asp:ListItem>
                    <asp:ListItem Value="1">PDF</asp:ListItem>
                    <asp:ListItem Value="2">Excel</asp:ListItem>
                </asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>--%>
        <asp:TableRow HorizontalAlign="Center">
            <asp:TableCell>
                <asp:Button ID="cmdExportar" runat="server" Text="Exportar" CssClass="btn btn-logout"
                    OnClick="cmdExportar_Click" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <br />

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" Visible="true" ShowHeaderWhenEmpty="True" DataKeyNames="IDAlumno"  CssClass="Oculto">
                <Columns>
                    <asp:BoundField DataField="IDAlumno" HeaderText="Matrícula" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Campus" HeaderText="Campus" />
                    <asp:BoundField DataField="Nivel" HeaderText="Nivel" />
                    <asp:BoundField DataField="Programa" HeaderText="Programa" />
                    <asp:BoundField DataField="Estatus_Alumno" HeaderText="Estatus Alumno" />
                    <asp:BoundField DataField="Estatus_Expediente" HeaderText="Estatus Expediente" />
                    <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />
                    <asp:BoundField DataField="FechaUltNotificacion" HeaderText="Fecha Notificación" ItemStyle-CssClass="Oculto" HeaderStyle-CssClass="Oculto"/>
                    <asp:BoundField DataField="NoAlerta" HeaderText="No. Alerta" ItemStyle-CssClass="Oculto" HeaderStyle-CssClass="Oculto"/>
                    <asp:BoundField DataField="Modalidad" HeaderText="Modalidad" />
                    <%--<asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgExpediente" runat="server" AlternateText="Expediente"
                                CommandName="Expediente" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' ImageUrl="~/Images/Listado_Administracion/Expediente.png" Height="52px" Width="52px" />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
                <HeaderStyle HorizontalAlign="Center" CssClass="table-header_1"></HeaderStyle>

<%--                <PagerSettings Mode="NumericFirstLast" FirstPageText="Primero" LastPageText="Último" PageButtonCount="4" />
                <PagerStyle CssClass="pagination-ys" BorderColor="White" HorizontalAlign="Center" />--%>

            </asp:GridView>  
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="Server">
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

