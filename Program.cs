using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// First Endpoint: /hello?name={SOME_NAME}
app.MapGet("/hello", (HttpContext context) =>
{
    var name = context.Request.Query["name"];
    var greeting = string.IsNullOrWhiteSpace(name) ? "Hello, World!" : $"Hello, {name}";
    return Results.Json(new { greeting });
});

// Second Endpoint: /info
app.MapGet("/info", (HttpContext context) =>
{
    var requestTime = DateTime.UtcNow.ToString("o"); // ISO8601 format
    var clientIp = context.Connection.RemoteIpAddress?.ToString();
    var hostName = Dns.GetHostName();
    var headers = context.Request.Headers;

    var response = new
    {
        time = requestTime,
        client_address = clientIp,
        host_name = hostName,
        headers = headers
    };

    return Results.Json(response);
});

// Start the application
app.Run();
