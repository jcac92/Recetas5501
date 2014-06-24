using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using Recetas_1.Models;

namespace Recetas_1
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // Para permitir que los usuarios de este sitio inicien sesión con sus cuentas de otros sitios como, por ejemplo, Microsoft, Facebook y Twitter,
            // es necesario actualizar este sitio. Para obtener más información, visite http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            Dictionary<string, object> FacebookData = new Dictionary<string, object>();

            OAuthWebSecurity.RegisterFacebookClient(
                appId: "124752004299427",
                appSecret: "a500e7b597a3fde28819d7d3c29dc749",
                displayName: "Facebook",
                extraData: FacebookData);

            //OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
