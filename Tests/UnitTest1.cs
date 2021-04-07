using System;
using System.Linq;
using KitchenWeb.Controllers;
using Service.Logic;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var weather = new WeatherForecastController();
            var weat =  weather.Get();
            Assert.True(weat.Count() > 0);

        }
    }
}
