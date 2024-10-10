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





	}
}
