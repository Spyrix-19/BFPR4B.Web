using BFPR4B.Web.Models.DTO.Location;
using BFPR4B.Web.Models.DTO.Member;

namespace BFPR4B.Web.Services.IServices.Member
{
	public interface IMemberService
	{
		Task<T> CreateMemberAsync<T>(CreateMemberDTO parameters, string accesstoken);
		Task<T> GetMemberDetailAsync<T>(int memberno, string accesstoken);
		Task<T> GetMemberLedgerAsync<T>(string searchkey, int rankno, int areaassign, int dutystatus, int appstatus, string gender, int province, string AccessToken);
		Task<T> DeleteMemberAsync<T>(int memberno, string accesstoken);

		Task<T> CreateMemberJournalAsync<T>(CreateMemberJournalDTO parameters, string accesstoken);
		Task<T> GetMemberJournalAsync<T>(string searchkey, int memberno, string accesstoken);
	}
}
