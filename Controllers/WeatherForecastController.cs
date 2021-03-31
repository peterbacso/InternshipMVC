using System;
using System.Collections.Generic;
using System.Linq;
using InternshipMvc.Utilities;
using Microsoft.AspNetCore.Mvc;
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Getting Weather Forecast for 5 days.
        /// </summary>
        /// <returns>
        /// weatherForecast.
        /// </returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureK = rng.Next(-20, 55) + 273.15,
                Summary = Summaries[rng.Next(Summaries.Length)],
            })
            .ToArray();
        }

        public IList<WeatherForecast> FetchWeatherForecasts(double lat, double lon, string apiKey)
        {
            var client = new RestClient($"https://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&exclude=hourly,minutely&appid={apiKey}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return ConvertResponseToWeatherForecastList(response.Content);
        }

        private IList<WeatherForecast> ConvertResponseToWeatherForecastList(string content)
        {
            var json = JObject.Parse(content);
            var dailyArray = json["daily"];
            Console.WriteLine(dailyArray);
            IList<WeatherForecast> weatherForecasts = new List<WeatherForecast>();
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
