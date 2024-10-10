namespace BFPR4B.Web.Models.DTO.Location
{
	public class CreateCityDTO
	{
		public int Cityno { get; set; } = 0;
		public int Provinceno { get; set; } = 0;
		public string Cityname { get; set; } = "";
		public string Zipcode { get; set; } = "";
		public decimal Sortorder { get; set; } = 0.00m;
		public bool Required { get; set; } = false;
		public int Encodedby { get; set; } = 0;
	}

	public class CreateCityJournalDTO
	{
		public int Cityno { get; set; } = 0;
		public int Encodedby { get; set; } = 0;
		public List<CreateCityJournalClass> Journallist { get; set; } = new List<CreateCityJournalClass>();
	}

	public class CreateCityJournalClass
	{
		public int Detno { get; set; } = 0;
		public string Description { get; set; } = "";
		public bool Required { get; set; } = false;
	}
}
