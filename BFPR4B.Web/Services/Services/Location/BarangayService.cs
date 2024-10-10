using BFPR4B.Utility;
using BFPR4B.Web.Models.DTO.Location;
using BFPR4B.Web.Models.DTO.Office;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Barangay;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.Location
{
	public class BarangayService : BaseService, IBarangayService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public BarangayService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}

		public Task<T> CreateBarangayAsync<T>(CreateBarangayDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/Barangay/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetBarangayDetailAsync<T>(int barangayno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + (barangayno == 0 || barangayno == null ? "api/v1/Barangay/Details" : "api/v1/Barangay/Details?barangayno=" + barangayno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetBarangayLedgerAsync<T>(string searchkey, int cityno, int provinceno, int regionno, string AccessToken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/Barangay/Ledger?searchkey=" + searchkey.Trim() + "&cityno=" + cityno + "&provinceno=" + provinceno + "&regionno=" + regionno,
				AccessToken = AccessToken,
			});
		}

		public Task<T> DeleteBarangayAsync<T>(int barangayno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Data = barangayno,
				ApiUrl = _apiURL + (barangayno == 0 || barangayno == null ? "api/v1/Barangay/Remove" : "api/v1/Barangay/Remove?barangayno=" + barangayno),
				AccessToken = accesstoken,
			});
		}



	}
}
