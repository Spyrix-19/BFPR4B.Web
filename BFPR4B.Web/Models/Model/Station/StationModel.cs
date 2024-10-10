using System.ComponentModel.DataAnnotations;

namespace BFPR4B.Web.Models.Model.Station
{
	public class StationModel
	{
		[Key]
		public int Stationno { get; set; } = 0;
		public string Unitcode { get; set; } = "";
		public string Stationname { get; set; } = "";
		public int Stationtype { get; set; } = 0;
		public string Stationtypename { get; set; } = "";
		public int Areaassign { get; set; } = 0;
		public string Areaassignname { get; set; } = "";
		public string Streetaddress { get; set; } = "";
		public int Barangayno { get; set; } = 0;
		public string Barangayname { get; set; } = "";
		public int Cityno { get; set; } = 0;
		public string Cityname { get; set; } = "";
		public int Provinceno { get; set; } = 0;
		public string Provincename { get; set; } = "";
		public int Regionno { get; set; } = 0;
		public string Regionname { get; set; } = "";
		public decimal Latitude { get; set; } = 0.00m;
		public decimal Longitude { get; set; } = 0.00m;
		public bool Required { get; set; } = false;
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class StationJournalModel
	{
		[Key]
		public int Detno { get; set; } = 0;
		public int Stationno { get; set; } = 0;
		public string Description { get; set; } = "";
		public bool Required { get; set; } = false;
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}


	public class StationContactModel
	{
		[Key]
		public int Contactcode { get; set; } = 0;
		public int Stationno { get; set; } = 0;
		public string Contactname { get; set; } = "";
		public bool Selected { get; set; } = false;
		public bool Email { get; set; } = false;
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class StationContactJournalModel
	{
		[Key]
		public int Detno { get; set; } = 0;
		public int Contactcode { get; set; } = 0;
		public string Description { get; set; } = "";
		public bool Required { get; set; } = false;
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}
}
