namespace BFPR4B.Web.Services.IServices.Framework
{
	public interface ISystemService
	{
		Task<T> GetGentables<T>(string searchkey, string tablename, int parentcode, int subparentcode, string accesstoken);
	}
}
