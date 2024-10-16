using BFPR4B.Web._keenthemes.libs;
using Microsoft.AspNetCore.Mvc;

namespace BFPR4B.Web.Controllers.Member
{
	public class MemberEducationController : Controller
	{
		private readonly ILogger<MemberEducationController> _logger;
		private readonly IKTTheme _theme;

		public MemberEducationController(ILogger<MemberEducationController> logger, IKTTheme theme)
		{
			_logger = logger;
			_theme = theme;
		}

		[HttpGet("/membereducation")]
		[ServiceFilter(typeof(AccessTokenAuthorizationFilter))] // Apply the custom filter here
		public IActionResult Index()
		{
			return View(_theme.GetPageView("Member\\Dependent", "Index.cshtml"));
		}




	}
}
