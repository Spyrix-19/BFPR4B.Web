namespace BFPR4B.Web.Models.DTO.Location
{
	public class CreateBarangayDTO
	{
		public int Barangayno { get; set; } = 0;
		public int Cityno { get; set; } = 0;
		public string Barangayname { get; set; } = "";
		public bool Required { get; set; } = false;
		public decimal Sortorder { get; set; } = 0.00m;
		public int Encodedby { get; set; } = 0;
	}

	public class CreateBarangayJournalDTO
	{
		public int Barangayno { get; set; } = 0;
		public int Encodedby { get; set; } = 0;
		public List<CreateBarangayJournalClass> Journallist { get; set; } = new List<CreateBarangayJournalClass>();
	}

	public class CreateBarangayJournalClass
	{
		public int Detno { get; set; } = 0;
		public string Description { get; set; } = "";
		public bool Required { get; set; } = false;
	}
}
