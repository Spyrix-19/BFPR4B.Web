using BFPR4B.Web.Models.DTO.Gentable;

namespace BFPR4B.Web.Services.IServices.Training
{
	public interface ITrainingService
	{
		Task<T> CreateTrainingAsync<T>(CreateGentableDTO parameters, string accesstoken);
		Task<T> GetTrainingDetailAsync<T>(int detno, string accesstoken);
		Task<T> GetTrainingLedgerAsync<T>(string searchkey, int parentcode, int subparentcode, string AccessToken);
		Task<T> DeleteTrainingAsync<T>(int detno, string accesstoken);

		Task<T> CreateTrainingJournalAsync<T>(CreateGentableJournalDTO parameters, string accesstoken);
		Task<T> GetTrainingJournalAsync<T>(string searchkey, int gendetno, string accesstoken);
	}
}
