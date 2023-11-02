using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Interfaces;
using QuanLyNhanSu.Models.ModelDTO;

namespace QuanLyNhanSu.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class Employee_ProjectController : ControllerBase
	{
		private readonly IEmployee_ProjectService _employee_project_service;

		public Employee_ProjectController(IEmployee_ProjectService employee_project_service)
		{
			_employee_project_service = employee_project_service;
		}

		[HttpPost]
		public async Task<IActionResult> AddEmployee_Project(ProjectEmpDTO projectEmpDTO)
		{
			try
			{
				var res = await _employee_project_service.AddEmployee_Project(projectEmpDTO.id, projectEmpDTO.employees);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
		[HttpPost]
		public async Task<IActionResult> DeleteEmployee_Project(ProjectEmpDTO projectEmpDTO)
		{
			try
			{
				var res = await _employee_project_service.DeleteEmployee_Project(projectEmpDTO.id, projectEmpDTO.employees);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
	}
}
