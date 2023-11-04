using QuanLyNhanSu.Models.ModelDTO;

namespace QuanLyNhanSu.Interfaces
{
	public interface IEmployeeService
	{
		Task<string> DeleteEmployee(string employeeId);
		Task<string> UpdateSalaryEmployee(string employeeId, string salaryAmount);
		Task<string> UpdateProjectEmployee(string employeeId, string projectId);
		Task<string> DeleteMultiEmployee(List<string> employeeId);
		Task<List<EmployeeView>> GetEmployeeViews(string? name, string? departmentID, string? positionID, string? projectID, string? skillID, int? pageNum, int? pageSize);
		Task<EmployeeDTO> AddEmployee(EmployeeDTO employeeDTO);
		Task<EmployeeDTO> UpdateEmployee(EmployeeDTO employeeDTO);
	}
}
