using DEW.BIS.WCC.WeatherObservation.Services;
using DEW.BIS.WCC.WeatherObservation.Services.Extensions;
using DEW.BIS.WCC.WeatherObservation.Shared.Interfaces;

namespace DEW.BIS.WCC.WeatherObservation
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherObservationService _weatherObservationService;
        public WeatherService(IWeatherObservationService weatherObservationService)
        {
            _weatherObservationService = weatherObservationService;
        }

        public async Task GetAverageWeather()
        {
            Console.WriteLine("Using this application you could get the last 3 days average temperature of provided station id of Adelaide area");

            while (true)
            {
                Console.WriteLine("Press any key to continue. Press ESC to leave the application.");
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }

                Console.WriteLine("Please enter a station id or leave it black to get the result of the Adelaide Airport station");
                var inputStationId = Console.ReadLine();
                var stationId = 0;

                while (stationId == 0)
                {
                    if (string.IsNullOrEmpty(inputStationId))
                    {
                        stationId = 94672;
                    }
                    else
                    {
                        if (int.TryParse(inputStationId, out int id))
                        {
                            if (id < 90000 || id > 99999)
                            {
                                Console.WriteLine("The provided station id is not valid! Please try again");
                                inputStationId = Console.ReadLine();
                            }
                            else
                            {
                                stationId = id;
                            }
                        }
                        else
                        {
                            Console.WriteLine("The provided station id is not valid! Please try again");
                            inputStationId = Console.ReadLine();
                        }
                    }
                }

                var stationWeather = await _weatherObservationService.GetStationWeather(stationId);
                var averageTemperature = stationWeather.Observations?.Data?.CalculateThreeDaysWeatherAverage(TemperatureDegreeType.Celsius);

                var resultMessage = (stationWeather.Observations != null) ?
                    "The average of last 72 hours of staion id " + stationId + " of station " + stationWeather?.Observations?.Data[0].StationName + " is: " + averageTemperature :
                    "There is no result for the provided station id or the station id is not correct.";
                Console.WriteLine(resultMessage);
                Console.WriteLine();
            }
        }
    }
}
