using DEW.BIS.WCC.WeatherObservation.API.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DEW.BIS.WCC.WeatherObservation.Services.Extensions;
using DEW.BIS.WCC.WeatherObservation.Shared.Models;
using DEW.BIS.WCC.WeatherObservation.Shared.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;
using DEW.BIS.WCC.WeatherObservation.Shared.Settings;
using Microsoft.Extensions.Options;
using DEW.BIS.WCC.WeatherObservation.Shared;

namespace DEW.BIS.WCC.WeatherObservationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherObservationController : ControllerBase
    {
        private readonly ILogger<WeatherObservationController> _logger;
        private readonly IMapper _mapper;
        private readonly IWeatherObservationService _weatherObservationService;
        private readonly IMemoryCache _memoryCache;
        private readonly IOptions<CacheSettings> _cacheSettings;

        public WeatherObservationController(ILogger<WeatherObservationController> logger,
            IMapper mapper,
            IWeatherObservationService weatherObservationService,
            IMemoryCache memoryCache,
            IOptions<CacheSettings> cacheSettings)
        {
            _logger = logger;
            _mapper = mapper;
            _weatherObservationService = weatherObservationService;
            _memoryCache = memoryCache;
            _cacheSettings = cacheSettings;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<List<WeatherObservationDto>> GetWeatherForecast(int stationId = 94672)
        {
            if (stationId < 90000 || stationId > 99999)
            {
                throw new ArgumentException("The StationId must be between 90000 and 99999.");
            }

            var stationWeather = await _weatherObservationService.GetStationWeather(stationId);
            var result = _mapper.Map<List<ObservationData>, List<WeatherObservationDto>>(stationWeather?.Observations?.Data);

            return result;
        }

        [HttpGet(Name = "GetStationAverageTemperature")]
        public async Task<WeatherAverageDto> GetStationAverageTemperature(int stationId = 94672, TemperatureUnitType temperatureUnitType = TemperatureUnitType.Celsius)
        {
            var ss = _cacheSettings.Value.IsCacheEnabled;
            if (stationId < 90000 || stationId > 99999)
            {
                throw new ArgumentException("The StationId must be between 90000 and 99999.");
            }

            if (_cacheSettings.Value.IsCacheEnabled &&
                _memoryCache.TryGetValue(CacheKeys.AverageTemperatureCacheKey + stationId, out WeatherAverageDto? cachedWeatherAverage) &&
                DateTime.Now.Subtract(cachedWeatherAverage.LastUpdateDateTime).TotalMinutes < 30)
            {
                var averageTemperature = cachedWeatherAverage.AverageTemperature;
                if (cachedWeatherAverage.TemperatureUnitType != temperatureUnitType)
                {
                    switch (temperatureUnitType)
                    {
                        case TemperatureUnitType.Celsius:
                        default:
                            averageTemperature = averageTemperature.ConvertFahrenheitToCelsius();
                            break;
                        case TemperatureUnitType.Fahrenheit:
                            averageTemperature = averageTemperature.ConvertCelsiusToFahrenheit();
                            break;
                    }
                }

                return new WeatherAverageDto(
                    AverageTemperature: averageTemperature,
                    StationName: cachedWeatherAverage.StationName,
                    LastUpdateDateTime: cachedWeatherAverage.LastUpdateDateTime,
                    TemperatureUnitType: temperatureUnitType);
            }
            else
            {
                var stationWeather = await _weatherObservationService.GetStationWeather(stationId);

                var averageTemperature = stationWeather.Observations?.Data?.CalculateThreeDaysWeatherAverage(temperatureUnitType);

                if (averageTemperature == null)
                    return null;

                var result = new WeatherAverageDto(
                    AverageTemperature: averageTemperature.Value,
                    StationName: stationWeather.Observations?.Data?.First().StationName,
                    LastUpdateDateTime: DateTime.ParseExact(stationWeather.Observations.Data.First().LocalDateTime, "yyyyMMddHHmmss", CultureInfo.InvariantCulture),
                    TemperatureUnitType: temperatureUnitType);

                _memoryCache.Set(CacheKeys.AverageTemperatureCacheKey + stationId, result, TimeSpan.FromMinutes(30));

                return result;
            }

        }
    }
}
