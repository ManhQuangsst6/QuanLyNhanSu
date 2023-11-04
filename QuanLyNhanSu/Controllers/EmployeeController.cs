using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Interfaces;
using QuanLyNhanSu.Models;
using QuanLyNhanSu.Models.ModelDTO;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeDTO employeeDTO)
        {
            try
            {
                var res = await _employeeService.AddEmployee(employeeDTO);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(string employeeId)
        {
            try
            {
                var res = await _employeeService.DeleteEmployee(employeeId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMultipleEmployees(List<string> employeeIds)
        {
            try
            {
                var res = await _employeeService.DeleteMultiEmployee(employeeIds);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeViews(string? name, string? departmentID, string? positionID, string? projectID, string? skillID, int? pageNum, int? pageSize)
        {
            try
            {
                var res = await _employeeService.GetEmployeeViews(name, departmentID, positionID, projectID, skillID, pageNum, pageSize);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
		[HttpPut]
		public async Task<IActionResult> UpdateProjectEmployee(string employeeId, string projectId)
		{
			try
			{
				var res = await _employeeService.UpdateProjectEmployee(employeeId, projectId);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}
		[HttpPut]
		public async Task<IActionResult> UpdateSalaryEmployee(string employeeId, double salaryAmount)
		{
			try
			{
				var res = await _employeeService.UpdateSalaryEmployee(employeeId, salaryAmount);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}
	}
}
