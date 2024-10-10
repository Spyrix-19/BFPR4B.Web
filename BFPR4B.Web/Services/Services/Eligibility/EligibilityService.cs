using BFPR4B.Utility;
using BFPR4B.Web.Models.DTO.Gentable;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Eligibility;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.Eligibility
{
	public class EligibilityService : BaseService, IEligibilityService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public EligibilityService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}

		public Task<T> CreateEligibilityAsync<T>(CreateGentableDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/Eligibility/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetEligibilityDetailAsync<T>(int detno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + (detno == 0 || detno == null ? "api/v1/Eligibility/Details" : "api/v1/Eligibility/Details?detno=" + detno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetEligibilityLedgerAsync<T>(string searchkey, int parentcode, int subparentcode, string AccessToken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/Eligibility/Ledger?searchkey=" + searchkey.Trim() + "&parentcode=" + parentcode + "&subparentcode=" + subparentcode,
				AccessToken = AccessToken,
			});
		}

		public Task<T> DeleteEligibilityAsync<T>(int detno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Data = detno,
				ApiUrl = _apiURL + (detno == 0 || detno == null ? "api/v1/Eligibility/Remove" : "api/v1/Eligibility/Remove?detno=" + detno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> CreateEligibilityJournalAsync<T>(CreateGentableJournalDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/EligibilityJournal/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetEligibilityJournalAsync<T>(string searchkey, int gendetno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/EligibilityJournal/Ledger?searchkey=" + searchkey + "&gendetno=" + gendetno,
				AccessToken = accesstoken,
			});
		}



	}
}
