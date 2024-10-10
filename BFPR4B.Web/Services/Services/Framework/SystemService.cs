using BFPR4B.Utility;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Framework;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.Framework
{
	public class SystemService : BaseService, ISystemService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public SystemService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}

		public Task<T> GetGentables<T>(string searchkey, string tablename, int parentcode, int subparentcode, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/Setting/Gentable?searchkey=" + searchkey.Trim() + "&tablename=" + tablename.Trim() + "&parentcode=" + parentcode + "&subparentcode=" + subparentcode,
				AccessToken = accesstoken,
			});
		}


	}
}
