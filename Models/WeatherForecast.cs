using System;
using System.Collections.Generic;

namespace InternshipMvc.WebAPI
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public double TemperatureC => Math.Round(TemperatureK - 273.15);

        public string Summary { get; set; }

        public double TemperatureK { get; set; }
    }
}
