using BFPR4B.Web._keenthemes.libs;
using BFPR4B.Web.Controllers.Dashboard;
using BFPR4B.Web.Models.Model.Member;
using BFPR4B.Web.Models.Model.User;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Member;
using BFPR4B.Web.Services.IServices.User;
using BFPR4B.Web.Services.Services.User;
using BFPR4B.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace BFPR4B.Web.Controllers.Member
{
	public class MemberController : Controller
	{
		private readonly ILogger<MemberController> _logger;
		private readonly IKTTheme _theme;
		private readonly IMemberService _memberService;
		private readonly AccessTokenValidator _accessTokenValidator;

		public MemberController(AccessTokenValidator accessTokenValidator, IMemberService memberService, ILogger<MemberController> logger, IKTTheme theme)
		{
			_accessTokenValidator = accessTokenValidator;
			_memberService = memberService;
			_logger = logger;
			_theme = theme;
		}

		[HttpGet("/member")]
		[ServiceFilter(typeof(AccessTokenAuthorizationFilter))] // Apply the custom filter here
		public IActionResult Index()
		{
			return View(_theme.GetPageView("Member\\Member", "Index.cshtml"));
		}


		[HttpGet("/member/ledger")]
		public async Task<IActionResult> GetMemberLedgerAsync(string searchkey = "", int rankno = 0, int areaassign = 0, int dutystatus = 0, int appstatus = 0, string gender = "", int province = 0, int draw = 1, int start = 1, int length = 20)
		{
			try
			{
				var _data = _accessTokenValidator.ValidateAccessToken(HttpContext);

				if (_data.Item1)
				{
					List<MemberLedgerModel> list = new List<MemberLedgerModel>();

					if (string.IsNullOrEmpty(_data.Item2))
					{
						return Unauthorized(new { IsSuccess = false, ErrorMessages = Settings.INVALID_SESSION_ERR_MSG });
					}

					var _response = await _memberService.GetMemberLedgerAsync<APIResponse>(searchkey, rankno, areaassign, dutystatus, appstatus, gender, province, _data.Item2);

					if (_response != null && _response.IsSuccess)
					{
						list = JsonConvert.DeserializeObject<List<MemberLedgerModel>>(Convert.ToString(_response.Result));

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
