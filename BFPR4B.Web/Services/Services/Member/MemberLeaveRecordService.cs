using BFPR4B.Utility;
using BFPR4B.Web.Models.DTO.Member;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Member;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.Member
{
	public class MemberLeaveRecordService : BaseService, IMemberLeaveRecordService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public MemberLeaveRecordService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}

		public Task<T> CreateMemberLeaveRecordAsync<T>(CreateMemberLeaveRecordDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/MemberLeaveRecord/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetMemberLeaveRecordDetailAsync<T>(int detno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + (detno == 0 || detno == null ? "api/v1/MemberLeaveRecord/Details" : "api/v1/MemberLeaveRecord/Details?detno=" + detno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetMemberLeaveRecordLedgerAsync<T>(string searchkey, int memberno, string AccessToken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/MemberLeaveRecord/Ledger?searchkey=" + searchkey.Trim() + "&memberno=" + memberno,
				AccessToken = AccessToken,
			});
		}

		public Task<T> DeleteMemberLeaveRecordAsync<T>(int detno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Data = detno,
				ApiUrl = _apiURL + (detno == 0 || detno == null ? "api/v1/MemberLeaveRecord/Remove" : "api/v1/MemberLeaveRecord/Remove?detno=" + detno),
				AccessToken = accesstoken,
			});
		}



	}
}
