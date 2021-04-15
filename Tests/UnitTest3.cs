using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Xunit;

namespace Tests
{

    class UnitTest3
    {
        DbContextOptions<InTheKitchenDBContext> testOptions = new DbContextOptionsBuilder<InTheKitchenDBContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        
    }
}
