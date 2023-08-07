using MKPRG.Naming.TechTerms.Operators.Relations;
using System.Diagnostics;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using TryOut.MySingeltons;

namespace TryOut.Middelware
{
    /// <summary>
    /// mko, 6.8.2023
    /// Middelware, die eine Authentifizierung auf Basis von Cookies ermöglicht.
    /// </summary>
    public static class AuthenticCookies
    {
        /// <summary>
        /// ID des Cookies
        /// </summary>
        public const string AuthenicationCoockieId = "AUTHENTIC_COOKIES2023";

        /// <summary>
        /// Name des Querystring- Parameters für Login- Service, in dem die Route mitgeteilt wird, für die Authorisierung erforderlich ist
        /// </summary>
        public const string RouteThatRequiresAuthorizationQueryStringParameter = "routeToAuthorize";


        private static string NameOfLoginService = "";
        public static string LoginPageUrl(HttpRequest req, string route) => $"{req.Scheme}://{req.Host}/{NameOfLoginService}?{RouteThatRequiresAuthorizationQueryStringParameter}={route}";

        /// <summary>
        /// Hier werden alle Routen eingetragen, bei denen der Zugriff eine gültige Authentifizierung und eine Athorisierung erfodert
        /// </summary>
        static HashSet<string> RoutesWithAuthorizationRequired = new HashSet<string>();

        /// <summary>
        /// Mit dieser Methode kann ein Http- Handler explizit für die Authorisierung angemeldet werden.
        /// </summary>
        /// <param name="rhb"></param>
        /// <returns></returns>
        public static RouteHandlerBuilder Authorize(this RouteHandlerBuilder rhb)
        {   
            // Hier wird die Logik registriert, die ein sogenannter EndPointBuilder zu gegebner Zeit ausführen soll
            rhb.Add(epb =>
            {
                Debug.WriteLine($"DisplayName: {epb.DisplayName}");
                if (epb is Microsoft.AspNetCore.Routing.RouteEndpointBuilder rb)
                {
                    if (rb.RoutePattern != null)
                    {
                        // Die Route des Http Handlers wird explizit in der Liste der Routen, die Authorisiert werden müssen, eingetragen.
                        var _ = RoutesWithAuthorizationRequired.Add(rb.RoutePattern.RawText);
                    }
                }
            });
            return rhb;
        }

        /// <summary>
        /// Erweiterungsmethode, die den Authentifizierungsprozess festlegt.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="NameOfLoginService"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public static WebApplication AuthenticCookiesAuthentication(this WebApplication app, string NameOfLoginService = "Login")
        {
            AuthenticCookies.NameOfLoginService = NameOfLoginService;
            app.Use(async (ctx, next) =>
            {
                var req = ctx.Request;                
                
                if (!RoutesWithAuthorizationRequired.Contains(req.Path.Value ?? "")) //  == "/Login" || req.Path.Value == "/Login/TryAuthenticate")
                {
                    // Die Route wurde nicht explizit mittels .Authorize() für die Authorisierung bestimmt
                    await next.Invoke(ctx);
                }
                // Prüfen, ob ein Authentication- Cookie vorliegt
                else if (req.Cookies.ContainsKey(AuthenicationCoockieId))
                {
                    // Wenn ja, dann Sitzungsnummer ermitteln
                    var authCookie = req.Cookies[AuthenicationCoockieId];
                    if (long.TryParse(authCookie, out long sessionId))
                    {
                        // Existenz der Sitzung überprüfen
                        // Dazu auf Sitzungsverwaltung zugreifen ...
                        var mySessionStore = (MySessionStore?)app.Services.GetService(typeof(MySessionStore));
                        if (mySessionStore != null)
                        {
                            // Prüfen, ob unter der Nummer eine Sitzung existiert
                            var getSession = await mySessionStore.GetSession(sessionId);
                            if (!getSession.SessionFound)
                            {
                                // Sitzung unter der Sitzungsnummer existiert nicht:
                                // Redirektion zur Login- Page!
                                ctx.Response.Redirect(LoginPageUrl(req, req.Path.Value));
                            }
                            else
                            {
                                // Sitzung existiert unter der Sitzungsnummer
                                // Authentifizierung erfolgreich!
                                // Weiter mit der nächsten WabApi Pipeline Stufe

                                await next.Invoke(ctx);
                            }
                        }
                        else
                        {
                            // Kein Zugriff auf den Sitzungsspeicher möglich- da ist etwas fundamental 
                            // nicht in Ordnung!
                            throw new ApplicationException("SessionStore existiert nicht");
                        }
                    } else
                    {
                        // Das Authentifizierungs- Cookie muss eine 64bit Zahl sein- wenn nicht, dann ist was faul!
                        ctx.Response.Cookies.Delete(AuthenicationCoockieId);
                        ctx.Response.Redirect(LoginPageUrl(req, req.Path.Value));
                    }
                }
                else
                {
                    // Wenn kein Authentifizierungscookie vorlag, dann Redirect zur Login- Page
                    ctx.Response.Redirect(LoginPageUrl(req, req.Path.Value));
                }                
            });

            return app;
        }
    }
}
