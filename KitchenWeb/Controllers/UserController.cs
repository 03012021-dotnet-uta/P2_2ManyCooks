using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository.Models;
using Service.Logic;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Service.Interfaces;

namespace KitchenWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public readonly IUserLogic iUserLogic;
        private readonly IAuthenticator _authenticator;
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger, IUserLogic iUserLogic, IAuthenticator _authenticator)
        {
            _logger = logger;
            this.iUserLogic = iUserLogic;
            this._authenticator = _authenticator;
        }


        [HttpGet]
        [Authorize]
        public List<User> getList()
        {
            System.Console.WriteLine("the request headers:");
            var tok = this.Request.Headers.Where(h => h.Key == "Authorization").FirstOrDefault();
            System.Console.WriteLine(tok.Value);
            var x = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            System.Console.WriteLine("whwat is this?");
            System.Console.WriteLine(x);
            _authenticator.CheckIfNewUser(tok.Value);

            return iUserLogic.getAUsers();

            // return true;
        }
    }
}
