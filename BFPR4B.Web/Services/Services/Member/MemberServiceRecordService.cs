using BFPR4B.Utility;
using BFPR4B.Web.Models.DTO.Location;
using BFPR4B.Web.Models.DTO.Member;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Member;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.Member
{
	public class MemberServiceRecordService : BaseService, IMemberServiceRecordService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public MemberServiceRecordService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}

		public Task<T> CreateMemberServiceRecordAsync<T>(CreateMemberServiceRecordDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/MemberServiceRecord/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetMemberServiceRecordDetailAsync<T>(int detno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + (detno == 0 || detno == null ? "api/v1/MemberServiceRecord/Details" : "api/v1/MemberServiceRecord/Details?detno=" + detno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetMemberServiceRecordLedgerAsync<T>(string searchkey, int memberno, int appstatus, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/MemberServiceRecord/Ledger?searchkey=" + searchkey.Trim() + "&memberno=" + memberno + "&appstatus=" + appstatus,
				AccessToken = accesstoken,
			});
		}

		public Task<T> DeleteMemberServiceRecordAsync<T>(int detno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Data = detno,
				ApiUrl = _apiURL + (detno == 0 || detno == null ? "api/v1/MemberServiceRecord/Remove" : "api/v1/MemberServiceRecord/Remove?detno=" + detno),
				AccessToken = accesstoken,
			});
		}




	}
}
