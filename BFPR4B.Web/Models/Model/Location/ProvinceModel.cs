using System.ComponentModel.DataAnnotations;

namespace BFPR4B.Web.Models.Model.Location
{
	public class ProvinceModel
	{
		[Key]
		public int Provinceno { get; set; } = 0;
		public int Regionno { get; set; } = 0;
		public string Regioncode { get; set; } = "";
		public string Regionname { get; set; } = "";
		public string Provincename { get; set; } = "";
		public decimal Sortorder { get; set; } = 0.00m;
		public bool Required { get; set; } = false;
		public bool Rowmarker { get; set; } = false;
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}
}
