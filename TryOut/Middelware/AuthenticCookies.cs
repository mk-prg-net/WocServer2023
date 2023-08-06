using MKPRG.Naming.TechTerms.Operators.Relations;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using TryOut.MySingeltons;

namespace TryOut.Middelware
{
    public static class AuthenticCookies
    {
        /// <summary>
        /// ID des Cookies
        /// </summary>
        public const string AuthenicationCoockieId = "AUTHENTIC_COOKIES2023";

        public static string LoginPageUrl(HttpRequest req) => $"{req.Scheme}://{req.Host}/Login";

        public static WebApplication AuthenticCookiesAuthentication(this WebApplication app)
        {
            app.Use(async (ctx, next) =>
            {
                var req = ctx.Request;

                if (req.Path.Value == "/Login" || req.Path.Value == "/Login/TryAuthenticate")
                {
                    // Beim Zugriff auf die Login- Seite erfogt kein weiterer Authentifizierungsversuch
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
                                ctx.Response.Redirect(LoginPageUrl(req));
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
                        ctx.Response.Redirect(LoginPageUrl(req));
                    }
                }
                else
                {
                    // Wenn kein Authentifizierungscookie vorlag, dann Redirect zur Login- Page
                    ctx.Response.Redirect(LoginPageUrl(req));
                }                
            });

            return app;
        }
    }
}
