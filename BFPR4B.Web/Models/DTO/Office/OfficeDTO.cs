namespace BFPR4B.Web.Models.DTO.Office
{
	public class CreateOfficeDTO
	{
		public int Officeno { get; set; } = 0;
		public string Officecode { get; set; } = "";
		public string Officename { get; set; } = "";
		public int Officetype { get; set; } = 0;
		public bool Required { get; set; } = false;
		public int Encodedby { get; set; } = 0;
	}

	public class CreateOfficeJournalDTO
	{
		public int Officeno { get; set; } = 0;
		public int Encodedby { get; set; } = 0;
		public List<CreateOfficeJournalClass> journallist { get; set; }
	}

	public class CreateOfficeJournalClass
	{
		public int Detno { get; set; } = 0;
		public string Description { get; set; } = "";
		public bool Required { get; set; } = false;
	}
}
