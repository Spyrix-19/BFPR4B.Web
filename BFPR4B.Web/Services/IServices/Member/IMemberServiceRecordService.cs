using BFPR4B.Web.Models.DTO.Location;
using BFPR4B.Web.Models.DTO.Member;

namespace BFPR4B.Web.Services.IServices.Member
{
	public interface IMemberServiceRecordService
	{
		Task<T> CreateMemberServiceRecordAsync<T>(CreateMemberServiceRecordDTO parameters, string accesstoken);
		Task<T> GetMemberServiceRecordDetailAsync<T>(int detno, string accesstoken);
		Task<T> GetMemberServiceRecordLedgerAsync<T>(string searchkey, int memberno, int appstatus, string accesstoken);
		Task<T> DeleteMemberServiceRecordAsync<T>(int detno, string accesstoken);

	}
}
