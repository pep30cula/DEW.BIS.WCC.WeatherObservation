using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEW.BIS.WCC.WeatherObservation
{
    public class ConsoleHelper
    {
        public static void WriteToConsole(string message, ConsoleColor backgroundColor = ConsoleColor.Black, ConsoleColor foreColor = ConsoleColor.White)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foreColor;
            Console.Write(message);
            Console.ResetColor();
        }
    }
}
