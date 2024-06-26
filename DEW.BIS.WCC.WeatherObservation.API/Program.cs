using DEW.BIS.WCC.WeatherObservation.API.Mappings;
using DEW.BIS.WCC.WeatherObservation.Services.Services;
using DEW.BIS.WCC.WeatherObservation.Shared.Interfaces;
using DEW.BIS.WCC.WeatherObservation.Shared.Settings;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(WeatherObservationMappingProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Settings
builder.Services.Configure<BaseAddressSettings>(builder.Configuration.GetSection("BaseAddress"));

builder.Services.AddTransient(typeof(IWeatherObservationService), typeof(WeatherObservationService));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
