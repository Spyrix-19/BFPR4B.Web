using BFPR4B.Web.Models.DTO.Location;

namespace BFPR4B.Web.Services.IServices.Barangay
{
	public interface IBarangayService
	{
		Task<T> CreateBarangayAsync<T>(CreateBarangayDTO parameters, string accesstoken);
		Task<T> GetBarangayDetailAsync<T>(int barangayno, string accesstoken);
		Task<T> GetBarangayLedgerAsync<T>(string searchkey, int cityno, int provinceno, int regionno, string AccessToken);
		Task<T> DeleteBarangayAsync<T>(int barangayno, string accesstoken);

		Task<T> CreateBarangayJournalAsync<T>(CreateBarangayJournalDTO parameters, string accesstoken);
		Task<T> GetBarangayJournalAsync<T>(string searchkey, int barangayno, string accesstoken);
	}
}
