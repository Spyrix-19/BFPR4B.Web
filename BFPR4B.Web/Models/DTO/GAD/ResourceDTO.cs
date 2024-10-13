namespace BFPR4B.Web.Models.DTO.GAD
{
	public class CreateResourceDTO
	{
		public int Resourceno { get; set; } = 0;
		public string Title { get; set; } = "";
		public string Description { get; set; } = "";
		public int Encodedby { get; set; } = 0;
	}

	public class CreateResourceAttachmentDTO
	{
		public int Attachmentno { get; set; } = 0;
		public int Resourceno { get; set; } = 0;
		public string Title { get; set; } = "";
		public string Description { get; set; } = "";
		public byte[] Attachment { get; set; } = new byte[0];
		public string Attachmenttype { get; set; } = "";
		public int Encodedby { get; set; } = 0;
	}

	public class CreateResourceJournalDTO
	{
		public int Resourceno { get; set; } = 0;
		public int Encodedby { get; set; } = 0;
		public List<CreateResourceJournalClass> Journallist { get; set; } = new List<CreateResourceJournalClass>();
	}

	public class CreateResourceJournalClass
	{
		public int Detno { get; set; } = 0;
		public string Description { get; set; } = "";
		public bool Required { get; set; } = false;
	}
}
