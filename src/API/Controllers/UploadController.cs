using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/upload")]
public class UploadController : ControllerBase
{
	private readonly FileStorage _fileStorage;

	public UploadController(FileStorage fileStorage)
	{
		_fileStorage = fileStorage;
	}

	[HttpPost("image")]
	[Authorize]
	public async Task<IActionResult> UploadImage(
		IFormFile file,
		[FromForm] string folder)
	{
		if (file == null || file.Length == 0)
			return BadRequest("File không hợp lệ");

		using var stream = file.OpenReadStream();

		var url = await _fileStorage.UploadImageAsync(
			stream,
			file.FileName,
			folder,
			file.ContentType
		);

		return Ok(new
		{
			url
		});
	}
}