using BFPR4B.Web._keenthemes.libs;
using Microsoft.AspNetCore.Mvc;

namespace BFPR4B.Web.Controllers.Member
{
	public class MemberEligibilityController : Controller
	{
		private readonly ILogger<MemberEligibilityController> _logger;
		private readonly IKTTheme _theme;

		public MemberEligibilityController(ILogger<MemberEligibilityController> logger, IKTTheme theme)
		{
			_logger = logger;
			_theme = theme;
		}

		[HttpGet("/membereligibility")]
		[ServiceFilter(typeof(AccessTokenAuthorizationFilter))] // Apply the custom filter here
		public IActionResult Index()
		{
			return View(_theme.GetPageView("Member\\Eligibility", "Index.cshtml"));
		}




	}
}
