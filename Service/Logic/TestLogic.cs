using System;
using System.Linq;
using Repository.Models;

namespace Service.Logic
{
    public class TestLogic
    {
        private InTheKitchenDBContext _context;
        public TestLogic(InTheKitchenDBContext _context)
        {
            this._context = _context;
        }

        public User TestDBContext()
        {
            User u1 = new User();
            // u1.DateCreated = DateTime.Now;
            // u1.DateLastAccessed = DateTime.UnixEpoch;
            // u1.Email = "test@email.com";
            // u1.Firstname = "fname";
            // u1.Lastname = "lname";
            // u1.PasswordHash = "asdfasfsadfsdfasdfs";
            // u1.PasswordSalt = "asdfsdfasfdsdfsaf";

            // _context.Users.Add(u1);

            var testuser = _context.Users.Where(u => u.Email == "test@email.com").FirstOrDefault();
            return testuser;
        }

    }
}