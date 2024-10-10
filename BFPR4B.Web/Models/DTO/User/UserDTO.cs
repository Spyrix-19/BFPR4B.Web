namespace BFPR4B.Web.Models.DTO.User
{
	public class CreateUserDTO
	{
		public int Userno { get; set; } = 0;
		public string Accountnumber { get; set; } = "";
		public string Userpass { get; set; } = "";
		public bool Activeuser { get; set; } = false;
		public int Role { get; set; } = 0;
		public string Base64 { get; set; } = "";
		public int Encodedby { get; set; } = 0;
	}

	public class UpdateUserDTO
	{
		public int Userno { get; set; } = 0;
		public int Role { get; set; } = 0;
		public string Base64 { get; set; } = "";
	}

	public class ActivateUserPasswordDTO
	{
		public int Userno { get; set; } = 0;
		public bool Activeuser { get; set; } = false;
	}

	public class UnLockUserPasswordDTO
	{
		public int Userno { get; set; } = 0;
		public bool Passwordlock { get; set; } = false;
	}

	public class UpdateUserPasswordDTO
	{
		public int Userno { get; set; } = 0;
		public string Userpass { get; set; } = "";
	}

	public class UpdatePasswordExpiry
	{
		public int Userno { get; set; } = 0;
		public DateTime Passwordexpiry { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class CreateUserJournalDTO
	{
		public int Userno { get; set; } = 0;
		public int Encodedby { get; set; } = 0;
		public List<CreateUserJournalClass> Journallist { get; set; } = new List<CreateUserJournalClass>();
	}

	public class CreateUserJournalClass
	{
		public int Detno { get; set; } = 0;
		public string Description { get; set; } = "";
		public bool Required { get; set; } = false;
	}
}
