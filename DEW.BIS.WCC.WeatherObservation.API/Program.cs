using DEW.BIS.WCC.WeatherObservation.API.Mappings;
using DEW.BIS.WCC.WeatherObservation.Services.Services;
using DEW.BIS.WCC.WeatherObservation.Shared.Interfaces;
using DEW.BIS.WCC.WeatherObservation.Shared.Settings;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using Serilog.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(WeatherObservationMappingProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddMemoryCache();

builder.Services.Configure<BaseAddressSettings>(builder.Configuration.GetSection("BaseAddress"));
builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection("Cache"));

builder.Services.AddTransient(typeof(IWeatherObservationService), typeof(WeatherObservationService));

var app = builder.Build();

var logger = app.Services.GetService<ILogger<Program>>();
logger?.LogInformation("The Application Is Started!");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(a => a.Run(async context =>
{
    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
    var exception = exceptionHandlerPathFeature?.Error;

    await context.Response.WriteAsJsonAsync(new { error = exception?.Message });
}));

app.UseSerilogRequestLogging();
app.UseAuthorization();
app.MapControllers();

app.Run();
