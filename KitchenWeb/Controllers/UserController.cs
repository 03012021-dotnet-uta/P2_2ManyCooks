using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository.Models;
using Service.Logic;

namespace KitchenWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public readonly IUserLogic iUserLogic;
        private readonly  ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger ,IUserLogic iUserLogic)
        {
            _logger = logger;
            this.iUserLogic = iUserLogic;
        }


        [HttpGet]
        public async Task<List<User>> getList() {
            return await iUserLogic.getAUsers();
        }
    }
}
