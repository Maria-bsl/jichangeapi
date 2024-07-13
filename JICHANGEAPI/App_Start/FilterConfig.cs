using JichangeApi.Models.JWTAuthentication;
using System.Web;
using System.Web.Mvc;

namespace JichangeApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
