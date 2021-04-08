using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Service.Helpers
{
    public class RecipeProcessor {
        public static async Task<Recipe> LoadRecipe()
        {
            
            var url = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://nutritionix-api.p.rapidapi.com/v1_1/search/cheddar%20cheese?fields=item_name%2Citem_id%2Cbrand_name%2Cnf_calories%2Cnf_total_fat"),
                Headers =
                {
                    { "x-rapidapi-key", "e157b8d687msh431e30623e70dd3p174a1cjsn7ea0d090c0f9" },
                    { "x-rapidapi-host", "nutritionix-api.p.rapidapi.com" },
                },
            };
            using (HttpResponseMessage response = await ApiHelper.ApiClient.SendAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    Recipe recipe = await response.Content.ReadFromJsonAsync<Recipe>();

                    return recipe;
                }

                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
