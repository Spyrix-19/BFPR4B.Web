using BFPR4B.Utility;
using BFPR4B.Web.Models.DTO.GAD;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.GAD;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.GAD
{
	public class EventService : BaseService, IEventService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public EventService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}

		public Task<T> CreateEventAsync<T>(CreateEventDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/Event/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetEventDetailAsync<T>(int eventno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + (eventno == 0 || eventno == null ? "api/v1/Event/Details" : "api/v1/Event/Details?eventno=" + eventno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetEventLedgerAsync<T>(string searchkey, int status, int eventtype, string AccessToken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/Event/Ledger?searchkey=" + searchkey.Trim() + "&status=" + status + "&eventtype=" + eventtype,
				AccessToken = AccessToken,
			});
		}

		public Task<T> DeleteEventAsync<T>(int eventno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Data = eventno,
				ApiUrl = _apiURL + (eventno == 0 || eventno == null ? "api/v1/Event/Remove" : "api/v1/Event/Remove?eventno=" + eventno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> CreateEventAttachmentAsync<T>(CreateEventAttachmentDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/EventAttachment/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetEventAttachmentLedgerAsync<T>(string searchkey, int eventno, string attachmenttype, string AccessToken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/EventAttachment/Ledger?searchkey=" + searchkey.Trim() + "&eventno=" + eventno + "&attachmenttype=" + attachmenttype,
				AccessToken = AccessToken,
			});
		}

		public Task<T> DeleteEventAttachmentAsync<T>(int attachmentno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Data = attachmentno,
				ApiUrl = _apiURL + (attachmentno == 0 || attachmentno == null ? "api/v1/EventAttachment/Remove" : "api/v1/EventAttachment/Remove?attachmentno=" + attachmentno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> CreateEventJournalAsync<T>(CreateEventJournalDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/EventJournal/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetEventJournalAsync<T>(string searchkey, int eventno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/EventJournal/Ledger?searchkey=" + searchkey + "&eventno=" + eventno,
				AccessToken = accesstoken,
			});
		}





	}
}
