using BFPR4B.Web.Models.DTO.User;

namespace BFPR4B.Web.Services.IServices.User
{
	public interface IUserService
	{
		Task<T> CreateUserAsync<T>(CreateUserDTO parameters, string accesstoken);
		Task<T> UpdateUserAsync<T>(UpdateUserDTO parameters, string accesstoken);
		Task<T> GetUserDetailAsync<T>(int userno, string accesstoken);
		Task<T> GetUserDetailByBadgeAsync<T>(string accountnumber);
		Task<T> GetUserLedgerAsync<T>(string searchkey, int role, string AccessToken);
		Task<T> DeleteUserAsync<T>(int userno, string accesstoken);
		Task<T> ActivateUserAsync<T>(ActivateUserPasswordDTO parameters, string accesstoken);
		Task<T> UnlockUserAsync<T>(UnLockUserPasswordDTO parameters, string accesstoken);
		Task<T> UpdateUserPasswordAsync<T>(UpdateUserPasswordDTO parameters, string accesstoken);
		Task<T> UpdateUserPasswordExpiryAsync<T>(UpdatePasswordExpiry parameters, string accesstoken);

		Task<T> CreateUserJournalAsync<T>(CreateUserJournalDTO parameters, string accesstoken);
		Task<T> GetUserJournalAsync<T>(string searchkey, int userno, string accesstoken);
	}
}
