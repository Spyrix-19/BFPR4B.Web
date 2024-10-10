using BFPR4B.Web._keenthemes.libs;
using BFPR4B.Web.Controllers.Location;
using BFPR4B.Web.Models.DTO.Gentable;
using BFPR4B.Web.Models.Model.Gentable;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Course;
using BFPR4B.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace BFPR4B.Web.Controllers.Course
{
	public class CourseController : Controller
	{
		private readonly ILogger<CourseController> _logger;
		private readonly IKTTheme _theme;
		private readonly ICourseService _courseService;
		private readonly AccessTokenValidator _accessTokenValidator;

		public CourseController(AccessTokenValidator accessTokenValidator, ICourseService courseService, ILogger<CourseController> logger, IKTTheme theme)
		{
			_accessTokenValidator = accessTokenValidator;
			_courseService = courseService;
			_theme = theme; 
			_logger = logger;
		}

		[HttpGet("/course")]
		[ServiceFilter(typeof(AccessTokenAuthorizationFilter))] // Apply the custom filter here
		public IActionResult Index()
		{
			return View(_theme.GetPageView("Course", "Index.cshtml"));
		}

		[HttpPost("/course/create")]
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

						var _response = await _courseService.CreateCourseAsync<APIResponse>(parameters, _data.Item2);

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

		[HttpGet("/course/details")]
		public async Task<IActionResult> GetCourseDetailAsync(int detno)
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

					var _response = await _courseService.GetCourseDetailAsync<APIResponse>(detno, _data.Item2);

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

		[HttpGet("/course/ledger")]
		public async Task<IActionResult> GetCourse(string searchkey = "", int parentcode = 0, int draw = 1, int start = 1, int length = 20)
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

					var _response = await _courseService.GetCourseLedgerAsync<APIResponse>(searchkey, parentcode, 0, _data.Item2);

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

		[HttpDelete("/course/remove")]
		public async Task<IActionResult> DeleteCourseAsync(int detno)
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

					var _response = await _courseService.DeleteCourseAsync<APIResponse>(detno, _data.Item2);

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

		[HttpPost("/course/journal/create")]
		public async Task<IActionResult> CreateCourseJournalAsync([FromBody] CreateGentableJournalDTO parameters)
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

						var _response = await _courseService.CreateCourseJournalAsync<APIResponse>(parameters, _data.Item2);

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

		[HttpGet("/course/journal/ledger")]
		public async Task<IActionResult> GetCourseJournalAsync(string searchkey = "", int gendetno = 0, int draw = 1, int start = 1, int length = 20)
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

					var _response = await _courseService.GetCourseJournalAsync<APIResponse>(searchkey, gendetno, _data.Item2);

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

					// Handle login failure.
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
