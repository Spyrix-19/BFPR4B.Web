namespace BFPR4B.Web.Models.DTO.Gentable
{
	public class CreateGentableDTO
	{
		public int Detno { get; set; } = 0;
		public string Recordcode { get; set; } = "";
		public string Description { get; set; } = "";
		public int Parentcode { get; set; } = 0;
		public int Subparentcode { get; set; } = 0;
		public bool Required { get; set; } = false;
		public int Encodedby { get; set; } = 0;
	}

	public class CreateGentableJournalDTO
	{
		public int Gendetno { get; set; } = 0;
		public string Tablename { get; set; } = "";
		public int Encodedby { get; set; } = 0;
		public List<CreateGentableJournalClass> Journallist { get; set; } = new List<CreateGentableJournalClass>();
	}

	public class CreateGentableJournalClass
	{
		public int Detno { get; set; } = 0;
		public string Description { get; set; } = "";
		public bool Required { get; set; } = false;
	}
}
