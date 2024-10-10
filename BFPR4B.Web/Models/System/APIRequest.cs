using static BFPR4B.Utility.SD;

namespace BFPR4B.Web.Models.System
{
	public class APIRequest
	{
		public ApiType ApiType { get; set; } = ApiType.GET;
		public string ApiUrl { get; set; }
		public object Data { get; set; }
		public string AccessToken { get; set; }
	}
}
