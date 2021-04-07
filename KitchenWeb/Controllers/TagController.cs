using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Service.Logic;

namespace KitchenWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagController
    {

        private readonly IReviewStepTagLogic iReviewStepTagLogic;

        public TagController(IReviewStepTagLogic iReviewStepTagLogic)
        {
            this.iReviewStepTagLogic = iReviewStepTagLogic;
        }

        [HttpGet]
        public async Task GetTags()
        {
            iReviewStepTagLogic.geTags();
        }
    }
}
