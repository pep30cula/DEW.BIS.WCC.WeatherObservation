using AutoMapper;
using DEW.BIS.WCC.WeatherObservation.API.DTO;

namespace DEW.BIS.WCC.WeatherObservation.API.Mappings
{
    public class WeatherObservationMappingProfile : Profile
    {
        public WeatherObservationMappingProfile()
        {
            CreateMap<Services.Models.WeatherObservation, WeatherObservationDto>()
                .ForCtorParam(ctorParamName: "TemperatureInF", m => m.MapFrom(s => ((s.Temperature * 9) / 5 + 32).ToString("0.0")))
                .ForCtorParam(ctorParamName: "WindSpeedInMph", m => m.MapFrom(s => (0.6214 * s.WindSpeedInKmh)));
        }
    }
}
