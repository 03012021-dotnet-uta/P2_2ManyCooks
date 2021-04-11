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
using System.Security.Cryptography.X509Certificates;
using Service.Interfaces;
using Models.LogicModels;

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
            // System.Console.WriteLine("the request headers:");
            // System.Console.WriteLine(tok.Value);
            // var x = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // System.Console.WriteLine("whwat is this?");
            // System.Console.WriteLine(x);
            return iUserLogic.getAllUsers();
            // return true;
        }

        // [HttpGet("{id}")]
        // [Authorize]
        // public ActionResult<User> GetUser(int id)
        // {
        //     return iUserLogic.GetUserData(id);
        // }

        [HttpPut]
        public ActionResult<AuthModel> UpdateUser(AuthModel model)
        {
            System.Console.WriteLine("authmodel recieved in controller:");
            System.Console.WriteLine(model.FirstName);
            System.Console.WriteLine(model.LastName);
            var tok = this.Request.Headers.Where(h => h.Key == "Authorization").FirstOrDefault();
            var dic = _authenticator.GetUserAuth0Dictionary(tok.Value);
            return iUserLogic.UpdateUser(model, dic);
        }

        [HttpGet("myinfo")]
        [Authorize]
        public ActionResult<AuthModel> GetCurrentUser()
        {
            string sub = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var tok = this.Request.Headers.Where(h => h.Key == "Authorization").FirstOrDefault();
            var dictionary = _authenticator.GetUserAuth0Dictionary(tok.Value);
            var model = new AuthModel();
            iUserLogic.CheckIfNewUser(dictionary, out model);
            return model;

        }
    }
}
