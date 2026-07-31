using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Dsw2026Ej15.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Dsw2026Ej15.Data.MedicalContext>(options =>
    options.UseSqlite("Data Source=medical.db"), ServiceLifetime.Singleton);
builder.Services.AddSingleton<Dsw2026Ej15.Data.IPersistence, Dsw2026Ej15.Data.PersistenceEf>();
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

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