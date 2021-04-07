using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Service.Logic
{
    public class UserLogic : IUserLogic {

        private InTheKitchenDBContext _context;
        public UserLogic(InTheKitchenDBContext _context)
        {
            this._context = _context;
        }

        public async Task<List<User>> getAUsers()
        {
            return await _context.Users.FromSqlRaw("Select * From Users").ToListAsync();
            
        }
    }
}
