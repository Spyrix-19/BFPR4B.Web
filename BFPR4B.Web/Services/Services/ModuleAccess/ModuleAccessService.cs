
using BFPR4B.Web.Services.IServices.ModuleAccess;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.ModuleAccess
{
	public class ModuleAccessService : BaseService, IModuleAccessService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public ModuleAccessService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}



	}
}
