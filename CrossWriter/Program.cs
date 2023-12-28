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

// Add services to the container.
builder.Services.AddSingleton<MyNamingContainers>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

// Schaltet wwwroot und unterverzeichnisse frei
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthorization();

// Ermittelt die Origin der wwwroot
string GetWwwRootOrigin(HttpRequest req)
{
    return $"{req.Scheme}://{req.Host}";
}

// Liefer eine Liste von Naming- Containern. 
// Die Liste kann auf zwei Arten festgelegt werden:
// 1. Naming- Container werden durch eine Liste von NamingId's im Hex- Format (z.B. NC=0xABCDEF123,0x987654321,...,0xFFBBEEDD)
//    definiert.
// 2. Naming- Container werden durch einen Namensraum definiert.
app.MapGet("/NamingContainers", (HttpRequest request, MyNamingContainers myNamingContainers) =>
{
    if (request.Query.ContainsKey("NC") && request.Query["NC"].Any())
    {
        var nidString = request.Query["NC"].First() ?? "";

        var ncHlp = new NamingContainerWebApiHlp(myNamingContainers);

        var ncList = ncHlp.FetchNamingContainersWithNamingIds(nidString);

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

app.MapGet("/Main", (HttpRequest req, MyNamingContainers myNamingContainers) =>
{
    // get Origin (Path) of statical content
    var wwwroot = GetWwwRootOrigin(req);

    // old school templating :-)
    // Create html- content for Browser. Replace all placeholders for in server Urls etc. with valid Host adresses 
    var content = string.Join('\n', System.IO.File.ReadAllLines(@".\wwwroot\apps\nyt\MainView.html")).Replace("{*}", wwwroot);


});

app.Run();
