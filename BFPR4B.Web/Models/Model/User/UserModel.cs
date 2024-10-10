using System.ComponentModel.DataAnnotations;

namespace BFPR4B.Web.Models.Model.User
{
	public class UserModel
	{
		[Key]
		public int Userno { get; set; } = 0;
		public string Accountnumber { get; set; } = "";
		public string Userpass { get; set; } = "";
		public string Fullname { get; set; } = "";
		public string Mobileno { get; set; } = "";
		public string Emailaddress { get; set; } = "";
		public byte[] Passwordsalt { get; set; } = new byte[0];
		public bool Activeuser { get; set; } = false;
		public DateTime Inactivedate { get; set; } = Convert.ToDateTime("1/1/1900");
		public DateTime Lastaccess { get; set; } = Convert.ToDateTime("1/1/1900");
		public bool Passwordlock { get; set; } = false;
		public DateTime Lockdate { get; set; } = Convert.ToDateTime("1/1/1900");
		public DateTime Passwordexpiry { get; set; } = Convert.ToDateTime("1/1/1900");
		public DateTime Invalidtime { get; set; } = Convert.ToDateTime("1/1/1900");
		public int Invalidattempt { get; set; } = 0;
		public int Role { get; set; } = 0;
		public string Rolename { get; set; } = "";
		public byte[] Picture { get; set; } = new byte[0];
		public string Picturetype { get; set; } = "";
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class UserJournalModel
	{
		[Key]
		public int Detno { get; set; } = 0;
		public int Userno { get; set; } = 0;
		public string Description { get; set; } = "";
		public bool Required { get; set; } = false;
		public int Encodedby { get; set; } = 0;
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}
}
