using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Service.Interfaces
{
    public interface IAuthenticator
    {
        bool CheckIfNewUser(string token);
    }
}
