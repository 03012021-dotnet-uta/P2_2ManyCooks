using System;
using System.Linq;
using KitchenWeb.Controllers;
using Repository.Models;
using Service.Logic;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var weather = new WeatherForecastController(new TestLogic(new InTheKitchenDBContext()));
            var weat =  weather.Get();
            Assert.True(weat.Count() > 0);

        }
    }
}
