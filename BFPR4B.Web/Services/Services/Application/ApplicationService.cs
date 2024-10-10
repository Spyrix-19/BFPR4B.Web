using BFPR4B.Web.Services.IServices.Application;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.Application
{
	public class ApplicationService : BaseService, IApplicationService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public ApplicationService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}






	}
}
