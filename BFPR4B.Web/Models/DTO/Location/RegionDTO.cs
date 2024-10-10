namespace BFPR4B.Web.Models.DTO.Location
{
	public class CreateRegionDTO
	{
		public int Regionno { get; set; } = 0;
		public int Divisionno { get; set; } = 0;
		public string Regioncode { get; set; } = "";
		public string Regionname { get; set; } = "";
		public decimal Sortorder { get; set; } = 0.00m;
		public bool Required { get; set; } = false;
		public int Encodedby { get; set; } = 0;
	}

	public class CreateRegionJournalDTO
	{
		public int Regionno { get; set; } = 0;
		public int Encodedby { get; set; } = 0;
		public List<CreateRegionJournalClass> Journallist { get; set; } = new List<CreateRegionJournalClass>();
	}

	public class CreateRegionJournalClass
	{
		public int Detno { get; set; } = 0;
		public string Description { get; set; } = "";
		public bool Required { get; set; } = false;
	}
}
