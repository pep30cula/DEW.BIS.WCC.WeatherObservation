using Microsoft.AspNetCore.Mvc;
using DEW.BIS.WCC.WeatherObservation.Services;
using DEW.BIS.WCC.WeatherObservation.Services.Services;

namespace DEW.BIS.WCC.WeatherObservationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherObservationController : ControllerBase
    {
        //private readonly ILogger<WeatherObservationController> _logger;

        public WeatherObservationController(ILogger<WeatherObservationController> logger)
        {
            //_logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<object> Get()
        {
            //_logger.LogInformation("INFORMATION IS LOGGED!!");
            var weatherServices = new WeatherObservationService();
            var stationWeather = await weatherServices.GetStationWeather(94672);

            return stationWeather?.Data;
        }
    }
}
