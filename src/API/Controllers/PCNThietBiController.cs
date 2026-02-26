using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Authorize]
[Route("api/pcnThietBi")]
public class PCNThietBiController : ControllerBase
{
	private readonly PCNThietBiService _service;

	public PCNThietBiController(PCNThietBiService service)
	{
		_service = service;
	}
	[Authorize(Policy = "BacSiOrKyThuatVien")]
	[HttpGet("{pcnId}")]
	public async Task<IActionResult> DanhSach(int pcnId)
	{
		return Ok(await _service.DanhSachAsync(pcnId));
	}


	[Authorize(Roles = "Admin")]
	[HttpPost]
	public async Task<IActionResult> Them([FromBody] PCNThietBiCreateDTO dto)
	{
		await _service.ThemAsync(dto);
		return Ok(new { message = "Thêm thiết bị vào phòng chức năng thành công." });
	}


	[Authorize(Roles = "Admin")]
	[HttpDelete("{id}")]
	public async Task<IActionResult> Xoa(int id)
	{
		try
		{
			var result = await _service.XoaAsync(id);
			if (!result)
				return NotFound(new { message = "Không tồn tại PCN thiết bị." });

			return Ok(new { message = "Xóa thành công." });
		}
		catch (InvalidOperationException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}
}
