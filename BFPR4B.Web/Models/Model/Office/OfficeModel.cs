using System.ComponentModel.DataAnnotations;

namespace BFPR4B.Web.Models.Model.Office
{
	public class OfficeModel
	{
		[Key]
		public int Officeno { get; set; } = 0;
		public string Officecode { get; set; } = "";
		public string Officename { get; set; } = "";
		public bool Required { get; set; } = false;
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class OfficeJournalModel
	{
		[Key]
		public int Detno { get; set; } = 0;
		public int Officeno { get; set; } = 0;
		public string Description { get; set; } = "";
		public bool Required { get; set; } = false;
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}
}
