using BFPR4B.Web.Models.DTO.GAD;
using BFPR4B.Web.Models.DTO.Location;

namespace BFPR4B.Web.Services.IServices.GAD
{
	public interface IEventService
	{
		Task<T> CreateEventAsync<T>(CreateEventDTO parameters, string accesstoken);
		Task<T> GetEventDetailAsync<T>(int eventno, string accesstoken);
		Task<T> GetEventLedgerAsync<T>(string searchkey, int status, int eventtype, string AccessToken);
		Task<T> DeleteEventAsync<T>(int eventno, string accesstoken);

		Task<T> CreateEventAttachmentAsync<T>(CreateEventAttachmentDTO parameters, string accesstoken);
		Task<T> GetEventAttachmentLedgerAsync<T>(string searchkey, int eventno, string attachmenttype, string AccessToken);
		Task<T> DeleteEventAttachmentAsync<T>(int attachmentno, string accesstoken);

		Task<T> CreateEventJournalAsync<T>(CreateEventJournalDTO parameters, string accesstoken);
		Task<T> GetEventJournalAsync<T>(string searchkey, int eventno, string accesstoken);
	}
}
