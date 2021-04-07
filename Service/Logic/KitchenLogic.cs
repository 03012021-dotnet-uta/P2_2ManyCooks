using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Service.Logic
{
    public class KitchenLogic : ILogicKitchen
    {
        private InTheKitchenDBContext _context;
        public KitchenLogic(InTheKitchenDBContext _context)
        {
            this._context = _context;
        }
        
      
    }
}