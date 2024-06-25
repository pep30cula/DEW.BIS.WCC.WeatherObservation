using DEW.BIS.WCC.WeatherObservation.Services.Services;
using DEW.BIS.WCC.WeatherObservation.API.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DEW.BIS.WCC.WeatherObservation.Services.Extensions;
using DEW.BIS.WCC.WeatherObservation.Services;
using DEW.BIS.WCC.WeatherObservation.Services.Models;

namespace DEW.BIS.WCC.WeatherObservationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
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
        public async Task<List<WeatherObservationDto>> GetWeatherForecast(int stationId = 94672)
        {
            if (stationId < 90000 || stationId > 99999)
            {
                throw new ArgumentException("The StationId must be between 90000 and 99999.");
            }

            //_logger.LogInformation("INFORMATION IS LOGGED!!");
            var weatherServices = new WeatherObservationService();
            var stationWeather = await weatherServices.GetStationWeather(stationId);

            return _mapper.Map<List<WeatherObservationResponse>, List<WeatherObservationDto>>(stationWeather?.Observations?.Data);
        }

        [HttpGet(Name = "GetStationAverageTemperature")]
        public async Task<WeatherAverageDto> GetStationAverageTemperature(int stationId = 94672, TemperatureDegreeType temperatureDegreeType = TemperatureDegreeType.Celsius)
        {
            if (stationId < 90000 || stationId > 99999)
            {
                throw new ArgumentException("The StationId must be between 90000 and 99999.");
            }

            var weatherServices = new WeatherObservationService();
            var stationWeather = await weatherServices.GetStationWeather(stationId);

            var averageTemperature = stationWeather.Observations?.Data?.CalculateThreeDaysWeatherAverage(temperatureDegreeType);

            if (averageTemperature == null)
                return null;

            var result = new WeatherAverageDto(
                AverageTemperature: averageTemperature.Value,
                StationName: stationWeather.Observations?.Data?.First().StationName);

            return result;
        }
    }
}
