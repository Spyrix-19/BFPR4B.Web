using BFPR4B.Utility;
using BFPR4B.Web.Models.DTO.Gentable;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Training;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.Training
{
	public class TrainingService : BaseService, ITrainingService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public TrainingService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}

		public Task<T> CreateTrainingAsync<T>(CreateGentableDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/Training/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetTrainingDetailAsync<T>(int detno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + (detno == 0 || detno == null ? "api/v1/Training/Details" : "api/v1/Training/Details?detno=" + detno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetTrainingLedgerAsync<T>(string searchkey, int parentcode, int subparentcode, string AccessToken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/Training/Ledger?searchkey=" + searchkey.Trim() + "&parentcode=" + parentcode + "&subparentcode=" + subparentcode,
				AccessToken = AccessToken,
			});
		}

		public Task<T> DeleteTrainingAsync<T>(int detno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Data = detno,
				ApiUrl = _apiURL + (detno == 0 || detno == null ? "api/v1/Training/Remove" : "api/v1/Training/Remove?detno=" + detno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> CreateTrainingJournalAsync<T>(CreateGentableJournalDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/TrainingJournal/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetTrainingJournalAsync<T>(string searchkey, int gendetno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/TrainingJournal/Ledger?searchkey=" + searchkey + "&gendetno=" + gendetno,
				AccessToken = accesstoken,
			});
		}

	}
}
