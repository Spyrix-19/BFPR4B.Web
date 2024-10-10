using BFPR4B.Web._keenthemes.libs;
using BFPR4B.Web.Models.DTO.Auth;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Auth;
using BFPR4B.Web.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using BFPR4B.Web.Models.Model.User;
using BFPR4B.Web.Services.IServices.User;
using BFPR4B.Web.Services.Services.User;

namespace BFPR4B.Web.Controllers.Auth
{
	[Route("[controller]")]
	public class AuthController : Controller
	{
		//private readonly ILogger<DashboardsController> _logger;
		private readonly IKTTheme _theme;
		private readonly IAuthService _authService;
		private readonly IUserService _userService;
		private readonly IDataProtectionProvider _dataProtectionProvider;
		private readonly AccessTokenHelper _accessTokenHelper;

		public AuthController(AccessTokenHelper accessTokenHelper, IDataProtectionProvider dataProtectionProvider, IAuthService authService, IUserService userService, IKTTheme theme)
		{
			_authService = authService;
			_userService = userService;
			_dataProtectionProvider = dataProtectionProvider;
			_accessTokenHelper = accessTokenHelper;
			//_logger = logger;
			_theme = theme;
		}

		[HttpGet("/resetpassword")]
		public IActionResult ResetPassword()
		{
			return View(_theme.GetPageView("Auth", "ResetPassword.cshtml"));
		}

		[HttpGet("/newpassword")]
		public IActionResult NewPassword()
		{
			return View(_theme.GetPageView("Auth", "NewPassword.cshtml"));
		}

		[HttpGet("/")]
		[HttpGet("/signin")]
		public IActionResult Login()
		{
			return View(_theme.GetPageView("Auth", "SignIn.cshtml"));
		}

		[HttpPost("signin")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDTO parameters)
		{
			if (ModelState.IsValid)
			{
				//Call the API to authenticate the user.
				var response = await _authService.LoginAsync<APIResponse>(parameters);

				if (response.IsSuccess)
				{

					// Get Currently Login User's Data
					//UserModel _userData = new UserModel();
					var user_response = await _userService.GetUserDetailByBadgeAsync<APIResponse>(parameters.Accountnumber);

					if (user_response.IsSuccess && user_response != null)
					{
						//_userData = JsonConvert.DeserializeObject<UserModel>(Convert.ToString(user_response.Result));

						// Your authentication logic to obtain the access token
						var accessToken = response.Result.ToString(); // Replace with your actual token


						if (_accessTokenHelper.IsValidToken(accessToken))
						{
							// Protect the token before storing it in a cookie
							var protector = _dataProtectionProvider.CreateProtector("AccessTokenProtection");
							string protectedToken = protector.Protect(accessToken);

							// Store the protected token in a cookie
							HttpContext.Response.Cookies.Append("Access_Token", protectedToken, new CookieOptions
							{
								HttpOnly = true,
								SameSite = SameSiteMode.Strict,
								// Add more cookie options as needed
							});

							// Serialize the response to JSON and return it
							string json = JsonConvert.SerializeObject(user_response);

							return Content(json, "application/json");

						}
						else
						{
							return Unauthorized(new { IsSuccess = false, ErrorMessages = Settings.INVALID_SESSION_ERR_MSG });
						}
					}
					else
					{
						return BadRequest(new { IsSuccess = false, ErrorMessages = "Invalid login attempt" });
					}
				}
				else
				{
					// Handle login failure.
					return BadRequest(new { IsSuccess = false, ErrorMessages = response.StatusCode == HttpStatusCode.Conflict ? response.ErrorMessages.Trim() : Settings.API_ERR_MSG });
				}
			}

			return BadRequest(new { IsSuccess = false, ErrorMessages = Settings.INVALID_MODEL_ERR_MSG });

		}

		[HttpGet("/logout")]
		public async Task<IActionResult> Logout()
		{
			//await HttpContext.SignOutAsync();
			//HttpContext.Session.SetString(SD.SessionToken, "");
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			//await HttpContext.SignOutAsync("Access_Token");
			Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate"; // HTTP 1.1
			Response.Headers["Pragma"] = "no-cache"; // HTTP 1.0
			Response.Headers["Expires"] = "0"; // Proxies
			return new RedirectResult("/signin");
		}


	}
}
