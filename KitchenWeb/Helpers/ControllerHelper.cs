using System.Linq;

namespace KitchenWeb.Helpers
{
    class ControllerHelper
    {
        public static string GetTokenFromRequest(Microsoft.AspNetCore.Http.HttpRequest request)
        {
            return request.Headers.Where(h => h.Key == "Authorization").FirstOrDefault().Value;
        }
    }
}