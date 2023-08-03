using Microsoft.AspNetCore.Http.Extensions;
using MKPRG.Naming.TechTerms.Timeline;
using System.Net.Mime;
//using Microsoft.AspNetCore.Op
using Microsoft.AspNetCore.OpenApi;
using System.Text.Json.Nodes;
using TryOut.MySingeltons;
using TryOut.Models;
using Microsoft.AspNetCore.Mvc;
using MKPRG.Naming.TechTerms.Development;
using Microsoft.AspNetCore.Mvc.ModelBinding;

// Martin Korneffel, Feb.2023
// SPA- Grundgerüst auf Basis
// von minimal WebApi entwickeln

// Konfigurieren des Builders
var builder = WebApplication.CreateBuilder(
    new WebApplicationOptions
    {
        Args = args,
        ApplicationName = typeof(Program).Assembly.FullName,
        ContentRootPath = Directory.GetCurrentDirectory(),
        EnvironmentName = Environments.Staging,

        // Hier wird das Wurzelverzeichnis für den statischen Content definiert (html, css, scripte)
        WebRootPath = "wwwroot"
    }
);

// Alle Dienste konfigurieren, welche die Anwendung nutzt

builder.Services.AddSingleton<MyNamingContainers>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Schaltet wwwroot und unterverzeichnisse frei
app.UseStaticFiles();

// mko, 16.4.2023
// Ermittelt die Origin der wwwroot
string GetWwwRootOrigin(HttpRequest req)
{
    return $"{req.Scheme}://{req.Host}";
}


// Startup der SPA- Applikation: Hier wird das initiale HTML- Dokument geladen
app.MapGet("/", (HttpRequest req) =>
{
    // Origin des statischen Content bestimmen
    var wwwroot = GetWwwRootOrigin(req);

    // Alle {*} oOrigin Symbole mit der Root ersetzen in der HTML- Datei
    var content = string.Join('\n', System.IO.File.ReadAllLines(@".\wwwroot\index.html")).Replace("{*}", wwwroot);

    return Results.Content(content, "text/html", System.Text.Encoding.UTF8);

}).WithOpenApi();


app.MapGet("/ApplyF", (HttpRequest req) => {

    // Origin des statischen Content bestimmen
    var wwwroot = GetWwwRootOrigin(req);

    // Alle {*} oOrigin Symbole mit der Root ersetzen in der HTML- Datei
    var content = string.Join('\n', System.IO.File.ReadAllLines(@".\wwwroot\Apps\ApplyF\MainView.html")).Replace("{*}", wwwroot);

    return Results.Content(content, "text/html", System.Text.Encoding.UTF8);

});

// Lädt den Editor vom Server
app.MapGet("/edit", (HttpRequest req) =>
{

    // Origin des statischen Content bestimmen
    var wwwroot = GetWwwRootOrigin(req);

    // Alle {*} oOrigin Symbole mit der Root ersetzen in der HTML- Datei
    var content = string.Join('\n', System.IO.File.ReadAllLines(@".\wwwroot\edit.html")).Replace("{*}", wwwroot);

    return Results.Content(content, "text/html", System.Text.Encoding.UTF8);
});

// Lädt den Client vom Server
app.MapGet("/edit-test", (HttpRequest req) =>
{

    // Origin des statischen Content bestimmen
    var wwwroot = GetWwwRootOrigin(req);

    // Alle {*} oOrigin Symbole mit der Root ersetzen in der HTML- Datei
    var content = string.Join('\n', System.IO.File.ReadAllLines(@".\wwwroot\edit_test.html")).Replace("{*}", wwwroot);

    return Results.Content(content, "text/html", System.Text.Encoding.UTF8);
});

app.MapGet("/LLPedit-test", (HttpRequest req) =>
{

    // Origin des statischen Content bestimmen
    var wwwroot = GetWwwRootOrigin(req);

    // Alle {*} oOrigin Symbole mit der Root ersetzen in der HTML- Datei
    var content = string.Join('\n', System.IO.File.ReadAllLines(@".\wwwroot\Apps\LLPedit_Test\MainView.html")).Replace("{*}", wwwroot);

    return Results.Content(content, "text/html", System.Text.Encoding.UTF8);
});


// Lädt den Editor vom Server
app.MapGet("/LLPedit", (HttpRequest req) =>
{

    // Origin des statischen Content bestimmen
    var wwwroot = GetWwwRootOrigin(req);

    // Alle {*} oOrigin Symbole mit der Root ersetzen in der HTML- Datei
    var content = string.Join('\n', System.IO.File.ReadAllLines(@".\wwwroot\Apps\LLPedit\MainView.html")).Replace("{*}", wwwroot);

    return Results.Content(content, "text/html", System.Text.Encoding.UTF8);
});

