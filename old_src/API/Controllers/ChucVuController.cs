using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Services;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ChucVuController : ControllerBase
	{
		private readonly ChucVuService _chucVuService;

		public ChucVuController(ChucVuService chucVuService)
		{
			_chucVuService = chucVuService;
		}

		// GET: api/ChucVu
		[HttpGet]
		public async Task<IActionResult> LayDanhSachChucVu()
		{
			var result = await _chucVuService.DanhSachChucVuAsync();
			return Ok(result);
		}

		// GET: api/ChucVu/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> LayChucVuTheoID(int id)
		{
			var result = await _chucVuService.LayChucVuTheoIdAsync(id);

			if (result == null)
				return NotFound(new { message = "Chức vụ không tồn tại." });

			return Ok(result);
		}

		// POST: api/ChucVu
		[HttpPost]
		public async Task<IActionResult> ThemChucVu([FromBody] ThemChucVuDTO dto)
		{
			await _chucVuService.ThemChucVuAsync(dto);

			return Ok(new
			{
				message = "Thêm chức vụ thành công."
			});
		}

		// PUT: api/ChucVu/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> CapNhatChucVu(int id, [FromBody] CapNhatChucVuDTO dto)
		{
			var result = await _chucVuService.CapNhatChucVuAsync(id, dto);

			if (!result)
				return NotFound(new { message = "Chức vụ không tồn tại." });

			return Ok(new
			{
				message = "Cập nhật chức vụ thành công."
			});
		}

		// PUT: api/ChucVu/{id}/trangthai
		[HttpPut("{id}/trangthai")]
		public async Task<IActionResult> CapNhatTrangThai(int id, [FromBody] string trangThaiMoi)
		{
			var result = await _chucVuService.CapNhatTrangThaiChucVuAsync(id, trangThaiMoi);

			if (!result)
				return NotFound(new { message = "Chức vụ không tồn tại." });

			return Ok(new
			{
				message = "Cập nhật trạng thái chức vụ thành công."
			});
		}
	}
}
