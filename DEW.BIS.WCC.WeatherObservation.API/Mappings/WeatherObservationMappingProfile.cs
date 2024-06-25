using AutoMapper;
using DEW.BIS.WCC.WeatherObservation.API.DTO;
using DEW.BIS.WCC.WeatherObservation.Services;
using DEW.BIS.WCC.WeatherObservation.Services.Models;
using DEW.BIS.WCC.WeatherObservation.Services.Extensions;

namespace DEW.BIS.WCC.WeatherObservation.API.Mappings
{
    public class WeatherObservationMappingProfile : Profile
    {
        public WeatherObservationMappingProfile()
        {
            CreateMap<WeatherObservationResponse, WeatherObservationDto>()
                .ForCtorParam(ctorParamName: "TemperatureInF", m => m.MapFrom(s => s.Temperature.ConvertCelsiusToFahrenheit()))
                .ForCtorParam(ctorParamName: "WindSpeedInMph", m => m.MapFrom(s => s.WindSpeedInKmh.ConvertKmhToMph()));
        }
    }
}
