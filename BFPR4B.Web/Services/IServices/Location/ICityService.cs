using BFPR4B.Web.Models.DTO.Location;

namespace BFPR4B.Web.Services.IServices.Location
{
	public interface ICityService
	{
		Task<T> CreateCityAsync<T>(CreateCityDTO parameters, string accesstoken);
		Task<T> GetCityDetailAsync<T>(int cityno, string accesstoken);
		Task<T> GetCityLedgerAsync<T>(string searchkey, int provinceno, int regionno, string AccessToken);
		Task<T> DeleteCityAsync<T>(int cityno, string accesstoken);

		Task<T> CreateCityJournalAsync<T>(CreateCityJournalDTO parameters, string accesstoken);
		Task<T> GetCityJournalAsync<T>(string searchkey, int cityno, string accesstoken);
	}
}
