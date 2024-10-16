using BFPR4B.Web.Models.DTO.Member;

namespace BFPR4B.Web.Services.IServices.Member
{
	public interface IMemberEligibilityService
	{
		Task<T> CreateMemberEligibilityAsync<T>(CreateMemberEligibilityDTO parameters, string accesstoken);
		Task<T> GetMemberEligibilityDetailAsync<T>(int detno, string accesstoken);
		Task<T> GetMemberEligibilityLedgerAsync<T>(string searchkey, int memberno, int eligibilityno, string AccessToken);
		Task<T> DeleteMemberEligibilityAsync<T>(int detno, string accesstoken);
	}
}
