
using Npgsql;
using NpgsqlTypes;
using QuanLyNhanSu.Interfaces;
using QuanLyNhanSu.Models;
using QuanLyNhanSu.Models.ModelDTO;
using System.Data;
using System.Net;

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
                        new NpgsqlParameter("@p_username", NpgsqlDbType.Varchar) { Value = employeeDTO.UseName }
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



        public async Task<List<EmployeeView>> GetEmployeeViews(string? name, string? departmentID, string? positionID, string? projectID, int? pageNum, int? pageSize)
        {
            await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));
            await conn.OpenAsync();
            var employeeviews = new List<EmployeeView>();
            using var cmd = new NpgsqlCommand("SELECT * FROM GetFilteredEmployees(@search_name, @department_id, @position_id, @project_id, @page_num, @page_size)", conn)
            {
                CommandType = System.Data.CommandType.Text,
                Parameters =
                {
                    new NpgsqlParameter("@search_name", NpgsqlDbType.Varchar) { Value = name ?? (object)DBNull.Value },
                    new NpgsqlParameter("@department_id", NpgsqlDbType.Varchar) { Value = departmentID ?? (object)DBNull.Value },
                    new NpgsqlParameter("@position_id", NpgsqlDbType.Varchar) { Value = positionID ?? (object)DBNull.Value },
                    new NpgsqlParameter("@project_id", NpgsqlDbType.Varchar) { Value = projectID ?? (object)DBNull.Value },
                    new NpgsqlParameter("@page_num", NpgsqlDbType.Integer) { Value = pageNum != null ? pageNum : 1},
                    new NpgsqlParameter("@page_size", NpgsqlDbType.Integer) { Value = pageSize != null ? pageSize: 10}
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
                    employeeviews.Add(new EmployeeView { Id = Id, Employeecode = Employeecode, Name = Name, Department = Department, Position = Position, Project = Project, Salary = Salary});

                }
            }
            return employeeviews;
        }

        public Task<EmployeeDTO> UpdateEmployee(EmployeeDTO employeeDTO)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateProjectEmployee(string employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateSalaryEmployee(string employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
