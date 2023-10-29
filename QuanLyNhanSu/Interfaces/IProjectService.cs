using QuanLyNhanSu.Models;

namespace QuanLyNhanSu.Interfaces
{
	public interface IProjectService
	{
		Task<List<Project>> GetProjectsAsync();
		Task<Project> AddProject(Project project);
		Task<Project> UpdateProject(Project project);
		Task<Project> DeleteProject(string id);

		Task<Project> GetProjectById(string id);
		Task<Employee> GetEmployeeInProject(string id);
	}
}
