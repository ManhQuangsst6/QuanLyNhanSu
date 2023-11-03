
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
            using var command = new NpgsqlCommand("add_project", conn)
            {
                CommandType = CommandType.StoredProcedure,
				Parameters =
                    {
                        new NpgsqlParameter("@p_EmployeeID", NpgsqlDbType.Varchar) { Value = Guid.NewGuid().ToString() },
						new NpgsqlParameter("@p_EmployeeCode", NpgsqlDbType.Varchar) { Value = employeeDTO.Code },
						new NpgsqlParameter("@p_FullName", NpgsqlDbType.Varchar) { Value = employeeDTO.Name },
						new NpgsqlParameter("@p_BirthDate", NpgsqlDbType.Date) { Value = employeeDTO.BirthDate },
                        new NpgsqlParameter("@p_Address", NpgsqlDbType.Varchar) { Value = employeeDTO.Address },
                        new NpgsqlParameter("@p_PhoneNumber", NpgsqlDbType.Varchar) { Value = employeeDTO.PhoneNumber },
                        new NpgsqlParameter("@p_Email", NpgsqlDbType.Varchar) { Value = employeeDTO.Email },
                        new NpgsqlParameter("@p_DepartmentID", NpgsqlDbType.Varchar) { Value = employeeDTO.DepartmentID },
                        new NpgsqlParameter("@p_PositionID", NpgsqlDbType.Varchar) { Value = employeeDTO.PositionID },
                        new NpgsqlParameter("@p_DateStart", NpgsqlDbType.Date) { Value = DateTime.Now },
                        new NpgsqlParameter("@p_Gender", NpgsqlDbType.Integer) { Value = employeeDTO.Gender },
                        new NpgsqlParameter("@p_UserID", NpgsqlDbType.Varchar) { Value = Guid.NewGuid().ToString() },
                        new NpgsqlParameter("@p_Avatar", NpgsqlDbType.Varchar) { Value = employeeDTO.Avatar },
                        new NpgsqlParameter("@p_UserName", NpgsqlDbType.Varchar) { Value = employeeDTO.UseName }
                    }
            };
            await using var reader = await command.ExecuteReaderAsync();
            return employeeDTO;
        }

        public Task<string> DeleteEmployee(string employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteMultiEmployee(List<string> employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmployeeView>> GetEmployeeViews(string name, string departmentID, string positionID, string projectID)
        {
            throw new NotImplementedException();
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
