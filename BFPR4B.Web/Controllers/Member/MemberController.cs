using BFPR4B.Web._keenthemes.libs;
using BFPR4B.Web.Controllers.Dashboard;
using Microsoft.AspNetCore.Mvc;

namespace BFPR4B.Web.Controllers.Member
{
	public class MemberController : Controller
	{
		private readonly ILogger<MemberController> _logger;
		private readonly IKTTheme _theme;

		public MemberController(ILogger<MemberController> logger, IKTTheme theme)
		{
			_logger = logger;
			_theme = theme;
		}

		[HttpGet("/member")]
		[ServiceFilter(typeof(AccessTokenAuthorizationFilter))] // Apply the custom filter here
		public IActionResult Index()
		{
			return View(_theme.GetPageView("Member\\Member", "Index.cshtml"));
		}


	}
}
