using System;
using System.Linq;
using System.Threading.Tasks;
using KitchenWeb.Controllers;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Service.Logic;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        DbContextOptions<InTheKitchenDBContext> testOptions = new DbContextOptionsBuilder<InTheKitchenDBContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options; 

 

        [Fact]
        public void Test1()
        {
           

        }
    }
}
