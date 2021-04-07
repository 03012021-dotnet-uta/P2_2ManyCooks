﻿using System;
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
    public class IngredientController {
        public readonly IReviewStepTagLogic iReviewStepTagLogic;

        public IngredientController(IReviewStepTagLogic iReviewStepTagLogic)
        {
            this.iReviewStepTagLogic = iReviewStepTagLogic;
        }


        [HttpGet]
        public List<Ingredient> GetIngredients()
        {
            return iReviewStepTagLogic.getIngredients();
        }
    }
}