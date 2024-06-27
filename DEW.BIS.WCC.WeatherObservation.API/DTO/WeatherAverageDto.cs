using DEW.BIS.WCC.WeatherObservation.Shared;

namespace DEW.BIS.WCC.WeatherObservation.API.DTO
{
    public record WeatherAverageDto(float AverageTemperature, string? StationName, DateTime LastUpdateDateTime, TemperatureUnitType TemperatureUnitType);
}
