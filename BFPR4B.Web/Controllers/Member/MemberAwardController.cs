using BFPR4B.Web._keenthemes.libs;
using Microsoft.AspNetCore.Mvc;

namespace BFPR4B.Web.Controllers.Member
{
	public class MemberAwardController : Controller
	{
		private readonly ILogger<MemberAwardController> _logger;
		private readonly IKTTheme _theme;

		public MemberAwardController(ILogger<MemberAwardController> logger, IKTTheme theme)
		{
			_logger = logger;
			_theme = theme;
		}

		[HttpGet("/memberaward")]
		[ServiceFilter(typeof(AccessTokenAuthorizationFilter))] // Apply the custom filter here
		public IActionResult Index()
		{
			return View(_theme.GetPageView("Member\\Award", "Index.cshtml"));
		}



	}
}
