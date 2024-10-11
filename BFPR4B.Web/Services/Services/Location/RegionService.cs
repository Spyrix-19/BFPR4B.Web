using BFPR4B.Utility;
using BFPR4B.Web.Models.DTO.Location;
using BFPR4B.Web.Models.DTO.Office;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Location;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.Location
{
	public class RegionService : BaseService, IRegionService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public RegionService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}

		public Task<T> CreateRegionAsync<T>(CreateRegionDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/Region/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetRegionDetailAsync<T>(int regionno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + (regionno == 0 || regionno == null ? "api/v1/Region/Details" : "api/v1/Region/Details?regionno=" + regionno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetRegionLedgerAsync<T>(string searchkey, int divisionno, string AccessToken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/Region/Ledger?searchkey=" + searchkey.Trim() + "&divisionno=" + divisionno,
				AccessToken = AccessToken,
			});
		}

		public Task<T> DeleteRegionAsync<T>(int regionno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Data = regionno,
				ApiUrl = _apiURL + (regionno == 0 || regionno == null ? "api/v1/Region/Remove" : "api/v1/Region/Remove?regionno=" + regionno),
				AccessToken = accesstoken,
			});
		}





	}
}
