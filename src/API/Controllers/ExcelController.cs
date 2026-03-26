using Application.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
namespace API.Controllers;
[ApiController]
[Route("api/excel")]
[Authorize]
public class ExcelController : ControllerBase
{
	[HttpPost("sheets")]
	public IActionResult GetSheets(IFormFile file)
	{
		if (file == null || file.Length == 0)
			return BadRequest(ApiResponse<string>.Fail("File không hợp lệ"));

		using var stream = file.OpenReadStream();
		using var package = new ExcelPackage(stream);

		var sheets = package.Workbook.Worksheets
			.Select(x => x.Name)
			.ToList();

		return Ok(ApiResponse<List<string>>.SuccessResponse(sheets));
	}
}
