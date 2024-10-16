using System.ComponentModel.DataAnnotations;

namespace BFPR4B.Web.Models.DTO.Member
{
	public class CreateMemberDTO
	{
		[Key]
		public int Memberno { get; set; } = 0;
		public string Badgeno { get; set; } = "";
		public string Itemno { get; set; } = "";
		public string Lastname { get; set; } = "";
		public string Firstname { get; set; } = "";
		public string Miname { get; set; } = "";
		public string Suffix { get; set; } = "";
		public string Fullname { get; set; } = "";
		public int Rank { get; set; } = 0;
		public DateTime Birthday { get; set; } = Convert.ToDateTime("1/1/1900");
		public int Age { get; set; } = 0;
		public string Gender { get; set; } = "";
		public int Civilstatus { get; set; } = 0;
		public int Religion { get; set; } = 0;
		public string Cellphone { get; set; } = "";
		public string Telephone { get; set; } = "";
		public string Emailaddress { get; set; } = "";
		public string Height { get; set; } = "";
		public string Weight { get; set; } = "";
		public string Bloodtype { get; set; } = "";
		public string Gsis { get; set; } = "";
		public string Pagibig { get; set; } = "";
		public string Philhealth { get; set; } = "";
		public string Sss { get; set; } = "";
		public string Tin { get; set; } = "";
		public string Atm { get; set; } = "";
		public string Streetaddress { get; set; } = "";
		public int Barangayno { get; set; } = 0;
		public int Cityno { get; set; } = 0;
		public int Provinceno { get; set; } = 0;
		public string Zipcode { get; set; } = "";
		public string Streetaddresidential { get; set; } = "";
		public int Barangaynoresidential { get; set; } = 0;
		public int Citynoresidential { get; set; } = 0;
		public int Provincenoresidential { get; set; } = 0;
		public string Zipcoderesidential { get; set; } = "";
		public string Placeofbirth { get; set; } = "";
		public string Spouselastname { get; set; } = "";
		public string Spousefirstname { get; set; } = "";
		public string Spouseminame { get; set; } = "";
		public string Spouseextension { get; set; } = "";
		public string Spousework { get; set; } = "";
		public string Spouseemployer { get; set; } = "";
		public string Spouseemployeraddress { get; set; } = "";
		public string Spousemobile { get; set; } = "";
		public string Spouseemail { get; set; } = "";
		public int Divisionno { get; set; } = 0;
		public int Officeno { get; set; } = 0;
		public string Designation { get; set; } = "";
		public int Areaassign { get; set; } = 0;
		public int Unitassign { get; set; } = 0;
		public DateTime Degs { get; set; } = Convert.ToDateTime("1/1/1900");
		public DateTime Deus { get; set; } = Convert.ToDateTime("1/1/1900");
		public DateTime Defs { get; set; } = Convert.ToDateTime("1/1/1900");
		public DateTime Dpprts { get; set; } = Convert.ToDateTime("1/1/1900");
		public DateTime Dpprps { get; set; } = Convert.ToDateTime("1/1/1900");
		public DateTime Dao { get; set; } = Convert.ToDateTime("1/1/1900");
		public DateTime Dolp { get; set; } = Convert.ToDateTime("1/1/1900");
		public int Modeentry { get; set; } = 0;
		public int Appstatus { get; set; } = 0;
		public int Dutystatus { get; set; } = 0;
		public string Pnpaclass { get; set; } = "";
		public string Remarks { get; set; } = "";
		public int Encodedby { get; set; } = 0;
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class CreateMemberAwardDTO
	{
		[Key]
		public int Detno { get; set; } = 0;
		public int Memberno { get; set; } = 0;
		public string Awardname { get; set; } = "";
		public DateTime Issueddate { get; set; } = Convert.ToDateTime("1/1/1900");
		public string Issuedby { get; set; } = "";
		public int Awardtype { get; set; } = 0;
		public byte[] Attachment { get; set; } = new byte[0];
		public int Encodedby { get; set; } = 0;
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class CreateMemberDependentDTO
	{
		[Key]
		public int Dependentno { get; set; } = 0;
		public int Memberno { get; set; } = 0;
		public string Lastname { get; set; } = "";
		public string Firstname { get; set; } = "";
		public string Miname { get; set; } = "";
		public string Extension { get; set; } = "";
		public int Age { get; set; } = 0;
		public DateTime Birthdate { get; set; } = Convert.ToDateTime("1/1/1900");
		public string Dependentwork { get; set; } = "";
		public string Employer { get; set; } = "";
		public string Address { get; set; } = "";
		public string Contactno { get; set; } = "";
		public string Emailaddress { get; set; } = "";
		public int Dependenttype { get; set; } = 0;
		public int Encodedby { get; set; } = 0;
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class CreateMemberEducationDTO
	{
		[Key]
		public int Educationno { get; set; } = 0;
		public int Memberno { get; set; } = 0;
		public int Educationlevel { get; set; } = 0;
		public string Nameofschool { get; set; } = "";
		public int Course { get; set; } = 0;
		public DateTime Datefrom { get; set; } = Convert.ToDateTime("1/1/1900");
		public DateTime Dateto { get; set; } = Convert.ToDateTime("1/1/1900");
		public string Units { get; set; } = "";
		public int Yeargraduated { get; set; } = 0;
		public int Encodedby { get; set; } = 0;
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class CreateMemberEligibilityDTO
	{
		[Key]
		public int Detno { get; set; } = 0;
		public int Memberno { get; set; } = 0;
		public int Eligibilityno { get; set; } = 0;
		public decimal Rating { get; set; } = 0.00m;
		public DateTime Examinationdate { get; set; } = Convert.ToDateTime("1/1/1900");
		public string Placeofexam { get; set; } = "";
		public string Licensenumber { get; set; } = "";
		public DateTime Validuntil { get; set; } = Convert.ToDateTime("1/1/1900");
		public int Encodedby { get; set; } = 0;
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class CreateMemberJournalDTO
	{
		public int Memberno { get; set; } = 0;
		public int Encodedby { get; set; } = 0;
		public List<CreateMemberJournalClass> Journallist { get; set; } = new List<CreateMemberJournalClass>();
	}

	public class CreateMemberJournalClass
	{
		public int Detno { get; set; } = 0;
		public string Description { get; set; } = "";
		public bool Required { get; set; } = false;
	}


	public class CreateMemberLeaveRecordDTO
	{
		[Key]
		public int Detno { get; set; } = 0;
		public int Memberno { get; set; } = 0;
		public DateTime Periodfrom { get; set; } = Convert.ToDateTime("1/1/1900");
		public DateTime Periodto { get; set; } = Convert.ToDateTime("1/1/1900");
		public string Particulars { get; set; } = "";
		public decimal Vlearned { get; set; } = 0.00m;
		public decimal Vllwp { get; set; } = 0.00m;
		public decimal Vllwop { get; set; } = 0.00m;
		public decimal Slearned { get; set; } = 0.00m;
		public decimal Sllwp { get; set; } = 0.00m;
		public decimal Sllwop { get; set; } = 0.00m;
		public string Actiontaken { get; set; } = "";
		public int Encodedby { get; set; } = 0;
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class CreateMemberServiceRecordDTO
	{
		[Key]
		public int Detno { get; set; } = 0;
		public int Memberno { get; set; } = 0;
		public string Posdes { get; set; } = "";
		public string Unitcode { get; set; } = "";
		public decimal Salary { get; set; } = 0.00m;
		public int Appstatus { get; set; } = 0;
		public DateTime Datefrom { get; set; } = Convert.ToDateTime("1/1/1900");
		public DateTime Dateto { get; set; } = Convert.ToDateTime("1/1/1900");
		public int Encodedby { get; set; } = 0;
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class CreateMemberTrainingDTO
	{
		[Key]
		public int Detno { get; set; } = 0;
		public int Memberno { get; set; } = 0;
		public int Trainingno { get; set; } = 0;
		public DateTime Trainingdatefrom { get; set; } = Convert.ToDateTime("1/1/1900");
		public DateTime Trainingdateto { get; set; } = Convert.ToDateTime("1/1/1900");
		public string Noofhours { get; set; } = "";
		public string Ldtype { get; set; } = "";
		public string Conductedby { get; set; } = "";
		public int Encodedby { get; set; } = 0;
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}
}
