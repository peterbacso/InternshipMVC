using InternshipMvc.Utilities;
using InternshipMvc.WebAPI;
using InternshipMvc.WebAPI.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.IO;
using Xunit;

namespace InternshipMvc.Tests
{
    public class DayTest
    {
        private IConfigurationRoot configuration;

        public DayTest()
        {
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }
        [Fact]
        public void CheckEpochConversion()
        {
            // Assume
            long ticks = 1617184800;

            // Act
            DateTime dateTime = DateTimeConverter.ConvertEpochToDateTime(ticks);

            // Assert
            Assert.Equal(31, dateTime.Day);
            Assert.Equal(03, dateTime.Month);
            Assert.Equal(2021, dateTime.Year);

        }

        [Fact]
        public void ConvertOutputOfWeatherAPIToWeatherForecast()
        {
            // Assume
            WeatherForecastController weatherForecastController = InstantiateWeatherForecastController();

            // Act
            var weatherForecasts = weatherForecastController.Get();
            WeatherForecast weatherForecastForTomorrow = weatherForecasts[1];

            // Assert
            Assert.True(weatherForecastForTomorrow.TemperatureK > 0, "Kelvin temperature cannot be sub 0, please check openweathermap.org.");
        }

        [Fact]
        public void ConvertWeatherJsonToWeatherForecast()
        {
            // Assume
            string content = GetStreamContent("weatherForecast");
            WeatherForecastController weatherForecastController = InstantiateWeatherForecastController();

            // Act
            var weatherForecasts = weatherForecastController.ConvertResponseToWeatherForecastList(content);
            WeatherForecast weatherForecastForTommorow = weatherForecasts[1];

            // Assert
            Assert.Equal(285.39, weatherForecastForTommorow.TemperatureK);
        }

        [Fact]
        public void ShouldHandleJsonErrorFromOpenWeatherApi()
        {
            // Assume
            string content = GetStreamContent("weatherForecast_Exception");
            WeatherForecastController weatherForecastController = InstantiateWeatherForecastController();

            // Act

            // Assert
            Assert.Throws<Exception>(() => weatherForecastController.ConvertResponseToWeatherForecastList(content));
        }

        private string GetStreamContent(string resourceName)
        {
            var assembly = this.GetType().Assembly;
            using var stream = assembly.GetManifestResourceStream($"InternshipMvc.Tests.{resourceName}.json");
            StreamReader streamReader = new StreamReader(stream);

            var streamContent = "";

            while (!streamReader.EndOfStream)
            {
                streamContent += streamReader.ReadLine();
            }

            return streamContent;
        }

        private WeatherForecastController InstantiateWeatherForecastController()
        {
            Microsoft.Extensions.Logging.ILogger<WeatherForecastController> nullLogger = new NullLogger<WeatherForecastController>();
            var weatherForecastController = new WeatherForecastController(nullLogger, configuration);
            return weatherForecastController;
        }
    }
}
