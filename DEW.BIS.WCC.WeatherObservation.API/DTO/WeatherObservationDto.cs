namespace DEW.BIS.WCC.WeatherObservation.API.DTO
{
    public record WeatherObservationDto(float Temperature, float TemperatureInF, string? StationName, string Cloud, float DewPoint, ushort WindSpeedInKmh, ushort WindSpeedInMph);
}
