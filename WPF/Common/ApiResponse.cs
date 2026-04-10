namespace WPF.Common;

public class ApiResponse<T>
{
	public bool Success { get; set; }
	public string Message { get; set; } = "";
	public T? Data { get; set; }
}
public class ApiResult<T>
{
	public bool Success { get; set; }
	public string Message { get; set; } = "";
	public T? Data { get; set; }

	public static ApiResult<T> SuccessResult(T? data, string message = "")
	{
		return new ApiResult<T>
		{
			Success = true,
			Message = message,
			Data = data
		};
	}

	public static ApiResult<T> Fail(string message)
	{
		return new ApiResult<T>
		{
			Success = false,
			Message = message
		};
	}
}
