using BFPR4B.Web.Services.IServices.Dashboard;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.Dashboard
{
	public class DashboardService : BaseService, IDashboardService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public DashboardService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}


	}
}
