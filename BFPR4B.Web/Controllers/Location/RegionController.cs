using BFPR4B.Web._keenthemes.libs;
using BFPR4B.Web.Controllers.Dashboard;
using BFPR4B.Web.Models.DTO.Location;
using BFPR4B.Web.Models.Model.Location;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Location;
using BFPR4B.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace BFPR4B.Web.Controllers.Location
{
	public class RegionController : Controller
	{
		private readonly ILogger<RegionController> _logger;
		private readonly IKTTheme _theme;
		private readonly IRegionService _regionService;
		private readonly AccessTokenValidator _accessTokenValidator;

		public RegionController(AccessTokenValidator accessTokenValidator, IRegionService regionService, ILogger<RegionController> logger, IKTTheme theme)
		{
			_logger = logger;
			_theme = theme;
			_accessTokenValidator = accessTokenValidator;
			_regionService = regionService;
		}

		[HttpGet("/region")]
		[ServiceFilter(typeof(AccessTokenAuthorizationFilter))] // Apply the custom filter here
		public IActionResult Index()
		{
			return View(_theme.GetPageView("Location\\Region", "Index.cshtml"));
		}

		[HttpPost("/region/create")]
		public async Task<IActionResult> CreateRegionAsync([FromBody] CreateRegionDTO parameters)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var _data = _accessTokenValidator.ValidateAccessToken(HttpContext);

					if (_data.Item1)
					{

						if (string.IsNullOrEmpty(_data.Item2))
						{
							return Unauthorized(new { IsSuccess = false, ErrorMessages = Settings.INVALID_SESSION_ERR_MSG });
						}

						var _response = await _regionService.CreateRegionAsync<APIResponse>(parameters, _data.Item2);

						if (_response != null && _response.IsSuccess)
						{
							// Serialize the response to JSON and return it
							string json = JsonConvert.SerializeObject(_response);

							return Content(json, "application/json");
						}

						// Handle login failure.
						return BadRequest(new { IsSuccess = false, ErrorMessages = _response.StatusCode == HttpStatusCode.Conflict ? _response.ErrorMessages.Trim() : Settings.API_ERR_MSG });
					}
					else
					{
						return Unauthorized(new { IsSuccess = false, ErrorMessages = Settings.INVALID_SESSION_ERR_MSG });
					}
				}

				return BadRequest(new { IsSuccess = false, ErrorMessages = Settings.INVALID_MODEL_ERR_MSG });
			}
			catch (Exception exception)
			{
				return Json(new { error = Convert.ToInt32(HttpStatusCode.InternalServerError), message = Settings.UNKNOWN_ERR_MSG });
			}
		}

		[HttpGet("/region/details")]
		public async Task<IActionResult> GetRegionDetailAsync(int regionno)
		{
			try
			{
				var _data = _accessTokenValidator.ValidateAccessToken(HttpContext);

				if (_data.Item1)
				{
					RegionModel list = new RegionModel();

					if (string.IsNullOrEmpty(_data.Item2))
					{
						return Unauthorized(new { IsSuccess = false, ErrorMessages = Settings.INVALID_SESSION_ERR_MSG });
					}

					var _response = await _regionService.GetRegionDetailAsync<APIResponse>(regionno, _data.Item2);

					if (_response != null && _response.IsSuccess)
					{
						list = JsonConvert.DeserializeObject<RegionModel>(Convert.ToString(_response.Result));

						// Prepare the response object
						var response = new
						{
							data = list // Your list of UserModel
						};

						string json = JsonConvert.SerializeObject(response);

						return Content(json, "application/json");

					}

					return BadRequest(new { IsSuccess = false, ErrorMessages = Settings.API_ERR_MSG });
				}
				else
				{
					return Unauthorized(new { IsSuccess = false, ErrorMessages = Settings.INVALID_SESSION_ERR_MSG });
				}
			}
			catch (Exception exception)
			{
				return Json(new { error = Convert.ToInt32(HttpStatusCode.InternalServerError), message = Settings.UNKNOWN_ERR_MSG });
			}
		}

		[HttpGet("/region/ledger")]
		public async Task<IActionResult> GetRegionLedgerAsync(string searchkey = "", int divisionno = 0, int draw = 1, int start = 1, int length = 20)
		{
			try
			{
				var _data = _accessTokenValidator.ValidateAccessToken(HttpContext);

				if (_data.Item1)
				{
					List<RegionModel> list = new List<RegionModel>();

					if (string.IsNullOrEmpty(_data.Item2))
					{
						return Unauthorized(new { IsSuccess = false, ErrorMessages = Settings.INVALID_SESSION_ERR_MSG });
					}

					var _response = await _regionService.GetRegionLedgerAsync<APIResponse>(searchkey, divisionno, _data.Item2);

					if (_response != null && _response.IsSuccess)
					{
						list = JsonConvert.DeserializeObject<List<RegionModel>>(Convert.ToString(_response.Result));

						// Assuming you have a total user count
						var totalRecords = list.Count;

						// Filter and paginate the data
						var filteredData = list.Skip(start).Take(length).ToList();

						var filteredRecords = list.Count;

						// Prepare the response object
						var response = new
						{
							draw,
							recordsTotal = totalRecords,
							recordsFiltered = filteredRecords,
							data = list // Your list of UserModel
						};

						string json = JsonConvert.SerializeObject(response);

						return Content(json, "application/json");
					}

					return BadRequest(new { IsSuccess = false, ErrorMessages = Settings.API_ERR_MSG });
				}
				else
				{
					return Unauthorized(new { IsSuccess = false, ErrorMessages = Settings.INVALID_SESSION_ERR_MSG });
				}
			}
			catch (Exception exception)
			{
				return Json(new { error = Convert.ToInt32(HttpStatusCode.InternalServerError), message = Settings.UNKNOWN_ERR_MSG });
			}
		}

		[HttpDelete("/region/remove")]
		public async Task<IActionResult> DeleteRegionAsync(int regionno)
		{
			try
			{
				var _data = _accessTokenValidator.ValidateAccessToken(HttpContext);

				if (_data.Item1)
				{

					if (string.IsNullOrEmpty(_data.Item2))
					{
						return Unauthorized(new { IsSuccess = false, ErrorMessages = Settings.INVALID_SESSION_ERR_MSG });
					}

					var _response = await _regionService.DeleteRegionAsync<APIResponse>(regionno, _data.Item2);

					if (_response != null && _response.IsSuccess)
					{
						string json = JsonConvert.SerializeObject(_response);

						return Content(json, "application/json");
					}

					return BadRequest(new { IsSuccess = false, ErrorMessages = Settings.API_ERR_MSG });
				}
				else
				{
					return Unauthorized(new { IsSuccess = false, ErrorMessages = Settings.INVALID_SESSION_ERR_MSG });
				}
			}
			catch (Exception exception)
			{
				return Json(new { error = Convert.ToInt32(HttpStatusCode.InternalServerError), message = Settings.UNKNOWN_ERR_MSG });
			}
		}



	}
}
