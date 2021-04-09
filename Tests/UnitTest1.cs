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
            User user = new User()
            {
                UserId = 10,
                Firstname = "dbdfhbdb",
                Lastname = "Megdfhnfghfgjdini",
                Username = "usfgjfgjfgjer"
            };
            User result = new User();
            User result2 = new User();
           

             using (var context = new InTheKitchenDBContext(testOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var msr = new UserLogic(context);
                result2 =  msr.getUserById(user.UserId);
                
            }

            using (var contex2 = new InTheKitchenDBContext(testOptions))
            {
                 contex2.Database.EnsureCreatedAsync();
                 result =  contex2.Users.Find(user.UserId);
                
                
            }
            Assert.Equal(result2,result);

        }
    }
}
