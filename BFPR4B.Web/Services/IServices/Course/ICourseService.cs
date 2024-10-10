using BFPR4B.Web.Models.DTO.Gentable;

namespace BFPR4B.Web.Services.IServices.Course
{
	public interface ICourseService
	{
		Task<T> CreateCourseAsync<T>(CreateGentableDTO parameters, string accesstoken);
		Task<T> GetCourseDetailAsync<T>(int detno, string accesstoken);
		Task<T> GetCourseLedgerAsync<T>(string searchkey, int parentcode, int subparentcode, string AccessToken);
		Task<T> DeleteCourseAsync<T>(int detno, string accesstoken);

		Task<T> CreateCourseJournalAsync<T>(CreateGentableJournalDTO parameters, string accesstoken);
		Task<T> GetCourseJournalAsync<T>(string searchkey, int gendetno, string accesstoken);
	}
}
