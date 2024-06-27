using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEW.BIS.WCC.WeatherObservation.Shared.Interfaces
{
    public interface IWeatherService
    {
        Task GetAverageWeather();
    }
}
