namespace QuanLyNhanSu.Common
{
	public class ResultObjCommon<T>
	{
		public T? Obj { get; set; }
		public bool Success { set; get; } = true;
		public string? Message { get; set; }
	}
}
