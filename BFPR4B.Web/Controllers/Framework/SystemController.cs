using BFPR4B.Web._keenthemes.libs;
using BFPR4B.Web.Models.Model.Gentable;
using BFPR4B.Web.Models.Model.Location;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Framework;
using BFPR4B.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

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
		public async Task<IActionResult> GetGentableDropdown(string tablename = "")
		{
			try
			{
				if (ModelState.IsValid)
				{
					GentableModel list = new GentableModel();

					var _response = await _systemService.GetGentables<APIResponse>("", tablename, 0, 0);

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

				return BadRequest(new { IsSuccess = false, ErrorMessages = Settings.INVALID_MODEL_ERR_MSG });
			}
			catch (Exception exception)
			{
				return Json(new { error = Convert.ToInt32(HttpStatusCode.InternalServerError), message = Settings.UNKNOWN_ERR_MSG });
			}			
		}

		[HttpGet("system/getbarangay")]
		public async Task<IActionResult> GetBarangayDropdown(int cityno = 0, int provinceno = 0, int regionno = 0)
		{
			try
			{
				if (ModelState.IsValid)
				{
					BarangayModel list = new BarangayModel();

					var _response = await _systemService.GetBarangay<APIResponse>("", cityno, provinceno, regionno);

					if (_response != null && _response.IsSuccess)
					{
						var options = JsonConvert.DeserializeObject<List<BarangayModel>>(Convert.ToString(_response.Result));

						// Extract the relevant data for the dropdown options
						var dropdownOptions = options.Select(option => new
						{
							value = option.Barangayno,
							text = option.Barangayname
						});

						return Ok(dropdownOptions);
					}

					return BadRequest(new { IsSuccess = false, ErrorMessages = Settings.API_ERR_MSG });
				}

				return BadRequest(new { IsSuccess = false, ErrorMessages = Settings.INVALID_MODEL_ERR_MSG });
			}
			catch (Exception exception)
			{
				return Json(new { error = Convert.ToInt32(HttpStatusCode.InternalServerError), message = Settings.UNKNOWN_ERR_MSG });
			}
		}

		[HttpGet("system/getcity")]
		public async Task<IActionResult> GetCityDropdown(int provinceno = 0, int regionno = 0)
		{
			try
			{
				if (ModelState.IsValid)
				{
					CityModel list = new CityModel();

					var _response = await _systemService.GetCity<APIResponse>("", provinceno, regionno);

					if (_response != null && _response.IsSuccess)
					{
						var options = JsonConvert.DeserializeObject<List<CityModel>>(Convert.ToString(_response.Result));

						// Extract the relevant data for the dropdown options
						var dropdownOptions = options.Select(option => new
						{
							value = option.Cityno,
							text = option.Cityname
						});

						return Ok(dropdownOptions);
					}

					return BadRequest(new { IsSuccess = false, ErrorMessages = Settings.API_ERR_MSG });
				}

				return BadRequest(new { IsSuccess = false, ErrorMessages = Settings.INVALID_MODEL_ERR_MSG });
			}
			catch (Exception exception)
			{
				return Json(new { error = Convert.ToInt32(HttpStatusCode.InternalServerError), message = Settings.UNKNOWN_ERR_MSG });
			}
		}

		[HttpGet("system/getprovince")]
		public async Task<IActionResult> GetProvinceDropdown(int regionno = 0)
		{
			try
			{
				if (ModelState.IsValid)
				{
					ProvinceModel list = new ProvinceModel();

					var _response = await _systemService.GetProvince<APIResponse>("", regionno);

					if (_response != null && _response.IsSuccess)
					{
						var options = JsonConvert.DeserializeObject<List<ProvinceModel>>(Convert.ToString(_response.Result));

						// Extract the relevant data for the dropdown options
						var dropdownOptions = options.Select(option => new
						{
							value = option.Provinceno,
							text = option.Provincename
						});

						return Ok(dropdownOptions);
					}

					return BadRequest(new { IsSuccess = false, ErrorMessages = Settings.API_ERR_MSG });
				}

				return BadRequest(new { IsSuccess = false, ErrorMessages = Settings.INVALID_MODEL_ERR_MSG });
			}
			catch (Exception exception)
			{
				return Json(new { error = Convert.ToInt32(HttpStatusCode.InternalServerError), message = Settings.UNKNOWN_ERR_MSG });
			}
		}

		[HttpGet("system/getregion")]
		public async Task<IActionResult> GetRegionDropdown()
		{
			try
			{
				if (ModelState.IsValid)
				{
					RegionModel list = new RegionModel();

					var _response = await _systemService.GetRegion<APIResponse>("");

					if (_response != null && _response.IsSuccess)
					{
						var options = JsonConvert.DeserializeObject<List<RegionModel>>(Convert.ToString(_response.Result));

						// Extract the relevant data for the dropdown options
						var dropdownOptions = options.Select(option => new
						{
							value = option.Regionno,
							text = option.Regionname
						});

						return Ok(dropdownOptions);
					}

					return BadRequest(new { IsSuccess = false, ErrorMessages = Settings.API_ERR_MSG });
				}

				return BadRequest(new { IsSuccess = false, ErrorMessages = Settings.INVALID_MODEL_ERR_MSG });
			}
			catch (Exception exception)
			{
				return Json(new { error = Convert.ToInt32(HttpStatusCode.InternalServerError), message = Settings.UNKNOWN_ERR_MSG });
			}
		}

	}
}
