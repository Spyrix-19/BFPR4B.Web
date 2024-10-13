using System.ComponentModel.DataAnnotations;

namespace BFPR4B.Web.Models.Model.GAD
{
	public class EventModel
	{
		[Key]
		public int Eventno { get; set; } = 0;
		public string Eventname { get; set; } = "";
		public DateTime Eventdatefrom { get; set; } = Convert.ToDateTime("1/1/1900");
		public DateTime Eventdateto { get; set; } = Convert.ToDateTime("1/1/1900");
		public string Eventtime { get; set; } = "";
		public int Eventtype { get; set; } = 0;
		public string EventtypeName { get; set; } = "";
		public string Venue { get; set; } = "";
		public int Status { get; set; } = 0;
		public string StatusName { get; set; } = "";
		public string Remarks { get; set; } = "";
		public int Male { get; set; } = 0;
		public int Female { get; set; } = 0;
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = ""!;
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class EventAttachmentModel
	{
		[Key]
		public int Attachmentno { get; set; } = 0;
		public int Eventno { get; set; } = 0;
		public string Title { get; set; } = "";
		public string Description { get; set; } = "";
		public byte[] Attachment { get; set; } = new byte[0];
		public string Attachmenttype { get; set; } = "";
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class EventJournalModel
	{
		[Key]
		public int Detno { get; set; } = 0;
		public int Eventno { get; set; } = 0;
		public string Description { get; set; } = "";
		public bool Required { get; set; } = false;
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

}
