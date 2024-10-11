using BFPR4B.Web.Models.DTO.Location;

namespace BFPR4B.Web.Services.IServices.Location
{
	public interface IRegionService
	{
		Task<T> CreateRegionAsync<T>(CreateRegionDTO parameters, string accesstoken);
		Task<T> GetRegionDetailAsync<T>(int regionno, string accesstoken);
		Task<T> GetRegionLedgerAsync<T>(string searchkey, int divisionno, string AccessToken);
		Task<T> DeleteRegionAsync<T>(int regionno, string accesstoken);

		//Task<T> CreateRegionJournalAsync<T>(CreateRegionJournalDTO parameters, string accesstoken);
		//Task<T> GetRegionJournalAsync<T>(string searchkey, int gendetno, string accesstoken);
	}
}
