using BFPR4B.Utility;
using BFPR4B.Web.Models.DTO.Gentable;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Rank;
using BFPR4B.Web.Services.IServices.Religion;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.Religion
{
	public class ReligionService : BaseService, IReligionService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public ReligionService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}

		public Task<T> CreateReligionAsync<T>(CreateGentableDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/Religion/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetReligionDetailAsync<T>(int detno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + (detno == 0 || detno == null ? "api/v1/Religion/Details" : "api/v1/Religion/Details?detno=" + detno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetReligionLedgerAsync<T>(string searchkey, int parentcode, int subparentcode, string AccessToken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/Religion/Ledger?searchkey=" + searchkey.Trim() + "&parentcode=" + parentcode + "&subparentcode=" + subparentcode,
				AccessToken = AccessToken,
			});
		}

		public Task<T> DeleteReligionAsync<T>(int detno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Data = detno,
				ApiUrl = _apiURL + (detno == 0 || detno == null ? "api/v1/Religion/Remove" : "api/v1/Religion/Remove?detno=" + detno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> CreateReligionJournalAsync<T>(CreateGentableJournalDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/ReligionJournal/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetReligionJournalAsync<T>(string searchkey, int gendetno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/ReligionJournal/Ledger?searchkey=" + searchkey + "&gendetno=" + gendetno,
				AccessToken = accesstoken,
			});
		}




	}
}
