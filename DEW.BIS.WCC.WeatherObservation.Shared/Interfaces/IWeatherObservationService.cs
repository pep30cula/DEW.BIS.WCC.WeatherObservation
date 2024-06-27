using DEW.BIS.WCC.WeatherObservation.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEW.BIS.WCC.WeatherObservation.Shared.Interfaces
{
    public interface IWeatherObservationService
    {
        Task<WeatherObservationResponse> GetStationWeather(int stationId);
    }
}
