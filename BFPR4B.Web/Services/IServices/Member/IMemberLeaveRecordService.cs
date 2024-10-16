using BFPR4B.Web.Models.DTO.Location;
using BFPR4B.Web.Models.DTO.Member;

namespace BFPR4B.Web.Services.IServices.Member
{
	public interface IMemberLeaveRecordService
	{
		Task<T> CreateMemberLeaveRecordAsync<T>(CreateMemberLeaveRecordDTO parameters, string accesstoken);
		Task<T> GetMemberLeaveRecordDetailAsync<T>(int detno, string accesstoken);
		Task<T> GetMemberLeaveRecordLedgerAsync<T>(string searchkey, int memberno, string AccessToken);
		Task<T> DeleteMemberLeaveRecordAsync<T>(int detno, string accesstoken);

	}
}
