using Application.DTOs.PhienKham;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/phien-kham")]
public class PhienKhamController : ControllerBase
{
	private readonly PhienKhamService _service;

	public PhienKhamController(PhienKhamService service)
	{
		_service = service;
	}

	[HttpPost]
	public async Task<IActionResult> TaoMoi([FromBody] PhienKhamCreateDTO dto)
	{
		try
		{
			var phienKhamId = await _service.TaoMoiAsync(dto);
			return Ok(new
			{
				message = "Tạo phiên khám thành công",
				phienKhamID = phienKhamId
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}

	[HttpPut("{id:int}")]
	public async Task<IActionResult> CapNhat(int id, [FromBody] PhienKhamUpdateDTO dto)
	{
		try
		{
			await _service.CapNhatAsync(id, dto);
			return Ok(new
			{
				message = "Cập nhật phiên khám thành công"
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}
}
