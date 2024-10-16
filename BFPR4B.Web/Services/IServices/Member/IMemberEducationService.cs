using BFPR4B.Web.Models.DTO.Location;
using BFPR4B.Web.Models.DTO.Member;

namespace BFPR4B.Web.Services.IServices.Member
{
	public interface IMemberEducationService
	{
		Task<T> CreateMemberEducationAsync<T>(CreateMemberEducationDTO parameters, string accesstoken);
		Task<T> GetMemberEducationDetailAsync<T>(int detno, string accesstoken);
		Task<T> GetMemberEducationLedgerAsync<T>(string searchkey, int memberno, int course, int educationallevel, string AccessToken);
		Task<T> DeleteMemberEducationAsync<T>(int detno, string accesstoken);

	}
}
