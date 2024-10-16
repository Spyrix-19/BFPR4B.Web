using BFPR4B.Web._keenthemes.libs;
using Microsoft.AspNetCore.Mvc;

namespace BFPR4B.Web.Controllers.Member
{
	public class MemberLeaveController : Controller
	{
		private readonly ILogger<MemberLeaveController> _logger;
		private readonly IKTTheme _theme;

		public MemberLeaveController(ILogger<MemberLeaveController> logger, IKTTheme theme)
		{
			_logger = logger;
			_theme = theme;
		}

		[HttpGet("/memberleaverecord")]
		[ServiceFilter(typeof(AccessTokenAuthorizationFilter))] // Apply the custom filter here
		public IActionResult Index()
		{
			return View(_theme.GetPageView("Member\\LeaveRecord", "Index.cshtml"));
		}





	}
}
