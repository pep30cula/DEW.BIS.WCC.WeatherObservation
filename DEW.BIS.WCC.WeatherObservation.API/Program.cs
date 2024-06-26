using DEW.BIS.WCC.WeatherObservation.API.Mappings;
using DEW.BIS.WCC.WeatherObservation.Services.Services;
using DEW.BIS.WCC.WeatherObservation.Shared.Interfaces;
using DEW.BIS.WCC.WeatherObservation.Shared.Settings;
using Serilog;
using Serilog.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(WeatherObservationMappingProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSerilog();
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.Configure<BaseAddressSettings>(builder.Configuration.GetSection("BaseAddress"));

builder.Services.AddTransient(typeof(IWeatherObservationService), typeof(WeatherObservationService));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseAuthorization();
app.MapControllers();

app.Run();
