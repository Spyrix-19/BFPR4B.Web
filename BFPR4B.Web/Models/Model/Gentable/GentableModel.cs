using System.ComponentModel.DataAnnotations;

namespace BFPR4B.Web.Models.Model.Gentable
{
	public class GentableModel
	{
		[Key]
		public int Detno { get; set; } = 0;
		public string Recordcode { get; set; } = "";
		public string Description { get; set; } = "";
		public int Parentcode { get; set; } = 0;
		public string Parentcodename { get; set; } = "";
		public int Subparentcode { get; set; } = 0;
		public string Subparentcodename { get; set; } = "";
		public string Tablename { get; set; } = "";
		public bool Required { get; set; } = false;
		public int Sortorder { get; set; } = 0;
		public int Createdby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Datecreated { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class GentableJournalModel
	{
		[Key]
		public int Detno { get; set; } = 0;
		public int Gendetno { get; set; } = 0;
		public string Description { get; set; } = "";
		public string Tablename { get; set; } = "";
		public bool Required { get; set; } = false;
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}
}
