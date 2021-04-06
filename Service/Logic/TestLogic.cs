using System;
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

        public bool TestDBContext()
        {
            User u1 = new User();
            u1.DateCreated = DateTime.Now;
            u1.DateLastAccessed = DateTime.Now;
            u1.Email = "test@email.com";
            u1.Firstname = "fname";
            u1.Lastname = "lname";
            u1.PasswordHash = "asdfasfsadfsdfasdfs";
            u1.PasswordSalt = "asdfsdfasfdsdfsaf";

            _context.Users.Add(u1);
            return _context.SaveChanges() > 0;
        }

    }
}