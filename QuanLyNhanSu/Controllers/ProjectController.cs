using Microsoft.AspNetCore.Mvc;
using QuanLyNhanSu.Interfaces;
using QuanLyNhanSu.Models;

namespace QuanLyNhanSu.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class ProjectController : ControllerBase
	{
		private readonly IProjectService _projectService;
		public ProjectController(IProjectService projectService)
		{
			_projectService = projectService;
		}
		[HttpPost]
		public async Task<IActionResult> AddProject(Project project)
		{
			try
			{
				var res = await _projectService.AddProject(project);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetProjectsAsync()
		{
			try
			{
				var res = await _projectService.GetProjectsAsync();
				return Ok(res);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}
	}
}
