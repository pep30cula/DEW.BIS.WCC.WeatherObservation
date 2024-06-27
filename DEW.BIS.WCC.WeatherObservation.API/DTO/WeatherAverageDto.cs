using DEW.BIS.WCC.WeatherObservation.Services;

namespace DEW.BIS.WCC.WeatherObservation.API.DTO
{
    public record WeatherAverageDto(float AverageTemperature, string? StationName, DateTime LastUpdateDateTime, TemperatureDegreeType TemperatureDegreeType);
}
