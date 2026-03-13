
namespace Application.DTOs
{
	public class ImportError
	{
		public int Row { get; set; }
		public string Message { get; set; } = "";
	}
	public class ImportResult
	{
		public int SuccessCount { get; set; }
		public List<ImportError> Errors { get; set; } = new List<ImportError>();
	}
}
