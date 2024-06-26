using DEW.BIS.WCC.WeatherObservation.Shared.Interfaces;
using DEW.BIS.WCC.WeatherObservation.Shared.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace DEW.BIS.WCC.WeatherObservation.Services.Services
{
    public class WeatherObservationService : IWeatherObservationService
    {
        private const string BaseAddress = "http://www.bom.gov.au/fwo/IDS60901/IDS60901.";

        public async Task<WeatherResponse> GetStationWeather(int stationId)
        {
            var result = new WeatherResponse();
            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(BaseAddress + stationId + ".json");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadFromJsonAsync<WeatherResponse>();
                }
                else
                {

                }
            }

            return result;
        }
    }
}
