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
    public class StepController
    {
        public readonly IReviewStepTagLogic iReviewStepTagLogic;

        public StepController(IReviewStepTagLogic iReviewStepTagLogic)
        {
            this.iReviewStepTagLogic = iReviewStepTagLogic;
        }
    }
}
