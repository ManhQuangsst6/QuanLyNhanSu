namespace QuanLyNhanSu.Models
{
	public class Employee_Project
	{
		public Guid EmployeeID { get; set; }
		public Guid ProjectID { get; set; }
		public double? MoneyBonus { get; set; }
		public DateTime? DateStart { get; set; }
		public DateTime? DateEnd { get; set; }

	}
}
