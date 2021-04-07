using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Logic;

namespace KitchenWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController
    {
        public readonly ILogicKitchen iLogicKitchen;

        public RecipeController(ILogicKitchen iLogicKitchen)
        {
            this.iLogicKitchen = iLogicKitchen;
        }
    }
}
