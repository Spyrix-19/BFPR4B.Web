using BFPR4B.Web._keenthemes.libs;
using BFPR4B.Web.Controllers.Member;
using BFPR4B.Web.Models.DTO.Location;
using BFPR4B.Web.Models.Model.Location;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Barangay;
using BFPR4B.Web.Services.IServices.Location;
using BFPR4B.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace BFPR4B.Web.Controllers.Location
{
	public class BarangayController : Controller
	{
		private readonly ILogger<BarangayController> _logger;
		private readonly IKTTheme _theme;
		private readonly IBarangayService _barangayService;
		private readonly AccessTokenValidator _accessTokenValidator;

		public BarangayController(AccessTokenValidator accessTokenValidator, IBarangayService barangayService, ILogger<BarangayController> logger, IKTTheme theme)
		{
			_logger = logger;
			_theme = theme;
			_accessTokenValidator = accessTokenValidator;
			_barangayService = barangayService;
		}

		[HttpGet("/barangay")]
		[ServiceFilter(typeof(AccessTokenAuthorizationFilter))] // Apply the custom filter here
		public IActionResult Index()
		{
			return View(_theme.GetPageView("Location\\Barangay", "Index.cshtml"));
		}

		[HttpPost("/barangay/create")]
		public async Task<IActionResult> CreateBarangayAsync([FromBody] CreateBarangayDTO parameters)
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

						var _response = await _barangayService.CreateBarangayAsync<APIResponse>(parameters, _data.Item2);

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

		[HttpGet("/barangay/details")]
		public async Task<IActionResult> GetBarangayDetailAsync(int barangayno)
		{
			try
			{
				var _data = _accessTokenValidator.ValidateAccessToken(HttpContext);

				if (_data.Item1)
				{
					CityModel list = new CityModel();

					if (string.IsNullOrEmpty(_data.Item2))
					{
						return Unauthorized(new { IsSuccess = false, ErrorMessages = Settings.INVALID_SESSION_ERR_MSG });
					}

					var _response = await _barangayService.GetBarangayDetailAsync<APIResponse>(barangayno, _data.Item2);

					if (_response != null && _response.IsSuccess)
					{
						list = JsonConvert.DeserializeObject<CityModel>(Convert.ToString(_response.Result));

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

		[HttpGet("/barangay/ledger")]
		public async Task<IActionResult> GetBarangayLedgerAsync(string searchkey = "", int cityno = 0, int provinceno = 0, int regionno = 0, int draw = 1, int start = 1, int length = 20)
		{
			try
			{
				var _data = _accessTokenValidator.ValidateAccessToken(HttpContext);

				if (_data.Item1)
				{
					List<CityModel> list = new List<CityModel>();

					if (string.IsNullOrEmpty(_data.Item2))
					{
						return Unauthorized(new { IsSuccess = false, ErrorMessages = Settings.INVALID_SESSION_ERR_MSG });
					}

					var _response = await _barangayService.GetBarangayLedgerAsync<APIResponse>(searchkey, cityno, provinceno, regionno, _data.Item2);

					if (_response != null && _response.IsSuccess)
					{
						list = JsonConvert.DeserializeObject<List<CityModel>>(Convert.ToString(_response.Result));

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

		[HttpDelete("/barangay/remove")]
		public async Task<IActionResult> DeleteBarangayAsync(int barangayno)
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

					var _response = await _barangayService.DeleteBarangayAsync<APIResponse>(barangayno, _data.Item2);

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
