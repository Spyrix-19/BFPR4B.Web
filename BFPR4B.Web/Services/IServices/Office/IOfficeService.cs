using BFPR4B.Web.Models.DTO.Office;

namespace BFPR4B.Web.Services.IServices.Office
{
	public interface IOfficeService
	{
		Task<T> CreateOfficeAsync<T>(CreateOfficeDTO parameters, string accesstoken);
		Task<T> GetOfficeDetailAsync<T>(int officeno, string accesstoken);
		Task<T> GetOfficeLedgerAsync<T>(string searchkey, int officetype, string AccessToken);
		Task<T> DeleteOfficeAsync<T>(int officeno, string accesstoken);

		Task<T> CreateOfficeJournalAsync<T>(CreateOfficeJournalDTO parameters, string accesstoken);
		Task<T> GetOfficeJournalAsync<T>(string searchkey, int officeno, string accesstoken);
	}
}
