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

            this.latitude = double.Parse(Environment.GetEnvironmentVariable("LATITUDE") ?? configuration["WeatherForecast:Latitude"], CultureInfo.InvariantCulture);
            this.longtitude = double.Parse(Environment.GetEnvironmentVariable("LONGTITUDE") ?? configuration["WeatherForecast:Longtitude"], CultureInfo.InvariantCulture);
            this.apiKey = Environment.GetEnvironmentVariable("API_KEY") ?? configuration["WeatherForecast:ApiKey"];
        }

        /// <summary>
        /// Getting Weather Forecast for today and 7 days ahead for a specific location.
        /// </summary>
        /// <returns> List of weatherForecast objects. </returns>
        [HttpGet]
        public List<WeatherForecast> Get()
        {
            var weatherForecasts = Get(this.latitude, this.longtitude);

            return weatherForecasts.GetRange(1, 5);
        }

        /// <summary>
        /// Getting Weather Forecast for today and 7 days ahead for a specific location.
        /// </summary>
        /// <param name="latitude"> It should be between -90 and 90. For example: latitude for Brasov is 45.75. </param>
        /// <param name="longtitude"> It should be between -180 and 180. For example: longitude for Brasov is 25.3333. </param>
        /// <returns> List of weatherForecast objects. </returns>
        [HttpGet("/forecast")]
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
            if (dailyArray == null)
            {
                var codToken = json["cod"];
                var messageToken = json["message"];
                throw new Exception($"Weather API is not available. Please check Weather API: {messageToken}");
            }

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
