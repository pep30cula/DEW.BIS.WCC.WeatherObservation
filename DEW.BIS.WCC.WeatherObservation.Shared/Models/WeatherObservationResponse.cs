using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DEW.BIS.WCC.WeatherObservation.Shared.Models
{
    public class WeatherObservationResponse
    {
        [JsonPropertyName("air_temp")]
        public float Temperature { get; set; }

        [JsonPropertyName("name")]
        public string? StationName { get; set; }   

        [JsonPropertyName("local_date_time_full")]
        public string? LocalDateTime { get; set; }

        [JsonPropertyName("lat")]
        public float Latitude { get; set; }

        [JsonPropertyName("lon")]
        public float Longitude { get; set; }

        [JsonPropertyName("cloud")]
        public string? Cloud { get; set; }

        [JsonPropertyName("dewpt")]
        public float DewPoint { get; set; }

        [JsonPropertyName("vis_km")]
        public string? VisibilityInKm { get; set; }


        [JsonPropertyName("wind_dir")]
        public string? WindDirection { get; set; }

        [JsonPropertyName("wind_spd_kmh")]
        public ushort WindSpeedInKmh { get; set; }
    }
}
