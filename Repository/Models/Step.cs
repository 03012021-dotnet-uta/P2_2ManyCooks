using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class Step
    {
        public int StepId { get; set; }
        public int RecipeStepNo { get; set; }
        public string StepDescription { get; set; }
        public string StepImage { get; set; }
        public int? RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
