<%@ Application Language="C#" %>
<%@ Import Namespace="Portal_Documentos" %>
<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Código que se ejecuta al iniciar la aplicación
        AuthConfig.RegisterOpenAuth();
        RouteConfig.RegisterRoutes(RouteTable.Routes);
    }
    
    void Application_End(object sender, EventArgs e)
    {
        //  Código que se ejecuta al cerrarse la aplicación

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Código que se ejecuta cuando se produce un error no controlado

    }

</script>
