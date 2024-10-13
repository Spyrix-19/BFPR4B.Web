using BFPR4B.Web.Models.DTO.GAD;
using BFPR4B.Web.Models.DTO.Location;

namespace BFPR4B.Web.Services.IServices.GAD
{
	public interface IResourceService
	{
		Task<T> CreateResourceAsync<T>(CreateResourceDTO parameters, string accesstoken);
		Task<T> GetResourceDetailAsync<T>(int resourceno, string accesstoken);
		Task<T> GetResourceLedgerAsync<T>(string searchkey, string AccessToken);
		Task<T> DeleteResourceAsync<T>(int resourceno, string accesstoken);

		Task<T> CreateResourceAttachmentAsync<T>(CreateResourceAttachmentDTO parameters, string accesstoken);
		Task<T> GetResourceAttachmentLedgerAsync<T>(string searchkey, int resourceno, string attachmenttype, string AccessToken);
		Task<T> DeleteResourceAttachmentAsync<T>(int attachmentno, string accesstoken);

		Task<T> CreateResourceJournalAsync<T>(CreateResourceJournalDTO parameters, string accesstoken);
		Task<T> GetResourceJournalAsync<T>(string searchkey, int resourceno, string accesstoken);
	}
}
