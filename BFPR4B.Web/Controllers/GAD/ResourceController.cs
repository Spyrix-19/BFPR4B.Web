using BFPR4B.Web._keenthemes.libs;
using BFPR4B.Web.Controllers.Eligibility;
using BFPR4B.Web.Services.IServices.Eligibility;
using BFPR4B.Web.Utility;
using Microsoft.AspNetCore.Mvc;

namespace BFPR4B.Web.Controllers.GAD
{
	public class ResourceController : Controller
	{
		private readonly ILogger<ResourceController> _logger;
		private readonly IKTTheme _theme;
		private readonly IEligibilityService _eligibilityService;
		private readonly AccessTokenValidator _accessTokenValidator;

		public ResourceController(AccessTokenValidator accessTokenValidator, IEligibilityService eligibilityService, ILogger<ResourceController> logger, IKTTheme theme)
		{
			_accessTokenValidator = accessTokenValidator;
			_eligibilityService = eligibilityService;
			_logger = logger;
			_theme = theme;
		}

		[HttpGet("/gadresource")]
		[ServiceFilter(typeof(AccessTokenAuthorizationFilter))] // Apply the custom filter here
		public IActionResult Index()
		{
			return View(_theme.GetPageView("GAD\\Resource", "Index.cshtml"));
		}





	}
}
