using Npgsql;
using NpgsqlTypes;
using QuanLyNhanSu.Interfaces;
using QuanLyNhanSu.Models;
using System.Data;

namespace QuanLyNhanSu.Services
{
	public class ProjectService : IProjectService
	{
		private readonly IConfiguration _configuration;
		public ProjectService(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public async Task<Project> AddProject(Project project)
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));

			await conn.OpenAsync();
			using var command1 = new NpgsqlCommand("AddProject", conn)
			{
				CommandType = CommandType.StoredProcedure,
				Parameters =
						{
							new NpgsqlParameter("@projectid", NpgsqlDbType.Varchar){ Value = Guid.NewGuid().ToString() },
							new NpgsqlParameter("@projectname", NpgsqlDbType.Varchar){ Value=project.Name},
							new NpgsqlParameter("@datestart", NpgsqlDbType.Date){ Value=project.DateStart}
						}
			};
			await using var reader = await command1.ExecuteReaderAsync();
			return project;
		}

		public Task<Project> DeleteProject(string id)
		{
			throw new NotImplementedException();
		}

		public Task<Employee> GetEmployeeInProject(string id)
		{
			throw new NotImplementedException();
		}

		public Task<Project> GetProjectById(string id)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Project>> GetProjectsAsync()
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));

			await conn.OpenAsync();
			var projects = new List<Project>();
			using (var cmd = new NpgsqlCommand("select * from GetProjects", conn))
			{
				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						string? id = reader["ID"].ToString();
						string? name = reader["Name"].ToString();
						DateTime dateStart = Convert.ToDateTime(reader["DateStart"]);

						DateTime? dateEnd = reader["DateEnd"] is DBNull ? (DateTime?)null : Convert.ToDateTime(reader["DateEnd"]);

						// Xử lý dữ liệu
						projects.Add(new Project { ID = id, Name = name, DateStart = dateStart, DateEnd = dateEnd });

					}
				}
			}
			return projects;
		}

		public Task<Project> UpdateProject(Project project)
		{
			throw new NotImplementedException();
		}
	}
}
