using BFPR4B.Utility;
using BFPR4B.Web.Models.DTO.Member;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Member;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.Member
{
	public class MemberService : BaseService, IMemberService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public MemberService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}

		public Task<T> CreateMemberAsync<T>(CreateMemberDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/Member/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetMemberDetailAsync<T>(int memberno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + (memberno == 0 || memberno == null ? "api/v1/Member/Details" : "api/v1/Member/Details?memberno=" + memberno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetMemberLedgerAsync<T>(string searchkey, int rankno, int areaassign, int dutystatus, int appstatus, string gender, int province, string AccessToken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/Member/Ledger?searchkey=" + searchkey.Trim() + "&rankno=" + rankno + "&areaassign=" + areaassign + "&dutystatus=" + dutystatus + "&appstatus=" + appstatus + "&gender=" + gender + "&province=" + province,
				AccessToken = AccessToken,
			});
		}

		public Task<T> DeleteMemberAsync<T>(int memberno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Data = memberno,
				ApiUrl = _apiURL + (memberno == 0 || memberno == null ? "api/v1/Member/Remove" : "api/v1/Member/Remove?memberno=" + memberno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> CreateMemberJournalAsync<T>(CreateMemberJournalDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/MemberJournal/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetMemberJournalAsync<T>(string searchkey, int memberno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/MemberJournal/Ledger?searchkey=" + searchkey.Trim() + "&memberno=" + memberno,
				AccessToken = accesstoken,
			});



		}
	}
}
