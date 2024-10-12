using BFPR4B.Utility;
using BFPR4B.Web.Models.DTO.Location;
using BFPR4B.Web.Models.DTO.Office;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Location;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.Location
{
	public class CityService : BaseService, ICityService
	{ 
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public CityService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}

		public Task<T> CreateCityAsync<T>(CreateCityDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/City/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetCityDetailAsync<T>(int cityno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + (cityno == 0 || cityno == null ? "api/v1/City/Details" : "api/v1/City/Details?cityno=" + cityno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetCityLedgerAsync<T>(string searchkey, int provinceno, int regionno, string AccessToken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/City/Ledger?searchkey=" + searchkey.Trim() + "&provinceno=" + provinceno + "&regionno=" + regionno,
				AccessToken = AccessToken,
			});
		}

		public Task<T> DeleteCityAsync<T>(int cityno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Data = cityno,
				ApiUrl = _apiURL + (cityno == 0 || cityno == null ? "api/v1/City/Remove" : "api/v1/City/Remove?cityno=" + cityno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> CreateCityJournalAsync<T>(CreateCityJournalDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/CityJournal/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetCityJournalAsync<T>(string searchkey, int cityno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/CityJournal/Ledger?searchkey=" + searchkey + "&cityno=" + cityno,
				AccessToken = accesstoken,
			});
		}


	}
}
