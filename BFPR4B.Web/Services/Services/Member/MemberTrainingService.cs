using BFPR4B.Utility;
using BFPR4B.Web.Models.DTO.Member;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Member;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.Member
{
	public class MemberTrainingService : BaseService, IMemberTrainingService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public MemberTrainingService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}

		public Task<T> CreateMemberTrainingAsync<T>(CreateMemberTrainingDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/MemberTraining/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetMemberTrainingDetailAsync<T>(int detno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + (detno == 0 || detno == null ? "api/v1/MemberTraining/Details" : "api/v1/MemberTraining/Details?detno=" + detno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetMemberTrainingLedgerAsync<T>(string searchkey, int memberno, int trainingno, int trainingtype, string AccessToken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/MemberTraining/Ledger?searchkey=" + searchkey.Trim() + "&memberno=" + memberno + "&trainingno=" + trainingno + "&trainingtype=" + trainingtype,
				AccessToken = AccessToken,
			});
		}

		public Task<T> DeleteMemberTrainingAsync<T>(int detno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Data = detno,
				ApiUrl = _apiURL + (detno == 0 || detno == null ? "api/v1/MemberTraining/Remove" : "api/v1/MemberTraining/Remove?detno=" + detno),
				AccessToken = accesstoken,
			});
		}





	}
}
