using System.ComponentModel.DataAnnotations;

namespace BFPR4B.Web.Models.Model.Member
{
	public class MemberModel
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
		public string Rankname { get; set; } = "";
		public DateTime Birthday { get; set; } = Convert.ToDateTime("1/1/1900");
		public int Age { get; set; } = 0;
		public string Gender { get; set; } = "";
		public int Civilstatus { get; set; } = 0;
		public string Civilstatusname { get; set; } = "";
		public int Religion { get; set; } = 0;
		public string Religionname { get; set; } = "";
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
		public string Barangayname { get; set; } = "";
		public int Cityno { get; set; } = 0;
		public string Cityname { get; set; } = "";
		public int Provinceno { get; set; } = 0;
		public string Provincename { get; set; } = "";
		public string Zipcode { get; set; } = "";
		public string Streetaddresidential { get; set; } = "";
		public int Barangaynoresidential { get; set; } = 0;
		public string Barangaynameresidential { get; set; } = "";
		public int Citynoresidential { get; set; } = 0;
		public string Citynameresidential { get; set; } = "";
		public int Provincenoresidential { get; set; } = 0;
		public string Provincenameresidential { get; set; } = "";
		public string Zipcoderesidential { get; set; } = "";
		public string Placeofbirth { get; set; } = "";
		public int Divisionno { get; set; } = 0;
		public string Divisionname { get; set; } = "";
		public int Officeno { get; set; } = 0;
		public string Officename { get; set; } = "";
		public string Designation { get; set; } = "";
		public int Areaassign { get; set; } = 0;
		public string Areaassignname { get; set; } = "";
		public int Unitassign { get; set; } = 0;
		public string Unitassignname { get; set; } = "";
		public DateTime Degs { get; set; } = Convert.ToDateTime("1/1/1900");
		public DateTime Deus { get; set; } = Convert.ToDateTime("1/1/1900");
		public DateTime Defs { get; set; } = Convert.ToDateTime("1/1/1900");
		public DateTime Dpprts { get; set; } = Convert.ToDateTime("1/1/1900");
		public DateTime Dpprps { get; set; } = Convert.ToDateTime("1/1/1900");
		public DateTime Dao { get; set; } = Convert.ToDateTime("1/1/1900");
		public DateTime Dolp { get; set; } = Convert.ToDateTime("1/1/1900");
		public int Modeentry { get; set; } = 0;
		public string Modeentryname { get; set; } = "";
		public int Appstatus { get; set; } = 0;
		public string Appstatusname { get; set; } = "";
		public int Dutystatus { get; set; } = 0;
		public string Dutystatusname { get; set; } = "";
		public string Pnpaclass { get; set; } = "";
		public string Remarks { get; set; } = "";
		public string Lengthofservice { get; set; } = "";
		public bool Isdeleted { get; set; }
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class MemberLedgerModel
	{
		[Key]
		public int Memberno { get; set; } = 0;
		public string Badgeno { get; set; } = "";
		public string Itemno { get; set; } = "";
		public string Rankname { get; set; } = "";
		public string Lastname { get; set; } = "";
		public string Firstname { get; set; } = "";
		public string Miname { get; set; } = "";
		public string Suffix { get; set; } = "";
		public string Fullname { get; set; } = "";
		public string Areaassignname { get; set; } = "";
		public string Unitassignname { get; set; } = "";
		public string Gender { get; set; } = "";
		public string Civilstatusname { get; set; } = "";
		public string Designation { get; set; } = "";
		public DateTime Birthday { get; set; } = Convert.ToDateTime("1/1/1900");
		public int Age { get; set; } = 0;
		public string Dutystatusname { get; set; } = "";
		public string Appstatusname { get; set; } = "";
		public string Lengthofservice { get; set; } = "";
		public string Remarks { get; set; } = "";
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class MemberAwardModel
	{
		[Key]
		public int Detno { get; set; } = 0;
		public int Memberno { get; set; } = 0;
		public string Awardname { get; set; } = "";
		public DateTime Issueddate { get; set; } = Convert.ToDateTime("1/1/1900");
		public string Issuedby { get; set; } = "";
		public int Awardtype { get; set; } = 0;
		public string Awardtypename { get; set; } = "";
		public byte[] Attachment { get; set; } = new byte[0];
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class MemberDependentModel
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
		public string Dependenttypename { get; set; } = "";
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class MemberEducationModel
	{
		[Key]
		public int Educationno { get; set; } = 0;
		public int Memberno { get; set; } = 0;
		public int Educationlevel { get; set; } = 0;
		public string Educationlevelname { get; set; } = "";
		public string Nameofschool { get; set; } = "";
		public int Course { get; set; } = 0;
		public string Coursename { get; set; } = "";
		public DateTime Datefrom { get; set; } = Convert.ToDateTime("1/1/1900");
		public DateTime Dateto { get; set; } = Convert.ToDateTime("1/1/1900");
		public string Units { get; set; } = "";
		public int Yeargraduated { get; set; } = 0;
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class MemberEligibilityModel
	{
		[Key]
		public int Detno { get; set; } = 0;
		public int Memberno { get; set; } = 0;
		public int Eligibilityno { get; set; } = 0;
		public string Eligibilityname { get; set; } = "";
		public decimal Rating { get; set; } = 0.00m;
		public DateTime Examinationdate { get; set; } = Convert.ToDateTime("1/1/1900");
		public string Placeofexam { get; set; } = "";
		public string Licensenumber { get; set; } = "";
		public DateTime Validuntil { get; set; } = Convert.ToDateTime("1/1/1900");
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class MemberJournalModel
	{
		[Key]
		public int Detno { get; set; } = 0;
		public int Memberno { get; set; } = 0;
		public string Description { get; set; } = "";
		public bool Required { get; set; } = false;
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class MemberLeaveRecordModel
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
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class MemberServiceRecordModel
	{
		[Key]
		public int Detno { get; set; } = 0;
		public int Memberno { get; set; } = 0;
		public string Posdes { get; set; } = "";
		public string Unitcode { get; set; } = "";
		public decimal Salary { get; set; } = 0.00m;
		public int Appstatus { get; set; } = 0;
		public string Appstatusname { get; set; } = "";
		public DateTime Datefrom { get; set; } = Convert.ToDateTime("1/1/1900");
		public DateTime Dateto { get; set; } = Convert.ToDateTime("1/1/1900");
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}

	public class MemberTrainingModel
	{
		[Key]
		public int Detno { get; set; } = 0;
		public int Memberno { get; set; } = 0;
		public int Trainingno { get; set; } = 0;
		public string Trainingname { get; set; } = "";
		public DateTime Trainingdatefrom { get; set; } = Convert.ToDateTime("1/1/1900");
		public DateTime Trainingdateto { get; set; } = Convert.ToDateTime("1/1/1900");
		public string Noofhours { get; set; } = "";
		public string Ldtype { get; set; } = "";
		public string Conductedby { get; set; } = "";
		public int Encodedby { get; set; } = 0;
		public string Encodedbyname { get; set; } = "";
		public DateTime Dateencoded { get; set; } = Convert.ToDateTime("1/1/1900");
	}
}
