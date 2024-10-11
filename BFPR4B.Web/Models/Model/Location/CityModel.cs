using System.ComponentModel.DataAnnotations;

namespace BFPR4B.Web.Models.Model.Location
{
	public class CityModel
	{
		[Key]
		public int Cityno { get; set; } = 0;
		public int Provinceno { get; set; } = 0;
		public string Provincename { get; set; } = "";
		public string Cityname { get; set; } = "";
		public string Zipcode { get; set; } = "";
		public string Regioncode { get; set; } = "";
		public string Regionname { get; set; } = "";
		public decimal Sortorder { get; set; } = 0.00m;
		public bool Required { get; set; } = false;
		public bool Rowmarker { get; set; } = false;
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}
}
