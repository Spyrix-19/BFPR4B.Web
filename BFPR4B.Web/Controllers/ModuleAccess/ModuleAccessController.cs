using BFPR4B.Web._keenthemes.libs;
using BFPR4B.Web.Services.IServices.ModuleAccess;
using Microsoft.AspNetCore.Mvc;

namespace BFPR4B.Web.Controllers.ModuleAccess
{
	public class ModuleAccessController : Controller
	{
		private readonly ILogger<ModuleAccessController> _logger;
		private readonly IKTTheme _theme;
		private readonly IModuleAccessService _moduleaccessService;

		public ModuleAccessController(IModuleAccessService moduleaccessService, ILogger<ModuleAccessController> logger, IKTTheme theme)
		{
			_moduleaccessService = moduleaccessService;
			//_mapper = mapper;
			_logger = logger;
			_theme = theme;
		}

		[HttpGet("/moduleaccess")]
		[ServiceFilter(typeof(AccessTokenAuthorizationFilter))] // Apply the custom filter here
		public async Task<IActionResult> Index()
		{
			return View(_theme.GetPageView("ModuleAccess", "Index.cshtml"));
		}







	}
}
