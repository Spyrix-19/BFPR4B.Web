using System.ComponentModel.DataAnnotations;

namespace BFPR4B.Web.Models.Model.GAD
{
	public class ResourceModel
	{
		[Key]
		public int Resourceno { get; set; } = 0;
		public string Title { get; set; } = "";
		public string Description { get; set; } = "";
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class ResourceAttachmentModel
	{
		[Key]
		public int Attachmentno { get; set; } = 0;
		public int Resourceno { get; set; } = 0;
		public string Title { get; set; } = "";
		public string Description { get; set; } = "";
		public byte[] Attachment { get; set; } = new byte[0];
		public string Attachmenttype { get; set; } = "";
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class ResourceJournalModel
	{
		[Key]
		public int Detno { get; set; } = 0;
		public int Resourceno { get; set; } = 0;
		public string Description { get; set; } = "";
		public bool Required { get; set; } = false;
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}
}
