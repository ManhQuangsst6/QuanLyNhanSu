using QuanLyNhanSu.Models;

namespace QuanLyNhanSu.Interfaces
{
	public interface IProjectService
	{
		Task<List<Project>> GetProjectsAsync();
		Task<Project> AddProject(Project project);
		Task<Project> UpdateProject(Project project);
		Task<Project> UpdateComplete(Project project);
		Task<string> DeleteProject(string id);

		Task<Project> GetProjectById(string searchName, int? filterDay, int? filterMonth);
		Task<Employee> GetEmployeeInProject(string id);
	}
}
