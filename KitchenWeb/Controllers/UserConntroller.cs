using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        //[httpget]
        //public ienumerable<user> get()
        //{
        //    var rng = new random();
        //    return enumerable.range(1, 5).select(index => new user
        //    {
        //        date = datetime.now.adddays(index),
        //        temperaturec = rng.next(-20, 55),
        //        summary = summaries[rng.next(summaries.length)]
        //    })
        //    .toarray();
        //}

    }
}
