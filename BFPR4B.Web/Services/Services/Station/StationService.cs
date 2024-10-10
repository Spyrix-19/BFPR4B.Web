using BFPR4B.Utility;
using BFPR4B.Web.Models.DTO.Gentable;
using BFPR4B.Web.Models.DTO.Station;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Station;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.Station
{
	public class StationService : BaseService, IStationService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public StationService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}

		public Task<T> CreateStationAsync<T>(CreateStationDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/Station/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetStationDetailAsync<T>(int stationno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + (stationno == 0 || stationno == null ? "api/v1/Station/Details" : "api/v1/Station/Details?stationno=" + stationno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetStationLedgerAsync<T>(string searchkey, int stationtype, int areaassign, int provinceno, string AccessToken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/Station/Ledger?searchkey=" + searchkey.Trim() + "&stationtype=" + stationtype + "&areaassign=" + areaassign + "&provinceno=" + provinceno,
				AccessToken = AccessToken,
			});
		}

		public Task<T> DeleteStationAsync<T>(int stationno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Data = stationno,
				ApiUrl = _apiURL + (stationno == 0 || stationno == null ? "api/v1/Station/Remove" : "api/v1/Station/Remove?stationno=" + stationno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> CreateStationJournalAsync<T>(CreateStationJournalDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/StationJournal/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetStationJournalAsync<T>(string searchkey, int stationno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/StationJournal/Ledger?searchkey=" + searchkey + "&stationno=" + stationno,
				AccessToken = accesstoken,
			});
		}


	}
}
