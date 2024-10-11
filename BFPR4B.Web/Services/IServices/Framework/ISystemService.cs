namespace BFPR4B.Web.Services.IServices.Framework
{
	public interface ISystemService
	{
		Task<T> GetGentables<T>(string searchkey, string tablename, int parentcode, int subparentcode);
		Task<T> GetBarangay<T>(string searchkey, int cityno, int provinceno, int regionno);
		Task<T> GetCity<T>(string searchkey, int provinceno, int regionno);
		Task<T> GetProvince<T>(string searchkey, int regionno);
		Task<T> GetRegion<T>(string searchkey);
	}
}
