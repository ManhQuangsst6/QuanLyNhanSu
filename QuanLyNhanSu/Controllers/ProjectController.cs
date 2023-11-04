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
		[HttpPut]
		public async Task<IActionResult> UpdateProject(Project project)
		{
			try
			{
				var res = await _projectService.UpdateProject(project);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}
		[HttpPut]
		public async Task<IActionResult> UpdateComplete(string id)
		{
			try
			{
				var res = await _projectService.UpdateComplete(id);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteProject(string id)
		{
			try
			{
				var res = await _projectService.DeleteProject(id);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetProjectsAsync(string? searchName, int? filterMonth, int? filterYear, int? pageNum, int? pageSize)
		{
			try
			{
				var res = await _projectService.GetProjectsAsync(searchName, filterMonth, filterYear, pageNum, pageSize);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}
		[HttpGet]
		public async Task<IActionResult> GetProjectById(string id)
		{
			try
			{
				var res = await _projectService.GetProjectById(id);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}
	}
}
