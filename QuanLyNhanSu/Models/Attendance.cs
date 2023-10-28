namespace QuanLyNhanSu.Models
{
	public class Attendance
	{
		public string ID { get; set; }
		public string EmployeeID { get; set; }
		public DateTime? DateCurrent { get; set; }
		public bool Checked { get; set; } = false;
	}
}
