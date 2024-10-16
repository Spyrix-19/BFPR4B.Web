using BFPR4B.Utility;
using BFPR4B.Web.Models.DTO.Member;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Barangay;
using BFPR4B.Web.Services.IServices.Member;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.Member
{
	public class MemberDependentService : BaseService, IMemberDependentService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public MemberDependentService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}

		public Task<T> CreateMemberDependentAsync<T>(CreateMemberDependentDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/MemberDependent/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetMemberDependentDetailAsync<T>(int detno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + (detno == 0 || detno == null ? "api/v1/MemberDependent/Details" : "api/v1/MemberDependent/Details?detno=" + detno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetMemberDependentLedgerAsync<T>(string searchkey, int memberno, string AccessToken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/MemberDependent/Ledger?searchkey=" + searchkey.Trim() + "&memberno=" + memberno,
				AccessToken = AccessToken,
			});
		}

		public Task<T> DeleteMemberDependentAsync<T>(int detno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Data = detno,
				ApiUrl = _apiURL + (detno == 0 || detno == null ? "api/v1/MemberDependent/Remove" : "api/v1/MemberDependent/Remove?detno=" + detno),
				AccessToken = accesstoken,
			});
		}





	}
}
