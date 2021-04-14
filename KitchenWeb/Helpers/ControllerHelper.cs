using System.Linq;

namespace KitchenWeb.Helpers
{
    public static class  ControllerHelper
    {
        public static string GetTokenFromRequest(Microsoft.AspNetCore.Http.HttpRequest request)
        {
            return request.Headers.FirstOrDefault(h => h.Key == "Authorization").Value;
        }
    }
}