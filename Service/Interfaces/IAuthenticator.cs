using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.LogicModels;
using Repository.Models;

namespace Service.Interfaces
{
    public interface IAuthenticator
    {
        // bool CheckIfNewUser(string token);
        Task<Dictionary<string, string>> GetUserAuth0Dictionary(string token);

        // AuthModel GetCurrentUserData(string sub);

        // string GetTokenFromRequest(dynamic requet);
    }
}
