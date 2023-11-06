using Newtonsoft.Json.Linq;
using QuanLyNhanSu.Models.ModelDTO;

namespace QuanLyNhanSu.Interfaces
{
	public interface IEmployeeService
	{
		Task<string> DeleteEmployee(string employeeId);

		Task<string> UpdateSalaryEmployee(string employeeId, double salaryAmount, DateTime startDate);
		Task<string> UpdateProjectEmployee(string employeeId, string projectId, DateTime startDate);
		Task<string> DeleteMultiEmployee(List<string> employeeId);
		Task<List<EmployeeView>> GetEmployeeViews(string? name, string? departmentID, string? positionID, string? projectID, string? skillID, int? pageNum, int? pageSize);
		Task<EmployeeDTO> AddEmployee(EmployeeDTO employeeDTO);
		Task<EmployeeDTO> UpdateEmployee(EmployeeDTO employeeDTO);
		Task<List<EmployeeInProjectView>> GetEmployeeInProjectView(string projectId);
		Task<int> CountEmployeesInProject(string projectId);
		Task<int> CountEmployeesInAnyProject();
		Task<List<EmployeeViewProc>> GetEmployeeByID(string employeeId);

		Task<JObject> GetEmployeeOBJ(string employeeId);


	}
}
