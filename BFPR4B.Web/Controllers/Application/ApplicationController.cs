using BFPR4B.Web._keenthemes.libs;
using BFPR4B.Web.Controllers.Course;
using BFPR4B.Web.Controllers.Location;
using BFPR4B.Web.Services.IServices.Application;
using BFPR4B.Web.Services.IServices.Course;
using BFPR4B.Web.Utility;
using Microsoft.AspNetCore.Mvc;

namespace BFPR4B.Web.Controllers.Application
{
	public class ApplicationController : Controller
	{
		private readonly ILogger<ApplicationController> _logger;
		private readonly IKTTheme _theme;
		private readonly IApplicationService _applicationService;
		private readonly AccessTokenValidator _accessTokenValidator;

		public ApplicationController(AccessTokenValidator accessTokenValidator, IApplicationService applicationService, ILogger<ApplicationController> logger, IKTTheme theme)
		{
			_accessTokenValidator = accessTokenValidator;
			_applicationService = applicationService;
			_theme = theme;
		}

		[HttpGet("/application")]
		[ServiceFilter(typeof(AccessTokenAuthorizationFilter))] // Apply the custom filter here
		public IActionResult Index()
		{
			return View(_theme.GetPageView("Application", "Index.cshtml"));
		}




	}
}
