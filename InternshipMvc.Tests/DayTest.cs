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
            var weatherForecasts = weatherForecastController.FetchWeatherForecasts();
            WeatherForecast weatherForecastForTomorrow = weatherForecasts[1];

            // Assert
            Assert.Equal(285.39, weatherForecastForTomorrow.TemperatureK);

        }

        [Fact]
        public void ConvertWeatherJsonToWeatherForecast()
        {
            // Assume
            string content = GetStreamContent();
            WeatherForecastController weatherForecastController = InstantiateWeatherForecastController();

            // Act
            var weatherForecasts = weatherForecastController.ConvertResponseToWeatherForecastList(content);
            WeatherForecast weatherForecastForTommorow = weatherForecasts[1];

            // Assert
            Assert.Equal(285.39, weatherForecastForTommorow.TemperatureK);
        }

        private string GetStreamContent()
        {
            var assembly = this.GetType().Assembly;
            var stream = assembly.GetManifestResourceStream("InternshipMvc.Tests.weatherForecast.json");
            StreamReader streamReader = new StreamReader(stream);

            var streamContent = "";

            while (!streamReader.EndOfStream)
            {
                streamContent += streamReader.ReadLine();
            }

            streamReader.Close();
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
