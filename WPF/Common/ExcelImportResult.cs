namespace WPF.Common;
public class ExcelImportError
{
	public int Row { get; set; }
	public List<string> Errors { get; set; } = new();
}
public class ExcelImportResult<T>
{
	public int TotalRows { get; set; }
	public int SuccessRows { get; set; }
	public List<T> Data { get; set; } = new();
	public List<ExcelImportError> Errors { get; set; } = new();
	public bool HasError { get; set; }
}
