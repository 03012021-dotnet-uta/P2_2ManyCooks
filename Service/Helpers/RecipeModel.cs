using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers
{
    public class RecipeModel
    {
        public int total_hits { get; set; }
        public double max_score { get; set; }
        public List<dynamic> hits { get; set; }

    }
}
