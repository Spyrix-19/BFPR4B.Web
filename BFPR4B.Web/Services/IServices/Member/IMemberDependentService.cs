using BFPR4B.Web.Models.DTO.Location;
using BFPR4B.Web.Models.DTO.Member;

namespace BFPR4B.Web.Services.IServices.Member
{
	public interface IMemberDependentService
	{
		Task<T> CreateMemberDependentAsync<T>(CreateMemberDependentDTO parameters, string accesstoken);
		Task<T> GetMemberDependentDetailAsync<T>(int detno, string accesstoken);
		Task<T> GetMemberDependentLedgerAsync<T>(string searchkey, int memberno, string AccessToken);
		Task<T> DeleteMemberDependentAsync<T>(int detno, string accesstoken);

	}
}
