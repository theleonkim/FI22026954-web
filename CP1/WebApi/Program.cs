// ChatGPT (GPT-5 Thinking)
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CP1 Random List API", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Lista compartida: mezcla de int y double
List<object> bag = new();
Random rng = new();

// GET: redirige a Swagger
app.MapGet("/", () => Results.Redirect("/swagger")).WithOpenApi();

// POST: retorna la lista (JSON por defecto). Header opcional: xml: bool
app.MapPost("/", (HttpContext ctx) =>
{
    bool xml = false;
    if (ctx.Request.Headers.TryGetValue("xml", out var hdr))
    {
        bool.TryParse(hdr.ToString(), out xml);
    }

    if (!xml)
    {
        return Results.Ok(bag);
    }

    // Update: salida en XML
    var root = new XElement("items",
        bag.Select(item =>
        {
            string type = item is int ? "int" : (item is double ? "float" : item.GetType().Name);
            string value = item.ToString()!;
            return new XElement("item",
                new XAttribute("type", type),
                value);
        })
    );

    var xmlString = root.ToString();
    return Results.Text(xmlString, "application/xml", Encoding.UTF8);
}).WithOpenApi();

// PUT: agrega elementos
app.MapPut("/", (HttpRequest req) =>
{
    if (!int.TryParse(req.Form["quantity"], out int quantity))
        return Results.BadRequest(new { error = "'quantity' is required and must be an integer" });

    string type = req.Form["type"].ToString();

    // Improvement: validaciones
    if (quantity <= 0)
        return Results.BadRequest(new { error = "'quantity' must be higher than zero" });

    if (!string.Equals(type, "int", StringComparison.OrdinalIgnoreCase) &&
        !string.Equals(type, "float", StringComparison.OrdinalIgnoreCase))
        return Results.BadRequest(new { error = "'type' must be 'int' or 'float'" });

    for (int i = 0; i < quantity; i++)
    {
        if (string.Equals(type, "int", StringComparison.OrdinalIgnoreCase))
        {
            bag.Add(rng.Next(int.MinValue, int.MaxValue));
        }
        else
        {
            // double aleatorio en rango razonable
            bag.Add(rng.NextDouble() * 1_000_000d);
        }
    }

    return Results.Ok(bag);
}).Accepts<IFormCollection>("multipart/form-data").WithOpenApi();

// DELETE: elimina N desde el inicio
app.MapDelete("/", (HttpRequest req) =>
{
    if (!int.TryParse(req.Form["quantity"], out int quantity))
        return Results.BadRequest(new { error = "'quantity' is required and must be an integer" });

    if (quantity <= 0)
        return Results.BadRequest(new { error = "'quantity' must be higher than zero" });

    if (bag.Count < quantity)
        return Results.BadRequest(new { error = "List does not contain the requested amount to delete" });

    bag.RemoveRange(0, quantity);
    return Results.Ok(bag);
}).Accepts<IFormCollection>("multipart/form-data").WithOpenApi();

// PATCH: limpiar lista
app.MapPatch("/", () =>
{
    bag.Clear();
    return Results.Ok(bag);
}).WithOpenApi();

app.Run();
