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
    }
}