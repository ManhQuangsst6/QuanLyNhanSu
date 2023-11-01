namespace QuanLyNhanSu.Interfaces
{
	public interface IEmployee_ProjectService
	{
		Task<string> AddEmployee_Project(string project_id, List<string> emp_id);
		Task<string> DeleteEmployee_Project(string project_id, List<string> emp_id);
	}
}
