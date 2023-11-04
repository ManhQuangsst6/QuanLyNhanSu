using QuanLyNhanSu.Models;

namespace QuanLyNhanSu.Interfaces
{
	public interface IProjectService
	{
		Task<List<Project>> GetProjectsAsync(string? searchName, int? filterMonth, int? filterYear, int? pageNum, int? pageSize );
		Task<Project> AddProject(Project project);
		Task<Project> UpdateProject(Project project);
		Task<string> UpdateComplete(string id);
		Task<string> DeleteProject(string id);
		Task<Project> GetProjectById(string id);
		Task<Employee> GetEmployeeInProject(string id);
	}
}
