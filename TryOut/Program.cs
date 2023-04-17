using Microsoft.AspNetCore.Http.Extensions;
using MKPRG.Naming.TechTerms.Timeline;
using System.Net.Mime;
//using Microsoft.AspNetCore.Op
using Microsoft.AspNetCore.OpenApi;
using System.Text.Json.Nodes;
using TryOut.MySingeltons;

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

// For Autocomplete of Title fragments with Naming- Containers
app.MapPost("/WocTitlesStartsWith", (HttpRequest req, MyNamingContainers myNamingContainers) =>
{
    var wwwroot = GetWwwRootOrigin(req);

    // Erzeugt ein Default- Objekt
    var defaultValue = () => new JsonArray
                                    {
                                        new JsonObject { ["txt"] = "none", ["id"] = 0L }
                                    };

    if (req.ContentType == "application/json")
    {
        var wocTitleStartsWithJson = JsonNode.Parse(req.Body);
        var titleStart = wocTitleStartsWithJson?["titleStart"]?.ToString();

        if (!string.IsNullOrWhiteSpace(titleStart))
        {
            var allStartsWith = myNamingContainers.NC.Where(r => r.Value.DE.StartsWith(titleStart))
                                                     .OrderBy(r => r)
                                                     .Select(r => new { txt = r.Value.DE, id = r.Key })
                                                     .ToArray();

            if (allStartsWith.Any())
            {
                // Response aufbauen
                // Einzelner Treffer: { "txt": "bla bla...", "id": "1234..." }
                // Liste von Treffern als Array

                var options = new System.Text.Json.JsonSerializerOptions(System.Text.Json.JsonSerializerDefaults.Web)
                {
                    NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.WriteAsString,
                    WriteIndented = true,
                };

                return Results.Json(allStartsWith, options);

            }
            else
            {
                return Results.Json(defaultValue());
            }
        }
        else
        {
            return Results.Json(defaultValue());
        }
    }
    else
    {
        return Results.Problem("This Post accepts only JSON content with { 'titleStart': '...'} elements!");
    }
}).WithOpenApi();




app.Run();
