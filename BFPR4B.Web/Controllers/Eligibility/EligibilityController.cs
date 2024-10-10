using BFPR4B.Web._keenthemes.libs;
using BFPR4B.Web.Controllers.Course;
using BFPR4B.Web.Models.DTO.Gentable;
using BFPR4B.Web.Models.Model.Gentable;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Eligibility;
using BFPR4B.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace BFPR4B.Web.Controllers.Eligibility
{
	public class EligibilityController : Controller
	{
		private readonly ILogger<EligibilityController> _logger;
		private readonly IKTTheme _theme;
		private readonly IEligibilityService _eligibilityService;
		private readonly AccessTokenValidator _accessTokenValidator;

		public EligibilityController(AccessTokenValidator accessTokenValidator, IEligibilityService eligibilityService, ILogger<EligibilityController> logger, IKTTheme theme)
		{
			_accessTokenValidator = accessTokenValidator;
			_eligibilityService = eligibilityService;
			_logger = logger;
			_theme = theme;			
		}

		[HttpGet("/eligibility")]
		[ServiceFilter(typeof(AccessTokenAuthorizationFilter))] // Apply the custom filter here
		public IActionResult Index()
		{
			return View(_theme.GetPageView("Eligibility", "Index.cshtml"));
		}

		[HttpPost("/eligibility/create")]
		public async Task<IActionResult> CreateCourseAsync([FromBody] CreateGentableDTO parameters)
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

						var _response = await _eligibilityService.CreateEligibilityAsync<APIResponse>(parameters, _data.Item2);

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

		[HttpGet("/eligibility/details")]
		public async Task<IActionResult> GetEligibilityDetailAsync(int detno)
		{
			try
			{
				var _data = _accessTokenValidator.ValidateAccessToken(HttpContext);

				if (_data.Item1)
				{
					GentableModel list = new GentableModel();

					if (string.IsNullOrEmpty(_data.Item2))
					{
						return Unauthorized(new { IsSuccess = false, ErrorMessages = Settings.INVALID_SESSION_ERR_MSG });
					}

					var _response = await _eligibilityService.GetEligibilityDetailAsync<APIResponse>(detno, _data.Item2);

					if (_response != null && _response.IsSuccess)
					{
						list = JsonConvert.DeserializeObject<GentableModel>(Convert.ToString(_response.Result));

						// Prepare the response object
						var response = new
						{
							data = list // Your list of UserModel
						};

						// Serialize the response to JSON and return it
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

		[HttpGet("/eligibility/ledger")]
		public async Task<IActionResult> GetEligibility(string searchkey = "", int parentcode = 0, int subparentcode = 0, int draw = 1, int start = 1, int length = 20)
		{
			try
			{
				var _data = _accessTokenValidator.ValidateAccessToken(HttpContext);

				if (_data.Item1)
				{
					List<GentableModel> list = new List<GentableModel>();

					if (string.IsNullOrEmpty(_data.Item2))
					{
						return Unauthorized(new { IsSuccess = false, ErrorMessages = Settings.INVALID_SESSION_ERR_MSG });
					}

					var _response = await _eligibilityService.GetEligibilityLedgerAsync<APIResponse>(searchkey, parentcode, subparentcode, _data.Item2);

					if (_response != null && _response.IsSuccess)
					{
						list = JsonConvert.DeserializeObject<List<GentableModel>>(Convert.ToString(_response.Result));

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

						// Serialize the response to JSON and return it
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

		[HttpDelete("/eligibility/remove")]
		public async Task<IActionResult> DeleteEligibilityAsync(int detno)
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

					var _response = await _eligibilityService.DeleteEligibilityAsync<APIResponse>(detno, _data.Item2);

					if (_response != null && _response.IsSuccess)
					{
						// Serialize the response to JSON and return it
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

		[HttpPost("/eligibility/journal/create")]
		public async Task<IActionResult> CreateEligibilityJournalAsync([FromBody] CreateGentableJournalDTO parameters)
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

						var _response = await _eligibilityService.CreateEligibilityJournalAsync<APIResponse>(parameters, _data.Item2);

						if (_response != null && _response.IsSuccess)
						{

							// Serialize the response to JSON and return it
							string json = JsonConvert.SerializeObject(_response);

							return Content(json, "application/json");
						}

						// Handle login failure.
						return BadRequest(new { IsSuccess = false, ErrorMessages = Settings.API_ERR_MSG });
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

		[HttpGet("/eligibility/journal/ledger")]
		public async Task<IActionResult> GetEligibilityJournalAsync(string searchkey = "", int gendetno = 0, int draw = 1, int start = 1, int length = 20)
		{
			try
			{
				var _data = _accessTokenValidator.ValidateAccessToken(HttpContext);

				if (_data.Item1)
				{
					List<GentableJournalModel> list = new List<GentableJournalModel>();

					if (string.IsNullOrEmpty(_data.Item2))
					{
						return Unauthorized(new { IsSuccess = false, ErrorMessages = Settings.INVALID_SESSION_ERR_MSG });
					}

					var _response = await _eligibilityService.GetEligibilityJournalAsync<APIResponse>(searchkey, gendetno, _data.Item2);

					if (_response != null && _response.IsSuccess)
					{
						list = JsonConvert.DeserializeObject<List<GentableJournalModel>>(Convert.ToString(_response.Result));

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


						// Serialize the response to JSON and return it
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
	}
}
