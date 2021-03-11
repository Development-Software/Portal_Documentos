<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Permisos.aspx.cs" Inherits="Permisos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="Content/Bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="Content/Sweet_Alert/sweetalert.css" rel="stylesheet" />
    <script src="Scripts/Bootstrap/bootstrap.js"></script>
    <script src="Scripts/Sweet_Alert/sweetalert.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js" type="text/javascript"></script>
    <link href="https://fonts.googleapis.com/css?family=Raleway" rel="stylesheet">
    <script type="text/javascript">

        /*boton 1 sube*/
        function slide_up() {
            $("#panel").slideUp("slow");
            $("#flecha_down").show();
            $("#flecha_up").hide();
            $("#lstccampus").empty();

            return false;
        }
        /*boton 1 baja*/
        function slide_down() {
            $("#panel").slideDown("slow");
            $("#flecha_down").hide();
            $("#flecha_up").show();
            $("#panel_1").slideUp("slow");
            $("#flecha_down_1").show();
            $("#flecha_up_1").hide();
            $("#panel_2").slideUp("slow");
            $("#flecha_down_2").show();
            $("#flecha_up_2").hide();
            $("#privilegios_table").slideUp("slow");
            $("#save_button").slideUp("slow");
            $("#bloqueado").show();
            $("#desbloqueado").hide();
            return false;
        }
        /*boton 2 sube*/
        function slide_up_1() {
            $("#panel_1").slideUp("slow");
            $("#flecha_down_1").show();
            $("#flecha_up_1").hide();

            return false;
        }
        /*boton 2 baja*/
        function slide_down_1() {
            $("#panel_1").slideDown("slow");
            $("#flecha_down_1").hide();
            $("#flecha_up_1").show();
            $("#panel").slideUp("slow");
            $("#flecha_down").show();
            $("#flecha_up").hide();
            $("#panel_2").slideUp("slow");
            $("#flecha_down_2").show();
            $("#flecha_up_2").hide();
            $("#privilegios_table").slideUp("slow");
            $("#save_button").slideUp("slow");
            $("#bloqueado").show();
            $("#desbloqueado").hide();
            return false;
        }
        /*boton 3 sube*/
        function slide_up_2() {
            $("#panel_2").slideUp("slow");
            $("#flecha_down_2").show();
            $("#flecha_up_2").hide();
            //location.reload();
            return false;
        }
        /*boton 3 baja*/
        function slide_down_2() {
            $("#panel_2").slideDown("slow");
            $("#flecha_down_2").hide();
            $("#flecha_up_2").show();
            $("#panel").slideUp("slow");
            $("#flecha_down").show();
            $("#flecha_up").hide();
            $("#panel_1").slideUp("slow");
            $("#flecha_down_1").show();
            $("#flecha_up_1").hide();
            $("#privilegios_table").slideUp("slow");
            $("#save_button").slideUp("slow");
            $("#bloqueado").show();
            $("#desbloqueado").hide();
            return false;
        }
        /*boton 4 sube*/
        function slide_up_4() {
            $("#privilegios_table").slideUp("slow");
            $("#save_button").slideUp("slow");
            $("#bloqueado").show();
            $("#desbloqueado").hide();
            $("#lbl_permisos_2").hide();
            $("#lbl_permisos_1").show();
            //location.reload();
            return false;
        }
        /*Boton 4 baja*/
        function slide_down_4() {
            $("#privilegios_table").slideDown("slow");
            $("#save_button").slideDown("slow");
            $("#desbloqueado").show();
            $("#lbl_permisos_2").show();
            $("#lbl_permisos_1").hide();
            $("#bloqueado").hide();
            $("#panel_2").slideUp("slow");
            $("#flecha_down_2").show();
            $("#flecha_up_2").hide();
            $("#panel").slideUp("slow");
            $("#flecha_down").show();
            $("#flecha_up").hide();
            $("#panel_1").slideUp("slow");
            $("#flecha_down_1").show();
            $("#flecha_up_1").hide();
            return false;
        }
        /*boton 5 sube*/
        function slide_up_e() {
            $("#Panel_estatus").slideUp("slow");
            $("#flecha_down_3").show();
            $("#flecha_up_3").hide();
            $("#ListDoc").empty();

            return false;
        }

        /* etiqueta 1 sube*/
        function slide_up_lbl() {
            $("#panel").slideUp("slow");
            $("#flecha_down").show();
            $("#flecha_up").hide();
            $("#lbl_add_camp").show();
            $("#lbl_add_camp_1").hide();
            return false;
        }
        /* etiqueta 2 baja*/
        function slide_down_lbl_n() {
            $("#panel_1").slideDown("slow");
            $("#flecha_down_1").hide();
            $("#flecha_up_1").show();
            $("#lbl_add_nivel").hide();
            $("#lbl_add_nivel_1").show();
            $("#panel").slideUp("slow");
            $("#flecha_down").show();
            $("#flecha_up").hide();
            $("#panel_2").slideUp("slow");
            $("#flecha_down_2").show();
            $("#flecha_up_2").hide();
            $("#privilegios_table").slideUp("slow");
            $("#save_button").slideUp("slow");
            $("#bloqueado").show();
            $("#desbloqueado").hide();
            return false;
        }
        /* etiqueta 2 sube*/
        function slide_up_lbl_n() {
            $("#panel_1").slideUp("slow");
            $("#flecha_down_1").show();
            $("#flecha_up_1").hide();
            $("#lbl_add_nivel").show();
            $("#lbl_add_nivel_1").hide();
            return false;
        }
        /* etiqueta 3 baja*/
        function slide_down_lbl_d() {
            $("#panel_2").slideDown("slow");
            $("#flecha_down_2").hide();
            $("#flecha_up_2").show();
            $("#lbl_add_doc").hide();
            $("#lbl_add_doc_1").show();
            $("#panel").slideUp("slow");
            $("#flecha_down").show();
            $("#flecha_up").hide();
            $("#panel_1").slideUp("slow");
            $("#flecha_down_1").show();
            $("#flecha_up_1").hide();
            $("#privilegios_table").slideUp("slow");
            $("#save_button").slideUp("slow");
            $("#bloqueado").show();
            $("#desbloqueado").hide();
            return false;
        }
        /* etiqueta 3 sube*/
        function slide_up_lbl_d() {
            $("#panel_2").slideUp("slow");
            $("#flecha_down_2").show();
            $("#flecha_up_2").hide();
            $("#lbl_add_doc").show();
            $("#lbl_add_doc_1").hide();
            return false;
        }

        /* etiqueta 4 sube*/
        function slide_up_lbl_p() {
            $("#privilegios_table").slideUp("slow");
            $("#save_button").slideUp("slow");
            $("#bloqueado").show();
            $("#desbloqueado").hide();
            $("#lbl_permisos_2").hide();
            $("#lbl_permisos_1").show();
            //location.reload();
            return false;
        }
        /* etiqueta 4 baja*/
        function slide_down_lbl_p() {
            $("#privilegios_table").slideDown("slow");
            $("#save_button").slideDown("slow");
            $("#desbloqueado").show();
            $("#lbl_permisos_2").show();
            $("#lbl_permisos_1").hide();
            $("#bloqueado").hide();
            $("#panel_2").slideUp("slow");
            $("#flecha_down_2").show();
            $("#flecha_up_2").hide();
            $("#panel").slideUp("slow");
            $("#flecha_down").show();
            $("#flecha_up").hide();
            $("#panel_1").slideUp("slow");
            $("#flecha_down_1").show();
            $("#flecha_up_1").hide();
            return false;
        }
        
        function slide_down_fast() {
            $("#panel").slideDown("fast");
            $("#flecha_down").hide();
            $("#flecha_up").show();
            return false;
        }
        function slide_down_fast_n() {
            $("#panel_1").slideDown("fast");
            $("#flecha_down_1").hide();
            $("#flecha_up_1").show();
            return false;
        }
        function slide_down_fast_d() {
            $("#panel_2").slideDown("fast");
            $("#flecha_down_2").hide();
            $("#flecha_up_2").show();
            return false;
        }
        function hideRow(row, gv) {
            Row_num = row;
            rows = document.getElementById(gv).rows;
            rows[Row_num].style.display = 'none';
        }
        function ShowRow(row, gv) {
            Row_num = row;
            rows = document.getElementById(gv).rows;
            rows[Row_num].style.display = 'block'
        }
        function guardar_permisos() {
            swal("Permisos modificados exitosamente", "Los permisos del perifl han sido modificados exitosamente.", "success")
                .then(willDelete => {
                    if (willDelete) {
                        window.location = "Permisos.aspx";
                        //swal("Deleted!", "Your imaginary file has been deleted!", "success");
                    }
                });
        }
    </script>

    <style>
        .title{
            font-weight:bold;
        }
        #lbl_permisos_2 {
            display: none;
        }

        #panel {
            padding: 50px;
            display: none;
        }

        #flecha_up {
            display: none;
        }

        .auto-style1 {
            width: 26px;
            height: 20px;
        }

        #panel_1 {
            padding: 50px;
            display: none;
        }

        #flecha_up_1 {
            display: none;
        }

        #panel_2 {
            padding: 50px;
            display: none;
        }

        #flecha_up_2 {
            display: none;
        }

        #lbl_add_camp_1 {
            display: none;
        }

        #lbl_add_nivel_1 {
            display: none;
        }

        #lbl_add_doc_1 {
            display: none;
        }

        #lbl_add_estatus_1 {
            display: none;
        }

        .auto-style2 {
            height: 28px;
        }

        .ul_submenu {
            list-style-type: disc;
            margin-block-start: 1em;
            margin-block-end: 1em;
            margin-inline-start: 0px;
            margin-inline-end: 0px;
            padding-inline-start: 40px;
            margin-top: 0;
            margin-bottom: 0rem;
        }

        .ul_permiso {
            list-style-type: disc;
            margin-block-start: 1em;
            margin-block-end: 1em;
            margin-inline-start: 0px;
            margin-inline-end: 0px;
            padding-inline-start: 80px;
            margin-top: 0;
            margin-bottom: 0rem;
        }

        .table-mod_2 {
            color: #fff;
            width: 10px;
            border-right-style: hidden;
        }

        .ex3 {
            width: 100%;
            height: 300px;
            overflow: auto;
        }
        /* The switch - the box around the slider */
        .switch {
            position: relative;
            display: inline-block;
            width: 60px;
            height: 34px;
        }

            /* Hide default HTML checkbox */
            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        /* The slider */
        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 26px;
                width: 26px;
                left: 4px;
                bottom: 4px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #16fca9;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #16fca9;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
            }

        .ColumnaOculta {
            display: none;
        }

        #privilegios_table {
            display: none;
        }

        #save_button {
            display: none;
        }

        #desbloqueado {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent" ID="Contenido">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <center><h2>Seleccionar un rol para editar</h2></center>
    <br />
    <table style="margin-left: auto; margin-right: auto; font-size: 15px">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Rol:"></asp:Label>
            </td>
            <td>


                <asp:DropDownList ID="ddlRol" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlRol_SelectedIndexChanged" CssClass="form-control">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <div id="Permisos_1" runat="server" style="font-size: 15px">
        <asp:UpdatePanel ID="combos" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="UpdatePanel" runat="server">
                    <table>
                        <tr>
                            <td>

                                <input id="flecha_down" type="image" value="button" onclick="slide_down(); return false" src="Images/Permisos/flecha_rigth.png" class="auto-style1" />
                                <input id="flecha_up" type="image" value="button" onclick="slide_up(); return false" src="Images/Permisos/Flecha_down.png" class="auto-style1" />
                            </td>
                            <td>
                                <%--<asp:LinkButton ID="add_camp" runat="server" OnClientClick="slide_down_lbl(); return false">Agregar Campus</asp:LinkButton>--%>
                                <label id="lbl_add_camp" onclick="slide_down_lbl(); return false">Agregar Campus</label>
                                <label id="lbl_add_camp_1" onclick="slide_up_lbl(); return false">Agregar Campus</label>
                            </td>
                        </tr>
                    </table>
                    <table id="t_3" runat="server" align="center" class="table-responsive" width="100%" border="1"></table>



                    <div id="panel">

                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table style="margin-left: auto; margin-right: auto">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblCampus" runat="server" Text="Lista de Campus"></asp:Label>
                                        </td>
                                        <td></td>
                                        <td align="center">
                                            <asp:Label ID="lblUsuariosCampus" runat="server" Text="Usuarios en el Campus"></asp:Label>
                                        </td>
                                        <td></td>
                                        <td align="center">
                                            <asp:Label ID="Label9" runat="server" Text="Lista de Usuarios"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ListBox ID="lstCampus" runat="server" Height="150px" Width="200px" OnSelectedIndexChanged="lstCampus_SelectedIndexChanged" AutoPostBack="True" SelectionMode="Multiple"></asp:ListBox>
                                        </td>
                                        <td>
                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Permisos/fastforward.png" Height="57px" Width="37px" />
                                        </td>
                                        <td>
                                            <asp:ListBox ID="lstUsuariosCampus" runat="server" Height="150px" Width="200px" SelectionMode="Multiple"></asp:ListBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/Permisos/flecha-i.gif"
                                                Width="25px" OnClick="cmdAgregar_Click" /><br />
                                            <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/Permisos/flecha.gif"
                                                Width="25px" OnClick="cmdBorrar_Click" />
                                        </td>
                                        <td>
                                            <asp:ListBox ID="lstUsuarios" runat="server" Height="150px" Width="200px" SelectionMode="Multiple"></asp:ListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox_campus" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true" /><asp:Label ID="Label2" runat="server" Text="Seleccionar todos"></asp:Label></td>
                                    </tr>
                                </table>

                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lstCampus" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ListNiveles" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ListDocumentos" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ListDocs" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </asp:Panel>
                <asp:Panel ID="UpdatePanel1" runat="server">

                    <table>
                        <tr>
                            <td>
                                <input id="flecha_down_1" type="image" value="button" onclick="slide_down_1(); return false" src="Images/Permisos/flecha_rigth.png" class="auto-style1" />
                                <input id="flecha_up_1" type="image" value="button" onclick="slide_up_1(); return false" src="Images/Permisos/Flecha_down.png" class="auto-style1" />
                            </td>
                            <td>
                                <label id="lbl_add_nivel" onclick="slide_down_lbl_n(); return false">Agregar Niveles</label>
                                <label id="lbl_add_nivel_1" onclick="slide_up_lbl_n(); return false">Agregar Niveles</label>
                            </td>
                        </tr>
                    </table>
                    <table id="Table1" runat="server" align="center" class="table-responsive" width="100%" border="1"></table>




                    <div id="panel_1">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table style="margin-left: auto; margin-right: auto">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label4" runat="server" Text="Lista de Niveles"></asp:Label>
                                        </td>
                                        <td></td>
                                        <td align="center">
                                            <asp:Label ID="Label5" runat="server" Text="Usuarios en el Nivel"></asp:Label>
                                        </td>
                                        <td></td>
                                        <td align="center">
                                            <asp:Label ID="Label10" runat="server" Text="Lista de Usuarios"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ListBox ID="ListNiveles" runat="server" Height="150px" Width="100%" OnSelectedIndexChanged="ListNiveles_SelectedIndexChanged" AutoPostBack="True"
                                                SelectionMode="Multiple"></asp:ListBox>
                                        </td>
                                        <td>
                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Permisos/fastforward.png" Height="57px" Width="37px" />
                                        </td>
                                        <td>
                                            <asp:ListBox ID="ListUsuariosNivel" runat="server" Height="150px" Width="200px"
                                                SelectionMode="Multiple"></asp:ListBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Images/Permisos/flecha-i.gif"
                                                Width="25px" OnClick="cmdAgregar_Click_n" /><br />
                                            <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/Images/Permisos/flecha.gif"
                                                Width="25px" OnClick="cmdBorrar_Click_n" />
                                        </td>
                                        <td>
                                            <asp:ListBox ID="ListUsuarios" runat="server" Height="150px" Width="200px"
                                                SelectionMode="Multiple"></asp:ListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox_Niveles" runat="server" OnCheckedChanged="CheckBox_Niveles_CheckedChanged" AutoPostBack="true" /><asp:Label ID="Label3" runat="server" Text="Seleccionar todos"></asp:Label></td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lstCampus" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ListNiveles" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ListDocumentos" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ListDocs" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </asp:Panel>

                <asp:Panel ID="UpdatePanel2" runat="server">
                    <table>
                        <tr>
                            <td class="auto-style2">
                                <input id="flecha_down_2" type="image" value="button" onclick="slide_down_2(); return false" src="Images/Permisos/flecha_rigth.png" class="auto-style1" />
                                <input id="flecha_up_2" type="image" value="button" onclick="slide_up_2(); return false" src="Images/Permisos/Flecha_down.png" class="auto-style1" />
                            </td>
                            <td class="auto-style2">
                                <label id="lbl_add_doc" onclick="slide_down_lbl_d(); return false">Agregar Tipo de Documentos</label>
                                <label id="lbl_add_doc_1" onclick="slide_up_lbl_d(); return false">Agregar Tipo de Documentos</label>
                            </td>
                        </tr>
                    </table>
                    <table id="Table2" runat="server" align="center" class="table-responsive" width="100%" border="1"></table>



                    <div id="panel_2" style="text-align:center;">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="Label16" runat="server" Text="Asignar Documentos por Usuario" CssClass="title"></asp:Label>
                                <table style="margin-left: auto; margin-right: auto; text-align:left;">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label7" runat="server" Text="Lista de Tipos de Documentos"></asp:Label>
                                        </td>
                                        <td></td>
                                        <td align="center">
                                            <asp:Label ID="Label8" runat="server" Text="Documentos por Usuario"></asp:Label>
                                        </td>
                                        <td></td>
                                        <td align="center">
                                            <asp:Label ID="Label11" runat="server" Text="Lista de Usuarios"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>

                                            <asp:ListBox ID="ListDocumentos" runat="server" Height="150px" Width="100%" OnSelectedIndexChanged="ListDocumentos_SelectedIndexChanged"
                                                SelectionMode="Multiple" AutoPostBack="True"></asp:ListBox>

                                        </td>
                                        <td>
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Permisos/fastforward.png" Height="57px" Width="37px" />
                                        </td>
                                        <td>
                                            <asp:ListBox ID="ListUsuariosDocumentos" runat="server" Height="150px" Width="200px"
                                                SelectionMode="Multiple"></asp:ListBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/Images/Permisos/flecha-i.gif"
                                                Width="25px" OnClick="cmdAgregar_Click_d" /><br />
                                            <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/Images/Permisos/flecha.gif"
                                                Width="25px" OnClick="cmdBorrar_Click_d" />
                                        </td>
                                        <td>
                                            <asp:ListBox ID="ListUsuarios_1" runat="server" Height="150px" Width="200px"
                                                SelectionMode="Multiple"></asp:ListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox_Doc" runat="server" OnCheckedChanged="CheckBox_Doc_CheckedChanged" AutoPostBack="true" /><asp:Label ID="Label6" runat="server" Text="Seleccionar todos"></asp:Label></td>
                                    </tr>
                                </table><br />
                                                                <asp:Label ID="Label17" runat="server" Text="Asignar Documentos por Estatus" CssClass="title"></asp:Label>
                                 <table style="margin-left: auto; margin-right: auto; text-align:left;">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="Label12" runat="server" Text="Lista de Tipos de Documentos"></asp:Label>
                                        </td>
                                        <td></td>
                                        <td align="center">
                                            <asp:Label ID="Label13" runat="server" Text="Documentos por Estatus"></asp:Label>
                                        </td>
                                        <td></td>
                                        <td align="center">
                                            <asp:Label ID="Label14" runat="server" Text="Lista de Estatus"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>

                                            <asp:ListBox ID="ListDocs" runat="server" Height="150px" Width="100%" 
                                                SelectionMode="Multiple" AutoPostBack="True" OnSelectedIndexChanged="ListDocs_SelectedIndexChanged"></asp:ListBox>

                                        </td>
                                        <td>
                                            <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/Permisos/fastforward.png" Height="57px" Width="37px" />
                                        </td>
                                        <td>
                                            <asp:ListBox ID="ListDocE" runat="server" Height="150px" Width="200px"
                                                SelectionMode="Multiple"></asp:ListBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Permisos/flecha-i.gif"
                                                Width="25px" OnClick="cmAgregarEstatus_Click" /><br />
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Permisos/flecha.gif"
                                                Width="25px" OnClick="cmBorrarEstatus_Click" />
                                        </td>
                                        <td>
                                            <asp:ListBox ID="ListEstatus" runat="server" Height="150px" Width="200px"
                                                SelectionMode="Multiple"></asp:ListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged1" AutoPostBack="true" /><asp:Label ID="Label15" runat="server" Text="Seleccionar todos"></asp:Label></td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lstCampus" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ListNiveles" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ListDocs" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ListDocumentos" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </asp:Panel>
                <asp:Panel ID="Panel3" runat="server">
                    <table>
                        <tr>
                            <td class="auto-style2">
                                <input id="bloqueado" type="image" value="button" onclick="slide_down_4(); return false" src="Images/Permisos/bloqueado.png" />
                                <input id="desbloqueado" type="image" value="button" onclick="slide_up_4(); return false" src="Images/Permisos/desbloqueado.png" />
                                <%--<asp:Image ID="Image4" runat="server" ImageUrl="~/Images/Permisos/bloqueado.png"/>--%>
                            </td>
                            <td class="auto-style2">
                                <%--<label id="lbl_permisos">Privilegios de la aplicación</label>--%>
                                <label id="lbl_permisos_1" onclick="slide_down_lbl_p(); return false">Privilegios de la aplicación</label>
                                <label id="lbl_permisos_2" onclick="slide_up_lbl_p(); return false">Privilegios de la aplicación</label>
                            </td>
                        </tr>
                    </table>

                    <table id="Table3" runat="server" align="center" class="table-responsive" width="100%" border="1"></table>


                    <asp:Panel ID="GridView" runat="server">
                        <div id="privilegios_table">

                            <div class="ex3">
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="Permisos_App" runat="server" AutoGenerateColumns="False" RowHeaderColumn="IDPrivilegio" Width="100%" HorizontalAlign="Center" CssClass="table-hover" OnRowCommand="Permisos_App_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="IDPrivilegio" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">

                                                    <HeaderStyle CssClass="ColumnaOculta" />

                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton CommandName="Expand" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ID="MenuP" runat="server">
                                                            <asp:Image ID="MenuP_" runat="server" ImageUrl="Images/Permisos/Flecha_rigth.png" />
                                                        </asp:LinkButton>
                                                        <asp:LinkButton CommandName="Collapse" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ID="MenuP2" runat="server" Visible="False">
                                                            <asp:Image ID="Image5" runat="server" ImageUrl="Images/Permisos/Flecha_down.png" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Permiso" HeaderText="Permiso" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30%">
                                                    <ItemStyle Font-Size="Medium" HorizontalAlign="Left" Wrap="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50%">
                                                    <ItemStyle HorizontalAlign="Justify" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Activar/Desactivar">

                                                    <ItemTemplate>
                                                        <label class="switch">
                                                            <asp:CheckBox ID="Checkbox_P" runat="server" OnCheckedChanged="Checkbox_P_CheckedChanged" AutoPostBack="true" />
                                                            <%--<asp:CheckBox ID="Checkbox_P" runat="server" OnCheckedChanged="Checkbox_P_CheckedChanged" AutoPostBack="true" xmlns:asp="#unknown"/>--%>

                                                            <span class="slider round"></span>
                                                        </label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="table-dark_1" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <%--<asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />--%>
                                        <asp:AsyncPostBackTrigger ControlID="lstCampus" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ListNiveles" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ListDocumentos" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                        </div>
                    </asp:Panel>

                </asp:Panel>

                <%--</ContentTemplate>--%>
                <%--</asp:UpdatePanel>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="save_button">
            <asp:Panel ID="Button" runat="server" HorizontalAlign="Center">
                <asp:Button ID="Button1" runat="server" Text="Guardar" CssClass="btn btn-logout" OnClick="Button1_Click" />
            </asp:Panel>
        </div>
    </div>

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

