using Npgsql;
using QuanLyNhanSu.Interfaces;
using QuanLyNhanSu.Models;

namespace QuanLyNhanSu.Services
{
	public class DepartmentService : IDepartmentService
	{
		private readonly IConfiguration _configuration;
		public DepartmentService(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public Task<Department> AddDepartment()
		{
			throw new NotImplementedException();
		}

		public async Task<List<Department>> GetAllDepartment()
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));

			await conn.OpenAsync();
			using (NpgsqlCommand cmd = new NpgsqlCommand("select * from Departments", conn))
			{
				var result = await cmd.ExecuteReaderAsync();
				var departments = new List<Department>();
				while (await result.ReadAsync())
				{
					var department = new Department
					{
						//Department_Id = result.GetInt32(0),
						//Department_Name = result.GetString(1)
					};
					departments.Add(department);
				}

				result.Close();
				conn.Close();
				return departments;
			}
		}
	}
}
