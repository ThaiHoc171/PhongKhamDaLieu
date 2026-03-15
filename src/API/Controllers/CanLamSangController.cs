using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Services;
namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CanLamSangController : ControllerBase
{
	private readonly CanLamSangService _service;
	public CanLamSangController(CanLamSangService service)
	{
		_service = service;
	}
	[Authorize(Policy = "BacSiOrKyThuatVien")]
	[HttpGet("combobox")]
	public async Task<IActionResult> GetComboboxAsync()
	{
		return Ok(await _service.GetComboboxAsync());
	}
	[Authorize(Policy = "BacSiOrKyThuatVien")]
	[HttpGet("paged")]
	public async Task<IActionResult> LayDanhSach([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 15)
		=> Ok(await _service.DanhSachCanLamSangAsync(pageNumber,pageSize));
	[Authorize(Policy = "BacSiOrKyThuatVien")]
	[HttpGet("{id}")]
	public async Task<IActionResult> LayTheoId(int id)
	{
		var result = await _service.LayCanLamSangTheoIdAsync(id);
		return result == null
			? NotFound(new { message = "Cận lâm sàng không tồn tại." })
			: Ok(result);
	}
	[Authorize(Roles = "Admin")]
	[HttpPost]
	public async Task<IActionResult> Them([FromBody] CanLamSangRequestDTO dto)
	{
		await _service.ThemCanLamSangAsync(dto);
		return Ok(new { message = "Thêm cận lâm sàng thành công." });
	}
	[Authorize(Roles = "Admin")]
	[HttpPut("{id}")]
	public async Task<IActionResult> CapNhat(int id, [FromBody] CanLamSangRequestDTO dto)
	{
		var result = await _service.CapNhatCanLamSangAsync(id, dto);
		return result
			? Ok(new { message = "Cập nhật thành công." })
			: NotFound(new { message = "Cận lâm sàng không tồn tại." });
	}
	[Authorize(Roles = "Admin")]
	[HttpPut("{id}/ngungsudung")]
	public async Task<IActionResult> NgungSuDung(int id)
	{
		var result = await _service.CapNhatTrangThaiAsync(id, "Ngưng sử dụng");
		return result
			? Ok(new { message = "Ngưng sử dụng thành công." })
			: NotFound(new { message = "Cận lâm sàng không tồn tại." });
	}
	[Authorize(Roles = "Admin")]
	[HttpPut("{id}/kichhoat")]
	public async Task<IActionResult> KichHoat(int id)
	{
		var result = await _service.CapNhatTrangThaiAsync(id, "Hoạt động");
		return result
			? Ok(new { message = "Kích hoạt thành công." })
			: NotFound(new { message = "Cận lâm sàng không tồn tại." });
	}
	[HttpGet("timkiem")]
	public async Task<IActionResult> TimTheoTen([FromQuery] string tenCLS)
	{
		if (string.IsNullOrWhiteSpace(tenCLS))
			return BadRequest(new { message = "Tên thiết bị không hợp lệ." });
		var result = await _service.TimTheoTenAsync(tenCLS);
		return Ok(result);
	}
}
