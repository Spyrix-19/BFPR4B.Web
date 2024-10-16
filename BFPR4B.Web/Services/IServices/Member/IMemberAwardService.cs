using BFPR4B.Web.Models.DTO.Member;

namespace BFPR4B.Web.Services.IServices.Member
{
	public interface IMemberAwardService
	{
		Task<T> CreateMemberAwardAsync<T>(CreateMemberAwardDTO parameters, string accesstoken);
		Task<T> GetMemberAwardDetailAsync<T>(int detno, string accesstoken);
		Task<T> GetMemberAwardLedgerAsync<T>(string searchkey, int memberno, int awardtype, string AccessToken);
		Task<T> DeleteMemberAwardAsync<T>(int detno, string accesstoken);
	}
}
