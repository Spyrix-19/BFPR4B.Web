namespace BFPR4B.Web.Models.DTO.Location
{
	public class CreateProvinceDTO
	{
		public int Provinceno { get; set; } = 0;
		public int Regionno { get; set; } = 0;
		public string Provincename { get; set; } = "";
		public decimal Sortorder { get; set; } = 0.00m;
		public bool Required { get; set; } = false;
		public int Encodedby { get; set; } = 0;
	}

	public class CreateProvinceJournalDTO
	{
		public int Provinceno { get; set; } = 0;
		public int Encodedby { get; set; } = 0;
		public List<CreateProvinceJournalClass> Journallist { get; set; } = new List<CreateProvinceJournalClass>();
	}

	public class CreateProvinceJournalClass
	{
		public int Detno { get; set; } = 0;
		public string Description { get; set; } = "";
		public bool Required { get; set; } = false;
	}
}
