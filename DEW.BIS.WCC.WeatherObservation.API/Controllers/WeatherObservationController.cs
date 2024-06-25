using DEW.BIS.WCC.WeatherObservation.Services.Services;
using DEW.BIS.WCC.WeatherObservation.API.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace DEW.BIS.WCC.WeatherObservationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherObservationController : ControllerBase
    {
        //private readonly ILogger<WeatherObservationController> _logger;
        private readonly IMapper _mapper;

        public WeatherObservationController(ILogger<WeatherObservationController> logger, IMapper mapper)
        {
            //_logger = logger;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<List<WeatherObservationDto>> Get()
        {
            //_logger.LogInformation("INFORMATION IS LOGGED!!");
            var weatherServices = new WeatherObservationService();
            var stationWeather = await weatherServices.GetStationWeather(94672);

            return _mapper.Map<List<WeatherObservation.Services.Models.WeatherObservation>, List<WeatherObservationDto>>(stationWeather?.Observations?.Data);
        }
    }
}
