using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Service.Logic
{
    public interface IUserLogic {
        List<User> getAUsers();
        User getUserById(int id);

        Task<User> addUser(User user);
    }
}
