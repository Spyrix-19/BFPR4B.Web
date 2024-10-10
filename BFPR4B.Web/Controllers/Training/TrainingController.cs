using BFPR4B.Web._keenthemes.libs;
using BFPR4B.Web.Models.DTO.Gentable;
using BFPR4B.Web.Models.Model.Gentable;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Training;
using BFPR4B.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace BFPR4B.Web.Controllers.Training
{
	public class TrainingController : Controller
	{
		private readonly IKTTheme _theme;
		private readonly ITrainingService _trainingService;
		private readonly AccessTokenValidator _accessTokenValidator;

		public TrainingController(AccessTokenValidator accessTokenValidator, ITrainingService trainingService, IKTTheme theme)
		{
			_accessTokenValidator = accessTokenValidator;
			_trainingService = trainingService;
			_theme = theme;
		}

		[HttpGet("/training")]
		[ServiceFilter(typeof(AccessTokenAuthorizationFilter))] // Apply the custom filter here
		public IActionResult Index()
		{
			return View(_theme.GetPageView("Training", "Index.cshtml"));
		}

		[HttpPost("/training/create")]
		public async Task<IActionResult> CreateTrainingAsync([FromBody] CreateGentableDTO parameters)
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

						var _response = await _trainingService.CreateTrainingAsync<APIResponse>(parameters, _data.Item2);

						if (_response != null && _response.IsSuccess)
						{
							// Serialize the response to JSON and return it
							string json = JsonConvert.SerializeObject(_response);

							return Content(json, "application/json");
						}

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

		[HttpGet("/training/details")]
		public async Task<IActionResult> GetTrainingDetailAsync(int detno)
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

					var _response = await _trainingService.GetTrainingDetailAsync<APIResponse>(detno, _data.Item2);

					if (_response != null && _response.IsSuccess)
					{
						list = JsonConvert.DeserializeObject<GentableModel>(Convert.ToString(_response.Result));

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

		[HttpGet("/training/ledger")]
		public async Task<IActionResult> GetTraining(string searchkey = "", int parentcode = 0, int subparentcode = 0, int draw = 1, int start = 1, int length = 20)
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

					var _response = await _trainingService.GetTrainingLedgerAsync<APIResponse>(searchkey, parentcode, subparentcode, _data.Item2);

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


		[HttpDelete("/training/remove")]
		public async Task<IActionResult> DeleteTrainingAsync(int detno)
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

					var _response = await _trainingService.DeleteTrainingAsync<APIResponse>(detno, _data.Item2);

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

		[HttpPost("/training/journal/create")]
		public async Task<IActionResult> CreateTrainingJournalAsync([FromBody] CreateGentableJournalDTO parameters)
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

						var _response = await _trainingService.CreateTrainingJournalAsync<APIResponse>(parameters, _data.Item2);

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

		[HttpGet("/training/journal/ledger")]
		public async Task<IActionResult> GetTrainingJournalAsync(string searchkey = "", int gendetno = 0, int draw = 1, int start = 1, int length = 20)
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

					var _response = await _trainingService.GetTrainingJournalAsync<APIResponse>(searchkey, gendetno, _data.Item2);

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
