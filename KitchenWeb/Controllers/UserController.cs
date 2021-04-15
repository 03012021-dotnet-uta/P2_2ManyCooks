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
using KitchenWeb.Helpers;

namespace KitchenWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public readonly IUserLogic iUserLogic;
        private readonly IAuthenticator _authenticator;

        public UserController(IUserLogic iUserLogic, IAuthenticator _authenticator)
        {
            this.iUserLogic = iUserLogic;
            this._authenticator = _authenticator;
        }



        // [HttpGet("{id}")]
        // [Authorize]
        // public ActionResult<User> GetUser(int id)
        // {
        //     return iUserLogic.GetUserData(id);
        // }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<AuthModel>> UpdateUser(AuthModel model)
        {
            System.Console.WriteLine("authmodel recieved in controller:");
            System.Console.WriteLine(model.FirstName);
            System.Console.WriteLine(model.LastName);
            // var tok = this.Request.Headers.Where(h => h.Key == "Authorization").FirstOrDefault();
            var tok = ControllerHelper.GetTokenFromRequest(this.Request);
            var dic = await _authenticator.GetUserAuth0Dictionary(tok);
            return iUserLogic.UpdateUser(model, dic);
        }

        [HttpGet("myinfo")]
        [Authorize]
        public async Task<ActionResult<AuthModel>> GetCurrentUser()
        {
            string sub = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // var tok = this.Request.Headers.Where(h => h.Key == "Authorization").FirstOrDefault();
            // var dictionary = _authenticator.GetUserAuth0Dictionary(tok.Value);
            this.User.Claims.ToList().ForEach(c =>
            {
                System.Console.WriteLine("claim: " + c);
            });
            var tok = ControllerHelper.GetTokenFromRequest(this.Request);
            var dic = await _authenticator.GetUserAuth0Dictionary(tok);
            var model = new AuthModel();
            iUserLogic.CheckIfNewUser(dic, out model);
            return model;

        }

        [HttpGet("isadmin")]
        [Authorize("update:website")]
        // [Authorize]
        public async Task<ActionResult<Boolean>> GetUserAdmin()
        {
            System.Console.WriteLine("in getuseradmin");
            string sub = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // var tok = this.Request.Headers.Where(h => h.Key == "Authorization").FirstOrDefault();
            // var dictionary = _authenticator.GetUserAuth0Dictionary(tok.Value);

            var tok = ControllerHelper.GetTokenFromRequest(this.Request);
            var dic = await _authenticator.GetUserAuth0Dictionary(tok);
            // var model = new AuthModel();
            // iUserLogic.CheckIfNewUser(dic, out model);
            return true;

        }

        [HttpGet]
        [Authorize("update:website")]
        // [Authorize]
        public async Task<List<AuthModel>> getList()
        {
            return await iUserLogic.GetAllUsers();
        }


        [HttpDelete("{sub}")]
        [Authorize("update:website")]
        // [Authorize]
        public async Task<List<AuthModel>> DeleteUser(string sub)
        {
            var token = ControllerHelper.GetTokenFromRequest(this.Request);
            return await iUserLogic.DeleteUser(token, sub);
        }
    }
}
