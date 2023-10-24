using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Interfaces;

namespace QuanLyNhanSu.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class DepartmentController : ControllerBase
	{
		private readonly IDepartmentService _departmentService;
		public DepartmentController(IDepartmentService departmentService)
		{
			_departmentService = departmentService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllDepartment()
		{
			try
			{
				var listData = await _departmentService.GetAllDepartment();
				return Ok(listData);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
