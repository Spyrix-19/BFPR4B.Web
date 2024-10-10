using BFPR4B.Web._keenthemes.libs;
using BFPR4B.Web.Models.Model.Gentable;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Framework;
using BFPR4B.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BFPR4B.Web.Controllers.Framework
{
	public class SystemController : Controller
	{
		private readonly IKTTheme _theme;
		private readonly ISystemService _systemService;
		private readonly AccessTokenValidator _accessTokenValidator;

		public SystemController(AccessTokenValidator accessTokenValidator, ISystemService systemService, IKTTheme theme)
		{
			_accessTokenValidator = accessTokenValidator;
			_systemService = systemService;
			_theme = theme;
		}

		[HttpGet("/notfound")]
		public IActionResult PageNotFound()
		{
			return View(_theme.GetPageView("System", "NotFound.cshtml"));
		}

		[HttpGet("/error")]
		public IActionResult Error()
		{
			return View(_theme.GetPageView("System", "Error.cshtml"));
		}

		[HttpGet("/accessdenied")]
		public IActionResult AccessDenied()
		{
			return View(_theme.GetPageView("System", "AccessDenied.cshtml"));
		}

		[HttpGet("system/gentables")]
		public async Task<IActionResult> GetGentableDropdownOptions(string tablename = "")
		{
			var _data = _accessTokenValidator.ValidateAccessToken(HttpContext);

			if (_data.Item1)
			{
				GentableModel list = new GentableModel();

				if (string.IsNullOrEmpty(_data.Item2))
				{
					return Unauthorized(new { IsSuccess = false, ErrorMessages = Settings.INVALID_SESSION_ERR_MSG });
				}

				var _response = await _systemService.GetGentables<APIResponse>("", tablename, 0, 0, _data.Item2);

				if (_response != null && _response.IsSuccess)
				{
					var options = JsonConvert.DeserializeObject<List<GentableModel>>(Convert.ToString(_response.Result));

					// Extract the relevant data for the dropdown options
					var dropdownOptions = options.Select(option => new
					{
						value = option.Detno,
						text = option.Description
					});

					return Ok(dropdownOptions);
				}

				return BadRequest(new { IsSuccess = false, ErrorMessages = Settings.API_ERR_MSG });
			}
			else
			{
				return Unauthorized(new { IsSuccess = false, ErrorMessages = Settings.INVALID_SESSION_ERR_MSG });
			}


			

			return BadRequest(new { IsSuccess = false, ErrorMessages = Settings.API_ERR_MSG });
		}



	}
}
