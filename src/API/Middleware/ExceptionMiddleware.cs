using Application.Common;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
namespace API.Middleware;
public static class ExceptionMiddleware
{
	public static void ConfigureExceptionHandler(this WebApplication app)
	{
		app.UseExceptionHandler(errorApp =>
		{
			errorApp.Run(async context =>
			{
				var feature = context.Features.Get<IExceptionHandlerFeature>();
				var exception = feature?.Error;
				context.Response.ContentType = "application/json";
				if (exception is ArgumentException)
				{
					context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
					await context.Response.WriteAsJsonAsync(
						ApiResponse<string>.Fail(exception.Message)
					);
					return;
				}
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				await context.Response.WriteAsJsonAsync(
					ApiResponse<string>.Fail("Lỗi hệ thống")
				);
			});
		});
	}
}