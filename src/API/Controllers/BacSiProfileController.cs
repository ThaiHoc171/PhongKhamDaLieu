using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BacSiProfileController : ControllerBase
{
	private readonly BacSiProfileService _service;

	public BacSiProfileController(BacSiProfileService service)
	{
		_service = service;
	}

	[HttpGet("{nhanVienID}")]
	public async Task<IActionResult> Get(int nhanVienID)
	{
		var result = await _service.GetByNhanVienAsync(nhanVienID);
		if (result == null)
			return NotFound();

		return Ok(result);
	}

	// 🔹 CREATE
	[HttpPost("{nhanVienID}")]
	public async Task<IActionResult> Create(int nhanVienID,	BacSiProfileRequestDTO dto)
	{
		try
		{
			await _service.TaoMoiAsync(nhanVienID, dto);
			return Ok(new { message = "Tạo hồ sơ bác sĩ thành công" });
		}
		catch (InvalidOperationException ex)
		{
			return BadRequest(ex.Message);
		}
	}

	// 🔹 UPDATE
	[HttpPut("{nhanVienID}")]
	public async Task<IActionResult> Update(int nhanVienID, BacSiProfileRequestDTO dto)
	{
		try
		{
			await _service.CapNhatAsync(nhanVienID, dto);
			return Ok(new { message = "Cập nhật hồ sơ bác sĩ thành công" });
		}
		catch (InvalidOperationException ex)
		{
			return BadRequest(ex.Message);
		}
	}
}