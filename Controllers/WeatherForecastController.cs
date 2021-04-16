using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using InternshipMvc.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace InternshipMvc.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly double latitude;
        private readonly double longtitude;
        private readonly string apiKey;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;

            this.latitude = double.Parse(configuration["WeatherForecast:Latitude"], CultureInfo.InvariantCulture);
            this.longtitude = double.Parse(configuration["WeatherForecast:Longtitude"], CultureInfo.InvariantCulture);
            this.apiKey = configuration["WeatherForecast:ApiKey"];
        }

        /// <summary>
        /// Getting Weather Forecast for 5 days for default location.
        /// </summary>
        /// <returns>
        /// Enumerable of weatherForecast objects.
        /// </returns>
        [HttpGet]
        public List<WeatherForecast> Get()
        {
            var weatherForecasts = Get(this.latitude, this.longtitude);

            return weatherForecasts.GetRange(1, 5);
        }

        [HttpGet ("/forecast")]
        public List<WeatherForecast> Get(double latitude, double longtitude)
        {
            var client = new RestClient($"https://api.openweathermap.org/data/2.5/onecall?lat={latitude}&lon={longtitude}&exclude=hourly,minutely&appid={apiKey}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            return ConvertResponseToWeatherForecastList(response.Content);
        }

        [NonAction]
        public List<WeatherForecast> ConvertResponseToWeatherForecastList(string content)
        {
            var json = JObject.Parse(content);
            var dailyArray = json["daily"];
            var weatherForecasts = new List<WeatherForecast>();
            foreach (var item in dailyArray)
            {
                weatherForecasts.Add(new WeatherForecast
                {
                    Date = DateTimeConverter.ConvertEpochToDateTime(item.Value<long>("dt")),
                    TemperatureK = item["temp"].Value<double>("day"),
                    Summary = item["weather"][0].Value<string>("main"),
                });
            }

            return weatherForecasts;
        }
    }
}
