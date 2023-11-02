using QuanLyNhanSu.Models;
using QuanLyNhanSu.Models.ViewModel;

namespace QuanLyNhanSu.Interfaces
{
	public interface IViewService
	{
		Task<List<View_DepartmentList>> GetView_DepartmentList();
		Task<List<View_PositionList>> GetView_PositionList();
		Task<List<View_ProjectList>> GetView_ProjectList();
		Task<List<View_SkillList>> GetView_SkillList();
	}
}
