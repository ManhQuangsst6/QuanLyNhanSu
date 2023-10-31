using Npgsql;
using Microsoft.AspNetCore.Mvc;

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
							new NpgsqlParameter("@start_date", NpgsqlDbType.Date){ Value=project.DateStart},
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
							new NpgsqlParameter("@project_id", NpgsqlDbType.Varchar){ Value = Guid.NewGuid().ToString() },
							new NpgsqlParameter("@project_name", NpgsqlDbType.Varchar){ Value=project.Name},
							new NpgsqlParameter("@project_description", NpgsqlDbType.Varchar){ Value=project.Description}
						}
			};
			await using var reader = await command1.ExecuteReaderAsync();
			return project;
		}
		public async Task<Project> UpdateComplete(Project project)
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));

			await conn.OpenAsync();
			using var command1 = new NpgsqlCommand("complete_project", conn)
			{
				CommandType = CommandType.StoredProcedure,
				Parameters =
						{
							new NpgsqlParameter("@project_id", NpgsqlDbType.Varchar){ Value = Guid.NewGuid().ToString() },
							new NpgsqlParameter("@end_date", NpgsqlDbType.Date){ Value=project.DateStart},
						}
			};
			await using var reader = await command1.ExecuteReaderAsync();
			return project;
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

		public async Task<Project> GetProjectById(string searchName, int? filterDay, int? filterMonth)
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));

			await conn.OpenAsync();
			var projects = new Project();
			using (var cmd = new NpgsqlCommand("search_projects", conn))
			{
				cmd.CommandType = System.Data.CommandType.StoredProcedure;

				cmd.Parameters.AddWithValue("search_name", searchName ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("filter_day", filterDay ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("filter_month", filterMonth ?? (object)DBNull.Value);

				using (var reader = await cmd.ExecuteReaderAsync())
				{
					while (reader.Read())
					{
						string? id = reader["ID"].ToString();
						string? name = reader["Name"].ToString();
						DateTime dateStart = Convert.ToDateTime(reader["DateStart"]);

						DateTime? dateEnd = reader["DateEnd"] is DBNull ? (DateTime?)null : Convert.ToDateTime(reader["DateEnd"]);

						// Xử lý dữ liệu
						projects = new Project { ID = id, Name = name, DateStart = dateStart, DateEnd = dateEnd };

					}
				}
			}
			return projects;
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


	}
}
