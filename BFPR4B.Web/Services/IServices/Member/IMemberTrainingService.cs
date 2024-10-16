using BFPR4B.Web.Models.DTO.Location;
using BFPR4B.Web.Models.DTO.Member;

namespace BFPR4B.Web.Services.IServices.Member
{
	public interface IMemberTrainingService
	{
		Task<T> CreateMemberTrainingAsync<T>(CreateMemberTrainingDTO parameters, string accesstoken);
		Task<T> GetMemberTrainingDetailAsync<T>(int detno, string accesstoken);
		Task<T> GetMemberTrainingLedgerAsync<T>(string searchkey, int memberno, int trainingno, int trainingtype, string AccessToken);
		Task<T> DeleteMemberTrainingAsync<T>(int detno, string accesstoken);

	}
}
