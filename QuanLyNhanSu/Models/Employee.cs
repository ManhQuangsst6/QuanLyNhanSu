namespace QuanLyNhanSu.Models
{
	public class Employee
	{
		public Guid ID { get; set; }
		public string FullName { get; set; }
		public DateTime? BirthDate { get; set; }
		public string? Address { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Email { get; set; }
		public Guid DepartmentID { get; set; }
		public Guid PositionID { get; set; }
		public DateTime? DateStart { get; set; }
		public DateTime? DateEnd { get; set; }
		public int Gender { get; set; }
		public Guid UserID { get; set; }
	}
}
