using System.ComponentModel.DataAnnotations;

namespace BFPR4B.Web.Models.Model.Location
{
	public class BarangayModel
	{
		[Key]
		public int Barangayno { get; set; } = 0;
		public int Cityno { get; set; } = 0;
		public string Cityname { get; set; } = "";
		public int Provinceno { get; set; } = 0;
		public string Provincename { get; set; } = "";
		public int Regionno { get; set; } = 0;
		public string Regioncode { get; set; } = "";
		public string Regionname { get; set; } = "";
		public string Barangayname { get; set; } = "";
		public decimal Sortorder { get; set; } = 0.00m;
		public bool Required { get; set; } = false;
		public bool Rowmarker { get; set; } = false;
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class BarangayJournalModel
	{
		[Key]
		public int Detno { get; set; } = 0;
		public int Barangayno { get; set; } = 0;
		public string Description { get; set; } = "";
		public bool Required { get; set; } = false;
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}


}
