using BFPR4B.Web._keenthemes.libs;
using Microsoft.AspNetCore.Mvc;

namespace BFPR4B.Web.Controllers.Dashboard
{
    public class DashboardController : Controller
    {
        private readonly IKTTheme _theme;

        public DashboardController(IKTTheme theme)
        {
            _theme = theme;
        }


        //[Authorize(Policy = "ValidAccessToken")]
        [HttpGet("/dashboard")]
        [ServiceFilter(typeof(AccessTokenAuthorizationFilter))] // Apply the custom filter here
        public IActionResult Index()
        {
            //string accessToken = GetAccessTokenFromRequest(HttpContext);

            return View(_theme.GetPageView("Dashboard", "Index.cshtml"));
        }
    }
}
