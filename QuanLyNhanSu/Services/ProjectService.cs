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
			using var command1 = new NpgsqlCommand("add_project", conn)
			{
				CommandType = CommandType.StoredProcedure,
				Parameters =
						{
							new NpgsqlParameter("@project_id", NpgsqlDbType.Varchar){ Value = Guid.NewGuid().ToString() },
							new NpgsqlParameter("@project_name", NpgsqlDbType.Varchar){ Value=project.Name},
							new NpgsqlParameter("@start_date", NpgsqlDbType.Date){ Value=DateTime.Now},
							new NpgsqlParameter("@project_description", NpgsqlDbType.Varchar){ Value=project.Description}
						}
			};
			await using var reader = await command1.ExecuteReaderAsync();
			return project;
		}
		public async Task<Project> UpdateProject(Project project)
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));

			await conn.OpenAsync();
			using var command1 = new NpgsqlCommand("update_project", conn)
			{
				CommandType = CommandType.StoredProcedure,
				Parameters =
						{
							new NpgsqlParameter("@project_id", NpgsqlDbType.Varchar){ Value = project.ID },
							new NpgsqlParameter("@project_name", NpgsqlDbType.Varchar){ Value=project.Name},
							new NpgsqlParameter("@project_description", NpgsqlDbType.Varchar){ Value=project.Description}
						}
			};
			await using var reader = await command1.ExecuteReaderAsync();
			return project;
		}
		public async Task<string> UpdateComplete(string id)
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));

			await conn.OpenAsync();
			using var command1 = new NpgsqlCommand("complete_project", conn)
			{
				CommandType = CommandType.StoredProcedure,
				Parameters =
						{
							new NpgsqlParameter("@project_id", NpgsqlDbType.Varchar){ Value =id },
							new NpgsqlParameter("@end_date", NpgsqlDbType.Date){ Value=DateTime.Now},
						}
			};
			await using var reader = await command1.ExecuteReaderAsync();
			return id;
		}
		public async Task<string> DeleteProject(string id)
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));
			await conn.OpenAsync();
			using var cmd = new NpgsqlCommand("delete_project", conn);
			cmd.CommandType = System.Data.CommandType.StoredProcedure;

			cmd.Parameters.AddWithValue("project_id", id);

			cmd.ExecuteNonQuery();

			return id;
		}

		public Task<Employee> GetEmployeeInProject(string id)
		{
			throw new NotImplementedException();
		}

		public async Task<Project> GetProjectById(string projectID)
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));

			await conn.OpenAsync();

			using var cmd = new NpgsqlCommand("SELECT * FROM GetProjectInfo(@p_ProjectID)", conn)
			{
				CommandType = System.Data.CommandType.Text,
				Parameters =
				{
					new NpgsqlParameter("@p_ProjectID", NpgsqlDbType.Varchar) { Value = projectID },
				}

			};
			using (var reader = await cmd.ExecuteReaderAsync())
			{
				reader.Read();

				string? name = reader["ProjectName"].ToString();
				DateTime dateStart = Convert.ToDateTime(reader["ProjectDateStart"]);
				string? description = reader["ProjectDescription"].ToString();
				DateTime? dateEnd = reader["ProjectDateEnd"] is DBNull ? (DateTime?)null : Convert.ToDateTime(reader["ProjectDateEnd"]);

				var res = new Project() { ID = null, Name = name, DateStart = dateStart, DateEnd = dateEnd, Description = description };
				return res;
			}

		}

		public async Task<List<Project>> GetProjectsAsync(string? searchName, int? filterMonth, int? filterYear, int? pageNum, int? pageSize)
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));

			await conn.OpenAsync();
			var projects = new List<Project>();

			using var cmd = new NpgsqlCommand("SELECT * FROM search_projects(@search_name, @filter_month, @filter_year, @page_number, @page_size)", conn)
			{
				CommandType = System.Data.CommandType.Text,
				Parameters =
				{
					new NpgsqlParameter("@search_name", NpgsqlDbType.Varchar) { Value = searchName ?? (object)DBNull.Value },
					new NpgsqlParameter("@filter_month", NpgsqlDbType.Integer) { Value = filterMonth ?? (object)DBNull.Value },
					new NpgsqlParameter("@filter_year", NpgsqlDbType.Integer) { Value = filterYear ?? (object)DBNull.Value },
					new NpgsqlParameter("@page_number", NpgsqlDbType.Integer) { Value = pageNum ?? (object)DBNull.Value },
					new NpgsqlParameter("@page_size", NpgsqlDbType.Integer) { Value = pageSize ?? (object)DBNull.Value }
				}
			};
			using (var reader = await cmd.ExecuteReaderAsync())
			{
				while (reader.Read())
				{
					string? id = reader["project_id"].ToString();
					string? name = reader["project_name"].ToString();
					DateTime dateStart = Convert.ToDateTime(reader["project_start_date"]);
					string? description = reader["project_description"].ToString();
					DateTime? dateEnd = reader["project_end_date"] is DBNull ? (DateTime?)null : Convert.ToDateTime(reader["project_end_date"]);

					// Xử lý dữ liệu
					projects.Add(new Project { ID = id, Name = name, DateStart = dateStart, DateEnd = dateEnd, Description = description });

				}
			}
			return projects;
		}


	}
}
