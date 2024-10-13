using BFPR4B.Utility;
using BFPR4B.Web.Models.DTO.GAD;
using BFPR4B.Web.Models.DTO.Office;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.GAD;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.GAD
{
	public class ResourceService : BaseService, IResourceService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public ResourceService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}

		public Task<T> CreateResourceAsync<T>(CreateResourceDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/Resource/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetResourceDetailAsync<T>(int resourceno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + (resourceno == 0 || resourceno == null ? "api/v1/Resource/Details" : "api/v1/Resource/Details?resourceno=" + resourceno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetResourceLedgerAsync<T>(string searchkey, string AccessToken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/Resource/Ledger?searchkey=" + searchkey.Trim(),
				AccessToken = AccessToken,
			});
		}

		public Task<T> DeleteResourceAsync<T>(int resourceno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Data = resourceno,
				ApiUrl = _apiURL + (resourceno == 0 || resourceno == null ? "api/v1/Resource/Remove" : "api/v1/Resource/Remove?resourceno=" + resourceno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> CreateResourceAttachmentAsync<T>(CreateResourceAttachmentDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/ResourceAttachment/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetResourceAttachmentLedgerAsync<T>(string searchkey, int resourceno, string attachmenttype, string AccessToken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/ResourceAttachment/Ledger?searchkey=" + searchkey.Trim() + "&resourceno=" + resourceno + "&attachmenttype=" + attachmenttype,
				AccessToken = AccessToken,
			});
		}

		public Task<T> DeleteResourceAttachmentAsync<T>(int attachmentno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Data = attachmentno,
				ApiUrl = _apiURL + (attachmentno == 0 || attachmentno == null ? "api/v1/ResourceAttachment/Remove" : "api/v1/ResourceAttachment/Remove?attachmentno=" + attachmentno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> CreateResourceJournalAsync<T>(CreateResourceJournalDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/ResourceJournal/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetResourceJournalAsync<T>(string searchkey, int resourceno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/ResourceJournal/Ledger?searchkey=" + searchkey + "&resourceno=" + resourceno,
				AccessToken = accesstoken,
			});
		}



	}
}
