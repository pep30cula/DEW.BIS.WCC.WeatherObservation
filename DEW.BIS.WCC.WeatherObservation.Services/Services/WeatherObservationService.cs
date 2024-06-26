﻿using DEW.BIS.WCC.WeatherObservation.Shared.Interfaces;
using DEW.BIS.WCC.WeatherObservation.Shared.Models;
using DEW.BIS.WCC.WeatherObservation.Shared.Settings;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;

namespace DEW.BIS.WCC.WeatherObservation.Services.Services
{
    public class WeatherObservationService : IWeatherObservationService
    {
        private readonly IOptions<BaseAddressSettings> _baseAddressSettings;

        public WeatherObservationService(IOptions<BaseAddressSettings> baseAddressSettings)
        {
            _baseAddressSettings = baseAddressSettings;
        }

        public async Task<WeatherObservationResponse> GetStationWeather(int stationId)
        {
            var result = new WeatherObservationResponse();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(_baseAddressSettings.Value.WeatherService + stationId + ".json");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<WeatherObservationResponse>();
                }
                else
                {
                    throw new Exception("The HTTP request was not successful.");
                }
            }

            return result;
        }
    }
}
