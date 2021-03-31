using System;
using System.Collections.Generic;
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
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching",
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration configuration;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
        }

        /// <summary>
        /// Getting Weather Forecast for 5 days.
        /// </summary>
        /// <returns>
        /// Enumerable of weatherForecast objects.
        /// </returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var lat = double.Parse(configuration["WeatherForecast:Latitude"]);
            var lon = double.Parse(configuration["WeatherForecast:Longtitude"]);
            var apiKey = configuration["WeatherForecast:ApiKey"];
            var weatherForecasts = FetchWeatherForecasts(lat, lon, apiKey);

            return weatherForecasts.GetRange(1, 5);
        }

        public List<WeatherForecast> FetchWeatherForecasts(double lat, double lon, string apiKey)
        {
            var client = new RestClient($"https://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&exclude=hourly,minutely&appid={apiKey}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            return ConvertResponseToWeatherForecastList(response.Content);
        }

        public List<WeatherForecast> ConvertResponseToWeatherForecastList(string content)
        {
            var json = JObject.Parse(content);
            var dailyArray = json["daily"];
            var weatherForecasts = new List<WeatherForecast>();
            foreach (var item in dailyArray)
            {
                WeatherForecast obj = new WeatherForecast();
                obj.Date = DateTimeConverter.ConvertEpochToDateTime(item.Value<long>("dt"));
                obj.TemperatureK = item["temp"].Value<double>("day");
                obj.Summary = item["weather"][0].Value<string>("main");
                try
                {
                    weatherForecasts.Add(obj);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            return weatherForecasts;
        }
    }
}
