﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DEW.BIS.WCC.WeatherObservation.Services.Models
{
    public class WeatherObservation
    {
        [JsonPropertyName("air_temp")]
        public float Temperature { get; set; }
    }
}
