public class ExcelImportResult<T>
{
	public int TotalRows { get; set; }
	public int SuccessRows { get; set; }

	public List<T> Data { get; set; } = new();
	public List<string> Errors { get; set; } = new();
}