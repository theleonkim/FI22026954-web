// PP3/TextApi/Program.cs
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JSON camelCase para "ori"/"new"
builder.Services.ConfigureHttpJsonOptions(o =>
{
    o.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

var app = builder.Build();

// Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

// GET /
app.MapGet("/", () => Results.Redirect("/swagger"))
   .WithSummary("Redirige al UI de Swagger");

// ---------- helpers (funciones locales) ----------
static IResult OkMaybeXml(HttpContext ctx, ResultDto dto)
{
    bool wantsXml = false;
    if (ctx.Request.Headers.TryGetValue("xml", out var hv))
        bool.TryParse(hv.ToString(), out wantsXml);

    if (!wantsXml) return Results.Ok(dto);

    var xs = new XmlSerializer(typeof(ResultDto));
    using var ms = new MemoryStream();
    xs.Serialize(ms, dto);
    var xml = Encoding.UTF8.GetString(ms.ToArray());
    return Results.Content(xml, "application/xml", Encoding.UTF8);
}

static IResult Error(string msg) =>
    Results.Json(new { error = msg }, statusCode: 400);

static string[] SplitWords(string s) =>
    s.Split(' ', StringSplitOptions.RemoveEmptyEntries);

// longitud “lógica” ignorando puntuación simple al inicio/fin
static int LogicalLen(string w)
{
    var t = w.Trim().TrimStart('.', ',', ';', ':', '!', '?', '"')
             .TrimEnd('.', ',', ';', ':', '!', '?', '"');
    return t.Length;
}

// ---------- ENDPOINTS ----------

// POST /include/{position}
app.MapPost("/include/{position:int}", (
    HttpContext ctx,
    [FromRoute] int position,
    [FromQuery] string? value,
    [FromForm] string? text
) =>
{
    if (position < 0) return Error("'position' must be 0 or higher");
    if (string.IsNullOrWhiteSpace(value)) return Error("'value' cannot be empty");
    if (string.IsNullOrWhiteSpace(text)) return Error("'text' cannot be empty");

    var list = SplitWords(text).ToList();
    if (position >= list.Count) list.Add(value!);
    else list.Insert(position, value!);

    return OkMaybeXml(ctx, new ResultDto(text, string.Join(' ', list)));
})
.WithSummary("Incluye una palabra en la posición indicada.");

// PUT /replace/{length}
app.MapPut("/replace/{length:int}", (
    HttpContext ctx,
    [FromRoute] int length,
    [FromQuery] string? value,
    [FromForm] string? text
) =>
{
    if (length <= 0) return Error("'length' must be greater than 0");
    if (string.IsNullOrWhiteSpace(value)) return Error("'value' cannot be empty");
    if (string.IsNullOrWhiteSpace(text)) return Error("'text' cannot be empty");

    var replaced = SplitWords(text).Select(w => LogicalLen(w) == length ? value! : w);
    return OkMaybeXml(ctx, new ResultDto(text, string.Join(' ', replaced)));
})
.WithSummary("Reemplaza palabras de una longitud específica.");

// DELETE /erase/{length}
app.MapDelete("/erase/{length:int}", (
    HttpContext ctx,
    [FromRoute] int length,
    [FromForm] string? text
) =>
{
    if (length <= 0) return Error("'length' must be greater than 0");
    if (string.IsNullOrWhiteSpace(text)) return Error("'text' cannot be empty");

    var filtered = SplitWords(text).Where(w => LogicalLen(w) != length);
    return OkMaybeXml(ctx, new ResultDto(text, string.Join(' ', filtered)));
})
.WithSummary("Elimina palabras de una longitud específica.");

app.Run();
