using DEW.BIS.WCC.WeatherObservation.Shared.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DEW.BIS.WCC.WeatherObservation.Services.Services;
using DEW.BIS.WCC.WeatherObservation;
using DEW.BIS.WCC.WeatherObservation.Shared.Interfaces;


var configuration = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build();

var serviceCollection = new ServiceCollection();
serviceCollection.AddOptions<BaseAddressSettings>().Bind(configuration.GetSection("BaseAddress"));

serviceCollection.AddTransient<IWeatherObservationService, WeatherObservationService>();
serviceCollection.AddSingleton<WeatherService>();

var serviceProvider = serviceCollection.BuildServiceProvider();
var weatherService = (WeatherService)serviceProvider?.GetService(typeof(WeatherService));
await weatherService.GetAverageWeather();

Console.ReadLine();