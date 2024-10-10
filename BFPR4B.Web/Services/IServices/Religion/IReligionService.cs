using BFPR4B.Web.Models.DTO.Gentable;

namespace BFPR4B.Web.Services.IServices.Religion
{
	public interface IReligionService
	{
		Task<T> CreateReligionAsync<T>(CreateGentableDTO parameters, string accesstoken);
		Task<T> GetReligionDetailAsync<T>(int detno, string accesstoken);
		Task<T> GetReligionLedgerAsync<T>(string searchkey, int parentcode, int subparentcode, string AccessToken);
		Task<T> DeleteReligionAsync<T>(int detno, string accesstoken);

		Task<T> CreateReligionJournalAsync<T>(CreateGentableJournalDTO parameters, string accesstoken);
		Task<T> GetReligionJournalAsync<T>(string searchkey, int gendetno, string accesstoken);
	}
}
