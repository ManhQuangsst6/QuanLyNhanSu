using QuanLyNhanSu.Models;

namespace QuanLyNhanSu.Interfaces
{
	public interface IDepartmentService
	{
		Task<List<Department>> GetAllDepartment();
		Task<Department> AddDepartment();
	}
}
