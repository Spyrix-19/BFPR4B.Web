using BFPR4B.Utility;
using BFPR4B.Web.Models.DTO.Auth;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Auth;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.Auth
{
	public class AuthService : BaseService, IAuthService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}

		public Task<T> LoginAsync<T>(LoginRequestDTO parameters)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/Auth/Login",
			});
		}

		public Task<T> RegisterAsync<T>(RegistrationRequestDTO parameters)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/Auth/Register",
			});
		}
	}
}
