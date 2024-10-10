using BFPR4B.Utility;
using BFPR4B.Web.Models.DTO.Location;
using BFPR4B.Web.Models.DTO.Office;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Location;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.Location
{
	public class ProvinceService : BaseService, IProvinceService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public ProvinceService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}

		public Task<T> CreateProvinceAsync<T>(CreateProvinceDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/Province/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetProvinceDetailAsync<T>(int provinceno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + (provinceno == 0 || provinceno == null ? "api/v1/Province/Details" : "api/v1/Province/Details?provinceno=" + provinceno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetProvinceLedgerAsync<T>(string searchkey, int regionno, string AccessToken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/Province/Ledger?searchkey=" + searchkey.Trim() + "&regionno=" + regionno,
				AccessToken = AccessToken,
			});
		}

		public Task<T> DeleteProvinceAsync<T>(int provinceno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Data = provinceno,
				ApiUrl = _apiURL + (provinceno == 0 || provinceno == null ? "api/v1/Province/Remove" : "api/v1/Province/Remove?provinceno=" + provinceno),
				AccessToken = accesstoken,
			});
		}






	}
}
