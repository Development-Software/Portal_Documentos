using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Membership.OpenAuth;

namespace Portal_Documentos
{
    public static class AuthConfig
    {
        public static void RegisterOpenAuth()
        {
            // Consulte https://go.microsoft.com/fwlink/?LinkId=252803 para obtener detalles sobre la configuración de esta aplicación ASP.NET
            // para que admita el inicio de sesión mediante servicios externos.

            //OpenAuth.AuthenticationClients.AddTwitter(
            //    consumerKey: "su clave de consumidor de Twitter",
            //    consumerSecret: "su secreto de consumidor de Twitter");

            //OpenAuth.AuthenticationClients.AddFacebook(
            //    appId: "su id de aplicación de Facebook",
            //    appSecret: "su secreto de aplicación de Facebook");

            //OpenAuth.AuthenticationClients.AddMicrosoft(
            //    clientId: "su id de cliente de cuenta de Microsoft",
            //    clientSecret: "su secreto de cliente de cuenta de Microsoft");

            //OpenAuth.AuthenticationClients.AddGoogle();
        }
    }
}