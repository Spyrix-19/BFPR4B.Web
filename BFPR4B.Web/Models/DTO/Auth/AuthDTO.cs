using BFPR4B.Web.Models.Model.User;

namespace BFPR4B.Web.Models.DTO.Auth
{
	public class LoginResponseDTO
	{
		public bool isError { get; set; } = false;
		public string Message { get; set; } = "";
		public UserModel User { get; set; } = new UserModel();
		public string AccessToken { get; set; } = "";
	}

	public class LoginRequestDTO
	{
		public string Accountnumber { get; set; } = "";
		public string Userpass { get; set; } = "";
		public string Client_id { get; set; } = "";
		public string Client_secret { get; set; } = "";
		public string Grant_type { get; set; } = "";
		public string Login_type { get; set; } = "";
	}

	public class RegistrationRequestDTO
	{
		public int Userno { get; set; } = 0;
		public string Accountnumber { get; set; } = "";
		public string Userpass { get; set; } = "";
		public bool Activeuser { get; set; } = false;
		public int Role { get; set; } = 0;
		public string Base64 { get; set; } = "";
		public int Encodedby { get; set; } = 0;
	}

}
