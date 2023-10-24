namespace QuanLyNhanSu.Common
{
	public class ResultListCommon<T>
	{
		public T[] Obj { get; set; }
		public bool Success { set; get; } = true;
		public string? Message { get; set; }
	}
}
