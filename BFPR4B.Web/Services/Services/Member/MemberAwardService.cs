using BFPR4B.Utility;
using BFPR4B.Web.Models.DTO.Member;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Member;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.Member
{
	public class MemberAwardService : BaseService, IMemberAwardService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public MemberAwardService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}

		public Task<T> CreateMemberAwardAsync<T>(CreateMemberAwardDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/MemberAward/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetMemberAwardDetailAsync<T>(int detno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + (detno == 0 || detno == null ? "api/v1/MemberAward/Details" : "api/v1/MemberAward/Details?detno=" + detno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetMemberAwardLedgerAsync<T>(string searchkey, int memberno, int awardtype, string AccessToken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/MemberAward/Ledger?searchkey=" + searchkey.Trim() + "&memberno=" + memberno + "&awardtype=" + awardtype,
				AccessToken = AccessToken,
			});
		}

		public Task<T> DeleteMemberAwardAsync<T>(int detno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Data = detno,
				ApiUrl = _apiURL + (detno == 0 || detno == null ? "api/v1/MemberAward/Remove" : "api/v1/MemberAward/Remove?detno=" + detno),
				AccessToken = accesstoken,
			});
		}





	}
}
