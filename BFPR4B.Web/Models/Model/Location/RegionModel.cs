using System.ComponentModel.DataAnnotations;

namespace BFPR4B.Web.Models.Model.Location
{
	public class RegionModel
	{
		[Key]
		public int Regionno { get; set; } = 0;
		public string Regioncode { get; set; } = "";
		public string Regionname { get; set; } = "";
		public int Divisionno { get; set; } = 0;
		public string Divisionname { get; set; } = "";
		public decimal Sortorder { get; set; } = 0.00m;
		public bool Required { get; set; } = false;
		public bool Rowmarker { get; set; } = false;
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class RegionJournalModel
	{
		[Key]
		public int Detno { get; set; } = 0;
		public int Regionno { get; set; } = 0;
		public string Description { get; set; } = "";
		public bool Required { get; set; } = false;
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

}
