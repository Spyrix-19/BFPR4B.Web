using BFPR4B.Utility;
using BFPR4B.Web.Models.DTO.User;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.User;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.User
{
	public class UserService : BaseService, IUserService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public UserService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}

		public Task<T> CreateUserAsync<T>(CreateUserDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/User/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> UpdateUserAsync<T>(UpdateUserDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/User/Update",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetUserDetailAsync<T>(int userno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + (userno == 0 || userno == null ? "api/v1/User/Details" : "api/v1/User/Details?userno=" + userno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetUserDetailByBadgeAsync<T>(string accountnumber)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + (string.IsNullOrEmpty(accountnumber) ? "api/v1/User/DetailByAccountNumber?accountnumber=" : "api/v1/User/DetailByAccountNumber?accountnumber=" + accountnumber),
			});
		}

		public Task<T> GetUserLedgerAsync<T>(string searchkey, int role, string AccessToken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/User/Ledger?searchkey=" + searchkey.Trim() + "&role=" + role,
				AccessToken = AccessToken,
			});
		}

		public Task<T> DeleteUserAsync<T>(int userno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Data = userno,
				ApiUrl = _apiURL + (userno == 0 || userno == null ? "api/v1/User/Remove" : "api/v1/User/Remove?userno=" + userno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> ActivateUserAsync<T>(ActivateUserPasswordDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/User/Activate",
				AccessToken = accesstoken,
			});
		}

		public Task<T> UnlockUserAsync<T>(UnLockUserPasswordDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/User/Unlock",
				AccessToken = accesstoken,
			});
		}

		public Task<T> UpdateUserPasswordAsync<T>(UpdateUserPasswordDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/User/UpdatePassword",
				AccessToken = accesstoken,
			});
		}

		public Task<T> UpdateUserPasswordExpiryAsync<T>(UpdatePasswordExpiry parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/User/UpdatePasswordExpiry",
				AccessToken = accesstoken,
			});
		}

		public Task<T> CreateUserJournalAsync<T>(CreateUserJournalDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/UserJournal/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetUserJournalAsync<T>(string searchkey, int userno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/UserJournal/Ledger?searchkey=" + searchkey + "&userno=" + userno,
				AccessToken = accesstoken,
			});
		}
	}
}
