using BFPR4B.Web._keenthemes.libs;
using Microsoft.AspNetCore.Mvc;

namespace BFPR4B.Web.Controllers.Member
{
	public class MemberServiceRecordController : Controller
	{
		private readonly ILogger<MemberServiceRecordController> _logger;
		private readonly IKTTheme _theme;

		public MemberServiceRecordController(ILogger<MemberServiceRecordController> logger, IKTTheme theme)
		{
			_logger = logger;
			_theme = theme;
		}

		[HttpGet("/memberservicerecord")]
		[ServiceFilter(typeof(AccessTokenAuthorizationFilter))] // Apply the custom filter here
		public IActionResult Index()
		{
			return View(_theme.GetPageView("Member\\ServiceRecord", "Index.cshtml"));
		}






	}
}
