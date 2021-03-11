<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Tipodocumentos.aspx.cs" Inherits="Tipodocumentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="Content/Bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="Content/Otros/paginacion.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">
    <script src="Scripts/Bootstrap/bootstrap.js"></script>
    <link href="Content/Sweet_Alert/sweetalert.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Raleway" rel="stylesheet">
    <script src="Scripts/Sweet_Alert/sweetalert.js"></script>
    <style>
        .ColumnaOculta {
            display: none;
        }

        .form-control-1 {
            border-radius: 0.25rem;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ID="Contenido" ContentPlaceHolderID="MainContent">
    <%--<center><h2>Catalogo de Documentos</h2></center>--%>

    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <%--<asp:UpdatePanel runat="server">
        <ContentTemplate>--%>
    <div style="float: left">
        <i class="fas fa-search" style="font-size: 25px; vertical-align: -webkit-baseline-middle;"></i>
    </div>
    <div>
        <asp:TextBox ID="search" runat="server" OnTextChanged="search_TextChanged" AutoPostBack="true" Width="30%" CssClass="form-control"></asp:TextBox>
    </div>
    <%--    <table style="margin-left: auto; margin-right: auto; width: 100%">
        <tr>
            <td>
                <br />--%>
    <asp:UpdateProgress ID="updProgress"
        AssociatedUpdatePanelID="UpdatePanel1"
        runat="server">
        <ProgressTemplate>
            <img alt="progress" src="Images/Tipo_Documento/load.gif" />
            Processing...           
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="gvTipoDocumento" runat="server" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" Width="100%" DataKeyNames="IDTipoDocumento" AllowPaging="True" PageSize="5" OnPageIndexChanging="gvTipoDocumento_PageIndexChanging" CssClass="table table-bordered" OnRowEditing="gvTipoDocumento_RowEditing" OnRowCancelingEdit="gvTipoDocumento_RowCancelingEdit" OnRowUpdating="gvTipoDocumento_RowUpdating" OnRowUpdated="gvTipoDocumento_RowUpdated" OnRowDeleting="gvTipoDocumento_RowDeleting">
                <Columns>
                    <%--                        <asp:TemplateField HeaderText="IDTipoDocumento">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtIDTipoDocumento" runat="server" Text='<%# Bind("IDTipoDocumento") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("IDTipoDocumento") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="ColumnaOculta" />
                        </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Documento">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDocumento" runat="server" Text='<%# Bind("Nombre") %>' CssClass="form-control-1"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:CheckBoxField DataField="Forzoso" HeaderText="Forzoso" ItemStyle-HorizontalAlign="Center"/>--%>
                    <asp:TemplateField HeaderText="Descripción">
                        <EditItemTemplate>

                            <asp:TextBox ID="txtDescripcion" runat="server" Text='<%# Bind("Descripcion") %>' CssClass="form-control-1"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tamaño Mínimo (KB)">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTamanoMinimo" runat="server" Text='<%# Bind("tamanominimo") %>' ToolTip="Es necesario ingresar solo numeros" CssClass="form-control-1"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("tamanominimo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tamaño Máximo (KB)">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTamanoMaximo" runat="server" Text='<%# Bind("tamanomaximo") %>' ToolTip="Es necesario ingresar solo numeros" CssClass="form-control-1"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("tamanomaximo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Formato">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtFormato" runat="server" Text='<%# Bind("Formato") %>' ToolTip="Favor de ingresar la extensión del formato y sin puntos. En caso de ingresar mas de una favor de separar por una coma. Ej. PNG,JPGE" CssClass="form-control-1"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("Formato") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Forzoso" ControlStyle-CssClass="ColumnaOculta" ItemStyle-CssClass="ColumnaOculta" HeaderStyle-CssClass="ColumnaOculta">
                        <ItemTemplate>
                            <asp:CheckBox ID="chk1" runat="server" Checked='<%#Convert.ToBoolean(Eval("forzoso")) %>' />
                        </ItemTemplate>

                        <ControlStyle CssClass="ColumnaOculta"></ControlStyle>

                        <HeaderStyle CssClass="ColumnaOculta"></HeaderStyle>

                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="Edit" runat="server" ImageUrl="~/Images/Tipo_Documento/Edit.png" Width="32px" />
                                <asp:ImageButton ID="Eliminar" runat="server" ImageUrl="~/Images/Tipo_Documento/Delete.png" Width="32px" />
                            </ItemTemplate>

                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>--%>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:ImageButton ID="Update_b" runat="server" CausesValidation="True" CommandName="Update" ImageUrl="~/Images/Tipo_Documento/save.png" Text="Actualizar" />
                            &nbsp;<asp:ImageButton ID="Cancel_b" runat="server" CausesValidation="False" CommandName="Cancel" ImageUrl="~/Images/Tipo_Documento/cancel.png" Text="Cancelar" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="Edit_b" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/Images/Tipo_Documento/lapiz.png" Text="" Visible='<%# permiso_editar %>' />
                            &nbsp;<asp:ImageButton ID="Delete_b" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Images/Tipo_Documento/Delete.png" Text="" Visible='<%# permiso_eliminar %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                    </asp:TemplateField>
                </Columns>

                <HeaderStyle HorizontalAlign="Center" CssClass="table-header_1"></HeaderStyle>

                <PagerSettings Mode="NumericFirstLast" FirstPageText="Primero" LastPageText="Último" PageButtonCount="4" />
                <PagerStyle CssClass="pagination-ys" BorderColor="White" HorizontalAlign="Center" />

            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--            </td>
        </tr>
    </table>--%>
    <%--</ContentTemplate></asp:UpdatePanel>--%>
    <div align="right">
        <asp:Button ID="Sincronizar_doc" runat="server" Text="Sincroniza Documentos" CssClass="btn btn-secondary" OnClick="Sincronizar_doc_Click" />
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="Server">
    <link href="Content/Bootstrap/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/Sweet_Alert/sweetalert.js"></script>
    <link href="Content/Sweet_Alert/sweetalert.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script type="text/javascript" src="https://tinymce.cachefly.net/4.0/tinymce.min.js"></script>
    <script type="text/javascript">
        tinymce.init({ selector: 'textarea', width: 300 });
    </script>
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