// Löst eine Liste von NamingId's im Hex- Format (z.B. NC=0xABCDEF123,0x987654321,...,0xFFBBEEDD)
// in eine Liste von Namenscontainern auf.
app.MapGet("/NamingContainers", (HttpRequest request, MyNamingContainers myNamingContainers) =>
{
    if (request.Query.ContainsKey("NC") && request.Query["NC"].Any())
    {       
        var nidString = request.Query["NC"].First() ?? "";        

        var ncList = NamingContainerWebApiHlp.FetchNamingContainers(nidString, myNamingContainers);        

        return Results.Json(ncList, new System.Text.Json.JsonSerializerOptions()
        {
            PropertyNamingPolicy = null
        });
    }
    else
    {
        return Results.Problem("QueryString is incorrect! NC=ABCDEF123,987654321,...,FFBBEEDD expected");
    }
});


// mko, 23.4.2023
// Liefert Liste von GUID64
// Aufruf: .../GUID64?reqCount=3 -> JsonArray mit drei neuen GUID's
app.MapGet("/GUID64", (HttpRequest request) =>
{
    // Erzeugt ein Default - Objekt
    var defaultValue = () => new JsonArray { "none" };

    if (request.Query.ContainsKey("ReqCount"))
    {
        var reqCountStr = request.Query["RequestCount"];
        if (int.TryParse(reqCountStr, out int reqCount))
        {
            var lstGuid64 = new List<long>(reqCount);
            for (int i = 0; i < reqCount; i++)
            {
                lstGuid64.Add(MKPRG.GUID64.GUID64Generator.NewGUID64());
            }

            var ret = new JsonArray { lstGuid64.Select(r => r.ToString()).ToArray() };
            return Results.Json(ret);
        }
        else
        {
            return Results.Problem(statusCode: 500, detail: "RequestCount must be an integer");
        }
    }
    else
    {
        return Results.Json(new JsonArray { MKPRG.GUID64.GUID64Generator.NewGUID64().ToString() });
    }
});


// mko, 20.4.2023
// For Autocomplete of Title fragments with Naming- Containers
// Call: .../WocTitlesStartsWith, body: { "titleStart": "prefix..."} -> 
app.MapPost("/WocTitlesStartsWith", async (HttpRequest req, MyNamingContainers myNamingContainers) =>
{
    var wwwroot = GetWwwRootOrigin(req);

    // Erzeugt ein Default - Objekt
    var defaultValue = () => new JsonArray { new JsonObject { ["txt"] = "none", ["id"] = 0L } };

    if (req.ContentType == "application/json" || req.ContentType == "application/x-www-form-urlencoded")
    {

        var reader = new System.IO.StreamReader(req.Body);
        //var buffer = new byte[1024];
        //await req.Body.ReadAsync(buffer, 0, (int)1024);

        //var json = System.Text.Encoding.ASCII.GetString(buffer);

        var json = await reader.ReadToEndAsync();
        try
        {
            var wocTitleStartsWithJson = JsonNode.Parse(json);
            var titleStart = wocTitleStartsWithJson?["titleStart"]?.ToString();


            var options = new System.Text.Json.JsonSerializerOptions(System.Text.Json.JsonSerializerDefaults.Web)
            {
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.WriteAsString,
                WriteIndented = true,
                PropertyNamingPolicy = null
            };

            if (!string.IsNullOrWhiteSpace(titleStart))
            {
                var allStartsWith = myNamingContainers.NC.Where(r => r.Value.DE.StartsWith(titleStart))
                                                         .OrderBy(r => r.Value.DE)
                                                         .Select(r => new NamingContainerSimple()
                                                         {
                                                             NIDstr = r.Key.ToString("X"),
                                                             CNT = r.Value.CNT,
                                                             CN = r.Value.CN,
                                                             EN = r.Value.EN,
                                                             ES = r.Value.ES,
                                                             DE = r.Value.DE,
                                                         })
                                                         .ToArray();

                if (allStartsWith.Any())
                {
                    // Response aufbauen
                    // Einzelner Treffer: { "txt": "bla bla...", "id": "1234..." }
                    // Liste von Treffern als Array


                    return Results.Json(allStartsWith, options);

                }
                else
                {
                    return Results.Json(defaultValue(), options);
                }
            }
            else
            {
                return Results.Json(defaultValue(), options);
            }
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.ToString());
        }

    }
    else
    {
        return Results.Problem("This Post accepts only JSON content with { 'titleStart': '...'} elements!");
    }
;
}).WithOpenApi();




app.Run();
