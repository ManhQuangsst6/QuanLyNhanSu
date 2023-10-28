namespace QuanLyNhanSu.Models
{
	public class Salary
	{
		public Guid ID { get; set; }
		public string EmployeeID { get; set; }
		public double SalaryAmount { get; set; }
		public DateTime? DateStart { get; set; }
		public DateTime? DateEnd { get; set; }
	}
}
