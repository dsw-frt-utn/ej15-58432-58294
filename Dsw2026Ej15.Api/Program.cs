using Microsoft.AspNetCore.Diagnostics;
using Dsw2026Ej15.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<Dsw2026Ej15.Data.IPersistence, Dsw2026Ej15.Data.PersistenceInMemory>();

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        if (exception is ValidationException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { error = exception.Message });
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new { error = "Ocurrió un error inesperado en el servidor." });
        }
    });
});

app.MapGet("/health-check", () => Results.Ok());

app.UseAuthorization();
app.MapControllers();

app.Run();