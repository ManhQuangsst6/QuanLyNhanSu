namespace QuanLyNhanSu.Models
{
	public class Project
	{
		public Guid ID { get; set; }
		public string Name { get; set; }
		public DateTime? DateStart { get; set; }
		public DateTime? DateEnd { get; set; }

	}
}
