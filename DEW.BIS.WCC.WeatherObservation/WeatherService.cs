﻿using DEW.BIS.WCC.WeatherObservation.Services.Extensions;
using DEW.BIS.WCC.WeatherObservation.Shared;
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
            ConsoleHelper.WriteToConsole("Using this application you could get the last 3 days average temperature of provided station id of Adelaide area", ConsoleColor.DarkBlue);
            Console.WriteLine();



            while (true)
            {
                try
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
                    var averageTemperature = stationWeather.Observations?.Data?.CalculateThreeDaysWeatherAverage(TemperatureUnitType.Celsius);

                    if (stationWeather?.Observations != null || averageTemperature != null)
                    {
                        ConsoleHelper.WriteToConsole("The average of last 72 hours of station id " + stationId + " of station " + stationWeather?.Observations?.Data[0].StationName + " is: ");
                        ConsoleHelper.WriteToConsole(Convert.ToString(averageTemperature), ConsoleColor.Black, ConsoleColor.DarkCyan);
                    }
                    else
                    {
                        ConsoleHelper.WriteToConsole("There is no result for the provided station id or the station id is not correct.");
                    }

                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    ConsoleHelper.WriteToConsole("There is an error! Please try later.", ConsoleColor.Red);
                    Console.WriteLine();
                    ConsoleHelper.WriteToConsole(ex.Message, ConsoleColor.DarkRed);
                    Console.WriteLine();
                    ConsoleHelper.WriteToConsole(ex.InnerException?.Message, ConsoleColor.DarkRed);
                    Console.WriteLine();
                }
            }


        }
    }
}
