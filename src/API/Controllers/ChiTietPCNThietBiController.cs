using Application.DTOs;
using Application.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/chitiet-pcntb")]
[Authorize]
public class ChiTietPCNThietBiController : ControllerBase
{
	private readonly ChiTietPCNThietBiService _service;
	public ChiTietPCNThietBiController(ChiTietPCNThietBiService service)
	{
		_service = service;
	}
	[Authorize(Roles = "Admin,Nhân viên")]
	[HttpGet("combobox/{pcnId}")]
	public async Task<IActionResult> GetIdAndName(int pcnId)
	{
		var result = await _service.GetComboboxAsync(pcnId);
		return Ok(result);
	}
	[Authorize(Policy = "BacSiOrKyThuatVien")]
	[HttpGet("pcn-tb/{pcnTbId}")]
	public async Task<IActionResult> LayDanhSachTheoPCNTB(int pcnTbId)
	{
		var result = await _service.LayTheoPCNTBAsync(pcnTbId);
		return Ok(result);
	}
	[Authorize(Policy = "KyThuatVienOnly")]
	[HttpPost]
	public async Task<IActionResult> ThemChiTiet([FromBody] ChiTietPCNThietBiCreateDTO dto)
	{
		await _service.ThemChiTietAsync(dto);
		return Ok(new { message = "Thêm thiết bị vào phòng chức năng thành công." });
	}
	[Authorize(Policy = "KyThuatVienOnly")]
	[HttpPut("{id}")]
	public async Task<IActionResult> CapNhatChiTiet(
		int id,
		[FromBody] ChiTietPCNThietBiUpdateDTO dto)
	{
		var result = await _service.CapNhatChiTietAsync(id, dto);
		if (!result)
			return NotFound(new { message = "Thiết bị chi tiết không tồn tại." });
		return Ok(new { message = "Cập nhật thiết bị thành công." });
	}
	[Authorize(Policy = "KyThuatVienOnly")]
	[HttpPut("{id}/tinh-trang")]
	public async Task<IActionResult> CapNhatTinhTrangChiTiet(
		int id,
		[FromBody] TinhTrang tinhTrang)
	{
		var result = await _service.CapNhatTrangThaiAsync(id, tinhTrang);
		if (!result)
			return NotFound(new { message = "Thiết bị chi tiết không tồn tại." });
		return Ok(new { message = "Cập nhật tình trạng thiết bị thành công." });
	}
	// Chỉ Admin được xóa
	[Authorize(Roles = "Admin")]
	[HttpDelete("{id}")]
	public async Task<IActionResult> XoaChiTiet(int id)
	{
		try
		{
			var result = await _service.XoaChiTietAsync(id);
			if (!result)
				return NotFound(new { message = "Thiết bị chi tiết không tồn tại." });
			return Ok(new { message = "Xóa thiết bị thành công." });
		}
		catch (InvalidOperationException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
	}
}
