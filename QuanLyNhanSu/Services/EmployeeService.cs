
using Npgsql;
using NpgsqlTypes;
using QuanLyNhanSu.Interfaces;
using QuanLyNhanSu.Models;
using QuanLyNhanSu.Models.ModelDTO;
using System.Data;
using System.Net;
using System.Reflection;
using System.Xml.Linq;

namespace QuanLyNhanSu.Services
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IConfiguration _configuration;
		public EmployeeService(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public async Task<EmployeeDTO> AddEmployee(EmployeeDTO employeeDTO)
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));
			await conn.OpenAsync();
			using var command = new NpgsqlCommand("AddEmployee", conn)
			{
				CommandType = CommandType.StoredProcedure,
				Parameters =
					{
						new NpgsqlParameter("@p_employeeid", NpgsqlDbType.Varchar) { Value = Guid.NewGuid().ToString() },
						new NpgsqlParameter("@p_employeecode", NpgsqlDbType.Varchar) { Value = employeeDTO.Code },
						new NpgsqlParameter("@p_fullname", NpgsqlDbType.Varchar) { Value = employeeDTO.Name },
						new NpgsqlParameter("@p_birthdate", NpgsqlDbType.Date) { Value = employeeDTO.BirthDate },
						new NpgsqlParameter("@p_address", NpgsqlDbType.Varchar) { Value = employeeDTO.Address },
						new NpgsqlParameter("@p_phonenumber", NpgsqlDbType.Varchar) { Value = employeeDTO.PhoneNumber },
						new NpgsqlParameter("@p_email", NpgsqlDbType.Varchar) { Value = employeeDTO.Email },
						new NpgsqlParameter("@p_departmentid", NpgsqlDbType.Varchar) { Value = employeeDTO.DepartmentID },
						new NpgsqlParameter("@p_positionid", NpgsqlDbType.Varchar) { Value = employeeDTO.PositionID },
						new NpgsqlParameter("@p_datestart", NpgsqlDbType.Date) { Value = DateTime.Now },
						new NpgsqlParameter("@p_gender", NpgsqlDbType.Integer) { Value = employeeDTO.Gender },
						new NpgsqlParameter("@p_userid", NpgsqlDbType.Varchar) { Value = Guid.NewGuid().ToString() },
						new NpgsqlParameter("@p_avatar", NpgsqlDbType.Varchar) { Value = employeeDTO.Avatar },
						new NpgsqlParameter("@p_username", NpgsqlDbType.Varchar) { Value = employeeDTO.UserName },
						new NpgsqlParameter("@p_skills", NpgsqlDbType.Array | NpgsqlDbType.Text) { Value = employeeDTO.SkillList },
						new NpgsqlParameter("@p_salaryid", NpgsqlDbType.Varchar) { Value = Guid.NewGuid().ToString() },
						new NpgsqlParameter("@p_salaryamount", NpgsqlDbType.Numeric) { Value = employeeDTO.SalaryAmount },
						new NpgsqlParameter("@p_salarystartdate", NpgsqlDbType.Date) { Value = employeeDTO.DateStart },
					}
			};
			await using var reader = await command.ExecuteReaderAsync();
			return employeeDTO;
		}

		public async Task<string> DeleteEmployee(string employeeId)
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));
			await conn.OpenAsync();
			using var command = new NpgsqlCommand("DeleteEmployee", conn)
			{
				CommandType = CommandType.StoredProcedure,
				Parameters =
					{
						new NpgsqlParameter("@p_employeeid", NpgsqlDbType.Varchar) { Value = employeeId }
					}
			};
			await using var reader = await command.ExecuteReaderAsync();
			return employeeId;
		}

		public async Task<string> DeleteMultiEmployee(List<string> employeeId)
		{
			if (employeeId == null || employeeId.Count == 0)
			{
				return "No EmployeeIDs provided for deletion.";
			}

			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));
			await conn.OpenAsync();

			using var command = new NpgsqlCommand("DeleteEmployees", conn)
			{
				CommandType = CommandType.StoredProcedure
			};

			command.Parameters.AddWithValue("p_employeeids", employeeId.ToArray());

			await command.ExecuteNonQueryAsync();

			return "Employee(s) deleted successfully.";
		}



		public async Task<List<EmployeeView>> GetEmployeeViews(string? name, string? departmentID, string? positionID, string? projectID, string? skillID, int? pageNum, int? pageSize)
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));
			await conn.OpenAsync();
			var employeeviews = new List<EmployeeView>();
			using var cmd = new NpgsqlCommand("SELECT * FROM GetFilteredEmployees(@search_name, @department_id, @position_id, @project_id, @skill_id, @page_num, @page_size)", conn)
			{
				CommandType = System.Data.CommandType.Text,
				Parameters =
				{
					new NpgsqlParameter("@search_name", NpgsqlDbType.Varchar) { Value = string.IsNullOrEmpty(name) ? "" : name },
					new NpgsqlParameter("@department_id", NpgsqlDbType.Varchar) { Value = string.IsNullOrEmpty(departmentID) ? "" : departmentID },
					new NpgsqlParameter("@position_id", NpgsqlDbType.Varchar) { Value = string.IsNullOrEmpty(positionID) ? "" : positionID },
					new NpgsqlParameter("@project_id", NpgsqlDbType.Varchar) { Value = string.IsNullOrEmpty(projectID) ? "" : projectID },
					new NpgsqlParameter("@skill_id", NpgsqlDbType.Varchar) { Value = string.IsNullOrEmpty(skillID) ? "" : skillID },
					new NpgsqlParameter("@page_num", NpgsqlDbType.Integer) { Value = pageNum},
					new NpgsqlParameter("@page_size", NpgsqlDbType.Integer) { Value = pageSize}
				}
			};
			using (var reader = await cmd.ExecuteReaderAsync())
			{
				while (reader.Read())
				{
					string? Id = reader["id"].ToString();
					string? Employeecode = reader["employeecode"].ToString();
					string? Name = reader["hoten"].ToString();
					string? Department = reader["phongban"].ToString();
					string? Position = reader["vitri"].ToString();
					string? Project = reader["duan"].ToString();
					string? Salary = reader["mucluong"].ToString();

					// Xử lý dữ liệu
					employeeviews.Add(new EmployeeView { Id = Id, Employeecode = Employeecode, Name = Name, Department = Department, Position = Position, Project = Project, Salary = Salary });
				}
			}
			return employeeviews;
		}
	public async Task<EmployeeDTO> UpdateEmployee(EmployeeDTO employeeDTO)
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));
			await conn.OpenAsync();
			using var command = new NpgsqlCommand("UpdateEmployee", conn)
			{
				CommandType = CommandType.StoredProcedure,
				Parameters =
				{
					new NpgsqlParameter("@p_employeeid", NpgsqlDbType.Varchar) { Value = employeeDTO.Id },
					new NpgsqlParameter("@p_employeecode", NpgsqlDbType.Varchar) { Value = employeeDTO.Code },
					new NpgsqlParameter("@p_fullname", NpgsqlDbType.Varchar) { Value = employeeDTO.Name },
					new NpgsqlParameter("@p_birthdate", NpgsqlDbType.Date) { Value = employeeDTO.BirthDate },
					new NpgsqlParameter("@p_address", NpgsqlDbType.Varchar) { Value = employeeDTO.Address },
					new NpgsqlParameter("@p_phonenumber", NpgsqlDbType.Varchar) { Value = employeeDTO.PhoneNumber },
					new NpgsqlParameter("@p_email", NpgsqlDbType.Varchar) { Value = employeeDTO.Email },
					new NpgsqlParameter("@p_departmentid", NpgsqlDbType.Varchar) { Value = employeeDTO.DepartmentID },
					new NpgsqlParameter("@p_positionid", NpgsqlDbType.Varchar) { Value = employeeDTO.PositionID },
					new NpgsqlParameter("@p_datestart", NpgsqlDbType.Date) { Value = employeeDTO.DateStart },
					new NpgsqlParameter("@p_gender", NpgsqlDbType.Integer) { Value = employeeDTO.Gender },
                    //new NpgsqlParameter("@p_userid", NpgsqlDbType.Varchar) { Value = employeeDTO.UserID },
                    new NpgsqlParameter("@p_avatar", NpgsqlDbType.Varchar) { Value = employeeDTO.Avatar },
                    //new NpgsqlParameter("@p_username", NpgsqlDbType.Varchar) { Value = employeeDTO.UserName },
                    new NpgsqlParameter("@p_skills", NpgsqlDbType.Array | NpgsqlDbType.Text) { Value = employeeDTO.SkillList },
                    //new NpgsqlParameter("@p_salaryid", NpgsqlDbType.Varchar) { Value = employeeDTO.SalaryID },
                    new NpgsqlParameter("@p_salaryamount", NpgsqlDbType.Numeric) { Value = employeeDTO.SalaryAmount },
				}
			};
			await using var reader = await command.ExecuteReaderAsync();
			return employeeDTO;
		}



		public async Task<string> UpdateProjectEmployee(string employeeId, string projectId, DateTime startDate)
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));

			await conn.OpenAsync();
			using var command1 = new NpgsqlCommand("EmployeeJoinProject", conn)
			{
				CommandType = CommandType.StoredProcedure,
				Parameters =
						{
							new NpgsqlParameter("@p_employeeid", NpgsqlDbType.Varchar){ Value = employeeId },
							new NpgsqlParameter("@p_projectid", NpgsqlDbType.Varchar){ Value= projectId},
              	new NpgsqlParameter("@p_date", NpgsqlDbType.Date){ Value= startDate}

						}
			};
			await using var reader = await command1.ExecuteReaderAsync();
			return employeeId;
		}


		public async Task<string> UpdateSalaryEmployee(string employeeId, double salaryAmount, DateTime startDate)

		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));

			await conn.OpenAsync();
			using var command1 = new NpgsqlCommand("UpdateSalary", conn)
			{
				CommandType = CommandType.StoredProcedure,
				Parameters =
						{
							new NpgsqlParameter("@p_employeeid", NpgsqlDbType.Varchar){ Value = employeeId },
							new NpgsqlParameter("@p_salaryamount", NpgsqlDbType.Numeric){ Value= salaryAmount},
							new NpgsqlParameter("@p_date", NpgsqlDbType.Date){ Value= startDate}


						}
			};
			await using var reader = await command1.ExecuteReaderAsync();
			return employeeId;
		}


		public async Task<List<EmployeeInProjectView>> GetEmployeeInProjectView(string projectId)
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));
			await conn.OpenAsync();
			var employeeviews = new List<EmployeeInProjectView>();
			using var cmd = new NpgsqlCommand("SELECT * FROM getEmployeesInProject(@project_id)", conn)
			{
				CommandType = System.Data.CommandType.Text,
				Parameters =
				{
					new NpgsqlParameter("@project_id", NpgsqlDbType.Varchar) { Value = projectId },
				}
			};
			using (var reader = await cmd.ExecuteReaderAsync())
			{
				while (reader.Read())
				{
					string? Id = reader["id"].ToString();
					string? EmployeeCode = reader["employeeCode"].ToString();
					string? FullName = reader["fullName"].ToString();
					DateTime DateStart = Convert.ToDateTime(reader["dateStart"]);

					// Xử lý dữ liệu
					employeeviews.Add(new EmployeeInProjectView { Id = Id, EmployeeCode = EmployeeCode, FullName = FullName, DateStart = DateStart });
				}
			}
			return employeeviews;
		}

		public async Task<int> CountEmployeesInProject(string projectId)
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));
			await conn.OpenAsync();
			int count = 0;
			using var cmd = new NpgsqlCommand("SELECT CountEmployeesInProject(@project_id)", conn)
			{
				CommandType = System.Data.CommandType.Text,
				Parameters =
				{
					new NpgsqlParameter("@project_id", NpgsqlDbType.Varchar) { Value = projectId },
				}
			};
			using (var reader = await cmd.ExecuteReaderAsync())
			{
				while (reader.Read())
				{
					int employee_count = Convert.ToInt32(reader["countemployeesinproject"]);
					count = employee_count;
				}
			}
			return count;
		}

		public async Task<int> CountEmployeesInAnyProject()
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));
			await conn.OpenAsync();
			int count = 0;
			using var cmd = new NpgsqlCommand("SELECT CountEmployeesInAnyProject()", conn)
			{
				CommandType = System.Data.CommandType.Text,
			};
			using (var reader = await cmd.ExecuteReaderAsync())
			{
				while (reader.Read())
				{
					int employee_count = Convert.ToInt32(reader["countemployeesinanyproject"]);
					count = employee_count;
				}
			}
			return count;
		}
		public async Task<EmployeeViewProc> GetEmployeeByID(string employeeId)
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));

			await conn.OpenAsync();

			using var cmd = new NpgsqlCommand("SELECT * FROM GetEmployeeInfo(@p_employeeid)", conn)
			{
				CommandType = System.Data.CommandType.Text,
				Parameters =
				{
					new NpgsqlParameter("@p_employeeid", NpgsqlDbType.Varchar) { Value = employeeId },
				}

			};
			using (var reader = await cmd.ExecuteReaderAsync())
			{
				reader.Read();

				string? code = reader["EmployeeCode"].ToString();
				string? name = reader["FullName"].ToString();
				DateTime birthdate = Convert.ToDateTime(reader["BirthDate"]);
				string? address = reader["Address"].ToString();
				string? phonenum = reader["PhoneNumber"].ToString();
				string? email = reader["Email"].ToString();
				string? depname = reader["DepartmentName"].ToString();
				string? posiname = reader["PositionName"].ToString();
				DateTime? datestart = Convert.ToDateTime(reader["DateStart"]);
				int? gender = Convert.ToInt32(reader["Gender"]);
				string? projectname = reader["ProjectName"].ToString();
				DateTime? projectstart = Convert.ToDateTime(reader["ProjectStart"]);
				DateTime? projectend = reader["ProjectEnd"] is DBNull ? (DateTime?)null : Convert.ToDateTime(reader["ProjectEnd"]);
				double? moneybonus = Convert.ToDouble(reader["MoneyBonus"]);
				double? salaryamount = Convert.ToDouble(reader["SalaryAmount"]);

				var res = new EmployeeViewProc() { Code = code, Name = name, BirthDate = birthdate, Address = address, PhoneNumber = phonenum, Email = email, DepartmentName = depname, PositionName = posiname, DateStart = datestart, Gender = gender, ProjectName = projectname, ProjectStart = projectstart, ProjectEnd = projectend, MoneyBonus = moneybonus, SalaryAmount = salaryamount };
				return res;
			}

		}
	}
}
