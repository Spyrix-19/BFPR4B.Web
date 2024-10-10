using BFPR4B.Web.Models.DTO.Station;

namespace BFPR4B.Web.Services.IServices.Station
{
	public interface IStationService
	{
		Task<T> CreateStationAsync<T>(CreateStationDTO parameters, string accesstoken);
		Task<T> GetStationDetailAsync<T>(int stationno, string accesstoken);
		Task<T> GetStationLedgerAsync<T>(string searchkey, int stationtype, int areaassign, int provinceno, string AccessToken);
		Task<T> DeleteStationAsync<T>(int stationno, string accesstoken);

		Task<T> CreateStationJournalAsync<T>(CreateStationJournalDTO parameters, string accesstoken);
		Task<T> GetStationJournalAsync<T>(string searchkey, int stationno, string accesstoken);
	}
}
