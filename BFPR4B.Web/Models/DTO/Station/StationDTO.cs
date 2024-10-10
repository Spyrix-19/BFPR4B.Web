namespace BFPR4B.Web.Models.DTO.Station
{
	public class CreateStationDTO
	{
		public int Stationno { get; set; } = 0;
		public string Unitcode { get; set; } = "";
		public string Stationname { get; set; } = "";
		public int Stationtype { get; set; } = 0;
		public int Areaassign { get; set; } = 0;
		public string Streetaddress { get; set; } = "";
		public int Cityno { get; set; } = 0;
		public int Provinceno { get; set; } = 0;
		public int Regionno { get; set; } = 0;
		public decimal Latitude { get; set; } = 0.00m;
		public decimal Longitude { get; set; } = 0.00m;
		public bool Required { get; set; } = false;
		public int Encodedby { get; set; } = 0;
	}

	public class CreateStationJournalDTO
	{
		public int Stationno { get; set; } = 0;
		public int Encodedby { get; set; } = 0;
		public List<CreateStationJournalClass> journallist { get; set; }
	}

	public class CreateStationJournalClass
	{
		public int Detno { get; set; } = 0;
		public string Description { get; set; } = "";
		public bool Required { get; set; } = false;
	}
}
