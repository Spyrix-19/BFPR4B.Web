using BFPR4B.Web.Models.DTO.Location;

namespace BFPR4B.Web.Services.IServices.Location
{
	public interface IProvinceService
	{
		Task<T> CreateProvinceAsync<T>(CreateProvinceDTO parameters, string accesstoken);
		Task<T> GetProvinceDetailAsync<T>(int provinceno, string accesstoken);
		Task<T> GetProvinceLedgerAsync<T>(string searchkey, int regionno, string AccessToken);
		Task<T> DeleteProvinceAsync<T>(int provinceno, string accesstoken);

		//Task<T> CreateProvinceJournalAsync<T>(CreateProvinceJournalDTO parameters, string accesstoken);
		//Task<T> GetProvinceJournalAsync<T>(string searchkey, int gendetno, string accesstoken);
	}
}
