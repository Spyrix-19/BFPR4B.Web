using BFPR4B.Utility;
using BFPR4B.Web.Models.DTO.Member;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Member;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.Member
{
	public class MemberEducationService : BaseService, IMemberEducationService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public MemberEducationService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}

		public Task<T> CreateMemberEducationAsync<T>(CreateMemberEducationDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/MemberEducation/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetMemberEducationDetailAsync<T>(int detno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + (detno == 0 || detno == null ? "api/v1/MemberEducation/Details" : "api/v1/MemberEducation/Details?detno=" + detno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetMemberEducationLedgerAsync<T>(string searchkey, int memberno, int course, int educationallevel, string AccessToken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/MemberEducation/Ledger?searchkey=" + searchkey.Trim() + "&memberno=" + memberno + "&course=" + course + "&educationallevel=" + educationallevel ,
				AccessToken = AccessToken,
			});
		}

		public Task<T> DeleteMemberEducationAsync<T>(int detno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Data = detno,
				ApiUrl = _apiURL + (detno == 0 || detno == null ? "api/v1/MemberEducation/Remove" : "api/v1/MemberEducation/Remove?detno=" + detno),
				AccessToken = accesstoken,
			});
		}




	}
}
