using System.ComponentModel.DataAnnotations;

namespace BFPR4B.Web.Models.Model.Location
{
	public class BarangayModel
	{
		[Key]
		public int Barangayno { get; set; } = 0;
		public int Cityno { get; set; } = 0;
		public string Cityname { get; set; } = "";
		public string Barangayname { get; set; } = "";
		public decimal Sortorder { get; set; } = 0.00m;
		public bool Required { get; set; } = false;
		public bool Rowmarker { get; set; } = false;
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}
}
