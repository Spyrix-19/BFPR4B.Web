using BFPR4B.Web._keenthemes.libs;
using Microsoft.AspNetCore.Mvc;

namespace BFPR4B.Web.Controllers.Member
{
	public class MemberTrainingController : Controller
	{
		private readonly ILogger<MemberTrainingController> _logger;
		private readonly IKTTheme _theme;

		public MemberTrainingController(ILogger<MemberTrainingController> logger, IKTTheme theme)
		{
			_logger = logger;
			_theme = theme;
		}

		[HttpGet("/membertraining")]
		[ServiceFilter(typeof(AccessTokenAuthorizationFilter))] // Apply the custom filter here
		public IActionResult Index()
		{
			return View(_theme.GetPageView("Member\\Training", "Index.cshtml"));
		}





	}
}
