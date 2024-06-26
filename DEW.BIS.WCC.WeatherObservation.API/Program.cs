using DEW.BIS.WCC.WeatherObservation.API.Mappings;
using DEW.BIS.WCC.WeatherObservation.Services.Services;
using DEW.BIS.WCC.WeatherObservation.Shared.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(WeatherObservationMappingProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
