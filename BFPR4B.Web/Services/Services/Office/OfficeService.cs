using BFPR4B.Utility;
using BFPR4B.Web.Models.DTO.Gentable;
using BFPR4B.Web.Models.DTO.Office;
using BFPR4B.Web.Models.DTO.Station;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Office;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.Office
{
	public class OfficeService : BaseService, IOfficeService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public OfficeService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}

		public Task<T> CreateOfficeAsync<T>(CreateOfficeDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/Office/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetOfficeDetailAsync<T>(int officeno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + (officeno == 0 || officeno == null ? "api/v1/Office/Details" : "api/v1/Office/Details?officeno=" + officeno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetOfficeLedgerAsync<T>(string searchkey, int officetype, string AccessToken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/Office/Ledger?searchkey=" + searchkey.Trim() + "&officetype=" + officetype,
				AccessToken = AccessToken,
			});
		}

		public Task<T> DeleteOfficeAsync<T>(int officeno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Data = officeno,
				ApiUrl = _apiURL + (officeno == 0 || officeno == null ? "api/v1/Office/Remove" : "api/v1/Office/Remove?officeno=" + officeno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> CreateOfficeJournalAsync<T>(CreateOfficeJournalDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/OfficeJournal/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetOfficeJournalAsync<T>(string searchkey, int officeno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/OfficeJournal/Ledger?searchkey=" + searchkey + "&officeno=" + officeno,
				AccessToken = accesstoken,
			});
		}


	}
}
