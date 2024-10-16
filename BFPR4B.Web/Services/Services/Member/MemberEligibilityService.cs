using BFPR4B.Utility;
using BFPR4B.Web.Models.DTO.Member;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Member;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.Member
{
	public class MemberEligibilityService : BaseService, IMemberEligibilityService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public MemberEligibilityService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}

		public Task<T> CreateMemberEligibilityAsync<T>(CreateMemberEligibilityDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/MemberEligibility/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetMemberEligibilityDetailAsync<T>(int detno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + (detno == 0 || detno == null ? "api/v1/MemberEligibility/Details" : "api/v1/MemberEligibility/Details?detno=" + detno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetMemberEligibilityLedgerAsync<T>(string searchkey, int memberno, int eligibilityno, string AccessToken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/MemberEligibility/Ledger?searchkey=" + searchkey.Trim() + "&memberno=" + memberno + "&eligibilityno=" + eligibilityno,
				AccessToken = AccessToken,
			});
		}

		public Task<T> DeleteMemberEligibilityAsync<T>(int detno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Data = detno,
				ApiUrl = _apiURL + (detno == 0 || detno == null ? "api/v1/MemberEligibility/Remove" : "api/v1/MemberEligibility/Remove?detno=" + detno),
				AccessToken = accesstoken,
			});
		}

	}
}
