using BFPR4B.Utility;
using BFPR4B.Web.Models.DTO.Gentable;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Rank;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.Rank
{
	public class RankService : BaseService, IRankService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public RankService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}

		public Task<T> CreateRankAsync<T>(CreateGentableDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/Rank/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetRankDetailAsync<T>(int detno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + (detno == 0 || detno == null ? "api/v1/Rank/Details" : "api/v1/Rank/Details?detno=" + detno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetRankLedgerAsync<T>(string searchkey, int parentcode, int subparentcode, string AccessToken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/Rank/Ledger?searchkey=" + searchkey.Trim() + "&parentcode=" + parentcode + "&subparentcode=" + subparentcode,
				AccessToken = AccessToken,
			});
		}

		public Task<T> DeleteRankAsync<T>(int detno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Data = detno,
				ApiUrl = _apiURL + (detno == 0 || detno == null ? "api/v1/Rank/Remove" : "api/v1/Rank/Remove?detno=" + detno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> CreateRankJournalAsync<T>(CreateGentableJournalDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/RankJournal/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetRankJournalAsync<T>(string searchkey, int gendetno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/RankJournal/Ledger?searchkey=" + searchkey + "&gendetno=" + gendetno,
				AccessToken = accesstoken,
			});
		}



	}
}
