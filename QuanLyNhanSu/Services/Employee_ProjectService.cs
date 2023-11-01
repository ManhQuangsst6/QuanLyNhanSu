using Npgsql;
using NpgsqlTypes;
using QuanLyNhanSu.Interfaces;
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
		//public async Task<string> AddEmployee_Project(string project_id, List<string> emp_id)
		//{

		//	await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));
		//	await conn.OpenAsync();
		//	var employeeIdsArray = emp_id.Select(id => new NpgsqlParameter { Value = id, NpgsqlDbType = NpgsqlDbType.Text }).ToArray();
		//	using var command1 = new NpgsqlCommand("add_project", conn)
		//	{
		//		CommandType = CommandType.StoredProcedure,
		//		Parameters =
		//				{
		//					new NpgsqlParameter("@project_id", NpgsqlDbType.Varchar){ Value = project_id },
		//					new NpgsqlParameter("@employee_ids", NpgsqlDbType.Array | NpgsqlDbType.Text){ Value= employeeIdsArray}
		//				}
		//	};
		//	await using var reader = await command1.ExecuteReaderAsync();
		//	return project_id;
		//}
		public async Task<string> AddEmployee_Project(string project_id, List<string> employee_ids)
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));
			await conn.OpenAsync();
			using var command1 = new NpgsqlCommand("add_employees_to_project", conn)
			{
				CommandType = CommandType.StoredProcedure
			};

			command1.Parameters.Add(new NpgsqlParameter("@project_id", NpgsqlDbType.Varchar) { Value = project_id });

			command1.Parameters.Add(new NpgsqlParameter("@employee_ids", NpgsqlDbType.Array | NpgsqlDbType.Varchar)
			{
				Value = employee_ids.ToArray()
			});

			await using var reader = await command1.ExecuteReaderAsync();
			return project_id;
		}

		public async Task<string> DeleteEmployee_Project(string project_id, List<string> employee_ids)
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));
			await conn.OpenAsync();


			using var command1 = new NpgsqlCommand("remove_employees_from_project", conn)
			{
				CommandType = CommandType.StoredProcedure
			};

			command1.Parameters.Add(new NpgsqlParameter("@project_id", NpgsqlDbType.Varchar) { Value = project_id });

			command1.Parameters.Add(new NpgsqlParameter("@employee_ids", NpgsqlDbType.Array | NpgsqlDbType.Varchar)
			{
				Value = employee_ids.ToArray()
			});

			await using var reader = await command1.ExecuteReaderAsync();
			return project_id;
		}
	}
}
