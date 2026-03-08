using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] 
public class LieuTrinh_BuoiDieuTriController : ControllerBase
{
	private readonly LieuTrinh_BuoiDieuTriService _service;

	public LieuTrinh_BuoiDieuTriController(LieuTrinh_BuoiDieuTriService service)
	{
		_service = service;
	}

    [HttpPost]
    public async Task<IActionResult> TaoBuoiDieuTri(
    [FromBody] TaoBuoiDieuTriDTO dto)
    {
        try
        {
            await _service.TaoBuoiDieuTriAsync(dto);
            return Ok("Tạo buổi điều trị thành công");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [Authorize(Policy = "BacSiOrKyThuatVien")]
	[HttpPut("{buoiDieuTriID:int}/trang-thai")]
	public async Task<IActionResult> CapNhatTrangThai(
		int buoiDieuTriID,
		[FromBody] CapNhatTrangThaiBuoiDieuTriDTO dto)
	{
		var result = await _service.CapNhatTrangThaiAsync(buoiDieuTriID, dto);

		return result
			? Ok(new { message = "Cập nhật trạng thái thành công" })
			: NotFound(new { message = "Buổi điều trị không tồn tại" });
	}

	[Authorize(Policy = "BacSiOrKyThuatVien")]
	[HttpGet("lieutrinh/{lieuTrinhID:int}")]
	public async Task<IActionResult> LayTheoLieuTrinh(int lieuTrinhID)
	{
		return Ok(await _service.LayTheoLieuTrinhAsync(lieuTrinhID));
	}

	[Authorize(Roles = "Admin")]
	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		return Ok(await _service.GetAllAsync());
	}
}
