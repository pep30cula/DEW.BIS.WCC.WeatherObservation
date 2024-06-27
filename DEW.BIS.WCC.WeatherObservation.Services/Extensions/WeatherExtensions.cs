using DEW.BIS.WCC.WeatherObservation.Shared;
using DEW.BIS.WCC.WeatherObservation.Shared.Models;

namespace DEW.BIS.WCC.WeatherObservation.Services.Extensions
{
    public static class WeatherExtensions
    {
        public static float CalculateThreeDaysWeatherAverage(this List<ObservationData> input, TemperatureUnitType temperatureDegreeType)
        {
            var averageTemperature = Convert.ToSingle(input?.Average(x => x.Temperature).ToString("0.0"));

            if (temperatureDegreeType == TemperatureUnitType.Fahrenheit)
            {
                averageTemperature = averageTemperature.ConvertCelsiusToFahrenheit();
            }

            return averageTemperature;
        }

        public static float ConvertCelsiusToFahrenheit(this float input)
        {
            return Convert.ToSingle(((input * 9) / 5 + 32).ToString("0.0"));
        }

        public static float ConvertFahrenheitToCelsius(this float input)
        {
            return Convert.ToSingle(((input - 32) * 5 / 9).ToString("0.0"));
        }

        public static ushort ConvertKmhToMph(this ushort input)
        {
            return Convert.ToUInt16((0.6214 * input));
        }
    }
}
