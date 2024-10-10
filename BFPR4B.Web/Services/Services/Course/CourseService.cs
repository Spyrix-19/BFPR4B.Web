using BFPR4B.Utility;
using BFPR4B.Web.Models.DTO.Gentable;
using BFPR4B.Web.Models.DTO.User;
using BFPR4B.Web.Models.System;
using BFPR4B.Web.Services.IServices.Course;
using BFPR4B.Web.Services.Services.Base;

namespace BFPR4B.Web.Services.Services.Course
{
	public class CourseService : BaseService, ICourseService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _apiURL;

		public CourseService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			_apiURL = configuration.GetValue<string>("ServiceUrls:BFPR4B.API");
		}

		public Task<T> CreateCourseAsync<T>(CreateGentableDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/Course/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetCourseDetailAsync<T>(int detno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + (detno == 0 || detno == null ? "api/v1/Course/Details" : "api/v1/Course/Details?detno=" + detno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetCourseLedgerAsync<T>(string searchkey, int parentcode, int subparentcode, string AccessToken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/Course/Ledger?searchkey=" + searchkey.Trim() + "&parentcode=" + parentcode + "&subparentcode=" + subparentcode,
				AccessToken = AccessToken,
			});
		}

		public Task<T> DeleteCourseAsync<T>(int detno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Data = detno,
				ApiUrl = _apiURL + (detno == 0 || detno == null ? "api/v1/Course/Remove" : "api/v1/Course/Remove?detno=" + detno),
				AccessToken = accesstoken,
			});
		}

		public Task<T> CreateCourseJournalAsync<T>(CreateGentableJournalDTO parameters, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = parameters,
				ApiUrl = _apiURL + "api/v1/CourseJournal/Create",
				AccessToken = accesstoken,
			});
		}

		public Task<T> GetCourseJournalAsync<T>(string searchkey, int gendetno, string accesstoken)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				ApiUrl = _apiURL + "api/v1/CourseJournal/Ledger?searchkey=" + searchkey + "&gendetno=" + gendetno,
				AccessToken = accesstoken,
			});
		}



	}
}
