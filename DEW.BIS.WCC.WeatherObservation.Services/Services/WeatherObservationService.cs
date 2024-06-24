using DEW.BIS.WCC.WeatherObservation.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DEW.BIS.WCC.WeatherObservation.Services.Services
{
    public class WeatherObservationService
    {
        private const string BaseAddress = "http://www.bom.gov.au/fwo/IDS60901/IDS60901.";

        public async Task<Observations> GetStationWeather(int stationId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(BaseAddress + stationId + ".json");
                if (response.IsSuccessStatusCode)
                {
                    var obj = await response.Content.ReadFromJsonAsync<WeatherResponse>();
                }
                else
                {
                    
                }
            }



            return null;
        }
    }
}
