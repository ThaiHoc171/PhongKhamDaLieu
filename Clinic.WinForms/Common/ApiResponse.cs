namespace Clinic.WinForms.Common
{
	public class ApiResponse<T>
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public T Data { get; set; }
	}
	public class ApiResult<T>
	{
		public bool IsSuccess { get; set; }
		public T Data { get; set; }
		public string ErrorMessage { get; set; }

		public static ApiResult<T> Success(T data)
			=> new ApiResult<T> { IsSuccess = true, Data = data };

		public static ApiResult<T> Fail(string message)
			=> new ApiResult<T> { IsSuccess = false, ErrorMessage = message };
	}
}
