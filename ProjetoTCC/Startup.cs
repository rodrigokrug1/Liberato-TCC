using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using System.Web.Helpers;

[assembly: OwinStartup(typeof(ProjetoTCC.Startup))]

namespace ProjetoTCC
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Autenticacao/Login")
            });
            AntiForgeryConfig.UniqueClaimTypeIdentifier = "Login";
        }
    }
}
