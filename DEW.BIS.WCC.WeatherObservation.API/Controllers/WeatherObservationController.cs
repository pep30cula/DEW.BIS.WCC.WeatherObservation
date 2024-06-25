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
        public async Task<List<WeatherObservationDto>> GetStationWeather(int stationId = 94672)
        {
            if (stationId < 90000 || stationId > 99999)
            {
                throw new ArgumentException("The StationId must be between 90000 and 99999.");
            }

            //_logger.LogInformation("INFORMATION IS LOGGED!!");
            var weatherServices = new WeatherObservationService();
            var stationWeather = await weatherServices.GetStationWeather(stationId);

            return _mapper.Map<List<WeatherObservation.Services.Models.WeatherObservation>, List<WeatherObservationDto>>(stationWeather?.Observations?.Data);
        }
    }
}
