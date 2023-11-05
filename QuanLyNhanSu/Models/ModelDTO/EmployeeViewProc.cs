namespace QuanLyNhanSu.Models.ModelDTO
{
	public class EmployeeViewProc
	{
		public string? Code { get; set; }
		public string? Name { get; set; }
		public DateTime BirthDate { get; set; }
		public string? Address { set; get; }
		public string? PhoneNumber { set; get; }
		public string? Email { set; get; }
		public string? DepartmentID { set; get; }
		public string? PositionID { set; get; }
		public string? SkillID { set; get; }
		public DateTime? DateStart { get; set; }
		public int? Gender { set; get; }
		public string? ProjectName { set; get; }
		public DateTime? ProjectStart { set; get; }
		public DateTime? ProjectEnd { set; get; }
		public double? MoneyBonus { set; get; }
		public double? SalaryAmount { set; get; }
	}
}
