using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
using QuanLyNhanSu.Interfaces;
using QuanLyNhanSu.Models;
using System.Data;

namespace QuanLyNhanSu.Services
{
	public class Employee_ProjectService : IEmployee_ProjectService
	{
		private readonly IConfiguration _configuration;
		public Employee_ProjectService(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public async Task<string> AddEmployee_Project(string project_id, string[] emp_id)
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));

			await conn.OpenAsync();
			using var command1 = new NpgsqlCommand("add_project", conn)
			{
				CommandType = CommandType.StoredProcedure,
				Parameters =
						{
							new NpgsqlParameter("@project_id", NpgsqlDbType.Varchar){ Value = project_id },
							new NpgsqlParameter("@employee_ids", NpgsqlDbType.Array){ Value= emp_id}
						}
			};
			await using var reader = await command1.ExecuteReaderAsync();
			return project_id;
		}

		public async Task<string> DeleteEmployee_Project(string project_id, string[] emp_id)
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));
			await conn.OpenAsync();
			using var command1 = new NpgsqlCommand("remove_employees_from_project", conn)
			{
				CommandType = CommandType.StoredProcedure,
				Parameters =
						{
							new NpgsqlParameter("@project_id", NpgsqlDbType.Varchar){ Value = project_id },
							new NpgsqlParameter("@employee_ids", NpgsqlDbType.Array){ Value= emp_id}
						}
			};

			return project_id;
		}
	}
}
