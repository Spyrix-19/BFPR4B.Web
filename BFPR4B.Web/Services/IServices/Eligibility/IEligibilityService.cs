using BFPR4B.Web.Models.DTO.Gentable;

namespace BFPR4B.Web.Services.IServices.Eligibility
{
	public interface IEligibilityService
	{
		Task<T> CreateEligibilityAsync<T>(CreateGentableDTO parameters, string accesstoken);
		Task<T> GetEligibilityDetailAsync<T>(int detno, string accesstoken);
		Task<T> GetEligibilityLedgerAsync<T>(string searchkey, int parentcode, int subparentcode, string AccessToken);
		Task<T> DeleteEligibilityAsync<T>(int detno, string accesstoken);

		Task<T> CreateEligibilityJournalAsync<T>(CreateGentableJournalDTO parameters, string accesstoken);
		Task<T> GetEligibilityJournalAsync<T>(string searchkey, int gendetno, string accesstoken);
	}
}
