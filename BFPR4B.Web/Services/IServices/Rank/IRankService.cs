using BFPR4B.Web.Models.DTO.Gentable;

namespace BFPR4B.Web.Services.IServices.Rank
{
	public interface IRankService
	{
		Task<T> CreateRankAsync<T>(CreateGentableDTO parameters, string accesstoken);
		Task<T> GetRankDetailAsync<T>(int detno, string accesstoken);
		Task<T> GetRankLedgerAsync<T>(string searchkey, int parentcode, int subparentcode, string AccessToken);
		Task<T> DeleteRankAsync<T>(int detno, string accesstoken);

		Task<T> CreateRankJournalAsync<T>(CreateGentableJournalDTO parameters, string accesstoken);
		Task<T> GetRankJournalAsync<T>(string searchkey, int gendetno, string accesstoken);
	}
}
